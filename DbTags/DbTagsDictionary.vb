Imports System.Collections.Generic
Imports SignWriterStudio.SQLiteAdapters

Public NotInheritable Class DbTagsDictionary
    Inherits BaseTableAdapter

    Public Sub New()
        PrimaryKey = "IdTagDictionary"
        TableName = "TagDictionary"
        DefaultColumns = New List(Of String) From {"IdTagDictionary", "IDDictionary", "IdTag"}
    End Sub

    Public Shared Function Insert(path As String, columns As List(Of String), valuesToInsert As List(Of List(Of String))) As IQueryResult
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultInsertQuery()
        query.Path = path
        query.Columns = columns
        query.Values = valuesToInsert
        Return query.Execute()
    End Function

    Public Shared Function GetData(path As String, columns As List(Of String), where As String) As IQueryResult
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultGetQuery()
        query.Path = path
        query.Columns = columns
        query.Where = where
        Return query.Execute()
    End Function

    Public Shared Function InsertDoNotExportTag(ByVal path As String, ByVal listIdDictionary As List(Of String)) As Integer
        Dim dbtd = New DbTagsDictionary()
        Dim query = dbtd.DefaultInsertQuery()
        query.Path = path

        query.Values = (From id In listIdDictionary Select New List(Of String)() From
                {Guid.NewGuid.ToString, id, "b9e38963-59e4-4878-ad68-922911dcce17"}).ToList()

        Dim result = query.Execute()
        Return result.AffectedRows
    End Function
End Class
