Imports SignWriterStudio.SWClasses

Public Class ExportFingerSpellingFrm
    Public Property MyDictionary() As SWDict
    Private Sub TextBrowseBtn_Click(sender As Object, e As EventArgs) Handles TextBrowseBtn.Click
        Dim saveFileDialog = New SaveFileDialog()
        saveFileDialog.AddExtension = True
        saveFileDialog.DefaultExt = "fngr"
        saveFileDialog.ShowDialog()
        TextFilenameTb.Text = saveFileDialog.FileName
    End Sub

    Private Sub ExportBtn_Click(sender As Object, e As EventArgs) Handles ExportBtn.Click

        Dim tagFilterValues = TagFilter1.GetTagFilterValues()
        Dim dt = MyDictionary.GetDictionaryEntriesPaging("%", tagFilterValues, Integer.MaxValue, 0)

        ExportFingerSpelling.Export(TextFilenameTb.Text, MyDictionary, dt)
        MessageBox.Show("Export finished!")
    End Sub
 
End Class