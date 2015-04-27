'Option Infer On
'Option Strict On
'Imports SignWriterStudio.SWClasses

'#Const NUnitTest = True   ' NUnit Tests on
'#Const AssertTest = True    ' Assertion rules on
'Imports NUnit.Framework
'Imports NUnit.Framework.Constraints
'Imports SPML

'<TestFixture()> _
'Public Class FindCodePointTests
'    Inherits AssertionHelper
'    Dim Ints() As Integer = {1, 3, 5, 6, 34, 312, 3, 23, 3, 43, 24, 2, 342, 34, 23, 423, 4, 2}
'    <Test()> _
'     Public Sub FindFirstCodePointTest1()
'        Assert.AreEqual(0, Ints.FindFirstCodePoint(1))
'    End Sub
'    <Test()> _
'     Public Sub FindFirstCodePointTest2()
'        Assert.AreEqual(2, Ints.FindFirstCodePoint(5))
'    End Sub
'    <Test()> _
'     Public Sub FindFirstCodePointTest3()
'        Assert.AreEqual(9, Ints.FindFirstCodePoint(43))
'    End Sub
'    <Test()> _
'     Public Sub FindFirstCodePointArrTest1()
'        Dim int2() As Integer = {3, 43, 3}
'        Assert.AreEqual(1, Ints.FindFirstCodePoint(int2))
'    End Sub
'    <Test()> _
'     Public Sub FindFirstCodePointArrTest2()
'        Dim int2() As Integer = {342, 43, 16}
'        Assert.AreEqual(9, Ints.FindFirstCodePoint(int2))
'    End Sub

'    <Test()> _
'    Public Sub FindCodePointsTestArr1()
'        Dim int2() As Integer = {3, 43, 2}
'        Dim Res = Ints.FindCodePoints(int2)
'        Assert.AreEqual(1, Res(0))
'        Assert.AreEqual(6, Res(1))
'        Assert.AreEqual(8, Res(2))
'        Assert.AreEqual(9, Res(3))
'        Assert.AreEqual(11, Res(4))
'        Assert.AreEqual(17, Res(5))
'    End Sub
'    <Test()> _
'     Public Sub FindCodePointsTestArr2()
'        Dim int2() As Integer = {342, 43, 423}
'        Dim Res = Ints.FindCodePoints(int2)
'        Assert.AreEqual(9, Res(0))
'        Assert.AreEqual(12, Res(1))
'        Assert.AreEqual(15, Res(2))
'    End Sub
'    <Test()> _
'     Public Sub FindCodePointsTest1()

'        Dim Res = Ints.FindCodePoints(3)
'        Assert.AreEqual(1, Res(0))
'        Assert.AreEqual(6, Res(1))
'        Assert.AreEqual(8, Res(2))
'    End Sub
'    <Test()> _
'     Public Sub FindCodePointsTest2()

'        Dim Res = Ints.FindCodePoints(2)
'        Assert.AreEqual(11, Res(0))
'        Assert.AreEqual(17, Res(1))
'    End Sub
'    <Test()> _
'   Public Sub GetArrayTest1()

'        Dim Res = Ints.GetArray(15, 17)
'        Assert.AreEqual(423, Res(0))
'        Assert.AreEqual(4, Res(1))
'        Assert.AreEqual(2, Res(2))
'    End Sub
'    <Test()> _
'    Public Sub GetArrayTest2()

'        Dim Res = Ints.GetArray(-1, 17)
'        Assert.AreEqual(Nothing, Res)
'    End Sub
'    <Test()> _
'    Public Sub GetArrayTest3()

'        Dim Res = Ints.GetArray(6, 19)
'        Assert.AreEqual(Nothing, Res)

'    End Sub
'    <Test()> _
'    Public Sub GetArrayTest4()

'        Dim Res = Ints.GetArray(0, 2)
'        Assert.AreEqual(1, Res(0))
'        Assert.AreEqual(3, Res(1))
'        Assert.AreEqual(5, Res(2))
'    End Sub

'    <Test()> _
'    Public Sub AppendTest1()

'        Dim Res As Integer() = {1, 2}
'        Res = Res.Append(3)
'        Assert.AreEqual(1, Res(0))
'        Assert.AreEqual(2, Res(1))
'        Assert.AreEqual(3, Res(2))
'    End Sub

'    <Test()> _
'    Public Sub AppendTest2()

'        Dim Res As Integer() = {55, 56}
'        Res = Res.Append(57)
'        Assert.AreEqual(55, Res(0))
'        Assert.AreEqual(56, Res(1))
'        Assert.AreEqual(57, Res(2))
'    End Sub
'    <Test()> _
'   Public Sub AppendTest3()

'        Dim Res As Integer()
'        Res = Res.Append(57)
'        Assert.AreEqual(57, Res(0))

'    End Sub

'    <Test()> _
'  Public Sub GetString1()

'        Dim Res As Integer() = {65, 66, 67}
'        Assert.AreEqual("ABC", Res.GetString)

'    End Sub

'     <Test()> _
'Public Sub AppendArrayTest1()
'        Dim Arr1 As Integer() = {1, 2, 3}
'        Dim Arr2 As Integer() = {4, 5, 6}
'        Dim Arr3 = Arr1.Append(Arr2)
'        Assert.AreEqual(3, Arr3(2))
'        Assert.AreEqual(4, Arr3(3))
'        Assert.AreEqual(5, Arr3(4))
'        Assert.AreEqual(6, Arr3(5))
'    End Sub
'    <Test()> _
'Public Sub AppendArrayTest2()
'        Dim Arr1 As Integer() = {1}
'        Dim Arr2 As Integer() = {2}
'        Dim Arr3 = Arr1.Append(Arr2)
'        Assert.AreEqual(1, Arr3(0))
'        Assert.AreEqual(2, Arr3(1))

'    End Sub
'End Class