Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Text

Public Class FI

    ''THX to pinvoke.net for those implementations of native functions !
    Public Enum FINDEX_INFO_LEVELS
        FindExInfoStandard = 0
        FindExInfoBasic = 1
    End Enum
    Public Enum FINDEX_SEARCH_OPS
        FindExSearchNameMatch = 0
        FindExSearchLimitToDirectories = 1
        FindExSearchLimitToDevices = 2
    End Enum

    <DllImport("kernel32.dll", CharSet:=CharSet.Auto)>
    Public Shared Function FindFirstFileExW(ByVal lpFileName As String, ByVal fInfoLevelId As FINDEX_INFO_LEVELS, ByRef lpFindFileData As WIN32_FIND_DATA, ByVal fSearchOp As FINDEX_SEARCH_OPS, lpSearchFilter As Int32, dwAdditionalFlags As Integer) As Int32
    End Function

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Structure WIN32_FIND_DATA
        Public dwFileAttributes As UInteger
        Public ftCreationTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public ftLastAccessTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public ftLastWriteTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public nFileSizeHigh As UInteger
        Public nFileSizeLow As UInteger
        Public dwReserved0 As UInteger
        Public dwReserved1 As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> Public cFileName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
    End Structure
    ''

    Public Shared Async Sub FI(ByVal k As TcpClient, ByVal Path As String)

        Dim INFO As New StringBuilder
        Dim n As New WIN32_FIND_DATA
        Dim i As IntPtr = FindFirstFileExW(Path, FINDEX_INFO_LEVELS.FindExInfoBasic, n, FINDEX_SEARCH_OPS.FindExSearchNameMatch, 0, 1)


        ''https://snipplr.com/view/32409/filetime-to-datetime
        Dim ftCreationTime As Long = ((CLng(Math.Truncate(n.ftCreationTime.dwHighDateTime))) << 32) + n.ftCreationTime.dwLowDateTime

        Dim dftCreationTimete As Date = Date.FromFileTime(ftCreationTime)


        Dim ftLastWriteTime As Long = ((CLng(Math.Truncate(n.ftLastWriteTime.dwHighDateTime))) << 32) + n.ftLastWriteTime.dwLowDateTime

        Dim ftLastWriteTimete As Date = Date.FromFileTime(ftLastWriteTime)


        INFO.Append(dftCreationTimete & "||" & ftLastWriteTimete & "||" & Numeric2Bytes(n.nFileSizeLow) & "||" & Path & "|FI|")

        Dim Data As Byte() = Encoding.Default.GetBytes(INFO.ToString)


        Await k.GetStream.WriteAsync(Data, 0, Data.Length)
    End Sub






















    ''those functions come from : http://www.vb-helper.com/howto_net_format_bytes_big2.html
    Shared Function Numeric2Bytes(ByVal b As Double) As String
        Dim bSize(8) As String
        Dim i As Integer

        bSize(0) = "Bytes"
        bSize(1) = "KB" 'Kilobytes
        bSize(2) = "MB" 'Megabytes
        bSize(3) = "GB" 'Gigabytes
        bSize(4) = "TB" 'Terabytes
        bSize(5) = "PB" 'Petabytes
        bSize(6) = "EB" 'Exabytes
        bSize(7) = "ZB" 'Zettabytes
        bSize(8) = "YB" 'Yottabytes

        b = CDbl(b) ' Make sure var is a Double (not just
        ' variant)
        For i = UBound(bSize) To 0 Step -1
            If b >= (1024 ^ i) Then
                Numeric2Bytes = ThreeNonZeroDigits(b / (1024 ^
                    i)) & " " & bSize(i)
                Exit For
            End If
        Next
    End Function


    Private Shared Function ThreeNonZeroDigits(ByVal value As Double) _
        As String
        If value >= 100 Then
            ' No digits after the decimal.
            Return Format$(CInt(value))
        ElseIf value >= 10 Then
            ' One digit after the decimal.
            Return Format$(value, "0.0")
        Else
            ' Two digits after the decimal.
            Return Format$(value, "0.00")
        End If
    End Function
End Class
