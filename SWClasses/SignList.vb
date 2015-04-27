'Option Strict On
'Imports System.Drawing
'Imports System.Drawing.Imaging
'Imports System.IO
'Imports System.Windows.Forms
'Imports System.Windows.Forms.Layout
'Imports System.Security.Permissions
'Imports System.Globalization
'Imports SignWriterStudio.General.All
'Imports SignWriterStudio
'Imports SignWriterStudio.Database
'Imports SignWriterStudio.Database.Dictionary

''Public Class PrintArea
''    Private _areaPrintHeight As Integer
''    Public Property AreaPrintHeight() As Integer
''        Get
''            Return _areaPrintHeight
''        End Get
''        Set(ByVal value As Integer)
''            _areaPrintHeight = value
''        End Set
''    End Property
''    Private _areaPrintWidth As Integer
''    Public Property AreaPrintWidth() As Integer
''        Get
''            Return _areaPrintWidth
''        End Get
''        Set(ByVal value As Integer)
''            _areaPrintWidth = value
''        End Set
''    End Property
''    Private _marginleft As Integer
''    Public Property Marginleft() As Integer
''        Get
''            Return _marginleft
''        End Get
''        Set(ByVal value As Integer)
''            _marginleft = value
''        End Set
''    End Property
''    Private _margintop As Integer
''    Public Property Margintop() As Integer
''        Get
''            Return _margintop
''        End Get
''        Set(ByVal value As Integer)
''            _margintop = value
''        End Set
''    End Property

''End Class
''Public NotInheritable Class PrintPreview

''End Class

'Public Class SignListSubTitle
'    Implements IDisposable

'    ' In this section you can add your own using directives
'    ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:00000000000007AA begin
'    ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:00000000000007AA end
'    ' *
'    '          *   A class that represents ...
'    '          *   All rights Reserved Copyright(c) 2008
'    '          *       @see OtherClasses
'    '          *       @author Jonathan Duncan
'    '          */

'    ' Attributes
'    Private SigntoChange As Nullable(Of Integer) = Nothing
'    Public Property SigntoChange1() As Nullable(Of Integer)
'        Get
'            Return SigntoChange
'        End Get
'        Set(ByVal value As Nullable(Of Integer))
'            SigntoChange = value
'        End Set
'    End Property
'    Private _idSignListSubTitle As Integer
'    Public Property IdSignListSubTitle() As Integer
'        Get
'            Return _idSignListSubTitle
'        End Get
'        Set(ByVal value As Integer)
'            _idSignListSubTitle = value
'        End Set
'    End Property
'    '    Dim LastDataRow As Data.DataRow '= Nothing
'    Private _mySignList As SignList
'    Public Property MySignList() As SignList
'        Get
'            Return _mySignList
'        End Get
'        Set(ByVal value As SignList)
'            _mySignList = value
'        End Set
'    End Property
'    Private TASignListSubTitles As New Database.DictionaryDataSetTableAdapters.SignListSubTitlesTableAdapter
'    Private TASignsinSignListSubTitles As New Database.DictionaryDataSetTableAdapters.SignsinSignListSubTitlesTableAdapter
'    Private TADictionary As New Dictionary.DictionaryDataSetTableAdapters.DictionaryTableAdapter
'    Private TASubTitles As New Database.DictionaryDataSetTableAdapters.SignListSubTitlesTableAdapter

'    ' Operations
'    Public Sub UpdateSettings()
'        Me.MySignList.FirstGlossLanguage = My.Settings.FirstGlossLanguage
'        Me.MySignList.SecondGlossLanguage = My.Settings.SecondGlossLanguage
'        Me.MySignList.DefaultSignLanguage = My.Settings.DefaultSignLanguage
'        Me.MySignList.BilingualMode = My.Settings.BilingualMode
'    End Sub
'    Private Function NewRank(ByVal idSignList As Integer) As Integer
'        Return HighestRank(idSignList) + 1
'    End Function
'    Private Function HighestRank(ByVal idSignList As Integer) As Integer
'        Dim ID As Nullable(Of Short)
'        ID = TASubTitles.MaxSubTitleRank(idSignList)
'        If ID.HasValue Then
'            Return CInt(ID)
'        Else
'            Return 0
'        End If
'    End Function
'    Public Sub AddSubTitle(ByVal subTitleName As String)
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000080E begin
'        If subTitleName Is Nothing Then
'            Throw New ArgumentNullException("subTitleName")
'        End If

'        If Not subTitleName = String.Empty Then
'            If subTitleName.Length <= 50 Then
'                TASignListSubTitles.InsertNewSubTitle(Me.MySignList.SignListId, subTitleName, NewRank(Me.MySignList.SignListId), Now)
'            Else
'                Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
'                MessageBox.Show("The sublist name must be less than 50 characters.", "Sublist name", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
'            End If
'        Else
'            Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
'            MessageBox.Show("The sublist name cannot be blank.", "Sublist name", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
'        End If
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000080E end
'    End Sub
'    Public Sub AddSign(ByVal idDictionary As Integer)
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000080E begin
'        TASignsinSignListSubTitles.InsertQuery(Me.IdSignListSubTitle, idDictionary, Guid.NewGuid, Now)
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000080E end
'    End Sub
'    Public Sub Delete()
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000810 begin
'        Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
'        If MessageBox.Show("Do you want to delete sublist " & Me.SubTitleName() & "'?", String.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MBO, False) = Windows.Forms.DialogResult.Yes Then
'            TASignListSubTitles.DeletebyID(Me.IdSignListSubTitle)
'        End If
'        ReOrderRank()
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000810 end
'    End Sub
'    Public Function SubTitleName() As String
'        Return TASignListSubTitles.GetSubTitleName(Me.IdSignListSubTitle)
'    End Function
'    Public Function MissingSigns() As String
'        Return TASignListSubTitles.GetMissingSigns(Me.IdSignListSubTitle)
'    End Function
'    Public Sub Rename(ByVal newSubTitleName As String)
'        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000891 begin
'        If newSubTitleName Is Nothing Then
'            Throw New ArgumentNullException("newSubTitleName")
'        End If

'        If Not newSubTitleName = String.Empty Then
'            If newSubTitleName.Length <= 50 Then
'                TASignListSubTitles.UpdateNamebyID(newSubTitleName, IdSignListSubTitle)
'            Else
'                Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
'                MessageBox.Show("Sublist name must be less the 50 characters", "Sublist name", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
'            End If
'        Else
'            Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
'            MessageBox.Show("Sublist name must not be blank", "Sublist name", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
'        End If
'        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:0000000000000891 end
'    End Sub

