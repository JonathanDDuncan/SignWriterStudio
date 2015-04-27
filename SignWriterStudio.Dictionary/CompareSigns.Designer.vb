<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompareSigns
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CompareSigns))
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.OverwritefromPuddle = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.PuddleGloss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PuddleGlosses = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PuddleImage = New System.Windows.Forms.DataGridViewImageColumn()
        Me.PuddleModified = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PuddleSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudioGloss = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudioGlosses = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudioImage = New System.Windows.Forms.DataGridViewImageColumn()
        Me.StudioModified = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StudioSource = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnContinue = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OverwritefromPuddle, Me.PuddleGloss, Me.PuddleGlosses, Me.PuddleImage, Me.PuddleModified, Me.PuddleSource, Me.StudioGloss, Me.StudioGlosses, Me.StudioImage, Me.StudioModified, Me.StudioSource})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1144, 519)
        Me.DataGridView1.TabIndex = 0
        '
        'OverwritefromPuddle
        '
        Me.OverwritefromPuddle.DataPropertyName = "OverwritefromPuddle"
        Me.OverwritefromPuddle.HeaderText = "Update"
        Me.OverwritefromPuddle.Name = "OverwritefromPuddle"
        Me.OverwritefromPuddle.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.OverwritefromPuddle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.OverwritefromPuddle.Width = 60
        '
        'PuddleGloss
        '
        Me.PuddleGloss.DataPropertyName = "PuddleGloss"
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleGloss.DefaultCellStyle = DataGridViewCellStyle1
        Me.PuddleGloss.HeaderText = "Puddle Term"
        Me.PuddleGloss.Name = "PuddleGloss"
        Me.PuddleGloss.ReadOnly = True
        '
        'PuddleGlosses
        '
        Me.PuddleGlosses.DataPropertyName = "PuddleGlosses"
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleGlosses.DefaultCellStyle = DataGridViewCellStyle2
        Me.PuddleGlosses.HeaderText = "Puddle Terms"
        Me.PuddleGlosses.Name = "PuddleGlosses"
        Me.PuddleGlosses.ReadOnly = True
        '
        'PuddleImage
        '
        Me.PuddleImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.PuddleImage.DataPropertyName = "PuddleImage"
        Me.PuddleImage.HeaderText = "Puddle Sign"
        Me.PuddleImage.MinimumWidth = 100
        Me.PuddleImage.Name = "PuddleImage"
        Me.PuddleImage.ReadOnly = True
        Me.PuddleImage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'PuddleModified
        '
        Me.PuddleModified.DataPropertyName = "PuddleModified"
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleModified.DefaultCellStyle = DataGridViewCellStyle3
        Me.PuddleModified.HeaderText = "Puddle Modified"
        Me.PuddleModified.Name = "PuddleModified"
        Me.PuddleModified.ReadOnly = True
        Me.PuddleModified.Width = 110
        '
        'PuddleSource
        '
        Me.PuddleSource.DataPropertyName = "PuddleSource"
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleSource.DefaultCellStyle = DataGridViewCellStyle4
        Me.PuddleSource.HeaderText = "Puddle Source"
        Me.PuddleSource.Name = "PuddleSource"
        Me.PuddleSource.ReadOnly = True
        '
        'StudioGloss
        '
        Me.StudioGloss.DataPropertyName = "StudioGloss"
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioGloss.DefaultCellStyle = DataGridViewCellStyle5
        Me.StudioGloss.HeaderText = "Studio Gloss"
        Me.StudioGloss.Name = "StudioGloss"
        Me.StudioGloss.ReadOnly = True
        '
        'StudioGlosses
        '
        Me.StudioGlosses.DataPropertyName = "StudioGlosses"
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioGlosses.DefaultCellStyle = DataGridViewCellStyle6
        Me.StudioGlosses.HeaderText = "Studio Glosses"
        Me.StudioGlosses.Name = "StudioGlosses"
        Me.StudioGlosses.ReadOnly = True
        '
        'StudioImage
        '
        Me.StudioImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.StudioImage.DataPropertyName = "StudioImage"
        Me.StudioImage.HeaderText = "Studio Sign"
        Me.StudioImage.MinimumWidth = 100
        Me.StudioImage.Name = "StudioImage"
        Me.StudioImage.ReadOnly = True
        Me.StudioImage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'StudioModified
        '
        Me.StudioModified.DataPropertyName = "StudioModified"
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioModified.DefaultCellStyle = DataGridViewCellStyle7
        Me.StudioModified.HeaderText = "Studio Modified"
        Me.StudioModified.Name = "StudioModified"
        Me.StudioModified.ReadOnly = True
        Me.StudioModified.Width = 110
        '
        'StudioSource
        '
        Me.StudioSource.DataPropertyName = "StudioSource"
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioSource.DefaultCellStyle = DataGridViewCellStyle8
        Me.StudioSource.HeaderText = "Studio Source"
        Me.StudioSource.Name = "StudioSource"
        Me.StudioSource.ReadOnly = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnContinue)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1144, 561)
        Me.SplitContainer1.SplitterDistance = 38
        Me.SplitContainer1.TabIndex = 1
        '
        'btnContinue
        '
        Me.btnContinue.Location = New System.Drawing.Point(931, 8)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(75, 23)
        Me.btnContinue.TabIndex = 0
        Me.btnContinue.Text = "Continue"
        Me.btnContinue.UseVisualStyleBackColor = True
        '
        'CompareSigns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1144, 561)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "CompareSigns"
        Me.Text = "Compare Signs"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents OverwritefromPuddle As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents PuddleGloss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PuddleGlosses As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PuddleImage As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents PuddleModified As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PuddleSource As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StudioGloss As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StudioGlosses As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StudioImage As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents StudioModified As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StudioSource As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
