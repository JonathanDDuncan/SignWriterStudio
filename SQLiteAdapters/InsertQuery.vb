Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class InsertQuery
    Implements ISqliteQuery
    Public Property TableName As String

    Public Property Values As List(Of List(Of String))

    Public Property Path As String Implements ISqliteQuery.Path

    Public Property Columns As List(Of String)

    Public Function Execute() As IQueryResult
        Return InsertSqlite.Insert(Me)
    End Function
    Public Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult Implements ISqliteQuery.Execute
        Return InsertSqlite.Insert(conn, tr, Me)
    End Function
End Class