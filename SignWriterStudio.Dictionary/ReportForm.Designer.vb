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
        Me.TagFilter1 = New TagFilter()
        Me.CBReports = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TitleTB = New System.Windows.Forms.TextBox()
        Me.GlossTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SignWritingTB = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.IllustrationTB = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PhotoSignTB = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnReport
        '
        Me.btnReport.Location = New System.Drawing.Point(285, 279)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(75, 23)
        Me.btnReport.TabIndex = 15
        Me.btnReport.Text = "Show"
        Me.btnReport.UseVisualStyleBackColor = True
        '
        'TagFilter1
        '
        Me.TagFilter1.AssumeFiltering = False
        Me.TagFilter1.DataSource = Nothing
        Me.TagFilter1.Location = New System.Drawing.Point(35, 173)
        Me.TagFilter1.Name = "TagFilter1"
        Me.TagFilter1.Size = New System.Drawing.Size(605, 100)
        Me.TagFilter1.TabIndex = 14
        '
        'CBReports
        '
        Me.CBReports.FormattingEnabled = True
        Me.CBReports.Location = New System.Drawing.Point(77, 12)
        Me.CBReports.Name = "CBReports"
        Me.CBReports.Size = New System.Drawing.Size(563, 21)
        Me.CBReports.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Report"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Title"
        '
        'TitleTB
        '
        Me.TitleTB.Location = New System.Drawing.Point(77, 43)
        Me.TitleTB.Name = "TitleTB"
        Me.TitleTB.Size = New System.Drawing.Size(563, 20)
        Me.TitleTB.TabIndex = 5
        Me.TitleTB.Text = "SignWriting Dictionary"
        '
        'GlossTB
        '
        Me.GlossTB.Location = New System.Drawing.Point(77, 69)
        Me.GlossTB.Name = "GlossTB"
        Me.GlossTB.Size = New System.Drawing.Size(563, 20)
        Me.GlossTB.TabIndex = 7
        Me.GlossTB.Text = "Gloss"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(38, 72)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Gloss"
        '
        'SignWritingTB
        '
        Me.SignWritingTB.Location = New System.Drawing.Point(77, 95)
        Me.SignWritingTB.Name = "SignWritingTB"
        Me.SignWritingTB.Size = New System.Drawing.Size(563, 20)
        Me.SignWritingTB.TabIndex = 9
        Me.SignWritingTB.Text = "SignWriting"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(61, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "SignWriting"
        '
        'IllustrationTB
        '
        Me.IllustrationTB.Location = New System.Drawing.Point(77, 121)
        Me.IllustrationTB.Name = "IllustrationTB"
        Me.IllustrationTB.Size = New System.Drawing.Size(563, 20)
        Me.IllustrationTB.TabIndex = 11
        Me.IllustrationTB.Text = "Illustratiion"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 124)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Illustratiion"
        '
        'PhotoSignTB
        '
        Me.PhotoSignTB.Location = New System.Drawing.Point(77, 147)
        Me.PhotoSignTB.Name = "PhotoSignTB"
        Me.PhotoSignTB.Size = New System.Drawing.Size(563, 20)
        Me.PhotoSignTB.TabIndex = 13
        Me.PhotoSignTB.Text = "Photo Sign"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 150)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 13)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Photo Sign"
        '
        'ReportForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(670, 311)
        Me.Controls.Add(Me.PhotoSignTB)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.IllustrationTB)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.SignWritingTB)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.GlossTB)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TitleTB)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CBReports)
        Me.Controls.Add(Me.TagFilter1)
        Me.Controls.Add(Me.btnReport)
        Me.Name = "ReportForm"
        Me.Text = "Report"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents TagFilter1 As Global.SignWriterStudio.Dictionary.TagFilter
    Friend WithEvents CBReports As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TitleTB As System.Windows.Forms.TextBox
    Friend WithEvents GlossTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SignWritingTB As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents IllustrationTB As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PhotoSignTB As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
