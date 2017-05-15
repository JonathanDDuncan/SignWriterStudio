<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SignWriterMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SignWriterMenu))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportSettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseCerrarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuitesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SignWritingDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DictionaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowLogFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.HelpProvider1 = New System.Windows.Forms.HelpProvider()
        Me.hpAdvancedCHM = New System.Windows.Forms.HelpProvider()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.BtnDocument = New System.Windows.Forms.Button()
        Me.BtnDictionary = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.SuitesToolStripMenuItem, Me.AboutToolStripMenuItem1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(357, 24)
        Me.MenuStrip1.TabIndex = 6
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.ExportSettingsToolStripMenuItem, Me.ImportSettingsToolStripMenuItem, Me.CloseCerrarToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.FileToolStripMenuItem.Text = "Settings"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.OptionsToolStripMenuItem.Text = "Op&tions ..."
        '
        'ExportSettingsToolStripMenuItem
        '
        Me.ExportSettingsToolStripMenuItem.Name = "ExportSettingsToolStripMenuItem"
        Me.ExportSettingsToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.ExportSettingsToolStripMenuItem.Text = "&Export Settings"
        '
        'ImportSettingsToolStripMenuItem
        '
        Me.ImportSettingsToolStripMenuItem.Name = "ImportSettingsToolStripMenuItem"
        Me.ImportSettingsToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.ImportSettingsToolStripMenuItem.Text = "&Import Settings"
        '
        'CloseCerrarToolStripMenuItem
        '
        Me.CloseCerrarToolStripMenuItem.Name = "CloseCerrarToolStripMenuItem"
        Me.CloseCerrarToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.CloseCerrarToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.CloseCerrarToolStripMenuItem.Text = "&Close"
        '
        'SuitesToolStripMenuItem
        '
        Me.SuitesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SignWritingDocumentToolStripMenuItem, Me.DictionaryToolStripMenuItem, Me.ShowLogFilesToolStripMenuItem})
        Me.SuitesToolStripMenuItem.Name = "SuitesToolStripMenuItem"
        Me.SuitesToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.SuitesToolStripMenuItem.Text = "&Modules"
        '
        'SignWritingDocumentToolStripMenuItem
        '
        Me.SignWritingDocumentToolStripMenuItem.Name = "SignWritingDocumentToolStripMenuItem"
        Me.SignWritingDocumentToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.SignWritingDocumentToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.SignWritingDocumentToolStripMenuItem.Text = "&Document ..."
        '
        'DictionaryToolStripMenuItem
        '
        Me.DictionaryToolStripMenuItem.Name = "DictionaryToolStripMenuItem"
        Me.DictionaryToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.DictionaryToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.DictionaryToolStripMenuItem.Text = "Dic&tionary ..."
        '
        'ShowLogFilesToolStripMenuItem
        '
        Me.ShowLogFilesToolStripMenuItem.Name = "ShowLogFilesToolStripMenuItem"
        Me.ShowLogFilesToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.ShowLogFilesToolStripMenuItem.Text = "Show log files"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "SWS"
        Me.OpenFileDialog1.Filter = "SWS (*.SWS)|*.SWS"
        '
        'HelpProvider1
        '
        Me.HelpProvider1.HelpNamespace = "SignWriterStudio.chm"
        '
        'hpAdvancedCHM
        '
        Me.hpAdvancedCHM.HelpNamespace = "SignWriterStudio.chm"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "SWS"
        Me.SaveFileDialog1.Filter = "SWS (*.SWS)|*.SWS"
        '
        'BtnDocument
        '
        Me.BtnDocument.Location = New System.Drawing.Point(50, 73)
        Me.BtnDocument.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnDocument.Name = "BtnDocument"
        Me.BtnDocument.Size = New System.Drawing.Size(115, 58)
        Me.BtnDocument.TabIndex = 7
        Me.BtnDocument.Text = "Document"
        Me.BtnDocument.UseVisualStyleBackColor = True
        '
        'BtnDictionary
        '
        Me.BtnDictionary.Location = New System.Drawing.Point(192, 73)
        Me.BtnDictionary.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnDictionary.Name = "BtnDictionary"
        Me.BtnDictionary.Size = New System.Drawing.Size(115, 58)
        Me.BtnDictionary.TabIndex = 8
        Me.BtnDictionary.Text = "Dictionary"
        Me.BtnDictionary.UseVisualStyleBackColor = True
        '
        'SignWriterMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(357, 173)
        Me.Controls.Add(Me.BtnDictionary)
        Me.Controls.Add(Me.BtnDocument)
        Me.Controls.Add(Me.MenuStrip1)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "SignWriterMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SignWriter Studio™"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseCerrarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents HelpProvider1 As System.Windows.Forms.HelpProvider
    Friend WithEvents hpAdvancedCHM As System.Windows.Forms.HelpProvider
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuitesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SignWritingDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DictionaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents BtnDocument As System.Windows.Forms.Button
    Friend WithEvents BtnDictionary As System.Windows.Forms.Button
    Friend WithEvents AboutToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowLogFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
