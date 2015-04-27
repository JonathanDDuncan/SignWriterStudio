Imports System.Drawing
Imports NUnit.Framework
Imports NUnit.Framework.Constraints
Imports SignWriterStudio.SWClasses.SWDrawing
Imports System.Drawing.Imaging

#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
#If NUnitTest Then
<TestFixture()> _
	Public Class SWDrawingTests
	Inherits AssertionHelper
    <Test()> _
  Public Shared Sub ColorSymbolTest2Colors1()
        Dim Bmp As Bitmap
        Dim g As Graphics
        Dim originalColor As Color = Color.Black
        Dim finalColor As Color = Color.Red

        Dim changedImage As Image

        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor)
        g.Dispose()
        changedImage = ColorSymbol(Bmp, originalColor, finalColor)

        Assert.AreEqual(Color.FromArgb(finalColor.ToArgb), CType(changedImage, Bitmap).GetPixel(1, 1))
    End Sub
	
	<Test()> _
		Public Shared Sub ColorSymbolTest2ColorsFail1()
		Dim Bmp As Bitmap
		Dim g As Graphics
		Dim originalColor As Color = Color.Black
		Dim finalColor As Color = Color.Red
		
		Dim changedImage As Image
		
		Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
		g = Graphics.FromImage(Bmp)
		g.Clear(originalColor)
        g.Dispose()
		changedImage = ColorSymbol(Bmp, originalColor, finalColor)
		
		Assert.AreNotEqual(Color.FromArgb(originalColor.ToArgb), CType(changedImage, Bitmap).GetPixel(1, 1))
	End Sub
	<Test()> _
		Public Shared Sub ColorSymbolTest4Colors1()
		Dim Bmp As Bitmap
		Dim g As Graphics
		Dim originalColor1 As Color = Color.Black
		Dim finalColor1 As Color = Color.Red
		Dim originalColor2 As Color = Color.Orange
		Dim finalColor2 As Color = Color.AliceBlue
		
        Dim changedImage1 As Image
        Dim changedImage2 As Image

		Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
		g = Graphics.FromImage(Bmp)
        g.Clear(originalColor1)
        g.Dispose()
        changedImage1 = ColorSymbol(Bmp, originalColor1, finalColor1)
		
        Assert.AreEqual(Color.FromArgb(finalColor1.ToArgb), CType(changedImage1, Bitmap).GetPixel(1, 1))

        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor2)
        g.Dispose()
        changedImage2 = ColorSymbol(Bmp, originalColor2, finalColor2)

        Assert.AreEqual(Color.FromArgb(finalColor2.ToArgb), CType(changedImage2, Bitmap).GetPixel(1, 1))
	End Sub
	
	<Test()> _
		Public Shared Sub ColorSymbolTest4ColorsFail1()
		Dim Bmp As Bitmap
		Dim g As Graphics
		Dim originalColor1 As Color = Color.Black
		Dim finalColor1 As Color = Color.Red
		Dim originalColor2 As Color = Color.Orange
		Dim finalColor2 As Color = Color.AliceBlue
		
        Dim changedImage1 As Image
        Dim changedImage2 As Image
		
		Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
		g = Graphics.FromImage(Bmp)
        g.Clear(originalColor1)
        g.Dispose()
        changedImage1 = ColorSymbol(Bmp, originalColor1, finalColor1)
		
        Assert.AreNotEqual(Color.FromArgb(originalColor1.ToArgb), CType(changedImage1, Bitmap).GetPixel(1, 1))
        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor2)
        g.Dispose()
        changedImage2 = ColorSymbol(Bmp, originalColor2, finalColor2)

        Assert.AreNotEqual(Color.FromArgb(originalColor2.ToArgb), CType(changedImage2, Bitmap).GetPixel(1, 1))
    End Sub

    <Test()> _
 Public Shared Sub ContainsColorTest1()
        Dim Bmp As Bitmap
        Dim g As Graphics
        Dim originalColor As Color = Color.Black

        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor)
        g.Dispose()
        Assert.AreEqual(True, ContainsColor(Bmp, originalColor))
    End Sub
     <Test()> _
    Public Shared Sub ContainsColorTest2()
        Dim Bmp As Bitmap
        Dim g As Graphics
        Dim originalColor As Color = Color.Black
        Dim finalColor As Color = Color.Red

        Dim changedImage As Image

        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor)
        g.Dispose()
        changedImage = ColorSymbol(Bmp, originalColor, finalColor)

        Assert.AreEqual(True, ContainsColor(changedImage, finalColor))
    End Sub
 <Test()> _
    Public Shared Sub ContainsColorTest1Fail()
        Dim Bmp As Bitmap
        Dim g As Graphics
        Dim originalColor As Color = Color.Black

        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor)
        g.Dispose()
        Assert.AreEqual(False, ContainsColor(Bmp, Color.White))
    End Sub
     <Test()> _
    Public Shared Sub ContainsColorTest2Fail()
        Dim Bmp As Bitmap
        Dim g As Graphics
        Dim originalColor As Color = Color.Black
        Dim finalColor As Color = Color.Red

        Dim changedImage As Image

        Bmp = New Bitmap(10, 10, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        g.Clear(originalColor)
        g.Dispose()
        changedImage = ColorSymbol(Bmp, originalColor, finalColor)

        Assert.AreEqual(False, ContainsColor(changedImage, originalColor))
    End Sub
End Class
#End If


