<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlossToSignRealTimeControl
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.btnInsertBefore = New System.Windows.Forms.Button()
        Me.btnInsertAfter = New System.Windows.Forms.Button()
        Me.btnAddFromDict = New System.Windows.Forms.Button()
        Me.Delete = New System.Windows.Forms.Button()
        Me.SignsbyGlossesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DictionaryDataSet = New SignWriterStudio.Database.Dictionary.DictionaryDataSet()
        Me.SignsbyGlossesUnilingualTableAdapter = New SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters.SignsbyGlossesUnilingualTableAdapter()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SignsbyGlossesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DictionaryDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(255, 161)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(121, 20)
        Me.TextBox1.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(216, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(112, 154)
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Location = New System.Drawing.Point(264, 3)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(112, 154)
        Me.PictureBox2.TabIndex = 6
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Location = New System.Drawing.Point(308, 3)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(112, 154)
        Me.PictureBox3.TabIndex = 7
        Me.PictureBox3.TabStop = False
        '
        'btnInsertBefore
        '
        Me.btnInsertBefore.Location = New System.Drawing.Point(0, 26)
        Me.btnInsertBefore.Name = "btnInsertBefore"
        Me.btnInsertBefore.Size = New System.Drawing.Size(75, 23)
        Me.btnInsertBefore.TabIndex = 8
        Me.btnInsertBefore.Text = "Insert Before"
        Me.btnInsertBefore.UseVisualStyleBackColor = True
        '
        'btnInsertAfter
        '
        Me.btnInsertAfter.Location = New System.Drawing.Point(0, 55)
        Me.btnInsertAfter.Name = "btnInsertAfter"
        Me.btnInsertAfter.Size = New System.Drawing.Size(75, 23)
        Me.btnInsertAfter.TabIndex = 9
        Me.btnInsertAfter.Text = "Insert After"
        Me.btnInsertAfter.UseVisualStyleBackColor = True
        '
        'btnAddFromDict
        '
        Me.btnAddFromDict.Location = New System.Drawing.Point(92, 26)
        Me.btnAddFromDict.Name = "btnAddFromDict"
        Me.btnAddFromDict.Size = New System.Drawing.Size(118, 23)
        Me.btnAddFromDict.TabIndex = 10
        Me.btnAddFromDict.Text = "Add from Dict"
        Me.btnAddFromDict.UseVisualStyleBackColor = True
        '
        'Delete
        '
        Me.Delete.Location = New System.Drawing.Point(92, 55)
        Me.Delete.Name = "Delete"
        Me.Delete.Size = New System.Drawing.Size(118, 23)
        Me.Delete.TabIndex = 11
        Me.Delete.Text = "Delete"
        Me.Delete.UseVisualStyleBackColor = True
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
        Me.SignsbyGlossesUnilingualTableAdapter.ClearBeforeFill = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Location = New System.Drawing.Point(3, 127)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveDown.TabIndex = 13
        Me.btnMoveDown.Text = "Move down"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Location = New System.Drawing.Point(3, 98)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveUp.TabIndex = 12
        Me.btnMoveUp.Text = "Move up"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'GlossToSignRealTimeControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoSize = True
        Me.Controls.Add(Me.btnMoveDown)
        Me.Controls.Add(Me.btnMoveUp)
        Me.Controls.Add(Me.Delete)
        Me.Controls.Add(Me.btnAddFromDict)
        Me.Controls.Add(Me.btnInsertAfter)
        Me.Controls.Add(Me.btnInsertBefore)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "GlossToSignRealTimeControl"
        Me.Size = New System.Drawing.Size(431, 184)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SignsbyGlossesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DictionaryDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    'Friend WithEvents ASL_ListDataSet As SLVocabularyLists.ASL_ListDataSet
    'Friend WithEvents WordsbyLanguagesTransTableAdapter As SLVocabularyLists.ASL_ListDataSetTableAdapters.WordsbyLanguagesTransTableAdapter
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
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents btnInsertBefore As System.Windows.Forms.Button
    Friend WithEvents btnInsertAfter As System.Windows.Forms.Button
    Friend WithEvents btnAddFromDict As System.Windows.Forms.Button
    Friend WithEvents Delete As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button

End Class
