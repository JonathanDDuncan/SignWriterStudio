Public Class Class1
    Public Shared Function GetGlossToSignArray(textString) As String()

        Dim glossToSignArray() As String

        Dim delimiters() As String = {" ", Chr(34), vbCrLf}

        textString = textString.Replace("{", "").Replace("}", "").Replace("}", "").Replace("(", "").Replace(")", "")
        textString = textString.Replace(",", " , ").Replace(".", " . ").Replace("!", " ! ").Replace("¡", " ¡ ").Replace("?", " ? ").Replace("¿", " ¿ ").Replace(":", " : ").Replace(";", " ; ").Replace("   ", " ").Replace("  ", " ")
        textString = textString.Replace(vbTab, " ")

        Dim textbetweenbrackets = GetTextsBetweenSquareBrackets(textString)


        glossToSignArray = textString.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
        Dim glossToSignArray1 = glossToSignArray.Where(Function(x) Not String.IsNullOrWhiteSpace(x) AndAlso x IsNot Environment.NewLine).ToArray()
        Dim glossToSignArray2 = MultipleSigns(glossToSignArray1)
        Return glossToSignArray2
    End Function

    Private Shared Function GetTextsBetweenSquareBrackets(textString As String) As String()
        Dim myList = New List(Of String)

        Dim TextBetween = GetTextBetween(textString, "[", "]")

    End Function

    Private Shared Function GetTextBetween(textString As String, startString As String, endString As String) As String
        Dim s As String = textString
        Dim i As Integer = s.IndexOf(startString, StringComparison.Ordinal)
        Dim f As String = s.Substring(i + 1, s.IndexOf(endString, i + 1, StringComparison.Ordinal) - i - 1)
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