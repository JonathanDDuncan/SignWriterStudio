Imports System.Data.SQLite

Public Class DeleteQuery
    Inherits BaseQuery
   
    Public Overrides Function Execute() As IQueryResult
        Return DeleteSqlite.Delete(Me)
    End Function
    Public Overrides Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
        Return DeleteSqlite.Delete(conn, tr, Me)
    End Function

    Public Property Delete() As List(Of String)

End Class