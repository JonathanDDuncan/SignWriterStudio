Option Strict On
Imports NUnit.Framework
Imports SignWriterStudio.General
Imports SignWriterStudio.Classes
Imports SignWriterStudio.General.SerializeObjects
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
'Module Settings1
'    Public Settings2 As New SwsSettings
'End Module
Public NotInheritable Class SwsSettings
    ''' <summary>
    ''' Sub UpdateSettings description
    ''' </summary>
    Public Shared Function UpdateSettings(ByVal settingsStr As String) As Integer
        'Require Test parameters
#If AssertTest Then
        If settingsStr IsNot Nothing OrElse settingsStr = String.Empty Then
            Throw New AssertionException("Update settings give it proper settings to update.")
        End If
#End If

        Dim TA As New SettingsDataSetTableAdapters.SettingsTableAdapter
        Dim Result As Integer
        Result = TA.Update(1, settingsStr, 1, Settings.SwsSettings.SwsSettings)

        'Ensure Test final changes to object
#If AssertTest Then
        If Not Result = 1 Then
            Throw New AssertionException("Update to settings failed.")
        End If
#End If
        Return Result
    End Function

    Public Shared ReadOnly Property SwsSettings() As String
        Get
            Dim TA As New SettingsDataSetTableAdapters.FavoritesTableAdapter
            Dim Row As SettingsDataSet.SettingsRow
            Row = CType(TA.GetData.Rows(1), Settings.SettingsDataSet.SettingsRow)
            Return Row.Settings

        End Get
    End Property

End Class

Public NotInheritable Class Favorites

    ''' <summary>
    ''' Sub UpdateFavorite description
    ''' </summary>
    Public Shared Function InsertFavorite(ByVal favoriteName As String, ByVal favsDatatable As Settings.SettingsDataSet.FavSymbolsDataTable, ByVal img As System.Drawing.Image) As Integer
        'Require Test parameters
#If AssertTest Then
        If favsDatatable Is Nothing Then
            Throw New AssertionException("No favorites to save.")
        End If
        If favoriteName Is Nothing Then
            Throw New AssertionException("Supply a name for favorite to save.")
        End If
#End If

        Dim FavTA As New SettingsDataSetTableAdapters.FavoritesTableAdapter
        Dim FavDT As New SettingsDataSet.FavoritesDataTable
        Dim FavRow As SettingsDataSet.FavoritesRow
        Dim SymbTA As New SettingsDataSetTableAdapters.FavSymbolsTableAdapter
        Dim FavID As Integer ' = GetId(favoriteName)
        Dim ResultFav As Integer
        Dim ResultSymb As Integer
        FavRow = FavDT.NewFavoritesRow
        FavRow.FavoriteName = favoriteName
        Dim SignBounds As New SignBounds
        Dim Sym As New SWS.SWSymbol
        For Each symbol As SettingsDataSet.FavSymbolsRow In favsDatatable.Rows
            Sym.Code = symbol.sym_code
            SignBounds.Update(symbol.x, symbol.y, Sym.Width, Sym.Height)
        Next

        ResultFav = FavTA.Insert(favoriteName, SignBounds.Bottom, SignBounds.Left, SignBounds.Height, SignBounds.Width, ImageToByteArray(img, System.Drawing.Imaging.ImageFormat.Png))

#If AssertTest Then
        If ResultFav = 0 Then
            Throw New AssertionException("Favorites where not saved.")
        End If
#End If
        'Add id to symbols
        FavID = Settings.Favorites.GetId(favoriteName)
        For Each symbol As SettingsDataSet.FavSymbolsRow In favsDatatable.Rows
            symbol.IDFavorites = FavID
            ResultSymb += SymbTA.InsertNew(FavID, symbol.sym_code, symbol.x, symbol.y, symbol.z, symbol.hand, symbol.handcolor, symbol.palmcolor, CDec(symbol.size))
            'Ensure Test final changes to object
