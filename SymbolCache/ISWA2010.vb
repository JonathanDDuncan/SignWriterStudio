Option Strict On
Option Explicit On

Imports System.Globalization
Imports SignWriterStudio.General

'Option Infer On
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
'SignWriterStudio/*-
Imports System
#If NUnitTest Then
Imports NUnit.Framework

#End If

Public Module Iswa2010
	Public Const Basesymbols As Integer = 652
	
	Private _SymbolCache As New SWSymbolCache
	Public Property SC() As SWSymbolCache
		Get
			Return _SymbolCache
		End Get
		Set(ByVal value As SWSymbolCache)
			_SymbolCache = value
		End Set
    End Property
    Private _SymbolImageCache As New Dictionary(Of Integer, Drawing.Image)
    Public Property SymbolImageCache(Code As Integer) As Drawing.Image
        Get
            Dim image As Drawing.Image

            If _SymbolImageCache.TryGetValue(Code, image) Then
                Return image
            End If
        End Get
        Set(ByVal value As Drawing.Image)
            If Not _SymbolImageCache.ContainsKey(Code) Then
                _SymbolImageCache.Add(Code, value)
            End If

        End Set
    End Property
   
    Property ISWA2010ConnectionString As String
        Get
            Return My.Settings.ISWA2010ConnectionString
        End Get
        Set(ByVal value As String)
            My.Settings.ISWA2010ConnectionString = value
            My.Settings.Save()
        End Set
    End Property


End Module

''' <summary>
''' SWSymbolCache loads and maintains the cache of Iswa symbols
''' </summary>
Public Class SWSymbolCache
	Implements IDisposable
	Private _allGroupsList As New System.Windows.Forms.ImageList()
	Public Property AllGroupsList() As System.Windows.Forms.ImageList
		Get
			Return _allGroupsList
		End Get
		Set(ByVal value As System.Windows.Forms.ImageList)
			_allGroupsList = value
		End Set
	End Property
	Private _favoritesList As New System.Windows.Forms.ImageList()
	
	Public Property FavoritesList() As System.Windows.Forms.ImageList
		Get
			Return _favoritesList
		End Get
		Set(ByVal value As System.Windows.Forms.ImageList)
			_favoritesList = value
		End Set
	End Property
    Friend CacheAllSymbols As Boolean '= False
    Private SWSymbolCacheTA As New SignWriterStudio.SymbolCache.ISWA2010DataSetTableAdapters.cacheTableAdapter
	Friend SymbolCacheDT As New ISWA2010DataSet.cacheDataTable
	
	Public Shared Function GetImagebyId(ByVal id As String) As System.Drawing.Image
		'Require Test parameters
		#If AssertTest Then
        If Not CheckId(id) Then
            Throw New AssertionException(id & " is not a valid Id for a symbol.")
        End If
		#End If
        Dim Rows As ISWA2010DataSet.cacheRow()
		Dim Sc As SymbolCache.SWSymbolCache = SymbolCache.SC
		'Return from cache
		Rows = CType(Sc.SymbolCacheDT.Select("Sym_Id='" & ID & "'"), ISWA2010DataSet.cacheRow())
		
		If Rows.Length < 1 Then 'was not in cache
			'Add to Cache
			Sc.FlushCache()
			Sc.LoadIdintoCache(ID)
			'Try getting again from cache
			Rows = CType(Sc.SymbolCacheDT.Select("Sym_Id='" & ID & "'"), ISWA2010DataSet.cacheRow())
		End If
		
		
		'Ensure Test return value
		#If AssertTest Then
        If Rows IsNot Nothing AndAlso Rows.Length >= 0 Then
            Return ByteArraytoImage(Rows(0).sym_png)
        Else
            Throw New AssertionException("Error returning rows from GetImagebyID")
        End If
		#End If
		
		
		
	End Function
	#Region "IswaId"
	
	
	''' <summary>
	''' Get symbol from cache by string Id
	''' </summary>
    Public Function GetId(ByVal swId As String) As SymbolCache.ISWA2010DataSet.cacheRow()
        Dim Rows As SymbolCache.ISWA2010DataSet.cacheRow()
        If Not CacheAllSymbols Then
            'Require Test parameters
