using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Abstract class containing a collection of <see cref="Photo"/> instances along with paged information about the result set.
    /// </summary>
    public abstract class PagedPhotoCollection : System.Collections.ObjectModel.Collection<Photo>
    {
        /// <summary>
        /// The Page of results that was returned from Flickr. The first page is 1.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of pages available from Flickr.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The number of photos per page in the result set.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The total number of photos available from Flickr (over all the pages).
        /// </summary>
        public int Total { get; set; }
    }
}
