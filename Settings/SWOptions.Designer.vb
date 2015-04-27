<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SWOptions
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SWOptions))
        Me.CBUserInterfaceLang = New System.Windows.Forms.ComboBox()
        Me.CulturesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LBFirstGlossLang = New System.Windows.Forms.Label()
        Me.LBUserInterfaceSignLang = New System.Windows.Forms.Label()
        Me.LBSecondGlossLang = New System.Windows.Forms.Label()
        Me.CBUserInterfaceSignLang = New System.Windows.Forms.ComboBox()
        Me.CBGlossLang2 = New System.Windows.Forms.ComboBox()
        Me.CBGlossLang1 = New System.Windows.Forms.ComboBox()
        Me.LBUserInterfaceLang = New System.Windows.Forms.Label()
        Me.SignLanguagesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LBDefaultSignLang = New System.Windows.Forms.Label()
        Me.CBDefaulSignLang = New System.Windows.Forms.ComboBox()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.CBBilingual = New System.Windows.Forms.CheckBox()
        CType(Me.CulturesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SignLanguagesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CBUserInterfaceLang
        '
        Me.CBUserInterfaceLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBUserInterfaceLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBUserInterfaceLang.DataSource = Me.CulturesBindingSource
        Me.CBUserInterfaceLang.DisplayMember = "Language-Country/Region"
        Me.CBUserInterfaceLang.FormattingEnabled = True
        Me.CBUserInterfaceLang.Location = New System.Drawing.Point(158, 18)
        Me.CBUserInterfaceLang.Name = "CBUserInterfaceLang"
        Me.CBUserInterfaceLang.Size = New System.Drawing.Size(121, 21)
        Me.CBUserInterfaceLang.TabIndex = 3
        Me.CBUserInterfaceLang.ValueMember = "Culture Name"
        Me.CBUserInterfaceLang.Visible = False
        '
        'CulturesBindingSource
        '
        Me.CulturesBindingSource.DataMember = "Cultures"
        Me.CulturesBindingSource.Filter = "[Id Culture] >1 "
        Me.CulturesBindingSource.Sort = ""
        '
        'LBFirstGlossLang
        '
        Me.LBFirstGlossLang.AutoSize = True
        Me.LBFirstGlossLang.Location = New System.Drawing.Point(154, 59)
        Me.LBFirstGlossLang.Name = "LBFirstGlossLang"
        Me.LBFirstGlossLang.Size = New System.Drawing.Size(106, 13)
        Me.LBFirstGlossLang.TabIndex = 7
        Me.LBFirstGlossLang.Text = "&First Gloss Language"
        '
        'LBUserInterfaceSignLang
        '
        Me.LBUserInterfaceSignLang.AutoSize = True
        Me.LBUserInterfaceSignLang.Location = New System.Drawing.Point(5, 2)
        Me.LBUserInterfaceSignLang.Name = "LBUserInterfaceSignLang"
        Me.LBUserInterfaceSignLang.Size = New System.Drawing.Size(149, 13)
        Me.LBUserInterfaceSignLang.TabIndex = 0
        Me.LBUserInterfaceSignLang.Text = "&User Interface Sign Language"
        Me.LBUserInterfaceSignLang.Visible = False
        '
        'LBSecondGlossLang
        '
        Me.LBSecondGlossLang.AutoSize = True
        Me.LBSecondGlossLang.Location = New System.Drawing.Point(281, 59)
        Me.LBSecondGlossLang.Name = "LBSecondGlossLang"
        Me.LBSecondGlossLang.Size = New System.Drawing.Size(124, 13)
        Me.LBSecondGlossLang.TabIndex = 9
        Me.LBSecondGlossLang.Text = "&Second Gloss Language"
        '
        'CBUserInterfaceSignLang
        '
        Me.CBUserInterfaceSignLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBUserInterfaceSignLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBUserInterfaceSignLang.DisplayMember = "ID SignLanguage"
        Me.CBUserInterfaceSignLang.DropDownWidth = 250
        Me.CBUserInterfaceSignLang.FormattingEnabled = True
        Me.CBUserInterfaceSignLang.Location = New System.Drawing.Point(8, 15)
        Me.CBUserInterfaceSignLang.Name = "CBUserInterfaceSignLang"
        Me.CBUserInterfaceSignLang.Size = New System.Drawing.Size(121, 21)
        Me.CBUserInterfaceSignLang.TabIndex = 1
        Me.CBUserInterfaceSignLang.ValueMember = "ID SignLanguage"
        Me.CBUserInterfaceSignLang.Visible = False
        '
        'CBGlossLang2
        '
        Me.CBGlossLang2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBGlossLang2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBGlossLang2.DisplayMember = "ID Culture"
        Me.CBGlossLang2.FormattingEnabled = True
        Me.CBGlossLang2.Location = New System.Drawing.Point(284, 72)
        Me.CBGlossLang2.Name = "CBGlossLang2"
        Me.CBGlossLang2.Size = New System.Drawing.Size(121, 21)
        Me.CBGlossLang2.TabIndex = 10
        Me.CBGlossLang2.ValueMember = "ID Culture"
        '
        'CBGlossLang1
        '
        Me.CBGlossLang1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBGlossLang1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBGlossLang1.DisplayMember = "ID Culture"
        Me.CBGlossLang1.FormattingEnabled = True
        Me.CBGlossLang1.Location = New System.Drawing.Point(157, 72)
        Me.CBGlossLang1.Name = "CBGlossLang1"
        Me.CBGlossLang1.Size = New System.Drawing.Size(121, 21)
        Me.CBGlossLang1.TabIndex = 8
        Me.CBGlossLang1.ValueMember = "ID Culture"
        '
        'LBUserInterfaceLang
        '
        Me.LBUserInterfaceLang.AutoSize = True
        Me.LBUserInterfaceLang.Location = New System.Drawing.Point(155, 2)
        Me.LBUserInterfaceLang.Name = "LBUserInterfaceLang"
        Me.LBUserInterfaceLang.Size = New System.Drawing.Size(125, 13)
        Me.LBUserInterfaceLang.TabIndex = 2
        Me.LBUserInterfaceLang.Text = "User &Inferface Language"
        Me.LBUserInterfaceLang.Visible = False
        '
        'SignLanguagesBindingSource
        '
        Me.SignLanguagesBindingSource.DataMember = "SignLanguages"
        Me.SignLanguagesBindingSource.Sort = ""
        '
        'LBDefaultSignLang
        '
        Me.LBDefaultSignLang.AutoSize = True
        Me.LBDefaultSignLang.Location = New System.Drawing.Point(4, 59)
        Me.LBDefaultSignLang.Name = "LBDefaultSignLang"
        Me.LBDefaultSignLang.Size = New System.Drawing.Size(79, 13)
        Me.LBDefaultSignLang.TabIndex = 5
        Me.LBDefaultSignLang.Text = "Sign &Language"
        '
        'CBDefaulSignLang
        '
        Me.CBDefaulSignLang.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBDefaulSignLang.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBDefaulSignLang.DisplayMember = "ID SignLanguage"
        Me.CBDefaulSignLang.DropDownWidth = 250
        Me.CBDefaulSignLang.FormattingEnabled = True
        Me.CBDefaulSignLang.Location = New System.Drawing.Point(7, 72)
        Me.CBDefaulSignLang.Name = "CBDefaulSignLang"
        Me.CBDefaulSignLang.Size = New System.Drawing.Size(121, 21)
        Me.CBDefaulSignLang.TabIndex = 6
        Me.CBDefaulSignLang.ValueMember = "ID SignLanguage"
        '
        'BtnSave
        '
        Me.BtnSave.Location = New System.Drawing.Point(235, 112)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnSave.TabIndex = 11
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(316, 112)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 12
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'CBBilingual
        '
        Me.CBBilingual.AutoSize = True
        Me.CBBilingual.Location = New System.Drawing.Point(157, 41)
        Me.CBBilingual.Name = "CBBilingual"
        Me.CBBilingual.Size = New System.Drawing.Size(94, 17)
        Me.CBBilingual.TabIndex = 4
        Me.CBBilingual.Text = "&Bilingual Gloss"
        Me.CBBilingual.UseVisualStyleBackColor = True
        '
        'SWOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 144)
        Me.Controls.Add(Me.CBBilingual)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.LBDefaultSignLang)
        Me.Controls.Add(Me.CBDefaulSignLang)
        Me.Controls.Add(Me.LBUserInterfaceLang)
        Me.Controls.Add(Me.LBFirstGlossLang)
        Me.Controls.Add(Me.LBUserInterfaceSignLang)
        Me.Controls.Add(Me.LBSecondGlossLang)
        Me.Controls.Add(Me.CBUserInterfaceSignLang)
        Me.Controls.Add(Me.CBGlossLang2)
        Me.Controls.Add(Me.CBGlossLang1)
        Me.Controls.Add(Me.CBUserInterfaceLang)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SWOptions"
        Me.Text = "SignWriter Studio™  Options"
        CType(Me.CulturesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SignLanguagesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBUserInterfaceLang As System.Windows.Forms.ComboBox
    Friend WithEvents LBFirstGlossLang As System.Windows.Forms.Label
    Friend WithEvents LBUserInterfaceSignLang As System.Windows.Forms.Label
    Friend WithEvents LBSecondGlossLang As System.Windows.Forms.Label
    Friend WithEvents CBUserInterfaceSignLang As System.Windows.Forms.ComboBox
    Friend WithEvents CBGlossLang2 As System.Windows.Forms.ComboBox
    Friend WithEvents CBGlossLang1 As System.Windows.Forms.ComboBox
    Friend WithEvents LBUserInterfaceLang As System.Windows.Forms.Label
    'Friend WithEvents UITranslationsDataSet As SLVocabularyLists.UITranslationsDataSet
    Friend WithEvents CulturesBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEventsDatabase.Databases.CulturesTableAdapter As SLVocabularyLists.UITranslationsDataSetTableAdapters.CulturesTableAdapter
    Friend WithEvents SignLanguagesBindingSource As System.Windows.Forms.BindingSource
    'Friend WithEvents SignLanguagesTableAdapter As SLVocabularyLists.UITranslationsDataSetTableAdapters.SignLanguagesTableAdapter
    Friend WithEvents LBDefaultSignLang As System.Windows.Forms.Label
    Friend WithEvents CBDefaulSignLang As System.Windows.Forms.ComboBox
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents CBBilingual As System.Windows.Forms.CheckBox
End Class
