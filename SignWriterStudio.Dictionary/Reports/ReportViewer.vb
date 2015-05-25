Public Class ReportViewer
    Private _dataTable1 As Object

    Private Sub ReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ReportViewer1.RefreshReport()
    End Sub

    Public Property DataTable() As Object
        Get
            Return _dataTable1
        End Get
        Set(value As Object)
            _dataTable1 = value
            DictionaryDataGridTableBindingSource.DataSource = value
            DictionaryDataGridTableBindingSource.ResetBindings(True)
        End Set
    End Property
End Class