#If AssertTest Then
            If ResultSymb = 0 Then
                Throw New AssertionException("Favorites where not saved.")
            End If
#End If
        Next

        Return ResultSymb
    End Function
    Public Shared Function ExistsFavoriteName(ByVal name As String) As Boolean
        Dim TA As New SettingsDataSetTableAdapters.FavoritesTableAdapter
        Dim FoundName As Object = TA.FindName(name)
        If FoundName IsNot Nothing AndAlso FoundName.ToString = name Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function DeleteFavorite(ByVal name As String) As Integer
        Dim TA As New Settings.SettingsDataSetTableAdapters.FavoritesTableAdapter
        Dim SymbTA As New Settings.SettingsDataSetTableAdapters.FavSymbolsTableAdapter
        Dim Id As Integer = CInt(TA.GetId(name))
        SymbTA.DeleteFav(Id)
        Return TA.DeleteQuery(Id)
    End Function

    ''' <summary>
    ''' Function GetId description
    ''' </summary>
    Shared Function GetId(ByVal favoriteName As String) As Integer
        'Require Test parameters

#If AssertTest Then
        If favoriteName Is Nothing OrElse favoriteName = "" Then
            Throw New AssertionException("GetId was not passed a valid Favorite Name.  Name passed was: " & favoriteName)
        End If
#End If
        Dim Id As Integer
        Dim ta As New Settings.SettingsDataSetTableAdapters.FavoritesTableAdapter
        Id = CInt(ta.GetId(favoriteName))


        'Ensure Test return value
#If AssertTest Then
        If Id = 0 Then
            Throw New AssertionException("Could not get Id for Favorite " & favoriteName)
        End If
#End If
        Return Id
    End Function



    Private Sub New()

    End Sub
End Class
<Serializable()> _
Public Class SerializableSettings
    Public BilingualMode As Boolean
    Public DefaultSignLanguage As Integer
    Public DictionaryConnectionString As String
    Public FirstGlossLanguage As Integer
    Public LastDictionaryString As String
    Public SecondGlossLanguage As Integer
    Public ShowWalkthroughs As Boolean
    Public UserInterfaceLanguage As Integer
    Public UserInterfaceSignLanguage As Integer

    Private Sub GetSettings(ByVal BilingualMode As Boolean, ByVal DefaultSignLanguage As Integer, ByVal LastDictionaryString As String, ByVal FirstGlossLanguage As Integer, ByVal SecondGlossLanguage As Integer, ByVal ShowWalkthroughs As Boolean, ByVal UserInterfaceLanguage As Integer, ByVal UserInterfaceSignLanguage As Integer)
        Me.BilingualMode = BilingualMode
        Me.DefaultSignLanguage = DefaultSignLanguage
        Me.DictionaryConnectionString = LastDictionaryString
        Me.FirstGlossLanguage = FirstGlossLanguage
        Me.LastDictionaryString = LastDictionaryString
        Me.SecondGlossLanguage = SecondGlossLanguage
        Me.ShowWalkthroughs = ShowWalkthroughs
        Me.UserInterfaceLanguage = UserInterfaceLanguage
        Me.UserInterfaceSignLanguage = UserInterfaceSignLanguage
    End Sub
    Private Sub SetSettings(ByVal NewSettings As SerializableSettings)
        'TODO implement in SignWriterMenu
        'My.Settings.BilingualMode() = NewSettings.BilingualMode
        'My.Settings.BilingualMode() = NewSettings.DefaultSignLanguage
        'My.Settings.LastDictionaryString() = NewSettings.DictionaryConnectionString

        'My.Settings.FirstGlossLanguage() = NewSettings.FirstGlossLanguage
        'My.Settings.LastDictionaryString() = NewSettings.LastDictionaryString
        'My.Settings.SecondGlossLanguage() = NewSettings.SecondGlossLanguage
        'My.Settings.ShowWalkthroughs() = NewSettings.ShowWalkthroughs
        'My.Settings.UserInterfaceLanguage() = NewSettings.UserInterfaceLanguage
        'My.Settings.UserInterfaceSignLanguage() = NewSettings.UserInterfaceSignLanguage
        'My.Settings.Save()
    End Sub
    Public Sub Save()
        'TODO Makesure Settings are already loaded
        'Me.GetSettings()
        Dim SettingsXML As Xml.XmlDocument
        SettingsXML = SerializeObject(Me, Me.GetType)
        Dim TA As New Settings.SettingsDataSetTableAdapters.SettingsTableAdapter

        Dim retInt As Integer = SignWriterStudio.Settings.SwsSettings.UpdateSettings(SettingsXML.OuterXml)

    End Sub
    Public Sub Load()
        Dim SettingsXML As New Xml.XmlDocument
        Dim NewSettings As SerializableSettings
        Dim TA As New Settings.SettingsDataSetTableAdapters.SettingsTableAdapter
        'TODO add changes 
        SettingsXML.LoadXml((TA.GetData).Rows(0).Item(1).ToString)

        NewSettings = CType(DESerializeObject(SettingsXML, Me.GetType), SerializableSettings)
        Me.SetSettings(NewSettings)
    End Sub

End Class
Public Class SettingsPublic
    Public Shared Property SettingsConnectionString() As String
        Get
            Return My.Settings.SettingsConnectionString
        End Get
        Set(value As String)
            My.Settings.SettingsConnectionString = value
            My.Settings.Save()
        End Set

    End Property

    Public Shared Property BilingualMode() As Boolean
        Get
            Return My.Settings.BilingualMode
        End Get
        Set(ByVal value As Boolean)
            My.Settings.BilingualMode = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property DefaultSignLanguage() As Integer
        Get
            Return My.Settings.DefaultSignLanguage
        End Get
        Set(ByVal value As Integer)
            My.Settings.DefaultSignLanguage = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property FirstGlossLanguage() As Integer
        Get
            Return My.Settings.FirstGlossLanguage
        End Get
        Set(ByVal value As Integer)
            My.Settings.FirstGlossLanguage = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property FirstRun() As Boolean
        Get
            Return My.Settings.FirstRun
        End Get
        Set(ByVal value As Boolean)
            My.Settings.FirstRun = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property LastDictionaryString() As String
        Get
            Return My.Settings.LastDictionaryString
        End Get
        Set(ByVal value As String)
            My.Settings.LastDictionaryString = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property LastDocumentString() As String
        Get
            Return My.Settings.LastDocumentString
        End Get
        Set(ByVal value As String)
            My.Settings.LastDocumentString = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property SecondGlossLanguage() As Integer
        Get
            Return My.Settings.SecondGlossLanguage
        End Get
        Set(ByVal value As Integer)
            My.Settings.SecondGlossLanguage = value
            My.Settings.Save()
        End Set
    End Property


    Public Shared Property ShowWalkthroughs() As Boolean
        Get
            Return My.Settings.ShowWalkthroughs
        End Get
        Set(ByVal value As Boolean)
            My.Settings.ShowWalkthroughs = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property UserInterfaceLanguage() As Integer
        Get
            Return My.Settings.UserInterfaceLanguage
        End Get
        Set(ByVal value As Integer)
            My.Settings.UserInterfaceLanguage = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property UserInterfaceSignLanguage() As Integer
        Get
            Return My.Settings.UserInterfaceSignLanguage
        End Get
        Set(ByVal value As Integer)
            My.Settings.UserInterfaceSignLanguage = value
            My.Settings.Save()
        End Set
    End Property
    Public Shared Property CacheAllSymbols() As Boolean
        Get
            Return My.Settings.CacheAllSymbols
        End Get
        Set(ByVal value As Boolean)
            My.Settings.CacheAllSymbols = value
            My.Settings.Save()
        End Set
    End Property
End Class
