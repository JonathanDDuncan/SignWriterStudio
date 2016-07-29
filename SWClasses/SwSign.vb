Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
'Imports Newtonsoft.Json.Converters
'Imports Newtonsoft.Json
Imports System.Text
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json
Imports UniversalSerializerLib2
Imports Microsoft.VisualBasic


<Serializable()> Public Class SwSign 'Must be serializable to be put on the clipboard
    Implements ICloneable, IDisposable

    ' In this section you can add your own using directives
    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000086A begin
    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000086A end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see SWSign
    '          *       @author Jonathan Duncan
    '          */
    ' Attributes

    Private _bkColor As Color = Color.Transparent
    Public Property BkColor() As Color
        Get
            Return _bkColor
        End Get
        Set(ByVal value As Color)
            _bkColor = value
        End Set
    End Property

    <NonSerialized()> <JsonIgnore()> Private ReadOnly _tauiSignLanguages As New UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
    <NonSerialized()> <JsonIgnore()> Private ReadOnly _taCultures As New UI.swsuiDataSetTableAdapters.UICulturesTableAdapter

    Private _signLanguageIso As String
    Public Property SignLanguageIso() As String
        Get
            Return _signLanguageIso
        End Get
        Set(value As String)
            _signLanguageIso = value
        End Set
    End Property

    Public Sub SetSignLanguageIso(ByVal idSignLanguage As Long)
        Dim iso = UI.Culture.GetSignLanguageIso(idSignLanguage)

        If Not iso = String.Empty Then
            _signLanguageIso = iso
        Else
            _signLanguageIso = Nothing
            Const mbo As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Sign Language not found for: " & idSignLanguage.ToString(CultureInfo.InvariantCulture), "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
        End If
    End Sub
    Public Sub SetSignLanguageIso(ByVal iso6393 As String)
        '        Dim SignLanguageIsoValue As String = CStr(TAUISignLanguages.CheckifSLExistsbyISO(iso6393))
        '        If SignLanguageIsoValue = iso6393 Then
        _signLanguageIso = iso6393
        '        Else
        '            _signLanguageISO = Nothing
        '            Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
        '            MessageBox.Show("Sign Language not found for: " & iso6393, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        '        End If
    End Sub

    Private _languageIso As String
    Public Property LanguageIso() As String
        Get
            Return _languageIso
        End Get
        Set(value As String)
            _languageIso = value
        End Set
    End Property
    Public Sub SetlanguageIso(ByVal idCulture As Long)

        Dim iso = UI.Culture.GetlanguageIso(idCulture)

        If Not iso = String.Empty Then
            _languageIso = iso
        Else
            _languageIso = String.Empty
            Const mbo As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("Language not found for: " & idCulture.ToString(CultureInfo.InvariantCulture), "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
        End If
    End Sub
    Public Sub SetlanguageIso(ByVal cultureName As String)
        '        Dim CultureValue As String = CStr(TACultures.CheckExistsCulturebyName(cultureName))
        '        If CultureValue = cultureName Then
        _languageIso = cultureName
        '        Else
        '            _languageISO = String.Empty
        '            Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
        '            MessageBox.Show("Language not found for: " & cultureName, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        '        End If
    End Sub

    Private _gloss As String
    Public Property Gloss() As String
        Get
            Return _gloss
        End Get
        Set(ByVal value As String)
            _gloss = value
        End Set
    End Property

    Private _glosses As String
    Public Property Glosses() As String
        Get
            Return _glosses
        End Get
        Set(ByVal value As String)
            _glosses = value
        End Set
    End Property

    Private _signPuddleId As String
    Public Property SignPuddleId() As String
        Get
            Return _signPuddleId
        End Get
        Set(ByVal value As String)
            _signPuddleId = value
        End Set
    End Property

    Private _signWriterGuid As Guid?
    Public Property SignWriterGuid() As Guid?
        Get
            Return _signWriterGuid
        End Get
        Set(ByVal value As Guid?)
            _signWriterGuid = value
        End Set
    End Property

    Private _currentFrameIndex As Integer
    Public Property CurrentFrameIndex() As Integer
        Get
            Return _currentFrameIndex
        End Get
        Set(ByVal value As Integer)
            _currentFrameIndex = value
        End Set
    End Property

    Private _frames As New SwCollection(Of SWFrame)()

    Public ReadOnly Property Frames() As SwCollection(Of SWFrame)
        Get
            Return _frames
        End Get
    End Property

    Private _tag As New Object
    Public Property Tag() As Object
        Get
            Return _tag
        End Get
        Set(ByVal value As Object)
            _tag = value
        End Set
    End Property

    Private _sWritingSource As String
    Public Property SWritingSource() As String
        Get
            Return _sWritingSource
        End Get
        Set(ByVal value As String)
            _sWritingSource = value
        End Set
    End Property

    Private _created As New DateTime
    Public Property Created() As DateTime
        Get
            Return _created
        End Get
        Set(ByVal value As DateTime)
            _created = value
        End Set
    End Property

    Private _lastModified As New DateTime
    Public Property LastModified() As DateTime
        Get
            Return _lastModified
        End Get
        Set(ByVal value As DateTime)
            _lastModified = value
        End Set
    End Property

    Private _signPuddleUser As String
    Public Property SignPuddleUser() As String
        Get
            Return _signPuddleUser
        End Get
        Set(ByVal value As String)
            _signPuddleUser = value
        End Set
    End Property

    Private _puddlePrev As String
    Public Property PuddlePrev() As String
        Get
            Return _puddlePrev
        End Get
        Set(ByVal value As String)
            _puddlePrev = value
        End Set
    End Property

    Private _puddleNext As String
    Public Property PuddleNext() As String
        Get
            Return _puddleNext
        End Get
        Set(ByVal value As String)
            _puddleNext = value
        End Set
    End Property

    Private _puddlePng As String
    Public Property PuddlePng() As String
        Get
            Return _puddlePng
        End Get
        Set(ByVal value As String)
            _puddlePng = value
        End Set
    End Property

    Private _puddleSvg As String
    Public Property PuddleSvg() As String
        Get
            Return _puddleSvg
        End Get
        Set(ByVal value As String)
            _puddleSvg = value
        End Set
    End Property

    Private _puddleVideoLink As String
    Public Property PuddleVideoLink() As String
        Get
            Return _puddleVideoLink
        End Get
        Set(ByVal value As String)
            _puddleVideoLink = value
        End Set
    End Property

    Public Property IsPrivate As Boolean

    Public Property PuddleText() As New List(Of String)

    Public Property SortString() As String
    
    ' Operations
    Public Function StackedFrame() As SWFrame
        Dim newFrame As New SWFrame
        Dim newFrameRectangle As Rectangle
        Dim previousHeight As Integer = 0
        Dim offset As Point
        Dim frameBounds As Rectangle
        'Get size of all Frames
        For Each frame As SWFrame In Frames
            frameBounds = frame.GetSWSignBounds(frame)
            'Get height
            newFrameRectangle.Height += frameBounds.Height 'Plus margins
            'Get Max width
            If newFrameRectangle.Width < frameBounds.Width Then
                newFrameRectangle.Width = frameBounds.Width
            End If
        Next
        For Each frame As SWFrame In Frames
            frameBounds = frame.GetSWSignBounds(frame)
            offset.X = CInt((newFrameRectangle.Width - frameBounds.Width) / 2) - frameBounds.X
            offset.Y = previousHeight - frameBounds.Y
            AddSymbolstoStackedFrame(newFrame, frame, offset)
            'Where to start next frame
            previousHeight += frameBounds.Height
        Next
        'Only add sequence from first frame
        Dim sWsequence As SWSequence
        For Each sWsequence In Frames(0).Sequences
            newFrame.Sequences.Add(sWsequence)
        Next

        Return newFrame
    End Function
    Private Shared Sub AddSymbolstoStackedFrame(ByRef resultFrame As SWFrame, ByVal sourceFrame As SWFrame, ByVal offset As Point)
        For Each sWsymbol As SWSignSymbol In sourceFrame.SignSymbols
            sWsymbol = sWsymbol.Clone
            sWsymbol.X += offset.X
            sWsymbol.Y += offset.Y
            resultFrame.SignSymbols.Add(sWsymbol)
        Next

    End Sub
    Public Function Render(ByVal frameIndex As Integer, ByVal paddingWidth As Integer) As Image
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A06 begin
        Return SWDrawing.DrawSWDrawing(Me, frameIndex, paddingWidth, False)
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A06 end
    End Function
    Public Function Render() As Image
        Return Render(-1, 10)
    End Function

    Public Shared Function MakepositiveRect(ByVal rect1 As Rectangle) As Rectangle
        If rect1.Width < 0 Then
            rect1.X = rect1.X + rect1.Width
            rect1.Width = Math.Abs(rect1.Width)
        End If
        If rect1.Height < 0 Then
            rect1.Y = rect1.Y + rect1.Height
            rect1.Height = Math.Abs(rect1.Height)
        End If
        Return rect1
    End Function

    Public Shared Function Changecolor(ByVal originalImage As Image, ByVal fromColor As Color, ByVal toColor As Color) As Image
        ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B32 begin
        'Dim Bitmap1 As New Bitmap(OriginalImage.Width, OriginalImage.Height, PixelFormat.Format32bppPArgb)
        Dim imageAttributes As New Imaging.ImageAttributes()
        Dim g As Graphics

        g.Clear(Color.Transparent)
        Dim colormap As New Imaging.ColorMap

        colormap.OldColor = fromColor
        colormap.NewColor = toColor
        Dim remapTable As Imaging.ColorMap() = {colormap}

        imageAttributes.SetRemapTable(remapTable, Imaging.ColorAdjustType.Bitmap)
        g.DrawImage(originalImage, New Rectangle((originalImage.Height - originalImage.Width) \ 2, (originalImage.Height - originalImage.Height) \ 2, originalImage.Width, originalImage.Height), 0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, imageAttributes)
        Return originalImage
        ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B32 end
    End Function
    Public Sub NextFrame()
        ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B60 begin
        If CurrentFrameIndex = Frames.Count - 1 Then
            Frames.Add(New SWFrame)
            CurrentFrameIndex += 1
        Else
            Frames(CurrentFrameIndex).UnSelectSymbols()
            CurrentFrameIndex += 1
        End If
        ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B60 end
    End Sub
    Public Sub PreviousFrame()
        ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B62 begin
        If Not CurrentFrameIndex <= 0 Then
            Frames(CurrentFrameIndex).UnSelectSymbols()
            CurrentFrameIndex -= 1
        End If
        ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B62 end
    End Sub
    Public Sub RemoveFrame(ByVal frameIndex As Integer)
        Frames.RemoveAt(frameIndex)
    End Sub
    Public Sub SetClipboardCrop(bounds As Rectangle)
        Dim img = Render()
        Clipboard.SetImage(SWDrawing.CropImage(img, bounds))
        img.Dispose()
    End Sub

   
    Public Sub SetClipboard()
        Dim str = Json.SerializeJson(Me)
        Clipboard.SetText(str)
    End Sub

   Public Sub SetClipboardImage()
        Dim img = Render()
        Clipboard.SetImage(img)
        img.Dispose()
    End Sub

    Public Sub OverlapSymbols()
        Dim count As Integer
        Dim sumX As Integer
        Dim sumY As Integer
        Dim avgX As Integer
        Dim avgY As Integer

        For Each symbol As SWSignSymbol In Frames(CurrentFrameIndex).SignSymbols
            If symbol.IsSelected Then
                sumX += symbol.X
                sumY += symbol.Y
                count += 1
            End If
        Next
        If count > 0 Then
            avgX = CInt(sumX / count)
            avgY = CInt(sumY / count)
        End If

        For Each symbol As SWSignSymbol In Frames(CurrentFrameIndex).SignSymbols
            If symbol.IsSelected Then
                symbol.X = avgX
                If symbol.SymbolDetails(False).Category = 3 And symbol.SymbolDetails(False).Group = 5 And symbol.SymbolDetails(False).Symbol >= 3 Then
                    symbol.Y = avgY - 11
                Else
                    symbol.Y = avgY
                End If
            End If
        Next
    End Sub


    Private Function Cloning() As Object Implements ICloneable.Clone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A begin
        Dim swSignClone As SwSign = CType(MemberwiseClone(), SwSign)
        'Created new in the Sign
        '        SWSignClone.Frames = New SWCollection(Of SWFrame)
        swSignClone.NewCollections()
        Dim frame As SWFrame
        For I As Integer = 0 To Frames.Count - 1
            frame = Frames(I)
            If frame IsNot Nothing Then
                swSignClone.Frames.Add(CType(frame.Clone, SWFrame))
            End If
        Next

        Return swSignClone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A end
    End Function
    Public Function Clone() As SwSign
        Return CType(Cloning(), SwSign)
    End Function
    Private Sub NewCollections()
        _frames = New SwCollection(Of SWFrame)
    End Sub

    Public Sub New()
        If Frames.Count = 0 Then
            Frames.Add(New SWFrame)
        End If
    End Sub

    Private _disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                ' free unmanaged resources when explicitly called
                _tauiSignLanguages.Dispose()
                _taCultures.Dispose()
            End If

            ' free shared unmanaged resources
        End If
        _disposedValue = True
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