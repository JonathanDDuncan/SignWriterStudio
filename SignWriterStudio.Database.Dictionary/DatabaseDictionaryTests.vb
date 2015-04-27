Imports NUnit.Framework
Imports NUnit.Framework.Constraints

#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
#If NUnitTest Then
<TestFixture()> _
Public Class DatabaseDictionaryTests
    Inherits AssertionHelper
	  Dim Prestring As String = "<?xml version=""1.0"" encoding=""utf-16""?> <SerializableConnectionString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">   <ConnectionString>data source="
        Dim PostString As String = """</ConnectionString>   <ProviderName>System.Data.SQLite</ProviderName>"" </SerializableConnectionString>"
     
	<Test()> _
   Public Sub BuildConnectionStringTest()
	Assert.AreEqual(Prestring & PostString, BuildConnectionString(""))
    End Sub
	
End Class
#End If
	