'Option Strict On
Imports System.Drawing

Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms

Imports SignWriterStudio.SWS
Imports CefSharp

Imports CefSharp.MinimalExample.WinForms
Imports Microsoft.VisualBasic

' FILE: E:/Mis Documentos/Jonathan/SignWritingDocs/Cs//cs

' In this section you can add your own using directives
' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000780 begin
' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000780 end
' *
'          *   A class that represents ...  All rights Reserved Copyright(c) 2008
'          *
'          *       @see OtherClasses
'          *       @author Jonathan Duncan
'          */
Imports CefSharp.WinForms
Imports SPML

''' <summary>
''' Class for editing SignWriting
''' </summary>
Partial Public Class Editor

#Region "Header"
    Dim FirstLoad As Boolean '= False
    Dim HandImageList As New ImageList()
    ReadOnly SymbolsList As New ImageList()
    '     *
    '          */
    Public mySWSign As New SwSign
    'Dim SWEditorProgressBar As New Progress
    '     *
    '          */
    Dim WithEvents symbolIn As New SWSignSymbol
    Dim WithEvents symbolOut As New SWSignSymbol
    '     *
    '          */

    ReadOnly EditorUndo As New General.Undo(Of SwSign)

    Dim SymbolStartOffset As Point
    Dim StartPoint As Point
    Dim isSelecting As Boolean '= False
    Dim theRectangle As New Rectangle(New Point(0, 0), New Size(0, 0))
    Dim EndPoint As Point
    Dim UpdateSignSymbolSelected As Boolean ' = False
    Dim DisplayingSign As Boolean '= False
    Dim CurrentFrame As SWFrame
    Dim JustLoaded As Boolean
    Dim isLoading As Boolean '= False


    '    Private WM_KEYDOWN As Integer = &H100
    Private _Area As AreaEnm = AreaEnm.AllGroups
    Enum AreaEnm
        AllGroups
        Favorites
        Search
        Choose
        Sequence
        Sign
    End Enum
#End Region
#Region "Editor"
    ''' <summary>
    ''' Sub Clear all symbols from sign
    ''' </summary>
    Public Sub ClearAll()
        If mySWSign IsNot Nothing Then
            mySWSign.Frames.Clear()

        End If
        EditorUndo.Clear()

        'Ensure Test final changes to object
#If AssertTest Then
		If Not Me.mySWSign.Frames.Count = 0 Then
			Throw new AssertionException ("Sign was not cleared of all frames.")
		End If
