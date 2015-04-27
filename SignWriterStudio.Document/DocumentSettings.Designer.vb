<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DocumentSettings
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DocumentSettings))
        Me.LBMarginBottom = New System.Windows.Forms.Label()
        Me.TBMarginBottom = New System.Windows.Forms.TextBox()
        Me.BtnApplytoAllPadding = New System.Windows.Forms.Button()
        Me.CBShowLines = New System.Windows.Forms.CheckBox()
        Me.TBMinLeft = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TBMinCenter = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TBMinRight = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TBMinCol = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TBSpaceBetween = New System.Windows.Forms.TextBox()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.BtnBackgroundcolor = New System.Windows.Forms.Button()
        Me.BtnAccept = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'LBMarginBottom
        '
        Me.LBMarginBottom.AutoSize = True
        Me.LBMarginBottom.Location = New System.Drawing.Point(321, 59)
        Me.LBMarginBottom.Name = "LBMarginBottom"
        Me.LBMarginBottom.Size = New System.Drawing.Size(118, 13)
        Me.LBMarginBottom.TabIndex = 6
        Me.LBMarginBottom.Text = "Bottom Margin All Signs"
        Me.LBMarginBottom.Visible = False
        '
        'TBMarginBottom
        '
        Me.TBMarginBottom.Location = New System.Drawing.Point(451, 57)
        Me.TBMarginBottom.Name = "TBMarginBottom"
        Me.TBMarginBottom.Size = New System.Drawing.Size(34, 20)
        Me.TBMarginBottom.TabIndex = 5
        Me.TBMarginBottom.Visible = False
        '
        'BtnApplytoAllPadding
        '
        Me.BtnApplytoAllPadding.Location = New System.Drawing.Point(472, 85)
        Me.BtnApplytoAllPadding.Name = "BtnApplytoAllPadding"
        Me.BtnApplytoAllPadding.Size = New System.Drawing.Size(76, 23)
        Me.BtnApplytoAllPadding.TabIndex = 11
        Me.BtnApplytoAllPadding.Text = "Apply to All"
        Me.BtnApplytoAllPadding.UseVisualStyleBackColor = True
        Me.BtnApplytoAllPadding.Visible = False
        '
        'CBShowLines
        '
        Me.CBShowLines.AutoSize = True
        Me.CBShowLines.Location = New System.Drawing.Point(12, 12)
        Me.CBShowLines.Name = "CBShowLines"
        Me.CBShowLines.Size = New System.Drawing.Size(119, 17)
        Me.CBShowLines.TabIndex = 12
        Me.CBShowLines.Text = "Show Vertical Lines"
        Me.CBShowLines.UseVisualStyleBackColor = True
        '
        'TBMinLeft
        '
        Me.TBMinLeft.Location = New System.Drawing.Point(270, 12)
        Me.TBMinLeft.Name = "TBMinLeft"
        Me.TBMinLeft.Size = New System.Drawing.Size(34, 20)
        Me.TBMinLeft.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(154, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Min Left Lane Width"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(154, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Min CenterLane Width"
        '
        'TBMinCenter
        '
        Me.TBMinCenter.Location = New System.Drawing.Point(270, 35)
        Me.TBMinCenter.Name = "TBMinCenter"
        Me.TBMinCenter.Size = New System.Drawing.Size(34, 20)
        Me.TBMinCenter.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(154, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Min Right Lane Width"
        '
        'TBMinRight
        '
        Me.TBMinRight.Location = New System.Drawing.Point(270, 56)
        Me.TBMinRight.Name = "TBMinRight"
        Me.TBMinRight.Size = New System.Drawing.Size(34, 20)
        Me.TBMinRight.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(321, 13)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Min Column Width"
        Me.Label4.Visible = False
        '
        'TBMinCol
        '
        Me.TBMinCol.Location = New System.Drawing.Point(451, 10)
        Me.TBMinCol.Name = "TBMinCol"
        Me.TBMinCol.Size = New System.Drawing.Size(34, 20)
        Me.TBMinCol.TabIndex = 19
        Me.TBMinCol.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(321, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(125, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Space Between columns"
        Me.Label5.Visible = False
        '
        'TBSpaceBetween
        '
        Me.TBSpaceBetween.Location = New System.Drawing.Point(451, 31)
        Me.TBSpaceBetween.Name = "TBSpaceBetween"
        Me.TBSpaceBetween.Size = New System.Drawing.Size(34, 20)
        Me.TBSpaceBetween.TabIndex = 21
        Me.TBSpaceBetween.Visible = False
        '
        'BtnBackgroundcolor
        '
        Me.BtnBackgroundcolor.Location = New System.Drawing.Point(12, 35)
        Me.BtnBackgroundcolor.Name = "BtnBackgroundcolor"
        Me.BtnBackgroundcolor.Size = New System.Drawing.Size(129, 23)
        Me.BtnBackgroundcolor.TabIndex = 23
        Me.BtnBackgroundcolor.Text = "Background color"
        Me.BtnBackgroundcolor.UseVisualStyleBackColor = True
        '
        'BtnAccept
        '
        Me.BtnAccept.Location = New System.Drawing.Point(58, 79)
        Me.BtnAccept.Name = "BtnAccept"
        Me.BtnAccept.Size = New System.Drawing.Size(75, 23)
        Me.BtnAccept.TabIndex = 24
        Me.BtnAccept.Text = "&Accept"
        Me.BtnAccept.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(156, 79)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 23)
        Me.BtnCancel.TabIndex = 25
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'DocumentSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(315, 112)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnAccept)
        Me.Controls.Add(Me.BtnBackgroundcolor)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TBSpaceBetween)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TBMinCol)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TBMinRight)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TBMinCenter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TBMinLeft)
        Me.Controls.Add(Me.CBShowLines)
        Me.Controls.Add(Me.BtnApplytoAllPadding)
        Me.Controls.Add(Me.LBMarginBottom)
        Me.Controls.Add(Me.TBMarginBottom)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DocumentSettings"
        Me.Text = "SignWriter Studio™ Document Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LBMarginBottom As System.Windows.Forms.Label
    Friend WithEvents TBMarginBottom As System.Windows.Forms.TextBox
    Friend WithEvents BtnApplytoAllPadding As System.Windows.Forms.Button
    Friend WithEvents CBShowLines As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TBMinCenter As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TBMinRight As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TBMinCol As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TBSpaceBetween As System.Windows.Forms.TextBox
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents BtnBackgroundcolor As System.Windows.Forms.Button
    Friend WithEvents TBMinLeft As System.Windows.Forms.TextBox
    Friend WithEvents BtnAccept As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
End Class