#If AssertTest Then
            If Not CheckId(swId) Then
                Throw New AssertionException(swId & " is not a valid Id for a symbol.")
            End If
#End If

            If swId IsNot Nothing AndAlso swId IsNot String.Empty Then
                If Not isIdInCache(swId) Then
                    'Add to Cache
                    FlushCache()
                    LoadIdintoCache(swId)
                End If
            End If
        End If
        'Return from cache
        Rows = CType(SymbolCacheDT.Select("Sym_Id='" & swId & "'"), ISWA2010DataSet.cacheRow())

        'Ensure Test return value
#If AssertTest Then

        If Rows.Length < 0 Then
            Throw New AssertionException("Error returning rows from GetId")
        End If
#End If
        Return Rows
       

    End Function
	
    Public Function GetSymbol(ByVal basegroup As Integer, ByVal fill As Integer, ByVal rotation As Integer) As ISWA2010DataSet.cacheRow()
        'Require Test parameters
        Dim Rows As SymbolCache.ISWA2010DataSet.cacheRow()
        If Not CacheAllSymbols Then
            '#If AssertTest Then
            '            If Not CheckCode(code) Then
            '                Throw New AssertionException(code & " is not a valid symbol code.")
            '            End If
            '#End If

            If Not isBaseGroupinCache(basegroup, fill, rotation) Then
                'Add to Cache
                FlushCache()
                LoadCodeintoCache(basegroup, fill, rotation)
            End If
        End If
        'Return from cache
        Rows = CType(SymbolCacheDT.Select("sym_bs_code=" & basegroup & " AND sym_fill=" & fill & " AND sym_rot=" & rotation), SymbolCache.ISWA2010DataSet.cacheRow())
        'Ensure Test return value
#If AssertTest Then
        If Rows.Length >= 0 Then
            Return Rows
        Else
            Throw New AssertionException("Error returning rows from GetCode")
        End If
#End If

    End Function
	''' <summary>
	''' Verify if symbol is in cache.
	''' </summary>
	Friend Function isIdInCache(ByVal SWId As String) As Boolean
		'Require Test parameters
		#If AssertTest Then
		If Not CheckId(SWId) Then
			Throw New AssertionException(SWId & " is not a valid Id for a symbol.")
		End If
		#End If
		
		Dim SymbolRows As DataRow()
		
		SymbolRows = SymbolCacheDT.Select("Sym_Id='" & SWId & "'", "Sym_Id")
		If (SymbolRows.Length < 1) Then
			Return False
		Else
			Return True
		End If
		
    End Function
    Friend Function isBaseGroupinCache(ByVal basegroup As Integer, fill As Integer, rotation As Integer) As Boolean
        'Require Test parameters
        '#If AssertTest Then
        '        If Not CheckId(SWId) Then
        '            Throw New AssertionException(SWId & " is not a valid Id for a symbol.")
        '        End If
        '#End If

        Dim symbolRows As DataRow()

        symbolRows = SymbolCacheDT.Select("sym_bs_code=" & basegroup & " AND sym_fill=" & fill & " AND sym_rot='" & rotation & "'", "Sym_Id")
        If (symbolRows.Length < 1) Then
            Return False
        Else
            Return True
        End If

    End Function

	
	''' <summary>
	''' Sub LoadIdintoCache description
	''' </summary>
	Friend Sub LoadIdintoCache(ByVal SWId As String)
		'Require Test parameters
		#If AssertTest Then
		If Not CheckId(SWId) Then
			Throw New AssertionException(SWId & " is not a valid Id for a symbol.")
		End If
		#End If
		
		'Load symbol into cache
        SymbolCacheDT.Merge(SWSymbolCacheTA.GetDataByID(SWId), True, MissingSchemaAction.Ignore)
    End Sub
