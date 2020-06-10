Option Strict On
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Web
Public MustInherit Class SessionStateBase
	Public MustOverride Property CurrentUser() As Security.UserBase
	Public MustOverride Property ErrorMessages() As List(Of String)
	Public MustOverride Property SuccessMessages() As List(Of String)
	Public MustOverride Sub addError(ByVal value As String)
	Public MustOverride Sub addSuccess(ByVal value As String)
	Public MustOverride Sub addPermissionError()
	Public MustOverride Property History() As Stack(Of String)
	Public MustOverride Property SiteConfig() As Security.SiteConfigBase
	Public MustOverride Sub RemoveItemFromSession(ByVal stringID As String)
	Public MustOverride Property LastPageRequested() As String
	Public MustOverride Sub save(ByVal name As String, ByVal value As String)
	Public MustOverride Function load(ByVal name As String, Optional ByVal clearValue As Boolean = False) As String
End Class
