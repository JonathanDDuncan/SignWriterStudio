Option Strict On


Imports SignWriterStudio.SWS

<Serializable()> Public NotInheritable Class SWSequence


    Implements IComparable(Of SWSequence), ICloneable
    ' In this section you can add your own using directives
    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B04 begin
    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B04 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see SWFrame
    '          *       @author Jonathan Duncan
    '          */

    ' Attributes

    Private _code As Integer
    Public Property Code() As Integer
        Get
            Return _code
        End Get
        Set(ByVal value As Integer)
            _code = value
        End Set
    End Property

    Private _rank As Integer

    Public Property Rank() As Integer
        Get
            Return _rank
        End Get
        Set(ByVal value As Integer)
            _rank = value
        End Set
    End Property
    Public Sub New(ByVal code As Integer, ByVal rank As Integer)
        Me.Code = code
        Me.Rank = rank
    End Sub
    Public Sub New()

    End Sub
    Public Function CompareTo(ByVal other As SWSequence) As Integer Implements IComparable(Of SWSequence).CompareTo
        Dim retval As Integer = Rank.CompareTo(other.Rank)
        Return retval
    End Function
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim sequenceClone As SWSequence = CType(MemberwiseClone(), SWSequence)
        Return sequenceClone
    End Function

    Public Function IsValid() As Boolean
        Return (New SWSymbol With {.CodeNotLoadImage = Code}).IsValid
    End Function

End Class