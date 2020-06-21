using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using cg = CodeGeneration;
using language = CodeGeneration.Language;
using tab = CodeGeneration.Tabs;

public class BasePage {
    private static string getHeader(language lang) {
        StringBuilder sb = new StringBuilder();
        sb.Append(cg.getPageImports(lang, includeWebUI: true));
        sb.AppendLine("Public MustInherit Class BasePage");
        sb.AppendLine(Strings.Space((int)tab.X) + "Inherits Web.UI.Page");
        return sb.ToString();
    }
    private static string getPrivateVariables() {
        StringBuilder sb = new StringBuilder();
        string namespaceString = "";
        if (StaticVariables.Instance.UserClass != null) {
            if (StaticVariables.Instance.UserClass.NameSpaceVariable.ID > 0)
                namespaceString = StaticVariables.Instance.UserClass.NameSpaceVariable.NameBasedOnID + ".";
            sb.Append(Strings.Space((int)tab.X) + "Private _CurrentUser As " + namespaceString + 
                    StaticVariables.Instance.UserClass.Name.Capitalized() + Constants.vbCrLf);
        }
        if (StaticVariables.Instance.AliasGroupClass != null) {
            if (StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.ID > 0)
                namespaceString = StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.NameBasedOnID + ".";
            sb.Append(Strings.Space((int)tab.X) + "Private _AliasGroup As " + namespaceString + 
                StaticVariables.Instance.AliasGroupClass.Name.Capitalized() + Constants.vbCrLf);
        }
        sb.Append(Strings.Space((int)tab.X) + "Protected _ReturnPath As String = Nothing" + Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getPreInit() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space((int)tab.X) + "Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)" + Constants.vbCrLf);
        if (StaticVariables.Instance.UserClass != null) {
            sb.Append(Strings.Space((int)tab.XX) + "_CurrentUser = SessionVariables.CurrentUser" + Constants.vbCrLf);
        }
        if (StaticVariables.Instance.AliasGroupClass != null) {
            sb.Append(Strings.Space((int)tab.XX) + "_AliasGroup = SessionVariables.AliasGroup" + Constants.vbCrLf);
        }
        sb.Append(Strings.Space((int)tab.X) + "End Sub" + Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getCurrentUser() {
        StringBuilder sb = new StringBuilder();
        string nameSpaceString = "";
        if (StaticVariables.Instance.UserClass.NameSpaceVariable.ID > 0)
            nameSpaceString = StaticVariables.Instance.UserClass.NameSpaceVariable.NameBasedOnID + ".";
        sb.Append(Strings.Space((int)tab.X) + "Protected ReadOnly Property CurrentUser() As " + nameSpaceString 
            + StaticVariables.Instance.UserClass.Name.Capitalized() + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "Get" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "Return _CurrentUser" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "End Get" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.X) + "End Property" + Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getCurrentAliasGroup() {
        StringBuilder sb = new StringBuilder();
        string nameSpaceString = "";
        if (StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.ID > 0)
            nameSpaceString = StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.NameBasedOnID + ".";
        sb.Append(Strings.Space((int)tab.X) + "Protected ReadOnly Property AliasGroup() As " + nameSpaceString 
            + StaticVariables.Instance.AliasGroupClass.Name.Capitalized() + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "Get" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "Return _AliasGroup" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "End Get" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.X) + "End Property" + Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getHandlerPermissions() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space((int)tab.X) + "Protected Sub handlePermissions(ByVal caseToCheck As Boolean)" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "If Not caseToCheck Then" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "Niatec.SessionVariables.addPermissionError()" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "Redirect(\"Login.aspx\")" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "End If" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.X) + "End Sub" + Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getRedirect() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space((int)tab.X) + "Protected Sub Redirect(ByVal defaultURL As String)" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "If Request.QueryString(\"redirect\") IsNot Nothing And SessionVariables.History.Count > 1 Then" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "SessionVariables.History.Pop()" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "While SessionVariables.History.Count > 0 And SessionVariables.History.Peek() = Request.Url.ToString()" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXXX) + "SessionVariables.History.Pop()" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "End While" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "If SessionVariables.History.Count > 0 Then" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXXX) + "Response.Redirect(SessionVariables.History.Pop)" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "End If" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "End If" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "Response.Redirect(defaultURL)" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.X) + "End Sub" + Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getReturnPath() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space((int)tab.X) + "Protected Function getReturnPath() As String" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "Dim retStr As String = _ReturnPath" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "If Request.QueryString(\"from\") IsNot Nothing Then" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XXX) + "retStr = Request.QueryString(\"from\") & \".aspx\"" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "End If" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.XX) + "Return retStr" + Constants.vbCrLf);
        sb.Append(Strings.Space((int)tab.X) + "End Function" + Constants.vbCrLf);
        return sb.ToString();
    }
    public static string getBasePage(language lang) {
        StringBuilder sb = new StringBuilder();
        sb.Append(getHeader(lang));
        sb.Append(getPrivateVariables());
        sb.Append(getPreInit());
        sb.Append(getCurrentUser());
        sb.Append(getCurrentAliasGroup());
        sb.Append(getHandlerPermissions());
        sb.Append(getRedirect());
        sb.Append(getReturnPath());
        return sb.ToString();
    }
}
