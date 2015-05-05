Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class GetQuery
    Implements ISqliteQuery
    Public Property TableName As String

    Public Property Columns As List(Of String)

    Public Property Path As String Implements ISqliteQuery.Path

    Public Property Where As String

    Public Property WhereIn As Tuple(Of String, List(Of String))

    Public Property OrderBy As List(Of String)

    Public Function Execute() As IQueryResult
        Return GetSqlite.GetData(Me)
    End Function
    Public Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult Implements ISqliteQuery.Execute
        Return GetSqlite.GetData(conn, tr, Me)
    End Function
End Class