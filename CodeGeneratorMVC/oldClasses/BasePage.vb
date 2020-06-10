Imports System.Text
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs

Public Class BasePage
    Private Shared Function getHeader(lang As language) As String
        Dim sb As New StringBuilder()
        sb.Append(cg.getPageImports(lang, includeWebUI:=True))
        sb.AppendLine("Public MustInherit Class BasePage")
        sb.AppendLine(Space(tab.X)) : sb.Append("Inherits Web.UI.Page")
        Return sb.ToString()
    End Function
    Private Shared Function getPrivateVariables() As String
        Dim sb As New StringBuilder()
        Dim namespaceString As String = ""
        If StaticVariables.Instance.UserClass IsNot Nothing Then
            If StaticVariables.Instance.UserClass.NameSpaceVariable.ID > 0 Then
                namespaceString = StaticVariables.Instance.UserClass.NameSpaceVariable.NameBasedOnID & "."
            End If
            sb.Append(Space(4)) : sb.Append("Private _CurrentUser As ") : sb.Append(namespaceString) : sb.Append(StaticVariables.Instance.UserClass.Name.Capitalized) : sb.Append(vbCrLf)
        End If
        If StaticVariables.Instance.AliasGroupClass IsNot Nothing Then
            If StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.ID > 0 Then
                namespaceString = StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.NameBasedOnID & "."
            End If
            sb.Append(Space(4)) : sb.Append("Private _AliasGroup As ") : sb.Append(namespaceString) : sb.Append(StaticVariables.Instance.AliasGroupClass.Name.Capitalized) : sb.Append(vbCrLf)
        End If
        sb.Append(Space(4)) : sb.Append("Protected _ReturnPath As String = Nothing") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Private Shared Function getPreInit() As String
        Dim sb As New StringBuilder()
        sb.Append(Space(4)) : sb.Append("Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)") : sb.Append(vbCrLf)
        If StaticVariables.Instance.UserClass IsNot Nothing Then
            sb.Append(Space(8)) : sb.Append("_CurrentUser = SessionVariables.CurrentUser") : sb.Append(vbCrLf)
        End If
        If StaticVariables.Instance.AliasGroupClass IsNot Nothing Then
            sb.Append(Space(8)) : sb.Append("_AliasGroup = SessionVariables.AliasGroup") : sb.Append(vbCrLf)
        End If
        sb.Append(Space(4)) : sb.Append("End Sub") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Private Shared Function getCurrentUser() As String
        Dim sb As New StringBuilder()
        Dim nameSpaceString As String = ""
        If StaticVariables.Instance.UserClass.NameSpaceVariable.ID > 0 Then
            nameSpaceString = StaticVariables.Instance.UserClass.NameSpaceVariable.NameBasedOnID & "."
        End If
        sb.Append(Space(4)) : sb.Append("Protected ReadOnly Property CurrentUser() As ") : sb.Append(nameSpaceString) : sb.Append(StaticVariables.Instance.UserClass.Name.Capitalized) : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("Get") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("Return _CurrentUser") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("End Get") : sb.Append(vbCrLf)
        sb.Append(Space(4)) : sb.Append("End Property") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Private Shared Function getCurrentAliasGroup() As String
        Dim sb As New StringBuilder()
        Dim nameSpaceString As String = ""
        If StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.ID > 0 Then
            nameSpaceString = StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.NameBasedOnID & "."
        End If
        sb.Append(Space(4)) : sb.Append("Protected ReadOnly Property AliasGroup() As ") : sb.Append(nameSpaceString) : sb.Append(StaticVariables.Instance.AliasGroupClass.Name.Capitalized) : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("Get") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("Return _AliasGroup") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("End Get") : sb.Append(vbCrLf)
        sb.Append(Space(4)) : sb.Append("End Property") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Private Shared Function getHandlerPermissions() As String
        Dim sb As New StringBuilder()
        sb.Append(Space(4)) : sb.Append("Protected Sub handlePermissions(ByVal caseToCheck As Boolean)") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("If Not caseToCheck Then") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("Niatec.SessionVariables.addPermissionError()") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("Redirect(""Login.aspx"")") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("End If") : sb.Append(vbCrLf)
        sb.Append(Space(4)) : sb.Append("End Sub") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Private Shared Function getRedirect() As String
        Dim sb As New StringBuilder()
        sb.Append(Space(4)) : sb.Append("Protected Sub Redirect(ByVal defaultURL As String)") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("If Request.QueryString(""redirect"") IsNot Nothing And SessionVariables.History.Count > 1 Then") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("SessionVariables.History.Pop()") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("While SessionVariables.History.Count > 0 And SessionVariables.History.Peek() = Request.Url.ToString()") : sb.Append(vbCrLf)
        sb.Append(Space(16)) : sb.Append("SessionVariables.History.Pop()") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("End While") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("If SessionVariables.History.Count > 0 Then") : sb.Append(vbCrLf)
        sb.Append(Space(16)) : sb.Append("Response.Redirect(SessionVariables.History.Pop)") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("End If") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("End If") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("Response.Redirect(defaultURL)") : sb.Append(vbCrLf)
        sb.Append(Space(4)) : sb.Append("End Sub") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Private Shared Function getReturnPath() As String
        Dim sb As New StringBuilder()
        sb.Append(Space(4)) : sb.Append("Protected Function getReturnPath() As String") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("Dim retStr As String = _ReturnPath") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("If Request.QueryString(""from"") IsNot Nothing Then") : sb.Append(vbCrLf)
        sb.Append(Space(12)) : sb.Append("retStr = Request.QueryString(""from"") & "".aspx""") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("End If") : sb.Append(vbCrLf)
        sb.Append(Space(8)) : sb.Append("Return retStr") : sb.Append(vbCrLf)
        sb.Append(Space(4)) : sb.Append("End Function") : sb.Append(vbCrLf)
        Return sb.ToString()
    End Function
    Public Shared Function getBasePage(lang As language) As String
        Dim sb As New StringBuilder()
        sb.Append(getHeader(lang))
        sb.Append(getPrivateVariables())
        sb.Append(getPreInit())
        sb.Append(getCurrentUser())
        sb.Append(getCurrentAliasGroup())
        sb.Append(getHandlerPermissions())
        sb.Append(getRedirect())
        sb.Append(getReturnPath())
        Return sb.ToString()

    End Function
End Class
