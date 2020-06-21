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
        sb.AppendLine(Strings.Space((int)tab.X)); sb.Append("Inherits Web.UI.Page");
        return sb.ToString();
    }
    private static string getPrivateVariables() {
        StringBuilder sb = new StringBuilder();
        string namespaceString = "";
        if (StaticVariables.Instance.UserClass != null) {
            if (StaticVariables.Instance.UserClass.NameSpaceVariable.ID > 0)
                namespaceString = StaticVariables.Instance.UserClass.NameSpaceVariable.NameBasedOnID + ".";
            sb.Append(Strings.Space(4)); sb.Append("Private _CurrentUser As "); sb.Append(namespaceString); sb.Append(StaticVariables.Instance.UserClass.Name.Capitalized); sb.Append(Constants.vbCrLf);
        }
        if (StaticVariables.Instance.AliasGroupClass != null) {
            if (StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.ID > 0)
                namespaceString = StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.NameBasedOnID + ".";
            sb.Append(Strings.Space(4)); sb.Append("Private _AliasGroup As "); sb.Append(namespaceString); sb.Append(StaticVariables.Instance.AliasGroupClass.Name.Capitalized); sb.Append(Constants.vbCrLf);
        }
        sb.Append(Strings.Space(4)); sb.Append("Protected _ReturnPath As String = Nothing"); sb.Append(Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getPreInit() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space(4)); sb.Append("Protected Overrides Sub OnPreInit(ByVal e As System.EventArgs)"); sb.Append(Constants.vbCrLf);
        if (StaticVariables.Instance.UserClass != null) {
            sb.Append(Strings.Space(8)); sb.Append("_CurrentUser = SessionVariables.CurrentUser"); sb.Append(Constants.vbCrLf);
        }
        if (StaticVariables.Instance.AliasGroupClass != null) {
            sb.Append(Strings.Space(8)); sb.Append("_AliasGroup = SessionVariables.AliasGroup"); sb.Append(Constants.vbCrLf);
        }
        sb.Append(Strings.Space(4)); sb.Append("End Sub"); sb.Append(Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getCurrentUser() {
        StringBuilder sb = new StringBuilder();
        string nameSpaceString = "";
        if (StaticVariables.Instance.UserClass.NameSpaceVariable.ID > 0)
            nameSpaceString = StaticVariables.Instance.UserClass.NameSpaceVariable.NameBasedOnID + ".";
        sb.Append(Strings.Space(4)); sb.Append("Protected ReadOnly Property CurrentUser() As "); sb.Append(nameSpaceString); sb.Append(StaticVariables.Instance.UserClass.Name.Capitalized); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("Get"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("Return _CurrentUser"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("End Get"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(4)); sb.Append("End Property"); sb.Append(Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getCurrentAliasGroup() {
        StringBuilder sb = new StringBuilder();
        string nameSpaceString = "";
        if (StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.ID > 0)
            nameSpaceString = StaticVariables.Instance.AliasGroupClass.NameSpaceVariable.NameBasedOnID + ".";
        sb.Append(Strings.Space(4)); sb.Append("Protected ReadOnly Property AliasGroup() As "); sb.Append(nameSpaceString); sb.Append(StaticVariables.Instance.AliasGroupClass.Name.Capitalized); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("Get"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("Return _AliasGroup"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("End Get"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(4)); sb.Append("End Property"); sb.Append(Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getHandlerPermissions() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space(4)); sb.Append("Protected Sub handlePermissions(ByVal caseToCheck As Boolean)"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("If Not caseToCheck Then"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("Niatec.SessionVariables.addPermissionError()"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("Redirect(\"Login.aspx\")"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("End If"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(4)); sb.Append("End Sub"); sb.Append(Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getRedirect() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space(4)); sb.Append("Protected Sub Redirect(ByVal defaultURL As String)"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("If Request.QueryString(\"redirect\") IsNot Nothing And SessionVariables.History.Count > 1 Then"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("SessionVariables.History.Pop()"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("While SessionVariables.History.Count > 0 And SessionVariables.History.Peek() = Request.Url.ToString()"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(16)); sb.Append("SessionVariables.History.Pop()"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("End While"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("If SessionVariables.History.Count > 0 Then"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(16)); sb.Append("Response.Redirect(SessionVariables.History.Pop)"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("End If"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("End If"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("Response.Redirect(defaultURL)"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(4)); sb.Append("End Sub"); sb.Append(Constants.vbCrLf);
        return sb.ToString();
    }
    private static string getReturnPath() {
        StringBuilder sb = new StringBuilder();
        sb.Append(Strings.Space(4)); sb.Append("Protected Function getReturnPath() As String"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("Dim retStr As String = _ReturnPath"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("If Request.QueryString(\"from\") IsNot Nothing Then"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(12)); sb.Append("retStr = Request.QueryString(\"from\") & \".aspx\""); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("End If"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(8)); sb.Append("Return retStr"); sb.Append(Constants.vbCrLf);
        sb.Append(Strings.Space(4)); sb.Append("End Function"); sb.Append(Constants.vbCrLf);
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
