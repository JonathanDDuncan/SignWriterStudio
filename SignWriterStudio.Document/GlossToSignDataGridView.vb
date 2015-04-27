Public Class GlossToSignControl
    Private Sub GlossToSignDataGridView_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.AutoSize = True
        GlossToSignDataGridView.AutoSize = True
        GlossToSignDataGridView.Sort(GlossToSignDataGridView.Columns("gloss1"), System.ComponentModel.ListSortDirection.Ascending)
    End Sub
    Private Sub GlossToSignDataGridView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GlossToSignDataGridView.CellContentClick
        If Not (e.RowIndex = -1) And Not (e.ColumnIndex = -1) Then
            If sender.CurrentCell.OwningColumn.DataPropertyName = "Selected" Then
                For Each row As DataGridViewRow In GlossToSignDataGridView.Rows
                    row.Cells("Selected").Value = False
                Next
            End If
        End If
    End Sub

    Public WriteOnly Property SetBackColor() As Color
        Set(ByVal value As Color)
            GlossToSignDataGridView.BackgroundColor = value
            GlossToSignDataGridView.BackColor = value
        End Set
    End Property
End Class
