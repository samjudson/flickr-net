using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace FlickrNet.Internals
{
    internal static partial class FlickrResponder
    {
        /// <summary>
        /// Gets a data response for the given base url and parameters, 
        /// either using OAuth or not depending on which parameters were passed in.
        /// </summary>
        /// <param name="flickr">The current instance of the <see cref="Flickr"/> class.</param>
        /// <param name="baseUrl">The base url to be called.</param>
        /// <param name="parameters">A dictionary of parameters.</param>
        /// <returns></returns>
        public static string GetDataResponse(Flickr flickr, string baseUrl, IDictionary<string, string> parameters)
        {
            const string method = "POST";

            // Remove api key if it exists.
            if (parameters.ContainsKey("api_key")) parameters.Remove("api_key");
            if (parameters.ContainsKey("api_sig")) parameters.Remove("api_sig");

            // If OAuth Access Token is set then add token and generate signature.
            if (!String.IsNullOrEmpty(flickr.OAuthAccessToken) && !parameters.ContainsKey("oauth_token"))
            {
                OAuthGetBasicParameters(parameters);
                parameters.Add("oauth_token", flickr.OAuthAccessToken);
            }
            if (!String.IsNullOrEmpty(flickr.OAuthAccessTokenSecret) && !parameters.ContainsKey("oauth_signature"))
            {
                var sig = flickr.OAuthCalculateSignature(method, baseUrl, parameters, flickr.OAuthAccessTokenSecret);
                parameters.Add("oauth_signature", sig);
            }

            // Calculate post data, content header and auth header
            string data = OAuthCalculatePostData(parameters);
            string authHeader = OAuthCalculateAuthHeader(parameters);

            flickr.LastRequest = baseUrl + "?" + data;

            // Download data.
            try
            {
                return DownloadData(method, baseUrl, data, PostContentType, authHeader);
            }
            catch (WebException ex)
            {
                if (ex.Status != WebExceptionStatus.ProtocolError) throw;

                var response = ex.Response as HttpWebResponse;
                if (response == null) throw;

                if (response.StatusCode != HttpStatusCode.BadRequest &&
                    response.StatusCode != HttpStatusCode.Unauthorized) throw;

                using (var responseReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseData = responseReader.ReadToEnd();
                    responseReader.Close();

                    Debug.WriteLine("OAuth response = " + responseData);

                    throw new OAuthException(responseData, ex);
                }
            }
        }


#if !WindowsCE
        private static string DownloadData(string method, string baseUrl, string data, string contentType,
                                           string authHeader)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                if (!String.IsNullOrEmpty(contentType)) client.Headers.Add("Content-Type", contentType);
                if (!String.IsNullOrEmpty(authHeader)) client.Headers.Add("Authorization", authHeader);

                if (method == "POST")
                    return client.UploadString(baseUrl, data);
                else
                    return client.DownloadString(baseUrl);
            }
        }
#else
        private static string DownloadData(string method, string baseUrl, string data, string contentType, string authHeader)
        {
            byte[] postArray = Encoding.UTF8.GetBytes(data);

            // Initialise the web request
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(baseUrl);
            req.Method = method;

            if (req.Method == "POST") req.ContentLength = postArray.Length;

            //req.UserAgent = Flickr.UserAgent;
            //if (Proxy != null) req.Proxy = Proxy;
            //req.Timeout = HttpTimeout;
            req.KeepAlive = false;

            if (data.Length > 0)
            {
                req.ContentType = "application/x-www-form-urlencoded";
                using (Stream dataStream = req.GetRequestStream())
                {
                    dataStream.Write(postArray, 0, postArray.Length);
                }
            }
            else
            {
                // This is needed in the Compact Framework
                // See for more details: http://msdn2.microsoft.com/en-us/library/1afx2b0f.aspx
                req.GetRequestStream().Close();
            }

            HttpWebResponse res = null;

            try
            {
                // Get response from the internet
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse res2 = (HttpWebResponse)ex.Response;
                    if (res2 != null)
                    {
                        throw new FlickrWebException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "HTTP Error {0}, {1}", (int)res2.StatusCode, res2.StatusDescription), ex);
                    }
                }
                throw new FlickrWebException(ex.Message, ex);
            }

            string responseString = string.Empty;

            using (StreamReader sr = new StreamReader(res.GetResponseStream()))
            {
                responseString = sr.ReadToEnd();
            }

            return responseString;
        }
#endif

        public static string UploadData(string   url, byte[] data, string contentType, string authorizationHeader)
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = contentType;
            client.Headers[HttpRequestHeader.Authorization] = authorizationHeader;

            var response = client.UploadData(url, data);

            return Encoding.UTF8.GetString(response);

        }
    }
}