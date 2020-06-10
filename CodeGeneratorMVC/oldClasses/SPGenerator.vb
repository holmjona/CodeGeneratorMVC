Imports System.Windows.Forms
Imports System.Text

Public Class SPGenerator
    Const tabSize As Integer = 5
    Public Shared Function getSprocText(ByVal pClass As ProjectClass, ByVal copyResultsToClipboard As Boolean, ByVal creatorName As String) As String
        Dim strB As New StringBuilder
        If pClass.Name.Text.Length > 0 Then
            strB.Append(getSummaryText(creatorName, "Add a new  " & pClass.Name.Capitalized & " to the database."))
            strB.Append(getAddSprocText(pClass))
            strB.Append(getGrantAccessScript("sproc_" & pClass.Name.Capitalized & "Add", pClass.DALClassVariable.EditOnlyConnectionstring.UserName))

            strB.Append(getSummaryText(creatorName, "Update " & pClass.Name.Capitalized & " in the database."))
            strB.Append(getUpdateSprocText(pClass))
            strB.Append(getGrantAccessScript("sproc_" & pClass.Name.Capitalized & "Update", pClass.DALClassVariable.EditOnlyConnectionstring.UserName))

            strB.Append(getSummaryText(creatorName, "Retrieve specific " & pClass.Name.Capitalized & " from the database."))
            strB.Append(getSingleItemSprocText(pClass))
            strB.Append(getGrantAccessScript("sproc" & pClass.Name.Capitalized & "Get", pClass.DALClassVariable.ReadOnlyConnectionString.UserName))

            strB.Append(getSummaryText(creatorName, "Retrieve all " & pClass.Name.PluralAndCapitalized & " from the database."))
            strB.Append(getAllItemsSprocText(pClass))
            strB.Append(getGrantAccessScript("sproc" & pClass.Name.PluralAndCapitalized & "GetAll", pClass.DALClassVariable.ReadOnlyConnectionString.UserName))

            strB.Append(getSummaryText(creatorName, "Remove specific " & pClass.Name.Capitalized & " from the database."))
            strB.Append(getRemoveItemSprocText(pClass))
            strB.Append(getGrantAccessScript("sproc_" & pClass.Name.Capitalized & "Remove", pClass.DALClassVariable.EditOnlyConnectionstring.UserName))

            'TODO: Add SPROCs for Foreign Key Relations.
            'For Each classVar As ClassVariable In pClass.ClassVariables
            '    If classVar.IsForeignKey Then
            '        
            '        strB.Append(getSummaryText(creatorName, "Retrieve all " & pClass.Name.PluralAndCapitalized & " from the database for a specific " _
            '                                   & "TYPE OF OBJECT"))
            '        strB.Append(getAllItemsSprocText(pClass))
            '        strB.Append(getGrantAccessScript("sproc" & pClass.Name.PluralAndCapitalized & "GetAll", pClass.DALClassVariable.ReadOnlyConnectionString.UserName))
            '    End If
            'Next

            If copyResultsToClipboard Then
                Clipboard.Clear()
                Clipboard.SetText(strB.ToString())
            End If
        Else
            If pClass.Name.Text.Length = 0 Then
                MessageBox.Show("You must provide an object name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

        Return strB.ToString()
    End Function
    Private Shared Function getSummaryText(ByVal creator As String, description As String) As String
        Return String.Format("-- ============================================={4}" & _
                             "-- Author:{3}{3}{0}{4}" & _
                             "-- Create date:{3}{1}{4}" & _
                             "-- Description:{3}{2}{4}" & _
                             "-- ============================================={4}", _
                             creator, Date.Now.ToString("d MMM yyyy"), description, vbTab, vbCrLf)

    End Function
    Private Shared Function getGrantAccessScript(ByVal StoredProcedureName As String, ByVal userName As String) As String
        Dim retString As String = ""
        retString &= "GRANT EXECUTE ON dbo." & StoredProcedureName & " TO " & userName & vbCrLf
        retString &= "GO" & vbCrLf
        Return retString
    End Function
    Public Shared Function getUpdateSprocText(ByVal pClass As ProjectClass) As String
        Dim retString As String = "CREATE PROCEDURE dbo.sproc_" & pClass.Name.Capitalized & "Update" & vbCrLf
        Dim count As Integer = 0
        For Each classVar As ClassVariable In pClass.ClassVariables
            count += 1
            retString &= "@" & classVar.DatabaseColumnName & " " & classVar.DatabaseTypeWithLength
            If count < pClass.ClassVariables.Count Then
                retString &= ","
            End If
            retString &= vbCrLf
        Next
        retString &= "AS" & vbCrLf
        retString &= Space(tabSize) & "UPDATE " & pClass.DatabaseTableName & vbCrLf
        retString &= Space(tabSize * 2) & "SET" & vbCrLf
        count = 0

        For Each classVar As ClassVariable In pClass.ClassVariables
            count += 1
            If pClass.ValueVariable <> classVar Then

                retString &= Space(tabSize * 3) & classVar.DatabaseColumnName & " = @" & classVar.DatabaseColumnName
                If count < pClass.ClassVariables.Count Then
                    retString &= ","
                End If
                retString &= vbCrLf
            End If
        Next

        retString &= Space(tabSize * 2) & "WHERE " & pClass.ValueVariable.DatabaseColumnName & " = @" & pClass.ValueVariable.DatabaseColumnName & vbCrLf & "GO" & vbCrLf & vbCrLf
        Return retString
    End Function
    Public Shared Function getAddSprocText(ByVal pClass As ProjectClass) As String
        Dim retString As String = "CREATE PROCEDURE dbo.sproc_" & pClass.Name.Capitalized & "Add" & vbCrLf
        Dim count As Integer = 0
        For Each classVar As ClassVariable In pClass.ClassVariables
            count += 1
            If classVar = pClass.ValueVariable Then
                retString &= "@" & classVar.DatabaseColumnName & " " & classVar.DatabaseType & " OUTPUT"
            Else
                retString &= "@" & classVar.DatabaseColumnName & " " & classVar.DatabaseTypeWithLength
            End If
            If count < pClass.ClassVariables.Count Then
                retString &= ","
            End If
            retString &= vbCrLf

        Next
        retString &= "AS" & vbCrLf
        retString &= Space(tabSize) & "INSERT INTO " & pClass.DatabaseTableName & "("
        count = 0

        For Each classVar As ClassVariable In pClass.ClassVariables
            count += 1
            If classVar <> pClass.ValueVariable Then
                retString &= classVar.DatabaseColumnName
                If count < pClass.ClassVariables.Count Then
                    retString &= ","
                Else
                    retString &= ")"
                End If
            End If
        Next
        retString &= vbCrLf & Space(tabSize * 3) & "VALUES("
        count = 0
        For Each classVar As ClassVariable In pClass.ClassVariables
            count += 1
            If classVar <> pClass.ValueVariable Then
                retString &= "@" & classVar.DatabaseColumnName
                If count < pClass.ClassVariables.Count Then
                    retString &= ","
                Else
                    retString &= ")"
                End If
                If count Mod 5 = 0 Then
                    retString &= vbCrLf & Space(tabSize * 3)
                End If
            End If
        Next
        retString &= vbCrLf
        retString &= Space(tabSize) & "SET @" & pClass.ValueVariable.DatabaseColumnName & " = @@IDENTITY" & vbCrLf & "GO" & vbCrLf & vbCrLf
        Return retString
    End Function
    Public Shared Function getAllItemsSprocText(ByVal pClass As ProjectClass) As String
        Dim retString As String = "CREATE PROCEDURE dbo.sproc" & pClass.Name.PluralAndCapitalized & "GetAll" & vbCrLf & "AS" & vbCrLf
        retString &= getSelectText("SELECT * FROM " & pClass.DatabaseTableName)
        Return retString
    End Function
    Public Shared Function getSingleItemSprocText(ByVal pClass As ProjectClass) As String
        'TODO: Fix this to handle multiple Primary Keys
        Dim retString As String = "CREATE PROCEDURE dbo.sproc" & pClass.Name.Capitalized & "Get" & vbCrLf
        retString &= "@" & pClass.ValueVariable.DatabaseColumnName & " " & pClass.ValueVariable.DatabaseType & vbCrLf & "AS" & vbCrLf
        retString &= getSelectText("SELECT * FROM " & pClass.DatabaseTableName & vbCrLf & _
                                   Space(tabSize) & "WHERE " & pClass.ValueVariable.DatabaseColumnName & " = @" & pClass.ValueVariable.DatabaseColumnName)
        Return retString
    End Function
    Public Shared Function getRemoveItemSprocText(ByVal pClass As ProjectClass) As String
        'TODO: Fix this to handle multiple Primary Keys
        Return String.Format("CREATE PROCEDURE dbo.sproc_{0}Remove{5}@{1} {2}{5}AS{5}BEGIN{5}" _
                             & "{4}DELETE FROM {3}{5}{4}{4}WHERE {1} = @{1}{5}" _
                             & "{5}{4}-- Return -1 if we had an error{5}" _
                             & "{4}IF @@ERROR > 0{5}{4}BEGIN{5}{4}{4}RETURN -1{5}{4}END{5}{4}" _
                             & "ELSE{5}{4}BEGIN{5}{4}{4}RETURN 1{5}{4}END{5}END{5}GO{5}{5}", _
                      pClass.Name.Capitalized, pClass.ValueVariable.DatabaseColumnName, _
                      pClass.ValueVariable.DatabaseType, pClass.DatabaseTableName, _
                      Space(tabSize), vbCrLf)
    End Function
    Private Shared Function getSelectText(selectStatement As String) As String
        Dim retstring As String = "BEGIN" & vbCrLf
        retstring &= Space(tabSize) & "-- SET NOCOUNT ON added to prevent extra result sets from" & vbCrLf
        retstring &= Space(tabSize) & "-- interfering with SELECT statements." & vbCrLf
        retstring &= Space(tabSize) & "SET NOCOUNT ON;" & vbCrLf & vbCrLf
        retstring &= Space(tabSize) & selectStatement & vbCrLf
        retstring &= "END" & vbCrLf
        retstring &= "GO" & vbCrLf & vbCrLf
        Return retstring
    End Function
End Class
