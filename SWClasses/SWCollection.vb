Option Strict On




<Serializable()> _
Public Class SwCollection(Of T)
    Inherits ObjectModel.Collection(Of T)
    'Private _version As Integer
    Public Function Find(ByVal match As Predicate(Of T)) As T
        If match Is Nothing Then
            Throw New ArgumentNullException("match")
        End If

        Dim i As Integer
        For i = 0 To Count - 1
            If match.Invoke(Items(i)) Then
                Return Items(i)
            End If
        Next i
        Return CType(Nothing, T)

    End Function
    Public Function FindAll(ByVal match As Predicate(Of T)) As ObjectModel.Collection(Of T)
        If match Is Nothing Then
            Throw New ArgumentNullException("match")
        End If

        Dim list As New SwCollection(Of T)
        Dim i As Integer
        For i = 0 To Count - 1
            If match.Invoke(Item(i)) Then
                list.Add(Item(i))
            End If
        Next i
        Return list
    End Function


    Public Function RemoveAll(ByVal match As Predicate(Of T)) As Integer
        If match Is Nothing Then
            Throw New ArgumentNullException("match")
        End If
        Dim index As Integer = 0
        Do While ((index < Count) AndAlso Not match.Invoke(Items(index)))
            index += 1
        Loop
        If (index >= Count) Then
            Return 0
        End If
        Dim num2 As Integer = (index + 1)
        Do While (num2 < Count)
            Do While ((num2 < Count) AndAlso match.Invoke(Items(num2)))
                num2 += 1
            Loop
            If (num2 < Count) Then
                index += 1
                num2 += 1
                Items(index) = Items(num2)
            End If
        Loop
        Items.Clear()
        Dim num3 As Integer = (Count - index)
        ' Count = index
        '_version += 1
        Return num3

    End Function

    Public Sub Sort(ByVal index As Integer, ByVal totalcount As Integer, ByVal comparer As IComparer(Of T))

        If ((totalcount - index) < totalcount) Then
            Throw New ArgumentNullException("Index negative")
        End If
        Array.Sort(Of T)(Items.ToArray, index, totalcount, comparer)
        '_version += 1
    End Sub

    Public Sub Sort(ByVal comparison As Comparison(Of T))
        If comparison Is Nothing Then
            Throw New ArgumentNullException("comparison")
        End If
        If (Count > 0) Then
            Dim comparer As IComparer(Of T) = New FunctorComparer(Of T)(comparison)
            Array.Sort(Of T)(Items.ToArray, 0, Count, comparer)
        End If
    End Sub


    Public Sub Sort(ByVal comparer As IComparer(Of T))
        Sort(0, Count, comparer)
    End Sub


    Public Sub Sort()
        Sort(0, Count, Nothing)
    End Sub
    Public Overloads Sub Add(ByVal itemtoAdd As T)
        MyBase.Add(itemtoAdd)
    End Sub
End Class