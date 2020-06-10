Imports System.Windows.Forms
Imports System.Runtime.CompilerServices

Public Module ObjectExtensions
    <Extension()> _
    Public Sub Flag(ByVal x As DataGridViewCell)
        x.Tag = True
    End Sub
    Public Sub UnFlag(ByVal x As DataGridViewCell)
        x.Tag = False
    End Sub
    <Extension()> _
    Public Function isFlagged(ByVal x As DataGridViewCell) As Boolean
        Return CBool(x.Tag)
    End Function
    <Extension()> _
    Public Function Count(ByVal st As String, ch As Char) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In st
            If c = ch Then cnt += 1
        Next
        Return cnt
    End Function
End Module
