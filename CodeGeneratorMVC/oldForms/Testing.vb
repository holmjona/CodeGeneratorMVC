Imports System.Collections.Generic
Imports System.Windows.Forms

Public Class Testing

    Private Sub Testing_Load(sender As Object, e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub testAliases()
        Dim lstTests As New List(Of String)
        lstTests.Add("MONEY")
        lstTests.Add("newname")
        lstTests.Add("newnamE")
        lstTests.Add("NewName")
        lstTests.Add("^New^Name")
        lstTests.Add("^new^name")
        lstTests.Add("New Name")
        lstTests.Add("^New ^Name")
        lstTests.Add("^new ^name")

        dgvNameAliases.Columns.Add(getColumn("Original"))
        dgvNameAliases.Columns.Add(getColumn("Plural"))
        dgvNameAliases.Columns.Add(getColumn("PluralCap"))

        For Each s As String In lstTests
            Dim row As New DataGridViewRow
            row.Cells.Add(getCell(s))
            Dim na As New IRICommonObjects.Words.NameAlias(s)
            row.Cells.Add(getCell(na.Plural))
            row.Cells.Add(getCell(na.PluralAndCapitalized))
            dgvNameAliases.Rows.Add(row)
        Next
    End Sub
    Private Function getColumn(text As String) As DataGridViewColumn
        Dim dvC As New DataGridViewColumn
        dvC.CellTemplate = New DataGridViewTextBoxCell
        dvC.HeaderText = text
        Return dvC
    End Function
    Private Function getCell(text As String) As DataGridViewTextBoxCell
        Dim retCell As New DataGridViewTextBoxCell
        retCell.Value = text
        Return retCell
    End Function

    Private Sub Testing_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        testAliases()
    End Sub
End Class