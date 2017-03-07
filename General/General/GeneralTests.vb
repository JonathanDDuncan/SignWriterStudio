Imports NUnit.Framework
Imports NUnit.Framework.Constraints
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
#If NUnitTest Then
<TestFixture()> _
Public Class UndoTests
	Inherits AssertionHelper
	Dim Undo as New Undo(Of String)
	<TestFixtureSetUp()> _
	Public Sub Setup()
		Undo.Add ("1")
		Undo.Add ("2")
		Undo.Add ("3")
		Undo.Add ("4")
		Undo.Add ("5")
		Undo.Add ("6")
		Undo.Add ("7")
		Undo.Add ("8")
		Undo.Add ("9")
		Undo.Add ("10")
	End Sub
	
	<Test()> _
	Public Sub UndoTest1()
		Assert.AreEqual( "10" , Undo.Undo ("9"))
	End Sub
	<Test()> _
	Public Sub UndoTest2()
		Assert.AreEqual( "9" , Undo.Undo ("8"))
	End Sub
	<Test()> _
	Public Sub UndoTest3()
		Assert.AreEqual( "8" , Undo.Undo ("7"))
	End Sub
	<Test()> _
	Public Sub UndoTest4()
		Assert.AreEqual( "7" , Undo.Redo ("10"))
	End Sub
	<Test()> _
	Public Sub UndoTest5()
		Assert.AreEqual( "8" , Undo.Redo ("11"))
	End Sub
	<Test()> _
	Public Sub UndoTest6()
		Undo.Clear
		Assert.AreEqual( Nothing , Undo.Undo(Nothing))
	End Sub
End Class
<TestFixture()> _
Public Class PathsTests
	Inherits AssertionHelper
	
	<Test()> _
	Public Sub AllUserDataTest1()
		Dim p as New Paths
		Assert.AreEqual(True, io.Path.IsPathRooted (Paths.AllUsersData()))
	End Sub
	
End Class


