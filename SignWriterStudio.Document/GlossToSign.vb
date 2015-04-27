Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.Settings
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSet
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters
Imports SignWriterStudio.Settings.SettingsPublic
Public Class GlossToSign
    Dim Dictionary As New SWDict
    Friend Signs As SwCollection(Of SwSign)
    Dim _rightClickDownSender As Object
    Dim _clickedControl As GlossToSignControl
    'Dim Settings As SerializableSettings
    Dim WithEvents GlossToSignControl As New GlossToSignControl
    'Private _glossNotFound As String = ""
    Private _glossNotFound As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Sub GlossToSignControlEventHandler(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles GlossToSignControl.MouseDown

        MsgBox("Received Event.")
    End Sub

    Private Sub GlossToSign_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            Help.ShowHelp(Me, "SignWriterStudio.chm", "glosstosign.htm")
        End If
    End Sub

    Private Sub GlossToSign_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        KeyPreview = True

        Dictionary.FirstGlossLanguage = FirstGlossLanguage
        Dictionary.SecondGlossLanguage = SecondGlossLanguage
        Dictionary.DefaultSignLanguage = DefaultSignLanguage

        FindifTextinClipboard()
    End Sub

    Private Sub FindifTextinClipboard()
        Dim clipboardText = Clipboard.GetText()
        If Not String.IsNullOrEmpty(clipboardText) Then
            TBGlossToSign.Text = clipboardText
            GlossToSign()
        End If
    End Sub

    Private Sub btnGlossToSign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGlossToSign.Click
        GlossToSign()
    End Sub

    Private Sub GlossToSign()
        Try
            FlowLayoutPanel1.Controls.Clear()
            _glossNotFound.Clear()
            Dim glossToSignArray() As String
            'Dim Delimiters() As String = {" ", ",", ".", "?", "¿", "!", Chr(34), vbCrLf}
            Dim delimiters() As String = {" ", Chr(34), vbCrLf}
            Dim textString As String = TBGlossToSign.Text
            textString = textString.Replace("{", "").Replace("}", "").Replace("}", "").Replace("(", "").Replace(")", "")
            textString = textString.Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("¡", " ¡ ").Replace("?", " ? ").Replace("¿", " ¿ ").Replace(":", " : ").Replace(";", " ; ").Replace("   ", " ").Replace("  ", " ")
            glossToSignArray = textString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            glossToSignArray = glossToSignArray.Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso x IsNot Environment.NewLine).ToArray()
            Dim i As Integer
            For i = 0 To glossToSignArray.GetUpperBound(0)
                AddGlossControl(glossToSignArray(i))
            Next

            TBGlossNotFound.Text = String.Join(", ", _glossNotFound.Select(Function(kvp) String.Format("{0}", kvp.Value)).ToArray())
        Catch ex As ArgumentException
            MessageBox.Show("Choose SignWriter Studio Dictionary before continuing")
        End Try

    End Sub

    Private Sub BtnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAccept.Click
        Dim dt As SignsbyGlossesUnilingualDataTable
        Dim rows() As SignsbyGlossesUnilingualRow
        Dim idDictionary As Integer


        Dim IDs = New List(Of Tuple(Of Integer, String))

        For Each control As Control In Me.FlowLayoutPanel1.Controls
            Dim glossToSignControl As GlossToSignControl = CType(control, GlossToSignControl)
            dt = glossToSignControl.GlossToSignDataGridView.DataSource
            rows = dt.Select("Selected=True")
            If rows.Length > 0 Then
                idDictionary = rows(0).IDDictionary
                If Not idDictionary = 0 Then
                    IDs.Add(Tuple.Create(idDictionary, ""))
                Else
                    IDs.Add(Tuple.Create(0, glossToSignControl.LBGloss.Text))
                End If
            Else
                IDs.Add(Tuple.Create(0, glossToSignControl.LBGloss.Text))
            End If
        Next
        Signs = Dictionary.GetGlosstoSign(IDs)


        DialogResult = Windows.Forms.DialogResult.OK
        Close()
    End Sub
    Private Sub AddGlossControl(ByVal searchString As String)
        Dim glossToSignControl1 = GetGtoSControl(searchString)

        FlowLayoutPanel1.Controls.Add(glossToSignControl1)

    End Sub

    Private Function GetGtoSControl(ByVal searchString As String) As GlossToSignControl

        Dim foundWordsDt As New SignsbyGlossesUnilingualDataTable
        GlossToSignControl = New GlossToSignControl
        AddHandler GlossToSignControl.MouseDown, AddressOf Me.GlossToSignControlEventHandler
        GlossToSignControl.LBGloss.Text = searchString
        Dim ta As New SignsbyGlossesUnilingualTableAdapter
        Dim resultType = 2 'Signs matched full word
        If Not searchString = "" Then
            foundWordsDt = ta.GetData(DefaultSignLanguage, FirstGlossLanguage, searchString)
            resultType = 0

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
            Dim selectedColumn As New DataColumn
            selectedColumn.ColumnName = "Selected"
            selectedColumn.DataType = Type.GetType("System.Boolean")

            CheckFirstItem(foundWordsDt, selectedColumn)

            GlossToSignControl.GlossToSignDataGridView.DataSource = foundWordsDt.Copy


        End If
        ColorControl(GlossToSignControl, resultType)
        GlossToSignControl.ContextMenuStrip = GlossMenuStrip
        Return GlossToSignControl
    End Function

    Private Sub ColorControl(ByVal glossToSignContr As GlossToSignControl, ByVal resultType As Integer)
        Select Case resultType
            Case 0
                glossToSignContr.SetBackColor = Color.Green
            Case 1
                glossToSignContr.SetBackColor = Color.Yellow
            Case Else
                glossToSignContr.SetBackColor = Color.Red
        End Select
    End Sub

    Private Sub CheckFirstItem(ByVal foundWordsDt As SignsbyGlossesUnilingualDataTable, ByVal selectedColumn As DataColumn)

        foundWordsDt.Columns.Add(selectedColumn)
        If foundWordsDt.Rows IsNot Nothing AndAlso foundWordsDt.Rows.Count > 0 Then
            Dim row = foundWordsDt.Rows(0)
            row.Item(selectedColumn) = True
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
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub InsertGlossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InsertGlossToolStripMenuItem.Click
        Dim Index As Integer
        If _clickedControl IsNot Nothing Then
            Index = FlowLayoutPanel1.Controls.IndexOf(_clickedControl)
            InsertGlossControl(InputBox("Please enter new gloss to insert", "Insert Gloss"), Index)
        End If
    End Sub
    Private Sub GlossMenuStrip_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles GlossMenuStrip.Opening
        Dim ContextMenuStrip As ContextMenuStrip
        If sender IsNot Nothing AndAlso sender.GetType.ToString = "System.Windows.Forms.ContextMenuStrip" Then

            ContextMenuStrip = CType(sender, ContextMenuStrip)
            'FlowLayoutPanel1.Controls.Add()
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
            Me.FlowLayoutPanel1.Controls.RemoveAt(Index)
        End If
    End Sub

    Private Sub ChangeGlossToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChangeGlossToolStripMenuItem.Click
        Dim Index As Integer
        If _clickedControl IsNot Nothing Then
            Index = FlowLayoutPanel1.Controls.IndexOf(_clickedControl)
            Dim glossToSignControl1 As GlossToSignControl = _clickedControl
            Dim previousGloss As String
            If glossToSignControl1 IsNot Nothing Then
                previousGloss = glossToSignControl1.LBGloss.Text
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

End Class
