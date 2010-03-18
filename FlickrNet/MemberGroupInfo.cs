using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Information about a group the authenticated user is a member of.
    /// </summary>
    public class MemberGroupInfo : IFlickrParsable
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
        /// The number of photos currently in the group pool.
        /// </summary>
        public int NumberOfPhotos { get; private set; }

        /// <summary>
        /// The privacy of the pool (see <see cref="PoolPrivacy"/>).
        /// </summary>
        public PoolPrivacy Privacy { get; private set; }

        /// <summary>
        /// The server number for the group icon.
        /// </summary>
        public string IconServer { get; private set; }

        /// <summary>
        /// The web farm ID for the group icon.
        /// </summary>
        public string IconFarm { get; private set; }

        /// <summary>
        /// The number of photos currently in this group.
        /// </summary>
        public long Photos { get; private set; }

        /// <summary>
        /// The URL for the group icon.
        /// </summary>
        public Uri GroupIconUrl
        {
            get
            {
                if (String.IsNullOrEmpty(IconServer) || IconServer == "0")
                {
                    return new Uri("http://www.flickr.com/images/buddyicon.jpg");
                }
                else
                {
                    return new Uri(String.Format("http://farm{0}.static.flickr.com/{1}/buddyicons/{2}.jpg", IconFarm, IconServer, GroupId));
                }
            }
        }

        /// <summary>
        /// The URL for the group web page.
        /// </summary>
        public Uri GroupUrl
        {
            get { return new Uri(String.Format("http://www.flickr.com/groups/{0}/", GroupId)); }
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                    case "id":
                        GroupId = reader.Value;
                        break;
                    case "name":
                        GroupName = reader.Value;
                        break;
                    case "admin":
                        IsAdmin = reader.Value == "1";
                        break;
                    case "privacy":
                        Privacy = (PoolPrivacy)Enum.Parse(typeof(PoolPrivacy), reader.Value, true);
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    case "photos":
                        Photos = long.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }
        }
    }

}
