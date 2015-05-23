Option Explicit On
Imports SignWriterStudio.General
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.Settings


Public Class SignWriterMenu
    'Friend CA As ClientApp

    'Friend AcercaDE As New AcercaDE
    Friend WithEvents SaveSettingsDialog As New SaveFileDialog
    Friend WithEvents LoadSettingsDialog As New OpenFileDialog
    'Friend WithEvents monitor As EQATEC.Analytics.Monitor.IAnalyticsMonitor = EQATEC.Analytics.Monitor.AnalyticsMonitorFactory.Create("7A55FE8188FD4072B11C3EA5D30EB7F9")
    Private _swDictForm As Dictionary.SWDictForm
    'Private SignLists As SignWriterStudio.SignList.SignLists
    Private _swDocumentForm As Document.SwDocumentForm
    'Private ImportExport As Document.ImportExport
    'Sub NewVersion(ByVal sender As Object, ByVal e As EQATEC.Analytics.Monitor.VersionAvailableEventArgs) Handles monitor.VersionAvailable
    '    MessageBox.Show("Version " & e.OfficialVersion.ToString & " is available")
    'End Sub

    Private Sub CloseCerrarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles CloseCerrarToolStripMenuItem.Click
        Close()
    End Sub

    'Private Sub HelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    Help.ShowHelp(Me, HelpProvider1.HelpNamespace)
    'End Sub
    'Private Sub indexToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    ' Show index of the help file.
    '    Help.ShowHelpIndex(Me, hpAdvancedCHM.HelpNamespace)
    'End Sub

    'Private Sub searchToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    ' Show the search tab of the help file.
    '    Help.ShowHelp(Me, hpAdvancedCHM.HelpNamespace, HelpNavigator.Find, "")
    'End Sub

    'Private Sub ThemesHelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    Help.ShowHelp(Me, "SignWriterStudio.chm", "mainmenu.htm")
    'End Sub
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
        If (dialogRes = Windows.Forms.DialogResult.OK) Then
            'Action to take when accepted
            swsOptions.Close()
        ElseIf (dialogRes = Windows.Forms.DialogResult.Cancel) Then
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
        'If _swDocumentForm Is Nothing OrElse _swDocumentForm.IsDisposed Then
        '    _swDocumentForm = New Document.SwDocumentForm
        'End If
        '_swDocumentForm.ShowDialog()

        '_swDocumentForm.Close()
        Show()
    End Sub

    Private Shared Sub frmSignWriter1_FormClosing(ByVal sender As Object, ByVal e As Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        My.Application.Log.WriteEntry("End of SignWriterStudio " & Microsoft.VisualBasic.DateAndTime.Now().ToString)
    End Sub

    Sub MyHandler(ByVal sender As Object, ByVal args As UnhandledExceptionEventArgs)
        Dim e As Exception = DirectCast(args.ExceptionObject, Exception)
        MessageBox.Show("There has been an error in the application: " & e.Message)
        'monitor.TrackException(e)
    End Sub
    Private Sub frmSignWriterMenu_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            'StoreKey.GenKey_SaveInContainer("SignWriterStudio")
            'StoreKey.GenKey_SaveInContainer("software")
            'Set up culture in thread for this session
            Dim cd As AppDomain = AppDomain.CurrentDomain

            AddHandler cd.UnhandledException, AddressOf MyHandler
            CheckforSettingsFile()
            My.Application.Log.WriteEntry("Start of SignWriterStudio " & Microsoft.VisualBasic.DateAndTime.Now().ToString)
            Dim swOptions As New SWOptions
            swOptions.SetCulture()

            'CA = New ClientApp(Me.AcercaDE)
            Dim connStrUi As String = CreateConnectionString(Paths.Join(Paths.ApplicationPath, "swsui.dat"))
            If CheckSQLiteConnectionString(connStrUi) Then
                UI.Cultures.SetCultureConnectionString(connStrUi)
            Else
                UI.Cultures.SetCultureConnectionString("")
            End If
            Dim connStrIswa As String = CreateConnectionString(Paths.Join(Paths.ApplicationPath, "ISWA2010.dat"))
            If CheckSQLiteConnectionString(connStrIswa) Then
                SymbolCache.Iswa2010.ISWA2010ConnectionString = connStrIswa

            Else
                SymbolCache.Iswa2010.ISWA2010ConnectionString = ""
            End If
            Dim connStrSettings As String = CreateConnectionString(Paths.Join(Paths.ApplicationPath, "Settings.dat"))
            'MessageBox.Show(connStrSettings)
            If CheckSQLiteConnectionString(connStrSettings) Then
                SettingsPublic.SettingsConnectionString = connStrSettings

            Else
                SettingsPublic.SettingsConnectionString = ""
                'MessageBox.Show("Could not find settings file")
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
            'NotContinue()
            Application.Exit()
        End Try
    End Sub
    Private Function CreateConnectionString(filename As String) As String
        Return "data source=""" & Filename & """"
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
        oledbStBuild.ConnectionString = ConnString
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
    'Private Sub OpenDocument(ByVal filename As String)
    '    'If SWDocumentForm Is Nothing OrElse SWDocumentForm.IsDisposed Then
    '    '    SWDocumentForm = New Document.SWDocumentForm
    '    'End If

    '    'SWDocumentForm.OpenDocument(Filename)
    '    'SWDocumentForm.ShowDialog()
    '    'SWDocumentForm.Close()
    'End Sub
    Private Sub OpenDictionary(ByVal filename As String)
        If _swDictForm Is Nothing OrElse _swDictForm.IsDisposed Then
            _swDictForm = New Dictionary.SWDictForm(New SWEditor.Editor)
        End If

        _swDictForm.OpenDictionary(Filename)
        _swDictForm.ShowDialog()
        _swDictForm.Close()
    End Sub
    'Private Sub OpenSignWriterStudioFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    OpenFileDialog1.InitialDirectory = ""
    '    OpenFileDialog1.ShowDialog()
    'End Sub

    'Private Sub NewSignWriterStudioFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)
    '    SaveFileDialog1.ShowDialog()
    'End Sub

    'Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs)

    'End Sub

    Private Sub ExportSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ExportSettingsToolStripMenuItem.Click
        MessageBox.Show("This option has been temporarily disabled because it is not yet fully functional.")

        'ExportSettings()
    End Sub

    'Private Sub ExportSettings()
    '    Dim ss As New SerializableSettings
    '    ss.Save()

    '    SaveSettingsDialog.AddExtension = True
    '    SaveSettingsDialog.AutoUpgradeEnabled = True
    '    SaveSettingsDialog.CheckPathExists = True
    '    SaveSettingsDialog.DefaultExt = "SWSSettings"
    '    SaveSettingsDialog.ShowDialog()
    'End Sub

    Private Sub ImportSettingsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles ImportSettingsToolStripMenuItem.Click
        MessageBox.Show("This option has been temporarily disabled because it is not yet fully functional.")


        'LoadSettingsDialog.AddExtension = True
        'LoadSettingsDialog.AutoUpgradeEnabled = True
        'LoadSettingsDialog.CheckPathExists = True
        'LoadSettingsDialog.DefaultExt = "SWSSettings"
        'LoadSettingsDialog.ShowDialog()

    End Sub

    Private Sub SaveSettingsDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveSettingsDialog.FileOk
        IO.File.Copy(Paths.Join(Paths.AllUsersData, "Settings.dat"), SaveSettingsDialog.FileName, True)
    End Sub

    Private Sub LoadSettingsDialog_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles LoadSettingsDialog.FileOk
        Dim ss As New SerializableSettings
        IO.File.Copy(SaveSettingsDialog.FileName, Paths.Join(Paths.AllUsersData, "Settings.dat"), True)
        ss.Load()
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
            'MessageBox.Show("SWSDOC assocation successful")
        Catch ex As Exception
            LogError(ex, "")
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Exception ")
        End Try


    End Sub

    Private Shared Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim ab As New About()
        ab.ShowDialog()
    End Sub
End Class