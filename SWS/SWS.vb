Imports SignWriterStudio.General.All
Imports SignWriterStudio.SymbolCache


<Serializable()> Public Class SWSymbol
    Implements ICloneable
    ' In this section you can add your own using directives
    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000844 begin
    ' section 127-0-0-1-12759f70:11b4c9e83f8:-8000:0000000000000844 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */

    ' Attributes
    Public Sub SetIds(ByVal code As Integer, ByVal id As String)
        _Code = code
        _ID = id

    End Sub
    Private _LoadImage As Boolean
    'Public Property LoadImage() As Boolean
    '    Get
    '        Return _LoadImage
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _LoadImage = value
    '    End Set
    'End Property
    Private _code As Integer
    Public Property Code() As Integer
        Get
            Return _Code
        End Get
        Set(ByVal value As Integer)
            _Code = value
            _LoadImage = True
            Me._ID = String.Empty
            Me.Update()


        End Set
    End Property
    Public Property CodeNotLoadImage As Integer
        Get
            Return _Code
        End Get
        Set(ByVal value As Integer)
            _Code = value
            _LoadImage = False
            Me._ID = String.Empty
            Me.Update()


        End Set
    End Property
    Private _ID As String
    Public Property Id() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
            _LoadImage = True
            Me._Code = 0
            Me.Update()
        End Set
    End Property
    Public Property IdNotLoadImage As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
            _LoadImage = False
            Me._Code = 0
            Me.Update()
        End Set
    End Property
    Private _isValid As Boolean '= False
    Public ReadOnly Property IsValid() As Boolean
        Get
            Return _isValid
        End Get
    End Property
    Private _baseGroup As Integer
    Public Property BaseGroup() As Integer
        Get
            Return _baseGroup
        End Get
        Set(ByVal value As Integer)
            _baseGroup = value
            'Me.ResetPreviousId()
        End Set
    End Property
    Private _group As Integer
    Public Property Group() As Integer
        Get
            Return _group
        End Get
        Set(ByVal value As Integer)
            _group = value
            Me.ResetPreviousId()
        End Set
    End Property

    Private _category As Integer
    Public Property Category() As Integer
        Get
            Return _category
        End Get
        Set(ByVal value As Integer)
            _category = value
            Me.ResetPreviousId()
        End Set
    End Property

    Private _symbol As Integer
    Public Property Symbol() As Integer
        Get
            Return _symbol
        End Get
        Set(ByVal value As Integer)
            _symbol = value
            Me.ResetPreviousId()
        End Set
    End Property
    Private _variation As Integer
    Public Property Variation() As Integer
        Get
            Return _variation
        End Get
        Set(ByVal value As Integer)
            _variation = value
            Me.ResetPreviousId()
        End Set
    End Property

    Private _fill As Integer
    Public Property Fill() As Integer
        Get
            Return _fill
        End Get
        Set(ByVal value As Integer)
            _fill = value
            Me.ResetPreviousId()
        End Set
    End Property
    Private _rotation As Integer
    Public Property Rotation() As Integer
        Get
            Return _rotation
        End Get
        Set(ByVal value As Integer)
            _rotation = value
            Me.ResetPreviousId()
        End Set
    End Property

    Private _width As Integer
    Public Property Width() As Integer
        Get
            Return _width
        End Get
        Set(ByVal value As Integer)
            _width = value
        End Set
    End Property
    Private _height As Integer
    Public Property Height() As Integer
        Get
            Return _height
        End Get
        Set(ByVal value As Integer)
            _height = value
        End Set
    End Property
    Private _baseName As String
    Public Property BaseName() As String
        Get
            Return _BaseName
        End Get
        Set(ByVal value As String)
            _BaseName = value
        End Set
    End Property
    Private _standardColor As Color
    Public Property StandardColor() As Color
        Get
            Return _standardColor
        End Get
        Set(ByVal value As Color)
            _standardColor = value
        End Set
    End Property
    Private _symImage As Image
    Public Property SymImage() As Image
        Get
            Return _SymImage
        End Get
        Set(ByVal value As Image)
            _SymImage = value
        End Set
    End Property
    Private _illusImage As Image
    Public Property Illustration() As Image
        Get
            Return _IllusImage
        End Get
        Set(ByVal value As Image)
            _IllusImage = value
        End Set
    End Property
    Private _sortWeight As String
    Public Property SortWeight() As String
        Get
            Return _sortWeight
        End Get
        Set(ByVal value As String)
            _sortWeight = value
        End Set
    End Property
     

    Public Shared Function CodefromId(ByVal id As String) As Integer
        Dim symbol As New SWSymbol With {.IdNotLoadImage = id}
        Return symbol.Code
    End Function
    Public Shared Function Fills(ByVal code As Integer) As Integer
        Return SC.GetFills(code)
    End Function
    Public Shared Function Rotations(ByVal code As Integer) As Integer
        Return SC.GetRotations(code)
    End Function
    Public Function Fills() As Integer
        Return Fills(Me.Code)
    End Function
    Public Function Rotations() As Integer
        Return Rotations(Me.Code)
    End Function
    Public Sub FromDataRow(ByVal tempDataRow() As SymbolCache.ISWA2010DataSet.cacheRow)
        ' section 127-0-0-1-fe1248e:11b50ee3448:-8000:0000000000000985 begin
        If tempDataRow IsNot Nothing AndAlso tempDataRow.Length > 0 Then
            Dim cacheRow As SymbolCache.ISWA2010DataSet.cacheRow = tempDataRow(0)
            If CInt(cacheRow.sym_code) > 0 Then
                Me.Category = cacheRow.sg_cat_num
                Me.Group = cacheRow.sg_grp_num
                Me.Symbol = cacheRow.bs_bas_num
                Me.Variation = cacheRow.bs_var_num
                Me.Fill = cacheRow.sym_fill
                Me.Rotation = cacheRow.sym_rot
                Me.Height = cacheRow.sym_h
                Me.Width = cacheRow.sym_w
                Me.StandardColor = Color.FromArgb(Convert.ToInt32("FF" & cacheRow.sg_color, 16))
                If _LoadImage Then
                    Dim image = SymbolImageCache(cacheRow.sym_code)
                    If image IsNot Nothing Then
                        Me.SymImage = image
                    Else
                        Me.SymImage = ByteArraytoImage(cacheRow.sym_png)
                        SymbolImageCache(cacheRow.sym_code) = Me.SymImage
                    End If

                    Me.Illustration = ByteArraytoImage(cacheRow.sym_illus)
                End If
                Me.BaseName = cacheRow.bs_name
                Me.BaseGroup = cacheRow.sym_bs_code
                Me.SortWeight = cacheRow.sort_weight
                Me.SetIds(cacheRow.sym_code, cacheRow.sym_id)
                Me._isValid = True
            End If
        Else
            Me._isValid = False
        End If
        ' section 127-0-0-1-fe1248e:11b50ee3448:-8000:0000000000000985 end
    End Sub
    Private Sub Update()
        ' section 127-0-0-1-fe1248e:11b50ee3448:-8000:0000000000000989 begin
        If Me.Code > 0 Then
            Me.FromDataRow(SymbolCache.Iswa2010.SC.GetCode(Me.Code))
        ElseIf CheckId(Me.Id) Then
            Me.FromDataRow(SymbolCache.Iswa2010.SC.GetId(Me.Id))
        End If
        ' section 127-0-0-1-fe1248e:11b50ee3448:-8000:0000000000000989 end
    End Sub
    Public Sub MakeId()
        Dim SSS As New System.Text.StringBuilder
        If Not (Me.Category = 0 And Me.Group = 0 And Me.Symbol = 0 And Me.Variation = 0 And Me.Fill = 0 And Me.Rotation = 0) Then
            SSS.Append(Format(Me.Category, "00"))
            SSS.Append("-")
            SSS.Append(Format(Me.Group, "00"))
            SSS.Append("-")
            SSS.Append(Format(Me.Symbol, "000"))
            SSS.Append("-")
            SSS.Append(Format(Me.Variation, "00"))
            SSS.Append("-")
            SSS.Append(Format(Me.Fill, "00"))
            SSS.Append("-")
            SSS.Append(Format(Me.Rotation, "00"))
            Me._Code = 0
            Me._ID = SSS.ToString
            Update()
        End If
    End Sub
    Public Function CharacterCodeToString() As String
        ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000957 begin
        Return Me.Code.ToString(Globalization.CultureInfo.InvariantCulture)
        ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000957 end
    End Function
    Public Function CharacterCodetoStringHex() As String
        ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000959 begin
        'TODO Check that this is working to Hex
        Return Me.Code.ToString("X")
        ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000959 end
    End Function
    Public Function GetID(basegroup As Integer, fill As Integer, rotation As Integer) As String
        Dim result As ISWA2010DataSet.cacheRow = SC.GetSymbol(basegroup, fill, rotation).FirstOrDefault()
        If result IsNot Nothing Then
            Return result.sym_id
        End If
        Return String.Empty
    End Function
    Public Shared Function GetCode(basegroup As Integer, fill As Integer, rotation As Integer) As Integer
        Dim result As ISWA2010DataSet.cacheRow = SC.GetSymbol(basegroup, fill, rotation).FirstOrDefault()
        If result IsNot Nothing Then
            Return result.sym_code
        End If
        Return 0
    End Function
    Public Sub ResetPreviousId()
        Me._ID = String.Empty
        Me._Code = 0
        Me._isValid = False
    End Sub


    Public Shadows Function Clone() As Object Implements System.ICloneable.Clone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A begin
        ' Performs a shallow copy of Me and assign it to Newclone.
        Dim Newclone As SWSymbol = CType(Me.MemberwiseClone(), SWSymbol)
        'Newclone.Sss = CType(Me.Clone, SssDetails)
        'Newclone.mySWSymbol = Newclone

        Return Newclone
        ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:0000000000000A0A end
    End Function
 
End Class