Option Strict Off
Imports System.Dynamic
Imports SignWriterStudio.Database.Dictionary.DictionaryDataSetTableAdapters
Imports SignWriterStudio.General
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.SWClasses
Imports System.Data.SQLite
Imports System.Xml
Imports System.Text
Imports SPML
Imports System.ComponentModel
 

Public Class ImportSigns
    Dim WithEvents SPMLImportbw As BackgroundWorker ' With {.WorkerReportsProgress = True}
    Dim SWEditorProgressBar As Progress
    Dim _myDictionary As SWDict
    Private _importedSigns As Tuple(Of Integer, Integer, Integer)

    Public Sub New(dictionary As SWDict, progress As Progress)
        _myDictionary = dictionary
        SWEditorProgressBar = progress
    End Sub

    Public Sub ImportSPMLSigns(spmlfilename As String)
        Try
            Dim signs = GetsignsfromSPMLfile(spmlfilename, _myDictionary.DefaultSignLanguage, _myDictionary.FirstGlossLanguage)
            ImportSigns(signs)

        Catch ex As XmlException
            LogError(ex, "XML Exception " & ex.GetType().Name)

            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Public Sub ImportSWSSigns(swsfilename As String)
        Try
            Dim signs = GetsignsfromSwsfile(swsfilename, _myDictionary.DefaultSignLanguage, _myDictionary.FirstGlossLanguage)
            ImportSigns(signs)

        Catch ex As Exception
            LogError(ex, "Exception " & ex.GetType().Name)

            MessageBox.Show(ex.Message)
        End Try
    End Sub



    Private Sub ImportSigns(signs As List(Of SwSign))
        Dim classifiedSigns As Tuple(Of List(Of SwSign), List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)), List(Of SwSign))
        classifiedSigns = ClassifySigns(signs)

        Dim selectedSigns As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))
        selectedSigns = SelectComparedSigns(classifiedSigns.Item2)

        ShowProgressBar(classifiedSigns.Item1.Count, selectedSigns.Count, classifiedSigns.Item3.Count, 0, "SignWriter Studio™ Importing ...")

        UpdateSigns(selectedSigns)

        AddSigns(classifiedSigns.Item3)

    End Sub
    Private Function SelectedSignsToCollection(SelectedSigns As  _
                                                  List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))) _
        As List(Of SwSign)
        Dim Coll As New List(Of SwSign)
        For Each SelectedSign In SelectedSigns
            Coll.Add(SelectedSign.Item1)
        Next
        Return Coll
    End Function


    Private Sub SPMLImportbw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) ' Handles SPMLImportbw.DoWork
        Dim Args = CType(e.Argument, Tuple(Of List(Of SwSign), BackgroundWorker))

        _myDictionary.SignstoDictionary(Args.Item1, Args.Item2)
    End Sub


    Private Sub SPMLImportbw_ProgressChanged(ByVal sender As Object, ByVal e As ProgressChangedEventArgs) _
' Handles SPMLImportbw.ProgressChanged
        SWEditorProgressBar.ProgressBar1.Value = e.ProgressPercentage
    End Sub

    Private Sub SPMLImportbw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) _
