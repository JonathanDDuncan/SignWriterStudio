Imports System.Dynamic

Public Class TagTreeView
    Private _nodeEdited As TreeNode
    Private _afterCheck As Boolean = False
    Private _selectedNode As TreeNode
    Private ReadOnly _removed As List(Of TagData) = New List(Of TagData)
    Private ReadOnly _added As List(Of TagData) = New List(Of TagData)
    Private ReadOnly _updated As List(Of TagData) = New List(Of TagData)
    Private _updateAll As Boolean

    Public Property CheckBoxes() As Boolean
        Get
            Return tvTags.CheckBoxes
        End Get
        Set(ByVal value As Boolean)
            tvTags.CheckBoxes = value
        End Set
    End Property

    Public Property NodeEdited() As TreeNode
        Set(value As TreeNode)
            _nodeEdited = value
        End Set
        Get
            Return _nodeEdited
        End Get
    End Property

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        EditVisibility(True)
        Dim firstNode = tvTags.SelectedNode()
        _nodeEdited = firstNode
        If firstNode IsNot Nothing Then
            Dim data = CType(firstNode.Tag, TagData)
            tvTags.Enabled = False
            tbDescription.Text = data.Description
            tbAbreviation.Text = data.Abbreviation
            btnColor.BackColor = data.Color
        End If
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        EditVisibility(True)
        _nodeEdited = Nothing
        tvTags.Enabled = False
        cbTagGroup.Visible = True
        tbDescription.Text = ""
        tbAbreviation.Text = ""
    End Sub
    Private Sub ButtonsVisibility(show As Boolean)
        btnAdd.Visible = show
        btnCancel.Visible = show
        btnEdit.Visible = show
        btnRemove.Visible = show
        btnSave.Visible = show
        btnRemove.Visible = show
    End Sub

    Private Sub btnEditOk_Click(sender As Object, e As EventArgs) Handles btnEditOk.Click
        EditVisibility(False)
        Dim tagParent = GetNodeParent(_nodeEdited, cbTagGroup.Checked AndAlso _nodeEdited Is Nothing)

        Dim nodeName
        Dim checked = False
        Dim wasAdded = False
        If (_nodeEdited IsNot Nothing) Then
            nodeName = _nodeEdited.Name
            If String.IsNullOrEmpty(nodeName) Then nodeName = Guid.NewGuid.ToString()
            checked = _nodeEdited.Checked
        Else
            wasAdded = True
            nodeName = Guid.NewGuid.ToString()

            _nodeEdited = TreeViewHelper.AddNewNode(tvTags, nodeName, cbTagGroup.Checked)
            If (_nodeEdited.Parent IsNot Nothing) Then tagParent = _nodeEdited.Parent.Name
        End If

        Dim tagData = GetTagData(nodeName, checked, tbDescription.Text, tbAbreviation.Text, btnColor.BackColor, tagParent)

        UpdateNode(tagData)

        If (wasAdded) Then
            _added.Add(tagData)
        Else
            _updated.Add(tagData)
        End If

        tvTags.Enabled = True
        cbTagGroup.Checked = False
        cbTagGroup.Visible = False
    End Sub

    Private Sub UpdateNode(ByVal tagData As TagData)
        _nodeEdited.Text = tagData.Description
        _nodeEdited.Tag = tagData
        _nodeEdited.BackColor = tagData.Color
    End Sub

    Private Shared Function GetNodeParent(ByVal treeNode As TreeNode, ByVal isGroup As Boolean) As String
        If isGroup Then
            Return String.Empty
        ElseIf (treeNode IsNot Nothing AndAlso treeNode.Parent IsNot Nothing) Then
            Return treeNode.Parent.Name
        End If
        Return String.Empty
    End Function

    Private Shared Function GetTagData(ByVal nodeName As String, ByVal checked As Boolean, ByVal description As String, ByVal abreviation As String, ByVal color As Color, ByVal tagParent As String) As TagData
        Return New TagData With {.Name = nodeName, .Description = description, .Abbreviation = abreviation, .Color = color, .Print = checked, .Parent = tagParent}
    End Function

    Private Sub EditVisibility(show As Boolean)
        gbEdit.Visible = show
        ButtonsVisibility(Not show)
    End Sub

    Private Sub btnEditCancel_Click(sender As Object, e As EventArgs) Handles btnEditCancel.Click
        EditVisibility(False)
        tvTags.Enabled = True
        cbTagGroup.Checked = False
        cbTagGroup.Visible = False
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        CheckBoxes = True
    End Sub

    Private Sub LoadNodes(ByVal tagDatas As List(Of TagData))
        'TagGroups
        For Each tagData As TagData In tagDatas.Where(Function(x) String.IsNullOrEmpty(x.Parent))
            AddTagData(tagData)
        Next

        'Tags
        For Each tagData As TagData In tagDatas.Where(Function(x) Not String.IsNullOrEmpty(x.Parent))
            AddTagData(tagData)
        Next
    End Sub

    Private Sub AddTagData(ByVal tagData As TagData)
        Dim treeNode As TreeNode = CreateTreeNode(tagData)
        If (String.IsNullOrEmpty(tagData.Parent)) Then
            tvTags.Nodes.Add(treeNode)
        Else
            Dim parentNode = tvTags.Nodes.Find(tagData.Parent, True)
            If parentNode IsNot Nothing Then
                parentNode.FirstOrDefault().Nodes.Add(treeNode)
            End If
        End If

    End Sub

    Private Shared Function CreateTreeNode(ByVal tagData As TagData) As TreeNode

        Dim treeNode = New TreeNode()
        treeNode.Name = tagData.Name
        treeNode.Text = tagData.Description
        treeNode.Checked = tagData.Print
        treeNode.BackColor = tagData.Color
        treeNode.Tag = tagData
        Return treeNode
    End Function

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If MessageBox.Show("Do you really want to remove tag " & tvTags.SelectedNode.Text + "?", "Remove Tag", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            tvTags.Nodes.Remove(tvTags.SelectedNode)
            _removed.Add(TryCast(tvTags.SelectedNode.Tag, TagData))
        End If
    End Sub

    Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
        TreeViewHelper.MoveUp(tvTags, tvTags.SelectedNode)
        _updateAll = True
    End Sub

    Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
        TreeViewHelper.MoveDown(tvTags, tvTags.SelectedNode)
    End Sub

    Private Sub node_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles tvTags.AfterCheck
        If Not _afterCheck Then 'Keep from reentering when setting other node check values below
            _selectedNode = tvTags.SelectedNode
            _afterCheck = True
            TreeViewHelper.CheckChildren(e.Node, e.Action)
            TreeViewHelper.CheckParent(e.Node, e.Action)
            TreeViewHelper.SelectNode(tvTags, _selectedNode)
            _afterCheck = False
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        RaiseEvent SaveEvent(Me, New EventArgs())
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        RaiseEvent CancelEvent(Me, New EventArgs())
    End Sub

    Public Event CancelEvent As EventHandler(Of EventArgs)
    Public Event SaveEvent As EventHandler(Of EventArgs)

    Private Sub btnColor_Click(sender As Object, e As EventArgs) Handles btnColor.Click
        ColorDialog1.Color = btnColor.BackColor
        Dim result = ColorDialog1.ShowDialog()

        If result = DialogResult.OK Then btnColor.BackColor = ColorDialog1.Color

    End Sub

    Public Sub SetFromDb(ByVal tags As List(Of ExpandoObject))
        Dim nodes = (From expandoObject In tags Select row = TryCast(expandoObject, IDictionary(Of [String], Object)) Select New TagData With {.Name = row.Item("IdTag").ToString(), .Description = row.Item("Description"), .Abbreviation = row.Item("Abbreviation"), .Color = ConvertColor(row.Item("Color")), .Parent = row.Item("Parent").ToString()}).ToList()

        LoadNodes(nodes)
    End Sub
    Public Function GetChanges() As TagChanges

        Dim changes = New TagChanges()
        changes.Removed = GetRemoved()
        changes.Added = GetAdded()
        changes.Updated = GetUpdated()

        Return changes
    End Function

    Private Function GetUpdated() As List(Of ExpandoObject)
        Dim updatedTagData As List(Of TagData)

        If (_updateAll) Then
            updatedTagData = GetTagData(TreeViewHelper.GetIEnumerableAllTreeNode(tvTags.Nodes))
        Else
            updatedTagData = _updated
        End If

        Return (From tagData In updatedTagData Select GetExpandoObject(tagData)).ToList()

    End Function

    Private Function GetExpandoObject(ByVal tagData As TagData) As ExpandoObject
        Dim expandoObject = New ExpandoObject()
        Dim row = TryCast(expandoObject, IDictionary(Of [String], Object))
        row.Add("IdTag", tagData.Name)
        row.Add("Description", tagData.Description)
        row.Add("Abbreviation", tagData.Abbreviation)
        row.Add("Color", tagData.Color.ToArgb())
        row.Add("Parent", tagData.Parent)
        row.Add("Rank", GetRank(tagData.Name))
        Return expandoObject
    End Function

    Private Function GetRank(ByVal nodeName As String) As Integer
        Dim node = TreeViewHelper.FindNode(tvTags, nodeName)
        If node IsNot Nothing Then
            Return node.Index
        Else
            Return Integer.MaxValue
        End If
    End Function

    Private Function GetTagData(nodes As List(Of TreeNode)) As List(Of TagData)
        Return (From treeNode In nodes Select TryCast(treeNode.Tag, TagData)).ToList()
    End Function

    Private Function GetAdded() As List(Of ExpandoObject)
        Return (From tagData In _added Select GetExpandoObject(tagData)).ToList()
    End Function

    Private Function GetRemoved() As List(Of String)
        Return (From tagData In _removed Select tagData.Name).ToList()
    End Function

    Private Shared Function ConvertColor(intColor As Integer) As Color
        Return Color.FromArgb(intColor)
    End Function

End Class

Public Class TagChanges
    Public Property Removed() As List(Of String)
    Public Property Added() As List(Of ExpandoObject)
    Public Property Updated() As List(Of ExpandoObject)
End Class