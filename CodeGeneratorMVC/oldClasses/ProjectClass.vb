Imports System.Collections.Generic
Imports System.ComponentModel
Imports IRICommonObjects.Words

<Serializable()> Public Class ProjectClass

    Implements INotifyPropertyChanged


    <NonSerialized()> Private _Name As NameAlias
    Private _NameString As String

    Private _ClassVariables As New BindingList(Of ClassVariable)
    Private _AssociatedClasses As List(Of ProjectClass)
    Private _ID As Integer
    Private _NameSpaceVariable As ProjectVariable
    Private _DALClassVariable As DALClass
    Private _Summary As String = ""
    Private _BaseClass As ProjectVariable
	Private _MasterPage As MasterPageClass
	Private _TextProperty As ClassVariable
	Private _ValueProperty As ClassVariable
    Private _IsSelected As Boolean
    Private _ClassObjectIsNotNeeded As Boolean = False
    Private _IsAssociatedWithAliasGroup As Boolean = False
    Private _IsAssociatedWithUser As Boolean = False
    Private _DatabaseTableName As String = ""
    Private _IsAssociateEntitiy As Boolean = False
    Private _RequriesViewModel As Boolean
    Public Property RequiresViewModel() As Boolean
        Get
            Return _RequriesViewModel
        End Get
        Set(ByVal value As Boolean)
            _RequriesViewModel = value
        End Set
    End Property

    Public OriginalSQLText As String
    Public Property DatabaseTableName() As String
        Get
            Return _DatabaseTableName
        End Get
        Set(ByVal value As String)
            _DatabaseTableName = value
        End Set
    End Property
    Public Property IsAssociatedWithUser() As Boolean
        Get
            Return _IsAssociatedWithUser
        End Get
        Set(ByVal value As Boolean)
            _IsAssociatedWithUser = value
        End Set
    End Property

    Public Property IsAssociatedWithAliasGroup() As Boolean
        Get
            Return _IsAssociatedWithAliasGroup
        End Get
        Set(ByVal value As Boolean)
            _IsAssociatedWithAliasGroup = value
        End Set
    End Property
    Public Property ClassObjectIsNotNeeded As Boolean
        Get
            Return _ClassObjectIsNotNeeded
        End Get
        Set(value As Boolean)
            _ClassObjectIsNotNeeded = value
        End Set
    End Property
    Public Property IsAssociateEntitiy As Boolean
        Get
            Return _IsAssociateEntitiy
        End Get
        Set(value As Boolean)
            _IsAssociateEntitiy = value
        End Set
    End Property
    Public Sub New()
        AddHandler _ClassVariables.ListChanged, AddressOf ClassVariables_ListChanges
    End Sub
    Private Sub ClassVariables_ListChanges(ByVal sender As Object, ByVal e As ListChangedEventArgs)
        Dim index As Integer = 1
        For Each cv As ClassVariable In _ClassVariables
            If cv.ID = 0 Then
                cv.ID = index
            End If
            index += 1
        Next
    End Sub

    Public Property BaseClass() As ProjectVariable
        Get
            Return _BaseClass
        End Get
        Set(ByVal value As ProjectVariable)
            _BaseClass = value
            Me.NotifyPropertyChanged("BaseClass")
        End Set
    End Property
    Public Property MasterPage() As MasterPageClass
        Get
            Return _MasterPage
        End Get
        Set(ByVal value As MasterPageClass)
            _MasterPage = value
            Me.NotifyPropertyChanged("MasterPage")
        End Set
    End Property
    Public Property Name() As NameAlias
        Get
            If _Name Is Nothing Then
                _Name = New NameAlias(_NameString)
            End If
            Return _Name
        End Get
        Set(ByVal value As NameAlias)
            _Name = value
            _NameString = value.Capitalized
            Me.NotifyPropertyChanged("Name")
        End Set
    End Property
    Public ReadOnly Property NameForKeyAlias() As NameAlias
        Get
            Return New NameAlias(_Name.TextUnFormatted & "^i^d^")
        End Get
    End Property
    Public Property NameString() As String
        Get
            Return _NameString  '"(" & Me.ID.ToString & ") " &
        End Get
        Set(ByVal value As String)
            _NameString = value
            _Name = New NameAlias(value, True)
            Me.NotifyPropertyChanged("NameString")
            'Me.NotifyPropertyChanged("Name")
        End Set
    End Property
    Public Property NameSpaceVariable() As ProjectVariable
        Get
            Return _NameSpaceVariable
        End Get
        Set(ByVal value As ProjectVariable)
            _NameSpaceVariable = value
            Me.NotifyPropertyChanged("NameSpaceVariable")
        End Set
    End Property
    Public ReadOnly Property NameWithNameSpace As String
        Get
            Return NameSpaceVariable.NameBasedOnID & "." & Name.Capitalized
        End Get
    End Property
    Public Property DALClassVariable() As DALClass
        Get
            Return _DALClassVariable
        End Get
        Set(ByVal value As DALClass)
            _DALClassVariable = value
            Me.NotifyPropertyChanged("DALClassVariable")
        End Set
    End Property
    Public Property Summary() As String
        Get
            Return _Summary
        End Get
        Set(ByVal value As String)
            _Summary = value
            Me.NotifyPropertyChanged("Summary")
        End Set
    End Property
    Public Property ClassVariables() As BindingList(Of ClassVariable)
        Get
            Return _ClassVariables
        End Get
        Set(ByVal value As BindingList(Of ClassVariable))
            _ClassVariables = value
            Me.NotifyPropertyChanged("ClassVariables")
        End Set
    End Property
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
            Me.NotifyPropertyChanged("ID")
        End Set
    End Property
    Public Property IsSelected() As Boolean
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
            If _IsSelected Then
                If Not StaticVariables.Instance.SelectedProjectClasses.Contains(Me) Then
                    StaticVariables.Instance.SelectedProjectClasses.Add(Me)
                End If
            Else
                StaticVariables.Instance.SelectedProjectClasses.Remove(Me)
            End If
            Me.NotifyPropertyChanged("IsSelected")
        End Set
    End Property

    Public Property TextVariable() As ClassVariable
        Get
            Return _TextProperty
        End Get
        Set(ByVal value As ClassVariable)
            _TextProperty = value
            Me.NotifyPropertyChanged("TextProperty")
        End Set
    End Property
    Public Property ValueVariable() As ClassVariable
        Get
            Return _ValueProperty
        End Get
        Set(ByVal value As ClassVariable)
            _ValueProperty = value
            Me.NotifyPropertyChanged("ValueProperty")
        End Set
    End Property
    Public Property AssociatedClasses As List(Of ProjectClass)
        Get
            If _AssociatedClasses Is Nothing Then _AssociatedClasses = New List(Of ProjectClass)
            Return _AssociatedClasses
        End Get
        Set(value As List(Of ProjectClass))
            _AssociatedClasses = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return _Name.Capitalized
    End Function

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If obj Is Nothing Or Not Me.GetType() Is obj.GetType() Then
            Return False
        End If
        Dim pc As ProjectClass = CType(obj, ProjectClass)
        Return pc.ID = Me.ID
    End Function

End Class
Public Class ProjectClassList
    Inherits BindingList(Of ProjectClass)

End Class
