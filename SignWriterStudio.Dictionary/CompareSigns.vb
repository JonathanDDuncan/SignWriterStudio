Imports System.ComponentModel
Imports SignWriterStudio.Settings
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.SWClasses
Imports System.Data.SQLite

Public Class CompareSigns
    Public Property SignsToCompare As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))
    Public ListToShow As New BindingList(Of CompareItem)

    Private Sub CompareSigns_Load(sender As Object, e As EventArgs) Handles Me.Load
        DataGridView1.AutoGenerateColumns = False
        Dim listtoShow1 As BindingList(Of CompareItem) = PrepareListtoShow(SignsToCompare)

        BindingSource1.DataSource = listtoShow1
        DataGridView1.DataSource = BindingSource1
        PuddleGloss.DataPropertyName = "PuddleGloss"
    End Sub

    'Public Property Trans As SQLiteTransaction
    'Public Property Conn As SQLiteConnection


    Private Function PrepareListtoShow(SignstoCompare As  _
                                          List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))) _
        As BindingList(Of CompareItem)

        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection()
        Dim trans As SQLiteTransaction = SWDict.GetNewDictionaryTransaction(conn)

        Using conn
            Try
                Dim CompareItem As CompareItem
                Dim StudioSign As SwSign
                For Each SigntoCompare In SignstoCompare
                    CompareItem = New CompareItem
                    CompareItem.puddleGloss = SigntoCompare.Item1.Gloss
                    CompareItem.puddleGlosses = SigntoCompare.Item1.Glosses

                    CompareItem.puddleImage = SigntoCompare.Item1.Render
                    CompareItem.puddleModified = SigntoCompare.Item1.LastModified
                    CompareItem.puddleSource = SigntoCompare.Item1.SWritingSource
                    CompareItem.puddleSign = SigntoCompare.Item1
                    CompareItem.OverwritefromPuddle = False

                    Dim SWDict As New SWDict
                    SWDict.DefaultSignLanguage = SettingsPublic.DefaultSignLanguage
                    SWDict.FirstGlossLanguage = SettingsPublic.FirstGlossLanguage

                    StudioSign = SWDict.GetSWSign(SigntoCompare.Item2.IDDictionary, conn, trans)
                    CompareItem.StudioGloss = StudioSign.Gloss
                    CompareItem.StudioGlosses = StudioSign.Glosses
                    CompareItem.StudioImage = StudioSign.Render
                    CompareItem.StudioModified = StudioSign.LastModified
                    CompareItem.StudioSource = StudioSign.SWritingSource
                    CompareItem.StudioDictRow = SigntoCompare.Item2

                    ListToShow.Add(CompareItem)
                Next
                trans.Commit()
            Catch ex As Exception
                General.LogError(ex, "")
                MessageBox.Show(ex.ToString)
                If trans IsNot Nothing Then trans.Rollback()
            Finally
                conn.Close()

            End Try
        End Using
        Return ListToShow
    End Function

    Private Sub CompareSigns_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DataGridView1.EndEdit()
    End Sub

    'Private Sub DataGridView1_CellFormatting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
    '    'if (e.Value isnot nothing && dataGridView1.Columns(e.ColumnIndex) = theRelevantColumn) then
    '    If e.Value IsNot Nothing Then
    '        e.Value = e.Value.ToString()
    '    End If

    'End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Me.Close()
    End Sub
End Class

Public Class CompareItem
    Implements INotifyPropertyChanged

    Public Property puddleGloss As String
    Public Property puddleGlosses As String
    Public Property puddleImage As Image
    Public Property puddleModified As Date
    Public Property puddleSource As String
    Public Property puddleSign As SwSign
    Public Property OverwritefromPuddle As Boolean
    Public Property StudioGloss As String
    Public Property StudioGlosses As String
    Public Property StudioImage As Image
    Public Property StudioModified As Date
    Public Property StudioSource As String
    Public Property StudioDictRow As DictionaryDataSet.DictionaryRow

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged
End Class