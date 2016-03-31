Option Strict On
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Dynamic
Imports NUnit.Framework
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.General.All
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSet
Imports System.Data.SQLite
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters
Imports SignWriterStudio.Database.Dictionary.DatabaseDictionary
Imports System.Text
'Imports SignsbyGlossesBilingual = SignWriterStudio.DbTags.SignsbyGlosses.SignsbyGlossesBilingual
'Imports SignsByGlossesUnilingual = SignWriterStudio.DbTags.SignsbyGlosses.SignsByGlossesUnilingual

Public NotInheritable Class SWDict
    Implements IDisposable
    'Dim transaction As SQLiteTransaction = Nothing

    ' In this section you can add your own using directives
    ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:000000000000077C begin
    ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:000000000000077C end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */


    ' Attributes
    Public WithEvents DictionaryBindingSource1 As New BindingSource
    Public WithEvents DictionaryBindingSource2 As New BindingSource
    Private _defaultSignLanguage As Integer

    Public Property DefaultSignLanguage() As Integer
        Get
            Return _defaultSignLanguage
        End Get
        Set(ByVal value As Integer)
            _defaultSignLanguage = value
        End Set
    End Property
    Private _firstGlossLanguage As Integer
    Public Property FirstGlossLanguage() As Integer
        Get
            Return _firstGlossLanguage
        End Get
        Set(ByVal value As Integer)
            _firstGlossLanguage = value
        End Set
    End Property
    Private _secondGlossLanguage As Integer
    Public Property SecondGlossLanguage() As Integer
        Get
            Return _secondGlossLanguage
        End Get
        Set(ByVal value As Integer)
            _secondGlossLanguage = value
        End Set
    End Property
    Private _bilingualMode As Boolean = True

    Public Property BilingualMode() As Boolean
        Get
            Return _bilingualMode
        End Get
        Set(ByVal value As Boolean)
            _bilingualMode = value
        End Set
    End Property
    '    Private RetrieveDictID As Boolean '= False
    Private ReadOnly _taDictionarybyLanguages As New Database.Dictionary.DictionaryDataSetTableAdapters.SignsbyGlossesBilingualTableAdapter
    'Private TASWSign As New Database.Dictionary.DictionaryDataSetTableAdapters.SignTableAdapter
    Private ReadOnly _taswFrame As New Database.Dictionary.DictionaryDataSetTableAdapters.FrameTableAdapter
    Private ReadOnly _taswSignSymbol As New Database.Dictionary.DictionaryDataSetTableAdapters.SignSymbolsTableAdapter
    Private ReadOnly _taswSignSequence As New Database.Dictionary.DictionaryDataSetTableAdapters.SignSequenceTableAdapter
    Private ReadOnly _taSignsbyGlossesBilingual As New Database.Dictionary.DictionaryDataSetTableAdapters.SignsbyGlossesBilingualTableAdapter
    Private ReadOnly _taSignsbyGlossesUnilingual As New Database.Dictionary.DictionaryDataSetTableAdapters.SignsbyGlossesUnilingualTableAdapter
    Private ReadOnly _tauiSignLanguages As New UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
    Private ReadOnly _tauiCultures As New UI.swsuiDataSetTableAdapters.UICulturesTableAdapter
    Private ReadOnly _taSignsBilingual As New Database.Dictionary.DictionaryDataSetTableAdapters.SignsBilingualTableAdapter
    Private ReadOnly _taDictionaryGloss As New Database.Dictionary.DictionaryDataSetTableAdapters.DictionaryGlossTableAdapter
    Private ReadOnly _taDictionary As New Database.Dictionary.DictionaryDataSetTableAdapters.DictionaryTableAdapter

    ' Operations
    Public Sub SearchText(ByVal search As String)
        UpdateDataSources(search)
    End Sub
    Public Function PagingSearchText(ByVal search As String, ByVal pageSize As Integer, ByVal skip As Integer, ByVal count As Boolean, ByVal tagFilterValues As TagFilterValues) As Integer
        Dim totalRowCount = UpdateDataSources(search, pageSize, skip, count, tagFilterValues)
        Return totalRowCount
    End Function
    Public Sub GetbyIdDictionary(ByVal idDictionary As Integer)
        UpdateDataSources(idDictionary)
    End Sub

    Public Function SignsinDictionaryCount() As Long
        Dim count As Long

        count = CLng(_taSignsbyGlossesUnilingual.Count(DefaultSignLanguage, FirstGlossLanguage))
        Return count
    End Function
    Public Shared Function GetNewDictionaryConnection() As SQLiteConnection
        Dim dictionaryTa As New DictionaryTableAdapter
        Return CType(dictionaryTa.PublicConnection.Clone, SQLiteConnection)
    End Function
    Public Shared Function GetNewDictionaryTransaction(ByVal conn As SQLiteConnection) As SQLiteTransaction
        conn.Open()
        Return conn.BeginTransaction
    End Function

    Public Sub SignstoDictionary(ByVal signs As ICollection(Of SwSign), ByVal bw As System.ComponentModel.BackgroundWorker)
        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)
        Using conn
            Try

                Dim I As Integer
                Dim count As Integer = signs.Count
                Dim dictionaryTa As New DictionaryTableAdapter

                'Dim timestart = Now()

                Dim dictionaryGlossTa As New DictionaryGlossTableAdapter
                Dim frameTa As New FrameTableAdapter
                Dim symbolTa As New SignSymbolsTableAdapter
                Dim sequenceTa As New SignSequenceTableAdapter
                Dim puddleTextTa As New PuddleTextTableAdapter

                dictionaryTa.AssignConnection(conn, trans)
                dictionaryGlossTa.AssignConnection(conn, trans)
                frameTa.AssignConnection(conn, trans)
                symbolTa.AssignConnection(conn, trans)
                sequenceTa.AssignConnection(conn, trans)
                puddleTextTa.AssignConnection(conn, trans)

                Dim dictionaryGuid As Guid
                Dim slId As Long
                Dim dictionaryId As Long

                Dim frameId As Long

                For Each sign As SwSign In signs
                    I += 1

                    If sign.SignWriterGuid.HasValue Then
                        dictionaryGuid = sign.SignWriterGuid.Value
                    Else
                        dictionaryGuid = Guid.NewGuid
                    End If
                    Dim tauisl As New UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
                    slId = CLng(tauisl.GetIDbyISO(sign.SignLanguageIso))
                    Dim img = sign.Render
                    Dim swSignByte As Byte() = ImageToByteArray(img)
                    img.Dispose()
                    Dim photoByte As Byte()
                    Dim signByte As Byte()

                    Dim sortWeight = SequencetoSortingString(sign.Frames(0).Sequences)

                    dictionaryId = CLng(dictionaryTa.InsertGetId(slId, False, sign.BkColor.ToArgb, swSignByte, photoByte, signByte, sign.SWritingSource, String.Empty, String.Empty, dictionaryGuid, sign.Created, sign.LastModified, sign.SignPuddleId, sign.SignPuddleUser, sign.PuddlePrev, sign.PuddleNext, sign.PuddlePng, sign.PuddleSvg, sign.PuddleVideoLink, sortWeight))




                    frameId = CLng(frameTa.InsertGetId(dictionaryId, 0, sign.Frames(0).Bounds.Left, sign.Frames(0).Bounds.Top, sign.Frames(0).MinWidth, sign.Frames(0).MinHeight))

                    For Each SignSymbol In sign.Frames(0).SignSymbols
                        symbolTa.InsertQuery(frameId, SignSymbol.Code, SignSymbol.X, SignSymbol.Y, SignSymbol.Z, SignSymbol.Hand, SignSymbol.Handcolor, SignSymbol.Palmcolor, SignSymbol.Size)

                    Next

                    For Each Sequence In sign.Frames(0).Sequences
                        sequenceTa.InsertQuery(frameId, Sequence.Code, Sequence.Rank)
                    Next

                    For Each txt In sign.PuddleText
                        puddleTextTa.InsertQuery(dictionaryId, txt)
                    Next
                    Dim tauiCult As New UI.swsuiDataSetTableAdapters.UICulturesTableAdapter
                    Dim cultureId As Long? = tauiCult.GetIDbyName(sign.LanguageIso)

                    dictionaryGlossTa.InsertQuery(dictionaryId, cultureId, sign.Gloss, sign.Glosses)
                    If bw IsNot Nothing Then
                        bw.ReportProgress(15 + CInt((I / count) * 85))
                    End If
                Next

                trans.Commit()
                'MessageBox.Show((Now() - timestart).TotalSeconds.ToString)
            Catch ex As Exception
                LogError(ex, "")
                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                conn.Close()
                conn.Dispose()
                trans.Dispose()
            End Try
        End Using
    End Sub

    Public Sub SignstoDictionaryInsert(ByVal signs As ICollection(Of SwSign), ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)
        Dim dictionaryTa As New DictionaryTableAdapter

        'Dim timestart = Now()

        Dim dictionaryGlossTa As New DictionaryGlossTableAdapter
        Dim frameTa As New FrameTableAdapter
        Dim symbolTa As New SignSymbolsTableAdapter
        Dim sequenceTa As New SignSequenceTableAdapter
        Dim puddleTextTa As New PuddleTextTableAdapter


        dictionaryTa.AssignConnection(conn, trans)
        dictionaryGlossTa.AssignConnection(conn, trans)
        frameTa.AssignConnection(conn, trans)
        symbolTa.AssignConnection(conn, trans)
        sequenceTa.AssignConnection(conn, trans)
        puddleTextTa.AssignConnection(conn, trans)



        Dim dictionaryGuid As Guid
        Dim slId As Long
        Dim dictionaryId As Long

        Dim frameId As Long

        For Each sign As SwSign In signs

            If sign.SignWriterGuid.HasValue Then
                dictionaryGuid = sign.SignWriterGuid.Value
            Else
                dictionaryGuid = Guid.NewGuid
            End If
            Dim tauisl As New UI.swsuiDataSetTableAdapters.UISignLanguagesTableAdapter
            slId = CLng(tauisl.GetIDbyISO(sign.SignLanguageIso))
            Dim img = sign.Render
            Dim swSignByte As Byte() = ImageToByteArray(img)
            img.Dispose()
            Dim photoByte As Byte()
            Dim signByte As Byte()

            Dim sortOrder = SequencetoSortingString(sign.Frames(0).Sequences)

            dictionaryId = CLng(dictionaryTa.InsertGetId(slId, False, sign.BkColor.ToArgb, swSignByte, photoByte, signByte, sign.SWritingSource, String.Empty, String.Empty, dictionaryGuid, sign.Created, sign.LastModified, sign.SignPuddleId, sign.SignPuddleUser, sign.PuddlePrev, sign.PuddleNext, sign.PuddlePng, sign.PuddleSvg, sign.PuddleVideoLink, sortOrder))


            frameId = CLng(frameTa.InsertGetId(dictionaryId, 0, sign.Frames(0).Bounds.Left, sign.Frames(0).Bounds.Top, sign.Frames(0).MinWidth, sign.Frames(0).MinHeight))

            For Each SignSymbol In sign.Frames(0).SignSymbols
                symbolTa.InsertQuery(frameId, SignSymbol.Code, SignSymbol.X, SignSymbol.Y, SignSymbol.Z, SignSymbol.Hand, SignSymbol.Handcolor, SignSymbol.Palmcolor, SignSymbol.Size)

            Next

            For Each Sequence In sign.Frames(0).Sequences
                sequenceTa.InsertQuery(frameId, Sequence.Code, Sequence.Rank)
            Next

            For Each txt In sign.PuddleText
                puddleTextTa.InsertQuery(dictionaryId, txt)
            Next
            Dim tauiCult As New UI.swsuiDataSetTableAdapters.UICulturesTableAdapter
            Dim cultureId As Long? = tauiCult.GetIDbyName(sign.LanguageIso)

            dictionaryGlossTa.InsertQuery(dictionaryId, cultureId, sign.Gloss, sign.Glosses)

        Next



    End Sub

    Public Function GetSymbolSearchDt(ByVal connectionString As String, _
      ByVal queryStr As String) As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable

        Dim dt As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
        Dim signsbyGlossesBilingualTableAdapter As New SignsbyGlossesBilingualTableAdapter()
        Dim dt1 As New Data.DataTable
        Dim strConection = CreateConnectionStringFromPath(connectionString)

        Using connection As New SQLiteConnection(strConection)
            Dim adapter As New SQLiteDataAdapter
            adapter.SelectCommand = New SQLiteCommand(queryStr, connection)
            'Dim cb As OleDb.OleDbCommandBuilder = New OleDb.OleDbCommandBuilder(adapter)
            'TODO use one transaction
            'TODO different search parameters
            connection.Open()

            adapter.Fill(dt1)
            Dim idNums = From row1 In dt1.Rows Select DirectCast(row1, DataRow).Item(0) Distinct
            For Each rowId As Long In idNums

                Dim results = signsbyGlossesBilingualTableAdapter.GetDataByID(DefaultSignLanguage, rowId, FirstGlossLanguage, SecondGlossLanguage)
                'Dim listofRows As New List(Of SignsbyGlossesBilingualRow)
                For Each signsbyGlossesBilingualRow As SignsbyGlossesBilingualRow In results
                    'listofRows.add(signsbyGlossesBilingualRow)
                    dt.ImportRow(signsbyGlossesBilingualRow)
                Next
                'For Each signsbyGlossesBilingualRow As SignsbyGlossesBilingualRow In listofRows
                '    results.RemoveSignsbyGlossesBilingualRow(signsbyGlossesBilingualRow)
                '    dt.AddSignsbyGlossesBilingualRow(signsbyGlossesBilingualRow)
                'Next

            Next


            connection.Close()
        End Using

        Return dt
    End Function

    Private Function CreateConnectionStringFromPath(ByVal connectionString As String) As String

        Return "data source=""" & connectionString & """"
        'data source="C:\Users\Jonathan\Documents\SignWriter Studio Sample Files\LESHO.SWS"
    End Function

    Public Function AddSign(ByVal newEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow, ByVal lang1 As Integer, ByVal lang2 As Integer, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction) As Integer
        ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000793 begin
        'TODO figure out how to split newEntry into two typed datarows.
        Dim newId As Integer

        newId = InsertDictionaryEntry(newEntry, conn, trans)
        'Language1 
        _taDictionaryGloss.AssignConnection(conn, trans)
        _taDictionaryGloss.InsertQuery(newId, lang1, NZ(newEntry.Item("gloss1"), String.Empty).ToString, NZ(newEntry.Item("glosses1"), String.Empty).ToString)
        'Language2
        If BilingualMode Then
            _taDictionaryGloss.InsertQuery(newId, lang2, NZ(newEntry.Item("gloss2"), String.Empty).ToString, NZ(newEntry.Item("glosses2"), String.Empty).ToString)
        End If
        Return newId
        ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000793 end
    End Function

    Public Sub DeleteSign(ByVal idDictionary As Long, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)
        ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000795 begin
        _taDictionaryGloss.AssignConnection(conn, trans)
        _taDictionary.AssignConnection(conn, trans)
        _taswFrame.AssignConnection(conn, trans)
        _taswSignSequence.AssignConnection(conn, trans)
        _taswSignSymbol.AssignConnection(conn, trans)

        Dim puddleText = New Database.Dictionary.DictionaryDataSetTableAdapters.PuddleTextTableAdapter
        puddleText.AssignConnection(conn, trans)

        Dim idFrames = _taswFrame.GetDataByIDDictionary(idDictionary)
        For Each frameRow As FrameRow In idFrames
            _taswSignSymbol.DeletebyIDFrame(frameRow.IDFrame)
            _taswSignSequence.DeletebyFrame(frameRow.IDFrame)
        Next
        puddleText.DeletebyIDDictionary(idDictionary)
        _taswFrame.DeletebyIDDictionary(idDictionary)
        _taDictionaryGloss.DeletebyID(idDictionary)
        _taDictionary.DeletebyID(idDictionary)
        ' section 127-0-0-1-64774d6b:11b4c03f30f:-8000:0000000000000795 end
    End Sub

    Sub DeleteSigns(selectedSigns As SwCollection(Of Tuple(Of SwSign, DictionaryRow)), ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)
        For Each Sign In selectedSigns
            DeleteSign(Sign.Item2.IDDictionary, conn, trans)
        Next
    End Sub

    Public Sub ModifySign(ByVal modifiedEntry As DataRow, ByVal lang1 As Integer, ByVal lang2 As Integer, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)
        Dim modifiedInfo As SignsbyGlossesBilingualRow = CType(modifiedEntry, SignsbyGlossesBilingualRow)
        'TODO figure out how to distinguish gloss1 and gloss2 from modifiedEntry

        Dim swSignByte As Byte()
        Dim photoByte As Byte()
        Dim signByte As Byte()

        swSignByte = NulltoByteArray(modifiedInfo.SWriting)
        photoByte = NulltoByteArray(modifiedInfo.Photo)
        signByte = NulltoByteArray(modifiedInfo.Sign)

        _taDictionary.AssignConnection(conn, trans)
        _taDictionary.UpdateQuery(modifiedInfo.IDSignLanguage, modifiedInfo.isPrivate, swSignByte, photoByte, signByte, modifiedInfo.PhotoSource, modifiedInfo.SignSource, modifiedInfo.SWritingSource, Date.UtcNow, modifiedInfo.IDSignPuddle, modifiedInfo.Sorting, modifiedInfo.IDDictionary)
        'Language1
        _taDictionaryGloss.AssignConnection(conn, trans)
        Dim lang1RowsAlreadyinDb As Integer = CInt(_taDictionaryGloss.CheckifExists(modifiedInfo.IDDictionary, lang1))
        Dim lang2RowsAlreadyinDb As Integer = CInt(_taDictionaryGloss.CheckifExists(modifiedInfo.IDDictionary, lang2))

        If lang1RowsAlreadyinDb > 0 Then
            _taDictionaryGloss.UpdateQuery(modifiedInfo.IDDictionary, lang1, NZ(modifiedEntry.Item("gloss1"), String.Empty).ToString, NZ(modifiedEntry.Item("glosses1"), String.Empty).ToString)
        Else
            _taDictionaryGloss.InsertQuery(modifiedInfo.IDDictionary, lang1, NZ(modifiedEntry.Item("gloss1"), String.Empty).ToString, NZ(modifiedEntry.Item("glosses1"), String.Empty).ToString)
        End If
        'Language2
        If BilingualMode Then
            If lang2RowsAlreadyinDb > 0 Then
                _taDictionaryGloss.UpdateQuery(modifiedInfo.IDDictionary, lang2, NZ(modifiedEntry.Item("gloss2"), String.Empty).ToString, NZ(modifiedEntry.Item("glosses2"), String.Empty).ToString)
            Else
                _taDictionaryGloss.InsertQuery(modifiedInfo.IDDictionary, lang2, NZ(modifiedEntry.Item("gloss2"), String.Empty).ToString, NZ(modifiedEntry.Item("glosses2"), String.Empty).ToString)
            End If
        End If
    End Sub


    Public Sub DuplicateSign(ByVal rowtoInsert As DataRowView)

        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)
        Using conn
            Try
                If TypeOf rowtoInsert.Row Is SignsbyGlossesBilingualRow Then
                    Dim rowtoIns As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow = CType(rowtoInsert.Row, Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow)

                    ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D begin
                    If rowtoInsert Is Nothing Then
                        Throw New ArgumentNullException("rowtoInsert")
                    End If
                    Dim insertedRowId As Integer

                    insertedRowId = InsertDuplicatedDictionaryEntry(rowtoIns, conn, trans)
                    DuplicateDictionaryEntryTranslations(CInt(rowtoIns.IDDictionary), insertedRowId, conn, trans)
                    DuplicateRowSign(rowtoIns.IDDictionary, insertedRowId, conn, trans)
                Else
                    Dim rowtoIns As Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualRow = CType(rowtoInsert.Row, Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualRow)

                    ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D begin
                    If rowtoInsert Is Nothing Then
                        Throw New ArgumentNullException("rowtoInsert")
                    End If
                    Dim insertedRowId As Integer

                    insertedRowId = InsertDuplicatedDictionaryEntry(rowtoIns, conn, trans)
                    DuplicateDictionaryEntryTranslations(CInt(rowtoIns.IDDictionary), insertedRowId, conn, trans)
                    DuplicateRowSign(rowtoIns.IDDictionary, insertedRowId, conn, trans)
                End If

                trans.Commit()
            Catch ex As Exception
                LogError(ex, "")
                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                conn.Close()

            End Try
        End Using

        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D end
    End Sub

    Private Sub DuplicateRowSign(toDuplicateRowId As Long, insertedRowId As Integer, conn As SQLiteConnection, trans As SQLiteTransaction)

        Dim sign = GetSWSign(toDuplicateRowId, conn, trans)
        'Save SWriting
        SaveSWSign(insertedRowId, sign, conn, trans)
    End Sub
    'Public Function GetSignDT(ByVal idDictionary As Integer) As Database.Dictionary.DictionaryDataSet.SignDataTable
    '    Dim DT As New Database.Dictionary.DictionaryDataSet.SignDataTable
    '    TASWSign.FillByIDDictionary(DT, idDictionary)
    '    Return DT
    'End Function
    'Friend Function GetImage(ByVal idDictionary As Integer) As Image

    '    Return SWDrawing.ByteArrayToImage(TADictionary.GetPhotobyId(IDDictionary))

    'End Function
    'Public Function GetSign(ByVal idDictionary As Integer) As Image

    '    Return ByteArraytoImage(CType(TADictionary.GetSignbyID(idDictionary), Byte()))

    'End Function

    Private Function GetFrameDt(ByVal idDictionary As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) As Database.Dictionary.DictionaryDataSet.FrameDataTable
        _taswFrame.AssignConnection(conn, trans)
        Dim dt As New Database.Dictionary.DictionaryDataSet.FrameDataTable
        _taswFrame.FillByIDDictionary(dt, idDictionary)
        Return dt
    End Function
    Public Function GetSymbolsDt(ByVal idSWFrame As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) As Database.Dictionary.DictionaryDataSet.SignSymbolsDataTable
        _taswSignSymbol.AssignConnection(conn, trans)
        Dim dt As New Database.Dictionary.DictionaryDataSet.SignSymbolsDataTable
        _taswSignSymbol.FillByIDFrame(dt, idSWFrame)
        Return dt
    End Function
    Public Function GetSequenceDt(ByVal idSWFrame As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) As Database.Dictionary.DictionaryDataSet.SignSequenceDataTable
        _taswSignSequence.AssignConnection(conn, trans)
        Dim dt As New Database.Dictionary.DictionaryDataSet.SignSequenceDataTable
        _taswSignSequence.FillByIDFrame(dt, idSWFrame)
        Return dt
    End Function

    Public Function GetPuddleTextDt(ByVal idDictionary As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) As Database.Dictionary.DictionaryDataSet.PuddleTextDataTable
        Dim dt As New Database.Dictionary.DictionaryDataSet.PuddleTextDataTable
        Dim taPuddleText As New PuddleTextTableAdapter
        taPuddleText.AssignConnection(conn, trans)
        taPuddleText.FillByIDDictionary(dt, idDictionary)
        Return dt
    End Function
    Function GetGlosstoSign(ds As List(Of Tuple(Of Integer, String, Integer))) As List(Of Tuple(Of SwSign, Integer))
        Dim signs = New List(Of Tuple(Of SwSign, Integer))
        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)
        Using conn
            Try


                For Each id In ds
                    If id.Item1 > 0 Then
                        signs.Add(Tuple.Create(GetSWSign(id.Item1, conn, trans), id.Item3))
                    Else
                        Dim blankSign As New SwSign
                        If id.Item2 = String.Empty Then
                            blankSign.Gloss = id.Item2
                        End If
                        signs.Add(Tuple.Create(blankSign, id.Item3))
                    End If
                Next

                Return signs


            Catch

            End Try

        End Using
        Return Nothing
    End Function

    Public Function GetSWSign(ByVal idDictionary As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) As SwSign
        Dim sign As SwSign
        If Not idDictionary = 0 Then
            'Dim DTSignInfo As Database.Dictionary.DictionaryDataSet.DictionaryDataTable = GetSignDT(idDictionary)

            _taDictionary.AssignConnection(conn, trans)
            Dim dtDictionary As Database.Dictionary.DictionaryDataSet.DictionaryDataTable = _taDictionary.GetDataByID(idDictionary)

            'Skip if none found
            If dtDictionary.Count >= 1 Then
                sign = New SwSign
                sign.BkColor = Color.FromArgb(dtDictionary(0).bkColor)
                _taDictionaryGloss.AssignConnection(conn, trans)
                Dim dtDictionaryGloss As Database.Dictionary.DictionaryDataSet.DictionaryGlossDataTable = _taDictionaryGloss.GetDataByIDDictionayandLanguage(idDictionary, My.Settings.FirstGlossLanguage)

                If dtDictionaryGloss.Count > 0 Then
                    sign.Gloss = dtDictionaryGloss(0).gloss
                    sign.Glosses = dtDictionaryGloss(0).glosses
                    sign.SetlanguageIso(My.Settings.FirstGlossLanguage)
                End If
                If dtDictionary.Count > 0 Then
                    sign.SetSignLanguageIso(CInt(dtDictionary(0).IDSignLanguage))
                    sign.Created = dtDictionary(0).Created
                    sign.LastModified = dtDictionary(0).LastModified
                    sign.PuddleNext = dtDictionary(0).PuddleNext
                    sign.PuddleNext = dtDictionary(0).PuddleNext
                    sign.PuddlePng = dtDictionary(0).PuddlePNG
                    sign.PuddlePrev = dtDictionary(0).PuddlePrev
                    sign.PuddleSvg = dtDictionary(0).PuddleSVG
                    sign.PuddleVideoLink = dtDictionary(0).PuddleVideoLink
                    sign.SignPuddleUser = dtDictionary(0).SignPuddleUser
                    sign.SWritingSource = dtDictionary(0).SWritingSource
                    sign.SignPuddleId = dtDictionary(0).IDSignPuddle
                    sign.SignWriterGuid = dtDictionary(0).GUID
                    sign.IsPrivate = dtDictionary(0).isPrivate
                End If

                Dim dtFrameInfo As Database.Dictionary.DictionaryDataSet.FrameDataTable = GetFrameDt(dtDictionary(0).IDDictionary, conn, trans)
                Dim frameRow As Database.Dictionary.DictionaryDataSet.FrameRow

                For Each frameRow In dtFrameInfo.Rows
                    If frameRow.FrameIndex > sign.Frames.Count - 1 Then
                        sign.Frames.Add(New SWFrame)
                    End If
                    If Not frameRow.FrameIndex > sign.Frames.Count - 1 Then
                        sign.Frames(frameRow.FrameIndex).MinHeight = frameRow.MinHeight
                        sign.Frames(frameRow.FrameIndex).MinWidth = frameRow.MinWidth
                    Else
                        Const mbo As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
                        MessageBox.Show("Error loading Frame from Dictionary", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
                    End If
                    'Add Symbols

                    Dim dtSymbolsInfo As Database.Dictionary.DictionaryDataSet.SignSymbolsDataTable = GetSymbolsDt(frameRow.IDFrame, conn, trans)

                    Dim symbolRow As Database.Dictionary.DictionaryDataSet.SignSymbolsRow
                    For Each symbolRow In dtSymbolsInfo.Rows
                        Dim symbol As New SWSignSymbol
                        symbol.Code = symbolRow.code
                        Try
                            symbol.Hand = symbolRow.hand
                        Catch ex As StrongTypingException
                            symbol.Hand = 0
                        End Try

                        symbol.Handcolor = symbolRow.handcolor
                        symbol.Palmcolor = symbolRow.palmcolor
                        symbol.Size = symbolRow.size
                        symbol.X = symbolRow.x
                        symbol.Y = symbolRow.y
                        symbol.Z = symbolRow.z
                        'Symbol.Update()
                        sign.Frames(frameRow.FrameIndex).SignSymbols.Add(symbol.Clone)
                    Next

                    'Add Sequence

                    Dim dtSequenceInfo As Database.Dictionary.DictionaryDataSet.SignSequenceDataTable = GetSequenceDt(frameRow.IDFrame, conn, trans)
                    Dim sequenceRow As Database.Dictionary.DictionaryDataSet.SignSequenceRow
                    For Each sequenceRow In dtSequenceInfo.Rows
                        Dim sequence As New SWSequence(sequenceRow.code, sequenceRow.rank)
                        sign.Frames(frameRow.FrameIndex).Sequences.Add(CType(sequence.Clone, SWSequence))
                    Next
                    'Add PuddleText
                    Dim dtPuddleText As Database.Dictionary.DictionaryDataSet.PuddleTextDataTable = GetPuddleTextDt(dtDictionary(0).IDDictionary, conn, trans)
                    Dim puddleTextRow As Database.Dictionary.DictionaryDataSet.PuddleTextRow
                    For Each puddleTextRow In dtPuddleText.Rows
                        sign.PuddleText.Add(puddleTextRow.EntryText)
                    Next
                Next
            Else
                sign = Nothing
            End If
        Else
            sign = Nothing
        End If
        Return sign
    End Function
    Public Function GetSWSign(ByVal idDictionary As Long) As SwSign
        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)

        Dim sign = GetSWSign(idDictionary, conn, trans)
        trans.Commit()
        trans.Dispose()
        conn.Close()
        conn.Dispose()
        Return sign

    End Function
    Public Function GetSWSignCached(ByVal idDictionary As Long) As SwSign
        Dim sign As SwSign
        Static cacheLoaded As Boolean
        Static allCachedDictionaryDataTable As DataTable
        Static allCachedDictionaryGlossDataTable As DataTable
        Static allCachedFrameDataTable As DataTable
        Static allCachedSignSymbolsDatatable As DataTable
        Static allCachedSignSequenceDataTable As DataTable
        Static allCachedPuddleTextDataTable As DataTable
        If cacheLoaded = False Then
            Dim taDictionary As New Database.Dictionary.DictionaryDataSetTableAdapters.DictionaryTableAdapter
            allCachedDictionaryDataTable = taDictionary.GetData()

            Dim taDictionaryGloss As DictionaryGlossTableAdapter = New Database.Dictionary.DictionaryDataSetTableAdapters.DictionaryGlossTableAdapter
            allCachedDictionaryGlossDataTable = taDictionaryGloss.GetData()

            Dim taFrame As New Database.Dictionary.DictionaryDataSetTableAdapters.FrameTableAdapter
            allCachedFrameDataTable = taFrame.GetData()

            Dim taSignSymbols As New Database.Dictionary.DictionaryDataSetTableAdapters.SignSymbolsTableAdapter
            allCachedSignSymbolsDatatable = taSignSymbols.GetData()

            Dim taSignSequence As New Database.Dictionary.DictionaryDataSetTableAdapters.SignSequenceTableAdapter
            allCachedSignSequenceDataTable = taSignSequence.GetData()

            Dim taPuddleText As New Database.Dictionary.DictionaryDataSetTableAdapters.PuddleTextTableAdapter
            allCachedPuddleTextDataTable = taPuddleText.GetData()
            cacheLoaded = True
        End If
        If Not idDictionary = 0 Then
            'Dim DTSignInfo As Database.Dictionary.DictionaryDataSet.DictionaryDataTable = GetSignDT(idDictionary)
            Dim dtDictionary As Database.Dictionary.DictionaryDataSet.DictionaryDataTable = GetDataDictionaryByIdCached(allCachedDictionaryDataTable, idDictionary)

            'Skip if none found
            If dtDictionary.Count >= 1 Then
                sign = New SwSign
                sign.BkColor = Color.FromArgb(dtDictionary(0).bkColor)
                Dim dtDictionaryGloss As Database.Dictionary.DictionaryDataSet.DictionaryGlossDataTable = GetDataDictionaryGlossCached(allCachedDictionaryGlossDataTable, idDictionary, FirstGlossLanguage)

                If dtDictionaryGloss.Count > 0 Then
                    sign.Gloss = dtDictionaryGloss(0).gloss
                    sign.Glosses = dtDictionaryGloss(0).glosses
                    sign.SetlanguageIso(My.Settings.FirstGlossLanguage)
                End If
                If dtDictionary.Count > 0 Then
                    sign.SetSignLanguageIso(CInt(dtDictionary(0).IDSignLanguage))
                    sign.Created = dtDictionary(0).Created
                    sign.LastModified = dtDictionary(0).LastModified
                    sign.PuddleNext = dtDictionary(0).PuddleNext
                    sign.PuddleNext = dtDictionary(0).PuddleNext
                    sign.PuddlePng = dtDictionary(0).PuddlePNG
                    sign.PuddlePrev = dtDictionary(0).PuddlePrev
                    sign.PuddleSvg = dtDictionary(0).PuddleSVG
                    sign.PuddleVideoLink = dtDictionary(0).PuddleVideoLink
                    sign.SignPuddleUser = dtDictionary(0).SignPuddleUser
                    sign.SWritingSource = dtDictionary(0).SWritingSource
                    sign.SignPuddleId = dtDictionary(0).IDSignPuddle
                    sign.SignWriterGuid = dtDictionary(0).GUID
                    sign.IsPrivate = dtDictionary(0).isPrivate
                End If
                Dim dtFrameInfo As Database.Dictionary.DictionaryDataSet.FrameDataTable = GetDataFrameCached(allCachedFrameDataTable, dtDictionary(0).IDDictionary)
                Dim frameRow As Database.Dictionary.DictionaryDataSet.FrameRow

                For Each frameRow In dtFrameInfo.Rows
                    If frameRow.FrameIndex > sign.Frames.Count - 1 Then
                        sign.Frames.Add(New SWFrame)
                    End If
                    If Not frameRow.FrameIndex > sign.Frames.Count - 1 Then
                        sign.Frames(frameRow.FrameIndex).MinHeight = frameRow.MinHeight
                        sign.Frames(frameRow.FrameIndex).MinWidth = frameRow.MinWidth
                    Else
                        Const mbo As MessageBoxOptions = MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign
                        MessageBox.Show("Error loading Frame from Dictionary", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, mbo, False)
                    End If
                    'Add Symbols
                    Dim dtSymbolsInfo As Database.Dictionary.DictionaryDataSet.SignSymbolsDataTable = GetSymbolsDtCached(allCachedSignSymbolsDatatable, frameRow.IDFrame)
                    Dim symbolRow As Database.Dictionary.DictionaryDataSet.SignSymbolsRow
                    For Each symbolRow In dtSymbolsInfo.Rows
                        Dim symbol As New SWSignSymbol
                        symbol.Code = symbolRow.code
                        Try
                            symbol.Hand = symbolRow.hand
                        Catch ex As StrongTypingException
                            symbol.Hand = 0
                        End Try

                        symbol.Handcolor = symbolRow.handcolor
                        symbol.Palmcolor = symbolRow.palmcolor
                        symbol.Size = symbolRow.size
                        symbol.X = symbolRow.x
                        symbol.Y = symbolRow.y
                        symbol.Z = symbolRow.z
                        'Symbol.Update()
                        sign.Frames(frameRow.FrameIndex).SignSymbols.Add(symbol.Clone)
                    Next

                    'Add Sequence
                    Dim dtSequenceInfo As Database.Dictionary.DictionaryDataSet.SignSequenceDataTable = GetSequenceDtCached(allCachedSignSequenceDataTable, frameRow.IDFrame)
                    Dim sequenceRow As Database.Dictionary.DictionaryDataSet.SignSequenceRow
                    For Each sequenceRow In dtSequenceInfo.Rows
                        Dim sequence As New SWSequence(sequenceRow.code, sequenceRow.rank)
                        sign.Frames(frameRow.FrameIndex).Sequences.Add(CType(sequence.Clone, SWSequence))
                    Next
                    'Add PuddleText
                    Dim dtPuddleText As Database.Dictionary.DictionaryDataSet.PuddleTextDataTable = GetPuddleTextDtCached(allCachedPuddleTextDataTable, dtDictionary(0).IDDictionary)
                    Dim puddleTextRow As Database.Dictionary.DictionaryDataSet.PuddleTextRow
                    For Each puddleTextRow In dtPuddleText.Rows
                        sign.PuddleText.Add(puddleTextRow.EntryText)
                    Next
                Next
            Else
                sign = Nothing
            End If
        Else
            sign = Nothing
        End If
        If idDictionary = Long.MaxValue Then
            cacheLoaded = False
            allCachedDictionaryDataTable = Nothing
            allCachedDictionaryGlossDataTable = Nothing
            allCachedFrameDataTable = Nothing
            allCachedSignSymbolsDatatable = Nothing
            allCachedSignSequenceDataTable = Nothing
            allCachedPuddleTextDataTable = Nothing
        End If
        Return sign
    End Function
    'Public Function GetidSWSign(ByVal idDictionary As Integer) As Integer
    '    Try
    '        Return CInt(TASWSign.GetIDFromIDDictionary(idDictionary))
    '    Catch
    '        Return 0
    '    End Try
    'End Function
    Private Sub DeleteSymbol(ByVal idSWFrame As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        _taswSignSymbol.AssignConnection(conn, trans)
        _taswSignSymbol.DeletebyIDFrame(idSWFrame)
    End Sub
    Private Sub DeleteSequence(ByVal idSWFrame As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        _taswSignSequence.AssignConnection(conn, trans)
        _taswSignSequence.DeletebyFrame(idSWFrame)
    End Sub
    Public Sub DeleteFrames(ByVal idDictionary As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        _taswSignSymbol.AssignConnection(conn, trans)
        _taswFrame.DeletebyIDDictionary(idDictionary)
    End Sub

    Private Sub EmptySWSign(ByVal idDictionary As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        'Get Current idSWSign
        'Dim idSWSign As Integer '= GetidSWSign(idDictionary)
        'Get idSWFrame
        Dim dtFrameInfo As Database.Dictionary.DictionaryDataSet.FrameDataTable = GetFrameDt(idDictionary, conn, trans)
        Dim frameRow As Database.Dictionary.DictionaryDataSet.FrameRow
        For Each frameRow In dtFrameInfo.Rows
            Dim idSWFrame As Long = frameRow.IDFrame
            'Delete Symbols
            DeleteSymbol(idSWFrame, conn, trans)
            'Delete Sequences
            DeleteSequence(idSWFrame, conn, trans)
        Next
        'Delete Frames
        DeleteFrames(idDictionary, conn, trans)
    End Sub

    Private Sub InsertSWFrame(ByVal idSWSign As Long, ByVal frame As SWFrame, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        'TODO Future EnterSign Bounds
        _taswFrame.AssignConnection(conn, trans)
        _taswFrame.InsertQuery(idSWSign, frame.FrameZ, frame.Bounds.Left, frame.Bounds.Top, frame.Bounds.Width, frame.Bounds.Height)
    End Sub
    Private Function GetFrameId(ByVal idDictionary As Long, ByVal frameIndex As Long, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction) As Long
        _taswFrame.AssignConnection(conn, trans)
        Dim idSWFrame As Nullable(Of Integer) = CInt(_taswFrame.GetFrameID(idDictionary, CType(frameIndex, Integer?)))

        If idSWFrame.HasValue Then
            Return CInt(idSWFrame)
        Else
            Return 0
        End If
    End Function
    Public Sub InsertSWSymbol(ByVal idSWFrame As Long, ByVal symbol As SWSignSymbol, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        _taswSignSymbol.AssignConnection(conn, trans)
        _taswSignSymbol.InsertQuery(idSWFrame, symbol.SymbolDetails.Code, symbol.X, symbol.Y, symbol.Z, CShort(symbol.Hand), symbol.Handcolor, symbol.Palmcolor, symbol.Size)
    End Sub
    Private Sub InsertSWSequence(ByVal idSWFrame As Long, ByVal sequence As SWSequence, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        _taswSignSequence.AssignConnection(conn, trans)
        _taswSignSequence.InsertQuery(idSWFrame, sequence.Code, sequence.Rank)
    End Sub
    Public Sub SaveSWSign(ByVal idDictionary As Long, ByVal sign As SwSign)
        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)
        Try
            SaveSWSign(idDictionary, sign, conn, trans)
            trans.Commit()
        Catch ex As Exception
            LogError(ex, "")
            MessageBox.Show(ex.ToString)
            If trans IsNot Nothing Then trans.Rollback()
        Finally
            conn.Close()

        End Try
    End Sub
    Public Sub SaveSWSign(ByVal idDictionary As Long, ByVal sign As SwSign, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)


        EmptySWSign(idDictionary, conn, trans)
        'Save SWSign
        'InserSWSign(idDictionary, sign)
        'Get Current idSWSign
        'Dim idSWSign As Integer '= GetidSWSign(idDictionary)

        If sign.SortString Is Nothing Then sign.SortString = "" 'Cannot save null value to SortString

        UpdateSortString(idDictionary, sign.SortString, conn, trans)

        'Save Frames
        Dim frame As SWFrame
        For Each frame In sign.Frames
            InsertSWFrame(idDictionary, frame, conn, trans)
            'Get idSWFrame
            Dim idSWFrame = GetFrameId(idDictionary, frame.FrameZ, conn, trans)
            'Save Symbols
            Dim symbol As SWSignSymbol
            For Each symbol In frame.SignSymbols
                InsertSWSymbol(idSWFrame, symbol, conn, trans)
            Next

            'Save Sequences
            Dim sequence As SWSequence
            For Each sequence In frame.Sequences
                InsertSWSequence(idSWFrame, sequence, conn, trans)
            Next

        Next
    End Sub

    Private Sub UpdateSortString(ByVal idDictionary As Long, ByVal sortString As String, ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction)
        _taDictionary.AssignConnection(conn, trans)
        _taDictionary.UpdateSorting(sortString, idDictionary)
    End Sub
    'Friend Sub SaveSWSign(ByVal sign As SWSign)
    '    SaveSWSign(Sign, False)
    'End Sub
    Public Sub SaveSWSign(ByVal sign As SwSign)
        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)
        Try
            SaveSWSign(sign, conn, trans)
            trans.Commit()

        Catch ex As Exception
            LogError(ex, "")
            MessageBox.Show(ex.ToString)
            If trans IsNot Nothing Then trans.Rollback()
        Finally
            conn.Close()

        End Try
    End Sub
    Public Sub SaveSWSign(ByVal sign As SwSign, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)
        Dim idDictionary As Integer = InsertDictionaryEntry(sign, True, conn, trans)
        EmptySWSign(idDictionary, conn, trans)
        'Save SWSign
        'InserSWSign(IDDictionary, sign)
        'Get Current idSWSign
        'Dim idSWSign As Integer = GetidSWSign(IDDictionary)

        'Save Frames
        Dim frame As SWFrame
        For Each frame In sign.Frames
            InsertSWFrame(idDictionary, frame, conn, trans)
            'Get idSWFrame
            Dim idSWFrame = GetFrameId(idDictionary, frame.FrameZ, conn, trans)
            'Save Symbols
            Dim symbol As SWSignSymbol
            For Each symbol In frame.SignSymbols
                InsertSWSymbol(idSWFrame, symbol, conn, trans)
            Next

            'Save Sequences
            Dim sequence As SWSequence
            For Each sequence In frame.Sequences
                InsertSWSequence(idSWFrame, sequence, conn, trans)
            Next

        Next
    End Sub
    Public Function SWSignsbyGlossesUnilingual(ByVal searchStr As String) As Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D begin
        Return _taSignsbyGlossesUnilingual.GetData(FirstGlossLanguage, DefaultSignLanguage, searchStr)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D end
    End Function
    Public Function SWSignsbyGlossesBilingual(ByVal searchStr As String) As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D begin
        Return _taSignsbyGlossesBilingual.GetData(FirstGlossLanguage, SecondGlossLanguage, DefaultSignLanguage, searchStr)
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D end
    End Function
    Public Function SignLanguagesTable() As DataTable
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D begin
        Return _tauiSignLanguages.GetDataBySignLanguages()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D end
    End Function
    Public Function LanguagesTable() As DataTable
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D begin
        Return _tauiCultures.GetDataCulture()
        ' section 127-0-0-1--1e49af91:11b4e3ad262:-8000:000000000000088D end
    End Function
    Private Sub UpdateDataSources(ByVal searchWord As String)
        Dim dt As DataTable = GetDictionaryEntries(searchWord)


        'AddTags(dt)
        'SetTags(dt)
        SetBindingSources(dt)
    End Sub
    Private Function UpdateDataSources(ByVal searchWord As String, ByVal pageSize As Integer, ByVal skip As Integer, ByVal count As Boolean, ByVal tagFilterValues As TagFilterValues) As Integer

        Dim totalRowCount As Integer
        If count Then
            totalRowCount = GetDictionaryEntriesCount(searchWord, tagFilterValues)
        End If
        Dim dt As DataTable = GetDictionaryEntriesPaging(searchWord, tagFilterValues, pageSize, skip)

        SetBindingSources(dt)
        Return totalRowCount
    End Function

    Private Sub SetTags(ByVal dt As DataTable)
        Dim entryIds = GetEntryIds(dt)
        Dim tagDictionaryEntries = GetTagEntries(entryIds.ConvertAll(Function(id) id.ToString()))

        SetTagDictionaryEntries(dt, tagDictionaryEntries)
        dt.AcceptChanges()
    End Sub
    Private Sub SetTagDictionaryEntries(ByVal dt As DataTable, ByVal tagDictionaryEntries As List(Of ExpandoObject))

        Dim groups = GetTagDictionaryGroups(tagDictionaryEntries)

        For Each groupItem In groups
            Dim row As DataRow = dt.Rows.Find(groupItem.Key)
            If row IsNot Nothing Then
                AddTagToRow(row, groupItem.Value)
            End If


        Next

    End Sub

    Private Shared Function GetTagDictionaryGroups(ByVal entries As List(Of ExpandoObject)) As Dictionary(Of Long, List(Of Guid))
        Dim entries2 = entries.ConvertAll(Function(x) TryCast(x, IDictionary(Of String, Object)))
        Dim entries3 = entries2.Select(Function(x) New With {Key .IdTagDictionary = x.Item("IdTagDictionary"), .IDDictionary = x.Item("IDDictionary"), .IdTag = x.Item("IdTag")})
        Dim grouped = New Dictionary(Of Long, List(Of Guid))
        For Each anonymous In entries3
            If Not grouped.ContainsKey(CType(anonymous.IDDictionary, Long)) Then
                grouped.Add(CType(anonymous.IDDictionary, Long), New List(Of Guid)())
            End If
            Dim list As List(Of Guid)
            Dim result = grouped.TryGetValue(CType(anonymous.IDDictionary, Long), list)

            list.Add(CType(anonymous.IdTag, Guid))


        Next

        Return grouped
    End Function

    Private Sub AddTagToRow(ByVal row As DataRow, ByVal value As List(Of Guid))
        Dim tags = value.ConvertAll(Function(x) x.ToString()).ToList()
        row("Tags") = tags
        row("OriginalTags") = tags.ToList()
    End Sub
    Public Sub TopSigns(ByVal top As Integer)
        Dim dt As DataTable = GetTopSigns(top)
        'AddTags(dt)
        'SetTags(dt)
        SetBindingSources(dt)
        'If DT.Rows.Count = 0 Then
        '    Dim MBO As MessageBoxOptions = CType(MessageBoxOptions.RtlReading And MessageBoxOptions.RightAlign, MessageBoxOptions)
        '    MessageBox.Show("There are no matches for your criteria.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MBO, False)
        'End If
    End Sub

    Private Function GetTopSigns(ByVal top As Integer) As DataTable
        Dim dt As DataTable

        Dim lang1Id As Integer = FirstGlossLanguage
        Dim lang2Id As Integer = SecondGlossLanguage
        Dim slid As Integer = DefaultSignLanguage

        If lang1Id = 0 Then
            lang1Id = 54
        End If
        If lang2Id = 0 Then
            lang2Id = 157
        End If
        If slid = 0 Then
            slid = 4
        End If
        If BilingualMode Then
            Dim dtBilingual As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
            _taSignsbyGlossesBilingual.FillByTop(dtBilingual, slid, lang1Id, lang2Id, top)
            dt = dtBilingual
        Else
            Dim dtUnilingual As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable
            _taSignsbyGlossesUnilingual.FillByTop(dtUnilingual, slid, lang1Id, top)
            dt = dtUnilingual
        End If
        Return dt
    End Function

    Public Sub AllSigns()
        Dim dt As DataTable = GetAllSigns()
        'AddTags(dt)
        'SetTags(dt)
        SetBindingSources(dt)

    End Sub

    Private Function GetAllSigns() As DataTable
        Dim dt As DataTable

        Dim lang1Id As Integer = GetLang1Id()
        Dim lang2Id As Integer = GetLang2Id()
        Dim slid As Integer = GetSlid()


        If BilingualMode Then
            dt = GetAllSignsBilingualDt(lang1Id, lang2Id, slid)
        Else
            dt = GetAllSignsUnilingualDt(lang1Id, slid)
        End If
        Return dt
    End Function

    Private Function GetLang2Id() As Integer

        Dim lang2Id As Integer = SecondGlossLanguage
        If lang2Id = 0 Then
            lang2Id = 157
        End If
        Return lang2Id
    End Function

    Private Function GetSlid() As Integer

        Dim slid As Integer = DefaultSignLanguage
        If slid = 0 Then
            slid = 4
        End If
        Return slid
    End Function

    Private Function GetLang1Id() As Integer

        Dim lang1Id As Integer = FirstGlossLanguage



        If lang1Id = 0 Then
            lang1Id = 54
        End If
        Return lang1Id
    End Function

    Private Function GetAllSignsBilingualDt(ByVal lang1Id As Integer, ByVal lang2Id As Integer, ByVal slid As Integer) As DataTable
        Dim dt As DataTable

        Dim dtBilingual As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
        _taSignsbyGlossesBilingual.FillByAll(dtBilingual, slid, lang1Id, lang2Id)
        dt = dtBilingual
        Return dt
    End Function

    Public Function GetAllSignsUnilingualDt() As DataTable
        Dim lang1Id As Integer = GetLang1Id()
        Dim slid As Integer = GetSlid()

        Dim dt = GetAllSignsUnilingualDt(lang1Id, slid)
        Return dt
    End Function
    Private Function GetAllSignsUnilingualDt(ByVal lang1Id As Integer, ByVal slid As Integer) As DataTable
        Dim dt As DataTable

        Dim dtUnilingual As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable
        _taSignsbyGlossesUnilingual.FillByAll(dtUnilingual, slid, lang1Id)
        dt = dtUnilingual
        Return dt
    End Function

    Private Sub SetBindingSources(dt As DataTable)


        DictionaryBindingSource1.RaiseListChangedEvents = False
        DictionaryBindingSource1.DataSource = dt
        DictionaryBindingSource1.RaiseListChangedEvents = True


        If BilingualMode Then
            DictionaryBindingSource2.RaiseListChangedEvents = False
            DictionaryBindingSource2.DataSource = dt.Copy
            DictionaryBindingSource2.Sort = "gloss2"
            DictionaryBindingSource2.RaiseListChangedEvents = True
            DictionaryBindingSource2.ResetBindings(False)
        Else
            DictionaryBindingSource2.DataSource = BlankDictionaryTable()
        End If

        DictionaryBindingSource1.ResetBindings(True)
    End Sub

    Private Function AddTags(ByVal dataTable As DataTable) As DataTable

        Dim dc1 = New DataColumn("Tags", GetType(List(Of String)))
        Dim dc2 = New DataColumn("OriginalTags", GetType(List(Of String)))
        dataTable.Columns.Add(dc1)
        dataTable.Columns.Add(dc2)

        Return dataTable
    End Function

    Public Sub UpdateDataSources(ByVal dt As DataTable)
        If dt Is Nothing Then
            Throw New ArgumentNullException("dt")
        End If
        SetBindingSources(dt)
    End Sub
    Private Sub UpdateDataSources(ByVal idDictionary As Integer)
        Dim dt As DataTable = GetDictionaryEntries(idDictionary)
        SetBindingSources(dt)
    End Sub

    Private Function GetDictionaryEntriesCount(ByVal searchWord As String, ByVal tagFilterValues As TagFilterValues) As Integer
        Dim filter = (tagFilterValues IsNot Nothing) AndAlso tagFilterValues.Filter
        Dim allExcept = (tagFilterValues IsNot Nothing) AndAlso tagFilterValues.AllExcept
        Dim tags = If(tagFilterValues Is Nothing, New List(Of String), tagFilterValues.Tags)

        If BilingualMode Then
            Return DbTags.SignsbyGlosses.SignsbyGlossesBilingual.Count(DictionaryConnectionString, DefaultSignLanguage, FirstGlossLanguage, SecondGlossLanguage, searchWord, filter, allExcept, tags)
        Else
            Return DbTags.SignsbyGlosses.SignsByGlossesUnilingual.Count(DictionaryConnectionString, DefaultSignLanguage, FirstGlossLanguage, searchWord, filter, allExcept, tags)
        End If
    End Function

    Public Function GetDictionaryEntriesPaging(ByVal searchWord As String, ByVal tagFilterValues As TagFilterValues, ByVal pageSize As Integer, ByVal skip As Integer) As DataTable
        Dim filter = (tagFilterValues IsNot Nothing) AndAlso tagFilterValues.Filter
        Dim allExcept = (tagFilterValues IsNot Nothing) AndAlso tagFilterValues.AllExcept
        Dim tags = If(tagFilterValues Is Nothing, New List(Of String), tagFilterValues.Tags)

        If BilingualMode Then
            Return ConvertoSignsbyGlossesBilingualDataTable(DbTags.SignsbyGlosses.SignsbyGlossesBilingual.GetPage(DictionaryConnectionString, DefaultSignLanguage, FirstGlossLanguage, SecondGlossLanguage, searchWord, pageSize, skip, filter, allExcept, tags))
        Else
            Return ConvertoSignsbyGlossesBilingualDataTable(DbTags.SignsbyGlosses.SignsByGlossesUnilingual.GetPage(DictionaryConnectionString, DefaultSignLanguage, FirstGlossLanguage, searchWord, pageSize, skip, filter, allExcept, tags))
        End If
    End Function

    Private Shared Function ConvertoSignsbyGlossesBilingualDataTable(ByVal eoList As IEnumerable(Of ExpandoObject)) As DictionaryDataGridTable
        Dim table = New DictionaryDataGridTable()

        For Each eo As ExpandoObject In eoList
            Dim dict = TryCast(eo, IDictionary(Of String, Object))
            Dim row = table.NewRow()

            row.Item("gloss1") = dict.Item("gloss1")
            row.Item("glosses1") = dict.Item("glosses1")
            row.Item("IDDictionaryGloss1") = dict.Item("IDDictionaryGloss1")
            row.Item("Culture1") = dict.Item("Culture1")
            row.Item("IDDictionary") = dict.Item("IDDictionary")
            row.Item("IDSignLanguage") = dict.Item("IDSignLanguage")
            row.Item("IDSignPuddle") = dict.Item("IDSignPuddle")
            row.Item("isPrivate") = dict.Item("isPrivate")
            row.Item("SWriting") = dict.Item("SWriting")
            row.Item("Photo") = dict.Item("Photo")
            row.Item("Sign") = dict.Item("Sign")
            row.Item("SWritingSource") = dict.Item("SWritingSource")
            row.Item("PhotoSource") = dict.Item("PhotoSource")
            row.Item("SignSource") = dict.Item("SignSource")
            row.Item("GUID") = dict.Item("GUID")
            row.Item("LastModified") = dict.Item("LastModified")
            row.Item("Sorting") = dict.Item("Sorting")
            Dim tags = StringToList(dict.Item("Tags"))
            If (tags IsNot Nothing) Then
                row.Item("Tags") = tags
                row.Item("OriginalTags") = tags.ToList()
            End If


            If dict.ContainsKey("gloss2") Then
                row.Item("gloss2") = dict.Item("gloss2")
                row.Item("glosses2") = dict.Item("glosses2")
                row.Item("IDDictionaryGloss2") = dict.Item("IDDictionaryGloss2")
                row.Item("Culture2") = dict.Item("Culture2")
            End If

            If Not table.Rows.Contains(row.Item("IDDictionary")) Then
                table.Rows.Add(row)
            End If

        Next

        table.AcceptChanges()
        Return table
    End Function
    Public Shared Function StringToList(value As Object) As List(Of String)
        Dim str = TryCast(value, [String])
        Dim lst = TryCast(value, List(Of String))

        If str IsNot Nothing Then
            Return ValueToList(str)
        Else
            Return lst
        End If
    End Function
    Private Shared Function ValueToList(value As String) As List(Of String)
        If Not String.IsNullOrEmpty(value) Then
            Return value.Split(","c).[Select](Function(x) x.Trim()).ToList()
        Else
            Return Nothing
        End If
    End Function

    Private Function GetDictionaryEntries(ByVal searchWord As String) As DataTable
        'Try
        Dim lang1Id As Integer = FirstGlossLanguage
        Dim lang2Id As Integer = SecondGlossLanguage
        Dim slid As Integer = DefaultSignLanguage

        If lang1Id = 0 Then
            lang1Id = 54
        End If
        If lang2Id = 0 Then
            lang2Id = 157
        End If
        If slid = 0 Then
            slid = 4
        End If


        If BilingualMode Then
            Dim dtBilingual As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
            _taSignsbyGlossesBilingual.Fillby(dtBilingual, slid, lang1Id, lang2Id, searchWord)
            Return dtBilingual
        Else
            Dim dtUnilingual As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable
            _taSignsbyGlossesUnilingual.Fillby(dtUnilingual, slid, lang1Id, searchWord)
            Return dtUnilingual
        End If
    End Function
    Private Function GetDictionaryEntries(ByVal idDictionary As Integer) As DataTable
        'Try
        Dim lang1Id As Integer = FirstGlossLanguage
        Dim lang2Id As Integer = SecondGlossLanguage
        Dim slid As Integer = DefaultSignLanguage

        If lang1Id = 0 Then
            lang1Id = 54
        End If
        If lang2Id = 0 Then
            lang2Id = 157
        End If
        If slid = 0 Then
            slid = 4
        End If
        Dim dt As New Database.Dictionary.DictionaryDataSet.SignsBilingualDataTable
        _taSignsBilingual.Fill(dt, idDictionary, lang1Id, lang2Id, slid)
        Return dt
    End Function
    Public Shared Function BlankDictionaryTable() As DataTable
        Return New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
    End Function
    Public Sub DuplicateDictionaryEntryTranslations(ByVal dictionaryOriginalId As Integer, ByVal dictionaryDuplicatedId As Integer, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)

        Dim dt As Database.Dictionary.DictionaryDataSet.DictionaryGlossDataTable
        Dim result As Integer
        _taDictionaryGloss.AssignConnection(conn, trans)
        dt = _taDictionaryGloss.GetDataByDictionaryEntry(dictionaryOriginalId)
        For Each row As Database.Dictionary.DictionaryDataSet.DictionaryGlossRow In dt.Rows
            result = _taDictionaryGloss.InsertQuery(dictionaryDuplicatedId, row.IDCulture, row.gloss & " Copy ", row.glosses)
            If result = 1 Then
                'Success
            Else
                'Not success
            End If
        Next
    End Sub
    Public Sub UpdateDictionaryEntries(ByVal dictionaryDataTable As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable, ByVal lang1 As Integer, ByVal lang2 As Integer, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction)

        If dictionaryDataTable IsNot Nothing Then
            Dim deletedDictionaryView As DataTable = dictionaryDataTable.GetChanges(DataRowState.Deleted)

            Dim newDictionaryView As DataTable = dictionaryDataTable.GetChanges(DataRowState.Added)

            Dim modifiedDictionaryView As DataTable = dictionaryDataTable.GetChanges(DataRowState.Modified)
            'Try
            ' Remove all deleted rows.
            If deletedDictionaryView IsNot Nothing Then
                Dim deletedEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow

                For Each deletedEntry In deletedDictionaryView.Rows
                    deletedEntry.RejectChanges() 'See what the IDDictionary was
                    DeleteSign(deletedEntry.IDDictionary, conn, trans)
                    deletedEntry.Delete() 'Redelete entry
                Next
            End If

            ' Add new rows.
            If newDictionaryView IsNot Nothing Then
                Dim newEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow
                For Each newEntry In newDictionaryView.Rows
                    Dim id As Integer = AddSign(newEntry, lang1, lang2, conn, trans)
                    UpdateId(dictionaryDataTable, id, newEntry.GUID)
                Next
            End If

            ' Update all rows.
            If modifiedDictionaryView IsNot Nothing Then
                Dim modifiedEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow

                For Each modifiedEntry In modifiedDictionaryView.Rows
                    ModifySign(modifiedEntry, lang1, lang2, conn, trans)
                Next
            End If

            dictionaryDataTable.AcceptChanges()
            'Catch ex As Exception
            '    monitor.TrackException(ex, _
            '                      TraceEventType.Error.ToString, _
            '                      "Saving Dictionary Entries, UpdateDictionaryEntries")
            '    My.Application.Log.WriteException(ex, _
            '                      TraceEventType.Error.ToString, _
            '                      "Saving Dictionary Entries, UpdateDictionaryEntries")

            'Finally
            If Not deletedDictionaryView Is Nothing Then
                deletedDictionaryView.Dispose()
            End If
            If Not newDictionaryView Is Nothing Then
                newDictionaryView.Dispose()
            End If
            If Not modifiedDictionaryView Is Nothing Then
                modifiedDictionaryView.Dispose()
            End If
            'End Try
        End If
    End Sub
    Private Shared Sub UpdateId(ByRef dictionaryDataTable As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable, ByVal id As Integer, ByVal guid As Guid)

        Dim view As DataView = dictionaryDataTable.DefaultView
        view.RowFilter = "GUID='" & guid.ToString & "'"
        If view.Count > 0 Then
            view(0).Item("IDDictionary") = id
        End If

    End Sub

    Public Function InsertDuplicatedDictionaryEntry(ByVal newEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction) As Integer
        Dim newGuid As Guid

        Dim toInsert As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow = newEntry

        newGuid = Guid.NewGuid()


        Dim result As Integer
        _taDictionary.AssignConnection(conn, trans)
        result = _taDictionary.InsertQuery(toInsert.IDSignLanguage, toInsert.isPrivate, -1, toInsert.SWriting, toInsert.Photo, toInsert.Sign, toInsert.SWritingSource, toInsert.PhotoSource, toInsert.SignSource, newGuid, Date.UtcNow, Date.UtcNow, toInsert.IDSignPuddle, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        If result = 1 Then
            'Success
        Else
            'Errr
            'MsgBox("Please fix this error")
        End If


        Return CInt(_taDictionary.GetIDbyGUID(newGuid))
    End Function
    Public Function InsertDuplicatedDictionaryEntry(ByVal newEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualRow, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction) As Integer
        Dim newGuid As Guid
        Dim toInsert As Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualRow = newEntry

        newGuid = Guid.NewGuid()


        Dim result As Integer
        _taDictionary.AssignConnection(conn, trans)
        result = _taDictionary.InsertQuery(toInsert.IDSignLanguage, toInsert.isPrivate, -1, toInsert.SWriting, toInsert.Photo, toInsert.Sign, toInsert.SWritingSource, toInsert.PhotoSource, toInsert.SignSource, newGuid, Date.UtcNow, Date.UtcNow, toInsert.IDSignPuddle, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        If result = 1 Then
            'Success
        Else
            'Errr
            'MsgBox("Please fix this error")
        End If


        Return CInt(_taDictionary.GetIDbyGUID(newGuid))
    End Function
    Public Function InsertDictionaryEntry(ByVal newEntry As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction) As Integer
        Dim newGuid As Guid
        Dim toInsert As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow = newEntry

        If IsDbNull(toInsert.GUID) Then
            newGuid = Guid.NewGuid()
            toInsert.GUID = newGuid
        Else
            newGuid = toInsert.GUID
        End If


        Dim result As Integer
        _taDictionary.AssignConnection(conn, trans)

        Dim isPrivate = Not IsDbNull(toInsert.isPrivate) AndAlso toInsert.isPrivate

        result = _taDictionary.InsertQuery(toInsert.IDSignLanguage, isPrivate, -1, toInsert.SWriting, toInsert.Photo, toInsert.Sign, toInsert.SWritingSource, toInsert.PhotoSource, toInsert.SignSource, newGuid, Date.UtcNow, Date.UtcNow, toInsert.IDSignPuddle, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)
        If result = 1 Then
            'Success
        Else
            'Errr
            'MsgBox("Please fix this error")
        End If


        Return CInt(_taDictionary.GetIDbyGUID(newGuid))
    End Function
    Public Function InsertDictionaryEntry(ByVal sign As SwSign, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction) As Integer
        Return InsertDictionaryEntry(sign, False, conn, trans)
    End Function
    Public Function InsertDictionaryEntry(ByVal sign As SwSign, ByVal toBw As Boolean, ByRef conn As SQLiteConnection, ByRef trans As SQLiteTransaction) As Integer
        Dim newId As Integer

        If Not sign.SignWriterGuid.HasValue Then
            sign.SignWriterGuid = Guid.NewGuid
        End If

        Dim byteArray As Byte()
        If toBw Then
            Dim img = sign.Render
            byteArray = ImageToByteArray(SWDrawing.ConvertBW(New Bitmap(img)), ImageFormat.Png)
            img.Dispose()
        Else
            Dim img = sign.Render
            byteArray = ImageToByteArray(img, ImageFormat.Png)
            img.Dispose()
        End If

        _taDictionary.AssignConnection(conn, trans)
        _taDictionary.InsertQuery(UI.Cultures.GetIdSignLanguages(sign.SignLanguageIso), False, -1, byteArray, Nothing, Nothing, String.Empty, String.Empty, String.Empty, sign.SignWriterGuid, sign.Created, sign.LastModified, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty)


        'If Result = 1 Then
        '    'Success
        'Else
        '    'Errr
        '    'MsgBox("Please fix this error")
        'End If
        newId = CInt(_taDictionary.GetIDbyGUID(sign.SignWriterGuid))

        Dim lang1 As Integer = UI.Cultures.GetCultureIDbyIso(sign.LanguageIso)
        'Language1
        _taDictionaryGloss.AssignConnection(conn, trans)
        _taDictionaryGloss.InsertQuery(newId, lang1, NZ(sign.Gloss, String.Empty).ToString, NZ(sign.Glosses, String.Empty).ToString)

        Return newId
    End Function

    Private _disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Private Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                'free unmanaged resources when explicitly called
                DictionaryBindingSource1.Dispose()
                DictionaryBindingSource2.Dispose()
                _taDictionarybyLanguages.Dispose()
                'TASWSign.Dispose()
                _taswFrame.Dispose()
                _taswSignSymbol.Dispose()
                _taswSignSequence.Dispose()
                _taSignsbyGlossesBilingual.Dispose()
                _taSignsbyGlossesUnilingual.Dispose()
                _taSignsbyGlossesBilingual.Dispose()
                _taSignsbyGlossesUnilingual.Dispose()
                _tauiSignLanguages.Dispose()
                _tauiCultures.Dispose()
                _taSignsBilingual.Dispose()
                _taDictionaryGloss.Dispose()
                _taDictionary.Dispose()
            End If

            'free shared unmanaged resources
        End If
        _disposedValue = True
    End Sub
    Public Function IsSignsbyGlossesBilingual(ByVal obj As Object) As Boolean
        Return TypeOf obj Is Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
    End Function
    Public Function IsSignsbyGlossesUnilingual(ByVal obj As Object) As Boolean
        Return TypeOf obj Is Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable
    End Function
    Public Function ConvertUnilingualDTtoBilingualDt(ByVal dt As Object) As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
        If IsSignsbyGlossesBilingual(dt) Then
            Return CType(dt, Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable)
        ElseIf IsSignsbyGlossesUnilingual(dt) Then
            Dim dtgb As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
            Dim dtgu As Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable = CType(dt, Database.Dictionary.DictionaryDataSet.SignsbyGlossesUnilingualDataTable)
            Dim nr As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow
            Dim deleted As Boolean
            For Each dr As DataRow In dtgu.Rows
                nr = CType(dtgb.NewRow(), Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualRow)

                If dr.RowState = DataRowState.Deleted Then
                    deleted = True
                    dr.RejectChanges()
                End If
                With dr
                    nr.IDSignLanguage = DefaultSignLanguage
                    nr.gloss1 = .Item("gloss1").ToString
                    nr.glosses1 = .Item("glosses1").ToString
                    nr.IDDictionary = CLng(.Item("IDDictionary"))
                    nr.GUID = CType(.Item("GUID"), Guid)
                    nr.isPrivate = CBool(NZ(.Item("isPrivate"), False))
                    nr.SWriting = CType(NZ(.Item("SWriting"), Nothing), Byte())
                    nr.Photo = CType(NZ(.Item("Photo"), Nothing), Byte())
                    nr.IDSignPuddle = .Item("IDSignPuddle").ToString
                    nr.Sign = CType(NZ(.Item("Sign"), Nothing), Byte())
                    nr.SWritingSource = .Item("SWritingSource").ToString
                    nr.PhotoSource = .Item("PhotoSource").ToString
                    nr.SignSource = .Item("SignSource").ToString
                    nr.LastModified = CDate(NZ(.Item("LastModified"), Nothing))
                    nr.Sorting = .Item("Sorting").ToString

                    dtgb.Rows.Add(nr)
                    nr.AcceptChanges()

                    If .RowState = DataRowState.Modified Then
                        nr.SetModified()
                    End If
                    If .RowState = DataRowState.Added Then
                        nr.SetAdded()
                    End If
                    If deleted Then
                        nr.Delete()
                    End If
                End With

            Next
            Return dtgb
        Else
            Throw New Exception("Cannot convert object of type " & dt.GetType.ToString & " to SignsbyGlossesBilingualDataTable")
        End If
    End Function
#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

    Function GetPhoto(idDictionary As Integer) As Image
        Dim img As Image
        If Not idDictionary = 0 Then
            Dim dtDictionary As Database.Dictionary.DictionaryDataSet.DictionaryDataTable = _taDictionary.GetDataByID(idDictionary)
            If dtDictionary.Count > 0 Then
                If dtDictionary(0).Photo IsNot Nothing Then
                    img = ByteArraytoImage(dtDictionary(0).Photo)
                End If
            End If

        Else
            img = Nothing
        End If

        Return img
    End Function

    Function GetSignPhoto(idDictionary As Integer) As Image
        Dim img As Image
        If Not idDictionary = 0 Then
            Dim dtDictionary As Database.Dictionary.DictionaryDataSet.DictionaryDataTable = _taDictionary.GetDataByID(idDictionary)
            If dtDictionary.Count > 0 Then
                img = ByteArraytoImage(dtDictionary(0).Sign)
            End If
        Else
            img = Nothing
        End If

        Return img
    End Function


    Public Sub CreateSortString()
        Dim dt = GetTopSigns(1)
        If dt.Rows.Count > 0 Then
            Dim value1 = dt.Rows(0)("Sorting")

            If String.IsNullOrEmpty(value1.ToString()) Then ResetAllSortStrings()
        End If
    End Sub

    Private Sub ResetAllSortStrings()
        Dim conn As SQLiteConnection = GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = GetNewDictionaryTransaction(conn)

        Dim dictionaryTa As New DictionaryTableAdapter
        dictionaryTa.AssignConnection(conn, trans)

        Using conn
            Dim dt As DictionaryDataTable = dictionaryTa.GetData()

            For Each row As DataRow In dt.Rows
                Dim id As Long
                id = row.Field(Of Long)("IDDictionary")
                'row("Sorting") = GetSortingString(conn, trans, id)
                dictionaryTa.UpdateSorting(GetSortingString(conn, trans, id), id)
            Next
            'dictionaryTa.Update(dt)
            trans.Commit()
            dictionaryTa.Dispose()
            conn.Close()
            conn.Dispose()
            trans.Dispose()
        End Using

    End Sub

    Private Function GetSortingString(ByVal conn As SQLiteConnection, ByVal trans As SQLiteTransaction, ByVal idDictionary As Long) As String
        Dim sign = GetSWSign(idDictionary, conn, trans)

        Dim sequence = sign.Frames.First.Sequences
        Return SequencetoSortingString(sequence)
    End Function

    Public Function SequencetoSortingString(ByVal sequences As IEnumerable(Of SWSequence)) As String
        Dim sb As New StringBuilder

        For Each sequence As SWSequence In sequences
            Dim sortWeight = GetSortWeight(sequence)
            sb.Append(sortWeight)
        Next
        Return sb.ToString()
    End Function

    Private Shared Function GetSortWeight(ByVal sequence As SWSequence) As String
        Dim symbol As New SWSignSymbol With {.Code = sequence.Code}
        Return symbol.SymbolDetails.SortWeight
    End Function


    Public Function GetTags() As List(Of ExpandoObject)
        Return DatabaseDictionary.GetTags()
    End Function

    Public Sub SaveTags(ByVal added As List(Of ExpandoObject), ByVal updated As List(Of ExpandoObject), ByVal removed As List(Of String))
        DatabaseDictionary.SaveTags(added, updated, removed)
    End Sub

    Public Function GetEntryIds(ByVal dt As DataTable) As List(Of Long)
        Dim ids = New List(Of Long)

        For Each row As DataRow In dt.Rows
            ids.Add(CType(row.Item("IDDictionary"), Long))
        Next


        Return ids
    End Function

    Public Function GetTagEntries(ByVal entryIds As List(Of String)) As List(Of ExpandoObject)
        Return DatabaseDictionary.GetTagEntries(entryIds)
    End Function

    Public Sub SaveTagDictionary(ByVal tagChanges As Tuple(Of List(Of List(Of String)), List(Of List(Of String))))
        DatabaseDictionary.SaveTagDictionary(tagChanges)
    End Sub


    Public Function GetSignbyId(ByVal connectionString As String, ByVal idDict As Integer) As Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable

        Dim dt As New Database.Dictionary.DictionaryDataSet.SignsbyGlossesBilingualDataTable
        Dim signsbyGlossesBilingualTableAdapter As New SignsbyGlossesBilingualTableAdapter()
        Dim strConection = CreateConnectionStringFromPath(connectionString)

        Using connection As New SQLiteConnection(strConection)
            Dim results = signsbyGlossesBilingualTableAdapter.GetDataByID(DefaultSignLanguage, idDict, FirstGlossLanguage, SecondGlossLanguage)

            For Each signsbyGlossesBilingualRow As SignsbyGlossesBilingualRow In results
                dt.ImportRow(signsbyGlossesBilingualRow)
            Next


            connection.Close()
        End Using

        Return dt
    End Function
End Class