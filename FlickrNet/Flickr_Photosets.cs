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
        /// Add a photo to a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to add the photo to.</param>
        /// <param name="photoId">The ID of the photo to add.</param>
        public void PhotosetsAddPhoto(string photosetId, string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.addPhoto");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("photo_id", photoId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Creates a blank photoset, with a title and a primary photo (minimum requirements).
        /// </summary>
        /// <param name="title">The title of the photoset.</param>
        /// <param name="primaryPhotoId">The ID of the photo which will be the primary photo for the photoset. This photo will also be added to the set.</param>
        /// <returns>The <see cref="Photoset"/> that is created.</returns>
        public Photoset PhotosetsCreate(string title, string primaryPhotoId)
        {
            return PhotosetsCreate(title, null, primaryPhotoId);
        }

        /// <summary>
        /// Creates a blank photoset, with a title, description and a primary photo.
        /// </summary>
        /// <param name="title">The title of the photoset.</param>
        /// <param name="description">THe description of the photoset.</param>
        /// <param name="primaryPhotoId">The ID of the photo which will be the primary photo for the photoset. This photo will also be added to the set.</param>
        /// <returns>The <see cref="Photoset"/> that is created.</returns>
        public Photoset PhotosetsCreate(string title, string description, string primaryPhotoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.create");
            parameters.Add("title", title);
            parameters.Add("primary_photo_id", primaryPhotoId);
            parameters.Add("description", description);

            return GetResponseNoCache<Photoset>(parameters);
        }

        /// <summary>
        /// Deletes the specified photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to delete.</param>
        public void PhotosetsDelete(string photosetId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.delete");
            parameters.Add("photoset_id", photosetId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Updates the title and description for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to update.</param>
        /// <param name="title">The new title for the photoset.</param>
        /// <param name="description">The new description for the photoset.</param>
        public void PhotosetsEditMeta(string photosetId, string title, string description)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.editMeta");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("title", title);
            parameters.Add("description", description);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Sets the photos for a photoset.
        /// </summary>
        /// <remarks>
        /// Will remove any previous photos from the photoset. 
        /// The order in thich the photoids are given is the order they will appear in the 
        /// photoset page.
        /// </remarks>
        /// <param name="photosetId">The ID of the photoset to update.</param>
        /// <param name="primaryPhotoId">The ID of the new primary photo for the photoset.</param>
        /// <param name="photoIds">An array of photo IDs.</param>
        public void PhotosetsEditPhotos(string photosetId, string primaryPhotoId, string[] photoIds)
        {
            PhotosetsEditPhotos(photosetId, primaryPhotoId, string.Join(",", photoIds));
        }


        /// <summary>
        /// Sets the photos for a photoset.
        /// </summary>
        /// <remarks>
        /// Will remove any previous photos from the photoset. 
        /// The order in thich the photoids are given is the order they will appear in the 
        /// photoset page.
        /// </remarks>
        /// <param name="photosetId">The ID of the photoset to update.</param>
        /// <param name="primaryPhotoId">The ID of the new primary photo for the photoset.</param>
        /// <param name="photoIds">An comma seperated list of photo IDs.</param>
        public void PhotosetsEditPhotos(string photosetId, string primaryPhotoId, string photoIds)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.editPhotos");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("primary_photo_id", primaryPhotoId);
            parameters.Add("photo_ids", photoIds);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Gets the context of the specified photo within the photoset.
        /// </summary>
        /// <param name="photoId">The photo id of the photo in the set.</param>
        /// <param name="photosetId">The id of the set.</param>
        /// <returns><see cref="Context"/> of the specified photo.</returns>
        public Context PhotosetsGetContext(string photoId, string photosetId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.getContext");
            parameters.Add("photo_id", photoId);
            parameters.Add("photoset_id", photosetId);

            return GetResponseCache<Context>(parameters);
        }

        /// <summary>
        /// Gets the information about a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return information for.</param>
        /// <returns>A <see cref="Photoset"/> instance.</returns>
        public Photoset PhotosetsGetInfo(string photosetId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.getInfo");
            parameters.Add("photoset_id", photosetId);

            return GetResponseCache<Photoset>(parameters);
        }

        /// <summary>
        /// Gets a list of the currently authenticated users photosets.
        /// </summary>
        /// <returns>A <see cref="PhotosetCollection"/> instance containing a collection of photosets.</returns>
        public PhotosetCollection PhotosetsGetList()
        {
            return PhotosetsGetList(null);
        }

        /// <summary>
        /// Gets a list of the specified users photosets.
        /// </summary>
        /// <param name="userId">The ID of the user to return the photosets of.</param>
        /// <returns>A <see cref="PhotosetCollection"/> instance containing a collection of photosets.</returns>
        public PhotosetCollection PhotosetsGetList(string userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.getList");
            if (userId != null) parameters.Add("user_id", userId);

            PhotosetCollection photosets = GetResponseCache<PhotosetCollection>(parameters);
            foreach(Photoset photoset in photosets)
            {
                photoset.OwnerId = userId;
            }
            return photosets;
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId)
        {
            return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, PrivacyFilter.None, 0, 0);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="page">The page to return, defaults to 1.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, int page, int perPage)
        {
            return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, PrivacyFilter.None, page, perPage);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="privacyFilter">The privacy filter to search on.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter)
        {
            return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, privacyFilter, 0, 0);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="privacyFilter">The privacy filter to search on.</param>
        /// <param name="page">The page to return, defaults to 1.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PrivacyFilter privacyFilter, int page, int perPage)
        {
            return PhotosetsGetPhotos(photosetId, PhotoSearchExtras.None, privacyFilter, page, perPage);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras)
        {
            return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None, 0, 0);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="page">The page to return, defaults to 1.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, int page, int perPage)
        {
            return PhotosetsGetPhotos(photosetId, extras, PrivacyFilter.None, page, perPage);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="privacyFilter">The privacy filter to search on.</param>
        /// <returns>A <see cref="PhotosetPhotoCollection"/> object containing the list of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter)
        {
            return PhotosetsGetPhotos(photosetId, extras, privacyFilter, 0, 0);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="privacyFilter">The privacy filter to search on.</param>
        /// <param name="page">The page to return, defaults to 1.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <returns>An array of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter, int page, int perPage)
        {
            return PhotosetsGetPhotos(photosetId, extras, privacyFilter, page, perPage, MediaType.None);
        }

        /// <summary>
        /// Gets a collection of photos for a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to return photos for.</param>
        /// <param name="extras">The extras to return for each photo.</param>
        /// <param name="privacyFilter">The privacy filter to search on.</param>
        /// <param name="page">The page to return, defaults to 1.</param>
        /// <param name="perPage">The number of photos to return per page.</param>
        /// <param name="media">Filter on the type of media.</param>
        /// <returns>An array of <see cref="Photo"/> instances.</returns>
        public PhotosetPhotoCollection PhotosetsGetPhotos(string photosetId, PhotoSearchExtras extras, PrivacyFilter privacyFilter, int page, int perPage, MediaType media)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.getPhotos");
            parameters.Add("photoset_id", photosetId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (privacyFilter != PrivacyFilter.None) parameters.Add("privacy_filter", privacyFilter.ToString("d"));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (media != MediaType.None) parameters.Add("media", (media == MediaType.All ? "all" : (media == MediaType.Photos ? "photos" : (media == MediaType.Videos ? "videos" : ""))));

            return GetResponseCache<PhotosetPhotoCollection>(parameters);
        }

        /// <summary>
        /// Changes the order of your photosets.
        /// </summary>
        /// <param name="photosetIds">An array of photoset IDs, 
        /// ordered with the set to show first, first in the list. 
        /// Any set IDs not given in the list will be set to appear at the end of the list, ordered by their IDs.</param>
        public void PhotosetsOrderSets(string[] photosetIds)
        {
            PhotosetsOrderSets(string.Join(",", photosetIds));
        }

        /// <summary>
        /// Changes the order of your photosets.
        /// </summary>
        /// <param name="photosetIds">A comma delimited list of photoset IDs, 
        /// ordered with the set to show first, first in the list. 
        /// Any set IDs not given in the list will be set to appear at the end of the list, ordered by their IDs.</param>
        public void PhotosetsOrderSets(string photosetIds)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.orderSets");
            parameters.Add("photoset_ids", photosetIds);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Removes a photo from a photoset.
        /// </summary>
        /// <remarks>
        /// An exception will be raised if the photo is not in the set.
        /// </remarks>
        /// <param name="photosetId">The ID of the photoset to remove the photo from.</param>
        /// <param name="photoId">The ID of the photo to remove.</param>
        public void PhotosetsRemovePhoto(string photosetId, string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.removePhoto");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("photo_id", photoId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Removes a photo from a photoset.
        /// </summary>
        /// <remarks>
        /// An exception will be raised if the photo is not in the set.
        /// </remarks>
        /// <param name="photosetId">The ID of the photoset to remove the photo from.</param>
        /// <param name="photoIds">The IDs of the photo to remove.</param>
        public void PhotosetsRemovePhotos(string photosetId, string[] photoIds)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.removePhotos");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("photo_ids", String.Join(",", photoIds));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Removes a photo from a photoset.
        /// </summary>
        /// <remarks>
        /// An exception will be raised if the photo is not in the set.
        /// </remarks>
        /// <param name="photosetId">The ID of the photoset to reorder the photo for.</param>
        /// <param name="photoIds">The IDs of the photo to reorder.</param>
        public void PhotosetsReorderPhotos(string photosetId, string[] photoIds)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.reorderPhotos");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("photo_ids", String.Join(",", photoIds));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Removes a photo from a photoset.
        /// </summary>
        /// <remarks>
        /// An exception will be raised if the photo is not in the set.
        /// </remarks>
        /// <param name="photosetId">The ID of the photoset to set the primary photo for.</param>
        /// <param name="photoId">The IDs of the photo to become the primary photo.</param>
        public void PhotosetsSetPrimaryPhoto(string photosetId, string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.setPrimaryPhoto");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("photo_id", photoId);

            GetResponseNoCache<NoResponse>(parameters);
        }


        /// <summary>
        /// Gets a list of comments for a photoset.
        /// </summary>
        /// <param name="photosetId">The id of the photoset to return the comments for.</param>
        /// <returns>An array of <see cref="PhotoComment"/> objects.</returns>
        public PhotosetCommentCollection PhotosetsCommentsGetList(string photosetId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.comments.getList");
            parameters.Add("photoset_id", photosetId);

            return GetResponseCache<PhotosetCommentCollection>(parameters);
        }

        /// <summary>
        /// Adds a new comment to a photoset.
        /// </summary>
        /// <param name="photosetId">The ID of the photoset to add the comment to.</param>
        /// <param name="commentText">The text of the comment. Can contain some HTML.</param>
        /// <returns>The new ID of the created comment.</returns>
        public string PhotosetsCommentsAddComment(string photosetId, string commentText)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.comments.addComment");
            parameters.Add("photoset_id", photosetId);
            parameters.Add("comment_text", commentText);

            UnknownResponse response = GetResponseNoCache<UnknownResponse>(parameters);

            System.Xml.XmlNode nav = response.GetXmlDocument().SelectSingleNode("*/@id");
            return nav == null ? null : nav.Value;
        }

        /// <summary>
        /// Deletes a comment from a photoset.
        /// </summary>
        /// <param name="commentId">The ID of the comment to delete.</param>
        public void PhotosetsCommentsDeleteComment(string commentId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.comments.deleteComment");
            parameters.Add("comment_id", commentId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Edits a comment.
        /// </summary>
        /// <param name="commentId">The ID of the comment to edit.</param>
        /// <param name="commentText">The new text for the comment.</param>
        public void PhotosetsCommentsEditComment(string commentId, string commentText)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photosets.comments.editComment");
            parameters.Add("comment_id", commentId);
            parameters.Add("comment_text", commentText);

            GetResponseNoCache<NoResponse>(parameters);
        }


    }
}
