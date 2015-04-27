<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlossToSignControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.LBGloss = New System.Windows.Forms.Label()
        Me.GlossToSignDataGridView = New System.Windows.Forms.DataGridView()
        Me.Selected = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.SWriting = New System.Windows.Forms.DataGridViewImageColumn()
        Me.gloss1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.glosses1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IDDictionary = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SignsbyGlossesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DictionaryDataSet = New SignWriterStudio.Database.Dictionary.DictionaryDataSet()
        Me.SignsbyGlossesUnilingualTableAdapter = New SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters.SignsbyGlossesUnilingualTableAdapter()
        CType(Me.GlossToSignDataGridView,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SignsbyGlossesBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DictionaryDataSet,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'LBGloss
        '
        Me.LBGloss.AutoSize = true
        Me.LBGloss.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.LBGloss.Location = New System.Drawing.Point(2, 3)
        Me.LBGloss.Name = "LBGloss"
        Me.LBGloss.Size = New System.Drawing.Size(39, 13)
        Me.LBGloss.TabIndex = 2
        Me.LBGloss.Text = "Label1"
        '
        'GlossToSignDataGridView
        '
        Me.GlossToSignDataGridView.AllowUserToAddRows = false
        Me.GlossToSignDataGridView.AllowUserToDeleteRows = false
        Me.GlossToSignDataGridView.AllowUserToOrderColumns = true
        Me.GlossToSignDataGridView.AutoGenerateColumns = false
        Me.GlossToSignDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.GlossToSignDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GlossToSignDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Selected, Me.SWriting, Me.gloss1, Me.glosses1, Me.IDDictionary})
        Me.GlossToSignDataGridView.DataSource = Me.SignsbyGlossesBindingSource
        Me.GlossToSignDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GlossToSignDataGridView.GridColor = System.Drawing.SystemColors.AppWorkspace
        Me.GlossToSignDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.GlossToSignDataGridView.Name = "GlossToSignDataGridView"
        Me.GlossToSignDataGridView.Size = New System.Drawing.Size(186, 105)
        Me.GlossToSignDataGridView.TabIndex = 3
        '
        'Selected
        '
        Me.Selected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.Selected.DataPropertyName = "Selected"
        Me.Selected.HeaderText = "Select"
        Me.Selected.Name = "Selected"
        Me.Selected.Width = 43
        '
        'SWriting
        '
        Me.SWriting.DataPropertyName = "SWriting"
        Me.SWriting.HeaderText = "Sign"
        Me.SWriting.Name = "SWriting"
        '
        'gloss1
        '
        Me.gloss1.DataPropertyName = "gloss1"
        Me.gloss1.HeaderText = "Gloss"
        Me.gloss1.Name = "gloss1"
        '
        'glosses1
        '
        Me.glosses1.DataPropertyName = "glosses1"
        Me.glosses1.HeaderText = "Glosses"
        Me.glosses1.Name = "glosses1"
        '
        'IDDictionary
        '
        Me.IDDictionary.DataPropertyName = "IDDictionary"
        Me.IDDictionary.HeaderText = "IDDictionary"
        Me.IDDictionary.Name = "IDDictionary"
        Me.IDDictionary.Visible = false
        '
        'SignsbyGlossesBindingSource
        '
        Me.SignsbyGlossesBindingSource.DataMember = "SignsbyGlossesUnilingual"
        Me.SignsbyGlossesBindingSource.DataSource = Me.DictionaryDataSet
        '
        'DictionaryDataSet
        '
        Me.DictionaryDataSet.DataSetName = "Database.DictionaryDataSet"
        Me.DictionaryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'SignsbyGlossesUnilingualTableAdapter
        '
        Me.SignsbyGlossesUnilingualTableAdapter.ClearBeforeFill = true
        '
        'GlossToSignControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = true
        Me.AutoSize = true
        Me.Controls.Add(Me.LBGloss)
        Me.Controls.Add(Me.GlossToSignDataGridView)
        Me.Name = "GlossToSignControl"
        Me.Size = New System.Drawing.Size(186, 105)
        CType(Me.GlossToSignDataGridView,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SignsbyGlossesBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DictionaryDataSet,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    'Friend WithEvents ASL_ListDataSet As SLVocabularyLists.ASL_ListDataSet
    'Friend WithEvents WordsbyLanguagesTransTableAdapter As SLVocabularyLists.ASL_ListDataSetTableAdapters.WordsbyLanguagesTransTableAdapter
    Friend WithEvents LBGloss As System.Windows.Forms.Label
    Friend WithEvents IDWordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDSignLanguageDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language1PWordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language1SWordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language2PWordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language2SWordDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SWSignDataGridViewImageColumn As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Language1IDCultureDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Language2IDCultureDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IdSignWritingDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DictionaryDataSet As Database.Dictionary.DictionaryDataSet
    'Friend WithEvents WordsbyLanguageTransBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents WordsbyLanguageTransTableAdapter As SignWriter.Database.DictionaryDataSetTableAdapters.WordsbyLanguageTransTableAdapter
    Friend WithEvents GlossToSignDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn1 As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SignsbyGlossesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SignsbyGlossesUnilingualTableAdapter As Database.Dictionary.DictionaryDataSetTableAdapters.SignsbyGlossesUnilingualTableAdapter
    Friend WithEvents SWSign As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents Selected As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents SWriting As System.Windows.Forms.DataGridViewImageColumn
    Friend WithEvents gloss1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents glosses1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDDictionary As System.Windows.Forms.DataGridViewTextBoxColumn

End Class
