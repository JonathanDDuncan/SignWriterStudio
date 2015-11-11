using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace SignWriterStudio.WebSessions
{
    public class WebSession
    {
        private readonly CookieContainer _cookieContainer;

        public WebSession()
        {
            _cookieContainer = new CookieContainer();
        }
        public string Login(string loginuri, string username, string password)
        {
            var paramList = new List<Tuple<string, string>> { Tuple.Create("username", username), Tuple.Create("password", password) };
            var webpageStr = Post(loginuri, paramList);

            return webpageStr;
        }

        private static string GetPostData(IEnumerable<Tuple<string, string>> paramList)
        {
            var postData = HttpUtility.ParseQueryString(String.Empty);
            if (paramList != null)
                foreach (var item in paramList)
                {
                    postData.Add(item.Item1, item.Item2);
                }

            return postData.ToString();
        }

        public string Post(string uri, List<Tuple<string, string>> paramList = null)
        {
            var request = CreatePostRequest(uri);
            var postData = GetPostData(paramList);
            if (!string.IsNullOrEmpty(postData))
                PostData(request, postData);
            var webpageStr = GetResponse(request);
            return webpageStr;
        }

        private HttpWebRequest CreatePostRequest(string loginuri)
        {
            var request = (HttpWebRequest)WebRequest.Create(loginuri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = _cookieContainer;
            return request;
        }

        private static void PostData(WebRequest request, string postData)
        {
            request.ContentLength = postData.Length;

            var postStream = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            postStream.Write(postData);
            postStream.Close();
        }

        private static string GetResponse(WebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponse();
            var data = response.GetResponseStream();

            var webpageStr = StreamToString(data);
            response.Close();
            return webpageStr;
        }

        public static bool IsLoggedIn(string webpageStr)
        {
            var loggedin = !(webpageStr.Contains("Invalid password") ||webpageStr.Contains("Invalid user name")) && webpageStr.Contains("logout.php");
            return loggedin;
        }

        public static string StreamToString(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
