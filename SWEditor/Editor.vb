'Option Strict On
Imports System.Drawing

Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms

Imports SignWriterStudio.SWS
Imports CefSharp

Imports CefSharp.MinimalExample.WinForms
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

''' <summary>
''' Class for editing SignWriting
''' </summary>
Partial Public Class Editor

#Region "Header"
    Dim FirstLoad As Boolean '= False
    Dim HandImageList As New ImageList()
    Dim SymbolsList As New ImageList()
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

    Dim EditorUndo As New SignWriterStudio.General.Undo(Of SwSign)

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
        If Me.mySWSign IsNot Nothing Then
            Me.mySWSign.Frames.Clear()

        End If
        Me.EditorUndo.Clear()

        'Ensure Test final changes to object
#If AssertTest Then
		If Not Me.mySWSign.Frames.Count = 0 Then
			Throw new AssertionException ("Sign was not cleared of all frames.")
		End If
#End If
    End Sub

    Friend Sub btnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAccept.Click
        Accept()
    End Sub
    Private Sub Accept()
        CurrentFrame.UnSelectSymbols()
        EditorUndo.Clear()
        DialogResult = DialogResult.OK
        Hide()
    End Sub


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
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
        Me.mySWSign.CurrentFrameIndex = 0
        CurrentFrame = GetCurrentFrame(Me.mySWSign)
        LoadSequence()
        Me.DisplaySign()
    End Sub

    Private Sub Editor_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
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





        'Dim designMode = (LicenseManager.UsageMode = LicenseUsageMode.Designtime)
        'SWEditorProgressBar.Text = "SignWriter Studio™ SignEditor is loading ..."
        'SWEditorProgressBar.ProgressBar1.Minimum = 0
        'SWEditorProgressBar.ProgressBar1.Maximum = 100
        'SWEditorProgressBar.ProgressBar1.Value = 0

        KeyPreview = True
        'TODO check if needed to be set each time editor loads.
        PBSign.AllowDrop = True

        HandChooser.EditorForm = Me
        ArrowChooser.EditorForm = Me

        'LoadTranslations()

        'SWEditorProgressBar.ProgressBar1.Value = 10

        'Load Favorites
        TvFavoriteLoad()

        'SWEditorProgressBar.ProgressBar1.Value = 30

        'Load All Group Symbols
        AllGroupSymbols_Load()

        'SWEditorProgressBar.ProgressBar1.Value = 50

        'SWEditorProgressBar.ProgressBar1.Value = 55

        'Load Search Selections

        FilterRootShape.DataSource = ISWARootShapesQuickTableAdapter.GetData()

        FilterActionFinger.DataSource = ISWAActionFingersTableAdapter.GetData().DefaultView


        FilterMultipleFingers.DataSource = ISWAMultipleFingersTableAdapter.GetData().DefaultView

        FilterThumbPosition.DataSource = ISWAThumbPositionsTableAdapter.GetData().DefaultView

        Dim TA As New SymbolCache.ISWA2010DataSetTableAdapters.classificationTableAdapter
        Dim DT As DataTable
        DT = TA.GetClassificationView
        HandsClassifiedBindingSource.DataSource = DT

        BaseGroupSuggestion_Load()

        'SWEditorProgressBar.ProgressBar1.Value = 70

        TCSymbols.SelectedTab = TPAllSymbols
        TCSymbols.SelectedTab = TPFavorites

        'SWEditorProgressBar.ProgressBar1.Value = 80

        Me.FirstLoad = True

    End Sub
    Dim AddingUndo As Boolean '= False
    Private Sub AddUndo()

        If AddingUndo Then
            MessageBox.Show("Recurring function Call")
        End If
        AddingUndo = True
        Me.EditorUndo.Add(Me.mySWSign.Clone)
        AddingUndo = False
    End Sub

    Private Sub Undo()
        Dim Sign As SwSign = Me.EditorUndo.Undo(Me.mySWSign.Clone)
        If Sign IsNot Nothing Then
            Me.mySWSign = Sign
            Me.CurrentFrame = Me.GetCurrentFrame(Me.mySWSign)
            Me.DisplaySign()
        End If
    End Sub


    Private Sub Redo()
        Dim Sign As SwSign = Me.EditorUndo.Redo(Me.mySWSign.Clone)
        If Sign IsNot Nothing Then
            Me.mySWSign = Sign
            Me.CurrentFrame = Me.GetCurrentFrame(Me.mySWSign)
            Me.DisplaySign()
        End If
    End Sub

    'Private Sub LoadTranslations()
    '    'TPFavorites.Text = Database.UI.UICGetTranslation("SWEditor", "TPFavorites", 54)
    '    'Me.BtnRemoveSymbol.Text = Database.UI.UICGetTranslation("SWEditor", "BtnRemoveSymbol", 54)
    '    'Me.BtnAddSymbol.Text = Database.UI.UICGetTranslation("SWEditor", "BtnAddSymbol", 54)
    '    'TPSearch.Text = Database.UI.UICGetTranslation("SWEditor", "TPSearch", 54)
    '    'Me.LBThumbPosition.Text = Database.UI.UICGetTranslation("SWEditor", "LBThumbPosition", 54)
    '    'Me.BtnReset.Text = Database.UI.UICGetTranslation("SWEditor", "BtnReset", 54)
    '    'Me.BtnFilter.Text = Database.UI.UICGetTranslation("SWEditor", "BtnFilter", 54)
    '    'Me.BtnBaseGroupName.Text = Database.UI.UICGetTranslation("SWEditor", "BtnBaseGroupName", 54)
    '    'Me.LbMultipleFinger.Text = Database.UI.UICGetTranslation("SWEditor", "LbMultipleFinger", 54)
    '    'Me.LBActionFinger.Text = Database.UI.UICGetTranslation("SWEditor", "LBActionFinger", 54)
    '    'Me.LBRootShape.Text = Database.UI.UICGetTranslation("SWEditor", "LBRootShape", 54)
    '    'Me.GBSign.Text = Database.UI.UICGetTranslation("SWEditor", "GBSign", 54)
    '    'Me.TSMICopy.Text = Database.UI.UICGetTranslation("SWEditor", "TSMICopy", 54)
    '    'Me.TSMICopyCrop.Text = Database.UI.UICGetTranslation("SWEditor", "TSMICopyCrop", 54)
    '    'Me.TSMICenter.Text = Database.UI.UICGetTranslation("SWEditor", "TSMICenter", 54)
    '    'Me.TSMICenterHead.Text = Database.UI.UICGetTranslation("SWEditor", "TSMICenterHead", 54)
    '    'Me.TSMIRemoveSymbols.Text = Database.UI.UICGetTranslation("SWEditor", "TSMIRemove", 54)
    '    'Me.TSMIDuplicateSymbols.Text = Database.UI.UICGetTranslation("SWEditor", "TSMIDuplicate", 54)
    '    'Me.TSMIMoveUp.Text = Database.UI.UICGetTranslation("SWEditor", "MoveUpToolStripMenuItem", 54)
    '    'Me.TSMIMoveDown.Text = Database.UI.UICGetTranslation("SWEditor", "MoveDownToolStripMenuItem", 54)
    '    'HandChooser.GBFills.Text = Database.UI.UICGetTranslation("SWEditor", "GBFills", 54)
    '    'Me.LBSequence.Text = Database.UI.UICGetTranslation("SWEditor", "LBSequence", 54)
    '    'HandChooser.Text = Database.UI.UICGetTranslation("SWEditor", "GBRotations", 54)
    '    'TPSequence.Text = Database.UI.UICGetTranslation("SWEditor", "TabPage1", 54)
    '    'Me.Text = Database.UI.UICGetTranslation("SWEditor", "Me", 54)
    '    'TPAllSymbols.Text = Database.UI.UICGetTranslation("SWEditor", "TPAllSymbols", 54)
    'End Sub
    Public Property Area() As AreaEnm
        Get
            Return _Area
        End Get
        Set(ByVal value As AreaEnm)
            _Area = value
            SetArea()
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
                ActiveControl = PBSign
        End Select
    End Sub
    Private Sub AreaSequenceColor(color As Color)
        TPSequence.BackColor = color
        TVSequence.BackColor = color
    End Sub
    Private Sub AreaSignColor(color As Color)
        GBSign.BackColor = color

    End Sub
    Private Sub AreaChooserColor(color As Color)
        GBChooser.BackColor = color
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
        Select Case e.KeyCode

            Case Keys.F5
                Area = AreaEnm.Favorites
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.F6
                Area = AreaEnm.AllGroups
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.F7
                Area = AreaEnm.Search
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.F8
                Area = AreaEnm.Sequence
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.F9
                Area = AreaEnm.Choose
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
            Case Keys.F12
                Area = AreaEnm.Sign
                e.SuppressKeyPress = True
                e.Handled = True
                Exit Sub
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



    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        Dim DeglossChoosers1 = New DeglossChoosers
        Dim DeglossedList As List(Of DeglossResult) = DeglossChoosers1.GetDeglossed(TVAllGroups)

        Dim sb As New System.Text.StringBuilder

        For Each item In DeglossedList
            sb.Append("UPDATE basesymbol SET ")
            sb.Append("UseArrowChooser " & "=" & TrueFalse(item.UseArrowChooser))
            sb.Append(", ShowWallPlane " & "=" & TrueFalse(item.ShowWallPlane))
            sb.Append(", ShowWallPlaneImage " & "=" & TrueFalse(item.ShowWallPlaneImage))
            sb.Append(", ShowFloorPlane " & "=" & TrueFalse(item.ShowFloorPlane))
            sb.Append(", ShowFloorPlaneImage " & "=" & TrueFalse(item.ShowFloorPlaneImage))
            sb.Append(", ShowFlip " & "=" & TrueFalse(item.ShowFlip))
            sb.Append(", ShowVP3VP7 " & "=" & TrueFalse(item.ShowVP3VP7))
            sb.AppendLine("  WHERE  bs_sym_code = " & item.Code & ";")
        Next

        Using outfile As New IO.StreamWriter("ShowArrowsUpdate.txt")
            outfile.Write(sb.ToString())
        End Using
    End Sub

    Private Function TrueFalse(bool As Boolean) As String
        If bool Then
            Return "1"
        Else
            Return "0"
        End If
    End Function

    Private Sub CMSPBSign_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles CMSPBSign.Opening

    End Sub

    Private Sub btnHelp_MouseClick(sender As System.Object, e As System.Windows.Forms.MouseEventArgs) Handles btnHelp.MouseClick
        Help.ShowHelp(Me, "SignWriterStudio.chm", "signeditor.htm")
    End Sub

    Private Sub GBChooser_MouseHover(sender As System.Object, e As System.EventArgs) Handles GBChooser.MouseHover
        If Not Area = AreaEnm.Choose Then
            Area = AreaEnm.Choose
        End If
    End Sub

    Private Sub TPFavorites_MouseEnter(sender As System.Object, e As System.EventArgs) Handles TPFavorites.MouseEnter
        If Not Area = AreaEnm.Favorites Then
            Area = AreaEnm.Favorites
        End If
    End Sub

    Private Sub TPAllSymbols_MouseEnter(sender As System.Object, e As System.EventArgs) Handles TPAllSymbols.MouseEnter
        If Not Area = AreaEnm.AllGroups Then
            Area = AreaEnm.AllGroups
        End If
    End Sub


    Private Sub GBSign_MouseHover(sender As System.Object, e As System.EventArgs) Handles GBSign.MouseHover
        If Not Area = AreaEnm.Sign Then
            Area = AreaEnm.Sign
        End If
    End Sub



    Private Sub TVHand_MouseEnter(sender As System.Object, e As System.EventArgs) Handles TVHand.MouseEnter
        If Not Area = AreaEnm.Search Then
            Area = AreaEnm.Search
        End If
    End Sub

    Private Sub Editor_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        JustLoaded = True
        isLoading = True
        If (Not Me.DesignMode) Then
            'SWEditorProgressBar.Text = "SignWriter Studio™ SignEditor is loading ..."
            'SWEditorProgressBar.ProgressBar1.Minimum = 0
            'SWEditorProgressBar.ProgressBar1.Maximum = 100
            'SWEditorProgressBar.ProgressBar1.Value = 0
            'SWEditorProgressBar.Show()

            If Not Me.FirstLoad Then
                LoadFirstTime()
            End If

            HandChooser.Visible = False
            ArrowChooser.Visible = False

            ResetHandFilter()

            'SWEditorProgressBar.ProgressBar1.Value = 85

            Area = AreaEnm.Favorites

            'SWEditorProgressBar.ProgressBar1.Value = 90

            If Me.mySWSign IsNot Nothing Then
                CurrentFrame = GetCurrentFrame(Me.mySWSign)
            Else
                Me.mySWSign = New SwSign
                CurrentFrame = GetCurrentFrame(Me.mySWSign)
            End If
            Me.DisplaySign()
            PBSign.Invalidate()

            'SWEditorProgressBar.ProgressBar1.Value = 100
            'SWEditorProgressBar.Hide()
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

    Private Sub TVSequence_MouseEnter(sender As System.Object, e As System.EventArgs)
        Area = AreaEnm.Sequence
    End Sub

    Private Sub ArrowChooser_Load(sender As System.Object, e As System.EventArgs) Handles ArrowChooser.Load

    End Sub

    Private Sub CopyImageToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyImageToolStripMenuItem.Click
        Me.mySWSign.SetClipboardImage()
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
            MinimalExample.WinForms.Program.ShowForm()
        Catch ex As Exception
            MessageBox.Show(ex.Message & ex.StackTrace)
        End Try

    End Sub
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
