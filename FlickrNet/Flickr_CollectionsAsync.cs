using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {

        /// <summary>
        /// Gets information about a collection. Requires authentication with 'read' access.
        /// </summary>
        /// <param name="collectionId">The ID for the collection to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void CollectionsGetInfoAsync(string collectionId, Action<FlickrResult<CollectionInfo>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.getInfo");
            parameters.Add("collection_id", collectionId);

            GetResponseAsync<CollectionInfo>(parameters, callback);

        }

        /// <summary>
        /// Gets a tree of collection. Requires authentication.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void CollectionsGetTreeAsync(Action<FlickrResult<CollectionCollection>> callback)
        {
            CollectionsGetTreeAsync(null, null, callback);
        }

        /// <summary>
        /// Gets a tree of collection.
        /// </summary>
        /// <param name="collectionId ">The ID of the collection to fetch a tree for, or zero to fetch the root collection.</param>
        /// <param name="userId">The ID of the user to fetch the tree for, or null if using the authenticated user.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void CollectionsGetTreeAsync(string collectionId, string userId, Action<FlickrResult<CollectionCollection>> callback)
        {
            if (String.IsNullOrEmpty(userId)) CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.getTree");
            if (collectionId != null) parameters.Add("collection_id", collectionId);
            if (userId != null) parameters.Add("user_id", userId);

            GetResponseAsync<CollectionCollection>(parameters, callback);
        }

    }
}
