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
		/// <summary>The user id of the user.</summary>
		/// <remarks/>
		[XmlAttribute("nsid", Form=XmlSchemaForm.Unqualified)]
		public string UserId;
    
		/// <summary>Is the user an administrator. 
		/// 1 = admin, 0 = normal user.</summary>
		/// <remarks></remarks>
		[XmlAttribute("isadmin", Form=XmlSchemaForm.Unqualified)]
		public int IsAdmin;

		/// <summary>Does the user posses a pro account.
		/// 0 = free acouunt, 1 = pro account holder.</summary>
		[XmlAttribute("ispro", Form=XmlSchemaForm.Unqualified)]
		public int IsPro;
	
		/// <summary>Does the user posses a pro account.
		/// 0 = free acouunt, 1 = pro account holder.</summary>
		[XmlAttribute("iconserver", Form=XmlSchemaForm.Unqualified)]
		public int IconServer;
	
		/// <summary>The users username, also known as their screenname.</summary>
		[XmlElement("username", Form=XmlSchemaForm.Unqualified)]
		public string UserName;
	
		/// <summary>The users real name, as entered in their profile.</summary>
		[XmlElement("realname", Form=XmlSchemaForm.Unqualified)]
		public string RealName;
	
		/// <summary>Consists of your current location followed by country.</summary>
		/// <example>e.g. Newcastle, UK.</example>
		[XmlElement("location", Form=XmlSchemaForm.Unqualified)]
		public string Location;

		/// <summary>Sub element containing a summary of the users photo information.</summary>
		/// <remarks/>
		[XmlElement("photos", Form=XmlSchemaForm.Unqualified)]
		public PersonPhotosSummary PhotosSummary;

		/// <summary>
		/// The users photo location on Flickr
		/// http://www.flickr.com/photos/username/
		/// </summary>
		[XmlElement("photosurl",Form=XmlSchemaForm.Unqualified)]
		public string PhotosUrl;

		/// <summary>
		/// The users profile location on Flickr
		/// http://www.flickr.com/people/username/
		/// </summary>
		[XmlElement("profileurl",Form=XmlSchemaForm.Unqualified)]
		public string ProfileUrl;

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
			get { return Utils.UnixTimestampToDate(firsttakendate_raw); }
		}

		/// <summary>The total number of photos for the user.</summary>
		/// <remarks/>
		[XmlElement("count", Form=XmlSchemaForm.Unqualified)]
		public int PhotoCount;

		/// <remarks>The unix timestamp of the date the first photo was uploaded.</remarks>
		[XmlElement("firstdate", Form=XmlSchemaForm.Unqualified)]
		public string firstdate_raw;

		/// <remarks>The unix timestamp of the date the first photo was uploaded.</remarks>
		[XmlElement("firsttakendate", Form=XmlSchemaForm.Unqualified)]
		public string firsttakendate_raw;

	}
}
