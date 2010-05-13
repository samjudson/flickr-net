using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The referrer details returned by <see cref="Flickr.StatsGetCollectionReferrers(DateTime, string, string, int, int)"/>, <see cref="Flickr.StatsGetPhotoReferrers(DateTime, string, string, int, int)"/>,
    /// <see cref="Flickr.StatsGetPhotosetReferrers(DateTime, string, string, int, int)"/> and <see cref="Flickr.StatsGetPhotostreamReferrers(DateTime, string, int, int)"/>.
    /// </summary>
    public sealed class StatReferrer : IFlickrParsable
    {
        /// <summary>
        /// The url that the referrer referred from.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// The number of times that URL was referred from.
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// Then the referrer is a search engine this will contain the search term used.
        /// </summary>
        public string SearchTerm { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "referrer")
                throw new System.Xml.XmlException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "url":
                        Url = reader.Value;
                        break;
                    case "searchterm":
                        SearchTerm = reader.Value;
                        break;
                    case "views":
                        Views = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Skip();
        }
    }
}