'    Public Function GetSubTitles(ByVal idSignList As Integer) As Database.DictionaryDataSet.SignListSubTitlesDataTable
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD begin
'        Return TASignListSubTitles.GetDataByIDSignList(idSignList)
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD end
'    End Function
'    Public Function GetSignsInSubTitles(ByVal idSignListSubTitle As Integer) As Database.DictionaryDataSet.SignsinSignListSubTitlesDataTable
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD begin
'        If Not idSignListSubTitle = 0 Then
'            Return TASignsinSignListSubTitles.GetDataBySubTitles(idSignListSubTitle)
'        Else
'            Return Nothing
'        End If
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD end
'    End Function
'    Public Function GetSign(ByVal idDictionary As Integer) As Dictionary.DictionaryDataSet.DictionaryDataTable
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD begin

'        Return TADictionary.GetDataByIDAndLanguages(idDictionary, My.Settings.FirstGlossLanguage, My.Settings.SecondGlossLanguage)
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD end
'    End Function
'    Public Function GetSignWithoutGloss(ByVal idDictionary As Integer) As Dictionary.DictionaryDataSet.DictionaryDataTable
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD begin

'        Return TADictionary.GetDataByID(idDictionary)
'        ' section 127-0-0-1--3b209089:11b4f406fa6:-8000:00000000000008DD end
'    End Function
'    Private Sub ReOrderRank()
'        Dim DTSignListSubTitles As New Database.DictionaryDataSet.SignListSubTitlesDataTable
'        Dim I As Integer = 0
'        'Step through signs in order
'        TASignListSubTitles.FillByRank(DTSignListSubTitles, Me.MySignList.SignListId)
'        For Each Row As Database.DictionaryDataSet.SignListSubTitlesRow In DTSignListSubTitles.Rows
'            I = I + 1
'            TASignListSubTitles.UpdateRank(I, Row.IDSignListSubTitle)
'        Next
'    End Sub
'    Private Function GetRank(ByVal idSignListSubTitle As Integer) As Integer
'        Return CInt(TASignListSubTitles.GetRank(idSignListSubTitle))
'    End Function
'    Private Sub ChangeRank(ByVal idSignListSubTitle As Integer, ByVal Rank As Integer)
'        TASignListSubTitles.UpdateRank(Rank, idSignListSubTitle)
'    End Sub
'    Private Function GetIDSignListSubTitle(ByVal rank As Integer) As Integer
'        Return CInt(TASignListSubTitles.GetIDFromRank(Me.MySignList.SignListId, rank))
'    End Function
'    Public Sub MoveUp()
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000827 begin
'        Dim CurrentRank As Integer = GetRank(IdSignListSubTitle)
'        If CurrentRank > 1 Then
'            Dim IDPrecedingSubTitle = GetIDSignListSubTitle(CurrentRank - 1)
'            ChangeRank(IDPrecedingSubTitle, CurrentRank)
'            ChangeRank(IdSignListSubTitle, CurrentRank - 1)
'        End If
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000827 end
'    End Sub
'    Public Sub MoveDown()
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000829 begin
'        Dim CurrentRank As Integer = GetRank(IdSignListSubTitle)
'        If CurrentRank < HighestRank(Me.MySignList.SignListId) Then
'            Dim IDNextSubTitle As Integer = GetIDSignListSubTitle(CurrentRank + 1)
'            If Not IDNextSubTitle = 0 Then
'                ChangeRank(IDNextSubTitle, CurrentRank)
'                ChangeRank(IdSignListSubTitle, CurrentRank + 1)
'            End If
'        End If
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000829 end
'    End Sub
'    Public Sub MarkNewSigns()
'        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000886 begin
'        Dim DTSubTitlesinList As New Database.DictionaryDataSet.SignListSubTitlesDataTable
'        Dim DTSignsinSignList As New Database.DictionaryDataSet.SignsinSignListSubTitlesDataTable

'        'Reset all new signs mark
'        TASignListSubTitles.FillByIDSignList(DTSubTitlesinList, Me.MySignList.SignListId)
'        For Each Row As Database.DictionaryDataSet.SignListSubTitlesRow In DTSubTitlesinList.Rows
'            TASignsinSignListSubTitles.ClearNewInSignListSubTitle(Row.IDSignListSubTitle)
'        Next

'        'Step through signs in order
'        TASignsinSignListSubTitles.FillBySignsOrderedSubTitleRank(DTSignsinSignList, Me.MySignList.SignListId)
'        Dim SignDict As New SortedDictionary(Of Integer, Integer)
'        For Each Row As Database.DictionaryDataSet.SignsinSignListSubTitlesRow In DTSignsinSignList.Rows
'            If Not SignDict.ContainsKey(Row.IDDictionary) Then
'                SignDict.Add(Row.IDDictionary, Row.IDDictionary)
'                'Set new signs
'                TASignsinSignListSubTitles.SetNewSigninSubTitle(Row.IDSignsinSignListSubTitle)
'            End If
'        Next
'        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000886 end
'    End Sub
'    Public Sub MarkNewSigns(ByVal idSignListSubTitle As Integer)
'        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000886 begin
'        Dim DTSubTitlesinList As New Database.DictionaryDataSet.SignListSubTitlesDataTable
'        Dim DTSignsinSignList As New Database.DictionaryDataSet.SignsinSignListSubTitlesDataTable

'        'Reset all new signs mark in SubTitle
'        For Each Row As Database.DictionaryDataSet.SignListSubTitlesRow In DTSubTitlesinList.Rows
'            TASignsinSignListSubTitles.ClearNewInSignListSubTitle(Row.IDSignListSubTitle)
'        Next
'        'Step through signs in order
'        TASignsinSignListSubTitles.FillBySignsOrderedSubTitleRank(DTSignsinSignList, Me.MySignList.SignListId)
'        Dim SignDict As New SortedDictionary(Of Integer, Integer)
'        For Each Row As Database.DictionaryDataSet.SignsinSignListSubTitlesRow In DTSignsinSignList.Rows
'            If Not SignDict.ContainsKey(Row.IDDictionary) Then
'                SignDict.Add(Row.IDDictionary, Row.IDDictionary)
'                'Set new signs
'                If Row.IDSignListSubTitle = idSignListSubTitle Then
'                    TASignsinSignListSubTitles.SetNewSigninSubTitle(Row.IDSignsinSignListSubTitle)
'                End If
'            End If
'        Next
'        ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000886 end
'    End Sub
'    Public Sub SaveMissingSigns(ByVal missingSigns As String)
'        TASignListSubTitles.UpdateMissingSigns(missingSigns, Me.IdSignListSubTitle)
'    End Sub

