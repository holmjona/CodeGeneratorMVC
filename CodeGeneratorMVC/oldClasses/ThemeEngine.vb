Imports System.Drawing

Public Class ThemeEngine
    Public Shared Sub SetRowRegularColors(ByRef back As Color, ByRef fore As Color)
        back = Color.White
        fore = Color.Black
    End Sub
    Public Shared Sub SetRowGoodColors(ByRef back As Color, ByRef fore As Color)
        back = Color.FromArgb(255, 230, 245, 230)
        fore = Color.Black
    End Sub
    Public Shared Sub SetRowErrorColors(ByRef back As Color, ByRef fore As Color)
        back = Color.FromArgb(255, 255, 230, 230)
        fore = Color.Black
    End Sub
    Public Shared Sub SetRowWarningColors(ByRef back As Color, ByRef fore As Color)
        back = Color.FromArgb(255, 255, 245, 230)
        fore = Color.Black
    End Sub

End Class
