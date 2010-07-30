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
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        /// <remarks></remarks>
        public void BlogsGetListAsync(Action<FlickrResult<BlogCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.blogs.getList");
            GetResponseAsync<BlogCollection>(parameters, callback);
        }

        /// <summary>
        /// Return a list of Flickr supported blogging services.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void BlogsGetServicesAsync(Action<FlickrResult<BlogServiceCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.blogs.getServices");

            GetResponseAsync<BlogServiceCollection>(parameters, callback);
        }

        /// <summary>
        /// Posts a photo already uploaded to a blog.
        /// Requires authentication.
        /// </summary>
        /// <param name="blogId">The Id of the blog to post the photo too.</param>
        /// <param name="photoId">The Id of the photograph to post.</param>
        /// <param name="title">The title of the blog post.</param>
        /// <param name="description">The body of the blog post.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void BlogsPostPhotoAsync(string blogId, string photoId, string title, string description, Action<FlickrResult<NoResponse>> callback)
        {
            BlogsPostPhotoAsync(blogId, photoId, title, description, null, callback);
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
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void BlogsPostPhotoAsync(string blogId, string photoId, string title, string description, string blogPassword, Action<FlickrResult<NoResponse>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.blogs.postPhoto");
            parameters.Add("blog_id", blogId);
            parameters.Add("photo_id", photoId);
            parameters.Add("title", title);
            parameters.Add("description", description);
            if (blogPassword != null) parameters.Add("blog_password", blogPassword);

            GetResponseAsync<NoResponse>(parameters, callback);
        }
    }
}
