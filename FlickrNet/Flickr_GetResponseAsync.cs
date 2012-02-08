using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Collections.Generic;

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
            
            if (method.StartsWith("flickr.auth") && !method.EndsWith("oauth.checkToken"))
            {
                if (!String.IsNullOrEmpty(AuthToken)) parameters["auth_token"] = AuthToken;
            }
            else
            {
                // If OAuth Token exists or no authentication required then use new OAuth
                if (!String.IsNullOrEmpty(OAuthAccessToken) || String.IsNullOrEmpty(AuthToken))
                {
                    OAuthGetBasicParameters(parameters);
                    if (!String.IsNullOrEmpty(OAuthAccessToken)) parameters["oauth_token"] = OAuthAccessToken;
                }
                else
                {
                    parameters["auth_token"] = AuthToken;
                }
            }


            Uri url;
            if (!String.IsNullOrEmpty(sharedSecret))
                url = CalculateUri(parameters, true);
            else
                url = CalculateUri(parameters, false);

            lastRequest = url.AbsoluteUri;

            try
            {
                FlickrResponder.GetDataResponseAsync(this, BaseUri.AbsoluteUri, parameters, (r)
                    =>
                    {
                        FlickrResult<T> result = new FlickrResult<T>();
                        if (r.HasError)
                        {
                            result.Error = r.Error;
                        }
                        else
                        {
                            lastResponse = r.Result;

                            XmlReaderSettings settings = new XmlReaderSettings();
                            settings.IgnoreWhitespace = true;
                            XmlReader reader = XmlReader.Create(new StringReader(r.Result), settings);

                            if (!reader.ReadToDescendant("rsp"))
                            {
                                throw new XmlException("Unable to find response element 'rsp' in Flickr response");
                            }
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.LocalName == "stat" && reader.Value == "fail")
                                    throw ExceptionHandler.CreateResponseException(reader);
                                continue;
                            }

                            reader.MoveToElement();
                            reader.Read();

                            T t = new T();
                            ((IFlickrParsable)t).Load(reader);
                            result.Result = t;
                            result.HasError = false;
                        }

                        if (callback != null) callback(result);
                    });
            }
            catch (Exception ex)
            {
                FlickrResult<T> result = new FlickrResult<T>();
                result.Error = ex;
                if (null != callback) callback(result);
            }

        }

        private void DoGetResponseAsync<T>(Uri url, Action<FlickrResult<T>> callback) where T : IFlickrParsable, new()
        {
            string postContents = String.Empty;

            if (url.AbsoluteUri.Length > 2000)
            {
                postContents = url.Query.Substring(1);
                url = new Uri(url, String.Empty);
            }

            FlickrResult<T> result = new FlickrResult<T>();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
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
                        HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(responseAsyncResult);
                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            string responseXml = sr.ReadToEnd();

                            lastResponse = responseXml;

                            XmlReaderSettings settings = new XmlReaderSettings();
                            settings.IgnoreWhitespace = true;
                            XmlReader reader = XmlReader.Create(new StringReader(responseXml), settings);

                            if (!reader.ReadToDescendant("rsp"))
                            {
                                throw new XmlException("Unable to find response element 'rsp' in Flickr response");
                            }
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.LocalName == "stat" && reader.Value == "fail")
                                    throw ExceptionHandler.CreateResponseException(reader);
                                continue;
                            }

                            reader.MoveToElement();
                            reader.Read();

                            T t = new T();
                            ((IFlickrParsable)t).Load(reader);
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
                        return;
                    }
                }, null);

            }, null);

            //WebClient client = new WebClient();
            //client.UploadStringCompleted += delegate(object sender, UploadStringCompletedEventArgs e)
            //{
            //    FlickrResult<T> result = new FlickrResult<T>();

            //    if (e.Error != null)
            //    {
            //        result.Error = e.Error;
            //        callback(result);
            //        return;
            //    }

            //    try
            //    {
            //        string responseXml = e.Result;

            //        lastResponse = responseXml;

            //        XmlReaderSettings settings = new XmlReaderSettings();
            //        settings.IgnoreWhitespace = true;
            //        XmlReader reader = XmlReader.Create(new StringReader(responseXml), settings);

            //        if (!reader.ReadToDescendant("rsp"))
            //        {
            //            throw new XmlException("Unable to find response element 'rsp' in Flickr response");
            //        }
            //        while (reader.MoveToNextAttribute())
            //        {
            //            if (reader.LocalName == "stat" && reader.Value == "fail")
            //                throw ExceptionHandler.CreateResponseException(reader);
            //            continue;
            //        }

            //        reader.MoveToElement();
            //        reader.Read();

            //        T t = new T();
            //        ((IFlickrParsable)t).Load(reader);
            //        result.Result = t;
            //        result.HasError = false;

            //    }
            //    catch (Exception ex)
            //    {
            //        result.HasError = true;
            //        result.Error = ex;
            //    }

            //    if (callback != null)
            //    {
            //        callback(result);
            //    }

            //};

            //client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            //client.UploadStringAsync(url, "POST", postContents);
        }
    }
}
