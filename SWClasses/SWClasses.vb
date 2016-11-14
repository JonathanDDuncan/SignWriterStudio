
Option Strict On
Imports System.Drawing
Imports System.Windows.Forms

Imports SignWriterStudio.General.All
Imports SignWriterStudio.SWS
Imports System
Imports SignWriterStudio.General.SerializeObjects

Public Module SWClassesMod


    'Friend WithEvents monitor As EQATEC.Analytics.Monitor.IAnalyticsMonitor = EQATEC.Analytics.Monitor.AnalyticsMonitorFactory.Create("7A55FE8188FD4072B11C3EA5D30EB7F9")
    'Private Sub NewVersion(ByVal sender As Object, ByVal e As EQATEC.Analytics.Monitor.VersionAvailableEventArgs) Handles monitor.VersionAvailable
    '    Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
    '    MessageBox.Show("Version " & e.OfficialVersion.ToString() & " is available", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)

    'End Sub
    'Friend isSettingup As Boolean '= False



    Function ViewSymbolDetails(ByVal symb As SWSymbol) As String
        If symb Is Nothing Then
            Throw New ArgumentNullException("symb")
        End If

        Dim string1 As String = "isValid: " & symb.IsValid

        string1 &= vbCrLf() & "Code: " & symb.Code
        string1 &= vbCrLf() & "sssID: " & symb.Id
        string1 &= vbCrLf() & "SSS.ToString: " & symb.ToString
        string1 &= vbCrLf() & "SSS.baseGroup: " & symb.BaseGroup
        string1 &= vbCrLf() & "SSS.group: " & symb.Group
        string1 &= vbCrLf() & "SSS.category: " & symb.Category
        string1 &= vbCrLf() & "SSS.symbol: " & symb.Symbol
        string1 &= vbCrLf() & "SSS.variation: " & symb.Variation
        string1 &= vbCrLf() & "SSS.fill: " & symb.Fill
        string1 &= vbCrLf() & "SSS.rotation: " & symb.Rotation

        Return string1
    End Function
    'Public Sub PrintValues(ByVal myCollection As IEnumerable, ByVal mySeparator As Char)
    '    Dim obj As [Object]
    '    Dim OBJ2 As Dictionary(Of Integer, SWSign) = CType(myCollection, Dictionary(Of Integer, SWSign))
    '    For Each obj In myCollection
    '        Dim obj1 As SWSign = CType(obj.value, SWSign)
    '        Console.Write("{0}{1}", mySeparator, obj1.GetHashCode)
    '    Next obj
    '    'Console.WriteLine()
    '    'Console.Write("{0}{1}", mySeparator, OBJ2.Count)
    '    'Console.WriteLine()
    'End Sub 'PrintValues
End Module

Public Class AcercaDe
    Private _autorizado As String
    Public Property Autorizado() As String
        Get
            Return _autorizado
        End Get
        Set(ByVal value As String)
            _autorizado = value
        End Set
    End Property
    Private _version As String
    Public Property Version() As String
        Get
            Return _version
        End Get
        Set(ByVal value As String)
            _version = value
        End Set
    End Property
    Private _prueba As String
    Public Property Prueba() As String
        Get
            Return _prueba
        End Get
        Set(ByVal value As String)
            _prueba = value
        End Set
    End Property
    Private _activado As String
    Public Property Activado() As String
        Get
            Return _activado
        End Get
        Set(ByVal value As String)
            _activado = value
        End Set
    End Property
    Private _level As String

    Public Property Level() As String
        Get
            Return _level
        End Get
        Set(ByVal value As String)
            _level = value
        End Set
    End Property
    Public Property IsBeta As Boolean

    Public Sub New()
        Version = My.Application.Info.Version.ToString
        Activado = "No"
    End Sub
End Class


Public Class ImageInfo
    Private _key As String
    Public Property Key() As String
        Get
            Return _key
        End Get
        Set(ByVal value As String)
            _key = value
        End Set
    End Property

    Private _image As Byte()
    Public Property Image() As Byte()
        Get
            Return _image
        End Get
        Set(ByVal value As Byte())
            _image = value
        End Set
    End Property
