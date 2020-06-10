Option Strict On

Imports EnvDTE
Imports System.Collections.Generic
Imports IRICommonObjects.Words
Imports System.Text
Imports cg = CodeGeneratorAddIn.CodeGeneration
Imports language = CodeGeneratorAddIn.CodeGeneration.Language
Imports tab = CodeGeneratorAddIn.CodeGeneration.Tabs

Public Class WebFormGenerator
    Public Sub New()

    End Sub
    Public Function getEditForm(ByVal pClass As ProjectClass, useLists As Boolean, lang As language) As String
        Dim strB As New StringBuilder
        strB.Append(getHeaderLine(pClass, lang, pageVersion.Edit))
        strB.AppendLine(generateContentHeaders(pClass, True, useLists, lang))
        Return strB.ToString()
    End Function
    Private Function getPageLoadForEdit(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Protected Sub Page_Load(ByVAl sender As Object, ByVal e As System.EventArgs) Handles Me.Load")
            strB.AppendLine(Space(tab.XX) & "If Not IsPostBack Then")
            strB.AppendLine(Space(tab.XXX) & "Dim myObject As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & _
                            " = get" & pClass.Name.Capitalized & "FromQueryString()")
            strB.AppendLine(Space(tab.XXX) & "If myObject Is Nothing Then")
            strB.AppendLine(Space(tab.XXXX) & "SessionVariables.addError(StringToolkit.ObjectNotFound(AliasGroup." & pClass.Name.Capitalized & "))")
            strB.AppendLine(Space(tab.XXXX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"")")
            strB.AppendLine(Space(tab.XXX) & "End If")
            strB.AppendLine(Space(tab.XXX) & "fillForm(myObject)")
            strB.AppendLine(Space(tab.XX) & "End If")
            strB.AppendLine(Space(tab.X) & "End Sub")
        Else
            strB.AppendLine(Space(tab.X) & "protected void Page_Load(object sender, EventArgs e)")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "if (!IsPostBack)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
                            & " myObject = get" & pClass.Name.Capitalized & "FromQueryString();")
            strB.AppendLine(Space(tab.XXX) & "if (myObject != null)")
            strB.AppendLine(Space(tab.XXX) & "{")
            strB.AppendLine(Space(tab.XXXX) & "SessionVariables.addError(StringToolkit.ObjectNotFound(AliasGroup." & pClass.Name.Capitalized & "));")
            strB.AppendLine(Space(tab.XXXX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"");")
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XXX) & "fillForm(myObject);")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.X) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getPageInstructions(lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Protected Overrides Sub fillPageInstructions()")
            strB.AppendLine(Space(tab.XX) & "lblPageInstructions.Text=""""")
            strB.AppendLine(Space(tab.X) & "End Sub")
        Else
            strB.AppendLine(Space(tab.X) & "protected override void fillPageInstructions()")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "lblPageInstructions.Text="""";")
            strB.AppendLine(Space(tab.X) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getGetFunctionForQueryString(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Private Function get" & pClass.Name.Capitalized & "FromQueryString() As " & _
                            pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized)
            strB.AppendLine(Space(tab.XX) & "Return " & pClass.NameSpaceVariable.NameBasedOnID & "." & _
                            pClass.DALClassVariable.Name & ".get" & pClass.Name.Capitalized & "(Request.QueryString(""id""), True)")
            strB.AppendLine(Space(tab.X) & "End Function")
        Else
            strB.AppendLine(Space(tab.X) & "private " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
                            & " get" & pClass.Name.Capitalized & "FromQueryString()")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "return " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.DALClassVariable.Name _
                            & ".get" & pClass.Name.Capitalized & "(Request.QueryString(""id""), True);")
            strB.AppendLine(Space(tab.X) & "}")
        End If
        strB.AppendLine()

        Return strB.ToString()
    End Function
    Private Function getFillFormForEdit(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        Dim lineEnd As Char = " "c
        Dim conCat As Char = "&"c
        If lang = language.CSharp Then
            lineEnd = ";"c
            conCat = "+"c
        End If
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Private Sub fillForm(ByVal my" & pClass.Name.Capitalized & " As " _
                            & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & ")")
            strB.AppendLine(Space(tab.XX) & "If my" & pClass.Name.Capitalized & ".ID = -1 Then ")
        Else
            strB.AppendLine(Space(tab.X) & "private void fillForm(" & pClass.NameSpaceVariable.NameBasedOnID _
                            & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized & ")")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "if (my" & pClass.Name.Capitalized & ".ID = -1)")
            strB.AppendLine(Space(tab.XX) & "{")
        End If
        strB.AppendLine(Space(tab.XXX) & "btnSaveChanges.Text = AliasGroup.Add.Capitalized" & lineEnd)
        strB.AppendLine(Space(tab.XXX) & "litTitle.Text = AliasGroup.Add.Capitalized " & conCat _
                        & " "" ""  & AliasGroup." & pClass.Name.Capitalized & ".Capitalized" & lineEnd)
        strB.AppendLine(Space(tab.XXX) & "lblSubTitle.Text=litTitle.Text" & lineEnd)

        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "Else")
        Else
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "else")
            strB.AppendLine(Space(tab.XX) & "{")
        End If

        strB.AppendLine(Space(tab.XXX) & "litTitle.Text = AliasGroup.Edit.Capitalized " & conCat _
                        & " "" "" " & conCat & " AliasGroup." & pClass.Name.Capitalized & ".Capitalized" & lineEnd)
        strB.AppendLine(Space(tab.XXX) & "lblSubTitle.Text=litTitle.Text" & lineEnd)
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.DisplayOnEditPage Then Continue For
            If classVar.IsTextBox Then
                strB.Append(Space(tab.XXX) & "txt" & classVar.Name & ".Text = my" & pClass.Name.Capitalized & "." & classVar.Name)
                If classVar.IsDouble OrElse classVar.IsInteger Then
                    strB.Append(".ToString()")
                ElseIf classVar.ParameterType.IsNameAlias Then
                    strB.Append(".TextUnFormatted")
                End If
                strB.AppendLine(lineEnd)
            ElseIf classVar.IsCheckBox Then
                strB.AppendLine(Space(tab.XXX) & classVar.DefaultHTMLName & ".Checked = my" & _
                                pClass.Name.Capitalized & "." & classVar.Name & lineEnd)
            ElseIf classVar.IsDate Then
                strB.AppendLine(Space(tab.XXX) & classVar.GetMonthTextControlName & ".Text = my" & _
                                pClass.Name.Capitalized & "." & classVar.Name & ".Month.ToString()" & lineEnd)
                strB.AppendLine(Space(tab.XXX) & classVar.getDayTextControlName & ".Text = my" & _
                                pClass.Name.Capitalized & "." & classVar.Name & ".Day.ToString()" & lineEnd)
                strB.AppendLine(Space(tab.XXX) & classVar.getYearTextControlName & ".Text = my" & _
                                pClass.Name.Capitalized & "." & classVar.Name & ".Year.ToString()" & lineEnd)

            ElseIf classVar.IsDropDownList Then
                Dim tempAlias As New NameAlias(classVar.ParameterType.Name.ToLower())
                If lang = language.VisualBasic Then
                    strB.AppendLine(Space(tab.XXX) & "For Each tempObject As " & pClass.NameSpaceVariable.NameBasedOnID & _
                                    "." & classVar.ParameterType.Name & " In " & pClass.NameSpaceVariable.NameBasedOnID & _
                                    "." & pClass.DALClassVariable.Name & ".Get" & tempAlias.PluralAndCapitalized & "()")
                Else
                    strB.AppendLine(Space(tab.XXX) & "foreach (" & pClass.NameSpaceVariable.NameBasedOnID & _
                                    "." & classVar.ParameterType.Name & " tempObject in " & pClass.NameSpaceVariable.NameBasedOnID & _
                                    "." & pClass.DALClassVariable.Name & ".Get" & tempAlias.PluralAndCapitalized & "())")
                    strB.AppendLine(Space(tab.XXX) & "{")
                End If
                strB.Append(Space(tab.XXXX) & classVar.DefaultHTMLName & ".Items.Add(new ListItem(tempObject.")
                If classVar.ParentClass IsNot Nothing Then
                    Dim valName As String = ""
                    Dim txtName As String = ""
                    If classVar.ParentClass.ValueVariable IsNot Nothing Then valName = classVar.ParentClass.ValueVariable.Name & "."
                    If classVar.ParentClass.TextVariable IsNot Nothing Then valName = classVar.ParentClass.TextVariable.Name & "."
                    strB.AppendLine(txtName & "ToString(), tempObject." & valName & "ToString()))" & lineEnd)
                Else
                    If classVar.ParameterType.AssociatedProjectClass IsNot Nothing Then
                        strB.AppendLine(classVar.ParameterType.AssociatedProjectClass.TextVariable.Name & ".ToString(), tempObject." & classVar.ParentClass.ValueVariable.Name & ".ToString()))" & lineEnd)
                    Else
                        strB.AppendLine("Name.ToString(), tempObject.test.ToString()))" & lineEnd)
                    End If
                End If
                If lang = language.VisualBasic Then
                    strB.AppendLine(Space(tab.XXX) & "Next")
                Else
                    strB.AppendLine(Space(tab.XXX) & "}")
                End If
                strB.AppendLine(Space(tab.XXX) & classVar.DefaultHTMLName & ".SelectedValue = my" & _
                                pClass.Name.Capitalized & "." & classVar.Name & "ID.ToString()" & lineEnd)
            End If
        Next
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "End If ")
            strB.AppendLine(Space(tab.X) & "End Sub")
        Else
            strB.AppendLine(Space(tab.XXX) & "}")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()

        Return strB.ToString()
    End Function
    Private Function getCancelForEdit(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click")
            strB.AppendLine(Space(tab.XX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"")")
            strB.AppendLine(Space(tab.X) & "End Sub")
        Else
            strB.AppendLine(Space(tab.XX) & "protected void btnCancel_Click(object sender, System.EventArgs e)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"");")
            strB.AppendLine(Space(tab.XX) & "}")
        End If
        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getValidateFunction(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        Dim lineEnd As Char
        If lang = language.VisualBasic Then
            lineEnd = " "c
            strB.AppendLine(Space(tab.X) & "Private Function validateForm() As Boolean")
            strB.AppendLine(Space(tab.XX) & "'TODO: Confirm validation")
            strB.AppendLine(Space(tab.XX) & "Dim retVal As Boolean = True")
        Else
            lineEnd = ";"c
            strB.AppendLine(Space(tab.X) & "private bool validateForm()")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "//TODO: Confirm validation")
            strB.AppendLine(Space(tab.XX) & "bool retVal = true;")
        End If
        Dim doubleExists As Boolean = False
        Dim integerExists As Boolean = False
        Dim dateExists As Boolean = False
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.DisplayOnEditPage Then Continue For
            If classVar.IsDouble Then
                If Not doubleExists Then
                    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDouble As Double", "double tempdouble;").ToString())
                    doubleExists = True
                End If
                If lang = language.VisualBasic Then
                    strB.AppendLine(Space(tab.XX) & "If Not Double.TryParse(" & classVar.DefaultHTMLName & ".Text, tempDouble) Then ")
                Else
                    strB.AppendLine(Space(tab.XX) & "if (!double.TryParse(" & classVar.DefaultHTMLName & ".Text, tempDouble))")
                    strB.AppendLine(Space(tab.XX) & "{")
                End If
                strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(""" & classVar.Name & " is in an invalid format."")" & lineEnd)
                strB.AppendLine(Space(tab.XXX) & "retVal = false" & lineEnd)

                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
            ElseIf classVar.IsInteger Then
                If Not integerExists Then
                    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempInteger As Integer", "int tempInteger;").ToString())
                    integerExists = True
                End If
                If lang = language.VisualBasic Then
                    strB.AppendLine(Space(tab.XX) & "If Not Integer.TryParse(" & classVar.DefaultHTMLName & ".Text, tempInteger) Then ")
                Else
                    strB.AppendLine(Space(tab.XX) & "if (!int.TryParse(" & classVar.DefaultHTMLName & ".Text, tempInteger))")
                    strB.AppendLine(Space(tab.XX) & "{")
                End If
                strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(""" & classVar.Name & " is in an invalid format."")" & lineEnd)
                strB.AppendLine(Space(tab.XXX) & "retVal = False" & lineEnd)

                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())

            ElseIf classVar.IsDate Then
                If Not dateExists Then
                    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDateTime As DateTime", "DateTime tempDateTime;").ToString())
                End If
                Dim dateString As String = classVar.GetMonthTextControlName() & ".Text.Trim() & ""/"" & " & _
                                            classVar.getDayTextControlName() & ".Text.Trim() & ""/"" & " & _
                                            classVar.getYearTextControlName() & ".Text.Trim()"
                If lang = language.VisualBasic Then
                    strB.AppendLine(Space(tab.XX) & "If Not DateTime.TryParse(" & dateString & ", tempDateTime) Then ")
                Else
                    strB.AppendLine(Space(tab.XX) & "if (!DateTime.TryParse(" & dateString & ", tempDateTime))")
                    strB.AppendLine(Space(tab.XX) & "{")
                End If
                strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(""" & classVar.Name & " is in an invalid format."")" & lineEnd)
                strB.AppendLine(Space(tab.XXX) & "retVal = false" & lineEnd)

                strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
                dateExists = True
            End If
        Next

        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Return", "return").ToString() & " retVal" & lineEnd)
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End Function", "}").ToString())

        strB.AppendLine()
        Return strB.ToString()
    End Function
    Private Function getSaveChanges(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        Dim lineEnd As Char
        If lang = language.VisualBasic Then
            lineEnd = " "c
            strB.AppendLine(Space(tab.X) & "Protected Sub btnSaveChanges_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click")
            strB.AppendLine(Space(tab.XX) & "If Not validateForm() Then Exit Sub")
            strB.AppendLine(Space(tab.XX) & "Dim my" & pClass.Name.Capitalized & " As " & pClass.NameSpaceVariable.Name & "." _
                            & pClass.Name.Capitalized & " = get" & pClass.Name.Capitalized & "FromQueryString()")
        Else
            lineEnd = ";"c
            strB.AppendLine(Space(tab.X) & "protected void btnSaveChanges_Click(object sender, System.EventArgs e)")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "if (!validateForm()) return;")
            strB.AppendLine(Space(tab.XX) & pClass.NameSpaceVariable.Name & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized _
                            & " = get" & pClass.Name.Capitalized & "FromQueryString();")
        End If
        Dim doubleExists As Boolean = False
        Dim integerExists As Boolean = False
        Dim dateExists As Boolean = False
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.DisplayOnEditPage OrElse Not classVar.IsDatabaseBound Then Continue For
            If classVar.IsCheckBox Then
                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = " & classVar.DefaultHTMLName & ".Checked" & lineEnd)
            ElseIf classVar.IsTextBox Then
                If classVar.IsDouble Then
                    If Not doubleExists Then
                        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDouble As Double", "double tempDouble;").ToString())
                        doubleExists = True
                    End If
                    strB.AppendLine(Space(tab.XX) & "Double.TryParse(" & classVar.DefaultHTMLName & ".Text, tempDouble)" & lineEnd)
                    strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = tempDouble" & lineEnd)
                ElseIf classVar.IsInteger Then
                    If Not integerExists Then
                        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempInteger As Integer", "int tempInteger;").ToString())
                        integerExists = True
                    End If
                    strB.Append(Space(tab.XX) & IIf(lang = language.VisualBasic, "Integer", "int").ToString())
                    strB.AppendLine(".TryParse(" & classVar.DefaultHTMLName & ".Text, tempInteger)" & lineEnd)
                    strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = tempInteger" & lineEnd)

                ElseIf classVar.ParameterType.IsNameAlias Then
                    strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & ".TextUnFormatted = txt" & classVar.Name & ".Text" & lineEnd)
                Else
                    strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = txt" & classVar.Name & ".Text" & lineEnd)
                End If

            ElseIf classVar.IsDropDownList Then
                strB.Append(Space(tab.XX) & "my" & pClass.Name.Capitalized)
                If classVar.ParameterType.AssociatedProjectClass IsNot Nothing Then
                    strB.Append("." & classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized)
                End If
                strB.AppendLine(" = " & IIf(lang = language.VisualBasic, cg.getConvertFunction("Integer", lang), "int.Parse").ToString() _
                                & "(" & classVar.DefaultHTMLName & ".SelectedValue)" & lineEnd)
            ElseIf classVar.IsDate Then
                If Not dateExists Then
                    strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Dim tempDateTime As DateTime", "DateTime tempDateTime;").ToString())
                End If
                strB.AppendLine(Space(tab.XX) & "DateTime.TryParse(" & classVar.GetMonthTextControlName() & ".Text.Trim() & ""/"" & " & _
                                                                    classVar.getDayTextControlName() & ".Text.Trim() & ""/"" & " & _
                                                                    classVar.getYearTextControlName() & ".Text.Trim(), tempDateTime)" & lineEnd)
                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = tempDateTime" & lineEnd)
                dateExists = True
                '                strB.AppendLine(Space(tab.XX) & "my" & pClass.Name.Capitalized & "." & classVar.Name & " = new DateTime(txtYear.Text, txtMonth.Text, txtDay.Text)" )
            End If
        Next
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, _
                                             "If my" & pClass.Name.Capitalized & ".ID = -1 Then", _
                                             "if (my" & pClass.Name.Capitalized & ".ID == -1)").ToString())
        If lang = language.CSharp Then strB.AppendLine(Space(tab.XX) & "{")
        strB.AppendLine(Space(tab.XXX) & "add" & pClass.Name.Capitalized & "(my" & pClass.Name.Capitalized & ")" & lineEnd)
        If lang = language.CSharp Then strB.Append("}")
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Else", "else").ToString())
        If lang = language.CSharp Then strB.Append("{")
        strB.AppendLine(Space(tab.XXX) & "update" & pClass.Name.Capitalized & "(my" & pClass.Name.Capitalized & ")" & lineEnd)
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
        strB.AppendLine(Space(tab.XX) & "Redirect(""" & pClass.Name.PluralAndCapitalized & ".aspx"")" & lineEnd)
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End Sub", "}").ToString())

        strB.AppendLine()

        Return strB.ToString()

    End Function
    Private Function getAddObject(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Private Function add" & pClass.Name.Capitalized & "(ByVal my" & pClass.Name.Capitalized & " As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & ") As Boolean")
            strB.AppendLine(Space(tab.XX) & "If my" & pClass.Name.Capitalized & ".dbAdd() > 0 Then")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add))")
            strB.AppendLine(Space(tab.XX) & "Else")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add))")
            strB.AppendLine(Space(tab.XX) & "End If")
            strB.AppendLine(Space(tab.X) & "End Function")
        Else
            strB.AppendLine(Space(tab.X) & "private bool add" & pClass.Name.Capitalized & "(" & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized & ")")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "if (my" & pClass.Name.Capitalized & ".dbAdd() > 0)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add));")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "else")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Add));")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.X) & "}")
        End If
        Return strB.ToString()
    End Function
    Private Function getUpdateObject(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Private Function update" & pClass.Name.Capitalized & "(ByVal my" & pClass.Name.Capitalized & _
                            " As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & ") As Boolean")
            strB.AppendLine(Space(tab.XX) & "If my" & pClass.Name.Capitalized & ".dbUpdate() > 0 Then")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit))")
            strB.AppendLine(Space(tab.XX) & "Else")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit))")
            strB.AppendLine(Space(tab.XX) & "End If")
            strB.AppendLine(Space(tab.X) & "End Function")
        Else
            strB.AppendLine(Space(tab.X) & "private bool update" & pClass.Name.Capitalized & "(" & _
                            pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " my" & pClass.Name.Capitalized & ")")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "if (my" & pClass.Name.Capitalized & ".dbUpdate() > 0)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit));")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.XX) & "else")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." & pClass.Name.Capitalized & ", AliasGroup.Edit));")
            strB.AppendLine(Space(tab.XX) & "}")
            strB.AppendLine(Space(tab.X) & "}")
        End If
        Return strB.ToString()

    End Function
   
    Public Function getEditCodeBehind(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        strB.Append(cg.getPageImports(lang))
        strB.Append(cg.getClassDeclaration(lang, "_Edit" & pClass.Name.Capitalized, tab.None, "BasePage"))
        strB.AppendLine()
        strB.AppendLine(getPageLoadForEdit(pClass, lang))
        strB.AppendLine(getPageInstructions(lang))
        strB.AppendLine(getGetFunctionForQueryString(pClass, lang))
        strB.AppendLine(getFillFormForEdit(pClass, lang))
        strB.AppendLine(getCancelForEdit(pClass, lang))
        strB.AppendLine(getValidateFunction(pClass, lang))
        strB.AppendLine(getSaveChanges(pClass, lang))
        strB.AppendLine(getAddObject(pClass, lang))
        strB.AppendLine(getUpdateObject(pClass, lang))
        strB.AppendLine(IIf(lang = language.VisualBasic, "End Class", "}").ToString())
        Return strB.ToString()
    End Function
    Public Function getViewCodeBehind(ByVal pClass As ProjectClass, lang As language) As String
        Dim strB As New StringBuilder
        Dim lineEnd As Char
        Dim conCat As Char
        strB.Append(cg.getPageImports(lang))
        strB.Append(cg.getClassDeclaration(lang, "_" & pClass.Name.PluralAndCapitalized, tab.None, "BasePage"))
        'Page Load
        If lang = language.VisualBasic Then
            lineEnd = " "c
            conCat = "&"c
            strB.AppendLine(Space(tab.X) & "Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load")
        Else
            lineEnd = ";"c
            conCat = "+"c
            strB.AppendLine(Space(tab.X) & "protected void Page_Load(object sender, System.EventArgs e)")
            strB.AppendLine(Space(tab.X) & "{")
        End If
        strB.AppendLine(Space(tab.XX) & "fill" & pClass.Name.PluralAndCapitalized & "Table()" & lineEnd)
        strB.AppendLine(Space(tab.XX) & "fillFieldsFromSiteConfig()" & lineEnd)
        strB.AppendLine(Space(tab.XX) & "fillPageInstructions()" & lineEnd)

        strB.AppendLine(Space(tab.X) & IIf(lang = language.VisualBasic, "End Sub", "}").ToString())

        strB.AppendLine(Space(tab.X) & getPageInstructions(lang))
        'fillTable
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Private Sub fill" & pClass.Name.PluralAndCapitalized & "Table()")
            strB.AppendLine(Space(tab.XX) & "Dim alternateRow As Boolean = False")
            strB.AppendLine(Space(tab.XX) & "Dim listOfObjects As List(Of " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
                            & ") = " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.DALClassVariable.Name & _
                            ".Get" & pClass.Name.PluralAndCapitalized & "()")
            strB.AppendLine(Space(tab.XX) & "For Each myObject As " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " In listOfObjects")
            strB.AppendLine(Space(tab.XXX) & "Dim tRow As New TableRow")
            strB.AppendLine(Space(tab.XXX) & "If alternateRow Then tRow.CssClass=""other""")
            strB.AppendLine(Space(tab.XXX) & "alternateRow = Not alternateRow")
        Else
            strB.AppendLine(Space(tab.X) & "private void fill" & pClass.Name.PluralAndCapitalized & "Table()")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.XX) & "bool alternateRow = false;")
            strB.AppendLine(Space(tab.XX) & "List<" & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized _
                            & "> listOfObjects = " & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.DALClassVariable.Name _
                            & ".Get" & pClass.Name.PluralAndCapitalized & "();")
            strB.AppendLine(Space(tab.XX) & "foreach (" & pClass.NameSpaceVariable.NameBasedOnID & "." & pClass.Name.Capitalized & " myObject in listOfObjects)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "TableRow tRow = new TableRow();")
            strB.AppendLine(Space(tab.XXX) & "if (alternateRow) tRow.CssClass=""other"";")
            strB.AppendLine(Space(tab.XXX) & "alternateRow = !alternateRow;")
        End If

        Dim countOfColumns As Integer = 0
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.DisplayOnViewPage Then Continue For
            countOfColumns += 1
            strB.Append(Space(tab.XXX) & "tRow.Controls.Add(TableToolkit.getTableCell(myObject.")
            If classVar.ParameterType.IsNameAlias Then
                strB.Append(classVar.Name & ".ToString()")
            ElseIf classVar.ParameterType.IsPrimitive Then
                strB.Append(classVar.Name)
            Else
                strB.Append(ClassGenerator.getSystemUniqueName(classVar.Name))
            End If
            If classVar.IsDouble OrElse classVar.IsInteger OrElse classVar.IsDropDownList OrElse classVar.IsDate OrElse classVar.IsCheckBox Then
                strB.Append(".ToString()")
            End If
            strB.AppendLine("))" & lineEnd)
        Next
        strB.AppendLine(Space(tab.XXX) & "tRow.Controls.Add(TableToolkit.getHyperlinkCell(AliasGroup.Edit.Capitalized, ""Edit" _
                        & pClass.Name.Capitalized & ".aspx?id="" " & conCat & " myObject.ID.ToString()))" & lineEnd)
        strB.AppendLine(Space(tab.XXX) & "tbl" & pClass.Name.PluralAndCapitalized & ".Rows.Add(tRow)" & lineEnd)
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "Next", "}").ToString())
        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.XX) & "If listOfObjects.Count = 0 Then")
            strB.AppendLine(Space(tab.XXX) & "Dim tRow As New TableRow")
        Else
            strB.AppendLine(Space(tab.XX) & "if (listOfObjects.Count == 0)")
            strB.AppendLine(Space(tab.XX) & "{")
            strB.AppendLine(Space(tab.XXX) & "TableRow tRow = new TableRow();")
        End If
        strB.AppendLine(Space(tab.XXX) & "tRow.Controls.Add(TableToolkit.getNoResultsFoundCell(AliasGroup." & pClass.Name.Capitalized _
                        & ", " & countOfColumns & "))" & lineEnd)
        strB.AppendLine(Space(tab.XXX) & "tbl" & pClass.Name.PluralAndCapitalized & ".Rows.Add(tRow)" & lineEnd)
        strB.AppendLine(Space(tab.XX) & IIf(lang = language.VisualBasic, "End If", "}").ToString())
        strB.AppendLine(Space(tab.X) & IIf(lang = language.VisualBasic, "End Sub", "}").ToString())

        If lang = language.VisualBasic Then
            strB.AppendLine(Space(tab.X) & "Private Sub fillFieldsFromSiteConfig()")
            strB.AppendLine(Space(tab.X) & "'TODO: Fill Site Variables on " & pClass.Name.PluralAndCapitalized & ".aspx")
            strB.AppendLine(Space(tab.X) & "hypAdd" & pClass.Name.Capitalized & ".Text = ""Add "" & AliasGroup." & pClass.Name.Capitalized & ".Capitalized")
            strB.AppendLine(Space(tab.X) & "End Sub")
        Else
            strB.AppendLine(Space(tab.X) & "private void fillFieldsFromSiteConfig()")
            strB.AppendLine(Space(tab.X) & "{")
            strB.AppendLine(Space(tab.X) & "//TODO: Fill Site Variables on " & pClass.Name.PluralAndCapitalized & ".aspx")
            strB.AppendLine(Space(tab.X) & "hypAdd" & pClass.Name.Capitalized & ".Text = ""Add "" & AliasGroup." & pClass.Name.Capitalized & ".Capitalized;")
            strB.AppendLine(Space(tab.X) & "}")
        End If

        strB.AppendLine(Space(tab.X) & IIf(lang = language.VisualBasic, "End Class", "}").ToString())

        Return strB.ToString()
    End Function
    Public Function getViewForm(ByVal pClass As ProjectClass, useLists As Boolean, lang As language) As String
        Dim strB As New StringBuilder
        strB.AppendLine(getHeaderLine(pClass, lang, pageVersion.View))
        strB.AppendLine(generateContentHeaders(pClass, False, useLists, lang))
        Return strB.ToString()
    End Function
    Public Function getViewBody(ByVal pClass As ProjectClass) As String
        Dim strB As New StringBuilder
        strB.AppendLine(Space(tab.X) & "<asp:Hyperlink ID=""hypAdd" & pClass.Name.Capitalized & """ runat=""server"" NavigateUrl=""Edit" _
                        & pClass.Name.Capitalized & ".aspx?id=-1""></asp:Hyperlink>")
        strB.AppendLine(Space(tab.X) & "<asp:Table ID=""tbl" & pClass.Name.PluralAndCapitalized & """ runat=""server"" CssClass=""list"">")
        strB.AppendLine(Space(tab.XX) & "<asp:TableHeaderRow>")
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.DisplayOnViewPage Then Continue For
            strB.AppendLine(Space(tab.XXX) & "<asp:TableHeaderCell>" & classVar.Name & "</asp:TableHeaderCell>")
        Next
        strB.AppendLine(Space(tab.XX) & "</asp:TableHeaderRow>")
        strB.AppendLine(Space(tab.X) & "</asp:Table>")
        Return strB.ToString()
    End Function
    Private Enum pageVersion
        Edit
        View
    End Enum
    Private Function getHeaderLine(ByVal pClass As ProjectClass, lang As language, pVersion As pageVersion) As String
        Dim codeExt As String = "vb"
        Dim codeVer As String = "VB"
        If lang = language.CSharp Then
            codeExt = "cs" : codeVer = "C#"
        End If
        Dim pageName As String = IIf(pVersion = pageVersion.Edit, _
                                    "Edit" & pClass.Name.Capitalized, _
                                    pClass.Name.PluralAndCapitalized).ToString()
       
        Return String.Format("<%@ Page Title="""" Language=""{3}"" MasterPageFile=""~/{0}"" AutoEventWireup=""false"" CodeFile=""~/{1}.aspx.{2}" _
                                & """ Inherits=""_{1}"" %>", pClass.MasterPage.FileName, pageName, codeExt, codeVer)

    End Function
    'Private Function getViewHeader(ByVal pClass As ProjectClass, lang As language) As String
    '    Dim codeExt As String = "vb"
    '    If lang = language.CSharp Then codeExt = "cs"
    '    Dim headerValue As String = "<%@ Page Title="""" Language=""VB"" MasterPageFile=""~/" & _
    '        pClass.MasterPage.FileName & """ AutoEventWireup=""false"" CodeFile=""~/" & pClass.Name.PluralAndCapitalized & ".aspx." & codeExt _
    '        & """ Inherits=""" & pClass.Name.PluralAndCapitalized & """ %>" & vbCrLf
    '    Return headerValue
    'End Function
    Private Function generateEditBody(ByVal pClass As ProjectClass, useLists As Boolean, lang As language) As String
        Dim formTag As String = IIf(useLists, "ul", "div").ToString()
        Dim rowTag As String = IIf(useLists, "li", "div").ToString()
        Dim rowBtnOpenTag As String = IIf(useLists, "", "<div>").ToString()
        Dim rowBtnCloseTag As String = IIf(useLists, "", "</div>").ToString()
        Dim retStrB As New StringBuilder()
        retStrB.AppendLine("<" & formTag & " class=""form"">")
        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not classVar.DisplayOnEditPage Then Continue For
            retStrB.AppendLine(Space(tab.XX) & "<" & rowTag & ">")
            retStrB.Append(Space(tab.XXX) & "<asp:Label ID=""lbl" & _
                               IIf(classVar.Name.ToLower().CompareTo("subtitle") = 0, pClass.Name, "").ToString() _
                               & classVar.Name & """ runat=""server"" AssociatedControlID=""")
            If classVar.IsInteger OrElse classVar.IsDouble Then
                'Small textBox: class= number
                retStrB.AppendLine(classVar.DefaultHTMLName & """>" & classVar.Name & "</asp:Label>")
                retStrB.AppendLine(Space(tab.XXX) & "<asp:TextBox ID=""" & classVar.DefaultHTMLName & """ runat=""server"" CssClass=""number""></asp:TextBox>")
            ElseIf classVar.IsCheckBox Then
                'CheckBox
                retStrB.AppendLine(classVar.DefaultHTMLName & """>" & classVar.Name & "</asp:Label>")
                retStrB.AppendLine(Space(tab.XXX) & "<asp:CheckBox ID=""" & classVar.DefaultHTMLName & """ runat=""server""></asp:CheckBox>")
            ElseIf classVar.IsDate Then
                'three textboxes (day,month,year)
                retStrB.AppendLine(classVar.GetMonthTextControlName() & """>" & classVar.Name & "</asp:Label>")
                retStrB.AppendLine(Space(tab.XXX) & "<asp:Panel ID=""pnl" & classVar.Name & "Date"" runat=""server"">")
                retStrB.AppendLine(Space(tab.XXXX) & "<asp:TextBox ID=""" & classVar.GetMonthTextControlName() _
                                   & """ runat=""server"" CssClass=""number""></asp:TextBox>")
                retStrB.AppendLine(Space(tab.XXXX) & "<asp:TextBox ID=""" & classVar.getDayTextControlName() _
                                   & """ runat=""server"" CssClass=""number""></asp:TextBox>")
                retStrB.AppendLine(Space(tab.XXXX) & "<asp:TextBox ID=""" & classVar.getYearTextControlName() _
                                   & """ runat=""server"" CssClass=""number""></asp:TextBox>")
                retStrB.AppendLine(Space(tab.XXX) & "</asp:Panel>")
            Else
                If classVar.IsTextBox Then
                    If classVar.ParameterType.Name.ToLower = "namealias" Then
                        retStrB.AppendLine("txt" & classVar.Name & """>" & classVar.Name & "</asp:Label>")
                        retStrB.AppendLine(Space(tab.XXX) & "<asp:TextBox ID=""txt" & classVar.Name & """ runat=""server""></asp:TextBox>")
                    Else
                        'textbox
                        If classVar.DefaultHTMLName.CompareTo("lblsubtitle") = 0 Then
                            retStrB.AppendLine(ClassGenerator.getSystemUniqueName(classVar.DefaultHTMLName) & """>" & classVar.Name & "</asp:Label>")
                        Else
                            retStrB.AppendLine(classVar.DefaultHTMLName & """>" & classVar.Name & "</asp:Label>")
                        End If
                        retStrB.AppendLine(Space(tab.XXX) & "<asp:TextBox ID=""" & classVar.DefaultHTMLName & """ runat=""server""></asp:TextBox>")
                    End If
                Else
                    'dropdownlist
                    Dim myAlias As New NameAlias(classVar.Name)
                    retStrB.AppendLine(classVar.DefaultHTMLName & """>" & myAlias.PluralAndCapitalized & "</asp:Label>")
                    retStrB.AppendLine(Space(tab.XXX) & "<asp:DropDownList ID=""" & classVar.DefaultHTMLName & """ runat=""server""></asp:DropDownList>")
                End If
            End If
            retStrB.AppendLine(Space(tab.XX) & "</" & rowTag & ">")
        Next
        retStrB.AppendLine(Space(tab.XX) & "<" & rowTag & " class=""buttons"">")
        retStrB.AppendLine(Space(tab.XXX) & rowBtnOpenTag & "<asp:Button ID=""btnSaveChanges"" runat=""server"" Text=""Save Changes"" " _
                           & IIf(lang = language.CSharp, "OnClick=""btnSaveChanges_Click"" ", "").ToString() & "/>" & rowBtnCloseTag)
        retStrB.AppendLine(Space(tab.XXX) & rowBtnOpenTag & "<asp:Button ID=""btnCancel"" runat=""server"" Text=""Cancel"" " _
                           & IIf(lang = language.CSharp, "OnClick=""btnCancel_Click"" ", "").ToString() & "/>" & rowBtnCloseTag)
        retStrB.AppendLine(Space(tab.XX) & "</" & rowTag & ">")
        retStrB.AppendLine(Space(tab.X) & "</" & formTag & ">")
        Return retStrB.ToString()
    End Function

    Private Function generateContentHeaders(ByVal pClass As ProjectClass, ByVal isEditForm As Boolean, useLists As Boolean, lang As language) As String
        Dim retStr As New System.Text.StringBuilder
        retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.TitleName & """ ContentPlaceHolderID=""cph" _
                          & pClass.MasterPage.TitleName & """ Runat=""Server"">")
        retStr.AppendLine(Space(tab.X) & "<asp:Literal ID=""lit" & pClass.MasterPage.TitleName & """ runat=""server""></asp:Literal>")
        retStr.AppendLine("</asp:Content>")
        retStr.AppendLine()
        retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.SubTitleName & """ ContentPlaceHolderID=""cph" _
                          & pClass.MasterPage.SubTitleName & """ Runat=""Server"">")
        retStr.AppendLine(Space(tab.X) & "<asp:Label ID=""lbl" & pClass.MasterPage.SubTitleName & """ runat=""server""></asp:Label>")
        retStr.AppendLine("</asp:Content>")
        retStr.AppendLine()
        retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.PageInstructionsName & """ ContentPlaceHolderID=""cph" _
                      & pClass.MasterPage.PageInstructionsName & """ Runat=""Server"">")
        retStr.AppendLine(Space(tab.X) & "<asp:Label ID=""lbl" & pClass.MasterPage.PageInstructionsName & """ runat=""server""></asp:Label>")
        retStr.AppendLine("</asp:Content>")
        retStr.AppendLine()

        retStr.AppendLine("<asp:Content ID=""ct" & pClass.MasterPage.BodyName & """ ContentPlaceHolderID=""cph" _
                          & pClass.MasterPage.BodyName & """ Runat=""Server"">")
        If isEditForm Then
            retStr.AppendLine(generateEditBody(pClass, useLists, lang))
        Else
            retStr.AppendLine(getViewBody(pClass))
        End If
        retStr.AppendLine("</asp:Content>")
        Return retStr.ToString
    End Function
End Class
