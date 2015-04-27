Imports System.IO
Imports System.Drawing.Imaging
Imports SignWriterStudio.SWClasses
Imports System.Text
Imports System.Web

Public Class ExportHtml
    Public Shared Sub Export(ByVal myDictionary As SWDict, ByVal htmlFilename As String, ByVal inclBeg As Boolean, ByVal inclEnd As Boolean, ByVal begFilename As String, ByVal endFilename As String, ByVal createIndex As Boolean, ByVal externalPng As Boolean, ByVal exportFields As ExportFields, ByVal sortAlphabetically As Boolean)
        Dim pngfolderName = Path.GetFileNameWithoutExtension(htmlFilename) & "_files"
        Dim pngFolderAbsolute = Path.Combine(Path.GetDirectoryName(htmlFilename), pngfolderName)
        Dim pngFolderRelative = ".\" & pngfolderName & "\"
        Using writer As StreamWriter = New StreamWriter(htmlFilename, False, Encoding.UTF8)
            Dim signs = ExportPng.GetSignsinDictionary(myDictionary)
            if (sortAlphabetically) Then
                signs = SortSignsAlphabetically(signs)
            End If
            writer.WriteLine("<html><head> <style type=""text/css"">.center {text-align: center;}  .breakhere {page-break-before: always;}  .super-centered {width:100%;height:100%;text-align:center; vertical-align:middle;z-index: 9999;} </style></head><body>")
            If inclBeg Then
                IncludeFile(writer, begFilename)
            End If
            CreateDocIndex(writer, createIndex, signs)
            writer.WriteLine("<div class=""breakhere"" />")
            CreateTable(writer, signs, externalPng, pngFolderAbsolute, pngFolderRelative, exportFields)

            If inclEnd Then
                IncludeFile(writer, endFilename)
            End If
            writer.WriteLine("</body></html>")
        End Using
    End Sub

    Private Shared Function SortSignsAlphabetically(ByVal signs As List(Of SignResult)) As List(Of SignResult)
        return signs.OrderBy(Function(x) x.Gloss).ToList()
    End Function

    Private Shared Sub CreateDocIndex(ByVal writer As StreamWriter, ByVal createIndex As Boolean, ByVal signs As List(Of SignResult))

        If createIndex Then
            writer.WriteLine("<ul class=""breakhere"">")

            For Each sign In signs
                writer.WriteLine("<li>")
                writer.WriteLine("<a ")
                writer.WriteLine("href=""#")
                writer.WriteLine(sign.Guid.ToString)
                writer.WriteLine(""" ")

                writer.WriteLine(">")
                writer.WriteLine(sign.Gloss)
                writer.WriteLine(" ")
                writer.WriteLine(sign.Glosses)
                writer.WriteLine("</a>")
                writer.WriteLine("</li>")
            Next
            writer.WriteLine("</ul>")
        End If
    End Sub

    Private Shared Sub CreateTable(ByVal writer As StreamWriter, ByVal signs As List(Of SignResult), ByVal externalPng As Boolean, ByVal pngFolderAbsolute As String, ByVal pngFolderRelative As String, ByVal exportFields As ExportFields)
        writer.WriteLine("<table border=""1"" class=""super-centered"" >")

        For Each sign In signs
            WriteTrGuid(writer, sign.Guid.ToString())
            WriteGloss_es(writer, sign, exportFields)
            WriteSequences(writer, sign, exportFields)
            WriteImages(writer, sign, exportFields, externalPng, pngFolderAbsolute, pngFolderRelative)

            writer.WriteLine("</tr>")
        Next
        writer.WriteLine("</table>")
    End Sub

    Private Shared Sub WriteSequences(ByVal writer As StreamWriter, ByVal sign As SignResult, ByVal exportFields As ExportFields)
        If exportFields.ShowSequence Then
            writer.Write("<td>")

            Dim sequences = sign.Sign.Frames.First().Sequences
            For Each swSequence As SWSequence In sequences
                writer.Write(" <img src=""" & General.ImageToDataUri(GetSequenceImage(swSequence.Code), ImageFormat.Png) & """ />")
            Next
            writer.WriteLine("</td>")
        End If
    End Sub

    Private Shared Function GetSequenceImage(ByVal code As Integer) As Image
        Dim symbol = New SWSignSymbol With {.Code = code}
        
        Return symbol.SymbolDetails.SymImage
    End Function

    Private Shared Sub WriteImages(ByVal writer As StreamWriter, ByVal sign As SignResult, ByVal exportFields As ExportFields, ByVal externalPng As Boolean, ByVal pngFolderAbsolute As String, ByVal pngFolderRelative As String)
        Dim signWritingFilename As String = String.Empty
        Dim illustrationFilename As String = String.Empty
        Dim photoSignFilename As String = String.Empty
        If externalPng Then
            signWritingFilename = ExportPng.GetFilename(sign.Gloss, sign.Guid.ToString)
            illustrationFilename = ExportPng.GetFilename(sign.Gloss, sign.Guid.ToString, "illus")
            photoSignFilename = ExportPng.GetFilename(sign.Gloss, sign.Guid.ToString, "ps")

            ExportPng.SavePng(pngFolderAbsolute, signWritingFilename, sign.SignWritingImage)
            ExportPng.SavePng(pngFolderAbsolute, illustrationFilename, sign.IllustrationImage)
            ExportPng.SavePng(pngFolderAbsolute, photoSignFilename, sign.PhotoSignImage)
        End If

        WriteImage(writer, externalPng, pngFolderRelative, signWritingFilename, exportFields.ShowSignWriting, sign.SignWritingImage, exportFields.ShowSignWritingSource, sign.SignWritingSource)
        WriteImage(writer, externalPng, pngFolderRelative, illustrationFilename, exportFields.ShowIllustration, sign.IllustrationImage, exportFields.ShowIllustrationSource, sign.IllustrationSource)
        WriteImage(writer, externalPng, pngFolderRelative, photoSignFilename, exportFields.ShowPhotoSign, sign.PhotoSignImage, exportFields.ShowPhotoSignSource, sign.PhotoSignSource)
    End Sub

    Private Shared Sub WriteImage(ByVal writer As StreamWriter, ByVal externalPng As Boolean, ByVal pngFolderstr As String, ByVal imageFilename As String, ByVal showImage As Boolean, ByVal image As Image, ByVal showSource As Boolean, ByVal source As String)
        If showImage Then
            writer.Write("<td>")
            If image IsNot Nothing Then
                If externalPng Then
                    writer.Write(" <img src=""" & pngFolderstr & imageFilename & """ />")
                Else
                    writer.Write(" <img src=""" & General.ImageToDataUri(image, ImageFormat.Png) & """ />")
                End If
            End If

            writer.WriteLine("")
            If showSource Then
                writer.Write("<br/><span>Source: ")
                writer.Write(source)
                writer.Write("</span>")
            End If
            writer.WriteLine("</td>")
        End If
    End Sub

    Private Shared Sub WriteGloss_es(ByVal writer As StreamWriter, ByVal sign As SignResult, ByVal exportFields As ExportFields)
        If exportFields.ShowGloss Or exportFields.ShowGlosses Then
            writer.Write("<td>")
        End If

        If exportFields.ShowGloss Then
            writer.Write(HttpUtility.HtmlEncode(sign.Gloss))
        End If
        If exportFields.ShowGlosses Then
            If Not String.IsNullOrEmpty(sign.Glosses) Then
                If exportFields.ShowGloss Then
                    writer.Write(HttpUtility.HtmlEncode(", "))
                End If
                writer.Write(HttpUtility.HtmlEncode(sign.Glosses))
            End If
        End If
        If exportFields.ShowGloss Or exportFields.ShowGlosses Then
            writer.WriteLine("</td>")
        End If
    End Sub

    Private Shared Sub WriteTrGuid(ByVal writer As StreamWriter, ByVal guid As String)
        writer.Write("<tr")
        writer.Write(" id=""")
        writer.Write(guid)
        writer.Write("""")
        writer.Write(">")
    End Sub

    Private Shared Sub IncludeFile(ByVal writer As StreamWriter, ByVal includeFilename As String)
        Using reader As StreamReader = New StreamReader(includeFilename, Encoding.UTF8, False)
            Do
                Dim line As String = reader.ReadLine()
                If line Is Nothing Then Exit Do
                writer.WriteLine(line)
            Loop
        End Using
    End Sub

End Class
