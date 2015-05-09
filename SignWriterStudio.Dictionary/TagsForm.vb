Imports System.Dynamic

Public Class TagsForm
    Public Sub SetFromDb(ByVal tags As List(Of ExpandoObject))
        _TagTreeView1.SetFromDb(tags)
    End Sub

    Public Function GetChanges() As TagChanges
        Return _TagTreeView1.GetChanges()
    End Function

    Private Sub Save(ByVal sender As Object, ByVal e As EventArgs) Handles TagTreeView1.SaveEvent
        DialogResult = DialogResult.OK
        Hide()
    End Sub
    Private Sub Cancel(ByVal sender As Object, ByVal e As EventArgs) Handles TagTreeView1.CancelEvent
        Hide()
    End Sub
End Class