Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class CreateTableQuery
    Implements ISqliteQuery
    Public Property TableName As String

    Public Property Path As String Implements ISqliteQuery.Path

    Public Property Fields As Dictionary(Of String, String)

    Public Property PrimaryKey As String

    Public Function Execute() As IQueryResult
        Return CreateTableSqlite.CreateTable(Me)
    End Function
    Public Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult Implements ISqliteQuery.Execute
        Return CreateTableSqlite.CreateTable(conn, tr, Me)
    End Function


End Class