'Created By: Michael Smuin
'Created On: 1/8/2009 12:44:58 PM
Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Security
	''' <summary>
	''' Object reference for any person who is accessing the system. Credentials differential all users. No credentials creates an anonymous user.
	''' </summary>
	''' <remarks></remarks>
	Public MustInherit Class UserBase
		Inherits Person
#Region "Private Variables"
		Protected _Email As String
		Protected _IsActive As Boolean
		Protected _Password As String
		Protected _UserName As String
#End Region
		Public Sub New()

		End Sub
		Public Sub New(ByVal isAnon As Boolean)
			If isAnon Then
				_Email = "invalid"
				_IsActive = True

			End If
		End Sub
#Region "Public Properties"
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property Email() As String
			Get
				Return _Email
			End Get
			Set(ByVal value As String)
				_Email = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Boolean</returns>
		''' <remarks></remarks>
		Public Property IsActive() As Boolean
			Get
				Return _IsActive
			End Get
			Set(ByVal value As Boolean)
				_IsActive = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property Password() As String
			Get
				Return _Password
			End Get
			Set(ByVal value As String)
				_Password = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property UserName() As String
			Get
				Return _UserName
			End Get
			Set(ByVal value As String)
				_UserName = value
			End Set
		End Property

		Public ReadOnly Property Hash() As String
			Get
				Return Tools.Hasher.CreateSHA1Hash(_Email & _Password)
			End Get
		End Property

#End Region

	End Class
End Namespace