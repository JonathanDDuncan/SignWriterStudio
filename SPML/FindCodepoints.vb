Option Infer On
Option Strict On
Module FindCodePoint
    <Runtime.CompilerServices.Extension()> _
    Friend Function FindCodePoints(ByVal CodePoint() As Integer, ByVal Find As Integer) As List(Of Integer)
        Dim Found As New List(Of Integer)
        For I = 0 To CodePoint.GetUpperBound(0)
            If CodePoint(I) = Find Then
                Found.Add(I)
            End If
        Next
        Return Found
    End Function
    <Runtime.CompilerServices.Extension()> _
    Friend Function FindCodePoints(ByVal CodePoint() As Integer, ByVal Find As Integer()) As List(Of Integer)
        Dim Found As New List(Of Integer)
        For I = 0 To CodePoint.GetUpperBound(0)
        	Dim CP = CodePoint(I)
            If EqualsList(CodePoint(I), Find) Then
                Found.Add(I)
            End If
        Next
        Return Found
    End Function
    <Runtime.CompilerServices.Extension()> _
    Friend Function FindFirstCodePoint(ByVal CodePoint() As Integer, ByVal Find As Integer) As Integer
        For I = 0 To CodePoint.GetUpperBound(0)
            If CodePoint(I) = Find Then
                Return I
            End If
        Next
        Return Nothing
    End Function
    <Runtime.CompilerServices.Extension()> _
    Friend Function FindFirstCodePoint(ByVal CodePoint() As Integer, ByVal Find As Integer()) As Integer
        For I = 0 To CodePoint.GetUpperBound(0)
            If EqualsList(CodePoint(I), Find) Then
                Return I
            End If
        Next
        Return Nothing
    End Function
    Private Function EqualsList(ByVal cmp As Integer, ByVal Find As Integer()) As Boolean
        For Each item In Find
            If cmp = item Then
                Return True
            End If
        Next
        Return False
    End Function
    <Runtime.CompilerServices.Extension()> _
    Public Function GetArray(ByVal Arr As Integer(), ByVal StartPos As Integer, ByVal EndPos As Integer) As Integer()
        Dim NewArray(EndPos - StartPos) As Integer

        If StartPos < Arr.GetLowerBound(0) OrElse EndPos > Arr.GetUpperBound(0) Then
            Return Nothing
        Else
            For I = StartPos To EndPos
                NewArray(I - StartPos) = Arr(I)
            Next
            Return NewArray
        End If
    End Function
    <Runtime.CompilerServices.Extension()> _
   Public Function Append(ByVal Arr As Integer(), ByVal ToAppend As Integer) As Integer()
        Dim ArrLength As Integer
        If Arr IsNot Nothing Then
            ArrLength = Arr.Length
        Else
            ArrLength = 0
        End If
        Dim NewArray(ArrLength) As Integer
        For I = 0 To ArrLength - 1
            NewArray(I) = Arr(I)
        Next
        NewArray(ArrLength) = ToAppend
        Return NewArray

    End Function
    <Runtime.CompilerServices.Extension()> _
  Public Function Append(ByVal Arr As Integer(), ByVal ToAppend As Integer()) As Integer()
        Dim ArrLength As Integer
        If Arr IsNot Nothing Then
            ArrLength = Arr.Length + ToAppend.Length - 1
        Else
            ArrLength = ToAppend.Length
        End If
        Dim NewArray(ArrLength) As Integer
        For I = 0 To Arr.Length - 1
            NewArray(I) = Arr(I)
        Next
        Dim Offset = Arr.Length
        For I = 0 To ToAppend.Length - 1
            NewArray(I + Offset) = ToAppend(I)
        Next

        Return NewArray

    End Function

    '<Runtime.CompilerServices.Extension()> _
    'Public Function GetString(ByVal Arr As Integer()) As String
    '    Return UTF8CodePoint.UTF8CodePoint.GetString(Arr)
    'End Function

End Module
