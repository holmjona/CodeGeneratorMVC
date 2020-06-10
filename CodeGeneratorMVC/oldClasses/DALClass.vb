Option Strict On

Imports System.ComponentModel

<Serializable()> Public Class DALClass
    Implements INotifyPropertyChanged
    Private _ReadOnlyConnectionString As ConnectionString
    Private _EditOnlyConnectionSTring As ConnectionString
    Private _NamespaceName As ProjectVariable
    Private _ID As Integer
    Private _Name As String = ""
    Public Property ReadOnlyConnectionString() As ConnectionString
        Get
            Return _ReadOnlyConnectionString
        End Get
        Set(ByVal value As ConnectionString)
            _ReadOnlyConnectionString = value
            Me.NotifyPropertyChanged("ReadOnlyConnectionString")
        End Set
    End Property
    Public Property EditOnlyConnectionstring() As ConnectionString
        Get
            Return _EditOnlyConnectionSTring
        End Get
        Set(ByVal value As ConnectionString)
            _EditOnlyConnectionSTring = value
            Me.NotifyPropertyChanged("EditOnlyConnectionString")
        End Set
    End Property
    Public Property NameSpaceName() As ProjectVariable
        Get
            Return _NamespaceName

        End Get
        Set(ByVal value As ProjectVariable)
            _NamespaceName = value
        End Set
    End Property

    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property
    Public ReadOnly Property Self() As DALClass
        Get
            Return Me
        End Get
    End Property
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
    Private Sub NotifyPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
    Public Overrides Function ToString() As String
        Return Name
    End Function

End Class
