Public Class TagFilter
    Private _dataSource1 As Object

    Event ValueChanged(sender As Object, args As EventArgs)

    Property DataSource As Object
        Get
            Return _dataSource1
        End Get
        Set(value As Object)
            _dataSource1 = value
            TagListControl1.TagValues = value
        End Set
    End Property

    Private Sub TagFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FilterEnable()
    End Sub

    Private Sub FilterEnable()

        If CBFilter.Checked Then
            CBAllBut.Enabled = True
            TagListControl1.Enabled = True
        Else
            CBAllBut.Enabled = False
            TagListControl1.Enabled = False
        End If
    End Sub

    Private Sub CBFilter_CheckedChanged(sender As Object, e As EventArgs) Handles CBFilter.CheckedChanged
        FilterEnable()
    End Sub

    Private Sub TagListControl1_ValueChanged(sender As Object, args As EventArgs) Handles TagListControl1.ValueChanged, CBFilter.CheckedChanged, CBAllBut.CheckedChanged
        RaiseEvent ValueChanged(sender, args)
    End Sub
End Class
