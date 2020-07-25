using System;

[Serializable()]
public class ProjectVariable {
    private int _ID;
    private string _Name;
    public int ID {
        get {
            return _ID;
        }
        set {
            _ID = value;
        }
    }
    public string Name {
        get {
            return _Name;
        }
        set {
            _Name = value;
        }
    }
    public string NameBasedOnID {
        get {
            if (Name == StaticVariables.Instance.NoAssociatedNameSpaceString)
                return "";
            else
                return _Name;
        }
    }
    public ProjectVariable() {
    }
    public ProjectVariable(int ProjectVariableID, string ProjectVariableName) {
        _ID = ProjectVariableID;
        _Name = ProjectVariableName;
    }
    public override string ToString() {
        return _Name;
    }
    public ProjectVariable Self {
        get {
            return this;
        }
    }

    public override bool Equals(object obj) {
        if (obj is ProjectVariable)
            return ReferenceEquals(this,(ProjectVariable)obj);
        return false;
    }
    public static bool operator ==(ProjectVariable pv1, ProjectVariable pv2) {
        if (ReferenceEquals(pv1,null) & ReferenceEquals(pv2,null))
            return true;
        if (ReferenceEquals(pv1, null) || ReferenceEquals(pv2, null))
            return false;
        return pv1.ID == pv2.ID;
    }
    public static bool operator !=(ProjectVariable pv1, ProjectVariable pv2) {
        return !(pv1 == pv2);
    }
}
