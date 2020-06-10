'Created By: Michael Smuin
'Created On: 1/13/2009 2:26:22 PM
Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Namespace Security
	''' <summary>
	''' Site Variables for the site that the user is at.
	''' </summary>
	''' <remarks></remarks>
	Public MustInherit Class SiteConfigBase
		'##########################################################
		' Static Variables   
		Protected Shared VersionReleaseNumber As Integer = 0
		Protected Shared VersionMajorNumber As Integer = 0
		Protected Shared VersionMinorNumber As Integer = 0
		Protected Shared VersionDate As Date

		Public Enum verClass As Integer
			live	'Live Production 
			dev		'In Development
			test	'Testing
			alpha	'Alpha Stage
			beta	'Beta Stage
		End Enum


		' the VersionClass given here will not only flag the code but place the correct banner on the site.
		' the VersionClass must match specifically to the list below.
		'==============================
		' live = Live Production 
		' dev = In Development
		' test = Testing 
		' alpha = Alpha Stage
		' beta = Beta Stage
		'==============================
		Public Shared VersionClass As verClass = verClass.dev
		' if site is being tested and developed. 
		Public Shared TestingSite As Boolean = False
		'########################################################## 

		Public Shared Years As String = "2008 - 2009"
		Public Shared CompanyName As String = "Informatics Research Institute (IRI)"
		Public Shared CompanyAddress As String = "http://iri.isu.edu"
		'########################################################## 		


#Region "Private Variables"
		Private _doM As String = "IRI"
		Private _userN As String = "iriwebsites"
		Private _passW As String = "XubeTrAga32ba7uP-&3EqutAbruC55"
		Private _Name As String
		Private _Title As String
		Private _SubTitle As String
		Private _Theme As String
		Private _AdminEmailAddress As String
		Private _ThumbnailMaxDimension As Integer = 100
        Private _Image_Error_FullPath As String = "App_Pics/error.jpg"
		Private _Image_Error_Type As String = "jpg"
		Private _FullImagePath As String = ""
		Private _Image_WaterMark_FullPath As String = ""
		Private _RemotePath As String = "\\IRISQL1\AnthroImages" ' dev = "\\RIO\Projects\Anthro"
		Private _SiteAddress As String

		'################### Email Settings ######################

		Private _EmailServerAddress As String
		Private _EmailUserName As String
		Private _EmailUserPassword As String
		Private _EmailServerPort As Integer


#End Region

#Region "Public Properties"
		Public Shared ReadOnly Property Version() As String
			Get
				Dim verStr As String
				verStr = VersionReleaseNumber.ToString & "." & _
				   VersionMajorNumber.ToString & "." & _
				   VersionMinorNumber.ToString
				'           If VersionNumber Mod (CLng(VersionNumber) \ 1) > 0 Then
				'verStr = VersionNumber.ToString
				'           Else
				'verStr = VersionNumber.ToString("F01")
				'           End If
				Return verStr & " | " & System.Enum.GetName(GetType(verClass), VersionClass) & " | " & VersionDate.ToString("d MMM yy")
			End Get
		End Property
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
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property AdministratorEmailAddress() As String
			Get
				Return _AdminEmailAddress
			End Get
			Set(ByVal value As String)
				_AdminEmailAddress = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property Title() As String
			Get
				Return _Title
			End Get
			Set(ByVal value As String)
				_Title = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property SubTitle() As String
			Get
				Return _SubTitle
			End Get
			Set(ByVal value As String)
				_SubTitle = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property Theme() As String
			Get
				Return _Theme
			End Get
			Set(ByVal value As String)
				_Theme = value
			End Set
		End Property
		''' <summary>
		''' TODO: Comment this
		''' </summary>
		''' <value></value>
		''' <returns>String</returns>
		''' <remarks></remarks>
		Public Property SiteAddress() As String
			Get
				Return _SiteAddress
			End Get
			Set(ByVal value As String)
				_SiteAddress = value
			End Set
		End Property
		Public Property ThumbnailMaxDimension() As Integer
			Get
				Return _ThumbnailMaxDimension
			End Get
			Set(ByVal value As Integer)
				_ThumbnailMaxDimension = value
			End Set
		End Property
		Public Property Image_Error_FullPath() As String
			Get
				Return "themes/" & Theme & "/" & _Image_Error_FullPath
			End Get
			Set(ByVal value As String)
				_Image_Error_FullPath = value
			End Set
		End Property
		Public Property Image_Error_Type() As String
			Get
				Return _Image_Error_Type
			End Get
			Set(ByVal value As String)
				_Image_Error_Type = value
			End Set
		End Property
		Public Property FullImagePath() As String
			Get
				Return _FullImagePath
			End Get
			Set(ByVal value As String)
				_FullImagePath = value
			End Set
		End Property
		Public Property Image_WaterMark_FullPath() As String
			Get
				Return _Image_WaterMark_FullPath
			End Get
			Set(ByVal value As String)
				_Image_WaterMark_FullPath = value
			End Set
		End Property
		Public Property RemotePath() As String
			Get
				If VersionClass = verClass.dev Then
					Return "\\RIO\Projects\Anthro"
				End If
				Return _RemotePath
			End Get
			Set(ByVal value As String)
				_RemotePath = value
			End Set
		End Property
		Public MustOverride ReadOnly Property MessagesCurrent() As List(Of SiteMessageBase)
		Public MustOverride ReadOnly Property Messages() As List(Of SiteMessageBase)
		Public ReadOnly Property ImpDomain() As String
			Get
				Return _doM
			End Get
		End Property
		Public ReadOnly Property ImpUserName() As String
			Get
				Return _userN
			End Get
		End Property
		Public ReadOnly Property ImpPassword() As String
			Get
				Return _passW
			End Get
		End Property
		Public Shared ReadOnly Property DatabaseType() As String
			Get
				If VersionClass = verClass.dev Then
					Return "dev"
				End If
				Return ""
			End Get
		End Property

		Public Property Email_ServerAddress() As String
			Get
				Return _EmailServerAddress
			End Get
			Set(ByVal value As String)
				_EmailServerAddress = value.Trim
			End Set
		End Property

		Public Property Email_Credentials_UserName() As String
			Get
				Return _EmailUserName
			End Get
			Set(ByVal value As String)
				_EmailUserName = value.Trim
			End Set
		End Property

		Public Property Email_Credientials_Password() As String
			Get
				Return _EmailUserPassword
			End Get
			Set(ByVal value As String)
				_EmailUserPassword = value.Trim
			End Set
		End Property

		Public Property Email_ServerPort() As Integer
			Get
				Return _EmailServerPort
			End Get
			Set(ByVal value As Integer)
				_EmailServerPort = value
			End Set
		End Property

#End Region

	End Class
End Namespace