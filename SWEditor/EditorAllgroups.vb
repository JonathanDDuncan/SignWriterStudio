Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms
Imports SignWriterStudio.General.All
Imports SignWriterStudio.SWS
Imports SignWriterStudio.SymbolCache

Partial Public Class Editor
#Region "AllGroups"

    Private _updateChooser As Boolean = True

    Private Sub AllGroupSymbols_Load()

        Dim myDataRow As ISWA2010DataSet.cacheRow() = CType(SC.GetBase(), ISWA2010DataSet.cacheRow())

        SymbolsToTreeView.Load(TVAllGroups, SC.AllGroupsList, myDataRow, True)

    End Sub
    Private Sub AllGroups_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim ControlActive As Control = ActiveControl
        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "allsymbols.htm")
            Case Keys.NumPad1, Keys.D1
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category1", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad2, Keys.D2
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category2", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad3, Keys.D3
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category3", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode

                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad4, Keys.D4
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category4", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad5, Keys.D5
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category5", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad6, Keys.D6
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category6", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.NumPad7, Keys.D7
                TVAllGroups.CollapseAll()
                Dim CatNode = TVAllGroups.Nodes.Find("Category7", False)(0)
                CatNode.EnsureVisible()
                CatNode.Expand()
                TVAllGroups.SelectedNode = CatNode
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Enter
                If ControlActive IsNot Nothing AndAlso Not ControlActive.Name = CBFavorites.Name Then


                    ChangeSymbolIn(SWSymbol.CodefromId(TVAllGroups.SelectedNode.Name))
                    Area = AreaEnm.Choose
                    e.SuppressKeyPress = True
                    e.Handled = True
                ElseIf ControlActive IsNot Nothing AndAlso ControlActive.Name = CBFavorites.Name Then
                    Dim FoundItem As Integer = CBFavorites.FindStringExact(CBFavorites.Text)
                    If Not FoundItem = -1 Then
                        CBFavorites.SelectedItem = CBFavorites.Items(FoundItem)
                    End If
                    e.SuppressKeyPress = True
                    e.Handled = True
                End If
        End Select
    End Sub
    Private Sub TVAllGroups_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TVAllGroups.MouseEnter
        Area = AreaEnm.AllGroups
    End Sub
    Private Sub AllGroupsFind(ByVal code As Integer, Optional ByVal updateChooser As Boolean = True)
        Dim TreeNodes() As TreeNode
        Dim Symbol As New SWSignSymbol With {.Code = code}
        Dim SSS As String = Symbol.SymbolDetails.Id

        If SSS.Length >= 12 Then
            SSS = SSS.Substring(0, 12) & "-02-01"
        End If

        TreeNodes = TVAllGroups.Nodes.Find(SSS, True)
        If TreeNodes.Length = 0 Then
            If SSS.Length >= 12 Then
                SSS = SSS.Substring(0, 12) & "-01-01"
            End If
            TreeNodes = TVAllGroups.Nodes.Find(SSS, True)
        End If
        If TreeNodes.Length > 0 Then
            _updateChooser = updateChooser
            TVAllGroups.CollapseAll()
            TVAllGroups.SelectedNode = TreeNodes(0)
            TreeNodes(0).Expand()
            _updateChooser = True
        End If
        TCSymbols.SelectTab(TPChooser)
    End Sub
    Private Sub symbolIn_SymbolChanged(ByVal sender As Object, ByVal e As EventArgs) Handles symbolIn.SymbolChanged
        If _updateChooser Then
            symbolOut.Code = symbolIn.Code
            DisplayChooseControls(symbolOut.Code)
        End If
    End Sub

    Private Sub TVAllGroups_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TVAllGroups.MouseDown
       
  
    End Sub


    Private Sub TVAllGroups_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TVAllGroups.Click
        TVAllGroups.SelectedNode = TVAllGroups.GetNodeAt(e.X, e.Y)
        Dim CurrentTreeNode As TreeNode = TVAllGroups.SelectedNode

        If CurrentTreeNode IsNot Nothing Then
            CurrentTreeNode.Expand()
            TVAllGroups.DoDragDrop(TVAllGroups.Name, DragDropEffects.Copy)
        End If
    End Sub
    
    Private Sub TVAllGroups_DoubleClick(sender As Object, e As EventArgs) Handles TVAllGroups.DoubleClick
       If TVAllGroups.SelectedNode IsNot Nothing Then

                Dim nodeName As String = TVAllGroups.SelectedNode.Name
                If CheckId(nodeName) And _updateChooser Then
                    ChangeSymbolIn(SWSymbol.CodefromId(nodeName))
                    'Area = AreaEnm.Choose
                End If
            End If
        End Sub
     
#End Region
End Class
