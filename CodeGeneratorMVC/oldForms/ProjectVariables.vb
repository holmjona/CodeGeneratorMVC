Imports System.ComponentModel
Imports System.Windows
Imports System.Collections.Generic

Public Class ProjectVariables
    Private Sub dgvMasterPages2_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMasterPages.CellContentClick
        If dgvMasterPages.Columns(e.ColumnIndex).Name = "EditMasterPageColumn" Then
            StaticVariables.Instance.SelectedMasterPage = StaticVariables.Instance.MasterPages(e.RowIndex)
            'Dim emp As New EditMasterPage
            'emp.ShowDialog()
        ElseIf dgvMasterPages.Columns(e.ColumnIndex).Name = "RemoveMasterPageColumn" Then
            StaticVariables.Instance.MasterPages.RemoveAt(e.RowIndex)
        ElseIf dgvMasterPages.Columns(e.ColumnIndex).Name = "btnSelectMasterPage" Then
            StaticVariables.Instance.SelectedMasterPage = StaticVariables.Instance.MasterPages(e.RowIndex)
            'dgvMasterPageContents.DataSource = StaticVariables.Instance.SelectedMasterPage.MasterPageContents
            If StaticVariables.Instance.SelectedMasterPage.Name = "" _
            OrElse StaticVariables.Instance.SelectedMasterPage.FileName = "" Then
                MsgBox("Please enter a name before you edit Place Holders.", MsgBoxStyle.Exclamation, "Master Page Placeholders")
            Else
                txtcphTitle.Text = StaticVariables.Instance.SelectedMasterPage.TitleName
                txtcphSubTitle.Text = StaticVariables.Instance.SelectedMasterPage.SubTitleName
                txtcphBody.Text = StaticVariables.Instance.SelectedMasterPage.BodyName
                txtcphPageInstructions.Text = StaticVariables.Instance.SelectedMasterPage.PageInstructionsName
                pnlPlaceHoldersForSelectedMasterPage.Visible = True
            End If
            If txtcphBody.Text.Trim = "" Then
                txtcphBody.Text = "Body"
                txtcphPageInstructions.Text = "PageInstructions"
                txtcphSubTitle.Text = "SubTitle"
                txtcphTitle.Text = "Title"
            End If

        End If

    End Sub

    Private Sub ProjectVariables_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetDataGrids()
        SetToolTips()
    End Sub
    Public Sub SetDataGrids()
        dgvMasterPages.DataSource = StaticVariables.Instance.MasterPages
        dgvBaseClasses.DataSource = StaticVariables.Instance.BaseClasses
        'dgvEditOnlyStrings.DataSource = StaticVariables.Instance.EditOnlyConnectionStrings
        dgvReadOnlyStrings.DataSource = StaticVariables.Instance.ConnectionStrings
        dgvNameSpaces.DataSource = StaticVariables.Instance.NameSpaceNames
        dgvDALs.DataSource = StaticVariables.Instance.DALs
        ReadOnlyConnectionStringDataGridViewTextBoxColumn1.DataSource = StaticVariables.Instance.ConnectionStrings
        EditOnlyConnectionstringDataGridViewTextBoxColumn1.DataSource = StaticVariables.Instance.ConnectionStrings
        DataGridViewComboBoxColumn1.DataSource = StaticVariables.Instance.NameSpaceNames
    End Sub

    Private Sub btnSavePlaceholders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePlaceholders.Click
        StaticVariables.Instance.SelectedMasterPage.TitleName = txtcphTitle.Text
        StaticVariables.Instance.SelectedMasterPage.SubTitleName = txtcphSubTitle.Text
        StaticVariables.Instance.SelectedMasterPage.BodyName = txtcphBody.Text
        StaticVariables.Instance.SelectedMasterPage.PageInstructionsName = txtcphPageInstructions.Text
        pnlPlaceHoldersForSelectedMasterPage.Visible = False
    End Sub
    Private Sub HandleDeletingRow(ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        Dim msgAnswer As MsgBoxResult
        msgAnswer = MsgBox("Are you sure", MsgBoxStyle.YesNo, "Deleting Row")
        e.Cancel = msgAnswer = MsgBoxResult.No
    End Sub

    Private Sub dgvMasterPages_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvMasterPages.UserDeletingRow
        HandleDeletingRow(e)
    End Sub
    Private Sub dgvBaseClasses_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvBaseClasses.UserDeletingRow
        HandleDeletingRow(e)
    End Sub

    Private Sub dgvDALs_CellLeave(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDALs.CellLeave
        '   dgvDALs_LostFocus(sender, Nothing)
    End Sub

    Private Sub dgvDALs_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvDALs.DataError
        'MessageBox.Show(dgvDALs.Columns(e.ColumnIndex).Name)
        'MessageBox.Show(e.Exception.ToString())
    End Sub
    Private Sub dgvDALs_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvDALs.UserDeletingRow
        HandleDeletingRow(e)
    End Sub
    Private Sub dgvEditOnlyStrings_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        HandleDeletingRow(e)
    End Sub
    Private Sub dgvReadOnlyStrings_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvReadOnlyStrings.UserDeletingRow
        HandleDeletingRow(e)
    End Sub

    Private Sub dgvNameSpaces_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvNameSpaces.DataError
        MessageBox.Show(e.Exception.ToString())
    End Sub

    Private Sub dgvNamespaces_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvNameSpaces.UserDeletingRow
        HandleDeletingRow(e)
    End Sub

    Private Sub doSomething(ByVal sender As Object, ByVal e As Forms.DataGridViewRowEventArgs) Handles dgvMasterPages.UserAddedRow, dgvDALs.UserAddedRow ', dgvMasterPages.CellLeave
        If StaticVariables.Instance.MasterPages.Count > 0 Then
            lblMasterPages.ForeColor = CodeGeneratorForm.DefaultForeColor
        End If
        If StaticVariables.Instance.DALs.Count > 0 Then
            lblDataAccessLayers.ForeColor = CodeGeneratorForm.DefaultForeColor
        End If
    End Sub

    Private Sub dgvDALs_LostFocus(sender As Object, e As System.EventArgs) _
        Handles dgvDALs.LostFocus, dgvMasterPages.LostFocus, dgvNameSpaces.LostFocus, dgvBaseClasses.LostFocus, dgvReadOnlyStrings.LostFocus

        Dim dgv As Forms.DataGridView = CType(sender, Forms.DataGridView)
        Dim nameColumnIndex As Integer = -1
        For Each col As Forms.DataGridViewColumn In dgv.Columns
            If col.HeaderText.ToLower = "name" Then
                nameColumnIndex = col.Index
                Exit For
            End If
        Next
        If nameColumnIndex < 0 Then
            For Each col As Forms.DataGridViewColumn In dgv.Columns
                If col.HeaderText.ToLower.Contains("name") Then
                    nameColumnIndex = col.Index
                    Exit For
                End If
            Next
        End If
        If nameColumnIndex >= 0 Then
            Dim rowsToRemove As New List(Of Forms.DataGridViewRow)
            For Each rw As Forms.DataGridViewRow In dgv.Rows
                Dim cel As Forms.DataGridViewCell = rw.Cells(nameColumnIndex)
                If cel.ValueType = GetType(String) Then
                    Dim val As String = CType(cel.Value, String)
                    If val Is Nothing OrElse val.Trim = "" Then
                        If Not rw.IsNewRow Then
                            rowsToRemove.Add(rw)
                        End If
                    End If
                End If
            Next
            For Each rw As Forms.DataGridViewRow In rowsToRemove
                dgv.Rows.Remove(rw)
            Next
            dgv.Refresh()
        End If
    End Sub

    Private Sub lblHelp_Enter(sender As System.Object, e As System.EventArgs) _
                    Handles lblHelpNameSpaces.MouseEnter, lblHelpDALs.MouseEnter, lblHelpBaseClasses.MouseEnter, _
                     lblHelpDatabaseVariables.MouseEnter, lblHelpMasterPages.MouseEnter
        Dim lbl As Forms.Label
        lbl = CType(sender, Forms.Label)
        lbl.BackColor = Drawing.Color.DarkBlue
    End Sub
    Private Sub lblHelp_Hover(sender As System.Object, e As System.EventArgs) _
                    Handles lblHelpNameSpaces.MouseHover, lblHelpDALs.MouseHover, lblHelpBaseClasses.MouseHover, _
                     lblHelpDatabaseVariables.MouseHover, lblHelpMasterPages.MouseHover

    End Sub
    Private Sub lblHelp_Leave(sender As System.Object, e As System.EventArgs) _
                    Handles lblHelpNameSpaces.MouseLeave, lblHelpDALs.MouseLeave, lblHelpBaseClasses.MouseLeave, _
                     lblHelpDatabaseVariables.MouseLeave, lblHelpMasterPages.MouseLeave
        Dim lbl As Forms.Label
        lbl = CType(sender, Forms.Label)
        lbl.BackColor = Drawing.Color.CornflowerBlue
    End Sub

    Private Sub SetToolTips()
        Dim helpText As String = "To delete a row, delete the value in the ""Name"" column."""
        ttDelete.ReshowDelay = 5
        ttDelete.SetToolTip(lblHelpNameSpaces, helpText)
        ttDelete.SetToolTip(lblHelpDALs, helpText)
        ttDelete.SetToolTip(lblHelpBaseClasses, helpText)
        ttDelete.SetToolTip(lblHelpDatabaseVariables, helpText)
        ttDelete.SetToolTip(lblHelpMasterPages, helpText)
    End Sub

End Class
