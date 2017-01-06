Option Strict On

Imports System.Xml.Serialization
Imports System.Windows.Forms
Imports Newtonsoft.Json


<Serializable()> Public NotInheritable Class SwDocument
    Implements IDisposable


    ' In this section you can add your own using directives
    ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:000000000000085D begin

    ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:000000000000085D end
    ' *
    '          *   A class that represents a SignWriting Document
    '          *   All rights Reserved Copyright(c) 2008
    '          *
    '          *       @see SWDocumentSign
    '          *       @author Jonathan Duncan
    '          */
    ' Attributes
    'TODO Keep class from dependent on the layoutpanel and menustrip.  Check that not nothing
    <NonSerialized()> <JsonIgnore()> Private _mySWFlowLayoutPanel As SwFlowLayoutPanel
    <XmlIgnore()>
   <JsonIgnore()>
    Public Property MySWFlowLayoutPanel() As SwFlowLayoutPanel
        Get
            Return _mySWFlowLayoutPanel
        End Get
        Set(ByVal value As SwFlowLayoutPanel)
            _mySWFlowLayoutPanel = value
        End Set
    End Property
    Private _version As Integer = 1

    Public Property Version() As Integer
        Get
            Return _version
        End Get
        Set(ByVal value As Integer)
            _version = value
        End Set
    End Property
    <NonSerialized()> <JsonIgnore()> Private _mySWControlMenuStrip As ContextMenuStrip
    <XmlIgnore()> <JsonIgnore()>
    Public Property MySWControlMenuStrip() As ContextMenuStrip
        Get
            Return _mySWControlMenuStrip
        End Get
        Set(ByVal value As ContextMenuStrip)
            _mySWControlMenuStrip = value
        End Set
    End Property
    <NonSerialized()> <JsonIgnore()> Private _myForm As Form
    <XmlIgnore()> <JsonIgnore()>
    Public Property MyForm() As Form
        Get
            Return _myForm
        End Get
        Set(ByVal value As Form)
            _myForm = value
        End Set
    End Property
    '
    Private _layoutEngineSettings As SwLayoutEngineSettings
    Public Property LayoutEngineSettings() As SwLayoutEngineSettings
        Get
            Return _layoutEngineSettings
        End Get
        Set(ByVal value As SwLayoutEngineSettings)
            _layoutEngineSettings = value
        End Set
    End Property
    '
    Private ReadOnly _documentSigns As New List(Of SwDocumentSign)()
    Public ReadOnly Property DocumentSigns() As List(Of SwDocumentSign)
        Get
            Return _documentSigns
        End Get
    End Property
    <NonSerialized()> Private _swDocumentPrintPages As New SWPrintPages()
    <XmlIgnore()> <JsonIgnore()>
    Public Property SWDocumentPrintPages() As SWPrintPages
        Get
            Return _swDocumentPrintPages
        End Get
        Set(ByVal value As SWPrintPages)
            _swDocumentPrintPages = value
        End Set
    End Property

    <XmlIgnore()> <JsonIgnore()>
    <NonSerialized()> Friend WithEvents LayoutControl As SwLayoutControl
    Public Sub AddSWSign(ByVal sign As SwSign)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        Dim documentSign As SwDocumentSign = ConverSWSignToSWDocumentSign(sign)

        DocumentSigns.Add(documentSign)

        Dim layoutControl1 = New SwLayoutControl
        layoutControl1.DocumentSign = documentSign
        MySWFlowLayoutPanel.Controls.Add(layoutControl1)

        MySWFlowLayoutPanel.Controls.Item(MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip = MySWControlMenuStrip
        layoutControl1.Refresh()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub
    Public Sub AddSWSignLane(ByVal sign As SwSign, ByVal lane As Integer)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        Dim documentSign As SwDocumentSign = ConverSWSignToSWDocumentSign(sign)
        SetLane(documentSign, lane)

        DocumentSigns.Add(documentSign)
        documentSign.IncorporateSWSign(sign)
        Dim layoutControl1 As New SwLayoutControl
        layoutControl1.DocumentSign = documentSign
        MySWFlowLayoutPanel.Controls.Add(layoutControl1)
        MySWFlowLayoutPanel.Controls.Item(MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip = MySWControlMenuStrip

        layoutControl1.Refresh()
        layoutControl1.InitializeText()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub

    Private Sub SetLane(ByVal documentSign As SwDocumentSign, ByVal lane As Integer)
        If lane = 1 Then
            documentSign.Lane = AnchorStyles.Left
        ElseIf lane = 2 Then
            documentSign.Lane = AnchorStyles.None
        ElseIf lane = 3 Then
            documentSign.Lane = AnchorStyles.Right
        End If
    End Sub

    Public Sub AddSWSign(ByVal documentSign As SwDocumentSign)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        DocumentSigns.Add(documentSign)

        Dim layoutControl1 As New SwLayoutControl
        layoutControl1.DocumentSign = documentSign
        MySWFlowLayoutPanel.Controls.Add(layoutControl1)
        MySWFlowLayoutPanel.Controls.Item(MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip = MySWControlMenuStrip
        layoutControl1.Refresh()
        layoutControl1.InitializeText()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub
    Public Sub AddSWSign(ByVal sign As SwSign, ByVal insertIndex As Integer)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        Dim documentSign As SwDocumentSign = ConverSWSignToSWDocumentSign(sign)
        DocumentSigns.Insert(insertIndex, documentSign)

        Dim layoutControl1 As New SwLayoutControl
        layoutControl1.DocumentSign = documentSign
        MySWFlowLayoutPanel.Controls.Add(layoutControl1)
        MySWFlowLayoutPanel.Controls.Item(MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip = MySWControlMenuStrip
        MySWFlowLayoutPanel.Controls.SetChildIndex(layoutControl1, insertIndex)
        layoutControl1.Refresh()
        layoutControl1.InitializeText()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub
    Public Sub AddSWSign(ByVal documentSign As SwDocumentSign, ByVal insertIndex As Integer)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        DocumentSigns.Insert(insertIndex, documentSign)

        Dim layoutControl1 As New SwLayoutControl
        layoutControl1.DocumentSign = documentSign
        MySWFlowLayoutPanel.Controls.Add(layoutControl1)
        MySWFlowLayoutPanel.Controls.Item(MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip = MySWControlMenuStrip
        MySWFlowLayoutPanel.Controls.SetChildIndex(layoutControl1, insertIndex)
        layoutControl1.DocumentSignRefresh()
        layoutControl1.Refresh()
        layoutControl1.InitializeText()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub
    Public Sub Refresh()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 begin
        MySWFlowLayoutPanel.Controls.Clear()
        MySWFlowLayoutPanel.SuspendLayout()
        For Each documentSign As SwDocumentSign In DocumentSigns
            Dim layoutControl1 As New SwLayoutControl

            layoutControl1.DocumentSign = documentSign

            MySWFlowLayoutPanel.Controls.Add(layoutControl1)
            'MySWFlowLayoutPanel.Controls.Item(MySWFlowLayoutPanel.Controls.Count - 1).ContextMenuStrip = MySWControlMenuStrip
            layoutControl1.ContextMenuStrip = MySWControlMenuStrip
            layoutControl1.DocumentSignRefresh()
            layoutControl1.Refresh()
            layoutControl1.InitializeText()
        Next
        MySWFlowLayoutPanel.ResumeLayout()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000900 end
    End Sub
    Public Shared Function ConverSWSignToSWDocumentSign(ByVal sign As SwSign) As SwDocumentSign
        Dim documentSign As New SwDocumentSign
        'Clear frames as a blank one is automatically created.
        documentSign.Frames.Clear()
        If sign IsNot Nothing Then
            documentSign.BkColor = sign.BkColor
            documentSign.CurrentFrameIndex = sign.CurrentFrameIndex
            documentSign.Gloss = sign.Gloss
            documentSign.Glosses = sign.Glosses
            documentSign.SetlanguageIso(sign.LanguageIso)
            documentSign.SetSignLanguageIso(sign.SignLanguageIso)
            documentSign.SignPuddleId = sign.SignPuddleId
            documentSign.SignWriterGuid = sign.SignWriterGuid
            'TODO Needs testing to see if works. Moving frame to new sign.
            '             DocumentSign.Frames = sign.Frames
            For Each Frame In sign.Frames
                documentSign.Frames.Add(Frame)
            Next

        End If

        Return documentSign
    End Function

    Public Sub RemoveSWSign(ByVal layoutControl1 As SwLayoutControl)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000904 begin
        Dim itemIndex As Integer = MySWFlowLayoutPanel.Controls.IndexOf(layoutControl1)
        DocumentSigns.RemoveAt(itemIndex)
        MySWFlowLayoutPanel.Controls.RemoveAt(itemIndex)
        If MySWFlowLayoutPanel.Controls.Count = 0 Then
            MySWFlowLayoutPanel.Focus()
        End If
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000904 end
    End Sub

    Private _disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Private Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                ' free unmanaged resources when explicitly called
                _mySWFlowLayoutPanel.Dispose()
                _mySWControlMenuStrip.Dispose()
                _myForm.Dispose()
            End If

            ' free shared unmanaged resources
        End If
        _disposedValue = True
    End Sub



    Public Sub Clear()
        MySWFlowLayoutPanel.Controls.Clear()
        DocumentSigns.Clear()
    End Sub
#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class