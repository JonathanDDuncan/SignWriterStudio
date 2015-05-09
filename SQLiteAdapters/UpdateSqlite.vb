Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite
Imports System.Linq


Public Class UpdateSqlite

    Private Shared Function Update(path As String, tableName As String, columns As IEnumerable(Of String), rows As IEnumerable(Of List(Of String))) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(path)
        conn.Open()

        Using tr = conn.BeginTransaction()
            Try
                Dim result = Update(conn, tr, tableName, columns, rows)
                tr.Commit()
                Return result
            Catch ex As Exception
                tr.Rollback()
                Throw
            Finally
                conn.Close()
            End Try
        End Using

    End Function

    Private Shared Function Update(conn As SQLiteConnection, tr As SQLiteTransaction, tableName As String, columns As IEnumerable(Of String), rows As IEnumerable(Of List(Of String))) As IQueryResult
        Dim totalRowsAffected = 0
        Dim commandList = New List(Of String)()
        Dim columnNames = If(TryCast(columns, IList(Of String)), columns.ToList())
        Dim sum = 0
        For Each row In rows
            Dim values = StringUtil.GetUpdateValues(columnNames, row)
            Dim where = StringUtil.GetUpdateWhere(columnNames, row)
            Dim sql = String.Format("UPDATE {0} SET {1} WHERE {2};", tableName, values, where)
            Dim command = New SQLiteCommand(sql, conn) With {.Transaction = tr}
            commandList.Add(command.CommandText)
            sum += command.ExecuteNonQuery()
        Next
        totalRowsAffected += sum

        Return New QueryResult() With { _
             .AffectedRows = totalRowsAffected, _
             .SqlCommands = commandList _
            }
    End Function

    Public Shared Function Update(query As UpdateQuery) As IQueryResult
        Return Update(query.Path, query.TableName, query.Columns, query.Values)
    End Function

    Public Shared Function Update(conn As SQLiteConnection, tr As SQLiteTransaction, query As UpdateQuery) As IQueryResult
        Return Update(conn, tr, query.TableName, query.Columns, query.Values)
    End Function
End Class