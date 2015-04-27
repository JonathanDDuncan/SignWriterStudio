Option Strict On
Imports System.Drawing
Imports System.Windows.Forms

Friend Class ColumnHeight
    Friend AccumulatedHeight As Integer
    Function TestHeight(ByVal control As SwLayoutControl) As Integer
        Return Control.Height + AccumulatedHeight
    End Function
    Sub ProcessHeight(ByVal control As SwLayoutControl)
        AccumulatedHeight += Control.Height
    End Sub
End Class


Friend Class LaneWidth
    Friend AccumulatedLeftLaneWidth As Integer
    Friend AccumulatedCenterLaneWidth As Integer
    Friend AccumulatedRightLaneWidth As Integer

    Sub New(ByVal minimumLeftLane As Integer, ByVal minimumCenterLane As Integer, ByVal minimumRightLane As Integer)
        AccumulatedLeftLaneWidth = minimumLeftLane
        AccumulatedCenterLaneWidth = minimumCenterLane
        AccumulatedRightLaneWidth = MinimumRightLane
    End Sub

    Sub New()
    End Sub

    Sub ProcesSWidth(ByVal control As SwLayoutControl)

        Select Case Control.Anchor
            Case AnchorStyles.Left
                If Control.Width > AccumulatedLeftLaneWidth Then
                    AccumulatedLeftLaneWidth = Control.Width
                End If
            Case AnchorStyles.None
                If Control.Width > AccumulatedCenterLaneWidth Then
                    AccumulatedCenterLaneWidth = Control.Width
                End If
            Case AnchorStyles.Right
                If Control.Width > AccumulatedRightLaneWidth Then
                    AccumulatedRightLaneWidth = Control.Width
                End If
        End Select
    End Sub

    Function TotalWidth() As Integer
        Return AccumulatedLeftLaneWidth + AccumulatedCenterLaneWidth + AccumulatedRightLaneWidth
    End Function

    Function WidthTest(ByVal control As SwLayoutControl) As Integer

        Select Case Control.Anchor
            Case AnchorStyles.Left
                If Control.Width > AccumulatedLeftLaneWidth Then
                    Return Control.Width + AccumulatedCenterLaneWidth + AccumulatedRightLaneWidth
                Else
                    Return AccumulatedLeftLaneWidth + AccumulatedCenterLaneWidth + AccumulatedRightLaneWidth
                End If

            Case AnchorStyles.Right
                If Control.Width > AccumulatedLeftLaneWidth Then
                    Return AccumulatedLeftLaneWidth + AccumulatedCenterLaneWidth + Control.Width
                Else
                    Return AccumulatedLeftLaneWidth + AccumulatedCenterLaneWidth + AccumulatedRightLaneWidth
                End If
            Case Else 'AnchorStyles.None
                If Control.Width > AccumulatedCenterLaneWidth Then
                    Return AccumulatedLeftLaneWidth + Control.Width + AccumulatedRightLaneWidth
                Else
                    Return AccumulatedLeftLaneWidth + AccumulatedCenterLaneWidth + AccumulatedRightLaneWidth
                End If
        End Select

    End Function
End Class

Public Class SWPrintPages
    Public LastPrintedPage As Integer
    Public TotalPages As Integer
    Public CurrentPage As Integer
    Public PrintPages As New SWCollection(Of Page)
    Public IsPrinting As Boolean '= False
    Public PrintSize As New Size
    Public LeftMargin As Integer
    Public TopMargin As Integer

    Public Class Page
        Private _fromControl As Integer
        Public Property FromControl() As Integer
            Get
                Return _fromControl
            End Get
            Set(ByVal value As Integer)
                _fromControl = value
            End Set
        End Property
        Private _toControl As Integer
        Public Property ToControl() As Integer
            Get
                Return _toControl
            End Get
            Set(ByVal value As Integer)
                _toControl = value
            End Set
        End Property
    End Class
    Friend Function GetPage(ByVal index As Integer) As Page
        If Index > 0 Then
            If Index > PrintPages.Count Then
                For I = PrintPages.Count To index - 1
                    PrintPages.Add(New Page)
                    TotalPages += 1
                Next
            End If
            Return PrintPages(Index - 1)
        End If
    End Function

End Class
Public Class SaveDocument
    Implements IDisposable


    ' In this section you can add your own using directives
    ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000C04 begin
    ' section 127-0-0-1--489ad8bc:11b55ce357a:-8000:0000000000000C04 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */
    ' Attributes

    Private _filename As String
    Public Property Filename() As String
        Get
            Return _filename
        End Get
        Set(ByVal value As String)
            _filename = value
        End Set
    End Property
    Private _mySWDocument As SwDocument

    Public Property MySWDocument() As SwDocument
        Get
            Return _mySWDocument
        End Get
        Set(ByVal value As SwDocument)
            _mySWDocument = value
        End Set
    End Property
    ' Operations
    Private _disposedValue As Boolean '= False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not _disposedValue Then
            If disposing Then
                'free unmanaged resources when explicitly called
                _mySWDocument.Dispose()
            End If

            'free shared unmanaged resources
        End If
        _disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
'TODO see if not being serializable break loading and saving SWDocuments
'<Serializable()> _

Public NotInheritable Class PrintDocument
    ' In this section you can add your own using directives
    ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000863 begin
    ' section 127-0-0-1--53dfeef5:11b4cd48d96:-8000:0000000000000863 end
    ' *
    '          *   A class that represents ...  All rights Reserved Copyright(c) 2008
    '          *
    '          *       @see OtherClasses
    '          *       @author Jonathan Duncan
    '          */

End Class