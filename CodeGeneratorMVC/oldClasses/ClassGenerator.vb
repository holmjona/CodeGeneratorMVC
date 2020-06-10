Option Strict On

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Text
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs

Public Class ClassGenerator


    Public Shared Function getDataReaderText(ByVal pClass As ProjectClass, ByVal copyToClipboard As Boolean, ByVal overridesBase As Boolean, ByVal lang As language) As String
        If lang = language.VisualBasic Then
            Return getDataReaderTextInVB(pClass, copyToClipboard, overridesBase)
        Else
            Return getDataReaderTextInCSharp(pClass, copyToClipboard, overridesBase)
        End If
    End Function
    Private Shared Function getDataReaderTextInVB(ByVal pClass As ProjectClass, ByVal copyToClipboard As Boolean, ByVal overridesBase As Boolean) As String
        Dim retStrB As New StringBuilder
        If pClass.Name.Text.Length > 0 Then
            retStrB.Append(cg.getMetaDataText("Fills object from a SqlClient Data Reader", False, tab.XX, language.VisualBasic))
            retStrB.AppendLine(Space(tab.XX) & "Public " & IIf(overridesBase, "Overrides ", "").ToString() & "Sub Fill(ByVal dr As Data.SqlClient.SqlDataReader)")
            For Each classVar As ClassVariable In pClass.ClassVariables
                If classVar.isAssociative Then Continue For
                retStrB.Append(Space(tab.XXX))
                If Not classVar.IsDatabaseBound OrElse classVar.ParameterType.IsImage Then Continue For
                Dim AttributeAndObjectHaveSameName As Boolean = classVar.Name = classVar.ParameterType.Name(language.VisualBasic)
                Dim DatabaseParameterName As String = "db_" & classVar.Name
                If Not cg.isRegularDataType(classVar.ParameterType.Name(language.VisualBasic)) AndAlso AttributeAndObjectHaveSameName Then
                    DatabaseParameterName = classVar.ParameterType.Name(language.VisualBasic) & ".db_ID"
                End If
                If classVar.ParameterType.IsNameAlias Then
                    retStrB.AppendLine("_" & classVar.Name & ".TextUnFormatted = dr(" & DatabaseParameterName & ").ToString()")
                    retStrB.Append(Space(tab.XXX))
                Else
                    If Not cg.isRegularDataType(classVar.ParameterType.Name(language.VisualBasic)) Then
                        If AttributeAndObjectHaveSameName Then
                            retStrB.AppendLine("_" & classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized & _
                                               " = CInt(dr(" & DatabaseParameterName & "))")
                        Else
                            retStrB.AppendLine("_" & classVar.Name & "ID = CInt(dr(" & DatabaseParameterName & "))")
                        End If
                    Else
                        Dim toStringText As String = ""
                        If classVar.ParameterType.Name.ToLower().CompareTo("datetime") = 0 Then
                            toStringText = ".ToString()"
                        End If
                        retStrB.AppendLine("_" & classVar.Name & " = " & _
                                           cg.getConvertFunction(classVar.ParameterType.Name, language.VisualBasic) & _
                                       "(dr(" & DatabaseParameterName & ")" & toStringText & ")")
                    End If
                End If
            Next
            retStrB.AppendLine(Space(tab.XX) & "End Sub")
            If copyToClipboard Then
                Clipboard.Clear()
                Clipboard.SetText(retStrB.ToString())
            End If
        End If
        Return retStrB.ToString()
    End Function
    Private Shared Function getDataReaderTextInCSharp(ByVal pClass As ProjectClass, ByVal copyToClipboard As Boolean, ByVal overridesBase As Boolean) As String
        Dim retStrB As New StringBuilder
        If pClass.Name.Text.Length > 0 Then
            retStrB.Append(cg.getMetaDataText("Fills object from a SqlClient Data Reader", False, tab.XX, language.CSharp))
            retStrB.AppendLine(Space(tab.XX) & "public " & IIf(overridesBase, "override ", "").ToString() & "void Fill(System.Data.SqlClient.SqlDataReader dr)")
            retStrB.AppendLine(Space(tab.XX) & "{")
            For Each classVar As ClassVariable In pClass.ClassVariables
                If classVar.isAssociative Then Continue For
                retStrB.Append(Space(tab.XXX))
                If Not classVar.IsDatabaseBound OrElse classVar.ParameterType.IsImage Then Continue For
                Dim AttributeAndObjectHaveSameName As Boolean = classVar.Name = classVar.ParameterType.Name
                Dim DatabaseParameterName As String = "db_" & classVar.Name
                If Not cg.isRegularDataType(classVar.ParameterType.Name) AndAlso AttributeAndObjectHaveSameName Then
                    DatabaseParameterName = classVar.ParameterType.Name & ".db_ID"
                End If
                If classVar.ParameterType.IsNameAlias Then
                    retStrB.AppendLine("_" & classVar.Name & ".TextUnFormatted = dr[" & DatabaseParameterName & "].ToString();")
                    retStrB.Append(Space(tab.XXX))
                Else
                    If Not cg.isRegularDataType(classVar.ParameterType.Name) Then
                        If AttributeAndObjectHaveSameName Then
                            retStrB.AppendLine("_" & classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized & _
                                               " = (int)dr[" & DatabaseParameterName & "];")
                        Else
                            retStrB.AppendLine("_" & classVar.Name & "ID = (int)dr[" & DatabaseParameterName & "];")
                        End If
                    Else
                        Dim toStringText As String = ""
                        'If classVar.ParameterType.Name.ToLower().CompareTo("datetime") = 0 Then
                        '    toStringText = ".ToString()"
                        'End If
                        retStrB.AppendLine("_" & classVar.Name & " = (" & _
                                           classVar.ParameterType.Name(language.CSharp) & _
                                       ")dr[" & DatabaseParameterName & "]" & toStringText & ";")
                    End If
                End If
            Next
            retStrB.AppendLine(Space(tab.XX) & "}")
            If copyToClipboard Then
                Clipboard.Clear()
                Clipboard.SetText(retStrB.ToString())
            End If
        End If
        Return retStrB.ToString()
    End Function
    Private Shared Function getAddUpdateFunctions(ByVal pClass As ProjectClass, ByVal copyToClipboard As Boolean, ByVal overridesBase As Boolean, lang As language) As String
        Dim retStrB As New StringBuilder
        Dim DALClassName As String = pClass.DALClassVariable.Name
        Dim objectName As String = pClass.Name.Capitalized
        If objectName.Length > 0 And DALClassName.Length > 0 Then
            If DALClassName.Length > 0 And objectName.Length > 0 Then
                retStrB.Append(getAddClassFunction(DALClassName, objectName, overridesBase, lang))
                retStrB.Append(getUpdateClassFunction(DALClassName, objectName, overridesBase, lang))
                retStrB.Append(getRemoveClassFunction(DALClassName, objectName, overridesBase, lang))
            End If
            If copyToClipboard Then
                Clipboard.Clear()
                Clipboard.SetText(retStrB.ToString())
            End If
        End If
        Return retStrB.ToString()
    End Function
    Private Shared Function getAddClassFunction(ByVal dalName As String, ByVal objectName As String, ByVal overridesBase As Boolean, lang As language) As String
        Dim retStrB As New StringBuilder(cg.getMetaDataText("Calls DAL function to add " & objectName & " to the database.", _
                                                                        False, tab.XX, lang, "Integer value greater than 0 if successful."))

        If lang = language.VisualBasic Then
            retStrB.AppendLine(Space(tab.XX) & "Public " & IIf(overridesBase, "Overrides ", "").ToString() & "Function dbAdd() As Integer")
            retStrB.AppendLine(Space(4 + tab.XX) & "_ID = " & dalName & ".Add" & objectName & "(Me)")
            retStrB.AppendLine(Space(4 + tab.XX) & "Return ID")
            retStrB.AppendLine(Space(tab.XX) & "End Function")
        Else
            retStrB.AppendLine(Space(tab.XX) & "public " & IIf(overridesBase, "override ", "").ToString() & "int dbAdd()")
            retStrB.AppendLine(Space(tab.XX) & "{")
            retStrB.AppendLine(Space(4 + tab.XX) & "_ID = " & dalName & ".Add" & objectName & "(this);")
            retStrB.AppendLine(Space(4 + tab.XX) & "return ID;")
            retStrB.AppendLine(Space(tab.XX) & "}")
        End If
        retStrB.AppendLine("")
        Return retStrB.ToString()
    End Function
    Private Shared Function getUpdateClassFunction(ByVal dalName As String, ByVal objectName As String, ByVal overridesBase As Boolean, lang As language) As String
        Dim retStrB As New StringBuilder(cg.getMetaDataText("Calls DAL function to update " & objectName & " to the database.", _
                                                                        False, tab.XX, lang, "Integer value greater than 0 if successful."))
        If lang = language.VisualBasic Then
            retStrB.AppendLine(Space(tab.XX) & "Public " & IIf(overridesBase, "Overrides ", "").ToString() & "Function dbUpdate() As Integer")
            retStrB.AppendLine(Space(tab.XXX) & "Return " & dalName & ".Update" & objectName & "(Me)")
            retStrB.AppendLine(Space(tab.XX) & "End Function")
        Else
            retStrB.AppendLine(Space(tab.XX) & "public " & IIf(overridesBase, "override ", "").ToString() & "int dbUpdate()")
            retStrB.AppendLine(Space(tab.XX) & "{")
            retStrB.AppendLine(Space(4 + tab.XX) & "return " & dalName & ".Update" & objectName & "(this);")
            retStrB.AppendLine(Space(tab.XX) & "}")
        End If
        retStrB.AppendLine("")
        Return retStrB.ToString()
    End Function
    Private Shared Function getRemoveClassFunction(ByVal dalName As String, ByVal objectName As String, ByVal overridesBase As Boolean, lang As language) As String
        overridesBase = False
        Dim retStrB As New StringBuilder(cg.getMetaDataText("Calls DAL function to remove " & objectName & " from the database.", _
                                                                        False, tab.XX, lang, "Integer value greater than 0 if successful."))
        If lang = language.VisualBasic Then
            retStrB.AppendLine(Space(tab.XX) & "Public " & IIf(overridesBase, "Overrides ", "").ToString() & "Function dbRemove() As Integer")
            retStrB.AppendLine(Space(tab.XXX) & "Return " & dalName & ".Remove" & objectName & "(Me)")
            retStrB.AppendLine(Space(tab.XX) & "End Function")
        Else
            retStrB.AppendLine(Space(tab.XX) & "public " & IIf(overridesBase, "override ", "").ToString() & "int dbRemove()")
            retStrB.AppendLine(Space(tab.XX) & "{")
            retStrB.AppendLine(Space(4 + tab.XX) & "return " & dalName & ".Remove" & objectName & "(this);")
            retStrB.AppendLine(Space(tab.XX) & "}")
        End If
        Return retStrB.ToString()
    End Function
    Private Shared Function getToStringFunction(ByVal objectName As String, lang As language) As String
        Dim retStrB As New StringBuilder
        If lang = language.VisualBasic Then
            retStrB.AppendLine(Space(tab.XX) & "Public Overrides Function ToString() As String")
            retStrB.AppendLine(Space(tab.XXX) & "Return Me.GetType().ToString()")
            retStrB.AppendLine(Space(tab.XX) & "End Function")
        Else
            retStrB.AppendLine(Space(tab.XX) & "public override string ToString()")
            retStrB.AppendLine(Space(tab.XX) & "{")
            retStrB.AppendLine(Space(4 + tab.XX) & "return this.GetType().ToString();")
            retStrB.AppendLine(Space(tab.XX) & "}")
        End If
        Return retStrB.ToString()
    End Function
    Private Shared Function getConstructors(objName As String, lang As language) As String
        Dim retStrB As New StringBuilder
        If lang = language.VisualBasic Then
            retStrB.AppendLine(Space(tab.XX) & "Public Sub New()")
            retStrB.AppendLine(Space(tab.XX) & "End Sub")
            retStrB.AppendLine(Space(tab.XX) & "Friend Sub New(ByVal dr As System.Data.SqlClient.SqlDataReader)")
            retStrB.AppendLine(Space(tab.XXX) & "Fill(dr)")
            retStrB.AppendLine(Space(tab.XX) & "End Sub")
        Else
            retStrB.AppendLine(Space(tab.XX) & "public " & objName & "()")
            retStrB.AppendLine(Space(tab.XX) & "{")
            retStrB.AppendLine(Space(tab.XX) & "}")
            retStrB.AppendLine(Space(tab.XX) & "internal " & objName & "(System.Data.SqlClient.SqlDataReader dr)")
            retStrB.AppendLine(Space(tab.XX) & "{")
            retStrB.AppendLine(Space(tab.XXX) & "Fill(dr);")
            retStrB.AppendLine(Space(tab.XX) & "}")
        End If
        Return retStrB.ToString()
    End Function
    Public Shared Function getEntireClass(ByVal pClass As ProjectClass, ByVal copyResultsToClipboard As Boolean, _
                                          ByVal creator As String, lang As language, codeFormat As CodeGeneration.Format) As String
        Dim retStrB As New StringBuilder
        Dim namSpace As String = ""
        If pClass.NameSpaceVariable IsNot Nothing Then
            namSpace = pClass.NameSpaceVariable.NameBasedOnID

        End If
        Dim Comments As String = pClass.Summary
        'Dim txtProperties As String = txtInVars.Text.Trim
        Dim dalClassName As String = pClass.DALClassVariable.Name
        Dim objName As String = pClass.Name.Capitalized
        If objName.Length > 0 And dalClassName.Length > 0 Then
            If objName.Length > 0 Then
                If lang = language.VisualBasic Then
                    retStrB.Append(getEntireClassInVB(pClass, objName, namSpace, Comments, copyResultsToClipboard, creator, codeFormat))
                Else
                    retStrB.Append(getEntireClassInCSharp(pClass, objName, namSpace, Comments, copyResultsToClipboard, creator, codeFormat))
                End If
            End If
            If copyResultsToClipboard Then
                Clipboard.Clear()
                Clipboard.SetText(retStrB.ToString)
            End If
        Else
            If pClass.Name.Text.Length = 0 Then
                MessageBox.Show("You must provide an Object/Class name.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            If dalClassName.Length = 0 Then
                MessageBox.Show("You must provide a Data Access Layer Class name", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
        Return retStrB.ToString()
    End Function
    Private Shared Function getEntireClassInVB(ByVal pClass As ProjectClass, objName As String, namSpace As String, Comments As String, _
                                                   ByVal copyResultsToClipboard As Boolean, ByVal creator As String, codeFormat As CodeGeneration.Format) As String
        Dim retStrB As New StringBuilder
        Dim properties As String = getProperties(pClass, copyResultsToClipboard, language.VisualBasic, codeFormat)
        retStrB.AppendLine("'Created By: " & creator & " (using Code generator)")
        retStrB.AppendLine("'Created On: " & Date.Now.ToString)
        retStrB.AppendLine("Option Strict On")
        retStrB.AppendLine("Imports Microsoft.VisualBasic")
        retStrB.AppendLine("Imports System.Collections.Generic")
        If codeFormat = CodeGeneration.Format.MVC Then
            retStrB.AppendLine("Imports System.ComponentModel.DataAnnotations")
        End If
        If properties.ToLower().Contains("xmlignore") Then
            retStrB.AppendLine("Imports System.Xml.Serialization")
        End If
        retStrB.AppendLine("Namespace " & namSpace)
        retStrB.AppendLine(cg.getMetaDataText(Comments, False, tab.XX, language.VisualBasic))
        retStrB.AppendLine(Space(tab.X) & "Public Class " & objName)
        Dim overridesBase As Boolean = False
        If pClass.BaseClass IsNot Nothing AndAlso Not String.IsNullOrEmpty(pClass.BaseClass.Name) Then
            retStrB.AppendLine(Space(tab.X) & "Inherits " & pClass.BaseClass.Name)
            overridesBase = pClass.BaseClass.Name.ToLower().Contains("databaserecord")
        End If
        retStrB.AppendLine("")
        retStrB.Append(getCodeBody(objName, pClass, properties, copyResultsToClipboard, overridesBase, language.VisualBasic))
        retStrB.AppendLine(Space(tab.X) & "End Class")
        retStrB.AppendLine("End Namespace")

        Return retStrB.ToString()
    End Function
    Private Shared Function getEntireClassInCSharp(ByVal pClass As ProjectClass, objName As String, namSpace As String, Comments As String, _
                                                   ByVal copyResultsToClipboard As Boolean, ByVal creator As String, codeFormat As CodeGeneration.Format) As String
        Dim retStrB As New StringBuilder

        Dim properties As String = getProperties(pClass, copyResultsToClipboard, language.CSharp, codeFormat)
        retStrB.AppendLine("//Created By: " & creator & " (using Code generator)")
        retStrB.AppendLine("//Created On: " & Date.Now.ToString)
        retStrB.AppendLine("using System;")
        retStrB.AppendLine("using System.Net;")
        retStrB.AppendLine("using System.Linq;")
        retStrB.AppendLine("using System.Collections.Generic;")
        If codeFormat = CodeGeneration.Format.MVC Then
            retStrB.AppendLine("using System.ComponentModel.DataAnnotations;")
        End If
        If properties.ToLower().Contains("xmlignore") Then
            retStrB.AppendLine("using System.Xml.Serialization;")
        End If
        retStrB.AppendLine("namespace " & namSpace)
        retStrB.AppendLine("{")
        retStrB.AppendLine(cg.getMetaDataText(Comments, False, tab.XX, language.CSharp))
        retStrB.Append(Space(4) & "public class " & objName)
        Dim overridesBase As Boolean = False
        If pClass.BaseClass IsNot Nothing AndAlso Not String.IsNullOrEmpty(pClass.BaseClass.Name) Then
            retStrB.Append(" : " & pClass.BaseClass.Name)
            overridesBase = pClass.BaseClass.Name.ToLower().Contains("databaserecord")
        End If
        retStrB.AppendLine()
        retStrB.AppendLine("{")
        retStrB.Append(getCodeBody(objName, pClass, properties, copyResultsToClipboard, overridesBase, language.CSharp))
        retStrB.AppendLine(Space(tab.X) & "}")
        retStrB.AppendLine("}")

        Return retStrB.ToString()
    End Function
    Private Shared Function getCodeBody(objName As String, pClass As ProjectClass, properties As String, _
                                        copyResultsToClipboard As Boolean, overridesBaseObject As Boolean, lang As language) As String
        Dim strB As New StringBuilder
        strB.AppendLine(cg.getRegionStart(lang, "Constructors"))
        strB.AppendLine(getConstructors(objName, lang))
        strB.AppendLine(cg.getRegionEnd(lang))
        strB.AppendLine("")
        strB.AppendLine(cg.getRegionStart(lang, "Database String"))
        strB.AppendLine(getDatabaseStrings(pClass, lang))
        strB.AppendLine(cg.getRegionEnd(lang))
        strB.AppendLine("")
        strB.AppendLine(cg.getRegionStart(lang, "Private Variables"))
        strB.AppendLine(getPrivateVariables(pClass, lang))
        strB.AppendLine(cg.getRegionEnd(lang))
        strB.AppendLine("")
        strB.AppendLine(cg.getRegionStart(lang, "Public Properties"))
        strB.AppendLine(properties)
        strB.AppendLine(cg.getRegionEnd(lang))
        strB.AppendLine("")
        strB.AppendLine(cg.getRegionStart(lang, "Public Functions"))
        strB.AppendLine(getAddUpdateFunctions(pClass, copyResultsToClipboard, overridesBaseObject, lang))
        strB.AppendLine(cg.getRegionEnd(lang))
        strB.AppendLine("")
        strB.AppendLine(cg.getRegionStart(lang, "Public Subs"))
        strB.AppendLine(getDataReaderText(pClass, copyResultsToClipboard, overridesBaseObject, lang))
        strB.AppendLine(cg.getRegionEnd(lang))
        strB.AppendLine("")
        strB.AppendLine(getToStringFunction(objName, lang))
        Return strB.ToString()
    End Function
    Private Shared Function getProperties(ByVal pClass As ProjectClass, ByVal copyResultsToClipboard As Boolean, _
                                          lang As language, codeFormat As CodeGeneration.Format) As String
        Dim retStrB As New StringBuilder
        Dim namSpace As String = pClass.NameSpaceVariable.NameBasedOnID
        For Each cv As ClassVariable In pClass.ClassVariables
            Try
                If cv.IsPropertyInherited OrElse cv.ParameterType.IsImage Then Continue For
                retStrB.AppendLine(getPropertyText(pClass, cv, namSpace, lang, codeFormat, pClass.DALClassVariable.Name))
            Catch ex As Exception
                retStrB.AppendLine(String.Format("{3}ERROR: While Adding Variable({0}), the following error occured: {1}{3}MSG: {2}{1}", _
                                cv.Name, vbCrLf, ex.Message, cg.getCommentString(lang)))
            End Try
        Next
        If copyResultsToClipboard Then
            Clipboard.Clear()
            Clipboard.SetText(retStrB.ToString())
        End If
        Return retStrB.ToString()
    End Function

    Private Shared Function getPropertyText(ByVal pClass As ProjectClass, ByVal classVar As ClassVariable, ByVal namSpace As String, _
                                            lang As language, codeFormat As CodeGeneration.Format, _
                                            Optional ByVal DAL As String = "") As String
        Dim mDataType As DataType = classVar.ParameterType
        Dim nameWithUnderscore As String = "_" & classVar.Name
        Dim nameWithoutUnderScore As String = classVar.Name
        Dim retStrB As New StringBuilder
        retStrB.Append(cg.getMetaDataText(String.Format("Gets or sets the {0} for this {1}.{2} object.", _
                                                        nameWithoutUnderScore, pClass.NameSpaceVariable.NameBasedOnID, _
                                                        pClass.Name.Text), False, tab.XX, lang, mDataType.Name, namSpace))
        Dim propertyAttribute As String = ""
        If classVar.IsPropertyXMLIgnored Then
            If lang = language.VisualBasic Then
                propertyAttribute = "<XmlIgnore()> "
            Else
                propertyAttribute = "[XmlIgnore]"
            End If
        End If

        If cg.isRegularDataType(mDataType.Name) Or DAL.Length = 0 Then
            retStrB.Append(getPropertyStringForRegularType(classVar, propertyAttribute, nameWithoutUnderScore, nameWithUnderscore, lang, codeFormat))
        Else
            retStrB.Append(getPropertyStringForDerivedObject(pClass, classVar, propertyAttribute, nameWithoutUnderScore, nameWithUnderscore, _
                                                  mDataType, namSpace, DAL, lang, codeFormat))
        End If

        Return retStrB.ToString()
    End Function
    Private Shared Function getPropertyStringForRegularType(clsVar As ClassVariable, propertyAttribute As String, nameWithoutUnderScore As String, _
                                                 nameWithUnderscore As String, lang As language, codeFormat As CodeGeneration.Format) As String
        Dim strB As New StringBuilder
        If codeFormat = CodeGeneration.Format.MVC Then
            strB.Append(getMVCMetaData(Space(tab.XX), clsVar, lang))
        End If
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & propertyAttribute & "Public Property " & nameWithoutUnderScore & "() As " & clsVar.ParameterType.Name)
            strB.AppendLine(Space(tab.XXX) & "Get")
            strB.AppendLine(Space(tab.XXXX) & "Return " & nameWithUnderscore)
            strB.AppendLine(Space(tab.XXX) & "End Get")
            strB.AppendLine(Space(tab.XXX) & "Set(ByVal value As " & clsVar.ParameterType.Name & ")")
            strB.Append(Space(tab.XXXX))
            Dim trimValue As String = ""
            If clsVar.ParameterType.Name.ToLower().CompareTo("string") = 0 Then
                trimValue = ".Trim()"
            End If
            strB.AppendLine(nameWithUnderscore & " = value" & trimValue)
            strB.AppendLine(Space(tab.XXX) & "End Set")
            strB.AppendLine(Space(tab.XX) & "End Property")
        Else
            strB.AppendLine(Space(tab.XX) & propertyAttribute & "public " & clsVar.ParameterType.Name(language.CSharp) & " " & nameWithoutUnderScore)
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "get")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "return " & nameWithUnderscore & ";")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "set")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.Append(Space(tab.XXXX))
            Dim trimValue As String = ""
            If clsVar.ParameterType.Name.ToLower().CompareTo("string") = 0 Then
                trimValue = ".Trim()"
            End If
            strB.AppendLine(nameWithUnderscore & " = value" & trimValue & ";")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function
    Private Shared Function getMVCMetaData(spacer As String, clsVar As ClassVariable, lang As language) As String
        Dim strB As New StringBuilder
        If clsVar.ParameterType.Name.ToLower = "string" AndAlso clsVar.LengthOfDatabaseProperty > 0 Then
            If lang = language.VisualBasic Then
                strB.Append(spacer & "<StringLength(" & clsVar.LengthOfDatabaseProperty.ToString())
                strB.Append(", ErrorMessage:= ""The length of " & clsVar.Name & " can not exceed ")
                strB.Append(clsVar.LengthOfDatabaseProperty.ToString() & " characters."")>")
                strB.AppendLine()
                strB.AppendLine(spacer & "<Display(Name:=""" & cg.MakeHumanReadable(clsVar.ParentClass.DatabaseTableName) & " " & _
                                cg.MakeHumanReadable(clsVar.DatabaseColumnName) & """)>")
                If clsVar.IsRequired Then
                    strB.AppendLine("<Required(ErrorMessage:=""" & clsVar.DatabaseColumnName & " is required."")>")
                End If
                If clsVar.IsDouble Then
                    strB.AppendLine("<Range(0.00,100.0,ErrorMessage:=""" & clsVar.DatabaseColumnName & " is required."")>")
                End If
                If clsVar.IsDate Then
                    strB.AppendLine("<DataType(DataType.DateTime)>")
                End If
                If clsVar.DatabaseColumnName.ToLower.Contains("email") Then
                    strB.AppendLine("<DataType(DataType.EmailAddress)>")
                End If
            Else
                strB.Append(spacer & "[StringLength(" & clsVar.LengthOfDatabaseProperty.ToString())
                strB.Append(", ErrorMessage = ""The length of " & clsVar.Name & " can not exceed ")
                strB.Append(clsVar.LengthOfDatabaseProperty.ToString() & " characters."")]")
                strB.AppendLine()
                strB.AppendLine(spacer & "[Display(Name =""" & cg.MakeHumanReadable(clsVar.ParentClass.DatabaseTableName) & " " & _
                                cg.MakeHumanReadable(clsVar.DatabaseColumnName) & """)]")
                If clsVar.IsRequired Then
                    strB.AppendLine(spacer & "[Required(ErrorMessage =""" & clsVar.DatabaseColumnName & " is required."")]")
                End If
                If clsVar.IsDouble Then
                    strB.AppendLine(spacer & "[Range(0.00,100.0,ErrorMessage =""" & clsVar.DatabaseColumnName & " is required."")]")
                End If
                If clsVar.IsDate Then
                    strB.AppendLine("[DataType(DataType.DateTime)]")
                End If
                If clsVar.DatabaseColumnName.ToLower.Contains("email") Then
                    strB.AppendLine("[DataType(DataType.EmailAddress)]")
                End If
            End If
        End If
        Return strB.ToString()
    End Function
    Private Shared Function getPropertyStringForDerivedObject(pClass As ProjectClass, classVar As ClassVariable, propertyAttribute As String, _
                                               nameWithoutUnderscore As String, nameWithUnderscore As String, mDataType As DataType, _
                                               namSpace As String, DAL As String, lang As language, codeFormat As CodeGeneration.Format) As String
        Dim strB As New StringBuilder
        Dim nameOfIDVariable = nameWithoutUnderscore & "ID"
        If lang = language.VisualBasic Then
            strB.Append(getPropertyStringForDerivedObjectInVB(nameOfIDVariable, classVar, propertyAttribute, nameWithoutUnderscore, nameWithUnderscore, mDataType))
        Else
            strB.Append(getPropertyStringForDerivedObjectInCSharp(nameOfIDVariable, classVar, propertyAttribute, nameWithoutUnderscore, nameWithUnderscore, mDataType))
        End If
        Dim ID As Integer = 0
        If pClass.ClassVariables.Count > 0 Then
            ID = pClass.ClassVariables(pClass.ClassVariables.Count - 1).ID + 1
        End If

        'TODO: Fix this
        If Not classVar.IsList Then
            strB.Append(getPropertyText(pClass, New ClassVariable(pClass, nameOfIDVariable, StaticVariables.Instance.GetDataType("Integer"), _
                                                                   False, False, False, True, classVar.IsPropertyInherited, classVar.DisplayOnEditPage, _
                                                                   classVar.DisplayOnViewPage, ID, True, False, "Integer", -1, "NA"), namSpace, _
                                                                    lang, codeFormat, DAL))
        End If
        Return strB.ToString
    End Function
    Private Shared Function getPropertyStringForDerivedObjectInVB(nameOfIDVariable As String, classVar As ClassVariable, propertyAttribute As String, _
                                               nameWithoutUnderscore As String, nameWithUnderscore As String, mDataType As DataType) As String
        Dim strB As New StringBuilder
        Dim ObjectWithNameSpace As String = IIf(classVar.ParameterType.AssociatedProjectClass IsNot Nothing _
                                           AndAlso classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable <> classVar.ParentClass.NameSpaceVariable, _
                                           classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID & ".", "").ToString() & mDataType.Name

        strB.Append(Space(tab.XX) & propertyAttribute & "Public Property " & getSystemUniqueName(nameWithoutUnderscore) & "() As ")
        strB.AppendLine(IIf(classVar.IsList, "List(Of ", "").ToString() _
         & ObjectWithNameSpace & IIf(classVar.IsList, ")", "").ToString())
        strB.AppendLine(Space(tab.XXX) & "Get")
        strB.AppendLine(Space(tab.XXXX) & "If " & nameWithUnderscore & " Is Nothing Then")
        Dim nameSpaceString As String = ""
        If mDataType.AssociatedProjectClass.NameSpaceVariable IsNot Nothing Then
            nameSpaceString = mDataType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID & "."
        End If

        Dim nameSpacer As String = IIf(mDataType.AssociatedProjectClass.NameSpaceVariable _
                                       <> classVar.ParentClass.NameSpaceVariable, nameSpaceString, "").ToString()
        strB.Append(Space(tab.XXX + tab.XX) & nameWithUnderscore & " = " & nameSpacer & mDataType.AssociatedProjectClass.DALClassVariable.Name & ".Get")
        If classVar.IsList Then
            strB.AppendLine(nameWithoutUnderscore & "(Me)")
        Else
            strB.AppendLine(mDataType.Name & "(_" & nameOfIDVariable & ")")
        End If
        strB.AppendLine(Space(tab.XXXX) & "End If")
        strB.AppendLine(Space(tab.XXXX) & "Return " & nameWithUnderscore)
        strB.AppendLine(Space(tab.XXX) & "End Get")
        strB.Append(Space(tab.XXXX) & "Set(ByVal value As ")
        strB.Append(IIf(classVar.IsList, "() As List(Of ", "").ToString())
        strB.Append(ObjectWithNameSpace)
        strB.AppendLine(IIf(classVar.IsList, ")", "").ToString() & ")")
        strB.AppendLine(Space(tab.XXXX) & nameWithUnderscore & " = value")
        If Not classVar.IsList Then
            strB.AppendLine(Space(tab.XXXX) & "If value Is nothing Then")
            strB.AppendLine(Space(tab.XXXXX) & "_" & nameOfIDVariable & "=-1")
            strB.AppendLine(Space(tab.XXXX) & "Else")
            strB.AppendLine(Space(tab.XXXXX) & "_" & nameOfIDVariable & "=value.ID")
            strB.AppendLine(Space(tab.XXXX) & "End If")
        End If
        strB.AppendLine(Space(tab.XXX) & "End Set")
        strB.AppendLine(Space(tab.XX) & "End Property")

        Return strB.ToString()
    End Function
    Private Shared Function getPropertyStringForDerivedObjectInCSharp(nameOfIDVariable As String, classVar As ClassVariable, propertyAttribute As String, _
                                               nameWithoutUnderscore As String, nameWithUnderscore As String, mDataType As DataType) As String
        Dim strB As New StringBuilder
        Dim ObjectWithNameSpace As String = IIf(classVar.ParameterType.AssociatedProjectClass IsNot Nothing _
                                           AndAlso classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable <> classVar.ParentClass.NameSpaceVariable, _
                                           classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID & ".", "").ToString() & mDataType.Name
        strB.AppendLine(Space(tab.XX) & propertyAttribute)
        strB.Append(Space(tab.XX) & "public ")
        strB.Append(IIf(classVar.IsList, "List<", "").ToString() _
         & ObjectWithNameSpace & IIf(classVar.IsList, "> ", " ").ToString())
        strB.Append(getSystemUniqueName(nameWithoutUnderscore))
        strB.AppendLine("{")
        strB.AppendLine(Space(tab.XXX) & "get")
        strB.AppendLine(Space(tab.XXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & "if(" & nameWithUnderscore & " == null)")
        strB.AppendLine(Space(tab.XXXX) & "{")
        Dim nameSpaceString As String = ""
        If mDataType.AssociatedProjectClass.NameSpaceVariable IsNot Nothing Then
            nameSpaceString = mDataType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID & "."
        End If

        Dim nameSpacer As String = IIf(mDataType.AssociatedProjectClass.NameSpaceVariable _
                                       <> classVar.ParentClass.NameSpaceVariable, nameSpaceString, "").ToString()
        strB.Append(Space(tab.XXX + tab.XX) & nameWithUnderscore & " = " & nameSpacer & mDataType.AssociatedProjectClass.DALClassVariable.Name & ".Get")
        If classVar.IsList Then
            strB.AppendLine(nameWithoutUnderscore & "(this);")
        Else
            strB.AppendLine(mDataType.Name(language.CSharp) & "(_" & nameOfIDVariable & ");")
        End If
        strB.AppendLine(Space(tab.XXXX) & "}")
        strB.AppendLine(Space(tab.XXXX) & "return " & nameWithUnderscore & ";")
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XXXX) & "set")
        strB.AppendLine(Space(tab.XXXX) & "{")
        strB.AppendLine(Space(tab.XXXX) & nameWithUnderscore & " = value;")
        If Not classVar.IsList Then
            strB.AppendLine(Space(tab.XXXX) & "if (value == null)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "_" & nameOfIDVariable & "=-1;")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "else")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "_" & nameOfIDVariable & "=value.ID;")
            strB.AppendLine(Space(tab.XXXX) & "}")
        End If
        strB.AppendLine(Space(tab.XXX) & "}")
        strB.AppendLine(Space(tab.XX) & "}")
        Return strB.ToString()
    End Function
    Public Shared Function getSystemUniqueName(str As String) As String
        Select Case str.ToLower()
            Case "class", "view", "property"
                Return str & "Object"
            Case Else
                Return str
        End Select
    End Function
    Public Shared Function getDatabaseStrings(ByVal pClass As ProjectClass, lang As language) As String
        Dim retStrB As New StringBuilder
        For Each classVar As ClassVariable In pClass.ClassVariables
            If classVar.IsDatabaseBound Then
                If lang = language.VisualBasic Then
                    retStrB.AppendLine(Space(tab.XX) & "Friend Const db_" & classVar.Name _
                                       & " As String = """ & classVar.DatabaseColumnName & """")
                Else
                    retStrB.AppendLine(Space(tab.XX) & "internal const string db_" & classVar.Name _
                                       & "= """ & classVar.DatabaseColumnName & """;")
                End If
            End If
        Next
        Return retStrB.ToString()
    End Function
    Public Shared Function getPrivateVariables(ByVal pClass As ProjectClass, lang As language) As String
        If lang = language.VisualBasic Then
            Return getPrivateVariablesInVB(pClass)
        Else
            Return getPrivateVariablesInCSharp(pClass)
        End If
    End Function
    Public Shared Function getPrivateVariablesInVB(ByVal pClass As ProjectClass) As String
        Dim retStrB As New StringBuilder
        For Each classVar As ClassVariable In pClass.ClassVariables
            If classVar.IsPropertyInherited OrElse classVar.ParameterType.IsImage Then Continue For
            retStrB.Append(Space(tab.XX) & "Private ")
            If Not classVar.Name.StartsWith("_") Then
                retStrB.Append("_")
            End If
            If classVar.ParameterType.IsNameAlias Then
                retStrB.AppendLine(classVar.Name & " As New " & classVar.ParameterType.Name & "(""" & classVar.Name & """)")
            ElseIf classVar.ParameterType.IsPrimitive Then
                retStrB.AppendLine(classVar.Name & " As " & classVar.ParameterType.Name)
            Else
                Dim nameSpaceString As String = ""
                If classVar.ParameterType.AssociatedProjectClass IsNot Nothing _
                    AndAlso classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable IsNot Nothing _
                    AndAlso classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable <> pClass.NameSpaceVariable Then
                    nameSpaceString = classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID & "."
                End If
                If Not classVar.isAssociative Then
                    retStrB.AppendLine(classVar.Name & "ID As Integer")
                    retStrB.Append(Space(tab.XX) & "Private _")
                End If

                retStrB.AppendLine(classVar.Name & " As " _
                            & IIf(classVar.IsList, "() As List(Of ", "").ToString() & _
                            nameSpaceString & classVar.ParameterType.Name _
                            & IIf(classVar.IsList, ") = Nothing", "").ToString())
                '& nameSpaceString & classVar.ParameterType.Name 
            End If
        Next
        Return retStrB.ToString()
    End Function
    Public Shared Function getPrivateVariablesInCSharp(ByVal pClass As ProjectClass) As String
        Dim retStrB As New StringBuilder
        For Each classVar As ClassVariable In pClass.ClassVariables
            If classVar.IsPropertyInherited OrElse classVar.ParameterType.IsImage Then Continue For

            Dim varName As String = classVar.Name
            Dim paraType As String = classVar.ParameterType.Name(language.CSharp)
            Dim isAssocName As Boolean = False
            If Not varName.StartsWith("_") Then varName = "_" & varName
            Dim objName As String = ""

            If classVar.ParameterType.IsPrimitive Then
                objName = paraType
            ElseIf Not classVar.ParameterType.IsNameAlias Then
                Dim nameSpaceString As String = ""
                If classVar.ParameterType.AssociatedProjectClass IsNot Nothing _
                    AndAlso classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable IsNot Nothing _
                    AndAlso classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable <> pClass.NameSpaceVariable Then
                    nameSpaceString = classVar.ParameterType.AssociatedProjectClass.NameSpaceVariable.NameBasedOnID & "."
                End If
                objName = IIf(classVar.IsList, "List<", "").ToString() & _
                                nameSpaceString & paraType _
                                & IIf(classVar.IsList, ">", "").ToString()

                isAssocName = Not classVar.isAssociative

            End If

            '' Name aliases need a default declaration.
            Dim objDecl As String = ""
            If classVar.ParameterType.IsNameAlias Then
                objDecl = " = new " & classVar.ParameterType.Name & "(""" & classVar.Name & """)"
            End If

            ' Write Variable Strings
            ' make associative id reference if variable is part of an associative entity
            If isAssocName Then retStrB.AppendLine(Space(tab.XX) & String.Format("private int {0}ID;", varName))
            retStrB.AppendLine(Space(tab.XX) & String.Format("private {0} {1}{2};", objName, varName, objDecl))


        Next
        Return retStrB.ToString()
    End Function

   

End Class
