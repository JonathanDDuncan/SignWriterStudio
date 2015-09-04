Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.General.All
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
        AddImageSizes(dt)
        For Each row In dt.Rows
            row.Item("NormalizedGloss") = Normalization.Normalize(row.Item("Gloss1"))
        Next


        Dim dt1 = Sort(dt, "NormalizedGloss", "ASC")
        Return dt1
    End Function

    Private Shared Sub AddImageSizes(ByVal dt As DataTable)
        For Each row In dt.Rows
            Dim sWriting As Image = Nothing
            If (Not IsDbNull(row.Item("SWriting"))) Then
                sWriting = ByteArraytoImage(row.Item("SWriting"))
            End If
            row.Item("SWritingWidth") = If(sWriting IsNot Nothing, sWriting.Width, 0)
            row.Item("SWritingHeight") = If(sWriting IsNot Nothing, sWriting.Height, 0)

            Dim illustration As Image = Nothing
            If (Not IsDbNull(row.Item("Photo"))) Then
                illustration = ByteArraytoImage(row.Item("Photo"))
            End If
            row.Item("IllustrationWidth") = If(illustration IsNot Nothing, illustration.Width, 0)
            row.Item("IllustrationHeight") = If(illustration IsNot Nothing, illustration.Height, 0)

            Dim sign As Image = Nothing
            If (Not IsDbNull(row.Item("Sign"))) Then
                sign = ByteArraytoImage(row.Item("Sign"))
            End If
            row.Item("SignWidth") = If(sign IsNot Nothing, sign.Width, 0)
            row.Item("SignHeight") = If(sign IsNot Nothing, sign.Height, 0)

        Next
    End Sub

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