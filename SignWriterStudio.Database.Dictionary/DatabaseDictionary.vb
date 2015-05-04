Option Strict On
Option Explicit On
'Option Infer On
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports System
#If NUnitTest Then
#End If
Imports SignWriterStudio.General
Imports System.Data.SQLite
Imports SignWriterStudio.UI
Imports SignWriterStudio.Settings
'Class
''' <summary>
''' Class DatabaseSignList description
''' </summary>
 
Public Class DatabaseDictionary
    Public Shared Function GetDataDictionaryByIdCached(ByVal allCachedDictionaryDataTable As DataTable, ByVal idDictionary As Long) As DictionaryDataSet.DictionaryDataTable
        Dim allCachedDictionaryView As DataView
        allCachedDictionaryView = allCachedDictionaryDataTable.DefaultView
        allCachedDictionaryView.Sort = "IDDictionary"

        Dim rows = allCachedDictionaryView.FindRows(New Object() {idDictionary})
        Dim dataTable As New DictionaryDataSet.DictionaryDataTable
        For Each row In rows

            dataTable.ImportRow(row.Row)
        Next

        Return dataTable
    End Function
    Public Shared Function GetDataDictionaryByGuid(ByVal allCachedDictionaryDataTable As DataTable, ByVal swGuid As Guid) As DictionaryDataSet.DictionaryRow
        Dim allCachedDictionaryView As DataView
        allCachedDictionaryView = allCachedDictionaryDataTable.DefaultView
        allCachedDictionaryView.Sort = "GUID"

        Dim rows = allCachedDictionaryView.FindRows(New Object() {swGuid})


        If rows.Count > 0 Then
            Return CType(rows(0).Row, DictionaryDataSet.DictionaryRow)
        Else
            Return Nothing
        End If

    End Function
    Public Shared Function GetDataDictionaryGlossCached(ByVal allCachedDictionaryGlossDataTable As DataTable, ByVal idDictionary As Long, ByVal idCulture As Long) As DictionaryDataSet.DictionaryGlossDataTable
        Dim allCachedDictionaryGlossView As DataView
        allCachedDictionaryGlossView = allCachedDictionaryGlossDataTable.DefaultView
        allCachedDictionaryGlossView.Sort = "IDDictionary, IDCulture"

        Dim rows = allCachedDictionaryGlossView.FindRows(New Object() {idDictionary, idCulture})
        Dim dataTable As New DictionaryDataSet.DictionaryGlossDataTable
        For Each row In rows

            dataTable.ImportRow(row.Row)
        Next

        Return dataTable
    End Function

    Public Shared Function GetDataFrameCached(ByVal allCachedFrameDataTable As DataTable, ByVal idDictionary As Long) As DictionaryDataSet.FrameDataTable
        Dim allCachedFrameView As DataView
        allCachedFrameView = allCachedFrameDataTable.DefaultView
        allCachedFrameView.Sort = "IDDictionary"
        Dim rows = allCachedFrameView.FindRows(New Object() {idDictionary})
        Dim dataTable As New DictionaryDataSet.FrameDataTable
        For Each row In rows

            dataTable.ImportRow(row.Row)
        Next

        Return dataTable
    End Function

    Public Shared Function GetSymbolsDtCached(ByVal allCachedSignSymbolsDatatable As DataTable, ByVal idSWFrame As Long) As DictionaryDataSet.SignSymbolsDataTable
        Dim allCachedSignSymbolsView As DataView
        allCachedSignSymbolsView = allCachedSignSymbolsDatatable.DefaultView
        allCachedSignSymbolsView.Sort = "IDFrame"

        Dim rows = allCachedSignSymbolsView.FindRows(New Object() {idSWFrame})
        Dim dataTable As New DictionaryDataSet.SignSymbolsDataTable
        For Each row In rows

            dataTable.ImportRow(row.Row)
        Next

        Return dataTable
    End Function

    Public Shared Function GetSequenceDtCached(ByVal allCachedSignSequenceDataTable As DataTable, ByVal idSWFrame As Long) As DictionaryDataSet.SignSequenceDataTable
        Dim allCachedSignSequenceView As DataView
        allCachedSignSequenceView = allCachedSignSequenceDataTable.DefaultView
        allCachedSignSequenceView.Sort = "IDFrame"

        Dim rows = allCachedSignSequenceView.FindRows(New Object() {idSWFrame})
        Dim dataTable As New DictionaryDataSet.SignSequenceDataTable
        For Each row In rows

            dataTable.ImportRow(row.Row)
        Next

        Return dataTable
    End Function

    Public Shared Function GetPuddleTextDtCached(ByVal allCachedPuddleTextDataTable As DataTable, ByVal idDictionary As Long) As DictionaryDataSet.PuddleTextDataTable
        Dim allCachedPuddleTextView As DataView
        allCachedPuddleTextView = allCachedPuddleTextDataTable.DefaultView
        allCachedPuddleTextView.Sort = "IDDictionary"

        Dim rows = allCachedPuddleTextView.FindRows(New Object() {idDictionary})
        Dim dataTable As New DictionaryDataSet.PuddleTextDataTable
        For Each row In rows

            dataTable.ImportRow(row.Row)
        Next

        Return dataTable
    End Function

End Class


Public NotInheritable Class DictLanguages

    Public Shared Function LanguagesInDictionary() As String
        'TODO set TableAdapter connection string on Dictionary DataTables
        Dim taDict As New DictionaryDataSetTableAdapters.DictionaryTableAdapter
        Dim stringBuilder As New Text.StringBuilder
        Dim cultureId As Integer
        Try
            Dim signLanguages As DictionaryDataSet.DictionaryDataTable = taDict.GetDataBySignLanguagesinDictionary
            Dim glossLanguages As DictionaryDataSet.DictionaryDataTable = taDict.GetDataByGlossLanguagesinDictionary


            Dim gldv As DictionaryDataSet.DictionaryRow()
            Dim first As Boolean = True
            If signLanguages.Rows.Count = 0 Then
                stringBuilder.Append("There are currently no entries in this file.")
            Else
                stringBuilder.Append("The following sign languages are in the current database:" & VbCrLf())
                For Each slRow As DictionaryDataSet.DictionaryRow In signLanguages.Rows
                    stringBuilder.Append(GetSignLanguageName(CInt(slRow.IDSignLanguage)))
                    stringBuilder.Append(" with the following gloss languages: " & VbCrLf())
                    gldv = CType(glossLanguages.Select("IDSignLanguage=" & slRow.IDSignLanguage.ToString(Globalization.CultureInfo.InvariantCulture), "IDCulture"), DictionaryDataSet.DictionaryRow())
                    'TODO review GLRow change to appropriate type.
                    For Each glRow As DataRow In gldv
                        cultureId = CInt(NZ(glRow.Item("IDCulture"), 0))
                        If cultureId <> 0 Then
                            If first Then
                                stringBuilder.Append(UI.Cultures.GetCultureFullName(cultureId))
                                first = False
                            Else
                                stringBuilder.Append(", ")
                                stringBuilder.Append(UI.Cultures.GetCultureFullName(cultureId))
                            End If
                        Else
                            stringBuilder.Append("language name not found")
                        End If
                    Next
                    stringBuilder.Append("." & VbCrLf())

                Next
            End If
        Catch exSqLite As SQLiteException
            LogError(exSqLite, "")
        Catch exOleDb As OleDb.OleDbException
            'MessageBox.Show(exOleDB.Message, "Error")
            LogError(exOleDb, "")
            'My.Application.Log.WriteException(exOleDB, _
            '                TraceEventType.Error, _
            '                "Exception ")
        Catch ex As DataException
            LogError(ex, "")
            'monitor.TrackException(ex, _
            '                                TraceEventType.Error.ToString, _
            '                                "Exception ")
            'My.Application.Log.WriteException(ex, _
            '                  TraceEventType.Error, _
            '                  "Exception ")


        End Try
        'StringBuilder.Append("To Continue, choose an option from the module menu." & vbCrLf)
        Return stringBuilder.ToString
    End Function
    Public Shared Function GetSignLanguageName(ByVal idSignLanguage As Integer) As String
        Dim tauisl As New UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
        Return tauisl.GetSLNamebyID(idSignLanguage)
    End Function
    Private Shared _dictionaryConnectionString As String
    Public Shared Property DictionaryConnectionString() As String
        Get
            Return _dictionaryConnectionString
        End Get
        Set(ByVal value As String)
            _dictionaryConnectionString = value
        End Set
    End Property

    Private Sub New()

    End Sub


End Class

Public NotInheritable Class ReportDataSources
    Public Shared Function GetDictionaryReport(lang1Id As Integer, lang2Id As Integer, slid As Integer, bilingualMode As Boolean) As DataTable
        Dim dt As DataTable
        Dim taSignsbyGlossesBilingual As New DictionaryDataSetTableAdapters.SignsbyGlossesBilingualTableAdapter
        Dim taSignsbyGlossesUnilingual As New DictionaryDataSetTableAdapters.SignsbyGlossesUnilingualTableAdapter


        If lang1Id = 0 Then
            lang1Id = 54
        End If
        If lang2Id = 0 Then
            lang2Id = 157
        End If
        If slid = 0 Then
            slid = 4
        End If
        If bilingualMode Then
            Dim dtBilingual As New DictionaryDataSet.SignsbyGlossesBilingualDataTable
            taSignsbyGlossesBilingual.FillByAll(dtBilingual, slid, lang1Id, lang2Id)
            dt = dtBilingual
        Else
            Dim dtUnilingual As New DictionaryDataSet.SignsbyGlossesUnilingualDataTable
            taSignsbyGlossesUnilingual.FillByAll(dtUnilingual, slid, lang1Id)
            dt = dtUnilingual
        End If

        Return dt
    End Function
End Class


