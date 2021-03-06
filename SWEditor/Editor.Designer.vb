Imports SignWriterStudio
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
 Partial Class Editor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Editor))
        Me.SCSWEditor = New System.Windows.Forms.SplitContainer()
        Me.TCSymbols = New System.Windows.Forms.TabControl()
        Me.TPFavorites = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GBAllSymbols = New System.Windows.Forms.GroupBox()
        Me.TVAllGroups = New System.Windows.Forms.TreeView()
        Me.GBChoosers = New System.Windows.Forms.GroupBox()
        Me.btnChooserAdd = New System.Windows.Forms.Button()
        Me.btnChooserReplace = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.PBsymbolOut = New System.Windows.Forms.PictureBox()
        Me.ArrowChooser = New SignWriterStudio.SWEditor.ArrowChooser()
        Me.HandChooser = New SignWriterStudio.SWEditor.HandChooser()
        Me.TVChooser = New System.Windows.Forms.TreeView()
        Me.TPSearch = New System.Windows.Forms.TabPage()
        Me.GBHandSearch = New System.Windows.Forms.GroupBox()
        Me.TVHand = New System.Windows.Forms.TreeView()
        Me.PBHand = New System.Windows.Forms.PictureBox()
        Me.LBThumbPosition = New System.Windows.Forms.Label()
        Me.FilterIndex = New System.Windows.Forms.CheckBox()
        Me.FilterThumbPosition = New System.Windows.Forms.ComboBox()
        Me.ISWAThumbPositionsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ISWADataSet = New SignWriterStudio.SymbolCache.ISWA2010DataSet()
        Me.FilterMiddle = New System.Windows.Forms.CheckBox()
        Me.BtnReset = New System.Windows.Forms.Button()
        Me.FilterRing = New System.Windows.Forms.CheckBox()
        Me.BtnFilter = New System.Windows.Forms.Button()
        Me.FilterBaby = New System.Windows.Forms.CheckBox()
        Me.BtnBaseGroupName = New System.Windows.Forms.Label()
        Me.FilterThumb = New System.Windows.Forms.CheckBox()
        Me.FilterSymbolName = New System.Windows.Forms.TextBox()
        Me.FilterRootShape = New System.Windows.Forms.ComboBox()
        Me.ISWARootShapesQuickBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LbMultipleFinger = New System.Windows.Forms.Label()
        Me.LBRootShape = New System.Windows.Forms.Label()
        Me.FilterMultipleFingers = New System.Windows.Forms.ComboBox()
        Me.ISWAMultipleFingersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FilterActionFinger = New System.Windows.Forms.ComboBox()
        Me.ISWAActionFingersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LBActionFinger = New System.Windows.Forms.Label()
        Me.TPSpelling = New System.Windows.Forms.TabPage()
        Me.TPSequence = New System.Windows.Forms.GroupBox()
        Me.PanelSpelling = New System.Windows.Forms.Panel()
        Me.TVSequence = New System.Windows.Forms.TreeView()
        Me.btnSugg2 = New System.Windows.Forms.Button()
        Me.BtnDeleteAll = New System.Windows.Forms.Button()
        Me.btnSugg1 = New System.Windows.Forms.Button()
        Me.BtnAddChooser = New System.Windows.Forms.Button()
        Me.BtnUp = New System.Windows.Forms.Button()
        Me.BtnAddSign = New System.Windows.Forms.Button()
        Me.BtnDown = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.SPSignView = New System.Windows.Forms.SplitContainer()
        Me.GBSign = New System.Windows.Forms.GroupBox()
        Me.FindSymbol = New System.Windows.Forms.Button()
        Me.QuickSignEditorBtn = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnAccept = New System.Windows.Forms.Button()
        Me.grid = New System.Windows.Forms.CheckBox()
        Me.PanelSign = New System.Windows.Forms.Panel()
        Me.PBSign = New System.Windows.Forms.PictureBox()
        Me.CMSPBSign = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TSMIInsertsymbolOuts = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIRemoveSymbols = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIDuplicateSymbols = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMICenter = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMICenterHead = New System.Windows.Forms.ToolStripMenuItem()
        Me.OverlapSymbolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SeperateSymbolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveArea = New System.Windows.Forms.ToolStripMenuItem()
        Me.BottomLeft1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Bottom2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BottomRight3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MiddleLeft4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Middle5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MiddleRigh6ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TopLeft7ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Top8ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TopRight9ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMINextSymbol = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIPreviousSymbol = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMINextAddToSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIPreviousAddToSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.MoveTopMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIMoveUp = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIMoveDown = New System.Windows.Forms.ToolStripMenuItem()
        Me.MoveBottomMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ColorsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIBackOfHandColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIPalmOfHandColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIBackgroundColor = New System.Windows.Forms.ToolStripMenuItem()
        Me.ColorizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToBlackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SizeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMILarger = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMISmaller = New System.Windows.Forms.ToolStripMenuItem()
        Me.ALotLargerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ALotSmallerCtrlToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSMIUndo = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIRedo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.SignToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FrameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMINextFrame = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIPreviousFrame = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIRemoveFrame = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyImageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMICopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.TSMIDeleteSign = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteCtrlVToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MirrorSignToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeArrowHandsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GBFavorites = New System.Windows.Forms.GroupBox()
        Me.TVFavoriteSymbols = New System.Windows.Forms.TreeView()
        Me.BtnRemoveSymbol = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.BtnNewFavorite = New System.Windows.Forms.Button()
        Me.CBFavorites = New System.Windows.Forms.ComboBox()
        Me.ISWAFavoriteSymbolsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SettingsDataSet = New SignWriterStudio.Settings.SettingsDataSet()
        Me.lblBaseGroupName = New System.Windows.Forms.Label()
        Me.lblMultipleFinger = New System.Windows.Forms.Label()
        Me.lblActionFinger = New System.Windows.Forms.Label()
        Me.lblRootShape = New System.Windows.Forms.Label()
        Me.SymbolToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.ISWAActionFingersTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.actionfingerTableAdapter()
        Me.ISWABaseSymbolsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ISWABaseSymbolsTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.basesymbolTableAdapter()
        Me.ISWAFavoriteSymbolsTableAdapter = New SignWriterStudio.Settings.SettingsDataSetTableAdapters.FavoritesTableAdapter()
        Me.ISWAMultipleFingersTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.multiplefingerTableAdapter()
        Me.ISWARootShapeGroupsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ISWARootShapeGroupsTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.rootshapegroupTableAdapter()
        Me.ISWARootShapesQuickTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.rootshapequickTableAdapter()
        Me.ISWAThumbPositionsTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.thumbpositionTableAdapter()
        Me.HandsClassifiedBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.IswaSymbolsTableAdapter = New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.symbolTableAdapter()
        Me.SequenceMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SCSWEditor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SCSWEditor.Panel1.SuspendLayout()
        Me.SCSWEditor.Panel2.SuspendLayout()
        Me.SCSWEditor.SuspendLayout()
        Me.TCSymbols.SuspendLayout()
        Me.TPFavorites.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GBAllSymbols.SuspendLayout()
        Me.GBChoosers.SuspendLayout()
        CType(Me.PBsymbolOut, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPSearch.SuspendLayout()
        Me.GBHandSearch.SuspendLayout()
        CType(Me.PBHand, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWAThumbPositionsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWADataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWARootShapesQuickBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWAMultipleFingersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWAActionFingersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TPSpelling.SuspendLayout()
        Me.TPSequence.SuspendLayout()
        Me.PanelSpelling.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.SPSignView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SPSignView.Panel1.SuspendLayout()
        Me.SPSignView.Panel2.SuspendLayout()
        Me.SPSignView.SuspendLayout()
        Me.GBSign.SuspendLayout()
        Me.PanelSign.SuspendLayout()
        CType(Me.PBSign, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CMSPBSign.SuspendLayout()
        Me.GBFavorites.SuspendLayout()
        CType(Me.ISWAFavoriteSymbolsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SettingsDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWABaseSymbolsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ISWARootShapeGroupsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HandsClassifiedBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SequenceMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'SCSWEditor
        '
        Me.SCSWEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SCSWEditor.Location = New System.Drawing.Point(0, 0)
        Me.SCSWEditor.Name = "SCSWEditor"
        '
        'SCSWEditor.Panel1
        '
        Me.SCSWEditor.Panel1.AutoScroll = True
        Me.SCSWEditor.Panel1.AutoScrollMinSize = New System.Drawing.Size(20, 0)
        Me.SCSWEditor.Panel1.Controls.Add(Me.TCSymbols)
        '
        'SCSWEditor.Panel2
        '
        Me.SCSWEditor.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SCSWEditor.Size = New System.Drawing.Size(1468, 660)
        Me.SCSWEditor.SplitterDistance = 626
        Me.SCSWEditor.TabIndex = 0
        '
        'TCSymbols
        '
        Me.TCSymbols.Controls.Add(Me.TPFavorites)
        Me.TCSymbols.Controls.Add(Me.TPSearch)
        Me.TCSymbols.Controls.Add(Me.TPSpelling)
        Me.TCSymbols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TCSymbols.Location = New System.Drawing.Point(0, 0)
        Me.TCSymbols.Name = "TCSymbols"
        Me.TCSymbols.SelectedIndex = 0
        Me.TCSymbols.Size = New System.Drawing.Size(626, 660)
        Me.TCSymbols.TabIndex = 0
        '
        'TPFavorites
        '
        Me.TPFavorites.Controls.Add(Me.SplitContainer2)
        Me.TPFavorites.Location = New System.Drawing.Point(4, 22)
        Me.TPFavorites.Name = "TPFavorites"
        Me.TPFavorites.Padding = New System.Windows.Forms.Padding(3)
        Me.TPFavorites.Size = New System.Drawing.Size(618, 634)
        Me.TPFavorites.TabIndex = 1
        Me.TPFavorites.Text = "Symbols"
        Me.SymbolToolTip.SetToolTip(Me.TPFavorites, "Favorites [F5]")
        Me.TPFavorites.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GBAllSymbols)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GBChoosers)
        Me.SplitContainer2.Size = New System.Drawing.Size(612, 628)
        Me.SplitContainer2.SplitterDistance = 305
        Me.SplitContainer2.TabIndex = 0
        '
        'GBAllSymbols
        '
        Me.GBAllSymbols.Controls.Add(Me.TVAllGroups)
        Me.GBAllSymbols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBAllSymbols.Location = New System.Drawing.Point(0, 0)
        Me.GBAllSymbols.Name = "GBAllSymbols"
        Me.GBAllSymbols.Size = New System.Drawing.Size(305, 628)
        Me.GBAllSymbols.TabIndex = 0
        Me.GBAllSymbols.TabStop = False
        Me.GBAllSymbols.Text = "All Symbols"
        '
        'TVAllGroups
        '
        Me.TVAllGroups.BackColor = System.Drawing.SystemColors.Window
        Me.TVAllGroups.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TVAllGroups.FullRowSelect = True
        Me.TVAllGroups.HotTracking = True
        Me.TVAllGroups.Indent = 19
        Me.TVAllGroups.ItemHeight = 55
        Me.TVAllGroups.Location = New System.Drawing.Point(3, 16)
        Me.TVAllGroups.Name = "TVAllGroups"
        Me.TVAllGroups.ShowNodeToolTips = True
        Me.TVAllGroups.Size = New System.Drawing.Size(299, 609)
        Me.TVAllGroups.TabIndex = 2
        '
        'GBChoosers
        '
        Me.GBChoosers.Controls.Add(Me.btnChooserAdd)
        Me.GBChoosers.Controls.Add(Me.btnChooserReplace)
        Me.GBChoosers.Controls.Add(Me.TextBox1)
        Me.GBChoosers.Controls.Add(Me.PBsymbolOut)
        Me.GBChoosers.Controls.Add(Me.ArrowChooser)
        Me.GBChoosers.Controls.Add(Me.HandChooser)
        Me.GBChoosers.Controls.Add(Me.TVChooser)
        Me.GBChoosers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBChoosers.Location = New System.Drawing.Point(0, 0)
        Me.GBChoosers.Name = "GBChoosers"
        Me.GBChoosers.Size = New System.Drawing.Size(303, 628)
        Me.GBChoosers.TabIndex = 0
        Me.GBChoosers.TabStop = False
        Me.GBChoosers.Text = "Choosers"
        '
        'btnChooserAdd
        '
        Me.btnChooserAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChooserAdd.Location = New System.Drawing.Point(175, 47)
        Me.btnChooserAdd.Name = "btnChooserAdd"
        Me.btnChooserAdd.Size = New System.Drawing.Size(59, 22)
        Me.btnChooserAdd.TabIndex = 79
        Me.btnChooserAdd.Text = "Add"
        Me.SymbolToolTip.SetToolTip(Me.btnChooserAdd, "Add to Sign / Replace Selected Symbol in Sign [Enter]")
        Me.btnChooserAdd.UseVisualStyleBackColor = True
        '
        'btnChooserReplace
        '
        Me.btnChooserReplace.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChooserReplace.Location = New System.Drawing.Point(175, 20)
        Me.btnChooserReplace.Name = "btnChooserReplace"
        Me.btnChooserReplace.Size = New System.Drawing.Size(59, 22)
        Me.btnChooserReplace.TabIndex = 78
        Me.btnChooserReplace.Text = "Replace"
        Me.SymbolToolTip.SetToolTip(Me.btnChooserReplace, "Add to Sign / Replace Selected Symbol in Sign [Enter]")
        Me.btnChooserReplace.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.Location = New System.Drawing.Point(203, 75)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 75
        Me.TextBox1.Visible = False
        '
        'PBsymbolOut
        '
        Me.PBsymbolOut.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PBsymbolOut.BackColor = System.Drawing.Color.White
        Me.PBsymbolOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PBsymbolOut.Location = New System.Drawing.Point(240, 16)
        Me.PBsymbolOut.Name = "PBsymbolOut"
        Me.PBsymbolOut.Size = New System.Drawing.Size(60, 60)
        Me.PBsymbolOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PBsymbolOut.TabIndex = 73
        Me.PBsymbolOut.TabStop = False
        '
        'ArrowChooser
        '
        Me.ArrowChooser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ArrowChooser.AutoScroll = True
        Me.ArrowChooser.EditorForm = Nothing
        Me.ArrowChooser.Location = New System.Drawing.Point(47, 89)
        Me.ArrowChooser.Margin = New System.Windows.Forms.Padding(4)
        Me.ArrowChooser.Name = "ArrowChooser"
        Me.ArrowChooser.Size = New System.Drawing.Size(244, 546)
        Me.ArrowChooser.TabIndex = 77
        Me.ArrowChooser.Visible = False
        '
        'HandChooser
        '
        Me.HandChooser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HandChooser.AutoScroll = True
        Me.HandChooser.EditorForm = Nothing
        Me.HandChooser.Location = New System.Drawing.Point(49, 88)
        Me.HandChooser.Margin = New System.Windows.Forms.Padding(4)
        Me.HandChooser.Name = "HandChooser"
        Me.HandChooser.Size = New System.Drawing.Size(242, 565)
        Me.HandChooser.TabIndex = 76
        '
        'TVChooser
        '
        Me.TVChooser.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TVChooser.BackColor = System.Drawing.Color.White
        Me.TVChooser.HotTracking = True
        Me.TVChooser.Indent = 5
        Me.TVChooser.ItemHeight = 55
        Me.TVChooser.Location = New System.Drawing.Point(13, 75)
        Me.TVChooser.Name = "TVChooser"
        Me.TVChooser.ShowLines = False
        Me.TVChooser.ShowNodeToolTips = True
        Me.TVChooser.Size = New System.Drawing.Size(289, 556)
        Me.TVChooser.TabIndex = 74
        '
        'TPSearch
        '
        Me.TPSearch.Controls.Add(Me.GBHandSearch)
        Me.TPSearch.Location = New System.Drawing.Point(4, 22)
        Me.TPSearch.Name = "TPSearch"
        Me.TPSearch.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSearch.Size = New System.Drawing.Size(618, 634)
        Me.TPSearch.TabIndex = 2
        Me.TPSearch.Text = "Search"
        Me.SymbolToolTip.SetToolTip(Me.TPSearch, "Hand Search [F7]")
        Me.TPSearch.UseVisualStyleBackColor = True
        '
        'GBHandSearch
        '
        Me.GBHandSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBHandSearch.Controls.Add(Me.TVHand)
        Me.GBHandSearch.Controls.Add(Me.PBHand)
        Me.GBHandSearch.Controls.Add(Me.LBThumbPosition)
        Me.GBHandSearch.Controls.Add(Me.FilterIndex)
        Me.GBHandSearch.Controls.Add(Me.FilterThumbPosition)
        Me.GBHandSearch.Controls.Add(Me.FilterMiddle)
        Me.GBHandSearch.Controls.Add(Me.BtnReset)
        Me.GBHandSearch.Controls.Add(Me.FilterRing)
        Me.GBHandSearch.Controls.Add(Me.BtnFilter)
        Me.GBHandSearch.Controls.Add(Me.FilterBaby)
        Me.GBHandSearch.Controls.Add(Me.BtnBaseGroupName)
        Me.GBHandSearch.Controls.Add(Me.FilterThumb)
        Me.GBHandSearch.Controls.Add(Me.FilterSymbolName)
        Me.GBHandSearch.Controls.Add(Me.FilterRootShape)
        Me.GBHandSearch.Controls.Add(Me.LbMultipleFinger)
        Me.GBHandSearch.Controls.Add(Me.LBRootShape)
        Me.GBHandSearch.Controls.Add(Me.FilterMultipleFingers)
        Me.GBHandSearch.Controls.Add(Me.FilterActionFinger)
        Me.GBHandSearch.Controls.Add(Me.LBActionFinger)
        Me.GBHandSearch.Location = New System.Drawing.Point(356, 3)
        Me.GBHandSearch.Name = "GBHandSearch"
        Me.GBHandSearch.Size = New System.Drawing.Size(262, 635)
        Me.GBHandSearch.TabIndex = 1
        Me.GBHandSearch.TabStop = False
        Me.GBHandSearch.Text = "Hand Search"
        '
        'TVHand
        '
        Me.TVHand.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.TVHand.HotTracking = True
        Me.TVHand.Indent = 5
        Me.TVHand.ItemHeight = 55
        Me.TVHand.Location = New System.Drawing.Point(6, 174)
        Me.TVHand.Margin = New System.Windows.Forms.Padding(3, 3, 3, 100)
        Me.TVHand.Name = "TVHand"
        Me.TVHand.ShowLines = False
        Me.TVHand.ShowNodeToolTips = True
        Me.TVHand.Size = New System.Drawing.Size(250, 481)
        Me.TVHand.TabIndex = 49
        '
        'PBHand
        '
        Me.PBHand.Image = CType(resources.GetObject("PBHand.Image"), System.Drawing.Image)
        Me.PBHand.Location = New System.Drawing.Point(33, 115)
        Me.PBHand.Name = "PBHand"
        Me.PBHand.Size = New System.Drawing.Size(35, 35)
        Me.PBHand.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PBHand.TabIndex = 47
        Me.PBHand.TabStop = False
        '
        'LBThumbPosition
        '
        Me.LBThumbPosition.AutoSize = True
        Me.LBThumbPosition.Location = New System.Drawing.Point(10, 50)
        Me.LBThumbPosition.Name = "LBThumbPosition"
        Me.LBThumbPosition.Size = New System.Drawing.Size(77, 13)
        Me.LBThumbPosition.TabIndex = 3
        Me.LBThumbPosition.Text = "&ThumbPosition"
        '
        'FilterIndex
        '
        Me.FilterIndex.AutoSize = True
        Me.FilterIndex.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.FilterIndex.Location = New System.Drawing.Point(10, 105)
        Me.FilterIndex.Name = "FilterIndex"
        Me.FilterIndex.Size = New System.Drawing.Size(32, 17)
        Me.FilterIndex.TabIndex = 12
        Me.FilterIndex.Text = "4"
        Me.FilterIndex.ThreeState = True
        Me.FilterIndex.UseVisualStyleBackColor = True
        '
        'FilterThumbPosition
        '
        Me.FilterThumbPosition.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FilterThumbPosition.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.FilterThumbPosition.DataSource = Me.ISWAThumbPositionsBindingSource
        Me.FilterThumbPosition.DisplayMember = "ThumbPositionName"
        Me.FilterThumbPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FilterThumbPosition.FormattingEnabled = True
        Me.FilterThumbPosition.Location = New System.Drawing.Point(10, 66)
        Me.FilterThumbPosition.Name = "FilterThumbPosition"
        Me.FilterThumbPosition.Size = New System.Drawing.Size(121, 21)
        Me.FilterThumbPosition.TabIndex = 4
        Me.FilterThumbPosition.ValueMember = "IDThumbPosition"
        '
        'ISWAThumbPositionsBindingSource
        '
        Me.ISWAThumbPositionsBindingSource.DataMember = "thumbposition"
        Me.ISWAThumbPositionsBindingSource.DataSource = Me.ISWADataSet
        '
        'ISWADataSet
        '
        Me.ISWADataSet.DataSetName = "ISWADataSet"
        Me.ISWADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'FilterMiddle
        '
        Me.FilterMiddle.AutoSize = True
        Me.FilterMiddle.CheckAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.FilterMiddle.Location = New System.Drawing.Point(43, 86)
        Me.FilterMiddle.Name = "FilterMiddle"
        Me.FilterMiddle.Size = New System.Drawing.Size(17, 31)
        Me.FilterMiddle.TabIndex = 13
        Me.FilterMiddle.Text = "8"
        Me.FilterMiddle.ThreeState = True
        Me.FilterMiddle.UseVisualStyleBackColor = True
        '
        'BtnReset
        '
        Me.BtnReset.Location = New System.Drawing.Point(179, 124)
        Me.BtnReset.Name = "BtnReset"
        Me.BtnReset.Size = New System.Drawing.Size(75, 23)
        Me.BtnReset.TabIndex = 17
        Me.BtnReset.Text = "Reset"
        Me.SymbolToolTip.SetToolTip(Me.BtnReset, "Reset filters [Esc]")
        Me.BtnReset.UseVisualStyleBackColor = True
        '
        'FilterRing
        '
        Me.FilterRing.AutoSize = True
        Me.FilterRing.Location = New System.Drawing.Point(59, 106)
        Me.FilterRing.Name = "FilterRing"
        Me.FilterRing.Size = New System.Drawing.Size(32, 17)
        Me.FilterRing.TabIndex = 14
        Me.FilterRing.Text = "6"
        Me.FilterRing.ThreeState = True
        Me.FilterRing.UseVisualStyleBackColor = True
        '
        'BtnFilter
        '
        Me.BtnFilter.Location = New System.Drawing.Point(98, 124)
        Me.BtnFilter.Name = "BtnFilter"
        Me.BtnFilter.Size = New System.Drawing.Size(75, 23)
        Me.BtnFilter.TabIndex = 16
        Me.BtnFilter.Text = "Search"
        Me.SymbolToolTip.SetToolTip(Me.BtnFilter, "Search [Enter]")
        Me.BtnFilter.UseVisualStyleBackColor = True
        '
        'FilterBaby
        '
        Me.FilterBaby.AutoSize = True
        Me.FilterBaby.Location = New System.Drawing.Point(64, 120)
        Me.FilterBaby.Name = "FilterBaby"
        Me.FilterBaby.Size = New System.Drawing.Size(32, 17)
        Me.FilterBaby.TabIndex = 15
        Me.FilterBaby.Text = "3"
        Me.FilterBaby.ThreeState = True
        Me.FilterBaby.UseVisualStyleBackColor = True
        '
        'BtnBaseGroupName
        '
        Me.BtnBaseGroupName.AutoSize = True
        Me.BtnBaseGroupName.Location = New System.Drawing.Point(136, 86)
        Me.BtnBaseGroupName.Name = "BtnBaseGroupName"
        Me.BtnBaseGroupName.Size = New System.Drawing.Size(72, 13)
        Me.BtnBaseGroupName.TabIndex = 9
        Me.BtnBaseGroupName.Text = "&Symbol Name"
        '
        'FilterThumb
        '
        Me.FilterThumb.AutoSize = True
        Me.FilterThumb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.FilterThumb.Location = New System.Drawing.Point(4, 126)
        Me.FilterThumb.Name = "FilterThumb"
        Me.FilterThumb.Size = New System.Drawing.Size(32, 17)
        Me.FilterThumb.TabIndex = 11
        Me.FilterThumb.Text = "1"
        Me.FilterThumb.ThreeState = True
        Me.FilterThumb.UseVisualStyleBackColor = True
        '
        'FilterSymbolName
        '
        Me.FilterSymbolName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FilterSymbolName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.FilterSymbolName.Location = New System.Drawing.Point(136, 102)
        Me.FilterSymbolName.Name = "FilterSymbolName"
        Me.FilterSymbolName.Size = New System.Drawing.Size(120, 20)
        Me.FilterSymbolName.TabIndex = 10
        '
        'FilterRootShape
        '
        Me.FilterRootShape.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FilterRootShape.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.FilterRootShape.DataSource = Me.ISWARootShapesQuickBindingSource
        Me.FilterRootShape.DisplayMember = "RootshapeQuickName"
        Me.FilterRootShape.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FilterRootShape.FormattingEnabled = True
        Me.FilterRootShape.Location = New System.Drawing.Point(10, 28)
        Me.FilterRootShape.Name = "FilterRootShape"
        Me.FilterRootShape.Size = New System.Drawing.Size(121, 21)
        Me.FilterRootShape.TabIndex = 1
        Me.FilterRootShape.ValueMember = "IDRootshapeQuick"
        '
        'ISWARootShapesQuickBindingSource
        '
        Me.ISWARootShapesQuickBindingSource.DataMember = "rootshapequick"
        Me.ISWARootShapesQuickBindingSource.DataSource = Me.ISWADataSet
        '
        'LbMultipleFinger
        '
        Me.LbMultipleFinger.AutoSize = True
        Me.LbMultipleFinger.Location = New System.Drawing.Point(133, 50)
        Me.LbMultipleFinger.Name = "LbMultipleFinger"
        Me.LbMultipleFinger.Size = New System.Drawing.Size(75, 13)
        Me.LbMultipleFinger.TabIndex = 7
        Me.LbMultipleFinger.Text = "&Multiple Finger"
        '
        'LBRootShape
        '
        Me.LBRootShape.AutoSize = True
        Me.LBRootShape.Location = New System.Drawing.Point(7, 12)
        Me.LBRootShape.Name = "LBRootShape"
        Me.LBRootShape.Size = New System.Drawing.Size(61, 13)
        Me.LBRootShape.TabIndex = 0
        Me.LBRootShape.Text = "&RootShape"
        '
        'FilterMultipleFingers
        '
        Me.FilterMultipleFingers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FilterMultipleFingers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.FilterMultipleFingers.DataSource = Me.ISWAMultipleFingersBindingSource
        Me.FilterMultipleFingers.DisplayMember = "MultipleFingerName"
        Me.FilterMultipleFingers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FilterMultipleFingers.FormattingEnabled = True
        Me.FilterMultipleFingers.Location = New System.Drawing.Point(136, 65)
        Me.FilterMultipleFingers.Name = "FilterMultipleFingers"
        Me.FilterMultipleFingers.Size = New System.Drawing.Size(121, 21)
        Me.FilterMultipleFingers.TabIndex = 8
        Me.FilterMultipleFingers.ValueMember = "IDMultipleFinger"
        '
        'ISWAMultipleFingersBindingSource
        '
        Me.ISWAMultipleFingersBindingSource.DataMember = "multiplefinger"
        Me.ISWAMultipleFingersBindingSource.DataSource = Me.ISWADataSet
        '
        'FilterActionFinger
        '
        Me.FilterActionFinger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.FilterActionFinger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.FilterActionFinger.DataSource = Me.ISWAActionFingersBindingSource
        Me.FilterActionFinger.DisplayMember = "ActionFingerName"
        Me.FilterActionFinger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FilterActionFinger.FormattingEnabled = True
        Me.FilterActionFinger.Location = New System.Drawing.Point(136, 28)
        Me.FilterActionFinger.Name = "FilterActionFinger"
        Me.FilterActionFinger.Size = New System.Drawing.Size(121, 21)
        Me.FilterActionFinger.TabIndex = 6
        Me.FilterActionFinger.ValueMember = "IDActionFinger"
        '
        'ISWAActionFingersBindingSource
        '
        Me.ISWAActionFingersBindingSource.DataMember = "actionfinger"
        Me.ISWAActionFingersBindingSource.DataSource = Me.ISWADataSet
        '
        'LBActionFinger
        '
        Me.LBActionFinger.AutoSize = True
        Me.LBActionFinger.Location = New System.Drawing.Point(136, 13)
        Me.LBActionFinger.Name = "LBActionFinger"
        Me.LBActionFinger.Size = New System.Drawing.Size(65, 13)
        Me.LBActionFinger.TabIndex = 5
        Me.LBActionFinger.Text = "&Charateristic"
        '
        'TPSpelling
        '
        Me.TPSpelling.Controls.Add(Me.TPSequence)
        Me.TPSpelling.Location = New System.Drawing.Point(4, 22)
        Me.TPSpelling.Name = "TPSpelling"
        Me.TPSpelling.Padding = New System.Windows.Forms.Padding(3)
        Me.TPSpelling.Size = New System.Drawing.Size(618, 634)
        Me.TPSpelling.TabIndex = 4
        Me.TPSpelling.Text = "Spelling"
        Me.TPSpelling.UseVisualStyleBackColor = True
        '
        'TPSequence
        '
        Me.TPSequence.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TPSequence.Controls.Add(Me.PanelSpelling)
        Me.TPSequence.Controls.Add(Me.btnSugg2)
        Me.TPSequence.Controls.Add(Me.BtnDeleteAll)
        Me.TPSequence.Controls.Add(Me.btnSugg1)
        Me.TPSequence.Controls.Add(Me.BtnAddChooser)
        Me.TPSequence.Controls.Add(Me.BtnUp)
        Me.TPSequence.Controls.Add(Me.BtnAddSign)
        Me.TPSequence.Controls.Add(Me.BtnDown)
        Me.TPSequence.Controls.Add(Me.btnDelete)
        Me.TPSequence.Location = New System.Drawing.Point(317, 3)
        Me.TPSequence.Name = "TPSequence"
        Me.TPSequence.Size = New System.Drawing.Size(298, 633)
        Me.TPSequence.TabIndex = 69
        Me.TPSequence.TabStop = False
        Me.TPSequence.Text = "Spelling"
        '
        'PanelSpelling
        '
        Me.PanelSpelling.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PanelSpelling.AutoScroll = True
        Me.PanelSpelling.Controls.Add(Me.TVSequence)
        Me.PanelSpelling.Location = New System.Drawing.Point(0, 100)
        Me.PanelSpelling.Name = "PanelSpelling"
        Me.PanelSpelling.Size = New System.Drawing.Size(264, 533)
        Me.PanelSpelling.TabIndex = 64
        '
        'TVSequence
        '
        Me.TVSequence.AllowDrop = True
        Me.TVSequence.BackColor = System.Drawing.Color.White
        Me.TVSequence.HotTracking = True
        Me.TVSequence.Indent = 5
        Me.TVSequence.ItemHeight = 55
        Me.TVSequence.Location = New System.Drawing.Point(0, 0)
        Me.TVSequence.Name = "TVSequence"
        Me.TVSequence.ShowLines = False
        Me.TVSequence.ShowNodeToolTips = True
        Me.TVSequence.Size = New System.Drawing.Size(244, 533)
        Me.TVSequence.TabIndex = 51
        '
        'btnSugg2
        '
        Me.btnSugg2.Location = New System.Drawing.Point(142, 74)
        Me.btnSugg2.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSugg2.Name = "btnSugg2"
        Me.btnSugg2.Size = New System.Drawing.Size(102, 21)
        Me.btnSugg2.TabIndex = 59
        Me.btnSugg2.TabStop = False
        Me.btnSugg2.Text = "Suggestion 2"
        Me.SymbolToolTip.SetToolTip(Me.btnSugg2, "Add Selected Symbols from Sign [Ctrl-Ins]")
        Me.btnSugg2.UseVisualStyleBackColor = True
        Me.btnSugg2.Visible = False
        '
        'BtnDeleteAll
        '
        Me.BtnDeleteAll.Location = New System.Drawing.Point(142, 49)
        Me.BtnDeleteAll.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnDeleteAll.Name = "BtnDeleteAll"
        Me.BtnDeleteAll.Size = New System.Drawing.Size(101, 21)
        Me.BtnDeleteAll.TabIndex = 57
        Me.BtnDeleteAll.TabStop = False
        Me.BtnDeleteAll.Text = "Delete All"
        Me.SymbolToolTip.SetToolTip(Me.BtnDeleteAll, "Delete All [Ctrl-Del]")
        Me.BtnDeleteAll.UseVisualStyleBackColor = True
        '
        'btnSugg1
        '
        Me.btnSugg1.Location = New System.Drawing.Point(36, 74)
        Me.btnSugg1.Margin = New System.Windows.Forms.Padding(2)
        Me.btnSugg1.Name = "btnSugg1"
        Me.btnSugg1.Size = New System.Drawing.Size(102, 21)
        Me.btnSugg1.TabIndex = 58
        Me.btnSugg1.TabStop = False
        Me.btnSugg1.Text = "Suggestion"
        Me.SymbolToolTip.SetToolTip(Me.btnSugg1, "Add Selected Symbols from Sign [Ctrl-Ins]")
        Me.btnSugg1.UseVisualStyleBackColor = True
        '
        'BtnAddChooser
        '
        Me.BtnAddChooser.Location = New System.Drawing.Point(36, 26)
        Me.BtnAddChooser.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnAddChooser.Name = "BtnAddChooser"
        Me.BtnAddChooser.Size = New System.Drawing.Size(102, 21)
        Me.BtnAddChooser.TabIndex = 52
        Me.BtnAddChooser.TabStop = False
        Me.BtnAddChooser.Text = "Add from Chooser"
        Me.SymbolToolTip.SetToolTip(Me.BtnAddChooser, "Add from Chooser [Ins]")
        Me.BtnAddChooser.UseVisualStyleBackColor = True
        '
        'BtnUp
        '
        Me.BtnUp.Location = New System.Drawing.Point(247, 26)
        Me.BtnUp.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnUp.Name = "BtnUp"
        Me.BtnUp.Size = New System.Drawing.Size(46, 21)
        Me.BtnUp.TabIndex = 53
        Me.BtnUp.Text = "Up"
        Me.SymbolToolTip.SetToolTip(Me.BtnUp, "Move Selected Up [Ctrl-Up]")
        Me.BtnUp.UseVisualStyleBackColor = True
        '
        'BtnAddSign
        '
        Me.BtnAddSign.Location = New System.Drawing.Point(36, 49)
        Me.BtnAddSign.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnAddSign.Name = "BtnAddSign"
        Me.BtnAddSign.Size = New System.Drawing.Size(102, 21)
        Me.BtnAddSign.TabIndex = 54
        Me.BtnAddSign.TabStop = False
        Me.BtnAddSign.Text = "Add from Sign"
        Me.SymbolToolTip.SetToolTip(Me.BtnAddSign, "Add Selected Symbols from Sign [Ctrl-Ins]")
        Me.BtnAddSign.UseVisualStyleBackColor = True
        '
        'BtnDown
        '
        Me.BtnDown.Location = New System.Drawing.Point(247, 49)
        Me.BtnDown.Margin = New System.Windows.Forms.Padding(2)
        Me.BtnDown.Name = "BtnDown"
        Me.BtnDown.Size = New System.Drawing.Size(46, 21)
        Me.BtnDown.TabIndex = 55
        Me.BtnDown.Text = "Down"
        Me.SymbolToolTip.SetToolTip(Me.BtnDown, "Move Selected Down [Ctrl-Down]")
        Me.BtnDown.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(142, 26)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(2)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(101, 21)
        Me.btnDelete.TabIndex = 56
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete Selected DeleteSelectedSequence(TVSequence.SelectedNode)"
        Me.SymbolToolTip.SetToolTip(Me.btnDelete, "Delete [Del]")
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SPSignView)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GBFavorites)
        Me.SplitContainer3.Size = New System.Drawing.Size(838, 660)
        Me.SplitContainer3.SplitterDistance = 505
        Me.SplitContainer3.TabIndex = 69
        '
        'SPSignView
        '
        Me.SPSignView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SPSignView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SPSignView.IsSplitterFixed = True
        Me.SPSignView.Location = New System.Drawing.Point(0, 0)
        Me.SPSignView.Name = "SPSignView"
        Me.SPSignView.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SPSignView.Panel1
        '
        Me.SPSignView.Panel1.Controls.Add(Me.GBSign)
        '
        'SPSignView.Panel2
        '
        Me.SPSignView.Panel2.AutoScroll = True
        Me.SPSignView.Panel2.Controls.Add(Me.PanelSign)
        Me.SPSignView.Size = New System.Drawing.Size(505, 660)
        Me.SPSignView.SplitterDistance = 55
        Me.SPSignView.TabIndex = 68
        '
        'GBSign
        '
        Me.GBSign.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GBSign.Controls.Add(Me.FindSymbol)
        Me.GBSign.Controls.Add(Me.QuickSignEditorBtn)
        Me.GBSign.Controls.Add(Me.btnCancel)
        Me.GBSign.Controls.Add(Me.btnAccept)
        Me.GBSign.Controls.Add(Me.grid)
        Me.GBSign.Location = New System.Drawing.Point(0, 0)
        Me.GBSign.Name = "GBSign"
        Me.GBSign.Size = New System.Drawing.Size(505, 55)
        Me.GBSign.TabIndex = 56
        Me.GBSign.TabStop = False
        Me.GBSign.Text = "Sign"
        Me.SymbolToolTip.SetToolTip(Me.GBSign, "(F12)")
        '
        'FindSymbol
        '
        Me.FindSymbol.Location = New System.Drawing.Point(6, 19)
        Me.FindSymbol.Name = "FindSymbol"
        Me.FindSymbol.Size = New System.Drawing.Size(94, 22)
        Me.FindSymbol.TabIndex = 67
        Me.FindSymbol.TabStop = False
        Me.FindSymbol.Text = "Find in Choosers"
        Me.SymbolToolTip.SetToolTip(Me.FindSymbol, "Add to Sign / Replace Selected Symbol in Sign [Enter]")
        Me.FindSymbol.UseVisualStyleBackColor = True
        '
        'QuickSignEditorBtn
        '
        Me.QuickSignEditorBtn.Location = New System.Drawing.Point(292, 20)
        Me.QuickSignEditorBtn.Name = "QuickSignEditorBtn"
        Me.QuickSignEditorBtn.Size = New System.Drawing.Size(94, 22)
        Me.QuickSignEditorBtn.TabIndex = 63
        Me.QuickSignEditorBtn.TabStop = False
        Me.QuickSignEditorBtn.Text = "QuickSignEditor"
        Me.SymbolToolTip.SetToolTip(Me.QuickSignEditorBtn, "Add to Sign / Replace Selected Symbol in Sign [Enter]")
        Me.QuickSignEditorBtn.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(191, 19)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(52, 23)
        Me.btnCancel.TabIndex = 58
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel"
        Me.SymbolToolTip.SetToolTip(Me.btnCancel, "Cancel [Alt-F4]")
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnAccept
        '
        Me.btnAccept.Location = New System.Drawing.Point(133, 19)
        Me.btnAccept.Name = "btnAccept"
        Me.btnAccept.Size = New System.Drawing.Size(52, 23)
        Me.btnAccept.TabIndex = 57
        Me.btnAccept.TabStop = False
        Me.btnAccept.Text = "Accept"
        Me.SymbolToolTip.SetToolTip(Me.btnAccept, "Accept Sign [Ctrl-Enter]")
        Me.btnAccept.UseVisualStyleBackColor = True
        '
        'grid
        '
        Me.grid.Appearance = System.Windows.Forms.Appearance.Button
        Me.grid.AutoSize = True
        Me.grid.Location = New System.Drawing.Point(392, 19)
        Me.grid.Name = "grid"
        Me.grid.Size = New System.Drawing.Size(36, 23)
        Me.grid.TabIndex = 66
        Me.grid.TabStop = False
        Me.grid.Text = "Grid"
        Me.grid.UseVisualStyleBackColor = True
        '
        'PanelSign
        '
        Me.PanelSign.AutoScroll = True
        Me.PanelSign.Controls.Add(Me.PBSign)
        Me.PanelSign.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSign.Location = New System.Drawing.Point(0, 0)
        Me.PanelSign.Name = "PanelSign"
        Me.PanelSign.Size = New System.Drawing.Size(505, 601)
        Me.PanelSign.TabIndex = 43
        '
        'PBSign
        '
        Me.PBSign.BackColor = System.Drawing.Color.White
        Me.PBSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PBSign.ContextMenuStrip = Me.CMSPBSign
        Me.PBSign.Location = New System.Drawing.Point(0, 0)
        Me.PBSign.Name = "PBSign"
        Me.PBSign.Size = New System.Drawing.Size(500, 500)
        Me.PBSign.TabIndex = 42
        Me.PBSign.TabStop = False
        '
        'CMSPBSign
        '
        Me.CMSPBSign.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMIInsertsymbolOuts, Me.TSMIRemoveSymbols, Me.TSMIDuplicateSymbols, Me.ToolStripSeparator3, Me.TSMICenter, Me.TSMICenterHead, Me.OverlapSymbolsToolStripMenuItem, Me.SeperateSymbolsToolStripMenuItem, Me.MoveArea, Me.ToolStripSeparator2, Me.SelectAllToolStripMenuItem, Me.TSMINextSymbol, Me.TSMIPreviousSymbol, Me.TSMINextAddToSelected, Me.TSMIPreviousAddToSelected, Me.ToolStripSeparator5, Me.MoveTopMenuItem, Me.TSMIMoveUp, Me.TSMIMoveDown, Me.MoveBottomMenuItem, Me.ToolStripSeparator4, Me.ColorsToolStripMenuItem, Me.SizeToolStripMenuItem, Me.ToolStripSeparator1, Me.TSMIUndo, Me.TSMIRedo, Me.ToolStripSeparator6, Me.SignToolStripMenuItem, Me.MirrorSignToolStripMenuItem, Me.ChangeArrowHandsToolStripMenuItem})
        Me.CMSPBSign.Name = "ContextMenuStrip1"
        Me.CMSPBSign.Size = New System.Drawing.Size(304, 568)
        '
        'TSMIInsertsymbolOuts
        '
        Me.TSMIInsertsymbolOuts.Name = "TSMIInsertsymbolOuts"
        Me.TSMIInsertsymbolOuts.ShortcutKeys = System.Windows.Forms.Keys.Insert
        Me.TSMIInsertsymbolOuts.Size = New System.Drawing.Size(303, 22)
        Me.TSMIInsertsymbolOuts.Text = "Insert Symbol"
        '
        'TSMIRemoveSymbols
        '
        Me.TSMIRemoveSymbols.Name = "TSMIRemoveSymbols"
        Me.TSMIRemoveSymbols.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.TSMIRemoveSymbols.Size = New System.Drawing.Size(303, 22)
        Me.TSMIRemoveSymbols.Text = "Remove Symbols"
        '
        'TSMIDuplicateSymbols
        '
        Me.TSMIDuplicateSymbols.Name = "TSMIDuplicateSymbols"
        Me.TSMIDuplicateSymbols.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.TSMIDuplicateSymbols.Size = New System.Drawing.Size(303, 22)
        Me.TSMIDuplicateSymbols.Text = "Duplicate Symbols"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(300, 6)
        '
        'TSMICenter
        '
        Me.TSMICenter.Name = "TSMICenter"
        Me.TSMICenter.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.TSMICenter.Size = New System.Drawing.Size(303, 22)
        Me.TSMICenter.Text = "Center "
        '
        'TSMICenterHead
        '
        Me.TSMICenterHead.Name = "TSMICenterHead"
        Me.TSMICenterHead.ShortcutKeys = CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.TSMICenterHead.Size = New System.Drawing.Size(303, 22)
        Me.TSMICenterHead.Text = "Center Head"
        '
        'OverlapSymbolsToolStripMenuItem
        '
        Me.OverlapSymbolsToolStripMenuItem.Name = "OverlapSymbolsToolStripMenuItem"
        Me.OverlapSymbolsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OverlapSymbolsToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.OverlapSymbolsToolStripMenuItem.Text = "Overlap Symbols"
        '
        'SeperateSymbolsToolStripMenuItem
        '
        Me.SeperateSymbolsToolStripMenuItem.Name = "SeperateSymbolsToolStripMenuItem"
        Me.SeperateSymbolsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SeperateSymbolsToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.SeperateSymbolsToolStripMenuItem.Text = "Seperate Symbols"
        '
        'MoveArea
        '
        Me.MoveArea.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BottomLeft1ToolStripMenuItem, Me.Bottom2ToolStripMenuItem, Me.BottomRight3ToolStripMenuItem, Me.MiddleLeft4ToolStripMenuItem, Me.Middle5ToolStripMenuItem, Me.MiddleRigh6ToolStripMenuItem, Me.TopLeft7ToolStripMenuItem, Me.Top8ToolStripMenuItem, Me.TopRight9ToolStripMenuItem})
        Me.MoveArea.Name = "MoveArea"
        Me.MoveArea.Size = New System.Drawing.Size(303, 22)
        Me.MoveArea.Text = "Move Selected to Area"
        '
        'BottomLeft1ToolStripMenuItem
        '
        Me.BottomLeft1ToolStripMenuItem.Name = "BottomLeft1ToolStripMenuItem"
        Me.BottomLeft1ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.BottomLeft1ToolStripMenuItem.Text = "Bottom Left [1]"
        '
        'Bottom2ToolStripMenuItem
        '
        Me.Bottom2ToolStripMenuItem.Name = "Bottom2ToolStripMenuItem"
        Me.Bottom2ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.Bottom2ToolStripMenuItem.Text = "Bottom [2]"
        '
        'BottomRight3ToolStripMenuItem
        '
        Me.BottomRight3ToolStripMenuItem.Name = "BottomRight3ToolStripMenuItem"
        Me.BottomRight3ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.BottomRight3ToolStripMenuItem.Text = "Bottom Right [3]"
        '
        'MiddleLeft4ToolStripMenuItem
        '
        Me.MiddleLeft4ToolStripMenuItem.Name = "MiddleLeft4ToolStripMenuItem"
        Me.MiddleLeft4ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.MiddleLeft4ToolStripMenuItem.Text = "Middle Left [4]"
        '
        'Middle5ToolStripMenuItem
        '
        Me.Middle5ToolStripMenuItem.Name = "Middle5ToolStripMenuItem"
        Me.Middle5ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.Middle5ToolStripMenuItem.Text = "Middle [5]"
        '
        'MiddleRigh6ToolStripMenuItem
        '
        Me.MiddleRigh6ToolStripMenuItem.Name = "MiddleRigh6ToolStripMenuItem"
        Me.MiddleRigh6ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.MiddleRigh6ToolStripMenuItem.Text = "Middle Righ [6]"
        '
        'TopLeft7ToolStripMenuItem
        '
        Me.TopLeft7ToolStripMenuItem.Name = "TopLeft7ToolStripMenuItem"
        Me.TopLeft7ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.TopLeft7ToolStripMenuItem.Text = "Top Left [7]"
        '
        'Top8ToolStripMenuItem
        '
        Me.Top8ToolStripMenuItem.Name = "Top8ToolStripMenuItem"
        Me.Top8ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.Top8ToolStripMenuItem.Text = "Top [8]"
        '
        'TopRight9ToolStripMenuItem
        '
        Me.TopRight9ToolStripMenuItem.Name = "TopRight9ToolStripMenuItem"
        Me.TopRight9ToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.TopRight9ToolStripMenuItem.Text = "Top Right [9]"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(300, 6)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.SelectAllToolStripMenuItem.Text = "Select All"
        '
        'TSMINextSymbol
        '
        Me.TSMINextSymbol.Name = "TSMINextSymbol"
        Me.TSMINextSymbol.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.TSMINextSymbol.Size = New System.Drawing.Size(303, 22)
        Me.TSMINextSymbol.Text = "Next Symbol"
        '
        'TSMIPreviousSymbol
        '
        Me.TSMIPreviousSymbol.Name = "TSMIPreviousSymbol"
        Me.TSMIPreviousSymbol.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.TSMIPreviousSymbol.Size = New System.Drawing.Size(303, 22)
        Me.TSMIPreviousSymbol.Text = "Previous Symbol"
        '
        'TSMINextAddToSelected
        '
        Me.TSMINextAddToSelected.Name = "TSMINextAddToSelected"
        Me.TSMINextAddToSelected.ShortcutKeys = CType(((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.TSMINextAddToSelected.Size = New System.Drawing.Size(303, 22)
        Me.TSMINextAddToSelected.Text = "Next Add to Selected"
        '
        'TSMIPreviousAddToSelected
        '
        Me.TSMIPreviousAddToSelected.Name = "TSMIPreviousAddToSelected"
        Me.TSMIPreviousAddToSelected.ShortcutKeys = CType((((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.TSMIPreviousAddToSelected.Size = New System.Drawing.Size(303, 22)
        Me.TSMIPreviousAddToSelected.Text = "Previous Add to Selected"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(300, 6)
        '
        'MoveTopMenuItem
        '
        Me.MoveTopMenuItem.Name = "MoveTopMenuItem"
        Me.MoveTopMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.MoveTopMenuItem.Text = "Move Top"
        '
        'TSMIMoveUp
        '
        Me.TSMIMoveUp.Name = "TSMIMoveUp"
        Me.TSMIMoveUp.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.TSMIMoveUp.Size = New System.Drawing.Size(303, 22)
        Me.TSMIMoveUp.Text = "Move Up"
        '
        'TSMIMoveDown
        '
        Me.TSMIMoveDown.Name = "TSMIMoveDown"
        Me.TSMIMoveDown.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Down), System.Windows.Forms.Keys)
        Me.TSMIMoveDown.Size = New System.Drawing.Size(303, 22)
        Me.TSMIMoveDown.Text = "Move Down"
        '
        'MoveBottomMenuItem
        '
        Me.MoveBottomMenuItem.Name = "MoveBottomMenuItem"
        Me.MoveBottomMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.MoveBottomMenuItem.Text = "Move Bottom"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(300, 6)
        '
        'ColorsToolStripMenuItem
        '
        Me.ColorsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMIBackOfHandColor, Me.TSMIPalmOfHandColor, Me.TSMIBackgroundColor, Me.ColorizeToolStripMenuItem, Me.ToBlackToolStripMenuItem})
        Me.ColorsToolStripMenuItem.Name = "ColorsToolStripMenuItem"
        Me.ColorsToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ColorsToolStripMenuItem.Text = "Colors"
        '
        'TSMIBackOfHandColor
        '
        Me.TSMIBackOfHandColor.Name = "TSMIBackOfHandColor"
        Me.TSMIBackOfHandColor.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.TSMIBackOfHandColor.Size = New System.Drawing.Size(197, 22)
        Me.TSMIBackOfHandColor.Text = "Principal Color"
        '
        'TSMIPalmOfHandColor
        '
        Me.TSMIPalmOfHandColor.Name = "TSMIPalmOfHandColor"
        Me.TSMIPalmOfHandColor.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.TSMIPalmOfHandColor.Size = New System.Drawing.Size(197, 22)
        Me.TSMIPalmOfHandColor.Text = "Secondary Color"
        '
        'TSMIBackgroundColor
        '
        Me.TSMIBackgroundColor.Name = "TSMIBackgroundColor"
        Me.TSMIBackgroundColor.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.TSMIBackgroundColor.Size = New System.Drawing.Size(197, 22)
        Me.TSMIBackgroundColor.Text = "Background"
        '
        'ColorizeToolStripMenuItem
        '
        Me.ColorizeToolStripMenuItem.Name = "ColorizeToolStripMenuItem"
        Me.ColorizeToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.ColorizeToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ColorizeToolStripMenuItem.Text = "Colorize"
        '
        'ToBlackToolStripMenuItem
        '
        Me.ToBlackToolStripMenuItem.Name = "ToBlackToolStripMenuItem"
        Me.ToBlackToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.ToBlackToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.ToBlackToolStripMenuItem.Text = "To Black"
        '
        'SizeToolStripMenuItem
        '
        Me.SizeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMILarger, Me.TSMISmaller, Me.ALotLargerToolStripMenuItem, Me.ALotSmallerCtrlToolStripMenuItem})
        Me.SizeToolStripMenuItem.Name = "SizeToolStripMenuItem"
        Me.SizeToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.SizeToolStripMenuItem.Text = "Size"
        '
        'TSMILarger
        '
        Me.TSMILarger.Name = "TSMILarger"
        Me.TSMILarger.Size = New System.Drawing.Size(190, 22)
        Me.TSMILarger.Text = "Larger [+]"
        '
        'TSMISmaller
        '
        Me.TSMISmaller.Name = "TSMISmaller"
        Me.TSMISmaller.Size = New System.Drawing.Size(190, 22)
        Me.TSMISmaller.Text = "Smaller [-]"
        '
        'ALotLargerToolStripMenuItem
        '
        Me.ALotLargerToolStripMenuItem.Name = "ALotLargerToolStripMenuItem"
        Me.ALotLargerToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ALotLargerToolStripMenuItem.Text = "A lot Larger [Ctrl - +]"
        '
        'ALotSmallerCtrlToolStripMenuItem
        '
        Me.ALotSmallerCtrlToolStripMenuItem.Name = "ALotSmallerCtrlToolStripMenuItem"
        Me.ALotSmallerCtrlToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.ALotSmallerCtrlToolStripMenuItem.Text = "A lot Smaller [Ctrl - +]"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(300, 6)
        '
        'TSMIUndo
        '
        Me.TSMIUndo.Name = "TSMIUndo"
        Me.TSMIUndo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.TSMIUndo.Size = New System.Drawing.Size(303, 22)
        Me.TSMIUndo.Text = "Undo"
        '
        'TSMIRedo
        '
        Me.TSMIRedo.Name = "TSMIRedo"
        Me.TSMIRedo.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Y), System.Windows.Forms.Keys)
        Me.TSMIRedo.Size = New System.Drawing.Size(303, 22)
        Me.TSMIRedo.Text = "Redo"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(300, 6)
        '
        'SignToolStripMenuItem
        '
        Me.SignToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FrameToolStripMenuItem, Me.CopyImageToolStripMenuItem, Me.TSMICopy, Me.TSMIDeleteSign, Me.PasteCtrlVToolStripMenuItem})
        Me.SignToolStripMenuItem.Name = "SignToolStripMenuItem"
        Me.SignToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.SignToolStripMenuItem.Text = "Sign"
        '
        'FrameToolStripMenuItem
        '
        Me.FrameToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSMINextFrame, Me.TSMIPreviousFrame, Me.TSMIRemoveFrame})
        Me.FrameToolStripMenuItem.Name = "FrameToolStripMenuItem"
        Me.FrameToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.FrameToolStripMenuItem.Text = "Frame"
        Me.FrameToolStripMenuItem.Visible = False
        '
        'TSMINextFrame
        '
        Me.TSMINextFrame.Name = "TSMINextFrame"
        Me.TSMINextFrame.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.TSMINextFrame.Size = New System.Drawing.Size(227, 22)
        Me.TSMINextFrame.Text = "Next Frame"
        '
        'TSMIPreviousFrame
        '
        Me.TSMIPreviousFrame.Name = "TSMIPreviousFrame"
        Me.TSMIPreviousFrame.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.TSMIPreviousFrame.Size = New System.Drawing.Size(227, 22)
        Me.TSMIPreviousFrame.Text = "Previous Frame"
        '
        'TSMIRemoveFrame
        '
        Me.TSMIRemoveFrame.Name = "TSMIRemoveFrame"
        Me.TSMIRemoveFrame.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Alt) _
            Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.TSMIRemoveFrame.Size = New System.Drawing.Size(227, 22)
        Me.TSMIRemoveFrame.Text = "Remove Frame"
        '
        'CopyImageToolStripMenuItem
        '
        Me.CopyImageToolStripMenuItem.Name = "CopyImageToolStripMenuItem"
        Me.CopyImageToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyImageToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.CopyImageToolStripMenuItem.Text = "Copy Image"
        '
        'TSMICopy
        '
        Me.TSMICopy.Name = "TSMICopy"
        Me.TSMICopy.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.TSMICopy.Size = New System.Drawing.Size(202, 22)
        Me.TSMICopy.Text = "Copy Sign"
        '
        'TSMIDeleteSign
        '
        Me.TSMIDeleteSign.Name = "TSMIDeleteSign"
        Me.TSMIDeleteSign.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Delete), System.Windows.Forms.Keys)
        Me.TSMIDeleteSign.Size = New System.Drawing.Size(202, 22)
        Me.TSMIDeleteSign.Text = "Delete Sign"
        '
        'PasteCtrlVToolStripMenuItem
        '
        Me.PasteCtrlVToolStripMenuItem.Name = "PasteCtrlVToolStripMenuItem"
        Me.PasteCtrlVToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteCtrlVToolStripMenuItem.Size = New System.Drawing.Size(202, 22)
        Me.PasteCtrlVToolStripMenuItem.Text = "Paste into Sign"
        '
        'MirrorSignToolStripMenuItem
        '
        Me.MirrorSignToolStripMenuItem.Name = "MirrorSignToolStripMenuItem"
        Me.MirrorSignToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.MirrorSignToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.MirrorSignToolStripMenuItem.Text = "Mirror Sign"
        '
        'ChangeArrowHandsToolStripMenuItem
        '
        Me.ChangeArrowHandsToolStripMenuItem.Name = "ChangeArrowHandsToolStripMenuItem"
        Me.ChangeArrowHandsToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.H), System.Windows.Forms.Keys)
        Me.ChangeArrowHandsToolStripMenuItem.Size = New System.Drawing.Size(303, 22)
        Me.ChangeArrowHandsToolStripMenuItem.Text = "Change arrow hands"
        '
        'GBFavorites
        '
        Me.GBFavorites.Controls.Add(Me.TVFavoriteSymbols)
        Me.GBFavorites.Controls.Add(Me.BtnRemoveSymbol)
        Me.GBFavorites.Controls.Add(Me.btnAdd)
        Me.GBFavorites.Controls.Add(Me.BtnNewFavorite)
        Me.GBFavorites.Controls.Add(Me.CBFavorites)
        Me.GBFavorites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GBFavorites.Location = New System.Drawing.Point(0, 0)
        Me.GBFavorites.Name = "GBFavorites"
        Me.GBFavorites.Size = New System.Drawing.Size(329, 660)
        Me.GBFavorites.TabIndex = 6
        Me.GBFavorites.TabStop = False
        Me.GBFavorites.Text = "Favorites"
        '
        'TVFavoriteSymbols
        '
        Me.TVFavoriteSymbols.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TVFavoriteSymbols.FullRowSelect = True
        Me.TVFavoriteSymbols.HotTracking = True
        Me.TVFavoriteSymbols.Indent = 5
        Me.TVFavoriteSymbols.ItemHeight = 100
        Me.TVFavoriteSymbols.Location = New System.Drawing.Point(5, 75)
        Me.TVFavoriteSymbols.Name = "TVFavoriteSymbols"
        Me.TVFavoriteSymbols.ShowLines = False
        Me.TVFavoriteSymbols.ShowNodeToolTips = True
        Me.TVFavoriteSymbols.Size = New System.Drawing.Size(319, 579)
        Me.TVFavoriteSymbols.TabIndex = 2
        '
        'BtnRemoveSymbol
        '
        Me.BtnRemoveSymbol.Location = New System.Drawing.Point(115, 43)
        Me.BtnRemoveSymbol.Name = "BtnRemoveSymbol"
        Me.BtnRemoveSymbol.Size = New System.Drawing.Size(68, 23)
        Me.BtnRemoveSymbol.TabIndex = 3
        Me.BtnRemoveSymbol.TabStop = False
        Me.BtnRemoveSymbol.Text = "Remove"
        Me.SymbolToolTip.SetToolTip(Me.BtnRemoveSymbol, "Remove Selected Favorite [Del]")
        Me.BtnRemoveSymbol.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(6, 18)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(103, 47)
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.TabStop = False
        Me.btnAdd.Text = "Add"
        Me.SymbolToolTip.SetToolTip(Me.btnAdd, "Add from Sign [Ins]")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'BtnNewFavorite
        '
        Me.BtnNewFavorite.Location = New System.Drawing.Point(115, 17)
        Me.BtnNewFavorite.Name = "BtnNewFavorite"
        Me.BtnNewFavorite.Size = New System.Drawing.Size(68, 23)
        Me.BtnNewFavorite.TabIndex = 4
        Me.BtnNewFavorite.TabStop = False
        Me.BtnNewFavorite.Text = "New"
        Me.SymbolToolTip.SetToolTip(Me.BtnNewFavorite, "Add from Sign [Ins]")
        Me.BtnNewFavorite.UseVisualStyleBackColor = True
        '
        'CBFavorites
        '
        Me.CBFavorites.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CBFavorites.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CBFavorites.DataSource = Me.ISWAFavoriteSymbolsBindingSource
        Me.CBFavorites.DisplayMember = "FavoriteName"
        Me.CBFavorites.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CBFavorites.DropDownWidth = 200
        Me.CBFavorites.FormattingEnabled = True
        Me.CBFavorites.Location = New System.Drawing.Point(105, 32)
        Me.CBFavorites.Name = "CBFavorites"
        Me.CBFavorites.Size = New System.Drawing.Size(204, 21)
        Me.CBFavorites.TabIndex = 1
        Me.CBFavorites.ValueMember = "SSS"
        Me.CBFavorites.Visible = False
        '
        'ISWAFavoriteSymbolsBindingSource
        '
        Me.ISWAFavoriteSymbolsBindingSource.DataMember = "Favorites"
        Me.ISWAFavoriteSymbolsBindingSource.DataSource = Me.SettingsDataSet
        Me.ISWAFavoriteSymbolsBindingSource.Sort = ""
        '
        'SettingsDataSet
        '
        Me.SettingsDataSet.DataSetName = "SettingsDataSet"
        Me.SettingsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblBaseGroupName
        '
        Me.lblBaseGroupName.Location = New System.Drawing.Point(0, 0)
        Me.lblBaseGroupName.Name = "lblBaseGroupName"
        Me.lblBaseGroupName.Size = New System.Drawing.Size(100, 23)
        Me.lblBaseGroupName.TabIndex = 0
        '
        'lblMultipleFinger
        '
        Me.lblMultipleFinger.Location = New System.Drawing.Point(0, 0)
        Me.lblMultipleFinger.Name = "lblMultipleFinger"
        Me.lblMultipleFinger.Size = New System.Drawing.Size(100, 23)
        Me.lblMultipleFinger.TabIndex = 0
        '
        'lblActionFinger
        '
        Me.lblActionFinger.Location = New System.Drawing.Point(0, 0)
        Me.lblActionFinger.Name = "lblActionFinger"
        Me.lblActionFinger.Size = New System.Drawing.Size(100, 23)
        Me.lblActionFinger.TabIndex = 0
        '
        'lblRootShape
        '
        Me.lblRootShape.Location = New System.Drawing.Point(0, 0)
        Me.lblRootShape.Name = "lblRootShape"
        Me.lblRootShape.Size = New System.Drawing.Size(100, 23)
        Me.lblRootShape.TabIndex = 0
        '
        'SymbolToolTip
        '
        Me.SymbolToolTip.AutoPopDelay = 5000
        Me.SymbolToolTip.InitialDelay = 500
        Me.SymbolToolTip.IsBalloon = True
        Me.SymbolToolTip.ReshowDelay = 100
        '
        'ColorDialog1
        '
        Me.ColorDialog1.AnyColor = True
        Me.ColorDialog1.FullOpen = True
        '
        'ISWAActionFingersTableAdapter
        '
        Me.ISWAActionFingersTableAdapter.ClearBeforeFill = True
        '
        'ISWABaseSymbolsBindingSource
        '
        Me.ISWABaseSymbolsBindingSource.DataMember = "basesymbol"
        Me.ISWABaseSymbolsBindingSource.DataSource = Me.ISWADataSet
        '
        'ISWABaseSymbolsTableAdapter
        '
        Me.ISWABaseSymbolsTableAdapter.ClearBeforeFill = True
        '
        'ISWAFavoriteSymbolsTableAdapter
        '
        Me.ISWAFavoriteSymbolsTableAdapter.ClearBeforeFill = True
        '
        'ISWAMultipleFingersTableAdapter
        '
        Me.ISWAMultipleFingersTableAdapter.ClearBeforeFill = True
        '
        'ISWARootShapeGroupsBindingSource
        '
        Me.ISWARootShapeGroupsBindingSource.DataMember = "rootshapegroup"
        Me.ISWARootShapeGroupsBindingSource.DataSource = Me.ISWADataSet
        '
        'ISWARootShapeGroupsTableAdapter
        '
        Me.ISWARootShapeGroupsTableAdapter.ClearBeforeFill = True
        '
        'ISWARootShapesQuickTableAdapter
        '
        Me.ISWARootShapesQuickTableAdapter.ClearBeforeFill = True
        '
        'ISWAThumbPositionsTableAdapter
        '
        Me.ISWAThumbPositionsTableAdapter.ClearBeforeFill = True
        '
        'IswaSymbolsTableAdapter
        '
        Me.IswaSymbolsTableAdapter.ClearBeforeFill = True
        '
        'SequenceMenuStrip
        '
        Me.SequenceMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UpToolStripMenuItem, Me.DownToolStripMenuItem, Me.DeleteToolStripMenuItem})
        Me.SequenceMenuStrip.Name = "SequenceMenuStrip"
        Me.SequenceMenuStrip.Size = New System.Drawing.Size(108, 70)
        '
        'UpToolStripMenuItem
        '
        Me.UpToolStripMenuItem.Name = "UpToolStripMenuItem"
        Me.UpToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.UpToolStripMenuItem.Text = "Up"
        '
        'DownToolStripMenuItem
        '
        Me.DownToolStripMenuItem.Name = "DownToolStripMenuItem"
        Me.DownToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DownToolStripMenuItem.Text = "Down"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1468, 660)
        Me.Controls.Add(Me.SCSWEditor)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Editor"
        Me.Text = "SignWriter Studio� SignEditor"
        Me.SCSWEditor.Panel1.ResumeLayout(False)
        Me.SCSWEditor.Panel2.ResumeLayout(False)
        CType(Me.SCSWEditor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SCSWEditor.ResumeLayout(False)
        Me.TCSymbols.ResumeLayout(False)
        Me.TPFavorites.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.GBAllSymbols.ResumeLayout(False)
        Me.GBChoosers.ResumeLayout(False)
        Me.GBChoosers.PerformLayout()
        CType(Me.PBsymbolOut, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPSearch.ResumeLayout(False)
        Me.GBHandSearch.ResumeLayout(False)
        Me.GBHandSearch.PerformLayout()
        CType(Me.PBHand, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWAThumbPositionsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWADataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWARootShapesQuickBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWAMultipleFingersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWAActionFingersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TPSpelling.ResumeLayout(False)
        Me.TPSequence.ResumeLayout(False)
        Me.PanelSpelling.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.SPSignView.Panel1.ResumeLayout(False)
        Me.SPSignView.Panel2.ResumeLayout(False)
        CType(Me.SPSignView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SPSignView.ResumeLayout(False)
        Me.GBSign.ResumeLayout(False)
        Me.GBSign.PerformLayout()
        Me.PanelSign.ResumeLayout(False)
        CType(Me.PBSign, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CMSPBSign.ResumeLayout(False)
        Me.GBFavorites.ResumeLayout(False)
        CType(Me.ISWAFavoriteSymbolsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SettingsDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWABaseSymbolsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ISWARootShapeGroupsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HandsClassifiedBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SequenceMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SCSWEditor As System.Windows.Forms.SplitContainer
    Friend WithEvents TCSymbols As System.Windows.Forms.TabControl
    Friend WithEvents TPFavorites As System.Windows.Forms.TabPage
    Friend WithEvents TPSearch As System.Windows.Forms.TabPage
    Friend WithEvents BtnRemoveSymbol As System.Windows.Forms.Button
    Friend WithEvents BtnNewFavorite As System.Windows.Forms.Button
    Friend WithEvents CBFavorites As System.Windows.Forms.ComboBox
    Friend WithEvents lblBaseGroupName As System.Windows.Forms.Label
    Friend WithEvents lblMultipleFinger As System.Windows.Forms.Label
    Friend WithEvents lblActionFinger As System.Windows.Forms.Label
    Friend WithEvents lblRootShape As System.Windows.Forms.Label
    Friend WithEvents LBThumbPosition As System.Windows.Forms.Label
    Friend WithEvents FilterThumbPosition As System.Windows.Forms.ComboBox
    Friend WithEvents BtnReset As System.Windows.Forms.Button
    Friend WithEvents BtnFilter As System.Windows.Forms.Button
    Friend WithEvents BtnBaseGroupName As System.Windows.Forms.Label
    Friend WithEvents FilterSymbolName As System.Windows.Forms.TextBox
    Friend WithEvents LbMultipleFinger As System.Windows.Forms.Label
    Friend WithEvents FilterMultipleFingers As System.Windows.Forms.ComboBox
    Friend WithEvents LBActionFinger As System.Windows.Forms.Label
    Friend WithEvents FilterActionFinger As System.Windows.Forms.ComboBox
    Friend WithEvents FilterRootShape As System.Windows.Forms.ComboBox
    Friend WithEvents FilterThumb As System.Windows.Forms.CheckBox
    Friend WithEvents FilterBaby As System.Windows.Forms.CheckBox
    Friend WithEvents FilterRing As System.Windows.Forms.CheckBox
    Friend WithEvents FilterMiddle As System.Windows.Forms.CheckBox
    Friend WithEvents FilterIndex As System.Windows.Forms.CheckBox
    Friend WithEvents ISWADataSet As SymbolCache.ISWA2010DataSet
    Friend WithEvents TVFavoriteSymbols As System.Windows.Forms.TreeView
    Friend WithEvents PBHand As System.Windows.Forms.PictureBox
    Friend WithEvents TVHand As System.Windows.Forms.TreeView
    Friend WithEvents SymbolToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents TSMICenter As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMICenterHead As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIRemoveSymbols As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIDuplicateSymbols As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIMoveUp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIMoveDown As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorDialog1 As System.Windows.Forms.ColorDialog
    Friend WithEvents ISWAActionFingersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWAActionFingersTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.actionfingerTableAdapter
    Friend WithEvents ISWABaseSymbolsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWABaseSymbolsTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.basesymbolTableAdapter
    Friend WithEvents ISWAFavoriteSymbolsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWAFavoriteSymbolsTableAdapter As Settings.SettingsDataSetTableAdapters.FavoritesTableAdapter
    Friend WithEvents ISWAMultipleFingersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWAMultipleFingersTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.multiplefingerTableAdapter
    Friend WithEvents ISWARootShapeGroupsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWARootShapeGroupsTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.rootshapegroupTableAdapter
    Friend WithEvents ISWARootShapesQuickBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWARootShapesQuickTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.rootshapequickTableAdapter
    Friend WithEvents ISWAThumbPositionsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ISWAThumbPositionsTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.thumbpositionTableAdapter
    Friend WithEvents LBRootShape As System.Windows.Forms.Label
    Friend WithEvents TSMIUndo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIBackOfHandColor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIPalmOfHandColor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIBackgroundColor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMINextSymbol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIPreviousSymbol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIInsertsymbolOuts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSMINextAddToSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIPreviousAddToSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMILarger As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMISmaller As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SignToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FrameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMINextFrame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIPreviousFrame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIRemoveFrame As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMICopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TSMIDeleteSign As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HandsClassifiedBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents IswaSymbolsTableAdapter As SymbolCache.ISWA2010DataSetTableAdapters.symbolTableAdapter
    Friend WithEvents SettingsDataSet As Settings.SettingsDataSet
    Friend WithEvents PasteCtrlVToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OverlapSymbolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents CMSPBSign As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TSMIRedo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SequenceMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorizeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToBlackToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeperateSymbolsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveArea As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BottomLeft1ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Bottom2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BottomRight3ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MiddleLeft4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Middle5ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MiddleRigh6ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopLeft7ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Top8ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TopRight9ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyImageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents ALotLargerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ALotSmallerCtrlToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveTopMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoveBottomMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MirrorSignToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeArrowHandsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TPSpelling As System.Windows.Forms.TabPage
    Friend WithEvents TPSequence As System.Windows.Forms.GroupBox
    Friend WithEvents PanelSpelling As System.Windows.Forms.Panel
    Friend WithEvents TVSequence As System.Windows.Forms.TreeView
    Friend WithEvents btnSugg2 As System.Windows.Forms.Button
    Friend WithEvents BtnDeleteAll As System.Windows.Forms.Button
    Friend WithEvents btnSugg1 As System.Windows.Forms.Button
    Friend WithEvents BtnAddChooser As System.Windows.Forms.Button
    Friend WithEvents BtnUp As System.Windows.Forms.Button
    Friend WithEvents BtnAddSign As System.Windows.Forms.Button
    Friend WithEvents BtnDown As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents SPSignView As System.Windows.Forms.SplitContainer
    Friend WithEvents GBSign As System.Windows.Forms.GroupBox
    Friend WithEvents FindSymbol As System.Windows.Forms.Button
    Friend WithEvents QuickSignEditorBtn As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Public WithEvents btnAccept As System.Windows.Forms.Button
    Friend WithEvents grid As System.Windows.Forms.CheckBox
    Friend WithEvents PanelSign As System.Windows.Forms.Panel
    Friend WithEvents PBSign As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GBAllSymbols As System.Windows.Forms.GroupBox
    Friend WithEvents TVAllGroups As System.Windows.Forms.TreeView
    Friend WithEvents GBFavorites As System.Windows.Forms.GroupBox
    Friend WithEvents GBChoosers As System.Windows.Forms.GroupBox
    Friend WithEvents btnChooserAdd As System.Windows.Forms.Button
    Friend WithEvents btnChooserReplace As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents PBsymbolOut As System.Windows.Forms.PictureBox
    Friend WithEvents ArrowChooser As SignWriterStudio.SWEditor.ArrowChooser
    Friend WithEvents HandChooser As SignWriterStudio.SWEditor.HandChooser
    Friend WithEvents TVChooser As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents GBHandSearch As System.Windows.Forms.GroupBox

End Class
