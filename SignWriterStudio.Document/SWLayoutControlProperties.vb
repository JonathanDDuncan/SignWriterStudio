Imports SignWriterStudio.SWClasses

Public Class SWLayoutControlProperties
    Friend DocumentSign As New SwDocumentSign

    Private Sub SWLayoutControlProperties_KeyDown(ByVal sender As Object, ByVal e As Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Help.ShowHelp(Me, "SignWriterStudio.chm", "signeditor.htm")
    End Sub

    Private Sub SWLayoutControlProperties_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        UICulturesTableAdapter.Fill(swsUIDataSet.UICultures)
        UISignLanguagesTableAdapter.Fill(swsUIDataSet.UISignLanguages)
        TBGloss.Text = DocumentSign.Gloss
        TBGlosses.Text = DocumentSign.Glosses
        CBGlossLang.SelectedValue = UI.Cultures.IdCulture(DocumentSign.LanguageIso)
        CBSLanguage.SelectedValue = UI.Cultures.GetIdSignLanguages(DocumentSign.SignLanguageIso)
        LBSignPuddleID.Text = DocumentSign.SignPuddleId
        LBSignWriterGUID.Text = DocumentSign.SignWriterGuid.ToString
    End Sub

    Private Sub BtnAccept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnAccept.Click
        If String.IsNullOrEmpty(TBGloss.Text) Then
            MessageBox.Show("You must enter a gloss.")
        ElseIf CBGlossLang.SelectedItem Is Nothing Then
            MessageBox.Show("You must enter a gloss Language.")
        ElseIf CBSLanguage.SelectedItem Is Nothing Then
            MessageBox.Show("You must enter a sign Language.")
        Else
            DocumentSign.Gloss = TBGloss.Text
            DocumentSign.Glosses = TBGlosses.Text
            DocumentSign.SetlanguageIso(CBGlossLang.SelectedValue)
            DocumentSign.SetSignLanguageIso(CBSLanguage.SelectedValue)
            DialogResult = Windows.Forms.DialogResult.OK
            Close()
        End If

    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnCancel.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Close()
    End Sub
 
End Class