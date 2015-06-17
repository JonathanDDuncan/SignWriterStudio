<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TagFilter
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TagFilter))
        Me.TagListControl1 = New TagList.Controls.TagListControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CBAllBut = New System.Windows.Forms.CheckBox()
        Me.CBFilter = New System.Windows.Forms.CheckBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TagListControl1
        '
        Me.TagListControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TagListControl1.LabelFont = Nothing
        Me.TagListControl1.Location = New System.Drawing.Point(0, 0)
        Me.TagListControl1.Name = "TagListControl1"
        Me.TagListControl1.Size = New System.Drawing.Size(232, 53)
        Me.TagListControl1.TabIndex = 0
        Me.TagListControl1.TagValues = CType(resources.GetObject("TagListControl1.TagValues"), System.Collections.Generic.List(Of String))
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.CBAllBut)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CBFilter)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TagListControl1)
        Me.SplitContainer1.Size = New System.Drawing.Size(232, 82)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.TabIndex = 1
        '
        'CBAllBut
        '
        Me.CBAllBut.AutoSize = True
        Me.CBAllBut.Location = New System.Drawing.Point(107, 3)
        Me.CBAllBut.Name = "CBAllBut"
        Me.CBAllBut.Size = New System.Drawing.Size(101, 17)
        Me.CBAllBut.TabIndex = 1
        Me.CBAllBut.Text = "All except these"
        Me.CBAllBut.UseVisualStyleBackColor = True
        '
        'CBFilter
        '
        Me.CBFilter.AutoSize = True
        Me.CBFilter.Location = New System.Drawing.Point(12, 3)
        Me.CBFilter.Name = "CBFilter"
        Me.CBFilter.Size = New System.Drawing.Size(89, 17)
        Me.CBFilter.TabIndex = 0
        Me.CBFilter.Text = "Filter by Tags"
        Me.CBFilter.UseVisualStyleBackColor = True
        '
        'TagFilter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "TagFilter"
        Me.Size = New System.Drawing.Size(232, 82)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TagListControl1 As TagList.Controls.TagListControl
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents CBAllBut As System.Windows.Forms.CheckBox
    Friend WithEvents CBFilter As System.Windows.Forms.CheckBox

End Class
