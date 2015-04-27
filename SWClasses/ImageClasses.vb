Option Strict On
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Windows.Forms
Imports SignWriterStudio.SymbolCache.SWSymbolCache
Imports SignWriterStudio.General.All
Imports SignWriterStudio.SWS
Imports System.Linq
Imports EQATEC.Analytics.Monitor
Imports NUnit.Framework
#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
''' <summary>
''' Class SWDrawing description
''' </summary>
Public NotInheritable Class SWDrawing
	Private Sub New()
		
	End Sub
	Public Shared Sub SetClipboardImage( _
		ByVal swSignImage As System.Drawing.Image)
		'Dim returnImage As System.Drawing.Image = Nothing
		If SWSignImage IsNot Nothing Then
			Clipboard.SetImage(CType(SWSignImage, Image))
		End If
	End Sub
    Public Shared Function DrawSWDrawing(ByVal sign As SwSign, ByVal paddingWidth As Integer, Optional ByVal grid As Boolean = False) As Image 'ByVal DT As DataTable, ByRef PB As PictureBox, ByVal isSelected As Boolean, ByVal BkColor As Color, ByVal Crop As Integer) As Point
        If sign Is Nothing Then
            Throw New ArgumentNullException("sign")
        End If
        Return DrawSWDrawing(sign, sign.CurrentFrameIndex, paddingWidth, grid)
    End Function
    Public Shared Function DrawSWDrawing(ByVal sign As SwSign, ByVal frameIndex As Integer, ByVal paddingWidth As Integer, ByVal grid As Boolean) As Image 'ByVal DT As DataTable, ByRef PB As PictureBox, ByVal isSelected As Boolean, ByVal BkColor As Color, ByVal Crop As Integer) As Point
        Dim SignLeft As Integer = SWFrame.FrameWidth
        Dim SignTop As Integer = SWFrame.FrameHeight
        'Dim SignRight As Integer = 0
        'Dim SignBottom As Integer = 0
        Dim CropRectangle As Rectangle
        Dim Frame As SWFrame
        Dim BMTemp As Bitmap
        Dim SymbolImage As Image
        Dim g As Graphics
        Dim imageAttributes As New ImageAttributes()
        Dim FrameImage As New SwCollection(Of Image)
        Dim StartFrameIndex As Integer
        Dim EndFrameIndex As Integer
        Dim CurrentFrameIndex As Integer


        If frameIndex = -1 Then
            'DrawAllFramesTogether
            StartFrameIndex = 0
            EndFrameIndex = sign.Frames.Count - 1
        Else
            'DrawSpecific Frame
            StartFrameIndex = frameIndex
            EndFrameIndex = frameIndex
        End If

        For CurrentFrameIndex = StartFrameIndex To EndFrameIndex
            Frame = sign.Frames(CurrentFrameIndex)
            BMTemp = New Bitmap(SignLeft, SignTop, PixelFormat.Format32bppPArgb)
            g = Graphics.FromImage(BMTemp)
            'CheckSymbolsinFrame(Frame)

            If Frame.SelectedSymbolCount > 0 Then
                g.Clear(Color.White)
            Else
                g.Clear(sign.BkColor)
            End If
            If grid Then
                DrawGrid(g, BMTemp.Width, BMTemp.Height)
            End If
            Frame.SignSymbols.Sort()

            Dim SortedFramSymbols = Frame.SignSymbols.OrderBy(Function(x) x.Z).ToList()
            'Dim Colors As SWCollection(Of Color)
            Dim I As Integer
            If SortedFramSymbols.Count = 0 Then
                Dim EmptyBitmap As New Bitmap(20, 20, PixelFormat.Format32bppPArgb)
                Dim gEmpty As Graphics = Graphics.FromImage(EmptyBitmap)
                gEmpty.Clear(Color.Cornsilk)
                FrameImage.Add(CType(EmptyBitmap, Image))
            Else
                Dim Symb As SWSignSymbol

                For I = 0 To SortedFramSymbols.Count - 1
                    Symb = SortedFramSymbols(I)

                    If Symb.SignSymbol Is Nothing Then
                        SymbolImage = ChooseColorsSize(Symb, Frame.SelectedSymbolCount)
                        Symb.SetSignSymbol(SymbolImage)
                    Else
                        SymbolImage = Symb.SignSymbol
                    End If
                    If SymbolImage IsNot Nothing Then
                        g.DrawImage(SymbolImage, New Rectangle(Symb.X, Symb.Y, SymbolImage.Width, SymbolImage.Height), 0, 0, SymbolImage.Width, SymbolImage.Height, Drawing.GraphicsUnit.Pixel, imageAttributes)
                    Else
                        MessageBox.Show("Symbol code number " & Symb.Code & " was not found in the symbol database")
                    End If
                Next
                CropRectangle = GetFrameBounds(Frame)
                If paddingWidth > -1 Then

                    CropRectangle = New Rectangle(CInt(CropRectangle.X - paddingWidth / 2), CInt(CropRectangle.Y - paddingWidth / 2), CropRectangle.Width + paddingWidth, CropRectangle.Height + paddingWidth)
                    If CropRectangle.Height > 0 AndAlso CropRectangle.Width > 0 Then
                        Dim cropedBitmap As New Bitmap(CropRectangle.Width, CropRectangle.Height, PixelFormat.Format32bppPArgb)
                        Dim gCrop As Graphics
                        gCrop = Graphics.FromImage(cropedBitmap)
                        gCrop.DrawImage(BMTemp, New Rectangle(0, 0, CropRectangle.Width, CropRectangle.Height), CropRectangle, GraphicsUnit.Pixel)

                        BMTemp = cropedBitmap
                    End If
                End If

                'Colors = ColorsContained(BMTemp)
                sign.Frames(CurrentFrameIndex).CropPoint = New Point(CropRectangle.X, CropRectangle.Y)
                FrameImage.Add(CType(BMTemp, Image))
            End If
            g.Dispose()
        Next
        If frameIndex = -1 And CurrentFrameIndex = 1 Then
            Return FrameImage(0)
        ElseIf frameIndex = -1 Then
            Return JoinFrameImages(FrameImage)
        Else
            Return FrameImage(0)
        End If
    End Function

    Private Shared Sub DrawGrid(ByVal g As Graphics, width As Integer, height As Integer)
        DrawHorizontalLines(g, width, height)
        DrawVerticalLines(g, width, height)
    End Sub

    Private Shared Sub DrawVerticalLines(ByVal g As Graphics, width As Integer, height As Integer)
        VerticalLines(g, New Pen(Color.LightGray), height, width, 10)
        VerticalLines(g, New Pen(Color.DarkGray, 2), height, width, 100)
        VerticalLines(g, New Pen(Color.Black, 1), height, width, 250)
    End Sub

    Private Shared Sub VerticalLines(ByVal g As Graphics, ByVal lightpen As Pen, ByVal height As Integer, ByVal width As Integer, ByVal step1 As Integer)
        For I = 0 To height Step step1
            g.DrawLine(lightpen, New Point(0, I), New Point(width, I))
        Next
    End Sub

    Private Shared Sub DrawHorizontalLines(ByVal g As Graphics, width As Integer, height As Integer)
        HorizontalLines(g, New Pen(Color.LightGray), width, height, 10)
        HorizontalLines(g, New Pen(Color.DarkGray, 2), width, height, 100)
        HorizontalLines(g, New Pen(Color.Black, 1), width, height, 250)
    End Sub

    Private Shared Sub HorizontalLines(ByVal g As Graphics, ByVal pen As Pen, ByVal width As Integer, ByVal height As Integer, ByVal step1 As Integer)
        For I = 0 To width Step step1
            g.DrawLine(pen, New Point(I, 0), New Point(I, height))
        Next
    End Sub

    Private Shared Function ChooseColorsSize(ByVal Symb As SWSignSymbol, ByVal SymbolsSelected As Integer) As Image
        Dim symbolImage As Image = Symb.SymbolDetails.SymImage
        If symbolImage IsNot Nothing Then

            Dim SelectedColor As Color = Color.BlueViolet
            Dim UnselectedColor As Color = Color.Black
            If SymbolsSelected > 0 AndAlso symbolImage IsNot Nothing AndAlso Not Symb.StandardColor.IsEmpty Then
                If Symb.IsSelected Then
                    symbolImage = ColorSymbol(symbolImage, Symb.StandardColor, SelectedColor)
                ElseIf Not Symb.IsSelected Then
                    symbolImage = ColorSymbol(symbolImage, Symb.StandardColor, UnselectedColor)
                End If
            Else
                symbolImage = ColorSymbol(symbolImage, Symb.StandardColor, Color.FromArgb(Symb.Handcolor), Color.White, Color.FromArgb(Symb.Palmcolor))
            End If

            If Not Math.Abs(Symb.Size - 1) < 0.01 Then
                symbolImage = ChangeSymbolSize(symbolImage, Symb.Size)
            End If

        End If
        Return symbolImage
    End Function
    Public Shared Function ReplaceColor(ByVal toReplace1 As Color, ByVal replaceBy1 As Color, ByVal toReplace2 As Color, ByVal replaceBy2 As Color) As Imaging.ImageAttributes
        Dim ICM As Imaging.ColorMap() = (New Imaging.ColorMap() {MakeColorMap(toReplace1, replaceBy1), MakeColorMap(toReplace2, replaceBy2)})
        Dim IA As New Imaging.ImageAttributes()
        IA.SetRemapTable(ICM, Imaging.ColorAdjustType.Bitmap)
        Return IA
    End Function
    Public Shared Function ReplaceColor(ByVal toReplace1 As Color, ByVal replaceBy1 As Color) As Imaging.ImageAttributes
        Dim ICM As Imaging.ColorMap() = (New Imaging.ColorMap() {MakeColorMap(toReplace1, replaceBy1)})
        Dim IA As New Imaging.ImageAttributes()
        IA.SetRemapTable(ICM, Imaging.ColorAdjustType.Bitmap)
        Return IA
    End Function
    Public Shared Function ContainsColor(ByVal img As Image, ByVal color1 As Color) As Boolean
        Dim Entries As New List(Of Integer)
        Dim bmp As Bitmap = CType(img, Bitmap)
        Dim PixColor As color
        For I = 0 To bmp.Width - 1
            For J = 0 To bmp.Height - 1
                PixColor = bmp.GetPixel(I, J)
                If Not entries.Contains(PixColor.ToArgb) Then
                    entries.Add(PixColor.ToArgb)
                    '					messagebox.Show ((PixColor.ToArgb).ToString )
                End If
            Next
        Next

        If entries.Contains(color1.ToArgb) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ContainsColor(ByVal bmp As Bitmap, ByVal color1 As Color) As Boolean
        Dim Entries As New List(Of Integer)
        Dim PixColor As color
        For I = 0 To bmp.Width - 1
            For J = 0 To bmp.Height - 1
                PixColor = bmp.GetPixel(I, J)
                If Not entries.Contains(PixColor.ToArgb) Then
                    entries.Add(PixColor.ToArgb)
                    '					messagebox.Show ((PixColor.ToArgb).ToString )
                End If
            Next
        Next

        If entries.Contains(color1.ToArgb) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ColorsContained(ByVal bmp As Bitmap) As SwCollection(Of Color)
        Dim Entries As New SWCollection(Of Color)
        Dim PixColor As Color
        For I = 0 To bmp.Width - 1
            For J = 0 To bmp.Height - 1
                PixColor = bmp.GetPixel(I, J)
                If Not Entries.Contains(Color.FromArgb(PixColor.ToArgb)) Then
                    Entries.Add(Color.FromArgb(PixColor.ToArgb))
                    '					messagebox.Show ((PixColor.ToArgb).ToString )
                End If
            Next
        Next
        Return Entries
    End Function

    Public Shared Function JoinFrameImages(ByVal frameImages As SwCollection(Of Image)) As Image
        Dim FinalHeight As Integer ' = 0
        Dim FinalWidth As Integer = 1
        Dim FrameImage As Image
        Dim TopofFrame As Integer ' = 0
        Dim Bmp As Bitmap
        Dim g As Graphics

        For I As Integer = 0 To frameImages.Count - 1
            FrameImage = frameImages(I)
            If FrameImage.Width > FinalWidth Then
                FinalWidth = FrameImage.Width
            End If
            FinalHeight += FrameImage.Height
        Next

        Bmp = New Bitmap(FinalWidth, FinalHeight, PixelFormat.Format32bppPArgb)
        g = Graphics.FromImage(Bmp)
        For I As Integer = 0 To frameImages.Count - 1
            FrameImage = frameImages(I)
            g.DrawImage(FrameImage, New Rectangle(CInt((Bmp.Width - FrameImage.Width) / 2), TopofFrame, FrameImage.Width, FrameImage.Height), 0, 0, FrameImage.Width, FrameImage.Height, GraphicsUnit.Pixel)
            TopofFrame += FrameImage.Height
        Next
        g.Dispose()
        Return CType(Bmp, Image)
    End Function

    Public Shared Function ChangeSymbolSize(ByVal swDrawing As Image, ByVal imageScale As Double, ByVal imageAttr As ImageAttributes) As Image

        If swDrawing Is Nothing Then
            Throw New ArgumentNullException("swDrawing")
        End If
        If imageAttr Is Nothing Then
            Throw New ArgumentNullException("imageAttr")
        End If

        Dim Bmp As Bitmap

        Bmp = New Bitmap(CInt(swDrawing.Width * imageScale * 2), CInt(swDrawing.Height * imageScale * 2), PixelFormat.Format32bppPArgb)
        Dim g As Graphics
        g = Graphics.FromImage(Bmp)
        g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)

        Bmp = New Bitmap(CInt(swDrawing.Width * imageScale), CInt(swDrawing.Height * imageScale), PixelFormat.Format32bppPArgb)
        g.Dispose()
        g = Graphics.FromImage(Bmp)
        g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)
        g.Dispose()
        Return CType(Bmp, Image)
    End Function
    Public Shared Function ChangeSymbolSize(ByVal swDrawing As Image, ByVal imageScale As Double) As Image
        Dim imageAttr As ImageAttributes
        If swDrawing Is Nothing Then
            Throw New ArgumentNullException("swDrawing")
        End If

        Dim Bmp As Bitmap
        If Math.Abs(imageScale - 0.0R) < Double.Epsilon Then imageScale = 1 'Can't create bitmap with height or width of 0
        Bmp = New Bitmap(CInt(swDrawing.Width * imageScale * 2), CInt(swDrawing.Height * imageScale * 2), PixelFormat.Format32bppPArgb)
        Dim g As Graphics
        g = Graphics.FromImage(Bmp)
        g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)

        Bmp = New Bitmap(CInt(swDrawing.Width * imageScale), CInt(swDrawing.Height * imageScale), PixelFormat.Format32bppPArgb)
        g.Dispose()
        g = Graphics.FromImage(Bmp)
        g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)
        g.Dispose()
        Return CType(Bmp, Image)
    End Function
    '	''' <summary>
    '	''' Function ColorSymbol description
    '	''' </summary>
    '	Public Shared Function ColorSymbol(ByVal swDrawing As Image, ByVal imageAttr As ImageAttributes) As Image
    '		'Require Test parameters
    '		'TODO: Add requirement test.
    '		#If AssertTest Then
    '		If swDrawing Is  Nothing Then
    '			Throw new AssertionException ("You need to supply an Image object to ColorSymbol.")
    '		End If
    '		#End If
    '
    '
    '		Dim Bmp As Bitmap
    '		Dim g As Graphics
    '
    '		'TODO try to change to indexed pixelformat
    '		Bmp = New Bitmap(swDrawing.Width, swDrawing.Height, PixelFormat.Format32bppPArgb)
    '		g = Graphics.FromImage(Bmp)
    '		g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)
    '
    '		Return CType(Bmp, Image)
    '	End Function
    '

    ''' <summary>
    ''' Function ColorSymbol description
    ''' </summary>
	Public Shared Function ColorSymbol(ByVal swDrawing As Image, ByVal originalColor1 As Color, ByVal finalColor1 As Color) As Image
		'Require Test parameters
		'TODO: Add requirement test.
		#If AssertTest Then
		If swDrawing Is Nothing Then
			Throw new AssertionException ("You need to supply an Image object to ColorSymbol.")
		End If
		If originalColor1.IsEmpty  Then
			Throw new AssertionException ("You need to supply an original color to ColorSymbol.")
		End If
		If finalColor1.IsEmpty  Then
			Throw new AssertionException ("You need to supply an final color to ColorSymbol.")
		End If
		#End If
		
		Dim imageAttr As ImageAttributes = ReplaceColor(originalColor1, finalColor1)
		
		
		Dim g As Graphics
		
		'TODO try to change to indexed pixelformat
		Dim Bmp As New Bitmap(swDrawing.Width, swDrawing.Height, PixelFormat.Format32bppPArgb)
		g = Graphics.FromImage(Bmp)
		g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)
        g.Dispose()
		'Ensure Test return value
#If AssertTest Then
        'TODO test assertion
        'If ContainsColor(Bmp, originalColor1) Then
        '	Throw new AssertionException ("Color change error first original color still present" & originalColor1.ToString )
        'End If
        'If Not ContainsColor(Bmp, finalColor1) Then
        '	Throw new AssertionException ("Color change error first final color not present" & finalColor1.ToString )
        'End If

#End If
		Return CType(Bmp, Image)
	End Function
	
	
	''' <summary>
	''' Function ColorSymbol description
	''' </summary>
	Public Shared Function ColorSymbol(ByVal swDrawing As Image, ByVal originalColor1 As Color, ByVal finalColor1 As Color, ByVal originalColor2 As Color, ByVal finalColor2 As Color) As Image
		'Require Test parameters
		'TODO: Add requirement test.
		#If AssertTest Then
		If swDrawing Is  Nothing Then
			Throw new AssertionException ("You need to supply an Image object to ColorSymbol.")
		End If
        'If originalColor1.IsEmpty Then
        '	Throw new AssertionException ("You need to supply an original color to ColorSymbol.")
        'End If
        'If finalColor1.IsEmpty Then
        '	Throw new AssertionException ("You need to supply an final color to ColorSymbol.")
        'End If
        'If originalColor2.IsEmpty Then
        '	Throw new AssertionException ("You need to supply an original color to ColorSymbol.")
        'End If
        'If finalColor2.IsEmpty Then
        '	Throw new AssertionException ("You need to supply an final color to ColorSymbol.")
        'End If
		#End If
		
		Dim imageAttr As ImageAttributes = ReplaceColor(originalColor1, finalColor1, originalColor2, finalColor2)
		
		
		Dim g As Graphics
		
		'TODO try to change to indexed pixelformat
		Dim Bmp As New Bitmap(swDrawing.Width, swDrawing.Height, PixelFormat.Format32bppPArgb)
		g = Graphics.FromImage(Bmp)
		g.DrawImage(swDrawing, New Rectangle(0, 0, Bmp.Width, Bmp.Height), 0, 0, swDrawing.Width, swDrawing.Height, GraphicsUnit.Pixel, imageAttr)
        g.Dispose()
		'Ensure Test return value
