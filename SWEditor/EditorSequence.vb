
Imports System.Drawing
Imports SignWriterStudio.SWClasses
Imports System.Windows.Forms

Partial Public Class Editor
#Region "Sequence"
    Private Sub TPSequence_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TPSequence.MouseEnter
        If Not Me.Area = AreaEnm.Sequence Then
            Me.Area = AreaEnm.Sequence
        End If
        
    End Sub
    Private SequenceRightClickedNode As TreeNode
    Private Sub TVSequence_NodeMouseClick(sender As Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TVSequence.NodeMouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then

            SequenceRightClickedNode = e.Node
        End If
    End Sub
    Private Sub Sequence_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        'Dim ControlActive As Object = ActiveControl
        'Dim ChangeSymbolIn As Boolean = CurrentFrame.SelectedSymbolCount > 0

        Select Case e.KeyCode
            Case Keys.F1
                Help.ShowHelp(Me, "SignWriterStudio.chm", "symbolsequence.htm")
            Case Keys.Delete
                If e.Control Then
                    DeleteAllSequences()
                Else
                    DeleteSelectedSequence(TVSequence.SelectedNode)
                End If
            Case Keys.Insert
                If e.Control Then
                    AddFromSign()
                Else
                    AddFromChooser()
                End If
            Case Keys.Up
                If e.Control Then
                    SequenceUp(TVSequence.SelectedNode)
                End If
            Case Keys.Down
                If e.Control Then
                    SequenceDown(TVSequence.SelectedNode)
                End If
        End Select

    End Sub
    Private Sub DeleteAllSequences()
        CurrentFrame.Sequences.Clear()
        LoadSequence()
    End Sub
   
    Private Sub DeleteSelectedSequence(node As TreeNode)
        If node IsNot Nothing Then
            CurrentFrame.CompareSWSequence = New SWSequence(CInt(node.Name), 0)
            Dim Sequence As SWSequence = CurrentFrame.Sequences.Find(AddressOf CurrentFrame.FindSequence)
            If Sequence IsNot Nothing Then
                CurrentFrame.Sequences.Remove(Sequence)
            End If
        End If
        LoadSequence()
    End Sub

    Private Sub BtnAddSign_Click(sender As System.Object, e As System.EventArgs) Handles BtnAddSign.Click
        AddFromSign()
    End Sub

    Private Sub BtnAddChooser_Click(sender As System.Object, e As System.EventArgs) Handles BtnAddChooser.Click
        AddFromChooser()
    End Sub
    Private Sub AddFromSign()
        CurrentFrame.AddSequenceSelectedItems()
        LoadSequence()
    End Sub
    Private Sub AddFromChooser()
        CurrentFrame.AddSequenceItem(Me.symbolOut.SymbolDetails)
        LoadSequence()
    End Sub

    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        DeleteSelectedSequence(TVSequence.SelectedNode)
    End Sub
 
    Private Sub BtnDeleteAll_Click(sender As System.Object, e As System.EventArgs) Handles BtnDeleteAll.Click
        DeleteAllSequences()
    End Sub
    Private Sub BtnUp_Click(sender As System.Object, e As System.EventArgs) Handles BtnUp.Click
        SequenceUp(TVSequence.SelectedNode)
    End Sub

    Private Sub BtnDown_Click(sender As System.Object, e As System.EventArgs) Handles BtnDown.Click
        SequenceDown(TVSequence.SelectedNode)
    End Sub
    Private Sub SequenceUp(node As TreeNode)
        If node IsNot Nothing Then
            'Dim CurrentNodeName As String = TVSequence.SelectedNode.Name

            CurrentFrame.MoveSequenceUp(node.Index)
            LoadSequence()
            If Not node.Index = 0 Then
                TVSequence.SelectedNode = TVSequence.Nodes(node.Index - 1)
            Else
                TVSequence.SelectedNode = TVSequence.Nodes(0)
            End If
        End If
    End Sub
    Private Sub SequenceDown(node As TreeNode)
        If node IsNot Nothing Then
            'Dim CurrentNodeName As String = TVSequence.SelectedNode.Name

            CurrentFrame.MoveSequenceDown(node.Index)
            LoadSequence()
            If Not node.Index = TVSequence.Nodes.Count - 1 Then
                TVSequence.SelectedNode = TVSequence.Nodes(node.Index + 1)
            Else
                TVSequence.SelectedNode = TVSequence.Nodes(node.Index)
            End If
        End If
    End Sub
    Private Sub LoadSequence()
        SWFrame.LoadSequence(TVSequence, CurrentFrame, SequenceMenuStrip)
    End Sub

    Private Sub UpToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles UpToolStripMenuItem.Click
        SequenceUp(SequenceRightClickedNode)
    End Sub

    Private Sub DownToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DownToolStripMenuItem.Click
        SequenceDown(SequenceRightClickedNode)
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        DeleteSelectedSequence(SequenceRightClickedNode)
    End Sub

    Private Sub TVSequence_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TVSequence.DragEnter
        'Dim SendingControl As Control = CType(sender, Control)

        e.Effect = DragDropEffects.Copy Or DragDropEffects.Move

    End Sub

    Private Sub TVSequence_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TVSequence.DragDrop
        Dim temp = e.Data.GetData(GetType(System.String))
        If temp IsNot Nothing AndAlso temp.ToString = PBsymbolOut.Name Then
            CurrentFrame.AddSequenceItem(Me.symbolOut.SymbolDetails)
            LoadSequence()
            Area = AreaEnm.Sequence
        End If
    End Sub
#End Region

    'Private Sub DisplayNodeInfo(node As TreeNode)
    '    Static I As Integer
    '    I += 1
    '    If node IsNot Nothing Then
    '        Me.Label1.Text = node.Index.ToString & " " & I
    '    Else
    '        Me.Label1.Text = "Nothing" & " " & I
    '    End If

    'End Sub


End Class
