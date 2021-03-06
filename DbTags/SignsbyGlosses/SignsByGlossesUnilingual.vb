﻿Imports System.Dynamic
Imports SignWriterStudio.SQLiteAdapters

Namespace SignsbyGlosses
    Public Class SignsByGlossesUnilingual
        Private Shared ReadOnly BaseQryStr = _
            "SELECT Dictionary.IDDictionary, IDSignLanguage, IDSignPuddle, isPrivate, SWriting, Photo, Sign, " & _
            "SWritingSource, PhotoSource, SignSource, GUID, LastModified, PuddleVideoLink, TableLanguage1.IDDictionaryGloss AS IDDictionaryGloss1, " & _
            "TableLanguage1.IDCulture AS Culture1, TableLanguage1.gloss AS gloss1, TableLanguage1.glosses AS glosses1,TagsList.Tags As Tags, Dictionary.Sorting " & _
            "FROM Dictionary LEFT OUTER JOIN DictionaryGloss TableLanguage1 ON Dictionary.IDDictionary = TableLanguage1.IDDictionary " & _
            "LEFT OUTER JOIN (select IDDictionary, Group_Concat(IdTag) as Tags FROM TagDictionary GROUP BY IDDictionary) as TagsList ON Dictionary.IDDictionary = TagsList.IDDictionary  " & _
            "WHERE (Dictionary.IDSignLanguage = @IDSL) AND (((TableLanguage1.IDCulture = @Lang1) OR " & _
            "(TableLanguage1.IDCulture IS NULL)) AND ((TableLanguage1.gloss LIKE @search) OR " & _
            "(TableLanguage1.glosses LIKE @search))) " & _
            "GROUP BY Dictionary.IDDictionary " & _
            "ORDER BY TableLanguage1.gloss "

        Public Shared Function Count(ByVal path As String, ByVal slid As Integer, ByVal lang1Id As Integer, ByVal searchWord As String, ByVal filterTags As Boolean, ByVal allTagsExcept As Boolean, ByVal tags As List(Of String)) As Integer
            Dim query = New StatementQuery()
            Dim sword As String = SignsGlossTags.SearchWord(searchWord)
            query.Path = path
            query.Sql = SignsGlossTags.GetQuery(BaseQryStr, filterTags, allTagsExcept, tags)
            query.Parameters = New Dictionary(Of String, String)
            query.Parameters.Add("@IDSL", slid.ToString())
            query.Parameters.Add("@Lang1", lang1Id.ToString())
            query.Parameters.Add("@search", sword.ToString())
            Return query.Count().ScalarResults.FirstOrDefault()

        End Function

        Public Shared Function GetPage(ByVal path As String, ByVal slid As Integer, ByVal lang1Id As Integer, ByVal searchWord As String, ByVal pageSize As Integer, ByVal skip As Integer, ByVal filterTags As Boolean, ByVal allTagsExcept As Boolean, ByVal tags As List(Of String)) As List(Of ExpandoObject)
            Dim query = New StatementQuery()
            Dim sword As String = SignsGlossTags.SearchWord(searchWord)
            query.Path = path
            query.Sql = SignsGlossTags.GetQuery(BaseQryStr, filterTags, allTagsExcept, tags)
            query.PageSize = pageSize
            query.Skip = skip
            query.Parameters = New Dictionary(Of String, String)
            query.Parameters.Add("@IDSL", slid.ToString())
            query.Parameters.Add("@Lang1", lang1Id.ToString())
            query.Parameters.Add("@search", sword.ToString())
            Return query.Page().TabularResults.FirstOrDefault()
        End Function
    End Class
End Namespace