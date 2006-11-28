using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{

	/// <summary>
	/// The <see cref="Person"/> class contains details returned by the <see cref="Flickr.PeopleGetInfo"/>
	/// method.
	/// </summary>
	[System.Serializable]
	public class Person
	{
		private string _userId;
		private int _isAdmin;
		private int _isPro;
		private int _iconServer;
		private int _iconFarm;
		private string _username;
		private string _realname;
		private string _location;
		private PersonPhotosSummary _summary = new PersonPhotosSummary();
		private string _photosUrl;
		private string _profileUrl;
		private string _mobileUrl;
		private string _mboxHash;

		/// <summary>The user id of the user.</summary>
		/// <remarks/>
		[XmlAttribute("nsid", Form=XmlSchemaForm.Unqualified)]
		public string UserId { get { return _userId; } set { _userId = value; } }
    
		/// <summary>Is the user an administrator. 
		/// 1 = admin, 0 = normal user.</summary>
		/// <remarks></remarks>
		[XmlAttribute("isadmin", Form=XmlSchemaForm.Unqualified)]
		public int IsAdmin { get { return _isAdmin; } set { _isAdmin = value; } }

		/// <summary>Does the user posses a pro account.
		/// 0 = free acouunt, 1 = pro account holder.</summary>
		[XmlAttribute("ispro", Form=XmlSchemaForm.Unqualified)]
		public int IsPro { get { return _isPro; } set { _isPro = value; } }
	
		/// <summary>Does the user posses a pro account.
		/// 0 = free acouunt, 1 = pro account holder.</summary>
		[XmlAttribute("iconserver", Form=XmlSchemaForm.Unqualified)]
		public int IconServer { get { return _iconServer; } set { _iconServer = value; } }
	
		/// <summary>No idea what purpose this field serves.</summary>
		[XmlAttribute("iconfarm", Form=XmlSchemaForm.Unqualified)]
		public int IconFarm { get { return _iconFarm; } set { _iconFarm = value; } }
	
		/// <summary>The users username, also known as their screenname.</summary>
		[XmlElement("username", Form=XmlSchemaForm.Unqualified)]
		public string UserName { get { return _username; } set { _username = value; } }
	
		/// <summary>The users real name, as entered in their profile.</summary>
		[XmlElement("realname", Form=XmlSchemaForm.Unqualified)]
		public string RealName { get { return _realname; } set { _realname = value; } }
	
		/// <summary>The SHA1 hash of the users email address - used for FOAF networking.</summary>
		[XmlElement("mbox_sha1sum", Form=XmlSchemaForm.Unqualified)]
		public string MailBoxSha1Hash { get { return _mboxHash; } set { _mboxHash = value; } }
	
		/// <summary>Consists of your current location followed by country.</summary>
		/// <example>e.g. Newcastle, UK.</example>
		[XmlElement("location", Form=XmlSchemaForm.Unqualified)]
		public string Location { get { return _location; } set { _location = value; } }

		/// <summary>Sub element containing a summary of the users photo information.</summary>
		/// <remarks/>
		[XmlElement("photos", Form=XmlSchemaForm.Unqualified)]
		public PersonPhotosSummary PhotosSummary { get { return _summary; } set { _summary = value; } }

		/// <summary>
		/// The users photo location on Flickr
		/// http://www.flickr.com/photos/username/
		/// </summary>
		[XmlElement("photosurl",Form=XmlSchemaForm.Unqualified)]
		public string PhotosUrl { get { return _photosUrl; } set { _photosUrl = value; } }

		/// <summary>
		/// The users profile location on Flickr
		/// http://www.flickr.com/people/username/
		/// </summary>
		[XmlElement("profileurl",Form=XmlSchemaForm.Unqualified)]
		public string ProfileUrl { get { return _profileUrl; } set { _profileUrl = value; } }

		/// <summary>
		/// The users mobile home page on Flickr
		/// http://www.flickr.com/mod/photostream.gne?id=xxx/
		/// </summary>
		[XmlElement("mobileurl",Form=XmlSchemaForm.Unqualified)]
		public string MobileUrl { get { return _mobileUrl; } set { _mobileUrl = value; } }

		/// <summary>
		/// Returns the <see cref="Uri"/> for the users Buddy Icon.
		/// </summary>
		[XmlIgnore()]
		public Uri BuddyIconUrl
		{
			get
			{
				if( IconServer == 0 )
					return new Uri("http://www.flickr.com/images/buddyicon.jpg");
				else
					return new Uri(String.Format("http://static.flickr.com/{0}/buddyicons/{1}.jpg", IconServer, UserId));
			}
		}
	}

	/// <summary>
	/// A summary of a users photos.
	/// </summary>
	[System.Serializable]
	public class PersonPhotosSummary
	{
		private int _photoCount;
		private int _views;

		/// <summary>The first date the user uploaded a picture, converted into <see cref="DateTime"/> format.</summary>
		[XmlIgnore()]
		public DateTime FirstDate
		{
			get { return Utils.UnixTimestampToDate(firstdate_raw); }
		}

		/// <summary>The first date the user took a picture, converted into <see cref="DateTime"/> format.</summary>
		[XmlIgnore()]
		public DateTime FirstTakenDate
		{
			get 
			{
				if( firsttakendate_raw == null || firsttakendate_raw.Length == 0 ) return DateTime.MinValue;
				return System.DateTime.Parse(firsttakendate_raw);
			}
		}

		/// <summary>The total number of photos for the user.</summary>
		/// <remarks/>
		[XmlElement("count", Form=XmlSchemaForm.Unqualified)]
		public int PhotoCount
		{
			get { return _photoCount; }
			set { _photoCount = value; }
		}

		/// <summary>The total number of photos for the user.</summary>
		/// <remarks/>
		[XmlElement("views", Form=XmlSchemaForm.Unqualified)]
		public int Views
		{
			get { return _views; }
			set { _views = value; }
		}

		/// <remarks>The unix timestamp of the date the first photo was uploaded.</remarks>
		[XmlElement("firstdate", Form=XmlSchemaForm.Unqualified)]
		public string firstdate_raw;

		/// <remarks>The date the first photo was taken.</remarks>
		[XmlElement("firsttakendate", Form=XmlSchemaForm.Unqualified)]
		public string firsttakendate_raw;

	}
}
