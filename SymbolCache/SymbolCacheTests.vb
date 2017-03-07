#Const NUnitTest = True   ' NUnit Tests on
#Const AssertTest = True    ' Assertion rules on
Imports NUnit.Framework
Imports Nunit.Framework.Constraints

<TestFixture()> Public NotInheritable Class SymbolCacheTests

	<Test()> Public Shared Sub First()
		Assert.That(True, Iz.True)
	End Sub
		
    <Test()> _
     Public Shared Sub GetIdTestFail3()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Function() SWSC.GetId("a1-01-01-001-01-01"))
    End Sub
	
    <Test()> _
     Public Shared Sub GetCodeTestFail3()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Function() SWSC.GetCode(-5))
    End Sub
	
	
    <Test()> _
     Public Shared Sub GetCodeFullTest()
        ' TODO: Add your test.

    End Sub
	
    <Test()> _
     Public Shared Sub IsCodeFullinCacheTest()
        ' TODO: Add your test.

    End Sub
    <Test()> _
     Public Shared Sub GetFillsTest1()
        ' TODO: Add your test.
        assert.AreEqual(6, sc.GetFills(1))
    End Sub
    <Test()> _
    Public Shared Sub GetRotationsTest1()
        ' TODO: Add your test.
        assert.AreEqual(16, sc.GetRotations(1))
    End Sub

    <Test()> _
     Public Shared Sub LoadCodeFullintoCacheTest()
        ' TODO: Add your test.

    End Sub
	
      	 
    <Test()> _
   Public Shared Sub SelectCodeFullCacheTest()
        ' TODO: Add your test.

    End Sub
	<Test()> _
		Public Shared Sub GetIdTest()
		Dim SWSC As New SWSymbolCache
		SWSC.GetId("01-01-001-01-01-01")
		Assert.That(SWSC.SymbolCacheDT.Count, Iz.EqualTo(1))
	End Sub
	
	<Test()> _
		Public Shared Sub FlushCacheTest()
		Dim SWSC As New SWSymbolCache
		SWSC.FlushCache(5)
		Assert.That(SWSC.SymbolCacheDT.Count, Iz.LessThan(5))
		
	End Sub
	<Test()> _
		Public Shared Sub FlushCacheTest1()
		Dim SWSC As New SWSymbolCache
		SWSC.GetId("01-01-001-01-01-01")
		SWSC.GetId("01-01-001-01-01-02")
		SWSC.GetId("01-01-001-01-01-03")
		SWSC.GetId("01-01-001-01-01-04")
		SWSC.FlushCache(2)
		Assert.That(SWSC.SymbolCacheDT.Count, Iz.EqualTo(0))
		
	End Sub
	
    <Test()> _
     Public Shared Sub FlushCacheTestFail1()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Sub() SWSC.FlushCache(-1))
    End Sub
	
    <Test()> _
     Public Shared Sub FlushCacheTestFail2()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Sub() SWSC.FlushCache(0))
    End Sub
	
	<Test()> _
		Public Shared Sub IsIdInCacheTest()
		Dim SWSC As New SWSymbolCache
		SWSC.GetId("01-01-001-01-01-01")
		Assert.That(SWSC.isIdInCache("01-01-001-01-01-01"), Iz.True)
		
	End Sub
    <Test()> _
     Public Shared Sub IsIdinCacheTestExcep()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Function() SWSC.isIdInCache(-5))
    End Sub
	<Test()> _
		Public Shared Sub LoadIdintoCacheTest()
		Dim SWSC As New SWSymbolCache
		SWSC.GetId("01-01-001-01-01-01")
		
		Assert.That(SWSC.SymbolCacheDT.Count, Iz.EqualTo(1))
	End Sub
	
    <Test()> _
     Public Shared Sub LoadIdintoCacheTestExcep()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Sub() SWSC.LoadIdintoCache("a1-01-001-01-01-01"))
    End Sub
	
	
	<Test()> _
		Public Shared Sub GetCodeTest()
		Dim SWSC As New SWSymbolCache
		SWSC.GetCode(258)
		Assert.That(SWSC.SymbolCacheDT.Count, Iz.EqualTo(1))
	End Sub
	<Test()> _
		Public Shared Sub IsCodeinCacheTest()
		Dim SWSC As New SWSymbolCache
		SWSC.GetCode(258)
		Assert.That(SWSC.isCodeinCache(258), Iz.True)
	End Sub
	<Test()> _
		Public Shared Sub IsCodeinCacheTestFail()
		Dim SWSC As New SWSymbolCache
		SWSC.GetCode(258)
		Assert.That(SWSC.isCodeinCache(259), Iz.False)
	End Sub
    <Test()> _
     Public Shared Sub IsCodeinCacheTestExcep()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Sub() SWSC.isCodeinCache(-5))
    End Sub
	
	<Test()> _
		Public Shared Sub CheckCodeTestPass()
        Assert.That(SWSymbolCache.CheckCode(258), Iz.True)
	End Sub
	<Test()> _
		Public Shared Sub CheckCodeTestFail1()
        Assert.That(SWSymbolCache.CheckCode(-5), Iz.False)
	End Sub
	<Test()> _
		Public Shared Sub CheckCodeTestFail2()
        Assert.That(SWSymbolCache.CheckCode(450000), Iz.False)
	End Sub
	
	<Test()> _
		Public Shared Sub LoadCodeintoCacheTest()
        Dim SWSC As New SWSymbolCache
		SWSC.LoadCodeintoCache(258)
		Assert.That(SWSC.SymbolCacheDT.Count, Iz.EqualTo(1))
	End Sub
    <Test()> _
    Public Shared Sub LoadCodeintoCacheTestExcep()
        Dim SWSC As New SWSymbolCache
        Assert.Throws(Of AssertionException)(Sub() SWSC.LoadCodeintoCache(-5))
    End Sub
	
	
	
	'<Test()> _
	' Public Shared Sub GetFavoritesTest()
	'    ' TODO: Add your test.
	
	'End Sub
	
	'<Test()> _
	' Public Shared Sub GetFavoriteSymbolsDTTest()
	'    ' TODO: Add your test.
	
	'End Sub
	
	
	'<Test()> _
	' Public Shared Sub GetFavoriteSymbolsListTest()
	'    ' TODO: Add your test.
	
	'End Sub
	
	
	<Test()> _
		Public Shared Sub GetBaseTest()
		Dim Symbolcache As New SWSymbolCache
		Dim DR As DataRow()
		DR = Symbolcache.GetBase()
		Assert.That(DR.Length, Iz.EqualTo(BASESYMBOLS))
	End Sub
	
End Class
