using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// An enumeration of different media types tto search for.
    /// </summary>
    public enum MediaType
    {
        /// <summary>
        /// Default MediaType. Does not correspond to a value that is sent to Flickr.
        /// </summary>
        None,
        /// <summary>
        /// All media types will be return.
        /// </summary>
        All,
        /// <summary>
        /// Only photos will be returned.
        /// </summary>
        Photos,
        /// <summary>
        /// Only videos will be returned.
        /// </summary>
        Videos
    }
}
