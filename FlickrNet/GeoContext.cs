using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The context to set a geotagged photo as. Used by <see cref="Flickr.PhotosGeoSetContext"/>.
    /// </summary>
    public enum GeoContext
    {
        /// <summary>
        /// The photo has no defined context.
        /// </summary>
        NotDefined = 0,
        /// <summary>
        /// The photo was taken indoors.
        /// </summary>
        Indoors = 1,
        /// <summary>
        /// The photo was taken outdoors.
        /// </summary>
        Outdoors = 2
    }
}
