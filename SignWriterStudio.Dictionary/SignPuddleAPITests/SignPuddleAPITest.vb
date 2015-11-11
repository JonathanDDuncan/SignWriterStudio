
Imports Microsoft.VisualStudio.TestTools.UnitTesting


Namespace SignPuddleAPITests
  
    <TestClass()>
    Public Class SignPuddleAPITest
        Private ReadOnly _username = "Jonathan"
        Private ReadOnly _password = "54321"

        <TestMethod()> Public Sub AddEntryTest()


            Dim api = New SignPuddleApi.SignPuddleApi(_username, _password)
            Dim isLoggedin = api.IsLoggedIn
            Dim ui = "1"
            Dim sgn = "16"

            Dim sgntxt = "M26x74S15a10n14xn74S15a18n26xn74S1000010x44S2ef213xn66S22c0010x1"
            Dim txt = "This is some text"
            Dim top = "5"
            Dim prev = "5"
            Dim nextStr = "6"
            Dim src = "This is the source field"
            Dim video = "This is the video field"
            Dim term1 = "Term1"
            Dim term2 = "Term2"
            Dim trm = New List(Of String)()
            trm.Add(term1)
            trm.Add(term2)


            Dim webPage = api.AddEntry(ui, sgn, sgntxt, txt, top, prev, nextStr, src, video, trm)


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains(src))
            Assert.IsTrue(webPage.Contains(txt))
            Assert.IsTrue(webPage.Contains(term1))
            Assert.IsTrue(webPage.Contains(term2))


        End Sub
        <TestMethod()> Public Sub AddFSWEntryTest()

            'TODO THis test is not working, SignPuddle will not accept FSW only KSW
            Dim api = New SignPuddleApi.SignPuddleApi(_username, _password)
            Dim isLoggedin = api.IsLoggedIn
            Dim ui = "1"
            Dim sgn = "16"

            Dim sgntxt = "M523x516S2e008502x487S11920478x485"
            Dim txt = "This is some text"
            Dim top = "5"
            Dim prev = "5"
            Dim nextStr = "6"
            Dim src = "This is the source field"
            Dim video = "This is the video field"
            Dim term1 = "Test1"
            Dim term2 = "Test2"
            Dim trm = New List(Of String)()
            trm.Add(term1)
            trm.Add(term2)


            Dim webPage = api.AddEntry(ui, sgn, sgntxt, txt, top, prev, nextStr, src, video, trm)


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains("M23x16S2e0082xn13S11920n22xn15"))
            Assert.IsTrue(webPage.Contains(src))
            Assert.IsTrue(webPage.Contains(txt))
            Assert.IsTrue(webPage.Contains(term1))
            Assert.IsTrue(webPage.Contains(term2))


        End Sub

        <TestMethod()> Public Sub UpdateEntryTest()
          
            Dim api = New SignPuddleApi.SignPuddleApi(_username, _password)
            Dim isLoggedin = api.IsLoggedIn
            Dim ui = "1"
            Dim sgn = "16"
            Dim sid = "541"

            Dim sgntxt = "M26x74S15a10n14xn74S15a18n26xn74S1000010x44S2ef213xn66S22c0010x1"
            Dim txt = "This is some text"
            Dim top = "5"
            Dim prev = "5"
            Dim nextStr = "6"
            Dim src = "This is the source field"
            Dim video = "This is the video field"
            Dim term3 = "Term3"
            Dim term4 = "Term4"
            Dim trm = New List(Of String)()
            trm.Add(term3)
            trm.Add(term4)


            Dim webPage = api.UpdateEntry(ui, sgn, sid, sgntxt, txt, top, prev, nextStr, src, video, trm)


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains(src))
            Assert.IsTrue(webPage.Contains(txt))
            Assert.IsTrue(webPage.Contains(term3))
            Assert.IsTrue(webPage.Contains(term4))


        End Sub

        <TestMethod()> Public Sub DeleteEntryTest()

            Dim api = New SignPuddleApi.SignPuddleApi(_username, _password)
            Dim isLoggedin = api.IsLoggedIn
            Dim ui = "1"
            Dim sgn = "16"
            Dim sid = "542"



            Dim webPage = api.DeleteEntry(ui, sgn, sid)


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains("Entry deleted"))

        End Sub
        <TestMethod()> Public Sub GetEntryTest()

            Dim api = New SignPuddleApi.SignPuddleApi(_username, "")
            Dim isLoggedin = api.IsLoggedIn
            Dim ui = "1"
            Dim sgn = "16"
            Dim sid = "477"



            Dim webPage = api.GetEntry(ui, sgn, sid)


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains("<input type=hidden name=""sid"" value=""" & sid & """>") AndAlso webPage.Contains("Terms and Titles"))

        End Sub

        <TestMethod()> Public Sub GetExportTest()

            Dim api = New SignPuddleApi.SignPuddleApi(_username, _password)
            Dim isLoggedin = api.IsLoggedIn
            Dim ui = "1"
            Dim sgn = "16"
            Dim sid = "511"

            Dim webPage = api.GetExport(ui, sgn, sid)


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains("<?xml version=""1.0"""))

        End Sub

        <TestMethod()> Public Sub GetPuddlesTest()

            Dim api = New SignPuddleApi.SignPuddleApi(_username, _password)
            Dim isLoggedin = api.IsLoggedIn

            Dim webPage = api.GetPuddles()


            Assert.IsTrue(isLoggedin)
            Assert.IsTrue(webPage.Contains("SignPuddle Home Directory"))

        End Sub
        '<form method="POST" action="/signpuddle2.0/canvas.php"><input type="hidden" name="ui" value="1"><input type="hidden" name="sgn" value="16"><input type="hidden" name="sid" value="541"><input type="hidden" name="name" value="541"><input type="hidden" name="action" value="Delete"><button type="submit"><img src="data/ui/1/45.png" border="0" title="Delete Entry"></button></form>
    End Class
End Namespace