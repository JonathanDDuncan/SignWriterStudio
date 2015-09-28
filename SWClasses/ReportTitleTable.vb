Option Strict On

Public Class ReportTitleTable
    Inherits TypedTableBase(Of ReportTitleRow)
    Sub New()

        AddColumn(New DataColumn("Title", GetType(String)))
        AddColumn(New DataColumn("Gloss", GetType(String)))
        AddColumn(New DataColumn("SignWriting", GetType(String)))
        AddColumn(New DataColumn("Illustration", GetType(String)))
        AddColumn(New DataColumn("PhotoSign", GetType(String)))
    End Sub

    Private Sub AddColumn(ByVal dataColumn As DataColumn)
        Columns.Add(dataColumn)
    End Sub
End Class