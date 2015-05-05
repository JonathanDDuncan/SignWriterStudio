Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite
Imports System.Linq


Public Class UnitOfWork
    Public Property Queries As List(Of ISqliteQuery)

    Public Function Execute() As List(Of IQueryResult)
        Dim cumresult = New List(Of IQueryResult)()

        Dim conn = GetConnection(Queries)
        conn.Open()
        Using tr = conn.BeginTransaction()
            Try
                For Each sqliteQuery In Queries
                    Dim result = sqliteQuery.Execute(conn, tr)
                    cumresult.Add(result)
                Next
                tr.Commit()
            Catch ex As Exception
                tr.Rollback()
                Throw
            Finally
                conn.Close()
            End Try
        End Using


        Return cumresult
    End Function


    Private Shared Function GetConnection(queries As IEnumerable(Of ISqliteQuery)) As SQLiteConnection
        Dim firstQuery = queries.FirstOrDefault()

        If firstQuery Is Nothing Then
            Return Nothing
        End If
        Dim conn = CommonSqlite.CreateConnection(firstQuery.Path)
        Return conn
    End Function
End Class