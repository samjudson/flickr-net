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

            var parameters = new Dictionary<string, string>();
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
            if (string.IsNullOrEmpty(userId)) CheckRequiresAuthentication();

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.getTree");
            if (collectionId != null) parameters.Add("collection_id", collectionId);
            if (userId != null) parameters.Add("user_id", userId);

            return GetResponseCache<CollectionCollection>(parameters);
        }

        /// <summary>
        /// Allows you to update the title and description for a collection.
        /// </summary>
        /// <remarks>This method is unsupported by Flickr currently, but it does appear to work.</remarks>
        /// <param name="collectionId">The collection id of the collection, in the format nnnnn-nnnnnnnnnnnnnnnnn.</param>
        /// <param name="title">The new title.</param>
        /// <param name="description">The new description.</param>
        public void CollectionsEditMeta(string collectionId, string title, string description)
        {
            CheckRequiresAuthentication();

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.editMeta");
            parameters.Add("collection_id", collectionId);
            parameters.Add("title", title);
            parameters.Add("description", description);

            GetResponseCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Edits sets in a collection. To add a set make sure that all current set IDs
        /// in the collections are listed along with the set ID to add.
        /// </summary>
        /// <param name="collectionId">The ID of the collection to edit</param>
        /// <param name="photosetIds">The IDs of photosets to be part of the collection</param>
        [Obsolete("Experimental and unsupported by Flickr at this time.")]
        public void CollectionsEditSets(string collectionId, IList<string> photosetIds)
        {
            CheckRequiresAuthentication();

            // construct comma separated list of photoset IDs
            string photosetIdsParameter = "";
            for (int i = 0; i < photosetIds.Count; i++)
            {
                photosetIdsParameter += photosetIds[i];
                if (i < photosetIds.Count - 1)
                    photosetIdsParameter += ",";
            }

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.collections.editSets");
            parameters.Add("collection_id", collectionId);
            parameters.Add("photoset_ids", photosetIdsParameter);
            // not sure what this parameter is for
            parameters.Add("do_remove", "0");

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Removes a set from a collection.
        /// </summary>
        /// <param name="collectionId">The ID of the collection to edit</param>
        /// <param name="photosetId">The ID of the photoset to be removed</param>
        [Obsolete("Experimental and unsupported by Flickr at this time.")]
        public void CollectionsRemoveSet(string collectionId, string photosetId)
        {
            CheckRequiresAuthentication();
            
            var parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.collections.removeSet");
            parameters.Add("collection_id", collectionId);
            parameters.Add("photoset_id", photosetId);

            GetResponseNoCache<NoResponse>(parameters);
        }

    }
}
