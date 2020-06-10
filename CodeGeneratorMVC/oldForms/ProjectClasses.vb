Imports System.Windows
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq

Public Class ProjectClasses


    Private Sub ProjectClasses_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler StaticVariables.Instance.ListOfProjectClasses.AddingNew, AddressOf HandleProjectClassDataSource
        AddHandler dgvProjectClasses.RowsAdded, AddressOf HandleAddingNewRow
        setDataGrids()
        setLegendColors()
    End Sub
    Private Sub setLegendColors()
        Dim clrGoodBack, clrGoodFore, _
                clrWarnBack, clrWarnFore, _
                clrBadBack, clrBadFore As Color

        ThemeEngine.SetRowGoodColors(clrGoodBack, clrGoodFore)
        ThemeEngine.SetRowWarningColors(clrWarnBack, clrWarnFore)
        ThemeEngine.SetRowErrorColors(clrBadBack, clrBadFore)

        With lblGood
            .BackColor = clrGoodBack
            .ForeColor = clrGoodFore
        End With
        With lblWarning
            .BackColor = clrWarnBack
            .ForeColor = clrWarnFore
        End With
        With lblBad
            .BackColor = clrBadBack
            .ForeColor = clrBadFore
        End With

    End Sub
    Public Sub setDataGrids()
        dgvProjectClasses.DataSource = StaticVariables.Instance.ListOfProjectClasses
        cbclmBaseClasse.DataSource = StaticVariables.Instance.BaseClasses
        cbclmMasterPages.DataSource = StaticVariables.Instance.MasterPages
        cbclmNameSpace.DataSource = StaticVariables.Instance.NameSpaceNames
        cbclmDAL.DataSource = StaticVariables.Instance.DALs

    End Sub
