using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FlickrNet
{
	/// <summary>
	/// EXIF data for the selected photo.
	/// </summary>
	[Serializable]
	public class ExifPhoto
	{
		internal ExifPhoto(string photoId, string secret, int server, ExifTag[] array)
		{
			_photoId = photoId;
			_secret = secret;
			_server = server;
			_tagCollection = array;
		}

		private string _photoId;
		private string _secret;
		private int _server;
		private ExifTag[] _tagCollection;

		/// <summary>
		/// The Photo ID for the photo whose EXIF data this is.
		/// </summary>
		public string PhotoId
		{
			get { return _photoId; }
		}

		/// <summary>
		/// The Secret of the photo.
		/// </summary>
		public string Secret
		{
			get { return _secret; }
		}

		/// <summary>
		/// The server number for the photo.
		/// </summary>
		public int Server
		{
			get { return _server; }
		}

		/// <summary>
		/// An array of EXIF tags. See <see cref="ExifTag"/> for more details.
		/// </summary>
		public ExifTag[] ExifTagCollection
		{
			get { return _tagCollection; }
		}
	}

	/// <summary>
	/// Details of an individual EXIF tag.
	/// </summary>
	[Serializable]
	public class ExifTag
	{
		private string _tagSpace;
		private int _tagSpaceId;
		private string _tag;
		private string _label;
		private string _raw;
		private string _clean;

		/// <summary>
		/// The type of EXIF data, e.g. EXIF, TIFF, GPS etc.
		/// </summary>
		[XmlAttribute("tagspace")]
		public string TagSpace
		{
			get { return _tagSpace; }
			set { _tagSpace = value; }
		}

		/// <summary>
		/// An id number for the type of tag space.
		/// </summary>
		[XmlAttribute("tagspaceid")]
		public int TagSpaceId
		{
			get { return _tagSpaceId; }
			set { _tagSpaceId = value; }
		}

		/// <summary>
		/// The tag number.
		/// </summary>
		[XmlAttribute("tag")]
		public string Tag
		{
			get { return _tag; }
			set { _tag = value; }
		}

		/// <summary>
		/// The label, or description for the tag, such as Aperture
		/// or Manufacturer
		/// </summary>
		[XmlAttribute("label")]
		public string Label
		{
			get { return _label; }
			set { _label = value; }
		}

		/// <summary>
		/// The raw EXIF data.
		/// </summary>
		[XmlElement("raw")]
		public string Raw
		{
			get { return _raw; }
			set { _raw = value; }
		}

		/// <summary>
		/// An optional clean version of the <see cref="Raw"/> property.
		/// May be null if the <c>Raw</c> property is in a suitable format already.
		/// </summary>
		[XmlElement("clean")]
		public string Clean
		{
			get { return _clean; }
			set { _clean = value; }
		}
	}
}
