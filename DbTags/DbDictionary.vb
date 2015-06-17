Imports System.Runtime.InteropServices
Imports SignWriterStudio.SQLiteAdapters

Public Class DbDictionary
    Inherits BaseTableAdapter


    Public Sub New()
        TableName = "Dictionary"
        PrimaryKey = "IDDictionary"
        DefaultColumns = New List(Of String) From {"IDDictionary", "IDSignLanguage", "isPrivate", "bkColor", "SWriting", "Photo", "Sign", "SWritingSource",
                                                   "PhotoSource", "SignSource", "GUID", "Created", "LastModified", "IDSignPuddle", "SignPuddleUser",
                                                   "PuddlePrev", "PuddleNext", "PuddlePNG", "PuddleSVG", "PuddleVideoLink", "Sorting"}
    End Sub


    Public Shared Function GetDictionaryEntries(path As String, where As String) As IQueryResult
        Dim dict = New DbDictionary()
        Dim query = dict.DefaultGetQuery()
        query.Path = path
        query.Where = where

        Return query.Execute()

    End Function


    Public Shared Function GetIdDoNotExport(path As String) As List(Of String)
        Const where As String = " isPrivate "
        Dim columns = New List(Of String)()
        columns.Add("IDDictionary")

        Dim dict = New DbDictionary()
        Dim query = dict.DefaultGetQuery()
        query.Path = path
        query.Columns = columns
        query.Where = where

        Dim result = query.Execute()

        Return IdList(result)
    End Function

    Public Shared Function GetAllIds(path As String) As List(Of String)

        Dim columns = New List(Of String)()
        columns.Add("IDDictionary")

        Dim dict = New DbDictionary()
        Dim query = dict.DefaultGetQuery()
        query.Path = path
        query.Columns = columns


        Dim result = query.Execute()

        Return IdList(result)
    End Function

    Private Shared Function IdList(ByVal queryResult As IQueryResult) As List(Of String)
        Dim table = queryResult.TabularResults.FirstOrDefault()
        Dim list1 = New List(Of String)
        For Each expandoObject In table
            Dim row = TryCast(expandoObject, IDictionary(Of [String], Object))
            Dim str = row.Item("IDDictionary").ToString()
            list1.Add(str)
        Next
        Return list1
 
    End Function
End Class