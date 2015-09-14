Imports System.Drawing
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SignWriterStudio.UI
Imports SignWriterStudio.SWClasses

Namespace Tests
    <TestClass()>
    Public Class KswTests
        <TestMethod()> Public Sub KswTest()
            Const fsw As String = "M523x516S2e008502x487S11920478x485"
            Dim ksw = SpmlConverter.Fsw2Ksw(fsw)
            Assert.AreEqual("M23x16S2e0082xn13S11920n22xn15", ksw)
        End Sub

        <TestMethod()> Public Sub KswPunctuationTest()
            Const fsw As String = "P"
            Dim ksw = SpmlConverter.Fsw2Ksw(fsw)
            Assert.AreEqual("P", ksw)
        End Sub
    End Class
End Namespace