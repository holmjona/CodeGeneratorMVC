Imports System.Windows.Forms
Imports System.Collections.Generic

Public Class EditProjectClass

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If StaticVariables.Instance.SelectedProjectClasses.Count = 0 And StaticVariables.Instance.SelectedProjectClass IsNot Nothing Then
            StaticVariables.Instance.SelectedProjectClasses.Add(StaticVariables.Instance.SelectedProjectClass)
        End If
        If StaticVariables.Instance.SelectedProjectClass Is Nothing Then
            StaticVariables.Instance.SelectedProjectClass = New ProjectClass()
            StaticVariables.Instance.ListOfProjectClasses.Add(StaticVariables.Instance.SelectedProjectClass)
            StaticVariables.Instance.SelectedProjectClasses.Add(StaticVariables.Instance.SelectedProjectClass)
        End If
        For Each pClass As ProjectClass In StaticVariables.Instance.SelectedProjectClasses
            'pClass.ReadOnlyConnectionString = CType(cbReadOnlyStrings.SelectedItem, ProjectVariable)
            'pClass.EditOnlyConnectionstring = CType(cbEditOnlyStrings.SelectedItem, ProjectVariable)
            pClass.MasterPage = CType(cbMasterPages.SelectedItem, MasterPageClass)
            pClass.BaseClass = CType(cbBaseClasses.SelectedItem, ProjectVariable)
            pClass.NameSpaceVariable = CType(cbNameSpaces.SelectedItem, ProjectVariable)
            pClass.DALClassVariable = CType(cbDALs.SelectedItem, DALClass)
        Next
        StaticVariables.Instance.SelectedProjectClasses = New List(Of ProjectClass)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fillForm()
        cbBaseClasses.DataSource = StaticVariables.Instance.BaseClasses
        cbMasterPages.DataSource = StaticVariables.Instance.MasterPages
        cbDALs.DataSource = StaticVariables.Instance.DALs
        cbNameSpaces.DataSource = StaticVariables.Instance.NameSpaceNames
    End Sub
    Private Sub fillForm()
        For Each value As ProjectVariable In StaticVariables.Instance.BaseClasses
            cbBaseClasses.Items.Add(value)
        Next
        cbBaseClasses.Items.Add("other")
        For Each value As MasterPageClass In StaticVariables.Instance.MasterPages
            cbMasterPages.Items.Add(value)
        Next
        If StaticVariables.Instance.SelectedProjectClass IsNot Nothing And StaticVariables.Instance.SelectedProjectClasses.Count < 1 Then
            For Each mp As MasterPageClass In StaticVariables.Instance.MasterPages
                If mp.FileName = StaticVariables.Instance.SelectedProjectClass.MasterPage.FileName Then
                    cbMasterPages.SelectedItem = mp
                End If
            Next
            If StaticVariables.Instance.BaseClasses.Contains(StaticVariables.Instance.SelectedProjectClass.BaseClass) Then
                cbBaseClasses.SelectedItem = StaticVariables.Instance.SelectedProjectClass.BaseClass
            End If
        End If
    End Sub

	'Private Sub btnAddReadOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'    Dim eRead As New EditReadOnlyConnectionString
	'    If eRead.ShowDialog = Windows.Forms.DialogResult.OK Then

	'    End If
	'End Sub

	'Private Sub btnAddEditOnly_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
	'    Dim eEdit As New EditEditOnlyConnectionString
	'    If eEdit.ShowDialog = Windows.Forms.DialogResult.OK Then

	'    End If
	'End Sub

	'Private Sub btnAddMasterPage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddMasterPage.Click
	'    Dim eMaster As New EditMasterPage
	'    If eMaster.ShowDialog = Windows.Forms.DialogResult.OK Then

	'    End If
	'End Sub

	'Private Sub btnAddBaseClass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBaseClass.Click
	'    Dim eBase As New EditBaseClass
	'    If eBase.ShowDialog = Windows.Forms.DialogResult.OK Then

	'    End If
	'End Sub

	'Private Sub btnAddNameSpace_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNameSpace.Click
	'    Dim eNamespace As New EditNameSpace
	'    If eNamespace.ShowDialog = Windows.Forms.DialogResult.OK Then

	'    End If
	'End Sub

	'Private Sub btnAddDAL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddDAL.Click
	'    Dim eDAL As New EditDAL
	'    If eDAL.ShowDialog = Windows.Forms.DialogResult.OK Then

	'    End If
	'End Sub
End Class
