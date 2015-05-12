<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TagTreeView
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
        Me.tvTags = New System.Windows.Forms.TreeView()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.tbDescription = New System.Windows.Forms.TextBox()
        Me.btnEditOk = New System.Windows.Forms.Button()
        Me.btnEditCancel = New System.Windows.Forms.Button()
        Me.gbEdit = New System.Windows.Forms.GroupBox()
        Me.btnColor = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbAbreviation = New System.Windows.Forms.TextBox()
        Me.cbTagGroup = New System.Windows.Forms.CheckBox()
        Me.SettingsDataSet1 = New Settings.SettingsDataSet()
        Me.btnMoveUp = New System.Windows.Forms.Button()
        Me.btnMoveDown = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.gbEdit.SuspendLayout()
        CType(Me.SettingsDataSet1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tvTags
        '
        Me.tvTags.Location = New System.Drawing.Point(3, 3)
        Me.tvTags.Name = "tvTags"
        Me.tvTags.Size = New System.Drawing.Size(241, 328)
        Me.tvTags.TabIndex = 0
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(250, 1)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(250, 30)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnRemove.TabIndex = 2
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(250, 117)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 3
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(26, 337)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(141, 337)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Location = New System.Drawing.Point(6, 15)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(60, 13)
        Me.lblDescription.TabIndex = 9
        Me.lblDescription.Text = "Description"
        '
        'tbDescription
        '
        Me.tbDescription.Location = New System.Drawing.Point(6, 32)
        Me.tbDescription.Name = "tbDescription"
        Me.tbDescription.Size = New System.Drawing.Size(94, 20)
        Me.tbDescription.TabIndex = 10
        '
        'btnEditOk
        '
        Me.btnEditOk.Location = New System.Drawing.Point(16, 161)
        Me.btnEditOk.Name = "btnEditOk"
        Me.btnEditOk.Size = New System.Drawing.Size(75, 23)
        Me.btnEditOk.TabIndex = 13
        Me.btnEditOk.Text = "OK"
        Me.btnEditOk.UseVisualStyleBackColor = True
        '
        'btnEditCancel
        '
        Me.btnEditCancel.Location = New System.Drawing.Point(16, 190)
        Me.btnEditCancel.Name = "btnEditCancel"
        Me.btnEditCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnEditCancel.TabIndex = 14
        Me.btnEditCancel.Text = "Cancel"
        Me.btnEditCancel.UseVisualStyleBackColor = True
        '
        'gbEdit
        '
        Me.gbEdit.Controls.Add(Me.btnColor)
        Me.gbEdit.Controls.Add(Me.Label1)
        Me.gbEdit.Controls.Add(Me.tbAbreviation)
        Me.gbEdit.Controls.Add(Me.cbTagGroup)
        Me.gbEdit.Controls.Add(Me.btnEditCancel)
        Me.gbEdit.Controls.Add(Me.btnEditOk)
        Me.gbEdit.Controls.Add(Me.lblDescription)
        Me.gbEdit.Controls.Add(Me.tbDescription)
        Me.gbEdit.Location = New System.Drawing.Point(250, 146)
        Me.gbEdit.Name = "gbEdit"
        Me.gbEdit.Size = New System.Drawing.Size(106, 224)
        Me.gbEdit.TabIndex = 15
        Me.gbEdit.TabStop = False
        Me.gbEdit.Text = "Edit"
        Me.gbEdit.Visible = False
        '
        'btnColor
        '
        Me.btnColor.Location = New System.Drawing.Point(16, 121)
        Me.btnColor.Name = "btnColor"
        Me.btnColor.Size = New System.Drawing.Size(75, 23)
        Me.btnColor.TabIndex = 18
        Me.btnColor.Text = "Color"
        Me.btnColor.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Abreviation"
        '
        'tbAbreviation
        '
        Me.tbAbreviation.Location = New System.Drawing.Point(6, 72)
        Me.tbAbreviation.Name = "tbAbreviation"
        Me.tbAbreviation.Size = New System.Drawing.Size(94, 20)
        Me.tbAbreviation.TabIndex = 17
        '
        'cbTagGroup
        '
        Me.cbTagGroup.AutoSize = True
        Me.cbTagGroup.Location = New System.Drawing.Point(6, 98)
        Me.cbTagGroup.Name = "cbTagGroup"
        Me.cbTagGroup.Size = New System.Drawing.Size(77, 17)
        Me.cbTagGroup.TabIndex = 15
        Me.cbTagGroup.Text = "Tag Group"
        Me.cbTagGroup.UseVisualStyleBackColor = True
        Me.cbTagGroup.Visible = False
        '
        'SettingsDataSet1
        '
        Me.SettingsDataSet1.DataSetName = "SettingsDataSet"
        Me.SettingsDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnMoveUp
        '
        Me.btnMoveUp.Location = New System.Drawing.Point(250, 59)
        Me.btnMoveUp.Name = "btnMoveUp"
        Me.btnMoveUp.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveUp.TabIndex = 16
        Me.btnMoveUp.Text = "Move Up"
        Me.btnMoveUp.UseVisualStyleBackColor = True
        '
        'btnMoveDown
        '
        Me.btnMoveDown.Location = New System.Drawing.Point(250, 88)
        Me.btnMoveDown.Name = "btnMoveDown"
        Me.btnMoveDown.Size = New System.Drawing.Size(75, 23)
        Me.btnMoveDown.TabIndex = 17
        Me.btnMoveDown.Text = "Move Down"
        Me.btnMoveDown.UseVisualStyleBackColor = True
        '
        'TagTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnMoveDown)
        Me.Controls.Add(Me.btnMoveUp)
        Me.Controls.Add(Me.gbEdit)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.tvTags)
        Me.Name = "TagTreeView"
        Me.Size = New System.Drawing.Size(376, 379)
        Me.gbEdit.ResumeLayout(False)
        Me.gbEdit.PerformLayout()
        CType(Me.SettingsDataSet1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tvTags As System.Windows.Forms.TreeView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents tbDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnEditOk As System.Windows.Forms.Button
    Friend WithEvents btnEditCancel As System.Windows.Forms.Button
    Friend WithEvents gbEdit As System.Windows.Forms.GroupBox
    Friend WithEvents SettingsDataSet1 As Settings.SettingsDataSet
    Friend WithEvents btnMoveUp As System.Windows.Forms.Button
    Friend WithEvents btnMoveDown As System.Windows.Forms.Button
    Friend WithEvents cbTagGroup As System.Windows.Forms.CheckBox
    Friend WithEvents btnColor As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbAbreviation As System.Windows.Forms.TextBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog

End Class