<TestFixture()> _
Public Class GeneralTests
	Inherits AssertionHelper
	
	
	<Test()> _
	Public Sub ImageToByteArrayTest1()
		Dim Bmp As New Bitmap (5,5, drawing.Imaging.PixelFormat.Format32bppRgb)
		Dim byt() As Byte = {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 5, 0, 0, 0, 5, 8, 6, 0, 0, 0, 141, 111, 38, 229, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 0, 18, 73, 68, 65, 84, 24, 87, 99, 0, 130, 255, 88, 48, 149, 5, 25, 254, 3, 0, 246, 170, 24, 232, 36, 165, 47, 143, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130}
		Dim Img As Image = CType(bmp, Image)
		Dim byt1() As Byte = ImageToByteArray(Img, Drawing.Imaging.ImageFormat.Png)
        Dim STRB As New System.Text.StringBuilder
		
		For Each I  As Integer In byt1
			STRB.append(I)
			STRB.append(", ")
		Next
		Debug.Print (STRB.tostring)
		Assert.AreEqual(byt, byt1 )
	End Sub
	<Test()> _
    Public Sub ImageToByteArrayFail1()
        Dim Bmp As New Bitmap(5, 5, drawing.Imaging.PixelFormat.Format32bppRgb)
        Dim byt() As Byte = {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 10, 0, 0, 0, 10, 8, 6, 0, 0, 0, 141, 50, 207, 189, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 32, 99, 72, 82, 77, 0, 0, 122, 38, 0, 0, 128, 132, 0, 0, 250, 0, 0, 0, 128, 232, 0, 0, 117, 48, 0, 0, 234, 96, 0, 0, 58, 152, 0, 0, 23, 112, 156, 186, 81, 60, 0, 0, 0, 30, 73, 68, 65, 84, 40, 83, 99, 100, 96, 96, 248, 15, 196, 68, 1, 144, 66, 98, 48, 81, 138, 192, 182, 18, 99, 218, 8, 85, 8, 0, 177, 23, 90, 167, 145, 196, 111, 229, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130}
        Dim Img As Image = CType(Bmp, Image)
        Assert.Throws(Of AssertionException)(Function() ImageToByteArray(Img, Nothing))
    End Sub
	<Test()> _
    Public Sub ImageToByteArrayTestFail2()
        Dim Bmp As New Bitmap(10, 10, drawing.Imaging.PixelFormat.Format32bppRgb)
        Dim byt() As Byte = {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 10, 0, 0, 0, 10, 8, 6, 0, 0, 0, 141, 50, 207, 189, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 32, 99, 72, 82, 77, 0, 0, 122, 38, 0, 0, 128, 132, 0, 0, 250, 0, 0, 0, 128, 232, 0, 0, 117, 48, 0, 0, 234, 96, 0, 0, 58, 152, 0, 0, 23, 112, 156, 186, 81, 60, 0, 0, 0, 30, 73, 68, 65, 84, 40, 83, 99, 100, 96, 96, 248, 15, 196, 68, 1, 144, 66, 98, 48, 81, 138, 192, 182, 18, 99, 218, 8, 85, 8, 0, 177, 23, 90, 167, 145, 196, 111, 229, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130}
        Dim Img As Image = CType(bmp, Image)
        Assert.Throws(Of AssertionException)(Function() ImageToByteArray(Nothing, Drawing.Imaging.ImageFormat.Png))
    End Sub
	
	<Test()> _
	Public Sub NulltoByteArrayTest1()
        Dim Thisobj As Byte() = Nothing
		Assert.AreEqual( Nothing , NulltoByteArray(Thisobj))
	End Sub

	<Test()> _
	Public Sub ByteArraytoImageTest1()
		Dim byt() As Byte = {137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 10, 0, 0, 0, 10, 8, 6, 0, 0, 0, 141, 50, 207, 189, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 32, 99, 72, 82, 77, 0, 0, 122, 38, 0, 0, 128, 132, 0, 0, 250, 0, 0, 0, 128, 232, 0, 0, 117, 48, 0, 0, 234, 96, 0, 0, 58, 152, 0, 0, 23, 112, 156, 186, 81, 60, 0, 0, 0, 30, 73, 68, 65, 84, 40, 83, 99, 100, 96, 96, 248, 15, 196, 68, 1, 144, 66, 98, 48, 81, 138, 192, 182, 18, 99, 218, 8, 85, 8, 0, 177, 23, 90, 167, 145, 196, 111, 229, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130}
		Dim Img As Image = ByteArraytoImage(byt)
		Assert.AreEqual( New Size(10,10) , img.Size)
	End Sub
	
	<Test()> _
	Public Sub NZTest1()
		Assert.AreEqual(String.Empty, Nz(Nothing,String.Empty))
	End Sub
	<Test()> _
	Public Sub NZTest2()
		Assert.AreEqual(1, Nz(1,String.Empty))
	End Sub
	<Test()> _
	Public Sub NZTest3()
		Assert.AreEqual("a", Nz("a",String.Empty))
	End Sub
	<Test()> _
	Public Sub NZTest4()
		Dim Testobj as Nullable(Of Integer)
		Assert.AreEqual(String.Empty, Nz(Testobj,String.Empty))
	End Sub
	<Test()> _
	Public Sub NZTest5()
		Dim Testobj as New Nullable(Of Integer)
		Assert.AreEqual(String.Empty, Nz(Testobj,String.Empty))
	End Sub
	<Test()> _
	Public Sub NZTest6()
		Dim Testobj As New Nullable(Of Integer)
		Testobj = 6
		Assert.AreEqual(Testobj, Nz(Testobj,String.Empty))
	End Sub
	'		<ExpectedException(GetType(AssertionException))> _
	<Test()> _
	Public Sub NZTestFail1()
		Dim Testobj As  Nullable(Of Integer)
		NZ(Testobj, Nothing)
		Assert.AreEqual(Testobj, Nz(Testobj,Nothing))
	End Sub
	
	<Test()> _
	Public Sub BinaryCountTest1()
		Assert.AreEqual(1, BinaryCount(1))
	End Sub
	
	<Test()> _
	Public Sub BinaryCountTest3()
		Assert.AreEqual(6, BinaryCount(63))
	End Sub
	
	<Test()> _
	Public Sub BinaryCountTest2()
		Assert.AreEqual(2, BinaryCount(3))
	End Sub
	
	<Test()> _
	Public Sub BinaryCountTest4()
		Assert.AreEqual(8, BinaryCount(255))
	End Sub
	
	<Test()> _
	Public Sub BinaryCountTest5()
		Assert.AreEqual(16, BinaryCount(65535))
	End Sub
	
	<Test()> _
    Public Sub BinaryCountFail1()
        Assert.Throws(Of AssertionException)(Function() BinaryCount(-1))
    End Sub
    <Test()> _
    Public Sub BinaryCountFail2()
        Assert.Throws(Of AssertionException)(Function() BinaryCount(65536))
    End Sub
	
	<Test()> Public Shared Sub CheckSWIdTestPass()
		
		Assert.That(CheckId("01-01-001-01-01-01"), Iz.True)
	End Sub
	<Test()> _
	Public Shared Sub CheckSWIdTestFail1()
		
		Assert.IsFalse(CheckId("001-01-01-001-01-01"), "Fail 1 Id")
		
	End Sub
	<Test()> _
	Public Shared Sub CheckSWIdTestFail2()
		
		Assert.IsFalse(CheckId("a1-01-01-001-01-01"), "Fail 2 Id")
	End Sub
	
End Class

 
    <TestFixture()> _
    Public Class SignBoundsTests
    Inherits AssertionHelper
    Dim SB As New SignBounds

   
    <TestFixtureSetUp()> _
    Public Sub TestFixtureSetUp()
        SB.Update(7, 4, 1, 10)
        SB.Update(4, 8, 5, 6)
        SB.Update(15, 6, 2, 16)
        SB.Update(16, 11, 3, 8)
        SB.Update(12, 5, 4, 9)
        SB.Update(6, 4, 7, 17)
    End Sub


    <Test()> _
    Public Sub Update1Top()
        Assert.AreEqual(4, SB.Top)
    End Sub

    <Test()> _
    Public Sub Update2Bottom()
        Assert.AreEqual(22, SB.Bottom)
    End Sub

    <Test()> _
    Public Sub Update3Left()
        Assert.AreEqual(4, SB.Left)
    End Sub

    <Test()> _
    Public Sub Update4Right()
        Assert.AreEqual(19, SB.Right)
    End Sub

    <Test()> _
    Public Sub Update5Width()
        Assert.AreEqual(15, SB.Width)
    End Sub

    <Test()> _
    Public Sub Update6Height()
        Assert.AreEqual(18, SB.Height)
    End Sub
End Class

#End If