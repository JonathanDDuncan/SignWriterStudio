Option Strict On
Imports System.Windows.Forms
Imports SignWriterStudio.SymbolCache.SWSymbolCache
Imports SignWriterStudio.SWS

Public Class ArrowChooser
    Event ChangeSymbol As EventHandler(Of EventArgs)
    Event Escape As EventHandler(Of EventArgs)
    Event Accept As EventHandler(Of EventArgs)
    Event Find As EventHandler(Of EventArgs)
    Event ChooserMouseDown As EventHandler(Of MouseEventArgs)

    Event ChangeSelectedSym As EventHandler(Of EventArgs)
    Private Property TypeItems() As Integer
    Private _editorForm As Editor
    Public Property EditorForm() As Editor
        Get
            Return _editorForm
        End Get
        Set(ByVal value As Editor)
            _editorForm = value
        End Set
    End Property
    '    Dim UpdateSignSymbolSelected As Boolean = False

#Region "Choose"
    Private Sub SetFill(ByVal fill As Integer)
        SetType(fill)
    End Sub
    Private Sub SetType(ByVal fill As Integer)
        Select Case fill
            Case 1
                RB1.Checked = True
            Case 2
                RB2.Checked = True
            Case 3
                RB3.Checked = True
            Case 4
                RB4.Checked = True
            Case 5
                RB5.Checked = True
            Case 6
                RB6.Checked = True
        End Select
    End Sub

    Private Function TypeValue() As Integer
        Dim value = 0
        If RB1.Checked = True Then
            value = 1
        ElseIf RB2.Checked = True Then
            value = 2
        ElseIf RB3.Checked = True Then
            value = 3
        ElseIf RB4.Checked = True Then
            value = 4
        ElseIf RB5.Checked = True Then
            value = 5
        ElseIf RB6.Checked = True Then
            value = 6
        End If
        Return value - 1

    End Function

    Private Sub SetRotation(ByVal code As Integer)
        Dim Symbol As New SWSymbol With {.Code = code}
        Dim Category As Integer = Symbol.Category
        Dim Rotation As Integer = Symbol.Rotation
        Dim Group As Integer = Symbol.Group

        If Category = 2 Then 'Arrows only
            'Set Flip
            CBFlip.Checked = Rotation > 8
            'Choose direction if floor plane
            If Group = 5 Or Group = 9 Then
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
            Else 'Choose direction if Wall plane

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

            End If
        End If
    End Sub
    Public Sub Reset(ByVal code As Integer)
        Dim Symbol As New SWSymbol With {.Code = code}
        If Symbol.IsValid Then
            SetFills(Symbol.Id, Symbol.Fills)
            SetPlanes(code)
            SetFill(Symbol.Fill)
            SetRotation(code)
        End If
    End Sub
    Private Sub SetPlanes(ByVal code As Integer)
        Dim Symbol As New SWSymbol With {.Code = code}
        Dim DT = SymbolCache.SWSymbolCache.GetArrowChoosingInfo(Symbol.BaseGroup)
        If DT.Rows.Count > 0 Then
            ShowWallPlane(DT.Rows(0).Field(Of Boolean)("ShowWallPlane"), DT.Rows(0).Field(Of Boolean)("ShowWallPlaneImage"))
            ShowFloorPlane(DT.Rows(0).Field(Of Boolean)("ShowFloorPlane"), DT.Rows(0).Field(Of Boolean)("ShowFloorPlaneImage"))
            CBFlip.Visible = DT.Rows(0).Field(Of Boolean)("ShowFlip")
            ShowVP3VP7(DT.Rows(0).Field(Of Boolean)("ShowVP3VP7"))
        End If
    End Sub
    Private Sub ShowVP3VP7(bool As Boolean)
        VP3.Visible = bool
        VP7.Visible = bool
    End Sub
    Private Sub ShowWallPlane(ByVal Show As Boolean, ByVal Image As Boolean)
        PBVertHand.Visible = Image
        VP1.Visible = Show
        VP2.Visible = Show
        VP3.Visible = Show
        VP4.Visible = Show
        VP5.Visible = Show
        VP6.Visible = Show
        VP7.Visible = Show
        VP8.Visible = Show
    End Sub
    Private Sub ShowFloorPlane(ByVal Show As Boolean, ByVal Image As Boolean)
        PBHorizHand.Visible = Image
        HP1.Visible = Show
        HP2.Visible = Show
        HP3.Visible = Show
        HP4.Visible = Show
        HP5.Visible = Show
        HP6.Visible = Show
        HP7.Visible = Show
        HP8.Visible = Show
    End Sub
    Private Sub SetFills(ByVal id As String, ByVal fills As Integer)
        TypeItems = fills
        If TypeItems = 3 Then
            RB1.Text = "Right"
            RB2.Text = "Left"
            RB3.Text = "Super- posed"
            RB4.Text = ""
            RB5.Text = ""
            RB6.Text = ""
            RB1.Visible = True
            RB2.Visible = True
            RB3.Visible = True
            RB4.Visible = False
            RB5.Visible = False
            RB6.Visible = False
            PB1.Visible = True
            PB2.Visible = True
            PB3.Visible = True
            PB4.Visible = False
            PB5.Visible = False
            PB6.Visible = False
            PB1.Image = GetImagebyId(GetFillId(id, 1))
            PB2.Image = GetImagebyId(GetFillId(id, 2))
            PB3.Image = GetImagebyId(GetFillId(id, 3))
            PB4.Image = Nothing
            PB5.Image = Nothing
            PB6.Image = Nothing
            RB1.Checked = True
        ElseIf TypeItems = 4 Then
            RB1.Text = "Right"
            RB2.Text = "Left"
            RB3.Text = "Super- posed"
            RB4.Text = "No Arrowhead"
            RB5.Text = ""
            RB6.Text = ""

            RB1.Visible = True
            RB2.Visible = True
            RB3.Visible = True
            RB4.Visible = True
            RB5.Visible = False
            RB6.Visible = False
            PB1.Visible = True
            PB2.Visible = True
            PB3.Visible = True
            PB4.Visible = True
            PB5.Visible = False
            PB6.Visible = False
            PB1.Image = GetImagebyId(GetFillId(id, 1))
            PB2.Image = GetImagebyId(GetFillId(id, 2))
            PB3.Image = GetImagebyId(GetFillId(id, 3))
            PB4.Image = GetImagebyId(GetFillId(id, 4))
            PB5.Image = Nothing
            PB6.Image = Nothing
            RB1.Checked = True

        ElseIf TypeItems = 6 Then
           RB1.Text = "Right"
            RB2.Text = "Left"
            RB3.Text = "Super- posed"
            RB4.Text = "Right Flipped"
            RB5.Text = "Left Flipped"
            RB6.Text = "Super- posed Flipped"
            RB1.Visible = True
            RB2.Visible = True
            RB3.Visible = True
            RB4.Visible = True
            RB5.Visible = True
            RB6.Visible = True
            PB1.Visible = True
            PB2.Visible = True
            PB3.Visible = True
            PB4.Visible = True
            PB5.Visible = True
            PB6.Visible = True
            PB1.Image = GetImagebyId(GetFillId(id, 1))
            PB2.Image = GetImagebyId(GetFillId(id, 2))
            PB3.Image = GetImagebyId(GetFillId(id, 3))
            PB4.Image = GetImagebyId(GetFillId(id, 4))
            PB5.Image = GetImagebyId(GetFillId(id, 5))
            PB6.Image = GetImagebyId(GetFillId(id, 6))
            RB1.Checked = True

        End If
    End Sub




    Private Function GetFillId(ByVal id As String, ByVal fill As Integer) As String
        Dim prefix = id.Substring(0, 12)
        Dim middle = "-0" & fill
        Dim suffix = "-01"
        Return prefix & middle & suffix
    End Function

    Friend Sub Choose_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "arrowchooser.htm")
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
            Case Keys.Subtract
                CBFlip.Checked = Not CBFlip.Checked
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
            Case Keys.OemPeriod, Keys.Decimal
                CBFlip.Checked = Not CBFlip.Checked
        End Select
    End Sub

    Private Sub NextHand()
        Dim NextIndex As Integer = TypeValue() + 1
        If NextIndex > TypeItems - 1 Then
            NextIndex = 0
        End If
        SetType(NextIndex)
    End Sub

    Private Function VerticalSelected() As Boolean
        If VP1.Checked Or VP2.Checked Or VP3.Checked Or VP4.Checked Or VP5.Checked Or VP6.Checked Or VP7.Checked Or VP8.Checked Then
            Return True
        Else
            Return False
        End If
    End Function
    Friend Function CheckPlane() As Integer
        If Not VerticalSelected() Then
            Return 1 'Floor Plane
        Else
            Return 0 'Wall Plane
        End If
    End Function
    Friend Function NewFill() As Integer
        Return TypeValue() + 1
    End Function
    Friend Function NewRotation(ByVal code As Integer) As Integer
        Dim IntRotation As Integer ' = 0
        Dim Symbol As New SWSymbol With {.Code = code}

        If Symbol.Category = 2 Then
            If CBFlip.Checked = False Then
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

            Else
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

        End If
        Return IntRotation
    End Function

