Imports System.IO
Imports SignWriterStudio.SWClasses
Imports SPML

Public Class ExportFingerSpelling
    Public Shared Sub Export(ByVal csvFilename As String, ByVal myDictionary As SWDict, ByVal dt As DataTable)
        If File.Exists(csvFilename) Then File.Delete(csvFilename)
        Dim conv As New SpmlConverter
        Using writer As StreamWriter = New StreamWriter(csvFilename)

            Dim signs = ExportPng.GetSignsinDictionary(dt, myDictionary)
            For Each sign In signs
                Dim gloss = sign.Gloss

                writer.Write(gloss)
                writer.Write(",")
                writer.Write(conv.GetFsw(sign.Sign))
                writer.WriteLine("")
            Next

        End Using
    End Sub

End Class
