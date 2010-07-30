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
            GetResponseAsync<T>(parameters, (r) =>
            {
                handler(this, new FlickrResultArgs<T>(r));
            });
        }

        private void GetResponseAsync<T>(Dictionary<string, string> parameters, Action<FlickrResult<T>> callback) where T : IFlickrParsable, new()
        {
            CheckApiKey();

            parameters["api_key"] = ApiKey;

            if (!String.IsNullOrEmpty(AuthToken))
            {
                parameters["auth_token"] = AuthToken;
            }

            Uri url;
            if (!String.IsNullOrEmpty(_sharedSecret))
                url = CalculateUri(parameters, true);
            else
                url = CalculateUri(parameters, false);

            _lastRequest = url.AbsoluteUri;

            DoGetResponseAsync<T>(url, callback);

        }

        private void DoGetResponseAsync<T>(Uri url, Action<FlickrResult<T>> callback) where T : IFlickrParsable, new()
        {
            string postContents = String.Empty;

            if (url.AbsoluteUri.Length > 2000)
            {
                postContents = url.Query.Substring(1);
                url = new Uri(url, "");
            }

            WebClient client = new WebClient();
            client.UploadStringCompleted += delegate(object sender, UploadStringCompletedEventArgs e)
            {
                FlickrResult<T> result = new FlickrResult<T>();

                if (e.Error != null)
                {
                    result.Error = e.Error;
                    callback(result);
                    return;
                }

                try
                {
                    string responseXml = e.Result;

                    _lastResponse = responseXml;

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

                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Error = ex;
                }

                if (callback != null)
                {
                    callback(result);
                }

            };

            client.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            client.UploadStringAsync(url, "POST", postContents);
        }
    }
}
