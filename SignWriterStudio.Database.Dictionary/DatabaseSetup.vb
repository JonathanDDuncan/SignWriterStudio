'Option Strict On
'Option Explicit On

'Imports System.Data.SQLite
'Imports SignWriterStudio.General
'Imports SignWriterStudio.Db.Tags

' ''' <summary>
' ''' Class DatabaseSignList description
' ''' </summary>
'Public Module DatabaseSetup
'    Property DictionaryConnectionString() As String
'        Get
'            Return My.Settings.DictionaryConnectionString

'        End Get
'        Set(ByVal value As String)
'            SetDictionaryConnectionString(value)
'        End Set
'    End Property

'    Friend Sub SetDictionaryConnectionString(ByVal fileName As String)
'        If Microsoft.VisualBasic.FileIO.FileSystem.FileExists(fileName) Then
'            My.Settings.DictionaryConnectionString = BuildConnectionString(fileName)
'            'Remember last database loaded
'            Settings.My.MySettings.Default.LastDictionaryString = fileName
'            Settings.My.MySettings.Default.Save()
'        Else
'            My.Settings.DictionaryConnectionString = BuildConnectionString("")
'        End If
'        My.Settings.Save()
'    End Sub
'    Friend Function BuildConnectionString(ByVal filename As String) As String
'        Return "data source=""" & filename & """"
'    End Function
'    Public Function CheckDictionary(Optional ByVal ask As Boolean = True, Optional ByRef wasUpdated As Boolean = False) As Boolean
'        'Try
'        Dim ta As New DictionaryDataSetTableAdapters.VersionTableAdapter() '(True)
'        'Check if database exists
'        Dim csBuilder As New SQLiteConnectionStringBuilder

'        Try
'            If Not ta.Connection.ConnectionString.Contains("<?xml") Then
'                csBuilder.ConnectionString = ta.Connection.ConnectionString
'                Dim filename = csBuilder.DataSource
'                If My.Computer.FileSystem.FileExists(filename) Then
'                    Dim table As New DataTable
'                    Dim firstRow As DictionaryDataSet.VersionRow
'                    Try
'                        table = ta.GetData()
'                    Catch ex As Exception
'                        Return False
'                    End Try
'                    Const currentVersion As Integer = 2
'                    Const currentMajor As Integer = 3
'                    Const currentMinor As Integer = 0
'                    Dim upgradeQuestion = "You need to upgrade your file '" & filename & "' before continuing. You may not be able to open it in previous versions. Make a backup before continuing. " & VbCrLf() & "Do you want to upgrade your file now? "


'                    If table.Rows.Count >= 1 Then
'                        firstRow = CType(table.Rows(0), DictionaryDataSet.VersionRow)

'                        If firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = currentVersion AndAlso firstRow.Major = currentMajor AndAlso firstRow.Minor = currentMinor Then
'                            'Already upgraded to most recent version
'                            Return True

'                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 0 AndAlso firstRow.Minor = 0 Then
'                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then
'                                ask = False
'                                UpgradeDatabase210(VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
'                                wasUpdated = True
'                                Return CheckDictionary(ask, wasUpdated)
'                            End If
'                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 1 AndAlso firstRow.Minor = 0 Then
'                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then
'                                ask = False
'                                UpgradeDatabase211(VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
'                                wasUpdated = True
'                                Return CheckDictionary(ask, wasUpdated)
'                            End If

'                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 1 AndAlso firstRow.Minor = 1 Then
'                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then

'                                ask = False
'                                UpgradeDatabase220(VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
'                                wasUpdated = True
'                                Return CheckDictionary(ask, wasUpdated)
'                            End If
'                        ElseIf firstRow.DatabaseName = "Dictionary" AndAlso firstRow.DatabaseType = "Dictionary" AndAlso firstRow.IDVersion = 2 AndAlso firstRow.Major = 2 AndAlso firstRow.Minor = 0 Then
'                            If Not ask OrElse MessageBox.Show(upgradeQuestion, "Upgrade file", MessageBoxButtons.YesNo) = DialogResult.Yes Then

'                                ask = False
'                                UpgradeDatabase230(VersionString(firstRow.IDVersion, firstRow.Major, firstRow.Minor))
'                                wasUpdated = True
'                                Return CheckDictionary(ask, wasUpdated)
'                            End If
'                        End If
'                    Else
'                        Return False
'                    End If
'                Else
'                    Return False
'                End If
'            Else
'                Return False
'            End If
'        Catch ex As ArgumentException
'            Return False
'        End Try
'        Return False
'    End Function

'    Private Function VersionString(ByVal idVersion As Long, ByVal major As Long, ByVal minor As Long) As String
'        Return idVersion & "." & major & "." & minor
'    End Function

'    Private Sub UpgradeDatabase230(ByVal fromVersion As String)
'        Dim script230 = New List(Of String)

