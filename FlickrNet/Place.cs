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
    public sealed class Place : IFlickrParsable
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "Although called 'URL' by the Flickr API it is not an actual URI.")]
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

        /// <summary>
        /// The latitude of this place.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The longitude of this place.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The timezone for the place.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// The number of photos the calling user has for this place.
        /// </summary>
        /// <remarks>
        /// Only returned for <see cref="Flickr.PlacesPlacesForUser()"/>.
        /// </remarks>
        public int? PhotoCount { get; set; }

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
                        Latitude = reader.ReadContentAsDouble();
                        break;
                    case "longitude":
                        Longitude = reader.ReadContentAsDouble();
                        break;
                    case "timezone":
                        TimeZone = reader.Value;
                        break;
                    case "photo_count":
                        PhotoCount = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
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
                            UtilityMethods.CheckParsingException(reader);
                            break;
                    }
                }
            }

            reader.Read();
        }
    }
}
