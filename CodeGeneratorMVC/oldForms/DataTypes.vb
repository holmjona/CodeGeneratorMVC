Imports System.Windows.Forms
Imports System.Drawing

Public Class DataTypes

    Private Sub DataTypes_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Leave

    End Sub
    Public Sub setDataGrids()
        dgvDataTypes.DataSource = StaticVariables.Instance.DataTypes
    End Sub
#Region "DataTypes"

    Private Sub dgvDataTypes_UserDeletingRow(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        HandleDeletingRow(e)
    End Sub
    Private Sub dgvDataTypes_FlagRows(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvDataTypes.RowsAdded
        Dim dgv As DataGridView = CType(sender, DataGridView)
        For Each ra As DataGridViewRow In dgv.Rows
            Dim dt As DataType = StaticVariables.Instance.DataTypes(ra.Index)
            If dt.IsPrimitive Then
                'ra.DefaultCellStyle.BackColor = Color.LightGray
                If Not ra.ReadOnly Then
                    For Each c As DataGridViewCell In ra.Cells
                        c.Style.BackColor = Color.LightGray
                        c.Style.ForeColor = Color.DarkGray
                        'If c.ColumnIndex = 1 Then
                        '	MsgBox(c.OwningColumn.CellType.ToString)
                        'End If
                        'MsgBox(c.FormattedValue.ToString)
                        'If c.FormattedValueType Is GetType(ComboBox) Then
                        '	MsgBox("found it")
                        'End If
                    Next
                    ra.ReadOnly = True
                End If
            Else
                For Each c As DataGridViewCell In ra.Cells
                    c.Style.BackColor = Color.White
                    c.Style.ForeColor = Color.Black
                Next
            End If
        Next
    End Sub

    Private Sub HandleDeletingRow(ByVal e As System.Windows.Forms.DataGridViewRowCancelEventArgs)
        Dim msgAnswer As MsgBoxResult
        msgAnswer = MsgBox("Are you sure", MsgBoxStyle.YesNo, "Deleting Row")
        e.Cancel = msgAnswer = MsgBoxResult.No
    End Sub

#End Region
End Class
