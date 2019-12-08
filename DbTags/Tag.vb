Imports System.Dynamic

Public Class Tag
    Public Property IdTag As Guid
    Public Property Parent As Guid
    Public Property Rank As Int32
    Public Property Description As String
    Public Property Color As Int32
    Public Property Abbreviation As String


    Public Shared Function ToTag(tags As List(Of ExpandoObject)) As List(Of Tag)
        Return tags.ToList().Select(Function(x) ConvertToTag(x)).ToList()
    End Function

    Shared Function ConvertToTag(x As Object) As Tag
        Dim tag = New Tag() With {
                .IdTag = x.IdTag,
                .Rank = x.Rank,
                .Description = x.Description,
                .Color = x.Color,
                .Abbreviation = x.Abbreviation
        }
        Try
            Dim Parent As Guid = x.Parent
            tag.Parent = x.Parent
        Catch ex As Exception

        End Try

        Return tag
    End Function
End Class
