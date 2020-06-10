using System.Collections.Generic;

public abstract class SessionStateBase {
    //public abstract System.Web.Security.UserBase CurrentUser { get; set; }
    public abstract List<string> ErrorMessages { get; set; }
    public abstract List<string> SuccessMessages { get; set; }
    public abstract void addError(string value);
    public abstract void addSuccess(string value);
    public abstract void addPermissionError();
    public abstract Stack<string> History { get; set; }
    //public abstract System.Web.Security.SiteConfigBase SiteConfig { get; set; }
    public abstract void RemoveItemFromSession(string stringID);
    public abstract string LastPageRequested { get; set; }
    public abstract void save(string name, string value);
    public abstract string load(string name, bool clearValue = false);
}
