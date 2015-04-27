#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports NUnit.Framework
Imports Nunit.Framework.Constraints

<TestFixture()> _
	Public Class SettingsTests
    Inherits AssertionHelper
    <Test()> _
      Public Shared Sub GetIdTest1()
        ' TODO: Add your test.
        '             Assert.AreEqual( Expected , GetId())
    End Sub
    <Test()> _
     Public Shared Sub UpdateSettingsTest()
        ' TODO: Add your test.

    End Sub
	
    <Test()> _
     Public Shared Sub GetSettingsTest()
        ' TODO: Add your test.

    End Sub
	
	
    <Test()> _
     Public Shared Sub UpdateFavoriteTest()
        ' TODO: Add your test.

    End Sub
End Class