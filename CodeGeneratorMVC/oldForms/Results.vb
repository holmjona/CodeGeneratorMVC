Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Windows.Documents

Public Class Results
    Private ctrolKeyPressed As Boolean = False
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub New(ByVal Text As Dictionary(Of String, Dictionary(Of String, String)))
        InitializeComponent()
        'txtResults.Text = Text
        tbResults.Controls.Clear()
        For Each pair As KeyValuePair(Of String, Dictionary(Of String, String)) In Text
            Dim tp As New TabPage
            tp.Text = pair.Key
            tbResults.Controls.Add(tp)
            Dim tb As New TabControl
            tb.Size = New System.Drawing.Size(800, 600)
            tp.Controls.Add(tb)
            'ContextMenuStrip
            Dim cxt As New ContextMenuStrip()
            Dim tms As New ToolStripMenuItem("Remove")
            AddHandler tms.Click, AddressOf RemoveSelectedTabToolStripMenuItem_Click
            cxt.Items.Add(tms)
            tms.Tag = tp
            tp.ContextMenuStrip = cxt

            For Each textElement As KeyValuePair(Of String, String) In pair.Value
                tp = New TabPage
                'ContextMenuStrip
                cxt = New ContextMenuStrip()
                tms = New ToolStripMenuItem("Remove")
                AddHandler tms.Click, AddressOf RemoveSelectedTabToolStripMenuItem_Click
                cxt.Items.Add(tms)
                tms.Tag = tp
                tp.ContextMenuStrip = cxt



                tp.Text = textElement.Key
                Dim rbx As New RichTextBox
                rbx.Text = textElement.Value
                rbx.Size = New System.Drawing.Size(800, 600)
                tp.Controls.Add(rbx)
                tb.TabPages.Add(tp)
            Next

        Next

    End Sub
    Private Sub setTextColorForVisualBasicFile(ByRef rbx As RichTextBox, ByVal text As String)




    End Sub
    ''' <summary>
    ''' http://www.c-sharpcorner.com/uploadfile/duncanharris/syntaxhighlightinrichtextboxp112012005050840am/syntaxhighlightinrichtextboxp1.aspx
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ParseLine(ByVal line As String, ByRef rbx As RichTextBox)
        Dim r As New Regex("([ \\t{}():;])")
        Dim tokens() As String = r.Split(line)
        Dim keywords() As String = {"Public", "Sub", "Imports", "Shared", "Class"}

        For Each token As String In tokens
            rbx.SelectedText = token
            rbx.SelectionColor = Drawing.Color.Black
            rbx.SelectionFont = New Drawing.Font("Courier New", 10, System.Drawing.FontStyle.Regular)
            For Each keyword As String In keywords
                If token = keyword Then
                    rbx.SelectionColor = Drawing.Color.Blue
                    rbx.SelectionFont = New Drawing.Font("Courier New", 10, Drawing.FontStyle.Regular)
                    Exit For
                End If
            Next
        Next

    End Sub
    Protected Sub txtResults_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.ControlKey Then
            ctrolKeyPressed = True
        End If
        If e.KeyCode = Keys.A And ctrolKeyPressed Then
            Dim txtBox As TextBox = CType(sender, TextBox)
            txtBox.SelectAll()
        End If
    End Sub
    Protected Sub txtResults_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.ControlKey Then
            ctrolKeyPressed = False
        End If
    End Sub

    Private Sub RemoveSelectedTabToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim msgAnswer As MsgBoxResult
        msgAnswer = MsgBox("Are you sure", MsgBoxStyle.YesNo, "Deleting Page")
        If msgAnswer = MsgBoxResult.Yes Then
            Dim tsMenuStrip As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            Dim myPage As TabPage = CType(tsMenuStrip.Tag, TabPage)
            CType(myPage.Parent, TabControl).TabPages.Remove(myPage)
        End If

    End Sub

    'Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim tsMenuStrip As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
    '    Dim cxt As ContextMenuStrip = CType(tsMenuStrip.Owner, ContextMenuStrip)
    '    Dim msgAnswer As MsgBoxResult
    '    msgAnswer = MsgBox("Are you sure", MsgBoxStyle.YesNo, "Deleting Page")
    '    If msgAnswer = MsgBoxResult.Yes Then
    '        tbResults.TabPages.Remove(tbResults.SelectedTab)
    '    End If
    'End Sub
End Class