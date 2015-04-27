Imports System.Windows.Forms

Public Class SeeHelp

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Help.ShowHelp(Me, "SignWriterStudio.chm", "walkthroughs.htm")
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub SeeHelp_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SignWriterStudio.Settings.SettingsPublic.ShowWalkthroughs = Me.CheckBox1.Checked
        My.Settings.Save()
    End Sub

    Private Sub SeeHelp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CheckBox1.Checked = SignWriterStudio.Settings.SettingsPublic.ShowWalkthroughs
    End Sub
End Class
