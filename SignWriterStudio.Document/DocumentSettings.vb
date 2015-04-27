Imports SignWriterStudio.SWClasses

Public Class DocumentSettings
    Private Padd As New Padding
    Private CallingForm As Form
    Private SWDocumentForm As SWDocumentForm
    Private Sub BtnApplytoAllPadding_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnApplytoAllPadding.Click
        If CallingForm IsNot Nothing AndAlso CallingForm.Name = "SWDocumentForm" Then

            'Dim DocForm As SWDocumentForm = CType(CallingForm, SWDocumentForm)
            'DocForm.SwFlowLayoutPanel1.SuspendLayout()
            'For Each item As SWDocumentSign In DocForm.Document.DocumentSigns
            '    DocForm.SwFlowLayoutPanel1.layoutEng.LayoutEngineSettings.bottomControlMargin = Me.TBMarginBottom.Text
            'Next
            'For Each item As SWLayoutControl In DocForm.SwFlowLayoutPanel1.Controls
            '    item.Refresh()
            'Next
            'DocForm.SwFlowLayoutPanel1.ResumeLayout()
        End If
    End Sub
    Public Sub SetCallignForm(ByVal Form As Form)
        Me.CallingForm = Form
    End Sub

    Private Sub TBPadding_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TBMarginBottom.Validating
        Dim sender1 As TextBox = CType(sender, TextBox)
        If Not IsNumeric(sender1.Text) Then
            e.Cancel = True
        Else
            sender1.Text = CInt(sender1.Text)
        End If

    End Sub


    Private Sub BtnBackgroundcolor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBackgroundcolor.Click
        Dim diagres As DialogResult = Me.ColorDialog1.ShowDialog()
        If diagres = Windows.Forms.DialogResult.OK Then
            BtnBackgroundcolor.BackColor = Me.ColorDialog1.Color
        End If
    End Sub

    Private Sub DocumentSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim layEngSettings As SwLayoutEngineSettings = SWDocumentForm.SwFlowLayoutPanel1.layoutEng.LayoutEngineSettings
        'Me.TBMinLeft.Text = LayEngSettings.MinLeftLaneWidth
        'Me.TBMinCenter.Text = LayEngSettings.MinCenterLaneWidth
        'Me.TBMinRight.Text = LayEngSettings.MinRightLaneWidth

        'Me.TBMinCol.Text = LayEngSettings.MinColumnWidth
        'Me.TBMarginBottom.Text = LayEngSettings.BottomControlMargin
        Me.TBSpaceBetween.Text = layEngSettings.SpaceBetweenCols
        Me.BtnBackgroundcolor.BackColor = layEngSettings.BackgroundColor
        Me.ColorDialog1.Color = layEngSettings.BackgroundColor
        Me.CBShowLines.Checked = layEngSettings.DrawColumnLines

    End Sub

    Private Sub BtnAccept_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAccept.Click
        Dim layEngSettings As SwLayoutEngineSettings = SWDocumentForm.SwFlowLayoutPanel1.layoutEng.LayoutEngineSettings
        'layEngSettings.MinLeftLaneWidth = Me.TBMinLeft.Text
        'layEngSettings.MinCenterLaneWidth = Me.TBMinCenter.Text
        'layEngSettings.MinRightLaneWidth = Me.TBMinRight.Text

        'layEngSettings.MinColumnWidth = Me.TBMinCol.Text
        'layEngSettings.BottomControlMargin = Me.TBMarginBottom.Text
        layEngSettings.SpaceBetweenCols = Me.TBSpaceBetween.Text
        layEngSettings.BackgroundColor = Me.BtnBackgroundcolor.BackColor
        layEngSettings.BackgroundColor = Me.ColorDialog1.Color
        layEngSettings.DrawColumnLines = Me.CBShowLines.Checked
        SWDocumentForm.SwFlowLayoutPanel1.layoutEng.LayoutEngineSettings = layEngSettings
        SWDocumentForm.SwFlowLayoutPanel1.Invalidate()
        SWDocumentForm.SwFlowLayoutPanel1.PerformLayout()

        Me.Close()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Close()

    End Sub

    Public Sub New(ByVal SWDocumentForm As SWDocumentForm)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SWDocumentForm = SWDocumentForm
    End Sub
End Class