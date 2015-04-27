Imports System.Runtime.CompilerServices
Imports System.Text.RegularExpressions

Public Module StringParser
    <Extension()>
    Public Function GetSequenceBuildStr(str As String) As String
        Const pattern As String = "A[^>MLR]*"
        Return Regex.Match(str, pattern).Value
    End Function
    <Extension()>
    Public Function GetSymbolsBuildStr(str As String) As String
        Const pattern As String = "[MLR].*$"
        Dim signBoxResult = Regex.Match(str, pattern).Value

        If signBoxResult = String.Empty Then
            Dim punctuation = getPunctuation(str)
            Return punctuation
        End If

        Return signBoxResult
    End Function

    Friend Function SplitSymbolBuildStr(ByVal buildStr As String) As List(Of String)
        Dim arrStrings = buildStr.Split("S")

        Return (From str1 In arrStrings Where str1.Length = 12 AndAlso Not str1.Contains("M") AndAlso Not str1.Contains("L") AndAlso Not str1.Contains("R") Select str1).ToList()
    End Function

    Friend Function SplitSequenceBuildStr(ByVal buildStr As String) As List(Of String)
        Dim arrStrings = buildStr.Split("S")

        Return (From str1 In arrStrings Where str1.Length = 5 Select str1).ToList()
    End Function

    Public Function IsSignBox(txt As String) As Boolean
        Const pattern As String = "((A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3})((A(S[123][0-9a-f]{2}[0-5][0-9a-f])+)?[BLMR]([0-9]{3}x[0-9]{3})(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*|S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3})*"

        Dim result = Text.RegularExpressions.Regex.IsMatch(txt, pattern)
        Return result
    End Function

    Public Function GetBuildString(termRows As SPMLDataSet.termRow()) As String
        For Each row In TermRows
            'If isBuildString(row.term_Text) OrElse isPunctuation(row.term_Text) Then
            If isBuildString(row.term_Text) OrElse IsSignBox(row.term_Text) Then
                Return row.term_Text
            End If
        Next
        Return ""
    End Function
    Public Function IsBuildString(str As String) As Boolean
        Const pattern As String = "[BLMR]([0-9]{3}x[0-9]{3})*(S[123][0-9a-f]{2}[0-5][0-9a-f][0-9]{3}x[0-9]{3})*"

        Dim result = Text.RegularExpressions.Regex.IsMatch(str, pattern)
        Return result
    End Function
    Public Function IsPunctuation(str As String) As Boolean
        Const pattern As String = "S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}"

        Dim result = Text.RegularExpressions.Regex.IsMatch(str, pattern)
        Return result
    End Function
    Public Function GetPunctuation(str As String) As String
        Const pattern As String = "S38[7-9ab][0-5][0-9a-f][0-9]{3}x[0-9]{3}"

        Dim result = Text.RegularExpressions.Regex.Match(str, pattern)
        If result.Success Then
            Return result.Value
        Else
            Return String.Empty
        End If
    End Function
    Public Function GetTermsNonBuild(termRows As SPMLDataSet.termRow()) As List(Of String)

        Return (From row In TermRows Where Not IsBuildString(row.term_Text) AndAlso Not IsSignBox(row.term_Text) Select row.term_Text).ToList()
    End Function
    Public Function EncodeXml(xml As String) As String
        Return xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("""", "&quot;").Replace("'", "&apos;")
    End Function
    Public Function UnEncodeXml(xml As String) As String
        Return xml.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&quot;", """").Replace("&apos;", "'").Replace("&amp;", "&")
    End Function

End Module
