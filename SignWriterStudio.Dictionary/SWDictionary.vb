Option Strict Off
Imports System.ComponentModel
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Data.OleDb
Imports Newtonsoft.Json
Imports SignWriterStudio.DbTags
Imports DropDownControls.FilteredGroupedComboBox
Imports System.Dynamic
Imports Microsoft.VisualBasic.FileIO
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters
Imports SignWriterStudio.Settings
Imports SignWriterStudio.General
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.SWClasses
Imports SPML
Imports SignWriterStudio.SWEditor
Imports System.Data.SQLite
Imports System.Xml
Imports System.Text
Imports System.Data.SqlClient
Imports System.Linq

Public Class SWDictForm
    Dim WithEvents SPMLImportbw As BackgroundWorker ' With {.WorkerReportsProgress = True}
    Dim WithEvents SPMLExportbw As New BackgroundWorker With {.WorkerReportsProgress = True}
    Dim SWEditorProgressBar As New Progress
    Private myExportSettings As New ExportSettings

    Private Editor As Editor


    Dim _myDictionary As New SWDict(DictionaryConnectionString)
    Dim ColumnClicked As DictionaryColumn
    Dim ClickedCell As DataGridViewCell
    Public CallingForm As Form
    Public IDDictionaryResult As Integer
    Dim isLoading As Boolean = True
    Private _DictionaryLoaded As Boolean = False

    Public Property DictionaryLoaded() As Boolean
        Get
            Return _DictionaryLoaded
        End Get
        Set(ByVal value As Boolean)
            _DictionaryLoaded = value
            DictLoadedChanged()
        End Set
    End Property

    Public Enum DictionaryColumn
        Photo
        Sign
        SWriting
    End Enum

    Public Property CbGloss() As String
        Get
            Return CBGloss1.SelectedValue
        End Get
        Set(ByVal value As String)
            CBGloss1.SelectedValue = value
        End Set
    End Property

    Private Sub SWDictForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        SaveDataGrid()
    End Sub

    Private Sub SWDict_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        KeyPreview = True
        WindowState = FormWindowState.Maximized
        LoadDictionary()
    End Sub

    Private Sub LoadDictionary(Optional ask As Boolean = True)
        isLoading = True
        SetPuddleMenu(False)
        SWSignSource.Visible = False
        SignSource.Visible = False
        PhotoSource.Visible = False
        AddHandler Pager1.PageChanged, AddressOf PageChanged

        CheckDictionary(DictionaryConnectionString, ask)

        SetupDictionary(DictionaryConnectionString)
        ShowButtons()

        SWEditorProgressBar.Text = "SignWriter Studio™ Converting data ..."
        SWEditorProgressBar.ProgressBar1.Minimum = 0
        SWEditorProgressBar.ProgressBar1.Maximum = 100
        SWEditorProgressBar.ProgressBar1.Value = 0

        isLoading = False
    End Sub
    Private Sub SetupDictionary(connectionstring As String)
        _myDictionary = New SWDict(connectionstring)
        _myDictionary.DictionaryBindingSource1.DataSource = SWDict.BlankDictionaryTable

        DictionaryDataGridView.AutoGenerateColumns = False
        DictionaryDataGridView.DataSource = _myDictionary.DictionaryBindingSource1
        CBGloss1.DataSource = _myDictionary.DictionaryBindingSource1
        CBGloss1.DisplayMember = "gloss1"
        CBGloss1.ValueMember = "IDDictionary"
        CBGloss2.DataSource = _myDictionary.DictionaryBindingSource2
        CBGloss2.DisplayMember = "gloss2"
        CBGloss2.ValueMember = "IDDictionary"
        DictionaryBindingNavigator.BindingSource = _myDictionary.DictionaryBindingSource1

        UpdateOptions()
        SetGlossTittles()
    End Sub
    Private Sub PageChanged(ByVal sender As Object, ByVal e As EventArgs)
        LoadPage()
    End Sub

    Private Function GetTagsData() As Object
        Return CreateGroupedComboBoxItems(_myDictionary.GetTags())
    End Function

    Private Shared Function CreateGroupedComboBoxItems(ByVal expandoObjecttags As List(Of ExpandoObject)) As List(Of GroupedColoredComboBoxItem)

        Dim dict = expandoObjecttags.Cast(Of IDictionary(Of String, Object))().ToList()

        Dim groups = dict.Where(Function(d) IsDbNull(d.Item("Parent")))
        Dim items = dict.Where(Function(d) Not IsDbNull(d.Item("Parent"))).ToList()

        Return (From group In groups Let groupText = group.Item("Description") Let groupId = group.Item("IdTag") From itemTag _
                    In items.Where(Function(x) x.Item("Parent") = groupId)
                    Select New GroupedColoredComboBoxItem With
                           {.Group = groupText.ToString(), .Value = itemTag.Item("IdTag").ToString(),
                            .Display = itemTag.Item("Description"), .Color = Color.FromArgb(itemTag.Item("Color"))}).ToList()
    End Function

    Private Sub CheckDictionary(ByVal connectionString As String, Optional ByVal ask As Boolean = True)
        Dim wasUpgraded = False

        Dim result = DatabaseSetup.CheckDictionary(connectionString, ask, wasUpgraded)

        If Not result.Item1 Then
            MessageBox.Show("Choose or create a SignWriter Dictionary (.SWS) file before continuing.")
        Else
            DictionaryLoaded = True
            ShowloadedFile()
            Dim tagData = GetTagsData()
            Tags.DataSource = tagData
            TagFilter1.TagListControl1.SelectionItemList(tagData)

        End If

        If wasUpgraded Then
            ' Create sort strings if first one is empty
            _myDictionary.CreateSortString()
        End If
    End Sub



    Private Sub ShowButtons()
        If CallingForm IsNot Nothing AndAlso Not Me.CallingForm.Name = "SignWriterMenu" Then
            BtnAccept.Visible = True
            BtnCancel.Visible = True
        Else
            BtnAccept.Visible = False
            BtnCancel.Visible = False
        End If
    End Sub

    Private Sub SetGlossTittles()
        DictionaryDataGridView.Columns("gloss1").HeaderText = "Gloss " &
                                                              UI.Cultures.GetCultureFullName(
                                                                  _myDictionary.FirstGlossLanguage)
        DictionaryDataGridView.Columns("glosses1").HeaderText = "Other Glosses " &
                                                                UI.Cultures.GetCultureFullName(
                                                                    _myDictionary.FirstGlossLanguage)
        DictionaryDataGridView.Columns("gloss2").HeaderText = "Gloss " &
                                                              UI.Cultures.GetCultureFullName(
                                                                  _myDictionary.SecondGlossLanguage)
        DictionaryDataGridView.Columns("glosses2").HeaderText = "Other Glosses " &
                                                                UI.Cultures.GetCultureFullName(
                                                                    _myDictionary.SecondGlossLanguage)
    End Sub
    Private Sub LoadPage()
        If Not LoadingEntries Then
            LoadingEntries = True
            PreLoadEntries()
            LoadEntries()
            LoadingEntries = False
        End If
    End Sub

    Public Property LoadingEntries() As Boolean


    Friend Sub LoadDictionaryEntries()
        If DictionaryLoaded Then
            If Not LoadingEntries Then
                LoadingEntries = True
                PreLoadEntries()
                Dim search As String

                If Not TBSearch.Text.Contains("%") AndAlso Not CBExactSearch.Checked Then
                    search = "%" & TBSearch.Text & "%"
                Else
                    search = TBSearch.Text
                End If

                Pager1.Search = search
                Pager1.CurrentPage = 1

                LoadEntries(True)

                LoadingEntries = False
            End If
        Else
            MessageBox.Show("Choose a valid SignWriter file before continuing.")
        End If
    End Sub

    Private Sub LoadEntries(Optional ByVal count As Boolean = False)

        Dim skip = (Pager1.CurrentPage - 1) * Pager1.PageSize
        Dim totalRowCount = _myDictionary.PagingSearchText(Pager1.Search, Pager1.PageSize, skip, count, currentTagFilterValues)
        If count Then
            Pager1.TotalRowCount = totalRowCount
        End If
        CBGloss1.DataSource = _myDictionary.DictionaryBindingSource1
        CBGloss1.DisplayMember = "gloss1"
        CBGloss1.ValueMember = "IDDictionary"
        CBGloss2.DataSource = _myDictionary.DictionaryBindingSource2
        CBGloss2.DisplayMember = "gloss2"
        CBGloss2.ValueMember = "IDDictionary"

        DictionaryDataGridView.ResumeLayout()
    End Sub

    Private Sub PreLoadEntries()

        DictionaryDataGridView.SuspendLayout()
        'Check if entries have been saved before reloading and losing changes.
        Dim dt As DataTable = Me._myDictionary.DictionaryBindingSource1.DataSource
        'Create dataset if not exists
        If dt.DataSet Is Nothing Then
            Dim ds As New DataSet
            ds.Tables.Add(_myDictionary.DictionaryBindingSource1.DataSource)
        End If

        SaveDataGrid()
    End Sub
 
    Private Sub EditImage()
        Dim imageEditor As New ImageEditor.ImageEditor
        Dim image1 As Image =
                Me.DictionaryDataGridView.CurrentCell.GetEditedFormattedValue(
                    Me.DictionaryDataGridView.CurrentCell.RowIndex, DataGridViewDataErrorContexts.Display)
        'Resize to same to not have indexed pixels.
        imageEditor.Image = SWDrawing.ResizeImage(image1, image1.Width, image1.Height)

        Dim dialogRes As DialogResult = imageEditor.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            DictionaryDataGridView.CurrentCell.Value = imageEditor.Image
            imageEditor.Close()
        ElseIf (dialogRes = DialogResult.Cancel) Then
            imageEditor.Close()
        End If
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As Object, ByVal e As CancelEventArgs) Handles OpenImage.FileOk
        ImportImage(OpenImage.FileName, DictionaryDataGridView.CurrentCell)
    End Sub

    Private Sub ImportImage(ByVal filename As String, ByVal currentCell As DataGridViewCell)
        Try
            'check for boundaries before performing delete: datatable is empty, or there is no selection
            If currentCell IsNot Nothing Then
                'convert generic Current object returned by DataConnector to the typed movie row object
 
                If ColumnClicked = DictionaryColumn.Photo Then
                    'open file as Readonly from file system, copy bytes, and assign to the image property of the current row
                    currentCell.Value = My.Computer.FileSystem.ReadAllBytes(filename)
                    'Edit
                    EditImage()
                End If
                If ColumnClicked = DictionaryColumn.Sign Then
                    'open file as Readonly from file system, copy bytes, and assign to the image property of the current row
                    currentCell.Value = My.Computer.FileSystem.ReadAllBytes(filename)
                    'Edit
                    EditImage()
                End If

            End If
        Catch ex As Exception
            LogError(ex, "Saving Dictionary Entries, UpdateDictionaryEntries " & ex.GetType().Name)

        End Try
    End Sub

    Private Sub Me_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        Dim controlActive As Object = ActiveControl

        Select Case e.KeyCode
            Case Keys.Enter
                If e.Control Then
                    Accept()
                ElseIf controlActive.Name = TBSearch.Name Then
                    LoadDictionaryEntries()

                End If
                'Case Keys.C
                '    If e.Control Then
                '        CopySign()
                '        e.Handled = True

                '    End If
                'Case Keys.V
                '    If e.Control Then
                '        PasteSign()
                '        e.Handled = True

                '    End If
            Case Keys.G
                If e.Control Then
                    DictionaryDataGridView.Focus()
                    e.Handled = True
                    e.SuppressKeyPress = True
                End If
            Case Keys.S
                If e.Control Then
                    TBSearch.Focus()
                    e.Handled = True
                    e.SuppressKeyPress = True
                End If


            Case Keys.Escape
                Cancel()
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "dictionary.htm")
        End Select
    End Sub

    Private Sub WordsViewDataGridView_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
        Handles DictionaryDataGridView.CellDoubleClick
        OpenEditor()
    End Sub

    Private Sub WordsViewDataGridView_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs)
        'Consume DataError  
    End Sub

    Sub SaveDataGrid()

        Try
            Dim saved = False
            Dim tagChanges As Tuple(Of List(Of List(Of String)), List(Of List(Of String))) = Nothing
            Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)

            Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
            Using conn
                Try
                    tagChanges = SignWriterStudio.Dictionary.TagChanges.GetTagChanges(_myDictionary)
                    SaveDataGrid(conn, trans)
                    trans.Commit()
                    saved = True
                Catch ex As SQLiteException
                    LogError(ex, "SQLite Error " & ex.GetType().Name)

                    MessageBox.Show(ex.ToString)
                    If trans IsNot Nothing Then trans.Rollback()
                Finally
                    conn.Close()

                End Try
            End Using
            If saved Then
                SaveTagDictionary(tagChanges)
            End If
        Catch ex As ArgumentException
            LogError(ex, "No current file can not save DataGrid " & ex.GetType().Name)


        End Try
    End Sub

    Private Sub SaveTagDictionary(ByVal tagChanges As Tuple(Of List(Of List(Of String)), List(Of List(Of String))))
        _myDictionary.SaveTagDictionary(tagChanges)
    End Sub

    Sub SaveDataGrid(ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)

        'Try
        UpdateDataset()
        Dim ds As New DataSet
        Dim previousDs As DataSet
        Dim changedDs As DataSet
        Dim dt As DataTable = _myDictionary.DictionaryBindingSource1.DataSource
        If dt IsNot Nothing Then
            If dt.DataSet IsNot Nothing Then
                previousDs = dt.DataSet
                previousDs.Tables.Remove(dt)
            End If

            ds.Tables.Add(dt)

            changedDs = ds.GetChanges

            If changedDs IsNot Nothing AndAlso changedDs.Tables.Count >= 1 Then
                Dim uniligualChanges = _myDictionary.ConvertUnilingualDTtoBilingualDt(changedDs.Tables(0))


                _myDictionary.UpdateDictionaryEntries(uniligualChanges,
                    _myDictionary.FirstGlossLanguage, _myDictionary.SecondGlossLanguage, conn, trans)

            End If

        End If
        dt.AcceptChanges()
    End Sub




    Private Function AskAddToPuddle() As Boolean
        Return MessageBox.Show("Would you like to add the new entries to SignPuddle?", "Add entry to SignPuddle", MessageBoxButtons.YesNo) = DialogResult.Yes
    End Function

    Sub UpdateDataset()

        Validate()
        _myDictionary.DictionaryBindingSource1.EndEdit()
    End Sub

    Public Sub FindSign(ByVal IDDictionary As Integer)
        _myDictionary.GetbyIdDictionary(IDDictionary)
    End Sub


    Private Sub LoadTranslations()
        'TODO Translations
    End Sub


    Private Sub DictionaryBindingNavigatorSaveItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.SaveDataGrid()
    End Sub

    Private Sub DictionaryDataGridView_CellEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) _
        Handles DictionaryDataGridView.CellEnter
        If sender.CurrentCell.OwningColumn.DataPropertyName = "Photo" Then
            Me.ColumnClicked = DictionaryColumn.Photo
        ElseIf sender.CurrentCell.OwningColumn.DataPropertyName = "Sign" Then
            Me.ColumnClicked = DictionaryColumn.Sign
        ElseIf sender.CurrentCell IsNot Nothing AndAlso sender.CurrentCell.OwningColumn.DataPropertyName = "SWriting" _
            Then
            Me.ColumnClicked = DictionaryColumn.SWriting
        Else
            Me.ColumnClicked = Nothing
        End If
    End Sub

    Private Sub DictionaryDataGridView_CellMouseDown(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs) _
        Handles DictionaryDataGridView.CellMouseDown
        If sender.currentcell IsNot Nothing Then
            If sender.CurrentCell.OwningColumn.DataPropertyName = "Photo" Then
                Me.ColumnClicked = DictionaryColumn.Photo
            ElseIf sender.CurrentCell.OwningColumn.DataPropertyName = "Sign" Then
                Me.ColumnClicked = DictionaryColumn.Sign
            ElseIf _
                sender.CurrentCell IsNot Nothing AndAlso sender.CurrentCell.OwningColumn.DataPropertyName = "SWriting" _
                Then
                Me.ColumnClicked = DictionaryColumn.SWriting
            Else
                Me.ColumnClicked = Nothing
            End If
        Else
            Me.ColumnClicked = Nothing
        End If
    End Sub

    Private Sub SetClipboardImage()
        Dim Value As Image
        If _
            DictionaryDataGridView.CurrentCell IsNot Nothing AndAlso
            DictionaryDataGridView.CurrentCell.Value IsNot Nothing AndAlso
            DictionaryDataGridView.CurrentCell.Value.GetType.FullName = "System.Byte[]" Then
            Value = ByteArraytoImage(DictionaryDataGridView.CurrentCell.Value)

            Select Case ColumnClicked
                Case DictionaryColumn.Photo
                    SWDrawing.SetClipboardImage(Value)
                Case DictionaryColumn.Sign
                    SWDrawing.SetClipboardImage(Value)
                Case DictionaryColumn.SWriting
                    SWDrawing.SetClipboardImage(Value)
            End Select
        End If
    End Sub

    Private Sub DictionaryDataGridView_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) _
        Handles DictionaryDataGridView.KeyDown
        Select Case e.KeyCode
            Case Keys.Delete
                If DictionaryDataGridView.SelectedCells.Count > 1 Then
                    DeleteEntries()
                Else
                    DeleteCellInfo(DictionaryDataGridView.CurrentCellAddress)
                End If
            Case Keys.Enter
                OpenEditor()
        End Select
    End Sub

    Private Sub DictionaryDataGridView_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) _
        Handles DictionaryDataGridView.MouseDown
        Dim HitTestInfo As DataGridView.HitTestInfo = DictionaryDataGridView.HitTest(e.X, e.Y)
        If HitTestInfo.Type = DataGridViewHitTestType.Cell Then
            Me.ClickedCell = DictionaryDataGridView.Rows(HitTestInfo.RowIndex).Cells(HitTestInfo.ColumnIndex)
        Else
            Me.ClickedCell = Nothing
        End If
    End Sub

    Private Sub DictionaryDataGridView_RowValidating(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs) _
        Handles DictionaryDataGridView.RowValidating
        If Information.IsDBNull(Me.DictionaryDataGridView.Rows(e.RowIndex).Cells("SignLanguage").Value) Then
            Me.DictionaryDataGridView.Rows(e.RowIndex).Cells("SignLanguage").Value =
                Me._myDictionary.DefaultSignLanguage
        End If
    End Sub

    Private Sub UpdateOptions()

        Me._myDictionary.DefaultSignLanguage = SettingsPublic.DefaultSignLanguage
        Me._myDictionary.FirstGlossLanguage = SettingsPublic.FirstGlossLanguage
        Me._myDictionary.SecondGlossLanguage = SettingsPublic.SecondGlossLanguage
        Me._myDictionary.BilingualMode = SettingsPublic.BilingualMode
        If Me._myDictionary.BilingualMode Then

            Me.LBGlossLang2.Visible = True
            Me.CBGloss2.Visible = True
            Me.DictionaryDataGridView.Columns("Gloss2").Visible = True
            Me.DictionaryDataGridView.Columns("Glosses2").Visible = True

        Else

            Me.LBGlossLang2.Visible = False
            Me.CBGloss2.Visible = False
            Me.DictionaryDataGridView.Columns("Gloss2").Visible = False
            Me.DictionaryDataGridView.Columns("Glosses2").Visible = False

        End If

        Me.LBGlossLang1.Text = UI.Cultures.GetCultureFullName(Me._myDictionary.FirstGlossLanguage)
        Me.LBGlossLang2.Text = UI.Cultures.GetCultureFullName(Me._myDictionary.SecondGlossLanguage)

        SetGlossTittles()
    End Sub

    Private Sub TBSearch_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TBSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            LoadDictionaryEntries()
            LetKnowNoMatchesFound()
            e.SuppressKeyPress = True
            e.Handled = True
        End If
    End Sub

    Private Sub CBGloss1_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles CBGloss1.SelectedValueChanged
        If Me._myDictionary.BilingualMode AndAlso CBGloss1.SelectedValue IsNot Nothing Then
            CBGloss2.SelectedValue = CBGloss1.SelectedValue
        End If
    End Sub

    Private Sub CBGloss2_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles CBGloss2.SelectedIndexChanged
        If Me._myDictionary.BilingualMode AndAlso CBGloss2.SelectedValue IsNot Nothing Then
            CBGloss1.SelectedValue = CBGloss2.SelectedValue
        End If
    End Sub

    Private Sub Cancel()
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Accept()
        SaveDataGrid()
        If _
            DictionaryDataGridView.CurrentRow IsNot Nothing AndAlso
            DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value IsNot Nothing Then
            IDDictionaryResult = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value
        End If
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Options()
        Dim SWOptions As New SWOptions
        Dim DialogRes As DialogResult = SWOptions.ShowDialog()
        If (DialogRes = DialogResult.OK) Then
            'Use TSWOptions
            UpdateOptions()
            SWOptions.Close()
        ElseIf (DialogRes = DialogResult.Cancel) Then
            SWOptions.Close()
        End If
    End Sub

    Private Sub TSBDuplicate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSBDuplicate.Click
        SaveDataGrid()
        Duplicate()
    End Sub

    Private Sub Duplicate()
        Dim btnDuplicateCopy As String = " Copy"
        If DictionaryDataGridView.CurrentRow IsNot Nothing Then
            Me._myDictionary.DuplicateSign(_myDictionary.DictionaryBindingSource1.Current)

            LoadDictionaryEntries()
        End If
    End Sub

    Private Sub TSBSymbolSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSBSymbolSearch.Click
        SearchSymbol()
    End Sub

    Public Sub SearchSymbol()
        Dim searchString As String
        Dim dt As DictionaryDataSet.SignsbyGlossesBilingualDataTable
        Dim swSignSearch As New SWSignSearch
        Dim dialogRes As DialogResult = swSignSearch.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            If _
                swSignSearch IsNot Nothing AndAlso swSignSearch.SearchCriteria IsNot Nothing AndAlso
                swSignSearch.SearchCriteria.SearchString IsNot Nothing AndAlso
                Not String.IsNullOrEmpty(swSignSearch.SearchCriteria.SearchString) Then
                searchString = swSignSearch.SearchCriteria.SearchString
                dt = _myDictionary.GetSymbolSearchDt(SettingsPublic.LastDictionaryString, searchString)
                _myDictionary.UpdateDataSources(dt)
                If dt.Rows.Count = 0 Then
                    MessageBox.Show("There are no matches for your criteria.")
                End If
            Else
                MessageBox.Show("Could not create search criteria")
            End If
        ElseIf (dialogRes = DialogResult.Cancel) Then

        End If
        swSignSearch.Close()
    End Sub


    'Private Sub CopyCell()

    '    Dim conn As SQLite.SQLiteConnection = SWDict.GetNewDictionaryConnection()
    '    Dim trans As SQLite.SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
    '    Using conn
    '        Try
    '            If Me.DictionaryDataGridView.GetCellCount( _
    '               DataGridViewElementStates.Selected) > 0 Then

    '                Try
    '                    If Me.DictionaryDataGridView.SelectedCells.Contains(Me.ClickedCell) AndAlso Me.DictionaryDataGridView.GetCellCount( _
    '         DataGridViewElementStates.Selected) > 1 Then
    '                        ' Add the selection to the clipboard.
    '                        Clipboard.SetDataObject( _
    '                            Me.DictionaryDataGridView.GetClipboardContent())
    '                    Else
    '                        'Copy Clicked Cell
    '                        If ClickedCell IsNot Nothing AndAlso Me.DictionaryDataGridView.Columns(Me.ClickedCell.ColumnIndex).Name = "SWriting" Then
    '                            Dim Sign As SWSign
    '                            Dim myDataObject As New DataObject()

    '                            Sign = Me.MyDictionary.GetSWSign(Me.DictionaryDataGridView.Rows(Me.ClickedCell.RowIndex).Cells("IDDictionary").Value, conn, trans)

    '                            If Not IsDBNull(Me.ClickedCell.Value) Then
    '                                myDataObject.SetImage(General.ByteArraytoImage(Me.ClickedCell.Value))
    '                            End If
    '                            myDataObject.SetData("SWriting", False, Sign)
    '                            Clipboard.SetDataObject(myDataObject)
    '                        ElseIf ClickedCell IsNot Nothing AndAlso Me.DictionaryDataGridView.Columns(Me.ClickedCell.ColumnIndex).Name = "Photo" Then
    '                            If Not IsDBNull(Me.ClickedCell.Value) Then
    '                                SetImagetoClipboard(General.ByteArraytoImage(Me.ClickedCell.Value))
    '                            Else
    '                                SetImagetoClipboard(Nothing)
    '                            End If
    '                        ElseIf ClickedCell IsNot Nothing AndAlso Me.DictionaryDataGridView.Columns(Me.ClickedCell.ColumnIndex).Name = "Sign" Then
    '                            If Not IsDBNull(Me.ClickedCell.Value) Then
    '                                SetImagetoClipboard(General.ByteArraytoImage(Me.ClickedCell.Value))
    '                            Else
    '                                SetImagetoClipboard(Nothing)
    '                            End If
    '                        Else
    '                            If ClickedCell IsNot Nothing Then
    '                                Me.ClickedCell.Selected = True
    '                                Clipboard.SetDataObject( _
    '                                                  Me.DictionaryDataGridView.GetClipboardContent())
    '                            End If
    '                        End If


    '                    End If


    '                Catch ex As Exception
    '                    'monitor.TrackException(ex, _
    '                    '                  TraceEventType.Error, _
    '                    '                  "Exception ")
    '                    My.Application.Log.WriteException(ex, _
    '                                      TraceEventType.Error, _
    '                                      "Exception ")

    '                End Try

    '            End If
    '            trans.Commit()
    '        Catch ex As SQLite.SQLiteException
    '            MessageBox.Show(ex.ToString)
    '            If trans IsNot Nothing Then trans.Rollback()
    '        Finally
    '            conn.Close()

    '        End Try
    '    End Using
    'End Sub

    'Private Sub PasteCellClicked()
    '     If Me.ClickedCell IsNot Nothing Then
    '         If Me.DictionaryDataGridView.Columns(Me.ClickedCell.ColumnIndex).Name = "SWriting" Then
    '             Dim ClipboardSign = GetSWSignFromClipboard()
    '             Dim Sign As SWSign = ClipboardSign.SWSign
    '             Dim RowClicked As DataGridViewRow = Me.DictionaryDataGridView.Rows(Me.ClickedCell.RowIndex)
    '             If Sign IsNot Nothing Then
    '                 Sign = Sign.Clone
    '                 Sign.SignWriterGuid = New Guid

    '                 If Me.ClickedCell.RowIndex < Me.DictionaryDataGridView.Rows.Count AndAlso Me.ClickedCell.RowIndex >= 0 Then

    '                     Dim IDDic As Integer = Me.DictionaryDataGridView.Rows.Item(Me.ClickedCell.RowIndex).Cells("IDDictionary").Value


    '                     Me.DictionaryDataGridView.Rows.Item(Me.ClickedCell.RowIndex).Cells("SWriting").Value = General.ImageToByteArray(Sign.Render(), Imaging.ImageFormat.Png)
    '                     Me.MyDictionary.SaveSWSign(IDDic, Sign)
    '                 End If
    '             Else
    '                 ClickedCell.Value = Nothing
    '             End If
    '         ElseIf Me.DictionaryDataGridView.Columns(Me.ClickedCell.ColumnIndex).Name = "Photo" Then
    '             ClickedCell.Value = GetImageFromClipboard()
    '         ElseIf Me.DictionaryDataGridView.Columns(Me.ClickedCell.ColumnIndex).Name = "Sign" Then
    '             ClickedCell.Value = GetImageFromClipboard()
    '         Else
    '             ClickedCell.Value = GetTextFromClipboard()
    '         End If
    '     End If
    ' End Sub

    Private Sub PasteFsw()
        Dim fsw As String = GetTextFromClipboard()

        If IsBuildString(fsw) Then
            Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
            Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
            Using conn
                Try
                    If DictionaryDataGridView.CurrentRow IsNot Nothing Then


                        Dim Sign = SpmlConverter.FswtoSwSign(fsw, _myDictionary.DefaultSignLanguage,
                                                             _myDictionary.FirstGlossLanguage)

                        If Sign IsNot Nothing Then
                            If _
                                MessageBox.Show("This will ovewrite the current row. Do you wish to continue?", "",
                                                MessageBoxButtons.YesNo) = DialogResult.Yes Then

                                Dim idDic As Integer = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value

                                DictionaryDataGridView.CurrentRow.Cells("SWriting").Value =
                                    ImageToByteArray(Sign.Render(), ImageFormat.Png)

                                _myDictionary.SaveSWSign(idDic, Sign, conn, trans)
                                SaveDataGrid(conn, trans)
                            End If
                        End If

                    End If

                    trans.Commit()
                Catch ex As SQLiteException
                    LogError(ex, "Exception " & ex.GetType().Name)

                    MessageBox.Show(ex.ToString)
                    If trans IsNot Nothing Then trans.Rollback()
                Finally
                    conn.Close()
                End Try
            End Using

        Else
            MessageBox.Show("Text in clipboard is not FSW")
        End If
    End Sub

    Private Sub PasteSign()
        Dim result = GetSWSignFromClipboard()
        If Me.DictionaryDataGridView.CurrentRow IsNot Nothing AndAlso result IsNot Nothing Then
            If _
                MessageBox.Show("This will ovewrite the current row. Do you wish to continue?", "",
                                MessageBoxButtons.YesNo) = DialogResult.Yes Then


                Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
                Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
                Using conn
                    Try


                        Dim Sign As SwSign = result.SWSign
                        Dim RowClicked As DataGridViewRow = Me.DictionaryDataGridView.CurrentRow
                        If Sign IsNot Nothing Then
                            Sign = Sign.Clone
                            Sign.SignWriterGuid = New Guid

                            Dim IDDic As Integer = Me.DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value

                            Me.DictionaryDataGridView.CurrentRow.Cells("SWriting").Value =
                                ImageToByteArray(Sign.Render(), ImageFormat.Png)

                            Me.DictionaryDataGridView.CurrentRow.Cells("Gloss1").Value = result.Gloss
                            Me.DictionaryDataGridView.CurrentRow.Cells("Glosses1").Value = result.Glosses
                            Me.DictionaryDataGridView.CurrentRow.Cells("Photo").Value = result.Illustration
                            Me.DictionaryDataGridView.CurrentRow.Cells("Sign").Value = result.Sign
                            Me.DictionaryDataGridView.CurrentRow.Cells("SWSignSource").Value = result.SWSignSource
                            Me.DictionaryDataGridView.CurrentRow.Cells("PhotoSource").Value = result.IllustrationSource
                            Me.DictionaryDataGridView.CurrentRow.Cells("SignSource").Value = result.SignSource
                            Me._myDictionary.SaveSWSign(IDDic, Sign, conn, trans)
                            SaveDataGrid(conn, trans)

                        End If
                        trans.Commit()
                    Catch ex As SQLiteException
                        LogError(ex, "SQLite Exception " & ex.GetType().Name)

                        MessageBox.Show(ex.ToString)
                        If trans IsNot Nothing Then trans.Rollback()
                    Finally
                        conn.Close()

                    End Try
                End Using
            End If
        End If
    End Sub
    'Private Function GetNewIDDictionary(ByVal NewRow As DataGridViewRow) As Integer
    '    'TODO()
    '    Dim InitialString As String
    '    Dim Lookup As String
    '    If Not IsDBNull(NewRow.Cells("gloss1").Value) Then
    '        InitialString = NewRow.Cells("gloss1").Value
    '    Else
    '        InitialString = String.Empty
    '    End If

    '    Lookup = Mid(InitialString & (System.Guid.NewGuid).ToString, 1, 50)
    '    NewRow.Cells("gloss1").Value = Lookup
    '    Exit Function
    '    SaveDataGrid()
    '    Me.TBSearch.Text = Lookup
    '    LoadDictionaryEntries()
    '    If Me.DictionaryDataGridView.Rows.Count > 0 Then
    '        Dim LoadedRow As DataGridViewRow = Me.DictionaryDataGridView.Rows(0)
    '        LoadedRow.Cells("gloss1").Value = InitialString
    '        Return LoadedRow.Cells("IDDictionary").Value
    '    Else
    '        Return 0
    '    End If
    'End Function

    Private Function GetSWSignFromClipboard() As ClipboardSign
        If Clipboard.GetDataObject.GetDataPresent("SignWriterStudio.Dictionary.ClipboardSign", False) Then
            Return CType(Clipboard.GetDataObject.GetData("SignWriterStudio.Dictionary.ClipboardSign"), ClipboardSign)
        Else
            Return Nothing
        End If
    End Function

    Private Sub SetSWSigntoClipboard(ByVal ClipboardSign As ClipboardSign, SWriting As Bitmap)
        Dim myDataObject As New DataObject()

        Dim myType As Type = ClipboardSign.GetType()
        'myDataObject.SetImage(SWriting)
        myDataObject.SetData(ClipboardSign.GetType().Name, False, ClipboardSign)


        Clipboard.Clear()

        'Clipboard.SetImage(SWriting)
        Clipboard.SetDataObject(myDataObject)
    End Sub

    Private Function GetImageFromClipboard() As Image
        If My.Computer.Clipboard.ContainsImage() Then
            Return Clipboard.GetImage()
        Else
            Return Nothing
        End If
    End Function

    Private Function GetTextFromClipboard() As String
        If My.Computer.Clipboard.ContainsText() Then
            Return Clipboard.GetText(TextDataFormat.Text)
        Else
            Return ""
        End If
    End Function

    Private Sub SetImagetoClipboard(ByVal Image As Image)
        If Image IsNot Nothing Then
            Clipboard.SetImage(Image)
        End If
    End Sub


    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        DeleteCellInfo(DictionaryDataGridView.CurrentCellAddress)
    End Sub

    Private Sub DeleteCellInfo(ByVal CellAddress As Point)

        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
        Using conn
            Try
                Dim CurrentColumn As DataGridViewColumn = DictionaryDataGridView.Columns(CellAddress.X)
                Select Case CurrentColumn.Name
                    Case "SWriting"
                        If _
                            MessageBox.Show("Do you want to delete the info in this cell?", "Delete",
                                            MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            Me._myDictionary.DeleteSign(
                                DictionaryDataGridView.Rows(CellAddress.Y).Cells("IDDictionary").Value, conn, trans)
                            DictionaryDataGridView.Rows(CellAddress.Y).Cells(CellAddress.X).Value = Nothing
                        End If
                    Case "Photo", "Sign"
                        If _
                            MessageBox.Show("Do you want to delete the info in this cell?", "Delete",
                                            MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            DictionaryDataGridView.Rows(CellAddress.Y).Cells(CellAddress.X).Value = Nothing
                        End If
                End Select
                trans.Commit()
            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                conn.Close()

            End Try
        End Using
    End Sub

    Private Sub OpenEditor()
        If Not _myDictionary.DictionaryBindingSource1.Current.GetType.ToString = "System.Object" Then

            If DictionaryDataGridView.CurrentCell.OwningColumn.DataPropertyName = "Photo" Then
                ColumnClicked = DictionaryColumn.Photo

                If Information.IsDBNull(DictionaryDataGridView.CurrentCell.Value) Then
                    OpenImage.ShowDialog()
                Else
                    EditImage()
                End If
            ElseIf DictionaryDataGridView.CurrentCell.OwningColumn.DataPropertyName = "Sign" Then
                ColumnClicked = DictionaryColumn.Sign
                If Information.IsDBNull(DictionaryDataGridView.CurrentCell.Value) Then
                    OpenImage.ShowDialog()
                Else
                    EditImage()
                End If

            ElseIf _
                DictionaryDataGridView.CurrentCell IsNot Nothing AndAlso
                DictionaryDataGridView.CurrentCell.OwningColumn.DataPropertyName = "SWriting" Then
                ColumnClicked = DictionaryColumn.SWriting
                Dim idDictionary1 As Integer = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value
                If idDictionary1 <> 0 Then

                    OpenSignEditorThenSave(idDictionary1)
                Else
                    Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
                    Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
                    Using conn
                        Try
                            ColumnClicked = Nothing
                            SaveDataGrid(conn, trans)

                            trans.Commit()

                        Catch ex As SQLiteException
                            LogError(ex, "SQLite Exception " & ex.GetType().Name)

                            MessageBox.Show(ex.ToString)
                            If trans IsNot Nothing Then trans.Rollback()
                        Finally
                            conn.Close()
                        End Try
                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub OpenSignEditorThenSave(idDictionary As Integer)
        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)

        Editor.ClearAll()
        Editor.Sign = _myDictionary.GetSWSign(idDictionary, conn, trans)
        'Keep SignEditor if error saving sign
        Dim continuewithdictionary = False
        Dim dialogRes As DialogResult

        Do
            dialogRes = Editor.ShowDialog()
            If (dialogRes = DialogResult.OK) Then
                Try
                    SaveSignWriting(conn, trans, idDictionary)

                    continuewithdictionary = True
                Catch ex As Exception
                    MessageBox.Show("An error occured, could not save sign. " & ex.Message)
                    trans.Rollback()
                    trans.Dispose()

                    trans = SWDict.GetNewDictionaryTransaction(conn)
                End Try
            Else
                continuewithdictionary = True
            End If
        Loop While continuewithdictionary = False
        trans.Commit()
    End Sub

    Private Sub ToolStripLabel1_Click(ByVal sender As Object, ByVal e As EventArgs)
    End Sub

    Private Sub DictionaryDataGridView_DataError(ByVal sender As Object, ByVal e As DataGridViewDataErrorEventArgs) _
        Handles DictionaryDataGridView.DataError
        'Do nothing
    End Sub


    Private Sub BtnAccept_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnAccept.Click
        Accept()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancel.Click
        Cancel()
    End Sub

    Private Sub BindingNavigatorAddNewItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles BindingNavigatorAddNewItem.Click
        AddNew()
    End Sub

    Private Sub AddNew()
        If DictionaryLoaded Then
            Dim newRow = DirectCast(DirectCast(_myDictionary.DictionaryBindingSource1.AddNew(), DataRowView).Row, DictionaryDataSet.SignsbyGlossesBilingualRow)

            Dim newId As Integer = GetNewId(newId)


            Dim currentRow As DataGridViewRow = Me.DictionaryDataGridView.CurrentRow
            currentRow.Cells("IDDictionary").Value = newId
            currentRow.Cells("SignLanguage").Value = _myDictionary.DefaultSignLanguage
            newRow.isPrivate = False
            newRow.Sorting = ""

            SaveDataGrid()
            _myDictionary.DictionaryBindingSource1.ResetBindings(False)


        Else
            MessageBox.Show("Choose or create a SignWriter Dictionary (.SWS) file before continuing.")
        End If
    End Sub

    Private Function GetNewId(ByVal newId As Integer) As Integer

        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)

        Try

            'Edit last added row so that it will save.
            Dim taDictionary As New DictionaryTableAdapter

            taDictionary.AssignConnection(conn, trans)
            newId = taDictionary.LastID + 1

            trans.Commit()
            conn.Close()
        Catch ex As SQLiteException
            LogError(ex, "SQLite Exception " & ex.GetType().Name)

            MessageBox.Show(ex.ToString)
            If trans IsNot Nothing Then trans.Rollback()
        End Try
        Return newId
    End Function

    Private Sub DictionaryDataGridView_RowsAdded(ByVal sender As Object, ByVal e As DataGridViewRowsAddedEventArgs) _
        Handles DictionaryDataGridView.RowsAdded
        If Information.IsDBNull(DictionaryDataGridView.Rows(e.RowIndex).Cells("GUID").Value) Then
            DictionaryDataGridView.Rows(e.RowIndex).Cells("GUID").Value = System.Guid.NewGuid
        End If
    End Sub

    Public Sub New(ByVal editor1 As Editor)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Editor = editor1
    End Sub

    Private _importedSigns As Tuple(Of Integer, Integer, Integer)
    Private currentTagFilterValues As TagFilterValues
    Private _puddleLoggedIn As Boolean
    Private _puddleSgn As String
    Private _puddleName As String
    Private _puddleApi As SignPuddleApi.SignPuddleApi

    Private Sub ImportFileDialog_FileOk(ByVal sender As Object, ByVal e As CancelEventArgs) _
        Handles ImportFileDialog.FileOk


        Dim _
            classifiedSigns _
                As  _
                Tuple _
                (Of List(Of SwSign), 
                List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)), 
                List(Of SwSign))
        Try
            Dim signs As List(Of SwSign)
            Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
            Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
            Dim selectedSigns As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))

            Dim spmlConverter As New SpmlConverter
            signs = spmlConverter.ImportSPML(ImportFileDialog.FileName, _myDictionary.DefaultSignLanguage,
                                             _myDictionary.FirstGlossLanguage)


            spmlConverter.CleanImportedSigns(signs)

            'signs = GetOnlyWithSequence(signs)
            'signs = GetOnlyWithTagSort(signs)


            Try
                Using conn
                    classifiedSigns = ClassifySigns(signs, conn, trans)
                    If trans.Connection IsNot Nothing Then

                        trans.Commit()
                    End If

                    conn.Close()
                End Using
            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            End Try

            Try
                Using conn
                    selectedSigns = SelectComparedSigns(classifiedSigns.Item2, conn, trans)
                    If trans.Connection IsNot Nothing Then

                        trans.Commit()
                    End If

                    'conn.Close()
                End Using

            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            End Try

            Try
                conn = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
                trans = SWDict.GetNewDictionaryTransaction(conn)
                Using conn
                    'Update current signs
                    _myDictionary.DeleteSigns(selectedSigns, conn, trans)
                    _myDictionary.SignstoDictionaryInsert(SelectedSignsToCollection(selectedSigns), conn, trans)
                    trans.Commit()
                    conn.Close()
                End Using
            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            End Try

            Try

                conn = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
                trans = SWDict.GetNewDictionaryTransaction(conn)
                Using conn
                    SWEditorProgressBar.ProgressBar1.Value = 0
                    SWEditorProgressBar.Text = "SignWriter Studio™ Importing ..."
                    SWEditorProgressBar.Show()
                    _importedSigns = Tuple.Create(classifiedSigns.Item1.Count, selectedSigns.Count,
                                                  classifiedSigns.Item3.Count)
                    trans.Commit()
                    conn.Close()
                End Using
            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            End Try

            Try
                conn = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
                trans = SWDict.GetNewDictionaryTransaction(conn)
                Using conn
                    'Add new signs
                    SPMLImportbw = New BackgroundWorker With {.WorkerReportsProgress = True}
                    AddHandler SPMLImportbw.DoWork, AddressOf SPMLImportbw_DoWork
                    AddHandler SPMLImportbw.RunWorkerCompleted, AddressOf SPMLImportbw_RunWorkerCompleted
                    AddHandler SPMLImportbw.ProgressChanged, AddressOf SPMLImportbw_ProgressChanged
                    SPMLImportbw.RunWorkerAsync(Tuple.Create(classifiedSigns.Item3, SPMLImportbw))
                    trans.Commit()
                    conn.Close()
                End Using

            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            End Try


        Catch ex As XmlException
            LogError(ex, "XML Exception " & ex.GetType().Name)

            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Shared Function GetOnlyWithSequence(ByVal List As List(Of SwSign)) _
        As List(Of SwSign)
        Dim newSignList = New List(Of SwSign)
        For Each sign As SwSign In List
            If sign.Frames.First().Sequences.Count > 0 AndAlso sign.SWritingSource.ToLower().Contains("Val".ToLower()) _
                Then
                newSignList.Add(sign)
            End If
        Next
        Return newSignList
    End Function

    Private Shared Function GetOnlyWithTagSort(ByVal List As List(Of SwSign)) As List(Of SwSign)
        Dim newSignList = New List(Of SwSign)
        For Each sign As SwSign In List
            If _
                sign.Frames.First().Sequences.Count > 0 AndAlso sign.SWritingSource.ToLower().Contains("Tag".ToLower()) AndAlso
                sign.SWritingSource.ToLower().Contains("Sort".ToLower()) Then
                newSignList.Add(sign)
            End If
        Next
        Return newSignList
    End Function

    Private Sub DisposeSigns(ByVal List As List(Of SwSign))
        For Each sign1 As SwSign In List
            sign1.Dispose()
        Next
    End Sub

    Private Sub SPMLImportbw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) ' Handles SPMLImportbw.DoWork
        Dim Args = CType(e.Argument, Tuple(Of List(Of SwSign), BackgroundWorker))

        _myDictionary.SignstoDictionary(Args.Item1, Args.Item2)
    End Sub

    Private Function SelectedSignsToCollection(
                                               SelectedSigns As  _
                                                  List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))) _
        As List(Of SwSign)
        Dim Coll As New List(Of SwSign)
        For Each SelectedSign In SelectedSigns
            Coll.Add(SelectedSign.Item1)
        Next
        Return Coll
    End Function

    Private Sub SPMLImportbw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
