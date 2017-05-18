Imports System.Drawing
Imports System.Windows.Forms
Imports NUnit.Framework
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.SWS

Partial Public Class Editor

#Region "Sign"

    Public Function ToImage() As Image
        Return Me.mySWSign.Render
    End Function

    Private Sub Sign_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        'Dim ControlActive As Object = ActiveControl
        Dim ChangeSymbolIn As Boolean = CurrentFrame.SelectedSymbolCount > 0

        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "signarea.htm")
            Case Keys.Right
                If ChangeSymbolIn Then
                    AddUndo()
                    If e.Shift = True Then
                        CurrentFrame.MoveSelected(ArrowDirection.Right, 15)
                        DisplaySign()
                    Else
                        CurrentFrame.MoveSelected(ArrowDirection.Right, 1)
                        DisplaySign()
                    End If

                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.Left
                If ChangeSymbolIn Then
                    AddUndo()
                    If e.Shift = True Then
                        CurrentFrame.MoveSelected(ArrowDirection.Left, 15)
                        DisplaySign()
                    Else
                        CurrentFrame.MoveSelected(ArrowDirection.Left, 1)
                        DisplaySign()
                    End If

                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.Up
                If ChangeSymbolIn Then
                    AddUndo()
                    If e.Shift = True Then
                        CurrentFrame.MoveSelected(ArrowDirection.Up, 15)
                        DisplaySign()

                    ElseIf e.Control Then
                        MoveUp()

                    Else
                        CurrentFrame.MoveSelected(ArrowDirection.Up, 1)
                        DisplaySign()

                    End If

                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.Down
                If ChangeSymbolIn Then
                    AddUndo()
                    If e.Shift = True Then
                        CurrentFrame.MoveSelected(ArrowDirection.Down, 15)
                        DisplaySign()

                    ElseIf e.Control Then
                        MoveDown()
                    Else

                        CurrentFrame.MoveSelected(ArrowDirection.Down, 1)
                        DisplaySign()

                    End If

                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.Enter
                AddUndo()
                If CurrentFrame.SelectedSymbolCount > 0 Then
                    CurrentFrame.UnSelectSymbols()
                    DisplaySign()
                Else
                    If CurrentFrame.SignSymbols.Count > 0 Then
                        CurrentFrame.SelectSymbol(0)
                        DisplaySign()
                    End If
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Escape
                Area = AreaEnm.Choose
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Insert
                Dim NewSignSymbol As SWSignSymbol = Me.symbolOut.Clone
                'NewSignSymbol.Update()
                'If NewSignSymbol.IsValid Then
                InsertSymbolIntoSign(NewSignSymbol.Code)
                DisplaySign()
                'End If
            Case Keys.Delete
                If e.Control Then
                    DeleteSign()
                ElseIf ChangeSymbolIn Then
                    RemoveSymbols()
                End If
            Case Keys.N
                If e.Alt And e.Shift And e.Control Then
                    'GetPreviousSymbolAddSelected()
                ElseIf e.Alt And e.Shift Then
                    'GetNextSymbolAddSelected()
                ElseIf e.Alt And e.Control Then
                    GetPreviousSymbol()

                ElseIf e.Alt Then
                    GetNextSymbol()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.M
                If e.Control Then
                    MirrorSign(mySWSign)
                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.H
                If e.Control Then
                    ChangeArrowHands(mySWSign)
                End If
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.C
                If e.Control And e.Shift Then
                    CopySignImageCrop()
                ElseIf e.Control Then
                    CopySign()
                ElseIf e.Alt And e.Shift And ChangeSymbolIn Then
                    AddUndo()
                    CurrentFrame.CenterHeadinSign()
                    DisplaySign()
                ElseIf e.Alt And ChangeSymbolIn Then
                    AddUndo()
                    CurrentFrame.CenterSymbols()
                    DisplaySign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.D
                If e.Alt And ChangeSymbolIn Then
                    DuplicateSymbols()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.H
                If e.Alt And ChangeSymbolIn Then
                    AddUndo()
                    ChangeHandColor()
                    DisplaySign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.P
                If e.Alt And ChangeSymbolIn Then
                    AddUndo()
                    ChangePalmColor()
                    DisplaySign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.B
                If e.Alt Then
                    AddUndo()
                    ChangeBkgColor()
                    DisplaySign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.E
                If e.Alt Then
                    CurrentFrame.EraseSign()
                    DisplaySign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.F
                If e.Control And e.Alt Then
                    RemoveFrame()
                ElseIf e.Control And e.Shift Then
                    PreviousFrame()
                ElseIf e.Control Then
                    NextFrame()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.O
                If e.Alt Then
                    Me.mySWSign.OverlapSymbols()
                    DisplaySign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.V
                If e.Control Then
                    PasteSign()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Z
                If e.Control Then
                    Undo()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Y
                If e.Control Then
                    Redo()
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Add
                If e.Control Then
                    SymbolAlotLarger()
                Else
                    SymbolLarger()
                End If
                e.SuppressKeyPress = True
                e.Handled = True

            Case Keys.Subtract
                If e.Control Then
                    SymbolAlotSmaller()
                Else
                    SymbolSmaller()
                End If
                e.SuppressKeyPress = True
                e.Handled = True

            Case Else
                If ChangeSymbolIn Then
                    Select Case e.KeyCode
                        Case Keys.NumPad1, Keys.D1
                            MoveSelectionToRegion(1)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad2, Keys.D2
                            MoveSelectionToRegion(2)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad3, Keys.D3
                            MoveSelectionToRegion(3)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad4, Keys.D4
                            MoveSelectionToRegion(4)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad5, Keys.D5
                            MoveSelectionToRegion(5)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad6, Keys.D6
                            MoveSelectionToRegion(6)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad7, Keys.D7
                            MoveSelectionToRegion(7)
                            e.SuppressKeyPress = True
                            e.Handled = True
                        Case Keys.NumPad8, Keys.D8
                            MoveSelectionToRegion(8)
                        Case Keys.NumPad9, Keys.D9
                            MoveSelectionToRegion(9)
                    End Select
                End If

        End Select
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub


    Private Sub BottomLeft1ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles BottomLeft1ToolStripMenuItem.Click
        MoveSelectionToRegion(1)
    End Sub

    Private Sub Bottom2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Bottom2ToolStripMenuItem.Click
        MoveSelectionToRegion(2)
    End Sub

    Private Sub BottomRight3ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles BottomRight3ToolStripMenuItem.Click
        MoveSelectionToRegion(3)
    End Sub

    Private Sub MiddleLeft4ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles MiddleLeft4ToolStripMenuItem.Click
        MoveSelectionToRegion(4)
    End Sub

    Private Sub Middle5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Middle5ToolStripMenuItem.Click
        MoveSelectionToRegion(5)
    End Sub

    Private Sub MiddleRigh6ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles MiddleRigh6ToolStripMenuItem.Click
        MoveSelectionToRegion(6)
    End Sub

    Private Sub TopLeft7ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles TopLeft7ToolStripMenuItem.Click
        MoveSelectionToRegion(7)
    End Sub

    Private Sub Top8ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Top8ToolStripMenuItem.Click
        MoveSelectionToRegion(8)
    End Sub

    Private Sub TopRight9ToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles TopRight9ToolStripMenuItem.Click
        MoveSelectionToRegion(9)
    End Sub

    Private Sub GetNextSymbolAddSelected()
        CurrentFrame.SelectNextAddSymbol()
        DisplaySign()
    End Sub

    Private Sub GetPreviousSymbolAddSelected()
        CurrentFrame.SelectPreviousAddSymbol()
        DisplaySign()
    End Sub

    Private Sub GetNextSymbol()
        CurrentFrame.SelectNextSymbol()
        DisplaySign()
    End Sub

    Private Sub GetPreviousSymbol()
        CurrentFrame.SelectPreviousSymbol()
        DisplaySign()
    End Sub

    Private Sub SymbolLarger()
        AddUndo()
        CurrentFrame.MakeSymbolsLarger()
        DisplaySign()
    End Sub
    Private Sub SymbolAlotLarger()
        AddUndo()
        CurrentFrame.MakeSymbolsAlotLarger()
        DisplaySign()
    End Sub
    Private Sub SymbolAlotSmaller()
        AddUndo()
        CurrentFrame.MakeSymbolsAlotSmaller()
        DisplaySign()
    End Sub
    Private Sub SymbolSmaller()
        AddUndo()
        CurrentFrame.MakeSymbolsSmaller()
        DisplaySign()
    End Sub

    Private Sub MoveSelectionToRegion(ByVal Region As Integer)
        AddUndo()
        CurrentFrame.MoveSelectionToRegion(Region)
        DisplaySign()
    End Sub

    Private Sub SetHandColorChangeSymbolIns(ByVal HandColor As Color)

        Dim symbol As SWSignSymbol
        If CurrentFrame.SelectedSymbolCount > 0 Then
            For Each symbol In CurrentFrame.SignSymbols
                If symbol.IsSelected Then
                    symbol.Handcolor = HandColor.ToArgb
                End If
            Next
        Else
            For Each symbol In CurrentFrame.SignSymbols
                symbol.Handcolor = HandColor.ToArgb
            Next
        End If
        DisplaySign()
    End Sub

    Private Sub SetPalmColorChangeSymbolIns(ByVal PalmColor As Color)
        Dim Symbol As SWSignSymbol
        If CurrentFrame.SelectedSymbolCount > 0 Then
            For Each Symbol In CurrentFrame.SignSymbols
                If Symbol.IsSelected Then
                    Symbol.Palmcolor = PalmColor.ToArgb
                End If
            Next
        Else
            For Each Symbol In CurrentFrame.SignSymbols
                Symbol.Palmcolor = PalmColor.ToArgb
            Next
        End If
        DisplaySign()
    End Sub

    Private Function GetCurrentFrame(ByVal Sign As SwSign) As SWFrame
        If CurrentFrame IsNot Nothing Then

            SWFrame.LoadSequence(TVSequence, CurrentFrame, SequenceMenuStrip)
        End If
        If Sign IsNot Nothing AndAlso Sign.Frames.Count > Sign.CurrentFrameIndex Then
            Return (Sign.Frames(Sign.CurrentFrameIndex))
        Else
            'Error find and fix
            Return Nothing
        End If
    End Function

    Private Sub PreviousFrame()
        Me.mySWSign.PreviousFrame()
        CurrentFrame = GetCurrentFrame(Me.mySWSign)
        DisplaySign()
        LoadSequence()
    End Sub

    Private Sub NextFrame()
        Me.mySWSign.NextFrame()
        CurrentFrame = GetCurrentFrame(Me.mySWSign)
        DisplaySign()
        LoadSequence()
    End Sub

    Private Sub RemoveFrame()
        Me.mySWSign.RemoveFrame(Me.mySWSign.CurrentFrameIndex)
        If Me.mySWSign.CurrentFrameIndex > Me.mySWSign.Frames.Count - 1 Then
            Me.mySWSign.CurrentFrameIndex = Me.mySWSign.Frames.Count - 1
        End If
        CurrentFrame = GetCurrentFrame(Me.mySWSign)
        DisplaySign()
        LoadSequence()
    End Sub

    Private Sub PBSign_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles PBSign.DragDrop

        Dim pPoint As Point = PBSign.PointToClient(New Point(e.X, e.Y))


        'Passing a sign to the PictureBox
        'Drag Drop from self.  Move Selected symbols over.
        If CType(e.Data.GetData(GetType(String)), Object) Is Nothing AndAlso CurrentFrame.SignSymbols.Count > 0 Then
            AddUndo()
            CurrentFrame.MoveSelected(New Point((pPoint.X - StartPoint.X), (pPoint.Y - StartPoint.Y)))
        ElseIf e.Data.GetData(GetType(String)).ToString = PBsymbolOut.Name Then
            Dim newSignSymbol As New SWSignSymbol
            newSignSymbol.Code = Me.symbolOut.Code
            'NewSignSymbol.Update()
            'If NewSignSymbol.IsValid Then
            AddUndo()
            InsertSymbolIntoSign(newSignSymbol.Code, pPoint.X - SymbolStartOffset.X, pPoint.Y - SymbolStartOffset.Y,
                                 Color.Black, Color.White, HandChooser.Hand)
            Area = AreaEnm.Sign
            'End If
        ElseIf e.Data.GetData(GetType(String)).ToString = TVAllGroups.Name Then

            CurrentFrame.UnSelectSymbols()
            Dim symbolFromAllGroups As New SWSignSymbol
            symbolFromAllGroups.Code = SWSymbol.CodefromId(TVAllGroups.SelectedNode.Name)
            If Not symbolFromAllGroups.Code = 0 Then
                AddUndo()
                InsertSymbolIntoSign(symbolFromAllGroups.Code, pPoint.X, pPoint.Y, Color.Black, Color.White, 0)
                Area = AreaEnm.Sign
            End If
        ElseIf e.Data.GetData(GetType(String)).ToString = TVChooser.Name Then
            Dim symbolFromSymbolList As New SWSignSymbol
            symbolFromSymbolList.Code = SWSymbol.CodefromId(TVChooser.SelectedNode.Name)
            If Not symbolFromSymbolList.Code = 0 Then
                AddUndo()
                InsertSymbolIntoSign(symbolFromSymbolList.Code, pPoint.X, pPoint.Y, Color.Black, Color.White, 0)
                Area = AreaEnm.Sign
            End If
        ElseIf e.Data.GetData(GetType(String)).ToString = TVHand.Name Then
            Dim symbolFromSymbolList As New SWSignSymbol
            symbolFromSymbolList.Code = SWSymbol.CodefromId(TVHand.SelectedNode.Name)
            If Not symbolFromSymbolList.Code = 0 Then
                AddUndo()
                InsertSymbolIntoSign(symbolFromSymbolList.Code, pPoint.X, pPoint.Y, Color.Black, Color.White, 0)
                Area = AreaEnm.Sign
            End If
        ElseIf e.Data.GetData(GetType(String)).ToString = TVFavoriteSymbols.Name Then
            AddSelectedFavorite(pPoint)
        End If
        
        DisplaySign()
    End Sub


    Public Sub DisplaySign()
        If Not DisplayingSign Then
            DisplayingSign = True
            PBSign.Image = SWDrawing.DrawSWDrawing(Me.mySWSign, -1, grid.Checked)
            DisplayingSign = False
        End If
    End Sub

    Private Sub OnlyOneSymbolJustSelected()
        If Not UpdateSignSymbolSelected Then
            UpdateSignSymbolSelected = True
            If CurrentFrame.SelectedSymbolCount = 1 Then
                For Each symbol As SWSignSymbol In CurrentFrame.SignSymbols
                    If symbol.IsSelected Then
                        SetSymbolIn(symbol)
                        Exit Sub
                    End If

                Next
            End If
            UpdateSignSymbolSelected = False
        End If
    End Sub
    Private Sub SetSymbolIn(symbol As SWSignSymbol)
        symbolIn.Code = symbol.Code
        HandChooser.Reset(symbol.Code, symbol.Hand)
        AllGroupsFind(symbol.Code, False)

        UpdateSignSymbolSelected = False
    End Sub
    Private Sub PBsymbolOut_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PBsymbolOut.MouseDown
        If PBsymbolOut.Image IsNot Nothing Then
            Me.SymbolStartOffset = New Point(CInt(e.X + 2 - (PBsymbolOut.Width - PBsymbolOut.Image.Width) / 2),
                                             CInt(e.Y + 1 - (PBsymbolOut.Height - PBsymbolOut.Image.Height) / 2))
            If PBsymbolOut.Image IsNot Nothing Then
                PBsymbolOut.DoDragDrop(PBsymbolOut.Name, DragDropEffects.Copy)
            End If
        End If
    End Sub

    Private Sub PBSign_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles PBSign.DragEnter
        'Dim SendingControl As Control = CType(sender, Control)

        e.Effect = DragDropEffects.Copy Or DragDropEffects.Move
    End Sub

    Private Sub PBSign_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PBSign.MouseDown
        Dim TestRegion As Region
        Dim isLeftButton As Boolean = (e.Button = MouseButtons.Left)
        Dim isOnSymbol As Boolean = False
        Dim isOnChangeSymbolIn As Boolean = False
        Dim isControlKey As Boolean = ((ModifierKeys And Keys.Control) = Keys.Control)
        Dim Symbol As SWSignSymbol

        'Mousedown on PictureBox at this point
        StartPoint = New Point(e.X, e.Y)

        If isLeftButton Then
            AddUndo()
            'If not clicked on a selected item, unselect.
            'Check for isOnSymbol and isOnChangeSymbolIn
            For Each Symbol In CurrentFrame.SignSymbols
                'CheckRowInformation(Row)
                'TestRegion the size and location of current symbol
                TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y, CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                      CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                'If Symbol is selected and the mousedown was on top
                If TestRegion.IsVisible(StartPoint) Then
                    'Clicked on a  symbol
                    isOnSymbol = True
                    If Symbol.IsSelected Then
                        isOnChangeSymbolIn = True
                    End If
                End If
            Next


            If isOnChangeSymbolIn Then
                If isControlKey Then
                    'Remove from Selection
                    For Each Symbol In CurrentFrame.SignSymbols
                        'TestRegion the size and location of current symbol
                        TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y,
                                                              CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                              CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                        'If Symbol is selected and the mousedown was on top
                        If TestRegion.IsVisible(StartPoint) Then
                            'Clicked on a  symbol
                            If Symbol.IsSelected Then
                                CurrentFrame.UNSelectSymbol(Symbol)
                                'Symbol.isSelected = False
                                'CurrentFrame.SelectedSymbolCount -= 1
                            End If
                        End If
                    Next
                Else
                    If CurrentFrame.SelectedSymbolCount > 0 Then
                        PBSign.DoDragDrop(New Object, DragDropEffects.Move)
                    End If
                End If

            ElseIf isOnSymbol Then
                If isControlKey Then
                    'Add to Selection
                    For Each Symbol In CurrentFrame.SignSymbols
                        'TestRegion the size and location of current symbol
                        TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y,
                                                              CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                              CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                        'If Symbol unselected and the mousedown was on top
                        If TestRegion.IsVisible(StartPoint) Then
                            'Clicked on a  symbol
                            If Not Symbol.IsSelected Then
                                'Symbol.isSelected = True
                                'CurrentFrame.SelectedSymbolCount += 1
                                CurrentFrame.SelectSymbol(Symbol)
                            End If
                        End If
                    Next

                Else
                    'is on unselected Symbol and Control Key not down
                    'Unselect other symbols and begin DragDrop for this symbol.
                    'Select current symbol
                    CurrentFrame.UnSelectSymbols()
                    For Each Symbol In CurrentFrame.SignSymbols
                        'TestRegion the size and location of current symbol
                        TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y,
                                                              CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                              CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                        'If Symbol is selected and the mousedown was on top
                        If TestRegion.IsVisible(StartPoint) Then
                            'Clicked on a  symbol
                            'Symbol.isSelected = True
                            'CurrentFrame.SelectedSymbolCount = 1
                            CurrentFrame.SelectSymbol(Symbol)
                        End If
                    Next
                    If CurrentFrame.SelectedSymbolCount > 0 Then
                        PBSign.DoDragDrop(New Object, DragDropEffects.Move)
                    End If
                End If

            Else
                If isControlKey Then
                    'Didn´t click on a selected symbol.  Control Key down
                    isSelecting = True
                Else
                    'Didn´t click on a selected symbol. Unselect all symbols. Control Key not down
                    CurrentFrame.UnSelectSymbols()
                    'Didn´t click on a symbol.  
                    isSelecting = True
                End If

            End If
            'Me.TextBox3.Text = "isOnSymbol " & isOnSymbol & "StartPoint " & StartPoint.ToString & "LeftButton " & (e.Button = Windows.Forms.MouseButtons.Left) & "isSelecting " & isSelecting & "Selected Symbols" & CurrentFrame.SelectedSymbolCount
        End If
    End Sub

    Private Sub PBSign_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PBSign.MouseMove
        Dim NewTooltipString As String = String.Empty
        'MouseMove None selected and Drag
        If isSelecting Then

            ' Hide the previous rectangle by calling the DrawReversibleFrame 
            ' method with the same parameters.
            ControlPaint.DrawReversibleFrame(theRectangle, BackColor,
                                             FrameStyle.Dashed)

            ' Calculate the endpoint and dimensions for the new rectangle, 
            ' again using the PointToScreen method.
            Dim control As Control = CType(sender, Control)
            Dim StartPoint1 As Point = control.PointToScreen(StartPoint)

            Dim endPoint As Point = control.PointToScreen(New Point(e.X, e.Y))
            Dim width As Integer = endPoint.X - StartPoint1.X
            Dim height As Integer = endPoint.Y - StartPoint1.Y

            ' Calculate the startPoint by using the PointToScreen 
            ' method.
            theRectangle = New Rectangle(StartPoint1.X, StartPoint1.Y,
                                         width, height)

            ' Draw the new rectangle by calling DrawReversibleFrame again.  
            ControlPaint.DrawReversibleFrame(theRectangle, Me.BackColor,
                                             FrameStyle.Dashed)
            'MouseMove and none selected
        ElseIf CurrentFrame IsNot Nothing AndAlso Not CurrentFrame.SelectedSymbolCount > 0 Then
            Dim TestRegion As Region

            Dim MovePoint As New Point(e.X, e.Y)
            'Check if know where startpoint is

            Dim RegionFound As Boolean = False
            Dim Symbol As SWSignSymbol
            For Each Symbol In CurrentFrame.SignSymbols
                'CheckRowInformation(Row)
                TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y, CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                      CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                'If in selection rectangle

                If TestRegion.IsVisible(MovePoint) Then
                    NewTooltipString = Symbol.SymbolDetails.BaseName
                    RegionFound = True
                    Exit For
                End If

            Next
            If RegionFound AndAlso Not SymbolToolTip.Active Then
                SymbolToolTip.Active = True
                SymbolToolTip.SetToolTip(PBSign, NewTooltipString)
            ElseIf Not RegionFound Then
                SymbolToolTip.Active = False
            End If
        End If
    End Sub

    Private Sub PBSign_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PBSign.MouseUp
        Dim TestRegion As Region
        'Dim isLeftButton As Boolean = (e.Button = Windows.Forms.MouseButtons.Left)
        Dim isControlKey As Boolean = ((ModifierKeys And Keys.Control) = Keys.Control)
        Dim Symbol As SWSignSymbol

        If (e.Button = MouseButtons.Left) Then
            'Set End point
            EndPoint = New Point(e.X, e.Y)
            'Check if know where startpoint is
            If Not StartPoint.IsEmpty Then
                'Get selection rectangle   
                Dim _
                    SelectionRec As _
                        New Rectangle(StartPoint.X, StartPoint.Y, EndPoint.X - StartPoint.X, EndPoint.Y - StartPoint.Y)
                If SelectionRec.Width = 0 Then
                    SelectionRec.Width = 1
                End If
                If SelectionRec.Height = 0 Then
                    SelectionRec.Height = 1
                End If
                SelectionRec = SwSign.MakepositiveRect(SelectionRec)

                If isSelecting Then
                    If isControlKey Then
                        'Add to Selection
                        For Each Symbol In CurrentFrame.SignSymbols
                            'TestRegion the size and location of current symbol
                            TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y,
                                                                  CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                                  CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                            'If Symbol unselected and the mousedown was on top
                            If TestRegion.IsVisible(SelectionRec) Then
                                'Clicked on a  symbol
                                If Not Symbol.IsSelected Then
                                    'Symbol.isSelected = True
                                    'CurrentFrame.SelectedSymbolCount += 1
                                    CurrentFrame.SelectSymbol(Symbol)
                                End If
                            End If
                        Next
                    Else
                        CurrentFrame.UnSelectSymbols()
                        'Add to Selection
                        For Each Symbol In CurrentFrame.SignSymbols
                            'TestRegion the size and location of current symbol
                            TestRegion = New Region(New Rectangle(Symbol.X, Symbol.Y,
                                                                  CInt(Symbol.SymbolDetails.Width * Symbol.Size),
                                                                  CInt(Symbol.SymbolDetails.Height * Symbol.Size)))
                            'If Symbol unselected and the mousedown was on top
                            If TestRegion.IsVisible(SelectionRec) Then
                                'Clicked on a  symbol
                                If Symbol IsNot Nothing AndAlso Not Symbol.IsSelected Then
                                    'Symbol.isSelected = True
                                    'CurrentFrame.SelectedSymbolCount += 1
                                    CurrentFrame.SelectSymbol(Symbol)
                                End If
                            End If
                        Next
                    End If
                End If


                'Reset rectangle
                theRectangle = New Rectangle(New Point(0, 0), New Size(0, 0))

                DisplaySign()
                'Me.TextBox1.Text = CurrentFrame.SelectedSymbolCount
                'Stop showing selectin rectangle
                isSelecting = False

            End If
        End If
        ''If 1 symbol Selected Set controls
        'If CurrentFrame.SelectedSymbolCount = 1 Then
        '    OnlyOneSymbolJustSelected()
        'End If
    End Sub

    Private Sub CopySign()
        Me.mySWSign.SetClipboard()
    End Sub

    Private Sub CopySignImageCrop()
        Me.mySWSign.SetClipboardCrop(CurrentFrame.GetSWSignBounds(CurrentFrame))
    End Sub

    Private Sub TSMICenter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMICenter.Click
        AddUndo()
        CurrentFrame.CenterSymbols()
        DisplaySign()
    End Sub

    Private Sub TSMICenterHead_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMICenterHead.Click
        AddUndo()
        CurrentFrame.CenterHeadinSign()
        DisplaySign()
    End Sub

    Private Sub TSMIRemoveSymbols_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIRemoveSymbols.Click
        RemoveSymbols()
    End Sub

    Private Sub RemoveSymbols()
        AddUndo()
        If CurrentFrame.SelectedSymbolCount > 0 Then
            CurrentFrame.RemoveSelected()
            CurrentFrame.RenumberZ()
        Else
            CurrentFrame.RemoveAll()
        End If
        DisplaySign()
    End Sub

    Private Sub MoveUpToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIMoveUp.Click
        MoveUp()
    End Sub

    Private Sub MoveUp()
        AddUndo()
        CurrentFrame.MoveSymbolUp()
        DisplaySign()
    End Sub

    Private Sub MoveDownToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIMoveDown.Click
        MoveDown()
    End Sub

    Private Sub MoveDown()
        AddUndo()
        CurrentFrame.MoveSymbolDown()
        DisplaySign()
    End Sub

    Private Sub MoveBottomMenuItem_Click(sender As Object, e As EventArgs) Handles MoveBottomMenuItem.Click
        MoveBottom()
    End Sub
    Private Sub MoveBottom()
        AddUndo()
        CurrentFrame.MoveSymbolBottom()
        DisplaySign()
    End Sub
    Private Sub MoveTopMenuItem_Click(sender As Object, e As EventArgs) Handles MoveTopMenuItem.Click
        MoveTop()
    End Sub
    Private Sub MoveTop()
        AddUndo()
        CurrentFrame.MoveSymbolTop()
        DisplaySign()
    End Sub
    Private Sub TSMIDuplicateSymbols_Click(sender As Object, e As EventArgs) Handles TSMIDuplicateSymbols.Click
        DuplicateSymbols()
        DisplaySign()
    End Sub

    Private Sub DuplicateSymbols()
        AddUndo()
        If CurrentFrame.SelectedSymbolCount > 0 Then
            CurrentFrame.DuplicateSelected()
        Else
            CurrentFrame.DuplicateAll()
        End If
        DisplaySign()
    End Sub

    Public Sub ChangeBkgColor()
        AddUndo()

        Dim result As DialogResult = ColorDialog1.ShowDialog()

        If (result = DialogResult.OK) Then
            Me.mySWSign.BkColor = ColorDialog1.Color
            'Me.mySWSign.BKColor = Color.Transparent
        End If
        DisplaySign()
    End Sub

    Public Sub ChangePalmColor()
        AddUndo()

        Dim result As DialogResult = ColorDialog1.ShowDialog()

        If (result = DialogResult.OK) Then
            SetPalmColorChangeSymbolIns(ColorDialog1.Color)
        End If
        DisplaySign()
    End Sub

    Public Sub ChangeHandColor()
        AddUndo()
        Dim result As DialogResult = ColorDialog1.ShowDialog()

        If (result = DialogResult.OK) Then
            SetHandColorChangeSymbolIns(ColorDialog1.Color)
        End If
        DisplaySign()
    End Sub

    Private Sub PBSign_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles PBSign.MouseEnter
        Area = AreaEnm.Sign
    End Sub

    Private Sub PBSign_MouseClick(sender As Object, e As MouseEventArgs) Handles PBSign.MouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not Area = AreaEnm.Sign Then
                Area = AreaEnm.Sign
            End If
        End If
        If e.Button = System.Windows.Forms.MouseButtons.Middle Then
            If CurrentFrame.SelectedSymbolCount = 1 Then
                OnlyOneSymbolJustSelected()
            End If
        End If
    End Sub
 
    Private Sub TSMIInsertsymbolOuts_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TSMIInsertsymbolOuts.Click
        InsertSymbolIntoSign(Me.symbolOut.Code)
    End Sub

    Private Sub InsertSymbolIntoSign(ByVal code As Integer, ByVal X As Integer, ByVal y As Integer,
                                     ByVal HandColor As Color, ByVal PalmColor As Color, ByVal Hand As Integer)
        If Not SignContainsPunctuation(mySWSign) Then

            If (isPunctuation(code) AndAlso Not SignhasSymbols(mySWSign)) OrElse (Not isPunctuation(code)) Then
                AddUndo()
                CurrentFrame.UnSelectSymbols()
                CurrentFrame.InsertSymbolIntoSign(code, True, X, y, HandColor, PalmColor, Hand)
                LoadSequence()
            Else
                OnlyOnePunctuationError()

            End If
        Else
            OnlyOnePunctuationError()
        End If
    End Sub

    Private Sub InsertFrameIntoCurrentFrame(ByVal frame As SWFrame)

        CurrentFrame.UnSelectSymbols()
        For Each symbol In frame.SignSymbols

            If Not SignContainsPunctuation(mySWSign) Then

                If _
                    (isPunctuation(symbol.Code) AndAlso Not SignhasSymbols(mySWSign)) OrElse
                    (Not isPunctuation(symbol.Code)) Then
                    AddUndo()

                    CurrentFrame.InsertSymbolIntoSign(symbol.Code, True, symbol.X, symbol.Y,
                                                      Color.FromArgb(symbol.Handcolor), Color.FromArgb(symbol.Palmcolor),
                                                      symbol.Hand)
                    LoadSequence()
                Else
                    OnlyOnePunctuationError()

                End If
            Else
                OnlyOnePunctuationError()
            End If
        Next
    End Sub

    Private Sub InsertSymbolIntoSign(ByVal code As Integer)
        Me.mySWSign.Frames(Me.mySWSign.CurrentFrameIndex).UnSelectSymbols()
        CurrentFrame.InsertSymbolIntoSign(code, True, 0, 0, Color.Black, Color.White, HandChooser.Hand)
    End Sub

    Private Sub TSMIDeleteSign_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIDeleteSign.Click
        DeleteSign()
    End Sub

    Private Sub DeleteSign()
        AddUndo()
        CurrentFrame.EraseSign()
        CurrentFrame.UnSelectSymbols()
        DisplaySign()
    End Sub

    Private Sub TSMILarger_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMILarger.Click
        SymbolLarger()
    End Sub

    Private Sub TSMISmaller_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMISmaller.Click
        SymbolSmaller()
    End Sub


    Private Sub TSMINextSymbol_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMINextSymbol.Click
        GetNextSymbol()
    End Sub

    Private Sub TSMIPreviousSymbol_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIPreviousSymbol.Click
        GetPreviousSymbol()
    End Sub

    Private Sub TSMINextAddToSelected_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TSMINextAddToSelected.Click
        GetNextSymbolAddSelected()
    End Sub

    Private Sub TSMIPreviousAddToSelected_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TSMIPreviousAddToSelected.Click
        GetPreviousSymbolAddSelected()
    End Sub

    Private Sub TSMIBackOfHandColor_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TSMIBackOfHandColor.Click
        AddUndo()
        ChangeHandColor()
        DisplaySign()
    End Sub

    Private Sub TSMIPalmOfHandColor_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TSMIPalmOfHandColor.Click
        AddUndo()
        ChangePalmColor()
        DisplaySign()
    End Sub

    Private Sub TSMIBackgroundColor_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles TSMIBackgroundColor.Click
        AddUndo()
        Me.ChangeBkgColor()
        DisplaySign()
    End Sub

    Private Sub TSMIUndo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIUndo.Click
        Undo()
    End Sub

    Private Sub TSMIRedo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIRedo.Click
        Redo()
    End Sub

    Private Sub TSMINextFrame_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMINextFrame.Click
        NextFrame()
    End Sub

    Private Sub TSMIPreviousFrame_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIPreviousFrame.Click
        PreviousFrame()
    End Sub

    Private Sub TSMIRemoveFrame_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMIRemoveFrame.Click
        RemoveFrame()
    End Sub

    Private Sub TSMICopy_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TSMICopy.Click
        CopySign()
    End Sub

    Private Sub PasteCtrlVToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles PasteCtrlVToolStripMenuItem.Click
        PasteSign()
    End Sub

    Private Sub PasteSign()
        If Clipboard.ContainsText() Then
            AddUndo()
            Dim str = Clipboard.GetText
            Dim deserializedSign = DeSerializeJson(Of SwSign)(str)
            If deserializedSign IsNot Nothing Then
                Dim frame2 As SWFrame = deserializedSign.Frames.Skip(1).Take(1).FirstOrDefault()
                If frame2 IsNot Nothing Then

                    InsertFrameIntoCurrentFrame(frame2)


                    DisplaySign()
                End If
            End If
        End If
    End Sub

    Private Function DeSerializeJson(Of T)(ByVal json As String) As T
        Dim serializer = New JsonSerializer()
        serializer.Converters.Add(New JavaScriptDateTimeConverter())
        serializer.NullValueHandling = NullValueHandling.Ignore
        Dim obj As T = Nothing
        Try
            obj = JsonConvert.DeserializeObject(Of T)(json, New JavaScriptDateTimeConverter())
        Catch ex As Exception
            Throw (New Exception("Could not deserialize object." & ex.Message, ex))
        End Try

        Return obj
    End Function

    Private Sub OverlapSymbolsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles OverlapSymbolsToolStripMenuItem.Click
        Me.mySWSign.OverlapSymbols()
        DisplaySign()
    End Sub

    Private Sub ColorizeToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles ColorizeToolStripMenuItem.Click
        AddUndo()
        Colorize()
        DisplaySign()
    End Sub

    Private Sub Colorize()
        If CurrentFrame.SelectedSymbolCount > 0 Then
            'If some select symbols, do them
            For Each Symbol In CurrentFrame.SignSymbols
                If Symbol.IsSelected Then
                    Symbol.Handcolor = Symbol.StandardColor.ToArgb
                    Symbol.Palmcolor = Color.White.ToArgb
                End If
            Next
        Else
            For Each Symbol In CurrentFrame.SignSymbols
                'Else do all symbols
                Symbol.Handcolor = Symbol.StandardColor.ToArgb
                Symbol.Palmcolor = Color.White.ToArgb
            Next
        End If
    End Sub

    Private Sub ToBlack()
        If CurrentFrame.SelectedSymbolCount > 0 Then
            'If some select symbols, do them
            For Each Symbol In CurrentFrame.SignSymbols
                If Symbol.IsSelected Then
                    Symbol.Handcolor = Color.Black.ToArgb
                    Symbol.Palmcolor = Color.White.ToArgb
                End If
            Next
        Else
            For Each Symbol In CurrentFrame.SignSymbols
                'Else do all symbols
                Symbol.Handcolor = Color.Black.ToArgb
                Symbol.Palmcolor = Color.White.ToArgb
            Next
        End If
    End Sub

    Private Sub ToBlackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToBlackToolStripMenuItem.Click
        AddUndo()
        ToBlack()
        DisplaySign()
    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles SelectAllToolStripMenuItem.Click
        CurrentFrame.SelectAll()
        DisplaySign()
    End Sub

    Private Sub btnChooserReplace_Click(sender As Object, e As EventArgs) Handles btnChooserReplace.Click
        ReplaceSymbol()
    End Sub
    Private Sub ReplaceSymbol()
        If CurrentFrame.SelectedSymbolCount = 1 Then
            AddUndo()
            'Replace selected symbol
            ChangeChangeSymbolIn(Me.symbolOut, HandChooser.Hand)
        End If
    End Sub
    Private Sub btnChooserAdd_Click(sender As Object, e As EventArgs) Handles btnChooserAdd.Click
        AddUndo()
        'Add Symbol
        Dim NewSignSymbol As SWSignSymbol = Me.symbolOut.Clone
        InsertSymbolIntoSign(NewSignSymbol.Code)
        DisplaySign()
    End Sub

    Private Sub SeperateSymbolsToolStripMenuItem_Click(sender As Object, e As EventArgs) _
        Handles SeperateSymbolsToolStripMenuItem.Click
        CurrentFrame.SeperateSymbols()
        DisplaySign()
    End Sub

    Private Sub MirrorSignToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MirrorSignToolStripMenuItem.Click
        MirrorSign(mySWSign)

    End Sub

    Private Sub ChangeArrowHandsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeArrowHandsToolStripMenuItem.Click
        ChangeArrowHands(mySWSign)
    End Sub

    Private Sub ChangeArrowHands(ByVal initialsign As SwSign)
        Dim initialcurrentFrame = initialsign.Frames(initialsign.CurrentFrameIndex)

        For Each symbol As SWSignSymbol In initialcurrentFrame.SignSymbols
            ChangeArrowHands(symbol)
        Next
        DisplaySign()
    End Sub

    Public Sub MirrorSign(initialsign As SwSign)
        Dim initialcurrentFrame = initialsign.Frames(initialsign.CurrentFrameIndex)

        For Each symbol As SWSignSymbol In initialcurrentFrame.SignSymbols
            mirrorSymbol(symbol)
        Next
        DisplaySign()
    End Sub

    Private Sub mirrorSymbol(ByVal symbol As SWSignSymbol)
        Dim mirroredpositionx = 500 - symbol.X - symbol.SymbolDetails.Width
        Dim initialcode = symbol.Code

        If isSymbolHand(symbol) Then
            If symbol.Hand = 0 Then
                symbol.Hand = 1
            Else
                symbol.Hand = 0
            End If
        End If
 
        symbol.Code = getmirroredssymbolcode(symbol)

        'Revert if new code is invalid
        If Not symbol.SymbolDetails.IsValid Then
            symbol.Code = initialcode
        End If

        symbol.X = mirroredpositionx

    End Sub
    Private Sub ChangeArrowHands(ByVal symbol As SWSignSymbol)

        Dim initialcode = symbol.Code
       
        symbol.Code = changearrowhandfill(symbol)

        'Revert if new code is invalid
        If Not symbol.SymbolDetails.IsValid Then
            symbol.Code = initialcode
        End If



    End Sub


    Private Function getmirroredssymbolcode(ByVal symbol As SWSignSymbol) As Integer
        Dim validrotations = SWSymbol.Rotations(symbol.Code)

        Dim fill = symbol.SymbolDetails.Fill
        
        Dim mirroredrotation = mirrorRotation(symbol.SymbolDetails.Rotation, validrotations)
        Return MakeNewSymbol(symbol.Code, symbol.Code, fill, mirroredrotation)
    End Function

    Private Function changearrowhandfill(ByVal symbol As SWSignSymbol) As Integer

        Dim cat = symbol.SymbolDetails.Category
        Dim fill = symbol.SymbolDetails.Fill
        'Switch right and left hand arrows
        If cat = 2 Then
            If fill = 1 Then
                fill = 2
            ElseIf fill = 2 Then
                fill = 1
            End If
        End If

        Return MakeNewSymbol(symbol.Code, symbol.Code, fill, symbol.SymbolDetails.Rotation)
    End Function

    Private Function isSymbolHand(ByVal sign As SWSignSymbol) As Boolean
        Return sign.SymbolDetails.Category = 1
    End Function

    Private Function mirrorRotation(ByVal rotation As Integer, ByVal validrotations As Integer) As Integer

        Dim newrotation As Integer = rotation


        If validrotations = 16 OrElse validrotations = -16 OrElse validrotations = 4 Then
            If rotation = 1 Then
                newrotation = 9
            ElseIf rotation = 2 Then
                newrotation = 10
            ElseIf rotation = 3 Then
                newrotation = 11
            ElseIf rotation = 4 Then
                newrotation = 12
            ElseIf rotation = 5 Then
                newrotation = 13
            ElseIf rotation = 6 Then
                newrotation = 14
            ElseIf rotation = 7 Then
                newrotation = 15
            ElseIf rotation = 8 Then
                newrotation = 16
            ElseIf rotation = 9 Then
                newrotation = 1
            ElseIf rotation = 10 Then
                newrotation = 2
            ElseIf rotation = 11 Then
                newrotation = 3
            ElseIf rotation = 12 Then
                newrotation = 4
            ElseIf rotation = 13 Then
                newrotation = 5
            ElseIf rotation = 14 Then
                newrotation = 6
            ElseIf rotation = 15 Then
                newrotation = 7
            ElseIf rotation = 16 Then
                newrotation = 8
                End If
            Else
                If rotation = 2 Then
                    newrotation = 8
                ElseIf rotation = 8 Then
                    newrotation = 2
                ElseIf rotation = 3 Then
                    newrotation = 7
                ElseIf rotation = 7 Then
                    newrotation = 3
                ElseIf rotation = 4 Then
                    newrotation = 6
                ElseIf rotation = 6 Then
                    newrotation = 4
                End If

            End If
            Return newrotation
    End Function
#End Region

    Private Sub OnlyOnePunctuationError()
        MessageBox.Show(
            "You can only add a punctuation to an empty sign. A sign with a punctuation cannot have additional symbols.")
    End Sub

    Private Function isPunctuation(code As Integer) As Boolean
        Return code >= 62113 'First punctuation until last of all symbols
    End Function

    Private Function SignhasSymbols(Sign As SwSign) As Boolean
        Dim symbolcount As Integer

        For Each frame In Sign.Frames
            symbolcount += frame.SignSymbols.Count
        Next
        Return symbolcount > 0
    End Function

    Private Function SignContainsPunctuation(mySWSign As SwSign) As Boolean
        Dim symbolcount As Integer

        For Each frame In Sign.Frames
            For Each symbol In frame.SignSymbols
                If isPunctuation(symbol.Code) Then
                    symbolcount += 1
                End If
            Next

        Next
        Return symbolcount > 0
    End Function


    Private Sub FindSymbol_Click(sender As Object, e As EventArgs) Handles FindSymbol.Click
        If CurrentFrame.SelectedSymbolCount = 1 Then
            OnlyOneSymbolJustSelected()
        End If
    End Sub
End Class