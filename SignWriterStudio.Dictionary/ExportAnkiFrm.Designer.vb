<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportAnkiFrm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextFilenameTb = New System.Windows.Forms.TextBox()
        Me.PNGFolderTb = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBrowseBtn = New System.Windows.Forms.Button()
        Me.PNGBrowseBtn = New System.Windows.Forms.Button()
        Me.ExportBtn = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Text Filename"
        '
        'TextFilenameTb
        '
        Me.TextFilenameTb.Location = New System.Drawing.Point(101, 29)
        Me.TextFilenameTb.Name = "TextFilenameTb"
        Me.TextFilenameTb.Size = New System.Drawing.Size(350, 20)
        Me.TextFilenameTb.TabIndex = 1
        '
        'PNGFolderTb
        '
        Me.PNGFolderTb.Location = New System.Drawing.Point(101, 58)
        Me.PNGFolderTb.Name = "PNGFolderTb"
        Me.PNGFolderTb.Size = New System.Drawing.Size(350, 20)
        Me.PNGFolderTb.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "PNG Folder"
        '
        'TextBrowseBtn
        '
        Me.TextBrowseBtn.Location = New System.Drawing.Point(457, 27)
        Me.TextBrowseBtn.Name = "TextBrowseBtn"
        Me.TextBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.TextBrowseBtn.TabIndex = 4
        Me.TextBrowseBtn.Text = "Browse"
        Me.TextBrowseBtn.UseVisualStyleBackColor = True
        '
        'PNGBrowseBtn
        '
        Me.PNGBrowseBtn.Location = New System.Drawing.Point(457, 56)
        Me.PNGBrowseBtn.Name = "PNGBrowseBtn"
        Me.PNGBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.PNGBrowseBtn.TabIndex = 5
        Me.PNGBrowseBtn.Text = "Browse"
        Me.PNGBrowseBtn.UseVisualStyleBackColor = True
        '
        'ExportBtn
        '
        Me.ExportBtn.Location = New System.Drawing.Point(245, 103)
        Me.ExportBtn.Name = "ExportBtn"
        Me.ExportBtn.Size = New System.Drawing.Size(75, 23)
        Me.ExportBtn.TabIndex = 6
        Me.ExportBtn.Text = "Export"
        Me.ExportBtn.UseVisualStyleBackColor = True
        '
        'ExportAnkiFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 138)
        Me.Controls.Add(Me.ExportBtn)
        Me.Controls.Add(Me.PNGBrowseBtn)
        Me.Controls.Add(Me.TextBrowseBtn)
        Me.Controls.Add(Me.PNGFolderTb)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextFilenameTb)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ExportAnkiFrm"
        Me.Text = "Export Anki"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextFilenameTb As System.Windows.Forms.TextBox
    Friend WithEvents PNGFolderTb As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBrowseBtn As System.Windows.Forms.Button
    Friend WithEvents PNGBrowseBtn As System.Windows.Forms.Button
    Friend WithEvents ExportBtn As System.Windows.Forms.Button
End Class
