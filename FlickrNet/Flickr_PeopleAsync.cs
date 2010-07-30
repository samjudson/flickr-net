using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Used to fid a flickr users details by specifying their email address.
        /// </summary>
        /// <param name="emailAddress">The email address to search on.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        /// <exception cref="FlickrApiException">A FlickrApiException is raised if the email address is not found.</exception>
        public void PeopleFindByEmailAsync(string emailAddress, Action<FlickrResult<FoundUser>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.findByEmail");
            parameters.Add("api_key", _apiKey);
            parameters.Add("find_email", emailAddress);

            GetResponseAsync<FoundUser>(parameters, callback);
        }

        /// <summary>
        /// Returns a <see cref="FoundUser"/> object matching the screen name.
        /// </summary>
        /// <param name="userName">The screen name or username of the user.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        /// <exception cref="FlickrApiException">A FlickrApiException is raised if the email address is not found.</exception>
        public void PeopleFindByUserNameAsync(string userName, Action<FlickrResult<FoundUser>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.findByUsername");
            parameters.Add("api_key", _apiKey);
            parameters.Add("username", userName);

            GetResponseAsync<FoundUser>(parameters, callback);
        }

        /// <summary>
        /// Gets the <see cref="Person"/> object for the given user id.
        /// </summary>
        /// <param name="userId">The user id to find.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetInfoAsync(string userId, Action<FlickrResult<Person>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getInfo");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);

            GetResponseAsync<Person>(parameters, callback);
        }

        /// <summary>
        /// Gets the upload status of the authenticated user.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetUploadStatusAsync(Action<FlickrResult<UserStatus>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getUploadStatus");

            GetResponseAsync<UserStatus>(parameters, callback);
        }

        /// <summary>
        /// Get a list of public groups for a user.
        /// </summary>
        /// <param name="userId">The user id to get groups for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPublicGroupsAsync(string userId, Action<FlickrResult<PublicGroupInfoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getPublicGroups");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);

            GetResponseAsync<PublicGroupInfoCollection>(parameters, callback);
        }

        /// <summary>
        /// Gets a users public photos. Excludes private photos.
        /// </summary>
        /// <param name="userId">The user id of the user.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPublicPhotosAsync(string userId, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPublicPhotosAsync(userId, 0, 0, SafetyLevel.None, PhotoSearchExtras.None, callback);
        }

        /// <summary>
        /// Gets a users public photos. Excludes private photos.
        /// </summary>
        /// <param name="userId">The user id of the user.</param>
        /// <param name="page">The page to return. Defaults to page 1.</param>
        /// <param name="perPage">The number of photos to return per page. Default is 100.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPublicPhotosAsync(string userId, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPublicPhotosAsync(userId, page, perPage, SafetyLevel.None, PhotoSearchExtras.None, callback);
        }

        /// <summary>
        /// Gets a users public photos. Excludes private photos.
        /// </summary>
        /// <param name="userId">The user id of the user.</param>
        /// <param name="page">The page to return. Defaults to page 1.</param>
        /// <param name="perPage">The number of photos to return per page. Default is 100.</param>
        /// <param name="extras">Which (if any) extra information to return. The default is none.</param>
        /// <param name="safetyLevel">The safety level of the returned photos. 
        /// Unauthenticated calls can only return Safe photos.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPublicPhotosAsync(string userId, int page, int perPage, SafetyLevel safetyLevel, PhotoSearchExtras extras, Action<FlickrResult<PhotoCollection>> callback)
        {
            if (!IsAuthenticated && safetyLevel > SafetyLevel.Safe)
                throw new ArgumentException("Safety level may only be 'Safe' for unauthenticated calls", "safetyLevel");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getPublicPhotos");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (safetyLevel != SafetyLevel.None) parameters.Add("safety_level", safetyLevel.ToString("D"));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            GetResponseAsync<PhotoCollection>(parameters, callback);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(PhotoSearchExtras extras, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0, callback);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage, callback);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(string userId, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(string userId, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(string userId, PhotoSearchExtras extras, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0, callback);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(string userId, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PeopleGetPhotosAsync(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage, callback);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="safeSearch">Safe search setting</param>
        /// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned.</param>
        /// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned.</param>
        /// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned. </param>
        /// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned. </param>
        /// <param name="contentType">Content Type setting</param>
        /// <param name="privacyFilter">Return photos only matching a certain privacy level.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosAsync(string userId, SafetyLevel safeSearch, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate, ContentTypeSearch contentType, PrivacyFilter privacyFilter, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.people.getPhotos");
            parameters.Add("user_id", userId ?? "me");
            if (safeSearch != SafetyLevel.None) parameters.Add("safe_search", safeSearch.ToString("d"));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(maxUploadDate));
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", UtilityMethods.DateToMySql(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", UtilityMethods.DateToMySql(maxTakenDate));

            if (contentType != ContentTypeSearch.None) parameters.Add("content_type", contentType.ToString("d"));
            if (privacyFilter != PrivacyFilter.None) parameters.Add("privacy_filter", privacyFilter.ToString("d"));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PhotoCollection>(parameters, callback);
        }

        /// <summary>
        /// Gets the photos containing the authenticated user. Requires that the AuthToken has been set.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosOfAsync(Action<FlickrResult<PeoplePhotoCollection>> callback)
        {
            CheckRequiresAuthentication();

            PeopleGetPhotosOfAsync("me", PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosOfAsync(string userId, Action<FlickrResult<PeoplePhotoCollection>> callback)
        {
            PeopleGetPhotosOfAsync(userId, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="extras">A list of extras to return for each photo.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosOfAsync(string userId, PhotoSearchExtras extras, Action<FlickrResult<PeoplePhotoCollection>> callback)
        {
            PeopleGetPhotosOfAsync(userId, extras, 0, 0, callback);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The page of photos to return. Default is 1.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosOfAsync(string userId, int page, int perPage, Action<FlickrResult<PeoplePhotoCollection>> callback)
        {
            PeopleGetPhotosOfAsync(userId, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="extras">A list of extras to return for each photo.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The page of photos to return. Default is 1.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PeopleGetPhotosOfAsync(string userId, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PeoplePhotoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getPhotosOf");
            parameters.Add("user_id", userId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PeoplePhotoCollection>(parameters, callback);
        }

    }
}
