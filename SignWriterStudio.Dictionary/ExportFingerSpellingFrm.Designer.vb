﻿Imports System.Net

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExportFingerSpellingFrm
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
        Me.TextFilenameTb = New System.Windows.Forms.TextBox()
        Me.TextBrowseBtn = New System.Windows.Forms.Button()
        Me.ExportBtn = New System.Windows.Forms.Button()
        Me.TagFilter1 = New Global.SignWriterStudio.Dictionary.TagFilter()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Text Filename"
        '
        'TextFilenameTb
        '
        Me.TextFilenameTb.Location = New System.Drawing.Point(95, 136)
        Me.TextFilenameTb.Name = "TextFilenameTb"
        Me.TextFilenameTb.Size = New System.Drawing.Size(350, 20)
        Me.TextFilenameTb.TabIndex = 1
        '
        'TextBrowseBtn
        '
        Me.TextBrowseBtn.Location = New System.Drawing.Point(451, 134)
        Me.TextBrowseBtn.Name = "TextBrowseBtn"
        Me.TextBrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.TextBrowseBtn.TabIndex = 4
        Me.TextBrowseBtn.Text = "Browse"
        Me.TextBrowseBtn.UseVisualStyleBackColor = True
        '
        'ExportBtn
        '
        Me.ExportBtn.Location = New System.Drawing.Point(239, 210)
        Me.ExportBtn.Name = "ExportBtn"
        Me.ExportBtn.Size = New System.Drawing.Size(75, 23)
        Me.ExportBtn.TabIndex = 6
        Me.ExportBtn.Text = "Export"
        Me.ExportBtn.UseVisualStyleBackColor = True
        '
        'TagFilter1
        '
        Me.TagFilter1.AssumeFiltering = False
        Me.TagFilter1.DataSource = Nothing
        Me.TagFilter1.Location = New System.Drawing.Point(74, 12)
        Me.TagFilter1.Name = "TagFilter1"
        Me.TagFilter1.Size = New System.Drawing.Size(421, 100)
        Me.TagFilter1.TabIndex = 7
        '
        'ExportFingerSpellingFrm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(557, 258)
        Me.Controls.Add(Me.TagFilter1)
        Me.Controls.Add(Me.ExportBtn)
        Me.Controls.Add(Me.TextBrowseBtn)
        Me.Controls.Add(Me.TextFilenameTb)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ExportFingerSpellingFrm"
        Me.Text = "Export Fingerspelling"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextFilenameTb As System.Windows.Forms.TextBox
    Friend WithEvents TextBrowseBtn As System.Windows.Forms.Button
    Friend WithEvents ExportBtn As System.Windows.Forms.Button
    Friend WithEvents TagFilter1 As Global.SignWriterStudio.Dictionary.TagFilter
End Class
