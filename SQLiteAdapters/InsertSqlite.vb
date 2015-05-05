Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class InsertSqlite
    Private Shared Function Insert(path As String, tableName As String, columns As IEnumerable(Of String), rows As IEnumerable(Of List(Of String))) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(path)

        conn.Open()

        Using tr = conn.BeginTransaction()
            Try
                Dim result = Insert(conn, tr, tableName, columns, rows)

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
    Private Shared Function Insert(conn As SQLiteConnection, tr As SQLiteTransaction, tableName As String, columns As IEnumerable(Of String), rows As IEnumerable(Of List(Of String))) As IQueryResult
        Dim columnNames = StringUtil.GetSelectNames(columns)

        Dim totalRowsAffected = 0
        Dim commandList = New List(Of String)()


        Dim sum As Integer = 0
        For Each row As List(Of String) In rows
            Dim values = StringUtil.GetValues(row)
            Dim sql = String.Format("INSERT INTO {0} ({1})  VALUES ({2});", tableName, columnNames, values)
            Dim command = New SQLiteCommand(sql, conn)

            commandList.Add(command.CommandText)
            sum += command.ExecuteNonQuery()
        Next
        totalRowsAffected += sum





        Return New QueryResult() With { _
             .AffectedRows = totalRowsAffected, _
             .SqlCommands = commandList _
            }

    End Function
    Public Shared Function Insert(query As InsertQuery) As IQueryResult

        Dim result = Insert(query.Path, query.TableName, query.Columns, query.Values)
        Return result
    End Function

    Public Shared Function Insert(conn As SQLiteConnection, tr As SQLiteTransaction, query As InsertQuery) As IQueryResult
        Dim result = Insert(conn, tr, query.TableName, query.Columns, query.Values)
        Return result
    End Function
End Class