using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for Location.
	/// </summary>
	public class Location : IXmlSerializable
	{
		private string _placeId;
		/// <summary>
		/// The unique id for this place.
		/// </summary>
		public string PlaceId { get { return _placeId; } }

		private string _placeUrl;
		/// <summary>
		/// The web page URL that corresponds to this place.
		/// </summary>
		public string PlaceUrl { get { return _placeUrl; } }

		private string _placeType;
		/// <summary>
		/// The 'type' of this place, e.g. Region, Country etc.
		/// </summary>
		public string PlaceType { get { return _placeType; } }

		private string _woeId;
		/// <summary>
		/// The WOE id for the locality.
		/// </summary>
		public string WoeId { get { return _woeId; } }

		private Place _locality;
		private Place _county;
		private Place _region;
		private Place _country;

		/// <summary>
		/// The Locality of this place.
		/// </summary>
		public Place Locality { get { return _locality; } }

		/// <summary>
		/// The County of this place.
		/// </summary>
		public Place County { get { return _county; } }

		/// <summary>
		/// The region of this place.
		/// </summary>
		public Place Region { get { return _region; } }

		/// <summary>
		/// The country of this place.
		/// </summary>
		public Place Country { get { return _country; } }

		private decimal _longitude;
		private decimal _latitude;

		/// <summary>
		/// The longitude of this place.
		/// </summary>
		public decimal Longitude { get { return _longitude; } }

		/// <summary>
		/// The latitude of this place.
		/// </summary>
		public decimal Latitude { get { return _longitude; } }

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			// TODO:  Add Location.GetSchema implementation
			return null;
		}

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			_placeId = reader.GetAttribute("place_id");
			_placeUrl = reader.GetAttribute("place_url");
			_placeType = reader.GetAttribute("place_type");
			_woeId = reader.GetAttribute("woeid");

			string dec = reader.GetAttribute("latitude");
			if( dec != null && dec.Length > 0 )
			{
				_latitude = decimal.Parse(dec);
			}
			dec = reader.GetAttribute("longitude");
			if( dec != null && dec.Length > 0 )
			{
				_longitude = decimal.Parse(dec);
			}

			reader.Read();

			_locality = new Place(reader);
			_county = new Place(reader);
			_region = new Place(reader);
			_country = new Place(reader);
		}

	}
}
