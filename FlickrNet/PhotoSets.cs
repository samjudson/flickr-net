using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Collection containing a users photosets.
	/// </summary>
	[System.Serializable]
	public class Photosets
	{
		private int _canCreate;
		private Photoset[] _photosetCollection = new Photoset[0];

		/// <summary>
		/// Can the user create more photosets.
		/// </summary>
		/// <remarks>
		/// 1 meants yes, 0 means no.
		/// </remarks>
		[XmlAttribute("cancreate", Form=XmlSchemaForm.Unqualified)]
		public int CanCreate
		{
			get { return _canCreate; }
			set { _canCreate = value; }
		}

		/// <summary>
		/// An array of <see cref="Photoset"/> objects.
		/// </summary>
		[XmlElement("photoset", Form=XmlSchemaForm.Unqualified)]
		public Photoset[] PhotosetCollection
		{
			get { return _photosetCollection; }
			set { _photosetCollection = value;}
		}
	}

	/// <summary>
	/// A set of properties for the photoset.
	/// </summary>
	[System.Serializable]
	public class Photoset
	{
		private string _photosetId;
		private string _url;
		private string _ownerId;
		private string _primaryPhotoId;
		private string _secret;
		private string _server;
		private string _farm;
		private int _numberOfPhotos;
		private string _title;
		private string _description;
		private Photo[] _photoCollection = new Photo[0];

		/// <summary>
		/// The ID of the photoset.
		/// </summary>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string PhotosetId
		{
			get { return _photosetId; } set { _photosetId = value; }
		}

		/// <summary>
		/// The URL of the photoset.
		/// </summary>
		[XmlAttribute("url", Form=XmlSchemaForm.Unqualified)]
		public string Url
		{
			get { return _url; } set { _url = value; }
		}

		/// <summary>
		/// The ID of the owner of the photoset.
		/// </summary>
		[XmlAttribute("owner", Form=XmlSchemaForm.Unqualified)]
		public string OwnerId
		{
			get { return _ownerId; } set { _ownerId = value; }
		}

		/// <summary>
		/// The photo ID of the primary photo of the photoset.
		/// </summary>
		[XmlAttribute("primary", Form=XmlSchemaForm.Unqualified)]
		public string PrimaryPhotoId
		{
			get { return _primaryPhotoId; } set { _primaryPhotoId = value; }
		}

		/// <summary>
		/// The secret for the primary photo for the photoset.
		/// </summary>
		[XmlAttribute("secret", Form=XmlSchemaForm.Unqualified)]
		public string Secret
		{
			get { return _secret; } set { _secret = value; }
		}

		/// <summary>
		/// The server for the primary photo for the photoset.
		/// </summary>
		[XmlAttribute("server", Form=XmlSchemaForm.Unqualified)]
		public string Server
		{
			get { return _server; } set { _server = value; }
		}

		/// <summary>
		/// The server farm for the primary photo for the photoset.
		/// </summary>
		[XmlAttribute("farm", Form=XmlSchemaForm.Unqualified)]
		public string Farm
		{
			get { return _farm; } set { _farm = value; }
		}

		/// <summary>
		/// The number of photos in the photoset.
		/// </summary>
		[XmlAttribute("photos", Form=XmlSchemaForm.Unqualified)]
		public int NumberOfPhotos
		{
			get { return _numberOfPhotos; } set { _numberOfPhotos = value; }
		}

		/// <summary>
		/// The title of the photoset.
		/// </summary>
		[XmlElement("title", Form=XmlSchemaForm.Unqualified)]
		public string Title
		{
			get { return _title; } set { _title = value; }
		}

		/// <summary>
		/// The description of the photoset.
		/// </summary>
		[XmlElement("description", Form=XmlSchemaForm.Unqualified)]
		public string Description
		{
			get { return _description; } set { _description = value; }
		}

		/// <summary>
		/// An array of photo objects in the photoset.
		/// </summary>
		[XmlElement("photo", Form=XmlSchemaForm.Unqualified)]
		public Photo[] PhotoCollection
		{
			get { return _photoCollection; } set { _photoCollection = value; }
		}

		/// <summary>
		/// The URL for the thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string PhotosetThumbnailUrl
		{
			get { return Utils.UrlFormat(this, "_t", "jpg"); }
		}

		/// <summary>
		/// The URL for the square thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string PhotosetSquareThumbnailUrl
		{
			get { return Utils.UrlFormat(this, "_s", "jpg"); }
		}

		/// <summary>
		/// The URL for the small copy of a photo.
		/// </summary>
		[XmlIgnore()]
		public string PhotosetSmallUrl
		{
			get { return Utils.UrlFormat(this, "_m", "jpg"); }
		}




	}
}