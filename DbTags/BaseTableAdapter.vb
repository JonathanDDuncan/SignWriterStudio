Imports System.Dynamic
Imports SignWriterStudio.SQLiteAdapters

Public Class BaseTableAdapter
    Public Property PrimaryKey() As String
    Public Property DefaultColumns() As List(Of String)
    Public Property TableName() As String

    Private Sub SetGeneralDefaults(ByVal query As BaseQuery)
        query.TableName = TableName
        query.Columns = DefaultColumns
        query.PrimaryKey = PrimaryKey
    End Sub
    Public Function DefaultGetQuery() As GetQuery
        Dim query = New GetQuery()
        SetGeneralDefaults(query)
        Return query
    End Function
    Public Function DefaultInsertQuery() As InsertQuery
        Dim query = New InsertQuery()
        SetGeneralDefaults(query)
        Return query
    End Function
    Public Function DefaultUpdateQuery() As UpdateQuery
        Dim query = New UpdateQuery()
        SetGeneralDefaults(query)
        Return query
    End Function
    Public Function DefaultDeleteQuery() As DeleteQuery
        Dim query = New DeleteQuery()
        SetGeneralDefaults(query)
        Return query
    End Function

    Public Function CreateDeleteQuery(ByVal path As String, ByVal removed As List(Of String))
        Dim query = DefaultDeleteQuery()
        query.Path = path
        query.Delete = removed

        Return query
    End Function

    Public Function CreateUpdateQuery(path As String, updated As List(Of ExpandoObject), ByVal getValues As Func(Of List(Of ExpandoObject), List(Of List(Of String)))) As UpdateQuery
        Dim query = DefaultUpdateQuery()
        query.Path = path
        query.Values = getValues(updated)

        Return query
    End Function
    Public Function CreateInsertQuery(ByVal path As String, ByVal added As List(Of ExpandoObject), ByVal getValues As Func(Of List(Of ExpandoObject), List(Of List(Of String)))) As InsertQuery
        Dim query = DefaultInsertQuery()
        query.Path = path
        query.Values = getValues(added)

        Return query
    End Function
End Class
