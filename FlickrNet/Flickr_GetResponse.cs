using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace FlickrNet
{
    public partial class Flickr
    {

        private T GetResponseNoCache<T>(Dictionary<string, string> parameters) where T : IFlickrParsable, new()
        {
            return GetResponse<T>(parameters, TimeSpan.MinValue);
        }

        private T GetResponseCache<T>(Dictionary<string, string> parameters) where T : IFlickrParsable, new()
        {
            return GetResponse<T>(parameters, Cache.CacheTimeout);
        }

        private T GetResponse<T>(Dictionary<string, string> parameters, TimeSpan cacheTimeout) where T : IFlickrParsable, new()
        {
            // Flow for GetResponse.
            // 1. Check API Key
            // 2. Calculate Cache URL.
            // 3. Check Cache for URL.
            // 4. Get Response if not in cache.
            // 5. Write Cache.
            // 6. Parse Response.

            CheckApiKey();

            parameters["api_key"] = ApiKey;

            // If performing one of the old 'flickr.auth' methods then use old authentication details.
            var method = parameters["method"];

            // User of obsolete AuthToken property while we transition over to the new OAuth authentication process.
#pragma warning disable 612,618
            if (method.StartsWith("flickr.auth") && !method.EndsWith("oauth.checkToken"))
            {
                if (!String.IsNullOrEmpty(AuthToken)) parameters["auth_token"] = AuthToken;
            }
            else
            {
                // If OAuth Token exists or no authentication required then use new OAuth
                if (!String.IsNullOrEmpty(OAuthAccessToken) || String.IsNullOrEmpty(AuthToken))
                {
                    parameters.Remove("api_key");
                    OAuthGetBasicParameters(parameters);
                    if (!String.IsNullOrEmpty(OAuthAccessToken)) parameters["oauth_token"] = OAuthAccessToken;
                }
                else
                {
                    parameters["auth_token"] = AuthToken;
                }
            }
#pragma warning restore 612,618

            var url = CalculateUri(parameters, !String.IsNullOrEmpty(sharedSecret));

            lastRequest = url;

            string responseXml;

            if (InstanceCacheDisabled)
            {
                responseXml = FlickrResponder.GetDataResponse(this, BaseUri.AbsoluteUri, parameters);
            }
            else
            {
                var urlComplete = url;

                var cached = (ResponseCacheItem)Cache.Responses.Get(urlComplete, cacheTimeout, true);
                if (cached != null)
                {
                    Debug.WriteLine("Cache hit.");
                    responseXml = cached.Response;
                }
                else
                {
                    Debug.WriteLine("Cache miss.");
                    responseXml = FlickrResponder.GetDataResponse(this, BaseUri.AbsoluteUri, parameters);

                    var resCache = new ResponseCacheItem(new Uri(urlComplete), responseXml, DateTime.UtcNow);

                    Cache.Responses.Shrink(Math.Max(0, Cache.CacheSizeLimit - responseXml.Length));
                    Cache.Responses[urlComplete] = resCache;
                }
            }

            lastResponse = responseXml;

            responseXml = responseXml.Trim();

            var reader = new XmlTextReader(new StringReader(responseXml))
                             {
                                 WhitespaceHandling = WhitespaceHandling.None
                             };

            if (!reader.ReadToDescendant("rsp"))
            {
                throw new XmlException("Unable to find response element 'rsp' in Flickr response");
            }
            while (reader.MoveToNextAttribute())
            {
                if (reader.LocalName == "stat" && reader.Value == "fail")
                    throw ExceptionHandler.CreateResponseException(reader);
            }

            reader.MoveToElement();
            reader.Read();

            var item = new T();
            item.Load(reader);

            return item;

        }

    }
}
