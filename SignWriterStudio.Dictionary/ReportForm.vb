Imports SignWriterStudio.Dictionary.Reports
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.General.All
Public Class ReportForm

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim rptViewr = New ReportViewer()

        Dim tagFilterValues = TagFilter1.GetTagFilterValues()
        Dim dt = Dictionary.GetDictionaryEntriesPaging("%", tagFilterValues, Integer.MaxValue, 0)
        Dim dt1 = NormalizeandSort(dt)
        rptViewr.DataDataSet = dt1

        Dim textDt = GetTextDt()
        rptViewr.TitleDataSet = textDt
        rptViewr.SetReport("Report" & CBReports.SelectedValue)
        rptViewr.Show()
    End Sub

    Private Function GetTextDt() As ReportTitleTable
        Dim dt = New ReportTitleTable()
        Dim row = dt.NewRow
        row.Item("Title") = TitleTB.Text
        row.Item("Gloss") = GlossTB.Text
        row.Item("SignWriting") = SignWritingTB.Text
        row.Item("Illustration") = IllustrationTB.Text
        row.Item("PhotoSign") = PhotoSignTB.Text

        dt.Rows.Add(row)
        Return dt
    End Function

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

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TagFilter1.AssumeFiltering = True
        Dim reportsList = New List(Of Tuple(Of Integer, String))()
        reportsList.Add(Tuple.Create(1, "Gloss, SignWriting, Illustration, Photo Sign"))
        reportsList.Add(Tuple.Create(2, "Gloss, SignWriting"))
        reportsList.Add(Tuple.Create(3, "Gloss, SignWriting, Illustration, Video Link"))
        CBReports.DataSource = reportsList
        CBReports.ValueMember = "Item1"
        CBReports.DisplayMember = "Item2"

    End Sub
  
    Private Sub CBReports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBReports.SelectedIndexChanged
        ReportChanged(CBReports)
    End Sub

    Private Sub ReportChanged(ByVal comboBox As ComboBox)
        Dim selectedValue = comboBox.SelectedValue

        Dim value = If(TypeOf selectedValue Is Tuple(Of Integer, String), selectedValue.Item1, selectedValue)

        Select Case value
            Case 1
                Report1Chosen()
            Case 2
                Report2Chosen()
            Case 3
                Report3Chosen()
        End Select
    End Sub

    Private Sub Report2Chosen()
        TitleTB.Enabled = True
        GlossTB.Enabled = True
        SignWritingTB.Enabled = True
        IllustrationTB.Enabled = False
        PhotoSignTB.Enabled = False
    End Sub

    Private Sub Report1Chosen()
        TitleTB.Enabled = True
        GlossTB.Enabled = True
        SignWritingTB.Enabled = True
        IllustrationTB.Enabled = True
        PhotoSignTB.Enabled = True
    End Sub
    Private Sub Report3Chosen()
        TitleTB.Enabled = True
        GlossTB.Enabled = True
        SignWritingTB.Enabled = True
        IllustrationTB.Enabled = True
        PhotoSignTB.Enabled = False
    End Sub
 
End Class