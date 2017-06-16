Imports System.ComponentModel
Imports SignWriterStudio.Settings
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.SWClasses
Imports System.Data.SQLite

Public Class CompareSigns
    Public Property SignsToCompare As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))
    Property SignsToAdd As List(Of SwSign)
    Public ListToShow As New BindingList(Of CompareItem)
    Public ListToAdd As New BindingList(Of AddItem)
    Private _connectionString As String
      Enum SelectionMode
         Compare
          Add
    End Enum
    Public Sub New(ByVal connectionString As String)
        _connectionString = connectionString
        InitializeComponent()
    End Sub

    Private Sub CompareSigns_Load(sender As Object, e As EventArgs) Handles Me.Load
        DataGridViewCompare.AutoGenerateColumns = False
        Dim SelectMode As SelectionMode
        If SignsToCompare IsNot Nothing Then
            SelectMode = SelectionMode.Compare
            Dim listtoShow1 As BindingList(Of CompareItem) = PrepareListtoShow(SignsToCompare)

            BindingSourceCompare.DataSource = listtoShow1
            DataGridViewCompare.DataSource = BindingSourceCompare
        ElseIf SignsToAdd IsNot Nothing Then
            Dim listtoShow2 As BindingList(Of AddItem) = PrepareListtoShow(SignsToAdd)

            BindingSourceAdd.DataSource = listtoShow2
            DataGridAdd.DataSource = BindingSourceAdd
            SelectMode = SelectionMode.Add
        End If

        DisplayDataGrid(SelectMode)
        SetFormCaption(SelectMode)
    End Sub

    Private Function PrepareListtoShow(SignstoCompare As List(Of Tuple(Of SwSign, DictionaryDataSet.DictionaryRow, Boolean))) _
        As BindingList(Of CompareItem)

        Dim conn As SQLiteConnection = SWDict.GetNewDictionaryConnection(_connectionString)
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

                    Dim SWDict As New SWDict(_connectionString)
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
    Private Function PrepareListtoShow(SignstoAdd As List(Of SwSign)) _
       As BindingList(Of AddItem)
 
            Try
                Dim AddItem As AddItem

                For Each SigntoAdd In SignstoAdd
                    AddItem = New AddItem
                    AddItem.puddleGloss = SigntoAdd.Gloss
                    AddItem.puddleGlosses = SigntoAdd.Glosses

                    AddItem.puddleImage = SigntoAdd.Render
                    AddItem.puddleModified = SigntoAdd.LastModified
                    AddItem.puddleSource = SigntoAdd.SWritingSource
                    AddItem.puddleSign = SigntoAdd
                    AddItem.OverwritefromPuddle = False

                    ListToAdd.Add(AddItem)
                Next

            Catch ex As Exception
                General.LogError(ex, "")
                MessageBox.Show(ex.ToString)
 
            End Try

        Return ListToAdd
    End Function
    Private Sub CompareSigns_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DataGridViewCompare.EndEdit()
    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click
        Me.Close()
    End Sub

    Private Sub DisplayDataGrid(SelectMode As SelectionMode)
        If SelectMode = SelectionMode.Compare Then
            DataGridViewCompare.Visible = True
            DataGridAdd.Visible = False
        ElseIf SelectMode = SelectionMode.Add Then
            DataGridViewCompare.Visible = False
            DataGridAdd.Visible = True
        End If
    End Sub

    Private Sub SetFormCaption(SelectMode As SelectionMode)
        If SelectMode = SelectionMode.Compare Then
            Me.Text = "Compare Signs"
        ElseIf SelectMode = SelectionMode.Add Then
            Me.Text = "Add Signs"
        End If
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

Public Class AddItem
    Implements INotifyPropertyChanged

    Public Property puddleGloss As String
    Public Property puddleGlosses As String
    Public Property puddleImage As Image
    Public Property puddleModified As Date
    Public Property puddleSource As String
    Public Property puddleSign As SwSign
    Public Property OverwritefromPuddle As Boolean

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged
End Class