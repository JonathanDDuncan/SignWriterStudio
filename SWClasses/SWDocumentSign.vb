Option Strict On

Imports System.Drawing
Imports Newtonsoft.Json


<Serializable()> Public NotInheritable Class SwDocumentSign
    Inherits SwSign
    Implements ICloneable

    ' In this section you can add your own using directives
    ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A28 begin
    ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A28 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see SWDocument, SWLayoutControl
    '          *       @author Jonathan Duncan
    '          */
    ' Attributes

    Private _framePadding As Integer

    Public Property FramePadding() As Integer
        Get
            Return _framePadding
        End Get
        Set(ByVal value As Integer)
            _framePadding = value
        End Set
    End Property

    Private _begColumn As Boolean
    Public Property BegColumn() As Boolean
        Get
            Return _begColumn
        End Get
        Set(ByVal value As Boolean)
            _begColumn = value
        End Set
    End Property

    Private _isSign As Boolean = True

    Public Property IsSign() As Boolean
        Get
            Return _isSign
        End Get
        Set(ByVal value As Boolean)
            _isSign = value
        End Set
    End Property

    Private _documentImage As Image
    <JsonIgnore()>
  Public Property DocumentImage() As Image
        Get
            Return _documentImage
        End Get
        Set(ByVal value As Image)
            _documentImage = value
        End Set
    End Property
    Public Property DocumentImg() As String
        Get
            Return General.ImageToBase64(_documentImage)
        End Get
        Set(ByVal value As String)
            _documentImage = General.Base64toImage(value)
           
        End Set
    End Property  
   
    Private _lane As Windows.Forms.AnchorStyles
    Public Property Lane() As Windows.Forms.AnchorStyles
        Get
            Return _lane
        End Get
        Set(ByVal value As Windows.Forms.AnchorStyles)
            _lane = value
        End Set
    End Property
    ' Operations
    Public Sub IncorporateSWSign(swSign As SwSign)
        If swSign IsNot Nothing Then
            Frames.Clear()
            Dim frame As SWFrame
            For I As Integer = 0 To swSign.Frames.Count - 1
                frame = swSign.Frames(I)
                If frame IsNot Nothing Then
                    Frames.Add(CType(frame.Clone, SWFrame))
                End If
            Next
            BkColor = swSign.BkColor
            Gloss = swSign.Gloss
            Glosses = swSign.Glosses
            SetSignLanguageIso(swSign.SignLanguageIso)
            SetlanguageIso(swSign.LanguageIso)
            SignWriterGuid = swSign.SignWriterGuid
            SignPuddleId = swSign.SignPuddleId

        End If
    End Sub
    Public Shadows Function Clone() As Object
        Dim newclone As SwDocumentSign = CType(MemberwiseClone(), SwDocumentSign)

        Dim frame As SWFrame
        For I As Integer = 0 To Frames.Count - 1
            frame = Frames(I)
            If frame IsNot Nothing Then
                newclone.Frames.Add(CType(frame.Clone, SWFrame))
            End If
        Next
        Return newclone
    End Function

    Public Function IsPunctuation() As Boolean
        Return (From swFrame In Frames Select swFrame.SignSymbols.Any(Function(symbol) IsPunctuationSymbol(symbol.Code))).FirstOrDefault()
    End Function
    Private Shared Function IsPunctuationSymbol(code As Integer) As Boolean
        Return code >= 62113 AndAlso code <= 62504
    End Function
End Class