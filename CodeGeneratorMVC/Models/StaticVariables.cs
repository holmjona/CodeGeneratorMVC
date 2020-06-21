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
using EnvDTE80;
using System.Runtime.Serialization;
using language = CodeGeneration.Language;

[Serializable()]
public class StaticVariables {
    private static StaticVariables myInstance = null;
    private bool _IsProjectOpen = false;
    [NonSerialized()]
    public DTE2 _ApplicationObject;
    private ClassVariable _ClassVariableToEdit;
    private BindingList<ProjectClass> _ListOfProjectClasses = new BindingList<ProjectClass>();
    private ProjectClass _SelectedProjectClass;
    private List<ProjectClass> _SelectedProjectClasses = new List<ProjectClass>();
    private List<string> _DatabaseTypes = new List<string>();
    private string _FileSaveAddress;
    private ProjectClass _AliasGroupClass;
    private ProjectClass _UserClass;
    private BindingList<string> _VBDataTypes;
    private BindingList<MasterPageClass> _MasterPages;
    private MasterPageClass _MasterPage;
    private BindingList<ConnectionString> _ConnectionStrings = new BindingList<ConnectionString>();
    private BindingList<DALClass> _DALs = new BindingList<DALClass>();
    private DALClass _SelectedDAL;
    private BindingList<ProjectVariable> _NameSpaceNames = new BindingList<ProjectVariable>();
    private ProjectVariable _SelectedNameSpaceName;
    private BindingList<ProjectVariable> _BaseClasses = new BindingList<ProjectVariable>();
    private List<ProjectVariable> _SelectedBaseClasses;
    private BindingList<DataType> _DataTypes = new BindingList<DataType>();
    private BindingList<ProjectVariable> _UserNames = new BindingList<ProjectVariable>();
    private Dictionary<string, string> _dictOfKnownFiles = new Dictionary<string, string>() { { "%%User%%", "Security.User" }, { "%%AliasGroup%%", "Security.AliasGroup" }, { "%%DescriptionGroup%%", "Security.DescriptionGroup" }, { "%%SiteConfig%%", "Security.SigeConfig" } };


    // '##################################################################################################
    // '##########################   KEEP THESE TOGETHER    ##############################################
    /// <summary>
    ///     ''' This keeps track of the index of each of these types in the Shared Array.
    ///     ''' </summary>
    ///     ''' <remarks>It is best to add new ones to the end
    ///     ''' Name Aliases are treated as primitive even though they are user defined.
    ///     ''' </remarks>
    public enum DataPrimitives : int {
        _String = 0,
        _Integer = 1,
        _Short = 2,
        _Single = 3,
        _Object = 4,
        _Double = 5,
        _Decimal = 6,
        _Image = 7,
        _ByteArray = 8,
        _Boolean = 9,
        _DateTime = 10,
        _Date = 11,
        _Int16 = 12,
        _Byte = 13,
        _Int64 = 14,
        _NameAlias = 15 // is not primitive, but we treat it like it is
    }
    /// <summary>
    ///     ''' This keeps track of the all of the text names of each known primative data types,
    ///     ''' use the DataPrimatives Enum to store the index.
    ///     ''' </summary>
    ///     ''' <remarks></remarks>
    public static string[] PrimitiveDataTypes = new[] { "String", "Integer", "Short", "Single", "Object", "Double", "Decimal", "Image", "Byte()", "Boolean", "DateTime", "Date", "Int16", "Byte", "Int64", "NameAlias" };

    private static Dictionary<string, string> _CSharpPrimitivesMap = new Dictionary<string, string>() { { "String", "string" }, { "Integer", "int" }, { "Short", "short" }, { "Single", "float" }, { "Object", "object" }, { "Double", "double" }, { "Decimal", "decimal" }, { "Image", "image" }, { "Byte()", "byte[]" }, { "Boolean", "bool" }, { "DateTime", "DateTime" }, { "Date", "DateTime" }, { "Int16", "Int16" }, { "Byte", "byte" }, { "Int64", "Int64" }, { "NameAlias", "NameAlias" } };

    // '#########################   KEEP PREVIOUS TOGETHER     ###########################################
    // '##################################################################################################

    public static string getDataPrimative(DataPrimitives dp, language language) {
        if (language == CodeGeneration.Language.VisualBasic)
            return PrimitiveDataTypes[dp];
        else
            return _CSharpPrimitivesMap[PrimitiveDataTypes[dp]];
    }