#End Region


    Private Sub HP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles HP1.CheckedChanged, HP2.CheckedChanged, HP3.CheckedChanged, HP4.CheckedChanged, HP5.CheckedChanged, HP6.CheckedChanged, HP7.CheckedChanged, HP8.CheckedChanged
        RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub VP_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles VP1.CheckedChanged, VP2.CheckedChanged, VP3.CheckedChanged, VP4.CheckedChanged, VP5.CheckedChanged, VP6.CheckedChanged, VP7.CheckedChanged, VP8.CheckedChanged
        RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub CBFlip_CheckedChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles CBFlip.CheckedChanged
        RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub TypeChanged(ByVal sender As System.Object, ByVal e As EventArgs) Handles RB1.CheckedChanged, RB2.CheckedChanged, RB3.CheckedChanged, RB4.CheckedChanged, RB5.CheckedChanged, RB6.CheckedChanged
        RaiseEvent ChangeSymbol(Me, New EventArgs)
    End Sub

    Private Sub PB1_Click(sender As Object, e As EventArgs) Handles PB1.Click
        RB1.Checked = True
    End Sub

    Private Sub PB2_Click(sender As Object, e As EventArgs) Handles PB2.Click
        RB2.Checked = True
    End Sub

    Private Sub PB3_Click(sender As Object, e As EventArgs) Handles PB3.Click
        RB3.Checked = True
    End Sub

    Private Sub PB4_Click(sender As Object, e As EventArgs) Handles PB4.Click
        RB4.Checked = True
    End Sub

    Private Sub PB5_Click(sender As Object, e As EventArgs) Handles PB5.Click
        RB5.Checked = True
    End Sub

    Private Sub PB6_Click(sender As Object, e As EventArgs) Handles PB6.Click
        RB6.Checked = True
    End Sub

    Private Sub Form_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        RaiseEvent ChooserMouseDown(sender, e)
    End Sub

End Class
