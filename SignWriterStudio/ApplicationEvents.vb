Imports System.Text

Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_UnhandledException(sender As Object, e As ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
            WriteDetailedException(sender, e.Exception)
            My.Application.Log.WriteException(e.Exception,
       TraceEventType.Critical,
       "Application shut down at " &
       My.Computer.Clock.GmtTime.ToString)

        End Sub


        Private Sub WriteDetailedException(sender As Object, exception As Exception)
            Dim sb As New StringBuilder()
            sb.Append("sender ")
            sb.Append(sender.ToString)
            sb.Append(" ,sender type: ")
            sb.Append(sender.GetType)
            sb.Append(DetailedException(exception))

            My.Application.Log.WriteEntry(sb.ToString())
        End Sub

        Private Function DetailedException(exception As Exception) As String
            Dim sb As New StringBuilder()
            sb.Append(" ,exception message: ")
            sb.Append(exception.Message)
            sb.Append(" ,exception HResult: ")
            sb.Append(exception.HResult)
            sb.Append(" ,exception Message: ")
            sb.Append(exception.Message)

            If exception.InnerException IsNot Nothing Then
                sb.Append(" ,InnerException: ")
                sb.Append(DetailedException(exception.InnerException))
            End If

            Return sb.ToString()
        End Function

    End Class

End Namespace