    public ProjectClass AliasGroupClass {
        get {
            return _AliasGroupClass;
        }
        set {
            _AliasGroupClass = value;
        }
    }
    public ProjectClass UserClass {
        get {
            return _UserClass;
        }
        set {
            _UserClass = value;
        }
    }

    public string FileSaveAddress {
        get {
            return _FileSaveAddress;
        }
        set {
            _FileSaveAddress = value;
        }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
    }
    private StaticVariables() {
        NameSpaceNames.ListChanged += NameSpace_ListChanged;
        ConnectionStrings.ListChanged += ConnectionStrings_ListChanged;
    }
    private void NameSpace_ListChanged(object sender, ListChangedEventArgs e) {
        int index = 0;
        foreach (ProjectVariable n in NameSpaceNames) {
            n.ID = index;
            index += 1;
        }
    }
    private void ConnectionStrings_ListChanged(object sender, ListChangedEventArgs e) {
        int index = 0;
        foreach (ConnectionString n in ConnectionStrings) {
            n.ID = index;
            index += 1;
        }
    }
    public static StaticVariables Instance {
        get {
            if (myInstance == null)
                myInstance = new StaticVariables();
            return myInstance;
        }
        set {
            myInstance = value;
        }
    }
    public DTE2 ApplicationObject {
        get {
            return _ApplicationObject;
        }
        set {
            _ApplicationObject = value;
        }
    }
    public ClassVariable ClassVariableToEdit {
        get {
            return _ClassVariableToEdit;
        }
        set {
            _ClassVariableToEdit = value;
        }
    }
    public BindingList<ProjectClass> ListOfProjectClasses {
        get {
            return _ListOfProjectClasses;
        }
        set {
            _ListOfProjectClasses = value;
        }
    }
    public int HighestProjectClassID {
        get {
            int highestID = 0;
            foreach (ProjectClass p in ListOfProjectClasses) {
                if (p.ID > highestID)
                    highestID = p.ID;
            }
            return highestID;
        }
    }
    public ProjectClass SelectedProjectClass {
        get {
            return _SelectedProjectClass;
        }
        set {
            _SelectedProjectClass = value;
        }
    }
    public List<ProjectClass> SelectedProjectClasses {
        get {
            return _SelectedProjectClasses;
        }
        set {
            _SelectedProjectClasses = value;
        }
    }
    public BindingList<ProjectVariable> UserNames {
        get {
            return _UserNames;
        }
        set {
            _UserNames = value;
        }
    }
    public BindingList<ConnectionString> ConnectionStrings {
        get {
            if (_ConnectionStrings.Count == 0) {
                _ConnectionStrings.Add(new ConnectionString(0, "EditOnlyConnectionString", "newdatabaseuser"));
                _ConnectionStrings.Add(new ConnectionString(1, "ReadOnlyConnectionString", "newdatabaseuser"));
            }
            return _ConnectionStrings;
        }
        set {
            _ConnectionStrings = value;
        }
    }
    public DALClass SelectedDAL {
        get {
            return _SelectedDAL;
        }
        set {
            _SelectedDAL = value;
        }
    }
    public BindingList<DALClass> DALs {
        get {
            return _DALs;
        }
        set {
            _DALs = value;
        }
    }


    public ProjectVariable SelectedNameSpaceName {
        get {
            return _SelectedNameSpaceName;
        }
        set {
            _SelectedNameSpaceName = value;
        }
    }
    public string NoAssociatedNameSpaceString {
        get {
            return "No associated namespace";
        }
    }
    public BindingList<ProjectVariable> NameSpaceNames {
        get {
            if (_NameSpaceNames.Count == 0)
                _NameSpaceNames.Add(new ProjectVariable(-5, NoAssociatedNameSpaceString));
            return _NameSpaceNames;
        }
        set {
            _NameSpaceNames = value;
        }
    }

