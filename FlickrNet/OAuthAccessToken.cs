using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The access authentication token return by Flickr after a successful authentication.
    /// </summary>
    [Serializable]
    public class OAuthAccessToken : IFlickrParsable
    {
        /// <summary>
        /// The access token string.
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// The access token secret.
        /// </summary>
        public string TokenSecret { get; set; }
        /// <summary>
        /// The user id of the authenticated user.
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// The username (screenname) of the authenticated user.
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The full name of the authenticated user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Parses a URL parameter encoded string and returns a new <see cref="OAuthAccessToken"/>
        /// </summary>
        /// <param name="response">A URL parameter encoded string, e.g. "oauth_token=ABC&amp;oauth_token_secret=DEF&amp;user_id=1234567@N00".</param>
        /// <returns></returns>
        public static OAuthAccessToken ParseResponse(string response)
        {
            Dictionary<string, string> parts = UtilityMethods.StringToDictionary(response);

            OAuthAccessToken token = new OAuthAccessToken();
            if( parts.ContainsKey("oauth_token") )
                token.Token = parts["oauth_token"];
            if (parts.ContainsKey("oauth_token_secret"))
                token.TokenSecret = parts["oauth_token_secret"];
            if (parts.ContainsKey("user_nsid"))
                token.UserId = parts["user_nsid"];
            if (parts.ContainsKey("fullname"))
                token.FullName = parts["fullname"];
            if (parts.ContainsKey("username"))
                token.Username = parts["username"];

            return token;
        }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "auth")
            {
                UtilityMethods.CheckParsingException(reader);
                return;
            }

            reader.ReadToDescendant("access_token");

            Token = reader.GetAttribute("oauth_token");
            TokenSecret = reader.GetAttribute("oauth_token_secret");
        }

        #endregion
    }
}
