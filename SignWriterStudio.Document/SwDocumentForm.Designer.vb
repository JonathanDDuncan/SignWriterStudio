Imports SignWriterStudio.SWClasses

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SwDocumentForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SwDocumentForm))
        Me.PictBoxContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditSignInEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToDictionaryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoveSignToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveUpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LaneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeftToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RightToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeginningOfColumnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToAnotherDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PrintSWDocument = New System.Drawing.Printing.PrintDocument()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.OpenDocumentFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveDocumentFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.DocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintSetupToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintPreviewToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportAsJSONToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportAsJSONWholeFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LayoutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteFSWDocumentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToSignPuddleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyAsFSWToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyAsImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowGlossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SignsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromDictionaryF10ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GlossToSignRealTimeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PicturesToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromDictionaryPhotoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromDictionarySignToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GlossToSignToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GlossToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialogNewDocument = New System.Windows.Forms.SaveFileDialog()
        Me.SwFlowLayoutPanel1 = New SignWriterStudio.SWClasses.SwFlowLayoutPanel()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PictBoxContextMenuStrip.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictBoxContextMenuStrip
        '
        Me.PictBoxContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditSignInEditorToolStripMenuItem, Me.SaveToDictionaryToolStripMenuItem, Me.RemoveSignToolStripMenuItem, Me.AToolStripMenuItem, Me.MoveUpToolStripMenuItem, Me.MoveDownToolStripMenuItem, Me.LaneToolStripMenuItem, Me.PropertiesToolStripMenuItem, Me.CopyToolStripMenuItem, Me.CutToolStripMenuItem, Me.PasteToolStripMenuItem, Me.BeginningOfColumnToolStripMenuItem, Me.SaveToAnotherDocumentToolStripMenuItem})
        Me.PictBoxContextMenuStrip.Name = "DocumentContextMenuStrip"
        Me.PictBoxContextMenuStrip.Size = New System.Drawing.Size(235, 274)
        '
        'EditSignInEditorToolStripMenuItem
        '
        Me.EditSignInEditorToolStripMenuItem.Name = "EditSignInEditorToolStripMenuItem"
        Me.EditSignInEditorToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.EditSignInEditorToolStripMenuItem.Text = "Edit in Editor"
        '
        'SaveToDictionaryToolStripMenuItem
        '
        Me.SaveToDictionaryToolStripMenuItem.Name = "SaveToDictionaryToolStripMenuItem"
        Me.SaveToDictionaryToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.SaveToDictionaryToolStripMenuItem.Text = "Save Sign to Dictionary"
        '
        'RemoveSignToolStripMenuItem
        '
        Me.RemoveSignToolStripMenuItem.Name = "RemoveSignToolStripMenuItem"
        Me.RemoveSignToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.RemoveSignToolStripMenuItem.Text = "Remove Sign"
        '
        'AToolStripMenuItem
        '
        Me.AToolStripMenuItem.Name = "AToolStripMenuItem"
        Me.AToolStripMenuItem.Size = New System.Drawing.Size(231, 6)
        '
        'MoveUpToolStripMenuItem
        '
        Me.MoveUpToolStripMenuItem.Name = "MoveUpToolStripMenuItem"
        Me.MoveUpToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.MoveUpToolStripMenuItem.Text = "Move Up"
        '
        'MoveDownToolStripMenuItem
        '
        Me.MoveDownToolStripMenuItem.Name = "MoveDownToolStripMenuItem"
        Me.MoveDownToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.MoveDownToolStripMenuItem.Text = "Move Down"
        '
        'LaneToolStripMenuItem
        '
        Me.LaneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LeftToolStripMenuItem, Me.CenterToolStripMenuItem, Me.RightToolStripMenuItem})
        Me.LaneToolStripMenuItem.Name = "LaneToolStripMenuItem"
        Me.LaneToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.LaneToolStripMenuItem.Text = "Lane"
        '
        'LeftToolStripMenuItem
        '
        Me.LeftToolStripMenuItem.Name = "LeftToolStripMenuItem"
        Me.LeftToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.LeftToolStripMenuItem.Text = "Left"
        '
        'CenterToolStripMenuItem
        '
        Me.CenterToolStripMenuItem.Name = "CenterToolStripMenuItem"
        Me.CenterToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.CenterToolStripMenuItem.Text = "Center"
        '
        'RightToolStripMenuItem
        '
        Me.RightToolStripMenuItem.Name = "RightToolStripMenuItem"
        Me.RightToolStripMenuItem.Size = New System.Drawing.Size(109, 22)
        Me.RightToolStripMenuItem.Text = "Right"
        '
        'PropertiesToolStripMenuItem
        '
        Me.PropertiesToolStripMenuItem.Name = "PropertiesToolStripMenuItem"
        Me.PropertiesToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.PropertiesToolStripMenuItem.Text = "Properties"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'BeginningOfColumnToolStripMenuItem
        '
        Me.BeginningOfColumnToolStripMenuItem.Name = "BeginningOfColumnToolStripMenuItem"
        Me.BeginningOfColumnToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.BeginningOfColumnToolStripMenuItem.Text = "Force to Beginning of Column"
        '
        'SaveToAnotherDocumentToolStripMenuItem
        '
        Me.SaveToAnotherDocumentToolStripMenuItem.Name = "SaveToAnotherDocumentToolStripMenuItem"
        Me.SaveToAnotherDocumentToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.SaveToAnotherDocumentToolStripMenuItem.Text = "Save to Another Document"
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PrintSWDocument
        '
        Me.PrintSWDocument.DocumentName = "SWDocument"
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'OpenDocumentFileDialog
        '
        '
        'SaveDocumentFileDialog
        '
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DocumentToolStripMenuItem, Me.EditToolStripMenuItem, Me.AddToolStripMenuItem2, Me.HelpToolStripMenuItem, Me.ToolStripMenuItem1, Me.NewToolStripMenuItem1, Me.SaveToolStripMenuItem, Me.CopyImageToolStripMenuItem, Me.GlossToSignToolStripMenuItem, Me.GlossToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(955, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DocumentToolStripMenuItem
        '
        Me.DocumentToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem2, Me.SaveAsToolStripMenuItem, Me.SettingsToolStripMenuItem, Me.PrintToolStripMenuItem1, Me.PrintSetupToolStripMenuItem, Me.PrintPreviewToolStripMenuItem1, Me.OptionsToolStripMenuItem, Me.ExportAsJSONToolStripMenuItem, Me.ExportAsJSONWholeFolderToolStripMenuItem})
        Me.DocumentToolStripMenuItem.Name = "DocumentToolStripMenuItem"
        Me.DocumentToolStripMenuItem.Size = New System.Drawing.Size(75, 20)
        Me.DocumentToolStripMenuItem.Text = "Document"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.NewToolStripMenuItem.Text = "&New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.OpenToolStripMenuItem.Text = "&Open ..."
        '
        'SaveToolStripMenuItem2
        '
        Me.SaveToolStripMenuItem2.Enabled = False
        Me.SaveToolStripMenuItem2.Name = "SaveToolStripMenuItem2"
        Me.SaveToolStripMenuItem2.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem2.Size = New System.Drawing.Size(222, 22)
        Me.SaveToolStripMenuItem2.Text = "&Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save &as ..."
        '
        'SettingsToolStripMenuItem
        '
        Me.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem"
        Me.SettingsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.SettingsToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.SettingsToolStripMenuItem.Text = "&Settings ..."
        Me.SettingsToolStripMenuItem.Visible = False
        '
        'PrintToolStripMenuItem1
        '
        Me.PrintToolStripMenuItem1.Name = "PrintToolStripMenuItem1"
        Me.PrintToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintToolStripMenuItem1.Size = New System.Drawing.Size(222, 22)
        Me.PrintToolStripMenuItem1.Text = "&Print ..."
        Me.PrintToolStripMenuItem1.Visible = False
        '
        'PrintSetupToolStripMenuItem
        '
        Me.PrintSetupToolStripMenuItem.Name = "PrintSetupToolStripMenuItem"
        Me.PrintSetupToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintSetupToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.PrintSetupToolStripMenuItem.Text = "Print Se&tup ..."
        Me.PrintSetupToolStripMenuItem.Visible = False
        '
        'PrintPreviewToolStripMenuItem1
        '
        Me.PrintPreviewToolStripMenuItem1.Name = "PrintPreviewToolStripMenuItem1"
        Me.PrintPreviewToolStripMenuItem1.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintPreviewToolStripMenuItem1.Size = New System.Drawing.Size(222, 22)
        Me.PrintPreviewToolStripMenuItem1.Text = "Print Pre&view ..."
        Me.PrintPreviewToolStripMenuItem1.Visible = False
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.T), System.Windows.Forms.Keys)
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.OptionsToolStripMenuItem.Text = "Op&tions"
        '
        'ExportAsJSONToolStripMenuItem
        '
        Me.ExportAsJSONToolStripMenuItem.Name = "ExportAsJSONToolStripMenuItem"
        Me.ExportAsJSONToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.J), System.Windows.Forms.Keys)
        Me.ExportAsJSONToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.ExportAsJSONToolStripMenuItem.Text = "Export as JSON"
        '
        'ExportAsJSONWholeFolderToolStripMenuItem
        '
        Me.ExportAsJSONWholeFolderToolStripMenuItem.Name = "ExportAsJSONWholeFolderToolStripMenuItem"
        Me.ExportAsJSONWholeFolderToolStripMenuItem.Size = New System.Drawing.Size(222, 22)
        Me.ExportAsJSONWholeFolderToolStripMenuItem.Text = "Export as JSON whole folder"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PasteToolStripMenuItem2, Me.LayoutToolStripMenuItem, Me.PasteFSWDocumentToolStripMenuItem, Me.PasteToSignPuddleToolStripMenuItem, Me.CopyAsFSWToolStripMenuItem, Me.CopyAsImageToolStripMenuItem, Me.ShowGlossToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.EditToolStripMenuItem.Text = "Edit"
        '
        'PasteToolStripMenuItem2
        '
        Me.PasteToolStripMenuItem2.Name = "PasteToolStripMenuItem2"
        Me.PasteToolStripMenuItem2.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteToolStripMenuItem2.Size = New System.Drawing.Size(194, 22)
        Me.PasteToolStripMenuItem2.Text = "Paste"
        '
        'LayoutToolStripMenuItem
        '
        Me.LayoutToolStripMenuItem.Name = "LayoutToolStripMenuItem"
        Me.LayoutToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.LayoutToolStripMenuItem.Text = "Layout"
        '
        'PasteFSWDocumentToolStripMenuItem
        '
        Me.PasteFSWDocumentToolStripMenuItem.Name = "PasteFSWDocumentToolStripMenuItem"
        Me.PasteFSWDocumentToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.PasteFSWDocumentToolStripMenuItem.Text = "Paste FSW"
        '
        'PasteToSignPuddleToolStripMenuItem
        '
        Me.PasteToSignPuddleToolStripMenuItem.Name = "PasteToSignPuddleToolStripMenuItem"
        Me.PasteToSignPuddleToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.PasteToSignPuddleToolStripMenuItem.Text = "Copy to SignPuddle"
        '
        'CopyAsFSWToolStripMenuItem
        '
        Me.CopyAsFSWToolStripMenuItem.Name = "CopyAsFSWToolStripMenuItem"
        Me.CopyAsFSWToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.CopyAsFSWToolStripMenuItem.Text = "Copy as FSW"
        '
        'CopyAsImageToolStripMenuItem
        '
        Me.CopyAsImageToolStripMenuItem.Name = "CopyAsImageToolStripMenuItem"
        Me.CopyAsImageToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyAsImageToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.CopyAsImageToolStripMenuItem.Text = "Copy as Image"
        '
        'ShowGlossToolStripMenuItem
        '
        Me.ShowGlossToolStripMenuItem.CheckOnClick = True
        Me.ShowGlossToolStripMenuItem.Name = "ShowGlossToolStripMenuItem"
        Me.ShowGlossToolStripMenuItem.Size = New System.Drawing.Size(194, 22)
        Me.ShowGlossToolStripMenuItem.Text = "Show gloss"
        '
        'AddToolStripMenuItem2
        '
        Me.AddToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SignsToolStripMenuItem1, Me.PicturesToolStripMenuItem2})
        Me.AddToolStripMenuItem2.Name = "AddToolStripMenuItem2"
        Me.AddToolStripMenuItem2.Size = New System.Drawing.Size(41, 20)
        Me.AddToolStripMenuItem2.Text = "Add"
        '
        'SignsToolStripMenuItem1
        '
        Me.SignsToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FromEditorToolStripMenuItem, Me.FromDictionaryF10ToolStripMenuItem, Me.GlossToSignRealTimeToolStripMenuItem})
        Me.SignsToolStripMenuItem1.Name = "SignsToolStripMenuItem1"
        Me.SignsToolStripMenuItem1.Size = New System.Drawing.Size(116, 22)
        Me.SignsToolStripMenuItem1.Text = "Signs"
        '
        'FromEditorToolStripMenuItem
        '
        Me.FromEditorToolStripMenuItem.Name = "FromEditorToolStripMenuItem"
        Me.FromEditorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9
        Me.FromEditorToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.FromEditorToolStripMenuItem.Text = "From Editor"
        '
        'FromDictionaryF10ToolStripMenuItem
        '
        Me.FromDictionaryF10ToolStripMenuItem.Name = "FromDictionaryF10ToolStripMenuItem"
        Me.FromDictionaryF10ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10
        Me.FromDictionaryF10ToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.FromDictionaryF10ToolStripMenuItem.Text = "From Dictionary"
        '
        'GlossToSignRealTimeToolStripMenuItem
        '
        Me.GlossToSignRealTimeToolStripMenuItem.Name = "GlossToSignRealTimeToolStripMenuItem"
        Me.GlossToSignRealTimeToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.G), System.Windows.Forms.Keys)
        Me.GlossToSignRealTimeToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.GlossToSignRealTimeToolStripMenuItem.Text = "Gloss to Sign"
        '
        'PicturesToolStripMenuItem2
        '
        Me.PicturesToolStripMenuItem2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FromFileToolStripMenuItem, Me.FromDictionaryPhotoToolStripMenuItem, Me.FromDictionarySignToolStripMenuItem1})
        Me.PicturesToolStripMenuItem2.Name = "PicturesToolStripMenuItem2"
        Me.PicturesToolStripMenuItem2.Size = New System.Drawing.Size(116, 22)
        Me.PicturesToolStripMenuItem2.Text = "Pictures"
        '
        'FromFileToolStripMenuItem
        '
        Me.FromFileToolStripMenuItem.Name = "FromFileToolStripMenuItem"
        Me.FromFileToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11
        Me.FromFileToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.FromFileToolStripMenuItem.Text = "From File"
        '
        'FromDictionaryPhotoToolStripMenuItem
        '
        Me.FromDictionaryPhotoToolStripMenuItem.Name = "FromDictionaryPhotoToolStripMenuItem"
        Me.FromDictionaryPhotoToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12
        Me.FromDictionaryPhotoToolStripMenuItem.Size = New System.Drawing.Size(251, 22)
        Me.FromDictionaryPhotoToolStripMenuItem.Text = "From Dictionary (Illustration)"
        '
        'FromDictionarySignToolStripMenuItem1
        '
        Me.FromDictionarySignToolStripMenuItem1.Name = "FromDictionarySignToolStripMenuItem1"
        Me.FromDictionarySignToolStripMenuItem1.Size = New System.Drawing.Size(251, 22)
        Me.FromDictionarySignToolStripMenuItem1.Text = "From Dictionary (Sign Photo)"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem1})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem1
        '
        Me.HelpToolStripMenuItem1.Name = "HelpToolStripMenuItem1"
        Me.HelpToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.HelpToolStripMenuItem1.Size = New System.Drawing.Size(118, 22)
        Me.HelpToolStripMenuItem1.Text = "Help"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(40, 20)
        Me.ToolStripMenuItem1.Text = "       "
        '
        'NewToolStripMenuItem1
        '
        Me.NewToolStripMenuItem1.Name = "NewToolStripMenuItem1"
        Me.NewToolStripMenuItem1.Size = New System.Drawing.Size(43, 20)
        Me.NewToolStripMenuItem1.Text = "New"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'CopyImageToolStripMenuItem
        '
        Me.CopyImageToolStripMenuItem.Name = "CopyImageToolStripMenuItem"
        Me.CopyImageToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.CopyImageToolStripMenuItem.Text = "Copy Image"
        '
        'GlossToSignToolStripMenuItem
        '
        Me.GlossToSignToolStripMenuItem.Name = "GlossToSignToolStripMenuItem"
        Me.GlossToSignToolStripMenuItem.Size = New System.Drawing.Size(88, 20)
        Me.GlossToSignToolStripMenuItem.Text = "Gloss To Sign"
        '
        'GlossToolStripMenuItem
        '
        Me.GlossToolStripMenuItem.Name = "GlossToolStripMenuItem"
        Me.GlossToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.GlossToolStripMenuItem.Text = "Gloss"
        '
        'SaveFileDialogNewDocument
        '
        '
        'SwFlowLayoutPanel1
        '
        Me.SwFlowLayoutPanel1.AllowDrop = True
        Me.SwFlowLayoutPanel1.AutoScroll = True
        Me.SwFlowLayoutPanel1.AutoSize = True
        Me.SwFlowLayoutPanel1.Direction = System.Windows.Forms.FlowDirection.TopDown
        Me.SwFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SwFlowLayoutPanel1.DrawColumnLines = True
        Me.SwFlowLayoutPanel1.Location = New System.Drawing.Point(0, 24)
        Me.SwFlowLayoutPanel1.MySWDocument = Nothing
        Me.SwFlowLayoutPanel1.Name = "SwFlowLayoutPanel1"
        Me.SwFlowLayoutPanel1.RightClickedControl = Nothing
        Me.SwFlowLayoutPanel1.Size = New System.Drawing.Size(955, 527)
        Me.SwFlowLayoutPanel1.SpaceBetweenCols = 10
        Me.SwFlowLayoutPanel1.TabIndex = 0
        Me.SwFlowLayoutPanel1.WrapContents = False
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'SwDocumentForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(955, 551)
        Me.Controls.Add(Me.SwFlowLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SwDocumentForm"
        Me.Text = "SignWriter Studio™ Document"
        Me.PictBoxContextMenuStrip.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SwFlowLayoutPanel1 As SwFlowLayoutPanel
    Friend WithEvents PictBoxContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents EditSignInEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoveSignToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MoveUpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LaneToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LeftToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CenterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RightToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PrintSWDocument As System.Drawing.Printing.PrintDocument
    Friend WithEvents PrintDialog1 As System.Windows.Forms.PrintDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents OpenDocumentFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveDocumentFileDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents SaveToDictionaryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeginningOfColumnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents DocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintSetupToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LayoutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SignsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromDictionaryF10ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PicturesToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromDictionaryPhotoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FromDictionarySignToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteFSWDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyAsFSWToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToSignPuddleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyAsImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GlossToSignRealTimeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToAnotherDocumentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveFileDialogNewDocument As System.Windows.Forms.SaveFileDialog
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ShowGlossToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportAsJSONToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportAsJSONWholeFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyImageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GlossToSignToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GlossToolStripMenuItem As ToolStripMenuItem
End Class
