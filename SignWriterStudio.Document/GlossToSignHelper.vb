Public Class GlossToSignHelper

    Public Shared Function GetGlossToSignArray(textString As String) As List(Of GlossWith)

        Dim glossToSignArray() As String

        Dim delimiters() As String = {" ", Chr(34), vbCrLf}

        textString = textString.Replace("{", "").Replace("}", "").Replace("}", "").Replace("(", "").Replace(")", "")
        textString = textString.Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("¡", " ¡ ").Replace("?", " ? ").Replace("¿", " ¿ ").Replace(":", " : ").Replace(";", " ; ").Replace("   ", " ").Replace("  ", " ")
        textString = textString.Replace(vbTab, " ")

        Dim startString = "["
        Dim endString = "]"
        Dim textsBetween = GetTextsBetweenSquareBrackets(textString, startString, endString)
        Dim replacedWithHash = ReplaceWithHash(textString, textsBetween, startString, endString)
        Dim processed = GetSigns(textsBetween, delimiters)

        glossToSignArray = replacedWithHash.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
        Dim glossToSignArray1 = glossToSignArray.Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso x IsNot Environment.NewLine).ToArray()
        Dim signStruc = ToSignStruc(glossToSignArray1)
        Dim merged = MergeSigns(signStruc, processed)

        Return MultipleSigns1(merged)
    End Function

    Private Shared Function MergeSigns(strucuturedSigns As List(Of GlossWith), processed As List(Of GlossWiths)) As List(Of GlossWith)
        Dim MyDict = New Dictionary(Of String, GlossWiths)
        For Each glossWithse As GlossWiths In processed
            MyDict.Add("##" & glossWithse.Text.GetHashCode() & "##", glossWithse)
        Next

        Dim mergedList = New List(Of GlossWith)
        For Each item As GlossWith In strucuturedSigns
            If item.Gloss.StartsWith("##") Then
                Dim Myvalue As GlossWiths = Nothing
                Dim found = MyDict.TryGetValue(item.Gloss, Myvalue)
                If found Then
                    mergedList.AddRange(Myvalue.Signs)
                End If
            Else
                mergedList.Add(item)
            End If
        Next
        Return mergedList
    End Function

    Private Shared Function ToSignStruc(glossToSignArray1 As String()) As List(Of GlossWith)
        Dim signs As New List(Of GlossWith)
        For Each text As String In glossToSignArray1
            signs.Add(New GlossWith With {.Gloss = text, .ToAdd = Nothing})
        Next
        Return signs
    End Function

    Private Shared Function GetSigns(textsBetween As List(Of String), delimiters As String()) As List(Of GlossWiths)
        Dim GlossWiths = New List(Of GlossWiths)
        For Each text As String In textsBetween
            GlossWiths.Add(GetGlossWith(delimiters, text))
        Next
        Return GlossWiths
    End Function

    Private Shared Function GetGlossWith(delimiters As String(), text As String) As GlossWiths

        Dim split = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
        Dim addToAll As String
        Dim signs As New List(Of GlossWith)
        For i As Integer = 0 To split.Count - 1
            If i = 0 Then
                addToAll = split(i)
            Else
                signs.Add(New GlossWith With {.Gloss = split(i), .ToAdd = addToAll})
            End If
        Next
        Return New GlossWiths With {.Signs = signs, .Text = text}
    End Function

    Private Shared Function ReplaceWithHash(textString As String, textsBetween As List(Of String), startString As String, endString As String) As Object
        For Each text As String In textsBetween
            Dim textToReplace = startString & text & endString
            textString = textString.Replace(textToReplace, " ##" & textToReplace.Replace(startString, "").Replace(endString, "").GetHashCode() & "## ")
        Next
        Return textString
    End Function

    Private Shared Function GetTextsBetweenSquareBrackets(textString As String, startString As String, endString As String) As List(Of String)
        Dim myList = New List(Of String)

        Dim nextString = textString
        Dim textBetween = String.Empty
        Do
            textBetween = GetTextBetween(nextString, startString, endString)
            If textBetween IsNot String.Empty Then
                myList.Add(textBetween)
                nextString = nextString.Replace(textString, "")
            End If
        Loop Until textBetween = String.Empty
        Return myList
    End Function

    Private Shared Function GetTextBetween(textString As String, startString As String, endString As String) As String
        Dim s As String = textString
        Dim i As Integer = s.IndexOf(startString, StringComparison.Ordinal)
        Dim f As String = String.Empty
        If Not i = -1 Then
            f = s.Substring(i + 1, s.IndexOf(endString, i + 1, StringComparison.Ordinal) - i - 1)
        End If
        Return f
    End Function
    Public Shared Function MultipleSigns1(ByVal glosses As List(Of GlossWith)) As List(Of GlossWith)
        Dim glossToSignArray = New List(Of GlossWith)()
        For Each item As GlossWith In glosses
            Dim s = item.Gloss
            If (s.Contains("X")) Then
                Dim textBeforeX = GetTextBefore(s, "X")
                Dim textAfterX = GetTextAfter(s, "X")
                Dim mult As Integer = 0
                Dim isInt = Integer.TryParse(textAfterX, mult)

                If isInt AndAlso mult > 0 Then
                    For i = 1 To mult
                        glossToSignArray.Add(New GlossWith() With {.Gloss = textBeforeX})
                    Next
                End If
            Else
                glossToSignArray.Add(item)
            End If
        Next
        Return glossToSignArray
    End Function
    Public Shared Function MultipleSigns(ByVal glosses As String()) As String()
        Dim glossToSignArray = New List(Of String)()
        For Each s As String In glosses

            If (s.Contains("X")) Then
                Dim textBeforeX = GetTextBefore(s, "X")
                Dim textAfterX = GetTextAfter(s, "X")
                Dim mult As Integer = 0
                Dim isInt = Integer.TryParse(textAfterX, mult)

                If isInt AndAlso mult > 0 Then
                    For i = 1 To mult
                        glossToSignArray.Add(textBeforeX)
                    Next
                End If
            Else
                glossToSignArray.Add(s)
            End If
        Next
        Return glossToSignArray.ToArray()
    End Function

    Private Shared Function GetTextAfter(ByVal str As String, ByVal find As String) As String
        Dim findStart = str.IndexOf(find, StringComparison.Ordinal)
        If findStart > 0 Then
            Dim findEnd As Integer = findStart + find.Length
            Return str.Substring(findEnd, str.Length - (findEnd))
        End If
        Return String.Empty
    End Function

    Private Shared Function GetTextBefore(ByVal str As String, ByVal find As String) As String
        Dim findStart = str.IndexOf(find, StringComparison.Ordinal)
        If findStart > 0 Then
            Return str.Substring(0, findStart)
        End If
        Return String.Empty
    End Function
End Class

Public Class GlossWiths
    Public Property Signs As List(Of GlossWith)

    Public Property Text As String

End Class

Public Class GlossWith
    Public Property Gloss As String

    Public Property ToAdd As String

End Class