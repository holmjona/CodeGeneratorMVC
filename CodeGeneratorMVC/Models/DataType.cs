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

[Serializable()]
public class DataType {
    private int _ID;
    private string _Name;
    private bool _IsPrimitive;
    private ProjectVariable _NameSpaceObject;
    private ProjectClass _AssociatedClass;
    public int ID {
        get {
            return _ID;
        }
        set {
            _ID = value;
        }
    }
    public string Name(CodeGeneration.Language lang = CodeGeneration.Language.VisualBasic) {

        if (_IsPrimitive || _AssociatedClass == null) {
            return CodeGeneration.GetByLanguage(lang, _Name, StaticVariables.Instance.getCSharpTypeName(_Name));
        } else if (_AssociatedClass != null) {
            return _AssociatedClass.Name.Capitalized();
        } else {
            return _Name;
        }

    }
    public bool IsPrimitive {
        get {
            return _IsPrimitive;
        }
        set {
        }
    }
    public bool IsImage {
        get {
            return Name(CodeGeneration.Language.VisualBasic).ToLower().CompareTo("image") == 0;
        }
    }
    // Public Property NameSpaceObject() As ProjectVariable
    // Get
    // Return _NameSpaceObject
    // End Get
    // Set(ByVal value As ProjectVariable)
    // _NameSpaceObject = value
    // End Set
    // End Property
    public bool IsNameAlias {
        get {
            return Name(CodeGeneration.Language.VisualBasic).ToLower().CompareTo("namealias") == 0;
        }
    }
    public ProjectClass AssociatedProjectClass {
        get {
            return _AssociatedClass;
        }
        set {
            _AssociatedClass = value;
        }
    }

    public DataType(int DataTypeID, string DataTypeName, bool IsDataTypePrimitive, ProjectClass pClass) {
        _ID = DataTypeID;
        _Name = DataTypeName;
        _IsPrimitive = IsDataTypePrimitive;
        _AssociatedClass = pClass;
    }
    public DataType Self {
        get {
            return this;
        }
    }
    [Obsolete()]
    //public override string ToString() {
    //    return Name(CodeGeneration.Language.VisualBasic);
    //}
    public new string ToString(CodeGeneration.Language lang = CodeGeneration.Language.VisualBasic) {
        return Name(lang);
    }
}
