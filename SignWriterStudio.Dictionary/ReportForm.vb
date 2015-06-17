Imports SignWriterStudio.SWClasses

Public Class ReportForm

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim rptViewr = New ReportViewer()
        Dim tagFilterValues = GetTagFilterValues(TagFilter1)
        Dim dt = Dictionary.GetDictionaryEntriesPaging("%", tagFilterValues, Integer.MaxValue, 0)
        rptViewr.DataTable = dt

        rptViewr.Show()
    End Sub

    Public Property Dictionary() As SWDict
    Private Shared Function GetTagFilterValues(ByVal tagFilter As TagFilter) As TagFilterValues
        Dim tagFilterValues = New TagFilterValues()
        tagFilterValues.Filter = tagFilter.CBFilter.Checked
        tagFilterValues.AllExcept = tagFilter.CBAllBut.Checked
        tagFilterValues.Tags = tagFilter.TagListControl1.TagValues

        Return tagFilterValues
    End Function
End Class