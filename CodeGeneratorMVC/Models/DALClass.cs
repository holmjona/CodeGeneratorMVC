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
using System.ComponentModel;

[Serializable()]
public class DALClass{ //: INotifyPropertyChanged {
    private ConnectionString _ReadOnlyConnectionString;
    private ConnectionString _EditOnlyConnectionSTring;
    private ProjectVariable _NamespaceName;
    private int _ID;
    private string _Name = "";
    public ConnectionString ReadOnlyConnectionString {
        get {
            return _ReadOnlyConnectionString;
        }
        set {
            _ReadOnlyConnectionString = value;
            this.NotifyPropertyChanged("ReadOnlyConnectionString");
        }
    }
    public ConnectionString EditOnlyConnectionstring {
        get {
            return _EditOnlyConnectionSTring;
        }
        set {
            _EditOnlyConnectionSTring = value;
            this.NotifyPropertyChanged("EditOnlyConnectionString");
        }
    }
    public ProjectVariable NameSpaceName {
        get {
            return _NamespaceName;
        }
        set {
            _NamespaceName = value;
        }
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
    public DALClass Self {
        get {
            return this;
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;

    public delegate void PropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e);

    private void NotifyPropertyChanged(string name) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public override string ToString() {
        return Name;
    }
}
