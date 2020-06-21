using System;

[Serializable()]
public class ConnectionString {
    private int _ID;
    private string _Name;
    private string _UserName;
    public ConnectionString() {
    }
    public ConnectionString(int ConnectionID, string ConnectionName, string ConnectionUserName) {
        _ID = ConnectionID;
        _Name = ConnectionName;
        _UserName = ConnectionUserName;
    }
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
    public string UserName {
        get {
            return _UserName;
        }
        set {
            _UserName = value;
        }
    }
    public ConnectionString Self {
        get {
            return this;
        }
    }
    public override string ToString() {
        return _Name;
    }
    public override bool Equals(object obj) {
        if (obj is ConnectionString)
            return this == (ConnectionString)obj;
        return false;
    }
    public static bool operator ==(ConnectionString pv1, ConnectionString pv2) {
        if (pv1 == null & pv2 == null)
            return true;
        if (pv1 == null || pv2 == null)
            return false;
        return pv1.ID == pv2.ID;
    }
    public static bool operator !=(ConnectionString pv1, ConnectionString pv2) {
        return pv1 != pv2;
    }
}
