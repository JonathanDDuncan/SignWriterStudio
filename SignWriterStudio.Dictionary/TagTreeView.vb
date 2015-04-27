Public Class TagTreeView
    Private _nodeEdited As TreeNode
    Private _afterCheck As Boolean = False
    Private _selectedNode As TreeNode
    Private ReadOnly _removed As List(Of TagData) = New List(Of TagData)
    Private ReadOnly _added As List(Of TagData) = New List(Of TagData)
    Private ReadOnly _updated As List(Of TagData) = New List(Of TagData)

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
            tbAbreviation.Text = data.Abreviation
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
        Dim tagData = GetTagData(tbDescription.Text, tbAbreviation.Text, btnColor.BackColor, tagParent)
        Dim wasAdded = False
        If _nodeEdited Is Nothing Then
            wasAdded = True
            _nodeEdited = TreeViewHelper.AddNewNode(tvTags, String.IsNullOrEmpty(tagData.Parent))

        End If

        UpdateNode(tagData)

        If (wasAdded) Then
            _added.Add(TryCast(tvTags.SelectedNode.Tag, TagData))
        Else
            _updated.Add(TryCast(tvTags.SelectedNode.Tag, TagData))
        End If

        'TODO add node data to updated when check changed

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

    Private Shared Function GetTagData(ByVal description As String, ByVal abreviation As String, ByVal color As Color, ByVal tagParent As String) As TagData
        Return New TagData With {.Description = description, .Abreviation = abreviation, .Color = color, .Parent = tagParent}
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
        CheckBoxes = True

        ' Add any initialization after the InitializeComponent() call.
        Dim nodes = New List(Of TagData)()
        nodes.Add(New TagData With {.Name = "90596132-d14d-4280-a4d1-26996fbb2a47", .Print = True, .Description = "Dictionaries", .Color = Color.Turquoise})
        nodes.Add(New TagData With {.Name = "cf1072d4-8d10-4270-9d86-cef814c7c3cc", .Description = "First Dictionary", .Parent = "90596132-d14d-4280-a4d1-26996fbb2a47", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "cca39db9-3ec2-41ab-adea-b8f59cbfb5bf", .Description = "Second Dictionary", .Parent = "90596132-d14d-4280-a4d1-26996fbb2a47", .Color = Color.PaleVioletRed})
        nodes.Add(New TagData With {.Name = "9a378bec-23ea-4116-92d8-7f13c490bb67", .Description = "Third Dictionary", .Parent = "90596132-d14d-4280-a4d1-26996fbb2a47", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "af29df83-df0a-4e59-a655-89b4947e4ddd", .Description = "Fourth Dictionary", .Parent = "90596132-d14d-4280-a4d1-26996fbb2a47", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "4b92f84d-49e3-43ea-8743-bc3b8be8a394", .Description = "Grammar"})
        nodes.Add(New TagData With {.Name = "cf1072d4-8d10-4270-9d86-cef814c7c3cc", .Description = "First Grammar", .Parent = "4b92f84d-49e3-43ea-8743-bc3b8be8a394", .Color = Color.Yellow})
        nodes.Add(New TagData With {.Name = "4b7efec6-88f1-4932-9033-b913f4929dbf", .Description = "Second Grammar", .Parent = "4b92f84d-49e3-43ea-8743-bc3b8be8a394", .Color = Color.YellowGreen})
        nodes.Add(New TagData With {.Name = "53ca79b4-639f-401c-a56c-854f1d13e587", .Description = "Third Grammar", .Parent = "4b92f84d-49e3-43ea-8743-bc3b8be8a394", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "e4131370-76eb-4629-9907-1927462d31c3", .Description = "Fourth Grammar", .Parent = "4b92f84d-49e3-43ea-8743-bc3b8be8a394", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "1531ea70-f7eb-4f83-96e0-9e6a1799542e", .Description = "Misc"})
        nodes.Add(New TagData With {.Name = "b82f7297-90ef-406c-a86c-7744efa512c4", .Description = "First Misc", .Parent = "1531ea70-f7eb-4f83-96e0-9e6a1799542e", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "97c163d2-6305-4ae3-a08b-fc8b967ac28e", .Description = "Second Misc", .Parent = "1531ea70-f7eb-4f83-96e0-9e6a1799542e", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "26d6a5cb-3dd4-42bb-baf8-929a2b93ae07", .Description = "Third Misc", .Parent = "1531ea70-f7eb-4f83-96e0-9e6a1799542e", .Color = Color.Blue})
        nodes.Add(New TagData With {.Name = "5b4cacdb-df8a-4430-9e70-5020d1ebf5b8", .Description = "Fourth Misc", .Parent = "1531ea70-f7eb-4f83-96e0-9e6a1799542e", .Color = Color.Blue})

        LoadNodes(nodes)
    End Sub

    Private Sub LoadNodes(ByVal tagDatas As List(Of TagData))
        For Each tagData As TagData In tagDatas
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

    End Sub

    Private Sub btnColor_Click(sender As Object, e As EventArgs) Handles btnColor.Click
        ColorDialog1.Color = btnColor.BackColor
        Dim result = ColorDialog1.ShowDialog()

        If result = DialogResult.OK Then btnColor.BackColor = ColorDialog1.Color

    End Sub
End Class