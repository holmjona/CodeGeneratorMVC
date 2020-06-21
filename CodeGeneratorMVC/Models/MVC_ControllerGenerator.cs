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
using Words;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;

public class MVC_ControllerGenerator {
    public MVC_ControllerGenerator() {
    }
    public string createFullController(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(getIndexGET(pClass, lang));
        strB.AppendLine(getDetailsGET(pClass, lang));
        strB.AppendLine(getCreateGET(pClass, lang));
        strB.AppendLine(getCreatePOST(pClass, lang));
        strB.AppendLine(getEditGET(pClass, lang));
        strB.AppendLine(getEditPOST(pClass, lang));
        strB.AppendLine(getDeleteGET(pClass, lang));
        strB.AppendLine(getDeletePOST(pClass, lang));
        strB.AppendLine(getDispose(pClass, lang));
        strB.AppendLine(getFind(pClass, lang));
        strB.AppendLine(getGetAutoCompleteObjects(pClass, lang));
        strB.AppendLine(getIsFieldValueUnique(pClass, lang));
        strB.AppendLine(getGetAsListItem(pClass, lang));
        strB.AppendLine(getGetItem(pClass, lang));
        strB.AppendLine(getExecuteCore(pClass, lang));
        return strB.ToString();
    }
    public string createDerivedController(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(getBaseControllerForClass(pClass, lang));
        return strB.ToString();
    }
    public string getBaseControllerforProject(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine("Imports System.Web.Mvc");
            strB.AppendLine("Imports AutoMapper");
            strB.AppendLine("Namespace Controllers.Shared");
            strB.AppendLine(Strings.Space((int)tab.X) + "Public Class BaseController(Of M As New, VM As New)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Inherits Controller");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Sub New()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Bind model and view model");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Mapper.CreateMap(Of M, VM)().ReverseMap()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Sub");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: Base");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Function Index() As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: Base/Edit");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Function Edit() As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Get model");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim model As New M = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Bind model and view model");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Mapper.CreateMap(Of M, VM)()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Performs mapping operation creating the viewModel object");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim viewModel As VM = Mapper.Map(Of VM)(model)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(viewModel)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Function Edit(viewModel As VM) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(\"Display\", viewModel)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: Base/Display");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Function Display() As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Get model");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim model As New M = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Bind model and view model");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Mapper.CreateMap(Of M, VM)()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "'Performs mapping operation creating the viewModel object");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim viewModel As VM = Mapper.Map(Of VM)(model)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(viewModel)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Function Display(model As VM)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return Redirect(\"Index\")");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Function Details() As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Class");
            strB.AppendLine("End Namespace");
        } else {
        }
        return strB.ToString();
    }
    private string getBaseControllerForClass(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine("Imports System.Web.Mvc");
            strB.AppendLine("Imports " + pClass.NameSpaceVariable.ToString() + ".Controllers.Shared");
            strB.AppendLine("");
            strB.AppendLine("Namespace Controllers");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Class " + pClass.Name.Capitalized() + "Controller");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Inherits BaseController(Of " + pClass.Name.Capitalized() + "Model, " + pClass.Name.Capitalized() + "ViewModel)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Class");
            strB.AppendLine("End Namespace");
        } else {
        }
        return strB.ToString();
    }
    private string getIndexGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: /" + pClass.Name.Capitalized() + "/");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + "CanModify)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Index() As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ".Capitalized(), _");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.List)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim start as DateTime = DateTime.Now;");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim lstOfObj As List(Of " + pClass.NameSpaceVariable.NameBasedOnID + ") = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "' put the newest at the start, therefore the oldest will be at the end.");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "lstOfObj.Reverse()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim pg As New Pagination(Request.QueryString, lstOfObj.Count)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.page = pg");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.TimeSpent = DateTime.Now - start");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim lst As New List(Of " + pClass.Name.Capitalized() + "ViewModel)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each i as " + pClass.NameSpaceVariable.NameBasedOnID + " in pg.getTruncatedList(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "lst.Add(New " + pClass.Name.Capitalized() + "ViewModel(i))");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Next");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(lst)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// GET: /" + pClass.Name.Capitalized() + "/");
            strB.AppendLine(Strings.Space((int)tab.XX) + "//[Permission(Role.Permission." + pClass.Name.Capitalized() + "CanModify)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Index()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ".Capitalized(), _");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.List);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "DateTime start = DateTime.Now;");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<" + pClass.NameSpaceVariable.NameBasedOnID + "> lstOfObj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "// put the newest at the start, therefore the oldest will be at the end.");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "lstOfObj.Reverse();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Pagination pg = new Pagination(Request.QueryString, lstOfObj.Count);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.page = pg;");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.TimeSpent = DateTime.Now - start;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<" + pClass.Name.Capitalized() + "ViewModel> lst = new List<" + pClass.Name.Capitalized() + "ViewModel>();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (" + pClass.NameSpaceVariable.NameBasedOnID + " i in pg.getTruncatedList(lstOfObj))");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "lst.Add(new " + pClass.Name.Capitalized() + "ViewModel(i));");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(lst)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }
    private string getDetailsGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: /" + pClass.Name.Capitalized() + "/Details/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Details(ByVal id As Integer?) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If IsNothing(id)  Then Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim obj As " + pClass.NameSpaceVariable.NameBasedOnID + " = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized()  + "(id)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If Obj Is Nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return HttpNotFound()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim vm As New " + pClass.Name.Capitalized()  + "ViewModel(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.NameString + ", _");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.Details, vm." + pClass.Name.Capitalized()  + ".Name)");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX));
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// GET: /" + pClass.Name.Capitalized()  + "/Details/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Details(int id = 0)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.NameSpaceVariable.NameBasedOnID + " obj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized()  + "(id);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj == null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return HttpNotFound();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + "ViewModel vm = new " + pClass.Name.Capitalized() + "ViewModel(obj);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ", _");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Details, vm." + pClass.Name.Capitalized() + ".Name);");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }



    private string getCreateGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: /" + pClass.Name.Capitalized() + "/Create");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + "Add)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Create() As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ", _");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.Create)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim vm As New " + pClass.Name.Capitalized() + "ViewModel(New " + pClass.Name.Capitalized() + "())");
            foreach (ClassVariable var in pClass.ClassVariables) {
                if (var.IsRequired && !var.IsForeignKey)
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "vm." + var.Name + " = " + var.DatabaseType + ".CreateEmpty()");
            }
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// GET: /" + pClass.Name.Capitalized() + "/Create");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[Permission(Role.Permission." + pClass.Name.Capitalized() + "Add)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Create()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.Create);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + "ViewModel vm = new " + pClass.Name.Capitalized() + "ViewModel(new " + pClass.Name.Capitalized() + "());");
            foreach (ClassVariable var in pClass.ClassVariables) {
                if (var.IsRequired && !var.IsForeignKey)
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "vm." + var.Name + " = " + var.DatabaseType + ".CreateEmpty();");
            }
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm;");
            strB.AppendLine(Strings.Space((int)tab.XXX));
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private string getCreatePOST(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// POST: /" + pClass.Name.Capitalized() + "/Create");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<ValidateAntiForgeryToken>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + "Add)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Create(ByVal obj As " + pClass.Name.Capitalized() + ") As ActionResult ");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim vm As New " + pClass.Name.Capitalized() + "ViewModel(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Create)");
            foreach (ClassVariable var in pClass.ClassVariables) {
                if (var.IsForeignKey) {
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "if obj." + var.Name + " < 0 Then ");
                    strB.AppendLine(Strings.Space((int)tab.XXXX) + "ModelState.AddModelError(\"" + var.Name.Remove(var.Name.Length - 2) + "Search\", SiteVariables.CurrentAliasGroup." + var.Name.Remove(var.Name.Length - 2) + ".Capitalized()");
                    strB.AppendLine(Strings.Space((int)tab.XXXXX) + "+ \" is not specified\")");
                    strB.AppendLine(Strings.Space((int)tab.XXXX) + "vm." + var.Name.Remove(var.Name.Length - 2) + " = " + var.Name.Remove(var.Name.Length - 2) + ".CreateEmpty()");
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
                }
            }

            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if ModelState.IsValid Then ");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if obj.dbAdd() > 0 Then ");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "return RedirectToAction(\"Index\")");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "vm." + pClass.Name.Capitalized() + ".DateScanned = DateTime.Now");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// POST: /" + pClass.Name.Capitalized() + "/Create");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[HttpPost]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[ValidateAntiForgeryToken]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[Permission(Role.Permission." + pClass.Name.Capitalized() + "Add)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Create(" + pClass.Name.Capitalized() + " obj)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + "ViewModel vm = new " + pClass.Name.Capitalized() + "ViewModel(obj);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Create);");
            foreach (ClassVariable var in pClass.ClassVariables) {
                if (var.IsForeignKey) {
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj." + var.Name + " < 0) ");
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
                    strB.AppendLine(Strings.Space((int)tab.XXXX) + "ModelState.AddModelError(\"" + var.Name.Remove(var.Name.Length - 2) + "Search\", SiteVariables.CurrentAliasGroup." + var.Name.Remove(var.Name.Length - 2) + ".Capitalized()");
                    strB.AppendLine(Strings.Space((int)tab.XXXXX) + "+ \" is not specified\");");
                    strB.AppendLine(Strings.Space((int)tab.XXXX) + "vm." + var.Name.Remove(var.Name.Length - 2) + " = " + var.Name.Remove(var.Name.Length - 2) + ".CreateEmpty();");
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
                }
            }

            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (ModelState.IsValid) ");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (obj.dbAdd() > 0) ");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "return RedirectToAction(\"Index\");");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "vm." + pClass.Name.Capitalized() + ".DateScanned = DateTime.Now;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private string getEditGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "' GET: /" + pClass.Name.Capitalized() + "/Edit/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + " Edit)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public Function Edit(ByVal id As Integer?) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If IsNothing(id) Then Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim obj As " + pClass.NameSpaceVariable.NameBasedOnID + "= " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(id)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.updateWarning = \"<p class='message-warning'>Must click update in order to save changes.</p>\"");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If obj Is Nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return HttpNotFound()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ButtonText = \"Update\"");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim vm as new " + pClass.Name.Capitalized() + "ViewModel(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.Edit, vm." + pClass.Name.Capitalized() + ".Name)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// GET: /" + pClass.Name.Capitalized() + "/Edit/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[Permission(Role.Permission." + pClass.Name.Capitalized() + "Edit)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Edit(int id = 0)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.NameSpaceVariable.NameBasedOnID + " obj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(id);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.updateWarning = \"<p class='message-warning'>Must click update in order to save changes.</p>\";");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj == null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return HttpNotFound();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ButtonText = \"Update\";");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + "ViewModel vm = new " + pClass.Name.Capitalized() + "ViewModel(obj);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ControllerMethods.ControllerAction.Edit, vm." + pClass.Name.Capitalized() + ".Name);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private string getEditPOST(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// POST: /" + pClass.Name.Capitalized() + "/Edit/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<ValidateAntiForgeryToken>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + "Edit)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Edit(" + pClass.Name.Capitalized() + " obj) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If ModelState.IsValid Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If obj.dbUpdate() > 0 Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "If Request.QueryString(\"redirect\") = \"True\" Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Dim hist as new List (Of PageHistoryItem)()");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "hist.Add(SiteVariables.History(SiteVariables.History.Count() - 2))");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "return RedirectToAction(hist(0).Route(\"action\").ToString(), _");
            strB.AppendLine(Strings.Space((int)tab.XXXXXXX) + "hist(0).Route(\"controller\").ToString(), new { id = hist[0].Route(\"id\") })");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "return RedirectToAction(\"Index\")");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "//error saving changes.");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "Return View(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim vm as New " + pClass.Name.Capitalized() + "ViewModel(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Edit, vm." + pClass.Name.Capitalized() + ".Name)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "// POST: /" + pClass.Name.Capitalized() + "/Edit/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[HttpPost]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[ValidateAntiForgeryToken]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[Permission(Role.Permission." + pClass.Name.Capitalized() + "Edit)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Edit(" + pClass.Name.Capitalized() + " obj)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (ModelState.IsValid)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (obj.dbUpdate() > 0)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "if (Request.QueryString[\"redirect\"] == \"True\")");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "List<PageHistoryItem> hist = new List<PageHistoryItem>();");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "hist.Add(SiteVariables.History[SiteVariables.History.Count() - 2]);");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "return RedirectToAction(hist[0].Route[\"action\"].ToString(), _");
            strB.AppendLine(Strings.Space((int)tab.XXXXXXX) + "hist[0].Route[\"controller\"].ToString(), new { id = hist[0].Route[\"id\"] });");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "return RedirectToAction(\"Index\");");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "//error saving changes.");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "return View(obj);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + "ViewModel vm = new " + pClass.Name.Capitalized() + "ViewModel(obj);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Edit, vm." + pClass.Name.Capitalized() + ".Name);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private string getDeleteGET(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + "Delete)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Delete(Optional id As Integer = 0) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim obj As " + pClass.NameSpaceVariable.NameBasedOnID + " = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(id)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim photo As List(Of Photo) = " + pClass.DALClassVariable.Name + ".GetPhotos()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim scan As List(Of Scan) = " + pClass.DALClassVariable.Name + ".GetScans()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim specInProj As List(Of SpecimenInProject)  = " + pClass.DALClassVariable.Name + ".GetSpecimenInProjects()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim mt As List(Of ModelTable)  = " + pClass.DALClassVariable.Name + ".GetModelTables()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim ListOfErrors As New List(Of AssociationErrorsList)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim asList As New List(Of AssociationErrorsList)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "asList.ListOfErrors = New List(Of AssociationError)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each p As Photo in photo");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If p.SpecimenID = specimen.ID Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "asList.ListOfErrors.Add(New AssociationError With");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".Model = \"Photo\",");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + @".MessageText = SiteVariables.CurrentAliasGroup.Photo + \"" \ "" + p.FileName,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".Destination = p.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "})");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End For");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each s As Scan in scan");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If s.SpecimenID = specimen.ID Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "asList.ListOfErrors.Add(New AssociationError With");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".Model = \"Scan\",");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".MessageText = SiteVariables.CurrentAliasGroup.Scan + \" for \" + IVLDAL.GetPerson(s.PersonID).FullName,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".Destination = s.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "})");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End For");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each sip As SpecimenInProject in specInProj");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If sip.SpecimenID = specimen.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "asList.ListOfErrors.Add(New AssociationError With");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".Model = \"Model\",");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".MessageText = SiteVariables.CurrentAliasGroup.Model + \" \" + m.FileLocation,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + ".Destination = m.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "})");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End For");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If asList.ListOfErrors.Count > 0 Then");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ListOfErrors.Add(asList)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "asList.Groupingtitle = \"You cannot delete the \" + SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ".Capitalized() + \" \" + obj.Name +");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "\" until the following dependencies are removed:");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ListOfErrors = ListOfErrors");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If obj is Nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return HttpNotFound()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim vm as New " + pClass.Name.Capitalized() + "ViewModel(obj)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Delete, vm." + pClass.Name.Capitalized() + ".Name)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return View(vm)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "[Permission(Role.Permission." + pClass.Name.Capitalized() + "Delete)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Delete(int id = 0)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.NameSpaceVariable.NameBasedOnID + " obj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(id);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<Photo> photo = " + pClass.DALClassVariable.Name + ".GetPhotos();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<Scan> scan = " + pClass.DALClassVariable.Name + ".GetScans();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<SpecimenInProject> specInProj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "InProjects();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<ModelTable> mt = " + pClass.DALClassVariable.Name + ".GetModelTables();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<AssociationErrorsList> ListOfErrors = new List<AssociationErrorsList>();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "AssociationErrorsList asList = new AssociationErrorsList();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "asList.ListOfErrors = new List<AssociationError>();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (Photo p in photo)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (p.ObjID == " + pClass.Name.Capitalized() + ".ID)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "asList.ListOfErrors.Add(new AssociationError()");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Model = \"Photo\",");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + @"MessageText = SiteVariables.CurrentAliasGroup.Photo + \"" \ "" + p.FileName,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Destination = p.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "});");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (Scan s in scan)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if ( .Obj == " + pClass.Name.Capitalized() + ".ID)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "asList.ListOfErrors.Add(new AssociationError())");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Model = \"Scan\",");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "MessageText = SiteVariables.CurrentAliasGroup.Scan + \" for \" + " + pClass.DALClassVariable.Name + ".GetPerson(s.PersonID).FullName,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Destination = s.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "});");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (SpecimenInProject sip in specInProj)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (sip.SpecimenID == specimen.ID)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "asList.ListOfErrors.Add(new AssociationError()");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Model = \"Model\",");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "MessageText = SiteVariables.CurrentAliasGroup.Model + \" \" + m.FileLocation,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "Destination = m.ID");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "});");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (asList.ListOfErrors.Count > 0)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "ListOfErrors.Add(asList);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "asList.Groupingtitle = \"You cannot delete the \" + SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ".Capitalized() + \" \" + obj.Name +");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "\" until the following dependencies are removed:\";");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ListOfErrors = ListOfErrors;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (obj == null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return HttpNotFound();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.Name.Capitalized() + "ViewModel vm = new " + pClass.Name.Capitalized() + "ViewModel(obj);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ViewBag.ViewModel = vm;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." + pClass.Name.Capitalized() + ",");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "ControllerMethods.ControllerAction.Delete, vm." + pClass.Name.Capitalized() + ".Name);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return View(vm);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private string getDeletePOST(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "//POST: /" + pClass.Name.Capitalized() + "/Delete/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost, ActionName(\"Delete\")>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<ValidateAntiForgeryToken>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "<Permission(Role.Permission." + pClass.Name.Capitalized() + "Delete)>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function DeleteConfirmed(ByVal id As Integer) As ActionLink");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim obj As " + pClass.NameSpaceVariable.NameBasedOnID + " = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(id)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "obj.dbRemove()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return RedirectToAction(\"Index\")");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "//POST: /" + pClass.Name.Capitalized() + "/Delete/5");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[HttpPost, ActionName(\"Delete\")]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[ValidateAntiForgeryToken]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "[Permission(Role.Permission." + pClass.Name.Capitalized() + "Delete)]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult DeleteConfirmed(int id)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.NameSpaceVariable.NameBasedOnID + " obj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(id);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "obj.dbRemove();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return RedirectToAction(\"Index\");");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
        }
        return strB.ToString();
    }
    private string getDispose(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Protected Overrides sub Dispose(ByVal disposing As Boolean)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "base.Dispose(disposing)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Sub");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "protected override void Dispose(bool disposing)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "base.Dispose(disposing);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
        }
        return strB.ToString();
    }

    private string getFind(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function Find(ByVal searchText As string, ByVal count As Integer) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim createNew As Boolean = False");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If Request.Form(\"showcreate\") <> nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "createNew = Boolean.Parse(Request.Form(\"showcreate\"))");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim showNone As Boolean = False");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If Request.Form(\"shownone\") <> nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "showNone = Boolean.Parse(Request.Form[\"shownone\"])");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "showNone = False");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If Request.Form(\"shownone\") <> nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "showNone = Boolean.Parse(Request.Form[\"shownone\"])");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return Json(new { ok = true, data = getAutoCompleteObjects(searchText, count, " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "(), createNew, showNone), message = \"ok\" })");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "[HttpPost]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult Find(string searchText, int count)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "bool createNew = false;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if ( Request.Form[\"showcreate\"] != null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "createNew = bool.Parse(Request.Form[\"showcreate\"]);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "bool showNone = false;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (Request.Form[\"shownone\"] != null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "showNone = bool.Parse(Request.Form[\"shownone\"]);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "bool showNone = false;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (Request.Form[\"shownone\"] != null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "showNone = bool.Parse(Request.Form[\"shownone\"]);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return Json(new { ok = true, data = getAutoCompleteObjects(searchText, count, " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "(), createNew, showNone), message = \"ok\" });");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
        }
        return strB.ToString();
    }

    private string getGetAutoCompleteObjects(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Static Function getAutoCompleteObjects(searchText as string, count as Integer, list as List (Of " + pClass.NameSpaceVariable.NameBasedOnID + ")) As  List (Of AutoCompleterObject)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim searchstr As string = searchText.ToLower()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim lst as List (Of AutoCompleterObject) lst = New List (Of AutoCompleterObject)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim useMe as Boolean = false");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each obj as " + pClass.NameWithNameSpace + " in list");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If obj.Name.ToLower().Contains(searchstr) Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "useMe = true");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else If obj.Number.ToLower().Contains(searchstr) Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "useMe = true");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "useMe = false");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If useMe Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "Dim aObj As AutoCompleterObject = new AutoCompleterObject(");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "obj.Name, obj.Number,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "\"" + pClass.Name.ToString() + "_\" + obj.ID,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "obj.ID.ToString())");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "lst.Add(aObj)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If lst.Count >= count Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "Exit For");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End For");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return lst");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "public static List<AutoCompleterObject> getAutoCompleteObjects(string searchText, int count, List<" + pClass.NameSpaceVariable.NameBasedOnID + "> list)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "string searchstr = searchText.ToLower();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "List<AutoCompleterObject> lst = new List<AutoCompleterObject>();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "bool useMe = false;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (" + pClass.NameWithNameSpace + " obj in list)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (obj.Name.ToLower().Contains(searchstr))");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "useMe = true;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "else if (obj.Number.ToLower().Contains(searchstr))");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "useMe = true;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "useMe = false;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (useMe)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "AutoCompleterObject aObj = new AutoCompleterObject(");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "obj.Name, obj.Number,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "\"" + pClass.Name.ToString() + "_\" + obj.ID,");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "obj.ID.ToString());");
            strB.AppendLine(Strings.Space((int)tab.XXXXXX) + "lst.Add(aObj);");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (lst.Count >= count)");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "break;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return lst;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    private string getIsFieldValueUnique(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function IsFieldValueUnique(ByVal searchText As string, ByVal fieldName As string, ByVal objID As string) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim isunique as Boolean? = True");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each obj As " + pClass.Name.Capitalized() + " in " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "()");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If obj.ID.ToString() <> objID Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "isunique = obj.IsFieldValueUnique(fieldName, searchText)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "If isunique = nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "isunique = False");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "Exit For");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Else If Not isunique Then");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "Exit For");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return Json(new { ok = isunique, message = \"ok\" })");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "[HttpPost]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult IsFieldValueUnique(string searchText, string fieldName, string objID)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "bool? isunique = true;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (" + pClass.Name.Capitalized() + " obj in " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "())");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (obj.ID.ToString() != objID)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "isunique = obj.IsFieldValueUnique(fieldName, searchText);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "if (isunique == null)");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "isunique = false;");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "break;");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "else if (isunique == false)");
            strB.AppendLine(Strings.Space((int)tab.XXXXX) + "break;");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return Json(new { ok = isunique, message = \"ok\" });");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }

    // Rick: Where did this come from?
    public string getGetAsListItem(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "<HttpPost>");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Function GetAsListItem(ByVal objID as Integer) As ActionResult");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Try");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return Json(new { ok = true, data = GetItem(objID), message = \"ok\" })");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Catch ex As Exception");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Return Json(new { ok = false, message = ex.Message });");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End Try");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "[HttpPost]");
            strB.AppendLine(Strings.Space((int)tab.XX) + "public ActionResult GetAsListItem(int objID)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "try");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return Json(new { ok = true, data = GetItem(objID), message = \"ok\" });");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "catch (Exception ex)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "return Json(new { ok = false, message = ex.Message });");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
        }
        return strB.ToString();
    }
    private string getGetItem(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Public Static Function GetItem(ByVal objID As Integer) As String");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim obj As " + pClass.NameSpaceVariable.NameBasedOnID + " = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(objID)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim strb As New StringBuilder");
            strB.AppendLine(Strings.Space((int)tab.XXX) + @"strb.Append(""<li id=\""" + pClass.Name.ToString() + "_\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "strb.Append(objID.ToString())");
            strB.AppendLine(Strings.Space((int)tab.XXX) + @"strb.Append(""\"">"")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "strb.Append(obj.Name)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + @"strb.Append(""<button onclick=\""removeItem(this)\"" class=\""remove\"" title=\""Remove ");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + @"+ obj.Name + "" from list\"" >Remove Me</button>"")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "strb.Append(\"</li>\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Return strb.ToString()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End function");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "public static String GetItem(int objID)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.NameSpaceVariable.NameBasedOnID + " obj = " + pClass.DALClassVariable.Name + ".Get" + pClass.Name.Capitalized() + "(objID);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "StringBuilder strb = new StringBuilder();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + @"strb.Append(""<li id=\""" + pClass.Name.ToString() + "_\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "strb.Append(objID.ToString());");
            strB.AppendLine(Strings.Space((int)tab.XXX) + @"strb.Append(""\"">"");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "strb.Append(obj.Name);");
            strB.AppendLine(Strings.Space((int)tab.XXX) + @"strb.Append(""<button onclick=\""removeItem(this)\"" class=\""remove\"" title=\""Remove ");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + @"+ obj.Name + "" from list\"" >Remove Me</button>"");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "strb.Append(\"</li>\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "return strb.ToString();");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }
    private string getExecuteCore(ProjectClass pClass, language lang) {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic) {
            strB.AppendLine(Strings.Space((int)tab.XX) + "Protected Overrides sub ExecuteCore()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "throw New NotImplementedException()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End Sub");
        } else {
            strB.AppendLine(Strings.Space((int)tab.XX) + "protected override void ExecuteCore()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "throw new NotImplementedException();");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        return strB.ToString();
    }
}
