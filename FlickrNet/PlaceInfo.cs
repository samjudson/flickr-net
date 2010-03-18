using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Detailed information about a place. Returned by <see cref="Flickr.PlacesGetInfo"/>.
    /// </summary>
    public class PlaceInfo : IFlickrParsable
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
        /// Does this place have shape data for it.
        /// </summary>
        public bool HasShapeData { get; private set; }

        /// <summary>
        /// Details about the place's locality. May be null.
        /// </summary>
        public Place Locality { get; private set; }

        /// <summary>
        /// Details of the place's county. May be null.
        /// </summary>
        public Place County { get; private set; }

        /// <summary>
        /// Details of the place's region. May be null.
        /// </summary>
        public Place Region { get; private set; }

        /// <summary>
        /// Details of the place's country. May be null.
        /// </summary>
        public Place Country { get; private set; }

        /// <summary>
        /// The shape data for this place. Only available for some places (see <see cref="HasShapeData"/>).
        /// </summary>
        public ShapeData ShapeData { get; private set; }

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
                    case "has_shapedata":
                        HasShapeData = reader.Value == "1";
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "locality":
                        Locality = new Place();
                        ((IFlickrParsable)Locality).Load(reader);
                        break;
                    case "county":
                        County = new Place();
                        ((IFlickrParsable)Locality).Load(reader);
                        break;
                    case "region":
                        Region = new Place();
                        ((IFlickrParsable)Locality).Load(reader);
                        break;
                    case "country":
                        Country = new Place();
                        ((IFlickrParsable)Locality).Load(reader);
                        break;
                    case "shapedata":
                        ShapeData = new ShapeData();
                        ((IFlickrParsable)ShapeData).Load(reader);
                        break;
                    default:
                        throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");
                }
            }

			reader.Read();
		}
    }
}
