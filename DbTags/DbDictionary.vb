
Imports SignWriterStudio.SQLiteAdapters

Public Class DbDictionary
    Private Const TableName As String = "Dictionary"


    Public Shared Function GetDictionaryEntries(path As String, where As String) As IQueryResult
        Dim query = DefaultQuery(path)
        query.Where = where

        Return query.Execute()

    End Function

    Private Shared Function DefaultQuery(path As String) As GetQuery
        Return New GetQuery() With { _
             .Path = path, _
             .TableName = TableName _
        }
    End Function


End Class