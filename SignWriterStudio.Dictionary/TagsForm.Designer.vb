<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TagsForm
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
        Me.TagTreeView1 = New Dictionary.TagTreeView()
        Me.SuspendLayout()
        '
        'TagTreeView1
        '
        Me.TagTreeView1.CheckBoxes = True
        Me.TagTreeView1.Location = New System.Drawing.Point(3, 2)
        Me.TagTreeView1.Name = "TagTreeView1"
        Me.TagTreeView1.NodeEdited = Nothing
        Me.TagTreeView1.Size = New System.Drawing.Size(363, 379)
        Me.TagTreeView1.TabIndex = 0
        '
        'TagsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(377, 382)
        Me.Controls.Add(Me.TagTreeView1)
        Me.Name = "TagsForm"
        Me.Text = "Tags"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TagTreeView1 As Dictionary.TagTreeView
End Class
