' Created: 24 Aug 2007
' Creator: John Leith

Option Strict On

''' <summary>
''' when overriden in a child class
''' represents a database record
''' </summary>
Public MustInherit Class DatabaseRecord

	Protected _ID As Integer = -1

	''' <summary>
	''' The primary key of this record
	''' </summary>
    Public Property ID() As Integer
        Get
            Return _ID
        End Get
        Set(ByVal value As Integer) ' We need to be able to set this from code.
            _ID = value
        End Set
    End Property

	'''' <summary>
	'''' Saves this record to the database (it handles add or update as needed)
	'''' </summary>
	'Public Overridable Function dbSave() As Integer
	'	Dim retval As Integer
	'       If _ID = -1 Then ' The ID here needs to be -1
	'           retval = dbAdd()
	'       Else
	'           retval = dbUpdate()
	'       End If

	'	Return retval
	'End Function

	''' <summary>
	''' Determines based on class and ID if the given object is
	''' the same as this object
	''' </summary>
	Public Overrides Function Equals(ByVal obj As Object) As Boolean
		Dim retval As Boolean = False
		If Not obj Is Nothing AndAlso TypeOf obj Is DatabaseRecord Then
			If CType(obj, DatabaseRecord).ID = ID Then
				retval = True
			End If
		End If
		Return retval
	End Function

	''' <summary>
	''' This function is called by the Security DAL to load this object from the database
	''' </summary>
	Public MustOverride Sub Fill(ByVal dr As Data.SqlClient.SqlDataReader)

#Region "Operator Overloads"
	Overloads Shared Operator =(ByVal r1 As DatabaseRecord, ByVal r2 As DatabaseRecord) As Boolean
		Dim retval As Boolean = False
        If r1 Is Nothing AndAlso r2 Is Nothing Then
            Return True
        ElseIf r1 Is Nothing OrElse r2 Is Nothing Then
            Return False
        Else
            Return r1.ID = r2.ID
        End If
	End Operator
	Overloads Shared Operator <>(ByVal r1 As DatabaseRecord, ByVal r2 As DatabaseRecord) As Boolean
		Return Not r1 = r2
	End Operator
#End Region

#Region "MustOverride Members"

	''' <summary>
	''' When overridden in a child class this method 
	''' will insert a record into the database associated with this object
	''' </summary>
	Public MustOverride Function dbAdd() As Integer

	''' <summary>
	''' When overriden in a child class this method
	''' will update the assoicated record(s) in the database
	''' </summary>
	Public MustOverride Function dbUpdate() As Integer

	''' <summary>
	''' When overriden in a child class this method
	''' will return the most human-readable string representing the object
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Public MustOverride Overrides Function ToString() As String
#End Region

End Class
