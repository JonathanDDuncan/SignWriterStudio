Imports System.Collections.Generic
Imports SignWriterStudio.SQLiteAdapters

Public NotInheritable Class DbTagsDictionary
    Private Sub New()
    End Sub
    Private Const TableName As String = "TagDictionary"

    Public Shared Function Insert(path As String, columns As List(Of String), valuesToInsert As List(Of List(Of String))) As IQueryResult
        Dim query = DefaultInsertQuery(path)
        query.Columns = columns
        query.Values = valuesToInsert
        Return query.Execute()
    End Function

    Private Shared Function DefaultInsertQuery(path As String) As InsertQuery
        Return New InsertQuery() With { _
              .Path = path, _
              .TableName = TableName _
        }
    End Function
End Class
