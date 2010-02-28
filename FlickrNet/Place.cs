using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// 
	/// </summary>
	public class Places : List<Place>, IFlickrParsable
	{
        public int Total { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Place[] PlacesCollection { get { return this.ToArray(); } }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
		{
            int Total = int.Parse(reader.GetAttribute("total"), System.Globalization.CultureInfo.InvariantCulture);

			reader.Read();

            while (reader.LocalName == "place")
            {
                Place p = new Place();
                ((IFlickrParsable)p).Load(reader);
                Add(p);
            }

            reader.Skip();
		}
	}
	/// <summary>
	/// Summary description for Place.
	/// </summary>
    public class Place : IFlickrParsable
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

		private PlaceType _placeType;
		/// <summary>
		/// The 'type' of this place, e.g. Region, Country etc.
		/// </summary>
        public PlaceType PlaceType { get { return _placeType; } }

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

        internal Place(XmlReader reader)
        {
            ((IFlickrParsable)this).Load(reader);
        }

		/// <summary>
		/// Serializes the XML to an instance.
		/// </summary>
		/// <param name="reader"></param>
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
		{
			_description = reader.GetAttribute("name");
			_placeId = reader.GetAttribute("place_id");
			_placeUrl = reader.GetAttribute("place_url");
			_placeType = (PlaceType)Enum.Parse(typeof(PlaceType), reader.GetAttribute("place_type"), true);
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

            if (!reader.IsEmptyElement)
            {
                reader.Read();
                _description = reader.Value;
                reader.Read();
            }

			reader.Skip();
		}
	}
}
