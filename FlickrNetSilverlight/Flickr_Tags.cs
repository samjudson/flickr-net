using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Collections.ObjectModel;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get the tag list for a given photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to return tags for.</param>
        /// <returns>An instance of the <see cref="PhotoInfo"/> class containing only the <see cref="PhotoInfo.Tags"/> property.</returns>
        public Collection<PhotoInfoTag> TagsGetListPhoto(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListPhoto");
            parameters.Add("api_key", _apiKey);
            parameters.Add("photo_id", photoId);

            PhotoInfo info = GetResponseCache<PhotoInfo>(parameters);
            return info.Tags;
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

        /// <summary>
        /// Gives you a list of tag clusters for the given tag.
        /// </summary>
        /// <param name="tag">The tag to fetch clusters for.</param>
        /// <returns></returns>
        public ClusterCollection TagsGetClusters(string tag)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getClusters");
            parameters.Add("tag", tag);

            return GetResponseCache<ClusterCollection>(parameters);
        }

        /// <summary>
        /// Returns the first 24 photos for a given tag cluster.
        /// </summary>
        /// <param name="cluster">The <see cref="Cluster"/> instance to return the photos for.</param>
        /// <returns></returns>
        public PhotoCollection TagsGetClusterPhotos(Cluster cluster)
        {
            return TagsGetClusterPhotos(cluster.SourceTag, cluster.ClusterId, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Returns the first 24 photos for a given tag cluster.
        /// </summary>
        /// <param name="cluster">The <see cref="Cluster"/> instance to return the photos for.</param>
        /// <param name="extras">Extra information to return with each photo.</param>
        /// <returns></returns>
        public PhotoCollection TagsGetClusterPhotos(Cluster cluster, PhotoSearchExtras extras)
        {
            return TagsGetClusterPhotos(cluster.SourceTag, cluster.ClusterId, extras);
        }

        /// <summary>
        /// Returns the first 24 photos for a given tag cluster.
        /// </summary>
        /// <param name="tag">The tag whose cluster photos you want to return.</param>
        /// <param name="clusterId">The cluster id for the cluster you want to return the photos. This is the first three subtags of the tag cluster appended with hyphens ('-').</param>
        /// <param name="extras">Extra information to return with each photo.</param>
        /// <returns></returns>
        public PhotoCollection TagsGetClusterPhotos(string tag, string clusterId, PhotoSearchExtras extras)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getClusterPhotos");
            parameters.Add("tag", tag);
            parameters.Add("cluster_id", clusterId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Returns a list of hot tags for the given period.
        /// </summary>
        /// <returns></returns>
        public HotTagCollection TagsGetHotList()
        {
            return TagsGetHotList(null, 0);
        }

        /// <summary>
        /// Returns a list of hot tags for the given period.
        /// </summary>
        /// <param name="period">The period for which to fetch hot tags. Valid values are day and week (defaults to day).</param>
        /// <param name="count">The number of tags to return. Defaults to 20. Maximum allowed value is 200.</param>
        /// <returns></returns>
        public HotTagCollection TagsGetHotList(string period, int count)
        {
            if (!String.IsNullOrEmpty(period) && period != "day" && period != "week")
            {
                throw new ArgumentException("Period must be either 'day' or 'week'.", "period");
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getHotList");
            if (!String.IsNullOrEmpty(period)) parameters.Add("period", period);
            if (count > 0) parameters.Add("count", count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<HotTagCollection>(parameters);

        }
    }
}
