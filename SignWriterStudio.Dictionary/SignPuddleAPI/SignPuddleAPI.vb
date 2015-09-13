
Imports SignWriterStudio.WebSessions

Namespace SignPuddleApi

    Public Class SignPuddleApi
        Private ReadOnly _entrypointUri As String
        Private ReadOnly _post As WebSession
        Private ReadOnly _loginUri As String

        Property IsLoggedIn() As Boolean

        Public Sub New(ByVal username As String, ByVal password As String, Optional ByVal entrypointUri As String = Nothing, Optional ByVal loginUri As String = Nothing)
            If (String.IsNullOrEmpty(entrypointUri)) Then
                _entrypointUri = "http://www.signbank.org/signpuddle2.0/canvas.php"
            Else
                _entrypointUri = entrypointUri
            End If


            If (String.IsNullOrEmpty(entrypointUri)) Then
                _loginUri = "http://www.signbank.org/signpuddle2.0/login.php"
            Else
                _loginUri = loginUri
            End If

            _post = New WebSession()
            Dim loginWebPage = _post.Login(_loginUri, username, password)
            IsLoggedIn = WebSession.IsLoggedIn(loginWebPage)
        End Sub

        Public Function AddEntry(ByVal ui As String, ByVal sgn As String, ByVal sgntxt As String, ByVal txt As String, ByVal top As String,
                                 ByVal prev As String, ByVal nextStr As String, ByVal src As String, ByVal video As String,
                                 ByVal trm As List(Of String)) As String
            Const action As String = "Add"
            Dim webPage = CallApi(ui, sgn, Nothing, action, Nothing, trm,
                                         txt, sgntxt, video, src, top,
                                         prev, nextStr, Nothing, Nothing, Nothing)
            Return webPage
        End Function

        Public Function UpdateEntry(ByVal ui As String, ByVal sgn As String, ByVal sid As String, ByVal sgntxt As String, ByVal txt As String,
                                    ByVal top As String, ByVal prev As String, ByVal nextStr As String, ByVal src As String, ByVal video As String,
                                    ByVal trm As List(Of String)) As String
            Const action As String = "Update"
            Dim webPage = CallApi(ui, sgn, sid, action, Nothing, trm,
                                         txt, sgntxt, video, src, top,
                                         prev, nextStr, Nothing, Nothing, Nothing)


            Return webPage
        End Function


        Private Function CallApi(ByVal ui As String, ByVal sgn As String, ByVal sid As String, ByVal action As String, ByVal name As String, ByVal trm As List(Of String),
                                        ByVal txt As String, ByVal sgntxt As String, ByVal video As String, ByVal src As String, ByVal top As String,
                                        ByVal prev As String, ByVal nextStr As String, ByVal ext As String, ByVal list As String, ByVal imageBaseName As String)
            Dim paramList = New List(Of Tuple(Of String, String))

            AddParam(paramList, "ui", ui)
            AddParam(paramList, "sgn", sgn)
            AddParam(paramList, "sid", sid)
            AddParam(paramList, "action", action)
            AddParam(paramList, "name", name)

            If trm IsNot Nothing Then
                For Each term As String In trm
                    AddParam(paramList, "trm[]", term)
                Next
            End If

            AddParam(paramList, "txt", txt)
            AddParam(paramList, "sgntxt", sgntxt)
            AddParam(paramList, "video", video)
            AddParam(paramList, "src", src)
            AddParam(paramList, "top", top)
            AddParam(paramList, "prev", prev)
            AddParam(paramList, "nextStr", nextStr)
            AddParam(paramList, "ext", ext)
            AddParam(paramList, "list", list)
            AddParam(paramList, "imageBaseName", imageBaseName)


            'If (IsLoggedIn) Then
            If True Then
                Dim webpage = _post.Post(_entrypointUri, paramList)
                Return webpage
            Else
                Throw New ArgumentException("You must be logged in before doing operations.")
            End If
        End Function

        Private Sub AddParam(ByVal paramList As List(Of Tuple(Of String, String)), ByVal key As String, ByVal item As String)
            If item IsNot Nothing Then
                paramList.Add(Tuple.Create(key, item))
            End If
        End Sub

        Public Function DeleteEntry(ByVal ui As String, ByVal sgn As String, ByVal sid As String) As String
            Const action As String = "Delete"
            Dim webPage = CallApi(ui, sgn, sid, action, sid, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Return webPage
        End Function
        Public Function GetEntry(ByVal ui As String, ByVal sgn As String, ByVal sid As String) As String
            Const action As String = "Edit"
            Dim webPage = CallApi(ui, sgn, sid, action, sid, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Return webPage
        End Function
    End Class
End Namespace