    public List<ProjectVariable> SelectedBaseClasses {
        get {
            return _SelectedBaseClasses;
        }
        set {
            _SelectedBaseClasses = value;
        }
    }
    public BindingList<MasterPageClass> MasterPages {
        get {
            if (_MasterPages == null) {
                _MasterPages = new BindingList<MasterPageClass>();
                foreach (MasterPageClass mPage in MasterPageClass.getMasterPagesForProject(ApplicationObject))
                    _MasterPages.Add(mPage);
            }
            return _MasterPages;
        }
        set {
            _MasterPages = value;
        }
    }
    public MasterPageClass SelectedMasterPage {
        get {
            if (_MasterPage == null)
                _MasterPage = new MasterPageClass();
            return _MasterPage;
        }
        set {
            _MasterPage = value;
        }
    }
    public BindingList<DataType> DataTypes {
        get {
            if (_DataTypes.Count < 1) {
                // Add Primitive Types to the Structure.
                foreach (string s in PrimitiveDataTypes)
                    AddPrimitiveDataType(s, true);
            }
            return _DataTypes;
        }
        set {
            _DataTypes = value;
        }
    }
    /// <summary>
    ///     ''' Make sure that an exist project has all primitive types. 
    ///     ''' </summary>
    ///     ''' <remarks>This should only be called on newly opened Projects, because it should only affect older projects.</remarks>
    public void verifyPrimitiveDataTypesExist() {
        foreach (var pt in PrimitiveDataTypes) {
            bool wasFound = false;
            foreach (var dt in DataTypes) {
                if (dt.Name == pt) {
                    wasFound = true;
                    break;
                }
            }
            if (!wasFound)
                AddPrimitiveDataType(pt, true);
        }
    }
    public string getCSharpTypeName(string typeName) {
        // http://www.harding.edu/fmccown/vbnet_csharp_comparison.html
        // If _CSharpPrimitivesMap Is Nothing Then _CSharpPrimitivesMap = New Dictionary(Of String, String)
        // If _CSharpPrimitivesMap.Count < 1 Then
        // _CSharpPrimitivesMap.Add("String", "string")
        // _CSharpPrimitivesMap.Add("Integer", "int")
        // _CSharpPrimitivesMap.Add("Short", "short")
        // _CSharpPrimitivesMap.Add("Single", "float")
        // _CSharpPrimitivesMap.Add("Object", "object")
        // _CSharpPrimitivesMap.Add("Double", "double")
        // _CSharpPrimitivesMap.Add("Decimal", "decimal")
        // _CSharpPrimitivesMap.Add("Image", "image")
        // _CSharpPrimitivesMap.Add("Byte()", "byte[]")
        // _CSharpPrimitivesMap.Add("Boolean", "bool")
        // _CSharpPrimitivesMap.Add("DateTime", "DateTime")
        // _CSharpPrimitivesMap.Add("Date", "DateTime")
        // _CSharpPrimitivesMap.Add("Int16", "Int16")
        // _CSharpPrimitivesMap.Add("Byte", "byte")
        // _CSharpPrimitivesMap.Add("Int64", "Int64")
        // End If
        if (GetDataType(typeName) != null) {
            if (_CSharpPrimitivesMap.ContainsKey(typeName))
                return _CSharpPrimitivesMap[typeName];
        }
        return typeName;
    }
    public string NeededFile(string key) {
        if (_dictOfKnownFiles.ContainsKey(key))
            return _dictOfKnownFiles[key];
        return "Unknown.Type";
    }

    public void addDerivedTypeToSystem(ProjectClass pClass) {
        bool classExists = false;
        // check to see if datatype is already in the list.
        foreach (DataType dType in StaticVariables.Instance.DataTypes) {
            if (dType.Name.ToLower().CompareTo(pClass.Name.Capitalized.ToLower()) == 0) {
                classExists = false;
                return;
            }
        }
        if (!classExists)
            // add new datatype to list.
            StaticVariables.Instance.AddDataType(ref pClass, false);
    }

