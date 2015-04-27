Imports System.Threading
Imports System.Globalization
Imports SignWriterStudio.SWClasses
Public Class ImageEditor
    'Friend WithEvents monitor As EQATEC.Analytics.Monitor.IAnalyticsMonitor = EQATEC.Analytics.Monitor.AnalyticsMonitorFactory.Create("7A55FE8188FD4072B11C3EA5D30EB7F9")

    Dim DTImageEditorTranslations As DataTable
    ' The following three methods will draw a rectangle and allow 
    ' the user to use the mouse to resize the rectangle.  If the 
    ' rectangle intersects a control's client rectangle, the 
    ' control's color will change.
    Private SaveChange As Boolean = False
    Public Property Image() As Image
        Get
            Return Me.PBImageEditor.Image
        End Get
        Set(ByVal value As Image)
            Me.PBImageEditor.Image = value
        End Set
    End Property


    Dim isDrag As Boolean = False
    Dim isSelect As Boolean = False
    Dim isErase As Boolean = False
    Dim theRectangle As New Rectangle(New Point(0, 0), New Size(0, 0))
    Dim startPoint As Point
    Dim ImageEditorUndo As New General.Undo(Of Image)
    Dim MousePos As Point

    Function CheckCropRect(ByVal CropRect As Rectangle, ByVal BitmapRect As Rectangle) As Rectangle
        Dim NewWidth As Integer
        Dim NewHeight As Integer
        Dim NewX As Integer
        Dim NewY As Integer
        If CropRect.Right > BitmapRect.Width Then
            NewWidth = BitmapRect.Width - CropRect.Left
        Else
            NewWidth = CropRect.Width
        End If
        If CropRect.Bottom > BitmapRect.Height Then
            NewHeight = BitmapRect.Height - CropRect.Top
        Else
            NewHeight = CropRect.Height
        End If
        If CropRect.X < 0 Then
            NewWidth = NewWidth + CropRect.X
            NewX = 0
        Else
            NewX = CropRect.X
        End If
        If CropRect.Y < 0 Then
            NewHeight = NewHeight + CropRect.Y
            NewY = 0
        Else
            NewY = CropRect.Y
        End If
        Dim ReturnRec As New Rectangle(NewX, NewY, NewWidth, NewHeight)
        Return ReturnRec
    End Function
    Function MakepositiveRect(ByVal Rect1 As Rectangle) As Rectangle
        If Rect1.Width < 0 Then
            Rect1.X = Rect1.X + Rect1.Width
            Rect1.Width = Math.Abs(Rect1.Width)
        End If
        If Rect1.Height < 0 Then
            Rect1.Y = Rect1.Y + Rect1.Height
            Rect1.Height = Math.Abs(Rect1.Height)
        End If
        Return Rect1
    End Function
    Private Sub PBImageEditor_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PBImageEditor.MouseDown
        isSelect = True
        If isSelect Then
            PBImageEditor.Cursor = Cursors.Cross
        End If
        ' Set the isDrag variable to true and get the starting point 
        ' by using the PointToScreen method to convert form coordinates to
        ' screen coordinates.
        If (e.Button = Windows.Forms.MouseButtons.Left) Then
            isDrag = True
        End If
        If isSelect Then
            Dim control As Control = CType(sender, Control)

            ' Calculate the startPoint by using the PointToScreen 
            ' method.
            startPoint = control.PointToScreen(New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub PBImageEditor_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PBImageEditor.MouseMove
        'Dim PictureBox1MsgPrompt As String = GetTranslation("PictureBox1MsgPrompt", DTImageEditorTranslations)

        If Not isErase And isSelect Then


            ' If the mouse is being dragged, undraw and redraw the rectangle
            ' as the mouse moves.
            If (isDrag) Then

                ' Hide the previous rectangle by calling the DrawReversibleFrame 
                ' method with the same parameters.
                ControlPaint.DrawReversibleFrame(theRectangle, Me.BackColor, _
                    FrameStyle.Dashed)

                ' Calculate the endpoint and dimensions for the new rectangle, 
                ' again using the PointToScreen method.
                Dim control As Control = CType(sender, Control)

                Dim endPoint As Point = control.PointToScreen(New Point(e.X, e.Y))
                Dim width As Integer = endPoint.X - startPoint.X
                Dim height As Integer = endPoint.Y - startPoint.Y
                theRectangle = New Rectangle(startPoint.X, startPoint.Y, _
                    width, height)

                ' Draw the new rectangle by calling DrawReversibleFrame again.  
                ControlPaint.DrawReversibleFrame(theRectangle, Me.BackColor, _
                     FrameStyle.Dashed)
            End If
        ElseIf isErase And isDrag Then
            Dim control As Control = CType(sender, Control)
            'MousePos = control.PointToScreen(New Point(e.X, e.Y))
            MousePos = New Point(e.X, e.Y)
            If isErase Then
                Dim g As Graphics
                Try
                    g = Graphics.FromImage(Me.PBImageEditor.Image)
                    g.FillRectangle(Brushes.White, New Rectangle(MousePos.X, MousePos.Y, 20, 20))
                Catch ex As Exception
                    'Monitor.TrackException(ex, _
                    '                  TraceEventType.Error, _
                    '                  "Exception ")
                    General.LogError(ex, "Exception ")
                    'MsgBox(PictureBox1MsgPrompt, MsgBoxStyle.OkOnly)
                    isErase = False
                    My.Application.Log.WriteException(ex)
                    Exit Sub
                Finally
                    If g IsNot Nothing Then

                        g.Dispose()
                    End If
                End Try

            End If
            control.Refresh()

        End If
    End Sub

    Private Sub PBImageEditor_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PBImageEditor.MouseUp

        If isSelect Then
            ' If the MouseUp event occurs, the user is not dragging.
            isDrag = False
        End If
        If isErase Then
            isErase = False
            isDrag = False
        End If
    End Sub

    Private Sub ImageEditor_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        LoadTranslations()
    End Sub
    Private Sub ChangeImage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = FormWindowState.Maximized

        Me.SaveImage.Filter = "Portable Network Graphics(*.png)|*.png|Joint Photographic Experts Group(*.jpg)|*.jpg|Bitmap image(*.bmp)|*.bmp|Graphics Interchange Format(*.gif)|*.gif|Tag Image File Format(*.tiff)|*.tiff|All Image files (*.png)(*.jpg)(*.bmp)(*.gif)(*.tiff)|*.png;*.jpg;*.bmp;*.gif;*.tiff"
        Me.SaveImage.DefaultExt = ".png"
        Me.SaveImage.FilterIndex = 1

        Me.OpenImage.Filter = "Portable Network Graphics(*.png)|*.png|Joint Photographic Experts Group(*.jpg)|*.jpg|Bitmap image(*.bmp)|*.bmp|Graphics Interchange Format(*.gif)|*.gif|Tag Image File Format(*.tiff)|*.tiff|All Image files (*.png)(*.jpg)(*.bmp)(*.gif)(*.tiff)|*.png;*.jpg;*.bmp;*.gif;*.tiff"
        Me.OpenImage.DefaultExt = ".png"
        Me.OpenImage.FilterIndex = 6
    End Sub
    Private Sub AddUndo()
        If Me.PBImageEditor.Image IsNot Nothing AndAlso ImageEditorUndo IsNot Nothing Then
            ImageEditorUndo.Add(Me.PBImageEditor.Image.Clone)
        End If

    End Sub

    Private Sub ImportImage(ByVal filename As String)

        Me.PBImageEditor.Image = General.ByteArraytoImage(My.Computer.FileSystem.ReadAllBytes(filename))

    End Sub
    Private Sub ChangeImage_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'Clean up global variables.
        isDrag = False
        isSelect = False
        isErase = False
        PBImageEditor.Cursor = Cursors.Default
        theRectangle = New Rectangle(New Point(0, 0), New Size(0, 0))
        startPoint = Nothing
        ImageEditorUndo = Nothing
        MousePos = Nothing
    End Sub


    Private Sub LoadTranslations()

        'TODO Translations


    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        EraseImage()
    End Sub

    Private Sub EraseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EraseToolStripMenuItem.Click
        EraseImage()
    End Sub
    Private Sub EraseImage()
        AddUndo()
        PBImageEditor.Image = Nothing
    End Sub

    Private Sub LoadFromFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadFromFileToolStripMenuItem.Click
        LoadFromFile()
    End Sub
    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        LoadFromFile()
    End Sub
    Private Sub LoadFromFile()
        OpenImage.ShowDialog()
    End Sub

    Private Sub SaveToFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToFileToolStripMenuItem.Click
        SavetoFile()
    End Sub
    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem1.Click
        SavetoFile()
    End Sub
    Private Sub SavetoFile()
        Me.SaveImage.ShowDialog()
    End Sub
    Private Sub UndoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem.Click
        Undo()
    End Sub
    Private Sub UndoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoToolStripMenuItem1.Click
        Undo()
    End Sub
    Private Sub Undo()
        If Me.PBImageEditor.Image IsNot Nothing Then
            Me.PBImageEditor.Image = Me.ImageEditorUndo.Undo(Me.PBImageEditor.Image.Clone)
        Else
            Me.PBImageEditor.Image = Me.ImageEditorUndo.Undo(Nothing)
        End If
    End Sub
    Private Sub RedoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem.Click
        Redo()
    End Sub
    Private Sub RedoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RedoToolStripMenuItem1.Click
        Redo()
    End Sub
    Private Sub Redo()
        If Me.PBImageEditor.Image IsNot Nothing Then
            Me.PBImageEditor.Image = Me.ImageEditorUndo.Redo(Me.PBImageEditor.Image.Clone)
        Else
            Me.PBImageEditor.Image = Me.ImageEditorUndo.Redo(Nothing)
        End If
    End Sub
    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Save()
    End Sub
    Private Sub SaveToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem2.Click
        Save()
    End Sub
    Private Sub Save()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
    Private Sub EraserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EraserToolStripMenuItem.Click
        Eraser()
    End Sub
    Private Sub EraserToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EraserToolStripMenuItem1.Click
        Eraser()
    End Sub
    Private Sub Eraser()
        AddUndo()
        isErase = True
    End Sub
    Private Sub ResizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResizeToolStripMenuItem.Click
        ImageResize()
    End Sub
    Private Sub ResizeToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResizeToolStripMenuItem1.Click
        ImageResize()
    End Sub
    Private Sub ImageResize()

        AddUndo()
        Dim Bitmap1 As Bitmap = PBImageEditor.Image.Clone()
        Dim ImgHeight As Integer = PBImageEditor.Image.Height
        Dim ImgWidth As Integer = PBImageEditor.Image.Width
        Dim ScaleFact As Double
        Dim NewImgWidth As Integer
        Dim NewImgHeight As Integer
        Dim GoalImgWidth As Integer
        Dim GoalImgHeight As Integer
        Dim GoalImgWidthstr As String = InputBox("Please enter maximum width", "Width", 300)
        Dim GoalImgHeightstr As String = InputBox("Please enter maximum height", "Height", 300)
        If IsNumeric(GoalImgWidthstr) AndAlso CInt(GoalImgWidthstr) > 0 Then
            GoalImgWidth = CInt(GoalImgWidthstr)
        Else
            MessageBox.Show("Invalid value for width", "Invalid width", MessageBoxButtons.OK)
            Exit Sub
        End If
        If IsNumeric(GoalImgHeightstr) AndAlso CInt(GoalImgHeightstr) > 0 Then
            GoalImgHeight = CInt(GoalImgHeightstr)
        Else
            MessageBox.Show("Invalid value for height", "Invalid height", MessageBoxButtons.OK)
            Exit Sub
        End If
        If ImgHeight >= ImgWidth Then
            ScaleFact = GoalImgHeight / ImgHeight
        ElseIf ImgHeight <= ImgWidth Then
            ScaleFact = GoalImgWidth / ImgWidth
        End If
        NewImgWidth = ImgWidth * ScaleFact
        NewImgHeight = ImgHeight * ScaleFact
        PBImageEditor.Image = Bitmap1.GetThumbnailImage(NewImgWidth, NewImgHeight, Nothing, 0)

    End Sub

    Private Sub CropToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CropToolStripMenuItem1.Click
        Crop()
    End Sub
    Private Sub CropToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CropToolStripMenuItem.Click
        Crop()
    End Sub
    Private Sub Crop()

        ' Draw the rectangle to be evaluated. Set a dashed frame style 
        ' using the FrameStyle enumeration.
        ControlPaint.DrawReversibleFrame(theRectangle, Me.BackColor, _
            FrameStyle.Dashed)

        theRectangle = MakepositiveRect(theRectangle)
        Dim Bitmap1 As Bitmap = PBImageEditor.Image.Clone()
        Dim CropRect = PBImageEditor.RectangleToClient(theRectangle)
        Dim CroptoRec As Rectangle = CheckCropRect(CropRect, New Rectangle(0, 0, Bitmap1.Width, Bitmap1.Height))
        If CroptoRec.Width <= 0 Or CroptoRec.Height <= 0 Then
            Exit Sub
        Else
            AddUndo()
            Bitmap1 = Bitmap1.Clone(CroptoRec, Bitmap1.PixelFormat)
        End If

        PBImageEditor.Image = Bitmap1

        ' Reset the rectangle.
        theRectangle = New Rectangle(0, 0, 0, 0)
        isSelect = False
        If isSelect Then
            PBImageEditor.Cursor = Cursors.Cross
        Else
            PBImageEditor.Cursor = Cursors.Default
        End If
        PBImageEditor.Cursor = Cursors.Default

    End Sub
    Private Sub ChangeImage_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "imageeditor.htm")
            Case Keys.L
                If e.Control = True Then
                    EraseImage()
                End If
            Case Keys.O
                If e.Control = True Then
                    LoadFromFile()
                End If
            Case Keys.A
                If e.Control = True Then
                    SavetoFile()
                End If
            Case Keys.Z
                If e.Control = True Then
                    Undo()
                End If
            Case Keys.Y
                If e.Control = True Then
                    Redo()
                End If
            Case Keys.S
                If e.Control = True Then
                    Save()
                End If
            Case Keys.E
                If e.Control = True Then
                    Eraser()
                End If
            Case Keys.R
                If e.Control = True Then
                    ImageResize()
                End If
            Case Keys.P
                If e.Control = True Then
                    Crop()
                End If
            Case Keys.B
                If e.Control = True Then
                    ConvertBlackWhite()
                End If
            Case Keys.C
                If e.Alt = True Then
                    Cancel()
                ElseIf e.Control Then
                    Copy()
                End If
            Case Keys.Delete
                If e.Control = True Then
                    DeleteSelectedArea()
                End If
            Case Keys.X
                If e.Control = True Then
                    Cut()
                End If
            Case Keys.V
                If e.Control = True Then
                    AddUndo()
                    Me.PBImageEditor.Image = GetImageFromClipboard()
                End If
        End Select
    End Sub


    Private Sub CancelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelToolStripMenuItem.Click
        Cancel()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Cancel()
    End Sub
    Private Sub Cancel()
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OpenImage_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles OpenImage.FileOk
        ImportImage(OpenImage.FileName)
    End Sub

    Private Sub SaveImage_FileOk(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveImage.FileOk
        SaveImagetoFormat(CType(Me.PBImageEditor.Image, Bitmap), (SaveImage.FileName))
    End Sub
    Private Sub SaveImagetoFormat(ByVal Bitmap As Bitmap, ByVal Filename As String)
        Dim ImageFormat As Imaging.ImageFormat
        Dim Extension As String = System.IO.Path.GetExtension(Filename)
        Select Case Extension
            Case ".bmp"
                ImageFormat = Imaging.ImageFormat.Bmp
            Case ".gif"
                ImageFormat = Imaging.ImageFormat.Gif
            Case ".jpg"
                ImageFormat = Imaging.ImageFormat.Jpeg
            Case ".png"
                ImageFormat = Imaging.ImageFormat.Png
            Case ".tiff"
                ImageFormat = Imaging.ImageFormat.Tiff
            Case Else
                MessageBox.Show("Extension " & Extension & " is not a supported format")
                Exit Sub
        End Select

        Bitmap.Save(Filename, ImageFormat)

    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        DeleteSelectedArea()
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem1.Click
        DeleteSelectedArea()
    End Sub
    Private Sub DeleteSelectedArea()
        AddUndo()
        Dim control As Control = Me.PBImageEditor



        Dim g As Graphics
        Try
            g = Graphics.FromImage(Me.PBImageEditor.Image)
            g.FillRectangle(Brushes.White, Me.PBImageEditor.RectangleToClient(Me.theRectangle))
        Catch ex As Exception
            'Monitor.TrackException(ex, _
            '                  TraceEventType.Error, _
            '                  "Exception ")
            General.LogError(ex, "Exception ")
            'MsgBox(PictureBox1MsgPrompt, MsgBoxStyle.OkOnly)
            isErase = False
            My.Application.Log.WriteException(ex)
            Exit Sub
        Finally
            If g IsNot Nothing Then

                g.Dispose()
            End If
        End Try


        control.Refresh()
    End Sub

    Private Sub ConvertBlackWhiteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConvertBlackWhiteToolStripMenuItem.Click
        ConvertBlackWhite()
    End Sub

    Private Sub ConvertBlackWhiteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConvertBlackWhiteToolStripMenuItem1.Click
        ConvertBlackWhite()
    End Sub
    Private Sub ConvertBlackWhite()
        AddUndo()
        Me.PBImageEditor.Image = SWDrawing.ConvertBW(Me.PBImageEditor.Image)
    End Sub


    Private Sub CutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutToolStripMenuItem.Click
        Cut()
    End Sub
    Private Sub Cut()
        AddUndo()
        Copy()
        DeleteSelectedArea()

    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Copy()
    End Sub
    Private Sub Copy()
        Dim SelectedRectangle As New Rectangle
        Dim Image1 As Image
        If Me.theRectangle.Width > 0 AndAlso Me.theRectangle.Height > 0 Then
            SelectedRectangle = MakepositiveRect(theRectangle)
        Else
            SelectedRectangle = New Rectangle(0, 0, Me.PBImageEditor.Image.Width, Me.PBImageEditor.Image.Height)
        End If

        Dim Bitmap1 As Bitmap = PBImageEditor.Image.Clone()
        Dim CropRect = PBImageEditor.RectangleToClient(theRectangle)
        Dim CroptoRec As Rectangle = CheckCropRect(CropRect, New Rectangle(0, 0, Bitmap1.Width, Bitmap1.Height))
        If CroptoRec.Width < 1 Or CroptoRec.Height < 1 Then
            Exit Sub
        Else
            AddUndo()
            Bitmap1 = Bitmap1.Clone(CroptoRec, Bitmap1.PixelFormat)
        End If

        Image1 = Bitmap1

        SetImagetoClipboard(Image1)
    End Sub

    Private Sub PasteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteToolStripMenuItem.Click
        AddUndo()
        Me.PBImageEditor.Image = GetImageFromClipboard()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllToolStripMenuItem.Click
        Dim TopPoint As Point = Me.PBImageEditor.PointToScreen(Me.PBImageEditor.Location)

        theRectangle = New Rectangle(TopPoint.X, TopPoint.Y, Me.PBImageEditor.Image.Width, Me.PBImageEditor.Image.Height)
        ' Draw the rectangle to be evaluated. Set a dashed frame style 
        ' using the FrameStyle enumeration.
        ControlPaint.DrawReversibleFrame(theRectangle, Me.BackColor, _
            FrameStyle.Dashed)
    End Sub

    Private Sub ContentsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ContentsToolStripMenuItem.Click
        Help.ShowHelp(Me, "SignWriterStudio.chm", "imageeditor.htm")
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
    Private Sub SetSWSigntoClipboard(ByVal Sign As SwSign)
        If Sign IsNot Nothing Then
            Clipboard.SetData("SWSign", Sign)
        End If
    End Sub

End Class