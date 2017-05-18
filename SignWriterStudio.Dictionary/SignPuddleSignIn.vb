Imports HtmlAgilityPack

Public Class SignPuddleSignIn

    Private Sub SignPuddle_Sign_In_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'GetPuddles("http://signtyp.uconn.edu/signpuddle/")
        CBSiteUrl.SelectedIndex = 0
    End Sub

    Private Sub GetPuddles(siteurl As String)
        Dim api = New SignPuddleApi.SignPuddleApi(siteurl, "", "")

        Dim webPage = api.GetPuddles(siteurl)

        Dim puddles = PuddlesFromWebPage(webPage)
        LoadPuddlesComboBox(puddles)
    End Sub

    Private Sub LoadPuddlesComboBox(ByVal puddles As List(Of Puddle))
        CBPuddles.Items.Clear()
        For Each puddle1 As Puddle In puddles
            CBPuddles.Items.Add(puddle1)

        Next
        CBPuddles.DisplayMember = "Name"
        CBPuddles.ValueMember = "Sgn"
        CBPuddles.SelectedIndex = 0
    End Sub

    Private Function PuddlesFromWebPage(ByVal webPage As String) As List(Of Puddle)
        Dim puddles As List(Of Puddle)
        If webPage.Contains("uconn") Then
            puddles = GetUconnPuddles(webPage)

        Else
            puddles = GetSignBankPuddles(webPage)
        End If
        Return puddles
    End Function
    Private Function GetUconnPuddles(ByVal webPage As String) As List(Of Puddle)

        Dim puddles = New List(Of Puddle)

        Dim html As HtmlDocument = New HtmlDocument()
        html.LoadHtml(webPage)
        Dim document = html.DocumentNode

        Dim forms = document.SelectNodes("//form[@action='index.php']")
        For Each htmlNode As HtmlNode In forms
            Dim puddle = New Puddle()
            Dim sgnnode = htmlNode.ParentNode.SelectSingleNode("input[@name='sgn']")
            If sgnnode IsNot Nothing Then
                Dim sgn = sgnnode.GetAttributeValue("value", "")
                puddle.Sgn = sgn
            End If
            Dim puddleNamenode = htmlNode.ParentNode.SelectSingleNode("button/table/tr/td/font")
            If puddleNamenode IsNot Nothing Then
                Dim puddlname = puddleNamenode.InnerText()
                puddle.Name = puddlname
            End If
            puddles.Add(puddle)
        Next
        Return puddles
    End Function
    Private Function GetSignBankPuddles(ByVal webPage As String) As List(Of Puddle)

        Dim puddles = New List(Of Puddle)

        Dim html As HtmlDocument = New HtmlDocument()
        html.LoadHtml(webPage)
        Dim document = html.DocumentNode

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
        SignPuddleApi = New SignPuddleApi.SignPuddleApi(CBSiteUrl.Text, TBUsername.Text, TBPassword.Text)
        '"http://www.signbank.org/signpuddle2.0/"
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
     
  

    Private Sub CBSiteUrl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBSiteUrl.SelectedIndexChanged
        Try
            GetPuddles(CBSiteUrl.Text)
            CBPuddles.Enabled = True
            TBPassword.Enabled = True
            TBUsername.Enabled = True
            BtnSignIn.Enabled = True
        Catch webEx As Net.WebException
            MessageBox.Show("Could not connect to sign puddle site.")
            CBPuddles.Enabled = False
            TBPassword.Enabled = False
            TBUsername.Enabled = False
            BtnSignIn.Enabled = False
        End Try

    End Sub
End Class