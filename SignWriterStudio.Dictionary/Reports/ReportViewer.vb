Namespace Reports
    Public Class ReportViewer
        Private _dataTable1 As Object
        Private _titleTable1 As Object

        Private Sub ReportViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        End Sub

        Public Sub SetReport(ByVal reportname As String)
            ReportViewer1.LocalReport.ReportEmbeddedResource = String.Format("SignWriterStudio.Dictionary.{0}.rdlc", reportname)
            ReportViewer1.RefreshReport()
        End Sub

        Public Property DataDataSet() As Object
            Get
                Return _dataTable1
            End Get
            Set(value As Object)
                _dataTable1 = value
                DictionaryDataGridTableBindingSource.DataSource = value
                DictionaryDataGridTableBindingSource.ResetBindings(True)
            End Set
        End Property
        Public Property TitleDataSet() As Object
            Get
                Return _titleTable1
            End Get
            Set(value As Object)
                _titleTable1 = value
                ReportTitleTableBindingSource.DataSource = value
                ReportTitleTableBindingSource.ResetBindings(True)
            End Set
        End Property
    End Class
End Namespace