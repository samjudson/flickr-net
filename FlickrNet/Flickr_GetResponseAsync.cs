using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections.Generic;

#pragma warning disable CS0618 // Type or member is obsolete

namespace FlickrNet
{
    public partial class Flickr
    {
        private void GetResponseEvent<T>(Dictionary<string, string> parameters, EventHandler<FlickrResultArgs<T>> handler) where T : IFlickrParsable, new()
        {
            GetResponseAsync<T>(
                parameters,
                r =>
                {
                    handler(this, new FlickrResultArgs<T>(r));
                });
        }

        private void GetResponseAsync<T>(Dictionary<string, string> parameters, Action<FlickrResult<T>> callback) where T : IFlickrParsable, new()
        {
            CheckApiKey();

            parameters["api_key"] = ApiKey;

            // If performing one of the old 'flickr.auth' methods then use old authentication details.
            string method = parameters["method"];
            
            if (method.StartsWith("flickr.auth", StringComparison.Ordinal) && !method.EndsWith("oauth.checkToken", StringComparison.Ordinal))
            {
                if (!string.IsNullOrEmpty(AuthToken)) parameters["auth_token"] = AuthToken;
            }
            else
            {
                // If OAuth Token exists or no authentication required then use new OAuth
                if (!string.IsNullOrEmpty(OAuthAccessToken) || string.IsNullOrEmpty(AuthToken))
                {
                    OAuthGetBasicParameters(parameters);
                    if (!string.IsNullOrEmpty(OAuthAccessToken)) parameters["oauth_token"] = OAuthAccessToken;
                }
                else
                {
                    parameters["auth_token"] = AuthToken;
                }
            }


            var url = CalculateUri(parameters, !string.IsNullOrEmpty(sharedSecret));

            lastRequest = url;

            try
            {
                FlickrResponder.GetDataResponseAsync(this, BaseUri.AbsoluteUri, parameters, (r)
                    =>
                    {
                        var result = new FlickrResult<T>();
                        if (r.HasError)
                        {
                            result.Error = r.Error;
                        }
                        else
                        {
                            try
                            {
                                lastResponse = r.Result;

                                var t = new T();
                                ((IFlickrParsable)t).Load(r.Result);
                                result.Result = t;
                                result.HasError = false;
                            }
                            catch (Exception ex)
                            {
                                result.Error = ex;
                            }
                        }

                        if (callback != null) callback(result);
                    });
            }
            catch (Exception ex)
            {
                var result = new FlickrResult<T>();
                result.Error = ex;
                if (null != callback) callback(result);
            }

        }

        private void DoGetResponseAsync<T>(Uri url, Action<FlickrResult<T>> callback) where T : IFlickrParsable, new()
        {
            string postContents = string.Empty;

            if (url.AbsoluteUri.Length > 2000)
            {
                postContents = url.Query.Substring(1);
                url = new Uri(url, string.Empty);
            }

            var result = new FlickrResult<T>();

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.BeginGetRequestStream(requestAsyncResult =>
            {
                using (Stream s = request.EndGetRequestStream(requestAsyncResult))
                {
                    using (StreamWriter sw = new StreamWriter(s))
                    {
                        sw.Write(postContents);
                        sw.Close();
                    }
                    s.Close();
                }

                request.BeginGetResponse(responseAsyncResult =>
                {
                    try
                    {
                        var response = (HttpWebResponse)request.EndGetResponse(responseAsyncResult);
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            string responseXml = sr.ReadToEnd();

                            lastResponse = responseXml;
                            
                            var t = new T();
                            ((IFlickrParsable)t).Load(responseXml);
                            result.Result = t;
                            result.HasError = false;

                            sr.Close();
                        }

                        if (null != callback) callback(result);

                    }
                    catch(Exception ex)
                    {
                        result.Error = ex;
                        if (null != callback) callback(result);
                    }
                }, null);

            }, null);

        }
    }
}
