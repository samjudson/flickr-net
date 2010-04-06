using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Add a photo to a gallery.
        /// </summary>
        /// <param name="galleryId">he ID of the gallery to add a photo to. Note: this is the compound ID returned in methods like <see cref="Flickr.GalleriesGetList(string, int, int)"/>, and <see cref="Flickr.GalleriesGetListForPhoto(string, int, int)"/>.</param>
        /// <param name="photoId">The photo ID to add to the gallery</param>
        public void GalleriesAddPhoto(string galleryId, string photoId)
        {
            GalleriesAddPhoto(galleryId, photoId, null);
        }

        /// <summary>
        /// Add a photo to a gallery.
        /// </summary>
        /// <param name="galleryId">he ID of the gallery to add a photo to. Note: this is the compound ID returned in methods like <see cref="Flickr.GalleriesGetList(string, int, int)"/>, and <see cref="Flickr.GalleriesGetListForPhoto(string, int, int)"/>.</param>
        /// <param name="photoId">The photo ID to add to the gallery</param>
        /// <param name="comment">A short comment or story to accompany the photo.</param>
        public void GalleriesAddPhoto(string galleryId, string photoId, string comment)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.addPhoto");
            parameters.Add("gallery_id", galleryId);
            parameters.Add("photo_id", photoId);
            if (!String.IsNullOrEmpty(comment)) parameters.Add("comment", comment);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Create a new gallery for the calling user.
        /// </summary>
        /// <param name="title">The name of the gallery.</param>
        /// <param name="description">A short description for the gallery.</param>
        public void GalleriesCreate(string title, string description)
        {
            GalleriesCreate(title, description, null);
        }

        /// <summary>
        /// Create a new gallery for the calling user.
        /// </summary>
        /// <param name="title">The name of the gallery.</param>
        /// <param name="description">A short description for the gallery.</param>
        /// <param name="primaryPhotoId">The first photo to add to your gallery.</param>
        public void GalleriesCreate(string title, string description, string primaryPhotoId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.create");
            parameters.Add("title", title);
            parameters.Add("description", description);
            if (!String.IsNullOrEmpty(primaryPhotoId)) parameters.Add("primary_photo_id", primaryPhotoId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Modify the meta-data for a gallery.
        /// </summary>
        /// <param name="galleryId">The gallery ID to update.</param>
        /// <param name="title">The new title for the gallery.</param>
        public void GalleriesEditMeta(string galleryId, string title)
        {
            GalleriesEditMeta(galleryId, title, null);
        }

        /// <summary>
        /// Modify the meta-data for a gallery.
        /// </summary>
        /// <param name="galleryId">The gallery ID to update.</param>
        /// <param name="title">The new title for the gallery.</param>
        /// <param name="description">The new description for the gallery.</param>
        public void GalleriesEditMeta(string galleryId, string title, string description)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.editMeta");
            parameters.Add("gallery_id", galleryId);
            parameters.Add("title", title);
            if (!String.IsNullOrEmpty(description)) parameters.Add("description", description);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Edit the comment for a gallery photo.
        /// </summary>
        /// <param name="galleryId">The ID of the gallery to add a photo to. Note: this is the compound ID returned in methods like <see cref="Flickr.GalleriesGetList(string, int, int)"/>, and <see cref="Flickr.GalleriesGetListForPhoto(string, int, int)"/>.</param>
        /// <param name="photoId">The photo ID to add to the gallery.</param>
        /// <param name="comment">The updated comment the photo.</param>
        public void GalleriesEditPhoto(string galleryId, string photoId, string comment)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.editPhoto");
            parameters.Add("gallery_id", galleryId);
            parameters.Add("photo_id", photoId);
            parameters.Add("comment", comment);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Modify the photos in a gallery. Use this method to add, remove and re-order photos.
        /// </summary>
        /// <param name="galleryId">The id of the gallery to modify. The gallery must belong to the calling user.</param>
        /// <param name="primaryPhotoId">The id of the photo to use as the 'primary' photo for the gallery. This id must also be passed along in photo_ids list argument.</param>
        /// <param name="photoIds">An enumeration of photo ids to include in the gallery. They will appear in the set in the order sent. This list must contain the primary photo id. This list of photos replaces the existing list.</param>
        public void GalleriesEditPhotos(string galleryId, string primaryPhotoId, IEnumerable<string> photoIds)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.editPhotos");
            parameters.Add("gallery_id", galleryId);
            parameters.Add("primary_photo_id", primaryPhotoId);
            List<string> ids = new List<string>(photoIds);
            parameters.Add("photo_ids", String.Join(",", ids.ToArray()));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Get the information about a gallery.
        /// </summary>
        /// <param name="galleryId">The gallery ID you are requesting information for.</param>
        /// <returns></returns>
        public Gallery GalleriesGetInfo(string galleryId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.getInfo");
            parameters.Add("gallery_id", galleryId);

            return GetResponseCache<Gallery>(parameters);
        }

        /// <summary>
        /// Gets a list of galleries for the calling user. Must be authenticated.
        /// </summary>
        /// <returns></returns>
        public GalleryCollection GalleriesGetList()
        {
            CheckRequiresAuthentication();

            return GalleriesGetList(null, 0, 0);
        }

        /// <summary>
        /// Gets a list of galleries for the calling user. Must be authenticated.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public GalleryCollection GalleriesGetList(int page, int perPage)
        {
            CheckRequiresAuthentication();

            return GalleriesGetList(null, page, perPage);
        }

        /// <summary>
        /// Gets a list of galleries for the specified user.
        /// </summary>
        /// <param name="userId">The user to return the galleries for.</param>
        /// <returns></returns>
        public GalleryCollection GalleriesGetList(string userId)
        {
            return GalleriesGetList(userId, 0, 0);
        }

        /// <summary>
        /// Gets a list of galleries for the specified user.
        /// </summary>
        /// <param name="userId">The user to return the galleries for.</param>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public GalleryCollection GalleriesGetList(string userId, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.getList");
            if (!String.IsNullOrEmpty(userId)) parameters.Add("user_id", userId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<GalleryCollection>(parameters);
        }

        /// <summary>
        /// Return the list of galleries to which a photo has been added. Galleries are returned sorted by date which the photo was added to the gallery.
        /// </summary>
        /// <param name="photoId">The ID of the photo to fetch a list of galleries for.</param>
        /// <returns></returns>
        public GalleryCollection GalleriesGetListForPhoto(string photoId)
        {
            return GalleriesGetListForPhoto(photoId, 0, 0);
        }

        /// <summary>
        /// Return the list of galleries to which a photo has been added. Galleries are returned sorted by date which the photo was added to the gallery.
        /// </summary>
        /// <param name="photoId">The ID of the photo to fetch a list of galleries for.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of galleries to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public GalleryCollection GalleriesGetListForPhoto(string photoId, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.getListForPhoto");
            parameters.Add("photo_id", photoId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<GalleryCollection>(parameters);
        }

        /// <summary>
        /// Return the list of photos for a gallery.
        /// </summary>
        /// <param name="galleryId">The ID of the gallery of photos to return.</param>
        /// <returns></returns>
        public GalleryPhotoCollection GalleriesGetPhotos(string galleryId)
        {
            return GalleriesGetPhotos(galleryId, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Return the list of photos for a gallery.
        /// </summary>
        /// <param name="galleryId">The ID of the gallery of photos to return.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <returns></returns>
        public GalleryPhotoCollection GalleriesGetPhotos(string galleryId, PhotoSearchExtras extras)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.galleries.getPhotos");
            parameters.Add("gallery_id", galleryId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<GalleryPhotoCollection>(parameters);
        }
    }
}
