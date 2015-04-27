<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlossToSign
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim BtnCancel As System.Windows.Forms.Button
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GlossToSign))
        Me.TBGlossToSign = New System.Windows.Forms.TextBox()
        Me.LBEnterGloss = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.WordsbyLanguagesTransBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnGlossToSign = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TBGlossNotFound = New System.Windows.Forms.TextBox()
        Me.BtnAccept = New System.Windows.Forms.Button()
        Me.GlossMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.InsertGlossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeGlossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveGlossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        BtnCancel = New System.Windows.Forms.Button()
        CType(Me.WordsbyLanguagesTransBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        Me.GlossMenuStrip.SuspendLayout
        Me.SuspendLayout
        '
        'BtnCancel
        '
        BtnCancel.Location = New System.Drawing.Point(523, 3)
        BtnCancel.Name = "BtnCancel"
        BtnCancel.Size = New System.Drawing.Size(75, 23)
        BtnCancel.TabIndex = 5
        BtnCancel.Text = "&Cancel"
        BtnCancel.UseVisualStyleBackColor = true
        AddHandler BtnCancel.Click, AddressOf Me.BtnCancel_Click
        '
        'TBGlossToSign
        '
        Me.TBGlossToSign.AcceptsReturn = true
        Me.TBGlossToSign.Location = New System.Drawing.Point(75, 3)
        Me.TBGlossToSign.Multiline = true
        Me.TBGlossToSign.Name = "TBGlossToSign"
        Me.TBGlossToSign.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TBGlossToSign.Size = New System.Drawing.Size(255, 48)
        Me.TBGlossToSign.TabIndex = 2
        '
        'LBEnterGloss
        '
        Me.LBEnterGloss.AutoSize = true
        Me.LBEnterGloss.Location = New System.Drawing.Point(3, 3)
        Me.LBEnterGloss.Name = "LBEnterGloss"
        Me.LBEnterGloss.Size = New System.Drawing.Size(61, 13)
        Me.LBEnterGloss.TabIndex = 1
        Me.LBEnterGloss.Text = "Enter &Gloss"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = true
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(980, 384)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'btnGlossToSign
        '
        Me.btnGlossToSign.Location = New System.Drawing.Point(336, 3)
        Me.btnGlossToSign.Name = "btnGlossToSign"
        Me.btnGlossToSign.Size = New System.Drawing.Size(100, 23)
        Me.btnGlossToSign.TabIndex = 3
        Me.btnGlossToSign.Text = "&Find"
        Me.btnGlossToSign.UseVisualStyleBackColor = true
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TBGlossNotFound)
        Me.SplitContainer1.Panel1.Controls.Add(BtnCancel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.BtnAccept)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnGlossToSign)
        Me.SplitContainer1.Panel1.Controls.Add(Me.TBGlossToSign)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LBEnterGloss)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.FlowLayoutPanel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(980, 442)
        Me.SplitContainer1.SplitterDistance = 54
        Me.SplitContainer1.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(604, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Gloss not found"
        '
        'TBGlossNotFound
        '
        Me.TBGlossNotFound.Location = New System.Drawing.Point(690, 3)
        Me.TBGlossNotFound.Multiline = true
        Me.TBGlossNotFound.Name = "TBGlossNotFound"
        Me.TBGlossNotFound.Size = New System.Drawing.Size(278, 47)
        Me.TBGlossNotFound.TabIndex = 6
        '
        'BtnAccept
        '
        Me.BtnAccept.Location = New System.Drawing.Point(442, 3)
        Me.BtnAccept.Name = "BtnAccept"
        Me.BtnAccept.Size = New System.Drawing.Size(75, 23)
        Me.BtnAccept.TabIndex = 4
        Me.BtnAccept.Text = "&Accept"
        Me.BtnAccept.UseVisualStyleBackColor = true
        '
        'GlossMenuStrip
        '
        Me.GlossMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InsertGlossToolStripMenuItem, Me.ChangeGlossToolStripMenuItem, Me.RemoveGlossToolStripMenuItem})
        Me.GlossMenuStrip.Name = "GlossMenuStrip"
        Me.GlossMenuStrip.Size = New System.Drawing.Size(149, 70)
        '
        'InsertGlossToolStripMenuItem
        '
        Me.InsertGlossToolStripMenuItem.Name = "InsertGlossToolStripMenuItem"
        Me.InsertGlossToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.InsertGlossToolStripMenuItem.Text = "Insert Gloss"
        '
        'ChangeGlossToolStripMenuItem
        '
        Me.ChangeGlossToolStripMenuItem.Name = "ChangeGlossToolStripMenuItem"
        Me.ChangeGlossToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ChangeGlossToolStripMenuItem.Text = "Change Gloss"
        '
        'RemoveGlossToolStripMenuItem
        '
        Me.RemoveGlossToolStripMenuItem.Name = "RemoveGlossToolStripMenuItem"
        Me.RemoveGlossToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.RemoveGlossToolStripMenuItem.Text = "Remove Gloss"
        '
        'GlossToSign
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(980, 442)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "GlossToSign"
        Me.Text = "SignWriter Studio™ Gloss to Sign"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.WordsbyLanguagesTransBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel1.PerformLayout
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.GlossMenuStrip.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents TBGlossToSign As System.Windows.Forms.TextBox
    Friend WithEvents LBEnterGloss As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnGlossToSign As System.Windows.Forms.Button
    Friend WithEvents WordsbyLanguagesTransBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents WordsbyLanguagesTransTableAdapter As SLVocabularyLists.ASL_ListDataSetTableAdapters.WordsbyLanguagesTransTableAdapter
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents BtnAccept As System.Windows.Forms.Button
    Friend WithEvents GlossMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents InsertGlossToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeGlossToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveGlossToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TBGlossNotFound As System.Windows.Forms.TextBox
End Class
