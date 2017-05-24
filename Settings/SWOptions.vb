Imports System.Threading
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Configuration

Public Class SWOptions
    Dim DTfrmOptionsTranslations As DataTable

    Private Sub TSWOptions_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Help.ShowHelp(Me, "SignWriterStudio.chm", "options.htm")
        End If
    End Sub
    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.KeyPreview = True
        Me.CBUserInterfaceSignLang.DataSource = UI.Cultures.SignLanguages
        Me.CBUserInterfaceSignLang.DisplayMember = "SignLanguage"
        Me.CBUserInterfaceSignLang.ValueMember = "IDSignLanguage"
        If Not My.Settings.UserInterfaceSignLanguage = 0 Then
            Me.CBUserInterfaceSignLang.SelectedValue = My.Settings.UserInterfaceSignLanguage
        End If

        Me.CBDefaulSignLang.DataSource = UI.Cultures.SignLanguages
        Me.CBDefaulSignLang.DisplayMember = "SignLanguage"
        Me.CBDefaulSignLang.ValueMember = "IDSignLanguage"
        If Not My.Settings.DefaultSignLanguage = 0 Then
            Me.CBDefaulSignLang.SelectedValue = My.Settings.DefaultSignLanguage
        End If

        Me.CBGlossLang1.DataSource = UI.Cultures.Cultures
        Me.CBGlossLang1.DisplayMember = "LanguageCountryRegion"
        Me.CBGlossLang1.ValueMember = "IDCulture"
        If Not My.Settings.FirstGlossLanguage = 0 Then
            Me.CBGlossLang1.SelectedValue = My.Settings.FirstGlossLanguage
        End If

        Me.CBGlossLang2.DataSource = UI.Cultures.Cultures
        Me.CBGlossLang2.DisplayMember = "LanguageCountryRegion"
        Me.CBGlossLang2.ValueMember = "IDCulture"
        If Not My.Settings.SecondGlossLanguage = 0 Then
            Me.CBGlossLang2.SelectedValue = My.Settings.SecondGlossLanguage
        End If

        Me.CBUserInterfaceLang.DataSource = UI.Cultures.Cultures
        Me.CBUserInterfaceLang.DisplayMember = "LanguageCountryRegion"
        Me.CBUserInterfaceLang.ValueMember = "IDCulture"
        If Not My.Settings.UserInterfaceLanguage = 0 Then
            Me.CBUserInterfaceLang.SelectedValue = My.Settings.UserInterfaceLanguage
        End If

        Me.CBBilingual.Checked = My.Settings.BilingualMode
        BilingualChanged()
        SetCulture()

    End Sub
    Public Sub SetCulture()
        If Me.CBUserInterfaceLang.SelectedValue IsNot Nothing Then
            Dim SelectedCulture As String = UI.Cultures.GetCultureName(Me.CBUserInterfaceLang.SelectedValue)
            If SelectedCulture.Contains("-") Then
                Thread.CurrentThread.CurrentCulture = New CultureInfo(SelectedCulture)
            Else
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            End If

            Thread.CurrentThread.CurrentUICulture = New CultureInfo(SelectedCulture)
        End If
    End Sub

    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        My.Settings.UserInterfaceSignLanguage = Me.CBUserInterfaceSignLang.SelectedValue
        My.Settings.DefaultSignLanguage = Me.CBDefaulSignLang.SelectedValue
        My.Settings.FirstGlossLanguage = Me.CBGlossLang1.SelectedValue
        My.Settings.SecondGlossLanguage = Me.CBGlossLang2.SelectedValue
        My.Settings.UserInterfaceLanguage = Me.CBUserInterfaceLang.SelectedValue
        My.Settings.BilingualMode = Me.CBBilingual.Checked
        My.Settings.Save()
        SetCulture()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
     
 
    Private Sub LoadTranslations()
        'TODO Translations
        'Databases.UI.UICGetTranslation("", "")

    End Sub

    Private Sub CloseBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CBBilingual_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBBilingual.CheckedChanged
        BilingualChanged()
    End Sub
    Private Sub BilingualChanged()
        If CBBilingual.Checked Then
            CBGlossLang2.Enabled = True
            Me.LBSecondGlossLang.Enabled = True
        Else
            CBGlossLang2.Enabled = False
            Me.LBSecondGlossLang.Enabled = False

        End If
    End Sub
     

    Private Sub CBDefaulSignLang_Validated(sender As Object, e As EventArgs) Handles CBDefaulSignLang.Validated
        If CBDefaulSignLang.SelectedValue Is Nothing Then
            CBDefaulSignLang.Focus()

            MessageBox.Show("You must choose an sign language.", "Sign language.")
            'MessageBox.Show(Databases.UI.UICGetTranslation("", ""))
        End If
    End Sub

    Private Sub CBUserInterfaceLang_Validated(sender As Object, e As EventArgs) Handles CBUserInterfaceLang.Validated
        If CBUserInterfaceLang.SelectedValue Is Nothing Then
            CBUserInterfaceLang.Focus()

            MessageBox.Show("You must choose an interface language.", "User Interface language.")

        End If
    End Sub

    Private Sub CBUserInterfaceSignLang_Validated(sender As Object, e As EventArgs) Handles CBUserInterfaceSignLang.Validated
        If CBUserInterfaceSignLang.SelectedValue Is Nothing Then
            CBUserInterfaceSignLang.Focus()
            MessageBox.Show("You must choose an interface sign language.", "User Interface sign language.")

        End If
    End Sub
End Class
