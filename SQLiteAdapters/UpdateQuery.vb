Imports System.Collections.Generic
Imports System.Data.SQLite

Public Class UpdateQuery
    Inherits BaseQuery
    
    Public Property Values As List(Of List(Of String))
 
    Public Overrides Function Execute() As IQueryResult
        Return UpdateSqlite.Update(Me)
    End Function
    Public Overrides Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
        Return UpdateSqlite.Update(conn, tr, Me)
    End Function
End Class