#End If
    End Sub

    Friend Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnAccept.Click
        Accept()
    End Sub
    Private Sub Accept()
        CurrentFrame.UnSelectSymbols()
        EditorUndo.Clear()
        DialogResult = DialogResult.OK
        Hide()
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles btnCancel.Click
        Cancel()
    End Sub
    Private Sub Cancel()
        EditorUndo.Clear()
        DialogResult = DialogResult.Cancel
        Hide()
    End Sub
    Public Property Sign() As SwSign
        Get
            Return mySWSign.Clone
        End Get
        Set(ByVal value As SwSign)
            If value IsNot Nothing Then
                mySWSign = value.Clone
                SignInit()
            Else
                mySWSign = Nothing
            End If
        End Set
    End Property
    Private Sub SignInit()
        mySWSign.CurrentFrameIndex = 0
        CurrentFrame = GetCurrentFrame(mySWSign)
        LoadSequence()
        DisplaySign()
    End Sub

    Private Sub Editor_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If JustLoaded Then
            Area = AreaEnm.Favorites
            JustLoaded = False
        End If
    End Sub

    Private Sub Editor_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        EditorUndo.Clear()
    End Sub


    Private Sub Initiate()

    End Sub
    Private Sub LoadFirstTime()

        KeyPreview = True
        'TODO check if needed to be set each time editor loads.
        PBSign.AllowDrop = True

        HandChooser.EditorForm = Me
        ArrowChooser.EditorForm = Me

        TvFavoriteLoad()

        AllGroupSymbols_Load()

        FilterRootShape.DataSource = ISWARootShapesQuickTableAdapter.GetData()

        FilterActionFinger.DataSource = ISWAActionFingersTableAdapter.GetData().DefaultView


        FilterMultipleFingers.DataSource = ISWAMultipleFingersTableAdapter.GetData().DefaultView

        FilterThumbPosition.DataSource = ISWAThumbPositionsTableAdapter.GetData().DefaultView

        Dim TA As New SymbolCache.ISWA2010DataSetTableAdapters.classificationTableAdapter
        Dim DT As DataTable
        DT = TA.GetClassificationView
        HandsClassifiedBindingSource.DataSource = DT

        BaseGroupSuggestion_Load()

        TCSymbols.SelectedTab = TPAllSymbols
        TCSymbols.SelectedTab = TPFavorites

        FirstLoad = True

    End Sub
    Dim AddingUndo As Boolean
    Private browser As ChromiumWebBrowser
    Private _fsw1 As String

    Public Property FSW As String
        Get
            Return _fsw1
        End Get
        Set(value As String)
            _fsw1 = value
            SetFsw(_fsw1)
        End Set
    End Property

    Private Sub SetFsw(ByVal fsw1 As String)
        mySWSign = SpmlConverter.FswtoSwSign(fsw1, mySWSign.LanguageIso, mySWSign.SignLanguageIso)
        DisplaySign()
    End Sub

    Private Sub AddUndo()

        If AddingUndo Then
            MessageBox.Show("Recurring function Call")
        End If
        AddingUndo = True
        EditorUndo.Add(mySWSign.Clone)
        AddingUndo = False
    End Sub

    Private Sub Undo()
        Dim Sign1 As SwSign = EditorUndo.Undo(mySWSign.Clone)
        If Sign1 IsNot Nothing Then
            mySWSign = Sign1
            CurrentFrame = GetCurrentFrame(mySWSign)
            DisplaySign()
        End If
    End Sub


    Private Sub Redo()
        Dim Sign1 As SwSign = EditorUndo.Redo(mySWSign.Clone)
        If Sign1 IsNot Nothing Then
            mySWSign = Sign1
            CurrentFrame = GetCurrentFrame(mySWSign)
            DisplaySign()
        End If
    End Sub

    Public Property Area() As AreaEnm
        Get
            Return _Area
        End Get
        Set(ByVal value As AreaEnm)
            If (Not _Area = value) Then
                _Area = value
                SetArea()
            End If
        End Set
    End Property
    Private Sub SetArea()

        Dim ControlColor As Color = SystemColors.Control
        'Reset area colors
        AreaFavoritesColor(ControlColor)
        AreaAllColor(ControlColor)
        AreaChooserColor(ControlColor)
        AreaSequenceColor(ControlColor)
        AreaSignColor(ControlColor)
        AreaSearchColor(ControlColor)

        Dim ActiveAreaColor As Color = Color.LightBlue
        Select Case Area
            Case AreaEnm.AllGroups
                ActiveControl = Nothing
                TCSymbols.SelectTab(TPAllSymbols)
                TVAllGroups.Select()
                AreaAllColor(ActiveAreaColor)
            Case AreaEnm.Favorites
                If ActiveControl Is Nothing OrElse (ActiveControl IsNot Nothing AndAlso Not ActiveControl.Name = TPFavorites.Name) Then
                    TCSymbols.SelectTab(TPFavorites)
                    TVFavoriteSymbols.Focus()
                    'TVFavoriteSymbols.SelectedNode = TVFavoriteSymbols.Nodes.Item(0)
                End If
                AreaFavoritesColor(ActiveAreaColor)

            Case AreaEnm.Search
                ActiveControl = Nothing
                TCSymbols.SelectTab(TPSearch)
                FilterRootShape.Select()
                AreaSearchColor(ActiveAreaColor)
                ActiveControl = FilterRootShape
            Case AreaEnm.Choose
                ActiveControl = Nothing
                TCSymbols.SelectTab(TPChooser)
                If TVChooser.Visible Then
                    TVChooser.Select()
                ElseIf HandChooser.GBFills.Visible Then
                    HandChooser.GBFills.Select()
                    HandChooser.GBFills.Focus()
                End If
                AreaChooserColor(ActiveAreaColor)

            Case AreaEnm.Sequence

                AreaSequenceColor(ActiveAreaColor)
                ActiveControl = Nothing

            Case AreaEnm.Sign
                ActiveControl = Nothing
                AreaSignColor(ActiveAreaColor)
                ActiveControl = PanelSign
        End Select
    End Sub
    Private Sub AreaSequenceColor(color As Color)
        TPSequence.BackColor = color
        TVSequence.BackColor = color
    End Sub
    Private Sub AreaSignColor(color As Color)
        GBSign.BackColor = color
        SCRightSide.BackColor = color
    End Sub
    Private Sub AreaChooserColor(color As Color)
        TVChooser.BackColor = color
        HandChooser.BackColor = color
        ArrowChooser.BackColor = color

    End Sub
    Private Sub AreaAllColor(color As Color)
        TVAllGroups.BackColor = color
        TPAllSymbols.BackColor = color
    End Sub
    Private Sub AreaFavoritesColor(color As Color)
        TVFavoriteSymbols.BackColor = color
        TPFavorites.BackColor = color
    End Sub
    Private Sub AreaSearchColor(color As Color)
        TVHand.BackColor = color
        TPSearch.BackColor = color
    End Sub
    Private Sub Me_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        'Change Active Keyboard area

        If e.Control Then
            Select Case e.KeyCode

                Case Keys.D1
                    SetArea(e, AreaEnm.Favorites)
                Case Keys.NumPad1
                    SetArea(e, AreaEnm.Favorites)
                Case Keys.D2
                    SetArea(e, AreaEnm.AllGroups)
                Case Keys.NumPad2
                    SetArea(e, AreaEnm.AllGroups)
                Case Keys.D3
                    SetArea(e, AreaEnm.Search)
                Case Keys.NumPad3
                    SetArea(e, AreaEnm.Search)
                Case Keys.D4
                    SetArea(e, AreaEnm.Choose)
                Case Keys.NumPad4
                    SetArea(e, AreaEnm.Choose)
                Case Keys.D5
                    SetArea(e, AreaEnm.Sign)
                Case Keys.NumPad5
                    SetArea(e, AreaEnm.Sign)
                Case Keys.D6
                    SetArea(e, AreaEnm.Sequence)
                Case Keys.NumPad6
                    SetArea(e, AreaEnm.Sequence)
            End Select
        Else
            Select Case e.KeyCode
                Case Keys.Enter
                    If e.Control Then
                        Accept()
                        e.SuppressKeyPress = True
                        e.Handled = True
                        Exit Sub
                    End If
                Case Keys.F4
                    If e.Alt Then
                        Cancel()
                        e.SuppressKeyPress = True
                        e.Handled = True
                        Exit Sub
                    End If
            End Select
        End If
        Select Case Area
            Case AreaEnm.AllGroups
                AllGroups_KeyDown(sender, e)
            Case AreaEnm.Favorites
                Favorites_KeyDown(sender, e)
            Case AreaEnm.Search
                Search_KeyDown(sender, e)
            Case AreaEnm.Sequence
                Sequence_KeyDown(sender, e)
            Case AreaEnm.Choose
                If HandChooser.Visible = True Then
                    HandChooser.Choose_KeyDown(sender, e)
                End If
                If ArrowChooser.Visible = True Then
                    ArrowChooser.Choose_KeyDown(sender, e)
                End If
                If TVChooser.Visible = True Then
                    TVChooser_Choose_KeyDown(sender, e)
                End If
            Case AreaEnm.Sign
                Sign_KeyDown(sender, e)
        End Select
    End Sub

    Private Sub SetArea(ByVal e As KeyEventArgs, ByVal a As AreaEnm)

        Area = a
        e.SuppressKeyPress = True
        e.Handled = True
    End Sub

    Private Shared Sub SelectNode(ByVal TV As TreeView, ByVal Index As Integer)
        If Index - 1 <= TV.Nodes.Count - 1 Then
            TV.CollapseAll()
            TV.SelectedNode = TV.Nodes.Item(Index - 1)
            TV.SelectedNode.Expand()
            If TV.SelectedNode.Nodes.Count >= 1 Then
                TV.SelectedNode = TV.SelectedNode.Nodes(0)
            End If
        End If
    End Sub

    Private Shared Function MakeNewSymbol(ByVal selectedCode As Integer, ByVal newCode As Integer, ByVal newFill As Integer, ByVal newRotation As Integer) As Integer
        Dim ChangeSymbolIn As New SWSymbol With {.Code = selectedCode}
        Dim newSymbol As New SWSymbol With {.Code = newCode}

        If ChangeSymbolIn.Category = 1 Or ChangeSymbolIn.Category = 2 Then
            If ChangeSymbolIn.IsValid And newFill >= 1 And newRotation >= 1 Then
                newSymbol.Id = ChangeSymbolIn.Id
                newSymbol.Fill = newFill
                newSymbol.Rotation = newRotation
                newSymbol.MakeId()
                Return newSymbol.Code
            End If
        Else
            If ChangeSymbolIn.IsValid Then
                Return ChangeSymbolIn.Code
            End If

        End If
    End Function
    Private Shared Function MakeNewSymbol(ByVal selectedCode As Integer, ByVal newCode As Integer, ByVal newFill As Integer, ByVal newRotation As Integer, ByVal plane As Integer) As Integer
        Dim ChangeSymbolIn As New SWSymbol With {.Code = selectedCode}
        Dim newSymbol As New SWSymbol With {.Code = newCode}

        If ChangeSymbolIn.Category = 1 Or ChangeSymbolIn.Category = 2 Then
            If ChangeSymbolIn.IsValid And newFill >= 1 And newRotation >= 1 Then
                newSymbol.Id = ChangeSymbolIn.Id

                If plane = 1 AndAlso newSymbol.Category = 2 AndAlso newSymbol.Group = 3 Then 'Floor Plane
                    newSymbol.Group = 5
                ElseIf plane = 0 AndAlso newSymbol.Category = 2 AndAlso newSymbol.Group = 5 Then 'Wall Plane
                    newSymbol.Group = 3
                End If
                newSymbol.Fill = newFill
                newSymbol.Rotation = newRotation
                newSymbol.MakeId()
                Return newSymbol.Code
            End If
        ElseIf ChangeSymbolIn.IsValid Then
            Return ChangeSymbolIn.Code
        End If
    End Function
    Private Sub symbolOutChanged()
        PBsymbolOut.Image = symbolOut.SymbolDetails.SymImage
        Dim id As String = symbolOut.SymbolDetails.Id
        PBsymbolOut.Tag = id
        TextBox1.Text = id
        PBsymbolOut.Refresh()
    End Sub
    Private Sub symbolOut_SymbolChanged(ByVal sender As Object, ByVal e As EventArgs) Handles symbolOut.SymbolChanged
        symbolOutChanged()
    End Sub