#End Region
#Region "SWCode"

    ''' <summary>
    ''' Function CheckCode description
    ''' </summary>
    Shared Function CheckCode(ByVal code As Integer) As Boolean
        'Require Test parameters
        If code >= 1 AndAlso code <= 62504 Then
            Return True
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' Function GetCode description
    ''' </summary>
    Public Function GetCode(ByVal code As Integer) As SymbolCache.ISWA2010DataSet.cacheRow()
        'Require Test parameters

        Dim Rows As SymbolCache.ISWA2010DataSet.cacheRow()
        If Not CacheAllSymbols Then
#If AssertTest Then
            If Not CheckCode(code) Then
                Throw New AssertionException(code & " is not a valid symbol code.")
            End If
#End If

            If Not isCodeinCache(code) Then
                'Add to Cache
                FlushCache()
                LoadCodeintoCache(code)
            End If
        End If
        'Return from cache
        Rows = CType(SymbolCacheDT.Select("sym_code=" & code), SymbolCache.ISWA2010DataSet.cacheRow())
        'Ensure Test return value
#If AssertTest Then
        If Rows.Length >= 0 Then
            Return Rows
        Else
            Throw New AssertionException("Error returning rows from GetCode")
        End If
#End If
        
    End Function

    ''' <summary>
    ''' Function Get all symbols related to a code
    ''' </summary>
    Public Function GetCodeFull(ByVal code As Integer) As SymbolCache.ISWA2010DataSet.cacheRow()
        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(code) Then
            Throw New AssertionException(code & " is not a valid symbol code.")
        End If
#End If

        Dim Rows As SymbolCache.ISWA2010DataSet.cacheRow()
        Rows = CType(SelectCodeFullCache(code), SymbolCache.ISWA2010DataSet.cacheRow())
        If Not GetFills(code) * GetRotations(code) = Rows.Length Then
            'Add to Cache
            FlushCache()
            LoadCodeFullintoCache(code)
            Rows = CType(SelectCodeFullCache(code), SymbolCache.ISWA2010DataSet.cacheRow())
        End If
        'Return from cache

        'Ensure Test return value
#If AssertTest Then
        If GetFills(code) * GetRotations(code) = Rows.Length Then
            Return Rows
        Else
            Throw New AssertionException("Error returning rows from GetCodeFull")
        End If
#End If



    End Function

    ''' <summary>
    ''' Function SelectCodeFullCache description
    ''' </summary>
    Private Function SelectCodeFullCache(ByVal code As Integer) As DataRow()
        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(code) Then
            Throw New AssertionException(code & " is not a valid symbol code.")
        End If
#End If
        Dim Rows As DataRow()
        Dim Symbol() As SymbolCache.ISWA2010DataSet.cacheRow
        Symbol = GetCode(code)

        If Symbol.Length >= 1 Then
            Dim query As New System.Text.StringBuilder
            query.Append("sg_cat_num=")
            query.Append(Symbol(0).sg_cat_num)
            query.Append(" AND sg_grp_num=")
            query.Append(Symbol(0).sg_grp_num)
            query.Append(" AND bs_bas_num=")
            query.Append(Symbol(0).bs_bas_num)
            query.Append(" AND bs_var_num=")
            query.Append(Symbol(0).bs_var_num)

            Rows = SymbolCacheDT.Select(query.ToString)

            'Ensure Test return value
#If AssertTest Then
            If Rows.Length < 1 Then
                Throw New AssertionException("Could not find rows for code = " & code & ", category =" & Symbol(0).sg_cat_num & ", group =" & Symbol(0).sg_grp_num & ", base =" & Symbol(0).bs_bas_num & ", variation=" & Symbol(0).bs_var_num)
            End If
