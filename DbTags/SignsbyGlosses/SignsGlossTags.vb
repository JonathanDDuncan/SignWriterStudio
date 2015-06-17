Namespace SignsbyGlosses
    Public Class SignsGlossTags
        Public Shared Function GetQuery(ByVal qryStr As String, ByVal filterTags As Boolean, ByVal allTagsExcept As Boolean, ByVal tags As List(Of String)) As String
            Dim sql As String
            If filterTags Then
                If allTagsExcept Then
                    sql = "Select * From (" + qryStr + ") as GlossQuery left outer join (SELECT * From TagDictionary WHERE TagDictionary.IdTag IN (" + GetTagList(tags) + ") ) as TD ON GlossQuery.IDDictionary = TD.IDDictionary WHERE TD.IDDictionary is null"
                Else
                    sql = "Select * From (" + qryStr + ") as GlossQuery Join TagDictionary ON GlossQuery.IDDictionary = TagDictionary.IDDictionary WHERE TagDictionary.IdTag IN (" + GetTagList(tags) + ")"
                End If

            Else
                sql = qryStr
            End If
            Return sql
        End Function

        Private Shared Function GetTagList(tags As List(Of String)) As String
            If tags IsNot Nothing AndAlso tags.Count > 0 Then
                Return tags.Select(Function(x) "'" & x & "'").Aggregate(Function(current, [next]) current & ", " & [next])
            Else
                Return String.Empty
            End If
        End Function

        Public Shared Function SearchWord(ByVal str As String) As String
            Dim sword As String
            If str Is Nothing Then
                sword = String.Empty
            Else
                sword = str
            End If
            Return sword
        End Function
    End Class
End Namespace