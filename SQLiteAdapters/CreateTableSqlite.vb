Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class CreateTableSqlite
    Public Shared Function CreateTable(path As String, tableName As String, primaryKey As String, fields As Dictionary(Of String, String)) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(path)
        conn.Open()
        Using tr = conn.BeginTransaction()
            Try
                Dim result = CreateTable(conn, tr, tableName, fields)
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
    Public Shared Function CreateTable(conn As SQLiteConnection, tr As SQLiteTransaction, query As CreateTableQuery) As IQueryResult
        Return CreateTable(conn, tr, query.TableName, query.Fields)
    End Function
    Private Shared Function CreateTable(conn As SQLiteConnection, tr As SQLiteTransaction, tableName As String, fields As Dictionary(Of String, String)) As IQueryResult
        Dim columnTypes = StringUtil.ConcatenateColumnTypes(fields)
        Dim sql = String.Format("create table if not exists {0} ({1});", tableName, columnTypes)
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

    Public Shared Function CreateTable(query As CreateTableQuery) As IQueryResult
        Return CreateTable(query.Path, query.TableName, query.PrimaryKey, query.Fields)
    End Function


End Class