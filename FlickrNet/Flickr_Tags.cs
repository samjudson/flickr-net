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
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.tags.getListPhoto");
            parameters.Add("api_key", _apiKey);
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.PhotoInfo.Tags.TagCollection;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Get the tag list for a given user (or the currently logged in user).
        /// </summary>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetListUser()
        {
            return TagsGetListUser(null);
        }

        /// <summary>
        /// Get the tag list for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetListUser(string userId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.tags.getListUser");
            if (userId != null && userId.Length > 0) parameters.Add("user_id", userId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
                Tag[] tags = new Tag[nodes.Count];
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = new Tag(nodes[i]);
                }
                return tags;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetListUserPopular()
        {
            return TagsGetListUserPopular(null, 0);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetListUserPopular(int count)
        {
            return TagsGetListUserPopular(null, count);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetListUserPopular(string userId)
        {
            return TagsGetListUserPopular(userId, 0);
        }

        /// <summary>
        /// Get the popular tags for a given user (or the currently logged in user).
        /// </summary>
        /// <param name="userId">The NSID of the user to fetch the tag list for. If this argument is not specified, the currently logged in user (if any) is assumed.</param>
        /// <param name="count">Number of popular tags to return. defaults to 10 when this argument is not present.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetListUserPopular(string userId, int count)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.tags.getListUserPopular");
            if (userId != null) parameters.Add("user_id", userId);
            if (count > 0) parameters.Add("count", count.ToString());

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
                Tag[] tags = new Tag[nodes.Count];
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = new Tag(nodes[i]);
                }
                return tags;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets a list of 'cleaned' tags and the raw values for those tags.
        /// </summary>
        /// <returns>An array of <see cref="RawTag"/> objects.</returns>
        public RawTag[] TagsGetListUserRaw()
        {
            return TagsGetListUserRaw(null);
        }

        /// <summary>
        /// Gets a list of 'cleaned' tags and the raw values for a specific tag.
        /// </summary>
        /// <param name="tag">The tag to return the raw version of.</param>
        /// <returns>An array of <see cref="RawTag"/> objects.</returns>
        public RawTag[] TagsGetListUserRaw(string tag)
        {
            CheckRequiresAuthentication();

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.tags.getListUserRaw");
            if (tag != null && tag.Length > 0) parameters.Add("tag", tag);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
                RawTag[] tags = new RawTag[nodes.Count];
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = new RawTag(nodes[i]);
                }
                return tags;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns a list of tags 'related' to the given tag, based on clustered usage analysis.
        /// </summary>
        /// <param name="tag">The tag to fetch related tags for.</param>
        /// <returns>An array of <see cref="Tag"/> objects.</returns>
        public Tag[] TagsGetRelated(string tag)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.tags.getRelated");
            parameters.Add("api_key", _apiKey);
            parameters.Add("tag", tag);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                XmlNodeList nodes = response.AllElements[0].SelectNodes("//tag");
                Tag[] tags = new Tag[nodes.Count];
                for (int i = 0; i < tags.Length; i++)
                {
                    tags[i] = new Tag(nodes[i]);
                }
                return tags;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }
    }
}
