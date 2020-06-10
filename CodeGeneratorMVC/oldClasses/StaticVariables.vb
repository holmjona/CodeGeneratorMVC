Imports System.Collections.Generic
Imports System.ComponentModel
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.Serialization
Imports language = CodeGeneratorAddIn.CodeGeneration.Language

<Serializable()> Public Class StaticVariables
    Private Shared myInstance As StaticVariables = Nothing
    Private _IsProjectOpen As Boolean = False
    <NonSerialized()> Public _ApplicationObject As DTE2
    Private _ClassVariableToEdit As ClassVariable
    Private _ListOfProjectClasses As New BindingList(Of ProjectClass)
    Private _SelectedProjectClass As ProjectClass
    Private _SelectedProjectClasses As New List(Of ProjectClass)
    Private _DatabaseTypes As New List(Of String)
    Private _FileSaveAddress As String
    Private _AliasGroupClass As ProjectClass
    Private _UserClass As ProjectClass
    Private _VBDataTypes As BindingList(Of String)
    Private _MasterPages As BindingList(Of MasterPageClass)
    Private _MasterPage As MasterPageClass
    Private _ConnectionStrings As New BindingList(Of ConnectionString)
    Private _DALs As New BindingList(Of DALClass)
    Private _SelectedDAL As DALClass
    Private _NameSpaceNames As New BindingList(Of ProjectVariable)
    Private _SelectedNameSpaceName As ProjectVariable
    Private _BaseClasses As New BindingList(Of ProjectVariable)
    Private _SelectedBaseClasses As List(Of ProjectVariable)
    Private _DataTypes As New BindingList(Of DataType)
    Private _UserNames As New BindingList(Of ProjectVariable)
    Private _dictOfKnownFiles As Dictionary(Of String, String) = New Dictionary(Of String, String) From _
                                                                 {{"%%User%%", "Security.User"}, _
                                                                  {"%%AliasGroup%%", "Security.AliasGroup"}, _
                                                                  {"%%DescriptionGroup%%", "Security.DescriptionGroup"}, _
                                                                  {"%%SiteConfig%%", "Security.SigeConfig"}}


    ''##################################################################################################
    ''##########################   KEEP THESE TOGETHER    ##############################################
    ''' <summary>
    ''' This keeps track of the index of each of these types in the Shared Array.
    ''' </summary>
    ''' <remarks>It is best to add new ones to the end
    ''' Name Aliases are treated as primitive even though they are user defined.
    ''' </remarks>
    Public Enum DataPrimitives As Integer
        _String = 0 : _Integer = 1 : _Short = 2 : _Single = 3
        _Object = 4 : _Double = 5 : _Decimal = 6 : _Image = 7
        _ByteArray = 8 : _Boolean = 9 : _DateTime = 10 : _Date = 11
        _Int16 = 12 : _Byte = 13 : _Int64 = 14 : _NameAlias = 15 ' is not primitive, but we treat it like it is
    End Enum
    ''' <summary>
    ''' This keeps track of the all of the text names of each known primative data types,
    ''' use the DataPrimatives Enum to store the index.
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared PrimitiveDataTypes As String() = {"String", "Integer", "Short", "Single", _
                                                      "Object", "Double", "Decimal", "Image", _
                                                      "Byte()", "Boolean", "DateTime", "Date", _
                                                    "Int16", "Byte", "Int64", "NameAlias"}

    Private Shared _CSharpPrimitivesMap As Dictionary(Of String, String) _
        = New Dictionary(Of String, String) From {{"String", "string"}, {"Integer", "int"}, {"Short", "short"}, {"Single", "float"}, _
                                                  {"Object", "object"}, {"Double", "double"}, {"Decimal", "decimal"}, {"Image", "image"}, _
                                                  {"Byte()", "byte[]"}, {"Boolean", "bool"}, {"DateTime", "DateTime"}, {"Date", "DateTime"}, _
                                                  {"Int16", "Int16"}, {"Byte", "byte"}, {"Int64", "Int64"}, {"NameAlias", "NameAlias"}}

    ''#########################   KEEP PREVIOUS TOGETHER     ###########################################
    ''##################################################################################################

    Public Shared Function getDataPrimative(dp As DataPrimitives, language As language) As String
        If language = CodeGeneration.Language.VisualBasic Then
            Return PrimitiveDataTypes(dp)
        Else
            Return _CSharpPrimitivesMap(PrimitiveDataTypes(dp))
        End If
    End Function

    Public Property AliasGroupClass() As ProjectClass
        Get
            Return _AliasGroupClass
        End Get
        Set(ByVal value As ProjectClass)
            _AliasGroupClass = value
        End Set
    End Property
    Public Property UserClass() As ProjectClass
        Get
            Return _UserClass
        End Get
        Set(ByVal value As ProjectClass)
            _UserClass = value
        End Set
    End Property

    Public Property FileSaveAddress() As String
        Get
            Return _FileSaveAddress
        End Get
        Set(ByVal value As String)
            _FileSaveAddress = value
        End Set
    End Property

    Public Sub GetObjectData(ByVal info As SerializationInfo, ByVal ctxt As StreamingContext)

    End Sub
    Private Sub New()
        AddHandler NameSpaceNames.ListChanged, AddressOf NameSpace_ListChanged
        AddHandler ConnectionStrings.ListChanged, AddressOf ConnectionStrings_ListChanged
    End Sub
    Private Sub NameSpace_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        Dim index As Integer = 0
        For Each n As ProjectVariable In NameSpaceNames
            n.ID = index
            index += 1
        Next
    End Sub
    Private Sub ConnectionStrings_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        Dim index As Integer = 0
        For Each n As ConnectionString In ConnectionStrings
            n.ID = index
            index += 1
        Next
    End Sub
    Public Shared Property Instance() As StaticVariables
        Get
            If myInstance Is Nothing Then
                myInstance = New StaticVariables()

            End If
            Return myInstance
        End Get
        Set(ByVal value As StaticVariables)
            myInstance = value
        End Set
    End Property
    Public Property ApplicationObject() As DTE2
        Get
            Return _ApplicationObject
        End Get
        Set(ByVal value As DTE2)
            _ApplicationObject = value
        End Set
    End Property
    Public Property ClassVariableToEdit() As ClassVariable
        Get
            Return _ClassVariableToEdit
        End Get
        Set(ByVal value As ClassVariable)
            _ClassVariableToEdit = value
        End Set
    End Property
    Public Property ListOfProjectClasses() As BindingList(Of ProjectClass)
        Get
            Return _ListOfProjectClasses
        End Get
        Set(ByVal value As BindingList(Of ProjectClass))
            _ListOfProjectClasses = value
        End Set
    End Property
    Public ReadOnly Property HighestProjectClassID() As Integer
        Get
            Dim highestID As Integer = 0
            For Each p As ProjectClass In ListOfProjectClasses
                If p.ID > highestID Then highestID = p.ID
            Next
            Return highestID
        End Get
    End Property
    Public Property SelectedProjectClass() As ProjectClass
        Get
            Return _SelectedProjectClass
        End Get
        Set(ByVal value As ProjectClass)
            _SelectedProjectClass = value
        End Set
    End Property
    Public Property SelectedProjectClasses() As List(Of ProjectClass)
        Get
            Return _SelectedProjectClasses
        End Get
        Set(ByVal value As List(Of ProjectClass))
            _SelectedProjectClasses = value
        End Set
    End Property
    Public Property UserNames() As BindingList(Of ProjectVariable)
        Get
            Return _UserNames
        End Get
        Set(ByVal value As BindingList(Of ProjectVariable))
            _UserNames = value
        End Set
    End Property
    Public Property ConnectionStrings() As BindingList(Of ConnectionString)
        Get
            If _ConnectionStrings.Count = 0 Then
                _ConnectionStrings.Add(New ConnectionString(0, "EditOnlyConnectionString", "newdatabaseuser"))
                _ConnectionStrings.Add(New ConnectionString(1, "ReadOnlyConnectionString", "newdatabaseuser"))

            End If
            Return _ConnectionStrings
        End Get
        Set(ByVal value As BindingList(Of ConnectionString))
            _ConnectionStrings = value
        End Set
    End Property
    Public Property SelectedDAL() As DALClass
        Get
            Return _SelectedDAL
        End Get
        Set(ByVal value As DALClass)
            _SelectedDAL = value
        End Set
    End Property
    Public Property DALs() As BindingList(Of DALClass)
        Get
            Return _DALs
        End Get
        Set(ByVal value As BindingList(Of DALClass))
            _DALs = value
        End Set
    End Property


    Public Property SelectedNameSpaceName() As ProjectVariable
        Get
            Return _SelectedNameSpaceName
        End Get
        Set(ByVal value As ProjectVariable)
            _SelectedNameSpaceName = value
        End Set
    End Property
    Public ReadOnly Property NoAssociatedNameSpaceString() As String
        Get
            Return "No associated namespace"
        End Get
    End Property
    Public Property NameSpaceNames() As BindingList(Of ProjectVariable)
        Get
            If _NameSpaceNames.Count = 0 Then
                _NameSpaceNames.Add(New ProjectVariable(-5, NoAssociatedNameSpaceString))
            End If
            Return _NameSpaceNames
        End Get
        Set(ByVal value As BindingList(Of ProjectVariable))
            _NameSpaceNames = value
        End Set
    End Property

    Public Property SelectedBaseClasses() As List(Of ProjectVariable)
        Get
            Return _SelectedBaseClasses
        End Get
        Set(ByVal value As List(Of ProjectVariable))
            _SelectedBaseClasses = value
        End Set
    End Property
    Public Property MasterPages() As BindingList(Of MasterPageClass)
        Get
            If _MasterPages Is Nothing Then
                _MasterPages = New BindingList(Of MasterPageClass)
                For Each mPage As MasterPageClass In MasterPageClass.getMasterPagesForProject(ApplicationObject)
                    _MasterPages.Add(mPage)
                Next
            End If
            Return _MasterPages
        End Get
        Set(ByVal value As BindingList(Of MasterPageClass))
            _MasterPages = value
        End Set
    End Property
    Public Property SelectedMasterPage() As MasterPageClass
        Get
            If _MasterPage Is Nothing Then
                _MasterPage = New MasterPageClass()
            End If
            Return _MasterPage
        End Get
        Set(ByVal value As MasterPageClass)
            _MasterPage = value
        End Set
    End Property
    Public Property DataTypes() As BindingList(Of DataType)
        Get
            If _DataTypes.Count < 1 Then
                ' Add Primitive Types to the Structure.
                For Each s As String In PrimitiveDataTypes
                    AddPrimitiveDataType(s, True)
                Next
                'AddPrimitiveDataType("String", True)
                'AddPrimitiveDataType("Integer", True)
                'AddPrimitiveDataType("Short", True)
                'AddPrimitiveDataType("Single", True)
                'AddPrimitiveDataType("Object", True)
                'AddPrimitiveDataType("Double", True)
                'AddPrimitiveDataType("Decimal", True)
                'AddPrimitiveDataType("Image", True)
                'AddPrimitiveDataType("Byte()", True)
                'AddPrimitiveDataType("Boolean", True)
                'AddPrimitiveDataType("DateTime", True)
                'AddPrimitiveDataType("Date", True)
                'AddPrimitiveDataType("Int16", True)
                'AddPrimitiveDataType("Byte", True)
                'AddPrimitiveDataType("Int64", True)
                ' The following is not a primitive type, but acts like one.
                'AddPrimitiveDataType("NameAlias", True)
            End If
            Return _DataTypes
        End Get
        Set(ByVal value As BindingList(Of DataType))
            _DataTypes = value
        End Set
    End Property
    ''' <summary>
    ''' Make sure that an exist project has all primitive types. 
    ''' </summary>
    ''' <remarks>This should only be called on newly opened Projects, because it should only affect older projects.</remarks>
    Public Sub verifyPrimitiveDataTypesExist()
        For Each pt In PrimitiveDataTypes
            Dim wasFound As Boolean = False
            For Each dt In DataTypes
                If dt.Name = pt Then
                    wasFound = True
                    Exit For
                End If
            Next dt
            If Not wasFound Then
                AddPrimitiveDataType(pt, True)
            End If
        Next pt
    End Sub
    Public Function getCSharpTypeName(typeName As String) As String
        'http://www.harding.edu/fmccown/vbnet_csharp_comparison.html
        'If _CSharpPrimitivesMap Is Nothing Then _CSharpPrimitivesMap = New Dictionary(Of String, String)
        'If _CSharpPrimitivesMap.Count < 1 Then
        '    _CSharpPrimitivesMap.Add("String", "string")
        '    _CSharpPrimitivesMap.Add("Integer", "int")
        '    _CSharpPrimitivesMap.Add("Short", "short")
        '    _CSharpPrimitivesMap.Add("Single", "float")
        '    _CSharpPrimitivesMap.Add("Object", "object")
        '    _CSharpPrimitivesMap.Add("Double", "double")
        '    _CSharpPrimitivesMap.Add("Decimal", "decimal")
        '    _CSharpPrimitivesMap.Add("Image", "image")
        '    _CSharpPrimitivesMap.Add("Byte()", "byte[]")
        '    _CSharpPrimitivesMap.Add("Boolean", "bool")
        '    _CSharpPrimitivesMap.Add("DateTime", "DateTime")
        '    _CSharpPrimitivesMap.Add("Date", "DateTime")
        '    _CSharpPrimitivesMap.Add("Int16", "Int16")
        '    _CSharpPrimitivesMap.Add("Byte", "byte")
        '    _CSharpPrimitivesMap.Add("Int64", "Int64")
        'End If
        If GetDataType(typeName) IsNot Nothing Then
            If _CSharpPrimitivesMap.ContainsKey(typeName) Then
                Return _CSharpPrimitivesMap(typeName)
            End If
        End If
        Return typeName
    End Function
    Public Function NeededFile(key As String) As String
        If _dictOfKnownFiles.ContainsKey(key) Then
            Return _dictOfKnownFiles(key)
        End If
        Return "Unknown.Type"
    End Function

    Public Sub addDerivedTypeToSystem(pClass As ProjectClass)
        Dim classExists As Boolean = False
        'check to see if datatype is already in the list.
        For Each dType As DataType In StaticVariables.Instance.DataTypes
            If dType.Name.ToLower().CompareTo(pClass.Name.Capitalized.ToLower()) = 0 Then
                classExists = False
                Exit Sub
            End If
        Next
        If Not classExists Then
            ' add new datatype to list.
            StaticVariables.Instance.AddDataType(pClass, False)
        End If
    End Sub

    Public Sub AddDataType(ByRef pClass As ProjectClass, ByVal IsDataTypePrimitive As Boolean)
        _DataTypes.Add(New DataType(_DataTypes.Count, pClass.Name.Capitalized, IsDataTypePrimitive, pClass))
    End Sub
    Public Sub AddPrimitiveDataType(ByVal DataTypeName As String, ByVal IsDataTypePrimitive As Boolean)
        _DataTypes.Add(New DataType(_DataTypes.Count, DataTypeName, IsDataTypePrimitive, Nothing))
    End Sub
    Public Function GetDataType(ByVal ID As Integer) As DataType
        For Each dt As DataType In DataTypes
            If dt.ID = ID Then
                Return dt
            End If
        Next
        Return Nothing
    End Function
    Public Function GetDatatype(ByVal Name As String) As DataType
        For Each dt As DataType In DataTypes
            If dt.Name.ToLower().CompareTo(Name.ToLower.Trim()) = 0 Then
                Return dt
            End If
        Next
        Return Nothing
    End Function

    Public Property BaseClasses() As BindingList(Of ProjectVariable)
        Get
            If _BaseClasses Is Nothing Then
                _BaseClasses = New BindingList(Of ProjectVariable)
            End If
            If _BaseClasses.Count = 0 Then
                _BaseClasses.Add(New ProjectVariable(1, "IRICommonObjects.DatabaseRecord"))
                _BaseClasses.Add(New ProjectVariable(1, "IRICommonObjects.Security.UserBase"))
                _BaseClasses.Add(New ProjectVariable(1, "IRICommonObjects.Security.Person"))
                _BaseClasses.Add(New ProjectVariable(1, "IRICommonObjects.Security.Role"))
                _BaseClasses.Add(New ProjectVariable(1, "IRICommonObjects.Security.SiteConfigBase"))
            End If
            Return _BaseClasses
        End Get
        Set(ByVal value As BindingList(Of ProjectVariable))
            _BaseClasses = value

        End Set
    End Property
    Public Property IsProjectOpen() As Boolean
        Get
            Return _IsProjectOpen
        End Get
        Set(ByVal value As Boolean)
            _IsProjectOpen = value
        End Set
    End Property
    Public Sub Save(ByVal stream As MemoryStream)
        Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        formatter.Serialize(stream, Me)
        stream.Close()
    End Sub
    Public Sub Save(ByVal filename As String)
        Dim overWriteFile As Boolean = True
        If IO.File.Exists(filename) Then
            Dim over As MsgBoxResult
            over = MsgBox("File (" & filename & ") already exists. Do you wish to overwrite the file?", MsgBoxStyle.YesNo)
            overWriteFile = over = MsgBoxResult.Yes
        End If
        If overWriteFile Then
            Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            Dim stream As New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None)
            formatter.Serialize(stream, Me)
            stream.Close()
            StaticVariables.Instance.FileSaveAddress = filename
            MsgBox("Project successfully saved to " & filename & ".")
        Else
            CodeGeneratorForm.handleSaveAsDialog()
        End If
    End Sub
    Public Shared Function Load(ByVal filename As String) As StaticVariables
        Dim formatter As New Runtime.Serialization.Formatters.Binary.BinaryFormatter()
        Dim fromStream As New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim sv As StaticVariables = Nothing
        Try
            sv = CType(formatter.Deserialize(fromStream), StaticVariables)
            sv.verifyPrimitiveDataTypesExist()
        Catch ex As Exception
            MsgBox("Oops, we could not load the variables due to the following error: " _
                   & ex.Message)
        Finally
            If fromStream IsNot Nothing Then
                fromStream.Close()
                fromStream.Dispose()
            End If
        End Try


        Return sv
    End Function

    Shared Sub CleanUpObject()
        If Instance IsNot Nothing Then
            Dim dictOTypes As New Dictionary(Of String, DataType)
            Dim lstOfTypesToRemove As New List(Of DataType)
            For Each dt As DataType In Instance.DataTypes
                If Not dictOTypes.ContainsKey(dt.Name) Then
                    dictOTypes.Add(dt.Name, dt)
                Else
                    Dim typeInList As DataType = dictOTypes(dt.Name)
                    Dim moveFirstToSecond As Boolean = False
                    If dt.AssociatedProjectClass.NameSpaceVariable IsNot Nothing And typeInList.AssociatedProjectClass.NameSpaceVariable IsNot Nothing Then
                        ' both have class types
                        lstOfTypesToRemove.Add(dt) ' keep the first
                    ElseIf dt.AssociatedProjectClass.NameSpaceVariable Is Nothing Then
                        ' new item does not have class type
                        lstOfTypesToRemove.Add(dt)
                    ElseIf typeInList.AssociatedProjectClass.NameSpaceVariable Is Nothing Then
                        ' old item does nto have class type
                        If dt.AssociatedProjectClass.NameSpaceVariable IsNot Nothing Then
                            lstOfTypesToRemove.Add(typeInList)
                            moveFirstToSecond = True
                            dictOTypes(dt.Name) = dt
                        Else
                            lstOfTypesToRemove.Add(dt)
                        End If
                    Else
                        ' nether item has a type
                    End If
                    If moveFirstToSecond Then
                        updateClassesToNewDataType(typeInList, dt)
                    Else
                        updateClassesToNewDataType(dt, typeInList)
                    End If
                End If
            Next

            For Each dt As DataType In lstOfTypesToRemove
                Instance.DataTypes.Remove(dt)
            Next

        End If
    End Sub

    Private Shared Sub updateClassesToNewDataType(oldType As DataType, newType As DataType)
        For Each cl As ProjectClass In Instance._ListOfProjectClasses
            For Each cv As ClassVariable In cl.ClassVariables
                If cv.ParameterType.Name = oldType.Name Then
                    cv.ParameterType = newType
                End If
            Next
        Next
    End Sub

End Class
