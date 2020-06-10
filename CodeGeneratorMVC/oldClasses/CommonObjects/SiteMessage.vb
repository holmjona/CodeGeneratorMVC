'Created By: JDH
'Created On: 3/24/2009 2:19:09 PM
Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Security
	''' <summary>
	''' Messages for Sites
	''' </summary>
	''' <remarks></remarks>
	Public MustInherit Class SiteMessageBase
		Inherits DatabaseRecord
#Region "Private Variables"
		Private _DateAdded As Date
		Private _AddedBy As UserBase
		Private _AddedByID As Integer
		Private _DateShow As Date
		Private _DateExpire As Date
		Private _IsMaintenance As Boolean
		Private _IsExpired As Boolean
		Private _SiteConfig As SiteConfigBase
		Private _SiteConfigID As Integer
		Private _Message As String
#End Region

#Region "Public Properties"
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Date</returns>
		''' <remarks></remarks>
		Public Property DateAdded() As Date
			Get
				Return _DateAdded
			End Get
			Set(ByVal value As Date)
				_DateAdded = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Integer</returns>
		''' <remarks></remarks>
		Public MustOverride Property AddedBy() As UserBase

		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Integer</returns>
		''' <remarks></remarks>
		Public Property AddedByID() As Integer
			Get
				Return _AddedByID
			End Get
			Set(ByVal value As Integer)
				_AddedByID = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Date</returns>
		''' <remarks></remarks>
		Public Property DateShow() As Date
			Get
				Return _DateShow
			End Get
			Set(ByVal value As Date)
				_DateShow = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Date</returns>
		''' <remarks></remarks>
		Public Property DateExpire() As Date
			Get
				Return _DateExpire
			End Get
			Set(ByVal value As Date)
				_DateExpire = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Boolean</returns>
		''' <remarks></remarks>
		Public Property IsMaintenance() As Boolean
			Get
				Return _IsMaintenance
			End Get
			Set(ByVal value As Boolean)
				_IsMaintenance = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Boolean</returns>
		''' <remarks></remarks>
		Public Property IsExpired() As Boolean
			Get
				Return _IsExpired
			End Get
			Set(ByVal value As Boolean)
				_IsExpired = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Integer</returns>
		''' <remarks></remarks>
		Public MustOverride Property SiteConfig() As SiteConfigBase

		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>Integer</returns>
		''' <remarks></remarks>
		Public Property SiteConfigID() As Integer
			Get
				Return _SiteConfigID
			End Get
			Set(ByVal value As Integer)
				_SiteConfigID = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property Message() As String
			Get
				Return _Message
			End Get
			Set(ByVal value As String)
				_Message = value
			End Set
		End Property
#End Region
	End Class
End Namespace
