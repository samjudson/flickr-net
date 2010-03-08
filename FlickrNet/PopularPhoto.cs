using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Details about a popular photo, including the statistics for its views, comments and favourites for the date.
    /// </summary>
    public class PopularPhoto : Photo, IFlickrParsable
    {
        /// <summary>
        /// The number of views this photo has received in the context of the calling time period.
        /// </summary>
        public int StatViews { get; set; }
        /// <summary>
        /// The number of comments this photo has received in the context of the calling time period.
        /// </summary>
        public int StatComments { get; set; }
        /// <summary>
        /// The number of favorites this photo has received in the context of the calling time period.
        /// </summary>
        public int StatFavorites { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            DoLoad(reader);

            reader.Read();

            if (reader.LocalName != "stats")
            {
                throw new System.Xml.XmlException("Unknown element name '" + reader.LocalName + "' found in Flickr response");
            }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "views":
                        StatViews = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "comments":
                        StatComments = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "favorites":
                        StatFavorites = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new Exception("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Skip();
            reader.Skip();
        }
    }
}
