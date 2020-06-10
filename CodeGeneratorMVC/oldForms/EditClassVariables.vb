Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmEditClassVariables

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        dgvClassVariables.DataSource = StaticVariables.Instance.SelectedProjectClass.ClassVariables
        lblSelectedClassTitle.Text = StaticVariables.Instance.SelectedProjectClass.Name.Capitalized

        ParameterTypeDataGridViewTextBoxColumn.DataSource = StaticVariables.Instance.DataTypes
    End Sub
    Private Sub dgvClassVariables_CellContentClick_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvClassVariables.CellContentClick
        If dgvClassVariables.Columns(e.ColumnIndex).Name = "EditColumn" Then
            StaticVariables.Instance.ClassVariableToEdit = StaticVariables.Instance.SelectedProjectClass.ClassVariables(e.RowIndex)
            'Dim ecv As New EditClassVariable
            'ecv.ShowDialog()
        ElseIf dgvClassVariables.Columns(e.ColumnIndex).Name = "RemoveColumn" Then
            StaticVariables.Instance.SelectedProjectClass.ClassVariables.RemoveAt(e.RowIndex)
        End If
    End Sub
    Private Sub dgvClassVariables_DataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        'dgvProjectClasses.RefreshEdit()
        'MessageBox.Show(e.Exception.ToString())

    End Sub
    'Private Sub dgvClassVariables_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
    '	If dgvClassVariables.Columns(e.ColumnIndex).Name = "EditColumn" Then
    '		StaticVariables.Instance.ClassVariableToEdit = StaticVariables.Instance.SelectedProjectClasses(0).ClassVariables(e.RowIndex)
    '		Dim editClassVariableForm As New EditClassVariable
    '	ElseIf dgvClassVariables.Columns(e.ColumnIndex).Name = "RemoveColumn" Then
    '		StaticVariables.Instance.SelectedProjectClasses(0).ClassVariables.RemoveAt(e.RowIndex)
    '	End If
    'End Sub


    Private Sub ClassVariableBindingSource1_ListChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ListChangedEventArgs) Handles ClassVariableBindingSource1.ListChanged
        For Each cVariable As ClassVariable In StaticVariables.Instance.SelectedProjectClass.ClassVariables
            If cVariable.ParentClass Is Nothing Then
                cVariable.ParentClass = StaticVariables.Instance.SelectedProjectClass
            End If
        Next
    End Sub
    Public Sub setRowProperties()

        Dim madeIthere As Boolean = False
        Try

            For Each row As Forms.DataGridViewRow In dgvClassVariables.Rows
                If row.IsNewRow Then Continue For
                Dim correspondingObject As ClassVariable = CType(row.DataBoundItem, ClassVariable)
                Dim backColor, foreColor As Color
                ThemeEngine.SetRowGoodColors(backColor, foreColor)
                If correspondingObject IsNot Nothing Then
                    If Not validateClassVariable(correspondingObject) Then
                        ThemeEngine.SetRowErrorColors(backColor, foreColor)
                    End If
                    For Each c As DataGridViewCell In row.Cells
                        c.Style.BackColor = backColor
                        c.Style.ForeColor = foreColor
                    Next
                End If
            Next
        Catch ex As Exception
            System.Windows.MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub dgvClassVariables_FlagRows(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvClassVariables.RowsAdded
        setRowProperties()
    End Sub
    Public Shared Function validateClassVariable(ByVal classVar As ClassVariable) As Boolean
        If classVar.ParameterType Is Nothing OrElse String.IsNullOrEmpty(classVar.Name) Then
            Return False
        End If
        Return True
    End Function

    
End Class