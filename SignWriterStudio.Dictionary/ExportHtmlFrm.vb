Imports SignWriterStudio.SWClasses

Public Class ExportHtmlFrm
    Public Property MyDictionary() As SWDict
    Private Sub HTMLBrowseBtn_Click(sender As Object, e As EventArgs) Handles HTMLBrowseBtn.Click
        Dim saveFileDialog = New SaveFileDialog()
        saveFileDialog.AddExtension = True
        saveFileDialog.DefaultExt = "html"
        saveFileDialog.ShowDialog()
        HTMLFilenameTb.Text = saveFileDialog.FileName
    End Sub

    Private Sub ExportBtn_Click(sender As Object, e As EventArgs) Handles ExportBtn.Click
        Dim tagFilterValues = TagFilter1.GetTagFilterValues()
        Dim dt = MyDictionary.GetDictionaryEntriesPaging("%", tagFilterValues, Integer.MaxValue, 0)

        PrepareParameters(MyDictionary, dt, externPngCB.Checked, HTMLFilenameTb.Text, CBInclBegHtml.Checked, CBInclEndHtml.Checked, TBInclBegHtml.Text, TBInclEndHtml.Text, CBCreateIndex.Checked, FieldstoExport(), cbSortAlphabetically.Checked)
        MessageBox.Show("Export finished!")
    End Sub

    Private Function FieldstoExport() As ExportFields
        Dim ef As New ExportFields

        ef.ShowGloss = CBShowGloss.Checked
        ef.ShowGlosses = CBShowGlosses.Checked
        ef.ShowSequence = CBShowSequence.Checked
        ef.ShowSignWriting = CBShowSignWriting.Checked
        ef.ShowSignWritingSource = CBShowSignWritingSource.Checked
        ef.ShowIllustration = CBShowIllustration.Checked
        ef.ShowIllustrationSource = CBShowIllustrationSource.Checked
        ef.ShowPhotoSign = CBShowPhotoSign.Checked
        ef.ShowPhotoSignSource = CBShowPhotoSignSource.Checked

        Return ef
    End Function

    Private Sub PrepareParameters(ByVal dict As SWDict, ByVal dt As DataTable, ByVal externalPng As Boolean, ByVal htmlFilename As String, ByVal inclBeg As Boolean, ByVal inclEnd As Boolean, ByVal begFilename As String, ByVal endFilename As String, ByVal createIndex As Boolean, ByVal exportFields As ExportFields, ByVal sortAlphabetically As Boolean)
        ExportHtml.Export(dict, dt, htmlFilename, inclBeg, inclEnd, begFilename, endFilename, createIndex, externalPng, exportFields, sortAlphabetically)
    End Sub

    Private Sub BtnInclBegHtml_Click(sender As Object, e As EventArgs) Handles BtnInclBegHtml.Click
        Dim openFileDialog = New OpenFileDialog()
        openFileDialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*"
        openFileDialog.AddExtension = True
        openFileDialog.DefaultExt = "html"
        openFileDialog.ShowDialog()
        TBInclBegHtml.Text = openFileDialog.FileName
    End Sub

    Private Sub BtnInclEndHtml_Click(sender As Object, e As EventArgs) Handles BtnInclEndHtml.Click
        Dim openFileDialog = New OpenFileDialog()
        openFileDialog.Filter = "HTML files (*.html)|*.html|All files (*.*)|*.*"
        openFileDialog.AddExtension = True
        openFileDialog.DefaultExt = "html"
        openFileDialog.ShowDialog()
        TBInclEndHtml.Text = openFileDialog.FileName
    End Sub

    Private Sub CBInclBegHtml_CheckedChanged(sender As Object, e As EventArgs) Handles CBInclBegHtml.CheckedChanged
        EnableBegControls(CBInclBegHtml.Checked)
    End Sub

    Private Sub EnableBegControls(ByVal checked As Boolean)
        TBInclBegHtml.Enabled = checked
        BtnInclBegHtml.Enabled = checked
        LblInclBegHtml.Enabled = checked
    End Sub

    Private Sub CBInclEndHtml_CheckedChanged(sender As Object, e As EventArgs) Handles CBInclEndHtml.CheckedChanged
        EnableEndControls(CBInclBegHtml.Checked)
    End Sub

    Private Sub EnableEndControls(ByVal checked As Boolean)
        TBInclEndHtml.Enabled = checked
        BtnInclEndHtml.Enabled = checked
        LblInclEndHtml.Enabled = checked
    End Sub

    Private Sub ExportHtmlFrm_Load(sender As Object, e As EventArgs) Handles Me.Load
        CBInclBegHtml.Checked = False
        CBInclEndHtml.Checked = False
        EnableBegControls(CBInclBegHtml.Checked)
        EnableEndControls(CBInclBegHtml.Checked)
    End Sub

End Class