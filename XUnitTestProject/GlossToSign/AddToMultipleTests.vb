Imports System
Imports SignWriterStudio.Document
Imports Xunit

Namespace XUnitTestProject
    Public Class AddToMultipleTests
        <Fact>
        Sub TestSub()
            Dim text = "[topico Dios él-D]"
            Dim expected = {"topico", "Dios", "él-D"}
            Dim result = Class1.GetGlossToSignArray(text)
            Assert.StrictEqual(Of String())(expected, result)
        End Sub
    End Class
End Namespace

