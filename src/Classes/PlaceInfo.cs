using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Detailed information about a place. Returned by <see cref="Flickr.PlacesGetInfo"/>.
    /// </summary>
    public sealed class PlaceInfo : IFlickrParsable
    {
        /// <summary>
        /// The unique id for this place.
        /// </summary>
        public string PlaceId { get; set; }

        /// <summary>
        /// The web page URL that corresponds to this place.
        /// </summary>
        /// <remarks>
        /// The 'URL' returned is only a sudo url such as '/Canada/Quebec/Montreal'.
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings",
            Justification = "Although called 'URL' by the Flickr API it is not an actual URI.")]
        public string PlaceUrl { get; set; }

        /// <summary>
        /// The URL to the place web page on Flickr.
        /// </summary>
        public string PlaceFlickrUrl
        {
            get { return "https://www.flickr.com/places" + PlaceUrl; }
        }

        /// <summary>
        /// The 'type' of this place, e.g. Region, Country etc.
        /// </summary>
        public PlaceType PlaceType { get; set; }

        /// <summary>
        /// The WOE id for the locality.
        /// </summary>
        public string WoeId { get; set; }

        /// <summary>
        /// The name for the WOE id.
        /// </summary>
        public string WoeName { get; set; }

        /// <summary>
        /// The description of this place, where provided.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The latitude of this place.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of this place.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The accuracy of the location information, if this information is about a photo.
        /// </summary>
        public GeoAccuracy? Accuracy { get; set; }

        /// <summary>
        /// The context of the location, if this information is about a photo.
        /// </summary>
        public GeoContext? Context { get; set; }

        /// <summary>
        /// The timezone for the place.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// Does this place have shape data for it.
        /// </summary>
        public bool HasShapeData { get; set; }

        /// <summary>
        /// The neighbourhood for this location. May be null.
        /// </summary>
        public Place Neighbourhood { get; set; }

        /// <summary>
        /// Details about the place's locality. May be null.
        /// </summary>
        public Place Locality { get; set; }

        /// <summary>
        /// Details of the place's county. May be null.
        /// </summary>
        public Place County { get; set; }

        /// <summary>
        /// Details of the place's region. May be null.
        /// </summary>
        public Place Region { get; set; }

        /// <summary>
        /// Details of the place's country. May be null.
        /// </summary>
        public Place Country { get; set; }

        /// <summary>
        /// The shape data for this place. Only available for some places (see <see cref="HasShapeData"/>).
        /// </summary>
        public ShapeData ShapeData { get; set; }

        #region IFlickrParsable Members

        /// <summary>
        /// Serializes the XML to an instance.
        /// </summary>
        /// <param name="reader"></param>
        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "photo") reader.ReadToDescendant("location");

            LoadAttributes(reader);

            LoadElements(reader);
        }

        #endregion

        private void LoadElements(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "photo") return;
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "geoperms") return;

            while (reader.NodeType != XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "neighbourhood":
                        Neighbourhood = new Place();
                        ((IFlickrParsable)Neighbourhood).Load(reader);
                        break;
                    case "locality":
                        Locality = new Place();
                        ((IFlickrParsable)Locality).Load(reader);
                        break;
                    case "county":
                        County = new Place();
                        ((IFlickrParsable)County).Load(reader);
                        break;
                    case "region":
                        Region = new Place();
                        ((IFlickrParsable)Region).Load(reader);
                        break;
                    case "country":
                        Country = new Place();
                        ((IFlickrParsable)Country).Load(reader);
                        break;
                    case "shapedata":
                        ShapeData = new ShapeData();
                        ((IFlickrParsable)ShapeData).Load(reader);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;
                }
            }

            reader.Read();
        }

        private void LoadAttributes(XmlReader reader)
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
                    case "woe_name":
                        WoeName = reader.Value;
                        break;
                    case "latitude":
                        Latitude = reader.ReadContentAsDouble();
                        break;
                    case "longitude":
                        Longitude = reader.ReadContentAsDouble();
                        break;
                    case "accuracy":
                        Accuracy = (GeoAccuracy)reader.ReadContentAsInt();
                        break;
                    case "context":
                        Context = (GeoContext)reader.ReadContentAsInt();
                        break;
                    case "timezone":
                        TimeZone = reader.Value;
                        break;
                    case "has_shapedata":
                        HasShapeData = reader.Value == "1";
                        break;
                    case "id":
                        // Ignore the Photo ID
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }
    }
}