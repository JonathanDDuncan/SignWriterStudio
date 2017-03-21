Option Strict Off
Option Explicit On

Imports System.Data.SQLite
Imports SignWriterStudio.DbTags
Imports SignWriterStudio.General
Imports SignWriterStudio.SQLiteAdapters

''' <summary>
''' Class DatabaseSignList description
''' </summary>
Public Module DatabaseSetup
    ReadOnly Property DictionaryConnectionString() As String
        Get
            Return BuildConnectionString(Settings.My.MySettings.Default.CurrentDictionaryFilename)
        End Get
    End Property
    Property DictionaryFilename() As String
        Get
            Return Settings.My.MySettings.Default.CurrentDictionaryFilename
        End Get
        Set(ByVal fileName As String)
            If Not Microsoft.VisualBasic.FileIO.FileSystem.FileExists(fileName) Then
                fileName = ""
            End If

            Settings.My.MySettings.Default.CurrentDictionaryFilename = fileName
            Settings.My.MySettings.Default.Save()
            My.Settings.Save()
        End Set
    End Property
   
    Friend Function BuildConnectionString(ByVal filename As String) As String
        Return "data source=""" & filename & """"
    End Function
    Public Function CheckDictionary(ByVal connectionString As String, Optional ByVal ask As Boolean = True, Optional ByRef wasUpdated As Boolean = False, Optional ByVal todo As String = "") As Tuple(Of Boolean, String)
        'Try
        Dim ta As New DictionaryDataSetTableAdapters.VersionTableAdapter() '(True)
        'Check if database exists
        Dim csBuilder As New SQLiteConnectionStringBuilder

        Try
            If Not connectionString.Contains("<?xml") Then
                csBuilder.ConnectionString = connectionString
                Dim filename = csBuilder.DataSource
                If My.Computer.FileSystem.FileExists(filename) Then
                    Dim table As New DataTable
                    Dim firstRow As DictionaryDataSet.VersionRow
                    Try
                        ta.Connection.ConnectionString = csBuilder.ConnectionString
                        table = ta.GetData()
                    Catch ex As Exception
                        Return Tuple.Create(False, todo)
                    End Try
                    Const currentVersion As Integer = 2
                    Const currentMajor As Integer = 4
                    Const currentMinor As Integer = 0
                    Dim upgradeQuestion = "You need to upgrade your file '" & filename & "' before continuing. You may not be able to open it in previous versions. Make a backup before continuing. " & VbCrLf() & "Do you want to upgrade your file now? "


                    If table.Rows.Count >= 1 Then
                        firstRow = CType(table.Rows(0), DictionaryDataSet.VersionRow)

                        If firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = currentVersion AndAlso firstRow.Major = currentMajor AndAlso firstRow.Minor = currentMinor Then
                            'Already upgraded to most recent version
                            Return Tuple.Create(True, todo)

                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 0 AndAlso firstRow.Minor = 0 Then
                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                ask = False
                                UpgradeDatabase210(connectionString, VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
                                wasUpdated = True
                                Return CheckDictionary(connectionString, ask, wasUpdated, todo)
                            End If
                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 1 AndAlso firstRow.Minor = 0 Then
                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                ask = False
                                UpgradeDatabase211(connectionString, VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
                                wasUpdated = True
                                Return CheckDictionary(connectionString, ask, wasUpdated, todo)
                            End If

                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 1 AndAlso firstRow.Minor = 1 Then
                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                                ask = False
                                UpgradeDatabase220(connectionString, VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
                                wasUpdated = True
                                Return CheckDictionary(connectionString, ask, wasUpdated, todo)
                            End If
                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 2 AndAlso firstRow.Minor = 0 Then
                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                                ask = False
                                todo = UpgradeDatabase230(connectionString, VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
                                wasUpdated = True
                                Return CheckDictionary(connectionString, ask, wasUpdated, todo)
                            End If
                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 3 AndAlso firstRow.Minor = 0 Then
                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then

                                ask = False
                                todo = UpgradeDatabase240(connectionString, VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
                                wasUpdated = True
                                Return CheckDictionary(connectionString, ask, wasUpdated, todo)
                            End If
                        End If
                    Else
                        Return Tuple.Create(False, todo)
                    End If
                Else
                    Return Tuple.Create(False, todo)
                End If
            Else
                Return Tuple.Create(False, todo)
            End If
        Catch ex As ArgumentException
            Return Tuple.Create(False, todo)
        End Try
        Return Tuple.Create(False, todo)
    End Function

    Private Function VersionString(ByVal idVersion As Long, ByVal major As Long, ByVal minor As Long) As String
        Return idVersion & "." & major & "." & minor
    End Function

    Private Function UpgradeDatabase230(ByVal connectionString As String, ByVal fromVersion As String) As String
        Dim script230 = New List(Of String)

        script230.Add("BEGIN TRANSACTION;")
        script230.Add("CREATE TABLE [Tags] ([IdTag] guid NOT NULL, [Description] text NOT NULL, [Abbreviation] text NULL, [Color] int NULL, [Rank] int NOT NULL, [Parent] guid NULL, CONSTRAINT [PK_Tags] PRIMARY KEY ([IdTag]));")
        script230.Add("CREATE TABLE [TagDictionary] ([IdTagDictionary] guid NOT NULL, [IDDictionary] bigint NOT NULL, [IdTag] guid NOT NULL, CONSTRAINT [sqlite_autoindex_TagDictionary_1] PRIMARY KEY ([IdTagDictionary]), FOREIGN KEY ([IdTag]) REFERENCES [Tags] ([IdTag]) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY ([IDDictionary]) REFERENCES [Dictionary] ([IDDictionary]) ON DELETE CASCADE ON UPDATE CASCADE);CREATE INDEX [TagDictionary_IdTag_TagDictionary] ON [TagDictionary] ([IdTag] ASC);CREATE INDEX [TagDictionary_IDDictionary_TagDictionary] ON [TagDictionary] ([IDDictionary] ASC);")
        script230.Add("CREATE INDEX [SignWritingSorting] ON [Dictionary] ([Sorting] ASC);")

        script230.Add("UPDATE Version SET Major =3, Minor = 0, DatabaseName = ""Dictionary"", DatabaseType = ""Dictionary"" WHERE IDVersion=2;")

        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank])VALUES('5bfcb134-1689-4f65-9deb-4934e7c32585', 'Misc','Misc', -8355712,3);")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank])VALUES('1409bab8-9031-4249-b95e-81695a2a7f7c', 'SignLists','', -8355712,1);")
        script230.Add("INSERT INTO [Tags] ([IdTag] ,[Description],[Abbreviation],[Color],[Rank])VALUES('5f78f958-a299-482c-9412-7eca30cda394', 'Parts Of Speech','', -8355712, 2);")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('b9e38963-59e4-4878-ad68-922911dcce17', 'Do Not Export','', -8355712,1,'5bfcb134-1689-4f65-9deb-4934e7c32585');")

        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('6d6b67b2-1ab5-4c68-8bae-2c82218ac489', 'Noun','', -8355712,1,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('02572ed0-e09e-4d6b-9434-722527d57800', 'Pronoun','', -8355712,2,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('a58331ae-d062-4752-8c23-f15c46de2ce7', 'Plain Verb','', -8355712,3,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('a649c66b-b282-4207-9b28-d6b17dff6a9b', 'Inflecting/Indicating Verb','', -8355712,4,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('f7a97000-18b5-4aac-8dfb-ac47fe212ab5', 'Spatial Verb','', -8355712,5,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('402c809d-5999-424d-8e94-46b093decae3', 'Adjective','', -8355712,6,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('0ad7fa69-7617-411a-8f93-a2bdafce801d', 'Adverb','', -8355712,7,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('0db55ecb-7726-4e69-810a-39c001753c5e', 'Conjunction','', -8355712,8,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('6556dfb2-c53d-4628-b256-580b87ee02db', 'Interjection','', -8355712,9,'5f78f958-a299-482c-9412-7eca30cda394');")
        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('6769d1f7-c905-4772-8cbb-3db176055016', 'Preposition','', -8355712,10,'5f78f958-a299-482c-9412-7eca30cda394');")


        script230.Add("COMMIT;")

        RunUpgradeScript(connectionString, fromVersion, script230)

        AddDoNotExportTags()

        Return "AddDoNotExportTags"
    End Function

    Private Function UpgradeDatabase240(ByVal connectionString As String, ByVal fromVersion As String) As String
        Dim script240 = New List(Of String)

        script240.Add("BEGIN TRANSACTION;")

        script240.Add("ALTER TABLE Dictionary ADD COLUMN PuddleTop TEXT;")

        script240.Add("UPDATE Version SET Major =4, Minor = 0, DatabaseName = ""Dictionary"", DatabaseType = ""Dictionary"" WHERE IDVersion=2;")


        script240.Add("COMMIT;")

        RunUpgradeScript(connectionString, fromVersion, script240)



        Return "Add PuddleTop field"
    End Function

    Private Sub AddDoNotExportTags()
        Dim path = DictionaryFilename
        Dim listIdDictionary = DbDictionary.GetIdDoNotExport(path)
        Dim affectedRows = DbTagsDictionary.InsertDoNotExportTag(path, listIdDictionary)
    End Sub

    Private Sub UpgradeDatabase220(ByVal connectionString As String, ByVal fromVersion As String)
        RunUpgradeScript(connectionString, fromVersion, "Upgrade220.sql")
    End Sub

    Private Sub UpgradeDatabase211(ByVal connectionString As String, ByVal fromVersion As String)
        RunUpgradeScript(connectionString, fromVersion, "Upgrade211.sql")
    End Sub
    Private Sub UpgradeDatabase210(ByVal connectionString As String, ByVal fromVersion As String)
        RunUpgradeScript(connectionString, fromVersion, "Upgrade210.sql")
    End Sub

    Private Sub RunUpgradeScript(ByVal connectionString As String, ByVal fromVersion As String, ByVal upgradeScript As String)
       
        CreateBackup(connectionString, fromVersion)
        Dim sqliteConnection1 As SQLiteConnection = New SQLiteConnection(connectionString)
        Dim cmd As SQLiteCommand = New SQLiteCommand()

        Dim queryStr = IO.File.ReadLines(upgradeScript)

        cmd.CommandType = CommandType.Text
        cmd.Connection = sqliteConnection1
        sqliteConnection1.Open()

        For Each line In queryStr
            cmd.CommandText = line
            cmd.ExecuteNonQuery()
        Next

        sqliteConnection1.Close()
    End Sub

    Private Sub RunUpgradeScript(ByVal connectionString As String, ByVal fromVersion As String, ByVal queryStr As IEnumerable(Of String))

        CreateBackup(connectionString, fromVersion)
        Dim sqliteConnection1 As SQLiteConnection = New SQLiteConnection(connectionString)
        Dim cmd As SQLiteCommand = New SQLiteCommand()

        cmd.CommandType = CommandType.Text
        cmd.Connection = sqliteConnection1
        sqliteConnection1.Open()

        For Each line In queryStr
            cmd.CommandText = line
            cmd.ExecuteNonQuery()
        Next

        sqliteConnection1.Close()
    End Sub

    Private Function GetConnectionString() As String
        Dim taVer As New DictionaryDataSetTableAdapters.VersionTableAdapter
        Return taVer.Connection.ConnectionString
    End Function

    Private Sub CreateBackup(ByVal connectionString As String, ByVal fromVersion As String)
        Dim filename As String = StringUtil.GetConnectionFilename(connectionString)
        Dim newFilename = IO.Path.Combine(IO.Path.GetDirectoryName(filename), IO.Path.GetFileNameWithoutExtension(filename) + "_" + fromVersion + ".bak")
        IO.File.Copy(filename, newFilename, True)
    End Sub
End Module
