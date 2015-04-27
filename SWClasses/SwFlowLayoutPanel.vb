Option Strict On

Imports System.Windows.Forms
Imports System.Windows.Forms.Layout
Imports System.Drawing

Public NotInheritable Class SwFlowLayoutPanel
    Inherits FlowLayoutPanel
    ' In this section you can add your own using directives
    ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:00000000000009B7 begin
    ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:00000000000009B7 end
    ' *
    '          *   A class that represents ...  All rights Reserved Copyright(c) 2008
    '          *
    '          *       @see SWLayoutControl, SWDocument
    '          *       @author Jonathan Duncan
    '          */

    ' Attributes


    Private _mySWDocument As SwDocument
    Public Property MySWDocument() As SwDocument
        Get
            Return _mySWDocument
        End Get
        Set(ByVal value As SwDocument)
            _mySWDocument = value
        End Set
    End Property
    Public Property RightClickedControl As SwLayoutControl
    'Public mySWDocumentSign As ArrayList
    'Public mySWDocumentImage As ArrayList
    Public LayoutEng As New SwLayoutEngine
    ' Operations
    Public Property Direction() As FlowDirection
        Get
            Return LayoutEng.LayoutEngineSettings.FlowDirection
        End Get
        Set(ByVal value As FlowDirection)
            'Only accept these two options
            If value = Windows.Forms.FlowDirection.TopDown Or value = Windows.Forms.FlowDirection.LeftToRight Then
                LayoutEng.LayoutEngineSettings.FlowDirection = value
            End If
        End Set
    End Property

    Public Property SpaceBetweenCols() As Integer
        Get
            Return LayoutEng.LayoutEngineSettings.SpaceBetweenCols
        End Get
        Set(ByVal value As Integer)
            LayoutEng.LayoutEngineSettings.SpaceBetweenCols = value
        End Set
    End Property
    Public Property DrawColumnLines() As Boolean
        Get
            Return LayoutEng.LayoutEngineSettings.DrawColumnLines
        End Get
        Set(ByVal value As Boolean)
            LayoutEng.LayoutEngineSettings.DrawColumnLines = value
        End Set
    End Property
    Public Overrides ReadOnly Property LayoutEngine() As LayoutEngine
        Get
            If LayoutEng Is Nothing Then
                LayoutEng = New SwLayoutEngine()

            End If

            Return LayoutEng
        End Get
    End Property

    Friend Sub DrawLines()
        If LayoutEng.LayoutEngineSettings.DrawColumnLines Then
            Dim g As Graphics = CreateGraphics()
            g.Clear(LayoutEng.LayoutEngineSettings.BackgroundColor)
            Dim offset As Integer = DisplayRectangle.X
            For Each line As Rectangle In LayoutEng.Lines
                g.DrawLine(Pens.Black, line.X + offset, line.Y, line.X + offset + line.Width, line.Y + line.Height)
            Next
        End If
    End Sub

    Private Sub SWFlowLayoutPanel_Paint(ByVal sender As Object, ByVal e As Windows.Forms.PaintEventArgs) Handles Me.Paint
        DrawLines()
    End Sub
    Public Sub MoveControlInFrontof(signtomove As SwLayoutControl, beforesign As SwLayoutControl)
        Dim beforeSignIndex As Integer?
        beforeSignIndex = GetControlIndex(beforesign)
        If beforeSignIndex.HasValue AndAlso signtomove IsNot Nothing Then
            Controls.SetChildIndex(signtomove, beforeSignIndex.Value)


            Dim docBeforeSignIndex As Integer? = GetSignIndex(beforesign.DocumentSign)
            MySWDocument.DocumentSigns.Remove(signtomove.DocumentSign)
            If docBeforeSignIndex.HasValue Then MySWDocument.DocumentSigns.Insert(docBeforeSignIndex.Value, signtomove.DocumentSign)
        End If
    End Sub
    Private Function GetControlIndex(swLayoutControl As Control) As Integer?
        If Controls.Contains(swLayoutControl) Then
            Return Controls.GetChildIndex(swLayoutControl)
        Else
            Return Nothing
        End If

    End Function
    Private Function GetSignIndex(swDocumentSign As SwDocumentSign) As Integer?
        If MySWDocument.DocumentSigns.Contains(swDocumentSign) Then
            Return MySWDocument.DocumentSigns.IndexOf(swDocumentSign)
        Else
            Return Nothing
        End If
    End Function

    Public Sub New()
        VerticalScroll.Maximum = 0
    End Sub
End Class