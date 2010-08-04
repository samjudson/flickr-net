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
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListPhotoAsync(string photoId, Action<FlickrResult<Collection<PhotoInfoTag>>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListPhoto");
            parameters.Add("api_key", apiKey);
            parameters.Add("photo_id", photoId);

            GetResponseAsync<PhotoInfo>(
                parameters,
                r =>
                {
                    FlickrResult<Collection<PhotoInfoTag>> result = new FlickrResult<Collection<PhotoInfoTag>>();
                    result.Error = r.Error;
                    if (!r.HasError)
                    {
                        result.Result = r.Result.Tags;
                    }
                    callback(result);
                });
        }

        /// <summary>
        /// Get the tag list for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserAsync(Action<FlickrResult<TagCollection>> callback)
        {
            TagsGetListUserAsync(null, callback);
        }

        /// <summary>
        /// Get the tag list for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserAsync(string userId, Action<FlickrResult<TagCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListUser");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            GetResponseAsync<TagCollection>(parameters, callback);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserPopularAsync(Action<FlickrResult<TagCollection>> callback)
        {
            CheckRequiresAuthentication();

            TagsGetListUserPopularAsync(null, 0, callback);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserPopularAsync(int count, Action<FlickrResult<TagCollection>> callback)
        {
            CheckRequiresAuthentication();

            TagsGetListUserPopularAsync(null, count, callback);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserPopularAsync(string userId, Action<FlickrResult<TagCollection>> callback)
        {
            TagsGetListUserPopularAsync(userId, 0, callback);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserPopularAsync(string userId, int count, Action<FlickrResult<TagCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListUserPopular");
            if (userId != null) parameters.Add("user_id", userId);
            if (count > 0) parameters.Add("count", count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<TagCollection>(parameters, callback);
        }

        /// <summary>
        /// Gets a list of 'cleaned' tags and the raw values for those tags.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserRawAsync(Action<FlickrResult<RawTagCollection>> callback)
        {
            TagsGetListUserRawAsync(null, callback);
        }

        /// <summary>
        /// Gets a list of 'cleaned' tags and the raw values for a specific tag.
        /// </summary>
        /// <param name="tag">The tag to return the raw version of.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetListUserRawAsync(string tag, Action<FlickrResult<RawTagCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getListUserRaw");
            if (tag != null && tag.Length > 0) parameters.Add("tag", tag);

            GetResponseAsync<RawTagCollection>(parameters, callback);
        }

        /// <summary>
        /// Returns a list of tags 'related' to the given tag, based on clustered usage analysis.
        /// </summary>
        /// <param name="tag">The tag to fetch related tags for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetRelatedAsync(string tag, Action<FlickrResult<TagCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getRelated");
            parameters.Add("api_key", apiKey);
            parameters.Add("tag", tag);

            GetResponseAsync<TagCollection>(parameters, callback);
        }

        /// <summary>
        /// Gives you a list of tag clusters for the given tag.
        /// </summary>
        /// <param name="tag">The tag to fetch clusters for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetClustersAsync(string tag, Action<FlickrResult<ClusterCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getClusters");
            parameters.Add("tag", tag);

            GetResponseAsync<ClusterCollection>(parameters, callback);
        }

        /// <summary>
        /// Returns the first 24 photos for a given tag cluster.
        /// </summary>
        /// <param name="cluster">The <see cref="Cluster"/> instance to return the photos for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetClusterPhotosAsync(Cluster cluster, Action<FlickrResult<PhotoCollection>> callback)
        {
            TagsGetClusterPhotosAsync(cluster.SourceTag, cluster.ClusterId, PhotoSearchExtras.None, callback);
        }

        /// <summary>
        /// Returns the first 24 photos for a given tag cluster.
        /// </summary>
        /// <param name="cluster">The <see cref="Cluster"/> instance to return the photos for.</param>
        /// <param name="extras">Extra information to return with each photo.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetClusterPhotosAsync(Cluster cluster, PhotoSearchExtras extras, Action<FlickrResult<PhotoCollection>> callback)
        {
            TagsGetClusterPhotosAsync(cluster.SourceTag, cluster.ClusterId, extras, callback);
        }

        /// <summary>
        /// Returns the first 24 photos for a given tag cluster.
        /// </summary>
        /// <param name="tag">The tag whose cluster photos you want to return.</param>
        /// <param name="clusterId">The cluster id for the cluster you want to return the photos. This is the first three subtags of the tag cluster appended with hyphens ('-').</param>
        /// <param name="extras">Extra information to return with each photo.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetClusterPhotosAsync(string tag, string clusterId, PhotoSearchExtras extras, Action<FlickrResult<PhotoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getClusterPhotos");
            parameters.Add("tag", tag);
            parameters.Add("cluster_id", clusterId);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            GetResponseAsync<PhotoCollection>(parameters, callback);
        }

        /// <summary>
        /// Returns a list of hot tags for the given period.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetHotListAsync(Action<FlickrResult<HotTagCollection>> callback)
        {
            TagsGetHotListAsync(null, 0, callback);
        }

        /// <summary>
        /// Returns a list of hot tags for the given period.
        /// </summary>
        /// <param name="period">The period for which to fetch hot tags. Valid values are day and week (defaults to day).</param>
        /// <param name="count">The number of tags to return. Defaults to 20. Maximum allowed value is 200.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void TagsGetHotListAsync(string period, int count, Action<FlickrResult<HotTagCollection>> callback)
        {
            if (!String.IsNullOrEmpty(period) && period != "day" && period != "week")
            {
                throw new ArgumentException("Period must be either 'day' or 'week'.", "period");
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.tags.getHotList");
            if (!String.IsNullOrEmpty(period)) parameters.Add("period", period);
            if (count > 0) parameters.Add("count", count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<HotTagCollection>(parameters, callback);
        }
    }
}
