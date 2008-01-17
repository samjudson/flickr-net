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
		private double _latitude;
		private double _longitude;
		private GeoAccuracy _accuracy;
		private string _placeId;
		private PlaceInformation _locality;
		private PlaceInformation _county;
		private PlaceInformation _region;
		private PlaceInformation _country;

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
		public double Latitude
		{
			get { return _latitude; } set { _latitude = value; }
		}

		/// <summary>
		/// The longitude of the photo.
		/// </summary>
		[XmlAttribute("longitude", Form=XmlSchemaForm.Unqualified)]
		public double Longitude
		{
			get { return _longitude; } set { _longitude = value; }
		}

		/// <summary>
		/// The accuracy of the location information. See <see cref="GeoAccuracy"/> for accuracy levels.
		/// </summary>
		[XmlAttribute("accuracy", Form=XmlSchemaForm.Unqualified)]
		public GeoAccuracy Accuracy		
		{
			get { return _accuracy; } set { _accuracy = value; }
		}

		/// <summary>
		/// The Place ID of the geolocation of this photo.
		/// </summary>
		[XmlAttribute("place_id")]
		public string PlaceId
		{
			get { return _placeId; } set { _placeId = value; }
		}

		/// <summary>
		/// The locality for the geolocation for this photo. May be null.
		/// </summary>
		[XmlElementAttribute("locality")]
		public PlaceInformation Locality
		{
			get { return _locality; } set { _locality = value; }
		}

		/// <summary>
		/// The county for the geolocation for this photo. May be null.
		/// </summary>
		[XmlElementAttribute("county")]
		public PlaceInformation County
		{
			get { return _county; } set { _county = value; }
		}

		/// <summary>
		/// The region for the geolocation for this photo. May be null.
		/// </summary>
		[XmlElementAttribute("region")]
		public PlaceInformation Region
		{
			get { return _region; } set { _region = value; }
		}

		/// <summary>
		/// The country for the geolocation for this photo. May be null.
		/// </summary>
		[XmlElementAttribute("country")]
		public PlaceInformation Country
		{
			get { return _country; } set { _country = value; }
		}

	}

	/// <summary>
	/// Contains information about the 'Place' data for a geotagged photo.
	/// </summary>
	/// <remarks>
	/// This data contains its 'place id' as well as names and place ids for its containing regions (county, region, country etc).
	/// </remarks>
	public class PlaceInformation
	{
		private string _placeId;
		private string _placeName;

		/// <summary>
		/// The ID for this particular place.
		/// </summary>
		[XmlAttribute("place_id")]
		public string PlaceId
		{
			get { return _placeId; } set { _placeId = value; }
		}

		/// <summary>
		/// The name of this particular place.
		/// </summary>
		[XmlText()]
		public string PlaceName
		{
			get { return _placeName; } set { _placeName = value; }
		}
	}
}
