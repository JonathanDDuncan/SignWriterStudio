Imports System.Windows.Forms
Imports System.Drawing

Public Class ShowSymbol
    Public Question As String
    Public Symbol As Image
    Shared Function ShowDialogPersonalized(item As String, symbol As SWClasses.SWSignSymbol) As DialogResult
        Dim question = "Is this symbol " & item & "?"

        Dim dialog = New ShowSymbol With {.Question = question, .Symbol = symbol.SignSymbol}
        dialog.ShowDialog()

        Return dialog.DialogResult
    End Function

    Private Sub ShowSymbol_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label1.Text = Question
        PictureBox1.Image = Symbol
    End Sub

    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        DialogResult = DialogResult.Yes
        Hide()
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        DialogResult = DialogResult.No
        Hide()
    End Sub

Private Sub btnCancel_Click( sender As Object,  e As EventArgs) Handles btnCancel.Click
         DialogResult = DialogResult.Cancel
        Hide()
End Sub
End Class