Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class DeleteSqlite


    Private Shared Function Delete(path As String, tableName As String, where As String, Optional whereIn As Tuple(Of String, List(Of String)) = Nothing) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(path)
        conn.Open()

        Using tr = conn.BeginTransaction()
            Try
                Dim result = Delete(conn, tr, tableName, where, whereIn)
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

    Private Shared Function Delete(conn As SQLiteConnection, tr As SQLiteTransaction, tableName As String, where As String, Optional whereIn As Tuple(Of String, List(Of String)) = Nothing) As IQueryResult
        Dim whereclause = CommonSqlite.GetWhereClause(where, whereIn)
        Dim sql = String.Format("DELETE FROM {0}{1};", tableName, whereclause)
        Dim command = New SQLiteCommand(sql, conn) With { _
                  .Transaction = tr _
                }
        Dim commandText = command.CommandText
        Dim totalRowsAffected = command.ExecuteNonQuery()

        Return New QueryResult() With { _
              .AffectedRows = totalRowsAffected, _
              .SqlCommands = New List(Of String)() From { _
                    commandText _
            } _
        }
    End Function

    Public Shared Function Delete(conn As SQLiteConnection, tr As SQLiteTransaction, query As DeleteQuery) As IQueryResult
        Return Delete(conn, tr, query.TableName, query.Where, query.WhereIn)
    End Function

    Public Shared Function Delete(query As DeleteQuery) As IQueryResult
        Return Delete(query.Path, query.TableName, query.Where, query.WhereIn)
    End Function
End Class