' Handles SPMLImportbw.ProgressChanged
        SWEditorProgressBar.ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub SPMLImportbw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) _
'Handles SPMLImportbw.RunWorkerCompleted

        SWEditorProgressBar.Hide()
        SWEditorProgressBar.ProgressBar1.Value = 0
        Dim sb As New StringBuilder
        If Not _importedSigns.Item1 = 0 Then
            sb.AppendLine(_importedSigns.Item1 & " signs already up to date.")
        End If
        If Not _importedSigns.Item2 = 0 Then
            sb.AppendLine(_importedSigns.Item2 & " signs updated.")
        End If
        If Not _importedSigns.Item3 = 0 Then
            sb.AppendLine(_importedSigns.Item3 & " new signs added.")
        End If
        MessageBox.Show(sb.ToString)
        RemoveHandler SPMLImportbw.DoWork, AddressOf SPMLImportbw_DoWork
        RemoveHandler SPMLImportbw.RunWorkerCompleted, AddressOf SPMLImportbw_RunWorkerCompleted
        RemoveHandler SPMLImportbw.ProgressChanged, AddressOf SPMLImportbw_ProgressChanged
    End Sub

    Private Sub SignListsToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles SignListsToolStripMenuItem.Click
        SaveDataGrid()
        'If DatabaseSetup.CheckDictionary() Then
        '    Dim SignListPrint As New SignWriterStudio.SignList.SignListPrint
        '    SignListPrint.Show()
        'Else
        '    MessageBox.Show("Choose a valid SignWriter file before continuing.")
        'End If
    End Sub

    Private Sub TemporarilyDisabled()
        MessageBox.Show("This option has been temporarily disabled because it is not yet fully functional.")
    End Sub

    Private Sub NewSignWriterStudioFileToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles NewSignWriterStudioFileToolStripMenuItem.Click
        SaveDataGrid()
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenSignWriterStudioFileToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles OpenSignWriterStudioFileToolStripMenuItem.Click
        SaveDataGrid()
        OpenFileDialog1.InitialDirectory = ""

        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles OpenFileDialog1.FileOk
        OpenDictionary(OpenFileDialog1.FileName)
    End Sub

    Public Sub OpenDictionary(filename As String)
        Dim connectionString = CreateConnectionString(filename)
        If CheckSQLiteConnectionString(connectionString) Then
            SetDictionaryFilename(filename)

            Dim wasUpgraded = False
            Dim result = DatabaseSetup.CheckDictionary(connectionString, True, wasUpgraded)

            If result.Item1 Then
                Dim languages As String = DictLanguages.LanguagesInDictionary
                If Not languages = String.Empty Then
                    MessageBox.Show(languages)
                End If
                If wasUpgraded Then
                    ' Create sort strings if first one is empty
                    _myDictionary.CreateSortString()
                End If
                LoadDictionary()

                DictionaryLoaded = True
            Else
                MessageBox.Show(
                    "File '" & filename &
                    "' is not a valid SignWriter Studio file. Please choose a valid SignWriter file before continuing.")
                SetDictionaryFilename("")
                Me.DictionaryLoaded = False
            End If
        End If
    End Sub

    Private Sub SaveFileDialog1_FileOk_1(sender As Object, e As CancelEventArgs) Handles SaveFileDialog1.FileOk
        CopyBlankDB(SaveFileDialog1.FileName)
        SetDictionaryFilename(SaveFileDialog1.FileName)
        LoadDictionary(False)

        Me.DictionaryLoaded = True
    End Sub

    Friend Sub SetDictionaryFilename(ByVal filename As String)
        DictionaryFilename = filename
        ShowloadedFile()
    End Sub

    Private Sub ShowloadedFile()
        Dim filename As String = DictionaryFilename
        If File.Exists(filename) Then
            If Not (filename = String.Empty) Then
                Me.Text = "SignWriter Studio™ - " & Path.GetFileName(filename) & " - " & GetSLNameandAcronym()
            Else
                Me.Text = "SignWriter Studio™" & " - " & GetSLNameandAcronym()
            End If
        Else
            Me.Text = "SignWriter Studio™" & " - " & GetSLNameandAcronym()
        End If
    End Sub

    Private Shared Function GetCurrentDictFilename() As String

        Return DictionaryConnectionString.ToString.Replace("""", "").Replace("data source=", "")
    End Function

    Private Function GetSLNameandAcronym() As String
        Dim Acronym As String = UI.Cultures.GetSLAcronymbyID(_myDictionary.DefaultSignLanguage)
        Dim AcronymShow As String = String.Empty
        If Not Acronym = String.Empty Then
            AcronymShow = " (" & Acronym & ")"
        End If
        Return UI.Cultures.GetSLNamebyID(_myDictionary.DefaultSignLanguage) & AcronymShow
    End Function

    Friend Sub CopyBlankDB(ByVal Filename As String)
        My.Computer.FileSystem.CopyFile(Application.StartupPath & "\BlankDict.dat", Filename, True)
    End Sub

    Private Function CheckConnectionString(ByVal ConnString As String) As Boolean
        Dim oledbStBuild As New OleDbConnectionStringBuilder
        oledbStBuild.ConnectionString = ConnString
        Dim Filename As String = oledbStBuild.DataSource
        If FileSystem.FileExists(Filename) AndAlso oledbStBuild.Provider = "Microsoft.Jet.OLEDB.4.0" Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function CheckSQLiteConnectionString(ByVal ConnString As String) As Boolean
        Dim CSBuilder As New SqlConnectionStringBuilder
        CSBuilder.ConnectionString = ConnString
        If ConnString.Contains("data source=") Then
            Dim Filename As String = CSBuilder.DataSource
            If Paths.FileExists(Filename) Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    Private Function CreateConnectionString(Filename As String) As String
        Return "data source=" & Filename
    End Function

    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click
        Dim result =
                MessageBox.Show(
                    "You are importing to " & GetSLNameandAcronym() & " and gloss language " &
                    UI.Cultures.GetCultureFullName(Me._myDictionary.FirstGlossLanguage) &
                    ". If this is not what you want click No and change the options from the File menu.", "Import",
                    MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            SaveDataGrid()
            Me.ImportFileDialog.ShowDialog()
        End If
    End Sub

    Private Sub ExportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToolStripMenuItem.Click
        SaveDataGrid()
        myExportSettings.EntireDictionary =
            (MessageBox.Show(
                "Do you want to export the entire Dictionary?  Yes, for entire Dictionary.  No, for currently displaying items",
                "Export entire Dictionary?", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) = DialogResult.Yes)
        myExportSettings.Puddle =
            CInt(Interaction.InputBox("What puddle number would you like to use for the file? Default is 2000.",
                                      "Puddle Number", "2000"))
        myExportSettings.PuddleName = Interaction.InputBox("What what puddle name would you like to use?", "Puddle Name",
                                                           "Exported from SignWriter Studio™, puddle " &
                                                           myExportSettings.Puddle)

        ExportFileDialog.FileName = "sgn" & myExportSettings.Puddle & ".spml"
        ExportFileDialog.ShowDialog()
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        SaveDataGrid()
        Options()
        ShowloadedFile()
    End Sub


    Private Sub DictLoadedChanged()
        If Me.DictionaryLoaded Then
            Me.ImportToolStripMenuItem.Enabled = True
            Me.ExportToolStripMenuItem.Enabled = True
            Me.SignListsToolStripMenuItem.Enabled = True
            Me.TBSearch.Enabled = True
            Me.CBGloss1.Enabled = True
            Me.CBGloss2.Enabled = True
        Else
            Me.ImportToolStripMenuItem.Enabled = False
            Me.ExportToolStripMenuItem.Enabled = False
            Me.SignListsToolStripMenuItem.Enabled = False
            Me.TBSearch.Enabled = False
            Me.CBGloss1.Enabled = False
            Me.CBGloss2.Enabled = False
        End If
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Help.ShowHelp(Me, "SignWriterStudio.chm", "dictionary.htm")
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click

        LoadDictionaryEntries()
        LetKnowNoMatchesFound()

    End Sub

    Private Sub LetKnowNoMatchesFound()

        If _myDictionary.DictionaryBindingSource1.DataSource.Rows.Count = 0 Then
            Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, 
                                                 MessageBoxOptions)
            MessageBox.Show("There are no matches for your criteria.", "", MessageBoxButtons.OK,
                            MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        End If
    End Sub

    'Private Sub BtnShowAll_Click(sender As Object, e As EventArgs) Handles BtnShowAll.Click
    '    TBSearch.Text = ""
    '    LoadDictionaryAll()

    'End Sub

    Private Sub ExportFileDialog_FileOk(sender As Object, e As CancelEventArgs) Handles ExportFileDialog.FileOk

        SWEditorProgressBar.Text = "SignWriter Studio™ Exporting ..."
        SWEditorProgressBar.Show()
        SPMLExportbw.RunWorkerAsync()
    End Sub

    Private Sub SPMLExportbw_DoWork(sender As Object, e As DoWorkEventArgs) Handles SPMLExportbw.DoWork
        myExportSettings.Filename = ExportFileDialog.FileName
        If Directory.Exists(Path.GetDirectoryName(myExportSettings.Filename)) Then
            Dim SPMLConverter As New SpmlConverter

            'Dim Signs = SPMLConverter.ExportSPML(ExportFileDialog.FileName, myExportSettings.Puddle, MyDictionary, myExportSettings.EntireDictionary, SPMLExportbw)
            Dim Signs = SPMLConverter.ExportSPML(myExportSettings, _myDictionary, SPMLExportbw)

        End If
    End Sub

    Private Sub SPMLExportbw_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) _
        Handles SPMLExportbw.ProgressChanged
        SWEditorProgressBar.ProgressBar1.Value = e.ProgressPercentage
        If e.ProgressPercentage > 50 Then
            SWEditorProgressBar.Text = "SignWriter Studio™ Exporting ..."

        End If
    End Sub

    Private Sub SPMLExportbw_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) _
        Handles SPMLExportbw.RunWorkerCompleted
        SWEditorProgressBar.ProgressBar1.Value = 100
        SWEditorProgressBar.Hide()
        SWEditorProgressBar.ProgressBar1.Value = 0
    End Sub


    Private Sub DictionaryDataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) _
        Handles DictionaryDataGridView.CellContentClick
    End Sub

    Private Sub btnEditSignWriting_Click(sender As Object, e As EventArgs) Handles btnEditSignWriting.Click
        Try
            If DictionaryDataGridView.CurrentRow IsNot Nothing Then
                Dim idDictionary1 As Integer = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value
                If idDictionary1 <> 0 Then
                    OpenSignEditorThenSave(idDictionary1)
                End If
            End If

        Catch ex As SQLiteException
            LogError(ex, "SQLite Exception " & ex.GetType().Name)
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Private Sub SaveSignWriting(ByVal conn As SQLiteConnection, ByRef trans As SQLiteTransaction,
                                ByVal idDictionary1 As Integer)
        'Save SWriting
        DictionaryDataGridView.CurrentRow.Cells("SWriting").Value = Editor.ToImage()
        DictionaryDataGridView.CurrentRow.Cells("Sorting").Value =
            _myDictionary.SequencetoSortingString(Editor.Sign.Frames.First().Sequences)
        DictionaryDataGridView.EndEdit()
        _myDictionary.SaveSWSign(idDictionary1, Editor.Sign, conn, trans)

        SaveDataGrid(conn, trans)
    End Sub

    Private Function ClassifySigns(ByVal Signs As List(Of SwSign), ByRef conn As SQLiteConnection,
                                   ByRef trans As SQLiteTransaction) _
        As  _
        Tuple _
            (Of List(Of SwSign), List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)), 
                List(Of SwSign))
        Dim signsToAdd As New List(Of SwSign)
        Dim signsNotModified As New List(Of SwSign)
        Dim signstoCompare As New List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))
        Static allCachedDictionaryDataTable As DataTable
        Dim taDictionary As New DictionaryTableAdapter
        taDictionary.AssignConnection(conn, trans)
        allCachedDictionaryDataTable = taDictionary.GetData()

        Dim foundbyGuid As DictionaryDataSet.DictionaryRow

        For Each Sign As SwSign In Signs
            If Sign.SignWriterGuid.HasValue Then

                foundbyGuid = DatabaseDictionary.GetDataDictionaryByGuid(allCachedDictionaryDataTable,
                                                                         Sign.SignWriterGuid)
                If foundbyGuid IsNot Nothing Then

                    If Not Date.Compare(Sign.LastModified, foundbyGuid.LastModified) = 0 Then
                        signstoCompare.Add(Tuple.Create(Sign, foundbyGuid, True))
                    Else
                        signsNotModified.Add(Sign)
                    End If

                Else
                    signsToAdd.Add(Sign)
                End If
            Else
                signsToAdd.Add(Sign)
            End If
        Next
        Return Tuple.Create(signsNotModified, signstoCompare, signsToAdd)
    End Function

    Private Function SelectComparedSigns(
                                         ByVal signsToCompare As  _
                                            List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)),
                                         ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) _
        As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))
        Dim signsToOverwrite As New List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))
        Dim compareSigns As New CompareSigns(DictionaryConnectionString)
        If signsToCompare.Count > 0 Then
            'compareSigns.Conn = conn
            'compareSigns.Trans = trans
            compareSigns.SignsToCompare = signsToCompare
            compareSigns.ShowDialog()

            For Each Item In compareSigns.ListToShow
                If Item.OverwritefromPuddle Then
                    signsToOverwrite.Add(Tuple.Create(Item.puddleSign, Item.StudioDictRow))
                End If
            Next

        End If
        Return signsToOverwrite
    End Function

    Private Sub CopyToSignPuddle()
        Dim fsw = GetFsw()
        Process.Start("http://www.signbank.org/signpuddle2.0/signtextsave.php?ui=1&sgn=&sgntxt=" & fsw)
    End Sub

    Private Function GetFsw() As String

        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
        Using conn
            Try
                If DictionaryDataGridView.GetCellCount(
                    DataGridViewElementStates.Selected) > 0 Then

                    Try
                        If _
                            DictionaryDataGridView.SelectedCells.Contains(ClickedCell) AndAlso
                            DictionaryDataGridView.GetCellCount(
                                DataGridViewElementStates.Selected) > 1 Then
                            ' Add the selection to the clipboard.
                            Clipboard.SetDataObject(
                                Me.DictionaryDataGridView.GetClipboardContent())
                        Else
                            'Copy Clicked Cell

                            Dim idDictionary1 = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value

                            Dim sign = _myDictionary.GetSWSign(idDictionary1,
                                                               conn, trans)

                            Dim conv As New SpmlConverter

                            Dim fsw As String = conv.GetFsw(sign)
                            trans.Commit()
                            Return fsw
                        End If

                    Catch ex As Exception
                        LogError(ex, "Exception " & ex.GetType().Name)
                        If trans IsNot Nothing Then trans.Rollback()
                    End Try

                End If

            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                 If trans IsNot Nothing Then trans.Dispose()
                conn.Close()
                conn.Dispose()
            End Try
        End Using
    End Function


    Private Sub CopySign()

        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
        Using conn
            Try
                If DictionaryDataGridView.GetCellCount(
                    DataGridViewElementStates.Selected) > 0 Then

                    Try

                        ' Add the selection to the clipboard.

                        If DictionaryDataGridView.CurrentRow IsNot Nothing Then
                            Dim cr = DictionaryDataGridView.CurrentRow
                            Dim sign As SwSign = _myDictionary.GetSWSign(cr.Cells("IDDictionary").Value, conn, trans)
                            Dim clipboardSign As New ClipboardSign
                            clipboardSign.SWSign = sign
                            clipboardSign.Gloss = CStr(cr.Cells("Gloss1").Value)
                            clipboardSign.Glosses = CStr(cr.Cells("Glosses1").Value)
                            If Not Information.IsDBNull(cr.Cells("Photo").Value) Then
                                clipboardSign.Illustration = CType(ByteArraytoImage(cr.Cells("Photo").Value), Bitmap)
                            End If
                            If Not Information.IsDBNull(cr.Cells("Sign").Value) Then
                                clipboardSign.Sign = CType(ByteArraytoImage(cr.Cells("Sign").Value), Bitmap)
                            End If
                            clipboardSign.SWSignSource = CStr(cr.Cells("SWSignSource").Value)
                            clipboardSign.IllustrationSource = CStr(cr.Cells("PhotoSource").Value)
                            clipboardSign.SignSource = CStr(cr.Cells("SignSource").Value)
                            Dim signWritingImage As Image = Nothing
                            If Not Information.IsDBNull(cr.Cells("SWriting").Value) Then
                                signWritingImage = ByteArraytoImage(cr.Cells("SWriting").Value)
                            End If
                            SetSWSigntoClipboard(clipboardSign, signWritingImage)
                        End If

                    Catch ex As Exception
                        LogError(ex, "Exception " & ex.GetType().Name)


                    End Try

                End If
                trans.Commit()
            Catch ex As SQLiteException
                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                conn.Close()

            End Try
        End Using
    End Sub

    Private Sub CopyIllustrationToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles CopyIllustrationToolStripMenuItem.Click
        Try
            If DictionaryDataGridView.CurrentRow IsNot Nothing Then
                'Copy 
                If Not Information.IsDBNull(DictionaryDataGridView.CurrentRow.Cells("Photo").Value) Then
                    SetImagetoClipboard(ByteArraytoImage(DictionaryDataGridView.CurrentRow.Cells("Photo").Value))
                Else
                    SetImagetoClipboard(Nothing)
                End If
            End If
        Catch ex As Exception
            LogError(ex, "Exception " & ex.GetType().Name)

            Throw
        End Try
    End Sub

    Private Sub CopyPhotoSignToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles CopyPhotoSignToolStripMenuItem.Click
        Try
            If DictionaryDataGridView.CurrentRow IsNot Nothing Then
                'Copy 
                If Not Information.IsDBNull(DictionaryDataGridView.CurrentRow.Cells("Sign").Value) Then
                    SetImagetoClipboard(ByteArraytoImage(DictionaryDataGridView.CurrentRow.Cells("Sign").Value))
                Else
                    SetImagetoClipboard(Nothing)
                End If
            End If
        Catch ex As Exception
            LogError(ex, "Exception " & ex.GetType().Name)

            Throw
        End Try
    End Sub

    Private Sub CopyToSignPuddleToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
        Handles CopyToSignPuddleToolStripMenuItem1.Click
        CopyToSignPuddle()
    End Sub

    Private Sub PasteIllustrationToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles PasteIllustrationToolStripMenuItem.Click
        DictionaryDataGridView.CurrentRow.Cells("Photo").Value = GetImageFromClipboard()
        SaveDataGrid()
    End Sub

    Private Sub PastePhotoSignToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles PastePhotoSignToolStripMenuItem.Click
        DictionaryDataGridView.CurrentRow.Cells("Sign").Value = GetImageFromClipboard()
        SaveDataGrid()
    End Sub

    Private Sub PasteFSWFromSignPuddleToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles PasteFSWFromSignPuddleToolStripMenuItem.Click
        SaveDataGrid()
        PasteFsw()
    End Sub

    Private Sub CopySignToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles CopySignToolStripMenuItem.Click
        CopySign()
    End Sub


    Private Sub AddNewEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles AddNewEntryToolStripMenuItem.Click
        AddNew()
    End Sub

    Private Sub DuplicateEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles DuplicateEntryToolStripMenuItem.Click
        SaveDataGrid()
        Duplicate()
        SaveDataGrid()
    End Sub

    Private Sub PasteSignToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles PasteSignToolStripMenuItem.Click
        PasteSign()
    End Sub

    Private Sub DeleteEntryToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles DeleteEntryToolStripMenuItem.Click
        DeleteEntries()
    End Sub

    Private Sub DeleteEntries()
        If (DictionaryDataGridView.SelectedRows.Count > 1 AndAlso
            MessageBox.Show("Do you really want to delete selected entries?", "Delete Entries", MessageBoxButtons.YesNo) =
            DialogResult.Yes) Then
            Dim rowIds = New List(Of Tuple(Of Long, Object))()

            For Each row In DictionaryDataGridView.SelectedRows
                Dim id = CLng(row.Cells("IDDictionary").Value)
                rowIds.Add(Tuple.Create(id, row))
            Next

            For Each todelete In rowIds
                Try
                    DeleteEntry(todelete.Item1, todelete.Item2)
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                End Try
            Next
            SaveDataGrid()
        Else
            Dim id As Long = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value
            If Not id = 0 AndAlso
               MessageBox.Show("Do you really want to delete this entry?", "Delete Entry", MessageBoxButtons.YesNo) =
               DialogResult.Yes Then

                DeleteEntry(id, DictionaryDataGridView.CurrentRow)
                SaveDataGrid()
            End If
        End If
    End Sub

    Private Sub DeleteEntry(ByVal id As Long, row As DataGridViewRow)
        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
        Using conn
            Try
                AskDeleteFromPuddle(id, conn, trans)
                _myDictionary.DeleteSign(id, conn, trans)

                DictionaryDataGridView.Rows.Remove(row)
            Catch ex As SQLiteException
                LogError(ex, "SQLite Exception " & ex.GetType().Name)

                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                trans.Commit()

            End Try
        End Using
    End Sub

    Private Sub AskDeleteFromPuddle(ByVal id As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        If _puddleLoggedIn Then
            Dim sid As String
            If id <> 0 Then
                Dim currentSign = _myDictionary.GetSWSign(id, conn, trans)

                sid = currentSign.SignPuddleId
            End If

            If Not String.IsNullOrEmpty(sid) Then
                Dim btnResult = MessageBox.Show("Would you like to remove the entry from SignPuddle too?", "Delete from Puddle", MessageBoxButtons.YesNo)
                If btnResult = DialogResult.Yes Then
                    DeleteFromPuddle(id, "", conn, trans)
                End If
            End If
        End If
    End Sub

    Private Sub BindingNavigatorDeleteItem_Click(sender As Object, e As EventArgs) _
        Handles BindingNavigatorDeleteItem.Click
        DeleteEntries()
    End Sub

    Private Sub CopySignImageToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles CopySignImageToolStripMenuItem.Click
        Dim imageValue = DictionaryDataGridView.CurrentRow.Cells("SWriting").Value
        Dim signWritingImage As Image
        If Not Information.IsDBNull(imageValue) Then
            signWritingImage = ByteArraytoImage(imageValue)
            Clipboard.Clear()
            Clipboard.SetImage(signWritingImage)

        End If
    End Sub

    Private Sub ExportToAnkiToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ExportToAnkiToolStripMenuItem.Click
        SaveDataGrid()
        Dim ankiFrm = New ExportAnkiFrm
        ankiFrm.TagFilter1.TagListControl1.SelectionItemList(GetTagsData())
        ankiFrm.TagFilter1.AssumeFiltering = True
        ankiFrm.MyDictionary = _myDictionary
        ankiFrm.Show()
    End Sub

    Private Sub ExportToHTMLToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ExportToHTMLToolStripMenuItem.Click
        SaveDataGrid()
        Dim exportHtmlFrm = New ExportHtmlFrm
        exportHtmlFrm.TagFilter1.TagListControl1.SelectionItemList(GetTagsData())
        exportHtmlFrm.TagFilter1.AssumeFiltering = True
        exportHtmlFrm.MyDictionary = _myDictionary
        exportHtmlFrm.Show()
    End Sub

    Private Sub CopyFSWToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyFSWToolStripMenuItem.Click
        Dim fsw = GetFsw()
        If fsw IsNot Nothing Then
            Clipboard.SetText(fsw)
        End If
    End Sub

    Private Sub btnSpell_Click(sender As Object, e As EventArgs) Handles btnSpell.Click
        Dim result =
                       MessageBox.Show("This will attempt to spell any unspelt signs.  If there isn't enoough information to suggest a spelling, the spelling will be left blank. Do you with to continue?", "Spell signs", MessageBoxButtons.YesNo)

        If result = DialogResult.Yes Then
            TBSearch.Text = ""
            _myDictionary.AllSigns()
            SuggestSpellings()
        End If
    End Sub

    Public Sub SuggestSpellings()
        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)

        Try
            Dim start = 0
            Dim dt As DataTable = _myDictionary.GetAllSignsUnilingualDt()

            Do While start <= dt.Rows.Count

                Const qty As Integer = 100
                Dim rows = GetRows(dt.Rows, start, qty)
                start += qty
                For Each row As DataRow In rows
                    Dim idDictionary1 = CType(row.Item("IDDictionary"), Long)

                    Dim sign1 = _myDictionary.GetSWSign(idDictionary1, conn, trans)

                    If sign1.Frames.FirstOrDefault().Sequences.Count = 0 Then

                        Dim suggestedSequence = Editor.OrderSuggestion1(sign1, False).ToList()
                        If suggestedSequence.Count > 0 Then
                            Dim sequences = sign1.Frames.FirstOrDefault().Sequences
                            sequences.Clear()
                            For Each sequence As SWSequence In suggestedSequence
                                sequences.Add(sequence)
                            Next

                            Dim sortString = _myDictionary.SequencetoSortingString(suggestedSequence)
                            sign1.SortString = sortString
                            _myDictionary.SaveSWSign(idDictionary1, sign1, conn, trans)
                        End If
                    End If
                Next
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        trans.Commit()
        trans.Dispose()
        conn.Close()
        conn.Dispose()

    End Sub

    Private Shared Function GetRows(ByVal dataRowCollection As DataRowCollection, ByVal start As Integer, ByVal qty As Integer) As List(Of DataRow)
        Dim rowList = New List(Of DataRow)
        Dim count As Integer = dataRowCollection.Count
        For i As Integer = 0 To start + qty

            If i >= start AndAlso i < count Then
                rowList.Add(dataRowCollection(i))
            End If

        Next
        Return rowList
    End Function

    Private Sub SPMLImportbw_DoWork1(sender As Object, e As DoWorkEventArgs) Handles SPMLImportbw.DoWork

    End Sub

    Private Sub btnTagsForm_Click(sender As Object, e As EventArgs) Handles btnTagsForm.Click
        Dim tf = New TagsForm()
        tf.SetFromDb(_myDictionary.GetTags())
        tf.ShowDialog()
        Dim changes = tf.GetChanges()

        If (tf.DialogResult = DialogResult.OK) Then
            _myDictionary.SaveTags(changes.Added, changes.Updated, changes.Removed)
            Dim tagData = GetTagsData()
            Tags.DataSource = tagData
            TagFilter1.TagListControl1.SelectionItemList(tagData)
        End If
        tf.Close()
    End Sub

    Private Sub AddTagToRow(ByVal row As DataGridViewRow, ByVal value As List(Of Guid))
        Dim cell = row.Cells("Tags")

        cell.Value = value.ConvertAll(Function(x) x.ToString())

    End Sub

    Private Sub btnShowSource_Click(sender As Object, e As EventArgs) Handles btnShowSource.Click
        SWSignSource.Visible = Not SWSignSource.Visible
        SignSource.Visible = SWSignSource.Visible
        PhotoSource.Visible = SWSignSource.Visible

        btnShowSource.Text = If(SWSignSource.Visible, "Hide Sources", "Show Sources")
    End Sub


    Private Sub TagFilter1_ValueChanged(sender As Object, args As EventArgs) Handles TagFilter1.ValueChanged
        If DictionaryLoaded Then
            Dim newTagFilterValues = GetTagFilterValues(TagFilter1)
            Dim reFilter = ShouldRefilter(newTagFilterValues, currentTagFilterValues)

            currentTagFilterValues = newTagFilterValues
            If (reFilter) Then
                LoadPage()
            End If
        End If
    End Sub

    Private Shared Function ShouldRefilter(ByVal newTagFilterValues As TagFilterValues, ByVal previousTagFilterValues As TagFilterValues) As Boolean

        Dim refilter = False
        If (previousTagFilterValues Is Nothing AndAlso newTagFilterValues Is Nothing) Then refilter = False
        If (previousTagFilterValues Is Nothing AndAlso newTagFilterValues.Filter) Then refilter = True
        If (previousTagFilterValues IsNot Nothing AndAlso newTagFilterValues IsNot Nothing) Then
            If (Not previousTagFilterValues.Filter = newTagFilterValues.Filter) Then
                refilter = True
            End If
            If (Not previousTagFilterValues.AllExcept = newTagFilterValues.AllExcept) Then
                refilter = True
            End If
            If (Not previousTagFilterValues.Tags.Count = newTagFilterValues.Tags.Count) Then
                refilter = True
            End If
        End If

        Return refilter
    End Function

    Private Shared Function GetTagFilterValues(ByVal tagFilter As TagFilter) As TagFilterValues
        Dim tagFilterValues = New TagFilterValues()
        tagFilterValues.Filter = tagFilter.CBFilter.Checked
        tagFilterValues.AllExcept = tagFilter.CBAllBut.Checked
        tagFilterValues.Tags = tagFilter.TagListControl1.TagValues

        Return tagFilterValues
    End Function

    Private Sub ViewReportPDFWordExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewReportPDFWordExcelToolStripMenuItem.Click
        Dim reportForm = New ReportForm()
        reportForm.Dictionary = _myDictionary
        reportForm.TagFilter1.TagListControl1.SelectionItemList(GetTagsData())
        reportForm.Show()
    End Sub


    Private Function GetConnectionString() As String
        Dim taVer As New DictionaryDataSetTableAdapters.VersionTableAdapter
        Return taVer.Connection.ConnectionString
    End Function

    Private Sub SignInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SignInToolStripMenuItem.Click
        Dim signPuddleSignIn = New SignPuddleSignIn()
        signPuddleSignIn.ShowDialog()
        If (signPuddleSignIn.IsLoggedIn) Then
            _puddleLoggedIn = True
            _puddleSgn = signPuddleSignIn.Sgn
            _puddleName = signPuddleSignIn.Name
            _puddleApi = signPuddleSignIn.SignPuddleApi
        End If
        SetPuddleMenu(_puddleLoggedIn)
    End Sub

    Private Sub SetPuddleMenu(ByVal puddleLoggedIn As Boolean)
        SignInToolStripMenuItem.Enabled = Not puddleLoggedIn
        SignOutToolStripMenuItem.Enabled = puddleLoggedIn
        SendToPuddleToolStripMenuItem.Enabled = puddleLoggedIn
        SendSelectedEntriesToPuddleToolStripMenuItem.Enabled = puddleLoggedIn
        DeleteFromPuddleToolStripMenuItem.Enabled = puddleLoggedIn
        SendSignToPuddleToolStripMenuItem.Enabled = puddleLoggedIn
    End Sub

    Private Sub SignOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SignOutToolStripMenuItem.Click
        _puddleLoggedIn = False
        _puddleSgn = String.Empty
        _puddleName = String.Empty
        _puddleApi = Nothing
        SetPuddleMenu(_puddleLoggedIn)
    End Sub

    Private Sub SendToPuddleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendToPuddleToolStripMenuItem.Click
        SaveDataGrid()

        Dim idDictionary1 As Integer = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value
        Dim allTags = _myDictionary.GetTags()
        SendEntrytoPuddle(idDictionary1, allTags)
    End Sub

    Private Sub SendEntrytoPuddle(ByVal idDictionary1 As Integer, ByVal allTags As List(Of ExpandoObject))

        Dim gloss As String
        Dim glosses As String
        If Not IsDbNull(DictionaryDataGridView.CurrentRow.Cells("gloss1").Value) Then
            gloss = DictionaryDataGridView.CurrentRow.Cells("gloss1").Value
        End If
        If Not IsDbNull(DictionaryDataGridView.CurrentRow.Cells("glosses1").Value) Then
            glosses = DictionaryDataGridView.CurrentRow.Cells("glosses1").Value
        End If
        Dim sign As SwSign = Nothing
        If idDictionary1 <> 0 Then
            sign = _myDictionary.GetSWSign(idDictionary1)
        End If

        Dim tags1 = _myDictionary.GetTagEntries(New List(Of String)() From {idDictionary1.ToString})
        Dim tagNames = GetTagNames(allTags, tags1)
        SendToPuddle(sign, gloss, glosses, tagNames)
    End Sub
    Private Sub SendEntrySigntoPuddle(ByVal idDictionary1 As Integer)
        Dim gloss As String = ""

        If Not IsDbNull(DictionaryDataGridView.CurrentRow.Cells("gloss1").Value) Then
            gloss = DictionaryDataGridView.CurrentRow.Cells("gloss1").Value
        End If

        Dim sign As SwSign = Nothing
        If idDictionary1 <> 0 Then
            sign = _myDictionary.GetSWSign(idDictionary1)
        End If


        SendToPuddle(sign, gloss)
    End Sub


    Private Function GetTagNames(ByVal allTags As List(Of ExpandoObject), ByVal tags1 As List(Of ExpandoObject)) As List(Of String)
        Dim tagNames = New List(Of String)
        For Each Tag As Object In tags1
            Dim idTag = Tag.idTag
            Dim idTagParent = GetParentGuid(allTags, idTag)

            Dim tagName = GetTagName(allTags, idTag)
            Dim tagParentName = GetTagParentName(allTags, idTagParent)
            tagNames.Add(tagParentName & ":" & tagName)
        Next

        If (tagNames.Any()) Then

            Return tagNames
        Else
            Return Nothing
        End If
    End Function

    Private Function GetTagParentName(ByVal allTags As List(Of ExpandoObject), ByVal idTag As Guid) As Object
        Return GetTagName(allTags, idTag)
    End Function

    Private Function GetTagName(allTags As List(Of ExpandoObject), idTag As Guid) As String
        For Each Tag As Object In allTags
            If Tag.IdTag = idTag Then
                Return Tag.Description
            End If
        Next
        Return String.Empty
    End Function
    Private Function GetParentGuid(allTags As List(Of ExpandoObject), idTag As Guid) As Guid
        For Each Tag As Object In allTags
            If Tag.IdTag = idTag Then
                Return Tag.Parent
            End If
        Next
        Return System.Guid.Empty
    End Function
    Private Sub SendToPuddle(ByVal swSign As SwSign, ByVal gloss As String, ByVal glosses As String, ByVal tagNames As List(Of String))
        Dim converter = New SpmlConverter()

        Dim sgntxt = SpmlConverter.Fsw2Ksw(converter.GetFsw(swSign))
        Dim build = converter.GetBuild(swSign)
        Dim txt = ConcatenatePuddleText(swSign.PuddleText)

        'Dim signWriterJson = GetSignWriterJson(swSign, tagNames)
        'If (Not String.IsNullOrEmpty(signWriterJson)) Then
        '    txt &= signWriterJson
        'End If

        Dim prev = swSign.PuddlePrev
        Dim top = swSign.PuddleTop
        Dim nextStr = swSign.PuddleNext
        Dim src = swSign.SWritingSource
        Dim video = swSign.PuddleVideoLink

        Dim trm = GetSignTerms(gloss, glosses)
        Dim webPageResult As String
        Dim originalSid = swSign.SignPuddleId
        If (Not originalSid = String.Empty) Then
            webPageResult = _puddleApi.UpdateEntry("1", _puddleSgn, originalSid, sgntxt, build, txt, top, prev, nextStr, src, video, trm)
        Else
            webPageResult = _puddleApi.AddEntry("1", _puddleSgn, sgntxt, build, txt, top, prev, nextStr, src, video, trm)
        End If

        Dim sid = _puddleApi.GetFirsSidInWebPage(webPageResult)
        Dim wasAdded = _puddleApi.WasAdded(webPageResult)

        If Not wasAdded Then
            MessageBox.Show("There was an error adding the sign " & gloss & " to the puddle")
        Else
            If Not sid = originalSid Then
                swSign.SignPuddleId = sid
                SaveSign(swSign)
            End If
        End If
    End Sub
    Private Sub SendToPuddle(ByVal swSign As SwSign, gloss As String)
        Dim converter = New SpmlConverter()


        Dim build = converter.GetBuild(swSign)


        Dim webPageResult As String
        Dim originalSid = swSign.SignPuddleId
        If (Not originalSid = String.Empty) Then
            webPageResult = _puddleApi.SendSign("1", _puddleSgn, originalSid, build)
        Else
            MessageBox.Show("There was an error adding the sign " & gloss & " to the puddle. Create it first and send whole entry.")
        End If

        Dim sid = _puddleApi.GetFirsSidInWebPage(webPageResult)
       
    End Sub
    Private Sub SaveSign(ByVal signToSave As SwSign)

        Dim path = DictionaryFilename
        DbDictionary.UpdateSignPuddleId(path, signToSave.SignWriterGuid, signToSave.SignPuddleId)

    End Sub


    Private Shared Function GetSignWriterJson(ByVal swSign As SwSign, ByVal tagNames As List(Of String)) As String
        Dim signWriterJson = New SignWriterStudio.Dictionary.Json.SignWriterStudioJson()
        signWriterJson.SignWriterStudio = New SignWriterStudio.Dictionary.Json.SignWriterStudio()
        signWriterJson.SignWriterStudio.Guid = swSign.SignWriterGuid
        signWriterJson.SignWriterStudio.Tags = tagNames

        Dim json = Newtonsoft.Json.JsonConvert.SerializeObject(signWriterJson,
            New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
        Return json
    End Function

    Private Shared Function ConcatenatePuddleText(ByVal puddleText As List(Of String)) As String
        Dim sb As New StringBuilder()

        For Each pText As String In puddleText
            sb.AppendLine(pText)
        Next
        Return sb.ToString()
    End Function

    Private Shared Function GetSignTerms(ByVal gloss As String, ByVal glosses As String) As List(Of String)
        Dim terms = New List(Of String)
        If gloss IsNot Nothing Then
            terms.Add(gloss)
            If glosses IsNot Nothing Then
                terms.AddRange(glosses.Split(","))
            End If
        End If

        Return terms
    End Function

    Private Sub DeleteFromPuddleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteFromPuddleToolStripMenuItem.Click
        Dim idDictionary1 As Integer = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value
        Dim gloss As String = DictionaryDataGridView.CurrentRow.Cells("gloss1").Value
        DeleteFromPuddle(idDictionary1, gloss)
    End Sub

    Private Sub DeleteFromPuddle(ByVal idDictionary1 As Integer, ByVal gloss As String, Optional ByVal conn As SQLiteConnection = Nothing, Optional ByVal trans As SQLiteTransaction = Nothing)
        Dim sid As String
        Dim currentSign As SwSign
        If idDictionary1 <> 0 Then
            If conn Is Nothing Then
                currentSign = _myDictionary.GetSWSign(idDictionary1)
            Else
                currentSign = _myDictionary.GetSWSign(idDictionary1, conn, trans)
            End If

            If currentSign IsNot Nothing Then
                sid = currentSign.SignPuddleId
            End If
        End If

        If String.IsNullOrEmpty(sid) Then
            MessageBox.Show("Entry for " & gloss & " does not have SignPuddle Id.")
        Else
            Dim webPageResult = _puddleApi.DeleteEntry("1", _puddleSgn, sid)

            Dim wasDeleted = _puddleApi.WasDeleted(webPageResult)

            If Not wasDeleted Then
                MessageBox.Show("The sign " & gloss & " could not be deleted from the puddle.")
            End If
        End If
    End Sub

    Private Sub SendSelectedEntriesToPuddleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendSelectedEntriesToPuddleToolStripMenuItem.Click

        Dim rowIds = New List(Of Tuple(Of Long, Object))()

        For Each row In DictionaryDataGridView.SelectedRows
            Dim id = CLng(row.Cells("IDDictionary").Value)
            rowIds.Add(Tuple.Create(id, row))
        Next
        SaveDataGrid()
        Dim allTags = _myDictionary.GetTags()
        For Each tuple As Tuple(Of Long, Object) In rowIds
            SendEntrytoPuddle(tuple.Item1, allTags)
        Next
    End Sub



    Private Sub ExportFingerSpellingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportFingerSpellingToolStripMenuItem.Click
        SaveDataGrid()
        Dim fingerSpellingFrm = New ExportFingerSpellingFrm
        fingerSpellingFrm.TagFilter1.TagListControl1.SelectionItemList(GetTagsData())
        fingerSpellingFrm.TagFilter1.AssumeFiltering = True
        fingerSpellingFrm.MyDictionary = _myDictionary
        fingerSpellingFrm.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click


    End Sub
    Private Sub AddSignsinNewDictNoImageDibujoGroup1()
        Dim path = DictionaryFilename
        Dim listIdDictionary = DbDictionary.GetAllIds(path)

        Dim entries = DbTagsDictionary.GetTagEntries(path, listIdDictionary)
        Dim list = New List(Of String)()
        Dim newDict = New Guid("16D3558D-0767-4B2E-83D1-A5A02BD5052D")
        For Each entry As Object In entries
            If entry.IdTag = newDict Then
                Dim dictEntry = DbDictionary.GetDictionaryEntries(path, "IDDictionary = " & entry.IDDictionary)
                Dim foundEntry As Object = (dictEntry.TabularResults.FirstOrDefault).FirstOrDefault

                If foundEntry IsNot Nothing AndAlso IsDbNull(foundEntry.Photo) Then
                    list.Add(entry.IDDictionary)
                End If


            End If
        Next

        Dim dibujoGroup1 = New Guid("2F716EBB-561C-480F-8996-0ED0C34A1832")


        Dim affectedRows = DbTagsDictionary.InsertTag(path, list, dibujoGroup1.ToString())
    End Sub
    Private Sub AddNewDicttoPreviousDictEntries()
        Dim path = DictionaryFilename
        Dim listIdDictionary = DbDictionary.GetAllIds(path)

        Dim entries = DbTagsDictionary.GetTagEntries(path, listIdDictionary)
        Dim list = New List(Of String)()
        Dim previousDictionary = New Guid("D58D8114-7DC4-4D3D-9594-EE3756ED1854")
        For Each entry As Object In entries
            If entry.IdTag = previousDictionary Then
                list.Add(entry.IDDictionary)
            End If
        Next

        Dim newDictionary = New Guid("16D3558D-0767-4B2E-83D1-A5A02BD5052D")

        Dim affectedRows = DbTagsDictionary.InsertTag(path, list, newDictionary.ToString())
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub SendSignToPuddleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SendSignToPuddleToolStripMenuItem.Click
        SaveDataGrid()

        Dim idDictionary1 As Integer = DictionaryDataGridView.CurrentRow.Cells("IDDictionary").Value

        SendEntrySigntoPuddle(idDictionary1)
    End Sub
End Class

Public Class AddedEntry

    Public Property ID() As Long
    Public Property Gloss() As String
    Public Property Glosses() As String
End Class