using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Collection of <see cref="Size"/> items for a given photograph.
	/// </summary>
	[System.Serializable]
	public class Sizes
	{
		private Size[] _sizeCollection = new Size[0];

		/// <summary>
		/// The size collection contains an array of <see cref="Size"/> items.
		/// </summary>
		[XmlElement("size", Form=XmlSchemaForm.Unqualified)]
		public Size[] SizeCollection
		{
			get { return _sizeCollection; }
			set { _sizeCollection = value; }
		}
	}

	/// <summary>
	/// Contains details about all the sizes available for a given photograph.
	/// </summary>
	[System.Serializable]
	public class Size
	{
		private string _label;
		private int _width;
		private int _height;
		private string _source;
		private string _url;

		/// <summary>
		/// The label for the size, such as "Thumbnail", "Small", "Medium", "Large" and "Original".
		/// </summary>
		[XmlAttribute("label", Form=XmlSchemaForm.Unqualified)]
		public string Label
		{
			get { return _label; }
			set { _label = value; }
		}
    
        /// <summary>
        /// The width of the resulting image, in pixels
        /// </summary>
		[XmlAttribute("width", Form=XmlSchemaForm.Unqualified)]
		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}
    
		/// <summary>
		/// The height of the resulting image, in pixels
		/// </summary>
		[XmlAttribute("height", Form=XmlSchemaForm.Unqualified)]
		public int Height
		{
			get { return _height; }
			set { _height = value; }
		}
    
		/// <summary>
		/// The source url of the image.
		/// </summary>
		[XmlAttribute("source", Form=XmlSchemaForm.Unqualified)]
		public string Source
		{
			get { return _source; }
			set { _source = value; }
		}
    
		/// <summary>
		/// The url to the photographs web page for this particular size.
		/// </summary>
		[XmlAttribute("url", Form=XmlSchemaForm.Unqualified)]
		public string Url
		{
			get { return _url; }
			set { _url = value; }
		}
	}
}