'    Private disposedValue As Boolean '= False        ' To detect redundant calls

'    ' IDisposable
'    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
'        If Not Me.disposedValue Then
'            If disposing Then
'                ' free unmanaged resources when explicitly called
'                TASignListSubTitles.Dispose()
'                TASignsinSignListSubTitles.Dispose()
'                TADictionary.Dispose()
'                TASubTitles.Dispose()
'                MySignList.Dispose()
'            End If

'            ' free shared unmanaged resources
'        End If
'        Me.disposedValue = True
'    End Sub

'#Region " IDisposable Support "
'    ' This code added by Visual Basic to correctly implement the disposable pattern.
'    Public Sub Dispose() Implements IDisposable.Dispose
'        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
'        Dispose(True)
'        GC.SuppressFinalize(Me)
'    End Sub
'#End Region


'End Class
'Public Class SignList
'    Implements IDisposable

'    ' In this section you can add your own using directives
'    ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000782 begin
'    ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000782 end
'    ' *
'    '          *   A class that represents ...
'    '          *   All rights Reserved Copyright(c) 2008
'    '          *       @see OtherClasses
'    '          *       @author Jonathan Duncan
'    '          */

'    ' Attributes

'    Private _signListID As Integer
'    Public Property SignListId() As Integer
'        Get
'            Return _signListID
'        End Get
'        Set(ByVal value As Integer)
'            _signListID = value
'        End Set
'    End Property
'    Private _defaultSignLanguage As Integer
'    Public Property DefaultSignLanguage() As Integer
'        Get
'            Return _defaultSignLanguage
'        End Get
'        Set(ByVal value As Integer)
'            _defaultSignLanguage = value
'        End Set
'    End Property
'    Private _firstGlossLanguage As Integer
'    Public Property FirstGlossLanguage() As Integer
'        Get
'            Return _firstGlossLanguage
'        End Get
'        Set(ByVal value As Integer)
'            _firstGlossLanguage = value
'        End Set
'    End Property
'    Private _secondGlossLanguage As Integer
'    Public Property SecondGlossLanguage() As Integer
'        Get
'            Return _secondGlossLanguage
'        End Get
'        Set(ByVal value As Integer)
'            _secondGlossLanguage = value
'        End Set
'    End Property
'    Private _bilingualMode As Boolean = True
'    Public Property BilingualMode() As Boolean
'        Get
'            Return _bilingualMode
'        End Get
'        Set(ByVal value As Boolean)
'            _bilingualMode = value
'        End Set
'    End Property
'    Private _printSignList As New PrintSignList(Me)
'    Public Property PrintSignList() As PrintSignList
'        Get
'            Return _printSignList
'        End Get
'        Set(ByVal value As PrintSignList)
'            _printSignList = value
'        End Set
'    End Property
'    Dim TASignList As New Database.DictionaryDataSetTableAdapters.SignListTableAdapter

'    ' Operations

'    Public Sub DeleteSignList(ByVal idSignList As Integer)
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000825 begin
'        TASignList.DeleteSignList(idSignList)
'        ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000825 end
'    End Sub
'    Public Sub DeleteSignList()
'        DeleteSignList(Me.SignListId)
'    End Sub

'    Public Sub RenameSignList(ByVal idSignList As Integer, ByVal newName As String)
'        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088F begin
'        TASignList.UpdateSignListName(newName, idSignList)
'        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088F end
'    End Sub

'    Public Sub RenameSignList(ByVal newName As String)
'        RenameSignList(Me.SignListId, newName)
'    End Sub
'    Public Sub InsertSignList(ByVal newName As String)
'        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088F begin
'        TASignList.InsertSignList(newName)
'        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088F end
'    End Sub
'    Public Function SignListName(ByVal idSignList As Integer) As String
'        Return TASignList.GetSignListName(idSignList)
'    End Function

'    Public Function AllSignLists() As Database.DictionaryDataSet.SignListDataTable
'        Return TASignList.GetData()
'    End Function
'    Public Function SignListName() As String
'        Return SignListName(Me.SignListId)
'    End Function

'    Private disposedValue As Boolean '= False        ' To detect redundant calls

'    ' IDisposable
'    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
'        If Not Me.disposedValue Then
'            If disposing Then
'                ' free unmanaged resources when explicitly called
'                TASignList.Dispose()
'                _printSignList.Dispose()
'            End If

'            ' free shared unmanaged resources
'        End If
'        Me.disposedValue = True
'    End Sub

'#Region " IDisposable Support "
'    ' This code added by Visual Basic to correctly implement the disposable pattern.
'    Public Sub Dispose() Implements IDisposable.Dispose
'        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
'        Dispose(True)
'        GC.SuppressFinalize(Me)
'    End Sub
'#End Region

'End Class
'Public Class PrintSignList
'    Implements IDisposable

'    ' In this section you can add your own using directives
'    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000082E begin
'    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:000000000000082E end
'    ' *
'    '          *   A class that represents ...
'    '          *   All rights Reserved Copyright(c) 2008
'    '          *       @see OtherClasses
'    '          *       @author Jonathan Duncan
'    '          */
'    ' Attributes

'    '		Private PrintSignList_pdSignList As PrintDocument
'    Private psdSignList As PageSetupDialog
'    Private J As Integer '= 0
'    Private I As Integer '= 0
'    Private strMissingSignsFromPrintedOnce As Boolean '= False
'    Private font As New Font("Microsoft Sans Serif", 16)
'    Private PrintAreaSettings As New PrintArea

'    ' Associations
'    Friend SignList As SignList

'    ' Operations

'    'Public Sub PrintPage(ByRef e As System.Drawing.Printing.PrintPageEventArgs, ByVal signListPrintDocument As System.Drawing.Printing.PrintDocument, ByVal idSignList As Integer, ByVal idSignListSubTitle As Integer, ByVal graphicsSize As Integer, ByVal onlyNew As Boolean, ByVal onlyImportant As Boolean, ByVal printAllSubTitles As Boolean, ByVal printMissing As Boolean)


'    Public Sub PrintPage(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal idSignList As Integer, ByVal idSignListSubTitle As Integer, ByVal graphicsSize As Integer, ByVal onlyNew As Boolean, ByVal onlyImportant As Boolean, ByVal printAllSubTitles As Boolean, ByVal printMissing As Boolean)

'        If e Is Nothing Then
'            Throw New ArgumentNullException("e")
'        End If

