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
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CompareSigns))
        Me.DataGridViewCompare = New System.Windows.Forms.DataGridView()
        Me.BindingSourceCompare = New System.Windows.Forms.BindingSource(Me.components)
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.BindingSourceAdd = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridAdd = New System.Windows.Forms.DataGridView()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewImageColumn1 = New System.Windows.Forms.DataGridViewImageColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PuddleSign = New System.Windows.Forms.DataGridViewTextBoxColumn()
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
        CType(Me.DataGridViewCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BindingSourceCompare, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.BindingSourceAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridAdd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewCompare
        '
        Me.DataGridViewCompare.AllowUserToAddRows = False
        Me.DataGridViewCompare.AllowUserToDeleteRows = False
        Me.DataGridViewCompare.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.DataGridViewCompare.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewCompare.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.OverwritefromPuddle, Me.PuddleGloss, Me.PuddleGlosses, Me.PuddleImage, Me.PuddleModified, Me.PuddleSource, Me.StudioGloss, Me.StudioGlosses, Me.StudioImage, Me.StudioModified, Me.StudioSource})
        Me.DataGridViewCompare.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewCompare.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewCompare.Name = "DataGridViewCompare"
        Me.DataGridViewCompare.Size = New System.Drawing.Size(1144, 519)
        Me.DataGridViewCompare.TabIndex = 0
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridAdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridViewCompare)
        Me.SplitContainer1.Size = New System.Drawing.Size(1144, 561)
        Me.SplitContainer1.SplitterDistance = 38
        Me.SplitContainer1.TabIndex = 1
        '
        'btnContinue
        '
        Me.btnContinue.Location = New System.Drawing.Point(12, 12)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(75, 23)
        Me.btnContinue.TabIndex = 0
        Me.btnContinue.Text = "Continue"
        Me.btnContinue.UseVisualStyleBackColor = True
        '
        'DataGridAdd
        '
        Me.DataGridAdd.AllowUserToAddRows = False
        Me.DataGridAdd.AllowUserToDeleteRows = False
        Me.DataGridAdd.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        Me.DataGridAdd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridAdd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewCheckBoxColumn1, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewImageColumn1, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.PuddleSign})
        Me.DataGridAdd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridAdd.Location = New System.Drawing.Point(0, 0)
        Me.DataGridAdd.Name = "DataGridAdd"
        Me.DataGridAdd.Size = New System.Drawing.Size(1144, 519)
        Me.DataGridAdd.TabIndex = 1
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "OverwritefromPuddle"
        Me.DataGridViewCheckBoxColumn1.HeaderText = "Add"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn1.Width = 60
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "PuddleGloss"
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn1.DefaultCellStyle = DataGridViewCellStyle8
        Me.DataGridViewTextBoxColumn1.HeaderText = "Gloss"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "PuddleGlosses"
        DataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.DefaultCellStyle = DataGridViewCellStyle9
        Me.DataGridViewTextBoxColumn2.HeaderText = "Glosses"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewImageColumn1
        '
        Me.DataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewImageColumn1.DataPropertyName = "PuddleImage"
        Me.DataGridViewImageColumn1.HeaderText = "Sign"
        Me.DataGridViewImageColumn1.MinimumWidth = 100
        Me.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        Me.DataGridViewImageColumn1.ReadOnly = True
        Me.DataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "PuddleModified"
        DataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn3.DefaultCellStyle = DataGridViewCellStyle10
        Me.DataGridViewTextBoxColumn3.HeaderText = "Modified"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 110
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "PuddleSource"
        DataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn4.DefaultCellStyle = DataGridViewCellStyle11
        Me.DataGridViewTextBoxColumn4.HeaderText = "Source"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'PuddleSign
        '
        Me.PuddleSign.DataPropertyName = "puddleSign"
        Me.PuddleSign.HeaderText = "Sign"
        Me.PuddleSign.Name = "PuddleSign"
        Me.PuddleSign.Visible = False
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
        Me.PuddleGloss.HeaderText = "Import Gloss"
        Me.PuddleGloss.Name = "PuddleGloss"
        Me.PuddleGloss.ReadOnly = True
        '
        'PuddleGlosses
        '
        Me.PuddleGlosses.DataPropertyName = "PuddleGlosses"
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleGlosses.DefaultCellStyle = DataGridViewCellStyle1
        Me.PuddleGlosses.HeaderText = "Import Glosses"
        Me.PuddleGlosses.Name = "PuddleGlosses"
        Me.PuddleGlosses.ReadOnly = True
        '
        'PuddleImage
        '
        Me.PuddleImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.PuddleImage.DataPropertyName = "PuddleImage"
        Me.PuddleImage.HeaderText = "Import Sign"
        Me.PuddleImage.MinimumWidth = 100
        Me.PuddleImage.Name = "PuddleImage"
        Me.PuddleImage.ReadOnly = True
        Me.PuddleImage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'PuddleModified
        '
        Me.PuddleModified.DataPropertyName = "PuddleModified"
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleModified.DefaultCellStyle = DataGridViewCellStyle2
        Me.PuddleModified.HeaderText = "Import Modified Date"
        Me.PuddleModified.Name = "PuddleModified"
        Me.PuddleModified.ReadOnly = True
        Me.PuddleModified.Width = 110
        '
        'PuddleSource
        '
        Me.PuddleSource.DataPropertyName = "PuddleSource"
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PuddleSource.DefaultCellStyle = DataGridViewCellStyle3
        Me.PuddleSource.HeaderText = "Import Source"
        Me.PuddleSource.Name = "PuddleSource"
        Me.PuddleSource.ReadOnly = True
        '
        'StudioGloss
        '
        Me.StudioGloss.DataPropertyName = "StudioGloss"
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioGloss.DefaultCellStyle = DataGridViewCellStyle4
        Me.StudioGloss.HeaderText = "Existing Gloss"
        Me.StudioGloss.Name = "StudioGloss"
        Me.StudioGloss.ReadOnly = True
        '
        'StudioGlosses
        '
        Me.StudioGlosses.DataPropertyName = "StudioGlosses"
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioGlosses.DefaultCellStyle = DataGridViewCellStyle5
        Me.StudioGlosses.HeaderText = "Existing Glosses"
        Me.StudioGlosses.Name = "StudioGlosses"
        Me.StudioGlosses.ReadOnly = True
        '
        'StudioImage
        '
        Me.StudioImage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.StudioImage.DataPropertyName = "StudioImage"
        Me.StudioImage.HeaderText = "Existing Sign"
        Me.StudioImage.MinimumWidth = 100
        Me.StudioImage.Name = "StudioImage"
        Me.StudioImage.ReadOnly = True
        Me.StudioImage.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'StudioModified
        '
        Me.StudioModified.DataPropertyName = "StudioModified"
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioModified.DefaultCellStyle = DataGridViewCellStyle6
        Me.StudioModified.HeaderText = "Existing Modified Date"
        Me.StudioModified.Name = "StudioModified"
        Me.StudioModified.ReadOnly = True
        Me.StudioModified.Width = 110
        '
        'StudioSource
        '
        Me.StudioSource.DataPropertyName = "StudioSource"
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StudioSource.DefaultCellStyle = DataGridViewCellStyle7
        Me.StudioSource.HeaderText = "Existing Source"
        Me.StudioSource.Name = "StudioSource"
        Me.StudioSource.ReadOnly = True
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
        CType(Me.DataGridViewCompare, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BindingSourceCompare, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.BindingSourceAdd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridAdd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewCompare As System.Windows.Forms.DataGridView
    Friend WithEvents BindingSourceCompare As System.Windows.Forms.BindingSource
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnContinue As System.Windows.Forms.Button
    Friend WithEvents DataGridAdd As System.Windows.Forms.DataGridView
    Friend WithEvents BindingSourceAdd As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PuddleSign As System.Windows.Forms.DataGridViewTextBoxColumn
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
