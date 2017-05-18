'Option Strict On
Imports System.Drawing
Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms
Imports SignWriterStudio.SWS
Partial Public Class Editor
#Region "Search"
    Private Sub Search_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim ControlActive As Control = CType(ActiveControl, Control)
        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "handsearch.htm")
            Case Keys.NumPad1, Keys.D1
                FilterThumb.Select()
                FilterThumb.CheckState = NextThumb()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad4, Keys.D4
                FilterIndex.Select()
                FilterIndex.CheckState = NextIndex()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad8, Keys.D8
                FilterMiddle.Select()
                FilterMiddle.CheckState = NextMiddle()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad6, Keys.D6
                FilterRing.Select()
                FilterRing.CheckState = NextRing()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad3, Keys.D3
                FilterBaby.Select()
                FilterBaby.CheckState = NextBaby()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Enter
                If TVHand.Nodes.Count > 0 AndAlso TVHand.SelectedNode IsNot Nothing Then

                    symbolIn.Code = SWSymbol.CodefromId(TVHand.SelectedNode.Name)

                    symbolOut.Code = symbolIn.Code


                    Area = AreaEnm.Choose
                    e.SuppressKeyPress = True
                    e.Handled = True

                Else
                    SetFilter()
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
            Case Keys.Escape
                ResetHandFilter()
                e.SuppressKeyPress = True
                e.Handled = True

        End Select
    End Sub


    Private Sub FilterRootShape_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FilterRootShape.SelectedIndexChanged, FilterActionFinger.SelectedIndexChanged, FilterThumbPosition.SelectedIndexChanged, FilterMultipleFingers.SelectedIndexChanged
        ResetResults()
    End Sub

    Private Sub Filte_CheckStateChanged(sender As Object, e As EventArgs) Handles FilterThumb.CheckStateChanged, FilterIndex.CheckStateChanged, FilterMiddle.CheckedChanged, FilterRing.CheckedChanged, FilterBaby.CheckedChanged
        ResetResults()
    End Sub

    Private Sub Choose_Focus()
        If TVChooser.Visible Then
            TVChooser.Select()


        ElseIf HandChooser.Visible Then
            HandChooser.GBFills.Select()

        ElseIf HandChooser.Visible Then
            ArrowChooser.VP1.Select()

        Else
            PBSign.Select()
        End If
    End Sub
    Private Sub BaseGroupSuggestion_Load()
        FilterSymbolName.AutoCompleteMode = AutoCompleteMode.Suggest
        FilterSymbolName.AutoCompleteSource = AutoCompleteSource.CustomSource

        Dim DT As SymbolCache.ISWA2010DataSet.basesymbolDataTable = ISWABaseSymbolsTableAdapter.GetDataBySymbolNameHand
        Dim ACCS As AutoCompleteStringCollection = FilterSymbolName.AutoCompleteCustomSource
        For Each Row As SymbolCache.ISWA2010DataSet.basesymbolRow In DT.Rows
            ACCS.Add(Row.bs_name)
        Next
    End Sub
    Private Function NextThumb() As CheckState
        Select Case FilterThumb.CheckState
            Case CheckState.Checked
                Return CheckState.Unchecked
            Case CheckState.Indeterminate
                Return CheckState.Checked
            Case CheckState.Unchecked
                Return CheckState.Indeterminate
        End Select
    End Function
    Private Function NextIndex() As CheckState
        Select Case FilterIndex.CheckState
            Case CheckState.Checked
                Return CheckState.Unchecked
            Case CheckState.Indeterminate
                Return CheckState.Checked
            Case CheckState.Unchecked
                Return CheckState.Indeterminate
        End Select
    End Function
    Private Function NextMiddle() As CheckState
        Select Case FilterMiddle.CheckState
            Case CheckState.Checked
                Return CheckState.Unchecked
            Case CheckState.Indeterminate
                Return CheckState.Checked
            Case CheckState.Unchecked
                Return CheckState.Indeterminate
        End Select
    End Function
    Private Function NextRing() As CheckState
        Select Case FilterRing.CheckState
            Case CheckState.Checked
                Return CheckState.Unchecked
            Case CheckState.Indeterminate
                Return CheckState.Checked
            Case CheckState.Unchecked
                Return CheckState.Indeterminate
        End Select
    End Function
    Private Function NextBaby() As CheckState
        Select Case FilterBaby.CheckState
            Case CheckState.Checked
                Return CheckState.Unchecked
            Case CheckState.Indeterminate
                Return CheckState.Checked
            Case CheckState.Unchecked
                Return CheckState.Indeterminate
        End Select
    End Function

    'Private Sub TVHand_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TVHand.MouseDown
    '    TVHand.SelectedNode = TVHand.GetNodeAt(e.X, e.Y)

    '    Dim CurrentTreeNode As TreeNode = TVHand.SelectedNode

    '    If CurrentTreeNode IsNot Nothing Then
    '        TVHand.DoDragDrop(TVHand.Name, DragDropEffects.Copy)
    '    End If
    'End Sub

    Sub SetFilter()
        Dim strFilter As String = String.Empty
        SetFilterCategorized(strFilter)
        SetFilterFingers(strFilter)

        If strFilter = String.Empty Then
            HandsClassifiedBindingSource.Filter = Nothing
        Else
            HandsClassifiedBindingSource.Filter = strFilter
        End If
        TVHandLoad(strFilter)


    End Sub
    Private Sub SetFilterFingers(ByRef strFilter As String)
        If Not FilterThumb.CheckState = CheckState.Indeterminate Then
            If strFilter = String.Empty Then
                strFilter = "Thumb=" & IsChecked(FilterThumb.CheckState).ToString
            Else
                strFilter = strFilter & " AND Thumb=" & IsChecked(FilterThumb.CheckState).ToString
            End If

        End If
        If Not FilterIndex.CheckState = CheckState.Indeterminate Then
            If strFilter = String.Empty Then
                strFilter = "Index=" & IsChecked(FilterIndex.CheckState).ToString
            Else
                strFilter = strFilter & " AND Index=" & IsChecked(FilterIndex.CheckState).ToString
            End If
        End If
        If Not FilterMiddle.CheckState = CheckState.Indeterminate Then
            If strFilter = String.Empty Then
                strFilter = "Middle=" & IsChecked(FilterMiddle.CheckState).ToString
            Else
                strFilter = strFilter & " AND Middle=" & IsChecked(FilterMiddle.CheckState).ToString
            End If
        End If
        If Not FilterRing.CheckState = CheckState.Indeterminate Then
            If strFilter = String.Empty Then
                strFilter = "Ring=" & IsChecked(FilterRing.CheckState).ToString
            Else
                strFilter = strFilter & " AND Ring=" & IsChecked(FilterRing.CheckState).ToString
            End If

        End If
        If Not FilterBaby.CheckState = CheckState.Indeterminate Then
            If strFilter = String.Empty Then
                strFilter = "Baby=" & IsChecked(FilterBaby.CheckState).ToString
            Else
                strFilter = strFilter & " AND Baby=" & IsChecked(FilterBaby.CheckState).ToString
            End If

        End If
    End Sub
    Private Sub SetFilterCategorized(ByRef strFilter As String)
        If Not (FilterRootShape.SelectedIndex = -1 Or FilterRootShape.SelectedIndex = 0 Or FilterRootShape.SelectedValue Is Nothing) Then
            If strFilter = String.Empty Then
                strFilter = "[IDRootShapeQuick]=" & FilterRootShape.SelectedValue.ToString
            Else
                strFilter = strFilter & " AND [IDRootShapeQuick]=" & FilterRootShape.SelectedValue.ToString
            End If
        End If
        If Not (FilterSymbolName.Text = String.Empty) Then
            If strFilter = String.Empty Then
                strFilter = "[bs_name] LIKE '%" & FilterSymbolName.Text & "%'"
            Else
                strFilter = strFilter & " AND [bs_name]LIKE '%" & FilterSymbolName.Text & "%'"
            End If

        End If
        If Not (FilterActionFinger.SelectedIndex = -1 Or FilterActionFinger.SelectedIndex = 0 Or FilterActionFinger.SelectedValue Is Nothing) Then
            If strFilter = String.Empty Then
                strFilter = "[IDActionFinger]=" & FilterActionFinger.SelectedValue.ToString
            Else
                strFilter = strFilter & " AND [IDActionFinger]=" & FilterActionFinger.SelectedValue.ToString
            End If
        End If
        If Not (FilterMultipleFingers.SelectedIndex = -1 Or FilterMultipleFingers.SelectedIndex = 0 Or FilterMultipleFingers.SelectedValue Is Nothing) Then
            If strFilter = String.Empty Then
                strFilter = "[IDMultipleFinger]=" & FilterMultipleFingers.SelectedValue.ToString
            Else
                strFilter = strFilter & " AND [IDMultipleFinger]=" & FilterMultipleFingers.SelectedValue.ToString
            End If
        End If
        If Not (FilterThumbPosition.SelectedIndex = -1 Or FilterThumbPosition.SelectedIndex = 0 Or FilterThumbPosition.SelectedValue Is Nothing) Then
            If strFilter = String.Empty Then
                strFilter = "[IDThumbPosition]=" & FilterThumbPosition.SelectedValue.ToString.ToString
            Else
                strFilter = strFilter & " AND [IDThumbPosition]=" & FilterThumbPosition.SelectedValue.ToString
            End If
        End If
    End Sub
    Sub TVHandLoad(ByVal handFilter As String)
        'Dim SymbolRow As DataRow
        Dim ImageSize As Size
        ImageSize.Height = 55
        ImageSize.Width = 50
        TVHand.Nodes.Clear()

        Dim DataTable1 As DataTable
        Dim FilteredView As DataView
        DataTable1 = CType(HandsClassifiedBindingSource.DataSource, DataTable)
        FilteredView = New DataView(DataTable1)
        'Set up image list
        If HandImageList.Images.Count < 1 Then

            ' Assign the ImageList to the TreeView.
            HandImageList.Images.Clear()
            Dim CacheRows As SymbolCache.ISWA2010DataSet.cacheRow()
            Dim TA As New SymbolCache.ISWA2010DataSetTableAdapters.cacheTableAdapter
            CacheRows = TA.GetDataBaseHands.Select()
            HandImageList = SWDrawing.LoadImageList(HandImageList, CacheRows, ImageSize.Height, ImageSize.Width, Color.OrangeRed)

        End If
        TVHand.ImageList = HandImageList

        FilteredView.RowFilter = handFilter
        TVHand.BeginUpdate()
        If FilteredView.Count > 0 Then
            Dim i As Integer
            Dim symb As New SWSymbol

            For i = 0 To FilteredView.Count - 1
                symb.Code = CInt(FilteredView(i)("sym_code"))
                Dim newNode As New TreeNode
                newNode.Text = symb.BaseName 'SymbolRow.BaseGroupName
                newNode.SelectedImageKey = "S" & symb.Id
                newNode.ImageKey = symb.Id
                newNode.Name = symb.Id
                newNode.ToolTipText = symb.BaseName

                TVHand.Nodes.Add(newNode)
            Next

            TVHand.CollapseAll()
        Else
            Dim MBO As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
            MessageBox.Show("There are no matches for your criteria. ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)

        End If
        TVHand.EndUpdate()
    End Sub
    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnFilter.Click
        SetFilter()
    End Sub

    Private Sub Reset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnReset.Click
        ResetHandFilter()
    End Sub
    Private Sub ResetHandFilter()
        If Not isLoading Then
            FilterThumb.CheckState = CheckState.Indeterminate
            FilterIndex.CheckState = CheckState.Indeterminate
            FilterMiddle.CheckState = CheckState.Indeterminate
            FilterRing.CheckState = CheckState.Indeterminate
            FilterBaby.CheckState = CheckState.Indeterminate
            FilterRootShape.SelectedIndex = 0
            FilterThumbPosition.SelectedIndex = 0
            FilterActionFinger.SelectedIndex = 0
            FilterMultipleFingers.SelectedIndex = 0
            FilterSymbolName.Text = String.Empty
            ResetResults()
        End If
    End Sub
    Private Shared Function IsChecked(ByVal checkedValue As CheckState) As Boolean
        If checkedValue = CheckState.Checked Then
            Return True
        ElseIf checkedValue = CheckState.Unchecked Then
            Return False
        Else
            Return Nothing
        End If
    End Function
    Private Sub TVHand_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TVHand.NodeMouseClick
        Dim clickedNode As TreeNode = e.Node
        If clickedNode IsNot Nothing Then

            symbolIn.Code = SWSymbol.CodefromId(clickedNode.Name)
            symbolOut.Code = symbolIn.Code
          
            If TVChooser.Visible Then
                TVChooser.Select()

                TVExpand(TVChooser)
            ElseIf HandChooser.Visible Then
                HandChooser.GBFills.Select()

            Else
                PBSign.Select()
            End If
            Area = AreaEnm.Choose
        End If

    End Sub
    Private Sub TPSearch_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles TPSearch.MouseEnter
        If Not Area = AreaEnm.Search Then
            Area = AreaEnm.Search
        End If
    End Sub


#End Region

    Private Sub ResetResults()
        TVHand.Nodes.Clear()
    End Sub


End Class
