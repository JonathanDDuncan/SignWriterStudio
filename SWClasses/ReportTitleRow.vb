Option Strict On


Public Class ReportTitleRow
    Inherits DataRow

    <DebuggerNonUserCodeAttribute(), CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")> _
    Friend Sub New(ByVal rb As DataRowBuilder)
        MyBase.New(rb)
        ReportTitleTable = CType(Table, ReportTitleTable)
    End Sub

    Public Property ReportTitleTable() As ReportTitleTable
    Property Title As String
    Property Gloss As String
    Property SignWriting As String
    Property Illustration As String
    Property PhotoSign As String

End Class