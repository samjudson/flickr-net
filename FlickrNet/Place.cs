using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for Place.
	/// </summary>
    public class Place : IFlickrParsable
	{
		/// <summary>
		/// The unique id for this place.
		/// </summary>
        public string PlaceId { get; private set; }

		/// <summary>
		/// The web page URL that corresponds to this place.
		/// </summary>
        public string PlaceUrl { get; private set; }

		/// <summary>
		/// The 'type' of this place, e.g. Region, Country etc.
		/// </summary>
        public PlaceType PlaceType { get; private set; }

		/// <summary>
		/// The WOE id for the locality.
		/// </summary>
        public string WoeId { get; private set; }

		/// <summary>
		/// The description of this place, where provided.
		/// </summary>
        public string Description { get; private set; }

		/// <summary>
		/// The latitude of this place.
		/// </summary>
        public decimal Latitude { get; private set; }

		/// <summary>
		/// The longitude of this place.
		/// </summary>
        public decimal Longitude { get; private set; }

        /// <summary>
        /// The timezone for the place.
        /// </summary>
        public string TimeZone { get; private set; }

        /// <summary>
        /// The number of photos the calling user has for this place.
        /// </summary>
        /// <remarks>
        /// Only returned for <see cref="Flickr.PlacesPlacesForUser()"/>.
        /// </remarks>
        public int? PhotoCount { get; private set; }

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
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "name":
                        Description = reader.Value;
                        break;
                    case "place_id":
                        PlaceId = reader.Value;
                        break;
                    case "place_url":
                        PlaceUrl = reader.Value;
                        break;
                    case "place_type_id":
                        PlaceType = (PlaceType)reader.ReadContentAsInt();
                        break;
                    case "place_type":
                        PlaceType = (PlaceType)Enum.Parse(typeof(PlaceType), reader.Value, true);
                        break;
                    case "woeid":
                        WoeId = reader.Value;
                        break;
                    case "latitude":
                        Latitude = reader.ReadContentAsDecimal();
                        break;
                    case "longitude":
                        Longitude = reader.ReadContentAsDecimal();
                        break;
                    case "timezone":
                        TimeZone = reader.Value;
                        break;
                    case "photo_count":
                        PhotoCount = reader.ReadContentAsInt();
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    Description = reader.ReadContentAsString();
                }
                else
                {
                    switch (reader.LocalName)
                    {
                        default:
                            throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");
                    }
                }
            }

			reader.Read();
		}
	}
}
