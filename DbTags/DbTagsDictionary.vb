Imports System.Collections.Generic
Imports SignWriterStudio.SQLiteAdapters

Public NotInheritable Class DbTagsDictionary
    Private Const TableName As String = "TagDictionary"
    Private Shared ReadOnly DefaultColumns = New List(Of String) From {"IdTagDictionary", "IDDictionary", "IdTag"}

    Private Shared Function DefaultInsertQuery() As InsertQuery
        Return New InsertQuery() With {.TableName = TableName, .Columns = DefaultColumns}
    End Function
    Public Shared Function Insert(path As String, columns As List(Of String), valuesToInsert As List(Of List(Of String))) As IQueryResult
        Dim query = DefaultInsertQuery()
        query.Path = path
        query.Columns = columns
        query.Values = valuesToInsert
        Return query.Execute()
    End Function

    Public Shared Function GetData(path As String, columns As List(Of String), where As String) As IQueryResult
        Dim query = DefaultGetData()
        query.Path = path
        query.Columns = columns
        query.Where = where
        Return query.Execute()
    End Function

    Private Shared Function DefaultGetData() As GetQuery
        Return New GetQuery() With {.TableName = TableName}
    End Function

    Public Shared Function InsertDoNotExportTag(ByVal path As String, ByVal listIdDictionary As List(Of String)) As Integer
        Dim query = DefaultInsertQuery()
        query.Path = path

        query.Values = (From id In listIdDictionary Select New List(Of String)() From
                {Guid.NewGuid.ToString, id, "b9e38963-59e4-4878-ad68-922911dcce17"}).ToList()

        Dim result = query.Execute()
        Return result.AffectedRows
    End Function
End Class
