Option Strict On

Imports SignWriterStudio.Database.Dictionary

Friend Class DictionaryDataGridTable
    Inherits DictionaryDataSet.SignsbyGlossesBilingualDataTable
    Sub New()

        Dim dc1 = New DataColumn("Tags", GetType(List(Of String)))
        Dim dc2 = New DataColumn("OriginalTags", GetType(List(Of String)))
        Columns.Add(dc1)
        Columns.Add(dc2)
    End Sub
End Class