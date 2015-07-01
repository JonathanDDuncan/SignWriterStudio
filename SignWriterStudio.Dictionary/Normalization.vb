Imports System.Text
Imports System.Globalization

Friend Class Normalization
    Public Shared Function Normalize(ByVal text As String) As String
        Dim removedPunctuation = RemovePunctuationStart(text)
        Dim removedDiacritics = RemoveDiacritics(removedPunctuation)
        Dim byteEncoding = RemoveByteEncoding(removedDiacritics)
        Dim tolower = byteEncoding.ToLowerInvariant()
        Dim normalized = tolower
        Return normalized

    End Function

    Private Shared Function RemovePunctuationStart(ByVal text As String) As String
        Dim charstoRemove() As Char = {"!", "¡", """", "#", "%", "&", "'", "(", ")", "*", ",", "-", ".", "/", ":", ";", "?", "¿", "@", "[", "\\", "]", "_", "{", "}"}
       
        Dim newString As String = text.TrimStart(charstoRemove)
        Return newString
    End Function

    Private Shared Function RemoveByteEncoding(ByVal accentedStr As String)
        Dim tempBytes As Byte()
        tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr)
        Dim asciiStr As String = System.Text.Encoding.UTF8.GetString(tempBytes)
        Return asciiStr
    End Function

    Private Shared Function RemoveDiacritics(text As String) As String
        Dim normalizedString = text.Normalize(NormalizationForm.FormD)
        Dim stringBuilder = New StringBuilder()

        For Each c In normalizedString
            Dim unicodeCategory1 = CharUnicodeInfo.GetUnicodeCategory(c)
            If unicodeCategory1 <> UnicodeCategory.NonSpacingMark Then
                stringBuilder.Append(c)
            End If
        Next

        Return stringBuilder.ToString().Normalize(NormalizationForm.FormC)
    End Function
End Class