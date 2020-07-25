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
using Words;
using System.ComponentModel;

[Serializable()]
public class ClassVariable  {
    private int _ID;
    private string _name;
    private DataType _parameterType;
    private bool _IsIDField = false;
    private bool _IsForeignkey = false; // True if the field was added as a foreign key
    private bool _IsAssociative = false; // True if the field was added only because of an associate entity relation
    private bool _IsList = false;
    // Private _NeedsAttention As Boolean = False
    private bool _displayOnViewPage;
    private bool _displayOnEditPage;
    private bool _IsPropertyInherited;
    // Variables generated from chosen values
    private string _defaultHTMLName;
    private bool _IsTextBox;
    private bool _IsDropDownList;
    private bool _IsCheckBox;
    private bool _IsDate;
    private bool _IsInteger;
    private bool _IsDouble;
    private bool _IsRequired = false; // True if the field cannot be null in the database
    private bool _IsPropertyXMLIgnored = false;
    private ProjectClass _ParentClass;
    private bool _IsDatabaseBound = false;
    private int _LengthOfDatabaseProperty = -1;
    private string _DatabaseType = "";
    private string _DatabaseColumnName = "";
    private ClassVariable() {
    }
    public ClassVariable(ProjectClass pClass, string Name, DataType parameterType, bool isForeignKeyField, bool isAssociativeField, bool list, bool isID, bool PropertyInherited, bool DisplayOnEdit, bool DisplayOnView, int NewID, bool IsClassVariableDatabaseBound, bool isRequired, string ClassVariableDatabaseType, int ClassVariableLengthOfDatabaseProperty, string DatabaseColumnName) : base() {
        _ParentClass = pClass;
        _ID = NewID;
        _IsIDField = isID;
        _IsList = list;
        _name = Name;
        _parameterType = parameterType;
        _IsForeignkey = isForeignKeyField;
        _IsAssociative = isAssociativeField;
        _IsPropertyInherited = PropertyInherited;
        _displayOnEditPage = DisplayOnEdit;
        _displayOnViewPage = DisplayOnView;
        _IsDatabaseBound = IsClassVariableDatabaseBound;
        _DatabaseType = ClassVariableDatabaseType;
        _LengthOfDatabaseProperty = ClassVariableLengthOfDatabaseProperty;
        _DatabaseColumnName = DatabaseColumnName;
        _IsRequired = isRequired;
        setParameters();
    }
    public string DatabaseColumnName {
        get {
            return _DatabaseColumnName;
        }
        set {
            _DatabaseColumnName = value;
        }
    }
    public bool IsDatabaseBound {
        get {
            return _IsDatabaseBound;
        }
        set {
            _IsDatabaseBound = value;
        }
    }
    // Public Property NeedsAttention() As Boolean
    // Get
    // Return _NeedsAttention
    // End Get
    // Set(ByVal value As Boolean)
    // _NeedsAttention = value
    // End Set
    // End Property
    public ProjectClass ParentClass {
        get {
            return _ParentClass;
        }
        set {
            _ParentClass = value;
        }
    }
    private void setParameters() {
        _defaultHTMLName = "";
        _IsTextBox = false;
        _IsDropDownList = false;
        _IsCheckBox = false;
        _IsDate = false;
        _IsInteger = false;
        _IsDouble = false;
        if (_parameterType != null) {
            switch (_parameterType.Name().ToLower()) {
                case "string": {
                        // textbox
                        // _defaultHTMLName = "txt" & Name
                        _defaultHTMLName = getControlNameControl("txt", Name);
                        _IsTextBox = true;
                        break;
                    }

                case "integer":
                case "int16":
                case "int64":
                case "byte": {
                        _defaultHTMLName = getControlNameControl("txt", Name);
                        _IsTextBox = true;
                        _IsInteger = true;
                        break;
                    }

                case "double": {
                        _defaultHTMLName = getControlNameControl("txt", Name);
                        _IsTextBox = true;
                        _IsDouble = true;
                        break;
                    }

                case "boolean": {
                        _defaultHTMLName = getControlNameControl("chk", Name);
                        _IsCheckBox = true;
                        break;
                    }

                case "datetime":
                case "date": {
                        _IsDate = true;
                        break;
                    }

                case "namealias": {
                        _IsTextBox = true;
                        _defaultHTMLName = getControlNameControl("txt", Name);
                        break;
                    }

                default: {
                        // dropdownlist
                        _IsDropDownList = true;
                        NameAlias myAlias = new NameAlias(Name);
                        // _defaultHTMLName = "ddl" & myAlias.PluralAndCapitalized
                        _defaultHTMLName = getControlNameControl("ddl", myAlias.PluralAndCapitalized);
                        _IsPropertyXMLIgnored = true;
                        break;
                    }
            }
        }
    }
    public string getControlNameControl(string controlPrefix, string controlIdentifier) {
        return controlPrefix + controlIdentifier;
    }
    public string getDayTextControlName() {
        return getControlNameControl("txt" + this.Name, "Day");
    }
    public string GetMonthTextControlName() {
        return getControlNameControl("txt" + this.Name, "Month");
    }
    public string getYearTextControlName() {
        return getControlNameControl("txt" + this.Name, "Year");
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
    public string Name {
        get {
            return _name;
        }
        set {
            _name = value;
            setParameters();
            this.NotifyPropertyChanged("Name");
        }
    }
    public DataType ParameterType {
        get {
            return _parameterType;
        }
        set {
            _parameterType = value;
            setParameters();
            this.NotifyPropertyChanged("ParameterType");
        }
    }
    public bool IsForeignKey {
        get {
            return _IsForeignkey;
        }
        set {
            _IsForeignkey = value;
            this.NotifyPropertyChanged("IsForeignKey");
        }
    }
    public bool IsRequired {
        get {
            return _IsRequired;
        }
        set {
            _IsRequired = value;
            this.NotifyPropertyChanged("IsRequired");
        }
    }
    public bool IsAssociative {
        get {
            return _IsAssociative;
        }
        set {
            _IsAssociative = value;
            this.NotifyPropertyChanged("IsAssociative");
        }
    }
    public bool IsList {
        get {
            return _IsList;
        }
        set {
            _IsList = value;
            this.NotifyPropertyChanged("IsList");
        }
    }
    public bool IsIDField {
        get {
            return _IsIDField;
        }
        set {
            _IsIDField = value;
            this.NotifyPropertyChanged("IsIDField");
        }
    }
    public string DefaultHTMLName {
        get {
            return _defaultHTMLName;
        }
        set {
            _defaultHTMLName = value;
            this.NotifyPropertyChanged("DefaultHTMLName");
        }
    }
    public bool IsTextBox {
        get {
            return _IsTextBox;
        }
        set {
            _IsTextBox = value;
            this.NotifyPropertyChanged("IsTextBox");
        }
    }
    public bool IsDropDownList {
        get {
            return _IsDropDownList;
        }
        set {
            _IsDropDownList = value;
            this.NotifyPropertyChanged("IsDropDownList");
        }
    }
    public bool IsCheckBox {
        get {
            return _IsCheckBox;
        }
        set {
            _IsCheckBox = value;
            this.NotifyPropertyChanged("IsCheckBox");
        }
    }
    public bool IsDate {
        get {
            return _IsDate;
        }
        set {
            _IsDate = value;
            this.NotifyPropertyChanged("IsDate");
        }
    }
    public bool IsInteger {
        get {
            return _IsInteger;
        }
        set {
            _IsInteger = value;
            this.NotifyPropertyChanged("IsInteger");
        }
    }
    public bool IsDouble {
        get {
            return _IsDouble;
        }
        set {
            _IsDouble = value;
            this.NotifyPropertyChanged("IsDouble");
        }
    }
    public bool DisplayOnViewPage {
        get {
            return _displayOnViewPage;
        }
        set {
            _displayOnViewPage = value;
            this.NotifyPropertyChanged("DisplayOnViewPage");
        }
    }
    public bool DisplayOnEditPage {
        get {
            return _displayOnEditPage;
        }
        set {
            _displayOnEditPage = value;
            this.NotifyPropertyChanged("DisplayOnEditPage");
        }
    }
    public bool IsPropertyInherited {
        get {
            return _IsPropertyInherited;
        }
        set {
            _IsPropertyInherited = value;
            this.NotifyPropertyChanged("IsPropertyInherited");
        }
    }
    public bool IsPropertyXMLIgnored {
        get {
            return _IsPropertyXMLIgnored;
        }
        set {
            _IsPropertyXMLIgnored = value;
            this.NotifyPropertyChanged("IsPropertyXMLIgnored");
        }
    }
    public int LengthOfDatabaseProperty {
        get {
            return _LengthOfDatabaseProperty;
        }
        set {
            _LengthOfDatabaseProperty = value;
        }
    }
    public string DatabaseType {
        get {
            return _DatabaseType;
        }
        set {
            _DatabaseType = value;
        }
    }
    public string DatabaseTypeWithLength {
        get {
            return _DatabaseType + (_LengthOfDatabaseProperty > -1? "(" + _LengthOfDatabaseProperty + ")": "");
        }
    }
    public override string ToString() {
        return _name;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public delegate void PropertyChangedEventHandler(object sender, System.ComponentModel.PropertyChangedEventArgs e);

    private void NotifyPropertyChanged(string name) {
        // If PropertyChanged IsNot Nothing Then
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public ClassVariable Self {
        get {
            return this;
        }
    }
    public override bool Equals(object obj) {
        if (obj is ClassVariable)
            return this == (ClassVariable)obj;
        return false;
    }
    public static bool operator ==(ClassVariable pv1, ClassVariable pv2) {
        if (ReferenceEquals(pv1,null) && ReferenceEquals(pv2, null))
            return true;
        if (ReferenceEquals(pv1, null) || ReferenceEquals(pv2, null))
            return false;
        return pv1.ID == pv2.ID;
    }
    public static bool operator !=(ClassVariable pv1, ClassVariable pv2) {
        return !(pv1 == pv2);
    }
    public string getVariableName() {
        if (!CodeGeneration.isRegularDataType(ParameterType.Name()))
            return Name + "ID";
        else
            return Name;
    }
}
