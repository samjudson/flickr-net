using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace FlickrNet.Internals
{
    internal static partial class FlickrResponder
    {
        public static async Task<string> GetDataResponseAsync(Flickr flickr, string baseUrl, IDictionary<string, string> parameters)
        {
            const string method = "POST";

            // Remove api key if it exists.
            if (parameters.ContainsKey("api_key")) parameters.Remove("api_key");
            if (parameters.ContainsKey("api_sig")) parameters.Remove("api_sig");

            if (!parameters.ContainsKey("oauth_consumer_key"))
                parameters.Add("oauth_consumer_key", flickr.ApiKey);

            // If OAuth Access Token is set then add token and generate signature.
            if (!String.IsNullOrEmpty(flickr.OAuthAccessToken) && !parameters.ContainsKey("oauth_token"))
            {
                OAuthGetBasicParameters(parameters);
                parameters.Add("oauth_token", flickr.OAuthAccessToken);
            }
            if (!String.IsNullOrEmpty(flickr.OAuthAccessTokenSecret) && !parameters.ContainsKey("oauth_signature"))
            {
                var sig = flickr.OAuthCalculateSignature(method, baseUrl, parameters, flickr.OAuthAccessTokenSecret);
                parameters.Add("oauth_signature", sig);
            }

            // Calculate post data, content header and auth header
            var data = OAuthCalculatePostData(parameters);
            var authHeader = OAuthCalculateAuthHeader(parameters);

            // Download data.
            try
            {
                return await DownloadDataAsync(method, baseUrl, data, PostContentType, authHeader);
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (response == null) throw;

                if (response.StatusCode != HttpStatusCode.BadRequest &&
                    response.StatusCode != HttpStatusCode.Unauthorized) throw;

                var stream = response.GetResponseStream();
                if (stream == null) throw;

                using (var responseReader = new StreamReader(stream))
                {
                    var responseData = responseReader.ReadToEnd();
                    throw new OAuthException(responseData, ex);
                }
            }
        }

    }
}
