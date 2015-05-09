Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class CreateTableQuery
    Inherits BaseQuery
   
    Public Overrides Function Execute() As IQueryResult
        Return CreateTableSqlite.CreateTable(Me)
    End Function
    Public Overrides Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
        Return CreateTableSqlite.CreateTable(conn, tr, Me)
    End Function


End Class