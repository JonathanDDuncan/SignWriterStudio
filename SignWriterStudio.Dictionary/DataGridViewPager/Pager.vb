Imports NUnit.Framework

Namespace DataGridViewPager
    Public Class Pager
        Public Event PageChanged As EventHandler(Of EventArgs)

        Private _totalRowCount1 As Integer
        Public Property TotalPages() As Integer
        Private _isChanging As Boolean = False
        Public Property PageSize() As Integer

        Public Property CurrentPage() As Integer
            Get
                Return BindingNavigatorPositionItem.Text
            End Get
            Set(value As Integer)
                BindingNavigatorPositionItem.Text = value
            End Set
        End Property

        Public Property TotalRowCount() As Integer
            Get
                Return _totalRowCount1
            End Get
            Set(value As Integer)
                _totalRowCount1 = value
                CreatePages()
            End Set
        End Property

        Public Property Search() As String


        Private Sub CreatePages()
            Dim div As Integer = TotalRowCount \ PageSize
            Dim remain = TotalRowCount Mod PageSize

            TotalPages = div + If(remain > 0, 1, 0)
            PagerBindingSource.DataSource = CreatePageList(totalPages)
        End Sub




        Private Shared Function CreatePageList(ByVal totalPages As Double) As List(Of Object)
            Dim list = New List(Of Object)
            For i = 1 To totalPages
                list.Add(New Object())
            Next
            Return list
        End Function

        'Private Sub PageChanging(sender As Object, e As EventArgs) Handles BindingNavigatorMoveNextItem.Click, BindingNavigatorMoveFirstItem.Click, BindingNavigatorMovePreviousItem.Click, BindingNavigatorMoveLastItem.Click
        Private Sub PageChanging(sender As Object, e As EventArgs) Handles BindingNavigatorPositionItem.TextChanged
            If Not _isChanging Then
                _isChanging = True
                RaiseEvent PageChanged(Me, New EventArgs())
                _isChanging = False
            End If
        End Sub



        Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
            PageSize = 20
            CreatePages()
            CheckLessThanPages()
        End Sub

        Private Sub CheckLessThanPages()
            If CurrentPage > TotalPages Then
                CurrentPage = TotalPages
            End If
        End Sub

        Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
            PageSize = 50
            CreatePages()
        End Sub

        Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
            PageSize = 100
            CreatePages()
        End Sub

        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            PageSize = 10
        End Sub

        Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
            PageSize = 10
            CreatePages()
        End Sub
    End Class
End Namespace