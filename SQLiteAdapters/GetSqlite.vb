Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SQLite
Imports System.Dynamic


Public Class GetSqlite
    Private Shared Function GetData(path As String, tableName As String, Optional columns As IEnumerable(Of String) = Nothing, Optional where As String = Nothing, Optional whereIn As Tuple(Of String, List(Of String)) = Nothing, Optional orderBy As List(Of String) = Nothing) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(path)
        conn.Open()


        Using tr = conn.BeginTransaction()
            Try
                Dim result = GetData(conn, tr, tableName, columns, where, whereIn, _
                                     orderBy)
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

    Private Shared Function GetData(conn As SQLiteConnection, tr As SQLiteTransaction, tableName As String, Optional columns As IEnumerable(Of String) = Nothing, Optional where As String = Nothing, Optional whereIn As Tuple(Of String, List(Of String)) = Nothing, _
                                    Optional orderBy As List(Of String) = Nothing) As IQueryResult
        Dim columnNames = StringUtil.GetSelectNames(columns)
        Dim whereclause = CommonSqlite.GetWhereClause(where, whereIn)
        Dim orderbyclause = CommonSqlite.GetOrderByClause(orderBy)
        Dim sql = String.Format("select {0} from {1}{2}{3}", columnNames, tableName, whereclause, orderbyclause)

        Dim command = New SQLiteCommand(sql, conn) With { _
                 .Transaction = tr _
                }

        Dim table As List(Of ExpandoObject) = Nothing
        Using rdr = command.ExecuteReader()
            Dim readColumns = StringUtil.GetColumnNames(rdr)
            If readColumns IsNot Nothing Then
                table = ReadRows(rdr, readColumns)
            End If
        End Using
        Return New QueryResult() With { _
              .TabularResults = New List(Of List(Of ExpandoObject))() From { _
                    table _
            } _
        }
    End Function
    Private Shared Function ReadRows(rdr As IDataReader, readColumns As List(Of String)) As List(Of ExpandoObject)
        Dim table = New List(Of ExpandoObject)()


        While rdr.Read()
            Dim newRow = New ExpandoObject()
            Dim row = TryCast(newRow, IDictionary(Of [String], Object))
            For Each columnName In readColumns
                row(columnName) = rdr(columnName)
            Next
            table.Add(newRow)
        End While
        Return table
    End Function

    Public Shared Function GetData(query As GetQuery) As IQueryResult
        Dim result = GetData(query.Path, query.TableName, query.Columns, query.Where, query.WhereIn, query.OrderBy)
        Return result
    End Function

    Public Shared Function GetData(conn As SQLiteConnection, tr As SQLiteTransaction, query As GetQuery) As IQueryResult
        Dim result = GetData(conn, tr, query.TableName, query.Columns, query.Where, query.WhereIn, _
                             query.OrderBy)
        Return result
    End Function
End Class