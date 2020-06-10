Option Strict On

Imports System.ComponentModel
Imports IRICommonObjects.Words

<Serializable()> Public Class ClassVariable
    'Inherits System.ComponentModel.BindingList(Of ClassVariable)
    Implements INotifyPropertyChanged

    Private _ID As Integer
    Private _name As String
    Private _parameterType As DataType
    Private _IsIDField As Boolean = False
    Private _IsForeignkey As Boolean = False ' True if the field was added as a foreign key
    Private _IsAssociative As Boolean = False ' True if the field was added only because of an associate entity relation
    Private _IsList As Boolean = False
    'Private _NeedsAttention As Boolean = False
    Private _displayOnViewPage As Boolean
    Private _displayOnEditPage As Boolean
    Private _IsPropertyInherited As Boolean
    'Variables generated from chosen values
    Private _defaultHTMLName As String
    Private _IsTextBox As Boolean
    Private _IsDropDownList As Boolean
    Private _IsCheckBox As Boolean
    Private _IsDate As Boolean
    Private _IsInteger As Boolean
    Private _IsDouble As Boolean
    Private _IsRequired As Boolean = False ' True if the field cannot be null in the database
    Private _IsPropertyXMLIgnored As Boolean = False
    Private _ParentClass As ProjectClass
    Private _IsDatabaseBound As Boolean = False
    Private _LengthOfDatabaseProperty As Integer = -1
    Private _DatabaseType As String = ""
    Private _DatabaseColumnName As String = ""
    Private Sub New()

    End Sub
    Public Sub New(ByVal pClass As ProjectClass, ByVal Name As String, ByVal parameterType As DataType, ByVal isForeignKeyField As Boolean, _
       ByVal isAssociativeField As Boolean, ByVal list As Boolean, ByVal isID As Boolean, ByVal PropertyInherited As Boolean, ByVal DisplayOnEdit As Boolean, _
       ByVal DisplayOnView As Boolean, ByVal NewID As Integer, ByVal IsClassVariableDatabaseBound As Boolean, ByVal isRequired As Boolean, _
       ByVal ClassVariableDatabaseType As String, ByVal ClassVariableLengthOfDatabaseProperty As Integer, ByVal DatabaseColumnName As String)

        MyBase.New()
        _ParentClass = pClass
        _ID = NewID
        _IsIDField = isID
        _IsList = list
        _name = Name
        _parameterType = parameterType
        _IsForeignkey = isForeignKeyField
        _IsAssociative = isAssociativeField
        _IsPropertyInherited = PropertyInherited
        _displayOnEditPage = DisplayOnEdit
        _displayOnViewPage = DisplayOnView
        _IsDatabaseBound = IsClassVariableDatabaseBound
        _DatabaseType = ClassVariableDatabaseType
        _LengthOfDatabaseProperty = ClassVariableLengthOfDatabaseProperty
        _DatabaseColumnName = DatabaseColumnName
        _IsRequired = isRequired
        setParameters()

    End Sub
    Public Property DatabaseColumnName() As String
        Get
            Return _DatabaseColumnName
        End Get
        Set(ByVal value As String)
            _DatabaseColumnName = value
        End Set
    End Property
    Public Property IsDatabaseBound() As Boolean
        Get
            Return _IsDatabaseBound
        End Get
        Set(ByVal value As Boolean)
            _IsDatabaseBound = value
        End Set
    End Property
    'Public Property NeedsAttention() As Boolean
    '    Get
    '        Return _NeedsAttention
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _NeedsAttention = value
    '    End Set
    'End Property
    Public Property ParentClass() As ProjectClass
        Get
            Return _ParentClass
        End Get
        Set(ByVal value As ProjectClass)
            _ParentClass = value
        End Set
    End Property
    Private Sub setParameters()
        _defaultHTMLName = ""
        _IsTextBox = False
        _IsDropDownList = False
        _IsCheckBox = False
        _IsDate = False
        _IsInteger = False
        _IsDouble = False
        If _parameterType IsNot Nothing Then
            Select Case _parameterType.Name.ToLower()

                Case "string"
                    'textbox
                    '_defaultHTMLName = "txt" & Name
                    _defaultHTMLName = getControlNameControl("txt", Name)
                    _IsTextBox = True
                Case "integer", "int16", "int64", "byte"
                    _defaultHTMLName = getControlNameControl("txt", Name)
                    _IsTextBox = True
                    _IsInteger = True
                Case "double"
                    _defaultHTMLName = getControlNameControl("txt", Name)
                    _IsTextBox = True
                    _IsDouble = True
                Case "boolean"
                    _defaultHTMLName = getControlNameControl("chk", Name)
                    _IsCheckBox = True
                Case "datetime", "date"
                    _IsDate = True
                    'three textboxes
                Case "namealias"
                    _IsTextBox = True
                    _defaultHTMLName = getControlNameControl("txt", Name)
                Case Else
                    'dropdownlist
                    _IsDropDownList = True
                    Dim myAlias As New NameAlias(Name)
                    '_defaultHTMLName = "ddl" & myAlias.PluralAndCapitalized
                    _defaultHTMLName = getControlNameControl("ddl", myAlias.PluralAndCapitalized)
                    _IsPropertyXMLIgnored = True
            End Select

        End If

    End Sub
    Public Function getControlNameControl(ByVal controlPrefix As String, ByVal controlIdentifier As String) As String
        Return controlPrefix & controlIdentifier
    End Function
    Public Function getDayTextControlName() As String
        Return getControlNameControl("txt" & Me.Name, "Day")
    End Function
    Public Function GetMonthTextControlName() As String
        Return getControlNameControl("txt" & Me.Name, "Month")
    End Function
    Public Function getYearTextControlName() As String
        Return getControlNameControl("txt" & Me.Name, "Year")
    End Function
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
            Me.NotifyPropertyChanged("ID")
        End Set
    End Property
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
            setParameters()
            Me.NotifyPropertyChanged("Name")
        End Set
    End Property
    Public Property ParameterType() As DataType
        Get
            Return _parameterType
        End Get
        Set(ByVal value As DataType)
            _parameterType = value
            setParameters()
            Me.NotifyPropertyChanged("ParameterType")
        End Set
    End Property
    Public Property IsForeignKey() As Boolean
        Get
            Return _IsForeignkey
        End Get
        Set(ByVal value As Boolean)
            _IsForeignkey = value
            Me.NotifyPropertyChanged("IsForeignKey")
        End Set
    End Property
    Public Property IsRequired() As Boolean
        Get
            Return _IsRequired
        End Get
        Set(ByVal value As Boolean)
            _IsRequired = value
            Me.NotifyPropertyChanged("IsRequired")
        End Set
    End Property
    Public Property IsAssociative() As Boolean
        Get
            Return _IsAssociative
        End Get
        Set(ByVal value As Boolean)
            _IsAssociative = value
            Me.NotifyPropertyChanged("IsAssociative")
        End Set
    End Property
    Public Property IsList() As Boolean
        Get
            Return _IsList
        End Get
        Set(ByVal value As Boolean)
            _IsList = value
            Me.NotifyPropertyChanged("IsList")
        End Set
    End Property
    Public Property IsIDField() As Boolean
        Get
            Return _IsIDField
        End Get
        Set(ByVal value As Boolean)
            _IsIDField = value
            Me.NotifyPropertyChanged("IsIDField")
        End Set
    End Property
    Public Property DefaultHTMLName() As String
        Get
            Return _defaultHTMLName
        End Get
        Set(ByVal value As String)
            _defaultHTMLName = value
            Me.NotifyPropertyChanged("DefaultHTMLName")
        End Set
    End Property
    Public Property IsTextBox() As Boolean
        Get
            Return _IsTextBox
        End Get
        Set(ByVal value As Boolean)
            _IsTextBox = value
            Me.NotifyPropertyChanged("IsTextBox")
        End Set
    End Property
    Public Property IsDropDownList() As Boolean
        Get
            Return _IsDropDownList
        End Get
        Set(ByVal value As Boolean)
            _IsDropDownList = value
            Me.NotifyPropertyChanged("IsDropDownList")
        End Set
    End Property
    Public Property IsCheckBox() As Boolean
        Get
            Return _IsCheckBox
        End Get
        Set(ByVal value As Boolean)
            _IsCheckBox = value
            Me.NotifyPropertyChanged("IsCheckBox")
        End Set
    End Property
    Public Property IsDate() As Boolean
        Get
            Return _IsDate
        End Get
        Set(ByVal value As Boolean)
            _IsDate = value
            Me.NotifyPropertyChanged("IsDate")
        End Set
    End Property
    Public Property IsInteger() As Boolean
        Get
            Return _IsInteger
        End Get
        Set(ByVal value As Boolean)
            _IsInteger = value
            Me.NotifyPropertyChanged("IsInteger")
        End Set
    End Property
    Public Property IsDouble() As Boolean
        Get
            Return _IsDouble
        End Get
        Set(ByVal value As Boolean)
            _IsDouble = value
            Me.NotifyPropertyChanged("IsDouble")
        End Set
    End Property
    Public Property DisplayOnViewPage() As Boolean
        Get
            Return _displayOnViewPage
        End Get
        Set(ByVal value As Boolean)
            _displayOnViewPage = value
            Me.NotifyPropertyChanged("DisplayOnViewPage")
        End Set
    End Property
    Public Property DisplayOnEditPage() As Boolean
        Get
            Return _displayOnEditPage
        End Get
        Set(ByVal value As Boolean)
            _displayOnEditPage = value
            Me.NotifyPropertyChanged("DisplayOnEditPage")
        End Set
    End Property
    Public Property IsPropertyInherited() As Boolean
        Get
            Return _IsPropertyInherited
        End Get
        Set(ByVal value As Boolean)
            _IsPropertyInherited = value
            Me.NotifyPropertyChanged("IsPropertyInherited")
        End Set
    End Property
    Public Property IsPropertyXMLIgnored() As Boolean
        Get
            Return _IsPropertyXMLIgnored
        End Get
        Set(ByVal value As Boolean)
            _IsPropertyXMLIgnored = value
            Me.NotifyPropertyChanged("IsPropertyXMLIgnored")
        End Set
    End Property
    Public Property LengthOfDatabaseProperty() As Integer
        Get
            Return _LengthOfDatabaseProperty
        End Get
        Set(ByVal value As Integer)
            _LengthOfDatabaseProperty = value
        End Set
    End Property
    Public Property DatabaseType() As String
        Get
            Return _DatabaseType
        End Get
        Set(ByVal value As String)
            _DatabaseType = value
        End Set
    End Property
    Public ReadOnly Property DatabaseTypeWithLength() As String
        Get
            Return _DatabaseType & _
                IIf(_LengthOfDatabaseProperty > -1, "(" & _LengthOfDatabaseProperty & ")", "").ToString()
        End Get
    End Property
    Public Overrides Function ToString() As String
        Return _name
    End Function

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(ByVal name As String)
        '        If PropertyChanged IsNot Nothing Then
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
        'End If
	End Sub
	Public ReadOnly Property Self() As ClassVariable
		Get
			Return Me
		End Get
    End Property
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is ClassVariable Then
            Return Me = CType(obj, ClassVariable)
        End If
        Return False
    End Function
    Public Overloads Shared Operator =(ByVal pv1 As ClassVariable, ByVal pv2 As ClassVariable) As Boolean
        If pv1 Is Nothing And pv2 Is Nothing Then
            Return True
        End If
        If pv1 Is Nothing OrElse pv2 Is Nothing Then
            Return False
        End If
        Return pv1.ID = pv2.ID
    End Operator
    Public Overloads Shared Operator <>(ByVal pv1 As ClassVariable, ByVal pv2 As ClassVariable) As Boolean
        Return Not pv1 = pv2
    End Operator
    Public Function getVariableName() As String
        If Not CodeGeneration.isRegularDataType(ParameterType.Name) Then
            Return Name & "ID"
        Else
            Return Name
        End If
    End Function
End Class
