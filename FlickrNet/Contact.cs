using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Contains details of a contact for a particular user.
    /// </summary>
    public sealed class Contact : IFlickrParsable
    {
        /// <summary>
        /// The user id of the contact.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The username (or screen name) of the contact.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The users real name. Only returned for authenticated calls to <see cref="Flickr.ContactsGetList()"/>.
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// The location of the contact. Only returned for auehtnicated calls to <see cref="Flickr.ContactsGetList()"/>.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The URL path alias for the contact.  Only returned for auehtnicated calls to <see cref="Flickr.ContactsGetList()"/>.
        /// </summary>
        public string PathAlias { get; set; }

        /// <summary>
        /// The icon server for this contacts buddy icon.
        /// </summary>
        public string IconServer { get; set; }

        /// <summary>
        /// The icon farm for this contacts buddy icon.
        /// </summary>
        public string IconFarm { get; set; }

        /// <summary>
        /// The buddy icon for this contact.
        /// </summary>
        public string BuddyIconUrl
        {
            get
            {
                if (String.IsNullOrEmpty(IconServer) || IconServer == "0")
                    return "http://www.flickr.com/images/buddyicon.jpg";
                else
                    return String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://farm{0}.static.flickr.com/{1}/buddyicons/{2}.jpg", IconFarm, IconServer, UserId);
            }
        }

        /// <summary>
        /// The number of photos uploaded. Only returned by <see cref="Flickr.ContactsGetListRecentlyUploaded()"/>
        /// </summary>
        public int? PhotosUploaded { get; set; }

        /// <summary>
        /// Is this contact marked as a friend contact?
        /// </summary>
        public bool? IsFriend { get; set; }

        /// <summary>
        /// Is this user marked a family contact?
        /// </summary>
        public bool? IsFamily { get; set; }

        /// <summary>
        /// Unsure how to even set this!
        /// </summary>
        public bool? IsIgnored { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "contact")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                        UserId = reader.Value;
                        break;
                    case "username":
                        UserName = reader.Value;
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    case "ignored":
                        IsIgnored = reader.Value == "0";
                        break;
                    case "realname":
                        RealName = reader.Value;
                        break;
                    case "location":
                        Location = reader.Value;
                        break;
                    case "friend":
                        IsFriend = reader.Value == "1";
                        break;
                    case "family":
                        IsFamily = reader.Value == "1";
                        break;
                    case "path_alias":
                        PathAlias = reader.Value;
                        break;
                    case "photos_uploaded":
                        PhotosUploaded = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Skip();
        }

    }
}
