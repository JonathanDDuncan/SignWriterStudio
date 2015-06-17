<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReportForm
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
        Me.btnReport = New System.Windows.Forms.Button()
        Me.TagFilter1 = New Global.SignWriterStudio.Dictionary.TagFilter()
        Me.SuspendLayout()
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(290, 246)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(75, 23)
        Me.btnReport.TabIndex = 0
        Me.btnReport.Text = "Show"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'TagFilter1
        '
        Me.TagFilter1.DataSource = Nothing
        Me.TagFilter1.Location = New System.Drawing.Point(149, 83)
        Me.TagFilter1.Name = "TagFilter1"
        Me.TagFilter1.Size = New System.Drawing.Size(421, 100)
        Me.TagFilter1.TabIndex = 1
        '
        'ReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(671, 298)
        Me.Controls.Add(Me.TagFilter1)
        Me.Controls.Add(Me.btnReport)
        Me.Name = "ReportForm"
        Me.Text = "Report"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents TagFilter1 As Global.SignWriterStudio.Dictionary.TagFilter
End Class
