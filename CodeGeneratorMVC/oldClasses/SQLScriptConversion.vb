Imports System.Collections.Generic
Imports IRICommonObjects.Words
Imports System.Windows
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Public Class SQLScriptConversion
    Public Shared _dictionaryOfSQLToVBType As Dictionary(Of String, String)
    Public Shared ReadOnly Property DictionaryOfSQLToVBType() As Dictionary(Of String, String)
        Get
            If _dictionaryOfSQLToVBType Is Nothing Then
                _dictionaryOfSQLToVBType = New Dictionary(Of String, String)

                _dictionaryOfSQLToVBType.Add("smallint", "Int16")
                _dictionaryOfSQLToVBType.Add("tinyint", "Byte")
                _dictionaryOfSQLToVBType.Add("int", "Integer")
                _dictionaryOfSQLToVBType.Add("bigint", "Int64")

                _dictionaryOfSQLToVBType.Add("money", "Decimal")
                _dictionaryOfSQLToVBType.Add("smallmoney", "Double")
                _dictionaryOfSQLToVBType.Add("decimal", "Double")
                _dictionaryOfSQLToVBType.Add("numeric", "Double")
                _dictionaryOfSQLToVBType.Add("float", "Double")
                _dictionaryOfSQLToVBType.Add("real", "Single")

                _dictionaryOfSQLToVBType.Add("smalldatetime", "DateTime")
                _dictionaryOfSQLToVBType.Add("datetime", "DateTime")
                _dictionaryOfSQLToVBType.Add("datetime2", "DateTime")
                _dictionaryOfSQLToVBType.Add("date", "DateTime")

                _dictionaryOfSQLToVBType.Add("uniqueidentifier", "String")
                _dictionaryOfSQLToVBType.Add("guid", "String")

                _dictionaryOfSQLToVBType.Add("nvarchar", "String")
                _dictionaryOfSQLToVBType.Add("varchar", "String")
                _dictionaryOfSQLToVBType.Add("nchar", "String")
                _dictionaryOfSQLToVBType.Add("char", "String")
                _dictionaryOfSQLToVBType.Add("ntext", "String")
                _dictionaryOfSQLToVBType.Add("text", "String")

                _dictionaryOfSQLToVBType.Add("bit", "Boolean")

                _dictionaryOfSQLToVBType.Add("byte", "Image")

                _dictionaryOfSQLToVBType.Add("binary", "Byte()")
                _dictionaryOfSQLToVBType.Add("varbinary", "Byte()")
                _dictionaryOfSQLToVBType.Add("image", "Byte()")
            End If
            Dim tempDictionary As New Dictionary(Of String, Type)
            tempDictionary.Add("test", GetType(Integer))
            For Each pair As KeyValuePair(Of String, Type) In tempDictionary

                'MessageBox.Show(pair.Key & " " & pair.Value.ToString())

            Next
            Return _dictionaryOfSQLToVBType
        End Get
    End Property


    Public Shared Function generateObjects(ByVal fileName As String) As List(Of ProjectClass)
        Dim myRead As System.IO.StreamReader = System.IO.File.OpenText(fileName)

        Dim myString As String = myRead.ReadToEnd()
        ' remove comments (--This is a sample comment)
        While myString.IndexOf("--") > -1
            Dim count As Integer = myString.IndexOf(vbCrLf, myString.IndexOf("--")) - myString.IndexOf("--")
            If count < 0 Then count = myString.Length - myString.IndexOf("--")
            myString = myString.Remove(myString.IndexOf("--"), count)
        End While
        Dim ID As Integer = StaticVariables.Instance.HighestProjectClassID + 1
        Dim retList As New List(Of ProjectClass)
        Dim pAliasGroupClass As ProjectClass = Nothing
        'remove escape characters [ and ]
        myString = myString.Replace("[", "")
        myString = myString.Replace("]", "")

        ' remove issues that may arise from line breaks.
        myString = myString.Replace(vbCrLf, " ")
        myString = myString.Replace(Chr(10), " ")
        myString = myString.Replace(Chr(13), " ")

        While myString.IndexOf("CREATE TABLE", StringComparison.OrdinalIgnoreCase) > -1
            ' for each line remove create table statement.
            myString = myString.Remove(0, myString.IndexOf("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
            myString = myString.Remove(0, 12)
            Dim extractedText As String = ExtractCreateTableStatement(myString)
            Dim pClass As ProjectClass = ExtractProjectClass(extractedText, ID)
            'If StaticVariables.Instance.DALs.Count > 0 Then
            'pClass.DALClassVariable = StaticVariables.Instance.DALs(0)
            'End If
            'If StaticVariables.Instance.MasterPages.Count > 0 Then
            ' pClass.MasterPage = StaticVariables.Instance.MasterPages(0)
            'End If
            'pClass.BaseClass = StaticVariables.Instance.BaseClasses(0)
            'If StaticVariables.Instance.NameSpaceNames.Count > 0 Then
            'pClass.NameSpaceName = StaticVariables.Instance.NameSpaceNames(0)
            'End If

            '' See if this is the Alias Group Table/Object
            pClass.IsAssociatedWithAliasGroup = pClass.Name.Capitalized.ToLower().Contains("alias")
            If pClass.IsAssociatedWithAliasGroup Then
                pAliasGroupClass = pClass
            End If

            retList.Add(pClass)
            ID += 1
            'myString = myString.Remove(0, myString.IndexOf("CREATE TABLE"))
        End While

        '' If we did not find a Alias Group Table, create one.
        If pAliasGroupClass Is Nothing Then
            pAliasGroupClass = New ProjectClass()
            pAliasGroupClass.ID = ID
            pAliasGroupClass.IsAssociatedWithAliasGroup = True
            pAliasGroupClass.NameString = "AliasGroup"
            pAliasGroupClass.Summary = "This class contains the aliases that will be used in this site."
            pAliasGroupClass.DatabaseTableName = "AliasGroup"
            StaticVariables.Instance.ListOfProjectClasses.Add(pAliasGroupClass)
        End If

        Dim doesVariableExist As Boolean = False
        For Each pClass As ProjectClass In retList
            doesVariableExist = False
            Dim _ID As Integer = 1

            For Each variableSQLText As String In pClass.OriginalSQLText.Split(","c)
                Try
                    Dim newVar As ClassVariable = ExtractClassVariable(pClass, variableSQLText.Trim(), _ID)
                    If newVar IsNot Nothing Then
                        pClass.ClassVariables.Add(newVar)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                _ID += 1
            Next
            ' create the alias group variable for this object.
            Dim myVariable As New ClassVariable(pAliasGroupClass, pClass.Name.Capitalized, _
                                                StaticVariables.Instance.GetDataType("namealias"), _
                                                False, False, False, False, False, True, True, _
                                                pAliasGroupClass.ClassVariables.Count, _
                                                False, False, "String", -1, pClass.Name.Capitalized)
            ' check to see if alias already exists
            For Each cVariable As ClassVariable In pAliasGroupClass.ClassVariables
                If cVariable.Name.ToLower().CompareTo(myVariable.Name.ToLower()) = 0 Then
                    doesVariableExist = True
                    Exit For
                End If
            Next
            If Not doesVariableExist Then
                ' add alias for this object to the Alias Group
                pAliasGroupClass.ClassVariables.Add(myVariable)
            End If
        Next

        '' Check for associative classes. These are classes that 
        ''   are only an association between two other classes.
        Dim IsAssociativeClass As Boolean
        Dim failedObjects As String = ""
        For Each pClass As ProjectClass In retList
            IsAssociativeClass = True
            For Each cVar As ClassVariable In pClass.ClassVariables
                If Not cVar.IsIDField Then
                    IsAssociativeClass = IsAssociativeClass And cVar.isForeignKey
                    If Not IsAssociativeClass Then Exit For
                End If
            Next

            If IsAssociativeClass Then
                Try
                    pClass.IsAssociateEntitiy = True
                    pClass.ClassObjectIsNotNeeded = True
                    '' move classes to appropriate objects.
                    AddAssociationsToAppropriateClasses(pClass, retList)
                Catch
                    failedObjects &= pClass.Name.Capitalized & vbCrLf
                End Try
            End If
        Next
        If failedObjects <> "" Then
            MsgBox(failedObjects)
        End If

        '' Make sure we have the default Add, Edit, and Delete Aliases
        Dim listOfDefaultAliases() As String = {"Add", "Edit", "Delete"} '{"AddAlias", "EditAlias", "DeleteAlias"}
        For Each cVariableString As String In listOfDefaultAliases
            doesVariableExist = False
            For Each cVariable As ClassVariable In pAliasGroupClass.ClassVariables
                If cVariable.Name.ToLower().CompareTo(cVariableString.ToLower()) = 0 Then
                    doesVariableExist = True
                    Exit For
                End If
            Next
            ' Create Aliases needed based off of classes
            If Not doesVariableExist Then
                pAliasGroupClass.ClassVariables.Add(New ClassVariable(pAliasGroupClass, cVariableString, _
                                                        StaticVariables.Instance.GetDataType("namealias"), False, False, False, _
                                                        False, False, True, True, pAliasGroupClass.ClassVariables.Count, _
                                                        False, False, "String", -1, "NA"))
            End If
        Next

        Return retList
    End Function
    Private Shared Sub AddAssociationsToAppropriateClasses(assoEntityClass As ProjectClass, ByRef lst As List(Of ProjectClass))
        Dim varsCompleted As New List(Of ClassVariable)
        For Each cVar As ClassVariable In assoEntityClass.ClassVariables
            For Each cVarInner As ClassVariable In assoEntityClass.ClassVariables
                If cVar <> cVarInner Then
                    Dim pC As ProjectClass = getClass(cVar.ParameterType)
                    pC.ClassVariables.Add(New ClassVariable(pC, New NameAlias(cVar.Name, True).PluralAndCapitalized, cVar.ParameterType, _
                                                        True, True, True, cVar.IsPropertyInherited, False, False, False, pC.ClassVariables.Count + 1, _
                                                                       cVar.IsDatabaseBound, False, cVar.DatabaseType, _
                                                                       cVar.LengthOfDatabaseProperty, cVar.DatabaseColumnName))
                End If
            Next
        Next

        'Dim assocClasses As New List(Of ProjectClass)
        'Dim assocVariables As New List(Of ClassVariable)
        'For Each cVar As ClassVariable In assoClass.ClassVariables
        '    If cVar.isForeignKey Then
        '        Dim newProjClass As ProjectClass = getClass(cVar)
        '        If Not assocClasses.Contains(newProjClass) Then
        '            assocClasses.Add(newProjClass)
        '        End If
        '        If Not assocVariables.Contains(cVar) Then
        '            assocVariables.Add(cVar)
        '        End If
        '    End If
        'Next
        'For Each pC As ProjectClass In assocClasses
        '    Dim myAssocClasses As New List(Of ProjectClass)
        '    For Each cVar As ClassVariable In assocVariables
        '        ' Make sure not to add same class to class 
        '        '   Only add other class types for binding
        '        If pC.ID <> cVar.ParameterType.AssociatedProjectClass.ID Then
        '            pC.ClassVariables.Add(New ClassVariable(pC, New NameAlias(cVar.Name).PluralAndCapitalized, cVar.ParameterType, _
        '                                                    True, True, False, cVar.IsPropertyInherited, False, False, pC.ClassVariables.Count + 1, _
        '                                                    cVar.IsDatabaseBound, cVar.DatabaseType, cVar.LengthOfDatabaseProperty, _
        '                                                    cVar.DatabaseColumnName))
        '            myAssocClasses.Add(cVar.ParameterType.AssociatedProjectClass)
        '        End If
        '    Next
        '    pC.AssociatedClasses = myAssocClasses
        'Next

    End Sub
    Private Shared Function getClass(cVar As ClassVariable) As ProjectClass
        If cVar.ParameterType.AssociatedProjectClass Is Nothing Then
            cVar.ParameterType = StaticVariables.Instance.GetDataType(cVar.ParameterType.Name)
        End If
        Return cVar.ParameterType.AssociatedProjectClass
    End Function
    Private Shared Function getClass(pType As DataType) As ProjectClass
        If pType.AssociatedProjectClass Is Nothing Then
            pType = StaticVariables.Instance.GetDataType(pType.Name)
        End If
        Return pType.AssociatedProjectClass
    End Function
    Private Shared Function ExtractCreateTableStatement(ByVal text As String) As String
        Dim parenthesisLevel As Integer = 0
        Dim indexOfOpeningParenthesis As Integer = -1
        Dim indexOfClosingParenthesis As Integer = -1
        Dim textForTable As String = ""
        Do
            indexOfOpeningParenthesis = text.IndexOf("(")
            indexOfClosingParenthesis = text.IndexOf(")")
            If indexOfOpeningParenthesis < indexOfClosingParenthesis AndAlso indexOfOpeningParenthesis > -1 Then
                parenthesisLevel += 1
                textForTable &= text.Substring(0, indexOfOpeningParenthesis + 1)
                text = text.Remove(0, indexOfOpeningParenthesis + 1)
            ElseIf indexOfClosingParenthesis < indexOfOpeningParenthesis AndAlso indexOfClosingParenthesis > -1 Then
                parenthesisLevel -= 1
                textForTable &= text.Substring(0, indexOfClosingParenthesis + 1)
                text = text.Remove(0, indexOfClosingParenthesis + 1)
            ElseIf indexOfClosingParenthesis > -1 Then
                parenthesisLevel -= 1
                textForTable &= text.Substring(0, indexOfClosingParenthesis + 1)
                text = text.Remove(0, indexOfClosingParenthesis + 1)
            Else
                parenthesisLevel -= 1
            End If

        Loop While parenthesisLevel > 0 AndAlso text.Length > 0

        'While parenthesisLevel > 0 AndAlso text.Length > 0

        '    indexOfOpeningParenthesis = text.IndexOf("(")
        '    indexOfClosingParenthesis = text.IndexOf(")")
        '    If indexOfOpeningParenthesis < indexOfClosingParenthesis AndAlso indexOfOpeningParenthesis > -1 Then
        '        parenthesisLevel += 1
        '        textForTable &= text.Substring(0, indexOfOpeningParenthesis + 1)
        '        text = text.Remove(0, indexOfOpeningParenthesis + 1)
        '    ElseIf indexOfClosingParenthesis < indexOfOpeningParenthesis AndAlso indexOfClosingParenthesis > -1 Then
        '        parenthesisLevel -= 1
        '        textForTable &= text.Substring(0, indexOfClosingParenthesis + 1)
        '        text = text.Remove(0, indexOfClosingParenthesis + 1)
        '    ElseIf indexOfClosingParenthesis > -1 Then
        '        parenthesisLevel -= 1
        '        textForTable &= text.Substring(0, indexOfClosingParenthesis + 1)
        '        text = text.Remove(0, indexOfClosingParenthesis + 1)
        '    Else
        '        parenthesisLevel -= 1
        '    End If

        'End While
        If textForTable.Length > 0 AndAlso textForTable(textForTable.Length - 1) = ")" Then
            textForTable = textForTable.Remove(textForTable.Length - 1, 1)
        End If

        Return textForTable
    End Function
    Public Shared _VariablesWithCommaInParams As String() = {"IDENTITY", "PRIMARY", "DECIMAL"}
    Private Shared Function ExtractProjectClass(ByRef text As String, ByVal ID As Integer) As ProjectClass
        Dim retProjectClass As New ProjectClass
        Dim parenthesisLevel As Integer = 1


        Dim textName As String = text.Substring(0, text.IndexOf("(")).Trim()

        'look to see if owner.table syntax is used
        'get index of period.
        Dim indexOfPeriod As Integer = textName.IndexOf(".")
        'remove owner if used.
        If indexOfPeriod >= 0 Then textName = textName.Remove(0, indexOfPeriod + 1)
        retProjectClass.DatabaseTableName = textName
        Dim newString As String = NameAlias.getTextWithFormatting(textName)
        Dim singularVersion As String = IRICommonObjects.Words.PluralityDictionary.getPluralityInverse(newString)
        retProjectClass.Name = New NameAlias(singularVersion)
        ' Get rid of opening ( for Create table statement.)
        text = text.Remove(0, text.IndexOf("(") + 1)
        Dim indexOfOpeningParenthesis As Integer = -1
        Dim indexOfClosingParenthesis As Integer = -1
        Dim indexOFNextComma As Integer = -1
        Dim textTilParen As String = ""
        'Dim indexOfComma As Integer = -1


        ' Remove any instances of extra parenthesis that exist behind certain variables,
        '    such as PRIMARY KEY or IDENTITY
        'Dim indexToStart As Integer = 0
        Dim indexOfVar As Integer = 0
        For Each VarCheck As String In _VariablesWithCommaInParams
            indexOfVar = text.IndexOf(VarCheck, indexOfVar, StringComparison.OrdinalIgnoreCase)
            While indexOfVar >= 0
                If indexOfVar > -1 Then
                    indexOfOpeningParenthesis = text.IndexOf("(", indexOfVar)
                    indexOFNextComma = text.IndexOf(",", indexOfVar)
                    indexOfClosingParenthesis = text.IndexOf(")", indexOfVar)
                    If indexOFNextComma < indexOfOpeningParenthesis Then
                        indexOfOpeningParenthesis = indexOFNextComma
                        indexOfClosingParenthesis = indexOFNextComma - 1
                    End If
                    Dim lengthOfText As Integer = indexOfOpeningParenthesis - indexOfVar
                    Dim lengthOfTextTilEndParan As Integer = indexOfClosingParenthesis - indexOfVar - (VarCheck.Length - 1)

                    If lengthOfText < 0 Then
                        lengthOfText = VarCheck.Length
                        lengthOfTextTilEndParan = 0
                    End If

                    textTilParen = text.Substring(indexOfVar, lengthOfText) & " "

                    If Not textContainsKnownSQLType(textTilParen) Then
                        text = text.Remove(indexOfVar + VarCheck.Length, lengthOfTextTilEndParan)
                    End If
                    Dim newStart As Integer = indexOfVar + lengthOfText
                    If newStart < text.Length Then
                        indexOfVar = text.IndexOf(VarCheck, newStart, StringComparison.OrdinalIgnoreCase)
                    Else
                        indexOfVar = -1
                    End If
                End If
            End While
            indexOfVar = 0
        Next

        Dim _ID As Integer = 0
        StaticVariables.Instance.addDerivedTypeToSystem(retProjectClass)
        retProjectClass.OriginalSQLText = text
        retProjectClass.ID = ID
        Return retProjectClass
    End Function
    ''' <summary>
    ''' Check to see if text contains a key word. Such as int or nvarchar
    ''' </summary>
    ''' <param name="textToParse"></param>
    ''' <returns>True if key word found in keys of dictionaryOfSQLToVBType is in the gevn text followed immediately by a space (" ")</returns>
    ''' <remarks></remarks>
    Shared Function textContainsKnownSQLType(ByVal textToParse As String) As Boolean
        textToParse = textToParse.Replace("(", " (")
        textToParse = textToParse.PadLeft(1).PadRight(1)
        textToParse = textToParse.ToLower()
        For Each keyWord As String In DictionaryOfSQLToVBType.Keys
            If textToParse.Contains(" " & keyWord & " ") Then ' OrElse textToParse.Contains(keyWord & "(")
                Return True
            End If
        Next
        Return False
    End Function
    Private Shared Function ExtractClassVariable(ByRef retProjectClass As ProjectClass, ByRef text As String, ByVal ID As Integer) As ClassVariable
        ' exit function if we can not create a parameter from this entry.
        If Not textContainsKnownSQLType(text) Then Return Nothing
        'eliminate extra spaces 
        text = text.Replace(" (", "(")
        Dim parameters As String() = text.Split(" "c)

        ' if parameters is not long enough, it must not be a valid entry
        If Not parameters.Length > 1 Then Return Nothing

        ' strip empty (extra) spaces from each parameter
        For Each param As String In parameters
            param = param.Trim()
        Next
        Dim parameterName As String = ""
        Dim fieldName As String = parameters(0)
        Dim sqlType As String = parameters(1)
        Dim isRequired As Boolean = False
        If containsValue(parameters, "not", Sensitivity.CaseInsensitive) AndAlso _
            containsValue(parameters, "null", Sensitivity.CaseInsensitive) Then
            isRequired = True
        End If
        Dim dataType As DataType = Nothing
        Dim isInHerited As Boolean = False
        ' check to see if second parameter, which should be the type declaration, contains parenthesis.
        Dim indexOfFirstParen As Integer = sqlType.IndexOf("(")
        Dim indexOfSecondParen As Integer = sqlType.IndexOf(")")
        Dim length As Integer = -1
        ' if contains parenthesies
        If indexOfFirstParen > 0 And indexOfFirstParen < indexOfSecondParen Then
            ' get string including parenthesies
            Dim lengthString As String = sqlType.Substring(indexOfFirstParen, indexOfSecondParen - indexOfFirstParen + 1)
            ' remove first parentheses
            lengthString = lengthString.Remove(0, 1)
            ' remove second, ending parentheses
            lengthString = lengthString.Remove(lengthString.Length - 1, 1)
            ' try to convert to integer.
            'replace paramenter without parenthetical text.
            sqlType = sqlType.Remove(indexOfFirstParen, indexOfSecondParen - indexOfFirstParen + 1)

            If Integer.TryParse(lengthString, length) Then
                ' worked
            ElseIf lengthString.ToLower() = "max" Then
                ' technically max is only available for nvarchar, but will check just in case.
                If sqlType.ToLower() = "nvarchar" Then
                    length = 4000
                Else ' char OR binary
                    length = 8000
                End If
            Else
                ' if fails default to -1
                length = -1
            End If
        End If
        'MsgBox(parameters(0).Substring(parameters(0).Length - 2, 2))
        'see if text is primary key.

        Dim isForeignKey As Boolean = False

        Dim isID As Boolean = False
        Dim fieldIdentified As Boolean = False
        If fieldName.Substring(fieldName.Length - 2, 2).ToLower().CompareTo("id") = 0 Then
            If fieldName.ToLower.Substring(fieldName.Length - 2, 2) = "id" Then
                parameterName = fieldName.Remove(fieldName.Length - 2, 2)
            ElseIf fieldName.ToLower.Substring(fieldName.Length - 4, 4) = "idfk" Then
                parameterName = fieldName.Remove(fieldName.Length - 4, 4)
            ElseIf fieldName.ToLower.Substring(fieldName.Length - 2, 2) = "fk" Then
                parameterName = fieldName.Remove(fieldName.Length - 2, 2)
            Else
                parameterName = fieldName
            End If
            If (text.ToLower().Contains("primary") OrElse text.ToLower().Contains("identity")) Then
                'This is the primary key
                isID = True
                ' see if text is foreign key
            ElseIf text.ToLower.Contains("foreign key") _
                    OrElse fieldName.Substring(fieldName.Length - 2, 2).ToLower().CompareTo("fk") = 0 Then
                isForeignKey = True
            ElseIf parameterName.ToLower() = retProjectClass.Name.Text.ToLower() Then
                ' this is an ID that was not declared inline
                isID = True
            Else
                ' this is a foreign key that was not declared inline
                isForeignKey = True
            End If

            If isID Then
                parameterName = "ID"
                isInHerited = True
            ElseIf isForeignKey Then
                Dim indexOfRef As Integer = getReferenceIndex(parameters)
                ' Check to see of the row references another table with an inline declaration
                If indexOfRef > -1 Then
                    Dim refString As String = parameters(indexOfRef + 1)
                    Dim tblName As String = refString.Substring(0, refString.IndexOf("("c))
                    Dim objName As String = IRICommonObjects.Words.PluralityDictionary.getPluralityInverse(tblName)
                    dataType = StaticVariables.Instance.GetDataType(objName)
                Else
                    ' otherwise use the FieldName to try and guess the object
                    'if name ends in ID then remove the ID.

                    'set datatype based on name
                    dataType = StaticVariables.Instance.GetDataType(parameterName)
                    If dataType Is Nothing Then
                        '  Check to see if it can be mapped to a user.
                        '   make sure that parameter is long enough to check for byid on the end.
                        If fieldName.Length > 2 AndAlso fieldName.Length > 4 Then
                            If fieldName.Substring(fieldName.Length - 4, 4).ToLower().CompareTo("byid") = 0 Then
                                dataType = StaticVariables.Instance.GetDataType("user")
                            End If
                        Else
                            dataType = StaticVariables.Instance.GetDataType(sqlType.ToLower())
                        End If
                    End If
                End If
            Else
                ' Should not hit this code
                Throw New Exception("Field could not be identified.")
            End If
        Else
            parameterName = fieldName
        End If
        If dataType Is Nothing AndAlso DictionaryOfSQLToVBType.ContainsKey(sqlType.ToLower()) Then
            dataType = StaticVariables.Instance.GetDataType(DictionaryOfSQLToVBType(sqlType.ToLower()))
        End If
        If dataType Is Nothing Then
            dataType = StaticVariables.Instance.GetDataType(sqlType.ToLower())
        End If
        Return New ClassVariable(retProjectClass, parameterName, dataType, isForeignKey, False, False, isID, _
                                 isInHerited, parameterName <> "ID", _
                                 parameterName <> "ID", ID, True, isRequired, sqlType, length, fieldName)
    End Function

    Private Shared Function getReferenceIndex(parameters As String()) As Integer
        Dim ind As Integer = 0
        For Each p As String In parameters
            If p.Equals("references", StringComparison.OrdinalIgnoreCase) Then
                Return ind
            End If
            ind += 1
        Next
        Return -1
    End Function
    Private Enum Sensitivity
        CaseMatters
        CaseInsensitive
    End Enum
    Private Shared Function containsValue(arr As String(), value As String, sens As Sensitivity) As Boolean
        For Each s As String In arr
            If sens = Sensitivity.CaseInsensitive Then
                If value.ToLower() = s.ToLower() Then
                    Return True
                ElseIf value = s Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

End Class
