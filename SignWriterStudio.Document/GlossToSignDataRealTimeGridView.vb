Imports SignWriterStudio.Dictionary
Imports SignWriterStudio.Database.Dictionary
Imports SignWriterStudio.SWClasses
Imports SignWriterStudio.SWEditor

Public Class GlossToSignRealTimeControl
    Private _foundWordDt1 As DataTable
    Private _image1 As Image


    Event SearchTextChanged(glossToSignRealTimeControl As GlossToSignRealTimeControl, searchText As String)

    Event InsertBefore(glossToSignRealTimeControl As GlossToSignRealTimeControl)

    Event InsertAfter(glossToSignRealTimeControl As GlossToSignRealTimeControl)

    Event DeleteEntry(glossToSignRealTimeControl As GlossToSignRealTimeControl)

    Event AddFromDict(glossToSignRealTimeControl As GlossToSignRealTimeControl, searchText As String)



    Public Property FoundWordDt() As DataTable
        Get
            Return _foundWordDt1
        End Get
        Set(value As DataTable)
            _foundWordDt1 = value
        End Set
    End Property

    Public Property ResultType() As Integer

    Public Property Value() As Integer
    Public Property Lane() As Integer = 2
    Public Property NewLane() As Integer
    Public Property Image() As Image
        Get
            Return _image1
        End Get
        Set(value As Image)
            _image1 = value
            SetImage(Nothing)
        End Set
    End Property

    Private Sub SetImage(tempLane As Integer?)

        Dim imageLane As Integer
        If tempLane IsNot Nothing Then imageLane = tempLane Else imageLane = Lane
        If imageLane = 1 Then
            PictureBox1.Image = Image

            PictureBox2.Image = Nothing
            PictureBox3.Image = Nothing
        End If
        If imageLane = 2 Then
            PictureBox1.Image = Nothing
            PictureBox2.Image = Image

            PictureBox3.Image = Nothing
        End If
        If imageLane = 3 Then
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

    Private Sub GlossToSignRealTimeControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.AllowDrop = True
        PictureBox2.AllowDrop = True
        PictureBox3.AllowDrop = True
    End Sub

    Private Shared Sub PictureBox_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles PictureBox1.MouseDown, PictureBox2.MouseDown, PictureBox3.MouseDown
        Dim picbox As PictureBox = CType(sender, PictureBox)
        If picbox IsNot Nothing AndAlso picbox.Image IsNot Nothing Then
            picbox.DoDragDrop(picbox.Image, DragDropEffects.Copy)
        End If
    End Sub

    Private Sub PictureBox_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles PictureBox1.DragEnter, PictureBox2.DragEnter, PictureBox3.DragEnter
        e.Effect = DragDropEffects.Copy
        SetLane(sender)
    End Sub

    Private Shared Sub PictureBox_DragOver(ByVal sender As Object, ByVal e As DragEventArgs) Handles PictureBox1.DragOver, PictureBox2.DragOver, PictureBox3.DragOver
        sender.BringToFront()
    End Sub

    Private Sub SetLane(ByVal sender As Object)
        If sender IsNot Nothing Then
            Dim picbox As PictureBox = CType(sender, PictureBox)
            If picbox.Equals(PictureBox1) Then
                NewLane = 1
                SetImage(NewLane)
            ElseIf picbox.Equals(PictureBox2) Then
                NewLane = 2
                SetImage(NewLane)
            ElseIf picbox.Equals(PictureBox3) Then
                NewLane = 3
                SetImage(NewLane)
            End If
        End If
    End Sub

    Private Sub pictureBox_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles PictureBox1.DragDrop, PictureBox2.DragDrop, PictureBox3.DragDrop
        Lane = NewLane
        SetImage(Lane)

    End Sub
    Private Sub pictureBox_DragLeave(ByVal sender As Object, ByVal e As DragEventArgs) Handles PictureBox1.DragLeave, PictureBox2.DragLeave, PictureBox3.DragLeave
        SetImage(Lane)
    End Sub

    Private Sub btnInsertBefore_Click(sender As Object, e As EventArgs) Handles btnInsertBefore.Click
        RaiseEvent InsertBefore(Me)
    End Sub

    Private Sub btnInsertAfter_Click(sender As Object, e As EventArgs) Handles btnInsertAfter.Click
        RaiseEvent InsertAfter(Me)
    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click
        RaiseEvent DeleteEntry(Me)
    End Sub

    Private Sub btnAddFromDict_Click(sender As Object, e As EventArgs) Handles btnAddFromDict.Click
        RaiseEvent AddFromDict(Me, TextBox1.Text)
    End Sub
 End Class
