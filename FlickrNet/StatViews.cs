using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The number of views of each type that a users account has had for a given date (or overall if no date specified).
    /// </summary>
    /// <remarks>
    /// Used by <see cref="Flickr.StatsGetTotalViews(DateTime)"/>.
    /// </remarks>
    public class StatViews : IFlickrParsable
    {
        /// <summary>
        /// The total number of views for this account.
        /// </summary>
        public int TotalViews { get; set; }
        /// <summary>
        /// The number of photostream views.
        /// </summary>
        public int PhotostreamViews { get; set; }
        /// <summary>
        /// The number of individual photo views.
        /// </summary>
        public int PhotoViews { get; set; }
        /// <summary>
        /// The number of photoset views.
        /// </summary>
        public int PhotosetViews { get; set; }
        /// <summary>
        /// The number of collection views.
        /// </summary>
        public int CollectionViews { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "stats")
            {
                throw new ParsingException(String.Format("Unknown element '{0}' found in Flickr response.", reader.LocalName));
            }

            while (reader.Read() && reader.LocalName != "stats")
            {
                switch (reader.LocalName)
                {
                    case "total":
                        TotalViews = int.Parse(reader.GetAttribute("views"), System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "photos":
                        PhotoViews = int.Parse(reader.GetAttribute("views"), System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "photostream":
                        PhotostreamViews = int.Parse(reader.GetAttribute("views"), System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "sets":
                        PhotosetViews = int.Parse(reader.GetAttribute("views"), System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "collections":
                        CollectionViews = int.Parse(reader.GetAttribute("views"), System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new ParsingException(String.Format("Unknown element '{0}' found in Flickr response.", reader.LocalName));
                }
            }

            reader.Skip();

        }
    }
}
