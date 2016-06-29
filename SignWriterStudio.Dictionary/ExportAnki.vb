Imports System.IO
Imports SignWriterStudio.SWClasses

Public Class ExportAnki
    Public Shared Sub ExportExternalPng(ByVal csvFilename As String, ByVal pngFolder As String, ByVal myDictionary As SWDict, ByVal dt As DataTable)
        If File.Exists(csvFilename) Then File.Delete(csvFilename)
        Using writer As StreamWriter = New StreamWriter(csvFilename)

            Dim signs = ExportPng.GetSignsinDictionary(dt, myDictionary)
            For Each sign In signs
                Dim gloss = sign.Gloss
                Dim glosses = sign.Glosses
                Dim guid = sign.Guid
                Dim swpng = sign.SignWritingImage
                Dim iluspng = sign.IllustrationImage

                writer.Write("<span>")
                writer.Write(gloss)
                If Not String.IsNullOrEmpty(glosses) Then
                    writer.Write("," & glosses)
                   End If
                writer.Write("</span><br/>")
                If (iluspng IsNot Nothing) Then
                    Dim illusfilename As String = ExportPng.SavePng(gloss, guid, pngFolder, iluspng, "IL")
                    writer.Write(" <img src=""" & illusfilename & """/>")
                End If

                writer.Write(vbTab)
                If (swpng IsNot Nothing) Then
                    Dim swfilename As String = ExportPng.SavePng(gloss, guid, pngFolder, swpng, "SW")
                    writer.Write(" <img src=""" & swfilename & """/>")
                End If


                writer.WriteLine("")
            Next

        End Using
    End Sub
End Class
