using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets a list of comments for a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to return the comments for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsGetListAsync(string photoId, Action<FlickrResult<PhotoCommentCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.comments.getList");
            parameters.Add("photo_id", photoId);

            GetResponseAsync<PhotoCommentCollection>(parameters, callback);
        }

        /// <summary>
        /// Adds a new comment to a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo to add the comment to.</param>
        /// <param name="commentText">The text of the comment. Can contain some HTML.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsAddCommentAsync(string photoId, string commentText, Action<FlickrResult<string>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.comments.addComment");
            parameters.Add("photo_id", photoId);
            parameters.Add("comment_text", commentText);

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
                        result.Result = r.Result.GetAttributeValue("*", "id");
                    }
                    callback(result);
                });
        }

        /// <summary>
        /// Deletes a comment from a photo.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsDeleteCommentAsync(string commentId, Action<FlickrResult<NoResponse>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.comments.deleteComment");
            parameters.Add("comment_id", commentId);

            GetResponseAsync<NoResponse>(parameters, callback);
        }

        /// <summary>
        /// Edits a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to edit.</param>
        /// <param name="commentText">The new text for the comment.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsEditCommentAsync(string commentId, string commentText, Action<FlickrResult<NoResponse>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.comments.editComment");
            parameters.Add("comment_id", commentId);
            parameters.Add("comment_text", commentText);

            GetResponseAsync<NoResponse>(parameters, callback);

        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <returns></returns>
        public void PhotosCommentsGetRecentForContactsAsync(Action<FlickrResult<PhotoCollection>> callback)
        {
            PhotosCommentsGetRecentForContactsAsync(DateTime.MinValue, null, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsGetRecentForContactsAsync(int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PhotosCommentsGetRecentForContactsAsync(DateTime.MinValue, null, PhotoSearchExtras.None, page, perPage, callback);
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <param name="dateLastComment">Limits the resultset to photos that have been commented on since this date. The default, and maximum, offset is (1) hour.</param>
        /// <param name="extras"></param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsGetRecentForContactsAsync(DateTime dateLastComment, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            PhotosCommentsGetRecentForContactsAsync(dateLastComment, null, extras, page, perPage, callback);
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <param name="dateLastComment">Limits the resultset to photos that have been commented on since this date. The default, and maximum, offset is (1) hour.</param>
        /// <param name="contactsFilter">A list of contact NSIDs to limit the scope of the query to.</param>
        /// <param name="extras"></param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosCommentsGetRecentForContactsAsync(DateTime dateLastComment, string[] contactsFilter, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.comments.getRecentForContacts");
            if (dateLastComment != DateTime.MinValue) parameters.Add("date_lastcomment", UtilityMethods.DateToUnixTimestamp(dateLastComment));
            if (contactsFilter != null && contactsFilter.Length > 0) parameters.Add("contacts_filter", String.Join(",", contactsFilter));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PhotoCollection>(parameters, callback);
        }
    }
}
