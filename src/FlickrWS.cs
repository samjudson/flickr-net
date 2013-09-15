using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using FlickrNet.Internals;

namespace FlickrNet
{
    public partial class Flickr
    {
        public async Task<OAuthRequestToken> OAuthRequestTokenAsync(string callbackUrl)
        {
            const string url = "http://www.flickr.com/services/oauth/request_token";

            IDictionary<string, string> parameters = new Dictionary<string, string>();
            FlickrResponder.OAuthGetBasicParameters(parameters);
            parameters.Add("oauth_callback", callbackUrl);
            parameters.Add("oauth_consumer_key", ApiKey);

            var sig = OAuthCalculateSignature("POST", url, parameters, null);

            parameters.Add("oauth_signature", sig);

            var data = FlickrResponder.OAuthCalculatePostData(parameters);
            var authHeader = FlickrResponder.OAuthCalculateAuthHeader(parameters);

            var response = await FlickrResponder.DownloadDataAsync("POST", url, data, null, authHeader);

            return OAuthRequestToken.ParseResponse(response);
        }

        public async Task<OAuthAccessToken> OAuthAccessTokenAsync(string requestToken, string requestTokenSecret, string verifier)
        {
            const string url = "http://www.flickr.com/services/oauth/access_token";

            if (verifier.Contains("://"))
            {
                var uri = new Uri(verifier);
                verifier =
                    uri.Query.Split(new[] {'&'})
                       .Select(s => s.Split(new[] {'='}))
                       .First(s => s[0] == "oauth_verifier")[1];
            }
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            FlickrResponder.OAuthGetBasicParameters(parameters);

            parameters.Add("oauth_consumer_key", ApiKey);
            parameters.Add("oauth_verifier", verifier);
            parameters.Add("oauth_token", requestToken);

            var sig = OAuthCalculateSignature("POST", url, parameters, requestTokenSecret);

            parameters.Add("oauth_signature", sig);

            var data = FlickrResponder.OAuthCalculatePostData(parameters);
            var authHeader = FlickrResponder.OAuthCalculateAuthHeader(parameters);
            var response = await FlickrResponder.DownloadDataAsync("POST", url, data, null, authHeader);

            var accessToken = FlickrNet.OAuthAccessToken.ParseResponse(response);

            // Set current access token.
            OAuthAccessToken = accessToken.Token;
            OAuthAccessTokenSecret = accessToken.TokenSecret;

            return accessToken;
        }

        public string OAuthCalculateAuthorizationUrl(string requestToken, AuthLevel perms, bool mobile = false)
        {
            var permsString = (perms == AuthLevel.None) ? "" : "&perms=" + UtilityMethods.AuthLevelToString(perms);

            return "https://" + (mobile ? "m" : "www") + ".flickr.com/services/oauth/authorize?oauth_token=" +
                   requestToken + permsString;
        }

        internal async Task<T> GetResponseAsync<T>(IDictionary<string, string> parameters) where T : class, IFlickrParsable, new()
        {
            if (!parameters.ContainsKey("oauth_consumer_key"))
                parameters.Add("oauth_consumer_key", ApiKey);

            var result = await FlickrResponder.GetDataResponseAsync(this, BaseApiUrl, parameters);
            var typeInfo = typeof (T).GetTypeInfo();
            if (typeInfo.IsEnum)
            {
                return default(T);
            }
            if (typeInfo.ImplementedInterfaces.Any(t => t.Name == "IFlickrParsable"))
            {
                using (var reader = XmlReader.Create(new StringReader(result), new XmlReaderSettings{ IgnoreWhitespace = true}))
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

            var sb = new StringBuilder();
            foreach (var pair in parameters.OrderBy(p => p.Key))
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
            var crypt = Windows.Security.Cryptography.Core.MacAlgorithmProvider.OpenAlgorithm("HMAC_SHA1");

            var input = Windows.Security.Cryptography.CryptographicBuffer.ConvertStringToBinary(basestring, Windows.Security.Cryptography.BinaryStringEncoding.Utf8);
            var keyBuffer = Windows.Security.Cryptography.CryptographicBuffer.CreateFromByteArray(key);
            var cryptKey = crypt.CreateKey(keyBuffer);
            var signBuffer = Windows.Security.Cryptography.Core.CryptographicEngine.Sign(cryptKey, input);

            return Windows.Security.Cryptography.CryptographicBuffer.EncodeToBase64String(signBuffer);
        }

        public async Task<string> UploadPicture(Stream stream, string filename, string title, string description, string tags, bool isPublic, bool isFamily, bool isFriend, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
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

            var response = await FlickrResponder.UploadDataAsync(UploadUrl, data, contentTypeHeader, oauthHeader);

            var match = Regex.Match(response, "<photoid>(\\d+)</photoid>");

            if (!match.Success)
                throw new FlickrException("Unable to determine photo id from upload response: " + response);

            return match.Groups[1].Value;
        }
    }
}
