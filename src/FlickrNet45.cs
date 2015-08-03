using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using FlickrNet.Internals;

namespace FlickrNet
{
    public partial class Flickr
    {
        internal T GetResponse<T>(IDictionary<string, string> parameters) where T : class, IFlickrParsable, new()
        {
            if (!parameters.ContainsKey("oauth_consumer_key"))
                parameters.Add("oauth_consumer_key", ApiKey);

            var result = FlickrResponder.GetDataResponse(this, BaseApiUrl, parameters);

            LastResponse = result;

            if (typeof(T).IsEnum)
            {
                return default(T);
            }
            if (typeof(T).GetInterface("FlickrNet.IFlickrParsable") != null)
            {
                using (var reader = new XmlTextReader(new StringReader(result)))
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;

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

            return default(T);

        }

        internal async Task<T> GetResponseAsync<T>(IDictionary<string, string> parameters) where T : class, IFlickrParsable, new()
        {
            if( !parameters.ContainsKey("oauth_consumer_key"))
                parameters.Add("oauth_consumer_key", ApiKey);

            var result = await FlickrResponder.GetDataResponseAsync(this, BaseApiUrl, parameters);

            if (typeof(T).IsEnum)
            {
                return default(T);
            }
            if (typeof(T).GetInterface("FlickrNet.IFlickrParsable") != null)
            {
                using (var reader = new XmlTextReader(new StringReader(result)))
                {
                    reader.WhitespaceHandling = WhitespaceHandling.None;

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
            
            return default(T);
        }

        internal async Task GetResponseAsync(IDictionary<string, string> parameters)
        {
            await FlickrResponder.GetDataResponseAsync(this, BaseApiUrl, parameters);
        }

        internal string OAuthCalculateSignature(string method, string url, IDictionary<string, string> parameters,
                                              string tokenSecret)
        {
            var key = SharedSecret + "&" + tokenSecret;
            var keyBytes = Encoding.UTF8.GetBytes(key);

            var sorted = new SortedList<string, string>();
            foreach (var pair in parameters)
            {
                sorted.Add(pair.Key, pair.Value);
            }

            var sb = new StringBuilder();
            foreach (var pair in sorted)
            {
                sb.Append(pair.Key);
                sb.Append("=");
                sb.Append(UtilityMethods.EscapeOAuthString(pair.Value));
                sb.Append("&");
            }

            sb.Remove(sb.Length - 1, 1);

            var baseString = method + "&" + UtilityMethods.EscapeOAuthString(url) + "&" +
                                UtilityMethods.EscapeOAuthString(sb.ToString());

            var hash = Sha1Hash(keyBytes, baseString);

            return hash;
        }

        internal static string Sha1Hash(byte[] key, string basestring)
        {
            var sha1 = new System.Security.Cryptography.HMACSHA1(key);

            var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(basestring));

            return Convert.ToBase64String(hashBytes);
        }

        public string UploadPicture(Stream stream, string filename, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(title))
            {
                parameters.Add("title", title);
            }
            if (!string.IsNullOrEmpty(description))
            {
                parameters.Add("description", description);
            }
            if (!string.IsNullOrEmpty(tags))
            {
                parameters.Add("tags", tags);
            }

            parameters.Add("is_public", isPublic ? "1" : "0");
            parameters.Add("is_friend", isFriend ? "1" : "0");
            parameters.Add("is_family", isFamily ? "1" : "0");

            if (safetyLevel != SafetyLevel.None)
            {
                parameters.Add("safety_level", safetyLevel.ToString("D"));
            }
            if (contentType != ContentType.None)
            {
                parameters.Add("content_type", contentType.ToString("D"));
            }
            if (hiddenFromSearch != HiddenFromSearch.None)
            {
                parameters.Add("hidden", hiddenFromSearch.ToString("D"));
            }

            FlickrResponder.OAuthGetBasicParameters(parameters);
            parameters.Add("oauth_consumer_key", ApiKey);
            parameters.Add("oauth_token", OAuthAccessToken);
            parameters.Add("oauth_signature",
                           OAuthCalculateSignature("POST", UploadUrl, parameters, OAuthAccessTokenSecret));
            
            var boundary = FlickrResponder.CreateBoundary();
            var data = FlickrResponder.CreateUploadData(stream, filename, parameters, boundary);

            var oauthHeader = FlickrResponder.OAuthCalculateAuthHeader(parameters);
            var contentTypeHeader = "multipart/form-data; boundary=" + boundary;

            var response = FlickrResponder.UploadData(UploadUrl, data, contentTypeHeader, oauthHeader);

            var match = Regex.Match(response, "<photoid>(\\d+)</photoid>");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            using (var reader = XmlReader.Create(new StringReader(response), new XmlReaderSettings { IgnoreWhitespace = true }))
            {
                if (!reader.ReadToDescendant("rsp"))
                {
                    throw new XmlException("Unable to find response element 'rsp' in Flickr response");
                }
                while (reader.MoveToNextAttribute())
                {
                    if (reader.LocalName == "stat" && reader.Value == "fail")
                        throw ExceptionHandler.CreateResponseException(reader);
                }

            }

            throw new FlickrException("Unable to determine photo id from upload response: " + response);
        }
    }
}