'Handles SPMLImportbw.RunWorkerCompleted

        SWEditorProgressBar.Hide()
        SWEditorProgressBar.ProgressBar1.Value = 0
        Dim sb As New StringBuilder
        If Not _importedSigns.Item1 = 0 Then
            sb.AppendLine(_importedSigns.Item1 & " signs already up to date.")
        End If
        If Not _importedSigns.Item2 = 0 Then
            sb.AppendLine(_importedSigns.Item2 & " signs updated.")
        End If
        If Not _importedSigns.Item3 = 0 Then
            sb.AppendLine(_importedSigns.Item3 & " new signs added.")
        End If
        MessageBox.Show(sb.ToString)
        RemoveHandler SPMLImportbw.DoWork, AddressOf SPMLImportbw_DoWork
        RemoveHandler SPMLImportbw.RunWorkerCompleted, AddressOf SPMLImportbw_RunWorkerCompleted
        RemoveHandler SPMLImportbw.ProgressChanged, AddressOf SPMLImportbw_ProgressChanged
    End Sub

    Private Function ClassifySigns(ByVal Signs As List(Of SwSign), ByRef conn As SQLiteConnection,
                                 ByRef trans As SQLiteTransaction) _
      As  _
      Tuple _
          (Of List(Of SwSign), List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)), 
              List(Of SwSign))
        Dim signsToAdd As New List(Of SwSign)
        Dim signsNotModified As New List(Of SwSign)
        Dim signstoCompare As New List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))
        Static allCachedDictionaryDataTable As DataTable
        Dim taDictionary As New DictionaryTableAdapter
        taDictionary.AssignConnection(conn, trans)
        allCachedDictionaryDataTable = taDictionary.GetData()

        Dim foundbyGuid As DictionaryDataSet.DictionaryRow

        For Each Sign As SwSign In Signs
            If Sign.SignWriterGuid.HasValue Then

                foundbyGuid = DatabaseDictionary.GetDataDictionaryByGuid(allCachedDictionaryDataTable,
                                                                         Sign.SignWriterGuid)
                If foundbyGuid IsNot Nothing Then

                    If Not Date.Compare(Sign.LastModified, foundbyGuid.LastModified) = 0 Then
                        signstoCompare.Add(Tuple.Create(Sign, foundbyGuid, True))
                    Else
                        signsNotModified.Add(Sign)
                    End If

                Else
                    signsToAdd.Add(Sign)
                End If
            Else
                signsToAdd.Add(Sign)
            End If
        Next
        Return Tuple.Create(signsNotModified, signstoCompare, signsToAdd)
    End Function

    Private Function SelectComparedSigns(ByVal signsToCompare As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))) _
        As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))
        Dim signsToOverwrite As New List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow))
        Dim compareSigns As New CompareSigns(DictionaryConnectionString)
        If signsToCompare.Count > 0 Then

            compareSigns.SignsToCompare = signsToCompare
            compareSigns.ShowDialog()

            For Each Item In compareSigns.ListToShow
                If Item.OverwritefromPuddle Then
                    signsToOverwrite.Add(Tuple.Create(Item.puddleSign, Item.StudioDictRow))
                End If
            Next

        End If
        Return signsToOverwrite
    End Function


    Private Shared Function GetOnlyWithSequence(ByVal List As List(Of SwSign)) _
        As List(Of SwSign)
        Dim newSignList = New List(Of SwSign)
        For Each sign As SwSign In List
            If sign.Frames.First().Sequences.Count > 0 AndAlso sign.SWritingSource.ToLower().Contains("Val".ToLower()) _
                Then
                newSignList.Add(sign)
            End If
        Next
        Return newSignList
    End Function

    Private Shared Function GetOnlyWithTagSort(ByVal List As List(Of SwSign)) As List(Of SwSign)
        Dim newSignList = New List(Of SwSign)
        For Each sign As SwSign In List
            If _
                sign.Frames.First().Sequences.Count > 0 AndAlso sign.SWritingSource.ToLower().Contains("Tag".ToLower()) AndAlso
                sign.SWritingSource.ToLower().Contains("Sort".ToLower()) Then
                newSignList.Add(sign)
            End If
        Next
        Return newSignList
    End Function

    Private Function GetsignsfromSPMLfile(spmlfilename As String, signlanguage As Integer, glosslanguage As Integer) As List(Of SwSign)
        Dim signs As List(Of SwSign)
        Dim spmlConverter As New SpmlConverter
        signs = spmlConverter.ImportSPML(spmlfilename, signlanguage, glosslanguage)

        spmlConverter.CleanImportedSigns(signs)

        Return signs
    End Function
    Private Function GetsignsfromSwsfile(swsfilename As String, signlanguage As Integer, glosslanguage As Integer) As List(Of SwSign)
        Dim signs As List(Of SwSign)
        Dim dict = New SWDict("data source=""" & swsfilename & """")
        dict.FirstGlossLanguage = glosslanguage
        dict.DefaultSignLanguage = signlanguage

        Dim allsigns = dict.GetAllSignsUnilingualDt()

        signs = dict.ConvertUnilingualDttoSWSign(allsigns)
         
        Return signs
    End Function


    Private Sub AddSigns(signs As List(Of SwSign))
        Dim conn = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans = SWDict.GetNewDictionaryTransaction(conn)
        Try

            Using conn
                'Add new signs
                SPMLImportbw = New BackgroundWorker With {.WorkerReportsProgress = True}
                AddHandler SPMLImportbw.DoWork, AddressOf SPMLImportbw_DoWork
                AddHandler SPMLImportbw.RunWorkerCompleted, AddressOf SPMLImportbw_RunWorkerCompleted
                AddHandler SPMLImportbw.ProgressChanged, AddressOf SPMLImportbw_ProgressChanged
                SPMLImportbw.RunWorkerAsync(Tuple.Create(signs, SPMLImportbw))
                trans.Commit()
                conn.Close()
            End Using

        Catch ex As SQLiteException
            LogError(ex, "SQLite Exception " & ex.GetType().Name)

            MessageBox.Show(ex.ToString)
            If trans IsNot Nothing Then trans.Rollback()
        End Try
    End Sub

    Private Sub UpdateSigns(selectedSigns As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow)))
        Dim conn = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans = SWDict.GetNewDictionaryTransaction(conn)
        Try

            Using conn
                'Update current signs
                _myDictionary.DeleteSigns(selectedSigns, conn, trans)
                _myDictionary.SignstoDictionaryInsert(SelectedSignsToCollection(selectedSigns), conn, trans)
                trans.Commit()
                conn.Close()
            End Using
        Catch ex As SQLiteException
            LogError(ex, "SQLite Exception " & ex.GetType().Name)

            MessageBox.Show(ex.ToString)
            If trans IsNot Nothing Then trans.Rollback()
        End Try
    End Sub

    Private Function ClassifySigns(signs As List(Of SwSign)) As Tuple(Of List(Of SwSign), List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)), List(Of SwSign))
        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(DictionaryConnectionString)
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)
        Dim classifiedSigns As Tuple(Of List(Of SwSign), List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean)), List(Of SwSign))
        Try
            Using conn
                classifiedSigns = ClassifySigns(signs, conn, trans)
                If trans.Connection IsNot Nothing Then

                    trans.Commit()
                End If

                conn.Close()
            End Using
        Catch ex As SQLiteException
            LogError(ex, "SQLite Exception " & ex.GetType().Name)

            MessageBox.Show(ex.ToString)
            If trans IsNot Nothing Then trans.Rollback()
        End Try
        Return classifiedSigns
    End Function

    Private Sub ShowProgressBar(signsNotModifiedCount As Integer, selectedSignsCount As Integer, signsToAddCount As Integer, value As Integer, text As String)
        SWEditorProgressBar.ProgressBar1.Value = value
        SWEditorProgressBar.Text = text
        SWEditorProgressBar.Show()
        _importedSigns = Tuple.Create(signsNotModifiedCount, selectedSignsCount,
                                     signsToAddCount)
    End Sub

   

End Class


