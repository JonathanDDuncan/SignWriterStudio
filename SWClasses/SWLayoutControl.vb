﻿Option Strict On

Imports System.Windows.Forms
Imports System.Drawing

Public NotInheritable Class SwLayoutControl
    Inherits UserControl
    'Inherits PictureBox
    Implements ICloneable

    ' Attributes
    Public Property RightClickDownSender As SwLayoutControl
    Public Property LeftClickDownSender As SwLayoutControl
    Public SwFlowLayoutPanel1 As SwFlowLayoutPanel
    Friend WithEvents PictureBox1 As Windows.Forms.PictureBox
    Friend WithEvents TBGloss As Windows.Forms.TextBox
    Private _swDocumentSign As SwDocumentSign
    Public Property DocumentSign() As SwDocumentSign
        Get
            Return _swDocumentSign
        End Get
        Set(ByVal value As SwDocumentSign)
            If value Is Nothing Then
                Throw New ArgumentNullException("value")
            End If

            _swDocumentSign = value
            Anchor = _swDocumentSign.Lane
        End Set
    End Property

    Property Selected() As Boolean
        Get
            Return _selected1
        End Get
        Set(value As Boolean)
            If Not value = _selected1 Then
                _selected1 = value
                SetBackgroundColor()
            End If
        End Set
    End Property

    Private Sub SetBackgroundColor()
        If Selected Then
            DocumentSign().BkColor = Color.Blue
        Else
            DocumentSign().BkColor = Color.White
        End If
        DocumentSign().Render()
        Refresh()
    End Sub

    Private _symbolToolTip As New Windows.Forms.ToolTip()
    Private _isToolTipSet As Boolean
    Private _selected1 As Boolean
    Private _showGloss1 As Boolean = True


    ' Associations

    ' Operations

    Public Shadows Property Anchor() As Windows.Forms.AnchorStyles
        Get
            Return MyBase.Anchor
        End Get
        Set(ByVal value As Windows.Forms.AnchorStyles)
            MyBase.Anchor = value
            If DocumentSign IsNot Nothing Then
                DocumentSign.Lane = value
            End If
        End Set
    End Property

    Public ReadOnly Property Image() As Image
        Get
            Return PictureBox1.Image
        End Get

    End Property

    Public Property ShowGloss() As Boolean
        Get
            Return _showGloss1
        End Get
        Set(value As Boolean)
            _showGloss1 = value
            ResizeTextBox()
            Refresh()
            SetPictureSizeLocation()
        End Set
    End Property

    Public Overrides Sub Refresh()

        If DocumentSign.IsSign Then
            If DocumentSign IsNot Nothing AndAlso DocumentSign.Frames IsNot Nothing AndAlso DocumentSign.Frames.Count > 0 Then
                PictureBox1.Image = SWDrawing.DrawSWDrawing(DocumentSign, -1, DocumentSign.FramePadding, False)
                PictureBox1.Size = PictureBox1.Image.Size
            End If

            SetPictureSizeLocation()
        Else
            Padding = New Padding(0)
            If DocumentSign.DocumentImage IsNot Nothing Then
                PictureBox1.Image = DocumentSign.DocumentImage
                PictureBox1.Size = PictureBox1.Image.Size


            End If
        End If

        ResizeTextBox()
        SetControlSize()

        MyBase.Refresh()
    End Sub

    Private Sub SetControlSize()
        Dim TBGlossHeight = 0
        If ShowGloss Then
            TBGlossHeight = TBGloss.Height
        End If
        Size = New Size(PictureBox1.Image.Size.Width, PictureBox1.Image.Size.Height + TBGlossHeight)
    End Sub

    Public Sub InitializeText()

        If DocumentSign.IsSign Then
            If DocumentSign IsNot Nothing Then

                If DocumentSign.Glosses IsNot Nothing AndAlso Not DocumentSign.Glosses.Trim = String.Empty Then
                    TBGloss.Text = DocumentSign.Gloss & ", " & DocumentSign.Glosses
                Else
                    TBGloss.Text = DocumentSign.Gloss
                End If
            End If
            ResizeTextBox()
            SetPictureSizeLocation()

        End If

        MyBase.Refresh()
    End Sub
    Public Sub SetPictureSizeLocation()
        With DocumentSign

            Padding = New Padding(0)
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Location = New Point(0, 0)
                Dim TBGlossHeight = 0
                If ShowGloss Then
                    TBGlossHeight = TBGloss.Height
                End If
                Size = New Size(MinWidth, PictureBox1.Image.Size.Height + TBGlossHeight)
            End If
        End With

    End Sub

    Private Sub ResizeTextBox()
        If ShowGloss Then

            Dim text1 = If(TBGloss.Text IsNot Nothing AndAlso TBGloss.Text = String.Empty, " ", TBGloss.Text)
            Dim TextSize = TBGloss.CreateGraphics().MeasureString(text1,
                                                               TBGloss.Font,
                                                               MinWidth(),
                                                               New StringFormat(0))

            TBGloss.Height = Convert.ToInt32(TextSize.Height)
            TBGloss.Width = MinWidth()
        Else
            TBGloss.Height = 0
            TBGloss.Width = 0

        End If
        Dim image = PictureBox1?.Image
        Dim Y = If(image IsNot Nothing, image.Height, 0)
        TBGloss.Location = New Point(0, Y)
    End Sub
    Private Function MinWidth() As Integer
        Dim image = PictureBox1?.Image
        If image IsNot Nothing Then
            Return Math.Max(image.Size.Width, 50)
        Else
            Return 50
        End If
    End Function

    Protected Overrides Sub OnMousemove(ByVal e As Windows.Forms.MouseEventArgs)
        Dim newTooltipString As String = String.Empty

        Dim testRegion As Region
        Dim symbol As SWSignSymbol

        Dim movePoint As New Point(e.X, e.Y)
        'Check if know where startpoint is

        Dim regionFound As Boolean = False
        Dim frameIndex As Integer
        Dim frameOffset As Point
        Dim frameBounds As Rectangle
        Dim allFramesBounds As New Rectangle(0, 0, 0, 0)
        For frameIndex = 0 To DocumentSign.Frames.Count - 1
            frameBounds = DocumentSign.Frames(frameIndex).GetSWSignBounds(DocumentSign.Frames(frameIndex))
            allFramesBounds.Height += frameBounds.Height
            If frameBounds.Width >= allFramesBounds.Width Then
                allFramesBounds.Width = frameBounds.Width
            End If
        Next
        For frameIndex = 0 To DocumentSign.Frames.Count - 1
            frameBounds = DocumentSign.Frames(frameIndex).GetSWSignBounds(DocumentSign.Frames(frameIndex))
            frameOffset.X = CInt((allFramesBounds.Width - frameBounds.Width) / 2)

            For Each symbol In DocumentSign.Frames(frameIndex).SignSymbols
                testRegion = New Region(New Rectangle(symbol.X - DocumentSign.Frames(frameIndex).CropPoint.X + Padding.Left + frameOffset.X, symbol.Y - DocumentSign.Frames(frameIndex).CropPoint.Y + Padding.Top + frameOffset.Y, symbol.SymbolDetails.Width, symbol.SymbolDetails.Height))
                'If in selection rectangle

                If testRegion.IsVisible(movePoint) Then
                    newTooltipString = symbol.SymbolDetails.BaseName
                    regionFound = True
                    Exit For
                End If

            Next
            frameOffset.Y += frameBounds.Height + Padding.Bottom + Padding.Top + DocumentSign.FramePadding
            If regionFound Then
                Exit For
            End If
        Next
        If regionFound AndAlso Not _isToolTipSet Then
            _symbolToolTip.Active = True
            _symbolToolTip.SetToolTip(Me, newTooltipString)
            _isToolTipSet = True
        ElseIf Not regionFound And _isToolTipSet Then
            _symbolToolTip.SetToolTip(Me, DocumentSign.Gloss)
            _isToolTipSet = False
            _symbolToolTip.Active = True

        End If
        MyBase.OnMouseMove(e)
    End Sub

    Private Sub SWLayoutControl_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) Handles Me.DragDrop, PictureBox1.DragDrop
        Dim obj = e.Data.GetData("SignWriterStudio.SWClasses.SwLayoutControl")
        Dim signtoMove As SwLayoutControl = CType(obj, SwLayoutControl)
        MoveControlBefore(signtoMove, Me)
    End Sub


    Private Shared Sub SWLayoutControl_DragEnter(ByVal sender As Object, ByVal e As Windows.Forms.DragEventArgs) Handles Me.DragEnter, PictureBox1.DragEnter
        e.Effect = DragDropEffects.Copy Or DragDropEffects.Move
    End Sub

    Public Sub New()
        InitializeComponent()
        Width = 1
        Height = 1
        _symbolToolTip.AutoPopDelay = 5000
        _symbolToolTip.InitialDelay = 500
        _symbolToolTip.IsBalloon = True
        _symbolToolTip.ReshowDelay = 100
        BackColor = Color.White
        DocumentSign = New SwDocumentSign
        DocumentSign.FramePadding = 15
        AllowDrop = True
    End Sub

    Private Sub SWLayoutControl_MouseDown(sender As Object, e As Windows.Forms.MouseEventArgs) Handles Me.MouseDown, PictureBox1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Left Then
            LeftClickDownSender = Me
            RightClickDownSender = Nothing
            DoDragDrop(Me, DragDropEffects.Copy)
        End If

    End Sub

    Private Sub PictBox_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseEnter, PictureBox1.MouseEnter
        _symbolToolTip.Active = True
    End Sub
    Private Sub PictBox_MouseLeave(ByVal sender As Object, ByVal e As EventArgs) Handles Me.MouseLeave, PictureBox1.MouseLeave
        _symbolToolTip.Active = False
    End Sub
    Public Function Clone() As Object Implements ICloneable.Clone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A begin
        Dim newclone As New SwLayoutControl

        _symbolToolTip = New Windows.Forms.ToolTip()

        newclone.DocumentSign = CType(DocumentSign.Clone, SwDocumentSign)
        Return newclone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A end
    End Function
    Private Sub MoveControlBefore(signtomove As SwLayoutControl, beforesign As SwLayoutControl)
        If Parent IsNot Nothing Then
            Dim parentCtrl = TryCast(Parent, SwFlowLayoutPanel)

            Dim selectedControls = parentCtrl.SelectedControls()
            If selectedControls.Count > 0 Then
                parentCtrl.MoveControlsInFrontof(selectedControls, beforesign)
            Else
                parentCtrl.MoveControlInFrontof(signtomove, beforesign)
            End If

        End If
    End Sub

    Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TBGloss = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(131, 143)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'TBGloss
        '
        Me.TBGloss.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TBGloss.BackColor = System.Drawing.SystemColors.Window
        Me.TBGloss.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TBGloss.Location = New System.Drawing.Point(0, 143)
        Me.TBGloss.Multiline = True
        Me.TBGloss.Name = "TBGloss"
        Me.TBGloss.Size = New System.Drawing.Size(131, 20)
        Me.TBGloss.TabIndex = 1
        Me.TBGloss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SwLayoutControl
        '
        Me.Controls.Add(Me.TBGloss)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "SwLayoutControl"
        Me.Size = New System.Drawing.Size(131, 163)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Private Sub TBGloss_TextChanged(sender As Object, e As EventArgs) Handles TBGloss.TextChanged
        ResizeTextBox()
        SetPictureSizeLocation()
    End Sub


End Class