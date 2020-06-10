Option Strict On

Imports EnvDTE
Imports System.Collections.Generic
Imports IRICommonObjects.Words
Imports System.Text
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs

Public Class MVC_ControllerGenerator
    Public Sub New()

    End Sub
    Public Function createFullController(ByVal pClass As ProjectClass, ByVal lang As language) As String
        Dim strB As New StringBuilder
        strB.AppendLine(getIndexGET(pClass, lang))
        strB.AppendLine(getDetailsGET(pClass, lang))
        strB.AppendLine(getCreateGET(pClass, lang))
        strB.AppendLine(getCreatePOST(pClass, lang))
        strB.AppendLine(getEditGET(pClass, lang))
        strB.AppendLine(getEditPOST(pClass, lang))
        strB.AppendLine(getDeleteGET(pClass, lang))
        strB.AppendLine(getDeletePOST(pClass, lang))
        strB.AppendLine(getDispose(pClass, lang))
        strB.AppendLine(getFind(pClass, lang))
        strB.AppendLine(getGetAutoCompleteObjects(pClass, lang))
        strB.AppendLine(getIsFieldValueUnique(pClass, lang))
        strB.AppendLine(getGetAsListItem(pClass, lang))
        strB.AppendLine(getGetItem(pClass, lang))
        strB.AppendLine(getExecuteCore(pClass, lang))
        Return strB.ToString
    End Function
    Public Function createDerivedController(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        strB.AppendLine(getBaseControllerForClass(pClass, lang))
        Return strB.ToString
    End Function
    Public Function getBaseControllerforProject(ByVal pClass As ProjectClass, ByVal lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine("Imports System.Web.Mvc")
            strB.AppendLine("Imports AutoMapper")
            strB.AppendLine("Namespace Controllers.Shared")
            strB.AppendLine(Space(tab.X) & "Public Class BaseController(Of M As New, VM As New)")
            strB.AppendLine(Space(tab.XX) & "Inherits Controller")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "Public Sub New()")
            strB.AppendLine(Space(tab.XXX) & "'Bind model and view model")
            strB.AppendLine(Space(tab.XXX) & "Mapper.CreateMap(Of M, VM)().ReverseMap()")
            strB.AppendLine(Space(tab.XX) & "End Sub")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "' GET: Base")
            strB.AppendLine(Space(tab.XX) & "Function Index() As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "Return View()")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "' GET: Base/Edit")
            strB.AppendLine(Space(tab.XX) & "Function Edit() As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "'Get model")
            strB.AppendLine(Space(tab.XXX) & "Dim model As New M = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "()")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "'Bind model and view model")
            strB.AppendLine(Space(tab.XXX) & "Mapper.CreateMap(Of M, VM)()")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "'Performs mapping operation creating the viewModel object")
            strB.AppendLine(Space(tab.XXX) & "Dim viewModel As VM = Mapper.Map(Of VM)(model)")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "Return View(viewModel)")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "Function Edit(viewModel As VM) As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "Return View(""Display"", viewModel)")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XX) & "' GET: Base/Display")
            strB.AppendLine(Space(tab.XX) & "Function Display() As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "'Get model")
            strB.AppendLine(Space(tab.XXX) & "Dim model As New M = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "()")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "'Bind model and view model")
            strB.AppendLine(Space(tab.XXX) & "Mapper.CreateMap(Of M, VM)()")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "'Performs mapping operation creating the viewModel object")
            strB.AppendLine(Space(tab.XXX) & "Dim viewModel As VM = Mapper.Map(Of VM)(model)")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "Return View(viewModel)")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "Function Display(model As VM)")
            strB.AppendLine(Space(tab.XXX) & "Return Redirect(""Index"")")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "Function Details() As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "Return View()")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.X) & "End Class")
            strB.AppendLine("End Namespace")
        Else 'c-sharp

        End If
        Return strB.ToString
    End Function
    Private Function getBaseControllerForClass(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine("Imports System.Web.Mvc")
            strB.AppendLine("Imports " & pClass.NameSpaceVariable.ToString & ".Controllers.Shared")
            strB.AppendLine("")
            strB.AppendLine("Namespace Controllers")
            strB.AppendLine(Space(tab.XX) & "Public Class " & pClass.Name.Capitalized & "Controller")
            strB.AppendLine(Space(tab.XXX) & "Inherits BaseController(Of " & pClass.Name.Capitalized & "Model, " & pClass.Name.Capitalized & "ViewModel)")
            strB.AppendLine(Space(tab.XX) & "End Class")
            strB.AppendLine("End Namespace")
        Else 'c-sharp

        End If
        Return strB.ToString
    End Function
    Private Function getIndexGET(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "' GET: /" & pClass.Name.Capitalized & "/")
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & "CanModify)>")
            strB.AppendLine(Space(tab.XX) & "Public Function Index() As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ".Capitalized, _")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.List)")
            strB.AppendLine(Space(tab.XXX) & "Dim start as DateTime = DateTime.Now;")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "Dim lstOfObj As List(Of " & pClass.NameSpaceVariable.NameBasedOnID & ") = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.PluralAndCapitalized & "()")
            strB.AppendLine(Space(tab.XXX) & "' put the newest at the start, therefore the oldest will be at the end.")
            strB.AppendLine(Space(tab.XXX) & "lstOfObj.Reverse()")
            strB.AppendLine(Space(tab.XXX) & "Dim pg As New Pagination(Request.QueryString, lstOfObj.Count)")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.page = pg")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "ViewBag.TimeSpent = DateTime.Now - start")
            strB.AppendLine(Space(tab.XXX) & "Dim lst As New List(Of " & pClass.Name.Capitalized & "ViewModel)")
            strB.AppendLine(Space(tab.XXX) & "For Each i as " & pClass.NameSpaceVariable.NameBasedOnID & " in pg.getTruncatedList(obj)")
            strB.AppendLine(Space(tab.XXXX) & "lst.Add(New " & pClass.Name.Capitalized & "ViewModel(i))")
            strB.AppendLine(Space(tab.XXX) & "Next")
            strB.AppendLine(Space(tab.XXX) & "return View(lst)")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "// GET: /" & pClass.Name.Capitalized & "/")
            strB.AppendLine(Space(tab.XX) & "//[Permission(Role.Permission." & pClass.Name.Capitalized & "CanModify)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Index()")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ".Capitalized, _")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.List);")
            strB.AppendLine(Space(tab.XXX) & "DateTime start = DateTime.Now;")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "List<" & pClass.NameSpaceVariable.NameBasedOnID & "> lstOfObj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.PluralAndCapitalized & "();")
            strB.AppendLine(Space(tab.XXX) & "// put the newest at the start, therefore the oldest will be at the end.")
            strB.AppendLine(Space(tab.XXX) & "lstOfObj.Reverse();")
            strB.AppendLine(Space(tab.XXX) & "Pagination pg = new Pagination(Request.QueryString, lstOfObj.Count);")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.page = pg;")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "ViewBag.TimeSpent = DateTime.Now - start;")
            strB.AppendLine(Space(tab.XXX) & "List<" & pClass.Name.Capitalized & "ViewModel> lst = new List<" & pClass.Name.Capitalized & "ViewModel>();")
            strB.AppendLine(Space(tab.XXX) & "foreach (" & pClass.NameSpaceVariable.NameBasedOnID & " i in pg.getTruncatedList(lstOfObj))")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "lst.Add(new " & pClass.Name.Capitalized & "ViewModel(i));")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "return View(lst)")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function
    Private Function getDetailsGET(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "' GET: /" & pClass.Name.Capitalized & "/Details/5")
            strB.AppendLine(Space(tab.XX) & "Public Function Details(ByVal id As Integer?) As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "If IsNothing(id)  Then Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)")
            strB.AppendLine(Space(tab.XXX) & "Dim obj As " & pClass.NameSpaceVariable.NameBasedOnID & " = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id)")
            strB.AppendLine(Space(tab.XXX) & "If Obj Is Nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "Return HttpNotFound()")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "Dim vm As New " & pClass.Name.Capitalized & "ViewModel(obj)")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.NameString & ", _")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.Details, vm." & pClass.Name.Capitalized & ".Name)")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "Return View(vm)")
            strB.AppendLine(Space(tab.XX))
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "// GET: /" & pClass.Name.Capitalized & "/Details/5")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Details(int id = 0)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & " obj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id);")
            strB.AppendLine(Space(tab.XXX) & "if (obj == null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "return HttpNotFound();")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & "ViewModel vm = new " & pClass.Name.Capitalized & "ViewModel(obj);")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm;")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ", _")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Details, vm." & pClass.Name.Capitalized & ".Name);")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function



    Private Function getCreateGET(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "' GET: /" & pClass.Name.Capitalized & "/Create")
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & "Add)>")
            strB.AppendLine(Space(tab.XX) & "Public Function Create() As ActionResult")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ", _")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.Create)")
            strB.AppendLine(Space(tab.XXX) & "Dim vm As New " & pClass.Name.Capitalized & "ViewModel(New " & pClass.Name.Capitalized & "())")
            For Each var As ClassVariable In pClass.ClassVariables
                If var.IsRequired AndAlso Not var.IsForeignKey Then 'don't grab fields that are ID fields because I need the var name to try and get the object type
                    strB.AppendLine(Space(tab.XXX) & "vm." & var.Name & " = " & var.DatabaseType & ".CreateEmpty()")
                End If
            Next
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "Return View(vm)")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "// GET: /" & pClass.Name.Capitalized & "/Create")
            strB.AppendLine(Space(tab.XX) & "[Permission(Role.Permission." & pClass.Name.Capitalized & "Add)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Create()")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.Create);")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & "ViewModel vm = new " & pClass.Name.Capitalized & "ViewModel(new " & pClass.Name.Capitalized & "());")
            For Each var As ClassVariable In pClass.ClassVariables
                If var.IsRequired AndAlso Not var.IsForeignKey Then 'don't grab fields that are ID fields because I need the var name to try and get the object type
                    strB.AppendLine(Space(tab.XXX) & "vm." & var.Name & " = " & var.DatabaseType & ".CreateEmpty();")
                End If
            Next
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm;")
            strB.AppendLine(Space(tab.XXX))
            strB.AppendLine(Space(tab.XXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Function getCreatePOST(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "// POST: /" & pClass.Name.Capitalized & "/Create")
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "<ValidateAntiForgeryToken>")
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & "Add)>")
            strB.AppendLine(Space(tab.XX) & "Public Function Create(ByVal obj As " & pClass.Name.Capitalized & ") As ActionResult ")
            strB.AppendLine(Space(tab.XXX) & "Dim vm As New " & pClass.Name.Capitalized & "ViewModel(obj)")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Create)")
            For Each var As ClassVariable In pClass.ClassVariables
                If var.IsForeignKey Then
                    strB.AppendLine(Space(tab.XXX) & "if obj." & var.Name & " < 0 Then ")
                    strB.AppendLine(Space(tab.XXXX) & "ModelState.AddModelError(""" & var.Name.Remove(var.Name.Length - 2) & "Search"", SiteVariables.CurrentAliasGroup." & var.Name.Remove(var.Name.Length - 2) & ".Capitalized")
                    strB.AppendLine(Space(tab.XXXXX) & "+ "" is not specified"")")
                    strB.AppendLine(Space(tab.XXXX) & "vm." & var.Name.Remove(var.Name.Length - 2) & " = " & var.Name.Remove(var.Name.Length - 2) & ".CreateEmpty()")
                    strB.AppendLine(Space(tab.XXX) & "End If")
                End If
            Next

            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm")
            strB.AppendLine(Space(tab.XXX) & "if ModelState.IsValid Then ")
            strB.AppendLine(Space(tab.XXXX) & "if obj.dbAdd() > 0 Then ")
            strB.AppendLine(Space(tab.XXXXX) & "return RedirectToAction(""Index"")")
            strB.AppendLine(Space(tab.XXXX) & "Else")
            strB.AppendLine(Space(tab.XXXXX) & "return View(vm)")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "vm." & pClass.Name.Capitalized & ".DateScanned = DateTime.Now")
            strB.AppendLine(Space(tab.XXX) & "return View(vm)")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "// POST: /" & pClass.Name.Capitalized & "/Create")
            strB.AppendLine(Space(tab.XX) & "[HttpPost]")
            strB.AppendLine(Space(tab.XX) & "[ValidateAntiForgeryToken]")
            strB.AppendLine(Space(tab.XX) & "[Permission(Role.Permission." & pClass.Name.Capitalized & "Add)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Create(" & pClass.Name.Capitalized & " obj)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & "ViewModel vm = new " & pClass.Name.Capitalized & "ViewModel(obj);")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Create);")
            For Each var As ClassVariable In pClass.ClassVariables
                If var.IsForeignKey Then
                    strB.AppendLine(Space(tab.XXX) & "if (obj." & var.Name & " < 0) ")
                    strB.AppendLine(Space(tab.XXX) & "{")
                    strB.AppendLine(Space(tab.XXXX) & "ModelState.AddModelError(""" & var.Name.Remove(var.Name.Length - 2) & "Search"", SiteVariables.CurrentAliasGroup." & var.Name.Remove(var.Name.Length - 2) & ".Capitalized")
                    strB.AppendLine(Space(tab.XXXXX) & "+ "" is not specified"");")
                    strB.AppendLine(Space(tab.XXXX) & "vm." & var.Name.Remove(var.Name.Length - 2) & " = " & var.Name.Remove(var.Name.Length - 2) & ".CreateEmpty();")
                    strB.AppendLine(Space(tab.XXX) & "}")
                End If
            Next

            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm;")
            strB.AppendLine(Space(tab.XXX) & "if (ModelState.IsValid) ")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (obj.dbAdd() > 0) ")
            strB.AppendLine(Space(tab.XXXXX) & "return RedirectToAction(""Index"");")
            strB.AppendLine(Space(tab.XXXX) & "Else")
            strB.AppendLine(Space(tab.XXXXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "vm." & pClass.Name.Capitalized & ".DateScanned = DateTime.Now;")
            strB.AppendLine(Space(tab.XXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Function getEditGET(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "' GET: /" & pClass.Name.Capitalized & "/Edit/5")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & " Edit)>")
            strB.AppendLine(Space(tab.XX) & "public Function Edit(ByVal id As Integer?) As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "If IsNothing(id) Then Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)")
            strB.AppendLine(Space(tab.XXX) & "Dim obj As " & pClass.NameSpaceVariable.NameBasedOnID & "= " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id)")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.updateWarning = ""<p class='message-warning'>Must click update in order to save changes.</p>""")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "If obj Is Nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "return HttpNotFound()")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ButtonText = ""Update""")
            strB.AppendLine(Space(tab.XXX) & "Dim vm as new " & pClass.Name.Capitalized & "ViewModel(obj)")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.Edit, vm." & pClass.Name.Capitalized & ".Name)")
            strB.AppendLine(Space(tab.XXX) & "Return View(vm)")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "// GET: /" & pClass.Name.Capitalized & "/Edit/5")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "[Permission(Role.Permission." & pClass.Name.Capitalized & "Edit)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Edit(int id = 0)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & " obj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id);")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.updateWarning = ""<p class='message-warning'>Must click update in order to save changes.</p>"";")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "if (obj == null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "return HttpNotFound();")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ButtonText = ""Update"";")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & "ViewModel vm = new " & pClass.Name.Capitalized & "ViewModel(obj);")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm;")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXXX) & "ControllerMethods.ControllerAction.Edit, vm." & pClass.Name.Capitalized & ".Name);")
            strB.AppendLine(Space(tab.XXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Function getEditPOST(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "// POST: /" & pClass.Name.Capitalized & "/Edit/5")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "<ValidateAntiForgeryToken>")
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & "Edit)>")
            strB.AppendLine(Space(tab.XX) & "Public Function Edit(" & pClass.Name.Capitalized & " obj) As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "If ModelState.IsValid Then")
            strB.AppendLine(Space(tab.XXXX) & "If obj.dbUpdate() > 0 Then")
            strB.AppendLine(Space(tab.XXXXX) & "If Request.QueryString(""redirect"") = ""True"" Then")
            strB.AppendLine(Space(tab.XXXXXX) & "Dim hist as new List (Of PageHistoryItem)()")
            strB.AppendLine(Space(tab.XXXXXX) & "hist.Add(SiteVariables.History(SiteVariables.History.Count() - 2))")
            strB.AppendLine(Space(tab.XXXXXX) & "return RedirectToAction(hist(0).Route(""action"").ToString(), _")
            strB.AppendLine(Space(tab.XXXXXXX) & "hist(0).Route(""controller"").ToString(), new { id = hist[0].Route(""id"") })")
            strB.AppendLine(Space(tab.XXXXX) & "Else")
            strB.AppendLine(Space(tab.XXXXXX) & "return RedirectToAction(""Index"")")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXXX) & "Else")
            strB.AppendLine(Space(tab.XXXXX) & "//error saving changes.")
            strB.AppendLine(Space(tab.XXXXX) & "Return View(obj)")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "Dim vm as New " & pClass.Name.Capitalized & "ViewModel(obj)")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Edit, vm." & pClass.Name.Capitalized & ".Name)")
            strB.AppendLine(Space(tab.XXX) & "Return View(vm)")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "// POST: /" & pClass.Name.Capitalized & "/Edit/5")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XX) & "[HttpPost]")
            strB.AppendLine(Space(tab.XX) & "[ValidateAntiForgeryToken]")
            strB.AppendLine(Space(tab.XX) & "[Permission(Role.Permission." & pClass.Name.Capitalized & "Edit)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Edit(" & pClass.Name.Capitalized & " obj)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "if (ModelState.IsValid)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (obj.dbUpdate() > 0)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "if (Request.QueryString[""redirect""] == ""True"")")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & "List<PageHistoryItem> hist = new List<PageHistoryItem>();")
            strB.AppendLine(Space(tab.XXXXXX) & "hist.Add(SiteVariables.History[SiteVariables.History.Count() - 2]);")
            strB.AppendLine(Space(tab.XXXXXX) & "return RedirectToAction(hist[0].Route[""action""].ToString(), _")
            strB.AppendLine(Space(tab.XXXXXXX) & "hist[0].Route[""controller""].ToString(), new { id = hist[0].Route[""id""] });")
            strB.AppendLine(Space(tab.XXXXX) & "}")
            strB.AppendLine(Space(tab.XXXXX) & "else")
            strB.AppendLine(Space(tab.XXXXXX) & "return RedirectToAction(""Index"");")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "else")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "//error saving changes.")
            strB.AppendLine(Space(tab.XXXXX) & "return View(obj);")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & "ViewModel vm = new " & pClass.Name.Capitalized & "ViewModel(obj);")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm;")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Edit, vm." & pClass.Name.Capitalized & ".Name);")
            strB.AppendLine(Space(tab.XXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Function getDeleteGET(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & "Delete)>")
            strB.AppendLine(Space(tab.XX) & "Public Function Delete(Optional id As Integer = 0) As ActionResult")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "Dim obj As " & pClass.NameSpaceVariable.NameBasedOnID & " = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id)")
            strB.AppendLine(Space(tab.XXX) & "Dim photo As List(Of Photo) = " & pClass.DALClassVariable.Name & ".GetPhotos()")
            strB.AppendLine(Space(tab.XXX) & "Dim scan As List(Of Scan) = " & pClass.DALClassVariable.Name & ".GetScans()")
            strB.AppendLine(Space(tab.XXX) & "Dim specInProj As List(Of SpecimenInProject)  = " & pClass.DALClassVariable.Name & ".GetSpecimenInProjects()")
            strB.AppendLine(Space(tab.XXX) & "Dim mt As List(Of ModelTable)  = " & pClass.DALClassVariable.Name & ".GetModelTables()")
            strB.AppendLine(Space(tab.XXX) & "Dim ListOfErrors As New List(Of AssociationErrorsList)")
            strB.AppendLine(Space(tab.XXX) & "Dim asList As New List(Of AssociationErrorsList)")
            strB.AppendLine(Space(tab.XXX) & "asList.ListOfErrors = New List(Of AssociationError)")
            strB.AppendLine(Space(tab.XXX) & "For Each p As Photo in photo")
            strB.AppendLine(Space(tab.XXXX) & "If p.SpecimenID = specimen.ID Then")
            strB.AppendLine(Space(tab.XXXX) & "")
            strB.AppendLine(Space(tab.XXXXX) & "asList.ListOfErrors.Add(New AssociationError With")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & ".Model = ""Photo"",")
            strB.AppendLine(Space(tab.XXXXXX) & ".MessageText = SiteVariables.CurrentAliasGroup.Photo + \"" \ "" + p.FileName,")
            strB.AppendLine(Space(tab.XXXXXX) & ".Destination = p.ID")
            strB.AppendLine(Space(tab.XXXXX) & "})")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "End For")
            strB.AppendLine(Space(tab.XXX) & "For Each s As Scan in scan")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXXX) & "If s.SpecimenID = specimen.ID Then")
            strB.AppendLine(Space(tab.XXXX) & "")
            strB.AppendLine(Space(tab.XXXXX) & "asList.ListOfErrors.Add(New AssociationError With")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & ".Model = ""Scan"",")
            strB.AppendLine(Space(tab.XXXXXX) & ".MessageText = SiteVariables.CurrentAliasGroup.Scan + "" for "" + IVLDAL.GetPerson(s.PersonID).FullName,")
            strB.AppendLine(Space(tab.XXXXXX) & ".Destination = s.ID")
            strB.AppendLine(Space(tab.XXXXX) & "})")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "End For")
            strB.AppendLine(Space(tab.XXX) & "For Each sip As SpecimenInProject in specInProj")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXXX) & "If sip.SpecimenID = specimen.ID")
            strB.AppendLine(Space(tab.XXXX) & "")
            strB.AppendLine(Space(tab.XXXXX) & "asList.ListOfErrors.Add(New AssociationError With")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & ".Model = ""Model"",")
            strB.AppendLine(Space(tab.XXXXXX) & ".MessageText = SiteVariables.CurrentAliasGroup.Model + "" "" + m.FileLocation,")
            strB.AppendLine(Space(tab.XXXXXX) & ".Destination = m.ID")
            strB.AppendLine(Space(tab.XXXXX) & "})")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "End For")
            strB.AppendLine(Space(tab.XXX) & "If asList.ListOfErrors.Count > 0 Then")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXXX) & "ListOfErrors.Add(asList)")
            strB.AppendLine(Space(tab.XXXX) & "asList.Groupingtitle = ""You cannot delete the "" + SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ".Capitalized + "" "" + obj.Name +")
            strB.AppendLine(Space(tab.XXXX) & """ until the following dependencies are removed:")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ListOfErrors = ListOfErrors")
            strB.AppendLine(Space(tab.XXX) & "If obj is Nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "return HttpNotFound()")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "Dim vm as New " & pClass.Name.Capitalized & "ViewModel(obj)")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Delete, vm." & pClass.Name.Capitalized & ".Name)")
            strB.AppendLine(Space(tab.XXX) & "Return View(vm)")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "[Permission(Role.Permission." & pClass.Name.Capitalized & "Delete)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Delete(int id = 0)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & " obj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id);")
            strB.AppendLine(Space(tab.XXX) & "List<Photo> photo = " & pClass.DALClassVariable.Name & ".GetPhotos();")
            strB.AppendLine(Space(tab.XXX) & "List<Scan> scan = " & pClass.DALClassVariable.Name & ".GetScans();")
            strB.AppendLine(Space(tab.XXX) & "List<SpecimenInProject> specInProj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "InProjects();")
            strB.AppendLine(Space(tab.XXX) & "List<ModelTable> mt = " & pClass.DALClassVariable.Name & ".GetModelTables();")
            strB.AppendLine(Space(tab.XXX) & "List<AssociationErrorsList> ListOfErrors = new List<AssociationErrorsList>();")
            strB.AppendLine(Space(tab.XXX) & "AssociationErrorsList asList = new AssociationErrorsList();")
            strB.AppendLine(Space(tab.XXX) & "asList.ListOfErrors = new List<AssociationError>();")
            strB.AppendLine(Space(tab.XXX) & "foreach (Photo p in photo)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (p.ObjID == " & pClass.Name.Capitalized & ".ID)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "asList.ListOfErrors.Add(new AssociationError()")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & "Model = ""Photo"",")
            strB.AppendLine(Space(tab.XXXXXX) & "MessageText = SiteVariables.CurrentAliasGroup.Photo + \"" \ "" + p.FileName,")
            strB.AppendLine(Space(tab.XXXXXX) & "Destination = p.ID")
            strB.AppendLine(Space(tab.XXXXX) & "});")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "foreach (Scan s in scan)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if ( .Obj == " & pClass.Name.Capitalized & ".ID)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "asList.ListOfErrors.Add(new AssociationError())")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & "Model = ""Scan"",")
            strB.AppendLine(Space(tab.XXXXXX) & "MessageText = SiteVariables.CurrentAliasGroup.Scan + "" for "" + " & pClass.DALClassVariable.Name & ".GetPerson(s.PersonID).FullName,")
            strB.AppendLine(Space(tab.XXXXXX) & "Destination = s.ID")
            strB.AppendLine(Space(tab.XXXXX) & "});")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "foreach (SpecimenInProject sip in specInProj)")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "if (sip.SpecimenID == specimen.ID)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "asList.ListOfErrors.Add(new AssociationError()")
            strB.AppendLine(Space(tab.XXXXX) & "{")
            strB.AppendLine(Space(tab.XXXXXX) & "Model = ""Model"",")
            strB.AppendLine(Space(tab.XXXXXX) & "MessageText = SiteVariables.CurrentAliasGroup.Model + "" "" + m.FileLocation,")
            strB.AppendLine(Space(tab.XXXXXX) & "Destination = m.ID")
            strB.AppendLine(Space(tab.XXXXX) & "});")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "if (asList.ListOfErrors.Count > 0)")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "ListOfErrors.Add(asList);")
            strB.AppendLine(Space(tab.XXXX) & "asList.Groupingtitle = ""You cannot delete the "" + SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ".Capitalized + "" "" + obj.Name +")
            strB.AppendLine(Space(tab.XXXX) & """ until the following dependencies are removed:"";")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ListOfErrors = ListOfErrors;")
            strB.AppendLine(Space(tab.XXX) & "if (obj == null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "return HttpNotFound();")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.Name.Capitalized & "ViewModel vm = new " & pClass.Name.Capitalized & "ViewModel(obj);")
            strB.AppendLine(Space(tab.XXX) & "ViewBag.ViewModel = vm;")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.addPageVariablesToViewBag(ViewBag, SiteVariables.CurrentAliasGroup." & pClass.Name.Capitalized & ",")
            strB.AppendLine(Space(tab.XXX) & "ControllerMethods.ControllerAction.Delete, vm." & pClass.Name.Capitalized & ".Name);")
            strB.AppendLine(Space(tab.XXX) & "return View(vm);")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Function getDeletePOST(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "//POST: /" & pClass.Name.Capitalized & "/Delete/5")
            strB.AppendLine(Space(tab.XX) & "<HttpPost, ActionName(""Delete"")>")
            strB.AppendLine(Space(tab.XX) & "<ValidateAntiForgeryToken>")
            strB.AppendLine(Space(tab.XX) & "<Permission(Role.Permission." & pClass.Name.Capitalized & "Delete)>")
            strB.AppendLine(Space(tab.XX) & "Public Function DeleteConfirmed(ByVal id As Integer) As ActionLink")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "Dim obj As " & pClass.NameSpaceVariable.NameBasedOnID & " = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id)")
            strB.AppendLine(Space(tab.XXX) & "obj.dbRemove()")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "Return RedirectToAction(""Index"")")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XX) & "")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "//POST: /" & pClass.Name.Capitalized & "/Delete/5")
            strB.AppendLine(Space(tab.XX) & "[HttpPost, ActionName(""Delete"")]")
            strB.AppendLine(Space(tab.XX) & "[ValidateAntiForgeryToken]")
            strB.AppendLine(Space(tab.XX) & "[Permission(Role.Permission." & pClass.Name.Capitalized & "Delete)]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult DeleteConfirmed(int id)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & " obj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(id);")
            strB.AppendLine(Space(tab.XXX) & "obj.dbRemove();")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "return RedirectToAction(""Index"");")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "")
        End If
        Return strB.ToString()
    End Function
    Private Function getDispose(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Protected Overrides sub Dispose(ByVal disposing As Boolean)")
            strB.AppendLine(Space(tab.XXX) & "base.Dispose(disposing)")
            strB.AppendLine(Space(tab.XX) & "End Sub")
            strB.AppendLine(Space(tab.XX) & "")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "protected override void Dispose(bool disposing)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "base.Dispose(disposing);")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "")
        End If
        Return strB.ToString()
    End Function

    Private Function getFind(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "Public Function Find(ByVal searchText As string, ByVal count As Integer) As ActionResult")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "Dim createNew As Boolean = False")
            strB.AppendLine(Space(tab.XXX) & "If Request.Form(""showcreate"") <> nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "createNew = Boolean.Parse(Request.Form(""showcreate""))")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "Dim showNone As Boolean = False")
            strB.AppendLine(Space(tab.XXX) & "If Request.Form(""shownone"") <> nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "showNone = Boolean.Parse(Request.Form[""shownone""])")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "showNone = False")
            strB.AppendLine(Space(tab.XXX) & "If Request.Form(""shownone"") <> nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "showNone = Boolean.Parse(Request.Form[""shownone""])")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "Return Json(new { ok = true, data = getAutoCompleteObjects(searchText, count, " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.PluralAndCapitalized & "(), createNew, showNone), message = ""ok"" })")
            strB.AppendLine(Space(tab.XX) & "End Function")
            strB.AppendLine(Space(tab.XXX) & "")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "[HttpPost]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult Find(string searchText, int count)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "bool createNew = false;")
            strB.AppendLine(Space(tab.XXX) & "if ( Request.Form[""showcreate""] != null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "createNew = bool.Parse(Request.Form[""showcreate""]);")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "bool showNone = false;")
            strB.AppendLine(Space(tab.XXX) & "if (Request.Form[""shownone""] != null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "showNone = bool.Parse(Request.Form[""shownone""]);")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "bool showNone = false;")
            strB.AppendLine(Space(tab.XXX) & "if (Request.Form[""shownone""] != null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "showNone = bool.Parse(Request.Form[""shownone""]);")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return Json(new { ok = true, data = getAutoCompleteObjects(searchText, count, " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.PluralAndCapitalized & "(), createNew, showNone), message = ""ok"" });")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XXX) & "")
        End If
        Return strB.ToString()
    End Function

    Private Function getGetAutoCompleteObjects(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Public Static Function getAutoCompleteObjects(searchText as string, count as Integer, list as List (Of " & pClass.NameSpaceVariable.NameBasedOnID & ")) As  List (Of AutoCompleterObject)")
            strB.AppendLine(Space(tab.XXX) & "Dim searchstr As string = searchText.ToLower()")
            strB.AppendLine(Space(tab.XXX) & "Dim lst as List (Of AutoCompleterObject) lst = New List (Of AutoCompleterObject)")
            strB.AppendLine(Space(tab.XXX) & "Dim useMe as Boolean = false")
            strB.AppendLine(Space(tab.XXX) & "For Each obj as " & pClass.NameWithNameSpace & " in list")
            strB.AppendLine(Space(tab.XXXX) & "If obj.Name.ToLower().Contains(searchstr) Then")
            strB.AppendLine(Space(tab.XXXXX) & "useMe = true")
            strB.AppendLine(Space(tab.XXXX) & "Else If obj.Number.ToLower().Contains(searchstr) Then")
            strB.AppendLine(Space(tab.XXXXX) & "useMe = true")
            strB.AppendLine(Space(tab.XXXX) & "Else")
            strB.AppendLine(Space(tab.XXXXX) & "useMe = false")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXXX) & "")
            strB.AppendLine(Space(tab.XXXX) & "If useMe Then")
            strB.AppendLine(Space(tab.XXXXX) & "Dim aObj As AutoCompleterObject = new AutoCompleterObject(")
            strB.AppendLine(Space(tab.XXXXXX) & "obj.Name, obj.Number,")
            strB.AppendLine(Space(tab.XXXXXX) & """" & pClass.Name.ToString & "_"" + obj.ID,")
            strB.AppendLine(Space(tab.XXXXXX) & "obj.ID.ToString())")
            strB.AppendLine(Space(tab.XXXXXX) & "lst.Add(aObj)")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXXX) & "If lst.Count >= count Then")
            strB.AppendLine(Space(tab.XXXXX) & "Exit For")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "End For")
            strB.AppendLine(Space(tab.XXX) & "return lst")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "public static List<AutoCompleterObject> getAutoCompleteObjects(string searchText, int count, List<" & pClass.NameSpaceVariable.NameBasedOnID & "> list)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "string searchstr = searchText.ToLower();")
            strB.AppendLine(Space(tab.XXX) & "List<AutoCompleterObject> lst = new List<AutoCompleterObject>();")
            strB.AppendLine(Space(tab.XXX) & "bool useMe = false;")
            strB.AppendLine(Space(tab.XXX) & "foreach (" & pClass.NameWithNameSpace & " obj in list)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (obj.Name.ToLower().Contains(searchstr))")
            strB.AppendLine(Space(tab.XXXXX) & "useMe = true;")
            strB.AppendLine(Space(tab.XXXX) & "else if (obj.Number.ToLower().Contains(searchstr))")
            strB.AppendLine(Space(tab.XXXXX) & "useMe = true;")
            strB.AppendLine(Space(tab.XXXX) & "else")
            strB.AppendLine(Space(tab.XXXXX) & "useMe = false;")
            strB.AppendLine(Space(tab.XXXX) & "")
            strB.AppendLine(Space(tab.XXXX) & "if (useMe)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "AutoCompleterObject aObj = new AutoCompleterObject(")
            strB.AppendLine(Space(tab.XXXXXX) & "obj.Name, obj.Number,")
            strB.AppendLine(Space(tab.XXXXXX) & """" & pClass.Name.ToString & "_"" + obj.ID,")
            strB.AppendLine(Space(tab.XXXXXX) & "obj.ID.ToString());")
            strB.AppendLine(Space(tab.XXXXXX) & "lst.Add(aObj);")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "if (lst.Count >= count)")
            strB.AppendLine(Space(tab.XXXXX) & "break;")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return lst;")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    Private Function getIsFieldValueUnique(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "Public Function IsFieldValueUnique(ByVal searchText As string, ByVal fieldName As string, ByVal objID As string) As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "Dim isunique as Boolean? = True")
            strB.AppendLine(Space(tab.XXX) & "For Each obj As " & pClass.Name.Capitalized & " in " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.PluralAndCapitalized & "()")
            strB.AppendLine(Space(tab.XXXX) & "If obj.ID.ToString() <> objID Then")
            strB.AppendLine(Space(tab.XXXX) & "isunique = obj.IsFieldValueUnique(fieldName, searchText)")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXXX) & "")
            strB.AppendLine(Space(tab.XXXX) & "If isunique = nothing Then")
            strB.AppendLine(Space(tab.XXXXX) & "isunique = False")
            strB.AppendLine(Space(tab.XXXXX) & "Exit For")
            strB.AppendLine(Space(tab.XXXX) & "Else If Not isunique Then")
            strB.AppendLine(Space(tab.XXXXX) & "Exit For")
            strB.AppendLine(Space(tab.XXXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "Return Json(new { ok = isunique, message = ""ok"" })")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "[HttpPost]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult IsFieldValueUnique(string searchText, string fieldName, string objID)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "bool? isunique = true;")
            strB.AppendLine(Space(tab.XXX) & "foreach (" & pClass.Name.Capitalized & " obj in " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.PluralAndCapitalized & "())")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "if (obj.ID.ToString() != objID)")
            strB.AppendLine(Space(tab.XXXX) & "isunique = obj.IsFieldValueUnique(fieldName, searchText);")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXXX) & "if (isunique == null)")
            strB.AppendLine(Space(tab.XXXX) & "{")
            strB.AppendLine(Space(tab.XXXXX) & "isunique = false;")
            strB.AppendLine(Space(tab.XXXXX) & "break;")
            strB.AppendLine(Space(tab.XXXX) & "}")
            strB.AppendLine(Space(tab.XXXX) & "else if (isunique == false)")
            strB.AppendLine(Space(tab.XXXXX) & "break;")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "return Json(new { ok = isunique, message = ""ok"" });")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function

    'Rick: Where did this come from?
    Public Function getGetAsListItem(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "<HttpPost>")
            strB.AppendLine(Space(tab.XX) & "Public Function GetAsListItem(ByVal objID as Integer) As ActionResult")
            strB.AppendLine(Space(tab.XXX) & "Try")
            strB.AppendLine(Space(tab.XXXX) & "Return Json(new { ok = true, data = GetItem(objID), message = ""ok"" })")
            strB.AppendLine(Space(tab.XXX) & "")
            strB.AppendLine(Space(tab.XXX) & "Catch ex As Exception")
            strB.AppendLine(Space(tab.XXXX) & "Return Json(new { ok = false, message = ex.Message });")
            strB.AppendLine(Space(tab.XXX) & "End Try")
            strB.AppendLine(Space(tab.XX) & "End Function")
        Else 'If lang = Language.CSharp Then
            strB.AppendLine(Space(tab.XX) & "[HttpPost]")
            strB.AppendLine(Space(tab.XX) & "public ActionResult GetAsListItem(int objID)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "try")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "return Json(new { ok = true, data = GetItem(objID), message = ""ok"" });")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "catch (Exception ex)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "return Json(new { ok = false, message = ex.Message });")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "")
        End If
        Return strB.ToString()
    End Function
    Private Function getGetItem(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Public Static Function GetItem(ByVal objID As Integer) As String")
            strB.AppendLine(Space(tab.XXX) & "Dim obj As " & pClass.NameSpaceVariable.NameBasedOnID & " = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(objID)")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "Dim strb As New StringBuilder")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""<li id=\""" & pClass.Name.ToString & "_"")")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(objID.ToString())")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""\"">"")")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(obj.Name)")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""<button onclick=\""removeItem(this)\"" class=\""remove\"" title=\""Remove ")
            strB.AppendLine(Space(tab.XXXX) & "+ obj.Name + "" from list\"" >Remove Me</button>"")")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""</li>"")")
            strB.AppendLine(Space(tab.XXX) & "Return strb.ToString()")
            strB.AppendLine(Space(tab.XX) & "End function")
        Else 'c-sharp
            strB.AppendLine(Space(tab.XX) & "public static String GetItem(int objID)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & " obj = " & pClass.DALClassVariable.Name & ".Get" & pClass.Name.Capitalized & "(objID);")
            strB.AppendLine(Space(tab.XX) & "")
            strB.AppendLine(Space(tab.XXX) & "StringBuilder strb = new StringBuilder();")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""<li id=\""" & pClass.Name.ToString & "_"");")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(objID.ToString());")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""\"">"");")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(obj.Name);")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""<button onclick=\""removeItem(this)\"" class=\""remove\"" title=\""Remove ")
            strB.AppendLine(Space(tab.XXXX) & "+ obj.Name + "" from list\"" >Remove Me</button>"");")
            strB.AppendLine(Space(tab.XXX) & "strb.Append(""</li>"");")
            strB.AppendLine(Space(tab.XXX) & "return strb.ToString();")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function
    Private Function getExecuteCore(pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Protected Overrides sub ExecuteCore()")
            strB.AppendLine(Space(tab.XXX) & "throw New NotImplementedException()")
            strB.AppendLine(Space(tab.XX) & "End Sub")
        Else 'c-sharp
            strB.AppendLine(Space(tab.XX) & "protected override void ExecuteCore()")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "throw new NotImplementedException();")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        Return strB.ToString()
    End Function





    'Public Function getEditForm(ByVal pClass As ProjectClass, useLists As Boolean, lang As language) As String
    '    Dim strB As New StringBuilder
    '    strB.Append(getHeaderLine(pClass, lang, pageVersion.Edit))
    '    strB.AppendLine(generateContentHeaders(pClass, True, useLists, lang))
    '    Return strB.ToString()
    'End Function
    'Private Function getPageLoadForEdit(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Protected Sub Page_Load(ByVAl sender As Object, ByVal e As System.EventArgs) Handles Me.Load")
    '        strB.AppendLine(Space(tab.XX) & "If Not IsPostBack Then")
    '        strB.AppendLine(Space(tab.XXX) & "Dim myObject As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & _
    '                        " = get" & pClass.Name.Capitalized & "FromQueryString()")
    '        strB.AppendLine(Space(tab.XXX) & "If myObject Is Nothing Then")
    '        strB.AppendLine(Space(tab.XXXX) & "SessionVariables.addError(StringToolkit.ObjectNotFound(AliasGroup." & pClass.Name.Capitalized & "))")
    '        strB.AppendLine(Space(tab.XXXX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"")")
    '        strB.AppendLine(Space(tab.XXX) & "End If")
    '        strB.AppendLine(Space(tab.XXX) & "fillForm(myObject)")
    '        strB.AppendLine(Space(tab.XX) & "End If")
    '        strB.AppendLine(Space(tab.X) & "End Sub")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "protected void Page_Load(object sender, EventArgs e)")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "if (!IsPostBack)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
    '                        & " myObject = get" & pClass.Name.Capitalized & "FromQueryString();")
    '        strB.AppendLine(Space(tab.XXX) & "if (myObject != null)")
    '        strB.AppendLine(Space(tab.XXX) & "{")
    '        strB.AppendLine(Space(tab.XXXX) & "SessionVariables.addError(StringToolkit.ObjectNotFound(AliasGroup." & pClass.Name.Capitalized & "));")
    '        strB.AppendLine(Space(tab.XXXX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"");")
    '        strB.AppendLine(Space(tab.XXX) & "}")
    '        strB.AppendLine(Space(tab.XXX) & "fillForm(myObject);")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '        strB.AppendLine(Space(tab.X) & "}")
    '    End If
    '    strB.AppendLine()
    '    Return strB.ToString()
    'End Function
    'Private Function getPageInstructions(lang As language) As String
    '    Dim strB As New StringBuilder
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Protected Overrides Sub fillPageInstructions()")
    '        strB.AppendLine(Space(tab.XX) & "lblPageInstructions.Text=""""")
    '        strB.AppendLine(Space(tab.X) & "End Sub")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "protected override void fillPageInstructions()")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "lblPageInstructions.Text="""";")
    '        strB.AppendLine(Space(tab.X) & "}")
    '    End If
    '    strB.AppendLine()
    '    Return strB.ToString()
    'End Function
    'Private Function getGetFunctionForQueryString(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Private Function get" & pClass.Name.Capitalized & "FromQueryString() As " & _
    '                        pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized)
    '        strB.AppendLine(Space(tab.XX) & "Return " & pClass.NameSpaceVariable.NameBasedOnID & "." & _
    '                        pClass.DALClassVariable.Name & ".get" & pClass.Name.Capitalized & "(Request.QueryString(""id""), True)")
    '        strB.AppendLine(Space(tab.X) & "End Function")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "private " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
    '                        & " get" & pClass.Name.Capitalized & "FromQueryString()")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "return " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.DALClassVariable.Name _
    '                        & ".get" & pClass.Name.Capitalized & "(Request.QueryString(""id""), True);")
    '        strB.AppendLine(Space(tab.X) & "}")
    '    End If
    '    strB.AppendLine()

    '    Return strB.ToString()
    'End Function
    'Private Function getFillFormForEdit(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    Dim lineEnd As Char = " "c
    '    Dim conCat As Char = "&"c
    '    If lang = language.CSharp Then
    '        lineEnd = ";"c
    '        conCat = "+"c
    '    End If
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Private Sub fillForm(ByVal my" & pClass.Name.Capitalized & " As " _
    '                        & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & ")")
    '        strB.AppendLine(Space(tab.XX) & "If my" & pClass.Name.Capitalized & ".ID = -1 Then ")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "private void fillForm(" & pClass.NameSpaceVariable.NameBasedOnID _
    '                        & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized & ")")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "if (my" & pClass.Name.Capitalized & ".ID = -1)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '    End If
    '    strB.AppendLine(Space(tab.XXX) & "btnSaveChanges.Text = AliasGroup.Add.Capitalized" & lineEnd)
    '    strB.AppendLine(Space(tab.XXX) & "litTitle.Text = AliasGroup.Add.Capitalized " & conCat _
    '                    & " "" ""  & AliasGroup." & pClass.Name.Capitalized & ".Capitalized" & lineEnd)
    '    strB.AppendLine(Space(tab.XXX) & "lblSubTitle.Text=litTitle.Text" & lineEnd)

    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.XX) & "Else")
    '    Else
    '        strB.AppendLine(Space(tab.XX) & "}")
    '        strB.AppendLine(Space(tab.XX) & "else")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '    End If

    '    strB.AppendLine(Space(tab.XXX) & "litTitle.Text = AliasGroup.Edit.Capitalized " & conCat _
    '                    & " "" "" " & conCat & " AliasGroup." & pClass.Name.Capitalized & ".Capitalized" & lineEnd)
    '    strB.AppendLine(Space(tab.XXX) & "lblSubTitle.Text=litTitle.Text" & lineEnd)
    '    For Each classVar As ClassVariable In pClass.ClassVariables
    '        If Not classVar.DisplayOnEditPage Then Continue For
    '        If classVar.IsTextBox Then
    '            strB.Append(Space(tab.XXX) & "txt" & classVar.Name & ".Text = my" & pClass.Name.Capitalized & "." & classVar.Name)
    '            If classVar.IsDouble OrElse classVar.IsInteger Then
    '                strB.Append(".ToString()")
    '            ElseIf classVar.ParameterType.IsNameAlias Then
    '                strB.Append(".TextUnFormatted")
    '            End If
    '            strB.AppendLine(lineEnd)
    '        ElseIf classVar.IsCheckBox Then
    '            strB.AppendLine(Space(tab.XXX) & classVar.DefaultHTMLName & ".Checked = my" & _
    '                            pClass.Name.Capitalized & "." & classVar.Name & lineEnd)
    '        ElseIf classVar.IsDate Then
    '            strB.AppendLine(Space(tab.XXX) & classVar.GetMonthTextControlName & ".Text = my" & _
    '                            pClass.Name.Capitalized & "." & classVar.Name & ".Month.ToString()" & lineEnd)
    '            strB.AppendLine(Space(tab.XXX) & classVar.getDayTextControlName & ".Text = my" & _
    '                            pClass.Name.Capitalized & "." & classVar.Name & ".Day.ToString()" & lineEnd)
    '            strB.AppendLine(Space(tab.XXX) & classVar.getYearTextControlName & ".Text = my" & _
    '                            pClass.Name.Capitalized & "." & classVar.Name & ".Year.ToString()" & lineEnd)

    '        ElseIf classVar.IsDropDownList Then
    '            Dim tempAlias As New NameAlias(classVar.ParameterType.Name.ToLower())
    '            If lang = language.VisualBasic Then
    '                strB.AppendLine(Space(tab.XXX) & "For Each tempObject As " & pClass.NameSpaceVariable.NameBasedOnID & _
    '                                "." & classVar.ParameterType.Name & " In " & pClass.NameSpaceVariable.NameBasedOnID & _
    '                                "." & pClass.DALClassVariable.Name & ".Get" & tempAlias.PluralAndCapitalized & "()")
    '            Else
    '                strB.AppendLine(Space(tab.XXX) & "foreach (" & pClass.NameSpaceVariable.NameBasedOnID & _
    '                                "." & classVar.ParameterType.Name & " tempObject in " & pClass.NameSpaceVariable.NameBasedOnID & _
    '                                "." & pClass.DALClassVariable.Name & ".Get" & tempAlias.PluralAndCapitalized & "())")
    '                strB.AppendLine(Space(tab.XXX) & "{")
    '            End If
    '            strB.Append(Space(tab.XXXX) & classVar.DefaultHTMLName & ".Items.Add(new ListItem(tempObject.")
    '            If classVar.ParentClass IsNot Nothing Then
    '                Dim valName As String = ""
    '                Dim txtName As String = ""
    '                If classVar.ParentClass.ValueVariable IsNot Nothing Then valName = classVar.ParentClass.ValueVariable.Name & "."
    '                If classVar.ParentClass.TextVariable IsNot Nothing Then valName = classVar.ParentClass.TextVariable.Name & "."
    '                strB.AppendLine(txtName & "ToString(), tempObject." & valName & "ToString()))" & lineEnd)
    '            Else
    '                If classVar.ParameterType.AssociatedProjectClass IsNot Nothing Then
    '                    strB.AppendLine(classVar.ParameterType.AssociatedProjectClass.TextVariable.Name & ".ToString(), tempObject." & classVar.ParentClass.ValueVariable.Name & ".ToString()))" & lineEnd)
    '                Else
    '                    strB.AppendLine("Name.ToString(), tempObject.test.ToString()))" & lineEnd)
    '                End If
    '            End If
    '            If lang = language.VisualBasic Then
    '                strB.AppendLine(Space(tab.XXX) & "Next")
    '            Else
    '                strB.AppendLine(Space(tab.XXX) & "}")
    '            End If
    '            strB.AppendLine(Space(tab.XXX) & classVar.DefaultHTMLName & ".SelectedValue = my" & _
    '                            pClass.Name.Capitalized & "." & classVar.Name & "ID.ToString()" & lineEnd)
    '        End If
    '    Next
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.XX) & "End If ")
    '        strB.AppendLine(Space(tab.X) & "End Sub")
    '    Else
    '        strB.AppendLine(Space(tab.XXX) & "}")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '    End If
    '    strB.AppendLine()

    '    Return strB.ToString()
    'End Function
    'Private Function getCancelForEdit(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click")
    '        strB.AppendLine(Space(tab.XX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"")")
    '        strB.AppendLine(Space(tab.X) & "End Sub")
    '    Else
    '        strB.AppendLine(Space(tab.XX) & "protected void btnCancel_Click(object sender, System.EventArgs e)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"");")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '    End If
    '    strB.AppendLine()
    '    Return strB.ToString()
    'End Function
    'Private Function getValidateFunction(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    Dim lineEnd As Char
    '    If lang = language.VisualBasic Then
    '        lineEnd = " "c
    '        strB.AppendLine(Space(tab.X) & "Private Function validateForm() As Boolean")
    '        strB.AppendLine(Space(tab.XX) & "'TODO: Confirm validation")
    '        strB.AppendLine(Space(tab.XX) & "Dim retVal As Boolean = True")
    '    Else
    '        lineEnd = ";"c
    '        strB.AppendLine(Space(tab.X) & "private bool validateForm()")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "//TODO: Confirm validation")
    '        strB.AppendLine(Space(tab.XX) & "bool retVal = true;")
    '    End If
    '    Dim doubleExists As Boolean = False
    '    Dim integerExists As Boolean = False
    '    Dim dateExists As Boolean = False
    '    For Each classVar As ClassVariable In pClass.ClassVariables
    '        If Not classVar.DisplayOnEditPage Then Continue For
    '        If classVar.IsDouble Then
    '            If Not doubleExists Then
    '                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDouble As Double", "double tempdouble;").ToString())
    '                doubleExists = True
    '            End If
    '            If lang = language.VisualBasic Then
    '                strB.AppendLine(Space(tab.XX) & "If Not Double.TryParse(" & classVar.DefaultHTMLName & ".Text, tempDouble) Then ")
    '            Else
    '                strB.AppendLine(Space(tab.XX) & "if (!double.TryParse(" & classVar.DefaultHTMLName & ".Text, tempDouble))")
    '                strB.AppendLine(Space(tab.XX) & "{")
    '            End If
    '            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(""" & classVar.Name & " is in an invalid format."")" & lineEnd)
    '            strB.AppendLine(Space(tab.XXX) & "retVal = false" & lineEnd)

    '            strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
    '        ElseIf classVar.IsInteger Then
    '            If Not integerExists Then
    '                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempInteger As Integer", "int tempInteger;").ToString())
    '                integerExists = True
    '            End If
    '            If lang = language.VisualBasic Then
    '                strB.AppendLine(Space(tab.XX) & "If Not Integer.TryParse(" & classVar.DefaultHTMLName & ".Text, tempInteger) Then ")
    '            Else
    '                strB.AppendLine(Space(tab.XX) & "if (!int.TryParse(" & classVar.DefaultHTMLName & ".Text, tempInteger))")
    '                strB.AppendLine(Space(tab.XX) & "{")
    '            End If
    '            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(""" & classVar.Name & " is in an invalid format."")" & lineEnd)
    '            strB.AppendLine(Space(tab.XXX) & "retVal = False" & lineEnd)

    '            strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())

    '        ElseIf classVar.IsDate Then
    '            If Not dateExists Then
    '                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDateTime As DateTime", "DateTime tempDateTime;").ToString())
    '            End If
    '            Dim dateString As String = classVar.GetMonthTextControlName() & ".Text.Trim() & ""/"" & " & _
    '                                        classVar.getDayTextControlName() & ".Text.Trim() & ""/"" & " & _
    '                                        classVar.getYearTextControlName() & ".Text.Trim()"
    '            If lang = language.VisualBasic Then
    '                strB.AppendLine(Space(tab.XX) & "If Not DateTime.TryParse(" & dateString & ", tempDateTime) Then ")
    '            Else
    '                strB.AppendLine(Space(tab.XX) & "if (!DateTime.TryParse(" & dateString & ", tempDateTime))")
    '                strB.AppendLine(Space(tab.XX) & "{")
    '            End If
    '            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(""" & classVar.Name & " is in an invalid format."")" & lineEnd)
    '            strB.AppendLine(Space(tab.XXX) & "retVal = false" & lineEnd)

    '            strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
    '            dateExists = True
    '        End If
    '    Next

    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Return", "return").ToString() & " retVal" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End Function", "}").ToString())

    '    strB.AppendLine()
    '    Return strB.ToString()
    'End Function
    'Private Function getSaveChanges(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    Dim lineEnd As Char
    '    If lang = language.VisualBasic Then
    '        lineEnd = " "c
    '        strB.AppendLine(Space(tab.X) & "Protected Sub btnSaveChanges_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click")
    '        strB.AppendLine(Space(tab.XX) & "If Not validateForm() Then Exit Sub")
    '        strB.AppendLine(Space(tab.XX) & "Dim my" & pClass.Name.Capitalized & " As " & pClass.NameSpaceVariable.Name & "." _
    '                        & pClass.Name.Capitalized & " = get" & pClass.Name.Capitalized & "FromQueryString()")
    '    Else
    '        lineEnd = ";"c
    '        strB.AppendLine(Space(tab.X) & "protected void btnSaveChanges_Click(object sender, System.EventArgs e)")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "if (!validateForm()) return;")
    '        strB.AppendLine(Space(tab.XX) & pClass.NameSpaceVariable.Name & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized _
    '                        & " = get" & pClass.Name.Capitalized & "FromQueryString();")
    '    End If
    '    Dim doubleExists As Boolean = False
    '    Dim integerExists As Boolean = False
    '    Dim dateExists As Boolean = False
    '    For Each classVar As ClassVariable In pClass.ClassVariables
    '        If Not classVar.DisplayOnEditPage OrElse Not classVar.IsDatabaseBound Then Continue For
    '        If classVar.IsCheckBox Then
    '            strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = " & classVar.DefaultHTMLName & ".Checked" & lineEnd)
    '        ElseIf classVar.IsTextBox Then
    '            If classVar.IsDouble Then
    '                If Not doubleExists Then
    '                    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDouble As Double", "double tempDouble;").ToString())
    '                    doubleExists = True
    '                End If
    '                strB.AppendLine(Space(tab.XX) & "Double.TryParse(" & classVar.DefaultHTMLName & ".Text, tempDouble)" & lineEnd)
    '                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = tempDouble" & lineEnd)
    '            ElseIf classVar.IsInteger Then
    '                If Not integerExists Then
    '                    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempInteger As Integer", "int tempInteger;").ToString())
    '                    integerExists = True
    '                End If
    '                strB.Append(Space(tab.XX) & IIf(lang = language.VisualBasic, "Integer", "int").ToString())
    '                strB.AppendLine(".TryParse(" & classVar.DefaultHTMLName & ".Text, tempInteger)" & lineEnd)
    '                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = tempInteger" & lineEnd)

    '            ElseIf classVar.ParameterType.IsNameAlias Then
    '                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & ".TextUnFormatted = txt" & classVar.Name & ".Text" & lineEnd)
    '            Else
    '                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = txt" & classVar.Name & ".Text" & lineEnd)
    '            End If

    '        ElseIf classVar.IsDropDownList Then
    '            strB.Append(Space(tab.XX) & "my" & pClass.Name.Capitalized)
    '            If classVar.ParameterType.AssociatedProjectClass IsNot Nothing Then
    '                strB.Append("." & classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized)
    '            End If
    '            strB.AppendLine(" = " & IIf(lang = language.VisualBasic, cg.getConvertFunction("Integer", lang), "int.Parse").ToString() _
    '                            & "(" & classVar.DefaultHTMLName & ".SelectedValue)" & lineEnd)
    '        ElseIf classVar.IsDate Then
    '            If Not dateExists Then
    '                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDateTime As DateTime", "DateTime tempDateTime;").ToString())
    '            End If
    '            strB.AppendLine(Space(tab.XX) & "DateTime.TryParse(" & classVar.GetMonthTextControlName() & ".Text.Trim() & ""/"" & " & _
    '                                                                classVar.getDayTextControlName() & ".Text.Trim() & ""/"" & " & _
    '                                                                classVar.getYearTextControlName() & ".Text.Trim(), tempDateTime)" & lineEnd)
    '            strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = tempDateTime" & lineEnd)
    '            dateExists = True
    '            '                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = new DateTime(txtYear.Text, txtMonth.Text, txtDay.Text)" )
    '        End If
    '    Next
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, _
    '                                         "If my" & pClass.Name.Capitalized & ".ID = -1 Then", _
    '                                         "if (my" & pClass.Name.Capitalized & ".ID == -1)").ToString())
    '    If lang = language.CSharp Then strB.AppendLine(Space(tab.XX) & "{")
    '    strB.AppendLine(Space(tab.XXX) & "add" & pClass.Name.Capitalized & "(my" & pClass.Name.Capitalized & ")" & lineEnd)
    '    If lang = language.CSharp Then strB.Append("}")
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Else", "else").ToString())
    '    If lang = language.CSharp Then strB.Append("{")
    '    strB.AppendLine(Space(tab.XXX) & "update" & pClass.Name.Capitalized & "(my" & pClass.Name.Capitalized & ")" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
    '    strB.AppendLine(Space(tab.XX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"")" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End Sub", "}").ToString())

    '    strB.AppendLine()

    '    Return strB.ToString()

    'End Function
    'Private Function getAddObject(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Private Function add" & pClass.Name.Capitalized & "(ByVal my" & pClass.Name.Capitalized & " As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & ") As Boolean")
    '        strB.AppendLine(Space(tab.XX) & "If my" & pClass.Name.Capitalized & ".dbAdd() > 0 Then")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add))")
    '        strB.AppendLine(Space(tab.XX) & "Else")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add))")
    '        strB.AppendLine(Space(tab.XX) & "End If")
    '        strB.AppendLine(Space(tab.X) & "End Function")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "private bool add" & pClass.Name.Capitalized & "(" & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized & ")")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "if (my" & pClass.Name.Capitalized & ".dbAdd() > 0)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add));")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '        strB.AppendLine(Space(tab.XX) & "else")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add));")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '        strB.AppendLine(Space(tab.X) & "}")
    '    End If
    '    Return strB.ToString()
    'End Function
    'Private Function getUpdateObject(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Private Function update" & pClass.Name.Capitalized & "(ByVal my" & pClass.Name.Capitalized & _
    '                        " As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & ") As Boolean")
    '        strB.AppendLine(Space(tab.XX) & "If my" & pClass.Name.Capitalized & ".dbUpdate() > 0 Then")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit))")
    '        strB.AppendLine(Space(tab.XX) & "Else")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit))")
    '        strB.AppendLine(Space(tab.XX) & "End If")
    '        strB.AppendLine(Space(tab.X) & "End Function")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "private bool update" & pClass.Name.Capitalized & "(" & _
    '                        pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized & ")")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "if (my" & pClass.Name.Capitalized & ".dbUpdate() > 0)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit));")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '        strB.AppendLine(Space(tab.XX) & "else")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit));")
    '        strB.AppendLine(Space(tab.XX) & "}")
    '        strB.AppendLine(Space(tab.X) & "}")
    '    End If
    '    Return strB.ToString()

    'End Function

    'Public Function getEditCodeBehind(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    strB.Append(cg.getPageImports(lang))
    '    strB.Append(cg.getClassDeclaration(lang, "_Edit" & pClass.Name.Capitalized, tab.None, "BasePage"))
    '    strB.AppendLine()
    '    strB.AppendLine(getPageLoadForEdit(pClass, lang))
    '    strB.AppendLine(getPageInstructions(lang))
    '    strB.AppendLine(getGetFunctionForQueryString(pClass, lang))
    '    strB.AppendLine(getFillFormForEdit(pClass, lang))
    '    strB.AppendLine(getCancelForEdit(pClass, lang))
    '    strB.AppendLine(getValidateFunction(pClass, lang))
    '    strB.AppendLine(getSaveChanges(pClass, lang))
    '    strB.AppendLine(getAddObject(pClass, lang))
    '    strB.AppendLine(getUpdateObject(pClass, lang))
    '    strB.AppendLine(IIf(lang = language.VisualBasic, "End Class", "}").ToString())
    '    Return strB.ToString()
    'End Function
    'Public Function getViewCodeBehind(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim strB As New StringBuilder
    '    Dim lineEnd As Char
    '    Dim conCat As Char
    '    strB.Append(cg.getPageImports(lang))
    '    strB.Append(cg.getClassDeclaration(lang, "_" & pClass.Name.PluralAndCapitalized, tab.None, "BasePage"))
    '    'Page Load
    '    If lang = language.VisualBasic Then
    '        lineEnd = " "c
    '        conCat = "&"c
    '        strB.AppendLine(Space(tab.X) & "Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load")
    '    Else
    '        lineEnd = ";"c
    '        conCat = "+"c
    '        strB.AppendLine(Space(tab.X) & "protected void Page_Load(object sender, System.EventArgs e)")
    '        strB.AppendLine(Space(tab.X) & "{")
    '    End If
    '    strB.AppendLine(Space(tab.XX) & "fill" & pClass.Name.PluralAndCapitalized & "Table()" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & "fillFieldsFromSiteConfig()" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & "fillPageInstructions()" & lineEnd)

    '    strB.AppendLine(Space(tab.X) & IIf(lang = language.VisualBasic, "End Sub", "}").ToString())

    '    strB.AppendLine(Space(tab.X) & getPageInstructions(lang))
    '    'fillTable
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Private Sub fill" & pClass.Name.PluralAndCapitalized & "Table()")
    '        strB.AppendLine(Space(tab.XX) & "Dim alternateRow As Boolean = False")
    '        strB.AppendLine(Space(tab.XX) & "Dim listOfObjects As List(Of " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
    '                        & ") = " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.DALClassVariable.Name & _
    '                        ".Get" & pClass.Name.PluralAndCapitalized & "()")
    '        strB.AppendLine(Space(tab.XX) & "For Each myObject As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " In listOfObjects")
    '        strB.AppendLine(Space(tab.XXX) & "Dim tRow As New TableRow")
    '        strB.AppendLine(Space(tab.XXX) & "If alternateRow Then tRow.CssClass=""other""")
    '        strB.AppendLine(Space(tab.XXX) & "alternateRow = Not alternateRow")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "private void fill" & pClass.Name.PluralAndCapitalized & "Table()")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.XX) & "bool alternateRow = false;")
    '        strB.AppendLine(Space(tab.XX) & "List<" & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
    '                        & "> listOfObjects = " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.DALClassVariable.Name _
    '                        & ".Get" & pClass.Name.PluralAndCapitalized & "();")
    '        strB.AppendLine(Space(tab.XX) & "foreach (" & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " myObject in listOfObjects)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "TableRow tRow = new TableRow();")
    '        strB.AppendLine(Space(tab.XXX) & "if (alternateRow) tRow.CssClass=""other"";")
    '        strB.AppendLine(Space(tab.XXX) & "alternateRow = !alternateRow;")
    '    End If

    '    Dim countOfColumns As Integer = 0
    '    For Each classVar As ClassVariable In pClass.ClassVariables
    '        If Not classVar.DisplayOnViewPage Then Continue For
    '        countOfColumns += 1
    '        strB.Append(Space(tab.XXX) & "tRow.Controls.Add(TableToolkit.getTableCell(myObject.")
    '        If classVar.ParameterType.IsNameAlias Then
    '            strB.Append(classVar.Name & ".ToString()")
    '        ElseIf classVar.ParameterType.IsPrimitive Then
    '            strB.Append(classVar.Name)
    '        Else
    '            strB.Append(ClassGenerator.getSystemUniqueName(classVar.Name))
    '        End If
    '        If classVar.IsDouble OrElse classVar.IsInteger OrElse classVar.IsDropDownList OrElse classVar.IsDate OrElse classVar.IsCheckBox Then
    '            strB.Append(".ToString()")
    '        End If
    '        strB.AppendLine("))" & lineEnd)
    '    Next
    '    strB.AppendLine(Space(tab.XXX) & "tRow.Controls.Add(TableToolkit.getHyperlinkCell(AliasGroup.Edit.Capitalized, ""Edit" _
    '                    & pClass.Name.Capitalized & ".aspx?id="" " & conCat & " myObject.ID.ToString()))" & lineEnd)
    '    strB.AppendLine(Space(tab.XXX) & "tbl" & pClass.Name.PluralAndCapitalized & ".Rows.Add(tRow)" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Next", "}").ToString())
    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.XX) & "If listOfObjects.Count = 0 Then")
    '        strB.AppendLine(Space(tab.XXX) & "Dim tRow As New TableRow")
    '    Else
    '        strB.AppendLine(Space(tab.XX) & "if (listOfObjects.Count == 0)")
    '        strB.AppendLine(Space(tab.XX) & "{")
    '        strB.AppendLine(Space(tab.XXX) & "TableRow tRow = new TableRow();")
    '    End If
    '    strB.AppendLine(Space(tab.XXX) & "tRow.Controls.Add(TableToolkit.getNoResultsFoundCell(AliasGroup." & pClass.Name.Capitalized _
    '                    & ", " & countOfColumns & "))" & lineEnd)
    '    strB.AppendLine(Space(tab.XXX) & "tbl" & pClass.Name.PluralAndCapitalized & ".Rows.Add(tRow)" & lineEnd)
    '    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
    '    strB.AppendLine(Space(tab.X) & IIf(lang = language.VisualBasic, "End Sub", "}").ToString())

    '    If lang = language.VisualBasic Then
    '        strB.AppendLine(Space(tab.X) & "Private Sub fillFieldsFromSiteConfig()")
    '        strB.AppendLine(Space(tab.X) & "'TODO: Fill Site Variables on " & pClass.Name.PluralAndCapitalized & ".aspx")
    '        strB.AppendLine(Space(tab.X) & "hypAdd" & pClass.Name.Capitalized & ".Text = ""Add "" & AliasGroup." & pClass.Name.Capitalized & ".Capitalized")
    '        strB.AppendLine(Space(tab.X) & "End Sub")
    '    Else
    '        strB.AppendLine(Space(tab.X) & "private void fillFieldsFromSiteConfig()")
    '        strB.AppendLine(Space(tab.X) & "{")
    '        strB.AppendLine(Space(tab.X) & "//TODO: Fill Site Variables on " & pClass.Name.PluralAndCapitalized & ".aspx")
    '        strB.AppendLine(Space(tab.X) & "hypAdd" & pClass.Name.Capitalized & ".Text = ""Add "" & AliasGroup." & pClass.Name.Capitalized & ".Capitalized;")
    '        strB.AppendLine(Space(tab.X) & "}")
    '    End If

    '    strB.AppendLine(Space(tab.X) & IIf(lang = language.VisualBasic, "End Class", "}").ToString())

    '    Return strB.ToString()
    'End Function
    'Public Function getViewForm(ByVal pClass As ProjectClass, useLists As Boolean, lang As language) As String
    '    Dim strB As New StringBuilder
    '    strB.AppendLine(getHeaderLine(pClass, lang, pageVersion.View))
    '    strB.AppendLine(generateContentHeaders(pClass, False, useLists, lang))
    '    Return strB.ToString()
    'End Function
    'Public Function getViewBody(ByVal pClass As ProjectClass) As String
    '    Dim strB As New StringBuilder
    '    strB.AppendLine(Space(tab.X) & "<asp:Hyperlink ID=""hypAdd" & pClass.Name.Capitalized & """ runat=""server"" NavigateUrl=""Edit" _
    '                    & pClass.Name.Capitalized & ".aspx?id=-1""></asp:Hyperlink>")
    '    strB.AppendLine(Space(tab.X) & "<asp:Table ID=""tbl" & pClass.Name.PluralAndCapitalized & """ runat=""server"" CssClass=""list"">")
    '    strB.AppendLine(Space(tab.XX) & "<asp:TableHeaderRow>")
    '    For Each classVar As ClassVariable In pClass.ClassVariables
    '        If Not classVar.DisplayOnViewPage Then Continue For
    '        strB.AppendLine(Space(tab.XXX) & "<asp:TableHeaderCell>" & classVar.Name & "</asp:TableHeaderCell>")
    '    Next
    '    strB.AppendLine(Space(tab.XX) & "</asp:TableHeaderRow>")
    '    strB.AppendLine(Space(tab.X) & "</asp:Table>")
    '    Return strB.ToString()
    'End Function
    'Private Enum pageVersion
    '    Edit
    '    View
    'End Enum
    'Private Function getHeaderLine(ByVal pClass As ProjectClass, lang As language, pVersion As pageVersion) As String
    '    Dim codeExt As String = "vb"
    '    Dim codeVer As String = "VB"
    '    If lang = language.CSharp Then
    '        codeExt = "cs" : codeVer = "C#"
    '    End If
    '    Dim pageName As String = IIf(pVersion = pageVersion.Edit, _
    '                                "Edit" & pClass.Name.Capitalized, _
    '                                pClass.Name.PluralAndCapitalized).ToString()

    '    Return String.Format("<%@ Page Title="""" Language=""{3}"" MasterPageFile=""~/{0}"" AutoEventWireup=""false"" CodeFile=""~/{1}.aspx.{2}" _
    '                            & """ Inherits=""_{1}"" %>", pClass.MasterPage.FileName, pageName, codeExt, codeVer)

    'End Function
    ''Private Function getViewHeader(ByVal pClass As ProjectClass, lang As language) As String
    ''    Dim codeExt As String = "vb"
    ''    If lang = language.CSharp Then codeExt = "cs"
    ''    Dim headerValue As String = "<%@ Page Title="""" Language=""VB"" MasterPageFile=""~/" & _
    ''        pClass.MasterPage.FileName & """ AutoEventWireup=""false"" CodeFile=""~/" & pClass.Name.PluralAndCapitalized & ".aspx." & codeExt _
    ''        & """ Inherits=""" & pClass.Name.PluralAndCapitalized & """ %>" & vbCrLf
    ''    Return headerValue
    ''End Function
    'Private Function generateEditBody(ByVal pClass As ProjectClass, useLists As Boolean, lang As language) As String
    '    Dim formTag As String = IIf(useLists, "ul", "div").ToString()
    '    Dim rowTag As String = IIf(useLists, "li", "div").ToString()
    '    Dim rowBtnOpenTag As String = IIf(useLists, "", "<div>").ToString()
    '    Dim rowBtnCloseTag As String = IIf(useLists, "", "</div>").ToString()
    '    Dim retStrB As New StringBuilder()
    '    retStrB.AppendLine("<" & formTag & " class=""form"">")
    '    For Each classVar As ClassVariable In pClass.ClassVariables
    '        If Not classVar.DisplayOnEditPage Then Continue For
    '        retStrB.AppendLine(Space(tab.XX) & "<" & rowTag & ">")
    '        retStrB.Append(Space(tab.XXX) & "<asp:Label ID=""lbl" & _
    '                           IIf(classVar.Name.ToLower().CompareTo("subtitle") = 0, pClass.Name, "").ToString() _
    '                           & classVar.Name & """ runat=""server"" AssociatedControlID=""")
    '        If classVar.IsInteger OrElse classVar.IsDouble Then
    '            'Small textBox: class= number
    '            retStrB.AppendLine(classVar.DefaultHTMLName & """>" & classVar.Name & "</asp:Label>")
    '            retStrB.AppendLine(Space(tab.XXX) & "<asp:TextBox ID=""" & classVar.DefaultHTMLName & """ runat=""server"" CssClass=""number""></asp:TextBox>")
    '        ElseIf classVar.IsCheckBox Then
    '            'CheckBox
    '            retStrB.AppendLine(classVar.DefaultHTMLName & """>" & classVar.Name & "</asp:Label>")
    '            retStrB.AppendLine(Space(tab.XXX) & "<asp:CheckBox ID=""" & classVar.DefaultHTMLName & """ runat=""server""></asp:CheckBox>")
    '        ElseIf classVar.IsDate Then
    '            'three textboxes (day,month,year)
    '            retStrB.AppendLine(classVar.GetMonthTextControlName() & """>" & classVar.Name & "</asp:Label>")
    '            retStrB.AppendLine(Space(tab.XXX) & "<asp:Panel ID=""pnl" & classVar.Name & "Date"" runat=""server"">")
    '            retStrB.AppendLine(Space(tab.XXXX) & "<asp:TextBox ID=""" & classVar.GetMonthTextControlName() _
    '                               & """ runat=""server"" CssClass=""number""></asp:TextBox>")
    '            retStrB.AppendLine(Space(tab.XXXX) & "<asp:TextBox ID=""" & classVar.getDayTextControlName() _
    '                               & """ runat=""server"" CssClass=""number""></asp:TextBox>")
    '            retStrB.AppendLine(Space(tab.XXXX) & "<asp:TextBox ID=""" & classVar.getYearTextControlName() _
    '                               & """ runat=""server"" CssClass=""number""></asp:TextBox>")
    '            retStrB.AppendLine(Space(tab.XXX) & "</asp:Panel>")
    '        Else
    '            If classVar.IsTextBox Then
    '                If classVar.ParameterType.Name.ToLower = "namealias" Then
    '                    retStrB.AppendLine("txt" & classVar.Name & """>" & classVar.Name & "</asp:Label>")
    '                    retStrB.AppendLine(Space(tab.XXX) & "<asp:TextBox ID=""txt" & classVar.Name & """ runat=""server""></asp:TextBox>")
    '                Else
    '                    'textbox
    '                    If classVar.DefaultHTMLName.CompareTo("lblsubtitle") = 0 Then
    '                        retStrB.AppendLine(ClassGenerator.getSystemUniqueName(classVar.DefaultHTMLName) & """>" & classVar.Name & "</asp:Label>")
    '                    Else
    '                        retStrB.AppendLine(classVar.DefaultHTMLName & """>" & classVar.Name & "</asp:Label>")
    '                    End If
    '                    retStrB.AppendLine(Space(tab.XXX) & "<asp:TextBox ID=""" & classVar.DefaultHTMLName & """ runat=""server""></asp:TextBox>")
    '                End If
    '            Else
    '                'dropdownlist
    '                Dim myAlias As New NameAlias(classVar.Name)
    '                retStrB.AppendLine(classVar.DefaultHTMLName & """>" & myAlias.PluralAndCapitalized & "</asp:Label>")
    '                retStrB.AppendLine(Space(tab.XXX) & "<asp:DropDownList ID=""" & classVar.DefaultHTMLName & """ runat=""server""></asp:DropDownList>")
    '            End If
    '        End If
    '        retStrB.AppendLine(Space(tab.XX) & "</" & rowTag & ">")
    '    Next
    '    retStrB.AppendLine(Space(tab.XX) & "<" & rowTag & " class=""buttons"">")
    '    retStrB.AppendLine(Space(tab.XXX) & rowBtnOpenTag & "<asp:Button ID=""btnSaveChanges"" runat=""server"" Text=""Save Changes"" " _
    '                       & IIf(lang = language.CSharp, "OnClick=""btnSaveChanges_Click"" ", "").ToString() & "/>" & rowBtnCloseTag)
    '    retStrB.AppendLine(Space(tab.XXX) & rowBtnOpenTag & "<asp:Button ID=""btnCancel"" runat=""server"" Text=""Cancel"" " _
    '                       & IIf(lang = language.CSharp, "OnClick=""btnCancel_Click"" ", "").ToString() & "/>" & rowBtnCloseTag)
    '    retStrB.AppendLine(Space(tab.XX) & "</" & rowTag & ">")
    '    retStrB.AppendLine(Space(tab.X) & "</" & formTag & ">")
    '    Return retStrB.ToString()
    'End Function

    'Private Function generateContentHeaders(ByVal pClass As ProjectClass, ByVal isEditForm As Boolean, useLists As Boolean, lang As language) As String
    '    Dim retStr As New System.Text.StringBuilder
    '    retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.TitleName & """ ContentPlaceHolderID=""cph" _
    '                      & pClass.MasterPage.TitleName & """ Runat=""Server"">")
    '    retStr.AppendLine(Space(tab.X) & "<asp:Literal ID=""lit" & pClass.MasterPage.TitleName & """ runat=""server""></asp:Literal>")
    '    retStr.AppendLine("</asp:Content>")
    '    retStr.AppendLine()
    '    retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.SubTitleName & """ ContentPlaceHolderID=""cph" _
    '                      & pClass.MasterPage.SubTitleName & """ Runat=""Server"">")
    '    retStr.AppendLine(Space(tab.X) & "<asp:Label ID=""lbl" & pClass.MasterPage.SubTitleName & """ runat=""server""></asp:Label>")
    '    retStr.AppendLine("</asp:Content>")
    '    retStr.AppendLine()
    '    retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.PageInstructionsName & """ ContentPlaceHolderID=""cph" _
    '                  & pClass.MasterPage.PageInstructionsName & """ Runat=""Server"">")
    '    retStr.AppendLine(Space(tab.X) & "<asp:Label ID=""lbl" & pClass.MasterPage.PageInstructionsName & """ runat=""server""></asp:Label>")
    '    retStr.AppendLine("</asp:Content>")
    '    retStr.AppendLine()

    '    retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.BodyName & """ ContentPlaceHolderID=""cph" _
    '                      & pClass.MasterPage.BodyName & """ Runat=""Server"">")
    '    If isEditForm Then
    '        retStr.AppendLine(generateEditBody(pClass, useLists, lang))
    '    Else
    '        retStr.AppendLine(getViewBody(pClass))
    '    End If
    '    retStr.AppendLine("</asp:Content>")
    '    Return retStr.ToString
    'End Function



End Class
