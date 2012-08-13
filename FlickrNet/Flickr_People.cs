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
        /// <returns>The <see cref="FoundUser"/> object containing the matching details.</returns>
        /// <exception cref="FlickrApiException">A FlickrApiException is raised if the email address is not found.</exception>
        public FoundUser PeopleFindByEmail(string emailAddress)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.findByEmail");
            parameters.Add("api_key", apiKey);
            parameters.Add("find_email", emailAddress);

            return GetResponseCache<FoundUser>(parameters);
        }

        /// <summary>
        /// Returns a <see cref="FoundUser"/> object matching the screen name.
        /// </summary>
        /// <param name="userName">The screen name or username of the user.</param>
        /// <returns>A <see cref="FoundUser"/> class containing the userId and username of the user.</returns>
        /// <exception cref="FlickrApiException">A FlickrApiException is raised if the email address is not found.</exception>
        public FoundUser PeopleFindByUserName(string userName)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.findByUsername");
            parameters.Add("api_key", apiKey);
            parameters.Add("username", userName);

            return GetResponseCache<FoundUser>(parameters);
        }

        /// <summary>
        /// Gets a list of groups the user is a member of.
        /// </summary>
        /// <param name="userId">The user whose groups you wish to return.</param>
        /// <returns></returns>
        public GroupInfoCollection PeopleGetGroups(string userId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getGroups");
            parameters.Add("user_id", userId);

            return GetResponseCache<GroupInfoCollection>(parameters);
        }

        /// <summary>
        /// Gets the <see cref="Person"/> object for the given user id.
        /// </summary>
        /// <param name="userId">The user id to find.</param>
        /// <returns>The <see cref="Person"/> object containing the users details.</returns>
        public Person PeopleGetInfo(string userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getInfo");
            parameters.Add("user_id", userId);

            return GetResponseCache<Person>(parameters);
        }

        /// <summary>
        /// Returns the limits for a person. See <see cref="PersonLimits"/> for more details.
        /// </summary>
        /// <returns></returns>
        public PersonLimits PeopleGetLimits()
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getLimits");

            return GetResponseCache<PersonLimits>(parameters);
        }

        /// <summary>
        /// Gets the upload status of the authenticated user.
        /// </summary>
        /// <returns>The <see cref="UserStatus"/> object containing the users details.</returns>
        public UserStatus PeopleGetUploadStatus()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getUploadStatus");

            return GetResponseCache<UserStatus>(parameters);
        }

        /// <summary>
        /// Get a list of public groups for a user.
        /// </summary>
        /// <param name="userId">The user id to get groups for.</param>
        /// <returns>An array of <see cref="GroupInfo"/> instances.</returns>
        public GroupInfoCollection PeopleGetPublicGroups(string userId)
        {
            return PeopleGetPublicGroups(userId, null);
        }
        
        /// <summary>
        /// Get a list of public groups for a user.
        /// </summary>
        /// <param name="userId">The user id to get groups for.</param>
        /// <param name="includeInvitationOnly">Wheither to include public but invitation only groups in the results.</param>
        /// <returns>An array of <see cref="GroupInfo"/> instances.</returns>
        public GroupInfoCollection PeopleGetPublicGroups(string userId, bool? includeInvitationOnly)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getPublicGroups");
            parameters.Add("api_key", apiKey);
            parameters.Add("user_id", userId);
            if (includeInvitationOnly.HasValue) parameters.Add("invitation_only", includeInvitationOnly.Value ? "1" : "0");


            return GetResponseCache<GroupInfoCollection>(parameters);
        }

        /// <summary>
        /// Gets a users public photos. Excludes private photos.
        /// </summary>
        /// <param name="userId">The user id of the user.</param>
        /// <returns>The collection of photos contained within a <see cref="Photo"/> object.</returns>
        public PhotoCollection PeopleGetPublicPhotos(string userId)
        {
            return PeopleGetPublicPhotos(userId, 0, 0, SafetyLevel.None, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets a users public photos. Excludes private photos.
        /// </summary>
        /// <param name="userId">The user id of the user.</param>
        /// <param name="page">The page to return. Defaults to page 1.</param>
        /// <param name="perPage">The number of photos to return per page. Default is 100.</param>
        /// <returns>The collection of photos contained within a <see cref="Photo"/> object.</returns>
        public PhotoCollection PeopleGetPublicPhotos(string userId, int page, int perPage)
        {
            return PeopleGetPublicPhotos(userId, page, perPage, SafetyLevel.None, PhotoSearchExtras.None);
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
        /// <returns>The collection of photos contained within a <see cref="Photo"/> object.</returns>
        public PhotoCollection PeopleGetPublicPhotos(string userId, int page, int perPage, SafetyLevel safetyLevel, PhotoSearchExtras extras)
        {
            if (!IsAuthenticated && safetyLevel > SafetyLevel.Safe)
                throw new ArgumentException("Safety level may only be 'Safe' for unauthenticated calls", "safetyLevel");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getPublicPhotos");
            parameters.Add("api_key", apiKey);
            parameters.Add("user_id", userId);
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (safetyLevel != SafetyLevel.None) parameters.Add("safety_level", safetyLevel.ToString("D"));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos()
        {
            return PeopleGetPhotos(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(int page, int perPage)
        {
            return PeopleGetPhotos(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(PhotoSearchExtras extras)
        {
            return PeopleGetPhotos(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0);
        }

        /// <summary>
        /// Return photos from the calling user's photostream. This method must be authenticated;
        /// </summary>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(PhotoSearchExtras extras, int page, int perPage)
        {
            return PeopleGetPhotos(null, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(string userId)
        {
            return PeopleGetPhotos(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(string userId, int page, int perPage)
        {
            return PeopleGetPhotos(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(string userId, PhotoSearchExtras extras)
        {
            return PeopleGetPhotos(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, 0, 0);
        }

        /// <summary>
        /// Return photos from the given user's photostream. Only photos visible to the calling user will be returned. This method must be authenticated;
        /// </summary>
        /// <param name="userId">The NSID of the user who's photos to return. A value of "me" will return the calling user's photos.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(string userId, PhotoSearchExtras extras, int page, int perPage)
        {
            return PeopleGetPhotos(userId, SafetyLevel.None, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, ContentTypeSearch.None, PrivacyFilter.None, extras, page, perPage);
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
        /// <returns></returns>
        public PhotoCollection PeopleGetPhotos(string userId, SafetyLevel safeSearch, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate, ContentTypeSearch contentType, PrivacyFilter privacyFilter, PhotoSearchExtras extras, int page, int perPage)
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

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Gets the photos containing the authenticated user. Requires that the AuthToken has been set.
        /// </summary>
        /// <returns>A list of photos in the <see cref="PeoplePhotoCollection"/> class.</returns>
        public PeoplePhotoCollection PeopleGetPhotosOf()
        {
            CheckRequiresAuthentication();

            return PeopleGetPhotosOf("me", PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <returns>A list of photos in the <see cref="PeoplePhotoCollection"/> class.</returns>
        public PeoplePhotoCollection PeopleGetPhotosOf(string userId)
        {
            return PeopleGetPhotosOf(userId, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="extras">A list of extras to return for each photo.</param>
        /// <returns>A list of photos in the <see cref="PeoplePhotoCollection"/> class.</returns>
        public PeoplePhotoCollection PeopleGetPhotosOf(string userId, PhotoSearchExtras extras)
        {
            return PeopleGetPhotosOf(userId, extras, 0, 0);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The page of photos to return. Default is 1.</param>
        /// <returns>A list of photos in the <see cref="PeoplePhotoCollection"/> class.</returns>
        public PeoplePhotoCollection PeopleGetPhotosOf(string userId, int page, int perPage)
        {
            return PeopleGetPhotosOf(userId, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Gets the photos containing the specified user.
        /// </summary>
        /// <param name="userId">The user ID to get photos of.</param>
        /// <param name="extras">A list of extras to return for each photo.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="page">The page of photos to return. Default is 1.</param>
        /// <returns>A list of photos in the <see cref="PeoplePhotoCollection"/> class.</returns>
        public PeoplePhotoCollection PeopleGetPhotosOf(string userId, PhotoSearchExtras extras, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.people.getPhotosOf");
            parameters.Add("user_id", userId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PeoplePhotoCollection>(parameters);
        }

    }
}
