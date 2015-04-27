Option Strict On

Imports System.Windows.Forms
Imports System.Drawing



<Serializable()> Public NotInheritable Class SwLayoutEngineSettings
    
    Private _flowDirection As FlowDirection = Windows.Forms.FlowDirection.TopDown
    Public Property FlowDirection() As FlowDirection
        Get
            Return _flowDirection
        End Get
        Set(ByVal value As FlowDirection)
            _flowDirection = value
        End Set
    End Property
    
    Private _spaceBetweenCols As Integer = 10
    Public Property SpaceBetweenCols() As Integer
        Get
            Return _spaceBetweenCols
        End Get
        Set(ByVal value As Integer)
            _spaceBetweenCols = value
        End Set
    End Property
    
    Private _spaceBetweenSigns As Integer = 15
    Public Property SpaceBetweenSigns() As Integer
        Get
            Return _spaceBetweenSigns
        End Get
        Set(ByVal value As Integer)
            _spaceBetweenSigns = value
        End Set
    End Property
    
    Private _drawColumnLines As Boolean = True
    Public Property DrawColumnLines() As Boolean
        Get
            Return _drawColumnLines
        End Get
        Set(ByVal value As Boolean)
            _drawColumnLines = value
        End Set
    End Property
    
    Private _backgroundColor As Color = Color.White
    Public Property BackgroundColor() As Color
        Get
            Return _backgroundColor
        End Get
        Set(ByVal value As Color)
            _backgroundColor = value
        End Set
    End Property
    
    Private _spaceBetweenLanes As Integer = 50
    Public Property SpaceBetweenLanes() As Integer
        Get
            Return _spaceBetweenLanes
        End Get
        Set(ByVal value As Integer)
            _spaceBetweenLanes = value
        End Set
    End Property
    
    Private _spaceBetweenColumnEdgeLaneRightLeftCenter As Integer = 30
    Public Property SpaceBetweenColumnEdgeLaneRightLeftCenter() As Integer
        Get
            Return _spaceBetweenColumnEdgeLaneRightLeftCenter
        End Get
        Set(ByVal value As Integer)
            _spaceBetweenColumnEdgeLaneRightLeftCenter = value
        End Set
    End Property

End Class