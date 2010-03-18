using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get the tag list for a given photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to return tags for.</param>
        /// <returns>An instance of the <see cref="PhotoInfo"/> class containing only the <see cref="PhotoInfo.Tags"/> property.</returns>
        public PhotoInfoTag[] TagsGetListPhoto(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListPhoto");
            parameters.Add("api_key", _apiKey);
            parameters.Add("photo_id", photoId);

            PhotoInfo info = GetResponseCache<PhotoInfo>(parameters);
            return info.Tags.ToArray();
        }

        /// <summary>
        /// Get the tag list for a given user (or the currently logged in user).
        /// </summary>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetListUser()
        {
            return TagsGetListUser(null);
        }

        /// <summary>
        /// Get the tag list for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetListUser(string userId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListUser");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            return GetResponseCache<TagCollection>(parameters);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetListUserPopular()
        {
            CheckRequiresAuthentication();

            return TagsGetListUserPopular(null, 0);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetListUserPopular(int count)
        {
            CheckRequiresAuthentication();

            return TagsGetListUserPopular(null, count);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetListUserPopular(string userId)
        {
            return TagsGetListUserPopular(userId, 0);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetListUserPopular(string userId, int count)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListUserPopular");
            if (userId != null) parameters.Add("user_id", userId);
            if (count > 0) parameters.Add("count", count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<TagCollection>(parameters);
        }

        /// <summary>
        /// Gets a list of 'cleaned' tags and the raw values for those tags.
        /// </summary>
        /// <returns>An array of <see cref="RawTag"/> objects.</returns>
        public RawTagCollection TagsGetListUserRaw()
        {
            return TagsGetListUserRaw(null);
        }

        /// <summary>
        /// Gets a list of 'cleaned' tags and the raw values for a specific tag.
        /// </summary>
        /// <param name="tag">The tag to return the raw version of.</param>
        /// <returns>An array of <see cref="RawTag"/> objects.</returns>
        public RawTagCollection TagsGetListUserRaw(string tag)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListUserRaw");
            if (tag != null && tag.Length > 0) parameters.Add("tag", tag);

            return GetResponseCache<RawTagCollection>(parameters);
        }

        /// <summary>
        /// Returns a list of tags 'related' to the given tag, based on clustered usage analysis.
        /// </summary>
        /// <param name="tag">The tag to fetch related tags for.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public TagCollection TagsGetRelated(string tag)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getRelated");
            parameters.Add("api_key", _apiKey);
            parameters.Add("tag", tag);

            return GetResponseCache<TagCollection>(parameters);
        }
    }
}
