<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportHtmlFrm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.HTMLFilenameTb = New System.Windows.Forms.TextBox()
        Me.HTMLBrowseBtn = New System.Windows.Forms.Button()
        Me.ExportBtn = New System.Windows.Forms.Button()
        Me.externPngCB = New System.Windows.Forms.CheckBox()
        Me.BtnInclBegHtml = New System.Windows.Forms.Button()
        Me.TBInclBegHtml = New System.Windows.Forms.TextBox()
        Me.LblInclBegHtml = New System.Windows.Forms.Label()
        Me.BtnInclEndHtml = New System.Windows.Forms.Button()
        Me.TBInclEndHtml = New System.Windows.Forms.TextBox()
        Me.LblInclEndHtml = New System.Windows.Forms.Label()
        Me.CBInclBegHtml = New System.Windows.Forms.CheckBox()
        Me.CBInclEndHtml = New System.Windows.Forms.CheckBox()
        Me.CBShowSignWriting = New System.Windows.Forms.CheckBox()
        Me.CBShowIllustration = New System.Windows.Forms.CheckBox()
        Me.CBShowPhotoSign = New System.Windows.Forms.CheckBox()
        Me.CBShowPhotoSignSource = New System.Windows.Forms.CheckBox()
        Me.CBShowIllustrationSource = New System.Windows.Forms.CheckBox()
        Me.CBShowSignWritingSource = New System.Windows.Forms.CheckBox()
        Me.CBShowGloss = New System.Windows.Forms.CheckBox()
        Me.CBShowGlosses = New System.Windows.Forms.CheckBox()
        Me.CBCreateIndex = New System.Windows.Forms.CheckBox()
        Me.CBShowSequence = New System.Windows.Forms.CheckBox()
        Me.cbSortAlphabetically = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Location = New System.Drawing.Point(22, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "HTML Filename"
        '
        'HTMLFilenameTb
        '
        Me.HTMLFilenameTb.Location = New System.Drawing.Point(122, 29)
        Me.HTMLFilenameTb.Name = "HTMLFilenameTb"
        Me.HTMLFilenameTb.Size = New System.Drawing.Size(329, 20)
        Me.HTMLFilenameTb.TabIndex = 1
        '
        'HTMLBrowseBtn
        '
        Me.HTMLBrowseBtn.Location = New System.Drawing.Point(457, 27)
        Me.HTMLBrowseBtn.Name = "HTMLBrowseBtn"
        Me.HTMLBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.HTMLBrowseBtn.TabIndex = 4
        Me.HTMLBrowseBtn.Text = "Browse"
        Me.HTMLBrowseBtn.UseVisualStyleBackColor = true
        '
        'ExportBtn
        '
        Me.ExportBtn.Location = New System.Drawing.Point(262, 366)
        Me.ExportBtn.Name = "ExportBtn"
        Me.ExportBtn.Size = New System.Drawing.Size(75, 23)
        Me.ExportBtn.TabIndex = 6
        Me.ExportBtn.Text = "Export"
        Me.ExportBtn.UseVisualStyleBackColor = true
        '
        'externPngCB
        '
        Me.externPngCB.AutoSize = true
        Me.externPngCB.Checked = true
        Me.externPngCB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.externPngCB.Location = New System.Drawing.Point(41, 346)
        Me.externPngCB.Name = "externPngCB"
        Me.externPngCB.Size = New System.Drawing.Size(130, 17)
        Me.externPngCB.TabIndex = 7
        Me.externPngCB.Text = "Save PNGs Externally"
        Me.externPngCB.UseVisualStyleBackColor = true
        '
        'BtnInclBegHtml
        '
        Me.BtnInclBegHtml.Location = New System.Drawing.Point(457, 76)
        Me.BtnInclBegHtml.Name = "BtnInclBegHtml"
        Me.BtnInclBegHtml.Size = New System.Drawing.Size(75, 23)
        Me.BtnInclBegHtml.TabIndex = 10
        Me.BtnInclBegHtml.Text = "Browse"
        Me.BtnInclBegHtml.UseVisualStyleBackColor = true
        '
        'TBInclBegHtml
        '
        Me.TBInclBegHtml.Location = New System.Drawing.Point(122, 79)
        Me.TBInclBegHtml.Name = "TBInclBegHtml"
        Me.TBInclBegHtml.Size = New System.Drawing.Size(329, 20)
        Me.TBInclBegHtml.TabIndex = 9
        '
        'LblInclBegHtml
        '
        Me.LblInclBegHtml.AutoSize = true
        Me.LblInclBegHtml.Location = New System.Drawing.Point(22, 81)
        Me.LblInclBegHtml.Name = "LblInclBegHtml"
        Me.LblInclBegHtml.Size = New System.Drawing.Size(87, 13)
        Me.LblInclBegHtml.TabIndex = 8
        Me.LblInclBegHtml.Text = "Beggining HTML"
        '
        'BtnInclEndHtml
        '
        Me.BtnInclEndHtml.Location = New System.Drawing.Point(457, 123)
        Me.BtnInclEndHtml.Name = "BtnInclEndHtml"
        Me.BtnInclEndHtml.Size = New System.Drawing.Size(75, 23)
        Me.BtnInclEndHtml.TabIndex = 13
        Me.BtnInclEndHtml.Text = "Browse"
        Me.BtnInclEndHtml.UseVisualStyleBackColor = true
        '
        'TBInclEndHtml
        '
        Me.TBInclEndHtml.Location = New System.Drawing.Point(122, 125)
        Me.TBInclEndHtml.Name = "TBInclEndHtml"
        Me.TBInclEndHtml.Size = New System.Drawing.Size(329, 20)
        Me.TBInclEndHtml.TabIndex = 12
        '
        'LblInclEndHtml
        '
        Me.LblInclEndHtml.AutoSize = true
        Me.LblInclEndHtml.Location = New System.Drawing.Point(22, 128)
        Me.LblInclEndHtml.Name = "LblInclEndHtml"
        Me.LblInclEndHtml.Size = New System.Drawing.Size(73, 13)
        Me.LblInclEndHtml.TabIndex = 11
        Me.LblInclEndHtml.Text = "Ending HTML"
        '
        'CBInclBegHtml
        '
        Me.CBInclBegHtml.AutoSize = true
        Me.CBInclBegHtml.Location = New System.Drawing.Point(10, 61)
        Me.CBInclBegHtml.Name = "CBInclBegHtml"
        Me.CBInclBegHtml.Size = New System.Drawing.Size(144, 17)
        Me.CBInclBegHtml.TabIndex = 14
        Me.CBInclBegHtml.Text = "Include Beggining HTML"
        Me.CBInclBegHtml.UseVisualStyleBackColor = true
        '
        'CBInclEndHtml
        '
        Me.CBInclEndHtml.AutoSize = true
        Me.CBInclEndHtml.Location = New System.Drawing.Point(10, 107)
        Me.CBInclEndHtml.Name = "CBInclEndHtml"
        Me.CBInclEndHtml.Size = New System.Drawing.Size(130, 17)
        Me.CBInclEndHtml.TabIndex = 15
        Me.CBInclEndHtml.Text = "Include Ending HTML"
        Me.CBInclEndHtml.UseVisualStyleBackColor = true
        '
        'CBShowSignWriting
        '
        Me.CBShowSignWriting.AutoSize = true
        Me.CBShowSignWriting.Checked = true
        Me.CBShowSignWriting.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBShowSignWriting.Location = New System.Drawing.Point(41, 272)
        Me.CBShowSignWriting.Name = "CBShowSignWriting"
        Me.CBShowSignWriting.Size = New System.Drawing.Size(110, 17)
        Me.CBShowSignWriting.TabIndex = 16
        Me.CBShowSignWriting.Text = "Show SignWriting"
        Me.CBShowSignWriting.UseVisualStyleBackColor = true
        '
        'CBShowIllustration
        '
        Me.CBShowIllustration.AutoSize = true
        Me.CBShowIllustration.Checked = true
        Me.CBShowIllustration.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBShowIllustration.Location = New System.Drawing.Point(41, 295)
        Me.CBShowIllustration.Name = "CBShowIllustration"
        Me.CBShowIllustration.Size = New System.Drawing.Size(103, 17)
        Me.CBShowIllustration.TabIndex = 17
        Me.CBShowIllustration.Text = "Show Illustration"
        Me.CBShowIllustration.UseVisualStyleBackColor = true
        '
        'CBShowPhotoSign
        '
        Me.CBShowPhotoSign.AutoSize = true
        Me.CBShowPhotoSign.Checked = true
        Me.CBShowPhotoSign.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBShowPhotoSign.Location = New System.Drawing.Point(41, 319)
        Me.CBShowPhotoSign.Name = "CBShowPhotoSign"
        Me.CBShowPhotoSign.Size = New System.Drawing.Size(108, 17)
        Me.CBShowPhotoSign.TabIndex = 18
        Me.CBShowPhotoSign.Text = "Show Photo Sign"
        Me.CBShowPhotoSign.UseVisualStyleBackColor = true
        '
        'CBShowPhotoSignSource
        '
        Me.CBShowPhotoSignSource.AutoSize = true
        Me.CBShowPhotoSignSource.Location = New System.Drawing.Point(217, 319)
        Me.CBShowPhotoSignSource.Name = "CBShowPhotoSignSource"
        Me.CBShowPhotoSignSource.Size = New System.Drawing.Size(145, 17)
        Me.CBShowPhotoSignSource.TabIndex = 21
        Me.CBShowPhotoSignSource.Text = "Show Photo Sign Source"
        Me.CBShowPhotoSignSource.UseVisualStyleBackColor = true
        '
        'CBShowIllustrationSource
        '
        Me.CBShowIllustrationSource.AutoSize = true
        Me.CBShowIllustrationSource.Location = New System.Drawing.Point(217, 295)
        Me.CBShowIllustrationSource.Name = "CBShowIllustrationSource"
        Me.CBShowIllustrationSource.Size = New System.Drawing.Size(140, 17)
        Me.CBShowIllustrationSource.TabIndex = 20
        Me.CBShowIllustrationSource.Text = "Show Illustration Source"
        Me.CBShowIllustrationSource.UseVisualStyleBackColor = true
        '
        'CBShowSignWritingSource
        '
        Me.CBShowSignWritingSource.AutoSize = true
        Me.CBShowSignWritingSource.Location = New System.Drawing.Point(217, 272)
        Me.CBShowSignWritingSource.Name = "CBShowSignWritingSource"
        Me.CBShowSignWritingSource.Size = New System.Drawing.Size(147, 17)
        Me.CBShowSignWritingSource.TabIndex = 19
        Me.CBShowSignWritingSource.Text = "Show SignWriting Source"
        Me.CBShowSignWritingSource.UseVisualStyleBackColor = true
        '
        'CBShowGloss
        '
        Me.CBShowGloss.AutoSize = true
        Me.CBShowGloss.Checked = true
        Me.CBShowGloss.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBShowGloss.Location = New System.Drawing.Point(44, 206)
        Me.CBShowGloss.Name = "CBShowGloss"
        Me.CBShowGloss.Size = New System.Drawing.Size(82, 17)
        Me.CBShowGloss.TabIndex = 22
        Me.CBShowGloss.Text = "Show Gloss"
        Me.CBShowGloss.UseVisualStyleBackColor = true
        '
        'CBShowGlosses
        '
        Me.CBShowGlosses.AutoSize = true
        Me.CBShowGlosses.Checked = true
        Me.CBShowGlosses.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBShowGlosses.Location = New System.Drawing.Point(43, 229)
        Me.CBShowGlosses.Name = "CBShowGlosses"
        Me.CBShowGlosses.Size = New System.Drawing.Size(93, 17)
        Me.CBShowGlosses.TabIndex = 23
        Me.CBShowGlosses.Text = "Show Glosses"
        Me.CBShowGlosses.UseVisualStyleBackColor = true
        '
        'CBCreateIndex
        '
        Me.CBCreateIndex.AutoSize = true
        Me.CBCreateIndex.Location = New System.Drawing.Point(44, 168)
        Me.CBCreateIndex.Name = "CBCreateIndex"
        Me.CBCreateIndex.Size = New System.Drawing.Size(86, 17)
        Me.CBCreateIndex.TabIndex = 24
        Me.CBCreateIndex.Text = "Create Index"
        Me.CBCreateIndex.UseVisualStyleBackColor = true
        '
        'CBShowSequence
        '
        Me.CBShowSequence.AutoSize = true
        Me.CBShowSequence.Checked = true
        Me.CBShowSequence.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CBShowSequence.Location = New System.Drawing.Point(41, 252)
        Me.CBShowSequence.Name = "CBShowSequence"
        Me.CBShowSequence.Size = New System.Drawing.Size(105, 17)
        Me.CBShowSequence.TabIndex = 25
        Me.CBShowSequence.Text = "Show Sequence"
        Me.CBShowSequence.UseVisualStyleBackColor = true
        '
        'cbSortAlphabetically
        '
        Me.cbSortAlphabetically.AutoSize = true
        Me.cbSortAlphabetically.Checked = true
        Me.cbSortAlphabetically.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbSortAlphabetically.Location = New System.Drawing.Point(217, 168)
        Me.cbSortAlphabetically.Name = "cbSortAlphabetically"
        Me.cbSortAlphabetically.Size = New System.Drawing.Size(113, 17)
        Me.cbSortAlphabetically.TabIndex = 26
        Me.cbSortAlphabetically.Text = "Sort Alphabetically"
        Me.cbSortAlphabetically.UseVisualStyleBackColor = true
        '
        'ExportHtmlFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 421)
        Me.Controls.Add(Me.cbSortAlphabetically)
        Me.Controls.Add(Me.CBShowSequence)
        Me.Controls.Add(Me.CBCreateIndex)
        Me.Controls.Add(Me.CBShowGlosses)
        Me.Controls.Add(Me.CBShowGloss)
        Me.Controls.Add(Me.CBShowPhotoSignSource)
        Me.Controls.Add(Me.CBShowIllustrationSource)
        Me.Controls.Add(Me.CBShowSignWritingSource)
        Me.Controls.Add(Me.CBShowPhotoSign)
        Me.Controls.Add(Me.CBShowIllustration)
        Me.Controls.Add(Me.CBShowSignWriting)
        Me.Controls.Add(Me.CBInclEndHtml)
        Me.Controls.Add(Me.CBInclBegHtml)
        Me.Controls.Add(Me.BtnInclEndHtml)
        Me.Controls.Add(Me.TBInclEndHtml)
        Me.Controls.Add(Me.LblInclEndHtml)
        Me.Controls.Add(Me.BtnInclBegHtml)
        Me.Controls.Add(Me.TBInclBegHtml)
        Me.Controls.Add(Me.LblInclBegHtml)
        Me.Controls.Add(Me.externPngCB)
        Me.Controls.Add(Me.ExportBtn)
        Me.Controls.Add(Me.HTMLBrowseBtn)
        Me.Controls.Add(Me.HTMLFilenameTb)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ExportHtmlFrm"
        Me.Text = "Export Html"
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents HTMLFilenameTb As System.Windows.Forms.TextBox
    Friend WithEvents HTMLBrowseBtn As System.Windows.Forms.Button
    Friend WithEvents ExportBtn As System.Windows.Forms.Button
    Friend WithEvents externPngCB As System.Windows.Forms.CheckBox
    Friend WithEvents BtnInclBegHtml As System.Windows.Forms.Button
    Friend WithEvents TBInclBegHtml As System.Windows.Forms.TextBox
    Friend WithEvents LblInclBegHtml As System.Windows.Forms.Label
    Friend WithEvents BtnInclEndHtml As System.Windows.Forms.Button
    Friend WithEvents TBInclEndHtml As System.Windows.Forms.TextBox
    Friend WithEvents LblInclEndHtml As System.Windows.Forms.Label
    Friend WithEvents CBInclBegHtml As System.Windows.Forms.CheckBox
    Friend WithEvents CBInclEndHtml As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowSignWriting As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowIllustration As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowPhotoSign As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowPhotoSignSource As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowIllustrationSource As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowSignWritingSource As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowGloss As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowGlosses As System.Windows.Forms.CheckBox
    Friend WithEvents CBCreateIndex As System.Windows.Forms.CheckBox
    Friend WithEvents CBShowSequence As System.Windows.Forms.CheckBox
    Friend WithEvents cbSortAlphabetically As System.Windows.Forms.CheckBox
End Class
