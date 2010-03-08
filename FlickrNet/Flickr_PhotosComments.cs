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
        /// <returns>An array of <see cref="Comment"/> objects.</returns>
        public Comment[] PhotosCommentsGetList(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.comments.getList");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return PhotoComments.GetComments(response.AllElements[0]);
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Adds a new comment to a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo to add the comment to.</param>
        /// <param name="commentText">The text of the comment. Can contain some HTML.</param>
        /// <returns>The new ID of the created comment.</returns>
        public string PhotosCommentsAddComment(string photoId, string commentText)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.comments.addComment");
            parameters.Add("photo_id", photoId);
            parameters.Add("comment_text", commentText);

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                XmlNode node = response.AllElements[0];
                if (node.Attributes.GetNamedItem("id") != null)
                    return node.Attributes.GetNamedItem("id").Value;
                else
                    throw new ResponseXmlException("Comment ID not found in response Xml.");
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Deletes a comment from a photo.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        public void PhotosCommentsDeleteComment(string commentId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.comments.deleteComment");
            parameters.Add("comment_id", commentId);

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Edits a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to edit.</param>
        /// <param name="commentText">The new text for the comment.</param>
        public void PhotosCommentsEditComment(string commentId, string commentText)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.comments.editComment");
            parameters.Add("comment_id", commentId);
            parameters.Add("comment_text", commentText);

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <returns></returns>
        public Photos PhotosCommentsGetRecentForContacts()
        {
            return PhotosCommentsGetRecentForContacts(DateTime.MinValue, null, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public Photos PhotosCommentsGetRecentForContacts(int page, int perPage)
        {
            return PhotosCommentsGetRecentForContacts(DateTime.MinValue, null, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <param name="dateLastComment">Limits the resultset to photos that have been commented on since this date. The default, and maximum, offset is (1) hour.</param>
        /// <param name="extras"></param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public Photos PhotosCommentsGetRecentForContacts(DateTime dateLastComment, PhotoSearchExtras extras, int page, int perPage)
        {
            return PhotosCommentsGetRecentForContacts(dateLastComment, null, extras, page, perPage);
        }

        /// <summary>
        /// Return the list of photos belonging to your contacts that have been commented on recently.
        /// </summary>
        /// <param name="dateLastComment">Limits the resultset to photos that have been commented on since this date. The default, and maximum, offset is (1) hour.</param>
        /// <param name="contactsFilter">A list of contact NSIDs to limit the scope of the query to.</param>
        /// <param name="extras"></param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public Photos PhotosCommentsGetRecentForContacts(DateTime dateLastComment, string[] contactsFilter, PhotoSearchExtras extras, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.comments.getRecentForContacts");
            if (dateLastComment != DateTime.MinValue) parameters.Add("date_lastcomment", Utils.DateToUnixTimestamp(dateLastComment));
            if (contactsFilter != null && contactsFilter.Length > 0) parameters.Add("contacts_filter", String.Join(",", contactsFilter));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));
            if (page > 0) parameters.Add("page", page);
            if (perPage > 0) parameters.Add("per_page", perPage);

            return GetResponseCache<Photos>(parameters);
        }
    }
}