'        ' Declare a variable to hold the position of the last printed char. Declare
'        ' as static so that subsequent PrintPage events can reference it.
'        Dim SignsOnPage As Integer = 0
'        ' Initialize the font to be used for printing.
'        Dim AreaPrintHeight As Integer
'        Dim AreaPrintWidth As Integer
'        Dim marginLeft As Integer
'        Dim marginTop As Integer

'        'SetPrintArea(SignListPrintDocument.DefaultPageSettings, AreaPrintHeight, AreaPrintWidth, marginLeft, marginTop)
'        AreaPrintHeight = Me.PrintAreaSettings.AreaPrintHeight
'        AreaPrintWidth = Me.PrintAreaSettings.AreaPrintWidth
'        marginLeft = Me.PrintAreaSettings.Marginleft
'        marginTop = Me.PrintAreaSettings.Margintop

'        ' Initialize the rectangle structure that defines the printing area.
'        Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, AreaPrintWidth, AreaPrintHeight)
'        Dim DBSignlist As New Database.DatabaseSignList

'        Dim DTSignList As DictionaryDataSet.SignListDataTable = DBSignlist.GetSignListTablebyIdSignList(idSignList)
'        Dim SignListName As String = CType(DTSignList.Rows(0), DictionaryDataSet.SignListRow).SignListName

'        Dim numSignsinSignList As Integer
'        Dim DTSubTitle As DictionaryDataSet.SignListSubTitlesDataTable
'        Dim SubTitleName As String
'        '        Dim DTSigns As Database.DictionaryDataSet.SignsinSignListSubTitlesDataTable
'        Dim MissingSigns As String
'        Dim DRSigns() As DataRow

'        'Get SubTitles
'        If printAllSubTitles Then
'            'Get all SubTitles

