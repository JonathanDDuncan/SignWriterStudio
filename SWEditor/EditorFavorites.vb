Option Strict On
Imports System.Drawing
Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms
Imports SignWriterStudio.Settings.Favorites
Imports NUnit.Framework
Imports SignWriterStudio.General
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Partial Public Class Editor

#Region "Favorites"
    Private Sub TvFavoriteLoad()
        ISWAFavoriteSymbolsTableAdapter.Fill(SettingsDataSet.Favorites)
        CBFavorites.Refresh()
        LoadFavs()
    End Sub
    Private Sub LoadFavs()
        Dim imgList As New ImageList
        Dim rows As Settings.SettingsDataSet.FavoritesRow() = CType(SettingsDataSet.Tables("Favorites").Select(), Settings.SettingsDataSet.FavoritesRow())
        For Each favorite As Settings.SettingsDataSet.FavoritesRow In rows
            imgList = SWDrawing.AddImagestoImageList(imgList, favorite.FavoriteName, ByteArraytoImage(favorite.img), 100, 100)
        Next
        TVFavoriteSymbols.ImageList = imgList
        For Each favorite As Settings.SettingsDataSet.FavoritesRow In rows
            TVFavoriteSymbols.Nodes.Add(favorite.IDFavorites.ToString, favorite.FavoriteName, favorite.FavoriteName, favorite.FavoriteName)
        Next
        TVFavoriteSymbols.Refresh()
    End Sub


    Private Sub Favorites_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "favoritesymbols.htm")
            Case Keys.Insert
                NewFavorite()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Delete
                RemoveFavorite()
                e.SuppressKeyPress = True
                e.Handled = True
            Case Keys.Enter
                AddedFavoriteByDrop = False
                AddSelectedFavorite()
                e.SuppressKeyPress = True
                e.Handled = True

        End Select

    End Sub

    Private Sub CBFavorites_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles CBFavorites.GotFocus
        Area = AreaEnm.Favorites
    End Sub

    Private Sub BtnNewFavorite_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnNewFavorite.Click
        NewFavorite()
    End Sub
    'Sub
    ''' <summary>
    ''' Sub AddFavorite description
    ''' </summary>
    Friend Sub NewFavorite()
        Dim selectedSymbols As Integer = Sign.Frames(Sign.CurrentFrameIndex).CountSelectedSymbols
        'Dim Favs As Settings.SettingsDataSet.FavSymbolsDataTable = FavSymbols()
        'Dim FavSymbols As Settings.SettingsDataSet.FavSymbolsDataTable = FavSymbols()

        Dim savedSymbols As Integer
        If selectedSymbols > 0 Then
            Dim favoriteName As String '= String.Empty
            favoriteName = InputBox("Please enter name for this new favorite entry.", "Add Favorite")
            If Not favoriteName = String.Empty Then


                If Not ExistsFavoriteName(favoriteName) Then
                    If Not FavSymbols.Rows.Count = 0 Then
                        Dim img = FavSign.Render
                        savedSymbols = InsertFavorite(favoriteName, FavSymbols, img)
                        img.Dispose()
                        TVFavoriteSymbols.Nodes.Clear()
                        TvFavoriteLoad()
                    Else
                        Throw New AssertionException("Problem getting selected symbols from sign to save in Favorite.")
                    End If
                Else
                    Const mbo As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
                    MessageBox.Show("Name already exists please try another", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
                End If
            Else
                Const mbo As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
                MessageBox.Show("Favorite not added. Please enter a name for the favorite entry.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
            End If


            'Ensure Test final changes to object
#If AssertTest Then
            If Not favoriteName = String.Empty AndAlso savedSymbols <> FavSymbols.Count Then
                Throw New AssertionException("Problem saving symbols to new favorite.")
            End If
#End If
        Else
            Const mbo As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
            MessageBox.Show("No symbols selected to add to favorite. Please select symbol first.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
        End If
    End Sub

    ''' <summary>
    ''' Function FavSymbols description
    ''' </summary>
    Function FavSymbols() As Settings.SettingsDataSet.FavSymbolsDataTable
        Dim favSymbols1 As New Settings.SettingsDataSet.FavSymbolsDataTable
        Dim favSymbolsRow As Settings.SettingsDataSet.FavSymbolsRow
        Dim FavSign As New SwSign
        For Each symbol As SWSignSymbol In mySWSign.Frames(mySWSign.CurrentFrameIndex).SignSymbols
            If symbol.IsSelected Then
                favSymbolsRow = CType(favSymbols1.NewRow, Settings.SettingsDataSet.FavSymbolsRow)
                favSymbolsRow.hand = symbol.Hand
                favSymbolsRow.handcolor = symbol.Handcolor
                favSymbolsRow.palmcolor = symbol.Palmcolor
                favSymbolsRow.sym_code = symbol.Code
                favSymbolsRow.x = symbol.X
                favSymbolsRow.y = symbol.Y
                favSymbolsRow.z = symbol.Z
                favSymbolsRow.size = CSng(symbol.Size)

                favSymbols1.Rows.Add(favSymbolsRow)

                FavSign.Frames(0).SignSymbols.Add(symbol)

            End If
        Next
        Return favSymbols1
    End Function

    ''' <summary>
    ''' Function FavSymbols description
    ''' </summary>
    Function FavSign() As SwSign
        Dim favSign1 As New SwSign
        Dim symb As SWSignSymbol
        For Each symbol As SWSignSymbol In mySWSign.Frames(mySWSign.CurrentFrameIndex).SignSymbols
            If symbol.IsSelected Then
                symb = symbol.Clone
                symb.IsSelected = False
                favSign1.Frames(0).SignSymbols.Add(symb)
            End If
        Next
        Return favSign1
    End Function
    Private Sub BtnRemoveSymbol_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles BtnRemoveSymbol.Click
        RemoveFavorite()
    End Sub
    Private Sub RemoveFavorite()
        'Dim Favorites As Settings.Favorites
        Dim favoriteName = CBFavorites.Text
        If MessageBox.Show("Do you really want to remove favorite '" & favoriteName & "'?", "Remove favorite", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Dim rowsAffected As Integer = Settings.Favorites.DeleteFavorite(favoriteName)

            If Not rowsAffected = 1 Then
                MessageBox.Show("There was an error deleting Favorite Entry")
            Else
                TVFavoriteSymbols.Nodes.Clear()
                TvFavoriteLoad()
            End If
        End If
    End Sub

    Private Sub TVFavoriteSymbols_AfterSelect(ByVal sender As Object, ByVal e As TreeViewEventArgs) Handles TVFavoriteSymbols.AfterSelect
        If e.Action = TreeViewAction.ByKeyboard Then
            CBFavorites.Text = TVFavoriteSymbols.SelectedNode.Text
        ElseIf e.Action = TreeViewAction.ByMouse OrElse e.Action = TreeViewAction.Unknown Then
            CBFavorites.Text = TVFavoriteSymbols.SelectedNode.Text

        End If
    End Sub
    Private Sub AddFavSymbolstoFrame(ByVal id As Integer, ByVal pPoint As Point)
        If Not AddedFavoriteByDrop Then
            Dim ta As New Settings.SettingsDataSetTableAdapters.FavSymbolsTableAdapter
            Dim symbs As DataRowCollection

            symbs = ta.GetDataByFavId(id).Rows
            Dim frame As SWFrame = mySWSign.Frames(mySWSign.CurrentFrameIndex)
            frame.UnSelectSymbols()

            Dim dragOffset = GetDragOffset(symbs.Count(), CType(symbs.Item(0), Settings.SettingsDataSet.FavSymbolsRow), pPoint)
            If Not dragOffset.X = 0 AndAlso Not dragOffset.Y = 0 Then
                AddedFavoriteByDrop = True
            End If
            AddUndo()
            For Each row As Settings.SettingsDataSet.FavSymbolsRow In symbs
                CurrentFrame.InsertSymbolIntoSign(row.sym_code, True, row.x + dragOffset.X, row.y + dragOffset.Y, Color.FromArgb(row.handcolor), Color.FromArgb(row.palmcolor), row.hand)
            Next
            DisplaySign()
        End If
        If CurrentFrame.SelectedSymbolCount = 1 Then
            OnlyOneSymbolJustSelected()
        End If
    End Sub
    Private Sub ReplaceFirstFavSymbolstoFrame(ByVal id As Integer, ByVal pPoint As Point)
        If Not AddedFavoriteByDrop Then
            Dim ta As New Settings.SettingsDataSetTableAdapters.FavSymbolsTableAdapter
            Dim symbs As DataRowCollection

            symbs = ta.GetDataByFavId(id).Rows
            Dim firstsymbol As SWSignSymbol = New SWSignSymbol


            For Each row As Settings.SettingsDataSet.FavSymbolsRow In symbs
                firstsymbol.Code = row.sym_code
                firstsymbol.Hand = row.hand
                Exit For
            Next

            If firstsymbol IsNot Nothing Then
                ChangeChangeSymbolIn(firstsymbol, firstsymbol.Hand)
            End If


            'Dim frame As SWFrame = mySWSign.Frames(mySWSign.CurrentFrameIndex)
            'frame.UnSelectSymbols()

            'Dim dragOffset = GetDragOffset(symbs.Count(), CType(symbs.Item(0), Settings.SettingsDataSet.FavSymbolsRow), pPoint)
            'If Not dragOffset.X = 0 AndAlso Not dragOffset.Y = 0 Then
            '    AddedFavoriteByDrop = True
            'End If
            'AddUndo()
            'For Each row As Settings.SettingsDataSet.FavSymbolsRow In symbs
            '    CurrentFrame.InsertSymbolIntoSign(row.sym_code, True, row.x + dragOffset.X, row.y + dragOffset.Y, Color.FromArgb(row.handcolor), Color.FromArgb(row.palmcolor), row.hand)
            'Next
            'DisplaySign()
        End If
        If CurrentFrame.SelectedSymbolCount = 1 Then
            OnlyOneSymbolJustSelected()
        End If
    End Sub

    Property AddedFavoriteByDrop() As Boolean
     

    Private Shared Function GetDragOffset(ByVal count As Integer, ByVal symbol As Settings.SettingsDataSet.FavSymbolsRow, ByVal pPoint As Point) As Point
        Dim point = New Point(0, 0)
        If count = 1 Then
            If symbol IsNot Nothing AndAlso symbol.x = 0 AndAlso symbol.y = 0 Then
                Return pPoint
            End If
        End If

        Return point
    End Function

    Private Sub TVFavoriteSymbols_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles TVFavoriteSymbols.GotFocus
        Area = AreaEnm.Favorites
    End Sub

    Private Sub TVFavoriteSymbols_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles TVFavoriteSymbols.MouseDown
        TVFavoriteSymbols.SelectedNode = TVFavoriteSymbols.GetNodeAt(e.X, e.Y)
        Dim currentTreeNode As TreeNode = TVFavoriteSymbols.SelectedNode
        AddedFavoriteByDrop = False

        If currentTreeNode IsNot Nothing Then
            TVFavoriteSymbols.DoDragDrop(TVFavoriteSymbols.Name, DragDropEffects.Copy)
        End If
      
    End Sub

    Private Sub TVFavoriteSymbols_MouseClick(sender As Object, e As MouseEventArgs) Handles TVFavoriteSymbols.MouseClick
        If TVFavoriteSymbols.SelectedNode IsNot Nothing Then
            Dim selectednode = TVFavoriteSymbols.SelectedNode
            Dim mysender = DirectCast(sender, TreeView)

            Dim node = mysender.GetNodeAt(e.Location)

            If selectednode.Equals(node) Then

                If e.Button = System.Windows.Forms.MouseButtons.Left Then
                    AddSelectedFavorite()
                ElseIf e.Button = System.Windows.Forms.MouseButtons.Right Then
                    ReplaceSelectedFavorite()
                End If
            End If
        End If
    End Sub

    Private Sub TVFavoriteSymbols_MouseEnter(ByVal sender As Object, ByVal e As EventArgs) Handles TVFavoriteSymbols.MouseEnter
        Area = AreaEnm.Favorites
    End Sub

    Private Sub AddSelectedFavorite(Optional ByVal pPoint As Point = Nothing)
        If CBFavorites.SelectedValue IsNot Nothing And Not AddedFavoriteByDrop Then
            Dim favId As Integer

            favId = CInt(DirectCast(DirectCast(CBFavorites.SelectedValue, System.Object), DataRowView).Row.Item("IdFavorites"))
            AddFavSymbolstoFrame(favId, pPoint)

            Area = AreaEnm.Sign
        End If
    End Sub


    Private Sub ReplaceSelectedFavorite(Optional ByVal pPoint As Point = Nothing)
        If CBFavorites.SelectedValue IsNot Nothing And Not AddedFavoriteByDrop Then
            Dim favId As Integer

            favId = CInt(DirectCast(DirectCast(CBFavorites.SelectedValue, System.Object), DataRowView).Row.Item("IdFavorites"))
            ReplaceFirstFavSymbolstoFrame(favId, pPoint)

            Area = AreaEnm.Sign
        End If
    End Sub
#End Region


End Class
