using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlickrNet
{
    public partial class Flickr
    {
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

        public string UploadPicture(Stream stream, string filename, string videoUploadTest, string file, string videoTest, bool isFamily, bool isFriends, bool isPublic, ContentType contentType, SafetyLevel safetyLevel, HiddenFromSearch hiddenFromSearch)
        {
            throw new NotImplementedException();
        }
    }
}
