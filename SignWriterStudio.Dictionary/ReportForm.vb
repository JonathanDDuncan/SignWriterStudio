Imports SignWriterStudio.SWClasses

Public Class ReportForm

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim rptViewr = New ReportViewer()
        Dim tagFilterValues = GetTagFilterValues(TagFilter1)
        Dim dt = Dictionary.GetDictionaryEntriesPaging("%", tagFilterValues, Integer.MaxValue, 0)
        Dim dt1 = NormalizeandSort(dt)
        rptViewr.DataTable = dt1

        rptViewr.Show()
    End Sub

    Private Shared Function NormalizeandSort(ByVal dt As DataTable) As DataTable
        Dim dc As New DataColumn()
        dc.ColumnName = "NormalizedGloss"
        dc.DataType = Type.GetType("System.String")
        dt.Columns.Add(dc)

        For Each row In dt.Rows
            row.Item("NormalizedGloss") = Normalization.Normalize(row.Item("Gloss1"))
        Next

        Dim dt1 = Sort(dt, "NormalizedGloss", "ASC")
        Return dt1
    End Function
   
    Public Shared Function Sort(dt As DataTable, colName As String, direction As String) As DataTable
        dt.DefaultView.Sort = colName & " " & direction
        dt = dt.DefaultView.ToTable()
        Return dt
    End Function

    Public Property Dictionary() As SWDict
    Private Shared Function GetTagFilterValues(ByVal tagFilter As TagFilter) As TagFilterValues
        Dim tagFilterValues = New TagFilterValues()
        tagFilterValues.Filter = tagFilter.CBFilter.Checked
        tagFilterValues.AllExcept = tagFilter.CBAllBut.Checked
        tagFilterValues.Tags = tagFilter.TagListControl1.TagValues

        Return tagFilterValues
    End Function
End Class