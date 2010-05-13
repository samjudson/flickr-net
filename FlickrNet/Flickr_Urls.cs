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
        public string UrlsGetGroup(string groupId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.getGroup");
            parameters.Add("group_id", groupId);

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode node = response.GetXmlDocument().SelectSingleNode("*/@url");
            return node == null ? null : node.Value;
        }

        /// <summary>
        /// Returns the url to a user's photos.
        /// </summary>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users photos.</returns>
        public string UrlsGetUserPhotos()
        {
            CheckRequiresAuthentication();

            return UrlsGetUserPhotos(null);
        }

        /// <summary>
        /// Returns the url to a user's photos.
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
        /// <returns>The URL of the users photos.</returns>
        public string UrlsGetUserPhotos(string userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.getUserPhotos");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@url");
            return nav == null ? null : nav.Value;
        }

        /// <summary>
        /// Returns the url to a user's profile.
        /// </summary>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users profile.</returns>
        public string UrlsGetUserProfile()
        {
            CheckRequiresAuthentication();

            return UrlsGetUserProfile(null);
        }

        /// <summary>
        /// Returns the url to a user's profile.
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the url for. If omitted, the calling user is assumed.</param>
        /// <returns>An instance of the <see cref="Uri"/> class containing the URL for the users profile.</returns>
        public string UrlsGetUserProfile(string userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.getUserProfile");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@url");
            return nav == null ? null : nav.Value;
        }

        /// <summary>
        /// Returns gallery info, by url.
        /// </summary>
        /// <param name="url">The gallery's URL.</param>
        /// <returns></returns>
        public Gallery UrlsLookupGallery(string url)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.lookupGallery");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", url);

            return GetResponseCache<Gallery>(parameters);
        }

        /// <summary>
        /// Returns a group NSID, given the url to a group's page or photo pool.
        /// </summary>
        /// <param name="urlToFind">The url to the group's page or photo pool.</param>
        /// <returns>The ID of the group at the specified URL on success, a null reference (Nothing in Visual Basic) if the group cannot be found.</returns>
        public string UrlsLookupGroup(string urlToFind)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.lookupGroup");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", urlToFind);

            UnknownResponse response = GetResponseCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@id");
            return nav == null ? null : nav.Value;
        }

        /// <summary>
        /// Returns a user NSID, given the url to a user's photos or profile.
        /// </summary>
        /// <param name="urlToFind">Thr url to the user's profile or photos page.</param>
        /// <returns>An instance of the <see cref="FoundUser"/> class containing the users ID and username.</returns>
        public FoundUser UrlsLookupUser(string urlToFind)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.urls.lookupUser");
            parameters.Add("api_key", _apiKey);
            parameters.Add("url", urlToFind);

            return GetResponseCache<FoundUser>(parameters);
        }
    }
}
