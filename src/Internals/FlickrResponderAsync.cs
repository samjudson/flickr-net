using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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
        /// <param name="callback"></param>
        /// <returns></returns>
        public static void GetDataResponseAsync(Flickr flickr, string baseUrl, Dictionary<string, string> parameters,
                                                Action<FlickrResult<string>> callback)
        {
            GetDataResponseOAuthAsync(flickr, baseUrl, parameters, callback);
        }

        private static void GetDataResponseOAuthAsync(Flickr flickr, string baseUrl,
                                                      Dictionary<string, string> parameters,
                                                      Action<FlickrResult<string>> callback)
        {
            string method = "POST";

            // Remove api key if it exists.
            if (parameters.ContainsKey("api_key")) parameters.Remove("api_key");
            if (parameters.ContainsKey("api_sig")) parameters.Remove("api_sig");

            // If OAuth Access Token is set then add token and generate signature.
            if (!String.IsNullOrEmpty(flickr.OAuthAccessToken) && !parameters.ContainsKey("oauth_token"))
            {
                parameters.Add("oauth_token", flickr.OAuthAccessToken);
            }
            if (!String.IsNullOrEmpty(flickr.OAuthAccessTokenSecret) && !parameters.ContainsKey("oauth_signature"))
            {
                string sig = flickr.OAuthCalculateSignature(method, baseUrl, parameters, flickr.OAuthAccessTokenSecret);
                parameters.Add("oauth_signature", sig);
            }

            // Calculate post data, content header and auth header
            string data = OAuthCalculatePostData(parameters);
            string authHeader = OAuthCalculateAuthHeader(parameters);

            // Download data.
            try
            {
                DownloadDataAsync(method, baseUrl, data, PostContentType, authHeader, callback);
            }
            catch (WebException ex)
            {
                //if (ex.Status != WebExceptionStatus.ProtocolError) throw;

                var response = ex.Response as HttpWebResponse;
                if (response == null) throw;

                if (response.StatusCode != HttpStatusCode.BadRequest &&
                    response.StatusCode != HttpStatusCode.Unauthorized) throw;

                using (var responseReader = new StreamReader(response.GetResponseStream()))
                {
                    string responseData = responseReader.ReadToEnd();

                    throw new OAuthException(responseData, ex);
                }
            }
        }

        private static void DownloadDataAsync(string method, string baseUrl, string data, string contentType,
                                              string authHeader, Action<FlickrResult<string>> callback)
        {
#if NETFX_CORE
            var client = new System.Net.Http.HttpClient();
            if (!String.IsNullOrEmpty(contentType)) client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", contentType);
            if (!String.IsNullOrEmpty(authHeader)) client.DefaultRequestHeaders.Add("Authorization", authHeader);

            if (method == "POST")
            {
                var content = client.PostAsync(baseUrl, new System.Net.Http.StringContent(data, System.Text.Encoding.UTF8, contentType)).Result.Content;
                var stringContent = content as System.Net.Http.StringContent;
                var result = new FlickrResult<string> {Result = content.ReadAsStringAsync().Result};
                callback(result);
            }
            else
            {
                var content = client.GetStringAsync(baseUrl).Result;
                var result = new FlickrResult<string> {Result = content};
                callback(result);
            }
#else
            var client = new WebClient();
            if (!String.IsNullOrEmpty(contentType)) client.Headers["Content-Type"] = contentType;
            if (!String.IsNullOrEmpty(authHeader)) client.Headers["Authorization"] = authHeader;

             if (method == "POST")
            {
                client.UploadStringCompleted += delegate(object sender, UploadStringCompletedEventArgs e)
                                                    {
                                                        var result = new FlickrResult<string>();
                                                        if (e.Error != null)
                                                        {
                                                            result.Error = e.Error;
                                                            callback(result);
                                                            return;
                                                        }

                                                        result.Result = e.Result;
                                                        callback(result);
                                                        return;
                                                    };

                client.UploadStringAsync(new Uri(baseUrl), data);
            }
            else
            {
                client.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs e)
                                                      {
                                                          var result = new FlickrResult<string>();
                                                          if (e.Error != null)
                                                          {
                                                              result.Error = e.Error;
                                                              callback(result);
                                                              return;
                                                          }

                                                          result.Result = e.Result;
                                                          callback(result);
                                                          return;
                                                      };

                client.DownloadStringAsync(new Uri(baseUrl));
            }
#endif

        }
    }
}