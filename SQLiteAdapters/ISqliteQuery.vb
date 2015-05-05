Imports System.Data.SQLite


Public Interface ISqliteQuery
    Property Path() As String
    Function Execute(conn As SQLiteConnection, tr As SQLiteTransaction) As IQueryResult
End Interface