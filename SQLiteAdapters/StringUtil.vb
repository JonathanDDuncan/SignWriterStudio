Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq


Public Class StringUtil
    Public Shared Function GetSelectNames(selectitems As IEnumerable(Of String)) As String
        Return If(selectitems Is Nothing, " * ", ConcatValues(selectitems, "[", "]", ", "))
    End Function

    Public Shared Function GetColumnNames(reader As IDataReader) As List(Of String)
        Dim schemaTable = reader.GetSchemaTable()
        Dim myList = New List(Of String)()
        For Each row In schemaTable.Rows
            For Each column In schemaTable.Columns
                If column.ColumnName = "ColumnName" Then
                    myList.Add(row(column).ToString())
                End If
            Next
        Next
        Return myList
    End Function
    Public Shared Function GetValues(row As IEnumerable(Of String)) As String
        Return ConcatValues(row, "'", "'", ", ")
    End Function
    Public Shared Function GetInsertValues(row As IEnumerable(Of String)) As String
        Return Concat(row.Select(Function(x) GetSqlValue(x)), ", ")
    End Function
    Public Shared Function ConcatValues(row As IEnumerable(Of String), prepend As String, append As String, seperator As String) As String
        Return Concat(row.[Select](Function(x) prepend & x & append), seperator)
    End Function

    Friend Shared Function Concat(row As IEnumerable(Of String), seperator As String) As String
        Return row.Aggregate(Function(current, [next]) current & seperator & [next])
    End Function
    Public Shared Function Concatenate(items As IEnumerable(Of String)) As String
        Return items.Aggregate(Function(current, [next]) current & [next])
    End Function
    Public Shared Function GetUpdateValues(columnNames As IEnumerable(Of String), values As IEnumerable(Of Object)) As String
        Return columnNames.Skip(1).Zip(values.Skip(1), AddressOf Tuple.Create).[Select](Function(x) _
                         SquareBracket(Convert.ToString(x.Item1)) & " = " & GetSqlValue(x.Item2)) _
                            .Aggregate(Function(current, [next]) current & ", " & [next])
    End Function

    Private Shared Function GetSqlValue(ByVal value As Object) As String
        If value IsNot Nothing Then
            Return SingleQuote(Convert.ToString(value))
        Else
            Return "NULL"
        End If
    End Function

    Public Shared Function GetUpdateWhere(columns As IEnumerable(Of String), row As IEnumerable(Of Object)) As String
        Dim columnName = columns.FirstOrDefault()
        Dim id = row.FirstOrDefault()

        If columnName IsNot Nothing AndAlso id IsNot Nothing Then
            Return SquareBracket(columnName) & " = " & SingleQuote(Convert.ToString(id))
        End If
 
        Return Nothing
    End Function



    Public Shared Function RemoveLastChar(str As String) As String
        Return str.Substring(0, str.Length - 1)
    End Function

    Public Shared Function ConcatenateColumnTypes(fields As Dictionary(Of String, String)) As String
        Return Concat(fields.[Select](Function(x) SquareBracket(x.Key) & " " & x.Value), ", ")
    End Function

    Public Shared Function GetConnectionFilename(ByVal connectionString As String) As String
        If (connectionString.ToLower.Contains("data source=")) Then
            Dim filename = connectionString.Replace("data source=""", "")
            filename = filename.Substring(0, filename.Length - 1)
            Return filename
        Else
            Return connectionString
        End If
    End Function

    Public Shared Function SquareBracket(ByVal s As String) As String
        Return "[" & s & "]"
    End Function

    Public Shared Function SingleQuote(ByVal s As String) As String
        If (s.StartsWith("'")) Then
            'GUID Blob
            Return "X" & "'" & s.Substring(1, (s.Length - 1)) & "'"

        Else
            Return "'" & s & "'"
        End If
    End Function
End Class