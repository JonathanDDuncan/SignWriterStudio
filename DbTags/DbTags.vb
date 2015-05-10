Imports System.Dynamic
Imports SignWriterStudio.SQLiteAdapters

Public Class DbTags
    Inherits BaseTableAdapter
     
    Public Sub New()
        PrimaryKey = "IdTag"
        DefaultColumns = New List(Of String) From {"IdTag", "Description", "Abbreviation", "Color", "Rank", "Parent"}
        TableName = "Tags"
    End Sub


    Public Shared Function GetTagsData(path As String, orderBy As List(Of String)) As List(Of ExpandoObject)
        Dim dbTags = New DbTags()
        Dim query = dbTags.DefaultGetQuery()
        query.Path = path
        query.OrderBy = orderBy


        Dim result = query.Execute()
        Return result.TabularResults.FirstOrDefault()
    End Function

    Public Shared Sub SaveTags(ByVal path As String, ByVal added As List(Of ExpandoObject), ByVal updated As List(Of ExpandoObject), ByVal removed As List(Of String))
        Dim dbTags = New DbTags()

        Dim insertQuery = dbTags.CreateInsertQuery(path, GetTagValues(added))
        Dim updateQuery = dbTags.CreateUpdateQuery(path, GetTagValues(updated))
        Dim deleteQuery = dbTags.CreateDeleteQuery(path, removed)
        Dim unitofWork = New UnitOfWork()
        unitofWork.Queries.Add(insertQuery)
        unitofWork.Queries.Add(updateQuery)
        unitofWork.Queries.Add(deleteQuery)

        Dim result = unitofWork.Execute()
    End Sub


    Private Shared Function GetTagValues(ByVal added As List(Of ExpandoObject)) As List(Of List(Of String))

        Return (From expandoObject In added Select row = TryCast(expandoObject, IDictionary(Of String, Object)) _
                Select New List(Of String)() From { _
                    NullifEmpty(row.Item("IdTag")),
                    row.Item("Description"),
                    row.Item("Abbreviation"),
                    row.Item("Color"),
                    row.Item("Rank"),
                    NullifEmpty(row.Item("Parent"))}).ToList()
    End Function
End Class
