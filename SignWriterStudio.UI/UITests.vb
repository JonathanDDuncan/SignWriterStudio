Imports NUnit.Framework
Imports Nunit.Framework.Constraints

Imports SignWriterStudio.UI 

#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on

#If NUnitTest Then
	<TestFixture()> _
		Friend Class SWCulturesTests
		Inherits AssertionHelper
    Dim cultures As New SWCultures
		<Test ()> _
			Public Sub SWCulturesTest()
			Dim Dt As DataTable   = Cultures.Cultures
			Assert.AreEqual (202,Dt.Rows.Count )
		End Sub
		
		<Test ()> _
			Public Sub SignLanguagesTest()
			Dim Dt As DataTable = Cultures.SignLanguages 
			Assert.AreEqual (143, Dt.Rows.Count)
		End Sub
			<Test ()> _
			Public Sub GetIdSignLanguagesTest1()
			Dim ID As Integer = Cultures.GetIdSignLanguages("bvl") 
			Assert.AreEqual (13, id)
			End Sub
				<Test ()> _
			Public Sub GetIdSignLanguagesTest2()
			Dim ID As Integer = Cultures.GetIdSignLanguages("ase") 
			Assert.AreEqual (4, id)
				End Sub
				
				<Test ()> _
			Public Sub IdCultureTest1()
			Dim ID As Integer = Cultures.IdCulture("af") 
			Assert.AreEqual (2, id)
				End Sub
					<Test ()> _
			Public Sub IdCultureTest2()
			Dim ID As Integer = Cultures.IdCulture("ar-MA") 
			Assert.AreEqual (15, id)
					End Sub	
					
					
					<Test ()> _
			Public Sub GetCultureNameTest1()
			Dim Str As String = Cultures.GetCultureName(15) 
			Assert.AreEqual ("ar-MA", Str)
					End Sub
					<Test ()> _
			Public Sub GetCultureNameTest2()
			Dim Str As String = Cultures.GetCultureName(94) 
			Assert.AreEqual ("el-GR", Str)
					End Sub
					
						<Test ()> _
			Public Sub GetSignLanguageIsoTest1()
			Dim Str As String = Cultures.GetSignLanguageIso(15) 
			Assert.AreEqual ("bfi", Str)
						End Sub
						<Test ()> _
			Public Sub GetSignLanguageIsoTest2()
			Dim Str As String = Cultures.GetSignLanguageIso(4) 
			Assert.AreEqual ("ase", Str)
						End Sub
						
						<Test ()> _
			Public Sub GetCultureFullNameTest1()
			Dim Str As String = Cultures.GetCultureFullName(4) 
			Assert.AreEqual ("Albanian", Str)
						End Sub
						<Test ()> _
			Public Sub GetCultureFullNameTest2()
			Dim Str As String = Cultures.GetCultureFullName(66) 
			Assert.AreEqual ("English - United States", Str)
					End Sub
	End Class
	#End If
	
	#If NUnitTest Then
	<TestFixture()> _
		Friend Class SWUserInterfaceTests
		Inherits AssertionHelper
		Dim UI As New SWUserInterface
		'TODO write UI tests
	End Class
	#End If
	 