'        script230.Add("BEGIN TRANSACTION;")
'        script230.Add("CREATE TABLE [Tags] ([IdTag] guid NOT NULL, [Description] text NOT NULL, [Abbreviation] text NULL, [Color] int NULL, [Rank] int NOT NULL, [Parent] guid NULL, CONSTRAINT [PK_Tags] PRIMARY KEY ([IdTag]));")
'        script230.Add("CREATE TABLE [TagDictionary] ([IdTagDictionary] guid NOT NULL, [IDDictionary] bigint NOT NULL, [IdTag] guid NOT NULL, CONSTRAINT [sqlite_autoindex_TagDictionary_1] PRIMARY KEY ([IdTagDictionary]), FOREIGN KEY ([IdTag]) REFERENCES [Tags] ([IdTag]) ON DELETE CASCADE ON UPDATE CASCADE, FOREIGN KEY ([IDDictionary]) REFERENCES [Dictionary] ([IDDictionary]) ON DELETE CASCADE ON UPDATE CASCADE);CREATE INDEX [TagDictionary_IdTag_TagDictionary] ON [TagDictionary] ([IdTag] ASC);CREATE INDEX [TagDictionary_IDDictionary_TagDictionary] ON [TagDictionary] ([IDDictionary] ASC);")
'        script230.Add("UPDATE Version SET Major =3, Minor = 0, DatabaseName = ""Dictionary"", DatabaseType = ""Dictionary"" WHERE IDVersion=2;")

'        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank])VALUES('5bfcb134-1689-4f65-9deb-4934e7c32585', 'Misc','Misc', 0,3);")
'        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank])VALUES('1409bab8-9031-4249-b95e-81695a2a7f7c', 'SignLists','', 0,1);")
'        script230.Add("INSERT INTO [Tags] ([IdTag] ,[Description],[Abbreviation],[Color],[Rank])VALUES('5f78f958-a299-482c-9412-7eca30cda394', 'Parts Of Speech','', 0, 2);")
'        script230.Add("INSERT INTO [Tags] ([IdTag],[Description],[Abbreviation],[Color],[Rank],[Parent])VALUES('b9e38963-59e4-4878-ad68-922911dcce17', 'Do Not Export','', 0,1,'5bfcb134-1689-4f65-9deb-4934e7c32585');")
'        script230.Add("COMMIT;")

'        RunUpgradeScript(fromVersion, script230)

'        AddDoNotExportTags()
'    End Sub

'    Private Sub AddDoNotExportTags()
'        'Dim connectionString = GetConnectionString()
'        'Dim path = GetConnectionFilename(connectionString)
'        'Dim DictionaryEntriesMarkedasPrivate = DbDictionary.GetDictionaryEntries(path, " [isPrivate] ")
'        'DbTags.CreateDoNotExportTags(DictionaryEntriesMarkedasPrivate)

'    End Sub

'    Private Sub UpgradeDatabase220(ByVal fromVersion As String)
'        RunUpgradeScript(fromVersion, "Upgrade220.sql")
'    End Sub

'    Private Sub UpgradeDatabase211(ByVal fromVersion As String)
'        RunUpgradeScript(fromVersion, "Upgrade211.sql")
'    End Sub
'    Private Sub UpgradeDatabase210(ByVal fromVersion As String)
'        RunUpgradeScript(fromVersion, "Upgrade210.sql")
'    End Sub
'    Private Sub RunUpgradeScript(ByVal fromVersion As String, ByVal upgradeScript As String)
'        Dim connectionString = GetConnectionString()

'        CreateBackup(connectionString, fromVersion)
'        Dim sqliteConnection1 As SQLiteConnection = New SQLiteConnection(connectionString)
'        Dim cmd As SQLiteCommand = New SQLiteCommand()

'        Dim queryStr = IO.File.ReadLines(upgradeScript)

'        cmd.CommandType = CommandType.Text
'        cmd.Connection = sqliteConnection1
'        sqliteConnection1.Open()


'        For Each line In queryStr
'            cmd.CommandText = line
'            cmd.ExecuteNonQuery()
'        Next



'        sqliteConnection1.Close()
'    End Sub
'    Private Sub RunUpgradeScript(ByVal fromVersion As String, ByVal queryStr As IEnumerable(Of String))
'        Dim connectionString = GetConnectionString()

'        CreateBackup(connectionString, fromVersion)
'        Dim sqliteConnection1 As SQLiteConnection = New SQLiteConnection(connectionString)
'        Dim cmd As SQLiteCommand = New SQLiteCommand()

'        cmd.CommandType = CommandType.Text
'        cmd.Connection = sqliteConnection1
'        sqliteConnection1.Open()


'        For Each line In queryStr
'            cmd.CommandText = line
'            cmd.ExecuteNonQuery()
'        Next



'        sqliteConnection1.Close()
'    End Sub

'    Private Function GetConnectionString() As String
'        Dim taVer As New DictionaryDataSetTableAdapters.VersionTableAdapter
'        Return taVer.Connection.ConnectionString
'    End Function

'    Private Sub CreateBackup(ByVal connectionString As String, ByVal fromVersion As String)
'        Dim filename As String = GetConnectionFilename(connectionString)
'        Dim newFilename = IO.Path.Combine(IO.Path.GetDirectoryName(filename), IO.Path.GetFileNameWithoutExtension(filename) + "_" + fromVersion + ".bak")
'        IO.File.Copy(filename, newFilename, True)
'    End Sub

'    Private Function GetConnectionFilename(ByVal connectionString As String) As String

'        Dim filename = connectionString.Replace("data source=""", "")
'        filename = filename.Substring(0, filename.Length - 1)
'        Return filename
'    End Function

'    Private Function CreateConnectionStringFromPath(ByVal connectionString As String) As String

'        Return "data source=""" & connectionString & """"
'        'data source="C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO.SWS"
'    End Function

'End Module
