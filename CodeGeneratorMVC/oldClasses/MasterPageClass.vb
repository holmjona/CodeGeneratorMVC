Option Strict On

Imports System.Collections.Generic
Imports EnvDTE
Imports EnvDTE80
Imports System.ComponentModel

<Serializable()> Public Class MasterPageClass
    Private _fileName As String
    Private _MasterPageContents As New BindingList(Of MasterPageContent)
	Private _Name As String
	Private _TitleName As String
    Private _SubTitleName As String
    Private _PageInstructionsName As String
    Private _BodyName As String
    Public Property TitleName() As String
        Get
            Return _TitleName
        End Get
        Set(ByVal value As String)
            _TitleName = value
        End Set
    End Property
	Public Property SubTitleName() As String
		Get
			Return _SubTitleName
		End Get
		Set(ByVal value As String)
			_SubTitleName = value
		End Set
    End Property
    Public Property PageInstructionsName() As String
        Get
            Return _PageInstructionsName
        End Get
        Set(ByVal value As String)
            _PageInstructionsName = value
        End Set
    End Property
	Public Property BodyName() As String
		Get
			Return _BodyName
		End Get
		Set(ByVal value As String)
			_BodyName = value
		End Set
	End Property
    Public Property FileName() As String
        Get
            Return _fileName
        End Get
		Set(ByVal value As String)
			Dim tempName As String = value
			'Dim perLoc As Integer = value.LastIndexOf(".")
			Dim ext As String = System.IO.Path.GetExtension(tempName)
			If ext <> ".master" Then tempName &= ".master"
			_fileName = tempName
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
    Public Property MasterPageContents() As BindingList(Of MasterPageContent)
        Get
            Return _MasterPageContents
        End Get
        Set(ByVal value As BindingList(Of MasterPageContent))
            _MasterPageContents = value
        End Set
    End Property

    Public Shared Function getMasterPagesForProject(ByVal _applicationObject As DTE2) As List(Of MasterPageClass)
        Dim retList As New List(Of MasterPageClass)
        If _applicationObject IsNot Nothing AndAlso _applicationObject.Solution IsNot Nothing Then
            If _applicationObject.Solution.Projects.Count > 0 Then

                Dim myProject As Project = _applicationObject.Solution.Projects.Item(1)
                If myProject IsNot Nothing Then
                    StaticVariables.Instance.IsProjectOpen = True
                    For Each projItem As ProjectItem In myProject.ProjectItems
                        fillMasterPages(projItem, retList)
                    Next
                End If
            End If
        End If
        Return retList
    End Function
    Private Shared Sub fillMasterPages(ByVal myItem As ProjectItem, ByRef listOfMasterPageClasses As List(Of MasterPageClass))
        Try
            If myItem.Name.ToLower().Contains(".master") Then
                If Not myItem.Name.Contains(".master.vb") Then

                    Dim newMasterPage As New MasterPageClass
                    newMasterPage.FileName = myItem.Name
                    newMasterPage.Name = System.IO.Path.GetFileNameWithoutExtension(myItem.Name)
                    'MsgBox(myItem.Document.FullName)
                    Dim myValue As String = myItem.ContainingProject.FullName
                    newMasterPage.MasterPageContents = New BindingList(Of MasterPageContent)
                    FillMasterPageContent(newMasterPage, myValue & myItem.Name)
                    listOfMasterPageClasses.Add(newMasterPage)
                End If

                If myItem.Document IsNot Nothing Then
                    If myItem.Document.FullName.ToLower().Contains(".master") AndAlso Not myItem.Document.FullName.ToLower().Contains(".master.vb") Then
                        Dim newMasterPage As New MasterPageClass
                        newMasterPage.FileName = myItem.Document.FullName
                        'MsgBox(myItem.Document.FullName)
                        FillMasterPageContent(newMasterPage, myItem.Document.FullName)
                        listOfMasterPageClasses.Add(newMasterPage)
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
        If myItem.ProjectItems IsNot Nothing AndAlso myItem.ProjectItems.Count > 0 Then
            For Each projItem As ProjectItem In myItem.ProjectItems
                fillMasterPages(projItem, listOfMasterPageClasses)
            Next
        End If
    End Sub
    Private Shared Sub FillMasterPageContent(ByRef newMasterPage As MasterPageClass, ByVal FileName As String)
        If System.IO.File.Exists(FileName) Then
            Dim myRead As System.IO.StreamReader = System.IO.File.OpenText(FileName)
            Dim myString As String = myRead.ReadToEnd()
            myString = myString.ToLower()
            Dim indexOfCPH As Integer = myString.IndexOf("contentplaceholder")
            Dim contentPlaceHolderIndex As Integer = 0
            While indexOfCPH > -1
                'indexOfCPH = myString.IndexOf("contentplaceholder")
                myString = myString.Remove(0, indexOfCPH)
                myString = myString.Remove(0, myString.IndexOf("id="))
				'Dim newMasterPageContent As New MasterPageContent
				'newMasterPageContent.Name = myString.Substring(0, myString.IndexOf(" "))
                Dim strID As String = myString.Substring(0, myString.IndexOf(" "))
                strID = strID.Remove(0, 7)
                strID = strID.Remove(strID.Length - 1, 1)
                contentPlaceHolderIndex += 1
                If contentPlaceHolderIndex = 1 Then
                    newMasterPage.SubTitleName = strID
                ElseIf contentPlaceHolderIndex = 2 Then
                    newMasterPage.TitleName = strID
                ElseIf contentPlaceHolderIndex = 3 Then
                    newMasterPage.PageInstructionsName = strID
                ElseIf contentPlaceHolderIndex = 4 Then
                    newMasterPage.BodyName = strID
                End If
                'If strID.ToLower.Contains("subtitle") Then
                '	newMasterPage.SubTitleName = strID
                'ElseIf strID.ToLower.Contains("title") Then
                '	newMasterPage.TitleName = strID
                'ElseIf strID.ToLower.Contains("body") Then
                '	newMasterPage.BodyName = strID
                'End If
				'newMasterPageContent.Name = myString.Substring(0, myString.IndexOf(" "))
				indexOfCPH = myString.IndexOf("contentplaceholder")
				'newMasterPage.MasterPageContents.Add(newMasterPageContent)
			End While
        End If
    End Sub
    Public ReadOnly Property Self() As MasterPageClass
        Get
            Return Me
        End Get
    End Property
    Public Overrides Function ToString() As String
        Return _Name
    End Function
End Class
