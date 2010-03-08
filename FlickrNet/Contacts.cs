using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Contains a list of <see cref="Contact"/> items for a given user.
	/// </summary>
	public class Contacts : List<Contact>, IFlickrParsable
	{
        /// <summary>
        /// The total number of contacts that match the calling query.
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// The page of the result set that has been returned.
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// The number of contacts returned per page.
        /// </summary>
        public int PerPage { get; set; }
        /// <summary>
        /// The number of pages of contacts that are available.
        /// </summary>
        public int Pages { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "contacts")
                throw new XmlException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "per_page":
                    case "perpage":
                        PerPage = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new Exception("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.LocalName == "contact")
            {
                Contact contact = new Contact();
                ((IFlickrParsable)contact).Load(reader);
                Add(contact);
            }

            reader.Skip();
        }

        #endregion
    }

	/// <summary>
	/// Contains details of a contact for a particular user.
	/// </summary>
    public class Contact : IFlickrParsable
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
        public Uri BuddyIconUrl
        {
            get
            {
                if (String.IsNullOrEmpty(IconServer) || IconServer == "0")
                    return new Uri("http://www.flickr.com/images/buddyicon.jpg");
                else
                    return new Uri(String.Format("http://farm{0}.static.flickr.com/{1}/buddyicons/{2}.jpg", IconFarm, IconServer, UserId));
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
                throw new System.Xml.XmlException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

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
                        IsFriend = (reader.Value == "1");
                        break;
                    case "family":
                        IsFamily = (reader.Value == "1");
                        break;
                    case "path_alias":
                        PathAlias = reader.Value;
                        break;
                    case "photos_uploaded":
                        PhotosUploaded = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new Exception("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Skip();
        }

	}
}