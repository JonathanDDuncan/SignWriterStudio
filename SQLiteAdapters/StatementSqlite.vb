Imports System.Data.SQLite
Imports System.Dynamic

Public Class StatementSqlite
    Public Shared Function Execute(ByVal statementQuery As StatementQuery) As IQueryResult
        Dim conn = CommonSqlite.CreateConnection(statementQuery.Path)
        conn.Open()


        Using tr = conn.BeginTransaction()
            Try
                Dim result = Execute(conn, tr, statementQuery)
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

    Public Shared Function Execute(ByVal conn As SQLiteConnection, ByVal tr As SQLiteTransaction, ByVal statementQuery As StatementQuery) As IQueryResult
        Dim sql = statementQuery.Sql
        Dim command = New SQLiteCommand(sql, conn) With { _
                  .Transaction = tr _
                }
        command.Parameters.AddRange(CreateParameters(statementQuery.Parameters))
        Return Execute(command)
    End Function

    Private Shared Function Execute(ByVal command As SQLiteCommand) As QueryResult
        command.ExecuteNonQuery()

        Dim table As List(Of ExpandoObject) = Nothing
        Using rdr = command.ExecuteReader()
            Dim readColumns = StringUtil.GetColumnNames(rdr)
            If readColumns IsNot Nothing Then
                table = GetSqlite.ReadRows(rdr, readColumns)
            End If
        End Using
        Return New QueryResult() With { _
                .SqlCommands = New List(Of String) From {command.CommandText},
              .TabularResults = New List(Of List(Of ExpandoObject))() From { _
                    table _
            } _
        }
    End Function

    Private Shared Function CreateParameters(ByVal param As Dictionary(Of String, String)) As SQLiteParameter()
        Dim newParamList = New List(Of SQLiteParameter)()
        For Each keyValuePair As KeyValuePair(Of String, String) In param
            Dim newParam = New SQLiteParameter(keyValuePair.Key, keyValuePair.Value)
            newParamList.Add(newParam)
        Next

        Return newParamList.ToArray()
    End Function

    Public Shared Function Count(ByVal statementQuery As StatementQuery) As QueryResult
        Dim conn = CommonSqlite.CreateConnection(statementQuery.Path)
        conn.Open()


        Using tr = conn.BeginTransaction()
            Try
                Dim result = Count(conn, tr, statementQuery)
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

    Public Shared Function Count(ByVal conn As SQLiteConnection, ByVal tr As SQLiteTransaction, ByVal statementQuery As StatementQuery) As QueryResult
        Dim sql = statementQuery.Sql
        sql = "SELECT COUNT(*)  From (" & sql & ")"
        Dim command = New SQLiteCommand(sql, conn) With { _
                  .Transaction = tr _
                }
        command.Parameters.AddRange(CreateParameters(statementQuery.Parameters))
        Dim entriescount = command.ExecuteScalar()

        Return New QueryResult() With {.SqlCommands = New List(Of String) From {command.CommandText}, .ScalarResults = New List(Of Object)() From {entriescount}}
    End Function

    Public Shared Function Page(ByVal statementQuery As StatementQuery) As QueryResult
        Dim conn = CommonSqlite.CreateConnection(statementQuery.Path)
        conn.Open()


        Using tr = conn.BeginTransaction()
            Try
                Dim result = Page(conn, tr, statementQuery)
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
    Public Shared Function Page(ByVal conn As SQLiteConnection, ByVal tr As SQLiteTransaction, ByVal statementQuery As StatementQuery) As QueryResult
        Dim sql = statementQuery.Sql
        sql = sql & " LIMIT @PageSize OFFSET @Skip"
        Dim command = New SQLiteCommand(sql, conn) With { _
                  .Transaction = tr _
                }
        command.Parameters.AddRange(CreateParameters(statementQuery.Parameters))
        command.Parameters.Add(New SQLiteParameter("@PageSize", statementQuery.PageSize))
        command.Parameters.Add(New SQLiteParameter("@Skip", statementQuery.Skip))
        Return Execute(command)
    End Function
End Class