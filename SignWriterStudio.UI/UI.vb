Option Strict On
Option Explicit On
'Option Infer On
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports System
#if NUnitTest then
Imports NUnit.Framework
Imports NUnit.Framework.Constraints
#End If

Public Module Culture
    Friend _Cultures As New SWCultures
    Public Property Cultures() As SWCultures
        Get
            Return _Cultures
        End Get
        Set(ByVal value As SWCultures)
            _Cultures = value
        End Set
    End Property



    Public Function GetSignLanguageIso(ByVal idSignLanguage As Long) As String
        Static Dim ISODict As New Dictionary(Of Long, String)
        Dim ISO As String
        If ISODict.TryGetValue(idSignLanguage, ISO) Then
            Return ISO
        Else
            Dim TAUISignLanguages = New UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
            ISO = TAUISignLanguages.GetISObyID(idSignLanguage)
            ISODict.Add(idSignLanguage, ISO)
            Return ISO
        End If
    End Function

    Public Function GetlanguageIso(ByVal idCulture As Long) As String

        Static Dim ISODict As New Dictionary(Of Long, String)
        Dim ISO As String
        If ISODict.TryGetValue(idCulture, ISO) Then
            Return ISO
        Else
            Dim TACultures = New UI.swsuiDataSetTableAdapters.UICulturesTableAdapter
            ISO = TACultures.GetNamebyID(idCulture)
            ISODict.Add(idCulture, ISO)
            Return ISO
        End If

    End Function
End Module

