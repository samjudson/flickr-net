using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The various pricay settings for a group.
    /// </summary>
    public enum PoolPrivacy
    {
        /// <summary>
        /// No privacy setting specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// The group is a private group. You cannot view pictures or posts until you are a 
        /// member. The group is also invite only.
        /// </summary>
        Private = 1,
        /// <summary>
        /// A public group where you can see posts and photos in the group. The group is however invite only.
        /// </summary>
        InviteOnlyPublic = 2,
        /// <summary>
        /// A public group.
        /// </summary>
        OpenPublic = 3
    }

}