'            DTSubTitle = DBSignlist.SignListSubTitlesTablebyIdSignList(idSignList)
'        Else
'            'Dim ErrorSelectsubtitleFirstPrompt As String = GetTranslation("ErrorSelectsubtitleFirstPrompt", DTPrintTranslations)
'            'Dim ErrorSelectsubtitleFirstTitle As String = GetTranslation("ErrorSelectsubtitleFirstTitle", DTPrintTranslations)
'            'MessageBox.Show(ErrorSelectsubtitleFirstPrompt, ErrorSelectsubtitleFirstTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            Exit Sub

'            'Get Selected SubTitles
'            DTSubTitle = DBSignlist.GetSignListSubTitlesTablebyIdSignListSubTitle(idSignListSubTitle)
'        End If

'        'Load Information for page
'        'TODO set up not to need ByRef (Use several functions)
'        LoadInfoforPage(e, SubTitleName, DTSubTitle, MissingSigns, onlyNew, onlyImportant, graphicsSize, AreaPrintWidth, DRSigns, numSignsinSignList)

'        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(CInt(rectPrintingArea.X), CInt(rectPrintingArea.Y), CInt(rectPrintingArea.Width), CInt(rectPrintingArea.Height)))
'        Dim SignHeight As Integer

'        PrintSign(e, SignListName.ToString, SubTitleName, MissingSigns, printMissing, graphicsSize, rectPrintingArea, SignHeight, SignsOnPage, DRSigns)

'        HasMorePages(e, SignHeight, DRSigns, DTSubTitle, SignsOnPage, onlyNew, onlyImportant)
'    End Sub

'    Private Sub HasMorePages(ByRef e As System.Drawing.Printing.PrintPageEventArgs, ByVal signHeight As Integer, ByVal DRSigns As DataRow(), ByVal dtSubTitle As Database.DictionaryDataSet.SignListSubTitlesDataTable, ByVal signsOnPage As Integer, ByVal onlyNew As Boolean, ByVal onlyImportant As Boolean)
'        Dim numSignsinSignList As Integer
'        ' HasMorePages tells the printing module whether another PrintPage event
'        ' should be fired.
'        If signHeight = 0 Then
'            e.HasMorePages = True
'        ElseIf (I = DRSigns.Length) And (J < dtSubTitle.Rows.Count - 1) Then
'            Do
'                ' Print next Subheading
'                I = 0
'                J = J + 1
'                strMissingSignsFromPrintedOnce = False
'                'Check if next subtitle has signs
'                Dim IDSignListSubTitle = CType(dtSubTitle.Rows(J), Database.DictionaryDataSet.SignListSubTitlesRow).IDSignListSubTitle
'                'Check which signs to print
'                DRSigns = GetSignsTable(CInt(IDSignListSubTitle), onlyNew, onlyImportant).Select("")
'                numSignsinSignList = DRSigns.Length
'                If numSignsinSignList = 0 Then
'                    I = 0
'                    J = 0

'                    e.HasMorePages = False 'Last subtitle doesn´t have any signs
'                Else

'                    e.HasMorePages = True ' Last subtitle does have more signs
'                End If
'            Loop Until e.HasMorePages = True Or J < dtSubTitle.Rows.Count - 1
'        Else
'            I = 0
'            J = 0
'            e.HasMorePages = False
'            strMissingSignsFromPrintedOnce = False
'            ' You must explicitly reset intCurrentChar as it is static.
'            'intCurrentChar = 0
'        End If
'        'Check that at least one sign is printing per page or an infinity of pages with the header will print.
'        If e.HasMorePages = True And signsOnPage = 0 And Not numSignsinSignList = 0 Then
'            'Dim NotPrintImageSoBig As String = GetTranslation("NotPrintImageSoBig", DTPrintTranslations)
'            'MsgBox(NotPrintImageSoBig)
'            e.HasMorePages = False
'        End If
'    End Sub
'    Private Sub PrintSign(ByVal e As System.Drawing.Printing.PrintPageEventArgs, ByVal signListName As String, ByVal subTitleName As String, ByVal missingSigns As String, ByVal printMissing As Boolean, ByVal graphicsSize As Integer, ByVal rectPrintingArea As RectangleF, ByRef signHeight As Integer, ByRef signsOnPage As Integer, ByVal drSigns As DataRow())
'        Dim UsedHeight As Integer
'        UsedHeight = PrintSignList(font, signListName, rectPrintingArea, e)
'        font = New Font("Microsoft Sans Serif", 14)
'        UsedHeight += PrintSignListSubTitle(font, subTitleName, New RectangleF(rectPrintingArea.X, rectPrintingArea.Y + UsedHeight, rectPrintingArea.Width, rectPrintingArea.Height - UsedHeight), e)
'        If printMissing = True And strMissingSignsFromPrintedOnce = False AndAlso missingSigns IsNot Nothing Then
'            UsedHeight += 2 + PrintMissingSigns(font, missingSigns, New RectangleF(rectPrintingArea.X, rectPrintingArea.Y + UsedHeight, rectPrintingArea.Width, rectPrintingArea.Height - UsedHeight), e)
'            strMissingSignsFromPrintedOnce = True
'        End If

'        signHeight = -1 'Not skip to next page if no signs to print
'        Dim DRSign As DataRow

'        For I As Integer = I To drSigns.Length - 1
'            DRSign = drSigns(I)
'            signHeight = PrintSign(DRSign, New Font("Microsoft Sans Serif", 12), graphicsSize, New RectangleF(rectPrintingArea.X, rectPrintingArea.Y + UsedHeight, rectPrintingArea.Width, rectPrintingArea.Height - UsedHeight), e)
'            If signHeight = 0 Then
'                Exit For
'            Else
'                UsedHeight += signHeight
'                signsOnPage += 1
'            End If
'        Next
'    End Sub

'    'Private Sub LoadInfoforPage(ByRef e As System.Drawing.Printing.PrintPageEventArgs, ByRef subTitleName As String, ByVal dtSubTitle As DataTable, ByRef missingSigns As String, ByVal onlyNew As Boolean, ByVal onlyimportant As Boolean, ByVal graphicsSize As Integer, ByVal areaPrintWidth As Integer, ByVal dtSigns As DataTable, ByRef drSigns As DataRow(), ByVal numSignsinSignList As Integer)

'    Private Sub LoadInfoforPage(ByRef e As System.Drawing.Printing.PrintPageEventArgs, ByRef subTitleName As String, ByVal dtSubTitle As Database.DictionaryDataSet.SignListSubTitlesDataTable, ByRef missingSigns As String, ByVal onlyNew As Boolean, ByVal onlyimportant As Boolean, ByVal graphicsSize As Integer, ByVal areaPrintWidth As Integer, ByRef drSigns As DataRow(), ByVal numSignsinSignList As Integer)
'        Do
'            'Get SubTitle Name
'            subTitleName = CType(dtSubTitle.Rows(J), Database.DictionaryDataSet.SignListSubTitlesRow).SubTitleName

'            'Get MissingSigns String
'            Dim strMissingSignsFrom As String = CType(dtSubTitle.Rows(J), Database.DictionaryDataSet.SignListSubTitlesRow).MissingSigns
'            If Not strMissingSignsFrom = String.Empty Then

'                'Dim StrMissingSigns As String = GetTranslation("StrMissingSigns", DTPrintTranslations)
'                'MissingSigns = StrMissingSigns & strMissingSignsFrom
'                missingSigns = strMissingSignsFrom
'            Else
'                missingSigns = ""
'            End If

'            Dim IDSignListSubTitle As Integer = CType(dtSubTitle.Rows(J), Database.DictionaryDataSet.SignListSubTitlesRow).IDSignListSubTitle

'            'Check if fits horizontally
'            If (2 * graphicsSize) * 1.05 >= areaPrintWidth Then 'Two pictures plus a small margin for text.

'                'Dim NotPrintImageSoBig As String = GetTranslation("NotPrintImageSoBig", DTPrintTranslations)
'                'MsgBox(NotPrintImageSoBig)
'                e.HasMorePages = False
'                Exit Sub
'            End If

'            'Check which signs to print
'            drSigns = GetSignsTable(IDSignListSubTitle, onlyNew, onlyimportant).Select("", "gloss1")

'            numSignsinSignList = drSigns.Length
'            If numSignsinSignList = 0 And (J < dtSubTitle.Rows.Count - 1) Then
'                J += 1 'Move on to next subtitle if this does not have signs.
'            End If
'        Loop Until (numSignsinSignList > 0) Or (J = dtSubTitle.Rows.Count - 1)
'    End Sub
'    Private Shared Sub SetPrintArea(ByVal defaultPageSetting As System.Drawing.Printing.PageSettings, ByRef areaPrintHeight As Integer, ByRef areaPrintWidth As Integer, ByRef marginLeft As Integer, ByRef marginTop As Integer)

'        With defaultPageSetting
'            ' Initialize local variables to hold margin values that will serve
'            ' as the X and Y coordinates for the upper left corner of the printing
'            ' area rectangle.
'            marginLeft = .Margins.Left ' X coordinate
'            marginTop = .Margins.Top ' Y coordinate

'            ' Initialize local variables that contain the bounds of the printing
'            ' area rectangle.
'            areaPrintHeight = .PaperSize.Height - marginTop - .Margins.Bottom - 5
'            areaPrintWidth = .PaperSize.Width - marginLeft - .Margins.Right
'        End With

'        ' If the user selected Landscape mode, SWap the printing area height
'        ' and width.
'        If defaultPageSetting.Landscape Then
'            Dim SWap As Integer
'            SWap = areaPrintHeight
'            areaPrintHeight = areaPrintWidth
'            areaPrintWidth = SWap
'        End If
'    End Sub

'    Private Function GetSignsTable(ByVal idSignListSubTitle As Integer, ByVal newOnly As Boolean, ByVal importantOnly As Boolean) As DataTable
'        Dim DTSigns As New Database.DictionaryDataSet.SignsinSignListSubTitlesDataTable
'        Dim DBSignlist As New Database.DatabaseSignList
'        'Check which signs to print
'        DBSignlist.FillSignsinSignListSubTitlesTable(DTSigns, idSignListSubTitle, newOnly, importantOnly, Me.SignList.FirstGlossLanguage, Me.SignList.SecondGlossLanguage, Me.SignList.DefaultSignLanguage)

'        Return DTSigns
'    End Function
'    Private Shared Function PrintSignListSubTitle(ByVal font As Font, ByVal subTitleName As String, ByVal rectPrintAreaLeftF As RectangleF, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer

'        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
'        fmt.Alignment = StringAlignment.Center
'        Dim intLinesFilled, intCharsFitted As Int32
'        Dim subtitleF As SizeF
'        subtitleF = e.Graphics.MeasureString(subTitleName, font, _
'         New SizeF(rectPrintAreaLeftF.Width, rectPrintAreaLeftF.Height), fmt, _
'         intCharsFitted, intLinesFilled)
'        Dim SubTitleNamerectPrintingArea As New RectangleF(rectPrintAreaLeftF.Left, rectPrintAreaLeftF.Top, rectPrintAreaLeftF.Width, subtitleF.Height)

'        ' Print the text to the page.
'        e.Graphics.DrawString(subTitleName, font, _
'         Brushes.Black, SubTitleNamerectPrintingArea, fmt)
'        Return CInt(subtitleF.Height)

'    End Function
'    Private Shared Function PrintMissingSigns(ByVal font As Font, ByVal strMissingSignsFrom As String, ByVal rectPrintAreaLeftF As RectangleF, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer

'        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
'        fmt.Alignment = StringAlignment.Center
'        Dim intLinesFilled, intCharsFitted As Integer
'        Dim subtitleF As SizeF
'        subtitleF = e.Graphics.MeasureString(strMissingSignsFrom, font, _
'         New SizeF(rectPrintAreaLeftF.Width, rectPrintAreaLeftF.Height), fmt, _
'         intCharsFitted, intLinesFilled)
'        Dim strMissingSignsFromrectPrintingArea As New RectangleF(rectPrintAreaLeftF.Left, rectPrintAreaLeftF.Top, rectPrintAreaLeftF.Width, subtitleF.Height)

'        ' Print the text to the page.
'        e.Graphics.DrawString(strMissingSignsFrom, font, _
'         Brushes.Black, strMissingSignsFromrectPrintingArea, fmt)
'        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(CInt(strMissingSignsFromrectPrintingArea.X + (strMissingSignsFromrectPrintingArea.Width - subtitleF.Width - 5) / 2), CInt(strMissingSignsFromrectPrintingArea.Y), CInt(subtitleF.Width + 5), CInt(subtitleF.Height)))

'        Return CInt(subtitleF.Height)

'    End Function
'    Private Function PrintSign(ByVal drSign As DataRow, ByVal font As Font, ByVal graphicsize As Integer, ByVal rectPrintAreaLeftF As RectangleF, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer

'        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
'        fmt.Alignment = StringAlignment.Center
'        Dim intLinesFilled, intCharsFitted As Int32
'        Dim TextF As SizeF
'        'Prepare Gloss
'        Dim Gloss As String
'        If Me.SignList.BilingualMode Then
'        Else
'            Gloss = drSign("gloss1").ToString.ToUpper(CultureInfo.InvariantCulture) & " " & drSign("glosses1").ToString.ToLower(CultureInfo.InvariantCulture)

'            TextF = e.Graphics.MeasureString(Gloss, font, _
'             New SizeF(CSng(rectPrintAreaLeftF.Width - (2 * graphicsize + graphicsize * 0.75)), rectPrintAreaLeftF.Height), fmt, _
'             intCharsFitted, intLinesFilled)
'            Dim rectGlossPrintingArea As New RectangleF(rectPrintAreaLeftF.Left, rectPrintAreaLeftF.Top, CSng(rectPrintAreaLeftF.Width - (2 * graphicsize + graphicsize * 0.75)), TextF.Height)
'            Dim sectionGlossPrintHeight As Integer
'            If rectGlossPrintingArea.Height > graphicsize Then
'                sectionGlossPrintHeight = CInt(rectGlossPrintingArea.Height)
'            Else
'                sectionGlossPrintHeight = graphicsize
'            End If

'            'Print Sign
'            If sectionGlossPrintHeight > rectPrintAreaLeftF.Height Or graphicsize > rectPrintAreaLeftF.Height Then
'                Return 0
'            Else
'                'Draw line between each
'                e.Graphics.DrawLine(Pens.Black, rectPrintAreaLeftF.X, rectPrintAreaLeftF.Y, rectPrintAreaLeftF.X + rectPrintAreaLeftF.Width, rectPrintAreaLeftF.Y)
'                ' Print the text to the page.
'                e.Graphics.DrawString(Gloss, font, _
'                 Brushes.Black, rectGlossPrintingArea, fmt)
'                'Dim Obj1() As Byte

'                'Print photo
'                Dim recPhoto As New Rectangle(CInt(rectPrintAreaLeftF.X) + CInt(rectGlossPrintingArea.Width), CInt(rectPrintAreaLeftF.Y + 3), graphicsize, graphicsize)
'                PrintImage(ByteArraytoImage(CType(drSign("Photo"), Byte())), graphicsize, graphicsize, recPhoto, e)

'                'Print SWSign
'                Dim SWSignImage As Image = ByteArraytoImage(CType(drSign("SWSign"), Byte()))
'                Dim SWSignHeight As Integer
'                If SWSignImage IsNot Nothing AndAlso SWSignImage.Height < graphicsize * 0.75 Then
'                    SWSignHeight = SWSignImage.Height
'                Else
'                    SWSignHeight = CInt(graphicsize * 0.75)
'                End If
'                Dim SWSignWidth As Integer
'                If SWSignImage IsNot Nothing AndAlso SWSignImage.Width < graphicsize * 0.75 Then
'                    SWSignWidth = SWSignImage.Width
'                Else
'                    SWSignWidth = CInt(graphicsize * 0.75)
'                End If
'                Dim recSWSign As New Rectangle(CInt(rectPrintAreaLeftF.X + rectGlossPrintingArea.Width + graphicsize), CInt(rectPrintAreaLeftF.Y + 3), CInt(graphicsize * 0.75), CInt(graphicsize * 0.75))
'                PrintImage(SWSignImage, SWSignHeight, SWSignWidth, recSWSign, e)

'                'Print sign
'                Dim recSign As New Rectangle(CInt(rectPrintAreaLeftF.X + rectGlossPrintingArea.Width + graphicsize + graphicsize * 0.75), CInt(rectPrintAreaLeftF.Y + 3), graphicsize, graphicsize)
'                Dim SignSize As Integer
'                If graphicsize - rectGlossPrintingArea.Height < rectGlossPrintingArea.Width Then
'                    SignSize = CInt(graphicsize - rectGlossPrintingArea.Height)
'                Else
'                    SignSize = CInt(rectGlossPrintingArea.Width)
'                End If
'                If SignSize < 1 Then
'                    SignSize = 1
'                End If
'                PrintImage(ByteArraytoImage(CType(drSign("Sign"), Byte())), SignSize, SignSize, recSign, e)


'                Return sectionGlossPrintHeight
'            End If
'        End If
'    End Function
'    Private Shared Function PrintImage(ByVal image1 As Image, ByVal height As Integer, ByVal width As Integer, ByVal rectPrintAreaF As RectangleF, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
'        If image1 IsNot Nothing Then
'            image1 = SWDrawing.ResizeImage(image1, width, height)
'            Dim destRect As RectangleF = New RectangleF(rectPrintAreaF.X + (rectPrintAreaF.Width - image1.Width) / 2, rectPrintAreaF.Y + (rectPrintAreaF.Height - image1.Height) / 2, image1.Width, image1.Height)
'            e.Graphics.DrawImage(image1, destRect, New RectangleF(0, 0, image1.Width, image1.Height), GraphicsUnit.Pixel)
'        End If
'    End Function
'    Private Shared Function PrintSignList(ByVal font As Font, ByVal signListName As String, ByVal rectPrintAreaLeftF As RectangleF, ByVal e As System.Drawing.Printing.PrintPageEventArgs) As Integer
'        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)
'        fmt.Alignment = StringAlignment.Center
'        Dim ThemeF As SizeF
'        Dim intLinesFilled, intCharsFitted As Int32
'        ThemeF = e.Graphics.MeasureString(signListName, font, _
'         New SizeF(rectPrintAreaLeftF.Width, rectPrintAreaLeftF.Height), fmt, _
'         intCharsFitted, intLinesFilled)
'        Dim SignListNamerectPrintingArea As New RectangleF(rectPrintAreaLeftF.Left, rectPrintAreaLeftF.Top, rectPrintAreaLeftF.Width, ThemeF.Height)

'        ' Print the text to the page.
'        e.Graphics.DrawString(signListName, font, _
'         Brushes.Black, SignListNamerectPrintingArea, fmt)
'        Return CInt(ThemeF.Height)
'    End Function
'    Public Sub SignListPrintDocumentEndPrint()
'        I = 0
'        J = 0
'        strMissingSignsFromPrintedOnce = False
'        PrintAreaSettings = Nothing
'    End Sub
'    Public Sub SignListPrintDocumentBeginPrint(ByVal pageSettings As Printing.PageSettings)
'        If PrintAreaSettings Is Nothing Then
'            PrintAreaSettings = New PrintArea
'        End If
'        SWClasses.PrintSignList.SetPrintArea(pageSettings, PrintAreaSettings.AreaPrintHeight, PrintAreaSettings.AreaPrintWidth, PrintAreaSettings.Marginleft, PrintAreaSettings.Margintop)
'    End Sub
'    'Private Sub SignListPrintDocument_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles SignListPrintDocument.PrintPage
'    '    ' Declare a variable to hold the position of the last printed char. Declare
'    '    ' as static so that subsequent PrintPage events can reference it.
'    '    Dim SignsOnPage As Integer = 0
'    '    ' Initialize the font to be used for printing.
'    '    Dim font As New Font("Microsoft Sans Serif", 16)

'    '    Dim AreaPrintHeight, AreaPrintWidth, marginLeft, marginTop As Int32
'    '    With SignListPrintDocument.DefaultPageSettings
'    '        ' Initialize local variables that contain the bounds of the printing
'    '        ' area rectangle.
'    '        AreaPrintHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
'    '        AreaPrintWidth = .PaperSize.Width - .Margins.Left - .Margins.Right

'    '        ' Initialize local variables to hold margin values that will serve
'    '        ' as the X and Y coordinates for the upper left corner of the printing
'    '        ' area rectangle.
'    '        marginLeft = .Margins.Left ' X coordinate
'    '        marginTop = .Margins.Top ' Y coordinate
'    '    End With

'    '    ' If the user selected Landscape mode, SWap the printing area height
'    '    ' and width.
'    '    If SignListPrintDocument.DefaultPageSettings.Landscape Then
'    '        Dim SWap As Int32
'    '        SWap = AreaPrintHeight
'    '        AreaPrintHeight = AreaPrintWidth
'    '        AreaPrintWidth = SWap
'    '    End If

'    '    ' Calculate the total number of lines in the document based on the height of
'    '    ' the printing area and the height of the font.
'    '    ' Dim intLineCount As Int32 = CInt(AreaPrintHeight / font.Height)
'    '    ' Initialize the rectangle structure that defines the printing area.
'    '    Dim rectPrintingArea As New RectangleF(marginLeft, marginTop, AreaPrintWidth, AreaPrintHeight)

'    '    'Dim DTSignList As DataTable = ThemesTableAdapter.GetDatabyId(Me.LBSignListName.SelectedValue)
'    '    'Dim SignListName = DTSignList.Rows(0).Item("SignListName").ToString

'    '    Dim numSignsinSignList As Integer
'    '    Dim DTSubTitle As DataTable
'    '    Dim SubTitleName As String
'    '    Dim DTSigns As DataTable
'    '    Dim GraphicsSize As Integer = TBImageSize.Text
'    '    Dim MissingSigns As String
'    '    Do


'    '        If CBPrintAllSubTitles.Checked Then
'    '            'DTSubTitle = subtitlesTableAdapter.GetDataByThemeID(Me.LBSignListName.SelectedValue)
'    '        Else
'    '            If IsNothing(Me.LBSubTitles.SelectedValue) Then

'    '                'Dim ErrorSelectsubtitleFirstPrompt As String = GetTranslation("ErrorSelectsubtitleFirstPrompt", DTPrintTranslations)
'    '                'Dim ErrorSelectsubtitleFirstTitle As String = GetTranslation("ErrorSelectsubtitleFirstTitle", DTPrintTranslations)
'    '                'MessageBox.Show(ErrorSelectsubtitleFirstPrompt, ErrorSelectsubtitleFirstTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'    '                Exit Sub
'    '            End If

'    '            'DTSubTitle = subtitlesTableAdapter.GetDataBysubtitleID(Me.LBSubTitleName.SelectedValue)

'    '        End If


'    '        SubTitleName = DTSubTitle.Rows(J).Item("SubTitleName").ToString
'    '        Dim strMissingSignsFrom As String = DTSubTitle.Rows(J).Item("strMissingSignsFrom").ToString
'    '        If Not strMissingSignsFrom = String.Empty Then

'    '            'Dim StrMissingSigns As String = GetTranslation("StrMissingSigns", DTPrintTranslations)
'    '            'MissingSigns = StrMissingSigns & strMissingSignsFrom
'    '        Else
'    '            MissingSigns = ""
'    '        End If

'    '        Dim IDSignListSubTitle = DTSubTitle.Rows(J).Item("IDSignListSubTitle")


'    '        'DTSigns = Me.ASL_ListDataSet.numSignsinSignListDetails.DataSet.Tables("numSignsinSignListDetails")

'    '        'Check if fits horizontally
'    '        If (2 * GraphicsSize) * 1.05 >= AreaPrintWidth Then 'Two pictures plus a small margin for text.

'    '            'Dim NotPrintImageSoBig As String = GetTranslation("NotPrintImageSoBig", DTPrintTranslations)
'    '            'MsgBox(NotPrintImageSoBig)
'    '            e.HasMorePages = False
'    '            Exit Sub
'    '        End If

'    '        ''Check which languages to print
'    '        'If Not (Me.CBPrintEnglish.Checked Or Me.CBPrintSpanish.Checked) Then

'    '        '    'Dim SelectAtLeast1LangMsgPrompt As String = GetTranslation("SelectAtLeast1LangMsgPrompt", DTPrintTranslations)
'    '        '    'Dim SelectAtLeast1LangMsgCaption As String = GetTranslation("SelectAtLeast1LangMsgCaption", DTPrintTranslations)
'    '        '    'MessageBox.Show(SelectAtLeast1LangMsgPrompt, SelectAtLeast1LangMsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
'    '        '    Exit Sub
'    '        'End If

'    '        'Check which signs to print

'    '        DTSigns = GetSignsTable(IDSignListSubTitle, Me.CBNewOnlyNew.Checked, Me.CBNewOnlyImportant.Checked)
'    '        ' Sort and filter the data.
'    '        Dim view As DataView = DTSigns.DefaultView
'    '        view.Sort = "gloss1"
'    '        DTSigns = view.ToTable


'    '        numSignsinSignList = DTSigns.Rows.Count
'    '        If numSignsinSignList = 0 And (J < DTSubTitle.Rows.Count - 1) Then
'    '            J += 1 'Move on to next subtitle if this does not have signs.
'    '        End If
'    '    Loop Until (numSignsinSignList > 0) Or (J = DTSubTitle.Rows.Count - 1)


'    '    e.Graphics.DrawRectangle(Pens.Black, New Rectangle(rectPrintingArea.X, rectPrintingArea.Y, rectPrintingArea.Width, rectPrintingArea.Height))

'    '    Dim UsedHeight As Integer
'    '    'UsedHeight = PrintSignList(font, SignListName, rectPrintingArea, e)
'    '    font = New Font("Microsoft Sans Serif", 14)
'    '    UsedHeight += PrintSignListSubTitle(font, SubTitleName, New RectangleF(rectPrintingArea.X, rectPrintingArea.Y + UsedHeight, rectPrintingArea.Width, rectPrintingArea.Height - UsedHeight), e)
'    '    If CBPrintMissing.Checked = True And strMissingSignsFromPrintedOnce = False And Not IsNothing(MissingSigns) Then
'    '        UsedHeight += 2 + PrintMissingSigns(font, MissingSigns, New RectangleF(rectPrintingArea.X, rectPrintingArea.Y + UsedHeight, rectPrintingArea.Width, rectPrintingArea.Height - UsedHeight), e)
'    '        strMissingSignsFromPrintedOnce = True
'    '    End If

'    '    Dim SignHeight = -1 'Not skip to next page if no signs to print
'    '    Dim DRSign As DataRow

'    '    For I = I To DTSigns.Rows.Count - 1
'    '        DRSign = DTSigns.Rows(I)
'    '        SignHeight = PrintSign(DRSign, New Font("Microsoft Sans Serif", 12), GraphicsSize, New RectangleF(rectPrintingArea.X, rectPrintingArea.Y + UsedHeight, rectPrintingArea.Width, rectPrintingArea.Height - UsedHeight), e)
'    '        If SignHeight = 0 Then
'    '            Exit For
'    '        Else
'    '            UsedHeight += SignHeight
'    '            SignsOnPage += 1
'    '        End If
'    '    Next

'    '    ' Advance the current char to the last char printed on this page. As
'    '    ' intCurrentChar is a static variable, its value can be used for the next
'    '    ' page to be printed. It is advanced by 1 and passed to Mid() to print the
'    '    ' next page (see above in MeasureString()).
'    '    'intCurrentChar += intCharsFitted

'    '    ' HasMorePages tells the printing module whether another PrintPage event
'    '    ' should be fired.
'    '    If SignHeight = 0 Then
'    '        e.HasMorePages = True
'    '    ElseIf (I = DTSigns.Rows.Count) And (J < DTSubTitle.Rows.Count - 1) Then
'    '        Do
'    '            ' Print next Subheading
'    '            I = 0
'    '            J = J + 1
'    '            strMissingSignsFromPrintedOnce = False
'    '            'Check if next subtitle has signs

'    '            Dim IDSignListSubTitle = DTSubTitle.Rows(J).Item("IDSignListSubTitle")

'    '            'Check which signs to print
'    '            DTSigns = GetSignsTable(IDSignListSubTitle, Me.CBNewOnlyNew.Checked, Me.CBNewOnlyImportant.Checked)
'    '            numSignsinSignList = DTSigns.Rows.Count
'    '            If numSignsinSignList = 0 Then
'    '                I = 0
'    '                J = 0

'    '                e.HasMorePages = False 'Last subtitle doesn´t have any signs
'    '            Else

'    '                e.HasMorePages = True ' Last subtitle does have more signs
'    '            End If
'    '        Loop Until e.HasMorePages = True Or J < DTSubTitle.Rows.Count - 1
'    '    Else
'    '        I = 0
'    '        J = 0
'    '        e.HasMorePages = False
'    '        strMissingSignsFromPrintedOnce = False
'    '        ' You must explicitly reset intCurrentChar as it is static.
'    '        'intCurrentChar = 0
'    '    End If
'    '    'Check that at least one sign is printing per page or an infinity of pages with the header will print.
'    '    If e.HasMorePages = True And SignsOnPage = 0 And Not numSignsinSignList = 0 Then

'    '        'Dim NotPrintImageSoBig As String = GetTranslation("NotPrintImageSoBig", DTPrintTranslations)
'    '        'MsgBox(NotPrintImageSoBig)
'    '        e.HasMorePages = False
'    '        Exit Sub
'    '    End If

'    'End Sub

'    Public Sub New(ByVal signListNew As SignList)
'        Me.SignList = signListNew
'    End Sub

'    Private disposedValue As Boolean '= False        ' To detect redundant calls

'    ' IDisposable
'    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
'        If Not Me.disposedValue Then
'            If disposing Then
'                ' free unmanaged resources when explicitly called
'                psdSignList.Dispose()
'                font.Dispose()
'                SignList.Dispose()
'            End If

'            ' free shared unmanaged resources
'        End If
'        Me.disposedValue = True
'    End Sub

'#Region " IDisposable Support "
'    ' This code added by Visual Basic to correctly implement the disposable pattern.
'    Public Sub Dispose() Implements IDisposable.Dispose
'        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.

'        Dispose(True)
'        GC.SuppressFinalize(Me)
'    End Sub
'#End Region

'End Class
