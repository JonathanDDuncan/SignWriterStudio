Imports System.Data.SQLite


Public Class GetQuery
    Inherits BaseQuery
    Public Overrides Function Execute() As IQueryResult
        Return GetSqlite.GetData(Me)
    End Function
    Public Overrides Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
        Return GetSqlite.GetData(conn, tr, Me)
    End Function
     
End Class