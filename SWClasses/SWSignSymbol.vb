Option Strict On

Imports System.Drawing
Imports Newtonsoft.Json

Imports SignWriterStudio.SWS

<Serializable()> Public NotInheritable Class SWSignSymbol
    'Inherits SWSymbol
    Implements IEquatable(Of SWSignSymbol), ICloneable, IComparable(Of SWSignSymbol), IDisposable

    ' In this section you can add your own using directives
    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000086C begin
    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000086C end
    ' *
    '          *   A class that represents ...  All rights Reserved Copyright(c) 2008
    '          *
    '          *       @see OtherClasses
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
            _signSymbol = Nothing
            RaiseEvent SymbolChanged(Me, New EventArgs)
        End Set
    End Property

    Private _hand As Integer
    Public Property Hand() As Integer
        Get
            Return _hand
        End Get
        Set(ByVal value As Integer)
            _hand = value
            _signSymbol = Nothing
        End Set
    End Property

    Private _palmcolor As Integer

    Public Property Palmcolor() As Integer
        Get
            Return _palmcolor
        End Get
        Set(ByVal value As Integer)
            _palmcolor = value
            _signSymbol = Nothing
        End Set
    End Property

    Private _handcolor As Integer

    Public Property Handcolor() As Integer
        Get
            Return _handcolor
        End Get
        Set(ByVal value As Integer)
            _handcolor = value
            _signSymbol = Nothing
        End Set
    End Property

    Private _size As Double = 1

    Public Property Size() As Double
        Get
            Return _size
        End Get
        Set(ByVal value As Double)
            _size = value
            _signSymbol = Nothing
        End Set
    End Property

    Private _x As Integer

    Public Property X() As Integer
        Get
            Return _x
        End Get
        Set(ByVal value As Integer)
            _x = value
        End Set
    End Property

    Private _y As Integer

    Public Property Y() As Integer
        Get
            Return _y
        End Get
        Set(ByVal value As Integer)
            _y = value
        End Set
    End Property

    Private _z As Integer

    Public Property Z() As Integer
        Get
            Return _z
        End Get
        Set(ByVal value As Integer)
            _z = value
        End Set
    End Property

    Private _isSelected As Boolean
    <JsonIgnore()>
     Public Property IsSelected() As Boolean
        Get
            Return _isSelected
        End Get
        Set(ByVal value As Boolean)
            _isSelected = value
            _signSymbol = Nothing
        End Set
    End Property


    Private _swSignSymbolGuid As Guid
    <JsonIgnore()>
    Public ReadOnly Property SWSignSymbolGuid() As Guid
        Get
            Return _swSignSymbolGuid
        End Get
    End Property


    Private _signSymbol As Image
    <JsonIgnore()>
     Public ReadOnly Property SignSymbol() As Image
        Get
            Return _signSymbol
        End Get
    End Property
    
    Public Sub SetSignSymbol(ByVal img As Image)
        _signSymbol = img
    End Sub
    ' Associations
    '     *
    '          */
    '     *
    '          */

    ' Operations
    Public Shared Function GuessIfRightorLeft(ByVal fill As Integer) As Integer
        If fill < 9 Then
            'Probably Right hand
            Return 0
        Else
            'Probably Left hand
            Return 1
        End If
    End Function
    Public Function GuessIfRightorLeft() As Integer
        If SymbolDetails(False).Fill < 9 Then
            'Probably Right hand
            Return 0
        Else
            'Probably Left hand
            Return 1
        End If
    End Function
    <JsonIgnore()>
      Public ReadOnly Property StandardColor() As Color
        Get
            Dim symbol As New SWSymbol With {.CodeNotLoadImage = Code}
            Return symbol.StandardColor
        End Get
    End Property
    <JsonIgnore()>
    Public ReadOnly Property SymbolDetails() As SWSymbol
        Get
            Dim details = SWSymbolCache.SWSymbolCache(Code)
            If details IsNot Nothing AndAlso (details.SymImage IsNot Nothing) Then
                Return details
            Else
                Dim swSymbol As SWSymbol = New SWSymbol With {.Code = Code}

                SWSymbolCache.SWSymbolCache(Code) = swSymbol
                Return swSymbol
            End If
        End Get
    End Property
    <JsonIgnore()>
    Public ReadOnly Property SymbolDetails(loadImage As Boolean) As SWSymbol
        Get
            Dim details = SWSymbolCache.SWSymbolCache(Code)
            If details IsNot Nothing AndAlso (details.SymImage IsNot Nothing) = loadImage Then
                Return details
            Else
                Dim swSymbol As SWSymbol
                If loadImage Then
                    swSymbol = New SWSymbol With {.Code = Code}
                Else
                    swSymbol = New SWSymbol With {.CodeNotLoadImage = Code}
                End If


                SWSymbolCache.SWSymbolCache(Code) = swSymbol
                Return swSymbol
            End If
        End Get

    End Property
    Property SearchType() As MatchType
    

    Public Sub MakeSymbolLarger()
        If Size < 2 Then
            Size += 0.03
        End If
    End Sub

    Public Sub MakeSymbolAlotLarger()
        If Size < 2 Then
            Size += 0.3
        End If
    End Sub

    Public Sub MakeSymbolSmaller()

        If Size > 0.06 Then
            Size -= 0.03
        End If
    End Sub
    Public Sub MakeSymbolAlotSmaller()

        If Size > 0.6 Then
            Size -= 0.3
        End If
    End Sub

    Public Event SymbolChanged As EventHandler(Of EventArgs)


    'Public Sub GetImage()
    '    If Not  Code = 0 Then
    '        Dim DR() As SymbolCache.ISWA2010DataSet.cacheRow = SymbolCache.Iswa2010.SC.GetCode( Code)
    '        If DR IsNot Nothing AndAlso DR.Length > 0 Then
    '            SWImage = ByteArrayToImage(DR(0).sym_png)
    '        End If
    '    End If
    'End Sub

    Public Sub New()
        _swSignSymbolGuid = Guid.NewGuid
    End Sub

    Public Shadows Function Equals(ByVal other As SWSignSymbol) As Boolean Implements IEquatable(Of SWSignSymbol).Equals
        If SWSignSymbolGuid = other.SWSignSymbolGuid Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function Cloning() As Object Implements ICloneable.Clone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A begin
        Dim newclone As SWSignSymbol = CType(MemberwiseClone(), SWSignSymbol)
        newclone.SetSignSymbol(SignSymbol)
        newclone._swSignSymbolGuid = Guid.NewGuid
        Return newclone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A end
    End Function
    Public Overloads Function Clone() As SWSignSymbol
        Return CType(Cloning(), SWSignSymbol)
    End Function


    'Private Shared Function CompareByZ(ByVal z1 As Integer, ByVal z2 As Integer) As Integer

    '    '  Z1 and Z2 are not Nothing, compare the
    '    ' two integers
    '    '
    '    Dim retval As Integer = _
    '     Z1.CompareTo(Z2)

    '    If retval <> 0 Then
    '        ' If the strings are not of equal length,
    '        ' the longer string is greater.
    '        '
    '        Return retval
    '    Else
    '        ' If the strings are of equal length,
    '        ' sort them with ordinary string comparison.
    '        '
    '        Return Z1.CompareTo(Z2)
    '    End If

    'End Function
    Public Function CompareTo(ByVal other As SWSignSymbol) As Integer Implements IComparable(Of SWSignSymbol).CompareTo
        Dim retval As Integer = Z.CompareTo(other.Z)

        Return retval
    End Function


    Private _disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Private Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                ' free unmanaged resources when explicitly called
                _signSymbol.Dispose()

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

    Function IsValid() As Boolean

        Return SymbolDetails(False).IsValid
    End Function

End Class