Imports SignWriterStudio.SWS
Imports SignWriterStudio.SWClasses

Public Class SWSignSearch
    Inherits SWEditor.Editor
    Public SearchCriteria As SWSignSearchCriteria

    'TODO get synonyms from handbook
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SWSignSearch))
        SuspendLayout()

        AutoScaleDimensions = New SizeF(6.0!, 13.0!)
        ClientSize = New Size(1118, 579)
        Name = "SWSignSearch"
        Sign = CType(resources.GetObject("$this.Sign"), SwSign)
        ResumeLayout(False)

    End Sub
    Public Sub SWSignSearch()
        InitializeComponent()
    End Sub

    Private Sub SWSignSearch_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not DesignMode) Then

            Dim tsiSearch As New ToolStripMenuItem
            tsiSearch.Text = "Search"
            tsiSearch.Alignment = ToolStripItemAlignment.Right

            Dim tsiSameFillRotation As New ToolStripMenuItem
            tsiSameFillRotation.Text = "Same Fill && Rotation"
            tsiSameFillRotation.Alignment = ToolStripItemAlignment.Right
            AddHandler tsiSameFillRotation.Click, AddressOf tsiSameFillRotation_Click

            Dim tsiSameFill As New ToolStripMenuItem
            tsiSameFill.Text = "Same Fill"
            tsiSameFill.Alignment = ToolStripItemAlignment.Right
            AddHandler tsiSameFill.Click, AddressOf tsiSameFill_Click

            Dim tsiSameRotation As New ToolStripMenuItem
            tsiSameRotation.Text = "Same Rotation"
            tsiSameRotation.Alignment = ToolStripItemAlignment.Right
            AddHandler tsiSameRotation.Click, AddressOf tsiSameRotation_Click

            Dim tsiAnyFillRotation As New ToolStripMenuItem
            tsiAnyFillRotation.Text = "Any Fill or Rotation"
            tsiAnyFillRotation.Alignment = ToolStripItemAlignment.Right
            AddHandler tsiAnyFillRotation.Click, AddressOf tsiAnyFillRotation_Click


            tsiSearch.DropDown.Items.Add(tsiSameFillRotation)
            tsiSearch.DropDown.Items.Add(tsiSameFill)
            tsiSearch.DropDown.Items.Add(tsiSameRotation)
            tsiSearch.DropDown.Items.Add(tsiAnyFillRotation)

            CMSPBSign.Items.Insert(0, tsiSearch)
        End If
    End Sub

    Private Sub tsiAnyFillRotation_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSymbolSearchType(MatchType.AnyFillRotation)
    End Sub

    Private Sub tsiSameRotation_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSymbolSearchType(MatchType.SameRotation)
    End Sub

    Private Sub tsiSameFill_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSymbolSearchType(MatchType.SameFill)
    End Sub

    Private Sub tsiSameFillRotation_Click(ByVal sender As Object, ByVal e As EventArgs)
        SetSymbolSearchType(MatchType.SameFillRotation)
    End Sub
    Private Sub SetSymbolSearchType(ByVal searchType As MatchType)
        For Each symbol In mySWSign.Frames(mySWSign.CurrentFrameIndex).SignSymbols
            If symbol.IsSelected Then
                symbol.Handcolor = GetSearchColor(searchType)
                symbol.SearchType = searchType
            End If
        Next
        DisplaySign()
    End Sub
    Public Function Base(ByVal symbol As SWSymbol)

        Return CInt((symbol.Code - 1 - symbol.Rotation - symbol.Fill * 16) / 96) + 256  'Symbol.BaseGroup + 256 
        '   (Code - 1 - Rotation - Fill * 16 ) /96 =     (Base - 256) 
    End Function
    Private Function GetSearchColor(ByVal searchType As MatchType) As Integer
        Select Case searchType
            Case MatchType.AnyFillRotation
                Return Color.DarkGray.ToArgb
            Case MatchType.SameFillRotation
                Return Color.Black.ToArgb
            Case MatchType.SameFill
                Return Color.Red.ToArgb
            Case MatchType.SameRotation
                Return Color.Green.ToArgb
        End Select
    End Function



    Private Overloads Sub btnAccept_Click(sender As Object, e As EventArgs) Handles btnAccept.Click
        LoadSearchCriteria()
    End Sub
    Private Sub LoadSearchCriteria()
        SearchCriteria = New SWSignSearchCriteria
        For Each symbol As SWSignSymbol In Sign.Frames(0).SignSymbols
            Dim baseCriteria As New SWSignSearchCriteria.BaseCriteria
            baseCriteria.Iswasss = symbol.SymbolDetails.Id
            baseCriteria.Match = symbol.SearchType
            baseCriteria.SynonymSymbol = True
            baseCriteria.Similarity = False

            SearchCriteria.MyBaseCriteria.Add(baseCriteria)
        Next


        SearchCriteria.SetSearchString()
    End Sub


