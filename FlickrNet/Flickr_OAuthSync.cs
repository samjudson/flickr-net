using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get an <see cref="OAuthRequestToken"/> for the given callback URL.
        /// </summary>
        /// <remarks>Specify 'oob' as the callback for no callback to be performed.</remarks>
        /// <param name="callback">The callback Uri, or 'oob' if no callback is to be performed.</param>
        /// <returns></returns>
        public OAuthRequestToken OAuthGetRequestToken(string callback)
        {
            string url = "http://www.flickr.com/services/oauth/request_token";

            Dictionary<string, string> parameters = OAuthGetBasicParameters();

            parameters.Add("oauth_callback", callback);

            string sig = OAuthCalculateSignature("POST", url, parameters, null);

            parameters.Add("oauth_signature", sig);

            string response = FlickrResponder.GetDataResponse(this, url, parameters); ;

            return OAuthRequestToken.ParseResponse(response);
        }

        /// <summary>
        /// Returns an access token for the given request token, secret and authorization verifier.
        /// </summary>
        /// <param name="requestToken"></param>
        /// <param name="verifier"></param>
        /// <returns></returns>
        public OAuthAccessToken OAuthGetAccessToken(OAuthRequestToken requestToken, string verifier)
        {
            return OAuthGetAccessToken(requestToken.Token, requestToken.TokenSecret, verifier);
        }

        /// <summary>
        /// For a given request token and verifier string return an access token.
        /// </summary>
        /// <param name="requestToken"></param>
        /// <param name="requestTokenSecret"></param>
        /// <param name="verifier"></param>
        /// <returns></returns>
        public OAuthAccessToken OAuthGetAccessToken(string requestToken, string requestTokenSecret, string verifier)
        {
            string url = "http://www.flickr.com/services/oauth/access_token";

            Dictionary<string, string> parameters = OAuthGetBasicParameters();

            parameters.Add("oauth_verifier", verifier);
            parameters.Add("oauth_token", requestToken);

            string sig = OAuthCalculateSignature("POST", url, parameters, requestTokenSecret);

            parameters.Add("oauth_signature", sig);

            string response = FlickrResponder.GetDataResponse(this, url, parameters);

            return FlickrNet.OAuthAccessToken.ParseResponse(response);
        }


    }
}
