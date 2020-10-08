Imports System.Text

Public Class LogsForm

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Hide()

    End Sub

    Private Sub SaveLogsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveLogsToolStripMenuItem.Click

        Using LGFile As New SaveFileDialog

            With LGFile

                .AddExtension = True

                .DefaultExt = ".txt"

                If LGFile.ShowDialog = DialogResult.OK Then

                    Dim J As New StringBuilder

                    For Each i As ListViewItem In LOG_AeroListView2.Items

                        J.Append(i.Text & "       " & i.SubItems(1).Text & vbCrLf)

                    Next

                    IO.File.WriteAllText(LGFile.FileName, J.ToString)

                End If

            End With

        End Using

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If Me.WindowState = FormWindowState.Maximized Then

            Me.WindowState = FormWindowState.Normal

        Else

            Me.WindowState = FormWindowState.Minimized

        End If

    End Sub

End Class