'Class
''' <summary>
''' Class SWCultures description
''' </summary>

#Region "Cultures"
Public Class SWCultures
    Implements IDisposable

    Dim TaUiSignLanguages As New swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
    Dim TAUiCultures As New swsuiDataSetTableAdapters.UICulturesTableAdapter


    Public ReadOnly Property SignLanguages() As DataTable 
        Get
            Dim DT As New swsuiDataSet.UISignLanguagesDataTable
            TaUiSignLanguages.FillBySignLanguages(DT)
         Return DT
         End Get
    End Property


    Public Function GetIdSignLanguages(ByVal iso6393 As String) As Integer
        Dim Id As Integer = CInt(TaUiSignLanguages.GetIDbyISO(iso6393))
       Return Id
    End Function
    Public Function GetSLNamebyID(ByVal ID As Integer) As String

        Dim Str As String = TaUiSignLanguages.GetSLNamebyID(ID)
        Return Str
        
    End Function
    Public Function GetSLAcronymbyID(ByVal ID As Integer) As String

        Dim Str As String = CStr(TaUiSignLanguages.GetAcronymbyID(ID))
        Return Str

    End Function

    Public ReadOnly Property Cultures() As DataTable
        Get
            Dim DT As New swsuiDataSet.UICulturesDataTable
            TAUiCultures.FillCulture(DT)
            Return DT
        End Get

    End Property



    Public ReadOnly Property IdCulture(ByVal cultureName As String) As Integer
        Get
            If cultureName IsNot Nothing AndAlso Not cultureName = String.Empty Then
                Dim ID As Integer = CInt(TAUiCultures.GetIDbyName(cultureName))
                Return ID
            End If
        End Get

    End Property
    
    Public Function GetCultureName(ByVal idCulture As Integer) As String
        Return TAUiCultures.GetNamebyID(idCulture).ToString
    End Function
    Public Function GetSignLanguageIso(ByVal idSignLanguage As Integer) As String
        Return TaUiSignLanguages.GetISObyID(idSignLanguage)
    End Function
    Public Function GetCultureFullName(ByVal idCulture As Integer) As String

        Return CStr(TAUiCultures.GetFullNamebyID(idCulture))
    End Function
    Public Function GetCultureConnectionString() As String
        Return TAUiCultures.Connection.ConnectionString
    End Function

    Public Sub SetCultureConnectionString(connStr As String)
        My.Settings.swsuiConnectionString = connStr
        Try
            Me.GetCultureFullName(4)
        Catch ex As Exception
            General.LogError(ex, "SetCultureConnectionString Error")
            Throw
        End Try
        My.Settings.Save()
    End Sub

    Public Function GetCultureIDbyIso(ByVal iso As String) As Integer
        Dim ID = TAUiCultures.GetIDbyName(iso)
        If ID.HasValue Then
            Return CInt(ID)
        End If
    End Function

    Private disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                'free other state (managed objects).
                Me.TAUiCultures.Dispose()
                Me.TaUiSignLanguages.Dispose()
            End If

            ' free your own state (unmanaged objects).
            ' set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

#End Region



'Class
''' <summary>
''' Class SWUserInterface description
''' </summary>
#Region "UI"
Public Class SWUserInterface
    Implements IDisposable

    Private UiCacheTA As New swsuiDataSetTableAdapters.UITranslationsTableAdapter
    Private UiCacheDT As New swsuiDataSet.UITranslationsDataTable
    Private UiCacheDV As New DataView(UiCacheDT)

    Public Function UicGetTranslation(ByVal uiGroup As String, ByVal uiItem As String, ByVal idCulture As Integer) As String
        FlushCache()
        If Not UiTranslationinCache(uiGroup, idCulture) Then
            'Add to Cache
            LoadUITranslationintoCache(uiGroup, idCulture)
        End If

        'Return from cache
        Dim DR() As swsuiDataSet.UITranslationsRow
        DR = CType(UiCacheDT.Select("UIGroup='" & uiGroup & "' AND UIItem='" & uiItem & "' AND  IDCulture=" & idCulture), swsuiDataSet.UITranslationsRow())
        If DR.Length > 0 Then
            Return DR(0).UITranslation
        Else
            Return String.Empty
        End If

    End Function
    Public Function UicGetTranslation(ByVal uiGroup As String, ByVal idCulture As Integer) As DataRow()
        FlushCache()
        If Not UiTranslationinCache(uiGroup, idCulture) Then
            'Add to Cache
            LoadUITranslationintoCache(uiGroup, idCulture)
        End If

        'Return from cache
        Return UiCacheDT.Select("UIGroup='" & uiGroup & "' AND IDCulture=" & idCulture)

    End Function
    Private Function UiTranslationinCache(ByVal uiGroup As String, ByVal idCulture As Integer) As Boolean
        Dim UIRows As DataRow()
        UIRows = UiCacheDT.Select("UIGroup='" & uiGroup & "' AND IDCulture=" & idCulture)
        If (UIRows.Length < 1) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Sub LoadUITranslationintoCache(ByVal uiGroup As String, ByVal idCulture As Integer)
        Dim UIDT As New swsuiDataSet.UITranslationsDataTable
        'Dim Conn As New OleDb.OleDbConnectionStringBuilder
        'Conn.ConnectionString = UICacheTA.Connection.ConnectionString
        'Conn.Add("Jet OLEDB:Database Password", "Jonathan01")
        'UICacheTA.Connection.ConnectionString = Conn.ConnectionString
        UiCacheTA.FillByGroup(UIDT, uiGroup, idCulture)
        UiCacheDT.Merge(UIDT, True, MissingSchemaAction.Ignore)
    End Sub

#Region "Manage Cache"
    Private Sub FlushCache()
        If CountRowsinCache() > 500 Then
            'Remove from Cache
            For I As Integer = UiCacheDT.Rows.Count - 1 To 0 Step -1
                UiCacheDT.Rows.RemoveAt(I)
            Next
            UiCacheDT.AcceptChanges()

        End If
    End Sub
    Private Function CountRowsinCache() As Integer
        Return UiCacheDT.Rows.Count
    End Function
#End Region



    Private disposedValue As Boolean  '= False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' free other state (managed objects).
                Me.UiCacheDT.Dispose()
                Me.UiCacheDV.Dispose()
                Me.UiCacheTA.Dispose()
            End If

            ' free your own state (unmanaged objects).
            ' set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class


#End Region
