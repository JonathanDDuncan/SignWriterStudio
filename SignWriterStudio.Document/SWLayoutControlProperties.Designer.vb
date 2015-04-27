<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SWLayoutControlProperties
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SWLayoutControlProperties))
        Me.CBGlossLang = New System.Windows.Forms.ComboBox()
        Me.UICulturesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.swsUIDataSet = New SignWriterStudio.UI.swsuiDataSet()
        Me.CBSLanguage = New System.Windows.Forms.ComboBox()
        Me.UISignLanguagesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TBGloss = New System.Windows.Forms.TextBox()
        Me.TBGlosses = New System.Windows.Forms.TextBox()
        Me.LBSignPuddleID = New System.Windows.Forms.Label()
        Me.LBSignWriterGUID = New System.Windows.Forms.Label()
        Me.LBGloss = New System.Windows.Forms.Label()
        Me.LBGlosses = New System.Windows.Forms.Label()
        Me.LBDescIDSignPuddle = New System.Windows.Forms.Label()
        Me.LBDescSignWriterGUID = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.BtnAccept = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.UISignLanguagesTableAdapter = New SignWriterStudio.UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter()
        Me.UICulturesTableAdapter = New SignWriterStudio.UI.swsuiDataSetTableAdapters.UICulturesTableAdapter()
        CType(Me.UICulturesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.swsUIDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UISignLanguagesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CBGlossLang
        '
        Me.CBGlossLang.DataSource = Me.UICulturesBindingSource
        Me.CBGlossLang.DisplayMember = "LanguageCountryRegion"
        Me.CBGlossLang.FormattingEnabled = True
        Me.CBGlossLang.Location = New System.Drawing.Point(265, 4)
        Me.CBGlossLang.Name = "CBGlossLang"
        Me.CBGlossLang.Size = New System.Drawing.Size(121, 21)
        Me.CBGlossLang.TabIndex = 0
        Me.CBGlossLang.ValueMember = "IDCulture"
        '
        'UICulturesBindingSource
        '
        Me.UICulturesBindingSource.DataMember = "UICultures"
        Me.UICulturesBindingSource.DataSource = Me.swsUIDataSet
        '
        'swsUIDataSet
        '
        Me.swsUIDataSet.DataSetName = "swsUIDataSet"
        Me.swsUIDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'CBSLanguage
        '
        Me.CBSLanguage.DataSource = Me.UISignLanguagesBindingSource
        Me.CBSLanguage.DisplayMember = "SignLanguage"
        Me.CBSLanguage.FormattingEnabled = True
        Me.CBSLanguage.Location = New System.Drawing.Point(265, 30)
        Me.CBSLanguage.Name = "CBSLanguage"
        Me.CBSLanguage.Size = New System.Drawing.Size(121, 21)
        Me.CBSLanguage.TabIndex = 1
        Me.CBSLanguage.ValueMember = "IDSignLanguage"
        '
        'UISignLanguagesBindingSource
        '
        Me.UISignLanguagesBindingSource.DataMember = "UISignLanguages"
        Me.UISignLanguagesBindingSource.DataSource = Me.swsUIDataSet
        '
        'TBGloss
        '
        Me.TBGloss.Location = New System.Drawing.Point(60, 4)
        Me.TBGloss.Name = "TBGloss"
        Me.TBGloss.Size = New System.Drawing.Size(100, 20)
        Me.TBGloss.TabIndex = 2
        '
        'TBGlosses
        '
        Me.TBGlosses.Location = New System.Drawing.Point(60, 30)
        Me.TBGlosses.Name = "TBGlosses"
        Me.TBGlosses.Size = New System.Drawing.Size(100, 20)
        Me.TBGlosses.TabIndex = 3
        '
        'LBSignPuddleID
        '
        Me.LBSignPuddleID.AutoSize = True
        Me.LBSignPuddleID.Location = New System.Drawing.Point(93, 71)
        Me.LBSignPuddleID.Name = "LBSignPuddleID"
        Me.LBSignPuddleID.Size = New System.Drawing.Size(0, 13)
        Me.LBSignPuddleID.TabIndex = 4
        '
        'LBSignWriterGUID
        '
        Me.LBSignWriterGUID.AutoSize = True
        Me.LBSignWriterGUID.Location = New System.Drawing.Point(157, 53)
        Me.LBSignWriterGUID.Name = "LBSignWriterGUID"
        Me.LBSignWriterGUID.Size = New System.Drawing.Size(0, 13)
        Me.LBSignWriterGUID.TabIndex = 5
        '
        'LBGloss
        '
        Me.LBGloss.AutoSize = True
        Me.LBGloss.Location = New System.Drawing.Point(10, 11)
        Me.LBGloss.Name = "LBGloss"
        Me.LBGloss.Size = New System.Drawing.Size(33, 13)
        Me.LBGloss.TabIndex = 6
        Me.LBGloss.Text = "Gloss"
        '
        'LBGlosses
        '
        Me.LBGlosses.AutoSize = True
        Me.LBGlosses.Location = New System.Drawing.Point(10, 30)
        Me.LBGlosses.Name = "LBGlosses"
        Me.LBGlosses.Size = New System.Drawing.Size(44, 13)
        Me.LBGlosses.TabIndex = 7
        Me.LBGlosses.Text = "Glosses"
        '
        'LBDescIDSignPuddle
        '
        Me.LBDescIDSignPuddle.AutoSize = True
        Me.LBDescIDSignPuddle.Location = New System.Drawing.Point(12, 71)
        Me.LBDescIDSignPuddle.Name = "LBDescIDSignPuddle"
        Me.LBDescIDSignPuddle.Size = New System.Drawing.Size(75, 13)
        Me.LBDescIDSignPuddle.TabIndex = 8
        Me.LBDescIDSignPuddle.Text = "SignPuddle ID"
        '
        'LBDescSignWriterGUID
        '
        Me.LBDescSignWriterGUID.AutoSize = True
        Me.LBDescSignWriterGUID.Location = New System.Drawing.Point(10, 53)
        Me.LBDescSignWriterGUID.Name = "LBDescSignWriterGUID"
        Me.LBDescSignWriterGUID.Size = New System.Drawing.Size(128, 13)
        Me.LBDescSignWriterGUID.TabIndex = 9
        Me.LBDescSignWriterGUID.Text = "SignWriter Studio™ GUID"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(175, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(84, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Gloss Language"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(180, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Sign Language"
        '
        'BtnAccept
        '
        Me.BtnAccept.Location = New System.Drawing.Point(224, 76)
        Me.BtnAccept.Name = "BtnAccept"
        Me.BtnAccept.Size = New System.Drawing.Size(75, 23)
        Me.BtnAccept.TabIndex = 12
        Me.BtnAccept.Text = "Accept"
        Me.BtnAccept.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(306, 76)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 13
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'UISignLanguagesTableAdapter
        '
        Me.UISignLanguagesTableAdapter.ClearBeforeFill = True
        '
        'UICulturesTableAdapter
        '
        Me.UICulturesTableAdapter.ClearBeforeFill = True
        '
        'SWLayoutControlProperties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(391, 101)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnAccept)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LBDescSignWriterGUID)
        Me.Controls.Add(Me.LBDescIDSignPuddle)
        Me.Controls.Add(Me.LBGlosses)
        Me.Controls.Add(Me.LBGloss)
        Me.Controls.Add(Me.LBSignWriterGUID)
        Me.Controls.Add(Me.LBSignPuddleID)
        Me.Controls.Add(Me.TBGlosses)
        Me.Controls.Add(Me.TBGloss)
        Me.Controls.Add(Me.CBSLanguage)
        Me.Controls.Add(Me.CBGlossLang)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SWLayoutControlProperties"
        Me.Text = "SignWriter Studio™  Sign Properties"
        CType(Me.UICulturesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.swsUIDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UISignLanguagesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBGlossLang As System.Windows.Forms.ComboBox
    Friend WithEvents CBSLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents TBGloss As System.Windows.Forms.TextBox
    Friend WithEvents TBGlosses As System.Windows.Forms.TextBox
    Friend WithEvents LBSignPuddleID As System.Windows.Forms.Label
    Friend WithEvents LBSignWriterGUID As System.Windows.Forms.Label
    Friend WithEvents LBGloss As System.Windows.Forms.Label
    Friend WithEvents LBGlosses As System.Windows.Forms.Label
    Friend WithEvents LBDescIDSignPuddle As System.Windows.Forms.Label
    Friend WithEvents LBDescSignWriterGUID As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BtnAccept As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents swsUIDataSet As UI.swsuiDataSet
    Friend WithEvents UISignLanguagesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UISignLanguagesTableAdapter As UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
    Friend WithEvents UICulturesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UICulturesTableAdapter As UI.swsuiDataSetTableAdapters.UICulturesTableAdapter
End Class