#End If
            Return Rows
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Function LoadCodeFullintoCache description
    ''' </summary>
    Private Sub LoadCodeFullintoCache(ByRef code As Integer)
        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(code) Then
            Throw New AssertionException(code.ToString(CultureInfo.InvariantCulture) & " is not a valid symbol code.")
        End If
#End If

        Dim Symbol() As SymbolCache.ISWA2010DataSet.cacheRow
        Symbol = GetCode(code)

        If Symbol.Length >= 1 Then
            Dim DTSymbol As New ISWA2010DataSet.cacheDataTable
            SWSymbolCacheTA.FillByCodeFull(DTSymbol, Symbol(0).sg_cat_num, Symbol(0).sg_grp_num, Symbol(0).bs_bas_num, Symbol(0).bs_var_num)
            SymbolCacheDT.Merge(DTSymbol, True, MissingSchemaAction.Ignore)
        End If
    End Sub




    ''' <summary>
    ''' Get the number of fills of symbol.
    ''' </summary>
    Public Function GetFills(ByVal code As Integer) As Integer
        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(code) Then
            Throw New AssertionException(code & " is not a valid symbol code.")
        End If
#End If
        Dim Rows As SymbolCache.ISWA2010DataSet.cacheRow()
        If Not isCodeinCache(code) Then
            'Add to Cache
            FlushCache()
            LoadCodeintoCache(code)
        End If
        'Return from cache
        Rows = CType(SymbolCacheDT.Select("sym_code=" & code), ISWA2010DataSet.cacheRow())
        'Ensure Test return value
#If AssertTest Then
        If Rows.Length >= 0 Then
            Return General.BinaryCount(Rows(0).bs_fills)
        Else
            Throw New AssertionException("Error fills from GetFills")
        End If
#End If

    End Function

    ''' <summary>
    ''' Get the number of rotations of symbol.
    ''' </summary>
    Public Function GetRotations(ByVal code As Integer) As Integer
        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(code) Then
            Throw New AssertionException(code & " is not a valid symbol code.")
        End If
#End If
        Dim Rows As SymbolCache.ISWA2010DataSet.cacheRow()
        If Not isCodeinCache(code) Then
            'Add to Cache
            FlushCache()
            LoadCodeintoCache(code)
        End If
        'Return from cache
        Rows = CType(SymbolCacheDT.Select("sym_code=" & code), ISWA2010DataSet.cacheRow())
        'Ensure Test return value
#If AssertTest Then
        If Rows.Length >= 0 Then
            Return General.BinaryCount(Rows(0).bs_rots)
        Else
            Throw New AssertionException("Error rotations from GetRotations")
        End If
#End If

    End Function
    ''' <summary>
    ''' Is symbol with code in the cache
    ''' </summary>
    Friend Function isCodeinCache(ByVal Code As Integer) As Boolean

        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(Code) Then
            Throw New AssertionException(Code.ToString(CultureInfo.InvariantCulture) & " is not a valid symbol code.")
        End If
#End If

        Dim SymbolRows As DataRow()
        SymbolRows = SymbolCacheDT.Select("sym_code=" & Code.ToString(CultureInfo.InvariantCulture), "sym_code")
        If (SymbolRows.Length < 1) Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Load symbol with Code into Cache
    ''' </summary>
    Friend Sub LoadCodeintoCache(ByVal Code As Integer)
        'Require Test parameters
#If AssertTest Then
        If Not CheckCode(Code) Then
            Throw New AssertionException(Code.ToString(CultureInfo.InvariantCulture) & " is not a valid symbol code.")
        End If
#End If

        Dim DTSymbol As New ISWA2010DataSet.cacheDataTable
        SWSymbolCacheTA.FillByCode(DTSymbol, Code)
        SymbolCacheDT.Merge(DTSymbol, True, MissingSchemaAction.Ignore)
    End Sub

    Friend Sub LoadCodeintoCache(basegroup As Integer, fill As Integer, rotation As Integer)
        'Require Test parameters
        '#If AssertTest Then
        '        If Not CheckCode(Code) Then
        '            Throw New AssertionException(Code.ToString(CultureInfo.InvariantCulture) & " is not a valid symbol code.")
        '        End If
        '#End If

        Dim DTSymbol As New ISWA2010DataSet.cacheDataTable
        SWSymbolCacheTA.FillByBaseGroupFillRotation(DTSymbol, basegroup, fill, rotation)
        SymbolCacheDT.Merge(DTSymbol, True, MissingSchemaAction.Ignore)
    End Sub

#End Region
#Region "CacheAllSymbols"

    ' ''' <summary>
    ' ''' Function CheckCode description
    ' ''' </summary>
    'Shared Function CheckCode(ByVal code As Integer) As Boolean
    '    'Require Test parameters
    '    If code >= 1 AndAlso code <= 62504 Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    Public Sub LoadAllSymbols()
        CacheAllSymbols = True

        SWSymbolCacheTA.FillByLoadAllSymbols(SymbolCacheDT)

    End Sub


