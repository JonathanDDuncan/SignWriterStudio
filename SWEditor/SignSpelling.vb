Imports System.Windows.Forms
Imports SignWriterStudio.SWClasses

Friend Class SignSpelling
    Private Shared _ranking As Integer = 0
    Private Shared _canceling As Boolean
    Public Shared Function OrderSuggestion1(ByVal sign As SwSign, Optional canAsk As Boolean = True) As List(Of SWSequence)
        Dim symbs As IEnumerable(Of SWSignSymbol) = GetSWSignSymbols(sign.Frames.FirstOrDefault())
        Dim symbols = symbs.ToList()
        _ranking = 0
        _canceling = False
        Dim newSequence = EmptySequence()
        'http://www.signwriting.org/archive/docs6/sw0534-SignSpellingGuidelines-2008.pdf
        'Syllables 1: Hands Beginning position
        '1. Dominant Hand
        Dim beginningDominantHand = GetHand(symbols, True, True, canAsk)
        AddtoSequence(beginningDominantHand, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '2. Non-Dominant Hand
        Dim beginningNonDominantHand = GetHand(symbols, False, True, canAsk)
        AddtoSequence(beginningNonDominantHand, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        'Syllable 2: Movement
        '1. 1st Movement Dominant
        Dim firstMovementDominant = GetMovement(symbols, True, 1, canAsk, beginningDominantHand)
        AddtoSequence(firstMovementDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '2. 1st Movement Non-Dominant
        Dim firstMovementNonDominant = GetMovement(symbols, False, 1, canAsk, beginningNonDominantHand)
        AddtoSequence(firstMovementNonDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '3. 2nd Movement Dominant
        Dim secondMovementDominant = GetMovement(symbols, True, 2, canAsk, beginningNonDominantHand)
        AddtoSequence(secondMovementDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '4. 2nd Movement Non-Dominant
        Dim secondMovementNonDominant = GetMovement(symbols, False, 2, canAsk, beginningNonDominantHand)
        AddtoSequence(secondMovementNonDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()

        '3rd Movement Dominant
        Dim thirdMovementDominant = GetMovement(symbols, True, 3, canAsk, beginningNonDominantHand)
        AddtoSequence(thirdMovementDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        ' 3rd Movement Non-Dominant
        Dim thirdMovementNonDominant = GetMovement(symbols, False, 3, canAsk, beginningNonDominantHand)
        AddtoSequence(thirdMovementNonDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()


        '4th Movement Dominant
        Dim fourthMovementDominant = GetMovement(symbols, True, 4, canAsk, beginningNonDominantHand)
        AddtoSequence(fourthMovementDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        ' 4th Movement Non-Dominant
        Dim fourthMovementNonDominant = GetMovement(symbols, False, 4, canAsk, beginningNonDominantHand)
        AddtoSequence(fourthMovementNonDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '5. Dynamics Dominant
        Dim dynamicsDominant = GetDynamics(symbols, True, canAsk)
        AddtoSequence(dynamicsDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '6. Dynamics Non-Dominant
        Dim dynamicsNonDominant = GetDynamics(symbols, False, canAsk)
        AddtoSequence(dynamicsNonDominant, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        'Syllable 3: Hands Ending position
        '1. Dominant Hand
        Dim endingDominantHand = GetHand(symbols, True, False, canAsk)
        AddtoSequence(endingDominantHand, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        '2. Non-Dominant Hand
        Dim endingNonDominantHand = GetHand(symbols, False, False, canAsk)
        AddtoSequence(endingNonDominantHand, symbols, newSequence)
        If _canceling Then Return EmptySequence()
        'Syllable 4: Detailed Sorting
        '1. Location Dominant Hand
        AddtoSequence(GetLocationHand(symbols, True), newSequence)
        '2. Location Non-Dominant Hand
        AddtoSequence(GetLocationHand(symbols, False), newSequence)
        '3. Head
        AddtoSequence(GetLocationHead(symbols), newSequence)
        '4. Face (Top-Down)
        AddtoSequence(GetLocationFace(symbols), newSequence)
        '5. Neck
        AddtoSequence(GetLocationNeck(symbols), newSequence)
        '6. Shoulder
        AddtoSequence(GetLocationShoulder(symbols), newSequence)
        '6-1. Arms
        AddtoSequence(GetLocationArms(symbols), newSequence)
        '7. Torso 
        AddtoSequence(GetLocationTorso(symbols), newSequence)
        '8. Hips & Legs
        AddtoSequence(GetLocationHipsLegs(symbols), newSequence)
        '9 Other Location symbols
        AddtoSequence(GetAnyLocation(symbols), newSequence)

        Return newSequence
    End Function

    Private Shared Function EmptySequence() As List(Of SWSequence)

        Return New List(Of SWSequence)()
    End Function

    Private Shared Function GetSWSignSymbols(ByVal swFrame As SWFrame) As IEnumerable(Of SWSignSymbol)
        Dim previousLocationSymbols = swFrame.Sequences.Where(Function(x) IsLocation(x.Code)).Select(Function(x) New SWSignSymbol With {.Code = x.Code})
        Dim signSymbols As IEnumerable(Of SWSignSymbol)
        If swFrame.SelectedSymbolCount > 0 Then

            signSymbols = swFrame.SignSymbols.Where(Function(x) x.IsSelected)
        Else
            signSymbols = swFrame.SignSymbols.Select(Function(x) x)
        End If

        Return previousLocationSymbols.Concat(signSymbols)
    End Function

    Private Shared Function IsLocation(ByVal code As Integer) As Boolean
        Dim symbol = New SWSignSymbol With {.Code = code}
        Return (symbol.SymbolDetails.Category = 6)
    End Function

    Private Shared Function GetAnyLocation(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Return SequencesBetween(symbols, 61345, 62112)
    End Function

    Private Shared Function GetLocationArms(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Dim result1 = SequencesBetween(symbols, 62017, 62024)
        Dim result2 = SequencesBetween(symbols, 62033, 62040)
        Dim result3 = SequencesBetween(symbols, 62049, 62056)
        Dim result4 = SequencesBetween(symbols, 62065, 62072)

        Return result1.Concat(result2).Concat(result3).Concat(result4)
    End Function

    Private Shared Function GetLocationHipsLegs(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Dim result1 = SequencesBetween(symbols, 62025, 62032)
        Dim result2 = SequencesBetween(symbols, 62025, 62032)
        Dim result3 = SequencesBetween(symbols, 62041, 62048)
        Dim result4 = SequencesBetween(symbols, 62057, 62064)
        Dim result5 = SequencesBetween(symbols, 62073, 62080)

        Return result1.Concat(result2).Concat(result3).Concat(result4).Concat(result5)
    End Function

    Private Shared Function GetLocationTorso(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Return SequencesBetween(symbols, 61921, 61992)
    End Function

    Private Shared Function GetLocationShoulder(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Return SequencesBetween(symbols, 59617, 60096)
    End Function

    Private Shared Function GetLocationNeck(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Return SequencesBetween(symbols, 61905, 61912)
    End Function

    Private Shared Function SequencesBetween(ByVal symbols As List(Of SWSignSymbol), ByVal codeFrom As Integer, ByVal codeTo As Integer) As IEnumerable(Of SWSequence)
        Dim sequences As New List(Of SWSequence)()
        Dim matchingSymbols = symbols.Where(Function(x) x.Code >= codeFrom AndAlso x.Code <= codeTo).ToList()

        For Each symbol As SWSignSymbol In matchingSymbols
            _ranking += 1
            symbols.Remove(symbol)
            sequences.Add(New SWSequence() With {.Code = symbol.Code, .Rank = _ranking})
        Next
        Return sequences
    End Function
    Private Shared Function SymbolsBetween(ByVal symbols As List(Of SWSignSymbol), ByVal codeFrom As Integer, ByVal codeTo As Integer) As List(Of SWSignSymbol)
        Dim matchingSymbols = symbols.Where(Function(x) x.Code >= codeFrom AndAlso x.Code <= codeTo).ToList()

        Return matchingSymbols
    End Function

    Private Shared Function GetLocationFace(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Dim faceLocation = SequencesBetween(symbols, 61825, 61896)
        Dim otherFaces = SequencesBetween(symbols, 50113, 59569)
        Dim faces = New List(Of SWSequence)()
        faces.AddRange(faceLocation)
        faces.AddRange(otherFaces)
        Return faces
    End Function

    Private Shared Function GetLocationHead(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSequence)
        Return SequencesBetween(symbols, 49057, 50064)
    End Function

    Private Shared Function GetLocationHand(ByVal symbols As List(Of SWSignSymbol), ByVal dominantHand As Boolean, Optional ByVal rightDominant As Boolean = True) As List(Of SWSequence)
        Dim locationHands As New List(Of SWSequence)()
        Dim hands As List(Of SWSignSymbol)
        If dominantHand Then
            hands = GetDominantHands(GetLocationHands(symbols), rightDominant)
        Else
            hands = GetNonDominantHands(GetLocationHands(symbols), rightDominant)
        End If

        For Each symbol As SWSignSymbol In hands
            _ranking += 1
            symbols.Remove(symbol)
            locationHands.Add(New SWSequence() With {.Code = symbol.Code, .Rank = _ranking})
        Next

        Return locationHands
    End Function

    Private Shared Function GetLocationHands(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSignSymbol)
        Return symbols.Where(Function(x) x.Code >= 62081 AndAlso x.Code <= 62112)
    End Function

    Private Shared Sub AddtoSequence(ByVal symbol As SWSignSymbol, ByVal symbols As List(Of SWSignSymbol), ByVal newSequences As List(Of SWSequence))
        If symbol IsNot Nothing Then
            _ranking += 1
            symbols.Remove(symbol)
            Dim sequence = New SWSequence() With {.Code = symbol.Code, .Rank = _ranking}

            If sequence IsNot Nothing Then
                newSequences.Add(sequence)
            End If
        End If
    End Sub
    Private Shared Sub AddtoSequence(sequences As IEnumerable(Of SWSequence), ByVal newSequences As List(Of SWSequence))

        If sequences IsNot Nothing Then
            For Each sequence As SWSequence In sequences
                newSequences.Add(sequence)
            Next
        End If
    End Sub
    Private Shared Function GetHand(ByVal symbols As List(Of SWSignSymbol), ByVal dominantHand As Boolean, ByVal isBeginning As Boolean, ByVal canAsk As Boolean, Optional ByVal rightDominant As Boolean = True) As SWSignSymbol
        Dim hands As List(Of SWSignSymbol)
        Dim foundSymbol As SWSignSymbol = Nothing
        If dominantHand Then
            hands = GetDominantHands(symbols, rightDominant)
        Else
            hands = GetNonDominantHands(symbols, rightDominant)
        End If

        If hands.Count = 1 Then
            foundSymbol = hands.First()
        ElseIf hands.Count > 1 Then

            foundSymbol = GetHandByStraightArrow(dominantHand, symbols, isBeginning, rightDominant)
            If foundSymbol Is Nothing Then
                foundSymbol = GetHandByRotationArrow(dominantHand, symbols, isBeginning, rightDominant)
            End If
            If foundSymbol Is Nothing Then
                foundSymbol = GetHandByCurveArrow(dominantHand, symbols, isBeginning, rightDominant)
            End If

            If foundSymbol Is Nothing Then
                foundSymbol = AskHandSymbols(dominantHand, hands, canAsk, isBeginning)
            End If
        End If

        If foundSymbol IsNot Nothing Then

            Return foundSymbol
        Else
            Return Nothing
        End If
    End Function

    Private Shared Function GetHandByCurveArrow(ByVal dominantHand As Boolean, ByVal symbols As List(Of SWSignSymbol), ByVal isBeginning As Boolean, ByVal rightDominant As Boolean) As SWSignSymbol
        Dim arrows = GetCurveArrows(symbols)
        Dim hands As List(Of SWSignSymbol)

        If dominantHand Then
            hands = GetDominantHands(symbols, rightDominant)
        Else
            hands = GetNonDominantHands(symbols, rightDominant)
        End If

        Dim handsinPosition = GetHandByCurveArrowPosition(arrows, hands, isBeginning)

        If handsinPosition.Count = 1 Then
            Return handsinPosition.First
        Else
            Return Nothing
        End If
    End Function
    Private Shared Function GetHandByRotationArrow(ByVal dominantHand As Boolean, ByVal symbols As List(Of SWSignSymbol), ByVal isBeginning As Boolean, ByVal rightDominant As Boolean) As SWSignSymbol
        Dim arrows = GetRotationArrows(symbols, dominantHand, rightDominant)
        Dim hands As List(Of SWSignSymbol)

        If dominantHand Then
            hands = GetDominantHands(symbols, rightDominant)
        Else
            hands = GetNonDominantHands(symbols, rightDominant)
        End If

        Dim handsinPosition = GetHandByRotationArrowPosition(arrows, hands, isBeginning)

        If handsinPosition.Count = 1 Then
            Return handsinPosition.First
        Else
            Return Nothing
        End If
    End Function


    Private Shared Function GetHandByCurveArrowPosition(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol), ByVal isBeginning As Boolean) As List(Of SWSignSymbol)
        Dim handsinPosition As List(Of SWSignSymbol)
        If isBeginning Then
            handsinPosition = GetHandsBehindCurveArrow(arrows, hands)
        Else
            handsinPosition = GetHandsInFrontofCurveArrow(arrows, hands)

        End If
        Return handsinPosition

    End Function
    Private Shared Function GetHandByRotationArrowPosition(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol), ByVal isBeginning As Boolean) As List(Of SWSignSymbol)
        Dim handsinPosition As List(Of SWSignSymbol)
        If isBeginning Then
            handsinPosition = GetHandsBehindRotationArrow(arrows, hands)
        Else
            handsinPosition = GetHandsInFrontofRotationArrow(arrows, hands)

        End If
        Return handsinPosition

    End Function

    Private Shared Function GetHandsInFrontofCurveArrow(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim handsFound = New List(Of SWSignSymbol)
        For Each arrow As SWSignSymbol In arrows
            For Each hand As SWSignSymbol In hands
                If IsInFrontOfCurveArrow(hand, arrow) AndAlso Not handsFound.Contains(hand) Then
                    handsFound.Add(hand)
                End If
            Next
        Next
        Return handsFound

    End Function
    Private Shared Function GetHandsInFrontofRotationArrow(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim handsFound = New List(Of SWSignSymbol)
        For Each arrow As SWSignSymbol In arrows
            For Each hand As SWSignSymbol In hands
                If IsInFrontOfRotationArrow(hand, arrow) AndAlso Not handsFound.Contains(hand) Then
                    handsFound.Add(hand)
                End If
            Next
        Next
        Return handsFound

    End Function

    Private Shared Function IsInFrontOfRotationArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Dim result = False


        If arrow.Code >= 40129 AndAlso arrow.Code <= 40224 Then

            Select Case (arrow.SymbolDetails.Rotation)

                Case 1, 3, 7, 8, 11, 12, 13
                    If WestofArrow(hand, arrow) Then result = True
                Case 2, 10
                    If SouthofArrow(hand, arrow) Then result = True
                Case 4, 5, 9, 15, 16
                    If EastofArrow(hand, arrow) Then result = True
                Case 6, 14
                    If NorthOfArrow(hand, arrow) Then result = True

            End Select
        ElseIf arrow.Code >= 45985 AndAlso arrow.Code <= 46080 Then 'Rotation Single Floor Plane
            Select Case (arrow.SymbolDetails.Rotation)

                Case 1, 11, 12
                    If SouthWestofArrow(hand, arrow) Then result = True
                Case 2, 10
                    If SouthofArrow(hand, arrow) Then result = True
                Case 3, 4, 9, 15, 16
                    If EastofArrow(hand, arrow) Then result = True
                Case 5
                    If NorthEastofArrow(hand, arrow) Then result = True
                Case 6, 14
                    If NorthOfArrow(hand, arrow) Then result = True
                Case 7, 8
                    If WestofArrow(hand, arrow) Then result = True
                Case 13
                    If NorthWestofArrow(hand, arrow) Then result = True

            End Select
        End If


        Return result
    End Function


    Private Shared Function GetHandsBehindCurveArrow(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim handsFound = New List(Of SWSignSymbol)
        For Each arrow As SWSignSymbol In arrows
            For Each hand As SWSignSymbol In hands
                If IsBehindOfCurveArrow(hand, arrow) AndAlso Not handsFound.Contains(hand) Then
                    handsFound.Add(hand)
                End If
            Next
        Next
        Return handsFound

    End Function
    Private Shared Function GetHandsBehindRotationArrow(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim handsFound = New List(Of SWSignSymbol)
        For Each arrow As SWSignSymbol In arrows
            For Each hand As SWSignSymbol In hands
                If IsBehindOfRotationArrow(hand, arrow) AndAlso Not handsFound.Contains(hand) Then
                    handsFound.Add(hand)
                End If
            Next
        Next
        Return handsFound

    End Function

    Private Shared Function IsInFrontOfCurveArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Dim result = False
        Select Case (arrow.SymbolDetails.Rotation)

            Case 1, 15
                If NorthWestofArrow(hand, arrow) Then result = True
            Case 2, 14
                If WestofArrow(hand, arrow) Then result = True
            Case 3, 13
                If SouthWestofArrow(hand, arrow) Then result = True
            Case 4, 12
                If SouthofArrow(hand, arrow) Then result = True
            Case 5, 11
                If SouthEastofArrow(hand, arrow) Then result = True
            Case 6, 10
                If EastofArrow(hand, arrow) Then result = True
            Case 7, 9
                If NorthEastofArrow(hand, arrow) Then result = True
            Case 8, 16
                If NorthOfArrow(hand, arrow) Then result = True
        End Select

        Return result
    End Function
    Private Shared Function IsBehindOfCurveArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Dim result = False
        Select Case (arrow.SymbolDetails.Rotation)
            Case 1, 11
                If SouthWestofArrow(hand, arrow) Then result = True
            Case 2, 10
                If SouthofArrow(hand, arrow) Then result = True
            Case 3, 9
                If SouthEastofArrow(hand, arrow) Then result = True
            Case 4, 16
                If EastofArrow(hand, arrow) Then result = True
            Case 5, 15
                If NorthEastofArrow(hand, arrow) Then result = True
            Case 6, 14
                If NorthOfArrow(hand, arrow) Then result = True
            Case 7, 13
                If NorthWestofArrow(hand, arrow) Then result = True
            Case 8, 12
                If WestofArrow(hand, arrow) Then result = True
        End Select

        Return result
    End Function
    Private Shared Function IsBehindOfRotationArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Dim result = False

        If arrow.Code >= 40129 AndAlso arrow.Code <= 40224 Then

            Select Case (arrow.SymbolDetails.Rotation)
                Case 5, 6, 7, 9, 10, 11
                    If WestofArrow(hand, arrow) Then result = True
                Case 8, 16
                    If NorthOfArrow(hand, arrow) Then result = True
                Case 1, 2, 3, 13, 14, 15
                    If EastofArrow(hand, arrow) Then result = True
                Case 4, 12
                    If SouthofArrow(hand, arrow) Then result = True
            End Select
        ElseIf arrow.Code >= 45985 AndAlso arrow.Code <= 46080 Then 'Rotation Single Floor Plane

            Select Case (arrow.SymbolDetails.Rotation)
                Case 5, 6, 7, 9, 10, 11
                    If WestofArrow(hand, arrow) Then result = True
                Case 8, 16
                    If SouthofArrow(hand, arrow) Then result = True
                Case 1, 2, 3, 13, 14, 15
                    If EastofArrow(hand, arrow) Then result = True
                Case 4, 12
                    If NorthOfArrow(hand, arrow) Then result = True
            End Select
        End If
        Return result
    End Function


    Private Shared Function GetCurveArrows(ByVal symbols As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim wallPlane = SymbolsBetween(symbols, 37633, 37984)  'Wall Plane  

        Return wallPlane
    End Function
    Private Shared Function GetRotationArrows(ByVal symbols As List(Of SWSignSymbol), ByVal dominantHand As Boolean, ByVal rightDominant As Boolean) As List(Of SWSignSymbol)
        Dim wallPlane = SymbolsBetween(symbols, 40129, 40224).Where(Function(arrow) IsArrowForHand(arrow, dominantHand, rightDominant))  'Wall Plane  
        Dim floorPlane = SymbolsBetween(symbols, 45985, 46080).Where(Function(arrow) IsArrowForHand(arrow, dominantHand, rightDominant))   'Floor Plane  

        Dim returnList = New List(Of SWSignSymbol)()
        returnList.AddRange(wallPlane)
        returnList.AddRange(floorPlane)

        Return returnList
    End Function

    Private Shared Function IsArrowForHand(ByVal arrow As SWSignSymbol, ByVal dominantHand As Boolean, ByVal rightDominant As Boolean) As Boolean
        If (dominantHand And rightDominant) OrElse (Not dominantHand AndAlso Not rightDominant) Then
            Return arrow.SymbolDetails.Fill = 1 OrElse arrow.SymbolDetails.Fill = 4
        Else
            Return arrow.SymbolDetails.Fill = 2 OrElse arrow.SymbolDetails.Fill = 5
        End If
    End Function

    Private Shared Function GetHandByStraightArrow(ByVal dominantHand As Boolean, ByVal symbols As List(Of SWSignSymbol), ByVal isBeginning As Boolean, ByVal rightDominant As Boolean) As SWSignSymbol
        Dim arrows = GetStraightArrows(symbols)
        Dim hands As List(Of SWSignSymbol)

        If dominantHand Then
            hands = GetDominantHands(symbols, rightDominant)
        Else
            hands = GetNonDominantHands(symbols, rightDominant)
        End If

        Dim handsinPosition = GetHandByStraightArrowPosition(arrows, hands, isBeginning)

        If handsinPosition.Count = 1 Then
            Return handsinPosition.First
        Else
            Return Nothing
        End If
    End Function

    Private Shared Function GetHandByStraightArrowPosition(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol), ByVal isBeginning As Boolean) As List(Of SWSignSymbol)
        Dim handsinPosition As List(Of SWSignSymbol)
        If isBeginning Then
            handsinPosition = GetHandsBehindStraightArrow(arrows, hands)
        Else
            handsinPosition = GetHandsInFrontofStraightArrow(arrows, hands)

        End If
        Return handsinPosition
    End Function

    Private Shared Function GetHandsInFrontofStraightArrow(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim handsFound = New List(Of SWSignSymbol)
        For Each arrow As SWSignSymbol In arrows
            For Each hand As SWSignSymbol In hands
                If IsInFrontOfStraightArrow(hand, arrow) AndAlso Not handsFound.Contains(hand) Then
                    handsFound.Add(hand)
                End If
            Next
        Next
        Return handsFound
    End Function

    Private Shared Function IsInFrontOfStraightArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Dim result = False
        Select Case (arrow.SymbolDetails.Rotation)

            Case 1
                If NorthOfArrow(hand, arrow) Then result = True
            Case 2
                If NorthWestofArrow(hand, arrow) Then result = True
            Case 3
                If WestofArrow(hand, arrow) Then result = True
            Case 4
                If SouthWestofArrow(hand, arrow) Then result = True
            Case 5
                If SouthofArrow(hand, arrow) Then result = True
            Case 6
                If SouthEastofArrow(hand, arrow) Then result = True
            Case 7
                If EastofArrow(hand, arrow) Then result = True
            Case 8
                If NorthEastofArrow(hand, arrow) Then result = True
        End Select

        Return result
    End Function

    Private Shared Function NorthEastofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.Y < arrow.Y AndAlso hand.X > arrow.X
    End Function

    Private Shared Function SouthEastofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.X > arrow.X AndAlso hand.Y > arrow.Y
    End Function

    Private Shared Function SouthWestofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.Y > arrow.Y AndAlso hand.X < arrow.X
    End Function

    Private Shared Function NorthWestofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.Y < arrow.Y AndAlso hand.X < arrow.X
    End Function

    Private Shared Function EastofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.X > arrow.X
    End Function

    Private Shared Function WestofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.X < arrow.X
    End Function

    Private Shared Function SouthofArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.Y > arrow.Y
    End Function

    Private Shared Function NorthOfArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Return hand.Y < arrow.Y
    End Function

    Private Shared Function GetHandsBehindStraightArrow(ByVal arrows As List(Of SWSignSymbol), ByVal hands As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim handsFound = New List(Of SWSignSymbol)
        For Each arrow As SWSignSymbol In arrows
            For Each hand As SWSignSymbol In hands
                If IsBehindOfStraightArrow(hand, arrow) AndAlso Not handsFound.Contains(hand) Then
                    handsFound.Add(hand)
                End If
            Next
        Next
        Return handsFound
    End Function

    Private Shared Function IsBehindOfStraightArrow(ByVal hand As SWSignSymbol, ByVal arrow As SWSignSymbol) As Boolean
        Dim result = False
        Select Case (arrow.SymbolDetails.Rotation)

            Case 1
                If SouthofArrow(hand, arrow) Then result = True
            Case 2
                If SouthEastofArrow(hand, arrow) Then result = True
            Case 3
                If EastofArrow(hand, arrow) Then result = True
            Case 4
                If NorthEastofArrow(hand, arrow) Then result = True
            Case 5
                If NorthOfArrow(hand, arrow) Then result = True
            Case 6
                If NorthWestofArrow(hand, arrow) Then result = True
            Case 7
                If WestofArrow(hand, arrow) Then result = True
            Case 8
                If SouthWestofArrow(hand, arrow) Then result = True
        End Select

        Return result
    End Function

    Private Shared Function GetStraightArrows(ByVal symbols As List(Of SWSignSymbol)) As List(Of SWSignSymbol)
        Dim wallPlane = SymbolsBetween(symbols, 28609, 29224)  'Wall Plane  
        Dim floorPlane = SymbolsBetween(symbols, 34273, 34888)   'Floor Plane  
        Dim diagonalPlane = SymbolsBetween(symbols, 32737, 34232)   'Diagonal Plane  
        Dim returnList = New List(Of SWSignSymbol)()
        returnList.AddRange(CType(wallPlane, IEnumerable(Of SWSignSymbol)))
        returnList.AddRange(CType(floorPlane, IEnumerable(Of SWSignSymbol)))
        returnList.AddRange(CType(diagonalPlane, IEnumerable(Of SWSignSymbol)))
        Return returnList
    End Function

    Private Shared Function AskHandSymbols(ByVal dominantHand As Boolean, ByVal hands As List(Of SWSignSymbol), ByVal canAsk As Boolean, ByVal isBeginning As Boolean) As SWSignSymbol
        Dim foundSymbol As SWSignSymbol = Nothing
        If _canceling Then
            Return foundSymbol
        End If
        For Each symbol As SWSignSymbol In hands
            Dim answer As DialogResult
            If canAsk Then
                answer = AskHand(symbol, dominantHand, isBeginning)
            Else
                answer = DialogResult.Cancel
            End If

            If answer = DialogResult.Cancel Then
                _canceling = True
                Exit For
            End If
            If answer = DialogResult.Yes Then
                foundSymbol = symbol
                Exit For
            End If
        Next
        Return foundSymbol
    End Function

    Private Shared Function AskHand(ByVal symbol As SWSignSymbol, ByVal dominantHand As Boolean, ByVal isBeginning As Boolean) As DialogResult

        Dim answer As DialogResult
        If dominantHand Then
            If isBeginning Then
                answer = ShowSymbol.ShowDialogPersonalized("the Beginning Dominant Hand", symbol)
            Else
                answer = ShowSymbol.ShowDialogPersonalized("the Ending Dominant Hand", symbol)

            End If
        Else
            If isBeginning Then
                answer = ShowSymbol.ShowDialogPersonalized("the Beginning Non Dominant Hand", symbol)
            Else
                answer = ShowSymbol.ShowDialogPersonalized("the Ending Non Dominant Hand", symbol)

            End If
        End If
        Return answer
    End Function

    Private Shared Function GetDominantHands(ByVal symbols As IEnumerable(Of SWSignSymbol), ByVal rightDominant As Boolean) As List(Of SWSignSymbol)
        Dim dominantHands = New List(Of SWSignSymbol)()

        Dim dominantHand As Integer

        If rightDominant Then
            dominantHand = 0
        Else
            dominantHand = 1
        End If

        For Each symbol As SWSignSymbol In symbols
            If symbol.SymbolDetails.Category = 1 AndAlso symbol.Hand = dominantHand Then
                dominantHands.Add(symbol)
            End If
        Next

        Return dominantHands
    End Function

    Private Shared Function GetNonDominantHands(ByVal symbols As IEnumerable(Of SWSignSymbol), ByVal rightDominant As Boolean) As List(Of SWSignSymbol)
        Dim nonDominantHands = New List(Of SWSignSymbol)()

        Dim nonDominantHand As Integer

        If rightDominant Then
            nonDominantHand = 1
        Else
            nonDominantHand = 0
        End If

        For Each symbol As SWSignSymbol In symbols
            If symbol.SymbolDetails.Category = 1 AndAlso symbol.Hand = nonDominantHand Then
                nonDominantHands.Add(symbol)
            End If
        Next

        Return nonDominantHands
    End Function

    Private Shared Function GetMovement(ByVal symbols As List(Of SWSignSymbol), ByVal dominantHand As Boolean, ByVal nth As Integer, ByVal canAsk As Boolean, ByVal hand As SWSignSymbol, Optional ByVal rightDominant As Boolean = True) As SWSignSymbol
        Dim movements As List(Of SWSignSymbol)
        Dim foundSymbol As SWSignSymbol = Nothing
        If dominantHand Then
            movements = GetDominantMovements(symbols, rightDominant)
        Else
            movements = GetNonDominantMovements(symbols, rightDominant)

        End If

        If movements.Count = 1 Then
            Dim symbol = movements.First()
            foundSymbol = symbol
        ElseIf movements.Count > 1 Then
            foundSymbol = GetClosestMovement(movements, hand)

            If foundSymbol Is Nothing Then
                foundSymbol = AskForMovements(dominantHand, movements, canAsk, nth, foundSymbol)
            End If
        End If

        If foundSymbol IsNot Nothing Then

            Return foundSymbol
        Else
            Return Nothing
        End If
    End Function

    Private Shared Function GetClosestMovement(ByVal movements As List(Of SWSignSymbol), ByVal hand As SWSignSymbol) As SWSignSymbol
        Dim closestMovement As SWSignSymbol = Nothing
        Dim closestDistance = Double.MaxValue
        For Each movement As SWSignSymbol In movements
            Dim distance = GetSymbolDistance(movement, hand)
            If distance < closestDistance Then
                closestDistance = distance
                closestMovement = movement
            End If
        Next
        Return closestMovement
    End Function

    Private Shared Function GetSymbolDistance(ByVal movement As SWSignSymbol, ByVal hand As SWSignSymbol) As Double
        If movement IsNot Nothing AndAlso hand IsNot Nothing Then
            Dim a = Math.Abs(movement.X - hand.X)
            Dim b = Math.Abs(movement.Y - hand.Y)
            Dim distance = Math.Sqrt(a * a + b * b)
            Return distance
        Else
            Return Double.MaxValue
        End If
    End Function

    Private Shared Function AskForMovements(ByVal dominantHand As Boolean, ByVal movements As List(Of SWSignSymbol), ByVal canAsk As Boolean, ByVal nth As Integer, ByVal foundSymbol As SWSignSymbol) As SWSignSymbol
        If _canceling Then
            Return foundSymbol
        End If
        For Each symbol As SWSignSymbol In movements
            Dim answer As DialogResult
            If canAsk Then
                Dim cardinality As String = String.Empty
                If nth = 1 Then
                    cardinality = " first "
                ElseIf nth = 2 Then
                    cardinality = " second "
                ElseIf nth = 3 Then
                    cardinality = " third "
                ElseIf nth = 4 Then
                    cardinality = " fourth "
                End If
                If dominantHand Then
                    answer = ShowSymbol.ShowDialogPersonalized("the" & cardinality & "Movement of the Dominant Hand", symbol)
                Else
                    answer = ShowSymbol.ShowDialogPersonalized("the" & cardinality & "Movement of the Non Dominant Hand", symbol)

                End If
            Else
                answer = DialogResult.Cancel
            End If
            If answer = DialogResult.Cancel Then
                _canceling = True
                Exit For
            End If
            If answer = DialogResult.Yes Then
                foundSymbol = symbol
                Exit For
            End If
        Next
        Return foundSymbol
    End Function

    Private Shared Function GetDominantMovements(ByVal symbols As List(Of SWSignSymbol), ByVal rightDominant As Boolean) As List(Of SWSignSymbol)
        Dim dominantMovements = New List(Of SWSignSymbol)()

        For Each symbol As SWSignSymbol In symbols
            If IsDominantMovement(symbol, rightDominant) Then
                dominantMovements.Add(symbol)
            End If
        Next

        Return dominantMovements
    End Function

    Private Shared Function GetNonDominantMovements(ByVal symbols As List(Of SWSignSymbol), ByVal rightDominant As Boolean) As List(Of SWSignSymbol)
        Dim nonDominantMovements = New List(Of SWSignSymbol)()

        For Each symbol As SWSignSymbol In symbols
            If IsNonDominantMovement(symbol, rightDominant) Then
                nonDominantMovements.Add(symbol)
            End If
        Next

        Return nonDominantMovements
    End Function

    Private Shared Function IsDominantMovement(ByVal symbol As SWSignSymbol, ByVal rightDominant As Boolean) As Boolean
        If symbol.SymbolDetails.Category = 2 Then

            If symbol.Code >= 25057 AndAlso symbol.Code <= 28584 Then
                Return True
            End If
            If rightDominant Then
                If symbol.SymbolDetails.Fill = 1 OrElse symbol.SymbolDetails.Fill = 4 Then
                    Return True
                Else
                    Return False
                End If
            Else
                If symbol.SymbolDetails.Fill = 2 OrElse symbol.SymbolDetails.Fill = 5 Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Private Shared Function IsNonDominantMovement(ByVal symbol As SWSignSymbol, ByVal rightDominant As Boolean) As Boolean
        If symbol.SymbolDetails.Category = 2 Then
            If rightDominant Then
                If symbol.SymbolDetails.Fill = 2 Then
                    Return True
                Else
                    Return False
                End If
            Else
                If symbol.SymbolDetails.Fill = 1 Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Private Shared Function GetDynamics(ByVal symbols As List(Of SWSignSymbol), dominantHand As Boolean, canAsk As Boolean) As SWSignSymbol
        Dim foundSymbol As SWSignSymbol = Nothing
        If _canceling Then
            Return foundSymbol
        End If
        Dim dynamics = GetDynamics(symbols)
        If dynamics.Count = 1 Then
            foundSymbol = dynamics.First()
        ElseIf dynamics.Count > 1 Then
            For Each symbol As SWSignSymbol In dynamics
                Dim answer As DialogResult
                If canAsk Then
                    If dominantHand Then
                        answer = ShowSymbol.ShowDialogPersonalized("a Dynamic symbol for the Dominant Hand", symbol)
                    Else
                        answer = ShowSymbol.ShowDialogPersonalized("a Dynamic symbol for Non Dominant Hand", symbol)
                    End If
                Else
                    answer = DialogResult.Cancel
                End If
                If answer = DialogResult.Cancel Then
                    _canceling = True
                    Exit For
                End If
                If answer = DialogResult.Yes Then
                    foundSymbol = symbol

                End If
            Next

        End If
        Return foundSymbol
    End Function

    Private Shared Function GetDynamics(ByVal symbols As List(Of SWSignSymbol)) As IEnumerable(Of SWSignSymbol)
        Return (From symbol In symbols Where symbol.SymbolDetails.Category = 3).ToList()
    End Function

    Public Shared Function OrderSuggestion2(ByVal symbs As List(Of SWSignSymbol)) As List(Of SWSequence)
        Dim symbols = symbs.ToList()
        Dim newSequence As New List(Of SWSequence)()
        '1) Single hand - BaseSymbol order 
        '2) Orientation - Fill and Rotation
        '3) Second hand - BaseSymbol order
        '4) Second hand orientation - Fill and Rotation
        '5) Contacts (default is hand to hand contact first) (however many contacts in the sequence) 
        '- hand to body (head, shoulders, waist not shown)
        '- hand to other hand
        '- hand to head (head arc ordered including eyes, nose, ears, etc. lots of individual markers for location)
        '- hand to body
        '6) Movements (vertical, horizontal, arcs - as listed in the  SignPuddle)
        '7) Speed
        '8) Facial Expression (these are always being added)

        Return newSequence
    End Function
End Class
