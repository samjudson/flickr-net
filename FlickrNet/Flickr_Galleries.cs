using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
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
    }
}
