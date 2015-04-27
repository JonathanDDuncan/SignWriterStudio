Option Strict On

Imports System.Windows.Forms.Layout
Imports System.Windows.Forms
Imports System.Drawing

Public Class SwLayoutEngine
    Inherits LayoutEngine
    'Implements IDisposable

    ' In this section you can add your own using directives
    ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:00000000000009B5 begin
    ' section 127-0-0-1--6e0e347d:11b574a5270:-8000:00000000000009B5 end
    ' *
    '          *   A class that represents ...
    '          *   All rights Reserved Copyright(c) 2008
    '          *       @see SWFlowLayoutPanel
    '          *       @author Jonathan Duncan
    '          */

    ' Associations
    Private _layoutEngineSettings As New SwLayoutEngineSettings()
    Public Property LayoutEngineSettings() As SwLayoutEngineSettings
        Get
            Return _layoutEngineSettings
        End Get
        Set(ByVal value As SwLayoutEngineSettings)
            _layoutEngineSettings = value
        End Set
    End Property

    Public Shadows Lines As New ArrayList

    Public Overrides Function Layout(ByVal container As Object, ByVal layoutEventArgs As LayoutEventArgs) As Boolean
        Dim parent As Control = CType(container, Control)
        Dim panel = CType(parent, FlowLayoutPanel)

        ' Use DisplayRectangle so that parent.Padding is honored.
        Dim documentSize As Size = parent.DisplayRectangle.Size
        Dim laneCenter As Integer

        'Column values
        Dim columnLeft As Integer
        Dim lasttop As Integer
        Dim finalColumnWidth As Integer
        Dim columnControls As New List(Of Tuple(Of SwLayoutControl, Integer, Integer))

        Lines.Clear()


        Dim scrollRightDist = panel.HorizontalScroll.Value 'Distance scrolled over

        If parent.Controls.Count > 0 Then
            For Each control As SwLayoutControl In parent.Controls

                Dim columnHeightWithThisControl = lasttop + control.Height + LayoutEngineSettings.SpaceBetweenSigns
                If control.DocumentSign.BegColumn OrElse columnHeightWithThisControl > documentSize.Height Then
                    'Finish column
                    finalColumnWidth = FinishColumn(documentSize, columnControls, columnLeft, scrollRightDist)

                    'Start new column
                    columnLeft += finalColumnWidth + LayoutEngineSettings.SpaceBetweenCols
                    columnControls.Clear()
                    lasttop = 0
                End If

                laneCenter = GetLaneCenter(control.DocumentSign.Lane)

                If control IsNot Nothing AndAlso control.DocumentSign IsNot Nothing AndAlso control.DocumentSign.Frames IsNot Nothing Then
                    Dim controlLeftInColumn As Integer = GetControlLeftInColumn(control, laneCenter)
                    columnControls.Add(Tuple.Create(control, controlLeftInColumn, lasttop))
                    Dim controlLeft = CInt(columnLeft + controlLeftInColumn - scrollRightDist)

                    control.Location = New Point(controlLeft, lasttop)
                End If
                lasttop = lasttop + control.Height + LayoutEngineSettings.SpaceBetweenSigns

            Next

            'Finish column
            FinishColumn(documentSize, columnControls, columnLeft, scrollRightDist)


        End If


        Return False 'Dont resize parent control
    End Function

    Private Function FinishColumn(ByVal documentSize As Size, ByVal columnControls As List(Of Tuple(Of SwLayoutControl, Integer, Integer)), ByVal columnLeft As Integer, ByVal scrollRightDist As Integer) As Integer
        Dim finalColumnWidth As Integer

        finalColumnWidth = GetFinalColumnWidth(columnControls, columnLeft, scrollRightDist)
        AddLine(documentSize, columnLeft, finalColumnWidth)
        Return finalColumnWidth
    End Function

    Private Function GetControlLeftInColumn(ByVal control As SwLayoutControl, ByVal laneCenter As Integer) As Integer

        Dim edgeFromLanecenter As Integer = GetEdgeFromLanecenter(control)
        Return laneCenter - edgeFromLanecenter
    End Function

    Private Function GetEdgeFromLanecenter(ByVal control As SwLayoutControl) As Integer

        Dim frameClone = CType(control.DocumentSign.Frames.First().Clone(), SWFrame)
        frameClone.CenterSpmlSymbols(New Point(0, 0))

        Return SWFrame.GetCenterToLeftEdge(frameClone)
    End Function

    Private Function GetRightLaneCenter() As Integer

        Return LayoutEngineSettings.SpaceBetweenColumnEdgeLaneRightLeftCenter + LayoutEngineSettings.SpaceBetweenLanes * 2
    End Function

    Private Function GetLaneCenter(ByVal lane As AnchorStyles) As Integer
        Dim leftLaneCenter = LayoutEngineSettings.SpaceBetweenColumnEdgeLaneRightLeftCenter
        Dim middleLaneCenter = LayoutEngineSettings.SpaceBetweenColumnEdgeLaneRightLeftCenter + LayoutEngineSettings.SpaceBetweenLanes
        Dim rightLaneCenter = GetRightLaneCenter()

        Dim laneCenter As Integer

        Select Case lane
            Case AnchorStyles.Left
                laneCenter = leftLaneCenter ' 30
            Case AnchorStyles.None
                laneCenter = middleLaneCenter ' 80
            Case AnchorStyles.Right
                laneCenter = rightLaneCenter ' 130
            Case Else
                laneCenter = middleLaneCenter
        End Select
        Return laneCenter
    End Function

    Private Function GetFinalColumnWidth(ByVal columnControls As List(Of Tuple(Of SwLayoutControl, Integer, Integer)), ByVal columnLeft As Integer, ByVal scrollRightDist As Integer) As Integer
        Dim posControlsResult As Tuple(Of Integer, Integer)
        Dim finalColumnWidth As Integer
        Dim rightLaneCenter As Integer = GetRightLaneCenter()

        posControlsResult = PositionControls(columnControls, columnLeft, scrollRightDist)
        Dim widdestSpaceNeeded = posControlsResult.Item1
        Dim mostLeft = posControlsResult.Item2

        'TODO may need to take into account RightLane Width
        Dim columnWidth = mostLeft + rightLaneCenter + LayoutEngineSettings.SpaceBetweenColumnEdgeLaneRightLeftCenter


        'Get the widdest of the two posControlsWidth or the MinLeft plus SpaceBetweenColumnEdgeLaneRightLeftCenter
        If widdestSpaceNeeded >= columnWidth Then
            finalColumnWidth = widdestSpaceNeeded
        Else
            finalColumnWidth = columnWidth
        End If
        Return finalColumnWidth
    End Function

    Private Sub AddLine(ByVal documentSize As Size, ByVal columnLeft As Integer, ByVal finalColumnWidth As Integer)
        If LayoutEngineSettings.DrawColumnLines Then
            Dim newLine As Rectangle
            newLine.X = columnLeft + finalColumnWidth + (LayoutEngineSettings.SpaceBetweenCols \ 2) 'Half the space on each side of line

            newLine.Y = 0
            newLine.Height = documentSize.Height
            newLine.Width = 0

            Lines.Add(newLine)
        End If
    End Sub

    Private Shared Function PositionControls(ByVal columnControls As List(Of Tuple(Of SwLayoutControl, Integer, Integer)), ByVal columnLeft As Integer, ByVal scrollRightDist As Integer) As Tuple(Of Integer, Integer)
        Dim widdestSpaceNeeded As Integer
        Dim mostLeft As Integer

        For Each columnControl As Tuple(Of SwLayoutControl, Integer, Integer) In columnControls
            Dim controlLeftInColumn = columnControl.Item2
            mostLeft = Math.Min(mostLeft, controlLeftInColumn)
        Next

        For Each columnControl As Tuple(Of SwLayoutControl, Integer, Integer) In columnControls
            Dim control = columnControl.Item1
            Dim controlLeftInColumn = columnControl.Item2
            Dim lastTop = columnControl.Item3

            Dim controlLeft = columnLeft - mostLeft + controlLeftInColumn
            Dim controlRight = controlLeft + control.Width
            If widdestSpaceNeeded < controlRight - columnLeft Then 'Get the widdest control span
                widdestSpaceNeeded = controlRight - columnLeft
            End If

            Dim controlLeftInPanel = controlLeft - scrollRightDist 'scrollRightDist to adjust for scroll bar
            control.Location = New Point(controlLeftInPanel, lastTop) 'move control to the right of width going past to the right of the left of the column

        Next
        Return Tuple.Create(widdestSpaceNeeded, mostLeft)
    End Function

End Class