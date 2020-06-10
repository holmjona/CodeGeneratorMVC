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
using IRICommonObjects.Words;

[Serializable()]
public class ProjectClass : INotifyPropertyChanged {
    [NonSerialized()]
    private NameAlias _Name;
    private string _NameString;

    private BindingList<ClassVariable> _ClassVariables = new BindingList<ClassVariable>();
    private List<ProjectClass> _AssociatedClasses;
    private int _ID;
    private ProjectVariable _NameSpaceVariable;
    private DALClass _DALClassVariable;
    private string _Summary = "";
    private ProjectVariable _BaseClass;
    private MasterPageClass _MasterPage;
    private ClassVariable _TextProperty;
    private ClassVariable _ValueProperty;
    private bool _IsSelected;
    private bool _ClassObjectIsNotNeeded = false;
    private bool _IsAssociatedWithAliasGroup = false;
    private bool _IsAssociatedWithUser = false;
    private string _DatabaseTableName = "";
    private bool _IsAssociateEntitiy = false;
    private bool _RequriesViewModel;
    public bool RequiresViewModel {
        get {
            return _RequriesViewModel;
        }
        set {
            _RequriesViewModel = value;
        }
    }

    public string OriginalSQLText;
    public string DatabaseTableName {
        get {
            return _DatabaseTableName;
        }
        set {
            _DatabaseTableName = value;
        }
    }
    public bool IsAssociatedWithUser {
        get {
            return _IsAssociatedWithUser;
        }
        set {
            _IsAssociatedWithUser = value;
        }
    }

    public bool IsAssociatedWithAliasGroup {
        get {
            return _IsAssociatedWithAliasGroup;
        }
        set {
            _IsAssociatedWithAliasGroup = value;
        }
    }
    public bool ClassObjectIsNotNeeded {
        get {
            return _ClassObjectIsNotNeeded;
        }
        set {
            _ClassObjectIsNotNeeded = value;
        }
    }
    public bool IsAssociateEntitiy {
        get {
            return _IsAssociateEntitiy;
        }
        set {
            _IsAssociateEntitiy = value;
        }
    }
    public ProjectClass() {
        _ClassVariables.ListChanged += ClassVariables_ListChanges;
    }
    private void ClassVariables_ListChanges(object sender, ListChangedEventArgs e) {
        int index = 1;
        foreach (ClassVariable cv in _ClassVariables) {
            if (cv.ID == 0)
                cv.ID = index;
            index += 1;
        }
    }

    public ProjectVariable BaseClass {
        get {
            return _BaseClass;
        }
        set {
            _BaseClass = value;
            this.NotifyPropertyChanged("BaseClass");
        }
    }
    public MasterPageClass MasterPage {
        get {
            return _MasterPage;
        }
        set {
            _MasterPage = value;
            this.NotifyPropertyChanged("MasterPage");
        }
    }
    public NameAlias Name {
        get {
            if (_Name == null)
                _Name = new NameAlias(_NameString);
            return _Name;
        }
        set {
            _Name = value;
            _NameString = value.Capitalized;
            this.NotifyPropertyChanged("Name");
        }
    }
    public NameAlias NameForKeyAlias {
        get {
            return new NameAlias(_Name.TextUnFormatted + "^i^d^");
        }
    }
    public string NameString {
        get {
            return _NameString;  // "(" & Me.ID.ToString & ") " &
        }
        set {
            _NameString = value;
            _Name = new NameAlias(value, true);
            this.NotifyPropertyChanged("NameString");
        }
    }
    public ProjectVariable NameSpaceVariable {
        get {
            return _NameSpaceVariable;
        }
        set {
            _NameSpaceVariable = value;
            this.NotifyPropertyChanged("NameSpaceVariable");
        }
    }
    public string NameWithNameSpace {
        get {
            return NameSpaceVariable.NameBasedOnID + "." + Name.Capitalized;
        }
    }
    public DALClass DALClassVariable {
        get {
            return _DALClassVariable;
        }
        set {
            _DALClassVariable = value;
            this.NotifyPropertyChanged("DALClassVariable");
        }
    }
    public string Summary {
        get {
            return _Summary;
        }
        set {
            _Summary = value;
            this.NotifyPropertyChanged("Summary");
        }
    }
    public BindingList<ClassVariable> ClassVariables {
        get {
            return _ClassVariables;
        }
        set {
            _ClassVariables = value;
            this.NotifyPropertyChanged("ClassVariables");
        }
    }
    public int ID {
        get {
            return _ID;
        }
        set {
            _ID = value;
            this.NotifyPropertyChanged("ID");
        }
    }
    public bool IsSelected {
        get {
            return _IsSelected;
        }
        set {
            _IsSelected = value;
            if (_IsSelected) {
                if (!StaticVariables.Instance.SelectedProjectClasses.Contains(this))
                    StaticVariables.Instance.SelectedProjectClasses.Add(this);
            } else
                StaticVariables.Instance.SelectedProjectClasses.Remove(this);
            this.NotifyPropertyChanged("IsSelected");
        }
    }

    public ClassVariable TextVariable {
        get {
            return _TextProperty;
        }
        set {
            _TextProperty = value;
            this.NotifyPropertyChanged("TextProperty");
        }
    }
    public ClassVariable ValueVariable {
        get {
            return _ValueProperty;
        }
        set {
            _ValueProperty = value;
            this.NotifyPropertyChanged("ValueProperty");
        }
    }
    public List<ProjectClass> AssociatedClasses {
        get {
            if (_AssociatedClasses == null)
                _AssociatedClasses = new List<ProjectClass>();
            return _AssociatedClasses;
        }
        set {
            _AssociatedClasses = value;
        }
    }

    public override string ToString() {
        return _Name.Capitalized;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public delegate void PropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e);

    private void NotifyPropertyChanged(string name) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public override bool Equals(object obj) {
        if (obj == null | !this.GetType() == obj.GetType())
            return false;
        ProjectClass pc = (ProjectClass)obj;
        return pc.ID == this.ID;
    }
}

public class ProjectClassList : BindingList<ProjectClass> {
}
