Imports System.Dynamic
Imports SignWriterStudio.SQLiteAdapters

Namespace SignsbyGlosses
    Public Class SignsbyGlossesBilingual
        Private Shared ReadOnly BaseQryStr = _
            "SELECT Joinner.IDDictionary AS IDDictionary, Joinner.IDSignLanguage AS IDSignLanguage, Joinner.IDSignPuddle AS IDSignPuddle, Joinner.isPrivate AS isPrivate, " & _
            "Joinner.SWriting AS SWriting, Joinner.Photo AS Photo, Joinner.Sign AS Sign, Joinner.SWritingSource AS SWritingSource, Joinner.PhotoSource AS PhotoSource, " & _
            "Joinner.SignSource AS SignSource, Joinner.GUID AS GUID, Joinner.LastModified AS LastModified, TableLanguage1.IDDictionaryGloss AS IDDictionaryGloss1, " & _
            "TableLanguage1.IDCulture AS Culture1, TableLanguage1.gloss AS gloss1, TableLanguage1.glosses AS glosses1, " & _
            "TableLanguage2.IDDictionaryGloss AS IDDictionaryGloss2, TableLanguage2.IDCulture AS Culture2, TableLanguage2.gloss AS gloss2, " & _
            "TableLanguage2.glosses AS glosses2,TagsList.Tags AS Tags, Joinner.Sorting as Sorting " & _
            "FROM (SELECT IDDictionary, IDSignLanguage, IDSignPuddle, isPrivate, SWriting, Photo, Sign, SWritingSource, " & _
            "PhotoSource, SignSource, GUID, LastModified, Sorting " & _
            "FROM Dictionary) Joinner LEFT OUTER JOIN " & _
            "(SELECT IDDictionaryGloss, IDDictionary, IDCulture, gloss, glosses " & _
            "FROM DictionaryGloss DictionaryGloss_1 " & _
            "WHERE (IDCulture = @Lang1) OR (IDCulture IS NULL)) TableLanguage1 ON Joinner.IDDictionary = TableLanguage1.IDDictionary " & _
            "LEFT OUTER JOIN (SELECT IDDictionaryGloss, IDDictionary, IDCulture, gloss, glosses " & _
            "FROM DictionaryGloss DictionaryGloss_2 " & _
            "WHERE (IDCulture = @Lang2) OR (IDCulture IS NULL)) TableLanguage2 ON Joinner.IDDictionary = TableLanguage2.IDDictionary " & _
            "LEFT OUTER JOIN (select IDDictionary, Group_Concat(IdTag) as Tags FROM TagDictionary GROUP BY IDDictionary) as TagsList ON Joinner.IDDictionary = TagsList.IDDictionary  " & _
            "WHERE (Joinner.IDSignLanguage = @IDSL) AND (gloss1 LIKE @search) OR (Joinner.IDSignLanguage = @IDSL) AND (glosses1 LIKE @search) OR " & _
            "(Joinner.IDSignLanguage = @IDSL) AND (gloss2 LIKE @search) OR (Joinner.IDSignLanguage = @IDSL) AND (glosses2 LIKE @search) GROUP BY Joinner.IDDictionary ORDER BY TableLanguage1.gloss "

        Public Shared Function Count(ByVal path As String, ByVal slid As Integer, ByVal lang1Id As Integer, ByVal lang2Id As Integer, ByVal searchWord As String) As Integer
            Dim query = New StatementQuery()
            query.Path = path
            query.Sql = BaseQryStr
            query.Parameters = New Dictionary(Of String, String)
            query.Parameters.Add("@IDSL", slid.ToString())
            query.Parameters.Add("@Lang1", lang1Id.ToString())
            query.Parameters.Add("@Lang2", lang2Id.ToString())
            query.Parameters.Add("@search", searchWord.ToString())
            Return query.Count().ScalarResults.FirstOrDefault()
        End Function

        Public Shared Function GetPage(ByVal path As String, ByVal slid As Integer, ByVal lang1Id As Integer, ByVal lang2Id As Integer, ByVal searchWord As String, ByVal pageSize As Integer, ByVal skip As Integer) As List(Of ExpandoObject)
            Dim query = New StatementQuery()
            query.Path = path
            query.Sql = BaseQryStr
            query.PageSize = pageSize
            query.Skip = skip
            query.Parameters = New Dictionary(Of String, String)
            query.Parameters.Add("@IDSL", slid.ToString())
            query.Parameters.Add("@Lang1", lang1Id.ToString())
            query.Parameters.Add("@Lang2", lang2Id.ToString())
            query.Parameters.Add("@search", searchWord.ToString())
            Return query.Page().TabularResults.FirstOrDefault()
        End Function

    End Class
End Namespace