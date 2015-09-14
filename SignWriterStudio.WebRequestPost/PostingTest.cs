using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SignWriterStudio.WebSessions
{
    [TestClass]
    public class WebSessionTest
    {

        [TestMethod]
        public void LoginandTest()
        {
           
            var loginuri = "http://www.signbank.org/signpuddle2.0/login.php";
            var username = "TestSignWriterStudio";
            var password = "123456";

            var posting = new WebSession();
            var loginWebPage = posting.Login(loginuri, username, password);
            var isLoggedin = WebSession.IsLoggedIn(loginWebPage);

            var uri = "http://www.signbank.org/signpuddle2.0/index.php?ui=1&sgn=4";
            var secondwebPage = posting.Post(uri);
            var stillLoggedIn = WebSession.IsLoggedIn(secondwebPage);

            Assert.IsTrue(isLoggedin);
            Assert.IsTrue(stillLoggedIn);

        }

        [TestMethod]
        public void SaveToPuddleTest()
        {
         
            var loginuri = "http://www.signbank.org/signpuddle2.0/login.php";
            var username = "TestSignWriterStudio";
            var password = "123456";

            var posting = new WebSession();
            var loginWebPage = posting.Login(loginuri, username, password);
            var isLoggedin = WebSession.IsLoggedIn(loginWebPage);

            var uri = "http://www.signbank.org/signpuddle2.0/canvas.php";

           
            var paramList = new List<Tuple<string, string>>
            {
                Tuple.Create("ui", "1"),
                Tuple.Create("sgn", "16"),
                Tuple.Create("action", "Add"),
                Tuple.Create("sgntxt", "M26x74S15a10n14xn74S15a18n26xn74S1000010x44S2ef213xn66S22c0010x1"),
                Tuple.Create("txt", "This is some text"),
                Tuple.Create("top", "5"),
                Tuple.Create("prev", "5"),
                Tuple.Create("next", "6"),
                Tuple.Create("src", "This is the source field"),
                Tuple.Create("video", "This is the video field"),
                Tuple.Create("trm[]", "Term1"),
                Tuple.Create("trm[]", "Term2")
            };


            var secondwebPage = posting.Post(uri, paramList);
            var stillLoggedIn = WebSession.IsLoggedIn(secondwebPage);

            Assert.IsTrue(isLoggedin);
            Assert.IsTrue(stillLoggedIn);

        }
       
    }
}
