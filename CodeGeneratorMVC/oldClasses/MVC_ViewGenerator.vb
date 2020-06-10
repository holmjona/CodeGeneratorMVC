Option Strict On

Imports EnvDTE
Imports System.Collections.Generic
Imports System.Text
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs
Imports IRICommonObjects.Words
Imports System.IO
Imports System.Windows

Public Class MVCControllerGenerator
    Public Sub New()

    End Sub
    Private Function getCreateView(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@ModelType " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Html.Partial(""Parts/_Form"")")
            strB.AppendLine(Space(tab.XX) & "<div>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</div>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Section Scripts {")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "End Section")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@model" & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Html.Partial(""Parts/_Form"")")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<div>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</div>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@section Scripts{")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getDeleteView(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@ModelType " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h3>Are you sure that you want to delete this?</h3>")
            strB.AppendLine(Space(tab.XX) & "@using Html.BeginForm()")
            strB.AppendLine(Space(tab.XXX) & "@Html.Partial(""Parts/_Details"")")
            strB.AppendLine(Space(tab.XXX) & "@Html.Partial(""_DeletePartial"")")
            strB.AppendLine(Space(tab.XX) & "End Using")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<div>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</div>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Section Scripts")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "End Section")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@model" & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h3>Are you sure that you want to delete this?</h3>")
            strB.AppendLine(Space(tab.XXX) & "@using(Html.BeginForm()) {")
            strB.AppendLine(Space(tab.XXX) & "@Html.Partial(""Parts/_Details"")")
            strB.AppendLine(Space(tab.XXX) & "@Html.Partial(""_DeletePartial"")")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<div>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</div>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@section Scripts {")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getDetailsView(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@ModelType " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@using Html.BeginForm()")
            strB.AppendLine(Space(tab.XXX) & "@Html.Partial(""Parts/_Details"")")
            strB.AppendLine(Space(tab.XX) & "End Using")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<p>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Edit"", ""Edit"", new { id = Model." & pClass.NameString & ".ID})")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Delete"", ""Delete"", new { id = Model." & pClass.NameString & ".ID})")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</p>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Section Scripts")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "End Section")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@model" & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@using(Html.BeginForm()) {")
            strB.AppendLine(Space(tab.XXX) & "@Html.Partial(""Parts/_Details"")")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<p>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Edit"", ""Edit"", new { id = Model." & pClass.Name.Capitalized & ".ID})")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Delete"", ""Delete"", new { id = Model." & pClass.Name.Capitalized & ".ID})")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</p>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@section Scripts {")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getEditView(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@ModelType " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Html.Partial(""Parts/_Form"")")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<div>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</div>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Section Scripts")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "End Section")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@model" & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewBag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Html.Partial(""Parts/_Details"")")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<div>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Back to List"", ""Index"")")
            strB.AppendLine(Space(tab.XX) & "</div>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@section Scripts {")
            strB.AppendLine(Space(tab.XXX) & "@Scripts.Render(""~/bundles/jqueryval"")")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getIndexView(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@using MVC.Parts")
            strB.AppendLine(Space(tab.XX) & "@ModelType IEnumerable<" & pClass.NameWithNameSpace & ">")
            strB.AppendLine(Space(tab.XX) & "@Code")
            strB.AppendLine(Space(tab.XXX) & "Pagination pager = (Pagination)ViewBag.page;")
            strB.AppendLine(Space(tab.XX) & "End Code")
            strB.AppendLine(Space(tab.XX) & "<h2>@Viewbag.Title</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<p>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Create New "" + @SiteVariable.CurrentAliasGroup." & pClass.Name.Capitalized & ".Capitalized, ""Create"")")
            strB.AppendLine(Space(tab.XXX) & "@ViewBag.TimeSpent")
            strB.AppendLine(Space(tab.XX) & "</p>")
            strB.AppendLine(Space(tab.XX) & "@Html.Partial(""_PageVariables"", pager)")
            strB.AppendLine(Space(tab.XX) & "<table>")
            strB.AppendLine(Space(tab.XXX) & "<tr>")
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXXX) & "<th>")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.DisplayNameFor(Function(model) model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</th>")
            Next
            strB.AppendLine(Space(tab.XXXX) & "</tr>")
            strB.AppendLine(Space(tab.XX) & "@For Each item In Model")
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXX) & "<tr>")
                strB.AppendLine(Space(tab.XXXX) & "<td>")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.DisplayFor(Function(item) item." & cVar.Name)
                strB.AppendLine(Space(tab.XXXX) & "</td>")
                strB.AppendLine(Space(tab.XXXX) & "<td>")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ActionLink(""Edit"", ""Edit"", New With { .id=item.ID }) |")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ActionLink(""Details"", ""Details"", New With { .id=item.ID }) |")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ActionLink(""Delete"", ""Delete"", New With { .id=item.ID })")
                strB.AppendLine(Space(tab.XXXX) & "</td>")
                strB.AppendLine(Space(tab.XXX) & "</tr>")
            Next
            strB.AppendLine(Space(tab.XX) & "Next")
            strB.AppendLine(Space(tab.XX) & "</table>")

        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@using MVC.Parts")
            strB.AppendLine(Space(tab.XX) & "@model IEnumerable<" & pClass.NameWithNameSpace & ">")
            strB.AppendLine(Space(tab.XX) & "@{")
            strB.AppendLine(Space(tab.XXX) & "Pagination pager = (Pagination)ViewBag.page;")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "<h2>@ViewData(""Title"")</h2>")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<p>")
            strB.AppendLine(Space(tab.XXX) & "@Html.ActionLink(""Create New "" + @SiteVariable.CurrentAliasGroup." & pClass.Name.Capitalized & ".Capitalized, ""Create"")")
            strB.AppendLine(Space(tab.XXX) & "@ViewBag.TimeSpent")
            strB.AppendLine(Space(tab.XX) & "</p>")
            strB.AppendLine(Space(tab.XX) & "@Html.Partial(""_PageVariables"", pager)")
            strB.AppendLine(Space(tab.XX) & "<table>")
            strB.AppendLine(Space(tab.XXX) & "<tr>")
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXXX) & "<th>")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.DisplayNameFor(model => model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</th>")
            Next
            strB.AppendLine(Space(tab.XXXX) & "</tr>")
            strB.AppendLine(Space(tab.XX) & "@foreach (var item in Model){")
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXX) & "<tr>")
                strB.AppendLine(Space(tab.XXXX) & "<td>")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.DisplayFor(modelItem => item." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</td>")
                strB.AppendLine(Space(tab.XXXX) & "<td>")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ActionLink(""Edit"", ""Edit"", new { id=item.ID }) |")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ActionLink(""Details"", ""Details"", new { id=item.ID }) |")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ActionLink(""Delete"", ""Delete"", new { id=item.ID })")
                strB.AppendLine(Space(tab.XXXX) & "</td>")
                strB.AppendLine(Space(tab.XXX) & "</tr>")
            Next
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "</table>")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getDetailsPartialView(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@ModelType " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XXX) & "@Html.AntiForgeryToken()")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<fieldset>")
            strB.AppendLine(Space(tab.XXX) & "<legend>" & pClass.NameString & "</legend>")
            strB.AppendLine(Space(tab.XX))
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXX) & "<div class=""display-label"">")
                strB.AppendLine(Space(tab.XXXX) & "@Html.DisplayNameFor(Function(model) model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXX) & "</div>")
                strB.AppendLine(Space(tab.XXX) & "<div class=""display-field"">")
                strB.AppendLine(Space(tab.XXXX) & "@Html.DisplayFor(Function(model) model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXX) & "</div>")
            Next
            strB.AppendLine(Space(tab.XX) & "</fieldset>")

        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@model " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XXX) & "@Html.AntiForgeryToken()")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "<fieldset>")
            strB.AppendLine(Space(tab.XXX) & "<legend>" & pClass.NameString & "</legend>")
            strB.AppendLine(Space(tab.XX))
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXX) & "<div class=""display-label"">")
                strB.AppendLine(Space(tab.XXXX) & "@Html.DisplayNameFor(model => model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXX) & "</div>")
                strB.AppendLine(Space(tab.XXX) & "<div class=""display-field"">")
                strB.AppendLine(Space(tab.XXXX) & "@Html.DisplayFor(model => model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXX) & "</div>")
            Next
            strB.AppendLine(Space(tab.XX) & "</fieldset>")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getFormPartialView(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "@ModelType " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Scripts.Render(""~/bundles/forms"")")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@using Html.BeginForm()")
            strB.AppendLine(Space(tab.XXX) & "@Html.AntiForgeryToken()")
            strB.AppendLine(Space(tab.XXX) & "@Html.ValidationSummary(true)")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XXX) & "<fieldset>")
            strB.AppendLine(Space(tab.XXXX) & "<legend>" & pClass.Name.Capitalized & "</legend>")
            strB.AppendLine(Space(tab.XXXX))
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXXX) & "<div class=""editor-label"">")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.LabelFor(Function(model) model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</div>")
                strB.AppendLine(Space(tab.XXXX) & "<div class=""editor-field"">")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.DisplayFor(Function(model) model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ValidationMessageFor(Function(model) model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</div>")
            Next
            strB.AppendLine(Space(tab.XXXX) & "<p>")
            strB.AppendLine(Space(tab.XXXXX) & "<input type=""submit"" value=""@ViewBag.ButtonText"")/>")
            strB.AppendLine(Space(tab.XXXX) & "</p>")
            strB.AppendLine(Space(tab.XXX) & "</fieldset>")
            strB.AppendLine(Space(tab.XX) & "End Using")
            strB.AppendLine(Space(tab.XX) & "<script>")
            strB.AppendLine(Space(tab.XXX) & "$(""#Name"").focus(function () {")
            strB.AppendLine(Space(tab.XXXX) & "setUniquenessChecker({")
            strB.AppendLine(Space(tab.XXXXX) & "textBox: this,")
            strB.AppendLine(Space(tab.XXXXX) & "ajaxURL: ""@Url.Action(""IsFieldValueUnique"", ""Scanner""),")
            strB.AppendLine(Space(tab.XXXXX) & "fieldName: ""Name"",")
            strB.AppendLine(Space(tab.XXXXX) & "ID: @Model.ID")
            strB.AppendLine(Space(tab.XXXX) & "});")
            strB.AppendLine(Space(tab.XXX) & "});")
            strB.AppendLine(Space(tab.XX) & "</script>")

        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "@model " & pClass.NameWithNameSpace)
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@Scripts.Render(""~/bundles/forms"")")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "@using (Html.BeginForm()){")
            strB.AppendLine(Space(tab.XXX) & "@Html.AntiForgeryToken()")
            strB.AppendLine(Space(tab.XXX) & "@Html.ValidationSummary(true)")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XXX) & "<fieldset>")
            strB.AppendLine(Space(tab.XXXX) & "<legend>" & pClass.NameString & "</legend>")
            strB.AppendLine(Space(tab.XXXX))
            For Each cVar As ClassVariable In pClass.ClassVariables
                strB.AppendLine(Space(tab.XXXX) & "<div class=""editor-label"">")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.LabelFor(model => model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</div>")
                strB.AppendLine(Space(tab.XXXX) & "<div class=""editor-field"">")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.EditorFor(model => model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXXX) & "@Html.ValidationMessageFor(model => model." & cVar.Name & ")")
                strB.AppendLine(Space(tab.XXXX) & "</div>")
            Next
            strB.AppendLine(Space(tab.XXXX) & "<p>")
            strB.AppendLine(Space(tab.XXXXX) & "<input type=""submit"" value=""@ViewBag.ButtonText"")/>")
            strB.AppendLine(Space(tab.XXXX) & "</p>")
            strB.AppendLine(Space(tab.XXX) & "</fieldset>")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "<script>")
            strB.AppendLine(Space(tab.XXX) & "$(""#Name"").focus(function () {")
            strB.AppendLine(Space(tab.XXXX) & "setUniquenessChecker({")
            strB.AppendLine(Space(tab.XXXXX) & "textBox: this,")
            strB.AppendLine(Space(tab.XXXXX) & "ajaxURL: ""@Url.Action(""IsFieldValueUnique"", ""Scanner""),")
            strB.AppendLine(Space(tab.XXXXX) & "fieldName: ""Name"",")
            strB.AppendLine(Space(tab.XXXXX) & "ID: @Model.ID")
            strB.AppendLine(Space(tab.XXXX) & "});")
            strB.AppendLine(Space(tab.XXX) & "});")
            strB.AppendLine(Space(tab.XX) & "</script>")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    ''' <summary>
    ''' Creates all view files required
    ''' </summary>
    ''' <param name="pClass">Project Class</param>
    ''' <param name="lang"></param>
    ''' <returns>A dictionary with with the name of the view as the key and the data as the value </returns>
    ''' <remarks></remarks>
    Public Function getAllViewPages(ByVal pClass As ProjectClass, lang As language) As Dictionary(Of String, String)
        Dim retDict As New Dictionary(Of String, String)
        retDict.Add("Create", getCreateView(pClass, lang))
        retDict.Add("Delete", getDeleteView(pClass, lang))
        retDict.Add("Details", getDetailsView(pClass, lang))
        retDict.Add("Edit", getEditView(pClass, lang))
        retDict.Add("Index", getIndexView(pClass, lang))
        retDict.Add("_Details", getDetailsPartialView(pClass, lang))
        retDict.Add("_Form", getFormPartialView(pClass, lang))
        Return retDict
    End Function
    Private Function createFolder(path As String) As Boolean
        If Directory.Exists(path) Then
            Return True
        Else
            Directory.CreateDirectory(path)
            If Directory.Exists(path) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    ''' <summary>
    ''' Builds all view pages and puts them in the correct folder structure at the file path specified
    ''' </summary>
    ''' <param name="pClass"></param>
    ''' <param name="lang"></param>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function buildViewPages(ByVal pClass As ProjectClass, lang As language, filePath As String) As Boolean
        Dim dictOfViews As Dictionary(Of String, String)

        dictOfViews = getAllViewPages(pClass, lang)
        filePath = filePath & "\Views\"

        If createFolder(filePath) Then
            If dictOfViews IsNot Nothing AndAlso dictOfViews.Count > 0 Then
                For Each key As String In dictOfViews.Keys
                    If key.Contains("_"c) Then
                        If createFolder(filePath & "\" & pClass.Name.Capitalized & "\Parts\") Then
                            writeFileData(dictOfViews(key), key, filePath, lang)
                        Else
                            MessageBox.Show("Unable to create the view folder for: " & pClass.Name.Capitalized & ".  Giving up")
                            Exit For
                        End If
                    Else
                        If createFolder(filePath & "\" & pClass.Name.Capitalized & "\") Then
                            writeFileData(dictOfViews(key), key, filePath, lang)
                        Else
                            MessageBox.Show("Unable to create the view folder for: " & pClass.Name.Capitalized & ".  Giving up.")
                            Exit For
                        End If
                    End If
                Next
            End If
        End If
    End Function
    Private Function writeFileData(data As String, fileName As String, path As String, lang As language) As Boolean
        If lang = language.VisualBasic Then
            If fileName.Contains("_"c) Then
                Dim fs As FileStream = File.Create(path & "\Parts\" & fileName & ".vbhtml")
            Else
                Dim fs As FileStream = File.Create(path & fileName & ".vbhtml")
            End If
        Else
            If fileName.Contains("_"c) Then
                Dim fs As FileStream = File.Create(path & "\Parts\" & fileName & ".cshtml")
            Else
                Dim fs As FileStream = File.Create(path & fileName & ".cshtml")
            End If
        End If
        Dim fileData As Byte() = New UTF8Encoding(True).GetBytes(data)
    End Function
End Class
