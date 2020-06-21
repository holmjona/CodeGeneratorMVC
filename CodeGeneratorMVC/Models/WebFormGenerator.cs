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
using IRICommonObjects.Words;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;

public class WebFormGenerator
{
    public WebFormGenerator()
    {
    }
    public string getEditForm(ProjectClass pClass, bool useLists, language lang)
    {
        StringBuilder strB = new StringBuilder();
        strB.Append(getHeaderLine(pClass, lang, pageVersion.Edit));
        strB.AppendLine(generateContentHeaders(pClass, true, useLists, lang));
        return strB.ToString();
    }
    private string getPageLoadForEdit(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Protected Sub Page_Load(ByVAl sender As Object, ByVal e As System.EventArgs) Handles Me.Load");
            strB.AppendLine(Strings.Space((int)tab.XX) + "If Not IsPostBack Then");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim myObject As " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + " = get" + pClass.Name.Capitalized + "FromQueryString()");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If myObject Is Nothing Then");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "SessionVariables.addError(StringToolkit.ObjectNotFound(AliasGroup." + pClass.Name.Capitalized + "))");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Redirect(\"" + pClass.Name.PluralAndCapitalized + ".aspx\")");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "fillForm(myObject)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Sub");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "protected void Page_Load(object sender, EventArgs e)");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "if (!IsPostBack)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized
                            + " myObject = get" + pClass.Name.Capitalized + "FromQueryString();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (myObject != null)");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "SessionVariables.addError(StringToolkit.ObjectNotFound(AliasGroup." + pClass.Name.Capitalized + "));");
            strB.AppendLine(Strings.Space((int)tab.XXXX) + "Redirect(\"" + pClass.Name.PluralAndCapitalized + ".aspx\");");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "fillForm(myObject);");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getPageInstructions(language lang)
    {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Protected Overrides Sub fillPageInstructions()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "lblPageInstructions.Text=\"\"");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Sub");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "protected override void fillPageInstructions()");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "lblPageInstructions.Text=\"\";");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getGetFunctionForQueryString(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Function get" + pClass.Name.Capitalized + "FromQueryString() As " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized);
            strB.AppendLine(Strings.Space((int)tab.XX) + "Return " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.DALClassVariable.Name + ".get" + pClass.Name.Capitalized + "(Request.QueryString(\"id\"), True)");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Function");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "private " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized
                      + " get" + pClass.Name.Capitalized + "FromQueryString()");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "return " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.DALClassVariable.Name
                            + ".get" + pClass.Name.Capitalized + "(Request.QueryString(\"id\"), True);");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
        }
        strB.AppendLine();

        return strB.ToString();
    }
    private string getFillFormForEdit(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        char lineEnd = ' ';
        char conCat = '&';
        if (lang == language.CSharp)
        {
            lineEnd = ';';
            conCat = '+';
        }
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Sub fillForm(ByVal my" + pClass.Name.Capitalized + " As "
+ pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + ")");
            strB.AppendLine(Strings.Space((int)tab.XX) + "If my" + pClass.Name.Capitalized + ".ID = -1 Then ");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "private void fillForm(" + pClass.NameSpaceVariable.NameBasedOnID
                      + "." + pClass.Name.Capitalized + " my" + pClass.Name.Capitalized + ")");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "if (my" + pClass.Name.Capitalized + ".ID = -1)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        }
        strB.AppendLine(Strings.Space((int)tab.XXX) + "btnSaveChanges.Text = AliasGroup.Add.Capitalized" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XXX) + "litTitle.Text = AliasGroup.Add.Capitalized " + conCat
                        + " \" \"  & AliasGroup." + pClass.Name.Capitalized + ".Capitalized" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XXX) + "lblSubTitle.Text=litTitle.Text" + lineEnd);

        if (lang == language.VisualBasic)
            strB.AppendLine(Strings.Space((int)tab.XX) + "Else");
        else
        {
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        }

        strB.AppendLine(Strings.Space((int)tab.XXX) + "litTitle.Text = AliasGroup.Edit.Capitalized " + conCat
                        + " \" \" " + conCat + " AliasGroup." + pClass.Name.Capitalized + ".Capitalized" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XXX) + "lblSubTitle.Text=litTitle.Text" + lineEnd);
        foreach (ClassVariable classVar in pClass.ClassVariables)
        {
            if (!classVar.DisplayOnEditPage)
                continue;
            if (classVar.IsTextBox)
            {
                strB.Append(Strings.Space((int)tab.XXX) + "txt" + classVar.Name + ".Text = my" + pClass.Name.Capitalized + "." + classVar.Name);
                if (classVar.IsDouble || classVar.IsInteger)
                    strB.Append(".ToString()");
                else if (classVar.ParameterType.IsNameAlias)
                    strB.Append(".TextUnFormatted");
                strB.AppendLine(lineEnd);
            }
            else if (classVar.IsCheckBox)
                strB.AppendLine(Strings.Space((int)tab.XXX) + classVar.DefaultHTMLName + ".Checked = my" + pClass.Name.Capitalized + "." + classVar.Name + lineEnd);
            else if (classVar.IsDate)
            {
                strB.AppendLine(Strings.Space((int)tab.XXX) + classVar.GetMonthTextControlName + ".Text = my" + pClass.Name.Capitalized + "." + classVar.Name + ".Month.ToString()" + lineEnd);
                strB.AppendLine(Strings.Space((int)tab.XXX) + classVar.getDayTextControlName + ".Text = my" + pClass.Name.Capitalized + "." + classVar.Name + ".Day.ToString()" + lineEnd);
                strB.AppendLine(Strings.Space((int)tab.XXX) + classVar.getYearTextControlName + ".Text = my" + pClass.Name.Capitalized + "." + classVar.Name + ".Year.ToString()" + lineEnd);
            }
            else if (classVar.IsDropDownList)
            {
                NameAlias tempAlias = new NameAlias(classVar.ParameterType.Name.ToLower());
                if (lang == language.VisualBasic)
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "For Each tempObject As " + pClass.NameSpaceVariable.NameBasedOnID + "." + classVar.ParameterType.Name + " In " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.DALClassVariable.Name + ".Get" + tempAlias.PluralAndCapitalized + "()");
                else
                {
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "foreach (" + pClass.NameSpaceVariable.NameBasedOnID + "." + classVar.ParameterType.Name + " tempObject in " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.DALClassVariable.Name + ".Get" + tempAlias.PluralAndCapitalized + "())");
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "{");
                }
                strB.Append(Strings.Space((int)tab.XXXX) + classVar.DefaultHTMLName + ".Items.Add(new ListItem(tempObject.");
                if (classVar.ParentClass != null)
                {
                    string valName = "";
                    string txtName = "";
                    if (classVar.ParentClass.ValueVariable != null)
                        valName = classVar.ParentClass.ValueVariable.Name + ".";
                    if (classVar.ParentClass.TextVariable != null)
                        valName = classVar.ParentClass.TextVariable.Name + ".";
                    strB.AppendLine(txtName + "ToString(), tempObject." + valName + "ToString()))" + lineEnd);
                }
                else if (classVar.ParameterType.AssociatedProjectClass != null)
                    strB.AppendLine(classVar.ParameterType.AssociatedProjectClass.TextVariable.Name + ".ToString(), tempObject." + classVar.ParentClass.ValueVariable.Name + ".ToString()))" + lineEnd);
                else
                    strB.AppendLine("Name.ToString(), tempObject.test.ToString()))" + lineEnd);
                if (lang == language.VisualBasic)
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "Next");
                else
                    strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
                strB.AppendLine(Strings.Space((int)tab.XXX) + classVar.DefaultHTMLName + ".SelectedValue = my" + pClass.Name.Capitalized + "." + classVar.Name + "ID.ToString()" + lineEnd);
            }
        }
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.XX) + "End If ");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Sub");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.XXX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();

        return strB.ToString();
    }
    private string getCancelForEdit(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Redirect(\"" + pClass.Name.PluralAndCapitalized + ".aspx\")");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Sub");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.XX) + "protected void btnCancel_Click(object sender, System.EventArgs e)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Redirect(\"" + pClass.Name.PluralAndCapitalized + ".aspx\");");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
        }
        strB.AppendLine();
        return strB.ToString();
    }
    private string getValidateFunction(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        char lineEnd;
        if (lang == language.VisualBasic)
        {
            lineEnd = ' ';
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Function validateForm() As Boolean");
            strB.AppendLine(Strings.Space((int)tab.XX) + "'TODO: Confirm validation");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Dim retVal As Boolean = True");
        }
        else
        {
            lineEnd = ';';
            strB.AppendLine(Strings.Space((int)tab.X) + "private bool validateForm()");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "//TODO: Confirm validation");
            strB.AppendLine(Strings.Space((int)tab.XX) + "bool retVal = true;");
        }
        bool doubleExists = false;
        bool integerExists = false;
        bool dateExists = false;
        foreach (ClassVariable classVar in pClass.ClassVariables)
        {
            if (!classVar.DisplayOnEditPage)
                continue;
            if (classVar.IsDouble)
            {
                if (!doubleExists)
                {
                    strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Dim tempDouble As Double", "double tempdouble;").ToString());
                    doubleExists = true;
                }
                if (lang == language.VisualBasic)
                    strB.AppendLine(Strings.Space((int)tab.XX) + "If Not Double.TryParse(" + classVar.DefaultHTMLName + ".Text, tempDouble) Then ");
                else
                {
                    strB.AppendLine(Strings.Space((int)tab.XX) + "if (!double.TryParse(" + classVar.DefaultHTMLName + ".Text, tempDouble))");
                    strB.AppendLine(Strings.Space((int)tab.XX) + "{");
                }
                strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(\"" + classVar.Name + " is in an invalid format.\")" + lineEnd);
                strB.AppendLine(Strings.Space((int)tab.XXX) + "retVal = false" + lineEnd);

                strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End If", "}").ToString());
            }
            else if (classVar.IsInteger)
            {
                if (!integerExists)
                {
                    strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Dim tempInteger As Integer", "int tempInteger;").ToString());
                    integerExists = true;
                }
                if (lang == language.VisualBasic)
                    strB.AppendLine(Strings.Space((int)tab.XX) + "If Not Integer.TryParse(" + classVar.DefaultHTMLName + ".Text, tempInteger) Then ");
                else
                {
                    strB.AppendLine(Strings.Space((int)tab.XX) + "if (!int.TryParse(" + classVar.DefaultHTMLName + ".Text, tempInteger))");
                    strB.AppendLine(Strings.Space((int)tab.XX) + "{");
                }
                strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(\"" + classVar.Name + " is in an invalid format.\")" + lineEnd);
                strB.AppendLine(Strings.Space((int)tab.XXX) + "retVal = False" + lineEnd);

                strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End If", "}").ToString());
            }
            else if (classVar.IsDate)
            {
                if (!dateExists)
                    strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Dim tempDateTime As DateTime", "DateTime tempDateTime;").ToString());
                string dateString = classVar.GetMonthTextControlName() + ".Text.Trim() & \"/\" & " + classVar.getDayTextControlName() + ".Text.Trim() & \"/\" & " + classVar.getYearTextControlName() + ".Text.Trim()";
                if (lang == language.VisualBasic)
                    strB.AppendLine(Strings.Space((int)tab.XX) + "If Not DateTime.TryParse(" + dateString + ", tempDateTime) Then ");
                else
                {
                    strB.AppendLine(Strings.Space((int)tab.XX) + "if (!DateTime.TryParse(" + dateString + ", tempDateTime))");
                    strB.AppendLine(Strings.Space((int)tab.XX) + "{");
                }
                strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(\"" + classVar.Name + " is in an invalid format.\")" + lineEnd);
                strB.AppendLine(Strings.Space((int)tab.XXX) + "retVal = false" + lineEnd);

                strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End If", "}").ToString());
                dateExists = true;
            }
        }

        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Return", "return").ToString() + " retVal" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End Function", "}").ToString());

        strB.AppendLine();
        return strB.ToString();
    }
    private string getSaveChanges(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        char lineEnd;
        if (lang == language.VisualBasic)
        {
            lineEnd = ' ';
            strB.AppendLine(Strings.Space((int)tab.X) + "Protected Sub btnSaveChanges_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveChanges.Click");
            strB.AppendLine(Strings.Space((int)tab.XX) + "If Not validateForm() Then Exit Sub");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Dim my" + pClass.Name.Capitalized + " As " + pClass.NameSpaceVariable.Name + "."
                            + pClass.Name.Capitalized + " = get" + pClass.Name.Capitalized + "FromQueryString()");
        }
        else
        {
            lineEnd = ';';
            strB.AppendLine(Strings.Space((int)tab.X) + "protected void btnSaveChanges_Click(object sender, System.EventArgs e)");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "if (!validateForm()) return;");
            strB.AppendLine(Strings.Space((int)tab.XX) + pClass.NameSpaceVariable.Name + "." + pClass.Name.Capitalized + " my" + pClass.Name.Capitalized
                            + " = get" + pClass.Name.Capitalized + "FromQueryString();");
        }
        bool doubleExists = false;
        bool integerExists = false;
        bool dateExists = false;
        foreach (ClassVariable classVar in pClass.ClassVariables)
        {
            if (!classVar.DisplayOnEditPage || !classVar.IsDatabaseBound)
                continue;
            if (classVar.IsCheckBox)
                strB.AppendLine(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized + "." + classVar.Name + " = " + classVar.DefaultHTMLName + ".Checked" + lineEnd);
            else if (classVar.IsTextBox)
            {
                if (classVar.IsDouble)
                {
                    if (!doubleExists)
                    {
                        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Dim tempDouble As Double", "double tempDouble;").ToString());
                        doubleExists = true;
                    }
                    strB.AppendLine(Strings.Space((int)tab.XX) + "Double.TryParse(" + classVar.DefaultHTMLName + ".Text, tempDouble)" + lineEnd);
                    strB.AppendLine(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized + "." + classVar.Name + " = tempDouble" + lineEnd);
                }
                else if (classVar.IsInteger)
                {
                    if (!integerExists)
                    {
                        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Dim tempInteger As Integer", "int tempInteger;").ToString());
                        integerExists = true;
                    }
                    strB.Append(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Integer", "int").ToString());
                    strB.AppendLine(".TryParse(" + classVar.DefaultHTMLName + ".Text, tempInteger)" + lineEnd);
                    strB.AppendLine(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized + "." + classVar.Name + " = tempInteger" + lineEnd);
                }
                else if (classVar.ParameterType.IsNameAlias)
                    strB.AppendLine(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized + "." + classVar.Name + ".TextUnFormatted = txt" + classVar.Name + ".Text" + lineEnd);
                else
                    strB.AppendLine(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized + "." + classVar.Name + " = txt" + classVar.Name + ".Text" + lineEnd);
            }
            else if (classVar.IsDropDownList)
            {
                strB.Append(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized);
                if (classVar.ParameterType.AssociatedProjectClass != null)
                    strB.Append("." + classVar.ParameterType.AssociatedProjectClass.NameForKeyAlias.Capitalized);
                strB.AppendLine(" = " + IIf(lang == language.VisualBasic, cg.getConvertFunction("Integer", lang), "int.Parse").ToString()
                                + "(" + classVar.DefaultHTMLName + ".SelectedValue)" + lineEnd);
            }
            else if (classVar.IsDate)
            {
                if (!dateExists)
                    strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Dim tempDateTime As DateTime", "DateTime tempDateTime;").ToString());
                strB.AppendLine(Strings.Space((int)tab.XX) + "DateTime.TryParse(" + classVar.GetMonthTextControlName() + ".Text.Trim() & \"/\" & " + classVar.getDayTextControlName() + ".Text.Trim() & \"/\" & " + classVar.getYearTextControlName() + ".Text.Trim(), tempDateTime)" + lineEnd);
                strB.AppendLine(Strings.Space((int)tab.XX) + "my" + pClass.Name.Capitalized + "." + classVar.Name + " = tempDateTime" + lineEnd);
                dateExists = true;
            }
        }
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "If my" + pClass.Name.Capitalized + ".ID = -1 Then", "if (my" + pClass.Name.Capitalized + ".ID == -1)").ToString());
        if (lang == language.CSharp)
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "add" + pClass.Name.Capitalized + "(my" + pClass.Name.Capitalized + ")" + lineEnd);
        if (lang == language.CSharp)
            strB.Append("}");
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Else", "else").ToString());
        if (lang == language.CSharp)
            strB.Append("{");
        strB.AppendLine(Strings.Space((int)tab.XXX) + "update" + pClass.Name.Capitalized + "(my" + pClass.Name.Capitalized + ")" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End If", "}").ToString());
        strB.AppendLine(Strings.Space((int)tab.XX) + "Redirect(\"" + pClass.Name.PluralAndCapitalized + ".aspx\")" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End Sub", "}").ToString());

        strB.AppendLine();

        return strB.ToString();
    }
    private string getAddObject(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Function add" + pClass.Name.Capitalized + "(ByVal my" + pClass.Name.Capitalized + " As " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + ") As Boolean");
            strB.AppendLine(Strings.Space((int)tab.XX) + "If my" + pClass.Name.Capitalized + ".dbAdd() > 0 Then");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Add))");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Add))");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Function");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "private bool add" + pClass.Name.Capitalized + "(" + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + " my" + pClass.Name.Capitalized + ")");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "if (my" + pClass.Name.Capitalized + ".dbAdd() > 0)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Add));");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Add));");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
        }
        return strB.ToString();
    }
    private string getUpdateObject(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Function update" + pClass.Name.Capitalized + "(ByVal my" + pClass.Name.Capitalized + " As " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + ") As Boolean");
            strB.AppendLine(Strings.Space((int)tab.XX) + "If my" + pClass.Name.Capitalized + ".dbUpdate() > 0 Then");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Edit))");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Else");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Edit))");
            strB.AppendLine(Strings.Space((int)tab.XX) + "End If");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Function");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "private bool update" + pClass.Name.Capitalized + "(" + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + " my" + pClass.Name.Capitalized + ")");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "if (my" + pClass.Name.Capitalized + ".dbUpdate() > 0)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addSuccess(StringToolkit.getDatabaseSuccessString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Edit));");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.XX) + "else");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "SessionVariables.addError(StringToolkit.getDatabaseErrorString(AliasGroup." + pClass.Name.Capitalized + ", AliasGroup.Edit));");
            strB.AppendLine(Strings.Space((int)tab.XX) + "}");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
        }
        return strB.ToString();
    }

    public string getEditCodeBehind(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        strB.Append(cg.getPageImports(lang));
        strB.Append(cg.getClassDeclaration(lang, "_Edit" + pClass.Name.Capitalized, tab.None, "BasePage"));
        strB.AppendLine();
        strB.AppendLine(getPageLoadForEdit(pClass, lang));
        strB.AppendLine(getPageInstructions(lang));
        strB.AppendLine(getGetFunctionForQueryString(pClass, lang));
        strB.AppendLine(getFillFormForEdit(pClass, lang));
        strB.AppendLine(getCancelForEdit(pClass, lang));
        strB.AppendLine(getValidateFunction(pClass, lang));
        strB.AppendLine(getSaveChanges(pClass, lang));
        strB.AppendLine(getAddObject(pClass, lang));
        strB.AppendLine(getUpdateObject(pClass, lang));
        strB.AppendLine(IIf(lang == language.VisualBasic, "End Class", "}").ToString());
        return strB.ToString();
    }
    public string getViewCodeBehind(ProjectClass pClass, language lang)
    {
        StringBuilder strB = new StringBuilder();
        char lineEnd;
        char conCat;
        strB.Append(cg.getPageImports(lang));
        strB.Append(cg.getClassDeclaration(lang, "_" + pClass.Name.PluralAndCapitalized, tab.None, "BasePage"));
        // Page Load
        if (lang == language.VisualBasic)
        {
            lineEnd = ' ';
            conCat = '&';
            strB.AppendLine(Strings.Space((int)tab.X) + "Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load");
        }
        else
        {
            lineEnd = ';';
            conCat = '+';
            strB.AppendLine(Strings.Space((int)tab.X) + "protected void Page_Load(object sender, System.EventArgs e)");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
        }
        strB.AppendLine(Strings.Space((int)tab.XX) + "fill" + pClass.Name.PluralAndCapitalized + "Table()" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + "fillFieldsFromSiteConfig()" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + "fillPageInstructions()" + lineEnd);

        strB.AppendLine(Strings.Space((int)tab.X) + IIf(lang == language.VisualBasic, "End Sub", "}").ToString());

        strB.AppendLine(Strings.Space((int)tab.X) + getPageInstructions(lang));
        // fillTable
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Sub fill" + pClass.Name.PluralAndCapitalized + "Table()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Dim alternateRow As Boolean = False");
            strB.AppendLine(Strings.Space((int)tab.XX) + "Dim listOfObjects As List(Of " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized
                            + ") = " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.DALClassVariable.Name + ".Get" + pClass.Name.PluralAndCapitalized + "()");
            strB.AppendLine(Strings.Space((int)tab.XX) + "For Each myObject As " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + " In listOfObjects");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim tRow As New TableRow");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "If alternateRow Then tRow.CssClass=\"other\"");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "alternateRow = Not alternateRow");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "private void fill" + pClass.Name.PluralAndCapitalized + "Table()");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.XX) + "bool alternateRow = false;");
            strB.AppendLine(Strings.Space((int)tab.XX) + "List<" + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized
                            + "> listOfObjects = " + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.DALClassVariable.Name
                            + ".Get" + pClass.Name.PluralAndCapitalized + "();");
            strB.AppendLine(Strings.Space((int)tab.XX) + "foreach (" + pClass.NameSpaceVariable.NameBasedOnID + "." + pClass.Name.Capitalized + " myObject in listOfObjects)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "TableRow tRow = new TableRow();");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "if (alternateRow) tRow.CssClass=\"other\";");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "alternateRow = !alternateRow;");
        }

        int countOfColumns = 0;
        foreach (ClassVariable classVar in pClass.ClassVariables)
        {
            if (!classVar.DisplayOnViewPage)
                continue;
            countOfColumns += 1;
            strB.Append(Strings.Space((int)tab.XXX) + "tRow.Controls.Add(TableToolkit.getTableCell(myObject.");
            if (classVar.ParameterType.IsNameAlias)
                strB.Append(classVar.Name + ".ToString()");
            else if (classVar.ParameterType.IsPrimitive)
                strB.Append(classVar.Name);
            else
                strB.Append(ClassGenerator.getSystemUniqueName(classVar.Name));
            if (classVar.IsDouble || classVar.IsInteger || classVar.IsDropDownList || classVar.IsDate || classVar.IsCheckBox)
                strB.Append(".ToString()");
            strB.AppendLine("))" + lineEnd);
        }
        strB.AppendLine(Strings.Space((int)tab.XXX) + "tRow.Controls.Add(TableToolkit.getHyperlinkCell(AliasGroup.Edit.Capitalized, \"Edit"
                        + pClass.Name.Capitalized + ".aspx?id=\" " + conCat + " myObject.ID.ToString()))" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XXX) + "tbl" + pClass.Name.PluralAndCapitalized + ".Rows.Add(tRow)" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "Next", "}").ToString());
        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.XX) + "If listOfObjects.Count = 0 Then");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "Dim tRow As New TableRow");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.XX) + "if (listOfObjects.Count == 0)");
            strB.AppendLine(Strings.Space((int)tab.XX) + "{");
            strB.AppendLine(Strings.Space((int)tab.XXX) + "TableRow tRow = new TableRow();");
        }
        strB.AppendLine(Strings.Space((int)tab.XXX) + "tRow.Controls.Add(TableToolkit.getNoResultsFoundCell(AliasGroup." + pClass.Name.Capitalized
                        + ", " + countOfColumns + "))" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XXX) + "tbl" + pClass.Name.PluralAndCapitalized + ".Rows.Add(tRow)" + lineEnd);
        strB.AppendLine(Strings.Space((int)tab.XX) + IIf(lang == language.VisualBasic, "End If", "}").ToString());
        strB.AppendLine(Strings.Space((int)tab.X) + IIf(lang == language.VisualBasic, "End Sub", "}").ToString());

        if (lang == language.VisualBasic)
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "Private Sub fillFieldsFromSiteConfig()");
            strB.AppendLine(Strings.Space((int)tab.X) + "'TODO: Fill Site Variables on " + pClass.Name.PluralAndCapitalized + ".aspx");
            strB.AppendLine(Strings.Space((int)tab.X) + "hypAdd" + pClass.Name.Capitalized + ".Text = \"Add \" & AliasGroup." + pClass.Name.Capitalized + ".Capitalized");
            strB.AppendLine(Strings.Space((int)tab.X) + "End Sub");
        }
        else
        {
            strB.AppendLine(Strings.Space((int)tab.X) + "private void fillFieldsFromSiteConfig()");
            strB.AppendLine(Strings.Space((int)tab.X) + "{");
            strB.AppendLine(Strings.Space((int)tab.X) + "//TODO: Fill Site Variables on " + pClass.Name.PluralAndCapitalized + ".aspx");
            strB.AppendLine(Strings.Space((int)tab.X) + "hypAdd" + pClass.Name.Capitalized + ".Text = \"Add \" & AliasGroup." + pClass.Name.Capitalized + ".Capitalized;");
            strB.AppendLine(Strings.Space((int)tab.X) + "}");
        }

        strB.AppendLine(Strings.Space((int)tab.X) + IIf(lang == language.VisualBasic, "End Class", "}").ToString());

        return strB.ToString();
    }
    public string getViewForm(ProjectClass pClass, bool useLists, language lang)
    {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(getHeaderLine(pClass, lang, pageVersion.View));
        strB.AppendLine(generateContentHeaders(pClass, false, useLists, lang));
        return strB.ToString();
    }
    public string getViewBody(ProjectClass pClass)
    {
        StringBuilder strB = new StringBuilder();
        strB.AppendLine(Strings.Space((int)tab.X) + "<asp:Hyperlink ID=\"hypAdd" + pClass.Name.Capitalized + "\" runat=\"server\" NavigateUrl=\"Edit"
                        + pClass.Name.Capitalized + ".aspx?id=-1\"></asp:Hyperlink>");
        strB.AppendLine(Strings.Space((int)tab.X) + "<asp:Table ID=\"tbl" + pClass.Name.PluralAndCapitalized + "\" runat=\"server\" CssClass=\"list\">");
        strB.AppendLine(Strings.Space((int)tab.XX) + "<asp:TableHeaderRow>");
        foreach (ClassVariable classVar in pClass.ClassVariables)
        {
            if (!classVar.DisplayOnViewPage)
                continue;
            strB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:TableHeaderCell>" + classVar.Name + "</asp:TableHeaderCell>");
        }
        strB.AppendLine(Strings.Space((int)tab.XX) + "</asp:TableHeaderRow>");
        strB.AppendLine(Strings.Space((int)tab.X) + "</asp:Table>");
        return strB.ToString();
    }
    private enum pageVersion
    {
        Edit,
        View
    }
    private string getHeaderLine(ProjectClass pClass, language lang, pageVersion pVersion)
    {
        string codeExt = "vb";
        string codeVer = "VB";
        if (lang == language.CSharp)
        {
            codeExt = "cs"; codeVer = "C#";
        }
        string pageName = IIf(pVersion == pageVersion.Edit, "Edit" + pClass.Name.Capitalized, pClass.Name.PluralAndCapitalized).ToString();

        return string.Format("<%@ Page Title=\"\" Language=\"{3}\" MasterPageFile=\"~/{0}\" AutoEventWireup=\"false\" CodeFile=\"~/{1}.aspx.{2}"
                                + "\" Inherits=\"_{1}\" %>", pClass.MasterPage.FileName, pageName, codeExt, codeVer);
    }
    // Private Function getViewHeader(ByVal pClass As ProjectClass, lang As language) As String
    // Dim codeExt As String = "vb"
    // If lang = language.CSharp Then codeExt = "cs"
    // Dim headerValue As String = "<%@ Page Title="""" Language=""VB"" MasterPageFile=""~/" & _
    // pClass.MasterPage.FileName & """ AutoEventWireup=""false"" CodeFile=""~/" & pClass.Name.PluralAndCapitalized & ".aspx." & codeExt _
    // & """ Inherits=""" & pClass.Name.PluralAndCapitalized & """ %>" & vbCrLf
    // Return headerValue
    // End Function
    private string generateEditBody(ProjectClass pClass, bool useLists, language lang)
    {
        string formTag = Interaction.IIf(useLists, "ul", "div").ToString();
        string rowTag = Interaction.IIf(useLists, "li", "div").ToString();
        string rowBtnOpenTag = Interaction.IIf(useLists, "", "<div>").ToString();
        string rowBtnCloseTag = Interaction.IIf(useLists, "", "</div>").ToString();
        StringBuilder retStrB = new StringBuilder();
        retStrB.AppendLine("<" + formTag + " class=\"form\">");
        foreach (ClassVariable classVar in pClass.ClassVariables)
        {
            if (!classVar.DisplayOnEditPage)
                continue;
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "<" + rowTag + ">");
            retStrB.Append(Strings.Space((int)tab.XXX) + "<asp:Label ID=\"lbl" + IIf(classVar.Name.ToLower().CompareTo("subtitle") == 0, pClass.Name, "").ToString()
                               + classVar.Name + "\" runat=\"server\" AssociatedControlID=\"");
            if (classVar.IsInteger || classVar.IsDouble)
            {
                // Small textBox: class= number
                retStrB.AppendLine(classVar.DefaultHTMLName + "\">" + classVar.Name + "</asp:Label>");
                retStrB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:TextBox ID=\"" + classVar.DefaultHTMLName + "\" runat=\"server\" CssClass=\"number\"></asp:TextBox>");
            }
            else if (classVar.IsCheckBox)
            {
                // CheckBox
                retStrB.AppendLine(classVar.DefaultHTMLName + "\">" + classVar.Name + "</asp:Label>");
                retStrB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:CheckBox ID=\"" + classVar.DefaultHTMLName + "\" runat=\"server\"></asp:CheckBox>");
            }
            else if (classVar.IsDate)
            {
                // three textboxes (day,month,year)
                retStrB.AppendLine(classVar.GetMonthTextControlName() + "\">" + classVar.Name + "</asp:Label>");
                retStrB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:Panel ID=\"pnl" + classVar.Name + "Date\" runat=\"server\">");
                retStrB.AppendLine(Strings.Space((int)tab.XXXX) + "<asp:TextBox ID=\"" + classVar.GetMonthTextControlName()
                                   + "\" runat=\"server\" CssClass=\"number\"></asp:TextBox>");
                retStrB.AppendLine(Strings.Space((int)tab.XXXX) + "<asp:TextBox ID=\"" + classVar.getDayTextControlName()
                                   + "\" runat=\"server\" CssClass=\"number\"></asp:TextBox>");
                retStrB.AppendLine(Strings.Space((int)tab.XXXX) + "<asp:TextBox ID=\"" + classVar.getYearTextControlName()
                                   + "\" runat=\"server\" CssClass=\"number\"></asp:TextBox>");
                retStrB.AppendLine(Strings.Space((int)tab.XXX) + "</asp:Panel>");
            }
            else if (classVar.IsTextBox)
            {
                if (classVar.ParameterType.Name.ToLower == "namealias")
                {
                    retStrB.AppendLine("txt" + classVar.Name + "\">" + classVar.Name + "</asp:Label>");
                    retStrB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:TextBox ID=\"txt" + classVar.Name + "\" runat=\"server\"></asp:TextBox>");
                }
                else
                {
                    // textbox
                    if (classVar.DefaultHTMLName.CompareTo("lblsubtitle") == 0)
                        retStrB.AppendLine(ClassGenerator.getSystemUniqueName(classVar.DefaultHTMLName) + "\">" + classVar.Name + "</asp:Label>");
                    else
                        retStrB.AppendLine(classVar.DefaultHTMLName + "\">" + classVar.Name + "</asp:Label>");
                    retStrB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:TextBox ID=\"" + classVar.DefaultHTMLName + "\" runat=\"server\"></asp:TextBox>");
                }
            }
            else
            {
                // dropdownlist
                NameAlias myAlias = new NameAlias(classVar.Name);
                retStrB.AppendLine(classVar.DefaultHTMLName + "\">" + myAlias.PluralAndCapitalized + "</asp:Label>");
                retStrB.AppendLine(Strings.Space((int)tab.XXX) + "<asp:DropDownList ID=\"" + classVar.DefaultHTMLName + "\" runat=\"server\"></asp:DropDownList>");
            }
            retStrB.AppendLine(Strings.Space((int)tab.XX) + "</" + rowTag + ">");
        }
        retStrB.AppendLine(Strings.Space((int)tab.XX) + "<" + rowTag + " class=\"buttons\">");
        retStrB.AppendLine(Strings.Space((int)tab.XXX) + rowBtnOpenTag + "<asp:Button ID=\"btnSaveChanges\" runat=\"server\" Text=\"Save Changes\" "
                           + IIf(lang == language.CSharp, "OnClick=\"btnSaveChanges_Click\" ", "").ToString() + "/>" + rowBtnCloseTag);
        retStrB.AppendLine(Strings.Space((int)tab.XXX) + rowBtnOpenTag + "<asp:Button ID=\"btnCancel\" runat=\"server\" Text=\"Cancel\" "
                           + IIf(lang == language.CSharp, "OnClick=\"btnCancel_Click\" ", "").ToString() + "/>" + rowBtnCloseTag);
        retStrB.AppendLine(Strings.Space((int)tab.XX) + "</" + rowTag + ">");
        retStrB.AppendLine(Strings.Space((int)tab.X) + "</" + formTag + ">");
        return retStrB.ToString();
    }

    private string generateContentHeaders(ProjectClass pClass, bool isEditForm, bool useLists, language lang)
    {
        System.Text.StringBuilder retStr = new System.Text.StringBuilder();
        retStr.AppendLine("<asp:Content ID=\"ct" + pClass.MasterPage.TitleName + "\" ContentPlaceHolderID=\"cph"
                          + pClass.MasterPage.TitleName + "\" Runat=\"Server\">");
        retStr.AppendLine(Strings.Space((int)tab.X) + "<asp:Literal ID=\"lit" + pClass.MasterPage.TitleName + "\" runat=\"server\"></asp:Literal>");
        retStr.AppendLine("</asp:Content>");
        retStr.AppendLine();
        retStr.AppendLine("<asp:Content ID=\"ct" + pClass.MasterPage.SubTitleName + "\" ContentPlaceHolderID=\"cph"
                          + pClass.MasterPage.SubTitleName + "\" Runat=\"Server\">");
        retStr.AppendLine(Strings.Space((int)tab.X) + "<asp:Label ID=\"lbl" + pClass.MasterPage.SubTitleName + "\" runat=\"server\"></asp:Label>");
        retStr.AppendLine("</asp:Content>");
        retStr.AppendLine();
        retStr.AppendLine("<asp:Content ID=\"ct" + pClass.MasterPage.PageInstructionsName + "\" ContentPlaceHolderID=\"cph"
                      + pClass.MasterPage.PageInstructionsName + "\" Runat=\"Server\">");
        retStr.AppendLine(Strings.Space((int)tab.X) + "<asp:Label ID=\"lbl" + pClass.MasterPage.PageInstructionsName + "\" runat=\"server\"></asp:Label>");
        retStr.AppendLine("</asp:Content>");
        retStr.AppendLine();

        retStr.AppendLine("<asp:Content ID=\"ct" + pClass.MasterPage.BodyName + "\" ContentPlaceHolderID=\"cph"
                          + pClass.MasterPage.BodyName + "\" Runat=\"Server\">");
        if (isEditForm)
            retStr.AppendLine(generateEditBody(pClass, useLists, lang));
        else
            retStr.AppendLine(getViewBody(pClass));
        retStr.AppendLine("</asp:Content>");
        return retStr.ToString();
    }
}
