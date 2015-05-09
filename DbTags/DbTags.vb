Imports System.Dynamic
Imports SignWriterStudio.SQLiteAdapters

Public Class DbTags
    Private Const TableName As String = "Tags"
    Private Shared ReadOnly DefaultColumns = New List(Of String) From {
        "IdTag", "Description", "Abbreviation", "Color", "Rank", "Parent"}
    Private Const PrimaryKey = "IdTag"

    Public Shared Function DefaultGetQuery() As GetQuery
        Dim query = New GetQuery()
        SetGeneralDefaults(query)
        Return query
    End Function

    Private Shared Sub SetGeneralDefaults(ByVal query As BaseQuery)
        query.TableName = TableName
        query.Columns = DefaultColumns
        query.PrimaryKey = PrimaryKey
    End Sub

    Public Shared Function DefaultInsertQuery() As InsertQuery
        Dim query = New InsertQuery()
        SetGeneralDefaults(query)
        Return query
    End Function
    Public Shared Function DefaultUpdateQuery() As UpdateQuery
        Dim query = New UpdateQuery()
        SetGeneralDefaults(query)
        Return query
    End Function
    Public Shared Function DefaultDeleteQuery() As DeleteQuery
        Dim query = New DeleteQuery()
         SetGeneralDefaults(query)
        Return query
    End Function
    Public Shared Function GetTagsData(path As String, orderBy As List(Of String)) As List(Of ExpandoObject)
        Dim query = DefaultGetQuery()
        query.Path = path
        query.OrderBy = orderBy


        Dim result = query.Execute()
        Return result.TabularResults.FirstOrDefault()
    End Function

    Public Shared Sub SaveTags(ByVal path As String, ByVal added As List(Of ExpandoObject), ByVal updated As List(Of ExpandoObject), ByVal removed As List(Of String))
        Dim insertQuery = CreateInsertQuery(path, added)
        Dim updateQuery = CreateUpdateQuery(path, updated)
        Dim deleteQuery = CreateDeleteQuery(path, removed)
        Dim unitofWork = New UnitOfWork()
        unitofWork.Queries.Add(insertQuery)
        unitofWork.Queries.Add(updateQuery)
        unitofWork.Queries.Add(deleteQuery)

        Dim result = unitofWork.Execute()
    End Sub

    Private Shared Function CreateDeleteQuery(ByVal path As String, ByVal removed As List(Of String))
        Dim query = DefaultDeleteQuery()
        query.Path = path
        query.Delete = removed

        Return query
    End Function

    Private Shared Function CreateUpdateQuery(path As String, updated As List(Of ExpandoObject)) As UpdateQuery
        Dim query = DefaultUpdateQuery()
        query.Path = path
        query.Values = GetValues(updated)

        Return query
    End Function
    Private Shared Function CreateInsertQuery(ByVal path As String, ByVal added As List(Of ExpandoObject)) As InsertQuery
        Dim query = DefaultInsertQuery()
        query.Path = path
        query.Values = GetValues(added)

        Return query
    End Function

    Private Shared Function GetValues(ByVal added As List(Of ExpandoObject)) As List(Of List(Of String))

        Return (From expandoObject In added Select row = TryCast(expandoObject, IDictionary(Of String, Object)) _
                Select New List(Of String)() From { _
                    NullifEmpty(row.Item("IdTag")),
                    row.Item("Description"),
                    row.Item("Abbreviation"),
                    row.Item("Color"),
                    row.Item("Rank"),
                    NullifEmpty(row.Item("Parent"))}).ToList()
    End Function

    Private Shared Function NullifEmpty(ByVal item As Object) As String
        If item.GetType Is GetType(String) Then
            If (String.IsNullOrEmpty(item)) Then
                Return Nothing
            End If
        End If

        Return item
    End Function
End Class
