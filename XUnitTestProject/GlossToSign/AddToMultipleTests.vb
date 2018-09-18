Imports System
Imports DeepEqual.Syntax
Imports SignWriterStudio.Document
Imports Xunit

Namespace XUnitTestProject
    Public Class AddToMultipleTests
        <Fact>
        Sub Topic1Test1()
            Dim text = "[topico Dios él-D]"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "él-D", .ToAdd = "topico"})}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
        <Fact>
        Sub Topic1Test2()
            Dim text = "abc[topico Dios él-D]"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing}),
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "él-D", .ToAdd = "topico"})}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
        <Fact>
        Sub Topic1Test3()
            Dim text = "[topico Dios él-D]abc"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "él-D", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "abc", .ToAdd = Nothing})}

            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub

        <Fact>
        Sub Topic1Test4()
            Dim text = "[topico Dios él-D]abcX5"

            Dim expected = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "él-D", .ToAdd = "topico"}),
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

