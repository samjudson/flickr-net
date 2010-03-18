using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FlickrNet
{
    /// <summary>
    /// The various pricay settings for a group.
    /// </summary>
    [System.Serializable]
    public enum PoolPrivacy
    {
        /// <summary>
        /// No privacy setting specified.
        /// </summary>
        [XmlEnum("0")]
        None = 0,

        /// <summary>
        /// The group is a private group. You cannot view pictures or posts until you are a 
        /// member. The group is also invite only.
        /// </summary>
        [XmlEnum("1")]
        Private = 1,
        /// <summary>
        /// A public group where you can see posts and photos in the group. The group is however invite only.
        /// </summary>
        [XmlEnum("2")]
        InviteOnlyPublic = 2,
        /// <summary>
        /// A public group.
        /// </summary>
        [XmlEnum("3")]
        OpenPublic = 3
    }

}
