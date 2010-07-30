using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Returns the url to a group's page.
        /// </summary>
        /// <param name="groupId">The NSID of the group to fetch the url for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsGetGroupAsync(string groupId, Action<FlickrResult<string>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.getGroup");
            parameters.Add("group_id", groupId);

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<string> result = new FlickrResult<string>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = r.Result.GetAttributeValue("*", "url");
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns the url to a user's photos.
        /// </summary>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users photos.</returns>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsGetUserPhotosAsync(Action<FlickrResult<string>> callback)
        {
            CheckRequiresAuthentication();

            UrlsGetUserPhotosAsync(null, callback);
        }

        /// <summary>
        /// Returns the url to a user's photos.
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsGetUserPhotosAsync(string userId, Action<FlickrResult<string>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.getUserPhotos");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<string> result = new FlickrResult<string>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = r.Result.GetAttributeValue("*", "url");
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns the url to a user's profile.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsGetUserProfileAsync(Action<FlickrResult<string>> callback)
        {
            CheckRequiresAuthentication();

            UrlsGetUserProfileAsync(null, callback);
        }

        /// <summary>
        /// Returns the url to a user's profile.
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsGetUserProfileAsync(string userId, Action<FlickrResult<string>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.getUserProfile");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<string> result = new FlickrResult<string>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = r.Result.GetAttributeValue("*", "url");
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns gallery info, by url.
        /// </summary>
        /// <param name="url">The gallery's URL.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsLookupGalleryAsync(string url, Action<FlickrResult<Gallery>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.lookupGallery");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", url);

            GetResponseAsync<Gallery>(parameters, callback);
        }

        /// <summary>
        /// Returns a group NSID, given the url to a group's page or photo pool.
        /// </summary>
        /// <param name="urlToFind">The url to the group's page or photo pool.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsLookupGroupAsync(string urlToFind, Action<FlickrResult<string>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.lookupGroup");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", urlToFind);

            GetResponseAsync<UnknownResponse>(parameters, (r) =>
            {
                FlickrResult<string> result = new FlickrResult<string>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = r.Result.GetAttributeValue("*", "id");
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns a user NSID, given the url to a user's photos or profile.
        /// </summary>
        /// <param name="urlToFind">Thr url to the user's profile or photos page.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void UrlsLookupUserAsync(string urlToFind, Action<FlickrResult<FoundUser>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.lookupUser");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", urlToFind);

            GetResponseAsync<FoundUser>(parameters, callback);
        }
    }
}
