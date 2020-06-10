Public NotInheritable Class splProgress

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).


    Private Sub splProgress_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.  
    End Sub

    Public Overloads Sub Show(ByVal txt As String)
        MyBase.Show()
        Label1.Text = txt
        Label1.Update()
        pbWaiting.Value = 0
        tmrBar.Enabled = True
        tmrBar.Interval = 50
        tmrBar.Start()
    End Sub

    Private Sub timerTick(ByVal sender As Object, ByVal e As EventArgs) Handles tmrBar.Tick
        pbWaiting.Increment(1)
        pbWaiting.Update()
    End Sub

    Public Shadows Sub Hide()
        tmrBar.Stop()
        tmrBar.Enabled = False
        MyBase.Hide()
    End Sub

End Class