End Class
''' <summary>
''' Add symbols to the treeview
''' </summary>
Public NotInheritable Class SymbolsToTreeView
    Private Sub New()

    End Sub
    ' In this section you can add your own using directives
    ' section 127-0-0-1-7a280c1:11b57c58272:-8000:0000000000000B1F begin
    ' section 127-0-0-1-7a280c1:11b57c58272:-8000:0000000000000B1F end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see SWFrame
    '          *       @author Jonathan Duncan
    '          */

    ' Operations


    Private Shared Sub SaveImageList(ByVal imgList As ImageList, ByVal filename As String)

        'Dim ImgStream As ImageListStreamer = ImgList.ImageStream
        Dim listtoSave As New List(Of ImageInfo)
        For I As Integer = 0 To imgList.Images.Count - 1
            Dim img As Image = imgList.Images(I)
            Dim imgInfo As New ImageInfo
            imgInfo.Image = ImageToByteArray(img, Imaging.ImageFormat.Png)

            imgInfo.Key = imgList.Images.Keys(I)
            listtoSave.Add(imgInfo)
        Next
        Dim imgListXml As Xml.XmlDocument

        imgListXml = SerializeObject(listtoSave, listtoSave.GetType)
        'IO.File.Delete(filename)
        imgListXml.Save(filename)

    End Sub
    Private Shared Function LoadImageList(ByVal imgList As ImageList, ByVal filename As String) As Integer

        Dim listtoLoad As New List(Of ImageInfo)
        Dim imgListXml As New Xml.XmlDocument
        imgListXml.Load(filename)


        listtoLoad = CType(DESerializeObject(imgListXml, listtoLoad.GetType), List(Of ImageInfo))
        For Each imgInfo As ImageInfo In listtoLoad
            imgList.Images.Add(imgInfo.Key, ByteArraytoImage(imgInfo.Image))

        Next
        Return listtoLoad.Count
    End Function
    Public Shared Sub Load(ByVal tv As Windows.Forms.Control, ByVal imgList As ImageList, ByVal myDataRow() As SymbolCache.ISWA2010DataSet.cacheRow, ByVal useBaseName As Boolean)
        ' section 127-0-0-1-7a280c1:11b57c58272:-8000:0000000000000B2A begin
        If tv Is Nothing Then
            Throw New ArgumentNullException("tv")
        End If
        If imgList Is Nothing Then
            Throw New ArgumentNullException("imgList")
        End If
        If myDataRow Is Nothing Then
            Throw New ArgumentNullException("myDataRow")
        End If
        Dim filename As String = IO.Path.Combine(General.Paths.AllUsersData, tv.Name & ".xml")
        If My.Computer.FileSystem.FileExists(filename) Then
            Dim imageCount As Integer = LoadImageListFromFile(filename, imgList)

            If Not imageCount = 652 Then 'Damaged file, load from database
                imgList.Images.Clear()
                LoadImageListFromDatabase(filename, imgList, myDataRow)
            End If
        Else
            LoadImageListFromDatabase(filename, imgList, myDataRow)
        End If

        LoadNodes(imgList, CType(tv, Windows.Forms.TreeView), myDataRow, useBaseName)
        ' section 127-0-0-1-7a280c1:11b57c58272:-8000:0000000000000B2A end
    End Sub
    Private Shared Sub LoadNodes(ByRef imgList As ImageList, ByRef tv As TreeView, ByRef myDataRow() As SymbolCache.ISWA2010DataSet.cacheRow, ByVal useBaseName As Boolean)
        tv.ImageList = imgList
        tv.BeginUpdate()
        'Create Categories
        If tv.Nodes.Count = 0 Then
            tv.Tag = Nothing
        End If
        TVCategories_Load(tv)
        'Load GroupBase and Base Symbols

        TreeViewCategory_Load(tv, myDataRow, UseBaseName)

        tv.CollapseAll()
        tv.EndUpdate()
    End Sub
    Private Shared Function LoadImageListFromFile(ByVal filename As String, ByVal imgList As ImageList) As Integer
        If imgList.Images.Count = 0 Then
            imgList.ColorDepth = ColorDepth.Depth8Bit
            imgList.TransparentColor = Nothing
            imgList.ImageSize = New Size(50, 55)
        End If
        Return LoadImageList(imgList, filename)

    End Function
    Private Shared Sub LoadImageListFromDatabase(ByVal filename As String, ByVal imgList As ImageList, ByRef myDataRow() As SymbolCache.ISWA2010DataSet.cacheRow)
        'Try
        imgList = ImageList_Load(imgList, myDataRow, False)
        'If tv.Name = "TVFavoriteSymbols" Then
        '      imgList = SWDrawing.AddImagestoImageList(imgList, "01-01-001-01-01-01", 55, 50, Color.OrangeRed)
        'imgList = SWDrawing.AddImagestoImageList(imgList, "02-01-001-01-01-01", 55, 50, Color.OrangeRed)
        'imgList = SWDrawing.AddImagestoImageList(imgList, "03-01-001-01-01-01", 55, 50, Color.OrangeRed)
        'imgList = SWDrawing.AddImagestoImageList(imgList, "04-01-001-01-01-01", 55, 50, Color.OrangeRed)
        'imgList = SWDrawing.AddImagestoImageList(imgList, "05-01-001-01-01-01", 55, 50, Color.OrangeRed)
        'imgList = SWDrawing.AddImagestoImageList(imgList, "06-01-001-01-01-01", 55, 50, Color.OrangeRed)
        'imgList = SWDrawing.AddImagestoImageList(imgList, "07-01-001-01-01-01", 55, 50, Color.OrangeRed)
        ''End If

        SaveImageList(imgList, filename)
        'Catch ex As Exception
        '             MessageBox.Show(ex.Message, )
        '         End Try
    End Sub


    Private Shared Sub TVCategories_Load(ByRef tv As TreeView)

        If tv.Tag Is Nothing Then
            Dim newCatNode As New TreeNode
            newCatNode.Name = "Category1"
            newCatNode.ToolTipText = "Hands [1]"
            newCatNode.ImageKey = "01-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey ' "S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))

            newCatNode.Name = "Category2"
            newCatNode.ToolTipText = "Movement [2]"
            newCatNode.ImageKey = "02-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey ' "S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))

            newCatNode.Name = "Category3"
            newCatNode.ToolTipText = "Dynamics [3]"
            newCatNode.ImageKey = "03-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey ' "S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))

            newCatNode.Name = "Category4"
            newCatNode.ToolTipText = "Face Head [4]"
            newCatNode.ImageKey = "04-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey '"S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))

            newCatNode.Name = "Category5"
            newCatNode.ToolTipText = "Body [5]"
            newCatNode.ImageKey = "05-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey ' "S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))

            newCatNode.Name = "Category6"
            newCatNode.ToolTipText = "Location [6]"
            newCatNode.ImageKey = "06-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey ' "S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))

            newCatNode.Name = "Category7"
            newCatNode.ToolTipText = "Punctuation [7]"
            newCatNode.ImageKey = "07-01-001-01-01-01"
            newCatNode.SelectedImageKey = newCatNode.ImageKey '"S" & newCatNode.ImageKey
            tv.Nodes.Add(CType(newCatNode.Clone, TreeNode))
            tv.Tag = "Categories Set"

        End If
    End Sub
    '		Private Sub TreeViewCategory_Load(ByRef TV As TreeView, ByRef MyDataRows() As DataRow)
    '			TreeViewCategory_Load(TV, MyDataRows, True)
    '		End Sub
    Private Shared Sub TreeViewCategory_Load(ByRef tv As TreeView, ByRef myDataRows() As SymbolCache.ISWA2010DataSet.cacheRow, ByVal useBaseName As Boolean)
        Dim previousCatNodeName As String
        'Dim PreviousGroupNodeName As String = String.Empty
        'Dim PreviousBaseNodeName As String = String.Empty
        Dim currentCategory As Integer = 0
        'Dim PreviousBaseGroup As Integer = Nothing
        'Dim PreviousGroup As Integer = Nothing
        'Dim I As Integer
        Dim catRows As SymbolCache.ISWA2010DataSet.cacheRow() = myDataRows
        Dim catNode As TreeNode
        Dim groupBaseNode As New TreeNode
        Dim baseNode As New TreeNode
        Dim catRow As SymbolCache.ISWA2010DataSet.cacheRow
        Dim memGroupBase As Integer = 0
        Dim memBase As Integer = 0
        Dim newGroupBase As Boolean = True
        Dim newBase As Boolean = True

        tv.BeginUpdate()


        groupBaseNode = New TreeNode
        baseNode = New TreeNode


        Dim newNode As New TreeNode
        For Each catRow In catRows
            'Set up category
            If Not IsDBNull(catRow.sg_cat_num) AndAlso (currentCategory = 0 OrElse Not catRow.sg_cat_num = currentCategory) Then
                previousCatNodeName = "Category" & catRow.sg_cat_num
                currentCategory = catRow.sg_cat_num

                catNode = tv.Nodes(previousCatNodeName)
                'Start new Category reset these two.
                memGroupBase = 0
                memBase = 0
            End If

            'Create new node, set info
            newNode = New TreeNode

            If useBaseName Then
                If Not IsDBNull(catRow.bs_name) Then
                    newNode.Text = catRow.bs_name
                End If
            End If


            newNode.ImageKey = catRow.sym_id
            newNode.SelectedImageKey = catRow.sym_id '"S" & CatRow.sym_id
            newNode.Name = catRow.sym_id

            'Set Tooltiptext
            If IsDBNull(catRow.bs_name) Then
                newNode.ToolTipText = String.Empty
            Else
                newNode.ToolTipText = catRow.bs_name
            End If

            'If is new GroupBase
            If Not memGroupBase = catRow.sg_grp_num Then
                newGroupBase = True
            End If
            'If is new Base
            If Not memBase = catRow.bs_bas_num Then
                newBase = True
            End If

            'Add Base Group Name Node
            If newGroupBase Then
                'GroupBaseNode = CType(newNode.Clone, TreeNode)
                groupBaseNode = newNode
                catNode.Nodes.Add(groupBaseNode)

                'Add details of BaseItem
            ElseIf newBase = True Then
                If groupBaseNode.Name = String.Empty Then
                    'Add to CatNode (lowest level)
                    'BaseNode = CatNode
                    'Add BaseNode to GroupBaseNode
                    'BaseNode = CType(newNode.Clone, TreeNode)
                    baseNode = newNode
                    catNode.Nodes.Add(baseNode)
                    'SetGroupBase for next pass
                    groupBaseNode = baseNode
                Else
                    'Add BaseNode to GroupBaseNode
                    'BaseNode = CType(newNode.Clone, TreeNode)
                    baseNode = newNode
                    groupBaseNode.Nodes.Add(baseNode)

                End If

            ElseIf Not groupBaseNode.Name = String.Empty Then 'If TV.Nodes.Find(newNode.Name, True).Length = 0 Then
                groupBaseNode.Nodes.Add(newNode)
            Else
                catNode.Nodes.Add(newNode)
            End If

            'Resert if New
            newGroupBase = False
            newBase = False
            'Remember Previous Group and Base
            memGroupBase = catRow.sg_grp_num
            memBase = catRow.bs_bas_num
        Next

        tv.EndUpdate()

    End Sub
    Private Shared Function ImageList_Load(ByVal myImageList As ImageList, ByVal myDataRows() As SymbolCache.ISWA2010DataSet.cacheRow, Optional createdSelected As Boolean = True) As ImageList
        Return SWDrawing.LoadImageList(myImageList, myDataRows, 55, 50, Color.OrangeRed, CreatedSelected)
    End Function
End Class


Class StopWatch
    Private _swLap As New TimeSpan
    Private _swTotal As New TimeSpan
    Private _startTime As New DateTime
    Private _laps As Integer = 0
    Public ReadOnly Property LastLap() As TimeSpan
        Get
            Return _swLap
        End Get
    End Property
    Public ReadOnly Property TotalTime() As TimeSpan
        Get
            Return _swTotal
        End Get
    End Property
    Public ReadOnly Property TotalLaps() As Integer
        Get
            Return _laps
        End Get
    End Property
    Public Sub SWStop()
        _swLap = Date.Now - _startTime
        _swTotal = _swTotal.Add(_swLap)
    End Sub
    Public Sub SWStart()
        _startTime = Date.Now
        _laps += 1
    End Sub
    Public Sub Clear()
        _startTime = Nothing
        _swLap = New TimeSpan(0)
        _swTotal = New TimeSpan(0)
        _laps = 0
    End Sub
    Public Sub Stats()
        MessageBox.Show("Last Lap was: " & _swLap.TotalMilliseconds & vbCrLf() & "Total Time is: " & _swTotal.TotalMilliseconds & vbCrLf() & "Average lap is: " & _swTotal.TotalMilliseconds / _laps)
    End Sub
End Class
#Region "Unused"



Public NotInheritable Class SWQuiz
    'Inherits SignListSubTitle
    ' In this section you can add your own using directives
    ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B37 begin
    ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B37 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */

    ' Attributes

    ' Associations

    ' Operations
    'Public Sub getSignsforQuiz()
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B39 begin
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B39 end
    'End Sub

    'Public Sub saveQuirzResults()
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B3B begin
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B3B end
    'End Sub
    'Public Sub showQuirzResults()
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B3D begin
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B3D end
    'End Sub
    'Public Sub resetQuizResults()
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B3F begin
    '	' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B3F end
    'End Sub

End Class

Public NotInheritable Class SWSecurity
    ' In this section you can add your own using directives
    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B36 begin
    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B36 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */

    ' Attributes

    Private _cpuId As Integer
    Public Property CpuId() As Integer
        Get
            Return _cpuId
        End Get
        Set(ByVal value As Integer)
            _cpuId = value
        End Set
    End Property
    Private _harddriveId As Integer
    Public Property HarddriveId() As Integer
        Get
            Return _harddriveId
        End Get
        Set(ByVal value As Integer)
            _harddriveId = value
        End Set
    End Property
    Private _operatingsystemId As Integer
    Public Property OperatingsystemId() As Integer
        Get
            Return _operatingsystemId
        End Get
        Set(ByVal value As Integer)
            _operatingsystemId = value
        End Set
    End Property
    Private _seed As Integer
    Public Property Seed() As Integer
        Get
            Return _seed
        End Get
        Set(ByVal value As Integer)
            _seed = value
        End Set
    End Property
    Private _licencenumber As Integer
    Public Property Licencenumber() As Integer
        Get
            Return _licencenumber
        End Get
        Set(ByVal value As Integer)
            _licencenumber = value
        End Set
    End Property
    Private _instalationId As Integer
    Public Property InstalationId() As Integer
        Get
            Return _instalationId
        End Get
        Set(ByVal value As Integer)
            _instalationId = value
        End Set
    End Property

    ' Operations
    'Public Sub GetComputerInfo()
    '    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B38 begin
    '    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B38 end
    'End Sub
    'Public Sub GetLicenceInfo()
    '    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B3A begin
    '    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B3A end
    'End Sub
    'Public Sub UserLevel()
    '    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B3C begin
    '    ' section 127-0-0-1-10d587d1:11b793ea0a9:-8000:0000000000000B3C end
    'End Sub
End Class
Public NotInheritable Class SWUserLevel
    ' In this section you can add your own using directives
    ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B19 begin
    ' section 127-0-0-1-5dfe14c5:1088497b690:-8000:0000000000000B19 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */
    ' Attributes

    Private _swUserHash As String
    Public Property SWUserHash() As String
        Get
            Return _SWUserHash
        End Get
        Set(ByVal value As String)
            _SWUserHash = value
        End Set
    End Property
    Private _swLevel As String
    Public Property SWLevel() As String
        Get
            Return _SWLevel
        End Get
        Set(ByVal value As String)
            _SWLevel = value
        End Set
    End Property
    ' Associations

    ' Operations

End Class
#End Region
 

Friend NotInheritable Class FunctorComparer(Of T)
    Implements IComparer(Of T)

    ' Methods
    Public Sub New(ByVal comparison As Comparison(Of T))
        comparison = comparison
    End Sub

    Public Function [Compare](ByVal x As T, ByVal y As T) As Integer Implements IComparer(Of T).Compare

        Return _comparison.Invoke(x, y)
    End Function


    ' Fields
    Private ReadOnly _comparison As Comparison(Of T)

End Class
Module SWSymbolCache
    Private ReadOnly _swSymbolCache As New Dictionary(Of Integer, SWSymbol)
    Public Property SWSymbolCache(code As Integer) As SWSymbol
        Get
            Dim swSymbol As SWSymbol = Nothing

            If _swSymbolCache.TryGetValue(Code, swSymbol) Then
                Return swSymbol
            End If
            Return Nothing
        End Get
        Set(ByVal value As SWSymbol)
            If Not _swSymbolCache.ContainsKey(Code) Then
                _swSymbolCache.Add(Code, value)
            End If

        End Set
    End Property
End Module