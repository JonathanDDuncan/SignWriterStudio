Option Infer On
Option Strict On
Imports SignWriterStudio.SWClasses

#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports NUnit.Framework
Imports NUnit.Framework.Constraints


<TestFixture()> _
Public Class StringParserTests
    Private buildStr As String = "AS10020S14c1aS21000S26506M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483"
    <Test()> _
    Public Sub GetSequenceTest1()
        Assert.AreEqual("AS10020S14c1aS21000S26506", buildStr.GetSequenceBuildStr)
    End Sub
    <Test()> _
    Public Sub GetSymbolsTest1()
        Assert.AreEqual("M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483", buildStr.GetSymbolsBuildStr)
    End Sub
    <Test()> _
    Public Sub GetSequenceSymbolTest1()
        Dim Sequence = "AS10020"
        Assert.AreEqual("10020", SplitSequenceBuildStr(buildStr)(0))
    End Sub
    <Test()> _
    Public Sub GetSequenceSymbolTest2()
        Dim BuildStr = "AS10020S14c1aS21000S26506"
        Assert.AreEqual("10020", SplitSequenceBuildStr(BuildStr)(0))
    End Sub
    <Test()> _
    Public Sub GetSequenceSymbolTest3()
        Dim BuildStr = "AS10020S14c1aS21000S26506"
        Assert.AreEqual("14c1a", SplitSequenceBuildStr(BuildStr)(1))
    End Sub
    <Test()> _
    Public Sub GetSequenceSymbolTest4()
        Dim BuildStr = "AS10020S14c1aS21000S26506"
        Assert.AreEqual("21000", SplitSequenceBuildStr(BuildStr)(2))
    End Sub
    <Test()> _
    Public Sub GetSequenceSymbolTest5()
        Dim BuildStr = "AS10020S14c1aS21000S26506"
        Assert.AreEqual("26506", SplitSequenceBuildStr(BuildStr)(3))
    End Sub
    <Test()> _
    Public Sub GetSymbolSplitTest1()
        Dim Sequence = "M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483"
        Assert.AreEqual("14c1a462x482", SplitSymbolBuildStr(buildStr)(0))
    End Sub
    <Test()> _
    Public Sub GetSymbolSplitTest2()
        Dim Sequence = "M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483"
        Assert.AreEqual("10020489x488", SplitSymbolBuildStr(buildStr)(1))
    End Sub
    <Test()> _
    Public Sub GetSymbolSplitTest3()
        Dim Sequence = "M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483"
        Assert.AreEqual("26506523x483", SplitSymbolBuildStr(buildStr)(2))
    End Sub
    <Test()> _
    Public Sub GetSymbolSplitTest4()
        Dim Sequence = "M538x518S14c1a462x482S10020489x488S26506523x483S21000500x483"
        Assert.AreEqual("21000500x483", SplitSymbolBuildStr(buildStr)(3))
    End Sub
End Class
