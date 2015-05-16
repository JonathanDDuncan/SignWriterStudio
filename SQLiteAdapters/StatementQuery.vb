Imports System.Data.SQLite

Public Class StatementQuery
    Inherits BaseQuery

    Public Overrides Function Execute() As IQueryResult
        Return StatementSqlite.Execute(Me)
    End Function
    Public Overrides Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
        Return StatementSqlite.Execute(conn, tr, Me)
    End Function


    Public Property Sql() As String

    Public Property Parameters() As Dictionary(Of String, String)

    Public Property PageSize() As Integer

    Public Property Skip() As Integer


    Public Function Count() As QueryResult
        Return StatementSqlite.Count(Me)
    End Function

    Public Function Page() As QueryResult
        Return StatementSqlite.Page(Me)
    End Function
End Class