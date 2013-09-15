using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace FlickrNet.Internals
{
    /// <summary>
    /// Flickr library interaction with the web goes in here.
    /// </summary>
    internal static partial class FlickrResponder
    {
        public const string PostContentType = "application/x-www-form-urlencoded";


        /// <summary>
        /// Returns the string for the Authorisation header to be used for OAuth authentication.
        /// Parameters other than OAuth ones are ignored.
        /// </summary>
        /// <param name="parameters">OAuth and other parameters.</param>
        /// <returns></returns>
        public static string OAuthCalculateAuthHeader(IDictionary<string, string> parameters)
        {
            // Silverlight < 5 doesn't support modification of the Authorization header, so all data must be sent in post body.
#if SILVERLIGHT
            return "";
#else
            var sb = new StringBuilder("OAuth ");
            foreach (var pair in parameters)
            {
                if (pair.Key.StartsWith("oauth"))
                {
                    sb.Append(pair.Key + "=\"" + Uri.EscapeDataString(pair.Value) + "\",");
                }
            }
            return sb.Remove(sb.Length - 1, 1).ToString();
#endif
        }

        /// <summary>
        /// Calculates for form encoded POST data to be included in the body of an OAuth call.
        /// </summary>
        /// <remarks>This will include all non-OAuth parameters. The OAuth parameter will be included in the Authentication header.</remarks>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string OAuthCalculatePostData(IDictionary<string, string> parameters)
        {
            string data = String.Empty;
            foreach (var pair in parameters)
            {
                // Silverlight < 5 doesn't support modification of the Authorization header, so all data must be sent in post body.
#if SILVERLIGHT
                data += pair.Key + "=" + UtilityMethods.EscapeOAuthString(pair.Value) + "&";
#else
                if (!pair.Key.StartsWith("oauth"))
                {
                    data += pair.Key + "=" + Uri.EscapeDataString(pair.Value) + "&";
                }
#endif
            }
            return data;
        }

        /// <summary>
        /// Populates the given dictionary with the basic OAuth parameters, oauth_timestamp, oauth_noonce etc.
        /// </summary>
        /// <param name="parameters">Dictionary to be populated with the OAuth parameters.</param>
        public static void OAuthGetBasicParameters(IDictionary<string, string> parameters)
        {
            var oAuthParameters = OAuthGetBasicParameters();
            foreach (var k in oAuthParameters)
            {
                parameters.Add(k.Key, k.Value);
            }
        }

        /// <summary>
        /// Returns a new dictionary containing the basic OAuth parameters.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<string, string>> OAuthGetBasicParameters()
        {
            var oauthtimestamp = UtilityMethods.DateToUnixTimestamp(DateTime.UtcNow);
            var oauthnonce = Guid.NewGuid().ToString("N");

            var parameters = new Dictionary<string, string>
                                 {
                                     {"oauth_nonce", oauthnonce},
                                     {"oauth_timestamp", oauthtimestamp},
                                     {"oauth_version", "1.0"},
                                     {"oauth_signature_method", "HMAC-SHA1"}
                                 };
            return parameters;
        }

        public static byte[] CreateUploadData(Stream imageStream, string filename,
                                              IDictionary<string, string> parameters, string boundary)
        {
            var body = new MimeBody
                                {
                                    Boundary = boundary,
                                    MimeParts = parameters
                                    .Where(p => !p.Key.StartsWith("oauth_"))
                                    .Select(p => (MimePart)new FormDataPart { Name = p.Key, Value = p.Value}).ToList()
                                };

            var binaryPart = new BinaryPart
                                 {
                                     Name = "photo",
                                     ContentType = "image/jpeg",
                                     Filename = filename
                                 };
            binaryPart.LoadContent(imageStream);
            body.MimeParts.Add(binaryPart);

            using (var stream = new MemoryStream())
            {
                body.WriteTo(stream);
                return stream.ToArray();
            }
        }

        public static string CreateBoundary()
        {
            return "----FLICKR_MIME_" + DateTime.Now.ToString("yyyyMMddhhmmss", DateTimeFormatInfo.InvariantInfo) + "--";
        }
    }
}