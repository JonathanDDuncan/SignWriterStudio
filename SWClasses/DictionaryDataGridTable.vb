Option Strict On

Imports SignWriterStudio.Database.Dictionary

Public Class DictionaryDataGridTable
    Inherits DictionaryDataSet.SignsbyGlossesBilingualDataTable
    Sub New()

        AddColumn(New DataColumn("Tags", GetType(List(Of String))))
        AddColumn(New DataColumn("OriginalTags", GetType(List(Of String))))
        AddColumn(New DataColumn("NormalizedGloss", GetType(String)))
        AddColumn(New DataColumn("SWritingWidth", GetType(Int32)))
        AddColumn(New DataColumn("SWritingHeight", GetType(Int32)))
        AddColumn(New DataColumn("IllustrationWidth", GetType(Int32)))
        AddColumn(New DataColumn("IllustrationHeight", GetType(Int32)))
        AddColumn(New DataColumn("SignWidth", GetType(Int32)))
        AddColumn(New DataColumn("SignHeight", GetType(Int32)))
        AddColumn(New DataColumn("VideoUrl", GetType(String)))

    End Sub

    Private Sub AddColumn(ByVal dataColumn As DataColumn)
        Columns.Add(dataColumn)
    End Sub
End Class