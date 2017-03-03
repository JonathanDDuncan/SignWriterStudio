Imports System.IO
Imports SignWriterStudio.SWClasses
Imports System.Data.SQLite

Public Class ExportPng
    Public Shared Function GetFilename(ByVal gloss As String, ByVal id As String, Optional ByVal suffix As String = "") As String
        Return (CleanFileName(gloss) + " " + id + suffix + ".png").Replace(" ", "")
    End Function
    Public Shared Function SavePng(ByVal gloss As String, ByVal guid As Guid, ByVal pngFolder As String, ByVal png As Image, ByVal suffix As String) As String
        Dim filename As String = GetFilename(gloss, guid.ToString, suffix)

        SavePng(pngFolder, filename, png)
        Return filename
    End Function

    Public Shared Sub SavePng(pngFolder As String, filename As String, png As Image)
        If Not File.Exists(pngFolder) Then
            Directory.CreateDirectory(pngFolder)
        End If
        If png IsNot Nothing Then
            png.Save(Path.Combine(pngFolder, filename))
        End If
    End Sub

    Private Shared Function CleanFileName(ByVal filename As String) As String

        For Each c In Path.GetInvalidFileNameChars()
            If filename.Contains(c) Then
                filename = filename.Replace(c, "_")
            End If
        Next

        Return filename

    End Function

    Public Shared Function GetSignsinDictionary(ByVal dt As DataTable, ByVal myDictionary As SWDict) As List(Of SignResult)
        Dim list = New List(Of SignResult)()


        For Each row In dt.Rows

            Dim sign = myDictionary.GetSWSign(row.IDDictionary)
            Dim res = New SignResult(row.gloss1, row.glosses1, row.GUID, General.ByteArraytoImage(row.SWriting), General.ByteArraytoImage(row.Photo), General.ByteArraytoImage(row.Sign), row.SWritingSource, row.PhotoSource, row.SignSource, sign)
            list.Add(res)

        Next
       

        Return list
    End Function
End Class

Public Class SignResult

    Public Property Gloss As String
    Public Property Glosses As String
    Public Property Guid As Guid
    Public Property SignWritingImage As Image
    Public Property IllustrationImage As Image
    Public Property PhotoSignImage As Image
    Public Property SignWritingSource As String
    Public Property IllustrationSource As String
    Public Property PhotoSignSource As String
    Public Property Sign As SwSign

    Sub New(ByVal gloss As String, ByVal glosses As String, ByVal guid As Guid, ByVal signWritingImage As Image, ByVal illustrationImage As Image, ByVal photoSignImage As Image, ByVal signWritingSource As String, ByVal illustrationSource As String, ByVal photoSignSource As String, ByVal sign As SwSign)
        Me.Gloss = gloss
        Me.Glosses = glosses
        Me.Guid = guid
        Me.SignWritingImage = signWritingImage
        Me.IllustrationImage = illustrationImage
        Me.PhotoSignImage = photoSignImage
        Me.SignWritingSource = signWritingSource
        Me.IllustrationSource = illustrationSource
        Me.PhotoSignSource = photoSignSource
        Me.Sign = sign
    End Sub
 
End Class
