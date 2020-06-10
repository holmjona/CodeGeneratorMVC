'Created By: Michael Smuin
'Created On: 1/8/2009 12:30:56 PM
Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Security
	''' <summary>
	''' Categorization for users that also contains permission sets.
	''' </summary>
	''' <remarks></remarks>
	Public MustInherit Class RoleBase
		Inherits DatabaseRecord
#Region "Private Variables"

		Protected _Name As String = "Anonymous"
		Protected _IsAdmin As Boolean = False
		Protected _IsAnonymous As Boolean
#End Region
#Region "Constructors"
		Public Sub New(Optional ByVal IsAnon As Boolean = True)
			_ID = -3
			_IsAnonymous = True
		End Sub
#End Region

#Region "Public Properties"
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				_Name = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Boolean</returns>
		''' <remarks></remarks>
		Public Property IsAdmin() As Boolean
			Get
				Return _IsAdmin
			End Get
			Set(ByVal value As Boolean)
				_IsAdmin = value
			End Set
		End Property

		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Boolean</returns>
		''' <remarks></remarks>
		Public Property IsAnonymous() As Boolean
			Get
				Return _IsAnonymous
			End Get
			Set(ByVal value As Boolean)
				_IsAnonymous = value
			End Set
		End Property
#End Region

#Region "Public Subs"
		Public Overridable Sub setAnonymous()
			_Name = "Anonymous"
			'set all perms to false
			_IsAdmin = False
			'flag as anonymous
			_IsAnonymous = True
		End Sub
#End Region

	End Class
End Namespace