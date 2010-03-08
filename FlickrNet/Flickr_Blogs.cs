using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets a list of blogs that have been set up by the user.
        /// Requires authentication.
        /// </summary>
        /// <returns>A <see cref="Blogs"/> object containing the list of blogs.</returns>
        /// <remarks></remarks>
        public Blogs BlogsGetList()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.blogs.getList");
            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Blogs;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Return a list of Flickr supported blogging services.
        /// </summary>
        /// <returns></returns>
        public BlogServices BlogsGetServices()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.blogs.getServices");

            return GetResponseCache<BlogServices>(parameters);
        }

        /// <summary>
        /// Posts a photo already uploaded to a blog.
        /// Requires authentication.
        /// </summary>
        /// <param name="blogId">The Id of the blog to post the photo too.</param>
        /// <param name="photoId">The Id of the photograph to post.</param>
        /// <param name="title">The title of the blog post.</param>
        /// <param name="description">The body of the blog post.</param>
        /// <returns>True if the operation is successful.</returns>
        public bool BlogsPostPhoto(string blogId, string photoId, string title, string description)
        {
            return BlogsPostPhoto(blogId, photoId, title, description, null);
        }

        /// <summary>
        /// Posts a photo already uploaded to a blog.
        /// Requires authentication.
        /// </summary>
        /// <param name="blogId">The Id of the blog to post the photo too.</param>
        /// <param name="photoId">The Id of the photograph to post.</param>
        /// <param name="title">The title of the blog post.</param>
        /// <param name="description">The body of the blog post.</param>
        /// <param name="blogPassword">The password of the blog if it is not already stored in flickr.</param>
        /// <returns>True if the operation is successful.</returns>
        public bool BlogsPostPhoto(string blogId, string photoId, string title, string description, string blogPassword)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.blogs.postPhoto");
            parameters.Add("blog_id", blogId);
            parameters.Add("photo_id", photoId);
            parameters.Add("title", title);
            parameters.Add("description", description);
            if (blogPassword != null) parameters.Add("blog_password", blogPassword);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return true;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }


    }
}
