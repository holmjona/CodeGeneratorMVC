Imports System.Windows.Forms

Public Class Test
    Inherits UserControl
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        MyBase.OnLoad(e)
        If StaticVariables.Instance.ApplicationObject Is Nothing Then

        End If
		'Dim eHost As New ElementHost

    End Sub

End Class