#If AssertTest Then
        'TODO check assertions
        'If ContainsColor(Bmp, originalColor1) Then
        '	Throw new AssertionException ("Color change error first original color still present" & originalColor1.ToString  )
        'End If
        'If Not ContainsColor(Bmp, finalColor1) Then
        '    Throw New AssertionException("Color change error first final color not present" & finalColor1.ToString)
        'End If
        'If ContainsColor(Bmp, originalColor2) Then
        '	Throw new AssertionException ("Color change error second original color still present " & originalColor2.ToString )
        'End If

        'If Not ContainsColor(Bmp, finalColor2) Then
        '	Throw new AssertionException ("Color change error secand final color not present " & finalColor2.ToString )
        'End If
#End If
		
		Return CType(Bmp, Image)
	End Function
	
	
	Public Shared Function CropImage(ByVal fromImage As Image, ByVal cropRectangle As Rectangle) As Image
		Dim CropedBitmap As Bitmap
		If cropRectangle.Height > 0 And cropRectangle.Width > 0 Then
			CropedBitmap = New Bitmap(cropRectangle.Width, cropRectangle.Height, PixelFormat.Format32bppPArgb)
			Dim g As Graphics
			g = Graphics.FromImage(CropedBitmap)
            g.DrawImage(fromImage, New Rectangle(0, 0, cropRectangle.Width, cropRectangle.Height), cropRectangle, GraphicsUnit.Pixel)
            g.Dispose()
			Return CType(CropedBitmap, Image)
		Else
			Return Nothing
		End If
		
	End Function
	Public Shared Function GetRemapTable(ByVal colorMap As Imaging.ColorMap) As Imaging.ColorMap()
		Dim remapTable As Imaging.ColorMap() = {colorMap}
		Return remapTable
	End Function
	Public Shared Function MakeColorMap(ByVal oldColor As Color, ByVal newColor As Color) As Imaging.ColorMap
		Dim Colormap As New Imaging.ColorMap
		Colormap.OldColor = oldColor
		Colormap.NewColor = newColor
		Return Colormap
	End Function
	Public Shared Function GetFrameBounds(ByVal frame As SWFrame) As Rectangle
        Dim EachSymbol As SWSignSymbol
        Dim SymbolDetails As SWSymbol
        Dim x1 As Integer = Integer.MaxValue
		Dim x2 As Integer = 0
        Dim y1 As Integer = Integer.MaxValue
		Dim y2 As Integer = 0
		
        For Each EachSymbol In frame.SignSymbols
            SymbolDetails = EachSymbol.SymbolDetails(False)
            If EachSymbol.X <= x1 Then
                x1 = EachSymbol.X
            End If
            If EachSymbol.Y <= y1 Then
                y1 = EachSymbol.Y
            End If

            If EachSymbol.X + SymbolDetails.Width * EachSymbol.Size >= x2 Then
                x2 = CInt(EachSymbol.X + SymbolDetails.Width * EachSymbol.Size)
            End If
            If EachSymbol.Y + SymbolDetails.Height * EachSymbol.Size >= y2 Then
                y2 = CInt(EachSymbol.Y + SymbolDetails.Height * EachSymbol.Size)
            End If

        Next
        If x1 = Integer.MaxValue Then
            y1 = 0
        End If
        If y1 = Integer.MaxValue Then
            x1 = 0
        End If
		Return New Rectangle(x1, y1, x2 - x1, y2 - y1)
	End Function
	
	Public Shared Function ResizeImage(ByVal image1 As Image, ByVal width1 As Integer, ByVal height1 As Integer) As Image
		Dim Image2 As Image = image1
		Dim ImgHeight As Integer = Image2.Height
		Dim ImgWidth As Integer = Image2.Width
		Dim ScaleFact As Double
		Dim NewImgWidth As Integer
		Dim NewImgHeight As Integer
		If ImgHeight >= ImgWidth Then
			ScaleFact = height1 / ImgHeight
		ElseIf ImgHeight <= ImgWidth Then
			ScaleFact = width1 / ImgWidth
		End If
		NewImgWidth = CInt(ImgWidth * ScaleFact)
		NewImgHeight = CInt(ImgHeight * ScaleFact)
		Return Image2.GetThumbnailImage(NewImgWidth, NewImgHeight, Nothing, CType(0, System.IntPtr))
		
	End Function
	Public Shared Function IsImage(ByVal obj As Object) As Boolean
		
		If obj Is Nothing Then
			Throw New ArgumentNullException("obj")
		End If
		If obj Is Nothing Then
			
			Return False
		ElseIf obj.GetType.ToString = "System.Bitmap" Or obj.GetType.ToString = "System.Image" Then
			Return True
		ElseIf obj.GetType.ToString = "System.Byte[]" Then
            Dim img As Image = ByteArraytoImage(CType(obj, Byte()))
			If img Is Nothing Then
				Return False
			Else
				Return True
			End If
			
		Else
			Return False
			
		End If
	End Function
	'TODO Check Redesign works Changed from Sub to Function
	Public Shared Function AddImagestoImageList(ByVal symbolsImageList As ImageList, ByVal code As Integer, ByVal iconWidth As Integer, ByVal iconHeight As Integer, ByVal selectedColor As Color) As ImageList
		Dim Symbol As New SWSymbol With {.Code = code}
		symbolsImageList = AddImagestoImageList(symbolsImageList, Symbol.Id, iconWidth, iconHeight, selectedColor)
		Return symbolsImageList
	End Function
	Private Shared Function GetImageAttributes(ByVal selectedColor As Color) As Imaging.ImageAttributes
		Dim imageAttributes As New Imaging.ImageAttributes()
		Dim Colormap As New Imaging.ColorMap
		With Colormap
			.OldColor = Color.FromArgb(-16777216) 'GetStandardColor(Symbol) ' )
			.NewColor = selectedColor
		End With
		Dim remapTable As Imaging.ColorMap() = {Colormap}
		imageAttributes.SetRemapTable(remapTable, Imaging.ColorAdjustType.Bitmap)
		Return imageAttributes
		
	End Function
	'TODO Check Redesign works Changed from Sub to Function
	Public Shared Function AddImagestoImageList(ByVal symbolsImageList As ImageList, ByVal sss As String, ByVal iconHeight As Integer, ByVal iconWidth As Integer, ByVal selectedColor As Color) As ImageList
		If symbolsImageList Is Nothing Then
			Throw New ArgumentNullException("symbolsImageList")
		End If
		If sss Is Nothing Then
			Throw New ArgumentNullException("sss")
		End If
		
		'Set up new ImageList
		If symbolsImageList.Images.Count = 0 Then
			symbolsImageList.ColorDepth = ColorDepth.Depth8Bit
			symbolsImageList.TransparentColor = Nothing
			symbolsImageList.ImageSize = New Size(iconWidth, iconHeight)
		End If
		
		' Load the images in an ImageList.
		
		
		'Color attributes
		Dim imageAttributes As Imaging.ImageAttributes = GetImageAttributes(selectedColor)
		Dim Image1 As Image = SymbolCache.SWSymbolCache.GetImagebyId(sss)
		symbolsImageList.Images.Add(sss, CreateImageListIcon(Image1, iconWidth, iconHeight, Color.Transparent, Nothing))
		symbolsImageList.Images.Add("S" & sss, CreateImageListIcon(Image1, iconWidth, iconHeight, Color.Transparent, imageAttributes))
		Return symbolsImageList
	End Function
	Public Shared Function AddImagestoImageList(ByVal symbolsImageList As ImageList, ByVal favoriteText As String, ByVal image1 As Image, ByVal iconHeight As Integer, ByVal iconWidth As Integer) As ImageList
		If symbolsImageList Is Nothing Then
			Throw New ArgumentNullException("symbolsImageList")
		End If
		If FavoriteText Is Nothing Then
			Throw New ArgumentNullException("FavoriteText")
		End If
		
		'Set up new ImageList
		If symbolsImageList.Images.Count = 0 Then
			symbolsImageList.ColorDepth = ColorDepth.Depth8Bit
			symbolsImageList.TransparentColor = Nothing
			symbolsImageList.ImageSize = New Size(iconWidth, iconHeight)
		End If
		
		' Load the images in an ImageList.
		
		
		'Color attributes
		symbolsImageList.Images.Add(FavoriteText, CreateImageListIcon(Image1, iconWidth, iconHeight, Color.Transparent, Nothing))
		symbolsImageList.Images.Add("S" & FavoriteText, CreateImageListIcon(Image1, iconWidth, iconHeight, Color.Transparent, New Imaging.ImageAttributes))
		Return symbolsImageList
		
	End Function
    Public Shared Function LoadImageList(ByVal symbolsImageList As ImageList, ByVal dtSymbols As SignWriterStudio.SymbolCache.ISWA2010DataSet.cacheRow(), ByVal iconHeight As Integer, ByVal iconWidth As Integer, ByVal selectedColor As Color, Optional CreateSelected As Boolean = True) As ImageList
        If symbolsImageList Is Nothing Then
            Throw New ArgumentNullException("symbolsImageList")
        End If
        If dtSymbols Is Nothing Then
            Throw New ArgumentNullException("dtSymbols")
        End If

        If symbolsImageList.Images.Count = 0 Then
            symbolsImageList.ColorDepth = ColorDepth.Depth8Bit
            symbolsImageList.TransparentColor = Nothing
            symbolsImageList.ImageSize = New Size(iconWidth, iconHeight)
        End If

        Dim DTSymbolsRow As SymbolCache.ISWA2010DataSet.cacheRow
        ' Load the images in an ImageList.

        'IF DTSymbols has rows
        If dtSymbols.Length > 0 Then
            'Get first row
            DTSymbolsRow = CType(dtSymbols(0), SymbolCache.ISWA2010DataSet.cacheRow)
        End If
        'Catch ex As ArgumentException

        '    'Doesn't have an image row.  Get info symbol by symbol from cache
        '    GetIndividualInfo = True
        'Catch ex As Exception
        '    monitor.TrackException(ex, _
        '                      TraceEventType.Error.ToString.ToString, _
        '                      "Exception ")
        '    My.Application.Log.WriteException(ex, _
        '                      TraceEventType.Error, _
        '                      "Exception ")
        'End Try


        Dim Row As SymbolCache.ISWA2010DataSet.cacheRow
        Dim IDSSS As String
        Dim Code As Integer
        For Each DTSymbolsRow In dtSymbols

            Code = DTSymbolsRow.sym_code
            Dim symbol As New SWSymbol With {.Code = Code}
           
            IDSSS = symbol.Id
            Row = SymbolCache.Iswa2010.SC.GetCode(Code)(0)
            If Not IsDBNull(Row.sym_png) Then
                Dim Image1 As Image = ByteArraytoImage(CType(Row.sym_png, Byte()))
                symbolsImageList.Images.Add(IDSSS, CreateImageListIcon(Image1, iconWidth, iconHeight, Color.Transparent, Nothing))

                If CreateSelected Then
                    'Color attributes
                    Dim imageAttributes As New Imaging.ImageAttributes()
                    Dim Colormap As New Imaging.ColorMap
                    Colormap.OldColor = symbol.StandardColor 'Color.FromArgb(-16777216) 'GetStandardColor(Symbol) ' )
                    Colormap.NewColor = selectedColor
                    Dim remapTable As Imaging.ColorMap() = {Colormap}
                    imageAttributes.SetRemapTable(remapTable, Imaging.ColorAdjustType.Bitmap)



                    symbolsImageList.Images.Add("S" & IDSSS, CreateImageListIcon(Image1, iconWidth, iconHeight, Color.Transparent, imageAttributes))
                End If
            End If
        Next
        Return symbolsImageList
    End Function
	Public Shared Function CreateImageListIcon(ByVal image1 As Image, ByVal iconWidth As Integer, ByVal iconHeight As Integer, ByVal backgroundColor As Color, ByVal imageAttributes As Imaging.ImageAttributes) As Bitmap
		If image1 Is Nothing Then
			Throw New ArgumentNullException("image1")
		End If
		If imageAttributes Is Nothing Then
			'Throw New ArgumentNullException("imageAttributes")
		End If
		
		Dim Bitmap1 As New Bitmap(iconWidth, iconHeight, PixelFormat.Format32bppPArgb)
		
		Dim g As Graphics
		g = Graphics.FromImage(Bitmap1)
		'Do normal image
		g.Clear(backgroundColor)
		
		If imageAttributes Is Nothing Then
			g.DrawImage(image1, New Drawing.Rectangle((iconWidth - image1.Width) \ 2, (iconHeight - image1.Height) \ 2, image1.Width, image1.Height), 0, 0, image1.Width, image1.Height, Drawing.GraphicsUnit.Pixel)
		Else
			g.DrawImage(image1, New Drawing.Rectangle((iconWidth - image1.Width) \ 2, (iconHeight - image1.Height) \ 2, image1.Width, image1.Height), 0, 0, image1.Width, image1.Height, Drawing.GraphicsUnit.Pixel, imageAttributes)
        End If
        g.Dispose()
		Return Bitmap1
	End Function
	
	Public Shared Function GetOppositeColor(ByVal fromColor As Color) As Color
		
		Dim A As Integer = fromColor.A
		Dim R As Integer = (256 - fromColor.R)
		Dim G As Integer = (256 - fromColor.G)
		Dim B As Integer = (256 - fromColor.B)
		A <<= 48
		R <<= 32
		G <<= 16
		
		Return Color.FromArgb(A + R + G + B)
		
		
	End Function
	Public Shared Function ConvertBW(ByVal bm As Bitmap) As Bitmap
		
		'Try
		
		Dim img As Bitmap = bm
		
		'Ensure that it's a 32 bit per pixel file
		
		If img.PixelFormat <> PixelFormat.Format32bppPArgb Then
			
			Dim temp As New Bitmap(img.Width, img.Height, PixelFormat.Format32bppPArgb)
			
			Dim g As Graphics = Graphics.FromImage(temp)
			
			g.DrawImage(img, New Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel)
			
			img.Dispose()
			
			g.Dispose()
			
			img = temp
			
		End If
		
		'lock the bits of the original bitmap
		
		Dim bmdo As BitmapData = img.LockBits(New Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadOnly, img.PixelFormat)
		
		
		
		'and the new 1bpp bitmap
		
		bm = New Bitmap(img.Width, img.Height, PixelFormat.Format1bppIndexed)
		
		Dim bmdn As BitmapData = bm.LockBits(New Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed)
		
		
		
		'for diagnostics
		
		'Dim dt As DateTime = DateTime.Now
		
		
		
		'scan through the pixels Y by X
		
		Dim y As Integer
		
		For y = 0 To img.Height - 1
			
			Dim x As Integer
			
			For x = 0 To img.Width - 1
				
				'generate the address of the colour pixel
				
				Dim index As Integer = y * bmdo.Stride + x * 4
				
				'check its brightness
				
				If Color.FromArgb(Marshal.ReadByte(bmdo.Scan0, index + 2), Marshal.ReadByte(bmdo.Scan0, index + 1), Marshal.ReadByte(bmdo.Scan0, index)).GetBrightness() > 0.5F Then
					
					SetIndexedPixel(x, y, bmdn, True) 'set it if its bright.
					
				End If
				
			Next x
			
		Next y
		
		'tidy up
		
		bm.UnlockBits(bmdn)
		
		img.UnlockBits(bmdo)
		
		
		
		'show the time taken to do the conversion
		
		' Dim ts As TimeSpan = dt.Subtract(DateTime.Now)
		
		'  System.Diagnostics.Trace.WriteLine(("Conversion time was:" + ts.ToString(System.Globalization.CultureInfo)))
		
		
		
		'display the 1bpp image.
		
		
		'Catch ex As Exception
		'    monitor.TrackException(ex, _
		'                      TraceEventType.Error.ToString.ToString, _
		'                      "Exception ")
		'    My.Application.Log.WriteException(ex, _
		'                      TraceEventType.Error, _
		'                      "Exception ")
		'End Try
		Return bm
	End Function
	Private Shared Sub SetIndexedPixel(ByVal x As Integer, ByVal y As Integer, ByVal bmd As BitmapData, ByVal pixel As Boolean)
		'Try
		
		
		Dim index As Integer = y * bmd.Stride + (x >> 3)
		
		Dim p As Byte = Marshal.ReadByte(bmd.Scan0, index)
		
		Dim mask As Byte = CByte(&H80 >> (x And &H7))
		
		If pixel Then
			
			p = p Or mask
			
		Else
			
			p = p And CByte(mask ^ &HFF)
			
		End If
		
		Marshal.WriteByte(bmd.Scan0, index, p)
		'Catch ex As Exception
		'    monitor.TrackException(ex, _
		'                      TraceEventType.Error.ToString.ToString, _
		'                      "Exception ")
		'    My.Application.Log.WriteException(ex, _
		'                      TraceEventType.Error, _
		'                      "Exception ")
		'End Try
	End Sub 'SetIndexedPixel
End Class