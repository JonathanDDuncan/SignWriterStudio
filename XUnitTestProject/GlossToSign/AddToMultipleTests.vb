Imports System
Imports DeepEqual.Syntax
Imports SignWriterStudio.Document
Imports Xunit

Namespace XUnitTestProject
    Public Class AddToMultipleTests
        <Fact>
        Sub Topic1Test1()
            Dim text = "[topico Dios �l-D]"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"})}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
        <Fact>
        Sub Topic1Test2()
            Dim text = "abc[topico Dios �l-D]"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing}),
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"})}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
        <Fact>
        Sub Topic1Test3()
            Dim text = "[topico Dios �l-D]abc"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing})}

            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub

        <Fact>
        Sub Topic1Test4()
            Dim text = "[topico Dios �l-D]abcX5"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing})}

            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
    End Class
End Namespace