#End Region


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Initiate()
    End Sub



    'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
    '    Dim DeglossChoosers1 = New DeglossChoosers
    '    Dim DeglossedList As List(Of DeglossResult) = DeglossChoosers1.GetDeglossed(TVAllGroups)

    '    Dim sb As New System.Text.StringBuilder

    '    For Each item In DeglossedList
    '        sb.Append("UPDATE basesymbol SET ")
    '        sb.Append("UseArrowChooser " & "=" & TrueFalse(item.UseArrowChooser))
    '        sb.Append(", ShowWallPlane " & "=" & TrueFalse(item.ShowWallPlane))
    '        sb.Append(", ShowWallPlaneImage " & "=" & TrueFalse(item.ShowWallPlaneImage))
    '        sb.Append(", ShowFloorPlane " & "=" & TrueFalse(item.ShowFloorPlane))
    '        sb.Append(", ShowFloorPlaneImage " & "=" & TrueFalse(item.ShowFloorPlaneImage))
    '        sb.Append(", ShowFlip " & "=" & TrueFalse(item.ShowFlip))
    '        sb.Append(", ShowVP3VP7 " & "=" & TrueFalse(item.ShowVP3VP7))
    '        sb.AppendLine("  WHERE  bs_sym_code = " & item.Code & ";")
    '    Next

    '    Using outfile As New IO.StreamWriter("ShowArrowsUpdate.txt")
    '        outfile.Write(sb.ToString())
    '    End Using
    'End Sub

    'Private Function TrueFalse(bool As Boolean) As String
    '    If bool Then
    '        Return "1"
    '    Else
    '        Return "0"
    '    End If
    'End Function

    Private Sub CMSPBSign_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles CMSPBSign.Opening

    End Sub

    Private Sub btnHelp_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnHelp.MouseClick
        Help.ShowHelp(Me, "SignWriterStudio.chm", "signeditor.htm")
    End Sub


    Private Sub TPFavorites_MouseEnter(sender As System.Object, e As EventArgs) Handles TPFavorites.MouseEnter
        If Not Area = AreaEnm.Favorites Then
            Area = AreaEnm.Favorites
        End If
    End Sub

    Private Sub TPAllSymbols_MouseEnter(sender As System.Object, e As EventArgs) Handles TPAllSymbols.MouseEnter
        If Not Area = AreaEnm.AllGroups Then
            Area = AreaEnm.AllGroups
        End If
    End Sub


    Private Sub GBSign_MouseHover(sender As System.Object, e As EventArgs) Handles GBSign.MouseHover
        If Not Area = AreaEnm.Sign Then
            Area = AreaEnm.Sign
        End If
    End Sub



    Private Sub TVHand_MouseEnter(sender As System.Object, e As EventArgs) Handles TVHand.MouseEnter
        If Not Area = AreaEnm.Search Then
            Area = AreaEnm.Search
        End If
    End Sub


    Private Sub PBSign_MouseHover(sender As Object, e As EventArgs) Handles PBSign.MouseHover
        If Not Area = AreaEnm.Sign Then
            Area = AreaEnm.Sign
        End If
    End Sub

    Private Sub Editor_Load(sender As Object, e As EventArgs) Handles Me.Load
        JustLoaded = True
        isLoading = True
        If (Not DesignMode) Then
            If Not FirstLoad Then
                LoadFirstTime()
            End If

            HandChooser.Visible = False
            ArrowChooser.Visible = False

            ResetHandFilter()

            Area = AreaEnm.Favorites

            If mySWSign IsNot Nothing Then
                CurrentFrame = GetCurrentFrame(mySWSign)
            Else
                mySWSign = New SwSign
                CurrentFrame = GetCurrentFrame(mySWSign)
            End If
            DisplaySign()
            PBSign.Invalidate()
            ResetHandFilter()
        End If
        isLoading = False

    End Sub
    Private Shared Sub TVExpand(ByVal TV As TreeView)
        If TV.SelectedNode IsNot Nothing Then
            TV.SelectedNode.Expand()
            If TV.SelectedNode.Nodes.Count >= 1 Then
                TV.SelectedNode = TV.SelectedNode.Nodes(0)
            End If
        End If
    End Sub

    Private Sub TVSequence_MouseEnter(sender As System.Object, e As EventArgs) Handles TVSequence.MouseEnter
        Area = AreaEnm.Sequence
    End Sub

    Private Sub CopyImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyImageToolStripMenuItem.Click
        mySWSign.SetClipboardImage()
    End Sub

    Private Sub CBFavorites_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBFavorites.SelectedIndexChanged

    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        AddSelectedFavorite()
    End Sub

    Private Sub ALotLargerToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ALotLargerToolStripMenuItem.Click
        SymbolAlotLarger()
    End Sub


    Private Sub ALotSmallerCtrlToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ALotSmallerCtrlToolStripMenuItem.Click
        SymbolAlotSmaller()
    End Sub

    Private Sub grid_CheckedChanged(sender As Object, e As EventArgs) Handles grid.CheckedChanged
        DisplaySign()
    End Sub

    Private Sub btnSugg1_Click(sender As Object, e As EventArgs) Handles btnSugg1.Click

        Dim sequences = SignSpelling.OrderSuggestion1(mySWSign)

        If (sequences IsNot Nothing) Then
            CurrentFrame.Sequences.Clear()
            CurrentFrame.AddSequences(sequences)
            LoadSequence()
        End If
    End Sub

    Public Function OrderSuggestion1(ByVal swSign As SwSign, ByVal canAsk As Boolean) As IEnumerable(Of SWSequence)
        Return SignSpelling.OrderSuggestion1(swSign, canAsk)
    End Function

    Private Sub btnSugg2_Click(sender As Object, e As EventArgs) Handles btnSugg2.Click
        Dim sequences = SignSpelling.OrderSuggestion2(CurrentFrame.SignSymbols)
        CurrentFrame.Sequences.Clear()
        CurrentFrame.AddSequences(sequences)
        LoadSequence()
    End Sub

    Private Sub QuickSignEditorBtn_Click(sender As Object, e As EventArgs) Handles QuickSignEditorBtn.Click
        Try
            Dim conv As New SpmlConverter

            FSW = conv.GetFsw(Sign())
            Dim browserForm = Program.GetBrowserForm()

            browser = browserForm.browser

            AddHandler browser.FrameLoadEnd, AddressOf FrameLoadEnd

            browser.RegisterJsObject("callbackObj", New CallbackObjectForJs(Me))

            browserForm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message & ex.StackTrace)
        End Try

    End Sub

    Private Function FrameLoadEnd(sender As IWebBrowser, args As FrameLoadEndEventArgs) As EventHandler(Of FrameLoadEndEventArgs)
        'Wait for the MainFrame to finish loading
        If (args.Frame.IsMain) Then

            Dim script As String = "window.initialFSW = '" & FSW & "';" & vbCrLf &
            "var sign = sw10.symbolsList(window.initialFSW);" & vbCrLf &
            "app.ports.receiveSign.send(sign);"

            args.Frame.ExecuteJavaScriptAsync(script)
        End If
        Return Nothing
    End Function
    Public Class CallbackObjectForJs
        Private ReadOnly myEditor As Editor

        Public Sub New(editor As Editor)

            myEditor = editor
        End Sub
        Public Sub setFsw(fsw As String)
            myEditor.FSW = fsw


        End Sub
        Public Sub showMessage(msg As String)
            'Read Note
            MessageBox.Show(msg)
        End Sub
    End Class

