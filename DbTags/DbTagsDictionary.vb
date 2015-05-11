﻿Imports System.Collections.Generic
Imports System.Dynamic
Imports System.Security.Cryptography
Imports SignWriterStudio.SQLiteAdapters

Public NotInheritable Class DbTagsDictionary
    Inherits BaseTableAdapter

    Public Sub New()
        PrimaryKey = "IdTagDictionary"
        TableName = "TagDictionary"
        DefaultColumns = New List(Of String) From {"IdTagDictionary", "IDDictionary", "IdTag"}
    End Sub

    Public Shared Function Insert(path As String, columns As List(Of String), valuesToInsert As List(Of List(Of String))) As IQueryResult
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultInsertQuery()
        query.Path = path
        query.Columns = columns
        query.Values = valuesToInsert
        Return query.Execute()
    End Function

    Public Shared Function GetData(path As String, columns As List(Of String), where As String) As IQueryResult
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultGetQuery()
        query.Path = path
        query.Columns = columns
        query.Where = where
        Return query.Execute()
    End Function

    Public Shared Function InsertDoNotExportTag(ByVal path As String, ByVal listIdDictionary As List(Of String)) As Integer
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultInsertQuery()
        query.Path = path

        query.Values = (From id In listIdDictionary Select New List(Of String)() From
                {Guid.NewGuid.ToString, id, "b9e38963-59e4-4878-ad68-922911dcce17"}).ToList()

        Dim result = query.Execute()
        Return result.AffectedRows
    End Function

    Public Shared Function GetTagEntries(ByVal path As String, ByVal entryIds As List(Of String)) As List(Of ExpandoObject)
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultGetQuery()
        query.Path = path
        query.WhereIn = Tuple.Create("IDDictionary", entryIds)
        Dim result = query.Execute()
        Return result.TabularResults.FirstOrDefault()
    End Function

    Public Shared Sub SaveTagDictionary(ByVal path As String, ByVal tagChanges As Tuple(Of List(Of List(Of String)), List(Of Tuple(Of String, String))))
        Dim deleteQuery = CreateDeleteQuery(path, tagChanges.Item2)
        Dim insertQuery = CreateInsertQuery(path, tagChanges.Item1)
        Dim unitofWork = New UnitOfWork()
        unitofWork.Queries.Add(insertQuery)
        unitofWork.Queries.Add(deleteQuery)

        Dim result = unitofWork.Execute()
    End Sub

    Private Shared Function CreateInsertQuery(ByVal path As String, ByVal toInsert As List(Of List(Of String))) As InsertQuery
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultInsertQuery()
        query.Path = path
        query.Columns = dbtd.DefaultColumns
        query.Values = AddGUID(toInsert)
        Return query
    End Function

    Private Shared Function AddGuid(ByVal toInsert As List(Of List(Of String))) As List(Of List(Of String))

        Dim newListList = New List(Of List(Of String))
        For Each list As List(Of String) In toInsert
            Dim newList = New List(Of String)()
            newList.Add(Guid.NewGuid.ToString())
            newList.AddRange(list)
            newListList.Add(newList)
        Next
        Return newListList
    End Function

    Private Shared Function CreateDeleteQuery(ByVal path As String, ByVal toRemove As List(Of Tuple(Of String, String))) As DeleteQuery
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultDeleteQuery()
        query.Path = path


        Return query
    End Function
End Class
