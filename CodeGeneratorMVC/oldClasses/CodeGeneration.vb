Option Strict On

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Collections.Generic
Imports System.Text

Public Class CodeGeneration

    Public Shared Function isRegularDataType(ByVal typestr As String) As Boolean
        typestr = typestr.ToLower
        Dim associatedDataType As DataType = StaticVariables.Instance.GetDataType(typestr.ToLower())
        If associatedDataType IsNot Nothing AndAlso associatedDataType.IsPrimitive Then
            Return True
        Else
            Return False
        End If
        Return False
    End Function

    Public Enum Language As Integer
        CSharp
        VisualBasic
    End Enum
    Public Enum Format As Integer
        ASPX
        MVC
    End Enum
    Public Enum Tabs As Integer
        None = 0
        X = 4
        XX = 8
        XXX = 12
        XXXX = 16
        XXXXX = 20
        XXXXXX = 24
        XXXXXXX = 28
        XXXXXXXX = 32
    End Enum
    Public Shared Function getCommentString(lang As CodeGeneration.Language, Optional isSummary As Boolean = False) As String
        If lang = CodeGeneration.Language.VisualBasic Then
            If isSummary Then Return "'''"
            Return "'"
        End If
        If isSummary Then Return "///"
        Return "//"
    End Function
    Public Shared Function getSqlDataTypeConversion(ByVal datatype As String) As String
        Dim retString As String = ""
        Select Case datatype.ToLower
            Case "integer"
                retString = "int"
            Case "string"
                retString = "nvarchar()"
            Case "double"
                retString = "float"
            Case "byte()"
                retString = "image"
            Case "date"
                retString = "datetime"
            Case "boolean"
                retString = "bit"
            Case "datetime"
                retString = "datetime"
            Case "image"
                retString = "byte()"
            Case Else
                retString = "UnknownDataType"
        End Select
        Return retString
    End Function
    Private Shared _ConversionFunctions As Dictionary(Of String, String)
    Public Shared ReadOnly Property ConversionFunctions() As Dictionary(Of String, String)
        Get
            If _ConversionFunctions Is Nothing Then
                _ConversionFunctions = New Dictionary(Of String, String)
                _ConversionFunctions.Add("integer", "CInt")
                _ConversionFunctions.Add("short", "CShort")
                _ConversionFunctions.Add("string", "CStr")
                _ConversionFunctions.Add("date", "CDate")
                _ConversionFunctions.Add("boolean", "CBool")
                _ConversionFunctions.Add("single", "CSng")
                _ConversionFunctions.Add("double", "CDbl")
                _ConversionFunctions.Add("byte", "CByte")
                _ConversionFunctions.Add("decimal", "CDec")
                _ConversionFunctions.Add("datetime", "DateTime.Parse")


            End If
            Return _ConversionFunctions
        End Get
    End Property

    Public Shared Function getConvertFunction(ByVal dataTypeString As String, lang As CodeGeneration.Language) As String
        Dim retString As String = ""
        If lang = Language.VisualBasic Then
            If ConversionFunctions.ContainsKey(dataTypeString.ToLower()) Then
                Return ConversionFunctions(dataTypeString.ToLower())
            Else
                Return "UnknownDataType"
            End If
        Else
            Return "(" & dataTypeString & ")"
        End If
        'Select Case dataTypeString.ToLower
        '    Case "integer"
        '        retString = "CInt"
        '    Case "short"
        '        retString = "CShort"
        '        Case 
        '    Case "string"
        '        retString = "CStr"
        '    Case "date"
        '        retString = "CDate"
        '    Case "boolean"
        '        retString = "CBool"
        '    Case "single"
        '        retString = "CSng"
        '    Case "double"
        '        retString = "CDbl"
        '    Case "short"
        '        retString = "CShort"
        '    Case "byte"
        '        retString = "CByte"
        '    Case "decimal"
        '        retString = "CDec"
        '    Case "datetime"
        '        retString = "DateTime.Parse"
        '    Case Else
        '        retString = "UnknownDataType"
        'End Select
        Return retString
    End Function
    Public Shared Function getPageImports(lang As Language, _
                                          Optional includeSQL As Boolean = False, _
                                          Optional includeSystemConfig As Boolean = False, _
                                          Optional includeWebUI As Boolean = False) As String
        Dim strB As New StringBuilder
        If lang = Language.VisualBasic Then
            strB.AppendLine("Option Strict On")
            strB.AppendLine("Imports Microsoft.VisualBasic")
            strB.AppendLine("Imports IRICommonObjects.Tools")
            strB.AppendLine("Imports System.Collections.Generic")
            strB.AppendLine("Imports System.Linq")
            If includeWebUI Then strB.AppendLine("Imports System.Web.UI")
            If includeSQL Then strB.AppendLine("Imports System.Data.SqlClient")
            If includeSystemConfig Then strB.AppendLine("Imports System.Configuration")
            For Each nsp As ProjectVariable In StaticVariables.Instance.NameSpaceNames
                strB.AppendLine(String.Format("Imports {0}", nsp.Name))
            Next
        Else
            strB.AppendLine("using System;")
            strB.AppendLine("using System.Net;")
            strB.AppendLine("using System.Linq;")
            strB.AppendLine("using System.Collections.Generic;")
            If includeWebUI Then strB.AppendLine("using System.Web.UI;")
            strB.AppendLine("using IRICommonObjects.Tools;")
            If includeSQL Then strB.AppendLine("using System.Data.SqlClient;")
            If includeSystemConfig Then strB.AppendLine("using System.Configuration;")
            For Each nsp As ProjectVariable In StaticVariables.Instance.NameSpaceNames
                strB.AppendLine(String.Format("using {0};", nsp.Name))
            Next
        End If
        Return strB.ToString()
    End Function

    Public Shared Function getMetaDataText(ByVal comment As String, ByVal isProperty As Boolean, ByVal indentationOffset As Integer, _
                                           lang As Language, _
                                           Optional ByVal returnType As String = "", Optional ByVal namSpace As String = "") As String
        Dim commString As String = "'''"
        If lang = Language.CSharp Then commString = "///"
        Dim strB As New StringBuilder
        If comment.Length = 0 Then
            strB.AppendLine(Space(indentationOffset) & commString & " <summary>")
            strB.AppendLine(Space(indentationOffset) & commString & " TODO: Comment this")
            strB.AppendLine(Space(indentationOffset) & commString & " </summary>")
        Else
            strB.AppendLine(Space(indentationOffset) & commString & " <summary>" & vbCrLf & Space(indentationOffset) & commString & _
                " " & comment & vbCrLf & Space(indentationOffset) & commString & " </summary>")
        End If
        If comment.Length = 0 AndAlso isProperty Then 'only adding comments to class information
            If isRegularDataType(returnType) Then
                strB.AppendLine(Space(indentationOffset) & commString & " <value></value>")
                strB.AppendLine(Space(indentationOffset) & commString & " <returns>" & returnType & "</returns>")
            Else
                strB.AppendLine(Space(indentationOffset) & commString & " <value></value>")
                strB.AppendLine(Space(indentationOffset) & commString & " <returns>" & namSpace & "." & returnType & "</returns>")
            End If
        End If
        strB.AppendLine(Space(indentationOffset) & commString & " <remarks></remarks>")
        Return strB.ToString()
    End Function

    Shared Function getRegionStart(lang As Language, name As String) As String
        If lang = Language.VisualBasic Then
            Return "#Region """ & name & """"
        Else
            Return "#region " & name
        End If
    End Function
    Shared Function getRegionEnd(lang As Language) As String
        If lang = Language.VisualBasic Then
            Return "#End Region"
        Else
            Return "#endregion"
        End If
    End Function

    Shared Function getClassDeclaration(lang As Language, className As String, offset As Tabs, Optional inheritedClassName As String = "") As String
        Dim strB As New StringBuilder
        If lang = Language.VisualBasic Then
            strB.AppendLine(Space(Tabs.X + offset) & "Partial Class " & className)
            If inheritedClassName <> "" Then strB.AppendLine(Space(Tabs.XX + offset) & "Inherits " & inheritedClassName)
        Else
            strB.Append(Space(Tabs.X + offset) & "partial class " & className)
            If inheritedClassName <> "" Then strB.AppendLine(" : " & inheritedClassName)
            strB.AppendLine(Space(Tabs.X + offset) & "{")
        End If
        Return strB.ToString()
    End Function
  
    ''' <summary>
    ''' Make camelCasedString read normally by adding spaces infront of Capital letters.
    ''' </summary>
    ''' <param name="strToConvert"></param>
    ''' <returns>String of camelCasedString into a readable form ex: ShowMeATest --> Show Me A Test</returns>
    ''' <remarks></remarks>
    Public Shared Function MakeHumanReadable(strToConvert As String) As String
        Dim retStr As String = ""
        Dim upperCount As Integer = 0
        Dim numberCount As Integer = 0
        Dim lastCharWasNumber As Boolean
        For Each c As Char In strToConvert
            ' Add space in front of upper characters -- FirstName --> First Name
            If Char.IsUpper(c) Then
                retStr &= " "
                upperCount += 1
            End If
            ' Keep numbers together but add a space infront -- Field12 --> Field 12
            If Char.IsNumber(c) Then
                numberCount += 1
                If Not lastCharWasNumber Then
                    retStr &= " "
                End If
                lastCharWasNumber = True
            Else
                lastCharWasNumber = False
            End If
            retStr &= c
        Next
        If upperCount + numberCount = strToConvert.Length Then
            ' No CamelCase was in ALLCAPS. 
            ' So we will just return the original.
            If numberCount > 0 Then
                ' will resend through in lower so that it will space off any numbers
                Return MakeHumanReadable(strToConvert.ToLower()).ToUpper
            Else
                Return strToConvert
            End If
        End If
            Return retStr.Trim()
    End Function

End Class
