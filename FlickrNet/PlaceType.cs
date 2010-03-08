using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Used by <see cref="Flickr.PlacesPlacesForUser()"/>.
    /// </summary>
    public enum PlaceType
    {
        /// <summary>
        /// No place type selected. Not used by the Flickr API.
        /// </summary>
        None = 0,
        /// <summary>
        /// Locality.
        /// </summary>
        Locality = 7,
        /// <summary>
        /// Neighbourhood.
        /// </summary>
        Neighbourhood = 22,
        /// <summary>
        /// Region.
        /// </summary>
        Region = 8,
        /// <summary>
        /// Country.
        /// </summary>
        Country = 12,
        /// <summary>
        /// Continent.
        /// </summary>
        Continent = 29
    }
}
