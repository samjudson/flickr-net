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
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL of the group page.</returns>
        public Uri UrlsGetGroup(string groupId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.urls.getGroup");
            parameters.Add("api_key", _apiKey);
            parameters.Add("group_id", groupId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                if (response.AllElements[0] != null && response.AllElements[0].Attributes["url"] != null)
                    return new Uri(response.AllElements[0].Attributes["url"].Value);
                else
                    return null;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns the url to a user's photos.
        /// </summary>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users photos.</returns>
        public Uri UrlsGetUserPhotos()
        {
            return UrlsGetUserPhotos(null);
        }

        /// <summary>
        /// Returns the url to a user's photos.
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
        /// <returns>The URL of the users photos.</returns>
        public Uri UrlsGetUserPhotos(string userId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.urls.getUserPhotos");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                if (response.AllElements[0] != null && response.AllElements[0].Attributes["url"] != null)
                    return new Uri(response.AllElements[0].Attributes["url"].Value);
                else
                    return null;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns the url to a user's profile.
        /// </summary>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users profile.</returns>
        public Uri UrlsGetUserProfile()
        {
            return UrlsGetUserProfile(null);
        }

        /// <summary>
        /// Returns the url to a user's profile.
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users profile.</returns>
        public Uri UrlsGetUserProfile(string userId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.urls.getUserProfile");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                if (response.AllElements[0] != null && response.AllElements[0].Attributes["url"] != null)
                    return new Uri(response.AllElements[0].Attributes["url"].Value);
                else
                    return null;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns a group NSID, given the url to a group's page or photo pool.
        /// </summary>
        /// <param name="urlToFind">The url to the group's page or photo pool.</param>
        /// <returns>The ID of the group at the specified URL on success, a null reference (Nothing in Visual Basic) if the group cannot be found.</returns>
        public string UrlsLookupGroup(string urlToFind)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.urls.lookupGroup");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", urlToFind);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                if (response.AllElements[0] != null && response.AllElements[0].Attributes["id"] != null)
                {
                    return response.AllElements[0].Attributes["id"].Value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (response.Error.Code == 1)
                    return null;
                else
                    throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns a user NSID, given the url to a user's photos or profile.
        /// </summary>
        /// <param name="urlToFind">Thr url to the user's profile or photos page.</param>
        /// <returns>An instance of the <see cref="FoundUser"/> class containing the users ID and username.</returns>
        public FoundUser UrlsLookupUser(string urlToFind)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.urls.lookupUser");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", urlToFind);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return new FoundUser(response.AllElements[0]);
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }
    }
}
