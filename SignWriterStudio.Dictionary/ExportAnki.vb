Imports System.IO
Imports System.Drawing.Imaging
Imports SignWriterStudio.SWClasses
Imports System.Text

Public Class ExportAnki
    Public Shared Sub ExportExternalPng(ByVal csvFilename As String, ByVal pngFolder As String, ByVal myDictionary As SWDict)
        If File.Exists(csvFilename) Then File.Delete(csvFilename)
        Using writer As StreamWriter = New StreamWriter(csvFilename)

            Dim signs = ExportPng.GetSignsinDictionary(myDictionary)
            For Each sign In signs
                Dim gloss = sign.Gloss 
                Dim glosses = sign.Glosses 
                Dim guid = sign.Guid 
                Dim png = sign.SignWritingImage

                Dim filename As String = ExportPng.GetFilename(gloss, guid.ToString)

                ExportPng.SavePng(pngFolder, filename, png)

                writer.Write(gloss)
                If Not String.IsNullOrEmpty(glosses) Then
                    writer.Write("," & glosses)
                End If
                writer.Write(vbTab)
                writer.Write(" <img src=""" & filename & """/>")
                writer.WriteLine("")
            Next

        End Using
    End Sub
  
End Class
