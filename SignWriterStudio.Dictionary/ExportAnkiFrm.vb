﻿Imports SignWriterStudio.SWClasses

Public Class ExportAnkiFrm
    Public Property MyDictionary() As SWDict
    Private Sub TextBrowseBtn_Click(sender As Object, e As EventArgs) Handles TextBrowseBtn.Click
        Dim saveFileDialog = New SaveFileDialog()
        saveFileDialog.AddExtension = True
        saveFileDialog.DefaultExt = "txt"
        saveFileDialog.ShowDialog()
        TextFilenameTb.Text = saveFileDialog.FileName
    End Sub

    Private Sub ExportBtn_Click(sender As Object, e As EventArgs) Handles ExportBtn.Click

        Dim tagFilterValues = TagFilter1.GetTagFilterValues()
        Dim dt = MyDictionary.GetDictionaryEntriesPaging("%", tagFilterValues, Integer.MaxValue, 0)

        ExportAnki.ExportExternalPng(TextFilenameTb.Text, PNGFolderTb.Text, MyDictionary, dt)
        MessageBox.Show("Export finished!")
    End Sub

    Private Sub PNGBrowseBtn_Click(sender As Object, e As EventArgs) Handles PNGBrowseBtn.Click
        Dim folderBrowserDialog = New FolderBrowserDialog

        folderBrowserDialog.ShowDialog()
        PNGFolderTb.Text = folderBrowserDialog.SelectedPath
    End Sub

End Class