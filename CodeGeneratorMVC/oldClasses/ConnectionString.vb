Option Strict On
<Serializable()> Public Class ConnectionString
    Private _ID As Integer
    Private _Name As String
    Private _UserName As String
    Public Sub New()

    End Sub
    Public Sub New(ByVal ConnectionID As Integer, ByVal ConnectionName As String, ByVal ConnectionUserName As String)
        _ID = ConnectionID
        _Name = ConnectionName
        _UserName = ConnectionUserName
    End Sub
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
    Public Property UserName() As String
        Get
            Return _UserName
        End Get
        Set(ByVal value As String)
            _UserName = value
        End Set
    End Property
    Public ReadOnly Property Self() As ConnectionString
        Get
            Return Me
        End Get
    End Property
    Public Overrides Function ToString() As String
        Return _Name
    End Function
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is ConnectionString Then
            Return Me = CType(obj, ConnectionString)
        End If
        Return False
    End Function
    Public Overloads Shared Operator =(ByVal pv1 As ConnectionString, ByVal pv2 As ConnectionString) As Boolean
        If pv1 Is Nothing And pv2 Is Nothing Then
            Return True
        End If
        If pv1 Is Nothing OrElse pv2 Is Nothing Then
            Return False
        End If
        Return pv1.ID = pv2.ID
    End Operator
    Public Overloads Shared Operator <>(ByVal pv1 As ConnectionString, ByVal pv2 As ConnectionString) As Boolean
        Return Not pv1 = pv2
    End Operator




End Class
