Imports System.Windows.Forms
Imports System.Net
Imports System.Collections.Specialized
Imports System.IO
Imports System.Text
Imports System.IO.Compression

Public Class About
    Private AcercaDe As SignWriterStudio.SWClasses.AcercaDE
    Private Sub About_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.Activado.Text = AcercaDe.Activado
        'If Me.Activado.Text = "Yes" Then
        '    Me.LBDiasPrueba.Visible = False
        '    Me.Prueba.Visible = False

        'End If
        'If Not AcercaDe.IsBeta Then
        '    Me.LBBetaEndDate.Visible = False
        '    Me.BetaEndDate.Visible = False
        '    Me.BetaEndDate.Visible = False
        '    Me.LBBetaExplanation.Visible = False
        'End If
        'Me.Autoriza.Text = AcercaDe.Autorizado
        'Me.Prueba.Text = AcercaDe.Prueba
        Me.Version.Text = Application.ProductVersion
        'Me.Level.Text = AcercaDe.Level
        'Dim CA As New ClientAppSimple
        'Me.BetaEndDate.Text = CA.BetaED
        'If Me.Level.Text = String.Empty Then
        '    Me.Level.Text = "Trial"
        'End If
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub

    Private Sub BtnActivate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim CA As New ClientAppSimple(False)
        Me.Hide()
        CA.Activate()
        Application.Exit()
    End Sub

    Private Sub BtnSaveLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveLog.Click
        SaveLogFile()
    End Sub
    Private Sub SaveLogFile()
        Dim FullFileName As String = My.Application.Log.DefaultFileLogWriter.FullLogFileName
        Dim savedialog As New Windows.Forms.SaveFileDialog
        savedialog.FileName = "SignWriterStudio.log"
        Dim res As Windows.Forms.DialogResult = savedialog.ShowDialog()
        If res = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.CopyFile(FullFileName, savedialog.FileName, True)
        End If
    End Sub

    Public Sub New() 'ByVal AcercaDe As SWClasses.AcercaDE)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.AcercaDe = AcercaDe
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        System.Diagnostics.Process.Start("http://www.signwriterstudio.com")
    End Sub

    Private Sub btnPurchase_Click(sender As Object, e As EventArgs)
        'Dim Email As String = Microsoft.VisualBasic.Interaction.InputBox("Email address", "Email")
        'Dim Username As String = Microsoft.VisualBasic.Interaction.InputBox("Full Name", "Username")

        Dim strCompInfo As String
        Dim CompInfo As New CompInfo
        Dim CA As New ClientAppSimple(False)

        strCompInfo = CA.GetFileCompInfo()
        CA.DeserializeCompInfo(strCompInfo, CompInfo)
        Dim GUID As Guid = CompInfo.InstalationId
        Dim Params As New NameValueCollection
        'Dim remoteUrl As String = "http://localhost:3089/Transactions/GetInstalationInfo.aspx"
        Dim remoteUrl As String = "http://localhost:3089/GetCompInfo.asmx/CompInfo"

        'Params.Add("Email", General.URLEncode(General.HtmlEncode(Email)))
        'Params.Add("Username", General.URLEncode(General.HtmlEncode(Username)))
        Params.Add("Version", Uri.EscapeDataString(My.Application.Info.Version.ToString))
        Params.Add("GUID", Uri.EscapeDataString(GUID.ToString))
        Params.Add("CompInfo", Uri.EscapeDataString(strCompInfo)) 'Uri.EscapeDataString(strCompInfo))
        'Dim Xml1 = "<root>" & "<version>" & My.Application.Info.Version.ToString & "</version>" & _
        ' "<GUID>" & GUID.ToString & "</GUID>" & _
        '  "<SendCompInfo>" & GUID.ToString & "</SendCompInfo>" & _
        '"</root>"
        Try
            Dim postResult As String = General.HtmlDecode(General.SendPost(remoteUrl, Params))


            Dim message As String = General.GetTagValue(postResult, "message")
            Dim errorMessage As String = General.GetTagValue(postResult, "errormessage")
            Dim fullerrormessage As String = General.GetTagValue(postResult, "fullerrormessage")

            If Not message = String.Empty Then
                MessageBox.Show(message)
            ElseIf Not errorMessage = String.Empty Then
                MessageBox.Show("There was an error processing your request. " & errorMessage)
                My.Application.Log.WriteEntry(fullerrormessage)
            Else
                ShowPurchasePage(GUID)
            End If
        Catch ex As WebException
            General.LogError(ex, "Could not connect to license server.  If you will not be able to connect to the Internet from this computer follow the instructions in the help file for purchasing and activating a license on computer disconnected from the Internet. Would you like to save the registration file?")
            Dim result = MessageBox.Show("Could not connect to license server.  If you will not be able to connect to the Internet from this computer follow the instructions in the help file for purchasing and activating a license on computer disconnected from the Internet. Would you like to save the registration file?", "Could not connect.", MessageBoxButtons.YesNo)
            If result = Windows.Forms.DialogResult.Yes Then
                Dim CA1 As New ClientAppSimple(False)

                CA1.Guardarregistration()
            End If
        End Try

    End Sub

    Private Sub ShowPurchasePage(InstalationID As Guid)
        'Dim token As String = GetTagValue(postResult, "token")
        Dim URI As String
        'TODO Get base URI from config file
        'URI = "https://www.sandbox.paypal.com/webscr?cmd=_express-checkout&token=" & token
        URI = "http://localhost:3089/Transactions/PurchaseSWS.aspx?&InstalationID=" & InstalationID.ToString()
        System.Diagnostics.Process.Start(URI)

    End Sub
   


End Class