End Class

Public Class SWSignSearchCriteria

    Public MyBaseCriteria As New List(Of BaseCriteria)
    Public MinimumProximity As Integer
    Public SearchString As String

    Public Sub SetSearchString()

        FillOutExtendedCriteria()


        SearchString = CreateSearchString()
    End Sub





    'Private Function InttoIntList(ByVal myInt As Integer) As IEnumerable(Of Integer)
    '    Dim intsList = New List(Of Integer)
    '    For I = 1 To 6
    '        If myInt Mod 2 Then
    '            intsList.Add(I)
    '        End If
    '        myInt = myInt >> 1
    '    Next
    '    Return intsList
    'End Function

    Public Sub FillOutExtendedCriteria()
        For Each baseCriteria As BaseCriteria In MyBaseCriteria
            CreateExtendedCriteria(baseCriteria)
        Next
    End Sub
    Public Sub CreateExtendedCriteria(ByRef baseCriteria As BaseCriteria)
        Dim extendedCriteria As ExtendedCriteria

        extendedCriteria = New ExtendedCriteria
        extendedCriteria.SssSearch = baseCriteria.Iswasss
        baseCriteria.MyExtendedCriteria.Add(extendedCriteria)
        If baseCriteria.SynonymSymbol Then
            FindSynonymSymbols(baseCriteria)
        End If

    End Sub


    Public Function CreateSearchString() As String
        Dim searchStringSb = New System.Text.StringBuilder()
        Dim whereClauses = 0
        Dim firstWhereClause = False
        Dim k = 1
        If MyBaseCriteria.Count > 0 Then
            For Each baseCriteria As BaseCriteria In MyBaseCriteria
                If searchStringSb.Length = 0 Then
                    searchStringSb.Append(GetQuerySelectBeg())
                    firstWhereClause = True
                End If

                If Not firstWhereClause Then
                    searchStringSb.Append(") intersect  ")
                End If

                Select Case baseCriteria.Match
                    Case MatchType.SameFillRotation
                        Dim I As Integer = 0

                        searchStringSb.AppendFormat(GetClauseBeg, k)

                        searchStringSb.AppendFormat("SS{0}.code =", k)


                        For Each extendedCriteria As ExtendedCriteria In baseCriteria.MyExtendedCriteria
                            If Not I = 0 Then
                                searchStringSb.AppendFormat(" OR SS{0}.code =", k)
                            End If

                            If searchStringSb.Length < 2256 Then
                                searchStringSb.Append(extendedCriteria.Code)
                                searchStringSb.Append(" ")
                                whereClauses += 1
                            Else
                                MsgBox("Too Long")
                            End If

                            I += 1
                            firstWhereClause = False
                        Next


                    Case MatchType.SameFill

                        Dim I As Integer = 0
                        searchStringSb.AppendFormat(GetClauseBeg, k)
                        For Each extendedCriteria As ExtendedCriteria In baseCriteria.MyExtendedCriteria
                            If Not I = 0 Then
                                searchStringSb.Append(" OR ")
                            End If

                            If searchStringSb.Length < 2256 Then
                                Dim symbol = New SWSymbol
                                symbol.Code = extendedCriteria.Code


                                Dim fill = symbol.Fill

                                searchStringSb.AppendFormat("S{0}.sym_bs_code=", k)
                                searchStringSb.Append(symbol.BaseGroup)
                                searchStringSb.Append(" AND ")
                                searchStringSb.AppendFormat("S{0}.sym_fill=", k)
                                searchStringSb.Append(fill)
                                searchStringSb.Append(" ")
                                whereClauses += 1
                            Else
                                MsgBox("Too Long")
                            End If

                            I += 1
                            firstWhereClause = False
                        Next

                    Case MatchType.SameRotation


                        Dim I As Integer = 0
                        searchStringSb.AppendFormat(GetClauseBeg, k)
                        For Each extendedCriteria As ExtendedCriteria In baseCriteria.MyExtendedCriteria
                            If Not I = 0 Then
                                searchStringSb.Append(" OR ")
                            End If

                            If searchStringSb.Length < 2256 Then
                                Dim symbol = New SWSymbol
                                symbol.Code = extendedCriteria.Code


                                Dim rotation = symbol.Rotation
                                searchStringSb.AppendFormat("S{0}.sym_bs_code=", k)
                                searchStringSb.Append(symbol.BaseGroup)
                                searchStringSb.Append(" AND ")
                                searchStringSb.AppendFormat("S{0}.sym_rot=", k)
                                searchStringSb.Append(rotation)
                                searchStringSb.Append(" ")
                                whereClauses += 1
                            Else
                                MsgBox("Too Long")
                            End If

                            I += 1
                            firstWhereClause = False
                        Next

                    Case MatchType.AnyFillRotation

                        Dim I As Integer = 0
                        searchStringSb.AppendFormat(GetClauseBeg, k)
                        For Each extendedCriteria As ExtendedCriteria In baseCriteria.MyExtendedCriteria
                            If Not I = 0 Then
                                searchStringSb.Append(" OR ")
                            End If

                            If searchStringSb.Length < 2256 Then
                                Dim symbol = New SWSymbol
                                symbol.Code = extendedCriteria.Code


                                searchStringSb.AppendFormat("S{0}.sym_bs_code=", k)
                                searchStringSb.Append(symbol.BaseGroup)
                                searchStringSb.Append(" ")
                                whereClauses += 1
                            Else
                                MsgBox("Too Long")
                            End If

                            I += 1
                            firstWhereClause = False
                        Next
                End Select

            Next
            ' ReSharper disable RedundantAssignment
            k += 1
            ' ReSharper restore RedundantAssignment
        End If

        searchStringSb.Append(")")
        If whereClauses = 0 Then
            Return String.Empty
        Else

            searchStringSb.Append(GetQuerySelectEnd())
            Return searchStringSb.ToString
        End If

    End Function

    Private Function GetQuerySelectBeg() As String
        Return "Select SwSign.IDDictionary FROM Dictionary SWSign INNER JOIN Frame SWFrame ON SWSign.IDDictionary = SWFrame.IDDictionary INNER JOIN ("
    End Function
    Private Function GetQuerySelectEnd() As String
        Return ")  r1 ON SWFrame.IDFrame = r1.IDFrame"
    End Function

    Private Function GetClauseBeg() As String
        Return "SELECT SS{0}.IDFrame FROM SignSymbols SS{0} INNER JOIN symbol S{0} ON SS{0}.code = S{0}.sym_code WHERE ("
    End Function



    Private Sub MovementSynonym(ByRef baseCriteria As BaseCriteria, ByVal group As Object, ByVal rotation As Object)
        If group = 3 And (rotation = 3 Or rotation = 7) Then
            AddSynonymSymbolsGroup(baseCriteria, 5)
        ElseIf group = 5 And (rotation = 3 Or rotation = 7) Then
            AddSynonymSymbolsGroup(baseCriteria, 3)
        End If
    End Sub
    Public Sub FindSynonymSymbols(ByRef baseCriteria As BaseCriteria)
        Dim symbol As New SWSymbol
        symbol.Id = baseCriteria.Iswasss

        Dim group = symbol.Group
        Dim fill = symbol.Fill
        Dim rotation = symbol.Rotation

        If symbol.Category = 1 Then
            HandSynonym(baseCriteria, New SWSignSymbol With {.Code = symbol.Code}, fill, rotation)
        ElseIf symbol.Category = 2 Then
            MovementSynonym(baseCriteria, group, rotation)
        End If

        If IsHeelSynonym(symbol) Then
            HealSynonym(baseCriteria, symbol)
        End If

    End Sub

    Private Sub HealSynonym(ByVal baseCriteria As BaseCriteria, ByVal swSymbol As SWSymbol)

        Dim corrrespondingBg As Integer? = GetCorrespondingHeelBG(swSymbol.BaseGroup)

        If corrrespondingBg.HasValue Then
            Dim isHeel = GetIsHeel(swSymbol)
            Dim correspondinghealFillRotation = GetCorrespondingHeelFillRotation(isHeel, swSymbol.Fill, swSymbol.Rotation)
            For Each fillRotation As Tuple(Of Integer, Integer) In correspondinghealFillRotation
                AddSynonymHeel(baseCriteria, corrrespondingBg, fillRotation.Item1, fillRotation.Item2)
            Next

        End If


    End Sub

    Private Sub AddSynonymHeel(ByVal baseCriteria As BaseCriteria, ByVal bg As Integer?, ByVal fill As Integer, ByVal rotation As Integer)
        Dim synonymSymbol As New SWSymbol
        Dim code = SWSymbol.GetCode(bg, fill, rotation)
        If Not code = 0 Then
            synonymSymbol.Code = code

            Dim extendedCriteria As New ExtendedCriteria
            extendedCriteria.SssSearch = synonymSymbol.Id
            baseCriteria.MyExtendedCriteria.Add(extendedCriteria)
        End If
    End Sub

    Private Function GetCorrespondingHeelFillRotation(ByVal isHeel As Boolean, ByVal fill As Integer, ByVal rotation As Integer) As List(Of Tuple(Of Integer, Integer))
        Dim fillRotaionList As New List(Of Tuple(Of Integer, Integer))
        If isHeel Then
            Select Case rotation
                Case 1
                    fillRotaionList.Add(Tuple.Create(4, 1))
                Case 3
                    fillRotaionList.Add(Tuple.Create(5, 1))
                Case 5
                    fillRotaionList.Add(Tuple.Create(6, 1))
                Case 7
                    fillRotaionList.Add(Tuple.Create(5, 9))
                Case 9
                    fillRotaionList.Add(Tuple.Create(4, 9))
                Case 11
                    fillRotaionList.Add(Tuple.Create(5, 9))
                Case 13
                    fillRotaionList.Add(Tuple.Create(6, 9))
                Case 15
                    fillRotaionList.Add(Tuple.Create(5, 1))
            End Select

        Else
            If rotation = 1 Then
                If fill = 4 Then
                    fillRotaionList.Add(Tuple.Create(2, 1))
                ElseIf fill = 5 Then
                    fillRotaionList.Add(Tuple.Create(2, 3))
                    fillRotaionList.Add(Tuple.Create(2, 15))
                ElseIf fill = 6 Then
                    fillRotaionList.Add(Tuple.Create(2, 5))
                End If
            ElseIf rotation = 9 Then
                If fill = 4 Then
                    fillRotaionList.Add(Tuple.Create(2, 9))
                ElseIf fill = 5 Then
                    fillRotaionList.Add(Tuple.Create(2, 7))
                    fillRotaionList.Add(Tuple.Create(2, 11))
                ElseIf fill = 6 Then
                    fillRotaionList.Add(Tuple.Create(2, 13))
                End If
            End If
        End If
        Return fillRotaionList
    End Function

    Private Function GetCorrespondingHeelBG(ByVal baseGroup As Integer) As Integer?
        Select Case baseGroup
            Case 333
                Return 332
            Case 335
                Return 334
            Case 337
                Return 336
            Case 348
                Return 346
            Case 350
                Return 349
            Case 502
                Return 501
            Case 516
                Return 515
            Case 332
                Return 333
            Case 334
                Return 335
            Case 336
                Return 337
            Case 346
                Return 348
            Case 349
                Return 350
            Case 501
                Return 502
            Case 515
                Return 516
            Case Else
                Return Nothing
        End Select
    End Function

    Private Shared Function GetIsHeel(ByVal swSymbol As SWSymbol) As Boolean
        If swSymbol.BaseGroup = 333 OrElse swSymbol.BaseGroup = 335 OrElse swSymbol.BaseGroup = 337 OrElse swSymbol.BaseGroup = 348 OrElse swSymbol.BaseGroup = 350 OrElse swSymbol.BaseGroup = 502 OrElse swSymbol.BaseGroup = 516 Then
            Return True
        End If
        Return False
    End Function

    Private Shared Function IsHeelSynonym(ByVal swSymbol As SWSymbol) As Boolean

        If GetIsHeel(swSymbol) Then
            If swSymbol.Fill = 2 Then
                If swSymbol.Rotation = 1 OrElse swSymbol.Rotation = 3 OrElse swSymbol.Rotation = 5 OrElse swSymbol.Rotation = 7 OrElse swSymbol.Rotation = 9 OrElse swSymbol.Rotation = 11 OrElse swSymbol.Rotation = 13 OrElse swSymbol.Rotation = 15 Then
                    Return True
                End If
            End If
        End If
        If swSymbol.BaseGroup = 332 OrElse swSymbol.BaseGroup = 334 OrElse swSymbol.BaseGroup = 336 OrElse swSymbol.BaseGroup = 346 OrElse swSymbol.BaseGroup = 349 OrElse swSymbol.BaseGroup = 501 OrElse swSymbol.BaseGroup = 515 Then
            If swSymbol.Rotation = 1 OrElse swSymbol.Rotation = 9 Then

                If swSymbol.Fill = 4 OrElse swSymbol.Fill = 5 OrElse swSymbol.Fill = 6 Then
                    Return True
                End If
            End If
        End If

        Return False
    End Function

    Private Sub HandSynonym(ByRef baseCriteria As BaseCriteria, ByVal symbol As SWSignSymbol, ByVal fill As Object, ByVal rotation As Object)
        'Right Hand
        If symbol.Hand = 0 Then
            '0101001010107
            '0101001010511
            If fill = 1 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 11)
            ElseIf fill = 5 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 1, 7)
                '0101001010407
                '0101001010207
            ElseIf fill = 4 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 7)
            ElseIf fill = 2 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 4, 7)
                '0101001010307
                '0101001010507
            ElseIf fill = 3 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 7)
            ElseIf fill = 5 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 3, 7)
                '0101001010211
                '0101001010607
            ElseIf fill = 2 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 6, 7)
            ElseIf fill = 6 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 11)
                '0101001010103
                '0101001010503
            ElseIf fill = 1 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 3)
            ElseIf fill = 5 And rotation = 2 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 1, 3)
                '0101001010403
                '0101001010215
            ElseIf fill = 4 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 15)
            ElseIf fill = 2 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 4, 3)
                '0101001010303
                '0101001010515
            ElseIf fill = 3 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 15)
            ElseIf fill = 5 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 3, 3)
                '0101001010203
                '0101001010603
            ElseIf fill = 2 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 6, 3)
            ElseIf fill = 6 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 3)
            End If

        Else 'Left Hand
            '0101001010111
            '0101001010511
            If fill = 1 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 11)
            ElseIf fill = 5 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 1, 11)
                '0101001010211
                '0101001010611
            ElseIf fill = 2 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 6, 11)
            ElseIf fill = 6 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 11)
                '0101001010311
                '0101001010507
            ElseIf fill = 3 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 7)
            ElseIf fill = 5 And rotation = 7 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 3, 11)
                '0101001010207
                '0101001010411
            ElseIf fill = 1 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 11)
            ElseIf fill = 5 And rotation = 11 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 1, 11)
                '0101001010115
                '0101001010503
            ElseIf fill = 1 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 3)
            ElseIf fill = 5 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 1, 15)
                '0101001010215
                '0101001010415
            ElseIf fill = 2 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 4, 15)
            ElseIf fill = 4 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 15)
                '0101001010315
                '0101001010515
            ElseIf fill = 3 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 5, 15)
            ElseIf fill = 5 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 3, 15)
                '0101001010203
                '0101001010615
            ElseIf fill = 2 And rotation = 3 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 6, 15)
            ElseIf fill = 6 And rotation = 15 Then
                AddSynonymSymbolsFillRotation(baseCriteria, 2, 3)
            End If
        End If
    End Sub
    Private Sub AddSynonymSymbolsFillRotation(ByRef baseCriteria As BaseCriteria, ByVal fill As Integer, ByVal rotation As Integer)
        Dim synonymSymbol As New SWSymbol
        synonymSymbol.Id = baseCriteria.Iswasss

        synonymSymbol.Rotation = rotation
        synonymSymbol.Fill = fill

        synonymSymbol.MakeId()
        Dim extendedCriteria As New ExtendedCriteria
        extendedCriteria.SssSearch = synonymSymbol.Id
        baseCriteria.MyExtendedCriteria.Add(extendedCriteria)
    End Sub
    Private Sub AddSynonymSymbolsGroup(ByRef baseCriteria As BaseCriteria, ByVal group As Integer)
        Dim synonymSymbol As New SWSymbol
        synonymSymbol.Id = baseCriteria.Iswasss


        synonymSymbol.Group = group
        synonymSymbol.MakeId()

        Dim extendedCriteria As New ExtendedCriteria
        extendedCriteria.SssSearch = synonymSymbol.Id
        baseCriteria.MyExtendedCriteria.Add(extendedCriteria)
    End Sub

    Public Class BaseCriteria
        Public Iswasss As String
        Public Match As MatchType
        Public SynonymSymbol As Boolean
        Public Similarity As Boolean
        Public MyExtendedCriteria As New List(Of ExtendedCriteria)

        Public Function Clone() As BaseCriteria
            Dim nbc = New BaseCriteria
            nbc.Iswasss = Iswasss
            nbc.Match = Match
            nbc.Similarity = Similarity
            nbc.SynonymSymbol = SynonymSymbol
            Return nbc
        End Function
    End Class
    Public Class ExtendedCriteria
        Private _sssSearch As String
        Public Property SssSearch() As String
            Get
                Return _sssSearch
            End Get
            Set(ByVal value As String)
                _sssSearch = value

                Code = SWSymbol.CodefromId(value)
            End Set
        End Property


        Public Code As Integer
    End Class
End Class
