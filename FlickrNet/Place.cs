using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// 
	/// </summary>
	public class Places : IXmlSerializable
	{
		private Place[] _places = new Place[0];

		/// <summary>
		/// 
		/// </summary>
		public Place[] PlacesCollection { get { return _places; } set { _places = value; } }

		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			int count = int.Parse(reader.GetAttribute("total"));
			if( count == 0 ) return;

			_places = new Place[count];

			reader.Read();

			for(int i = 0; i < count; i++)
			{
				_places[i] = new Place();
				IXmlSerializable ser = (IXmlSerializable)_places[i];
				ser.ReadXml(reader);
			}

			reader.Read();
		}
	}
	/// <summary>
	/// Summary description for Place.
	/// </summary>
	public class Place : IXmlSerializable
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

		private string _description;

		/// <summary>
		/// The description of this place, where provided.
		/// </summary>
		public string Description { get { return _description; } }

		private decimal _latitude;
		private decimal _longitude;

		/// <summary>
		/// The latitude of this place.
		/// </summary>
		public decimal Latitude { get { return _latitude; } }

		/// <summary>
		/// The longitude of this place.
		/// </summary>
		public decimal Longitude { get { return _longitude; } }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Place()
		{
		}

		internal Place(System.Xml.XmlReader reader)
		{
			IXmlSerializable x = (IXmlSerializable)this;
			x.ReadXml(reader);
		}
		/// <summary>
		/// Not Implemented
		/// </summary>
		/// <exception cref="NotImplementedException"/>
		/// <param name="writer"></param>
		void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Always returns null.
		/// </summary>
		/// <returns></returns>
		System.Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Serializes the XML to an instance.
		/// </summary>
		/// <param name="reader"></param>
		void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
		{
			_description = reader.GetAttribute("name");
			_placeId = reader.GetAttribute("place_id");
			_placeUrl = reader.GetAttribute("place_url");
			_placeType = reader.GetAttribute("place_type");
			_woeId = reader.GetAttribute("woeid");
			string dec = reader.GetAttribute("latitude");
			if( dec != null && dec.Length > 0 )
			{
				_latitude = decimal.Parse(dec, System.Globalization.NumberFormatInfo.InvariantInfo);
			}
			dec = reader.GetAttribute("longitude");
			if( dec != null && dec.Length > 0 )
			{
				_longitude = decimal.Parse(dec, System.Globalization.NumberFormatInfo.InvariantInfo);
			}

			reader.Read();
		}
	}
}