End Class

Public Class DeglossChoosers
    Public Function GetDeglossed(tv As TreeView) As List(Of DeglossResult)

        Dim symbolList As List(Of String) = GetSymbolList(tv)
        Dim deglossedList As List(Of DeglossResult) = Degloss(symbolList)

        Return deglossedList
    End Function

    Private Function Degloss(SymbolList As List(Of String)) As List(Of DeglossResult)
        Dim Symbol As SWSymbol
        Dim DeglossedList As New List(Of DeglossResult)
        For Each ID In SymbolList
            Symbol = New SWSymbol
            Symbol.Id = ID
            If Symbol.IsValid AndAlso Symbol.Category = 2 Then
                Dim Res As New DeglossResult
                Res.Code = Symbol.Code
                Res.BS_Code = Symbol.BaseGroup
                Res.ID = Symbol.Id
                'Res.ID = Symbol.BaseGroup
                Res.UseArrowChooser = UseArrowChooser(Symbol)
                SetPlanes(Symbol.Code)
                Res.ShowWallPlane = ShowWP
                Res.ShowWallPlaneImage = ShowWPI
                Res.ShowFloorPlane = ShowFP
                Res.ShowFloorPlaneImage = ShowFPI
                Res.ShowFlip = ShowFlip
                Res.ShowVP3VP7 = ShowVP3VP7

                DeglossedList.Add(Res)

            End If
        Next
        Return DeglossedList
    End Function

    Private Function GetSymbolList(TV As TreeView) As List(Of String)
        Dim SymbolList As New List(Of String)

        GetNodeSymbolList(TV.Nodes, SymbolList)
        Return SymbolList
    End Function
    Private Sub GetNodeSymbolList(Nodes As TreeNodeCollection, SymbolList As List(Of String))

        For Each Node As TreeNode In Nodes

            SymbolList.Add(Node.Name)


            If Node.Nodes.Count > 0 Then
                GetNodeSymbolList(Node.Nodes, SymbolList)
            End If

        Next

    End Sub

    Private Shared Function UseArrowChooser(ByVal SSS As SWSymbol) As Boolean
        Dim ValidFills As Integer = SSS.Fills
        Dim ValidRotations As Integer = SSS.Rotations
        Dim Category As Integer = SSS.Category

        If Category = 2 AndAlso (ValidFills = 3 Or ValidFills = 4 Or ValidFills = 6) AndAlso (ValidRotations = 8 Or ValidRotations = -8 Or ValidRotations = 16 Or ValidRotations = -16) Then

            'Exceptions
            If Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 3) AndAlso
                    Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 6) AndAlso
                    Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 9) AndAlso
                    Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 10) AndAlso
                    Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 11) Then



                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function
    Property ShowWP As Boolean
    Property ShowWPI As Boolean
    Property ShowFP As Boolean
    Property ShowFPI As Boolean
    Property ShowFlip As Boolean
    Property ShowVP3VP7 As Boolean

    Private Sub ShowWallPlane(WP As Boolean, WPI As Boolean)
        ShowWP = WP
        ShowWPI = WPI
    End Sub
    Private Sub ShowFloorPlane(FP As Boolean, FPI As Boolean)
        ShowFP = FP
        ShowFPI = FPI
    End Sub

    Private Sub SetPlanes(ByVal code As Integer)

        Dim Symbol As New SWSymbol With {.Code = code}
        Dim ValidRotations As Integer = Symbol.Rotations '.Fills 'SymbolCache.Iswa2010.SC.GetFills(Symbol.Code)
        Dim Category As Integer = Symbol.Category
        Dim Group As Integer = Symbol.Group
        If Category = 2 Then
            Select Case Group
                Case 1, 2, 4, 7, 8, 10
                    'Selectors no image
                    'Symbols, Hit Wall Ceiling or Floor, selector no images
                    ShowWallPlane(True, False)
                    ShowFloorPlane(False, False)
                Case 3, 5
                    If Group = 3 AndAlso ((Symbol.Symbol = 7 AndAlso (Symbol.Variation = 2 OrElse Symbol.Variation = 3)) OrElse (Symbol.Symbol = 8 AndAlso Symbol.Variation = 4)) Then
                        'Wall Plane only
                        ShowWallPlane(True, True)
                        ShowFloorPlane(False, False)
                    Else
                        'Wall Plane and Floor Plane with images
                        ShowWallPlane(True, True)
                        ShowFloorPlane(True, True)
                    End If
                Case 6
                    'Wall Plane selector with image
                    ShowWallPlane(True, False)
                    ShowFloorPlane(False, False)
                Case 9
                    'Floor Plane selector with image
                    ShowWallPlane(False, False)
                    ShowFloorPlane(True, False)
            End Select
            If ValidRotations = 16 OrElse ValidRotations = -16 OrElse ValidRotations = 4 Then
                ShowFlip = True
            Else
                ShowFlip = False
            End If

            Select Case ValidRotations
                Case -8, -16
                    ShowVP3VP7 = False
                Case Else
                    ShowVP3VP7 = True


            End Select
        Else
            'Don't show selectors
            ShowWallPlane(False, False)
            ShowFloorPlane(False, False)
        End If
    End Sub

End Class

Public Class DeglossResult
    Property Code As Integer
    Property BS_Code As Integer
    Property ID As String
    Property UseArrowChooser As Boolean
    Property ShowWallPlane As Boolean
    Property ShowWallPlaneImage As Boolean
    Property ShowFloorPlane As Boolean
    Property ShowFloorPlaneImage As Boolean
    Property ShowFlip As Boolean
    Property ShowVP3VP7 As Boolean
End Class
