
Imports SignWriterStudio.Dictionary
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.Settings
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSet
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters
Imports SignWriterStudio.Settings.SettingsPublic
Imports SignWriterStudio.SWEditor

Public Class GlossToSignRealTime
    Dim Dictionary As New SWDict
    Friend Signs As List(Of Tuple(Of SwSign, Integer))
    Dim _clickedControl As GlossToSignRealTimeControl

    Dim WithEvents glossToSignRealTimeControl As New GlossToSignRealTimeControl

    Private _glossNotFound As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Dim _currentGlossControl As GlossToSignRealTimeControl

    Sub GlossToSignControlEventHandler(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles glossToSignRealTimeControl.MouseDown

        'MsgBox("Received Event.")
    End Sub

    Private Sub GlossToSign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Help.ShowHelp(Me, "SignWriterStudio.chm", "glosstosign.htm")
        End If
    End Sub

    Private Sub GlossToSign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        KeyPreview = True
        GlossToSignDataGridView.AutoGenerateColumns = False
        Dictionary.FirstGlossLanguage = FirstGlossLanguage
        Dictionary.SecondGlossLanguage = SecondGlossLanguage
        Dictionary.DefaultSignLanguage = DefaultSignLanguage

        FindifTextinClipboard()

    End Sub

    Private Sub FindifTextinClipboard()
        Dim clipboardText = Clipboard.GetText()
        If Not String.IsNullOrEmpty(clipboardText) AndAlso Not IsJson(clipboardText) Then
            TBGlossToSign.Text = clipboardText
            Dim glossToSignArray As String() = GetGlossToSignArray()
            If glossToSignArray.Count < 16 Then
                GlossToSign()
            End If
        End If
    End Sub

    Private Function IsJson(ByVal str As String) As Boolean
        Return str.StartsWith("{")
    End Function

    Private Sub btnGlossToSign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGlossToSign.Click
        GlossToSign()
    End Sub

    Private Sub GlossToSign()
        Try
            FlowLayoutPanel1.Controls.Clear()
            _glossNotFound.Clear()
            Dim glossToSignArray As String() = GetGlossToSignArray()
            Dim i As Integer
            For i = 0 To glossToSignArray.GetUpperBound(0)
                AddGlossControl(glossToSignArray(i))
            Next

            TBGlossNotFound.Text = String.Join(", ", _glossNotFound.Select(Function(kvp) String.Format("{0}", kvp.Value)).ToArray())
        Catch ex As ArgumentException
            MessageBox.Show("Choose SignWriter Studio Dictionary before continuing")
        End Try

    End Sub

    Private Function GetGlossToSignArray() As String()

        Dim glossToSignArray() As String

        Dim delimiters() As String = {" ", Chr(34), vbCrLf}
        Dim textString As String = TBGlossToSign.Text
        textString = textString.Replace("{", "").Replace("}", "").Replace("}", "").Replace("(", "").Replace(")", "")
        textString = textString.Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("¡", " ¡ ").Replace("?", " ? ").Replace("¿", " ¿ ").Replace(":", " : ").Replace(";", " ; ").Replace("   ", " ").Replace("  ", " ")
        glossToSignArray = textString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
        Dim glossToSignArray1 = glossToSignArray.Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso x IsNot Environment.NewLine).ToArray()
        Dim glossToSignArray2 = MultipleSigns(glossToSignArray1)
        Return glossToSignArray2
    End Function

    Private Function MultipleSigns(ByVal glosses As String()) As String()
        Dim glossToSignArray = New List(Of String)()
        For Each s As String In glosses

            If (s.Contains("X")) Then
                Dim textBeforeX = GetTextBefore(s, "X")
                Dim textAfterX = GetTextAfter(s, "X")
                Dim mult As Integer = 0
                Dim isInt = Integer.TryParse(textAfterX, mult)

                If isInt AndAlso mult > 0 Then
                    For i = 1 To mult
                        glossToSignArray.Add(textBeforeX)
                    Next
                End If
            Else
                glossToSignArray.Add(s)
            End If
        Next
        Return glossToSignArray.ToArray()
    End Function

    Private Function GetTextAfter(ByVal str As String, ByVal find As String) As String
        Dim findStart = str.IndexOf(find, StringComparison.Ordinal)
        If findStart > 0 Then
            Dim findEnd As Integer = findStart + find.Length
            Return str.Substring(findEnd, str.Length - (findEnd))
        End If
        Return String.Empty
    End Function

    Private Function GetTextBefore(ByVal str As String, ByVal find As String) As String
        Dim findStart = str.IndexOf(find, StringComparison.Ordinal)
        If findStart > 0 Then
            Return str.Substring(0, findStart)
        End If
        Return String.Empty
    End Function

    Private Sub BtnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAccept.Click
        Dim dt As DataTable
        Dim rows() As DataRow
        Dim idDictionary As Integer


        Dim IDs = New List(Of Tuple(Of Integer, String, Integer))

        For Each control As Control In Me.FlowLayoutPanel1.Controls
            Dim glossToSignControl As GlossToSignRealTimeControl = CType(control, GlossToSignRealTimeControl)
            dt = glossToSignControl.FoundWordDt
            rows = dt.Select("Selected=True")
            If rows.Length > 0 Then
                idDictionary = rows(0).Item("IDDictionary")
                If Not idDictionary = 0 Then
                    IDs.Add(Tuple.Create(idDictionary, "", glossToSignControl.Lane))
                Else
                    IDs.Add(Tuple.Create(0, glossToSignControl.TextBox1.Text, glossToSignControl.Lane))
                End If
            Else
                IDs.Add(Tuple.Create(0, glossToSignControl.TextBox1.Text, glossToSignControl.Lane))
            End If
        Next
        Signs = Dictionary.GetGlosstoSign(IDs)


        DialogResult = DialogResult.OK
        Close()
    End Sub
    Private Sub AddGlossControl(ByVal searchString As String)
        Dim glossToSignControl1 = GetGtoSControl(searchString)

        FlowLayoutPanel1.Controls.Add(glossToSignControl1)

    End Sub

    Private Function GetGtoSControl(ByVal searchString As String) As GlossToSignRealTimeControl


        glossToSignRealTimeControl = New GlossToSignRealTimeControl
        AddHandler glossToSignRealTimeControl.MouseDown, AddressOf Me.GlossToSignControlEventHandler
        glossToSignRealTimeControl.TextBox1.Text = searchString
        Dim result = Search(searchString)
         
        CheckMatchingGlossOrFirstItem(result.Item1, searchString)

        glossToSignRealTimeControl.FoundWordDt = result.Item1
        glossToSignRealTimeControl.ResultType = result.Item2

        glossToSignRealTimeControl.ContextMenuStrip = GlossMenuStrip
        ColorControl(glossToSignRealTimeControl, glossToSignRealTimeControl.ResultType)
        AddHandler glossToSignRealTimeControl.CurrentGlossControlChanged, AddressOf glossToSignRealTimeControl_CurrentGlossControlChanged
        AddHandler glossToSignRealTimeControl.SearchTextChanged, AddressOf GlossToSignRealTimeControl_SearchTextChanged
        AddHandler glossToSignRealTimeControl.InsertBefore, AddressOf GlossToSignRealTimeControl_InsertBefore
        AddHandler glossToSignRealTimeControl.InsertAfter, AddressOf GlossToSignRealTimeControl_InsertAfter
        AddHandler glossToSignRealTimeControl.DeleteEntry, AddressOf GlossToSignRealTimeControl_DeleteEntry
        AddHandler glossToSignRealTimeControl.AddFromDict, AddressOf GlossToSignRealTimeControl_AddFromDict
        AddHandler glossToSignRealTimeControl.MoveUp, AddressOf GlossToSignRealTimeControl_MoveUp
        AddHandler glossToSignRealTimeControl.MoveDown, AddressOf GlossToSignRealTimeControl_MoveDown

        glossToSignRealTimeControl.Value = GetId(glossToSignRealTimeControl.FoundWordDt)
        glossToSignRealTimeControl.Image = GetImage(glossToSignRealTimeControl.FoundWordDt)

        Return glossToSignRealTimeControl
    End Function

    Private Sub glossToSignRealTimeControl_CurrentGlossControlChanged(ByVal currentGlossControl As GlossToSignRealTimeControl)
        _currentGlossControl = currentGlossControl
        ColorControl(_currentGlossControl, _currentGlossControl.ResultType)
        GlossToSignDataGridView.DataSource = _currentGlossControl.FoundWordDt

    End Sub


    Private Function Search(ByVal searchString As String) As Tuple(Of SignsbyGlossesUnilingualDataTable, Integer)
        Dim foundWordsDt As SignsbyGlossesUnilingualDataTable = New SignsbyGlossesUnilingualDataTable()

        Dim ta As New SignsbyGlossesUnilingualTableAdapter
        Dim resultType = 2 'Signs matched full word
        If Not searchString = "" Then
            
            'If no exact search found
            If Not searchString.Contains("%") Then
                If foundWordsDt.Rows.Count = 0 Then
                    searchString = searchString & "%"
                    foundWordsDt = ta.GetData(DefaultSignLanguage, FirstGlossLanguage, searchString)
                    resultType = 1 'Signs matched by partial word
                End If
                If foundWordsDt.Rows.Count = 0 Then
                    searchString = "%" & searchString
                    foundWordsDt = ta.GetData(DefaultSignLanguage, FirstGlossLanguage, searchString)
                    resultType = 1 'Signs matched by partial word
                End If

            End If
            If foundWordsDt.Rows.Count = 0 Then 'No signs matched
                resultType = 2
                Dim notfound = searchString.Replace("%", "")

                If Not _glossNotFound.ContainsKey(notfound) Then
                    _glossNotFound.Add(notfound, notfound)
                End If
            End If
            If IfExactMatch(searchString, foundWordsDt.Rows) Then
                resultType = 0
            End If
        End If
        Return Tuple.Create(foundWordsDt, resultType)
    End Function

    Private Function IfExactMatch(ByVal searchString As String, ByVal dataRowCollection As DataRowCollection) As Boolean
        Dim search = searchString.Replace("%", "")
        For Each row As DataRow In dataRowCollection
            Dim glosses = Split(row.Item("glosses1"), ",").Select(Function(x) x.Trim)
            If row.Item("gloss1") = search OrElse glosses.Contains(search) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub ColorControl(ByVal glossToSignContr As GlossToSignRealTimeControl, ByVal resultType As Integer)
        Select Case resultType
            Case 0
                glossToSignContr.BackColor = Color.Green
            Case 1
                glossToSignContr.BackColor = Color.Yellow
            Case Else
                glossToSignContr.BackColor = Color.Red
        End Select
    End Sub

    Private Sub CheckMatchingGlossOrFirstItem(ByVal foundWordsDt As SignsbyGlossesUnilingualDataTable, ByVal searchString As Object)

        Dim selectedColumn As New DataColumn
        selectedColumn.ColumnName = "Selected"
        selectedColumn.DataType = Type.GetType("System.Boolean")

        foundWordsDt.Columns.Add(selectedColumn)
        Dim checked = False
        If foundWordsDt.Rows IsNot Nothing AndAlso foundWordsDt.Rows.Count > 0 Then
            Dim search = searchString.Replace("%", "")
            For Each row As DataRow In foundWordsDt.Rows
                Dim glosses = Split(row.Item("glosses1"), ",").Select(Function(x) x.Trim)
                If row.Item("gloss1") = search OrElse glosses.Contains(search) Then
                    row.Item(selectedColumn) = True
                    checked = True
                    Exit For
                End If
            Next
            If Not checked Then
                Dim row = foundWordsDt.Rows(0)
                row.Item(selectedColumn) = True
            End If
        End If
    End Sub

    Private Sub InsertGlossControl(ByVal searchString As String, ByVal index As Integer)
        Dim glossToSignControl1 = GetGtoSControl(searchString)
        FlowLayoutPanel1.Controls.Add(glossToSignControl1)
        FlowLayoutPanel1.Controls.SetChildIndex(glossToSignControl1, index)
    End Sub
    Private Sub ChangeGlossControl(ByVal searchString As String, ByVal index As Integer)

        Dim glossToSignControl1 = GetGtoSControl(searchString)

        RemoveGlossControl(index)

        FlowLayoutPanel1.Controls.Add(glossToSignControl1)
        FlowLayoutPanel1.Controls.SetChildIndex(glossToSignControl1, index)
    End Sub
    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub InsertGlossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertGlossToolStripMenuItem.Click
        Dim Index As Integer
        If _clickedControl IsNot Nothing Then
            Index = FlowLayoutPanel1.Controls.IndexOf(_clickedControl)
            InsertGlossControl(InputBox("Please enter new gloss to insert", "Insert Gloss"), Index)
        End If
    End Sub
    Private Sub GlossMenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GlossMenuStrip.Opening
        If sender IsNot Nothing AndAlso sender.GetType.ToString = "System.Windows.Forms.ContextMenuStrip" Then

            Dim ContextMenuStrip = CType(sender, ContextMenuStrip)

            _clickedControl = FlowLayoutPanel1.GetChildAtPoint(FlowLayoutPanel1.PointToClient(ContextMenuStrip.Location))
        End If

    End Sub

    Private Sub RemoveGlossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveGlossToolStripMenuItem.Click
        Dim Index As Integer
        If _clickedControl IsNot Nothing Then
            Index = FlowLayoutPanel1.Controls.IndexOf(_clickedControl)
            RemoveGlossControl(Index)
        End If
    End Sub
    Private Sub RemoveGlossControl(ByVal Index As Integer)
        If Not (Index < 0 Or Index > Me.FlowLayoutPanel1.Controls.Count - 1) Then
            FlowLayoutPanel1.Controls.RemoveAt(Index)
        End If
    End Sub

    Private Sub ChangeGlossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeGlossToolStripMenuItem.Click
        Dim Index As Integer
        If _clickedControl IsNot Nothing Then
            Index = FlowLayoutPanel1.Controls.IndexOf(_clickedControl)
            Dim glossToSignControl1 As GlossToSignRealTimeControl = _clickedControl
            Dim previousGloss As String
            If glossToSignControl1 IsNot Nothing Then
                previousGloss = glossToSignControl1.TextBox1.Text
            End If

            ChangeGlossControl(InputBox("Please enter new gloss to insert", "Change Gloss", previousGloss), Index)
        End If
    End Sub

    Private Sub FlowLayoutPanel1_LocationChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles FlowLayoutPanel1.LocationChanged

    End Sub


    Private Sub TBGlossToSign_KeyDown(sender As Object, e As KeyEventArgs) Handles TBGlossToSign.KeyDown
        If e.KeyCode = Keys.Enter Then
            GlossToSign()
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.F AndAlso e.Control Then
            GlossToSign()
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = Keys.A AndAlso e.Control Then

            TBGlossToSign.SelectAll()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub GlossToSignRealTimeControl_SearchTextChanged(ByVal glosstosignrealtimecontrol1 As GlossToSignRealTimeControl, ByVal searchtext As String)
        Dim result = Search(searchtext)
        CheckMatchingGlossOrFirstItem(result.Item1, searchtext)
        glosstosignrealtimecontrol1.FoundWordDt = result.Item1
        glosstosignrealtimecontrol1.ResultType = result.Item2
        ColorControl(glosstosignrealtimecontrol1, result.Item2)
        glosstosignrealtimecontrol1.ContextMenuStrip = GlossMenuStrip
        GlossToSignDataGridView.DataSource = glosstosignrealtimecontrol1.FoundWordDt

        glosstosignrealtimecontrol1.Value = GetId(CType(GlossToSignDataGridView.DataSource, DataTable))
        glosstosignrealtimecontrol1.Image = GetImage(CType(GlossToSignDataGridView.DataSource, DataTable))
    End Sub

    Private Sub GlossToSignDataGridView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.AutoSize = True
        GlossToSignDataGridView.AutoSize = True
        If GlossToSignDataGridView.DataSource IsNot Nothing Then
            GlossToSignDataGridView.Sort(GlossToSignDataGridView.Columns("gloss1"), System.ComponentModel.ListSortDirection.Ascending)
        End If


    End Sub
    Public WriteOnly Property SetBackColor() As Color
        Set(ByVal value As Color)
            GlossToSignDataGridView.BackgroundColor = value
            GlossToSignDataGridView.BackColor = value
        End Set
    End Property

    Private Sub FlowLayoutPanel1_ControlAdded(sender As Object, e As ControlEventArgs) Handles FlowLayoutPanel1.ControlAdded
        FlowLayoutPanel1.SetFlowBreak(e.Control, True)
    End Sub

    Private Sub GlossToSignDataGridView_Validated(sender As Object, e As EventArgs) Handles GlossToSignDataGridView.Validated
        _currentGlossControl.Value = GetId(CType(GlossToSignDataGridView.DataSource, DataTable))
        _currentGlossControl.Image = GetImage(CType(GlossToSignDataGridView.DataSource, DataTable))
    End Sub

    Private Function GetImage(dt As Object) As Image
        Dim image As Image = Nothing
        If dt IsNot Nothing Then
            Dim rows = dt.Select("Selected=True")
            If rows.Length > 0 Then
                image = General.ByteArraytoImage(rows(0).SWriting)

            End If
        End If
        Return image
    End Function

    Private Function GetId(dt As Object) As Integer

        Dim idDictionary = 0
        If dt IsNot Nothing Then
            Dim rows = dt.Select("Selected=True")
            If rows.Length > 0 Then
                idDictionary = rows(0).IDDictionary

            End If
        End If
        Return idDictionary
    End Function

    Private Sub GlossToSignRealTimeControl_MoveDown(ByVal control As GlossToSignRealTimeControl)
        Dim index = FlowLayoutPanel1.Controls.IndexOf(control)
        Dim moveIndex As Integer
        moveIndex = index + 1
        If moveIndex > FlowLayoutPanel1.Controls.Count Then
        Else
            MoveControl(control, moveIndex)
        End If

    End Sub

    Private Sub GlossToSignRealTimeControl_MoveUp(ByVal control As GlossToSignRealTimeControl)
        Dim index = FlowLayoutPanel1.Controls.IndexOf(control)
        Dim moveIndex As Integer
        moveIndex = index - 1
        If moveIndex < 0 Then
            moveIndex = 0
        End If
        MoveControl(control, moveIndex)
    End Sub

    Private Sub MoveControl(ByVal control As GlossToSignRealTimeControl, ByVal index As Integer)      
        FlowLayoutPanel1.Controls.SetChildIndex(control, index)
    End Sub
    Private Sub GlossToSignRealTimeControl_InsertBefore(control As Document.GlossToSignRealTimeControl)
        Dim index = FlowLayoutPanel1.Controls.IndexOf(control)
        Dim insertIndex As Integer
        insertIndex = index
        If insertIndex < 0 Then
            insertIndex = 0
        End If
        InsertGlossControl("", insertIndex)
    End Sub

    Private Sub GlossToSignRealTimeControl_InsertAfter(control As Document.GlossToSignRealTimeControl)
        Dim index = FlowLayoutPanel1.Controls.IndexOf(control)
        InsertGlossControl("", index + 1)
    End Sub

    Private Sub GlossToSignRealTimeControl_DeleteEntry(control As Document.GlossToSignRealTimeControl)
        FlowLayoutPanel1.Controls.Remove(control)
    End Sub

    Private Sub GlossToSignRealTimeControl_AddFromDict(control As Document.GlossToSignRealTimeControl, searchText As String)
        Dim signDict As New SwLayoutControl

        Dim result As Tuple(Of Integer, SwSign) = OpenDictionary(signDict.DocumentSign, searchText)
        If result IsNot Nothing Then

            Dim idDict As Integer = result.Item1
             
            Dim selectedColumn As New DataColumn
            selectedColumn.ColumnName = "Selected"
            selectedColumn.DataType = Type.GetType("System.Boolean")

            Dim dt = Dictionary.GetSignbyId(SettingsPublic.LastDictionaryString, idDict)
            dt.Columns.Add(selectedColumn)
            Dim row1 = dt.Rows(0)
            row1.Item("Selected") = True
            control.TextBox1.Text = row1.Item("gloss1")
            control.FoundWordDt = dt

            control.Value = GetId(control.FoundWordDt)
            control.Image = GetImage(control.FoundWordDt)
            control.ResultType = 0
            ColorControl(control, control.ResultType)
        End If
    End Sub
    Public Function OpenDictionary(documentSign As SwDocumentSign) As Tuple(Of Integer, SwSign)
        Dim idDictionary As Integer
        Dim swDictForm As SWDictForm

        swDictForm = New SWDictForm(New Editor())

        Dim f1 As New Form With {.Name = ToString()}
        swDictForm.CallingForm = f1

        Dim dialogRes As DialogResult = swDictForm.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            idDictionary = swDictForm.IDDictionaryResult
            swDictForm.Dispose()
            If Not idDictionary = 0 Then
                Dim dictionary1 As New SWDict

                Return Tuple.Create(idDictionary, dictionary1.GetSWSign(idDictionary))
            End If
        ElseIf (dialogRes = DialogResult.Cancel) Then
            swDictForm.Dispose()
            Return Nothing
        End If
        Return Nothing
    End Function
    Public Function OpenDictionary(documentSign As SwDocumentSign, searchText As String) As Tuple(Of Integer, SwSign)
        Dim idDictionary As Integer
        Dim swDictForm As SWDictForm

        swDictForm = New SWDictForm(New Editor())

        Dim f1 As New Form With {.Name = ToString()}
        swDictForm.CallingForm = f1
        swDictForm.TBSearch.Text = searchText
        Dim dialogRes As DialogResult = swDictForm.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            idDictionary = swDictForm.IDDictionaryResult
            swDictForm.Dispose()
            If Not idDictionary = 0 Then
                Dim dictionary1 As New SWDict

                Return Tuple.Create(idDictionary, dictionary1.GetSWSign(idDictionary))
            End If
        ElseIf (dialogRes = DialogResult.Cancel) Then
            swDictForm.Dispose()
            Return Nothing
        End If
        Return Nothing
    End Function

    Private Sub GlossToSignDataGridView_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles GlossToSignDataGridView.CellContentClick
        If Not (e.RowIndex = -1) And Not (e.ColumnIndex = -1) Then
            If sender.CurrentCell.OwningColumn.DataPropertyName = "Selected" Then
                For Each row As DataGridViewRow In GlossToSignDataGridView.Rows
                    row.Cells("Selected").Value = False
                Next
            End If
        End If
    End Sub

    Private Sub GlossToSignDataGridView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles GlossToSignDataGridView.CellClick
        Dim sd = CType(sender, DataGridView)

        For Each row In sd.Rows
            row.Cells("Selected").Value = False
        Next

        Dim clickedrow = sd.Rows(e.RowIndex)
        clickedrow.Cells("Selected").Value = True

        sd.EndEdit()
        TBGlossToSign.Focus()
    End Sub
     
End Class
