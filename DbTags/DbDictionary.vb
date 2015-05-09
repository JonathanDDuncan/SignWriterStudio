
Imports System.Dynamic
Imports SignWriterStudio.SQLiteAdapters

Public Class DbDictionary
    Private Const TableName As String = "Dictionary"


    Public Shared Function GetDictionaryEntries(path As String, where As String) As IQueryResult
        Dim query = DefaultQuery(path)
        query.Where = where

        Return query.Execute()

    End Function

    Private Shared Function DefaultQuery(path As String) As GetQuery
        Return New GetQuery() With { _
             .Path = path, _
             .TableName = TableName _
        }
    End Function

    Public Shared Function GetIdDoNotExport(path As String) As List(Of String)
        Const where As String = " isPrivate "
        Dim columns = New List(Of String)()
        columns.Add("IDDictionary")

        Dim query = DefaultGetData()
        query.Path = path
        query.Columns = columns
        query.Where = where

        Dim result = query.Execute()

        Dim table = result.TabularResults.FirstOrDefault()
        Dim listIDs = New List(Of String)

        For Each expandoObject As ExpandoObject In table
            Dim row = TryCast(expandoObject, IDictionary(Of [String], Object))
            listIDs.Add(row.Item("IDDictionary"))
        Next

        Return listIDs
    End Function

    Private Shared Function DefaultGetData() As GetQuery
        Return New GetQuery() With {.TableName = TableName}
    End Function
End Class