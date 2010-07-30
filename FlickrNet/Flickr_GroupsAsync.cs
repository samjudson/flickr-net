using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Browse the group category tree, finding groups and sub-categories.
        /// </summary>
        /// <remarks>
        /// Flickr no longer supports this method and it returns no useful information.
        /// </remarks>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsBrowseAsync(Action<FlickrResult<GroupCategory>> callback)
        {
            GroupsBrowseAsync(null, callback);
        }

        /// <summary>
        /// Browse the group category tree, finding groups and sub-categories.
        /// </summary>
        /// <param name="catId">The category id to fetch a list of groups and sub-categories for. If not specified, it defaults to zero, the root of the category tree.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsBrowseAsync(string catId, Action<FlickrResult<GroupCategory>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.browse");
            if (!String.IsNullOrEmpty(catId)) parameters.Add("cat_id", catId);

            GetResponseAsync<GroupCategory>(parameters, callback);
        }

        /// <summary>
        /// Search the list of groups on Flickr for the text.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsSearchAsync(string text, Action<FlickrResult<GroupSearchResultCollection>> callback)
        {
            GroupsSearchAsync(text, 0, 0, callback);
        }

        /// <summary>
        /// Search the list of groups on Flickr for the text.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <param name="page">The page of the results to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsSearchAsync(string text, int page, Action<FlickrResult<GroupSearchResultCollection>> callback)
        {
            GroupsSearchAsync(text, page, 0, callback);
        }

        /// <summary>
        /// Search the list of groups on Flickr for the text.
        /// </summary>
        /// <param name="text">The text to search for.</param>
        /// <param name="page">The page of the results to return.</param>
        /// <param name="perPage">The number of groups to list per page.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsSearchAsync(string text, int page, int perPage, Action<FlickrResult<GroupSearchResultCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.search");
            parameters.Add("api_key", _apiKey);
            parameters.Add("text", text);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<GroupSearchResultCollection>(parameters, callback);
        }

        /// <summary>
        /// Returns a <see cref="GroupFullInfo"/> object containing details about a group.
        /// </summary>
        /// <param name="groupId">The id of the group to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsGetInfoAsync(string groupId, Action<FlickrResult<GroupFullInfo>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.getInfo");
            parameters.Add("api_key", _apiKey);
            parameters.Add("group_id", groupId);
            GetResponseAsync<GroupFullInfo>(parameters, callback);
        }

        /// <summary>
        /// Get a list of group members.
        /// </summary>
        /// <param name="groupId">The group id to get the list of members for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsMembersGetListAsync(string groupId, Action<FlickrResult<MemberCollection>> callback)
        {
            GroupsMembersGetListAsync(groupId, 0, 0, MemberTypes.None, callback);
        }

        /// <summary>
        /// Get a list of the members of a group. 
        /// </summary>
        /// <remarks>
        /// The call must be signed on behalf of a Flickr member, and the ability to see the group membership will be determined by the Flickr member's group privileges.
        /// </remarks>
        /// <param name="groupId">Return a list of members for this group. The group must be viewable by the Flickr member on whose behalf the API call is made.</param>
        /// <param name="page">The page of the results to return (default is 1).</param>
        /// <param name="perPage">The number of members to return per page (default is 100, max is 500).</param>
        /// <param name="memberTypes">The types of members to be returned. Can be more than one.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsMembersGetListAsync(string groupId, int page, int perPage, MemberTypes memberTypes, Action<FlickrResult<MemberCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.members.getList");
            parameters.Add("api_key", _apiKey);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (memberTypes != MemberTypes.None) parameters.Add("membertypes", UtilityMethods.MemberTypeToString(memberTypes));
            parameters.Add("group_id", groupId);

            GetResponseAsync<MemberCollection>(parameters, callback);
        }

        /// <summary>
        /// Adds a photo to a pool you have permission to add photos to.
        /// </summary>
        /// <param name="photoId">The id of one of your photos to be added.</param>
        /// <param name="groupId">The id of a group you are a member of.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsAddAsync(string photoId, string groupId, Action<FlickrResult<NoResponse>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.pools.add");
            parameters.Add("photo_id", photoId);
            parameters.Add("group_id", groupId);
            GetResponseAsync<NoResponse>(parameters, callback);
        }

        /// <summary>
        /// Gets the context for a photo from within a group. This provides the
        /// id and thumbnail url for the next and previous photos in the group.
        /// </summary>
        /// <param name="photoId">The Photo ID for the photo you want the context for.</param>
        /// <param name="groupId">The group ID for the group you want the context to be relevant to.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetContextAsync(string photoId, string groupId, Action<FlickrResult<Context>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.pools.getContext");
            parameters.Add("photo_id", photoId);
            parameters.Add("group_id", groupId);
            GetResponseAsync<Context>(parameters, callback);
        }

        /// <summary>
        /// Remove a picture from a group.
        /// </summary>
        /// <param name="photoId">The id of one of your pictures you wish to remove.</param>
        /// <param name="groupId">The id of the group to remove the picture from.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsRemoveAsync(string photoId, string groupId, Action<FlickrResult<NoResponse>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.pools.remove");
            parameters.Add("photo_id", photoId);
            parameters.Add("group_id", groupId);
            GetResponseAsync<NoResponse>(parameters, callback);
        }

        /// <summary>
        /// Returns a list of groups to which you can add photos.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetGroupsAsync(Action<FlickrResult<MemberGroupInfoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.pools.getGroups");

            GetResponseAsync<MemberGroupInfoCollection>(parameters, callback);
        }

        /// <summary>
        /// Gets a list of photos for a given group.
        /// </summary>
        /// <param name="groupId">The group ID for the group.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetPhotosAsync(string groupId, Action<FlickrResult<PhotoCollection>> callback)
        {
            GroupsPoolsGetPhotosAsync(groupId, null, null, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets a list of photos for a given group.
        /// </summary>
        /// <param name="groupId">The group ID for the group.</param>
        /// <param name="tags">Space seperated list of tags that photos returned must have.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetPhotosAsync(string groupId, string tags, Action<FlickrResult<PhotoCollection>> callback)
        {
            GroupsPoolsGetPhotosAsync(groupId, tags, null, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets a list of photos for a given group.
        /// </summary>
        /// <param name="groupId">The group ID for the group.</param>
        /// <param name="perPage">The number of photos per page.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetPhotosAsync(string groupId, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            GroupsPoolsGetPhotosAsync(groupId, null, null, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Gets a list of photos for a given group.
        /// </summary>
        /// <param name="groupId">The group ID for the group.</param>
        /// <param name="tags">Space seperated list of tags that photos returned must have.</param>
        /// <param name="perPage">The number of photos per page.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetPhotosAsync(string groupId, string tags, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            GroupsPoolsGetPhotosAsync(groupId, tags, null, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Gets a list of photos for a given group.
        /// </summary>
        /// <param name="groupId">The group ID for the group.</param>
        /// <param name="tags">Space seperated list of tags that photos returned must have.
        /// Currently only supports 1 tag at a time.</param>
        /// <param name="userId">The group member to return photos for.</param>
        /// <param name="extras">The <see cref="PhotoSearchExtras"/> specifying which extras to return. All other overloads default to returning all extras.</param>
        /// <param name="perPage">The number of photos per page.</param>
        /// <param name="page">The page to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void GroupsPoolsGetPhotosAsync(string groupId, string tags, string userId, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.groups.pools.getPhotos");
            parameters.Add("group_id", groupId);
            if (tags != null && tags.Length > 0) parameters.Add("tags", tags);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            GetResponseAsync<PhotoCollection>(parameters, callback);
        }
    }
}
