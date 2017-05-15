Option Explicit On

Imports Microsoft.VisualBasic.Logging
Imports SignWriterStudio.General
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.Settings
Imports Microsoft.VisualBasic.FileIO
Imports System.IO


Public Class SignWriterMenu
    'Dim SerializableSettings As Settings.SerializableSettings = New Settings.SerializableSettings()
    'Private DictionaryConnectionString As String = SerializableSettings.DictionaryConnectionString

    Private DictionaryConnectionString = Database.Dictionary.DatabaseSetup.DictionaryConnectionString
    Friend WithEvents SaveSettingsDialog As New SaveFileDialog
    Friend WithEvents LoadSettingsDialog As New OpenFileDialog
    Dim settingsfilename As String = "Settings.dat"
    Private _swDictForm As Dictionary.SWDictForm

    Private _swDocumentForm As Document.SwDocumentForm

    Private Sub CloseCerrarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CloseCerrarToolStripMenuItem.Click
        Close()
    End Sub

    Sub LoadTranslation()
        ' Put the Imports statements at the beginning of the code module

        ' Put the following code before InitializeComponent()

        'Dim frmThemesTranslationsTableAdapter As New SLVocabularyLists.UITranslationsDataSetTableAdapters.UITranslationsTableAdapter()
        'frmThemesTranslations = frmThemesTranslationsTableAdapter.GetDataByGroupLanguage(My.Settings.UserInterfaceLanguage, "FrmThemes")

        'SetControlText(Me.btnNewTheme, frmThemesTranslations)
        'SetControlText(Me.btnOpenTheme, frmThemesTranslations)
        'SetControlText(Me.btnExportTheme, frmThemesTranslations)
        'SetControlText(Me.btnDeleteTheme, frmThemesTranslations)
        'SetControlText(Me.btnCloseThemes, frmThemesTranslations)
        'SetControlText(Me.FileToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.BackupCopiaDeSeguridadToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.BackupCopiaDeSeguridadToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.RestoreBackupRestaurarCopiaDeSeguridadToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.PrintToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.CloseCerrarToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.ToolStripMenuItem1, frmThemesTranslations)
        'SetControlText(Me.OpenAbrirToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.NewNuevoToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.DeleteBorrarToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.WordsToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.ViewWordsToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.ToolStripMenuItem2, frmThemesTranslations)
        'SetControlText(Me.contentsToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me.ThemesHelpToolStripMenuItem, frmThemesTranslations)
        'SetControlText(Me, frmThemesTranslations)
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Dim swsOptions As New SWOptions

        Dim dialogRes As DialogResult = swsOptions.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            'Action to take when accepted
            swsOptions.Close()
        ElseIf (dialogRes = DialogResult.Cancel) Then
            swsOptions.Close()
        End If

    End Sub

    Private Sub DictionaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles DictionaryToolStripMenuItem.Click
        OpenDictionary()
    End Sub

    Private Sub SignWritingDocumentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles SignWritingDocumentToolStripMenuItem.Click
        OpenDocument()
    End Sub
    Private Sub OpenDictionary()

        Hide()
        If _swDictForm Is Nothing OrElse _swDictForm.IsDisposed Then
            _swDictForm = New Dictionary.SWDictForm(New SWEditor.Editor)
        End If
        _swDictForm.ShowDialog()

        Show()
    End Sub
    Private Sub OpenDocument()
        Hide()
        If _swDocumentForm Is Nothing OrElse _swDocumentForm.IsDisposed Then
            _swDocumentForm = New Document.SwDocumentForm
        End If
        _swDocumentForm.ShowDialog()

        _swDocumentForm.Close()
        Show()
    End Sub
    Private Sub OpenDocument(filename As String)
        Hide()
        _swDocumentForm.OpenDocument(filename)

        Show()
    End Sub

    Private Shared Sub frmSignWriter1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        My.Application.Log.WriteEntry("End of SignWriterStudio " & Microsoft.VisualBasic.DateAndTime.Now().ToString)
    End Sub

    Sub MyHandler(ByVal sender As Object, ByVal args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        MessageBox.Show("There has been an error in the application: " & e.Message)

    End Sub
    Private Sub frmSignWriterMenu_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Try

            Dim cd As AppDomain = AppDomain.CurrentDomain

            AddHandler cd.UnhandledException, AddressOf MyHandler
            CheckforSettingsFile()
            My.Application.Log.WriteEntry("Start of SignWriterStudio " & Microsoft.VisualBasic.DateAndTime.Now().ToString)
            Dim swOptions As New SWOptions
            swOptions.SetCulture()

            Dim connStrUi As String = CreateConnectionString(Paths.Join(Paths.ApplicationPath, "swsui.dat"))
            If CheckSqLiteConnectionString(connStrUi) Then
                UI.Cultures.SetCultureConnectionString(connStrUi)
            Else
                UI.Cultures.SetCultureConnectionString("")
            End If
            Dim connStrIswa As String = CreateConnectionString(Paths.Join(Paths.ApplicationPath, "ISWA2010.dat"))
            If CheckSqLiteConnectionString(connStrIswa) Then
                SymbolCache.Iswa2010.ISWA2010ConnectionString = connStrIswa

            Else
                SymbolCache.Iswa2010.ISWA2010ConnectionString = ""
            End If
            Dim connStrSettings As String = CreateConnectionString(Paths.Join(Paths.ApplicationPath, "Settings.dat"))

            If CheckSqLiteConnectionString(connStrSettings) Then
                SettingsPublic.SettingsConnectionString = connStrSettings

            Else
                SettingsPublic.SettingsConnectionString = ""

            End If

            Dim connStr As String = My.Settings.LastDictionaryString
            If CheckConnectionString(connStr) Then
                My.Settings.LastDictionaryString = connStr
            Else
                My.Settings.LastDictionaryString = ""
            End If
            SetFileAssociation()

            CheckForFiletoOpen()

            Dim swsSetting As New Settings.My.MySettings
            If swsSetting.ShowWalkthroughs Then
                SeeHelp.ShowDialog()
            End If
        Catch ex As Exception
            LogError(ex, "")
            MessageBox.Show(ex.Message)

            Application.Exit()
        End Try
    End Sub
    Private Function CreateConnectionString(filename As String) As String
        Return "data source=""" & filename & """"
    End Function
    Private Shared Sub CheckforSettingsFile()
        Dim pathNeedstoExist As String = Paths.Join(Paths.AllUsersData, "Settings.dat")
        Dim pathSource As String = Paths.Join(Paths.ApplicationPath, "Settings.dat")
        If Not Paths.FileExists(pathNeedstoExist) Then
            Paths.Copy(pathSource, pathNeedstoExist)
        End If

    End Sub
    Private Function CheckConnectionString(ByVal connString As String) As Boolean
        Dim oledbStBuild As New OleDb.OleDbConnectionStringBuilder
        oledbStBuild.ConnectionString = connString
        Dim filename As String = oledbStBuild.DataSource
        If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(filename) AndAlso oledbStBuild.Provider = "Microsoft.Jet.OLEDB.4.0" Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Shared Function CheckSqLiteConnectionString(ByVal connString As String) As Boolean
        Dim csBuilder As New SqlClient.SqlConnectionStringBuilder
        csBuilder.ConnectionString = connString
        If connString.Contains("data source=") Then
            Dim filename As String = csBuilder.DataSource
            If Paths.FileExists(filename) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Sub CheckForFiletoOpen()
        Dim firstParameter As String
        If My.Application.CommandLineArgs.Count > 0 Then
            firstParameter = My.Application.CommandLineArgs(0)

            If firstParameter.Contains(".SWSDoc") Then
                OpenDocument(firstParameter)
            ElseIf firstParameter.Contains(".SWS") Then
                OpenDictionary(firstParameter)
            End If
        End If
    End Sub

    Private Sub OpenDictionary(ByVal filename As String)
        If _swDictForm Is Nothing OrElse _swDictForm.IsDisposed Then
            _swDictForm = New Dictionary.SWDictForm(New SWEditor.Editor)
        End If

        _swDictForm.OpenDictionary(filename)
        _swDictForm.ShowDialog()
        _swDictForm.Close()
    End Sub
    Private Function SettingsFolder() As String
        Dim folder As String = String.Empty
        If Application.ExecutablePath.Contains("Program Files") Then

            Dim virtualstore As String = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VirtualStore\")
            Dim programdrive As String = Path.GetPathRoot(Application.ExecutablePath)
            Dim programfolder As String = Path.GetDirectoryName(Application.ExecutablePath)
            Dim programfolderwithoutroot As String = programfolder.Replace(programdrive, "")

            folder = Path.Combine(virtualstore, programfolderwithoutroot)
        Else
            folder = Path.GetDirectoryName(Application.ExecutablePath)
        End If

        Return folder
    End Function

    Private Sub ExportSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportSettingsToolStripMenuItem.Click
        SaveSettingsDialog.InitialDirectory = SpecialDirectories.MyDocuments
        SaveSettingsDialog.FileName = settingsfilename
        SaveSettingsDialog.ShowDialog()     
    End Sub


    Private Sub ImportSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImportSettingsToolStripMenuItem.Click
        LoadSettingsDialog.InitialDirectory = SpecialDirectories.MyDocuments
        LoadSettingsDialog.FileName = settingsfilename
        LoadSettingsDialog.ShowDialog()
    End Sub

    Private Sub SaveSettingsDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveSettingsDialog.FileOk
        Dim settingsPath As String = Path.Combine(SettingsFolder(), settingsfilename)
        File.Copy(settingsPath, SaveSettingsDialog.FileName)
    End Sub

    Private Sub LoadSettingsDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles LoadSettingsDialog.FileOk
        Dim settingsPath As String = Path.Combine(SettingsFolder(), settingsfilename)
        File.Copy(settingsPath, settingsfilename & Path.GetRandomFileName, True)
        File.Copy(LoadSettingsDialog.FileName, settingsPath, True)
        LoadForms()
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadForms()
    End Sub

    Private Sub BtnDocument_Click(sender As System.Object, e As EventArgs) Handles BtnDocument.Click
        OpenDocument()
    End Sub

    Private Sub BtnDictionary_Click(sender As System.Object, e As EventArgs) Handles BtnDictionary.Click
        OpenDictionary()
    End Sub

    Private Sub LoadForms()
        'ToDo Let choose CacheAllSymbols in options
        SettingsPublic.CacheAllSymbols = False
        If SettingsPublic.CacheAllSymbols Then
            SymbolCache.Iswa2010.SC.LoadAllSymbols()
        End If
        Dim editor As New SWEditor.Editor
        _swDictForm = New Dictionary.SWDictForm(editor)
    End Sub

    Private Shared Sub SetFileAssociation()
        Try
            Dim fa As New Org.Mentalis.Utilities.FileAssociation
            fa.Extension = "SWS"
            fa.ContentType = "application/SignWriterStudio"
            fa.FullName = "SignWriterStudio™ File"
            fa.ProperName = "SWSFile"
            fa.IconPath = Paths.ApplicationPath & "\SignWriterStudio.ico"
            fa.AddCommand("open", Paths.ApplicationPath & "\SignWriterStudio.exe ""%1""")
            fa.Create()
            'MessageBox.Show("SWS assocation successful")

            Dim fa1 As New Org.Mentalis.Utilities.FileAssociation
            fa1.Extension = "SWSDoc"
            fa1.ContentType = "application/SignWriterStudioDocument"
            fa1.FullName = "SignWriterStudio™ Document"
            fa1.ProperName = "SWSDocFile"
            fa1.IconPath = Paths.ApplicationPath & "\SignWriterStudio.ico"
            fa1.AddCommand("open", Paths.ApplicationPath & "\SignWriterStudio.exe ""%1""")
            fa1.Create()

        Catch ex As Exception
            LogError(ex, "")

        End Try
    End Sub

    Private Shared Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim ab As New About()
        ab.ShowDialog()
    End Sub

    Private Sub ShowLogFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowLogFilesToolStripMenuItem.Click
        Process.Start(Application.UserAppDataPath)
    End Sub
End Class