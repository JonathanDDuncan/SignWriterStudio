Imports SignWriterStudio.General
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HandChooser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HandChooser))
        Me.GBFills = New System.Windows.Forms.GroupBox()
        Me.PBHandR4 = New System.Windows.Forms.PictureBox()
        Me.PBHandR3 = New System.Windows.Forms.PictureBox()
        Me.PBHandR2 = New System.Windows.Forms.PictureBox()
        Me.PBHandR1 = New System.Windows.Forms.PictureBox()
        Me.HandR4 = New SignWriterStudio.General.RadioButtonFull()
        Me.HandR1 = New SignWriterStudio.General.RadioButtonFull()
        Me.HandR2 = New SignWriterStudio.General.RadioButtonFull()
        Me.HandR3 = New SignWriterStudio.General.RadioButtonFull()
        Me.CBHand = New System.Windows.Forms.ComboBox()
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
        Me.GBFills.SuspendLayout()
        CType(Me.PBHandR4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBHandR3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBHandR2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBHandR1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GBRotations.SuspendLayout()
        CType(Me.PBHorizHand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PBVertHand, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GBFills
        '
        Me.GBFills.Controls.Add(Me.PBHandR4)
        Me.GBFills.Controls.Add(Me.PBHandR3)
        Me.GBFills.Controls.Add(Me.PBHandR2)
        Me.GBFills.Controls.Add(Me.PBHandR1)
        Me.GBFills.Controls.Add(Me.HandR4)
        Me.GBFills.Controls.Add(Me.HandR1)
        Me.GBFills.Controls.Add(Me.HandR2)
        Me.GBFills.Controls.Add(Me.HandR3)
        Me.GBFills.Location = New System.Drawing.Point(3, 30)
        Me.GBFills.Name = "GBFills"
        Me.GBFills.Size = New System.Drawing.Size(168, 69)
        Me.GBFills.TabIndex = 56
        Me.GBFills.TabStop = False
        Me.GBFills.Text = "Palm Facing"
        '
        'PBHandR4
        '
        Me.PBHandR4.Location = New System.Drawing.Point(8, 34)
        Me.PBHandR4.Name = "PBHandR4"
        Me.PBHandR4.Size = New System.Drawing.Size(35, 35)
        Me.PBHandR4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PBHandR4.TabIndex = 27
        Me.PBHandR4.TabStop = False
        '
        'PBHandR3
        '
        Me.PBHandR3.Location = New System.Drawing.Point(127, 34)
        Me.PBHandR3.Name = "PBHandR3"
        Me.PBHandR3.Size = New System.Drawing.Size(35, 35)
        Me.PBHandR3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PBHandR3.TabIndex = 26
        Me.PBHandR3.TabStop = False
        '
        'PBHandR2
        '
        Me.PBHandR2.Location = New System.Drawing.Point(88, 34)
        Me.PBHandR2.Name = "PBHandR2"
        Me.PBHandR2.Size = New System.Drawing.Size(35, 35)
        Me.PBHandR2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PBHandR2.TabIndex = 25
        Me.PBHandR2.TabStop = False
        '
        'PBHandR1
        '
        Me.PBHandR1.Location = New System.Drawing.Point(49, 34)
        Me.PBHandR1.Name = "PBHandR1"
        Me.PBHandR1.Size = New System.Drawing.Size(35, 35)
        Me.PBHandR1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PBHandR1.TabIndex = 24
        Me.PBHandR1.TabStop = False
        '
        'HandR4
        '
        Me.HandR4.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HandR4.Location = New System.Drawing.Point(12, 13)
        Me.HandR4.Margin = New System.Windows.Forms.Padding(0)
        Me.HandR4.Name = "HandR4"
        Me.HandR4.Size = New System.Drawing.Size(36, 25)
        Me.HandR4.TabIndex = 23
        Me.HandR4.Tag = "4"
        Me.HandR4.Text = "/"
        Me.HandR4.UseVisualStyleBackColor = True
        '
        'HandR1
        '
        Me.HandR1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.HandR1.Location = New System.Drawing.Point(53, 12)
        Me.HandR1.Name = "HandR1"
        Me.HandR1.Size = New System.Drawing.Size(29, 28)
        Me.HandR1.TabIndex = 17
        Me.HandR1.Tag = "1"
        Me.HandR1.Text = "*"
        Me.HandR1.UseVisualStyleBackColor = True
        '
        'HandR2
        '
        Me.HandR2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.HandR2.Location = New System.Drawing.Point(127, 10)
        Me.HandR2.Name = "HandR2"
        Me.HandR2.Size = New System.Drawing.Size(32, 31)
        Me.HandR2.TabIndex = 19
        Me.HandR2.Tag = "2"
        Me.HandR2.Text = "+"
        Me.HandR2.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.HandR2.UseVisualStyleBackColor = True
        '
        'HandR3
        '
        Me.HandR3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.HandR3.Location = New System.Drawing.Point(89, 10)
        Me.HandR3.Name = "HandR3"
        Me.HandR3.Size = New System.Drawing.Size(32, 31)
        Me.HandR3.TabIndex = 28
        Me.HandR3.Tag = "3"
        Me.HandR3.Text = "-"
        Me.HandR3.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.HandR3.UseVisualStyleBackColor = True
        '
        'CBHand
        '
        Me.CBHand.FormattingEnabled = True
        Me.CBHand.Items.AddRange(New Object() {"Right Hand", "Left Hand"})
        Me.CBHand.Location = New System.Drawing.Point(28, 3)
        Me.CBHand.Name = "CBHand"
        Me.CBHand.Size = New System.Drawing.Size(121, 21)
        Me.CBHand.TabIndex = 55
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
        Me.HP5.Location = New System.Drawing.Point(60, 233)
        Me.HP5.Name = "HP5"
        Me.HP5.Size = New System.Drawing.Size(19, 30)
        Me.HP5.TabIndex = 13
        Me.HP5.Tag = "5"
        Me.HP5.Text = "2"
        Me.HP5.UseVisualStyleBackColor = True
        '
        'HP1
        '
        Me.HP1.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.HP1.Location = New System.Drawing.Point(64, 170)
        Me.HP1.Name = "HP1"
        Me.HP1.Size = New System.Drawing.Size(15, 30)
        Me.HP1.TabIndex = 9
        Me.HP1.Tag = "1"
        Me.HP1.Text = "8"
        Me.HP1.UseVisualStyleBackColor = True
        '
        'HP2
        '
        Me.HP2.CheckAlign = System.Drawing.ContentAlignment.BottomRight
        Me.HP2.Location = New System.Drawing.Point(23, 173)
        Me.HP2.Name = "HP2"
        Me.HP2.Size = New System.Drawing.Size(30, 30)
        Me.HP2.TabIndex = 16
        Me.HP2.Tag = "2"
        Me.HP2.Text = "7"
        Me.HP2.UseVisualStyleBackColor = True
        '
        'HP3
        '
        Me.HP3.AutoSize = True
        Me.HP3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.HP3.Location = New System.Drawing.Point(7, 206)
        Me.HP3.Name = "HP3"
        Me.HP3.Size = New System.Drawing.Size(31, 17)
        Me.HP3.TabIndex = 15
        Me.HP3.Tag = "3"
        Me.HP3.Text = "4"
        Me.HP3.UseVisualStyleBackColor = True
        '
        'HP4
        '
        Me.HP4.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.HP4.Location = New System.Drawing.Point(19, 224)
        Me.HP4.Name = "HP4"
        Me.HP4.Size = New System.Drawing.Size(27, 48)
        Me.HP4.TabIndex = 14
        Me.HP4.Tag = "4"
        Me.HP4.Text = "1"
        Me.HP4.UseVisualStyleBackColor = True
        '
        'HP6
        '
        Me.HP6.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.HP6.Location = New System.Drawing.Point(96, 226)
        Me.HP6.Name = "HP6"
        Me.HP6.Size = New System.Drawing.Size(28, 39)
        Me.HP6.TabIndex = 12
        Me.HP6.Tag = "6"
        Me.HP6.Text = "3"
        Me.HP6.UseVisualStyleBackColor = True
        '
        'HP7
        '
        Me.HP7.AutoSize = True
        Me.HP7.Location = New System.Drawing.Point(105, 206)
        Me.HP7.Name = "HP7"
        Me.HP7.Size = New System.Drawing.Size(31, 17)
        Me.HP7.TabIndex = 11
        Me.HP7.Tag = "7"
        Me.HP7.Text = "6"
        Me.HP7.UseVisualStyleBackColor = True
        '
        'HP8
        '
        Me.HP8.CheckAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.HP8.Location = New System.Drawing.Point(92, 175)
        Me.HP8.Name = "HP8"
        Me.HP8.Size = New System.Drawing.Size(30, 30)
        Me.HP8.TabIndex = 10
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
        Me.VP2.TabIndex = 8
        Me.VP2.Tag = "2"
        Me.VP2.Text = "7"
        Me.VP2.UseVisualStyleBackColor = True
        '
        'VP3
        '
        Me.VP3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.VP3.Location = New System.Drawing.Point(10, 63)
        Me.VP3.Name = "VP3"
        Me.VP3.Size = New System.Drawing.Size(34, 19)
        Me.VP3.TabIndex = 7
        Me.VP3.Tag = "3"
        Me.VP3.Text = "4"
        Me.VP3.UseVisualStyleBackColor = True
        '
        'VP4
        '
        Me.VP4.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.VP4.Location = New System.Drawing.Point(22, 93)
        Me.VP4.Name = "VP4"
        Me.VP4.Size = New System.Drawing.Size(30, 30)
        Me.VP4.TabIndex = 6
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
        Me.VP6.TabIndex = 4
        Me.VP6.Tag = "6"
        Me.VP6.Text = "3"
        Me.VP6.UseVisualStyleBackColor = True
        '
        'VP7
        '
        Me.VP7.Location = New System.Drawing.Point(108, 63)
        Me.VP7.Name = "VP7"
        Me.VP7.Size = New System.Drawing.Size(26, 20)
        Me.VP7.TabIndex = 3
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
        Me.VP8.TabIndex = 2
        Me.VP8.Tag = "8"
        Me.VP8.Text = "9"
        Me.VP8.UseVisualStyleBackColor = True
        '
        'PBHorizHand
        '
        Me.PBHorizHand.BackColor = System.Drawing.Color.Transparent
        Me.PBHorizHand.Image = CType(resources.GetObject("PBHorizHand.Image"), System.Drawing.Image)
        Me.PBHorizHand.InitialImage = Nothing
        Me.PBHorizHand.Location = New System.Drawing.Point(39, 202)
        Me.PBHorizHand.Name = "PBHorizHand"
        Me.PBHorizHand.Size = New System.Drawing.Size(65, 30)
        Me.PBHorizHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PBHorizHand.TabIndex = 19
        Me.PBHorizHand.TabStop = False
        '
        'PBVertHand
        '
        Me.PBVertHand.BackColor = System.Drawing.Color.Transparent
        Me.PBVertHand.Image = CType(resources.GetObject("PBVertHand.Image"), System.Drawing.Image)
        Me.PBVertHand.InitialImage = Nothing
        Me.PBVertHand.Location = New System.Drawing.Point(43, 39)
        Me.PBVertHand.Name = "PBVertHand"
        Me.PBVertHand.Size = New System.Drawing.Size(65, 65)
        Me.PBVertHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PBVertHand.TabIndex = 18
        Me.PBVertHand.TabStop = False
        '
        'HandChooser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GBFills)
        Me.Controls.Add(Me.CBHand)
        Me.Controls.Add(Me.GBRotations)
        Me.Name = "HandChooser"
        Me.Size = New System.Drawing.Size(175, 412)
        Me.GBFills.ResumeLayout(false)
        CType(Me.PBHandR4,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PBHandR3,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PBHandR2,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PBHandR1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GBRotations.ResumeLayout(false)
        Me.GBRotations.PerformLayout
        CType(Me.PBHorizHand,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PBVertHand,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents GBFills As System.Windows.Forms.GroupBox
    Friend WithEvents PBHandR4 As System.Windows.Forms.PictureBox
    Friend WithEvents PBHandR3 As System.Windows.Forms.PictureBox
    Friend WithEvents PBHandR2 As System.Windows.Forms.PictureBox
    Friend WithEvents PBHandR1 As System.Windows.Forms.PictureBox
    Friend WithEvents HandR4 As RadioButtonFull
    Friend WithEvents HandR1 As RadioButtonFull
    Friend WithEvents HandR2 As RadioButtonFull
    Friend WithEvents CBHand As System.Windows.Forms.ComboBox
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
    Friend WithEvents HandR3 As SignWriterStudio.General.RadioButtonFull

End Class