    public void AddDataType(ref ProjectClass pClass, bool IsDataTypePrimitive) {
        _DataTypes.Add(new DataType(_DataTypes.Count, pClass.Name.Capitalized, IsDataTypePrimitive, pClass));
    }
    public void AddPrimitiveDataType(string DataTypeName, bool IsDataTypePrimitive) {
        _DataTypes.Add(new DataType(_DataTypes.Count, DataTypeName, IsDataTypePrimitive, null));
    }
    public DataType GetDataType(int ID) {
        foreach (DataType dt in DataTypes) {
            if (dt.ID == ID)
                return dt;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }
    public DataType GetDataType(string Name) {
        foreach (DataType dt in DataTypes) {
            if (dt.Name.ToLower().CompareTo(Name.ToLower().Trim()) == 0)
                return dt;
        }
        return null/* TODO Change to default(_) if this is not a reference type */;
    }

    public BindingList<ProjectVariable> BaseClasses {
        get {
            if (_BaseClasses == null)
                _BaseClasses = new BindingList<ProjectVariable>();
            if (_BaseClasses.Count == 0) {
                _BaseClasses.Add(new ProjectVariable(1, "IRICommonObjects.DatabaseRecord"));
                _BaseClasses.Add(new ProjectVariable(1, "IRICommonObjects.Security.UserBase"));
                _BaseClasses.Add(new ProjectVariable(1, "IRICommonObjects.Security.Person"));
                _BaseClasses.Add(new ProjectVariable(1, "IRICommonObjects.Security.Role"));
                _BaseClasses.Add(new ProjectVariable(1, "IRICommonObjects.Security.SiteConfigBase"));
            }
            return _BaseClasses;
        }
        set {
            _BaseClasses = value;
        }
    }
    public bool IsProjectOpen {
        get {
            return _IsProjectOpen;
        }
        set {
            _IsProjectOpen = value;
        }
    }
    public void Save(MemoryStream stream) {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize(stream, this);
        stream.Close();
    }
    public void Save(string filename) {
        bool overWriteFile = true;
        if (System.IO.File.Exists(filename)) {
            MsgBoxResult over;
            over = Interaction.MsgBox("File (" + filename + ") already exists. Do you wish to overwrite the file?", MsgBoxStyle.YesNo);
            overWriteFile = over == MsgBoxResult.Yes;
        }
        if (overWriteFile) {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this);
            stream.Close();
            StaticVariables.Instance.FileSaveAddress = filename;
            Interaction.MsgBox("Project successfully saved to " + filename + ".");
        } else
            CodeGeneratorForm.handleSaveAsDialog();
    }
    public static StaticVariables Load(string filename) {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        FileStream fromStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
        StaticVariables sv = null;
        try {
            sv = (StaticVariables)formatter.Deserialize(fromStream);
            sv.verifyPrimitiveDataTypesExist();
        } catch (Exception ex) {
            Interaction.MsgBox("Oops, we could not load the variables due to the following error: "
+ ex.Message);
        } finally {
            if (fromStream != null) {
                fromStream.Close();
                fromStream.Dispose();
            }
        }


        return sv;
    }

    public static void CleanUpObject() {
        if (Instance != null) {
            Dictionary<string, DataType> dictOTypes = new Dictionary<string, DataType>();
            List<DataType> lstOfTypesToRemove = new List<DataType>();
            foreach (DataType dt in Instance.DataTypes) {
                if (!dictOTypes.ContainsKey(dt.Name))
                    dictOTypes.Add(dt.Name, dt);
                else {
                    DataType typeInList = dictOTypes[dt.Name];
                    bool moveFirstToSecond = false;
                    if (dt.AssociatedProjectClass.NameSpaceVariable != null & typeInList.AssociatedProjectClass.NameSpaceVariable != null)
                        // both have class types
                        lstOfTypesToRemove.Add(dt); // keep the first
                    else if (dt.AssociatedProjectClass.NameSpaceVariable == null)
                        // new item does not have class type
                        lstOfTypesToRemove.Add(dt);
                    else if (typeInList.AssociatedProjectClass.NameSpaceVariable == null) {
                        // old item does nto have class type
                        if (dt.AssociatedProjectClass.NameSpaceVariable != null) {
                            lstOfTypesToRemove.Add(typeInList);
                            moveFirstToSecond = true;
                            dictOTypes[dt.Name] = dt;
                        } else
                            lstOfTypesToRemove.Add(dt);
                    } else {
                    }
                    if (moveFirstToSecond)
                        updateClassesToNewDataType(typeInList, dt);
                    else
                        updateClassesToNewDataType(dt, typeInList);
                }
            }

            foreach (DataType dt in lstOfTypesToRemove)
                Instance.DataTypes.Remove(dt);
        }
    }

    private static void updateClassesToNewDataType(DataType oldType, DataType newType) {
        foreach (ProjectClass cl in Instance._ListOfProjectClasses) {
            foreach (ClassVariable cv in cl.ClassVariables) {
                if (cv.ParameterType.Name == oldType.Name)
                    cv.ParameterType = newType;
            }
        }
    }
}