#End Region

#Region "Base"
    'Function
    ''' <summary>
    ''' Function GetBase description
    ''' </summary>
    Public Function GetBase() As DataRow()
        Dim DTSymbol As New ISWA2010DataSet.cacheDataTable
        SWSymbolCacheTA.FillBaseSymbols(DTSymbol)
        If Not CacheAllSymbols Then
            FlushCache()
            SymbolCacheDT.Merge(DTSymbol, True, MissingSchemaAction.Ignore)
        End If



#If AssertTest Then
        If (Not DTSymbol.Rows.Count = Basesymbols) OrElse (Not SymbolCacheDT.Rows.Count >= Basesymbols) Then
            Throw New AssertionException("Not all base symbols loaded. Basesymbols from database " & DTSymbol.Rows.Count.ToString(CultureInfo.InvariantCulture) & ".  Symbols in cache " & SymbolCacheDT.Rows.Count.ToString(CultureInfo.InvariantCulture))
        End If
#End If

        Return DTSymbol.Select(String.Empty, "sym_Id")
    End Function



#End Region
	#Region "Manage Cache"
	''' <summary>
	''' Flush Cache when a certain number of symbols are in the cache.
	''' </summary>
	Friend Sub FlushCache()
        'TODO: Make Default value settable in options
        If Not CacheAllSymbols Then
            FlushCache(6000)
        End If
    End Sub
	Friend Sub FlushCache(ByVal LeveltoFlush As Integer)
		'Require Test parameters
		#If AssertTest Then
		If Not LeveltoFlush > 0 Then
			Throw New AssertionException("FlushCache LeveltoFlush must be greater than 0")
		End If
		#End If
		
        If SymbolCacheDT.Count > LeveltoFlush AndAlso Not Me.CacheAllSymbols Then
            SymbolCacheDT.Clear()
            SymbolCacheDT.AcceptChanges()
        End If
		
		'Ensure Test final changes to object
		#If AssertTest Then
		If SymbolCacheDT.Rows.Count > LeveltoFlush Then
			Throw New AssertionException("FlushCache Error, symbols not flushed.")
		End If
		#End If
    End Sub

   


#End Region
	
	Private disposedValue As Boolean '= False        ' To detect redundant calls
	
	' Idisposable
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.disposedValue Then
			If disposing Then
				'free other state (managed objects).
				SymbolCacheDT.Dispose()
				SWSymbolCacheTA.Dispose()
				Me._allGroupsList.Dispose()
				Me._favoritesList.Dispose()
			End If
			
			'free your own state (unmanaged objects).
			'set large fields to null.
		End If
		Me.disposedValue = True
	End Sub
    Public Shared Function GetArrowChoosingInfo(bs_sym_code As Integer) As ISWA2010DataSet.basesymbolDataTable
        Dim basesymbolTA As New ISWA2010DataSetTableAdapters.basesymbolTableAdapter
        Dim DT = basesymbolTA.GetDataArrowChooser(bs_sym_code)

        Return DT

    End Function

	#Region " Idisposable Support "
	' This code added by Visual Basic to correctly implement the disposable pattern.
	Public Sub Dispose() Implements IDisposable.Dispose
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub
	#End Region


  
End Class

