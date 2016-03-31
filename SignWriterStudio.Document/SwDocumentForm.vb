Imports System.Drawing.Printing
Imports System.ComponentModel
Imports System.IO
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.Settings
Imports SignWriterStudio.General
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json
Imports SignWriterStudio.Dictionary
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.SWEditor
Imports SPML
Imports System.Text

Public NotInheritable Class SwDocumentForm
    Private _documentValue As New SwDocument

    Friend Property Document() As SwDocument
        Get
            Return _documentValue
        End Get
        Set(value As SwDocument)
            _documentValue = value
        End Set
    End Property

    Public ReadOnly Property RightClickDownSender() As Control
        Get
            Return Document.MySWFlowLayoutPanel.RightClickedControl
        End Get
    End Property

    Public LeftClickDownSender As Object

    Private _documentSettings As DocumentSettings
    Private _swLayoutControlProperties As New SWLayoutControlProperties
    Private _documentChanged As Boolean = False
    Private _documentFilename As String
    Private _filename As String
    Private ReadOnly _swEditor As New Editor
    Private _swDictForm As SWDictForm
    Private _imageEditor As ImageEditor.ImageEditor

    Private Sub PasteFswDocument(fsw As String)


        Const signLanguage As Integer = 4
        Const glossLanguage As Integer = 54

        Dim signs = SpmlConverter.FswtoSwDocumentSigns(fsw, signLanguage, glossLanguage)
        Document.MySWFlowLayoutPanel.SuspendLayout()
        For Each sign As SwDocumentSign In signs
            Document.AddSWSign(sign)
        Next
        Document.MySWFlowLayoutPanel.ResumeLayout()
    End Sub

    Property DocumentFilename() As String
        Get
            Return _documentFilename
        End Get
        Set(ByVal value As String)
            If Not _documentFilename = value Then
                _documentFilename = value
                _filename = Path.GetFileName(_documentFilename)
                ShowCaption()
            End If
        End Set
    End Property

    Property DocumentChanged() As String
        Get
            Return _documentChanged
        End Get
        Set(ByVal value As String)
            If Not _documentChanged = value Then
                _documentChanged = value
                ShowCaption()
            End If
        End Set
    End Property

    Private Sub ShowCaption()
        If Not _filename = String.Empty Then
            Text = "SignWriter Studio™ Document - " & _filename
        Else
            Text = "SignWriter Studio™ Document"
        End If

        If DocumentChanged Then
            Text = Text & " *"
        End If
    End Sub

    Private Sub CheckifNeedsSaving()
        If DocumentChanged Then
            Dim result As DialogResult = MessageBox.Show("Do you want to save SignWriter Studio Document?", "Save?",
                                                         MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                If File.Exists(DocumentFilename) Then
                    SaveCurrentDocument()
                Else
                    SaveDocumentFileDialog.ShowDialog()
                End If
            End If
        End If
    End Sub

    Private Sub SWDocumentForm_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) _
        Handles Me.FormClosing
        CheckifNeedsSaving()
        SettingsPublic.LastDocumentString = DocumentFilename

    End Sub

    Private Sub SWDocumentform_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        LoadForm()
    End Sub

    Private Sub LoadForm()
        LoadSettings()
        OpenDocumentFileDialog.Filter = "SignWriter Studio™ Document (*.SWSDoc)|*.SWSDoc|All files (*.*)|*.*"
        OpenDocumentFileDialog.FilterIndex = 1
        OpenDocumentFileDialog.RestoreDirectory = True

        SaveDocumentFileDialog.Filter = "SignWriter Studio™ Document (*.SWSDoc)|*.SWSDoc|All files (*.*)|*.*"
        SaveDocumentFileDialog.FilterIndex = 1
        SaveDocumentFileDialog.RestoreDirectory = True

        DocumentSetup()
        OpenLastDocument()
    End Sub

    Private Sub DocumentSetup()
        _documentChanged = False

        Document.MySWControlMenuStrip = PictBoxContextMenuStrip
        Document.MySWFlowLayoutPanel = SwFlowLayoutPanel1

        SwFlowLayoutPanel1.MySWDocument = Document
        Document.LayoutEngineSettings = Document.MySWFlowLayoutPanel.LayoutEng.LayoutEngineSettings
        Document.MyForm = Me
        SwFlowLayoutPanel1.BackColor = Document.LayoutEngineSettings.BackgroundColor



    End Sub

    Private Sub OpenLastDocument()
        Dim lastDocument = SettingsPublic.LastDocumentString

        If File.Exists(lastDocument) Then
            OpenDocument(lastDocument)
        End If
    End Sub

    Private Sub FindSenderOnMouseDown(ByVal sender As Object,
                                      ByVal e As MouseEventArgs) Handles Me.MouseDown

        If e.Button = MouseButtons.Right Then

            If RightClickDownSender IsNot Nothing AndAlso RightClickDownSender.GetType.Name.Contains("LayoutControl") _
                Then
                Dim docSign As SwDocumentSign = CType(RightClickDownSender, SwLayoutControl).DocumentSign
                BeginningOfColumnToolStripMenuItem.Checked = docSign.BegColumn
            End If
            LeftClickDownSender = Nothing
        Else
            LeftClickDownSender = sender
            SwFlowLayoutPanel1.DoDragDrop(SwFlowLayoutPanel1.Name, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub MoveUpToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MoveUpToolStripMenuItem.Click
        MoveUp()
    End Sub

    Private Sub MoveUp()
        If RightClickDownSender IsNot Nothing Then
            Dim previousIndex As Integer = SwFlowLayoutPanel1.Controls.GetChildIndex(RightClickDownSender)
            Dim newIndex As Integer = previousIndex - 1
            If _
                newIndex >= 0 AndAlso newIndex <= SwFlowLayoutPanel1.Controls.Count - 1 AndAlso previousIndex >= 0 AndAlso
                previousIndex <= SwFlowLayoutPanel1.Controls.Count - 1 Then

                Dim item As SwDocumentSign = CType(RightClickDownSender, SwLayoutControl).DocumentSign
                SwFlowLayoutPanel1.Controls.SetChildIndex(RightClickDownSender, newIndex)
                Document.DocumentSigns.RemoveAt(previousIndex)
                Document.DocumentSigns.Insert(newIndex, item)
            End If
        End If
        DocumentChanged = True
    End Sub

    Private Sub MoveDownToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles MoveDownToolStripMenuItem.Click
        MoveDown()
    End Sub

    Private Sub MoveDown()
        If RightClickDownSender IsNot Nothing Then
            Dim previousIndex As Integer = SwFlowLayoutPanel1.Controls.GetChildIndex(RightClickDownSender)
            Dim newIndex As Integer = previousIndex + 1
            If _
                newIndex >= 0 AndAlso newIndex <= SwFlowLayoutPanel1.Controls.Count - 1 AndAlso previousIndex >= 0 AndAlso
                previousIndex <= SwFlowLayoutPanel1.Controls.Count - 1 Then
                Dim item As SwDocumentSign = CType(RightClickDownSender, SwLayoutControl).DocumentSign

                SwFlowLayoutPanel1.Controls.SetChildIndex(RightClickDownSender, newIndex)
                Document.DocumentSigns.RemoveAt(previousIndex)
                Document.DocumentSigns.Insert(newIndex, item)
            End If
        End If
        DocumentChanged = True
    End Sub


    Private Sub LeftToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles LeftToolStripMenuItem.Click
        MoveLaneLeft()
    End Sub

    Private Sub MoveLaneLeft()
        Dim selectSwLayoutControl As SwLayoutControl = RightClickDownSender
        If RightClickDownSender IsNot Nothing And Not selectSwLayoutControl.DocumentSign.IsPunctuation() Then
            selectSwLayoutControl.DocumentSign.Lane = AnchorStyles.Left
        End If
        SwFlowLayoutPanel1.PerformLayout()
        DocumentChanged = True
    End Sub

    Private Sub CenterToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles CenterToolStripMenuItem.Click
        MoveLaneCenter()
    End Sub

    Private Sub MoveLaneCenter()
        Dim selectSwLayoutControl As SwLayoutControl = RightClickDownSender
        If selectSwLayoutControl IsNot Nothing Then
            selectSwLayoutControl.DocumentSign.Lane = AnchorStyles.None
        End If
        SwFlowLayoutPanel1.PerformLayout()
        DocumentChanged = True
    End Sub

    Private Sub RightToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles RightToolStripMenuItem.Click
        MoveLaneRight()
        DocumentChanged = True
    End Sub

    Private Sub MoveLaneRight()
        Dim selectSwLayoutControl As SwLayoutControl = RightClickDownSender

        If selectSwLayoutControl IsNot Nothing And Not selectSwLayoutControl.DocumentSign.IsPunctuation() Then
            selectSwLayoutControl.DocumentSign.Lane = AnchorStyles.Right
        End If
        SwFlowLayoutPanel1.PerformLayout()
        DocumentChanged = True
    End Sub

    Private Sub RemoveSignToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles RemoveSignToolStripMenuItem.Click
        RemoveSign()
    End Sub

    Private Sub RemoveSign()
        If RightClickDownSender IsNot Nothing Then
            Document.RemoveSWSign(CType(RightClickDownSender, SwLayoutControl))

        End If
        DocumentChanged = True
    End Sub

    Private Sub EditSignInEditorToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles EditSignInEditorToolStripMenuItem.Click
        If _
            RightClickDownSender IsNot Nothing AndAlso
            RightClickDownSender.GetType.ToString = "SignWriterStudio.SWClasses.SwLayoutControl" Then
            EditinSwEditor(CType(RightClickDownSender, SwLayoutControl))
        End If
        DocumentChanged = True
    End Sub

    Private Sub AddSignFromDictionary()
        Dim signDict As New SwLayoutControl

        Dim temp As SwDocumentSign = OpenDictionary(signDict.DocumentSign)
        If temp IsNot Nothing Then
            SwFlowLayoutPanel1.Controls.Add(signDict)
            SwFlowLayoutPanel1.Controls.Item(SwFlowLayoutPanel1.Controls.Count - 1).ContextMenuStrip =
                PictBoxContextMenuStrip

            signDict.DocumentSign = temp

            signDict.Refresh()

            Refresh()
            Document.DocumentSigns.Add(
                CType(SwFlowLayoutPanel1.Controls.Item(SwFlowLayoutPanel1.Controls.Count - 1), SwLayoutControl).DocumentSign)
            Document.MySWFlowLayoutPanel.Controls.Item(Document.MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip =
                Document.MySWControlMenuStrip
            DocumentChanged = True
        End If
    End Sub
    Private Sub SWDocument_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Resize
        'Me.SWFlowLayoutPanel1.Size = Size
        'DocumentChanged = True
    End Sub

    Private ReadOnly _swDocumentPrintPages As New SWPrintPages
    Private ReadOnly _printSwFlowLayoutPanel As New SwFlowLayoutPanel
    Private _intPrintAreaHeight, _intPrintAreaWidth, _marginLeft, _marginTop As Int32


    Private Sub SetupPage(ByVal pagetoPrint As Integer)
        _printSwFlowLayoutPanel.SuspendLayout()

        Dim firstControl As Integer = _swDocumentPrintPages.PrintPages(pagetoPrint).FromControl
        Dim lastControl As Integer = _swDocumentPrintPages.PrintPages(pagetoPrint).ToControl
        For I As Integer = 0 To _printSwFlowLayoutPanel.Controls.Count - 1
            If I >= firstControl And I <= lastControl Then
                _printSwFlowLayoutPanel.Controls(I).Visible = True
            Else
                _printSwFlowLayoutPanel.Controls(I).Visible = False
            End If
        Next
        _printSwFlowLayoutPanel.ResumeLayout()
    End Sub

    Private Sub PrintSWDocument_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs) _
        Handles PrintSWDocument.PrintPage
        Dim g As Graphics = e.Graphics
        Dim fromControl As Integer
        Dim toControl As Integer

        'Select only what can fit on one page at a time.  
        'Divide by lines
        _printSwFlowLayoutPanel.Size = New Size(2000, 2000)
        'SWDocumentPrintPages.PrintSize = New Size(600, 600)
        SetupPage(_swDocumentPrintPages.LastPrintedPage + 1)
        fromControl = _swDocumentPrintPages.PrintPages(_swDocumentPrintPages.LastPrintedPage + 1).FromControl
        toControl = _swDocumentPrintPages.PrintPages(_swDocumentPrintPages.LastPrintedPage + 1).ToControl
        'Dim bm As New Bitmap(PrintSwFlowLayoutPanel.DisplayRectangle.Width, PrintSwFlowLayoutPanel.DisplayRectangle.Height, CreateGraphics)
        _printSwFlowLayoutPanel.Invalidate()
        _printSwFlowLayoutPanel.PerformLayout()

        'Clipboard.SetImage(bm)
        'Add one pages items then next ect
        _printSwFlowLayoutPanel.SuspendLayout()
        _printSwFlowLayoutPanel.Controls.Clear()
        Dim item1 As SwLayoutControl
        Dim cntl As SwLayoutControl
        For I As Integer = 0 To SwFlowLayoutPanel1.Controls.Count - 1
            If I >= fromControl And I <= toControl Then
                item1 = SwFlowLayoutPanel1.Controls(I)
                cntl = item1.Clone
                cntl.SwFlowLayoutPanel1 = _printSwFlowLayoutPanel
                cntl.Refresh()
                _printSwFlowLayoutPanel.Controls.Add(cntl)
            End If
        Next


        _printSwFlowLayoutPanel.ResumeLayout()
        For Each item As Control In _printSwFlowLayoutPanel.Controls
            Dim bm1 As New Bitmap(item.Width, item.Height, CreateGraphics)
            item.DrawToBitmap(bm1, item.DisplayRectangle)
            Clipboard.SetImage(bm1)
            g.DrawImageUnscaled(bm1, item.Location)
        Next

        If _swDocumentPrintPages.LastPrintedPage + 1 < _swDocumentPrintPages.TotalPages Then
            e.HasMorePages = True
            _swDocumentPrintPages.LastPrintedPage += 1
        Else
            e.HasMorePages = False
        End If
        'Next Page
        Visible = True
    End Sub


    Private Sub ClearDocument()
        CheckifNeedsSaving()
        SwFlowLayoutPanel1.Controls.Clear()
        Document = New SwDocument
        DocumentSetup()
        DocumentFilename = String.Empty
        _documentFilename = String.Empty
        DocumentChanged = False
        ShowCaption()
        SaveToolStripMenuItem2.Enabled = False
    End Sub

    Private Sub PhotofromDictionary()
        Dim newSwPictureBox As New SwLayoutControl


        SwFlowLayoutPanel1.Controls.Add(newSwPictureBox)
        SwFlowLayoutPanel1.Controls.Item(SwFlowLayoutPanel1.Controls.Count - 1).ContextMenuStrip =
            PictBoxContextMenuStrip
        newSwPictureBox.DocumentSign.IsSign = False
        newSwPictureBox.DocumentSign.DocumentImage = GetDictionaryPhoto()
        Document.DocumentSigns.Add(newSwPictureBox.DocumentSign)

        newSwPictureBox.Refresh()
        Refresh()

        DocumentChanged = True
    End Sub

    Private Sub SignPhotofromDictionary()
        Dim newSwPictureBox As New SwLayoutControl


        SwFlowLayoutPanel1.Controls.Add(newSwPictureBox)
        SwFlowLayoutPanel1.Controls.Item(SwFlowLayoutPanel1.Controls.Count - 1).ContextMenuStrip =
            PictBoxContextMenuStrip
        newSwPictureBox.DocumentSign.IsSign = False
        newSwPictureBox.DocumentSign.DocumentImage = GetDictionarySignPhoto()
        Document.DocumentSigns.Add(newSwPictureBox.DocumentSign)

        newSwPictureBox.Refresh()
        Refresh()
        DocumentChanged = True
    End Sub

    Private Sub PropertiesToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PropertiesToolStripMenuItem.Click
        SignProperties()
    End Sub

    Private Function SignProperties()
        Dim layoutControl As SwLayoutControl
        Dim dialogRes As DialogResult
        If _
            RightClickDownSender IsNot Nothing AndAlso
            RightClickDownSender.GetType.ToString = "SignWriterStudio.SWClasses.SwLayoutControl" Then
            layoutControl = (CType(RightClickDownSender, SwLayoutControl))
            If _swLayoutControlProperties Is Nothing OrElse _swLayoutControlProperties.IsDisposed Then
                _swLayoutControlProperties = New SWLayoutControlProperties
            End If
            _swLayoutControlProperties.DocumentSign = layoutControl.DocumentSign

            dialogRes = _swLayoutControlProperties.ShowDialog()
            If (dialogRes = DialogResult.OK) Then
                _swLayoutControlProperties.Dispose()
                layoutControl.Refresh()
            ElseIf (dialogRes = DialogResult.Cancel) Then
                _swLayoutControlProperties.Dispose()
            End If
        End If
        DocumentChanged = True
        Return dialogRes
    End Function

    Private Sub SwFlowLayoutPanel1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) _
        Handles SwFlowLayoutPanel1.DragDrop
        'Moving a Sign
        If e.Data.GetData(GetType(String)) = SwFlowLayoutPanel1.Name Then
            If RightClickDownSender IsNot Nothing AndAlso RightClickDownSender.GetType.Name.Contains("SwLayoutControl") _
                Then
                Dim previousIndex As Integer = SwFlowLayoutPanel1.Controls.GetChildIndex(RightClickDownSender)
                Dim newIndex As Integer = SwFlowLayoutPanel1.Controls.GetChildIndex(sender)
                If _
                    newIndex >= 0 AndAlso newIndex <= SwFlowLayoutPanel1.Controls.Count - 1 AndAlso previousIndex >= 0 AndAlso
                    previousIndex <= SwFlowLayoutPanel1.Controls.Count - 1 Then
                    Dim item As SwDocumentSign = CType(RightClickDownSender, SwLayoutControl).DocumentSign
                    SwFlowLayoutPanel1.Controls.SetChildIndex(RightClickDownSender, newIndex)
                    Document.DocumentSigns.RemoveAt(previousIndex)
                    Document.DocumentSigns.Insert(newIndex, item)
                End If
            End If
        End If
        DocumentChanged = True
    End Sub

    'Private Sub Gloss2Sign()
    '    Dim glossToSign As New GlossToSign
    '    Dim dialogRes As DialogResult = glossToSign.ShowDialog()

    '    If (dialogRes = DialogResult.OK) Then
    '        SignstoDocument(glossToSign.Signs)
    '        glossToSign.Dispose()
    '        DocumentChanged = True
    '    ElseIf (dialogRes = DialogResult.Cancel) Then
    '        glossToSign.Dispose()
    '    End If
    'End Sub

    Private Sub Gloss2SignRealTime()
        Dim glossToSignRealTime As New GlossToSignRealTime
        Dim dialogRes As DialogResult = glossToSignRealTime.ShowDialog()

        If (dialogRes = DialogResult.OK) Then
            SignstoDocument(glossToSignRealTime.Signs)
            glossToSignRealTime.Dispose()
            DocumentChanged = True
        ElseIf (dialogRes = DialogResult.Cancel) Then
            glossToSignRealTime.Dispose()
        End If
    End Sub

    Private Sub SaveDocumentFileDialog_FileOk(ByVal sender As Object, ByVal e As CancelEventArgs) _
        Handles SaveDocumentFileDialog.FileOk
        SaveDocument(SaveDocumentFileDialog.FileName)
    End Sub

    Private Sub SaveDocument(filename As String)

        Dim serializer = New JsonSerializer()
        serializer.Converters.Add(New JavaScriptDateTimeConverter())
        serializer.NullValueHandling = NullValueHandling.Ignore
        DocumentFilename = filename
        Using sw = New StreamWriter(DocumentFilename)
            Using writer = New JsonTextWriter(sw)

                serializer.Serialize(writer, Document)

                DocumentChanged = False

                SaveToolStripMenuItem2.Enabled = True
            End Using
        End Using
    End Sub

    Private Sub SaveCurrentDocument()
        SaveDocument(DocumentFilename)
    End Sub

    'Protected Property Id() As Integer
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(ByVal value As Integer)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    'Protected Property Line2() As String
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(ByVal value As String)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    'Protected Property Line1() As String
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(ByVal value As String)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

    Private Sub OpenDocumentFileDialog_FileOk(ByVal sender As Object, ByVal e As CancelEventArgs) _
        Handles OpenDocumentFileDialog.FileOk
        OpenDocument(OpenDocumentFileDialog.FileName)
    End Sub

    Public Sub OpenDocument(ByVal filename As String)
        Try

            Dim serializer = New JsonSerializer()
            serializer.Converters.Add(New JavaScriptDateTimeConverter())
            serializer.NullValueHandling = NullValueHandling.Ignore

            DocumentFilename = filename
            Using sw = New StreamReader(DocumentFilename)
                Using reader = New JsonTextReader(sw)


                    Document = serializer.Deserialize(Of SwDocument)(reader)
                    RemoveFirstFrame(Document)

                    DocumentChanged = False


                    SaveToolStripMenuItem2.Enabled = True
                End Using
            End Using


            DocumentSetup()

            Document.Refresh()

        Catch ex As Exception
            MessageBox.Show("Cannot open document file.")

            LogError(ex, "Exception ")
        End Try
    End Sub

    Private Shared Sub RemoveFirstFrame(ByVal swDocument As SwDocument)
        For Each sign In swDocument.DocumentSigns
            sign.Frames.RemoveAt(0)
        Next
    End Sub

    Private Sub SaveToDictionaryToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles SaveToDictionaryToolStripMenuItem.Click
        SaveToDict()
    End Sub

    Private Sub SaveToDict()
        If _
            RightClickDownSender IsNot Nothing AndAlso
            RightClickDownSender.GetType.ToString = "SignWriterStudio.SWClasses.SwLayoutControl" Then


            Dim dictionary As New SWDict
            Dim dialogRes As DialogResult
            Dim layoutControl As SwLayoutControl = CType(RightClickDownSender, SwLayoutControl)
            If _
                String.IsNullOrEmpty(layoutControl.DocumentSign.LanguageIso) OrElse
                String.IsNullOrEmpty(layoutControl.DocumentSign.SignLanguageIso) OrElse
                String.IsNullOrEmpty(layoutControl.DocumentSign.Gloss) Then
                dialogRes = SignProperties()
            End If
            If Not dialogRes = DialogResult.Cancel Then
                dictionary.SaveSWSign(layoutControl.DocumentSign)
                DocumentChanged = True
            End If
        End If
    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles CopyToolStripMenuItem.Click
        CopySign()
    End Sub

    Private Sub CopySign()
        If RightClickDownSender IsNot Nothing AndAlso
            RightClickDownSender.GetType.ToString = "SignWriterStudio.SWClasses.SwLayoutControl" Then

            Dim layoutControl As SwLayoutControl = CType(RightClickDownSender, SwLayoutControl)
            If layoutControl.DocumentSign.IsSign Then
                Dim sign = CType(layoutControl.DocumentSign, SwSign)
                sign.SetClipboard()
            Else
                Clipboard.SetImage(layoutControl.Image)
            End If
        End If
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PasteToolStripMenuItem.Click
        PasteSign()
    End Sub

    Private Sub PasteSign()
        Dim layoutControl As SwLayoutControl = CType(RightClickDownSender, SwLayoutControl)
        Dim instertAt As Integer

        If RightClickDownSender IsNot Nothing AndAlso RightClickDownSender.GetType.Name.Contains("LayoutControl") Then
            instertAt = SwFlowLayoutPanel1.Controls.GetChildIndex(layoutControl) + 1
        Else
            instertAt = 0
        End If

        If Clipboard.ContainsText Then
            Try
                Dim sign = DeSerializeJson(Of SwSign)(Clipboard.GetText)
                If sign IsNot Nothing Then
                    sign.Frames.RemoveAt(0)
                    Document.AddSWSign(sign, instertAt)
                End If
                DocumentChanged = True
            Catch ex As ArgumentException
                'Swallow is not json
            End Try

        ElseIf Clipboard.ContainsImage Then
            Dim newSign As New SwDocumentSign
            newSign.IsSign = False
            newSign.DocumentImage = Clipboard.GetImage()
            Document.AddSWSign(newSign, instertAt)
            DocumentChanged = True
        End If


    End Sub
    Private Function DeSerializeJson(Of T)(ByVal json As String) As T
        Dim serializer = New JsonSerializer()
        serializer.Converters.Add(New JavaScriptDateTimeConverter())
        serializer.NullValueHandling = NullValueHandling.Ignore
        Dim obj As T = Nothing
        Try
            obj = JsonConvert.DeserializeObject(Of T)(json, New JavaScriptDateTimeConverter())
        Catch ex As Exception
            Throw (New ArgumentException("Could not deserialize object." & ex.Message, ex))
        End Try

        Return obj
    End Function
    Private Sub BeginningOfColumnToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles BeginningOfColumnToolStripMenuItem.Click
        BeginningofColumn()
    End Sub

    Private Sub BeginningofColumn()

        Dim docSign As SwDocumentSign
        Dim layoutControl As SwLayoutControl
        If RightClickDownSender IsNot Nothing AndAlso RightClickDownSender.GetType.Name.Contains("LayoutControl") Then

            layoutControl = CType(RightClickDownSender, SwLayoutControl)
            docSign = layoutControl.DocumentSign
            docSign.BegColumn = Not docSign.BegColumn
            layoutControl.Refresh()
            BeginningOfColumnToolStripMenuItem.Checked = docSign.BegColumn

            SwFlowLayoutPanel1.Refresh()
            SwFlowLayoutPanel1.PerformLayout()
            DocumentChanged = True
        End If
    End Sub

    Private Sub PasteToolStripMenuItem2_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PasteToolStripMenuItem2.Click
        PasteSign()
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles NewToolStripMenuItem.Click
        ClearDocument()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles OpenToolStripMenuItem.Click
        CheckifNeedsSaving()

        OpenDocumentFileDialog.ShowDialog()
    End Sub

    Private Sub SaveToolStripMenuItem2_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles SaveToolStripMenuItem2.Click


        SaveCurrentDocument()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles SaveAsToolStripMenuItem.Click
        SaveDocumentFileDialog.ShowDialog()
    End Sub

    Private Sub PrintToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PrintToolStripMenuItem1.Click
        TemporarilyDisabled()
        'Print()
    End Sub

    Private Sub PrintPreviewToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PrintPreviewToolStripMenuItem1.Click
        TemporarilyDisabled()
        'PrintPreview()
    End Sub

    Private Sub TemporarilyDisabled()
        MessageBox.Show("This option has been temporarily disabled because it is not yet fully functional.")
    End Sub

    Private Sub PrintSetupToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PrintSetupToolStripMenuItem.Click
        TemporarilyDisabled()
        'PrintSetup()
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles SettingsToolStripMenuItem.Click
        _documentSettings.ShowDialog()
        DocumentChanged = True
    End Sub

    Public Sub EditImage(ByVal layoutControl As SwLayoutControl)
        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000871 begin
        If _
            layoutControl IsNot Nothing AndAlso layoutControl.DocumentSign IsNot Nothing AndAlso
            Not layoutControl.DocumentSign.IsSign Then
            _imageEditor.Image = layoutControl.DocumentSign.DocumentImage
            Dim dialogRes As DialogResult = _imageEditor.ShowDialog()
            If (dialogRes = DialogResult.OK) Then
                layoutControl.DocumentSign.DocumentImage = _imageEditor.Image
                layoutControl.Refresh()
            ElseIf (dialogRes = DialogResult.Cancel) Then
            End If
        Else
            MessageBox.Show("This is not an image")
        End If
        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000871 end
    End Sub


    Friend Sub SignstoDocument(ByVal signs As List(Of Tuple(Of SwSign, Integer)))
        Show()
        SwFlowLayoutPanel1.SuspendLayout()
        For Each sign In signs

            Document.AddSWSignLane(sign.Item1, sign.Item2)
        Next
        SwFlowLayoutPanel1.ResumeLayout()
    End Sub

    Private _swFlowLayoutPanel As SwFlowLayoutPanel = SwFlowLayoutPanel1

    Public Property SwFlowLayoutPanel2() As SwFlowLayoutPanel
        Get
            Return _swFlowLayoutPanel
        End Get
        Set(ByVal value As SwFlowLayoutPanel)
            _swFlowLayoutPanel = value
        End Set
    End Property


    Public Function OpenImageEditor(image As Image) As Image
        'Check first if open 
        If _imageEditor Is Nothing OrElse _imageEditor.IsDisposed Then
            _imageEditor = New ImageEditor.ImageEditor
        End If
        If _imageEditor.IsHandleCreated = True Then
            _imageEditor.Image = Nothing
        End If
        _imageEditor.Image = image

        Dim dialogRes As DialogResult = _imageEditor.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            image = _imageEditor.Image
            Return image
        ElseIf (dialogRes = DialogResult.Cancel) Then

        End If
        Return Nothing
    End Function

    Public Function OpenSwEditor(documentSign As SwDocumentSign) As SwDocumentSign
        'Check first if open 

        If _swEditor.IsHandleCreated = True Then
            _swEditor.ClearAll()
        End If
        _swEditor.Sign = CType(documentSign, SwSign)


        Dim dialogRes As DialogResult = _swEditor.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            documentSign = DirectCast(_swEditor.Sign, SwDocumentSign)
        ElseIf (dialogRes = DialogResult.Cancel) Then
        End If
        Return documentSign
    End Function

    Public Function OpenDictionary(documentSign As SwDocumentSign) As SwDocumentSign
        Dim idDictionary As Integer
        If _swDictForm Is Nothing OrElse _swDictForm.IsDisposed Then
            _swDictForm = New SWDictForm(_swEditor)
        End If
        Dim f1 As New Form With {.Name = ToString()}
        _swDictForm.CallingForm = f1

        Dim dialogRes As DialogResult = _swDictForm.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            idDictionary = _swDictForm.IDDictionaryResult
            _swDictForm.Dispose()
            If Not idDictionary = 0 Then
                Dim dictionary1 As New SWDict
                documentSign.IncorporateSWSign(dictionary1.GetSWSign(idDictionary))
                Return documentSign
            End If
        ElseIf (dialogRes = DialogResult.Cancel) Then
            _swDictForm.Dispose()
            Exit Function
        End If
        Return Nothing
    End Function

    Public Function GetDictionaryPhoto() As Image
        Dim idDictionary As Integer
        If _swDictForm Is Nothing OrElse _swDictForm.IsDisposed Then
            _swDictForm = New SWDictForm(_swEditor)
        End If
        'TODO check if CallingForm is still needed
        Dim f1 As New Form With {.Name = ToString()}
        _swDictForm.CallingForm = f1

        Dim dialogRes As DialogResult = _swDictForm.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            idDictionary = _swDictForm.IDDictionaryResult
            _swDictForm.Dispose()
            If Not idDictionary = 0 Then
                Dim dictionary1 As New SWDict
                Return dictionary1.GetPhoto(idDictionary)
            End If
        ElseIf (dialogRes = DialogResult.Cancel) Then
            _swDictForm.Dispose()
            Exit Function
        End If
        Return Nothing
    End Function

    Public Function GetDictionarySignPhoto() As Image
        Dim idDictionary As Integer
        If _swDictForm Is Nothing OrElse _swDictForm.IsDisposed Then
            _swDictForm = New SWDictForm(_swEditor)
        End If
        'TODO check if CallingForm is still needed
        Dim f1 As New Form With {.Name = ToString()}
        _swDictForm.CallingForm = f1

        Dim dialogRes As DialogResult = _swDictForm.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            idDictionary = _swDictForm.IDDictionaryResult
            _swDictForm.Dispose()
            If Not idDictionary = 0 Then
                Dim dictionary1 As New SWDict
                Return dictionary1.GetSignPhoto(idDictionary)
            End If
        ElseIf (dialogRes = DialogResult.Cancel) Then
            _swDictForm.Dispose()
            Exit Function
        End If
        Return Nothing
    End Function

    Private Sub SWLayoutControl_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragDrop
        'Moving a Sign

        If e.Data.GetData(GetType(String)) = SwFlowLayoutPanel1.Name Then
            If LeftClickDownSender IsNot Nothing AndAlso LeftClickDownSender.GetType.Name.Contains("SwLayoutControl") _
                Then
                Dim previousIndex As Integer = SwFlowLayoutPanel1.Controls.GetChildIndex(LeftClickDownSender)
                Dim newIndex As Integer = SwFlowLayoutPanel1.Controls.GetChildIndex(sender)
                If _
                    newIndex >= 0 AndAlso newIndex <= SwFlowLayoutPanel1.Controls.Count - 1 AndAlso previousIndex >= 0 AndAlso
                    previousIndex <= SwFlowLayoutPanel1.Controls.Count - 1 Then
                    Dim item As SwDocumentSign = CType(LeftClickDownSender, SwLayoutControl).DocumentSign
                    SwFlowLayoutPanel1.Controls.SetChildIndex(LeftClickDownSender, newIndex)
                    Document.DocumentSigns.RemoveAt(previousIndex)
                    Document.DocumentSigns.Insert(newIndex, item)
                End If
            End If
        End If
    End Sub

    Public Sub AddSwSignFromSwEditor()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        Dim documentSign As New SwDocumentSign
        documentSign.SetlanguageIso(SettingsPublic.FirstGlossLanguage)
        documentSign.SetSignLanguageIso(SettingsPublic.DefaultSignLanguage)

        Dim layoutControl = New SwLayoutControl
        layoutControl.DocumentSign = documentSign
        Document.MySWFlowLayoutPanel.Controls.Add(layoutControl)
        Document.MySWFlowLayoutPanel.Controls.Item(Document.MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip =
            Document.MySWControlMenuStrip

        layoutControl.DocumentSign = OpenSwEditor(layoutControl.DocumentSign)

        Document.DocumentSigns.Add(layoutControl.DocumentSign)

        layoutControl.Refresh()
        Refresh()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub

    Public Sub AddImageFromImageEditor()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000906 begin
        If _imageEditor Is Nothing OrElse _imageEditor.IsDisposed Then
            _imageEditor = New ImageEditor.ImageEditor
        End If
        Dim layoutControl As New SwLayoutControl
        Dim documentSign As New SwDocumentSign
        documentSign.IsSign = False
        layoutControl.DocumentSign = documentSign

        Dim dialogRes As DialogResult = _imageEditor.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            layoutControl.DocumentSign = documentSign
            layoutControl.DocumentSign.DocumentImage = _imageEditor.Image

            layoutControl.Refresh()
            Refresh()
            Document.MySWFlowLayoutPanel.Controls.Add(layoutControl)
            Document.MySWFlowLayoutPanel.Controls.Item(Document.MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip _
                = Document.MySWControlMenuStrip
        ElseIf (dialogRes = DialogResult.Cancel) Then
        End If

        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000906 end
    End Sub

    Public Sub EditinSwEditor(ByRef layoutControl As SwLayoutControl)
        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:000000000000086F begin
        If _
            layoutControl IsNot Nothing AndAlso layoutControl.DocumentSign IsNot Nothing AndAlso
            layoutControl.DocumentSign.IsSign Then
            layoutControl.DocumentSign = OpenSwEditor(layoutControl.DocumentSign)
            layoutControl.Refresh()
            Refresh()
            Document.DocumentSigns.Item(SwFlowLayoutPanel1.Controls.GetChildIndex(layoutControl)) =
                layoutControl.DocumentSign

        ElseIf _
            layoutControl IsNot Nothing AndAlso layoutControl.DocumentSign IsNot Nothing AndAlso
            Not layoutControl.DocumentSign.IsSign Then
            layoutControl.DocumentSign.DocumentImage = OpenImageEditor(layoutControl.DocumentSign.DocumentImage)

            'Document.DocumentSigns.Item(SwFlowLayoutPanel1.Controls.GetChildIndex(LayoutControl)) = LayoutControl.DocumentSign

        End If
        'section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:000000000000086F end
    End Sub

    Private Sub SWLayoutControl_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles Me.DoubleClick
        Dim lc = DirectCast(sender, SwLayoutControl)
        If lc.DocumentSign.IsSign Then
            lc.DocumentSign = OpenSwEditor(lc.DocumentSign)
        Else
            lc.DocumentSign.DocumentImage = OpenImageEditor(lc.DocumentSign.DocumentImage)
        End If
    End Sub

    Private Sub LayoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LayoutToolStripMenuItem.Click
        SwFlowLayoutPanel1.PerformLayout()
    End Sub

    Private Shared Sub PictBoxContextMenuStrip_Opening(sender As Object, e As CancelEventArgs) _
        Handles PictBoxContextMenuStrip.Opening

        Dim cms As ContextMenuStrip = DirectCast(sender, ContextMenuStrip)
        Dim ctrl = cms.SourceControl
        Dim pictParent As SwFlowLayoutPanel = DirectCast(ctrl.Parent, SwFlowLayoutPanel)

        pictParent.RightClickedControl = ctrl
    End Sub

    Private Sub LoadSettings()
        If _documentSettings Is Nothing Then
            _documentSettings = New DocumentSettings(Me)
        End If
    End Sub

    Private Sub HelpToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HelpToolStripMenuItem1.Click
        Help.ShowHelp(Me, "SignWriterStudio.chm", "dictionary.htm")
    End Sub

    Private Sub FromEditorToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles FromEditorToolStripMenuItem.Click
        AddSwSignFromSwEditor()
        DocumentChanged = True
    End Sub

    Private Sub FromDictionaryF10ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles FromDictionaryF10ToolStripMenuItem.Click
        AddSignFromDictionary()
        Document.MySWFlowLayoutPanel.PerformLayout()
        DocumentChanged = True
    End Sub

    Private Sub FromFileToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles FromFileToolStripMenuItem.Click
        AddImageFromImageEditor()
        DocumentChanged = True
    End Sub

    Private Sub FromDictionaryPhotoToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles FromDictionaryPhotoToolStripMenuItem.Click
        PhotofromDictionary()
        DocumentChanged = True
    End Sub

    Private Sub FromDictionarySignToolStripMenuItem1_Click(sender As Object, e As EventArgs) _
        Handles FromDictionarySignToolStripMenuItem1.Click
        SignPhotofromDictionary()
        DocumentChanged = True
    End Sub

    'Private Sub GlossToSignToolStripMenuItem_Click(sender As Object, e As EventArgs)

    '    Gloss2Sign()
    'End Sub

    Private Sub PasteFSWDocumentToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles PasteFSWDocumentToolStripMenuItem.Click
        Dim clipboardText = Clipboard.GetText()
        Dim fsw = InputBox("FSW", "", clipboardText)
        PasteFswDocument(fsw)
    End Sub

    Private Sub CopyAsFSWToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles CopyAsFSWToolStripMenuItem.Click
        CopyAsFsw()
    End Sub

    Private Sub CopyAsFsw()
        Dim sb As New StringBuilder
        Dim conv As New SpmlConverter
        For Each DocumentSign In Document.DocumentSigns
            sb.Append(conv.GetFsw(DocumentSign))
            sb.Append(" ")

        Next
        Clipboard.SetText(sb.ToString())
    End Sub

    Private Sub PasteToSignPuddleToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles PasteToSignPuddleToolStripMenuItem.Click
        PastetoSignPuddle()
    End Sub

    Private Sub PastetoSignPuddle()

        Dim sb As New StringBuilder
        Dim conv As New SpmlConverter
        For Each DocumentSign In Document.DocumentSigns
            sb.Append(conv.GetFsw(DocumentSign))
            sb.Append(" ")

        Next
        Process.Start("http://www.signbank.org/signpuddle2.0/signtextsave.php?ui=1&sgn=&sgntxt=" & sb.ToString())
    End Sub

    Private Sub OptionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OptionsToolStripMenuItem.Click
        Options()
    End Sub

    Private Sub Options()
        Dim swOptions As New SWOptions
        Dim dialogRes As DialogResult = swOptions.ShowDialog()
        If (dialogRes = DialogResult.OK) Then
            'Use TSWOptions
            'UpdateOptions()
            swOptions.Close()
        ElseIf (dialogRes = DialogResult.Cancel) Then
            swOptions.Close()
        End If
    End Sub

    Private Sub CopyAsImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyAsImageToolStripMenuItem.Click
        SwFlowLayoutPanel1.HorizontalScroll.Value = 0
        Dim controlCollection As Control.ControlCollection = SwFlowLayoutPanel1.Controls

        Dim image = CropImage(GetPanelImage(SwFlowLayoutPanel1.DisplayRectangle, controlCollection), GetDocumentBounds(controlCollection), 5)
        Clipboard.SetImage(image)
    End Sub

    Private Function CropImage(ByVal image As Image, ByVal docBounds As Rectangle, imagePadding As Int32) As Image
        Dim bmp As New Bitmap(docBounds.Width + imagePadding * 2, docBounds.Height + imagePadding * 2)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.Clear(Color.White)
        g.DrawImage(image, New Rectangle(imagePadding, imagePadding, docBounds.Width, docBounds.Height), docBounds, GraphicsUnit.Pixel)
        Return bmp
    End Function

    Private Function GetDocumentBounds(ByVal panelControls As Control.ControlCollection) As Rectangle
        Return BoundingRectangle((From control As Control In panelControls Select New Rectangle(control.Left, control.Top, control.Width, control.Height)).ToArray())
    End Function

    Private Function GetPanelImage(ByVal dispRectangle As Rectangle, ByVal panelControls As Control.ControlCollection) As Image
        Dim bmp As New Bitmap(dispRectangle.Width, dispRectangle.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)

        g.Clear(Color.White)

        For Each control As Control In panelControls
            DrawControl(control, bmp)
        Next
        DrawLines(bmp)
        Return bmp
    End Function

    Public Sub DrawControl(control As Control, bitmap As Bitmap)
        control.DrawToBitmap(bitmap, control.Bounds)
        For Each childControl As Control In control.Controls
            DrawControl(childControl, bitmap)
        Next
    End Sub

    Friend Sub DrawLines(ByVal bitmap As Bitmap)
        Dim layoutEng = SwFlowLayoutPanel1.LayoutEng
        If layoutEng.LayoutEngineSettings.DrawColumnLines Then
            Dim g As Graphics = Graphics.FromImage(bitmap)

            Dim offset As Integer = DisplayRectangle.X
            For Each line As Rectangle In layoutEng.Lines
                g.DrawLine(Pens.Black, line.X + offset, line.Y, line.X + offset + line.Width, line.Y + line.Height)
            Next
        End If
    End Sub
    Private Shared Function BoundingRectangle(int32Rects As Rectangle()) As Rectangle
        Dim xMin As Integer = int32Rects.Min(Function(s) s.X)
        Dim yMin As Integer = int32Rects.Min(Function(s) s.Y)
        Dim xMax As Integer = int32Rects.Max(Function(s) s.X + s.Width)
        Dim yMax As Integer = int32Rects.Max(Function(s) s.Y + s.Height)
        Dim int32Rect = New Rectangle(xMin, yMin, xMax - xMin, yMax - yMin)
        Return int32Rect
    End Function

    Private Sub GlossToSignRealTimeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GlossToSignRealTimeToolStripMenuItem.Click
        Gloss2SignRealTime()
    End Sub
End Class

