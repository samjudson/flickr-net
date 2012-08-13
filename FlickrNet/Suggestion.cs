using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A suggestion for the correct location for a particular photo.
    /// </summary>
    /// <remarks>
    /// There is currently no UI support for this feature in Flickr.
    /// </remarks>
    public sealed class Suggestion : IFlickrParsable
    {
        /// <summary>
        /// The id for the suggestion.
        /// </summary>
        public string SuggestionId { get; set; }
        /// <summary>
        /// The id for the photo this suggestion applies to.
        /// </summary>
        public string PhotoId { get; set; }
        /// <summary>
        /// The date the location was suggested.
        /// </summary>
        public DateTime DateSuggested { get; set; }
        /// <summary>
        /// The user id of the user who suggested the location.
        /// </summary>
        public string SuggestedByUserId { get; set; }
        /// <summary>
        /// The name of the user who suggested the location.
        /// </summary>
        public string SuggestedByUserName { get; set; }
        /// <summary>
        /// The note (if any) that the user added to the suggestion.
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// The WOE id for the location, if any.
        /// </summary>
        public string WoeId { get; set; }
        /// <summary>
        /// The place id for the location, if any.
        /// </summary>
        public string PlaceId { get; set; }
        /// <summary>
        /// The latitude of the location suggestion.
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// The longitude of the location suggestion.
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// The geo accuracy of the location suggestion.
        /// </summary>
        public GeoAccuracy Accuracy { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader == null) throw new ArgumentNullException("reader");
            if (reader.LocalName != "suggestion") { UtilityMethods.CheckParsingException(reader); return; }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        SuggestionId = reader.Value;
                        break;
                    case "photo_id":
                        PhotoId = reader.Value;
                        break;
                    case "date_suggested":
                        DateSuggested = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName != "suggestion" && reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                switch (reader.LocalName)
                {
                    case "suggested_by":
                        SuggestedByUserId = reader.GetAttribute("nsid");
                        SuggestedByUserName = reader.GetAttribute("username");
                        reader.Skip();
                        break;
                    case "note":
                        Note = reader.ReadElementContentAsString();
                        break;
                    case "location":
                        while (reader.MoveToNextAttribute())
                        {
                            switch (reader.LocalName)
                            {
                                case "woeid":
                                    WoeId = reader.Value;
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
                                default:
                                    UtilityMethods.CheckParsingException(reader);
                                    break;
                            }
                        }
                        reader.Skip();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }


            
        }
    }
}
