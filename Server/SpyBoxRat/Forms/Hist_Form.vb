Imports System.IO
Imports System.Text

Public Class Hist_Form
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Application.ExitThread()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub UcBtnExt1_BtnClick(sender As Object, e As EventArgs) Handles UcBtnExt1.BtnClick
        '  Using S As New SaveFileDialog
        'If S.ShowDialog = DialogResult.OK Then

        Dim H As New StringBuilder
        For Each i As ListViewItem In ListView1.Items
            H.AppendLine("Application: " & i.Text & vbNewLine & "Title Page: " & i.SubItems(1).Text & vbNewLine & "URL: " & i.SubItems(2).Text & vbNewLine & "Last Visit: " & i.SubItems(3).Text & vbNewLine)
        Next
        Static J As New Random
        If IO.Directory.Exists(Application.StartupPath & "\History") Then
            IO.Directory.CreateDirectory(Application.StartupPath & "\History")
            IO.File.WriteAllText(Application.StartupPath & "\History\History_" & Label1.Text.Replace(":", "_") & "_" & Date.Now.ToString.Replace(":", "_") & J.Next(0, 99) & ".txt", "History From : " & Label1.Text & vbNewLine & vbNewLine & H.ToString & vbNewLine)

        Else
            IO.Directory.CreateDirectory(Application.StartupPath & "\History")
            IO.File.WriteAllText(Application.StartupPath & "\History\History_" & Label1.Text.Replace(":", "_") & "_" & Date.Now.ToString.Replace(":", "_") & J.Next(0, 99) & ".txt", "History From : " & Label1.Text & vbNewLine & vbNewLine & H.ToString & vbNewLine)
        End If



        '  End If
        ' End Using
    End Sub
End Class