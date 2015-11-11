﻿Imports HtmlAgilityPack

Public Class SignPuddleSignIn

    Private Sub SignPuddle_Sign_In_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetPuddles()
    End Sub

    Private Sub GetPuddles()
        Dim api = New SignPuddleApi.SignPuddleApi("", "")

        Dim webPage = api.GetPuddles()

        Dim puddles = PuddlesFromWebPage(webPage)
        LoadPuddlesComboBox(puddles)
    End Sub

    Private Sub LoadPuddlesComboBox(ByVal puddles As List(Of Puddle))
        For Each puddle1 As Puddle In puddles
            CBPuddles.Items.Add(puddle1)

        Next
        CBPuddles.DisplayMember = "Name"
        CBPuddles.ValueMember = "Sgn"
        CBPuddles.SelectedIndex = 0
    End Sub

    Private Function PuddlesFromWebPage(ByVal webPage As String) As List(Of Puddle)

        Dim html As HtmlDocument = New HtmlDocument()
        html.LoadHtml(webPage)
        Dim document = html.DocumentNode
        Dim puddles = New List(Of Puddle)
        Dim forms = document.SelectNodes("//form[@action='index.php']")
        For Each htmlNode As HtmlNode In forms
            Dim puddle = New Puddle()
            Dim sgnnode = htmlNode.ParentNode.SelectSingleNode("input[@name='sgn']")
            If sgnnode IsNot Nothing Then
                Dim sgn = sgnnode.GetAttributeValue("value", "")
                puddle.Sgn = sgn
            End If
            Dim puddleNamenode = htmlNode.ParentNode.SelectSingleNode("button/table/tr/td/img")
            If puddleNamenode IsNot Nothing Then
                Dim puddlname = puddleNamenode.GetAttributeValue("title", "")
                puddle.Name = puddlname
            End If
            puddles.Add(puddle)
        Next
        Return puddles
    End Function

    Private Sub BtnSignIn_Click(sender As Object, e As EventArgs) Handles BtnSignIn.Click
        SignPuddleApi = New SignPuddleApi.SignPuddleApi(TBUsername.Text, TBPassword.Text)

        If Not SignPuddleApi.IsLoggedIn Then
            MessageBox.Show("Could not log into account")
        Else
            Dim puddle = CType(CBPuddles.SelectedItem, Puddle)
            Sgn = puddle.Sgn
            PuddleName = puddle.Name
            IsLoggedIn = SignPuddleApi.IsLoggedIn
            Hide()
        End If
    End Sub

    Public Property IsLoggedIn() As Boolean


    Public Property SignPuddleApi() As SignPuddleApi.SignPuddleApi


    Public Property PuddleName() As String


    Public Property Sgn() As String

End Class