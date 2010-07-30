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
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void AuthGetFrobAsync(Action<FlickrResult<string>> callback)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.getFrob");

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
                {
                    FlickrResult<string> result = new FlickrResult<string>();
                    result.HasError = r.HasError;
                    if (r.HasError)
                    {
                        result.Error = r.Error;
                    }
                    else
                    {
                        result.Result = r.Result.GetElementValue("frob");
                    }
                    callback(result);

                });

        }

        /// <summary>
        /// Calculates the URL to redirect the user to Flickr web site for
        /// authentication. Used by desktop application. 
        /// See <see cref="AuthGetFrob"/> for example code.
        /// </summary>
        /// <param name="frob">The FROB to be used for authentication.</param>
        /// <param name="authLevel">The <see cref="AuthLevel"/> stating the maximum authentication level your application requires.</param>
        /// <returns>The url to redirect the user to.</returns>
        public string AuthCalcUrlAsync(string frob, AuthLevel authLevel)
        {
            if (_sharedSecret == null) throw new SignatureRequiredException();

            string hash = _sharedSecret + "api_key" + _apiKey + "frob" + frob + "perms" + UtilityMethods.AuthLevelToString(authLevel);
            hash = UtilityMethods.MD5Hash(hash);
            string url = AuthUrl + "?api_key=" + _apiKey + "&perms=" + UtilityMethods.AuthLevelToString(authLevel) + "&frob=" + frob;
            url += "&api_sig=" + hash;

            return url;
        }

        /// <summary>
        /// Calculates the URL to redirect the user to Flickr web site for
        /// authentication. Used by Web applications. 
        /// See <see cref="AuthGetFrob"/> for example code.
        /// </summary>
        /// <param name="authLevel">The <see cref="AuthLevel"/> stating the maximum authentication level your application requires.</param>
        /// <returns>The url to redirect the user to.</returns>
        public string AuthCalcWebUrlAsync(AuthLevel authLevel)
        {
            CheckApiKey();

            CheckSigned();

            string hash = _sharedSecret + "api_key" + _apiKey + "perms" + UtilityMethods.AuthLevelToString(authLevel);
            hash = UtilityMethods.MD5Hash(hash);
            string url = AuthUrl + "?api_key=" + _apiKey + "&perms=" + UtilityMethods.AuthLevelToString(authLevel);
            url += "&api_sig=" + hash;

            return url;
        }

        /// <summary>
        /// Calculates the URL to redirect the user to Flickr mobile web site for
        /// authentication. Used by Mobile Web applications. 
        /// See <see cref="AuthGetFrob"/> for example code.
        /// </summary>
        /// <param name="authLevel">The <see cref="AuthLevel"/> stating the maximum authentication level your application requires.</param>
        /// <returns>The url to redirect the user to.</returns>
        public string AuthCalcMobileUrlAsync(AuthLevel authLevel)
        {
            CheckApiKey();

            CheckSigned();

            string hash = _sharedSecret + "api_key" + _apiKey + "perms" + UtilityMethods.AuthLevelToString(authLevel);
            hash = UtilityMethods.MD5Hash(hash);
            string url = AuthUrl.Replace("www.flickr.com", "m.flickr.com") + "?api_key=" + _apiKey + "&perms=" + UtilityMethods.AuthLevelToString(authLevel);
            url += "&api_sig=" + hash;

            return url;
        }

        /// <summary>
        /// After the user has authenticated your application on the flickr web site call this 
        /// method with the FROB (either stored from <see cref="AuthGetFrob"/> or returned in the URL
        /// from the Flickr web site) to get the users token.
        /// </summary>
        /// <param name="frob">The string containing the FROB.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void AuthGetTokenAsync(string frob, Action<FlickrResult<Auth>> callback)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.getToken");
            parameters.Add("frob", frob);

            GetResponseAsync<Auth>(parameters, (r) => {
                if( !r.HasError )
                {
                    AuthToken = r.Result.Token;
                }
                callback(r);
            });
        }

        /// <summary>
        /// Gets the full token details for a given mini token, entered by the user following a 
        /// web based authentication.
        /// </summary>
        /// <param name="miniToken">The mini token.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void AuthGetFullTokenAsync(string miniToken, Action<FlickrResult<Auth>> callback)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.getFullToken");
            parameters.Add("mini_token", miniToken.Replace("-", ""));

            GetResponseAsync<Auth>(parameters, (r) => {
                if( !r.HasError )
                {
                    AuthToken = r.Result.Token;
                }
                callback(r);
            });
        }

        /// <summary>
        /// Checks the currently set authentication token with the flickr service to make
        /// sure it is still valid.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void AuthCheckTokenAsync(Action<FlickrResult<Auth>> callback)
        {
            AuthCheckTokenAsync(AuthToken, callback);
        }

        /// <summary>
        /// Checks a authentication token with the flickr service to make
        /// sure it is still valid.
        /// </summary>
        /// <param name="token">The authentication token to check.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void AuthCheckTokenAsync(string token, Action<FlickrResult<Auth>> callback)
        {
            CheckSigned();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.auth.checkToken");
            parameters.Add("auth_token", token);

            GetResponseAsync<Auth>(parameters, (r) =>
            {
                if (!r.HasError)
                {
                    AuthToken = r.Result.Token;
                }
                callback(r);
            });
        }

    }
}
