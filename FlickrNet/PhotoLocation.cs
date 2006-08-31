using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for PhotoLocation.
	/// </summary>
	public class PhotoLocation
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public PhotoLocation()
		{
		}

		/// <summary>
		/// The latitude of the photo.
		/// </summary>
		[XmlAttribute("latitude", Form=XmlSchemaForm.Unqualified)]
		public double Latitude;

		/// <summary>
		/// The longitude of the photo.
		/// </summary>
		[XmlAttribute("longitude", Form=XmlSchemaForm.Unqualified)]
		public double Longitude;

		/// <summary>
		/// The accuracy of the location information. See <see cref="GeoAccuracy"/> for accuracy levels.
		/// </summary>
		[XmlAttribute("accuracy", Form=XmlSchemaForm.Unqualified)]
		public GeoAccuracy Accuracy;
	}
}
