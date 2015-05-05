Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class DeleteQuery
    Implements ISqliteQuery
    Public Property TableName As String

    Public Property Path As String Implements ISqliteQuery.Path

    Public Property Where As String

    Public Property WhereIn As Tuple(Of String, List(Of String))

    Public Function Execute() As IQueryResult
        Return DeleteSqlite.Delete(Me)
    End Function



    Public Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult Implements ISqliteQuery.Execute
        Return DeleteSqlite.Delete(conn, tr, Me)
    End Function
End Class