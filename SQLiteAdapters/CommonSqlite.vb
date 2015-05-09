Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.SQLite


Public Class CommonSqlite

    Public Shared Function CreateConnection(path As String) As SQLiteConnection
        Return New SQLiteConnection("Data Source=" & path & ";Version=3;")
    End Function

    Friend Shared Function GetWhereClause(where As String, whereIn As Tuple(Of String, List(Of String))) As String
        If Not String.IsNullOrEmpty(where) Then
            Return " WHERE " & where
        End If
        If whereIn IsNot Nothing AndAlso whereIn.Item2.Any() Then

            Return " WHERE [" & whereIn.Item1 & "] IN (" & StringUtil.ConcatValues(whereIn.Item2, "'", "'", ",") & ")"
        End If
        Return String.Empty
    End Function

    Public Shared Function GetOrderByClause(ByVal orderBy As List(Of String)) As String
        If orderBy IsNot Nothing Then
            Return " ORDER BY " & StringUtil.ConcatValues(orderBy, "[", "]", ",")
        End If
        Return String.Empty
    End Function
End Class