#Region "Project Classes"
    Private Sub HandleAddingNewRow(ByVal sender As Object, ByVal e As DataGridViewRowsAddedEventArgs)

    End Sub

    Private Sub dgvProjectClasses_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProjectClasses.CellEndEdit
        SetRowProperties()
    End Sub
    Private Sub dgvProjectClasses_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs) Handles dgvProjectClasses.UserDeletingRow
        HandleDeletingRow(e)
    End Sub
    Private Sub dgvProjectClasses_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles dgvProjectClasses.DataError
        dgvProjectClasses.RefreshEdit()
        'System.Windows.MessageBox.Show(dgvProjectClasses.Columns(e.ColumnIndex).Name)
        'System.Windows.MessageBox.Show(e.Exception.ToString())
        Try
            If dgvProjectClasses.Rows(e.RowIndex).DataBoundItem IsNot Nothing Then
                Dim associatedobject As ProjectClass = CType(dgvProjectClasses.Rows(e.RowIndex).DataBoundItem, ProjectClass)
            End If
        Catch ex As Exception

        End Try
        'System.Windows.MessageBox.Show(associatedobject.TextVariable.ToString())

    End Sub
    Private Sub dgvProjectClasses_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvProjectClasses.CellContentClick
        If dgvProjectClasses.Columns(e.ColumnIndex).Name = "btnclmShowVariables" Then
            StaticVariables.Instance.SelectedProjectClass = CType(dgvProjectClasses.Rows(e.RowIndex).DataBoundItem, ProjectClass)
            Dim emp As New frmEditClassVariables
            emp.ShowDialog()
        End If
        'If dgvProjectClasses.Columns(e.ColumnIndex).Name = "cbxclmIsSelected" Then
        '    Dim myClasser As ProjectClass = CType(dgvProjectClasses.Rows(e.RowIndex).DataBoundItem, ProjectClass)
        '    myClasser.IsSelected = CType(dgvProjectClasses.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewCheckBoxCell).Selected
        'End If

    End Sub

    Private Sub HandleProjectClassDataSource(ByVal sender As Object, ByVal e As AddingNewEventArgs)
        If dgvProjectClasses.Rows.Count = StaticVariables.Instance.ListOfProjectClasses.Count Then
            StaticVariables.Instance.ListOfProjectClasses.RemoveAt(StaticVariables.Instance.ListOfProjectClasses.Count - 1)
        End If
    End Sub
    Private Sub HandleDeletingRow(ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        Dim msgAnswer As MsgBoxResult
        msgAnswer = MsgBox("Are you sure", MsgBoxStyle.YesNo, "Deleting Row")
        e.Cancel = msgAnswer = MsgBoxResult.No
    End Sub
    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        For Each pClass As ProjectClass In StaticVariables.Instance.ListOfProjectClasses
            pClass.IsSelected = True
        Next
    End Sub
    Private Sub btnEditSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditSelected.Click
        'lblSelectedClassTitle.Text = StaticVariables.Instance.SelectedProjectClass.Name.Capitalized
        StaticVariables.Instance.SelectedProjectClasses = New List(Of ProjectClass)
        For Each pClass As ProjectClass In StaticVariables.Instance.ListOfProjectClasses
            If pClass.IsSelected Then
                StaticVariables.Instance.SelectedProjectClasses.Add(pClass)
            End If
        Next
        Dim emp As New EditProjectClass
        emp.ShowDialog()
        'btnAddClassVariable.Enabled = True

    End Sub
    Private Sub btnDeselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeselectAll.Click
        For Each pClass As ProjectClass In StaticVariables.Instance.ListOfProjectClasses
            pClass.IsSelected = False
        Next
    End Sub
    Private Sub btnRemoveSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveSelected.Click
        Dim listToRemove As New List(Of ProjectClass)
        For Each pClass As ProjectClass In StaticVariables.Instance.ListOfProjectClasses
            If pClass.IsSelected Then
                listToRemove.Add(pClass)
            End If
        Next
        For Each pClass As ProjectClass In listToRemove
            StaticVariables.Instance.ListOfProjectClasses.Remove(pClass)
        Next
    End Sub

#End Region
    Public Sub SetRowProperties()
        Dim madeIthere As Boolean = False
        Try
            Dim listOfStrings As New List(Of String)
            For Each row As Forms.DataGridViewRow In dgvProjectClasses.Rows
                If row.IsNewRow Then Continue For
                Dim pClass As ProjectClass = StaticVariables.Instance.ListOfProjectClasses(row.Index)
                Dim comboCell As Forms.DataGridViewComboBoxCell = CType(dgvProjectClasses.Rows(row.Index).Cells("cbClmText"), DataGridViewComboBoxCell)

                If comboCell IsNot Nothing Then
                    comboCell.DataSource = pClass.ClassVariables

                    comboCell.ValueMember = "Self"
                    comboCell.DisplayMember = "Name"
                    comboCell.ValueType = GetType(ClassVariable)
                    If pClass.TextVariable Is Nothing Then
                        Dim textVariableFound As Boolean = False
                        Dim textVar As ClassVariable = Nothing
                        For Each cVariable As ClassVariable In pClass.ClassVariables
                            If textVariableFound Then Exit For
                            Dim nameAsLower As String = cVariable.Name.ToLower()
                            If nameAsLower.Contains("name") OrElse nameAsLower.Contains("title") _
                                OrElse nameAsLower.Contains("text") _
                                OrElse (nameAsLower.Contains("value") AndAlso cVariable.ParameterType.Name = "String") Then
                                textVar = cVariable
                                textVariableFound = True
                            ElseIf cVariable.ParameterType.Name.ToLower = "string" AndAlso textVar IsNot Nothing Then
                                textVar = cVariable
                            End If
                        Next
                        pClass.TextVariable = textVar
                        If pClass.TextVariable Is Nothing Then
                            pClass.TextVariable = pClass.ClassVariables.FirstOrDefault(Function(p) p.IsIDField)
                            comboCell.Flag()
                        End If
                    End If
                End If
                comboCell = CType(dgvProjectClasses.Rows(row.Index).Cells("cbClmValue"), DataGridViewComboBoxCell)
                If comboCell IsNot Nothing Then
                    comboCell.DataSource = pClass.ClassVariables
                    comboCell.ValueMember = "Self"
                    comboCell.DisplayMember = "Name"
                    comboCell.ValueType = GetType(ClassVariable)
                    If pClass.ValueVariable Is Nothing Then
                        For Each cVariable As ClassVariable In pClass.ClassVariables
                            Dim toLowerString As String = cVariable.Name.ToLower()
                            If toLowerString.CompareTo("id") = 0 Then
                                pClass.ValueVariable = cVariable
                            End If
                        Next
                    End If
                    If pClass.ValueVariable Is Nothing Then
                        pClass.ValueVariable = pClass.TextVariable
                        comboCell.Flag()
                    End If
                End If
                Dim backColor, foreColor As Color
                ThemeEngine.SetRowGoodColors(backColor, foreColor)
                Dim correspondingObject As ProjectClass = CType(row.DataBoundItem, ProjectClass)
                If correspondingObject IsNot Nothing Then
                    If Not validateRow(correspondingObject, listOfStrings) Then
                        ThemeEngine.SetRowErrorColors(backColor, foreColor)
                    End If
                    If correspondingObject.IsAssociateEntitiy Then
                        ThemeEngine.SetRowWarningColors(backColor, foreColor)
                    End If
                End If
                For Each c As DataGridViewCell In row.Cells
                    c.Style.BackColor = backColor
                    c.Style.ForeColor = foreColor
                    If c.isFlagged Then
                        c.Style.ForeColor = Color.Gray
                    End If
                Next
            Next
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.ToString())
        End Try

    End Sub
    Private Function validateRow(ByVal pClass As ProjectClass, ByRef listOfStrings As List(Of String)) As Boolean
        If pClass.DALClassVariable Is Nothing OrElse pClass.NameSpaceVariable Is Nothing OrElse pClass.MasterPage Is Nothing OrElse pClass.TextVariable Is Nothing OrElse pClass.ValueVariable Is Nothing Then
            Return False
        End If

        For Each classVar As ClassVariable In pClass.ClassVariables
            If Not frmEditClassVariables.validateClassVariable(classVar) Then
                Return False
            End If
        Next
        If listOfStrings.Contains(pClass.NameString.ToLower()) Then
            Return False
        End If
        listOfStrings.Add(pClass.NameString.ToLower())
        Return True
    End Function
    Private Sub dgvDataTypes_FlagRows(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvProjectClasses.RowsAdded
        SetRowProperties()
    End Sub


End Class
