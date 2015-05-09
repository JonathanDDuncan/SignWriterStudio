Imports System.Data.SQLite

Public Class InsertQuery
    Inherits BaseQuery

    Public Property Values As List(Of List(Of String))

    Public Overrides Function Execute() As IQueryResult
        Return InsertSqlite.Insert(Me)
    End Function
    Public Overrides Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
        Return InsertSqlite.Insert(conn, tr, Me)
    End Function
End Class