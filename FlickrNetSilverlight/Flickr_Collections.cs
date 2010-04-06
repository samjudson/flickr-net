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
        /// <returns>An instance of the <see cref="CollectionInfo"/> class.</returns>
        public CollectionInfo CollectionsGetInfo(string collectionId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.getInfo");
            parameters.Add("collection_id", collectionId);

            return GetResponseCache<CollectionInfo>(parameters);

        }

        /// <summary>
        /// Gets a tree of collection. Requires authentication.
        /// </summary>
        /// <returns>An array of <see cref="Collection"/> instances.</returns>
        public CollectionCollection CollectionsGetTree()
        {
            return CollectionsGetTree(null, null);
        }

        /// <summary>
        /// Gets a tree of collection.
        /// </summary>
        /// <param name="collectionId ">The ID of the collection to fetch a tree for, or zero to fetch the root collection.</param>
        /// <param name="userId">The ID of the user to fetch the tree for, or null if using the authenticated user.</param>
        /// <returns>An array of <see cref="Collection"/> instances.</returns>
        public CollectionCollection CollectionsGetTree(string collectionId, string userId)
        {
            if (String.IsNullOrEmpty(userId)) CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.getTree");
            if (collectionId != null) parameters.Add("collection_id", collectionId);
            if (userId != null) parameters.Add("user_id", userId);

            return GetResponseCache<CollectionCollection>(parameters);
        }

    }
}
