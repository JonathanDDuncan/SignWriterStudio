﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SignPuddleSignIn
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
        Me.LblUsername = New System.Windows.Forms.Label()
        Me.LblPassword = New System.Windows.Forms.Label()
        Me.TBUsername = New System.Windows.Forms.TextBox()
        Me.TBPassword = New System.Windows.Forms.TextBox()
        Me.BtnSignIn = New System.Windows.Forms.Button()
        Me.CBPuddles = New System.Windows.Forms.ComboBox()
        Me.LblPuddle = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'LblUsername
        '
        Me.LblUsername.AutoSize = True
        Me.LblUsername.Location = New System.Drawing.Point(35, 25)
        Me.LblUsername.Name = "LblUsername"
        Me.LblUsername.Size = New System.Drawing.Size(60, 13)
        Me.LblUsername.TabIndex = 0
        Me.LblUsername.Text = "User Name"
        '
        'LblPassword
        '
        Me.LblPassword.AutoSize = True
        Me.LblPassword.Location = New System.Drawing.Point(35, 53)
        Me.LblPassword.Name = "LblPassword"
        Me.LblPassword.Size = New System.Drawing.Size(53, 13)
        Me.LblPassword.TabIndex = 1
        Me.LblPassword.Text = "Password"
        '
        'TBUsername
        '
        Me.TBUsername.Location = New System.Drawing.Point(101, 22)
        Me.TBUsername.Name = "TBUsername"
        Me.TBUsername.Size = New System.Drawing.Size(100, 20)
        Me.TBUsername.TabIndex = 2
        '
        'TBPassword
        '
        Me.TBPassword.Location = New System.Drawing.Point(101, 50)
        Me.TBPassword.Name = "TBPassword"
        Me.TBPassword.Size = New System.Drawing.Size(100, 20)
        Me.TBPassword.TabIndex = 3
        '
        'BtnSignIn
        '
        Me.BtnSignIn.Location = New System.Drawing.Point(218, 142)
        Me.BtnSignIn.Name = "BtnSignIn"
        Me.BtnSignIn.Size = New System.Drawing.Size(75, 23)
        Me.BtnSignIn.TabIndex = 4
        Me.BtnSignIn.Text = "Sign In"
        Me.BtnSignIn.UseVisualStyleBackColor = True
        '
        'CBPuddles
        '
        Me.CBPuddles.FormattingEnabled = True
        Me.CBPuddles.Location = New System.Drawing.Point(101, 93)
        Me.CBPuddles.Name = "CBPuddles"
        Me.CBPuddles.Size = New System.Drawing.Size(425, 21)
        Me.CBPuddles.TabIndex = 5
        '
        'LblPuddle
        '
        Me.LblPuddle.AutoSize = True
        Me.LblPuddle.Location = New System.Drawing.Point(35, 96)
        Me.LblPuddle.Name = "LblPuddle"
        Me.LblPuddle.Size = New System.Drawing.Size(40, 13)
        Me.LblPuddle.TabIndex = 6
        Me.LblPuddle.Text = "Puddle"
        '
        'SignPuddle_Sign_In
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 195)
        Me.Controls.Add(Me.LblPuddle)
        Me.Controls.Add(Me.CBPuddles)
        Me.Controls.Add(Me.BtnSignIn)
        Me.Controls.Add(Me.TBPassword)
        Me.Controls.Add(Me.TBUsername)
        Me.Controls.Add(Me.LblPassword)
        Me.Controls.Add(Me.LblUsername)
        Me.Name = "SignPuddleSignIn"
        Me.Text = "SignPuddle Sign In"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblUsername As System.Windows.Forms.Label
    Friend WithEvents LblPassword As System.Windows.Forms.Label
    Friend WithEvents TBUsername As System.Windows.Forms.TextBox
    Friend WithEvents TBPassword As System.Windows.Forms.TextBox
    Friend WithEvents BtnSignIn As System.Windows.Forms.Button
    Friend WithEvents CBPuddles As System.Windows.Forms.ComboBox
    Friend WithEvents LblPuddle As System.Windows.Forms.Label
End Class