#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports NUnit.Framework
Imports NUnit.Framework.Constraints

#If NUnitTest Then
<TestFixture()> _
 Public Class SWEditorTest
    Inherits AssertionHelper
    <Test()> _
     Public Shared Sub ClearAllTest()

        Dim Newsymbol As New SignWriterStudio.SWClasses.SWSignSymbol
        Newsymbol.Code = 256
        Newsymbol.X = 2
        Newsymbol.Y = 20

    End Sub
End Class
<TestFixture()> _
 Public Class SWEditorFavoritesTest
    Inherits AssertionHelper


End Class
#End If