using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Information about public groups for a user.
    /// </summary>
    [System.Serializable]
    public class PublicGroupInfo : IFlickrParsable
    {

        /// <summary>
        /// Property which returns the group id for the group.
        /// </summary>
        public string GroupId { get; private set; }

        /// <summary>The group name.</summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// True if the user is the admin for the group, false if they are not.
        /// </summary>
        public bool IsAdmin { get; private set; }

        /// <summary>
        /// Will contain 1 if the group is restricted to people who are 18 years old or over, 0 if it is not.
        /// </summary>
        public bool EighteenPlus { get; private set; }

        /// <summary>
        /// The URL for the group web page.
        /// </summary>
        public Uri GroupUrl
        {
            get { return new Uri(String.Format("http://www.flickr.com/groups/{0}/", GroupId)); }
        }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                        GroupId = reader.Value;
                        break;
                    case "name":
                        GroupName = reader.Value;
                        break;
                    case "admin":
                        IsAdmin = reader.Value == "1";
                        break;
                    case "eighteenplus":
                        EighteenPlus = reader.Value == "1";
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();
        }
    }
}
