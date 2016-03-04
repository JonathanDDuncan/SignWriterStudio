Imports SignWriterStudio.Database.Dictionary

Public Class GlossToSignRealTimeControl
    Private _foundWordDt1 As DictionaryDataSet.SignsbyGlossesUnilingualDataTable
    Private _image1 As Image

    Event SearchTextChanged(glossToSignRealTimeControl As GlossToSignRealTimeControl, searchText As String)

    Public Property FoundWordDt() As DictionaryDataSet.SignsbyGlossesUnilingualDataTable
        Get
            Return _foundWordDt1
        End Get
        Set(value As DictionaryDataSet.SignsbyGlossesUnilingualDataTable)
            _foundWordDt1 = value
        End Set
    End Property

    Public Property ResultType() As Integer

    Public Property Value() As Integer
    Public Property Lane() As Integer = 2
    Public Property Image() As Image
        Get
            Return _image1
        End Get
        Set(value As Image)
            _image1 = value
            SetImage()
        End Set
    End Property

    Private Sub SetImage()
        If Lane = 1 Then
            PictureBox1.Image = Image
            PictureBox2.Image = Nothing
            PictureBox3.Image = Nothing
        End If
        If Lane = 2 Then
            PictureBox1.Image = Nothing
            PictureBox2.Image = Image
            PictureBox3.Image = Nothing
        End If
        If Lane = 3 Then
            PictureBox1.Image = Nothing
            PictureBox2.Image = Nothing
            PictureBox3.Image = Image
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        RaiseEvent SearchTextChanged(Me, TextBox1.Text)
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter, MyBase.Enter, MyBase.Click, PictureBox1.Click, PictureBox2.Click, PictureBox3.Click
        RaiseEvent CurrentGlossControlChanged(Me)
    End Sub

    Public Event CurrentGlossControlChanged As Action(Of GlossToSignRealTimeControl)
  
End Class
