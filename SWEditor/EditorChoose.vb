
Imports System.Drawing
Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms
Imports SignWriterStudio.SWS
Partial Public Class Editor
#Region "Which"
    Private ChooserLoading As Boolean
    Dim ChangingSelected As Boolean '= False
    Private Sub SetupChoosers()
       If TVChooser.Visible Then
            TVExpand(TVChooser)
       End If
    End Sub
    Private Sub DisplayChooseControls(ByVal code As Integer)
        Dim mySWsymbol As New SWSymbol With {.Code = code}

        If mySWsymbol.IsValid Then

            Dim Category = mySWsymbol.Category
            If Category = 1 Then 'Hands
                HandChooser.Visible = True
                ArrowChooser.Visible = False
                TVChooser.Visible = False

            ElseIf UseArrowChooser(mySWsymbol) Then 'Arrows
                TVChooser.Visible = False
                ArrowChooser.Visible = True
                ArrowChooser.Reset(mySWsymbol.Code)

                HandChooser.Visible = False

            Else 'Others
                TVChooser.Visible = True
                ArrowChooser.Visible = False
                HandChooser.Visible = False
                TVChooser_Load()
            End If
        Else
            TVChooser.Visible = False
            HandChooser.Visible = False
            ArrowChooser.Visible = False
        End If
        SetupChoosers()
    End Sub
    Private Sub ChangeSymbolIn(ByVal code As Integer)

        If Not UpdateSignSymbolSelected Then
            
            symbolIn.Code = code
            symbolIn.Hand = symbolIn.GuessIfRightorLeft
            
            If TVChooser.Visible Then
                TVChooser.Select()
                TVChooser.Focus()

            ElseIf HandChooser.Visible Then
                HandChooser.GBFills.Select()
                HandChooser.GBFills.Focus()
                HandChooser.Reset(symbolIn.Code)
            ElseIf ArrowChooser.Visible Then
                ArrowChooser.Select()
                ArrowChooser.Focus()
            Else
                PBSign.Select()
            End If
            Area = AreaEnm.Choose
        End If
    End Sub
    Private Sub ChangeChangeSymbolIn(ByVal NewSymbol As SWSignSymbol, ByVal Hand As Integer)
        Dim symbol As SWSignSymbol
        If Not ChangingSelected Then
            ChangingSelected = True

            NewSymbol.IsSelected = True

            If Not UpdateSignSymbolSelected Then
                UpdateSignSymbolSelected = True
                For I As Integer = CurrentFrame.SignSymbols.Count - 1 To 0 Step -1

                    symbol = CurrentFrame.SignSymbols(I)
                    If symbol.IsSelected Then

                        symbol.Code = NewSymbol.Code
                        symbol.Hand = Hand
                        symbol.SetSignSymbol(Nothing)
                    End If
                Next

                UpdateSignSymbolSelected = False
                DisplaySign()
            End If
            ChangingSelected = False
        End If
    End Sub
#End Region
#Region "HandChooser"
    Private Sub HandChooser_Accept(ByVal sender As Object, ByVal e As EventArgs) Handles HandChooser.Accept
        Dim ControlActive As Control = ActiveControl
        If ControlActive IsNot Nothing AndAlso (ControlActive.Name = "TVChooser") Then

            'Add symbol and go to sign to position it



            symbolIn.Code = SWSymbol.CodefromId(TVChooser.SelectedNode.Name)
            'symbolIn.Update()
            InsertSymbolIntoSign(symbolOut.Code)
            Area = AreaEnm.Sign
            DisplaySign()

        Else

            'Add symbol and go to sign to position it

            InsertSymbolIntoSign(symbolOut.Code)
            Area = AreaEnm.Sign
            DisplaySign()

        End If
    End Sub
    Private Sub HandChooser_Escape(ByVal sender As Object, ByVal e As EventArgs) Handles HandChooser.Escape
        'Area = AreaEnm.Favorites
        CBFavorites.Focus()
    End Sub
    Private Sub HandChooser_ChangeSymbol(ByVal sender As Object, ByVal e As EventArgs) Handles HandChooser.ChangeSymbol

        symbolOut.Code = MakeNewSymbol(symbolIn.Code, symbolOut.Code, HandChooser.NewFill, HandChooser.NewRotation(Me.symbolIn.SymbolDetails.Category))

    End Sub
    Private Sub HandChooser_ChangeChangeSymbolIn(ByVal sender As Object, ByVal e As EventArgs) Handles HandChooser.ChangeSelectedSym
        ChangeChangeSymbolIn(Me.symbolOut, HandChooser.Hand)
    End Sub
    Private Sub HandChooser_Find(ByVal sender As Object, ByVal e As EventArgs) Handles HandChooser.Find
        AllGroupsFind(Me.symbolOut.Code)
    End Sub
    Private Sub HandChooser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles HandChooser.MouseEnter
        Area = AreaEnm.Choose
    End Sub
