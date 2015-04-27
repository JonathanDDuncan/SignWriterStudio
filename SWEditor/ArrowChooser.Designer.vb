Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.General
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ArrowChooser
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
        Me.CBFill = New System.Windows.Forms.ComboBox()
        Me.GBRotations = New System.Windows.Forms.GroupBox()
        Me.HP5 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP1 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP2 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP3 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP4 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP6 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP7 = New SignWriterStudio.General.RadioButtonFull()
        Me.HP8 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP5 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP1 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP2 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP3 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP4 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP6 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP7 = New SignWriterStudio.General.RadioButtonFull()
        Me.VP8 = New SignWriterStudio.General.RadioButtonFull()
        Me.PBHorizHand = New System.Windows.Forms.PictureBox()
        Me.PBVertHand = New System.Windows.Forms.PictureBox()
        Me.CBFlip = New System.Windows.Forms.CheckBox()
        Me.GBRotations.SuspendLayout()
        CType(Me.PBHorizHand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBVertHand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CBFill
        '
        Me.CBFill.FormattingEnabled = True
        Me.CBFill.Items.AddRange(New Object() {"Right Hand", "Left Hand", "Superposed Hands", "No Arrow"})
        Me.CBFill.Location = New System.Drawing.Point(31, 3)
        Me.CBFill.Name = "CBFill"
        Me.CBFill.Size = New System.Drawing.Size(121, 21)
        Me.CBFill.TabIndex = 55
        '
        'GBRotations
        '
        Me.GBRotations.Controls.Add(Me.HP5)
        Me.GBRotations.Controls.Add(Me.HP1)
        Me.GBRotations.Controls.Add(Me.HP2)
        Me.GBRotations.Controls.Add(Me.HP3)
        Me.GBRotations.Controls.Add(Me.HP4)
        Me.GBRotations.Controls.Add(Me.HP6)
        Me.GBRotations.Controls.Add(Me.HP7)
        Me.GBRotations.Controls.Add(Me.HP8)
        Me.GBRotations.Controls.Add(Me.VP5)
        Me.GBRotations.Controls.Add(Me.VP1)
        Me.GBRotations.Controls.Add(Me.VP2)
        Me.GBRotations.Controls.Add(Me.VP3)
        Me.GBRotations.Controls.Add(Me.VP4)
        Me.GBRotations.Controls.Add(Me.VP6)
        Me.GBRotations.Controls.Add(Me.VP7)
        Me.GBRotations.Controls.Add(Me.VP8)
        Me.GBRotations.Controls.Add(Me.PBHorizHand)
        Me.GBRotations.Controls.Add(Me.PBVertHand)
        Me.GBRotations.Location = New System.Drawing.Point(13, 105)
        Me.GBRotations.Name = "GBRotations"
        Me.GBRotations.Size = New System.Drawing.Size(150, 297)
        Me.GBRotations.TabIndex = 57
        Me.GBRotations.TabStop = False
        Me.GBRotations.Text = "Plane"
        '
        'HP5
        '
        Me.HP5.CheckAlign = System.Drawing.ContentAlignment.TopCenter
        Me.HP5.Location = New System.Drawing.Point(67, 233)
        Me.HP5.Name = "HP5"
        Me.HP5.Size = New System.Drawing.Size(19, 30)
        Me.HP5.TabIndex = 14
        Me.HP5.Tag = "5"
        Me.HP5.Text = "2"
        Me.HP5.UseVisualStyleBackColor = True
        '
        'HP1
        '
        Me.HP1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.HP1.Location = New System.Drawing.Point(67, 170)
        Me.HP1.Name = "HP1"
        Me.HP1.Size = New System.Drawing.Size(15, 30)
        Me.HP1.TabIndex = 10
        Me.HP1.Tag = "1"
        Me.HP1.Text = "8"
        Me.HP1.UseVisualStyleBackColor = True
        '
        'HP2
        '
        Me.HP2.CheckAlign = System.Drawing.ContentAlignment.BottomRight
        Me.HP2.Location = New System.Drawing.Point(28, 173)
        Me.HP2.Name = "HP2"
        Me.HP2.Size = New System.Drawing.Size(30, 30)
        Me.HP2.TabIndex = 11
        Me.HP2.Tag = "2"
        Me.HP2.Text = "7"
        Me.HP2.UseVisualStyleBackColor = True
        '
        'HP3
        '
        Me.HP3.AutoSize = True
        Me.HP3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.HP3.Location = New System.Drawing.Point(7, 201)
        Me.HP3.Name = "HP3"
        Me.HP3.Size = New System.Drawing.Size(31, 17)
        Me.HP3.TabIndex = 12
        Me.HP3.Tag = "3"
        Me.HP3.Text = "4"
        Me.HP3.UseVisualStyleBackColor = True
        '
        'HP4
        '
        Me.HP4.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.HP4.Location = New System.Drawing.Point(16, 223)
        Me.HP4.Name = "HP4"
        Me.HP4.Size = New System.Drawing.Size(27, 48)
        Me.HP4.TabIndex = 13
        Me.HP4.Tag = "4"
        Me.HP4.Text = "1"
        Me.HP4.UseVisualStyleBackColor = True
        '
        'HP6
        '
        Me.HP6.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.HP6.Location = New System.Drawing.Point(109, 224)
        Me.HP6.Name = "HP6"
        Me.HP6.Size = New System.Drawing.Size(28, 39)
        Me.HP6.TabIndex = 15
        Me.HP6.Tag = "6"
        Me.HP6.Text = "3"
        Me.HP6.UseVisualStyleBackColor = True
        '
        'HP7
        '
        Me.HP7.AutoSize = True
        Me.HP7.Location = New System.Drawing.Point(111, 203)
        Me.HP7.Name = "HP7"
        Me.HP7.Size = New System.Drawing.Size(31, 17)
        Me.HP7.TabIndex = 16
        Me.HP7.Tag = "7"
        Me.HP7.Text = "6"
        Me.HP7.UseVisualStyleBackColor = True
        '
        'HP8
        '
        Me.HP8.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.HP8.Location = New System.Drawing.Point(94, 173)
        Me.HP8.Name = "HP8"
        Me.HP8.Size = New System.Drawing.Size(30, 30)
        Me.HP8.TabIndex = 17
        Me.HP8.Tag = "8"
        Me.HP8.Text = "9"
        Me.HP8.UseVisualStyleBackColor = True
        '
        'VP5
        '
        Me.VP5.CheckAlign = System.Drawing.ContentAlignment.TopCenter
        Me.VP5.Location = New System.Drawing.Point(68, 107)
        Me.VP5.Name = "VP5"
        Me.VP5.Size = New System.Drawing.Size(17, 30)
        Me.VP5.TabIndex = 5
        Me.VP5.Tag = "5"
        Me.VP5.Text = "2"
        Me.VP5.UseVisualStyleBackColor = True
        '
        'VP1
        '
        Me.VP1.AutoSize = True
        Me.VP1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.VP1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.VP1.Location = New System.Drawing.Point(67, 8)
        Me.VP1.Name = "VP1"
        Me.VP1.Size = New System.Drawing.Size(17, 30)
        Me.VP1.TabIndex = 1
        Me.VP1.Tag = "1"
        Me.VP1.Text = "8"
        Me.VP1.UseVisualStyleBackColor = True
        '
        'VP2
        '
        Me.VP2.CheckAlign = System.Drawing.ContentAlignment.BottomRight
        Me.VP2.Location = New System.Drawing.Point(23, 19)
        Me.VP2.Name = "VP2"
        Me.VP2.Size = New System.Drawing.Size(30, 30)
        Me.VP2.TabIndex = 2
        Me.VP2.Tag = "2"
        Me.VP2.Text = "7"
        Me.VP2.UseVisualStyleBackColor = True
        '
        'VP3
        '
        Me.VP3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VP3.Location = New System.Drawing.Point(8, 63)
        Me.VP3.Name = "VP3"
        Me.VP3.Size = New System.Drawing.Size(34, 19)
        Me.VP3.TabIndex = 3
        Me.VP3.Tag = "3"
        Me.VP3.Text = "4"
        Me.VP3.UseVisualStyleBackColor = True
        '
        'VP4
        '
        Me.VP4.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.VP4.Location = New System.Drawing.Point(20, 93)
        Me.VP4.Name = "VP4"
        Me.VP4.Size = New System.Drawing.Size(30, 30)
        Me.VP4.TabIndex = 4
        Me.VP4.Tag = "4"
        Me.VP4.Text = "1"
        Me.VP4.UseVisualStyleBackColor = True
        '
        'VP6
        '
        Me.VP6.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.VP6.Location = New System.Drawing.Point(99, 95)
        Me.VP6.Name = "VP6"
        Me.VP6.Size = New System.Drawing.Size(26, 30)
        Me.VP6.TabIndex = 6
        Me.VP6.Tag = "6"
        Me.VP6.Text = "3"
        Me.VP6.UseVisualStyleBackColor = True
        '
        'VP7
        '
        Me.VP7.Location = New System.Drawing.Point(108, 63)
        Me.VP7.Name = "VP7"
        Me.VP7.Size = New System.Drawing.Size(26, 20)
        Me.VP7.TabIndex = 7
        Me.VP7.Tag = "7"
        Me.VP7.Text = "6"
        Me.VP7.UseVisualStyleBackColor = True
        '
        'VP8
        '
        Me.VP8.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.VP8.Location = New System.Drawing.Point(98, 20)
        Me.VP8.Name = "VP8"
        Me.VP8.Size = New System.Drawing.Size(30, 30)
        Me.VP8.TabIndex = 8
        Me.VP8.Tag = "8"
        Me.VP8.Text = "9"
        Me.VP8.UseVisualStyleBackColor = True
        '
        'PBHorizHand
        '
        Me.PBHorizHand.BackColor = System.Drawing.Color.Transparent
        Me.PBHorizHand.Image = Global.SignWriterStudio.SWEditor.My.Resources.Resources.ArrowFloorPlanePersp
        Me.PBHorizHand.InitialImage = Nothing
        Me.PBHorizHand.Location = New System.Drawing.Point(22, 201)
        Me.PBHorizHand.Name = "PBHorizHand"
        Me.PBHorizHand.Size = New System.Drawing.Size(100, 30)
        Me.PBHorizHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBHorizHand.TabIndex = 19
        Me.PBHorizHand.TabStop = False
        '
        'PBVertHand
        '
        Me.PBVertHand.BackColor = System.Drawing.Color.Transparent
        Me.PBVertHand.Image = Global.SignWriterStudio.SWEditor.My.Resources.Resources.ArrowWallPlane
        Me.PBVertHand.InitialImage = Nothing
        Me.PBVertHand.Location = New System.Drawing.Point(44, 43)
        Me.PBVertHand.Name = "PBVertHand"
        Me.PBVertHand.Size = New System.Drawing.Size(60, 60)
        Me.PBVertHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBVertHand.TabIndex = 18
        Me.PBVertHand.TabStop = False
        '
        'CBFlip
        '
        Me.CBFlip.AutoSize = True
        Me.CBFlip.Location = New System.Drawing.Point(54, 68)
        Me.CBFlip.Name = "CBFlip"
        Me.CBFlip.Size = New System.Drawing.Size(42, 17)
        Me.CBFlip.TabIndex = 58
        Me.CBFlip.Text = "&Flip"
        Me.CBFlip.UseVisualStyleBackColor = True
        '
        'ArrowChooser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CBFlip)
        Me.Controls.Add(Me.CBFill)
        Me.Controls.Add(Me.GBRotations)
        Me.Name = "ArrowChooser"
        Me.Size = New System.Drawing.Size(175, 412)
        Me.GBRotations.ResumeLayout(False)
        Me.GBRotations.PerformLayout()
        CType(Me.PBHorizHand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PBVertHand, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CBFill As System.Windows.Forms.ComboBox
    Friend WithEvents GBRotations As System.Windows.Forms.GroupBox
    Friend WithEvents HP5 As RadioButtonFull
    Friend WithEvents HP1 As RadioButtonFull
    Friend WithEvents HP2 As RadioButtonFull
    Friend WithEvents HP3 As RadioButtonFull
    Friend WithEvents HP4 As RadioButtonFull
    Friend WithEvents HP6 As RadioButtonFull
    Friend WithEvents HP7 As RadioButtonFull
    Friend WithEvents HP8 As RadioButtonFull
    Friend WithEvents VP5 As RadioButtonFull
    Friend WithEvents VP1 As RadioButtonFull
    Friend WithEvents VP2 As RadioButtonFull
    Friend WithEvents VP3 As RadioButtonFull
    Friend WithEvents VP4 As RadioButtonFull
    Friend WithEvents VP6 As RadioButtonFull
    Friend WithEvents VP7 As RadioButtonFull
    Friend WithEvents VP8 As RadioButtonFull
    Friend WithEvents PBHorizHand As System.Windows.Forms.PictureBox
    Friend WithEvents PBVertHand As System.Windows.Forms.PictureBox
    Friend WithEvents CBFlip As System.Windows.Forms.CheckBox

End Class
