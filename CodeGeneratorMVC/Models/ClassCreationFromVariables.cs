public class ClassCreationFromVariables {
    private string getDataType(string privateStr) {
        string retString = privateStr;
        if (retString.IndexOf("As") > 0)
            retString = privateStr.Substring(privateStr.LastIndexOf("As "));
        else if (retString.IndexOf("as") > 0)
            retString = privateStr.Substring(privateStr.LastIndexOf("as "));

        retString = retString.Remove(0, 3);
        retString = retString.Trim();
        return retString;
    }
    private string getName(string privateStr, bool withUnderScore) {
        string retStr = privateStr;
        retStr = retStr.Replace("Private", "");
        retStr = retStr.Replace("private", "");
        retStr = retStr.Replace("Dim", "");
        if (retStr.IndexOf("As") > 0)
            retStr = retStr.Remove(retStr.LastIndexOf("As "));
        else if (retStr.IndexOf("as") > 0)
            retStr = retStr.Remove(retStr.LastIndexOf("as "));

        if (!withUnderScore)
            retStr = retStr.Replace("_", "");
        else
            retStr = retStr.Insert(0, "_");

        retStr = retStr.Trim();

        return retStr;
    }
}
