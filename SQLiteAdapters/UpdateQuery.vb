Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class UpdateQuery
    Implements ISqliteQuery
    Public Property TableName As String

    Public Property Values As List(Of List(Of Object))

    Public Property Path As String Implements ISqliteQuery.Path

    Public Property Columns As List(Of String)

    Public Function Execute() As IQueryResult
        Return UpdateSqlite.Update(Me)
    End Function
    Public Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult Implements ISqliteQuery.Execute
        Return UpdateSqlite.Update(conn, tr, Me)
    End Function
End Class