using System;
using System.Net;
using System.Net.Cache;

namespace FlickrNetTest
{
    public static class UrlHelper
    {
        public static bool Exists(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);
            req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);
            req.AllowAutoRedirect = false;
            req.Method = "HEAD";

            try
            {
                using (var res = (HttpWebResponse)req.GetResponse())
                {
                    return res.StatusCode == HttpStatusCode.OK;
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.GetType() + " thrown.");
                Console.WriteLine("Message:" + exception.Message);
                return false;
            }
        }
    }
}