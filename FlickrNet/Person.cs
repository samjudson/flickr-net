using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{

	/// <summary>
	/// The <see cref="Person"/> class contains details returned by the <see cref="Flickr.PeopleGetInfo"/>
	/// method.
	/// </summary>
	public class Person : IFlickrParsable
	{
		/// <summary>The user id of the user.</summary>
		/// <remarks/>
        public string UserId { get; private set; }
    
		/// <summary>Is the user an administrator. 
		/// 1 = admin, 0 = normal user.</summary>
		/// <remarks></remarks>
        public bool IsAdmin { get; private set; }

		/// <summary>Does the user posses a pro account.
		/// 0 = free acouunt, 1 = pro account holder.</summary>
        public bool IsPro { get; private set; }
	
		/// <summary>The server that will serve up the users Buddy Icon.</summary>
        public string IconServer { get; private set; }

        /// <summary>The server farm that will serve up the users Buddy Icon.</summary>
        public string IconFarm { get; private set; }

        /// <summary>The gender of the user on Flickr. May be null, or X for unspecified.</summary>
        public string Gender { get; private set; }

        /// <summary>
        /// Is the person ignored by the calling user. Will be null if not an authenticated call.
        /// </summary>
        public bool? IsIgnored { get; private set; }

        /// <summary>
        /// Is the person a contact of the calling user. Will be null if not an authenticated call.
        /// </summary>
        public bool? IsContact { get; private set; }

        /// <summary>
        /// Is the person a friend of the calling user. Will be null if not an authenticated call.
        /// </summary>
        public bool? IsFriend { get; private set; }

        /// <summary>
        /// Is the person family of the calling user. Will be null if not an authenticated call.
        /// </summary>
        public bool? IsFamily { get; private set; }

        /// <summary>
        /// Has the person marked the calling user as a contact.  Will be null if not an authenticated call.
        /// </summary>
        public bool? IsReverseContact { get; private set; }

        /// <summary>
        /// Has the person marked the calling user as a friend.  Will be null if not an authenticated call.
        /// </summary>
        public bool? IsReverseFriend { get; private set; }

        /// <summary>
        /// Has the person marked the calling user as family.  Will be null if not an authenticated call.
        /// </summary>
        public bool? IsReverseFamily { get; private set; }

        /// <summary>The users username, also known as their screenname.</summary>
        public string UserName { get; private set; }
	
		/// <summary>The users real name, as entered in their profile.</summary>
        public string RealName { get; private set; }
	
		/// <summary>The SHA1 hash of the users email address - used for FOAF networking.</summary>
        public string MailboxSha1Hash { get; private set; }
	
		/// <summary>Consists of your current location followed by country.</summary>
		/// <example>e.g. Newcastle, UK.</example>
        public string Location { get; private set; }

		/// <summary>Sub element containing a summary of the users photo information.</summary>
		/// <remarks/>
        public PersonPhotosSummary PhotosSummary { get; private set; }

        /// <summary>
        /// The users URL alias, if any.
        /// </summary>
        public string PathAlias { get; private set; }

		/// <summary>
		/// The users photo location on Flickr
		/// http://www.flickr.com/photos/username/
		/// </summary>
        public Uri PhotosUrl { get; private set; }

		/// <summary>
		/// The users profile location on Flickr
		/// http://www.flickr.com/people/username/
		/// </summary>
        public Uri ProfileUrl { get; private set; }

        /// <summary>
        /// The users profile location on Flickr
        /// http://m.flickr.com/photostream.gne?id=ID
        /// </summary>
        public Uri MobileUrl { get; private set; }

		/// <summary>
		/// Returns the <see cref="Uri"/> for the users Buddy Icon.
		/// </summary>
		public Uri BuddyIconUrl
		{
			get
			{
				if( String.IsNullOrEmpty(IconServer) || IconServer == "0" )
					return new Uri("http://www.flickr.com/images/buddyicon.jpg");
				else
                    return new Uri(String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://static.flickr.com/{0}/buddyicons/{1}.jpg", IconServer, UserId));
			}
		}

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                    case "nsid":
                        UserId = reader.LocalName;
                        break;
                    case "ispro":
                        IsPro = reader.Value == "1";
                        break;
                    case "iconserver":
                        IconServer = reader.Value;
                        break;
                    case "iconfarm":
                        IconFarm = reader.Value;
                        break;
                    case "path_alias":
                        PathAlias = reader.Value;
                        break;
                    case "gender":
                        Gender = reader.Value;
                        break;
                    case "ignored":
                        IsIgnored = reader.Value == "1";
                        break;
                    case "contact":
                        IsContact = reader.Value == "1";
                        break;
                    case "friend":
                        IsFriend = reader.Value == "1";
                        break;
                    case "family":
                        IsFamily = reader.Value == "1";
                        break;
                    case "revcontact":
                        IsReverseContact = reader.Value == "1";
                        break;
                    case "revfriend":
                        IsReverseFriend = reader.Value == "1";
                        break;
                    case "revfamily":
                        IsReverseFamily = reader.Value == "1";
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.LocalName != "person")
            {
                switch (reader.LocalName)
                {
                    case "username":
                        UserName = reader.ReadElementContentAsString();
                        break;
                    case "location":
                        Location = reader.ReadElementContentAsString();
                        break;
                    case "realname":
                        RealName = reader.ReadElementContentAsString();
                        break;
                    case "photosurl":
                        PhotosUrl = new Uri(reader.ReadElementContentAsString());
                        break;
                    case "profileurl":
                        ProfileUrl = new Uri(reader.ReadElementContentAsString());
                        break;
                    case "mobileurl":
                        MobileUrl = new Uri(reader.ReadElementContentAsString());
                        break;
                    case "photos":
                        PhotosSummary = new PersonPhotosSummary();
                        ((IFlickrParsable)PhotosSummary).Load(reader);
                        break;
                    default:
                        throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");
                }
            }

        }
    }

	/// <summary>
	/// A summary of a users photos.
	/// </summary>
	public class PersonPhotosSummary : IFlickrParsable
	{
		/// <summary>The first date the user uploaded a picture, converted into <see cref="DateTime"/> format.</summary>
		public DateTime FirstDate { get; private set; }

		/// <summary>The first date the user took a picture, converted into <see cref="DateTime"/> format.</summary>
		public DateTime FirstTakenDate { get; private set; }

        /// <summary>The total number of photos for the user.</summary>
		/// <remarks/>
		public int PhotoCount { get; private set; }

		/// <summary>The total number of photos for the user.</summary>
		/// <remarks/>
		public int Views { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName != "photos")
            {
                switch (reader.LocalName)
                {
                    case "firstdatetaken":
                        FirstTakenDate = UtilityMethods.ParseDateWithGranularity(reader.ReadElementContentAsString());
                        break;
                    case "firstdate":
                        FirstDate = UtilityMethods.UnixTimestampToDate(reader.ReadElementContentAsString());
                        break;
                    case "count":
                        PhotoCount = reader.ReadElementContentAsInt();
                        break;
                    default:
                        throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");
                }
            }

            reader.Read();
        }
    }
}
