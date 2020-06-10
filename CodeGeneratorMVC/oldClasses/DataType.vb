<Serializable()> Public Class DataType
    Private _ID As Integer
    Private _Name As String
    Private _IsPrimitive As Boolean
    Private _NameSpaceObject As ProjectVariable
    Private _AssociatedClass As ProjectClass
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer)
            _ID = value
        End Set
    End Property
    Public ReadOnly Property Name(Optional lang As CodeGeneration.Language = CodeGeneration.Language.VisualBasic) As String
        Get
            If _IsPrimitive OrElse _AssociatedClass Is Nothing Then
                If lang = CodeGeneration.Language.CSharp Then
                    Return StaticVariables.Instance.getCSharpTypeName(_Name)
                End If
                Return _Name
            ElseIf _AssociatedClass IsNot Nothing Then
                Return _AssociatedClass.Name.Capitalized
            End If
            Return _Name
        End Get
        'Set(ByVal value As String)
        '    _Name = value
        'End Set
    End Property
    Public Property IsPrimitive() As Boolean
        Get
            Return _IsPrimitive
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property
    Public ReadOnly Property IsImage() As Boolean
        Get
            Return Name(CodeGeneration.Language.VisualBasic).ToLower().CompareTo("image") = 0
        End Get
    End Property
    'Public Property NameSpaceObject() As ProjectVariable
    '    Get
    '        Return _NameSpaceObject
    '    End Get
    '    Set(ByVal value As ProjectVariable)
    '        _NameSpaceObject = value
    '    End Set
    'End Property
    Public ReadOnly Property IsNameAlias() As Boolean
        Get
            Return Name(CodeGeneration.Language.VisualBasic).ToLower().CompareTo("namealias") = 0
        End Get
    End Property
    Public Property AssociatedProjectClass() As ProjectClass
        Get
            Return _AssociatedClass
        End Get
        Set(ByVal value As ProjectClass)
            _AssociatedClass = value
        End Set
    End Property

    Public Sub New(ByVal DataTypeID As Integer, ByVal DataTypeName As String, ByVal IsDataTypePrimitive As Boolean, ByVal pClass As ProjectClass)
        _ID = DataTypeID
        _Name = DataTypeName
        _IsPrimitive = IsDataTypePrimitive
        _AssociatedClass = pClass
    End Sub
    Public ReadOnly Property Self() As DataType
        Get
            Return Me
        End Get
    End Property
    <Obsolete()>
    Public Overrides Function ToString() As String
        Return Name(CodeGeneration.Language.VisualBasic)
    End Function
    Public Overloads Function ToString(lang As CodeGeneration.Language) As String
        Return Name(lang)
    End Function

End Class
