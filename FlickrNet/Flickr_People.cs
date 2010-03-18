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
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.findByEmail");
            parameters.Add("api_key", _apiKey);
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
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.findByUsername");
            parameters.Add("api_key", _apiKey);
            parameters.Add("username", userName);

            return GetResponseCache<FoundUser>(parameters);
        }

        /// <summary>
        /// Gets the <see cref="Person"/> object for the given user id.
        /// </summary>
        /// <param name="userId">The user id to find.</param>
        /// <returns>The <see cref="Person"/> object containing the users details.</returns>
        public Person PeopleGetInfo(string userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.getInfo");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);

            return GetResponseCache<Person>(parameters);
        }

        /// <summary>
        /// Gets the upload status of the authenticated user.
        /// </summary>
        /// <returns>The <see cref="UserStatus"/> object containing the users details.</returns>
        public UserStatus PeopleGetUploadStatus()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.getUploadStatus");

            return GetResponseCache<UserStatus>(parameters);
        }

        /// <summary>
        /// Get a list of public groups for a user.
        /// </summary>
        /// <param name="userId">The user id to get groups for.</param>
        /// <returns>An array of <see cref="PublicGroupInfo"/> instances.</returns>
        public PublicGroupInfoCollection PeopleGetPublicGroups(string userId)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.getPublicGroups");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);

            return GetResponseCache<PublicGroupInfoCollection>(parameters);
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

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.getPublicPhotos");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);
            if (perPage > 0) parameters.Add("per_page", perPage.ToString());
            if (page > 0) parameters.Add("page", page.ToString());
            if (safetyLevel != SafetyLevel.None) parameters.Add("safety_level", (int)safetyLevel);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));

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
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.people.getPhotosOf");
            parameters.Add("user_id", userId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage);
            if (page > 0) parameters.Add("page", page);

            return GetResponseCache<PeoplePhotoCollection>(parameters);
        }

    }
}