#End Region
#Region "ArrowChooser"
    'Private Shared Function UseArrowChooser(ByVal SSS As SWSymbol) As Boolean
    '    Dim ValidFills As Integer = SSS.Fills
    '    Dim ValidRotations As Integer = SSS.Rotations
    '    Dim Category As Integer = SSS.Category

    '    If Category = 2 AndAlso (ValidFills = 3 Or ValidFills = 4 Or ValidFills = 6) AndAlso (ValidRotations = 8 Or ValidRotations = -8 Or ValidRotations = 16 Or ValidRotations = -16) Then

    '        'Exceptions
    '        If Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 3) AndAlso
    '                Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 6) AndAlso
    '                Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 9) AndAlso
    '                Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 10) AndAlso
    '                Not (Category = 2 AndAlso SSS.Group = 2 AndAlso SSS.Symbol = 11) Then



    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Else
    '        Return False
    '    End If

    'End Function
    Private Sub Choosers_RightClick(sender As Object, e As MouseEventArgs) Handles ArrowChooser.ChooserMouseDown, HandChooser.ChooserMouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Middle Then
            ReplaceSymbol()
        End If
    End Sub
    Private Sub Choosers_MouseDown(sender As Object, e As MouseEventArgs) Handles ArrowChooser.MouseDown, HandChooser.MouseDown, TVChooser.MouseClick, TVChooser.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Middle Then
            ReplaceSymbol()
        End If
    End Sub
   
    Private Shared Function UseArrowChooser(ByVal Symbol As SWSymbol) As Boolean
        Dim DT = SymbolCache.SWSymbolCache.GetArrowChoosingInfo(Symbol.BaseGroup)

        If DT.Rows.Count > 0 Then
            Dim Use As Boolean? = DT.Rows(0).Field(Of Boolean?)("UseArrowChooser")
            If Use.HasValue Then
                Return Use.Value
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Private Sub ArrowChooser_Accept(ByVal sender As Object, ByVal e As EventArgs) Handles ArrowChooser.Accept
        Dim ControlActive As Control = ActiveControl
        If ControlActive IsNot Nothing AndAlso (ControlActive.Name = "TVChooser") Then

            'Add symbol and go to sign to position it


            symbolIn.Code = SWSymbol.CodefromId(TVChooser.SelectedNode.Name)
            'symbolIn.Update()
            InsertSymbolIntoSign(symbolOut.Code)
            Area = AreaEnm.Sign
            DisplaySign()

        Else

            'Add symbol and go to sign to position it

            InsertSymbolIntoSign(symbolOut.Code)
            Area = AreaEnm.Sign
            DisplaySign()

        End If
    End Sub
    Private Sub ArrowChooser_ChangeChangeSymbolIn(ByVal sender As Object, ByVal e As EventArgs) Handles ArrowChooser.ChangeSelectedSym
        ChangeChangeSymbolIn(Me.symbolOut, 0)
    End Sub
    Private Sub ArrowChooser_ChangeSymbol(ByVal sender As Object, ByVal e As EventArgs) Handles ArrowChooser.ChangeSymbol
        symbolOut.Code = MakeNewSymbol(symbolIn.Code, symbolOut.Code, ArrowChooser.NewFill, ArrowChooser.NewRotation(Me.symbolIn.Code), ArrowChooser.CheckPlane)
    End Sub
    Private Sub ArrowChooser_Escape(ByVal sender As Object, ByVal e As EventArgs) Handles ArrowChooser.Escape
        Area = AreaEnm.Favorites
        CBFavorites.Focus()
    End Sub
    Private Sub ArrowChooser_Find(ByVal sender As Object, ByVal e As EventArgs) Handles ArrowChooser.Find
        AllGroupsFind(Me.symbolOut.Code)
    End Sub
    Private Sub ArrowChooser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles ArrowChooser.MouseEnter
        Area = AreaEnm.Choose
    End Sub
