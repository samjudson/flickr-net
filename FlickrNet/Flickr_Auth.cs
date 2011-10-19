using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Retrieve a temporary FROB from the Flickr service, to be used in redirecting the
        /// user to the Flickr web site for authentication. Only required for desktop authentication.
        /// </summary>
        /// <remarks>
        /// Pass the FROB to the <see cref="AuthCalcUrl"/> method to calculate the url.
        /// </remarks>
        /// <example>
        /// <code>
        /// string frob = flickr.AuthGetFrob();
        /// string url = flickr.AuthCalcUrl(frob, AuthLevel.Read);
        /// 
        /// // redirect the user to the url above and then wait till they have authenticated and return to the app.
        /// 
        /// Auth auth = flickr.AuthGetToken(frob);
        /// 
        /// // then store the auth.Token for later use.
        /// string token = auth.Token;
        /// </code>
        /// </example>
        /// <returns>The FROB.</returns>
        public string AuthGetFrob()
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.getFrob");

            UnknownResponse response = GetResponseNoCache<UnknownResponse>(parameters);

            return response.GetXmlDocument().SelectSingleNode("frob/text()").Value;
        }


        /// <summary>
        /// After the user has authenticated your application on the flickr web site call this 
        /// method with the FROB (either stored from <see cref="AuthGetFrob"/> or returned in the URL
        /// from the Flickr web site) to get the users token.
        /// </summary>
        /// <param name="frob">The string containing the FROB.</param>
        /// <returns>A <see cref="Auth"/> object containing user and token details.</returns>
        public Auth AuthGetToken(string frob)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.getToken");
            parameters.Add("frob", frob);

            Auth auth = GetResponseNoCache<Auth>(parameters);
            AuthToken = auth.Token;
            return auth;
        }

        /// <summary>
        /// Gets the full token details for a given mini token, entered by the user following a 
        /// web based authentication.
        /// </summary>
        /// <param name="miniToken">The mini token.</param>
        /// <returns>An instance <see cref="Auth"/> class, detailing the user and their full token.</returns>
        public Auth AuthGetFullToken(string miniToken)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.getFullToken");
            parameters.Add("mini_token", miniToken.Replace("-", String.Empty));

            Auth auth = GetResponseNoCache<Auth>(parameters);
            AuthToken = auth.Token;
            return auth;
        }

        /// <summary>
        /// Checks the currently set authentication token with the flickr service to make
        /// sure it is still valid.
        /// </summary>
        /// <returns>The <see cref="Auth"/> object detailing the user for the token.</returns>
        public Auth AuthCheckToken()
        {
            CheckRequiresAuthentication();

            return AuthCheckToken(AuthToken);
        }

        /// <summary>
        /// Checks a authentication token with the flickr service to make
        /// sure it is still valid.
        /// </summary>
        /// <param name="token">The authentication token to check.</param>
        /// <returns>The <see cref="Auth"/> object detailing the user for the token.</returns>
        public Auth AuthCheckToken(string token)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.checkToken");
            parameters.Add("auth_token", token);

            return GetResponseNoCache<Auth>(parameters);
        }

    }
}
