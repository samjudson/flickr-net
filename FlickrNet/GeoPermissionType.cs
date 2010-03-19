using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The default privacy level for geographic information attached to the user's photos.
    /// </summary>
    public enum GeoPermissionType
    {
        /// <summary>
        /// No default set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Anyone can see the geographic information.
        /// </summary>
        Public = 1,
        /// <summary>
        /// Only contacts can see the geographic information.
        /// </summary>
        ContatsOnly = 2,
        /// <summary>
        /// Only Friends and Family can see the geographic information.
        /// </summary>
        FriendsAndFamilyOnly = 3,
        /// <summary>
        /// Only Friends can see the geographic information.
        /// </summary>
        FriendsOnly = 4,
        /// <summary>
        /// Only Family can see the geographic information.
        /// </summary>
        FamilyOnly = 5,
        /// <summary>
        /// Only you can see the geographic information.
        /// </summary>
        Private = 6
    }
}
