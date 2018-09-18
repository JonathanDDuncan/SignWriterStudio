Imports System
Imports DeepEqual.Syntax
Imports SignWriterStudio.Document
Imports Xunit

Namespace XUnitTestProject
    Public Class AddToMultipleTests
        <Fact>
        Sub Topic1Test1()
            Dim text = "[topico Dios �l-D]"

            Dim a = New GlossWiths With {
                    .Signs = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"})}, .Text = "topico Dios �l-D"}
            Dim expected = New List(Of GlossWiths) From {a}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
        <Fact>
        Sub Topic1Test2()
            Dim text = "abc[topico Dios �l-D]"

            Dim a = New GlossWiths With {
                    .Signs = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"})}, .Text = "topico Dios �l-D"}
            Dim expected = New List(Of GlossWiths) From {a}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
        <Fact>
        Sub Topic1Test3()
            Dim text = "[topico Dios �l-D]abc"

            Dim a = New GlossWiths With {
                    .Signs = New List(Of GlossWith) From {
                    (New GlossWith With {.Gloss = "Dios", .ToAdd = "topico"}),
                    (New GlossWith With {.Gloss = "�l-D", .ToAdd = "topico"})}, .Text = "topico Dios �l-D"}
            Dim expected = New List(Of GlossWiths) From {a}
            Dim result = GlossToSignHelper.GetGlossToSignArray(text)

            result.ShouldDeepEqual(expected)

        End Sub
    End Class
End Namespace

