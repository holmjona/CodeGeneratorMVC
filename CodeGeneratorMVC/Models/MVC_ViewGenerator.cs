using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using EnvDTE;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;
using IRICommonObjects.Words;
using System.Windows;

public class MVCControllerGenerator {
    public MVCControllerGenerator() {
    }
    private string getCreateView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@ModelType " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Html.Partial(\"Parts/_Form\")");
            strB.AppendLine(Space(tab.XX) + "<div>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</div>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Section Scripts {");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "End Section");
        } else {
            strB.AppendLine(Space(tab.XX) + "@model" + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Html.Partial(\"Parts/_Form\")");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<div>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</div>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@section Scripts{");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getDeleteView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@ModelType " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h3>Are you sure that you want to delete this?</h3>");
            strB.AppendLine(Space(tab.XX) + "@using Html.BeginForm()");
            strB.AppendLine(Space(tab.XXX) + "@Html.Partial(\"Parts/_Details\")");
            strB.AppendLine(Space(tab.XXX) + "@Html.Partial(\"_DeletePartial\")");
            strB.AppendLine(Space(tab.XX) + "End Using");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<div>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</div>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Section Scripts");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "End Section");
        } else {
            strB.AppendLine(Space(tab.XX) + "@model" + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h3>Are you sure that you want to delete this?</h3>");
            strB.AppendLine(Space(tab.XXX) + "@using(Html.BeginForm()) {");
            strB.AppendLine(Space(tab.XXX) + "@Html.Partial(\"Parts/_Details\")");
            strB.AppendLine(Space(tab.XXX) + "@Html.Partial(\"_DeletePartial\")");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<div>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</div>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@section Scripts {");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getDetailsView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@ModelType " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@using Html.BeginForm()");
            strB.AppendLine(Space(tab.XXX) + "@Html.Partial(\"Parts/_Details\")");
            strB.AppendLine(Space(tab.XX) + "End Using");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<p>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Edit\", \"Edit\", new { id = Model." + pClass.NameString + ".ID})");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Delete\", \"Delete\", new { id = Model." + pClass.NameString + ".ID})");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</p>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Section Scripts");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "End Section");
        } else {
            strB.AppendLine(Space(tab.XX) + "@model" + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@using(Html.BeginForm()) {");
            strB.AppendLine(Space(tab.XXX) + "@Html.Partial(\"Parts/_Details\")");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<p>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Edit\", \"Edit\", new { id = Model." + pClass.Name.Capitalized + ".ID})");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Delete\", \"Delete\", new { id = Model." + pClass.Name.Capitalized + ".ID})");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</p>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@section Scripts {");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getEditView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@ModelType " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Html.Partial(\"Parts/_Form\")");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<div>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</div>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Section Scripts");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "End Section");
        } else {
            strB.AppendLine(Space(tab.XX) + "@model" + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewBag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Html.Partial(\"Parts/_Details\")");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<div>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Back to List\", \"Index\")");
            strB.AppendLine(Space(tab.XX) + "</div>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@section Scripts {");
            strB.AppendLine(Space(tab.XXX) + "@Scripts.Render(\"~/bundles/jqueryval\")");
            strB.AppendLine(Space(tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getIndexView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@using MVC.Parts");
            strB.AppendLine(Space(tab.XX) + "@ModelType IEnumerable<" + pClass.NameWithNameSpace + ">");
            strB.AppendLine(Space(tab.XX) + "@Code");
            strB.AppendLine(Space(tab.XXX) + "Pagination pager = (Pagination)ViewBag.page;");
            strB.AppendLine(Space(tab.XX) + "End Code");
            strB.AppendLine(Space(tab.XX) + "<h2>@Viewbag.Title</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<p>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Create New \" + @SiteVariable.CurrentAliasGroup." + pClass.Name.Capitalized + ".Capitalized, \"Create\")");
            strB.AppendLine(Space(tab.XXX) + "@ViewBag.TimeSpent");
            strB.AppendLine(Space(tab.XX) + "</p>");
            strB.AppendLine(Space(tab.XX) + "@Html.Partial(\"_PageVariables\", pager)");
            strB.AppendLine(Space(tab.XX) + "<table>");
            strB.AppendLine(Space(tab.XXX) + "<tr>");
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXXX) + "<th>");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.DisplayNameFor(Function(model) model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</th>");
            }
            strB.AppendLine(Space(tab.XXXX) + "</tr>");
            strB.AppendLine(Space(tab.XX) + "@For Each item In Model");
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXX) + "<tr>");
                strB.AppendLine(Space(tab.XXXX) + "<td>");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.DisplayFor(Function(item) item." + cVar.Name);
                strB.AppendLine(Space(tab.XXXX) + "</td>");
                strB.AppendLine(Space(tab.XXXX) + "<td>");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ActionLink(\"Edit\", \"Edit\", New With { .id=item.ID }) |");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ActionLink(\"Details\", \"Details\", New With { .id=item.ID }) |");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ActionLink(\"Delete\", \"Delete\", New With { .id=item.ID })");
                strB.AppendLine(Space(tab.XXXX) + "</td>");
                strB.AppendLine(Space(tab.XXX) + "</tr>");
            }
            strB.AppendLine(Space(tab.XX) + "Next");
            strB.AppendLine(Space(tab.XX) + "</table>");
        } else {
            strB.AppendLine(Space(tab.XX) + "@using MVC.Parts");
            strB.AppendLine(Space(tab.XX) + "@model IEnumerable<" + pClass.NameWithNameSpace + ">");
            strB.AppendLine(Space(tab.XX) + "@{");
            strB.AppendLine(Space(tab.XXX) + "Pagination pager = (Pagination)ViewBag.page;");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.XX) + "<h2>@ViewData(\"Title\")</h2>");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<p>");
            strB.AppendLine(Space(tab.XXX) + "@Html.ActionLink(\"Create New \" + @SiteVariable.CurrentAliasGroup." + pClass.Name.Capitalized + ".Capitalized, \"Create\")");
            strB.AppendLine(Space(tab.XXX) + "@ViewBag.TimeSpent");
            strB.AppendLine(Space(tab.XX) + "</p>");
            strB.AppendLine(Space(tab.XX) + "@Html.Partial(\"_PageVariables\", pager)");
            strB.AppendLine(Space(tab.XX) + "<table>");
            strB.AppendLine(Space(tab.XXX) + "<tr>");
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXXX) + "<th>");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.DisplayNameFor(model => model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</th>");
            }
            strB.AppendLine(Space(tab.XXXX) + "</tr>");
            strB.AppendLine(Space(tab.XX) + "@foreach (var item in Model){");
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXX) + "<tr>");
                strB.AppendLine(Space(tab.XXXX) + "<td>");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.DisplayFor(modelItem => item." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</td>");
                strB.AppendLine(Space(tab.XXXX) + "<td>");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ActionLink(\"Edit\", \"Edit\", new { id=item.ID }) |");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ActionLink(\"Details\", \"Details\", new { id=item.ID }) |");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ActionLink(\"Delete\", \"Delete\", new { id=item.ID })");
                strB.AppendLine(Space(tab.XXXX) + "</td>");
                strB.AppendLine(Space(tab.XXX) + "</tr>");
            }
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.XX) + "</table>");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getDetailsPartialView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@ModelType " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XXX) + "@Html.AntiForgeryToken()");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<fieldset>");
            strB.AppendLine(Space(tab.XXX) + "<legend>" + pClass.NameString + "</legend>");
            strB.AppendLine(Space(tab.XX));
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXX) + "<div class=\"display-label\">");
                strB.AppendLine(Space(tab.XXXX) + "@Html.DisplayNameFor(Function(model) model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXX) + "</div>");
                strB.AppendLine(Space(tab.XXX) + "<div class=\"display-field\">");
                strB.AppendLine(Space(tab.XXXX) + "@Html.DisplayFor(Function(model) model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXX) + "</div>");
            }
            strB.AppendLine(Space(tab.XX) + "</fieldset>");
        } else {
            strB.AppendLine(Space(tab.XX) + "@model " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XXX) + "@Html.AntiForgeryToken()");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "<fieldset>");
            strB.AppendLine(Space(tab.XXX) + "<legend>" + pClass.NameString + "</legend>");
            strB.AppendLine(Space(tab.XX));
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXX) + "<div class=\"display-label\">");
                strB.AppendLine(Space(tab.XXXX) + "@Html.DisplayNameFor(model => model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXX) + "</div>");
                strB.AppendLine(Space(tab.XXX) + "<div class=\"display-field\">");
                strB.AppendLine(Space(tab.XXXX) + "@Html.DisplayFor(model => model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXX) + "</div>");
            }
            strB.AppendLine(Space(tab.XX) + "</fieldset>");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getFormPartialView(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Space(tab.XX) + "@ModelType " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Scripts.Render(\"~/bundles/forms\")");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@using Html.BeginForm()");
            strB.AppendLine(Space(tab.XXX) + "@Html.AntiForgeryToken()");
            strB.AppendLine(Space(tab.XXX) + "@Html.ValidationSummary(true)");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XXX) + "<fieldset>");
            strB.AppendLine(Space(tab.XXXX) + "<legend>" + pClass.Name.Capitalized + "</legend>");
            strB.AppendLine(Space(tab.XXXX));
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXXX) + "<div class=\"editor-label\">");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.LabelFor(Function(model) model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</div>");
                strB.AppendLine(Space(tab.XXXX) + "<div class=\"editor-field\">");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.DisplayFor(Function(model) model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ValidationMessageFor(Function(model) model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</div>");
            }
            strB.AppendLine(Space(tab.XXXX) + "<p>");
            strB.AppendLine(Space(tab.XXXXX) + "<input type=\"submit\" value=\"@ViewBag.ButtonText\")/>");
            strB.AppendLine(Space(tab.XXXX) + "</p>");
            strB.AppendLine(Space(tab.XXX) + "</fieldset>");
            strB.AppendLine(Space(tab.XX) + "End Using");
            strB.AppendLine(Space(tab.XX) + "<script>");
            strB.AppendLine(Space(tab.XXX) + "$(\"#Name\").focus(function () {");
            strB.AppendLine(Space(tab.XXXX) + "setUniquenessChecker({");
            strB.AppendLine(Space(tab.XXXXX) + "textBox: this,");
            strB.AppendLine(Space(tab.XXXXX) + "ajaxURL: \"@Url.Action(\"IsFieldValueUnique\", \"Scanner\"),");
            strB.AppendLine(Space(tab.XXXXX) + "fieldName: \"Name\",");
            strB.AppendLine(Space(tab.XXXXX) + "ID: @Model.ID");
            strB.AppendLine(Space(tab.XXXX) + "});");
            strB.AppendLine(Space(tab.XXX) + "});");
            strB.AppendLine(Space(tab.XX) + "</script>");
        } else {
            strB.AppendLine(Space(tab.XX) + "@model " + pClass.NameWithNameSpace);
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@Scripts.Render(\"~/bundles/forms\")");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XX) + "@using (Html.BeginForm()){");
            strB.AppendLine(Space(tab.XXX) + "@Html.AntiForgeryToken()");
            strB.AppendLine(Space(tab.XXX) + "@Html.ValidationSummary(true)");
            strB.AppendLine(Space(tab.XX));
            strB.AppendLine(Space(tab.XXX) + "<fieldset>");
            strB.AppendLine(Space(tab.XXXX) + "<legend>" + pClass.NameString + "</legend>");
            strB.AppendLine(Space(tab.XXXX));
            foreach (ClassVariable cVar in pClass.ClassVariables) {
                strB.AppendLine(Space(tab.XXXX) + "<div class=\"editor-label\">");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.LabelFor(model => model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</div>");
                strB.AppendLine(Space(tab.XXXX) + "<div class=\"editor-field\">");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.EditorFor(model => model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXXX) + "@Html.ValidationMessageFor(model => model." + cVar.Name + ")");
                strB.AppendLine(Space(tab.XXXX) + "</div>");
            }
            strB.AppendLine(Space(tab.XXXX) + "<p>");
            strB.AppendLine(Space(tab.XXXXX) + "<input type=\"submit\" value=\"@ViewBag.ButtonText\")/>");
            strB.AppendLine(Space(tab.XXXX) + "</p>");
            strB.AppendLine(Space(tab.XXX) + "</fieldset>");
            strB.AppendLine(Space(tab.XX) + "}");
            strB.AppendLine(Space(tab.XX) + "<script>");
            strB.AppendLine(Space(tab.XXX) + "$(\"#Name\").focus(function () {");
            strB.AppendLine(Space(tab.XXXX) + "setUniquenessChecker({");
            strB.AppendLine(Space(tab.XXXXX) + "textBox: this,");
            strB.AppendLine(Space(tab.XXXXX) + "ajaxURL: \"@Url.Action(\"IsFieldValueUnique\", \"Scanner\"),");
            strB.AppendLine(Space(tab.XXXXX) + "fieldName: \"Name\",");
            strB.AppendLine(Space(tab.XXXXX) + "ID: @Model.ID");
            strB.AppendLine(Space(tab.XXXX) + "});");
            strB.AppendLine(Space(tab.XXX) + "});");
            strB.AppendLine(Space(tab.XX) + "</script>");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    /// <summary>
    ///     ''' Creates all view files required
    ///     ''' </summary>
    ///     ''' <param name="pClass">Project Class</param>
    ///     ''' <param name="lang"></param>
    ///     ''' <returns>A dictionary with with the name of the view as the key and the data as the value </returns>
    ///     ''' <remarks></remarks>
    public Dictionary<string, string> getAllViewPages(ProjectClass pClass, language lang) {
        Dictionary<string, string> retDict = new Dictionary<string, string>();
        retDict.Add("Create", getCreateView(pClass, lang));
        retDict.Add("Delete", getDeleteView(pClass, lang));
        retDict.Add("Details", getDetailsView(pClass, lang));
        retDict.Add("Edit", getEditView(pClass, lang));
        retDict.Add("Index", getIndexView(pClass, lang));
        retDict.Add("_Details", getDetailsPartialView(pClass, lang));
        retDict.Add("_Form", getFormPartialView(pClass, lang));
        return retDict;
    }
    private bool createFolder(string path) {
        if (Directory.Exists(path))
            return true;
        else {
            Directory.CreateDirectory(path);
            if (Directory.Exists(path))
                return true;
            else
                return false;
        }
    }
    /// <summary>
    ///     ''' Builds all view pages and puts them in the correct folder structure at the file path specified
    ///     ''' </summary>
    ///     ''' <param name="pClass"></param>
    ///     ''' <param name="lang"></param>
    ///     ''' <param name="filePath"></param>
    ///     ''' <returns></returns>
    ///     ''' <remarks></remarks>
    public bool buildViewPages(ProjectClass pClass, language lang, string filePath) {
        Dictionary<string, string> dictOfViews;

        dictOfViews = getAllViewPages(pClass, lang);
        filePath = filePath + @"\Views\";

        if (createFolder(filePath)) {
            if (dictOfViews != null && dictOfViews.Count > 0) {
                foreach (string key in dictOfViews.Keys) {
                    if (key.Contains('_')) {
                        if (createFolder(filePath + @"\" + pClass.Name.Capitalized + @"\Parts\"))
                            writeFileData(dictOfViews[key], key, filePath, lang);
                        else {
                            MessageBox.Show("Unable to create the view folder for: " + pClass.Name.Capitalized + ".  Giving up");
                            break;
                        }
                    } else if (createFolder(filePath + @"\" + pClass.Name.Capitalized + @"\"))
                        writeFileData(dictOfViews[key], key, filePath, lang);
                    else {
                        MessageBox.Show("Unable to create the view folder for: " + pClass.Name.Capitalized + ".  Giving up.");
                        break;
                    }
                }
            }
        }
    }
    private bool writeFileData(string data, string fileName, string path, language lang) {
        if (lang == language.VisualBasic) {
            if (fileName.Contains('_'))
                FileStream fs = File.Create(path + @"\Parts\" + fileName + ".vbhtml");
            else
                FileStream fs = File.Create(path + fileName + ".vbhtml");
        } else if (fileName.Contains('_'))
            FileStream fs = File.Create(path + @"\Parts\" + fileName + ".cshtml");
        else
            FileStream fs = File.Create(path + fileName + ".cshtml");
        byte[] fileData = new UTF8Encoding(true).GetBytes(data);
    }
}
