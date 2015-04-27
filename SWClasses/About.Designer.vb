<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.Derechos = New System.Windows.Forms.Label()
        Me.LBVersion = New System.Windows.Forms.Label()
        Me.Version = New System.Windows.Forms.Label()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.LBWarning = New System.Windows.Forms.Label()
        Me.LbLevel = New System.Windows.Forms.Label()
        Me.BtnSaveLog = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'Derechos
        '
        Me.Derechos.AutoSize = True
        Me.Derechos.Location = New System.Drawing.Point(81, 60)
        Me.Derechos.Name = "Derechos"
        Me.Derechos.Size = New System.Drawing.Size(161, 52)
        Me.Derechos.TabIndex = 2
        Me.Derechos.Text = "SignWriter Studio™" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "© 2009-2012 Jonathan Duncan " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "All Rights reserved " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'LBVersion
        '
        Me.LBVersion.AutoSize = True
        Me.LBVersion.Location = New System.Drawing.Point(12, 35)
        Me.LBVersion.Name = "LBVersion"
        Me.LBVersion.Size = New System.Drawing.Size(48, 13)
        Me.LBVersion.TabIndex = 3
        Me.LBVersion.Text = "Version: "
        '
        'Version
        '
        Me.Version.AutoSize = True
        Me.Version.Location = New System.Drawing.Point(66, 35)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(10, 13)
        Me.Version.TabIndex = 6
        Me.Version.Text = " "
        '
        'BtnOK
        '
        Me.BtnOK.Location = New System.Drawing.Point(237, 173)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(106, 23)
        Me.BtnOK.TabIndex = 9
        Me.BtnOK.Text = "Close"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'LBWarning
        '
        Me.LBWarning.Location = New System.Drawing.Point(12, 135)
        Me.LBWarning.Name = "LBWarning"
        Me.LBWarning.Size = New System.Drawing.Size(219, 99)
        Me.LBWarning.TabIndex = 10
        Me.LBWarning.Text = resources.GetString("LBWarning.Text")
        '
        'LbLevel
        '
        Me.LbLevel.AutoSize = True
        Me.LbLevel.Location = New System.Drawing.Point(12, 11)
        Me.LbLevel.Name = "LbLevel"
        Me.LbLevel.Size = New System.Drawing.Size(125, 13)
        Me.LbLevel.TabIndex = 13
        Me.LbLevel.Text = "This program is Freeware"
        '
        'BtnSaveLog
        '
        Me.BtnSaveLog.Location = New System.Drawing.Point(237, 144)
        Me.BtnSaveLog.Name = "BtnSaveLog"
        Me.BtnSaveLog.Size = New System.Drawing.Size(106, 23)
        Me.BtnSaveLog.TabIndex = 15
        Me.BtnSaveLog.Text = "Save Log File"
        Me.BtnSaveLog.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(81, 112)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(136, 13)
        Me.LinkLabel1.TabIndex = 20
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "www.SignWriterStudio.com"
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 241)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.BtnSaveLog)
        Me.Controls.Add(Me.LbLevel)
        Me.Controls.Add(Me.LBWarning)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Version)
        Me.Controls.Add(Me.LBVersion)
        Me.Controls.Add(Me.Derechos)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "About"
        Me.Text = "About SignWriter Studio™"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Derechos As System.Windows.Forms.Label
    Friend WithEvents LBVersion As System.Windows.Forms.Label
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents LBWarning As System.Windows.Forms.Label
    Friend WithEvents LbLevel As System.Windows.Forms.Label
    Friend WithEvents BtnSaveLog As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
End Class
