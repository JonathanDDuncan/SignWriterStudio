﻿Namespace Reports
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class ReportViewer
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim ReportDataSource1 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Dim ReportDataSource2 As Microsoft.Reporting.WinForms.ReportDataSource = New Microsoft.Reporting.WinForms.ReportDataSource()
            Me.DictionaryDataGridTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.ReportTitleTableBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.ReportViewer1 = New Microsoft.Reporting.WinForms.ReportViewer()
            CType(Me.DictionaryDataGridTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ReportTitleTableBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'DictionaryDataGridTableBindingSource
            '
            Me.DictionaryDataGridTableBindingSource.DataSource = GetType(SWClasses.DictionaryDataGridTable)
            '
            'ReportTitleTableBindingSource
            '
            Me.ReportTitleTableBindingSource.DataSource = GetType(SWClasses.ReportTitleTable)
            '
            'ReportViewer1
            '
            Me.ReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill
            ReportDataSource1.Name = "DataDataSet"
            ReportDataSource1.Value = Me.DictionaryDataGridTableBindingSource
            ReportDataSource2.Name = "TitlesDataSet"
            ReportDataSource2.Value = Me.ReportTitleTableBindingSource
            Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource1)
            Me.ReportViewer1.LocalReport.DataSources.Add(ReportDataSource2)
            Me.ReportViewer1.LocalReport.ReportEmbeddedResource = "SignWriterStudio.Dictionary.Report3.rdlc"
            Me.ReportViewer1.Location = New System.Drawing.Point(0, 0)
            Me.ReportViewer1.Name = "ReportViewer1"
            Me.ReportViewer1.Size = New System.Drawing.Size(771, 506)
            Me.ReportViewer1.TabIndex = 0
            '
            'ReportViewer
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(771, 506)
            Me.Controls.Add(Me.ReportViewer1)
            Me.Name = "ReportViewer"
            Me.Text = "ReportViewer"
            CType(Me.DictionaryDataGridTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ReportTitleTableBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ReportViewer1 As Microsoft.Reporting.WinForms.ReportViewer
        Friend WithEvents DictionaryDataGridTableBindingSource As System.Windows.Forms.BindingSource
        Friend WithEvents ReportTitleTableBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace