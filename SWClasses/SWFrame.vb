Option Strict On

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization
Imports Newtonsoft.Json

Imports SignWriterStudio.General
Imports SignWriterStudio.SWS

<Serializable()> Public NotInheritable Class SWFrame 'Must be serializable to be put on the clipboard
    Implements IEquatable(Of SWFrame), ICloneable, IDisposable

    ' In this section you can add your own using directives
    ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000992 begin
    ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000992 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see SWSign
    '          *       @author Jonathan Duncan
    '          */

    ' Attributes

    Private _frameZ As Integer

    Public Property FrameZ() As Integer
        Get
            Return _frameZ
        End Get
        Set(ByVal value As Integer)
            _frameZ = value
        End Set
    End Property

    Public ReadOnly Property Bounds() As Rectangle
        Get
            Return GetSWSignBounds(Me)
        End Get
    End Property

    Private _minHeight As Integer

    Public Property MinHeight() As Integer
        Get
            Return _minHeight
        End Get
        Set(ByVal value As Integer)
            _minHeight = value
        End Set
    End Property
    Private _minWidth As Integer

    Public Property MinWidth() As Integer
        Get
            Return _minWidth
        End Get
        Set(ByVal value As Integer)
            _minWidth = value
        End Set
    End Property

    Private _cropPoint As Point
    Public Property CropPoint() As Point
        Get
            Return _cropPoint
        End Get
        Set(ByVal value As Point)
            _cropPoint = value
        End Set
    End Property

    Private _selectedSymbolCount As Integer
    Public ReadOnly Property SelectedSymbolCount() As Integer
        Get
            Return _selectedSymbolCount
        End Get

    End Property
    Public Sub ResetSignSymbols()
        For Each symbol As SWSignSymbol In SignSymbols
            symbol.SetSignSymbol(Nothing)
        Next
    End Sub

    Public Shared ReadOnly Property FrameWidth() As Integer
        Get
            Return 500
        End Get
    End Property

    Public Shared ReadOnly Property FrameHeight() As Integer
        Get
            Return 500
        End Get
    End Property

    Private _compareZ As Integer
    <JsonIgnore()>
    Public Property CompareZ() As Integer
        Get
            Return _compareZ
        End Get
        Set(ByVal value As Integer)
            _compareZ = value
        End Set
    End Property

    Private _compareSWSignSymbol As SWSignSymbol
    Public Property CompareSWSignSymbol() As SWSignSymbol
        Get
            Return _compareSWSignSymbol
        End Get
        Set(ByVal value As SWSignSymbol)
            _compareSWSignSymbol = value
        End Set
    End Property

    Private _compareSWSequence As SWSequence

    Public Property CompareSWSequence() As SWSequence
        Get
            Return _compareSWSequence
        End Get
        Set(ByVal value As SWSequence)
            _compareSWSequence = value
        End Set
    End Property

    Private ReadOnly _myGuid As Guid

    Private _signSymbols As New List(Of SWSignSymbol)()
    Public ReadOnly Property SignSymbols() As List(Of SWSignSymbol)
        Get
            Return _signSymbols
        End Get
        'Set(ByVal value As List(Of SWSignSymbol))
        '    _SignSymbols = value
        'End Set
    End Property

    Private _sequences As New List(Of SWSequence)()


    Public ReadOnly Property Sequences() As List(Of SWSequence)
        Get
            Return _sequences
        End Get
        'Set(ByVal value As List(Of SWSequence))
        '    _Sequences = value
        'End Set
    End Property
    ' Operations

    Public Sub MakeSymbolsLarger()
        If SelectedSymbolCount > 0 Then
            For Each symbol As SWSignSymbol In SignSymbols
                If symbol.IsSelected Then
                    symbol.MakeSymbolLarger()
                End If
            Next
        Else
            For Each symbol As SWSignSymbol In SignSymbols
                symbol.MakeSymbolLarger()
            Next
        End If
    End Sub
    Public Sub MakeSymbolsAlotLarger()
        If SelectedSymbolCount > 0 Then
            For Each symbol As SWSignSymbol In SignSymbols
                If symbol.IsSelected Then
                    symbol.MakeSymbolAlotLarger()
                End If
            Next
        Else
            For Each symbol As SWSignSymbol In SignSymbols
                symbol.MakeSymbolAlotLarger()
            Next
        End If
    End Sub

    Public Sub MakeSymbolsSmaller()
        If SelectedSymbolCount > 0 Then
            For Each symbol As SWSignSymbol In SignSymbols
                If symbol.IsSelected Then
                    symbol.MakeSymbolSmaller()
                End If
            Next
        Else
            For Each symbol As SWSignSymbol In SignSymbols
                symbol.MakeSymbolSmaller()
            Next
        End If
    End Sub
    Public Sub MakeSymbolsAlotSmaller()
        If SelectedSymbolCount > 0 Then
            For Each symbol As SWSignSymbol In SignSymbols
                If symbol.IsSelected Then
                    symbol.MakeSymbolAlotSmaller()
                End If
            Next
        Else
            For Each symbol As SWSignSymbol In SignSymbols
                symbol.MakeSymbolAlotSmaller()
            Next
        End If
    End Sub
    Public Sub New()
        _myGuid = Guid.NewGuid()
    End Sub
    Public Function FindSequence(ByVal obj As SWSequence) As Boolean
        If obj Is Nothing Then
            Throw New ArgumentNullException("obj")
        End If
        If CompareSWSequence IsNot Nothing AndAlso obj.Code = CompareSWSequence.Code Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub AddSequenceItem(ByVal symbol As SWSymbol)
        If symbol.IsValid Then
            Sequences.Add(New SWSequence(symbol.Code, GetLastSequenceRank(Me) + 1))
        End If
    End Sub
    Public Sub AddSequenceSelectedItems()
        For Each symbol As SWSignSymbol In SignSymbols
            If symbol.IsSelected Then
                Sequences.Add(New SWSequence(symbol.Code, GetLastSequenceRank(Me) + 1))
            End If
        Next
    End Sub

    Public Sub MoveSequenceUp(ByVal index As Integer)

        If Not index = 0 Then
            Dim sequence As SWSequence = Sequences.Item(index)
            Sequences.Sort(AddressOf CompareSequencesByRank)
            Sequences.Remove(sequence)
            Sequences.Insert(index - 1, sequence)
            RenumberSequenceRank()

        End If
    End Sub
    Public Sub MoveSequenceDown(ByVal index As Integer)
        If Not index = Sequences.Count - 1 Then
            Dim sequence As SWSequence = Sequences.Item(index)
            Sequences.Sort(AddressOf CompareSequencesByRank)
            Sequences.Remove(sequence)
            Sequences.Insert(index + 1, sequence)
            RenumberSequenceRank()

        End If
    End Sub

    Public Shared Function GetLastSequenceRank(ByVal frame As SWFrame) As Integer

        Return (From sequence1 In frame.Sequences Select sequence1.Rank).Concat(New Integer() {0}).Max()
    End Function
    Public Sub RenumberSequenceRank()
        Dim sequence As SWSequence
        Dim I As Integer = 0

        For Each sequence In Sequences
            I += 1
            sequence.Rank = I
        Next
    End Sub
    Private Shared Function CompareSequencesByRank(
      ByVal x As SWSequence, ByVal y As SWSequence) As Integer


        If x Is Nothing Then
            If y Is Nothing Then
                ' If x is Nothing and y is Nothing, they're
                ' equal. 
                Return 0
            Else
                ' If x is Nothing and y is not Nothing, y
                ' is greater. 
                Return -1
            End If
        Else
            ' If x is not Nothing...
            '
            If y Is Nothing Then
                ' ...and y is Nothing, x is greater.
                Return 1
            Else
                ' ...and y is not Nothing, compare the 
                ' lengths of the two strings.
                '
                Dim retval As Integer =
                    x.Rank.CompareTo(y.Rank)

                If retval <> 0 Then
                    ' If the strings are not of equal length,
                    ' the longer string is greater.
                    '
                    Return retval
                Else
                    ' If the strings are of equal length,
                    ' sort them with ordinary string comparison.
                    '
                    Return x.CompareTo(y)
                End If
            End If
        End If

    End Function
    Private Shared Function CompareSignSymbolsByZ(
   ByVal x As SWSignSymbol, ByVal y As SWSignSymbol) As Integer


        If x Is Nothing Then
            If y Is Nothing Then
                ' If x is Nothing and y is Nothing, they're
                ' equal. 
                Return 0
            Else
                ' If x is Nothing and y is not Nothing, y
                ' is greater. 
                Return -1
            End If
        Else
            ' If x is not Nothing...
            '
            If y Is Nothing Then
                ' ...and y is Nothing, x is greater.
                Return 1
            Else
                ' ...and y is not Nothing, compare the 
                ' lengths of the two strings.
                '
                Dim retval As Integer =
                    x.Z.CompareTo(y.Z)

                If retval <> 0 Then
                    ' If the strings are not of equal length,
                    ' the longer string is greater.
                    '
                    Return retval
                Else
                    ' If the strings are of equal length,
                    ' sort them with ordinary string comparison.
                    '
                    Return x.CompareTo(y)
                End If
            End If
        End If

    End Function
    Public Shared Sub LoadSequence(ByVal tv As TreeView, ByVal frame As SWFrame, cm As ContextMenuStrip)
        Dim newNode As TreeNode
        Dim imgList As New ImageList
        Dim currSeq As SWSequence
        Dim symbol As New SWSignSymbol
        Const imageHeight As Integer = 50
        Const imageWidth As Integer = 55
        frame.Sequences.Sort(AddressOf CompareSequencesByRank)
        imgList.ImageSize = New Size(imageWidth, imageHeight)
        'CreateSequenceImageList
        For I As Integer = 0 To frame.Sequences.Count - 1
            currSeq = frame.Sequences(I)
            If Not currSeq.Code = 0 Then
                imgList = SWDrawing.AddImagestoImageList(imgList, currSeq.Code, imageWidth, imageHeight, Color.OrangeRed)
            End If
        Next

        tv.BeginUpdate()
        tv.Nodes.Clear()
        tv.ImageList = imgList

        'CreateNodes
        For I As Integer = 0 To frame.Sequences.Count - 1
            currSeq = frame.Sequences(I)
            newNode = New TreeNode

            symbol.Code = currSeq.Code
            'Symbol.Update()

            newNode.Text = symbol.SymbolDetails(False).BaseName
            newNode.SelectedImageKey = "S" & symbol.SymbolDetails(False).Id
            newNode.ImageKey = symbol.SymbolDetails(False).Id

            newNode.Name = symbol.Code.ToString(CultureInfo.InvariantCulture)
            'NewNode.ToolTipText = String.Empty
            newNode.ContextMenuStrip = cm


            tv.Nodes.Add(newNode)
        Next
        tv.EndUpdate()
    End Sub

    Public Shadows Function Equals(ByVal other As SWFrame) As Boolean Implements IEquatable(Of SWFrame).Equals
        If _myGuid = other._myGuid Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub UnSelectSymbols()
        Dim eachSymbol As SWSignSymbol
        If SignSymbols.Count > 0 Then
            For Each eachSymbol In SignSymbols
                eachSymbol.IsSelected = False
            Next
        End If
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    Public Function CountSelectedSymbols() As Integer

        Dim selectedSymbols = From eachSymbol1 In SignSymbols Where eachSymbol1.IsSelected = True

        _selectedSymbolCount = selectedSymbols.Count()
        Return _selectedSymbolCount

    End Function

    Public Sub InsertSymbolIntoSign(ByVal code As Integer, ByVal isSelected As Boolean, ByVal x As Integer, ByVal y As Integer, ByVal handColor As Color, ByVal palmColor As Color, ByVal hand As Integer)
        Dim newSymbol As New SWSignSymbol With {.Code = code}

        newSymbol.IsSelected = isSelected
        newSymbol.X = x
        newSymbol.Y = y
        newSymbol.Handcolor = handColor.ToArgb
        newSymbol.Palmcolor = palmColor.ToArgb
        newSymbol.Hand = hand
        newSymbol.Z = GetLastZ(Me) + 1
        SignSymbols.Add(newSymbol)


        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    Public Sub RemoveSelected()
        Dim I As Integer
        Dim symbol As SWSignSymbol
        For I = 0 To SignSymbols.Count - 1
            symbol = SignSymbols(I)

            If I < SignSymbols.Count - 1 Then
                If symbol.IsSelected Then
                    SignSymbols.RemoveAt(I)
                    I = I - 1

                End If

            ElseIf SignSymbols.Count > 0 Then
                I = SignSymbols.Count - 1
                If symbol.IsSelected Then
                    SignSymbols.RemoveAt(I)
                End If
                Exit For
            End If
        Next I
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    Public Sub RemoveAll()
        'Dim symbol As SWSignSymbol
        'Dim allSymbols As List(Of SWSignSymbol) = 

        'For Each symbol In allSymbols
        '    SignSymbols.Remove(symbol)
        'Next
        SignSymbols.RemoveAll(AddressOf All)

        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    'TODO Changed ByReg to ByVal Check if still works
    Public Sub SelectSymbol(ByVal symbol As SWSignSymbol)
        If symbol Is Nothing Then
            Throw New ArgumentNullException("symbol")
        End If
        CompareSWSignSymbol = symbol
        symbol = SignSymbols.Find(AddressOf FindSymbol)
        If symbol IsNot Nothing Then
            symbol.IsSelected = True
        End If
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    'TODO Changed ByReg to ByVal Check if still works
    Public Sub UNSelectSymbol(ByVal symbol As SWSignSymbol)

        If symbol Is Nothing Then
            Throw New ArgumentNullException("symbol")
        End If
        symbol = SignSymbols.Find(AddressOf FindSymbol)
        symbol.IsSelected = False
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    Public Sub SelectSymbol(ByRef symbolOutdex As Integer)
        SignSymbols(symbolOutdex).IsSelected = True
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub


    Public Sub UnSelectSymbol(ByRef symbolOutdex As Integer)
        SignSymbols(symbolOutdex).IsSelected = False
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub

    Public Function ZGreater(ByVal obj As SWSignSymbol) As Boolean
        If obj Is Nothing Then
            Throw New ArgumentNullException("obj")
        End If
        Return obj.Z > CompareZ
    End Function
    Public Function ZLesser(ByVal obj As SWSignSymbol) As Boolean
        If obj Is Nothing Then
            Throw New ArgumentNullException("obj")
        End If
        Return obj.Z < CompareZ
    End Function
    Friend Function FindZ(ByVal obj As SWSignSymbol) As Boolean
        If obj Is Nothing Then
            Throw New ArgumentNullException("obj")
        End If
        Return obj.Z = CompareZ
    End Function
    Friend Function FindSymbol(ByVal obj As SWSignSymbol) As Boolean
        If CompareSWSignSymbol IsNot Nothing AndAlso obj.SWSignSymbolGuid = CompareSWSignSymbol.SWSignSymbolGuid Then
            Return True
        Else
            Return False
        End If
    End Function

    Sub SelectAll()
        For Each Symbol In SignSymbols
            Symbol.IsSelected = True
        Next
        CountSelectedSymbols()
    End Sub

    Public Sub SelectPreviousSymbol()
        RenumberZ()
        Dim max As Integer
        max = SignSymbols.Count
        Dim currentPreviousSelected As Integer
        'Starting point
        If SelectedSymbolCount > 0 Then
            currentPreviousSelected = FirstSelected()
        Else
            currentPreviousSelected = max
        End If
        UnSelectSymbols()
        Dim symbol As SWSignSymbol
        CompareZ = PreviousSymbolZ(currentPreviousSelected, max)
        symbol = SignSymbols.Find(AddressOf FindZ)
        SelectSymbol(symbol)
    End Sub
    Public Sub SelectPreviousAddSymbol()
        RenumberZ()
        Dim max As Integer
        max = SignSymbols.Count
        Dim currentLastSelected As Integer
        If SelectedSymbolCount > 0 Then
            currentLastSelected = LastSelected()
        Else
            currentLastSelected = GetLastZ(Me)
        End If
        Dim symbol As SWSignSymbol
        CompareZ = PreviousSymbolZ(currentLastSelected, max)
        symbol = SignSymbols.Find(AddressOf FindZ)

        If symbol IsNot Nothing Then
            Do While symbol.IsSelected AndAlso Not symbol.Z = currentLastSelected
                CompareZ = PreviousSymbolZ(symbol.Z, max)
                symbol = SignSymbols.Find(AddressOf FindZ)
            Loop
            If symbol IsNot Nothing Then
                SelectSymbol(symbol)
            End If
        End If
    End Sub
    Private Shared Function NextSymbolZ(ByVal curr As Integer, ByVal max As Integer) As Integer
        If curr + 1 > max Then
            Return 1
        Else
            Return curr + 1
        End If
    End Function
    Private Shared Function PreviousSymbolZ(ByVal curr As Integer, ByVal max As Integer) As Integer
        If curr - 1 < 1 Then
            Return max
        Else
            Return curr - 1
        End If
    End Function
    Public Sub SelectNextSymbol()
        RenumberZ()
        Dim max As Integer = SignSymbols.Count
        Dim currentLastSelected As Integer
        If SelectedSymbolCount > 0 Then
            currentLastSelected = LastSelected()
        Else
            currentLastSelected = GetLastZ(Me)
        End If
        UnSelectSymbols()
        Dim symbol As SWSignSymbol
        CompareZ = NextSymbolZ(currentLastSelected, max)
        symbol = SignSymbols.Find(AddressOf FindZ)
        If symbol IsNot Nothing Then
            SelectSymbol(symbol)
        End If
    End Sub
    Public Sub SelectNextAddSymbol()
        RenumberZ()
        Dim max As Integer = SignSymbols.Count
        Dim currentLastSelected As Integer
        If SelectedSymbolCount > 0 Then
            currentLastSelected = LastSelected()
        Else
            currentLastSelected = GetLastZ(Me)
        End If
        Dim symbol As SWSignSymbol
        CompareZ = NextSymbolZ(currentLastSelected, max)
        symbol = SignSymbols.Find(AddressOf FindZ)

        If symbol IsNot Nothing Then
            Do While symbol.IsSelected AndAlso Not symbol.Z = currentLastSelected
                CompareZ = NextSymbolZ(symbol.Z, max)
                symbol = SignSymbols.Find(AddressOf FindZ)
            Loop
            If symbol IsNot Nothing Then
                SelectSymbol(symbol)
            End If
        End If
    End Sub
    Public Function LastSelected() As Integer
        Dim lastIndex As Integer
        Return (From symbol In SignSymbols Where symbol.IsSelected Select symbol.Z).Concat(New Integer() {lastIndex}).Max()
    End Function

    Public Function FirstSelected() As Integer
        Return (From symbol In SignSymbols Where symbol.IsSelected Select symbol.Z).Concat(New Integer() {SignSymbols.Count}).Min()
    End Function
    Public Sub RenumberZ()

        Dim sortedSymbols = SignSymbols.OrderBy(Function(x) x.Z).ToList()


        Dim I As Integer = 0

        For Each symbol As SWSignSymbol In sortedSymbols
            I += 1
            symbol.Z = I
        Next
    End Sub
    Public Sub MoveSelectionToRegion(ByVal frameRegion As Integer)
        Dim localBounds As Rectangle = GetSWSignBoundsSelected(Me)
        Dim toPoint As New Point
        Dim fromPoint As New Point
        Dim offset As New Size
        If frameRegion = 7 Then

            toPoint.X = CInt(FrameWidth / 6)
            toPoint.Y = CInt(FrameHeight / 6)
        ElseIf frameRegion = 8 Then

            toPoint.X = CInt(FrameWidth / 2 - localBounds.Width / 2)
            toPoint.Y = CInt(FrameWidth / 6)
        ElseIf frameRegion = 9 Then


            toPoint.X = CInt(FrameWidth - localBounds.Width - FrameWidth / 6)
            toPoint.Y = CInt(FrameWidth / 6)

        ElseIf frameRegion = 6 Then

            toPoint.X = CInt(FrameWidth - localBounds.Width - FrameHeight / 6)
            toPoint.Y = CInt(FrameHeight / 2 - localBounds.Height / 2)

        ElseIf frameRegion = 3 Then

            toPoint.X = CInt(FrameWidth - localBounds.Width - FrameWidth / 6)
            toPoint.Y = CInt(FrameHeight - localBounds.Height - FrameHeight / 6)

        ElseIf frameRegion = 5 Then

            toPoint.X = CInt(FrameWidth / 2 - localBounds.Width / 2)
            toPoint.Y = CInt(FrameHeight / 2 - localBounds.Height / 2)

        ElseIf frameRegion = 2 Then

            toPoint.X = CInt(FrameWidth / 2 - localBounds.Width / 2)
            toPoint.Y = CInt(FrameHeight - localBounds.Height - FrameHeight / 6)

        ElseIf frameRegion = 4 Then

            toPoint.X = CInt(FrameWidth / 6)
            toPoint.Y = CInt(FrameHeight / 2 - localBounds.Height / 2)

        ElseIf frameRegion = 1 Then

            toPoint.X = CInt(FrameWidth / 6)
            toPoint.Y = CInt(FrameHeight - localBounds.Height - FrameHeight / 6)

        End If
        fromPoint.X = localBounds.X
        fromPoint.Y = localBounds.Y
        'Get offset
        offset = New Size(toPoint.X - fromPoint.X, toPoint.Y - fromPoint.Y)
        'Move each selected item by x,y
        For Each symbol As SWSignSymbol In SignSymbols
            If symbol.IsSelected Then
                symbol.X += offset.Width
                symbol.Y += offset.Height
            End If
        Next
    End Sub
    Public Shared Function GetLastZ(ByVal frame As SWFrame) As Integer

        Return (From eachSs In frame.SignSymbols Select eachSs.Z).Concat(New Integer() {0}).Max()
    End Function
    Public Shared Function GetFirstZ(ByVal frame As SWFrame) As Integer
        Dim firstZ As Integer = frame.SignSymbols.Count

        Return (From eachSs In frame.SignSymbols Select eachSs.Z).Concat(New Integer() {firstZ}).Min()
    End Function
    Public Sub CenterSymbols()
        Dim signBounds As Rectangle = GetSWSignBounds(Me)
        Dim offset As New Size(CInt(signBounds.X - (FrameWidth - signBounds.Width) / 2), CInt(signBounds.Y - (FrameHeight - signBounds.Height) / 2))
        Dim symbol As SWSignSymbol

        If SelectedSymbolCount > 0 Then
            For Each symbol In SignSymbols
                If symbol.IsSelected Then
                    symbol.X -= offset.Width
                    symbol.Y -= offset.Height
                End If
            Next
        Else
            For Each symbol In SignSymbols
                symbol.X -= offset.Width
                symbol.Y -= offset.Height
            Next
        End If
    End Sub
    Public Sub CenterSpmlSymbols(centeron As Point)


        Dim initialBounds = GetMaxBounds(Me)
        Dim initialCenter As Point = New Point(CInt(initialBounds.X + initialBounds.Width / 2), CInt(initialBounds.Y + initialBounds.Height / 2))
        Dim offset As Point = New Point(centeron.X - initialCenter.X, centeron.Y - initialCenter.Y)

        For Each symb In SignSymbols
            symb.X = symb.X + offset.X
            symb.Y = symb.Y + offset.Y
        Next

    End Sub

    Public Sub CenterHeadinSign()
        If SelectedSymbolCount > 0 Then
            Dim signBounds As Rectangle = GetSWSignBounds(Me)
            Dim headBounds As Rectangle
            Dim offset As New Size(CInt(signBounds.X - (FrameWidth - signBounds.Width) / 2), CInt(signBounds.Y - (FrameHeight - signBounds.Height) / 2))
            Dim symbol As SWSignSymbol
            For Each symbol In SignSymbols
                If symbol.SymbolDetails(False).Category = 3 OrElse symbol.SymbolDetails(False).Category = 4 Then
                    headBounds = New Rectangle(symbol.X, symbol.Y, symbol.SymbolDetails(False).Width, symbol.SymbolDetails(False).Height)
                    offset = New Size(CInt(headBounds.X - (FrameWidth - headBounds.Width) / 2), offset.Height + 25)
                    Dim symbol1 As SWSignSymbol
                    For Each symbol1 In SignSymbols
                        If symbol.IsSelected Then
                            symbol1.X -= offset.Width
                            symbol1.Y -= offset.Height
                        End If
                    Next
                    Exit For
                End If
            Next
        Else
            Dim signBounds As Rectangle = GetSWSignBounds(Me)
            Dim headBounds As Rectangle
            Dim offset As New Size(CInt(signBounds.X - (FrameWidth - signBounds.Width) / 2), CInt(signBounds.Y - (FrameHeight - signBounds.Height) / 2))
            Dim symbol As SWSignSymbol
            For Each symbol In SignSymbols
                If symbol.SymbolDetails(False).Category = 3 OrElse symbol.SymbolDetails(False).Category = 4 Then
                    headBounds = New Rectangle(symbol.X, symbol.Y, symbol.SymbolDetails(False).Width, symbol.SymbolDetails(False).Height)
                    offset = New Size(CInt(headBounds.X - (FrameWidth - headBounds.Width) / 2), offset.Height + 25)
                    Dim symbol1 As SWSignSymbol
                    For Each symbol1 In SignSymbols

                        symbol1.X -= offset.Width
                        symbol1.Y -= offset.Height
                    Next
                    Exit For
                End If
            Next
        End If
    End Sub
    Public Sub MoveSymbolDown()
        Dim symbol As SWSignSymbol
        RenumberZ()
        For Each symbol In SignSymbols

            If symbol.IsSelected Then

                CompareZ = symbol.Z - 1
                Dim foundSymbol As SWSignSymbol =
                        SignSymbols.Find(AddressOf FindZ)
                If foundSymbol IsNot Nothing Then
                    foundSymbol.Z = symbol.Z
                    symbol.Z -= 1
                End If

            End If
        Next
    End Sub
    Public Sub MoveSymbolBottom()
        Dim symbol As SWSignSymbol
        RenumberZ()
        For Each symbol In SignSymbols

            If symbol.IsSelected Then

                symbol.Z = -1
                Exit For

            End If
        Next
        RenumberZ()
    End Sub
    Public Sub MoveSymbolTop()
        Dim symbol As SWSignSymbol
        RenumberZ()
        For Each symbol In SignSymbols

            If symbol.IsSelected Then

                symbol.Z = SignSymbols.Count + 1

                Exit For
            End If
        Next
        RenumberZ()
    End Sub
    Public Sub MoveSymbolUp()
        Dim symbol As SWSignSymbol
        RenumberZ()
        For Each symbol In SignSymbols
            If symbol.IsSelected Then
                'Move found row down
                CompareZ = symbol.Z + 1
                Dim foundSymbol As SWSignSymbol =
                        SignSymbols.Find(AddressOf FindZ)
                If foundSymbol IsNot Nothing Then
                    foundSymbol.Z = symbol.Z
                    symbol.Z += 1
                End If
            End If
        Next
    End Sub
    Public Sub DuplicateSelected(offset As Integer)
        Dim symbol As SWSignSymbol
        Dim newSymbol As SWSignSymbol
        Dim changeSymbolIns As List(Of SWSignSymbol) = CType(SignSymbols.FindAll(AddressOf Selected), List(Of SWSignSymbol))

        For Each symbol In changeSymbolIns
            newSymbol = symbol.Clone
            newSymbol.X += offset
            newSymbol.Y += offset
            newSymbol.IsSelected = True
            symbol.IsSelected = False
            SignSymbols.Add(newSymbol)
        Next
        ResetSignSymbols()
    End Sub
    Public Sub DuplicateAll(offset As Integer)
        Dim symbol As SWSignSymbol
        Dim newSymbol As SWSignSymbol

        Dim duplicatedList As New List(Of SWSignSymbol)

        For Each symbol In SignSymbols
            newSymbol = symbol.Clone
            newSymbol.X += offset
            newSymbol.Y += offset
            newSymbol.IsSelected = True
            symbol.IsSelected = False
            duplicatedList.Add(newSymbol)
        Next
        For Each symbol In duplicatedList
            SignSymbols.Add(symbol)
        Next
        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub
    Public Sub MoveSelected(ByVal dir As ArrowDirection, ByVal dist As Integer)
        'Dim Symbol As SWSignSymbol
        'For Each Symbol In  SignSymbols
        'If Symbol.IsSelected Then
        Select Case dir
            Case ArrowDirection.Down
                MoveSelected(New Point(0, dist))
                'Symbol.Y += dist
                'If Symbol.Y + Symbol.SymbolDetails.Height >  FrameHeight Then
                '    Symbol.Y =  FrameHeight - Symbol.SymbolDetails.Height
                'End If
            Case ArrowDirection.Left
                MoveSelected(New Point(-dist, 0))
                'Symbol.X -= dist
                'If Symbol.X < 0 Then
                '    Symbol.X = 0
                'End If
            Case ArrowDirection.Right
                MoveSelected(New Point(dist, 0))
                'Symbol.X += dist
                'If Symbol.X + Symbol.SymbolDetails.Width >  FrameWidth Then
                '    Symbol.X =  FrameWidth - Symbol.SymbolDetails.Width
                'End If
            Case ArrowDirection.Up
                MoveSelected(New Point(0, -dist))
                'Symbol.Y -= dist
                'If Symbol.Y < 0 Then
                '    Symbol.Y = 0
                'End If
        End Select
        'End If
        'Next
    End Sub
    Public Sub MoveSelected(ByVal offset As Point)
        Dim symbol As SWSignSymbol
        Dim selectedBounds = GetSWSignBoundsSelected(Me)
        Dim offsetToMove As Point = offset
        'Limit offset to not move sign out of sign box
        'Not move past X= 0
        If selectedBounds.X + offset.X < 0 Then
            offsetToMove.X = -selectedBounds.X
        End If
        'Not move past X = SWFrame.FrameWidth
        If selectedBounds.X + selectedBounds.Width + offset.X > FrameWidth Then
            offsetToMove.X = FrameWidth - (selectedBounds.X + selectedBounds.Width)
        End If

        'Not move past Y= 0
        If selectedBounds.Y + offset.Y < 0 Then
            offsetToMove.Y = -selectedBounds.Y
        End If
        'Not move past Y = SWFrame.FrameHeight
        If selectedBounds.Y + selectedBounds.Height + offset.Y > FrameHeight Then
            offsetToMove.Y = FrameHeight - (selectedBounds.Y + selectedBounds.Height)
        End If

        For Each symbol2 In From symbol1 In SignSymbols Where symbol1.IsSelected
            symbol2.X += offsetToMove.X
            symbol2.Y += offsetToMove.Y
        Next
    End Sub
    Public Function GetSWSignBounds(ByVal frame As SWFrame) As Rectangle
        Dim x1 As Integer = Integer.MaxValue
        Dim x2 As Integer = 0
        Dim y1 As Integer = Integer.MaxValue
        Dim y2 As Integer = 0

        Dim symbol As SWSignSymbol
        Dim symbolDetails As SWSymbol
        For Each symbol In frame.SignSymbols
            If symbol.X <= x1 Then
                x1 = symbol.X
            End If
            If symbol.Y <= y1 Then
                y1 = symbol.Y
            End If
            symbolDetails = symbol.SymbolDetails(False)
            If Not IsDbNull(symbolDetails.Width) AndAlso symbol.X + symbolDetails.Width >= x2 Then
                x2 = symbol.X + symbolDetails.Width
            End If
            If Not IsDbNull(symbolDetails.Height) AndAlso symbol.Y + symbolDetails.Height >= y2 Then
                y2 = symbol.Y + symbolDetails.Height
            End If
        Next
        If x1 = Integer.MaxValue Then
            x1 = 0
        End If
        If y1 = Integer.MaxValue Then
            y1 = 0
        End If
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function
    Public Shared Function GetMaxBounds(ByVal frame As SWFrame) As Rectangle

        Dim maxBounds As New Rectangle

        Dim headBounds = GetHeadBounds(frame)
        Dim headTrunkBounds = GetHeadTrunkBounds(frame)
        Dim allSymbolsBounds = GetAllSymbolsBounds(frame)

        If Not headTrunkBounds.Width = 0 Then
            maxBounds.X = headTrunkBounds.X
            maxBounds.Width = headTrunkBounds.Width
        ElseIf Not headBounds.Width = 0 Then
            maxBounds.X = headBounds.X
            maxBounds.Width = headBounds.Width

        Else
            maxBounds.X = allSymbolsBounds.X
            maxBounds.Width = allSymbolsBounds.Width
        End If


        If Not headBounds.Width = 0 Then
            maxBounds.Height = headBounds.Height
            maxBounds.Y = headBounds.Y
        Else
            maxBounds.Height = allSymbolsBounds.Height
            maxBounds.Y = allSymbolsBounds.Y
        End If

        Return maxBounds
    End Function
    Public Shared Function GetLayoutBounds(ByVal frame As SWFrame) As Rectangle

        Dim headBounds = GetHeadBounds(frame)
        Dim headTrunkBounds = GetHeadTrunkBounds(frame)
        Dim allSymbolsBounds = GetAllSymbolsBounds(frame)

        If Not headTrunkBounds.Width = 0 Then
            Return headTrunkBounds
        ElseIf Not headBounds.Width = 0 Then
            Return headBounds
        Else
            Return allSymbolsBounds
        End If


    End Function
    Public Shared Function GetCenterToLeftEdge(ByVal frame As SWFrame) As Integer
        Dim layoutBound As Rectangle
        Dim headBounds = GetHeadBounds(frame)
        Dim headTrunkBounds = GetHeadTrunkBounds(frame)
        Dim allSymbolsBounds = GetAllSymbolsBounds(frame)

        If Not headTrunkBounds.Width = 0 Then
            layoutBound = headTrunkBounds
        ElseIf Not headBounds.Width = 0 Then
            layoutBound = headBounds
        Else
            layoutBound = allSymbolsBounds
        End If

        Return CType((layoutBound.Width) / 2, Integer) + (layoutBound.X - allSymbolsBounds.X)


    End Function
    Public Shared Function GetMaxCoordinate(ByVal frame As SWFrame) As Point
        Dim maxBounds = GetAllSymbolsBounds(frame)

        Return New Point(maxBounds.X + maxBounds.Width, maxBounds.Y + maxBounds.Height)
    End Function
    Public Shared Function GetHeadBounds(ByVal frame As SWFrame) As Rectangle
        Dim x1 As Integer = Integer.MaxValue
        Dim x2 As Integer = 0
        Dim y1 As Integer = Integer.MaxValue
        Dim y2 As Integer = 0

        Dim symbol As SWSignSymbol
        Dim symbolDetails As SWSymbol
        For Each symbol In frame.SignSymbols
            If symbol.SymbolDetails(False).Category = 4 Then 'Head
                If symbol.X <= x1 Then
                    x1 = symbol.X
                End If
                If symbol.Y <= y1 Then
                    y1 = symbol.Y
                End If
                symbolDetails = symbol.SymbolDetails(False)
                If Not IsDbNull(symbolDetails.Width) AndAlso symbol.X + symbolDetails.Width >= x2 Then
                    x2 = symbol.X + symbolDetails.Width
                End If
                If Not IsDbNull(symbolDetails.Height) AndAlso symbol.Y + symbolDetails.Height >= y2 Then
                    y2 = symbol.Y + symbolDetails.Height
                End If
            End If
        Next



        If x1 = Integer.MaxValue Then
            x1 = 0
        End If
        If y1 = Integer.MaxValue Then
            y1 = 0
        End If
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function
    Public Shared Function GetHeadTrunkBounds(ByVal frame As SWFrame) As Rectangle
        Dim x1 As Integer = Integer.MaxValue
        Dim x2 As Integer = 0
        Dim y1 As Integer = Integer.MaxValue
        Dim y2 As Integer = 0

        Dim symbol As SWSignSymbol
        Dim symbolDetails As SWSymbol
        For Each symbol In frame.SignSymbols
            symbolDetails = symbol.SymbolDetails(False)
            If symbolDetails.Category = 4 OrElse symbolDetails.Category = 5 Then 'Head or Trunk
                If symbol.X <= x1 Then
                    x1 = symbol.X
                End If
                If symbol.Y <= y1 Then
                    y1 = symbol.Y
                End If

                If Not IsDbNull(symbolDetails.Width) AndAlso symbol.X + symbolDetails.Width >= x2 Then
                    x2 = symbol.X + symbolDetails.Width
                End If
                If Not IsDbNull(symbolDetails.Height) AndAlso symbol.Y + symbolDetails.Height >= y2 Then
                    y2 = symbol.Y + symbolDetails.Height
                End If
            End If
        Next



        If x1 = Integer.MaxValue Then
            x1 = 0
        End If
        If y1 = Integer.MaxValue Then
            y1 = 0
        End If
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function

    Public Shared Function GetAllSymbolsBounds(ByVal frame As SWFrame) As Rectangle
        Dim x1 As Integer = Integer.MaxValue
        Dim x2 As Integer = 0
        Dim y1 As Integer = Integer.MaxValue
        Dim y2 As Integer = 0

        Dim symbol As SWSignSymbol
        Dim symbolDetails As SWSymbol
        For Each symbol In frame.SignSymbols
            symbolDetails = symbol.SymbolDetails(False)
            If symbol.X <= x1 Then
                x1 = symbol.X
            End If
            If symbol.Y <= y1 Then
                y1 = symbol.Y
            End If

            If Not IsDbNull(symbolDetails.Width) AndAlso symbol.X + symbolDetails.Width >= x2 Then
                x2 = symbol.X + symbolDetails.Width
            End If
            If Not IsDbNull(symbolDetails.Height) AndAlso symbol.Y + symbolDetails.Height >= y2 Then
                y2 = symbol.Y + symbolDetails.Height
            End If

        Next



        If x1 = Integer.MaxValue Then
            x1 = 0
        End If
        If y1 = Integer.MaxValue Then
            y1 = 0
        End If
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function
    'Private Shared Function ContainsHeadSymbols(frame As SWFrame) As Boolean

    '    For Each Symbol In frame.SignSymbols
    '        If Symbol.SymbolDetails(False).Category = 4 Then 'Head
    '            Return True
    '        End If
    '    Next
    '    Return False
    'End Function

    'Private Shared Function ContainsTrunkSymbols(frame As SWFrame) As Boolean
    '    For Each Symbol In frame.SignSymbols
    '        If Symbol.SymbolDetails(False).Category = 5 Then 'Trunk
    '            Return True
    '        End If
    '    Next
    '    Return False
    'End Function
    Public Function GetSymbolBounds(ByVal frame As SWFrame) As Rectangle
        Dim x1 As Integer = 0
        Dim x2 As Integer = FrameWidth
        Dim y1 As Integer = 0
        Dim y2 As Integer = FrameHeight

        Dim symbol As SWSignSymbol
        For Each symbol In frame.SignSymbols
            If symbol.X <= x1 Then
                x1 = symbol.X
            End If
            If symbol.Y <= y1 Then
                y1 = symbol.Y
            End If

            If Not IsDbNull(symbol.SymbolDetails(False).Width) AndAlso symbol.X + symbol.SymbolDetails(False).Width >= x2 Then
                x2 = symbol.X + symbol.SymbolDetails(False).Width
            End If
            If Not IsDbNull(symbol.SymbolDetails(False).Height) AndAlso symbol.Y + symbol.SymbolDetails(False).Height >= y2 Then
                y2 = symbol.Y + symbol.SymbolDetails(False).Height
            End If
        Next
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function
    Public Function GetSWSignBoundsSelected(ByVal frame As SWFrame) As Rectangle
        Dim x1 As Integer = FrameWidth
        Dim x2 As Integer = 0
        Dim y1 As Integer = FrameHeight
        Dim y2 As Integer = 0

        Dim symbol As SWSignSymbol
        Dim symbolDetails As SWSymbol
        For Each symbol In frame.SignSymbols
            symbolDetails = symbol.SymbolDetails(False)
            If symbol.IsSelected Then
                If symbol.X <= x1 Then
                    x1 = symbol.X
                End If
                If symbol.Y <= y1 Then
                    y1 = symbol.Y
                End If

                If Not IsDbNull(symbolDetails.Width) AndAlso symbol.X + symbolDetails.Width >= x2 Then
                    x2 = symbol.X + symbolDetails.Width
                End If
                If Not IsDbNull(symbolDetails.Height) AndAlso symbol.Y + symbolDetails.Height >= y2 Then
                    y2 = symbol.Y + symbolDetails.Height
                End If
            End If
        Next
        Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
    End Function
    Public Sub EraseSign()
        SignSymbols.RemoveAll(AddressOf All)
        UnSelectSymbols()
    End Sub
    Friend Function All(ByVal obj As SWSignSymbol) As Boolean
        Return True
    End Function
    Friend Function Selected(ByVal obj As SWSignSymbol) As Boolean
        Return obj.IsSelected
    End Function
    Sub SeperateSymbols()
        Dim x As Integer
        Dim y As Integer
        If SelectedSymbolCount > 0 Then
            For Each Symbol In SignSymbols
                If Symbol.IsSelected Then
                    Symbol.X = x
                    Symbol.Y = y
                    x = x + 20
                    y = y + 20
                End If
            Next
        Else
            For Each Symbol In SignSymbols
                Symbol.X = x
                Symbol.Y = y
                x = x + 20
                y = y + 20
            Next
        End If
    End Sub
    Public Function Clone() As Object Implements ICloneable.Clone
        Dim frameClone As SWFrame = CType(MemberwiseClone(), SWFrame)
        Dim sequence As SWSequence
        Dim symbol As SWSignSymbol
        'Create new with the Frame
        '        FrameClone.Sequences = New List(Of SWSequence)
        '        FrameClone.SignSymbols = New List(Of SWSignSymbol)
        frameClone.NewCollections()
        For I As Integer = 0 To Sequences.Count - 1
            sequence = Sequences(I)
            frameClone.Sequences.Add(CType(sequence.Clone, SWSequence))
        Next
        For I As Integer = 0 To SignSymbols.Count - 1
            symbol = SignSymbols(I)
            frameClone.SignSymbols.Add(symbol.Clone)
        Next

        Return frameClone
    End Function
    Private Sub NewCollections()
        _sequences = New List(Of SWSequence)
        _signSymbols = New List(Of SWSignSymbol)

    End Sub
    Private _disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Private Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                'free unmanaged resources when explicitly called
                _compareSWSignSymbol.Dispose()
            End If

            'free shared unmanaged resources
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


    Public Sub AddSequences(ByVal sequencestoAdd As List(Of SWSequence))
        For Each swSequence As SWSequence In sequencestoAdd
            Sequences.Add(swSequence)
        Next

    End Sub


    Public Sub SignSymbolsSort()
        Me.SignSymbols.Sort(AddressOf CompareSignSymbolsByZ)
    End Sub

    Public Sub MergeFrame(frame As SWFrame)
        Dim myHeadBounds = GetHeadBounds(Me)
        Dim myHeadTrunkBounds = GetHeadTrunkBounds(Me)
        Dim myAllSymbolsBounds = GetAllSymbolsBounds(Me)
        Dim newFrameHeadBounds = GetHeadBounds(frame)
        Dim newFrameHeadTrunkBounds = GetHeadTrunkBounds(frame)
        Dim newFrameAllSymbolsBounds = GetAllSymbolsBounds(frame)
        Dim offset As Point = Point.Empty

        If Not myHeadBounds = Rectangle.Empty Then
            If Not newframeHeadBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myHeadBounds, newframeHeadBounds)
            ElseIf Not newframeHeadTrunkBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myHeadBounds, newframeHeadTrunkBounds)
            ElseIf Not newframeAllSymbolsBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myHeadBounds, newframeHeadTrunkBounds)

            End If

        ElseIf Not myHeadTrunkBounds = Rectangle.Empty Then
            If Not newframeHeadBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myHeadTrunkBounds, newframeHeadBounds)
            ElseIf Not newframeHeadTrunkBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myHeadTrunkBounds, newframeHeadTrunkBounds)
            ElseIf Not newframeAllSymbolsBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myHeadTrunkBounds, newframeHeadTrunkBounds)
            End If
        ElseIf Not myAllSymbolsBounds = Rectangle.Empty Then

            If Not newframeHeadBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myAllSymbolsBounds, newFrameHeadBounds)
                offset.Y -= myAllSymbolsBounds.Height + 10
                offset.X = CType(myAllSymbolsBounds.X + myAllSymbolsBounds.Width / 2 - newFrameHeadBounds.X - newFrameHeadBounds.Width / 2, Integer)
            ElseIf Not newframeHeadTrunkBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myAllSymbolsBounds, newframeHeadTrunkBounds)
            ElseIf Not newframeAllSymbolsBounds = Rectangle.Empty Then
                offset = GetBoundsTopLeftOffset(myAllSymbolsBounds, newframeHeadTrunkBounds)
            End If
        End If

        frame.SelectAll()
        frame.MoveSelected(offset)
        frame.UnSelectSymbols()
        Me.InsertSymbolsIntoSign(frame.SignSymbols)
    End Sub

    Private Sub InsertSymbolsIntoSign(swSignSymbols As List(Of SWSignSymbol))
        For Each swSignSymbol As SWSignSymbol In swSignSymbols
            swSignSymbol.Z = GetLastZ(Me) + 1
            SignSymbols.Add(swSignSymbol)
        Next

        CountSelectedSymbols()
        ResetSignSymbols()
    End Sub

    Private Function GetBoundsTopLeftOffset(bounds1 As Rectangle, bounds2 As Rectangle) As Point
        Return New Point(bounds1.X - bounds2.X, bounds1.Top - bounds2.Top)
    End Function
End Class