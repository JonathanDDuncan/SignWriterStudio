Imports System.Windows.Forms
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.SymbolCache.SWSymbolCache
Imports SignWriterStudio.General
Public Class HandChooser
    Event ChangeSymbol As EventHandler(Of EventArgs)
    Event Escape As EventHandler(Of EventArgs)
    Event Accept As EventHandler(Of EventArgs)
    Event Find As EventHandler(Of EventArgs)
    Event ChangeSelectedSym As EventHandler(Of EventArgs)
    Event ChooserMouseDown As EventHandler(Of MouseEventArgs)
    Private _editorForm As Editor
    Private isLoading As Boolean

    Public Property EditorForm() As Editor
        Get
            Return _editorForm
        End Get
        Set(ByVal value As Editor)
            _editorForm = value
        End Set
    End Property
    'Dim UpdateSignSymbolSelected As Boolean '= False
    Public ReadOnly Property Hand() As Integer
        Get
            If RBRightHand.Checked Then
                Return 0
            Else
                Return 1
            End If
        End Get

    End Property

#Region "Choose"
    Public Sub Reset(ByVal code As Integer, Optional ByVal hand1 As Integer? = Nothing)
        isLoading = True
        SetHand(code, hand1)
        SetFill(code)
        SetRotation(code)
        isLoading = False
    End Sub
    Private Sub SetFill(ByVal code As Integer)
        Dim symbol As New SWSignSymbol With {.Code = code}
        'Dim Category = mySWsymbol.category
        Dim Fill As Integer = symbol.SymbolDetails.Fill
        Dim Rotation As Integer = symbol.SymbolDetails.Rotation
        If RBRightHand.Checked Then
            'Right Hand
            Select Case Fill
                Case 1, 4
                    HandR1.Checked = True
                Case 2, 5
                    If Rotation < 9 Then
                        HandR3.Checked = True
                    Else
                        'Twisted around
                        HandR4.Checked = True
                    End If
                Case 3, 6
                    HandR2.Checked = True
            End Select
        ElseIf RBLeftHand.Checked Then
            'Left Hand
            Select Case Fill
                Case 1, 4
                    HandR1.Checked = True
                Case 2, 5
                    If Rotation > 8 Then
                        HandR3.Checked = True
                    Else
                        'Twisted around
                        HandR4.Checked = True
                    End If
                Case 3, 6
                    HandR2.Checked = True
            End Select
        End If

    End Sub
    Private Sub SetRotation(ByVal code As Integer)
        Dim symbol As New SWSignSymbol With {.Code = code}
        If symbol.SymbolDetails.IsValid Then
            Dim Category = symbol.SymbolDetails.Category
            Dim Fill As Integer = symbol.SymbolDetails.Fill
            Dim Rotation As Integer = symbol.SymbolDetails.Rotation

            If Category = 1 Then
                If RBRightHand.Checked Then
                    'Right Hand
                    If 1 <= Fill AndAlso Fill <= 3 Then ' Vertical plane
                        If Not HandR4.Checked Then
                            Select Case Rotation
                                Case 1
                                    VP1.Checked = True
                                Case 2
                                    VP2.Checked = True
                                Case 3
                                    VP3.Checked = True
                                Case 4
                                    VP4.Checked = True
                                Case 5
                                    VP5.Checked = True
                                Case 6
                                    VP6.Checked = True
                                Case 7
                                    VP7.Checked = True
                                Case 8
                                    VP8.Checked = True
                            End Select
                        Else ' Twisted around
                            Select Case Rotation
                                Case 9
                                    VP1.Checked = True
                                Case 10
                                    VP8.Checked = True
                                Case 11
                                    VP7.Checked = True
                                Case 12
                                    VP6.Checked = True
                                Case 13
                                    VP5.Checked = True
                                Case 14
                                    VP4.Checked = True
                                Case 15
                                    VP3.Checked = True
                                Case 16
                                    VP2.Checked = True
                            End Select

                        End If
                    Else ' Horizontal plane
                        If Not HandR4.Checked Then
                            Select Case Rotation
                                Case 1
                                    HP1.Checked = True
                                Case 2
                                    HP2.Checked = True
                                Case 3
                                    HP3.Checked = True
                                Case 4
                                    HP4.Checked = True
                                Case 5
                                    HP5.Checked = True
                                Case 6
                                    HP6.Checked = True
                                Case 7
                                    HP7.Checked = True
                                Case 8
                                    HP8.Checked = True
                            End Select
                        Else
                            Select Case Rotation
                                Case 9
                                    HP1.Checked = True
                                Case 10
                                    HP8.Checked = True
                                Case 11
                                    HP7.Checked = True
                                Case 12
                                    HP6.Checked = True
                                Case 13
                                    HP5.Checked = True
                                Case 14
                                    HP4.Checked = True
                                Case 15
                                    HP3.Checked = True
                                Case 16
                                    HP2.Checked = True
                            End Select
                        End If
                    End If
                ElseIf RBLeftHand.Checked Then 'Left hand
                    If 1 <= Fill AndAlso Fill <= 3 Then ' Vertical plane
                        If Not HandR4.Checked Then
                            Select Case Rotation
                                Case 9
                                    VP1.Checked = True
                                Case 16
                                    VP2.Checked = True
                                Case 15
                                    VP3.Checked = True
                                Case 14
                                    VP4.Checked = True
                                Case 13
                                    VP5.Checked = True
                                Case 12
                                    VP6.Checked = True
                                Case 11
                                    VP7.Checked = True
                                Case 10
                                    VP8.Checked = True
                            End Select
                        Else 'Twisted around
                            Select Case Rotation
                                Case 1
                                    VP1.Checked = True
                                Case 8
                                    VP8.Checked = True
                                Case 7
                                    VP7.Checked = True
                                Case 6
                                    VP6.Checked = True
                                Case 5
                                    VP5.Checked = True
                                Case 4
                                    VP4.Checked = True
                                Case 3
                                    VP3.Checked = True
                                Case 2
                                    VP2.Checked = True
                            End Select
                        End If
                    Else ' Horizontal plane
                        If Not HandR4.Checked Then
                            Select Case Rotation
                                Case 9
                                    HP1.Checked = True
                                Case 16
                                    HP2.Checked = True
                                Case 15
                                    HP3.Checked = True
                                Case 14
                                    HP4.Checked = True
                                Case 13
                                    HP5.Checked = True
                                Case 12
                                    HP6.Checked = True
                                Case 11
                                    HP7.Checked = True
                                Case 10
                                    HP8.Checked = True
                            End Select
                        Else 'Twisted around
                            Select Case Rotation
                                Case 1
                                    HP1.Checked = True
                                Case 8
                                    HP8.Checked = True
                                Case 7
                                    HP7.Checked = True
                                Case 6
                                    HP6.Checked = True
                                Case 5
                                    HP5.Checked = True
                                Case 4
                                    HP4.Checked = True
                                Case 3
                                    HP3.Checked = True
                                Case 2
                                    HP2.Checked = True
                            End Select
                        End If
                    End If
                End If
            Else 'All other categories
                Select Case Rotation
                    Case 1
                        VP1.Checked = True
                    Case 2
                        VP2.Checked = True
                    Case 3
                        VP3.Checked = True
                    Case 4
                        VP4.Checked = True
                    Case 5
                        VP5.Checked = True
                    Case 6
                        VP6.Checked = True
                    Case 7
                        VP7.Checked = True
                    Case 8
                        VP8.Checked = True
                    Case 9
                        HP1.Checked = True
                    Case 16
                        HP2.Checked = True
                    Case 15
                        HP3.Checked = True
                    Case 14
                        HP4.Checked = True
                    Case 13
                        HP5.Checked = True
                    Case 12
                        HP6.Checked = True
                    Case 11
                        HP7.Checked = True
                    Case 10
                        HP8.Checked = True
                End Select

            End If
        End If
    End Sub

    'Private Sub SetRotationControls(ByVal code As Integer)
    '    SetFill(code)
    '    SetRotation(code)

    'End Sub
    Private Sub SetHand(ByVal code As Integer, ByVal hand1 As Integer?)
        If hand1.HasValue Then
            SetHand(hand1.Value)
        Else
            Dim Symbol As New SWSignSymbol With {.Code = code}
            SetHand(Symbol.Hand)
        End If
    End Sub

    Private Sub SetHand(ByVal value As Integer)
        If value = 0 Then
            RBRightHand.Checked = True
        Else
            RBLeftHand.Checked = True
        End If
    End Sub

    Friend Sub Choose_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim ControlActive As Control = EditorForm.ActiveControl
        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "handchooser.htm")
            Case Keys.NumPad0, Keys.D0
                NextHand()
            Case Keys.NumPad8, Keys.D8
                If VerticalSelected() Then
                    VP1.Select()
                    VP1.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP1.Select()
                    HP1.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If

            Case Keys.NumPad9, Keys.D9
                If VerticalSelected() Then
                    VP8.Select()
                    VP8.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP8.Select()
                    HP8.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad6, Keys.D6
                If VerticalSelected() Then
                    VP7.Select()
                    VP7.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP7.Select()
                    HP7.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad3, Keys.D3
                If VerticalSelected() Then
                    VP6.Select()
                    VP6.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP6.Select()
                    HP6.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad2, Keys.D2
                If VerticalSelected() Then
                    VP5.Select()
                    VP5.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP5.Select()
                    HP5.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad1, Keys.D1
                If VerticalSelected() Then
                    VP4.Select()
                    VP4.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP4.Select()
                    HP4.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad4, Keys.D4
                If VerticalSelected() Then
                    VP3.Select()
                    VP3.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP3.Select()
                    HP3.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad7, Keys.D7
                If VerticalSelected() Then
                    VP2.Select()
                    VP2.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                Else
                    HP2.Select()
                    HP2.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.NumPad5, Keys.D5
                If VP1.Checked Then
                    HP1.Select()
                    HP1.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP1.Checked Then
                    VP1.Select()
                    VP1.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP2.Checked Then
                    HP2.Select()
                    HP2.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP2.Checked Then
                    VP2.Select()
                    VP2.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP3.Checked Then
                    HP3.Select()
                    HP3.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP3.Checked Then
                    VP3.Select()
                    VP3.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP4.Checked Then
                    HP4.Select()
                    HP4.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP4.Checked Then
                    VP4.Select()
                    VP4.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP5.Checked Then
                    HP5.Select()
                    HP5.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP5.Checked Then
                    VP5.Select()
                    VP5.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP6.Checked Then
                    HP6.Select()
                    HP6.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP6.Checked Then
                    VP6.Select()
                    VP6.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP7.Checked Then
                    HP7.Select()
                    HP7.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP7.Checked Then
                    VP7.Select()
                    VP7.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf VP8.Checked Then
                    HP8.Select()
                    HP8.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf HP8.Checked Then
                    VP8.Select()
                    VP8.Checked = True
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If

            Case Keys.Divide
                HandR4.Select()
                HandR4.Checked = True
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Multiply
                HandR1.Select()
                HandR1.Checked = True
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Subtract
                HandR3.Select()
                HandR3.Checked = True
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Add
                HandR2.Select()
                HandR2.Checked = True
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.C
                If e.Alt Then
                    RaiseEvent ChangeSelectedSym(Me, New EventArgs)
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.F
                If e.Alt Then
                    RaiseEvent Find(Me, New EventArgs)
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Enter
                RaiseEvent Accept(Me, New EventArgs)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Escape
                RaiseEvent Escape(Me, New EventArgs)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Up, Keys.Down, Keys.Right, Keys.Left
                If ControlActive.ToString = "SWEditor.HandChooser" Then
                    ArrowKeys(sender, e)
                End If
        End Select
    End Sub
    Private Sub ArrowKeys(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim ControlActive As Control = CType(EditorForm.ActiveControl, Control)
        Select Case e.KeyCode
            Case Keys.Up
                If ControlActive IsNot Nothing Then
                    Select Case ControlActive.Name
                        Case HandR1.Name, HandR3.Name, HandR2.Name, HandR4.Name
                            RBRightHand.Focus()
                        Case VP1.Name, VP2.Name, VP3.Name, VP4.Name, VP5.Name, VP6.Name, VP7.Name, VP8.Name
                            HandR1.Focus()
                        Case HP1.Name, HP2.Name, HP3.Name, HP4.Name, HP5.Name, HP6.Name, HP7.Name, HP8.Name
                            VP1.Focus()
                    End Select
                End If
            Case Keys.Down
                If ControlActive IsNot Nothing Then
                    Select Case ControlActive.Name
                        Case HandR1.Name, HandR3.Name, HandR2.Name, HandR4.Name
                            VP1.Focus()
                        Case VP1.Name, VP2.Name, VP3.Name, VP4.Name, VP5.Name, VP6.Name, VP7.Name, VP8.Name
                            HP1.Focus()
                        Case HP1.Name, HP2.Name, HP3.Name, HP4.Name, HP5.Name, HP6.Name, HP7.Name, HP8.Name
                            RBRightHand.Focus()
                    End Select
                End If
            Case Keys.Right
                If ControlActive IsNot Nothing Then
                    Select Case ControlActive.Name
                        Case HandR1.Name
                            HandR3.Focus()
                        Case HandR3.Name
                            HandR2.Focus()
                        Case HandR2.Name
                            HandR4.Focus()
                        Case HandR4.Name
                            HandR1.Focus()
                        Case VP1.Name
                            VP8.Focus()
                        Case VP2.Name
                            VP1.Focus()
                        Case VP3.Name
                            VP2.Focus()
                        Case VP4.Name
                            VP3.Focus()
                        Case VP5.Name
                            VP4.Focus()
                        Case VP6.Name
                            VP5.Focus()
                        Case VP7.Name
                            VP6.Focus()
                        Case VP8.Name
                            VP7.Focus()
                        Case HP1.Name
                            HP8.Focus()
                        Case HP2.Name
                            HP1.Focus()
                        Case HP3.Name
                            HP2.Focus()
                        Case HP4.Name
                            HP3.Focus()
                        Case HP5.Name
                            HP4.Focus()
                        Case HP6.Name
                            HP5.Focus()
                        Case HP7.Name
                            HP6.Focus()
                        Case HP8.Name
                            HP7.Focus()

                    End Select
                End If
            Case Keys.Left
                If ControlActive IsNot Nothing Then
                    Select Case ControlActive.Name
                        Case HandR1.Name
                            HandR4.Focus()
                        Case HandR3.Name
                            HandR1.Focus()
                        Case HandR2.Name
                            HandR3.Focus()
                        Case HandR4.Name
                            HandR4.Focus()
                        Case VP1.Name
                            VP2.Focus()
                        Case VP2.Name
                            VP3.Focus()
                        Case VP3.Name
                            VP4.Focus()
                        Case VP4.Name
                            VP5.Focus()
                        Case VP5.Name
                            VP6.Focus()
                        Case VP6.Name
                            VP7.Focus()
                        Case VP7.Name
                            VP8.Focus()
                        Case VP8.Name
                            VP1.Focus()
                        Case HP1.Name
                            HP2.Focus()
                        Case HP2.Name
                            HP3.Focus()
                        Case HP3.Name
                            HP4.Focus()
                        Case HP4.Name
                            HP5.Focus()
                        Case HP5.Name
                            HP6.Focus()
                        Case HP6.Name
                            HP7.Focus()
                        Case HP7.Name
                            HP8.Focus()
                        Case HP8.Name
                            HP1.Focus()

                    End Select
                End If
        End Select
    End Sub
    Private Sub NextHand()
        If RBRightHand.Checked Then
            RBLeftHand.Checked = True
        Else
            RBRightHand.Checked = True
        End If
    End Sub
    'Dim ChangingSelected As Boolean '= False
    'Private Sub ChangeChangeSymbolIn(ByVal NewSymbol As SWSignSymbol)
    '    'Dim symbol As SWSignSymbol
    '    If Not ChangingSelected Then
    '        ChangingSelected = True

    '        NewSymbol.IsSelected = True

    '        If Not UpdateSignSymbolSelected Then
    '            UpdateSignSymbolSelected = True
    '            If Not isLoading Then RaiseEvent ChangeSymbol(Me, New EventArgs)


    '            UpdateSignSymbolSelected = False

    '        End If
    '        ChangingSelected = False
    '    End If
    'End Sub
    Private Function VerticalSelected() As Boolean
        If VP1.Checked Or VP2.Checked Or VP3.Checked Or VP4.Checked Or VP5.Checked Or VP6.Checked Or VP7.Checked Or VP8.Checked Then
            Return True
        Else
            Return False
        End If
    End Function
    Friend Function NewFill() As Integer
        If RBRightHand.Checked Then '"Right Hand" Then
            If VP1.Checked Or VP2.Checked Or VP3.Checked Or VP4.Checked Or VP5.Checked Or VP6.Checked Or VP7.Checked Or VP8.Checked Then
                If HandR1.Checked Then
                    Return 1
                ElseIf HandR3.Checked Then
                    Return 2
                ElseIf HandR2.Checked Then
                    Return 3
                ElseIf HandR4.Checked Then
                    Return 2
                Else
                    Return 0
                End If
            ElseIf HP1.Checked Or HP2.Checked Or HP3.Checked Or HP4.Checked Or HP5.Checked Or HP6.Checked Or HP7.Checked Or HP8.Checked Then

                If HandR1.Checked Then
                    Return 4
                ElseIf HandR3.Checked Then
                    Return 5
                ElseIf HandR2.Checked Then
                    Return 6
                ElseIf HandR4.Checked Then
                    Return 5
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        ElseIf RBLeftHand.Checked Then 'Left Hand" Then
            If VP1.Checked Or VP2.Checked Or VP3.Checked Or VP4.Checked Or VP5.Checked Or VP6.Checked Or VP7.Checked Or VP8.Checked Then
                If HandR1.Checked Then
                    Return 1
                ElseIf HandR3.Checked Then
                    Return 2
                ElseIf HandR2.Checked Then
                    Return 3
                ElseIf HandR4.Checked Then
                    Return 2
                Else
                    Return 0
                End If
            ElseIf HP1.Checked Or HP2.Checked Or HP3.Checked Or HP4.Checked Or HP5.Checked Or HP6.Checked Or HP7.Checked Or HP8.Checked Then

                If HandR1.Checked Then
                    Return 4
                ElseIf HandR3.Checked Then
                    Return 5
                ElseIf HandR2.Checked Then
                    Return 6
                ElseIf HandR4.Checked Then
                    Return 5
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        End If

    End Function
    Friend Function NewRotation(ByVal Category As Integer) As Integer
        Dim IntRotation As Integer = 0

        If Category = 1 Then
            If (RBRightHand.Checked And Not HandR4.Checked) Or (RBLeftHand.Checked And HandR4.Checked) Then
                If VP1.Checked Or HP1.Checked Then
                    IntRotation = 1
                ElseIf VP2.Checked Or HP2.Checked Then
                    IntRotation = 2
                ElseIf VP3.Checked Or HP3.Checked Then
                    IntRotation = 3
                ElseIf VP4.Checked Or HP4.Checked Then
                    IntRotation = 4
                ElseIf VP5.Checked Or HP5.Checked Then
                    IntRotation = 5
                ElseIf VP6.Checked Or HP6.Checked Then
                    IntRotation = 6
                ElseIf VP7.Checked Or HP7.Checked Then
                    IntRotation = 7
                ElseIf VP8.Checked Or HP8.Checked Then
                    IntRotation = 8
                Else
                    IntRotation = 0
                End If

            ElseIf (RBLeftHand.Checked And Not HandR4.Checked) Or (RBRightHand.Checked And HandR4.Checked) Then
                If VP1.Checked Or HP1.Checked Then
                    IntRotation = 9
                ElseIf VP2.Checked Or HP2.Checked Then
                    IntRotation = 16
                ElseIf VP3.Checked Or HP3.Checked Then
                    IntRotation = 15
                ElseIf VP4.Checked Or HP4.Checked Then
                    IntRotation = 14
                ElseIf VP5.Checked Or HP5.Checked Then
                    IntRotation = 13
                ElseIf VP6.Checked Or HP6.Checked Then
                    IntRotation = 12
                ElseIf VP7.Checked Or HP7.Checked Then
                    IntRotation = 11
                ElseIf VP8.Checked Or HP8.Checked Then
                    IntRotation = 10
                Else
                    IntRotation = 0

                End If
            End If
        Else

        End If
        Return IntRotation
    End Function
    Friend Sub CBHand_SelectedIndChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RBLeftHand.CheckedChanged, RBRightHand.CheckedChanged
        'If Not isLoading Then
        Try
            If RBRightHand.Checked Then 'CBHand.SelectedItem = "Right Hand" Then
                PBHandR1.Image = GetImagebyId("01-05-001-01-01-01")
                PBHandR2.Image = GetImagebyId("01-05-001-01-02-01")
                PBHandR3.Image = GetImagebyId("01-05-001-01-03-01")
                PBHandR4.Image = GetImagebyId("01-05-001-01-02-09")
            Else
                PBHandR1.Image = GetImagebyId("01-05-001-01-01-09")
                PBHandR2.Image = GetImagebyId("01-05-001-01-02-09")
                PBHandR3.Image = GetImagebyId("01-05-001-01-03-09")
                PBHandR4.Image = GetImagebyId("01-05-001-01-02-01")
            End If

            ChangeFacing()

            If Not isLoading Then RaiseEvent ChangeSymbol(Me, New EventArgs)

        Catch ex As Exception
            LogError(ex, "")

        End Try


    End Sub

    Private Sub ChangeFacing()
        'Change facing when hand changes if edge of hand
        If HandR3.Checked Then
            HandR4.Checked = True
        ElseIf HandR4.Checked Then
            HandR3.Checked = True
        End If
    End Sub

#End Region

    Private Sub Hand_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles HandR1.CheckedChanged, HandR2.CheckedChanged, HandR4.CheckedChanged, HandR3.CheckedChanged
        Dim RB As RadioButtonFull = CType(sender, RadioButtonFull)
        If Not isLoading AndAlso RB.Checked = True Then RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub HP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles HP1.CheckedChanged, HP2.CheckedChanged, HP3.CheckedChanged, HP4.CheckedChanged, HP5.CheckedChanged, HP6.CheckedChanged, HP7.CheckedChanged, HP8.CheckedChanged
        Dim RB As RadioButtonFull = CType(sender, RadioButtonFull)
        If Not isLoading AndAlso RB.Checked = True Then RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub VP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles VP1.CheckedChanged, VP2.CheckedChanged, VP3.CheckedChanged, VP4.CheckedChanged, VP5.CheckedChanged, VP6.CheckedChanged, VP7.CheckedChanged, VP8.CheckedChanged
        Dim RB As RadioButtonFull = CType(sender, RadioButtonFull)
        If Not isLoading AndAlso RB.Checked = True Then RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub HandChooser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Right
                e.Handled = False
                e.SuppressKeyPress = False

            Case Keys.Left
                e.Handled = False
                e.SuppressKeyPress = False

            Case Keys.Up
                e.Handled = False
                e.SuppressKeyPress = False

            Case Keys.Down
                e.Handled = False
                e.SuppressKeyPress = False

        End Select
    End Sub


    Private Sub HandChooser_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        isLoading = True
        RBRightHand.Checked = True
        HandR1.Checked = True
        VP1.Checked = True
        Try
            PBRightHand.Image = GetImagebyId("01-05-001-01-03-01")
            PBLeftHand.Image = GetImagebyId("01-05-001-01-03-09")
        Catch ex As Exception
        End Try

        isLoading = False
    End Sub

    Private Sub PBHandR4_Click(sender As Object, e As EventArgs) Handles PBHandR4.Click
        HandR4.Checked = True
    End Sub

    Private Sub PBHandR1_Click(sender As Object, e As EventArgs) Handles PBHandR1.Click
        HandR1.Checked = True
    End Sub

    Private Sub PBHandR2_Click(sender As Object, e As EventArgs) Handles PBHandR2.Click
        HandR3.Checked = True
    End Sub

    Private Sub PBHandR3_Click(sender As Object, e As EventArgs) Handles PBHandR3.Click
        HandR2.Checked = True
    End Sub

    Private Sub PBRightHand_Click(sender As Object, e As EventArgs) Handles PBRightHand.Click
        RBRightHand.Checked = True
    End Sub

    Private Sub PBLeftHand_Click(sender As Object, e As EventArgs) Handles PBLeftHand.Click
        RBLeftHand.Checked = True
    End Sub


    Private Sub Form_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        RaiseEvent ChooserMouseDown(sender, e)
    End Sub
 
End Class