#End Region
#Region " Chooser"
    Private Sub TVChooser_Load()
        ChooserLoading = True
        TVChooser.Nodes.Clear()

        Dim SymbolsRows As SymbolCache.ISWA2010DataSet.cacheRow() = SymbolCache.Iswa2010.SC.GetCodeFull(Me.symbolIn.Code)
        'Dim SymbolsRows As DataTable = SymbolsTableAdapter.GetDataBySubSymbols(Mid(TextBox1.Text, 1, 9))

        Dim SymbolsImageList As ImageList = SWDrawing.LoadImageList(SymbolsList, SymbolsRows, 55, 50, Color.OrangeRed)
        Dim SymbolsRowsRow As SymbolCache.ISWA2010DataSet.cacheRow

        'Assign the ImageList to the TreeView.
        TVChooser.ImageList = SymbolsImageList
        Dim Group As String

        For Each SymbolsRowsRow In SymbolsRows
            Group = SymbolsRowsRow.sym_id.Substring(10, 2)
            If TVChooser.Nodes.Find("Group" & Group, True).Length = 0 Then
                Dim newGroupNode As New TreeNode
                'newGroupNode.Text = "Group" & Group
                newGroupNode.SelectedImageKey = SymbolsRowsRow.sym_id
                newGroupNode.ImageKey = SymbolsRowsRow.sym_id
                newGroupNode.Name = "Group" & Group
                'newGroupNode.ToolTipText = SymbolsRowsRow.bs_name

                TVChooser.Nodes.Add(CType(newGroupNode.Clone, TreeNode))
            End If
            Dim newNode As New TreeNode
            'newNode.Text = SymbolRow.bs_name
            newNode.SelectedImageKey = "S" & SymbolsRowsRow.sym_id
            newNode.ImageKey = SymbolsRowsRow.sym_id
            newNode.Name = SymbolsRowsRow.sym_id
            newNode.ToolTipText = SymbolsRowsRow.bs_name

            TVChooser.Nodes("Group" & Group).Nodes.Add(CType(newNode.Clone, TreeNode))
        Next
        Dim SybolsPadding As New System.Windows.Forms.Padding(2)
        TVChooser.Margin = SybolsPadding
        If Not TVChooser.Nodes.Find(Me.symbolOut.SymbolDetails.Id, True).Length = 0 Then
            TVChooser.SelectedNode = TVChooser.Nodes.Find(Me.symbolOut.SymbolDetails.Id, True)(0)
        Else
            TVChooser.CollapseAll()
        End If

        ChooserLoading = False
    End Sub
    Private Sub TVChooser_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TVChooser.AfterSelect
        If Not ChooserLoading AndAlso TVChooser.SelectedNode IsNot Nothing AndAlso SignWriterStudio.General.CheckId(TVChooser.SelectedNode.Name) Then
            symbolOut.Code = SWSymbol.CodefromId(sender.SelectedNode.Name)
        End If
    End Sub
   
    Private Sub TVChooser_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TVChooser.MouseDown
        TVChooser.SelectedNode = TVChooser.GetNodeAt(e.X, e.Y)
        Dim CurrentTreeNode As TreeNode = TVChooser.SelectedNode

        If CurrentTreeNode IsNot Nothing Then
            TVChooser.DoDragDrop(TVChooser.Name, DragDropEffects.Copy)
        End If
    End Sub
    Private Sub TVChooser_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TVChooser.MouseEnter
        Area = AreaEnm.Choose
    End Sub
    Private Sub TVChooser_Choose_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "generalchooser.htm")
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad1, Keys.D1
                SelectNode(TVChooser, 1)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad2, Keys.D2
                SelectNode(TVChooser, 2)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad3, Keys.D3
                SelectNode(TVChooser, 3)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad4, Keys.D4
                SelectNode(TVChooser, 4)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad5, Keys.D5
                SelectNode(TVChooser, 5)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad6, Keys.D6
                SelectNode(TVChooser, 6)
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.C
                If e.Alt Then
                    Me.ChangeChangeSymbolIn(symbolOut, HandChooser.Hand)
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.F
                If e.Alt Then
                    AllGroupsFind(Me.symbolOut.Code)
                End If
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Enter
                TVChooser_Accept()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Escape
                TVChooser_Escape()
                e.SuppressKeyPress = True
                e.Handled = True
        End Select

    End Sub
    Private Sub TVChooser_Accept()
        Dim ControlActive As Control = ActiveControl
        If ControlActive IsNot Nothing AndAlso (ControlActive.Name = "TVChooser") Then

            'Add symbol and go to sign to position it

            Me.symbolIn.Code = SWSymbol.CodefromId(TVChooser.SelectedNode.Name)
            'Me.symbolIn.Update()
            InsertSymbolIntoSign(Me.symbolOut.Code)
            Area = AreaEnm.Sign
            Me.DisplaySign()

        Else

            'Add symbol and go to sign to position it

            InsertSymbolIntoSign(Me.symbolOut.Code)
            Area = AreaEnm.Sign
            DisplaySign()

        End If
    End Sub
    Private Sub TVChooser_Escape()
        Area = AreaEnm.Favorites
        CBFavorites.Focus()
    End Sub
#End Region
End Class
