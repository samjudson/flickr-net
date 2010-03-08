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
        /// <summary>
        /// The total number of places that match the calling request.
        /// </summary>
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
		/// <summary>
		/// The unique id for this place.
		/// </summary>
        public string PlaceId { get; set; }

		/// <summary>
		/// The web page URL that corresponds to this place.
		/// </summary>
        public string PlaceUrl { get; set; }

		/// <summary>
		/// The 'type' of this place, e.g. Region, Country etc.
		/// </summary>
        public PlaceType PlaceType { get; set; }

		/// <summary>
		/// The WOE id for the locality.
		/// </summary>
        public string WoeId { get; set; }

		/// <summary>
		/// The description of this place, where provided.
		/// </summary>
        public string Description { get; set; }

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
			Description = reader.GetAttribute("name");
			PlaceId = reader.GetAttribute("place_id");
			PlaceUrl = reader.GetAttribute("place_url");
			PlaceType = (PlaceType)Enum.Parse(typeof(PlaceType), reader.GetAttribute("place_type"), true);
			WoeId = reader.GetAttribute("woeid");
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
                Description = reader.Value;
                reader.Read();
            }

			reader.Skip();
		}
	}
}
