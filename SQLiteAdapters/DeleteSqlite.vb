Imports System
Imports System.Collections.Generic
Imports System.Data.SQLite


Public Class DeleteSqlite


    Private Shared Function Delete(ByVal path As String, ByVal tableName As String, ByVal where As String, Optional ByVal whereIn As Tuple(Of String, List(Of String)) = Nothing, Optional ByVal deleteList As List(Of String) = Nothing, Optional ByVal primaryKey As String = Nothing, Optional ByVal columns As List(Of String) = Nothing, Optional ByVal values As List(Of String) = Nothing) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(path)
        conn.Open()

        Using tr = conn.BeginTransaction()
            Try
                Dim result = Delete(conn, tr, tableName, where, whereIn, deleteList, primaryKey, columns, values)
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

    Private Shared Function Delete(ByVal conn As SQLiteConnection, ByVal tr As SQLiteTransaction, ByVal tableName As String, ByVal where As String, Optional ByVal whereIn As Tuple(Of String, List(Of String)) = Nothing, Optional ByVal deleteList As List(Of String) = Nothing, Optional ByVal primaryKey As String = Nothing, Optional ByVal columns As List(Of String) = Nothing, Optional ByVal values As List(Of String) = Nothing) As IQueryResult

        If (deleteList IsNot Nothing AndAlso primaryKey IsNot Nothing) Then
            whereIn = Tuple.Create(primaryKey, deleteList)
        End If
        Dim totalRowsAffected As Integer
        Dim commandText As String = String.Empty
        Dim whereclause = ValuesWhere(CommonSqlite.GetWhereClause(where, whereIn), columns, values)
        If whereclause.Contains("WHERE") Then


            Dim sql = String.Format("DELETE FROM {0}{1};", tableName, whereclause)
            Dim command = New SQLiteCommand(sql, conn) With { _
                      .Transaction = tr _
                    }

            commandText = command.CommandText

            totalRowsAffected = command.ExecuteNonQuery()
        End If

        Return New QueryResult() With { _
              .AffectedRows = totalRowsAffected, _
              .SqlCommands = New List(Of String)() From { _
                    commandText _
            } _
        }
    End Function

    Private Shared Function ValuesWhere(ByVal initialWhereClause As String, ByVal columns As List(Of String), ByVal values As List(Of String)) As String
        If Not String.IsNullOrEmpty(initialWhereClause) Then
            Return initialWhereClause
        ElseIf columns IsNot Nothing AndAlso values IsNot Nothing Then

            Return " WHERE " & DeleteWhereValues(columns, values)
        Else
            Return String.Empty
        End If


    End Function

    Private Shared Function DeleteWhereValues(ByVal columns As List(Of String), ByVal values As List(Of String)) As String
        Return StringUtil.Concat(columns.Zip(values, Function(x, y) StringUtil.SquareBracket(x) & " = " & StringUtil.SingleQuote(y)), " AND ")
    End Function

    Public Shared Function Delete(conn As SQLiteConnection, tr As SQLiteTransaction, query As DeleteQuery) As IQueryResult
        Return Delete(conn, tr, query.TableName, query.Where, query.WhereIn, query.Delete, query.PrimaryKey, query.Columns, query.Values)
    End Function

    Public Shared Function Delete(query As DeleteQuery) As IQueryResult
        Return Delete(query.Path, query.TableName, query.Where, query.WhereIn, query.Delete, query.PrimaryKey, query.Columns, query.Values)
    End Function
End Class