Public Class TreeViewHelper
    
    Friend Shared Function AddNewNode(ByVal treeView As TreeView, ByVal nodeName As String, ByVal isTagGroup As Boolean) As TreeNode
        Dim nodeEdited As New TreeNode()
        nodeEdited.Name = nodeName
        Dim currentNode = treeView.SelectedNode

        If isTagGroup Then
            treeView.Nodes.Add(nodeEdited)
        ElseIf currentNode IsNot Nothing Then
            If currentNode.Parent IsNot Nothing Then 'Do not add a third level
                currentNode.Parent.Nodes.Insert(currentNode.Index + 1, nodeEdited)
            Else
                currentNode.Nodes.Add(nodeEdited)
            End If
        End If

        Return nodeEdited
    End Function

    Public Shared Function GetAllCheckedNodes(ByVal treeView As TreeView) As IEnumerable(Of TreeNode)
        Return Enumerable.Where(Of TreeNode)(GetIEnumerableAllTreeNode(treeView.Nodes), Function(x) x.Checked)
    End Function

    Public Shared Function GetIEnumerableAllTreeNode(ByVal treeNodeCollection As TreeNodeCollection) As IEnumerable(Of TreeNode)

        Dim currentNodeCollection = treeNodeCollection.Cast(Of TreeNode)().ToList()
        Dim allNodes = currentNodeCollection.ToList()
        For Each rootNode As TreeNode In currentNodeCollection
            allNodes.AddRange(currentNodeCollection)
            allNodes.AddRange(GetIEnumerableAllTreeNode(rootNode.Nodes))
        Next
        Return allNodes
    End Function

    Public Shared Sub MoveUp(ByVal treeView As TreeView, ByVal selectedNode As TreeNode)

        Dim node As TreeNode = TryCast(selectedNode.Clone(), TreeNode)
        Dim index As Integer = -1
        Dim parentNodes As Object = GetParentNodes(treeView)
        For j As Integer = 0 To parentNodes.Count - 1
            If selectedNode.Equals(parentNodes(j)) Then
                index = j
                Exit For
            End If
        Next

        treeView.BeginUpdate()
        parentNodes.Insert(index - 1, node)
        parentNodes.RemoveAt(index + 1)
        treeView.EndUpdate()

        SelectNode(treeView, node)
    End Sub

    Public Shared Sub MoveDown(ByVal treeView As TreeView, ByVal selectedNode As TreeNode)

        Dim node As TreeNode = TryCast(selectedNode.Clone(), TreeNode)
        Dim index As Integer = -1
        Dim parentNodes As Object = GetParentNodes(treeView)

        For j As Integer = 0 To parentNodes.Count - 1
            If selectedNode.Equals(parentNodes(j)) Then
                index = j
                Exit For
            End If
        Next

        treeView.BeginUpdate()
        parentNodes.Insert(index + 2, node)
        parentNodes.RemoveAt(index)
        treeView.EndUpdate()
        SelectNode(treeView, node)
    End Sub

    Public Shared Sub SelectNode(ByVal treeView As TreeView, ByVal node As TreeNode)
        treeView.SelectedNode = node
        treeView.[Select]()
    End Sub

    Private Shared Function GetParentNodes(ByVal treeView As TreeView) As Object
        Dim nodes = Nothing
        If treeView.SelectedNode.Parent IsNot Nothing Then
            nodes = treeView.SelectedNode.Parent.Nodes()
        End If

        If nodes Is Nothing Then
            nodes = treeView.Nodes
        End If
        Return nodes
    End Function

    Public Shared Sub CheckAllChildNodes(treeNode As TreeNode, nodeChecked As Boolean)
        Dim node As TreeNode
        For Each node In treeNode.Nodes
            node.Checked = nodeChecked
            If node.Nodes.Count > 0 Then
                ' If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                CheckAllChildNodes(node, nodeChecked)
            End If
        Next node
    End Sub

    Public Shared Function AreAllChecked(ByVal treeNodeCollection As TreeNodeCollection) As Boolean
        Return GetIEnumerableAllTreeNode(treeNodeCollection).All(Function(x) x.Checked = True)
    End Function

    Public Shared Sub CheckChildren(ByVal node As TreeNode, ByVal action As TreeViewAction)
        ' The code only executes if the user caused the checked state to change. 
        If action <> TreeViewAction.Unknown Then
            If node.Nodes.Count > 0 Then
                CheckAllChildNodes(node, node.Checked)
            End If
        End If
    End Sub

    Public Shared Sub CheckParent(ByVal treeNode As TreeNode, ByVal action As TreeViewAction)

        Dim parent = treeNode.Parent
        ' The code only executes if the user caused the checked state to change. 
        If action <> TreeViewAction.Unknown Then
            If parent IsNot Nothing Then
                If AreAllChecked(parent.Nodes) Then
                    parent.Checked = True
                Else
                    parent.Checked = False
                End If
            End If
        End If
    End Sub

    Public Shared Function FindNode(ByVal treeView As TreeView, ByVal nodeName As String) As TreeNode
        Return treeView.Nodes.Find(nodeName, True).FirstOrDefault()
    End Function
End Class