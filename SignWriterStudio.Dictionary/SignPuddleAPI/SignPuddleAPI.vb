Imports SignWriterStudio.WebSessions

Namespace SignPuddleApi

    Public Class SignPuddleApi
        Private ReadOnly _baseentrypointUri As String
        Private ReadOnly _canvasentrypointUri As String
        Private ReadOnly _post As WebSession
        Private ReadOnly _loginUri As String
        Private ReadOnly _exportUri As String

        Property IsLoggedIn() As Boolean

        Public Sub New(ByVal username As String, ByVal password As String, Optional ByVal canvasentrypointUri As String = Nothing, Optional ByVal loginUri As String = Nothing, Optional ByVal exportentrypointUri As String = Nothing)
            _baseentrypointUri = "http://www.signbank.org/signpuddle2.0/"


            If (String.IsNullOrEmpty(canvasentrypointUri)) Then
                _canvasentrypointUri = _baseentrypointUri + "canvas.php"
            Else
                _canvasentrypointUri = canvasentrypointUri
            End If

            If (String.IsNullOrEmpty(loginUri)) Then
                _loginUri = _baseentrypointUri + "login.php"
            Else
                _loginUri = loginUri
            End If

            If (String.IsNullOrEmpty(exportentrypointUri)) Then
                _exportUri = _baseentrypointUri + "export.php"
            Else
                _exportUri = exportentrypointUri
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
                Dim webpage = _post.Post(_canvasentrypointUri, paramList)
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
        Public Function GetExport(ByVal ui As String, ByVal sgn As String, ByVal sid As String) As String
            Const action As String = "View"
            Dim webPage = CallExportApi(ui, sgn, sid, action, sid, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            Return webPage
        End Function

        Private Function CallExportApi(ByVal ui As String, ByVal sgn As String, ByVal s As String, ByVal action As String, ByVal sid As String, ByVal o1 As Object, ByVal o2 As Object, ByVal o3 As Object, ByVal o4 As Object, ByVal o5 As Object, ByVal o6 As Object, ByVal o7 As Object, ByVal o8 As Object, ByVal o9 As Object, ByVal o10 As Object, ByVal o As Object) As String
            Dim paramList = New List(Of Tuple(Of String, String))

            AddParam(paramList, "ui", ui)
            AddParam(paramList, "sgn", sgn)
            AddParam(paramList, "action", "Start")
            Dim webpage1 = _post.Post(_exportUri, paramList)

            AddParam(paramList, "ui", ui)
            AddParam(paramList, "sgn", sgn)
            AddParam(paramList, "export_list", sid)
            AddParam(paramList, "ex_source", "Selected")
            AddParam(paramList, "action", action)
            'AddParam(paramList, "name", name)

            'If trm IsNot Nothing Then
            '    For Each term As String In trm
            '        AddParam(paramList, "trm[]", term)
            '    Next
            'End If

            'AddParam(paramList, "txt", txt)
            'AddParam(paramList, "sgntxt", sgntxt)
            'AddParam(paramList, "video", video)
            'AddParam(paramList, "src", src)
            'AddParam(paramList, "top", top)
            'AddParam(paramList, "prev", prev)
            'AddParam(paramList, "nextStr", nextStr)
            'AddParam(paramList, "ext", ext)
            'AddParam(paramList, "list", List)
            'AddParam(paramList, "imageBaseName", imageBaseName)


            'If (IsLoggedIn) Then
            If True Then
                Dim webpage = _post.Post(_exportUri, paramList)
                Return webpage
            Else
                Throw New ArgumentException("You must be logged in before doing operations.")
            End If
        End Function

        Public Function GetPuddles() As String
            Dim paramList = New List(Of Tuple(Of String, String))

            'If (IsLoggedIn) Then
            If True Then
                Dim webpage = _post.Post("http://www.signbank.org/signpuddle2.0/index.php?ui=1", paramList)
                Return webpage
            Else
                Throw New ArgumentException("You must be logged in before doing operations.")
            End If
        End Function

        Public Function WasAdded(ByVal webPage As String) As Boolean
            Dim added = True

            added = added AndAlso webPage.Contains("SignText data:")
            added = added AndAlso webPage.Contains("Modified:")
            added = added AndAlso webPage.Contains("Puddle Page")

            Return added
        End Function

        Public Function WasDeleted(ByVal webPage As String) As Boolean
            Dim deleted = True

            deleted = deleted AndAlso webPage IsNot Nothing AndAlso webPage.Contains("Entry deleted")
           
            Return deleted
        End Function

        Public Function GetFirsSidInWebPage(ByVal webPageResult As String) As String

            Dim puddlePageEndIndex = webPageResult.IndexOf("Puddle Page", StringComparison.Ordinal) + 11

            Dim nextTagIndex = webPageResult.IndexOf("<", puddlePageEndIndex, StringComparison.Ordinal)

            Dim sidStr = webPageResult.Substring(puddlePageEndIndex, nextTagIndex - (puddlePageEndIndex))
            
            Return Trim(sidStr)

        End Function
    End Class
End Namespace