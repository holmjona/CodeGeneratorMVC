Option Strict On
<Serializable()> Public Class ProjectVariable
    Private _ID As Integer
    Private _Name As String
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
    Public ReadOnly Property NameBasedOnID() As String
        Get
            If Name = StaticVariables.Instance.NoAssociatedNameSpaceString Then
                Return ""
            Else
                Return _Name
            End If

        End Get
    End Property
    Public Sub New()

    End Sub
    Public Sub New(ByVal ProjectVariableID As Integer, ByVal ProjectVariableName As String)
        _ID = ProjectVariableID
        _Name = ProjectVariableName
    End Sub
    Public Overrides Function ToString() As String
        Return _Name
    End Function
    Public ReadOnly Property Self() As ProjectVariable
        Get
            Return Me
        End Get
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is ProjectVariable Then
            Return Me = CType(obj, ProjectVariable)
        End If
        Return False
    End Function
    Public Overloads Shared Operator =(ByVal pv1 As ProjectVariable, ByVal pv2 As ProjectVariable) As Boolean
        If pv1 Is Nothing And pv2 Is Nothing Then
            Return True
        End If
        If pv1 Is Nothing OrElse pv2 Is Nothing Then
            Return False
        End If
        Return pv1.ID = pv2.ID
    End Operator
    Public Overloads Shared Operator <>(ByVal pv1 As ProjectVariable, ByVal pv2 As ProjectVariable) As Boolean
        Return Not pv1 = pv2
    End Operator



End Class
