using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;
using System;
using System.Collections.Generic;

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
			set 
			{
				if( value== null ) 
					_photosetCollection = new Photoset[0];
				else
					_photosetCollection = value;
			}
		}
	}

	/// <summary>
	/// A set of properties for the photoset.
	/// </summary>
	[System.Serializable]
	public class Photoset : List<Photo>, IXmlSerializable
	{
		private string _photosetId;
		private string _url;
		private string _ownerId;
		private string _primaryPhotoId;
		private string _secret;
		private string _server;
		private string _farm;
		private string _title;
		private string _description;

		/// <summary>
		/// The ID of the photoset.
		/// </summary>
		public string PhotosetId
		{
			get { return _photosetId; } set { _photosetId = value; }
		}

		/// <summary>
		/// The URL of the photoset.
		/// </summary>
		public string Url
		{
            get
            {
                if (String.IsNullOrEmpty(_url)) _url = String.Format("http://www.flickr.com/photos/{0}/sets/{1}/", _ownerId, _photosetId);
                return _url;
            }
            set { _url = value; }
		}

		/// <summary>
		/// The ID of the owner of the photoset.
		/// </summary>
		public string OwnerId
		{
			get { return _ownerId; } set { _ownerId = value; }
		}

		/// <summary>
		/// The photo ID of the primary photo of the photoset.
		/// </summary>
		public string PrimaryPhotoId
		{
			get { return _primaryPhotoId; } set { _primaryPhotoId = value; }
		}

		/// <summary>
		/// The secret for the primary photo for the photoset.
		/// </summary>
		public string Secret
		{
			get { return _secret; } set { _secret = value; }
		}

		/// <summary>
		/// The server for the primary photo for the photoset.
		/// </summary>
		public string Server
		{
			get { return _server; } set { _server = value; }
		}

		/// <summary>
		/// The server farm for the primary photo for the photoset.
		/// </summary>
		public string Farm
		{
			get { return _farm; } set { _farm = value; }
		}

		/// <summary>
		/// The number of photos in the photoset.
		/// </summary>
		public int NumberOfPhotos
		{
			get { return _numPhotos; }
			set { _numPhotos = value; }
		}

		/// <summary>
		/// The title of the photoset.
		/// </summary>
		public string Title
		{
			get { return _title; } set { _title = value; }
		}

		/// <summary>
		/// The description of the photoset.
		/// </summary>
		public string Description
		{
			get { return _description; } set { _description = value; }
		}

		/// <summary>
		/// An array of photo objects in the photoset.
		/// </summary>
		public Photo[] PhotoCollection
		{
			get { return this.ToArray(); } 
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

		#region IXmlSerializable Members

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			
			return null;
		}

		private int _numPhotos;

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
            _photosetId = reader.GetAttribute("id");
            _url = reader.GetAttribute("url");
            _ownerId = reader.GetAttribute("owner_id");
            if (String.IsNullOrEmpty(_ownerId)) _ownerId = reader.GetAttribute("owner");
            _primaryPhotoId = reader.GetAttribute("primary");
            _secret = reader.GetAttribute("secret");
            _server = reader.GetAttribute("server");
            _farm = reader.GetAttribute("farm");

            if (reader.GetAttribute("photos") != null) _numPhotos = int.Parse(reader.GetAttribute("photos"), System.Globalization.CultureInfo.InvariantCulture);
            if (reader.GetAttribute("total") != null) _numPhotos = int.Parse(reader.GetAttribute("total"), System.Globalization.CultureInfo.InvariantCulture);

			XmlSerializer ser = new XmlSerializer(typeof(Photo));

			if( !reader.IsEmptyElement )
			{
                reader.Read();

                while(true)
                {
                    if (reader.Name == "photo")
                    {
                        StringReader r = new StringReader(reader.ReadOuterXml());
                        try
                        {
                            Photo p = (Photo)ser.Deserialize(r);
                            Add(p);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                        continue;
                    }
                    if (reader.Name == "title")
                    {
                        _title = reader.ReadInnerXml();
                        continue;
                    }
                    if (reader.Name == "description")
                    {
                        _description = reader.ReadInnerXml();
                        continue;
                    }
                    if (reader.NodeType == System.Xml.XmlNodeType.EndElement && reader.Name == "photoset")
                    {
                        reader.Read();
                    }
                    break;
                }

			}
		}

		#endregion
	}
}