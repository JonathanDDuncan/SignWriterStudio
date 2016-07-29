Imports System.Text
Imports System.IO
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json

Public Class Json
    Public Shared Function SerializeJson(obj As Object) As String

        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As StringWriter = New StringWriter(sb)
        Dim jsonWriter As JsonSerializer = New JsonSerializer()

        jsonWriter.Converters.Add(New JavaScriptDateTimeConverter())
        jsonWriter.NullValueHandling = NullValueHandling.Ignore
        jsonWriter.Serialize(sw, obj)
        Return sb.ToString()
    End Function
End Class
