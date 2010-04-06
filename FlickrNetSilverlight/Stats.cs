using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The stats returned by <see cref="Flickr.StatsGetPhotoStats"/>, <see cref="Flickr.StatsGetPhotostreamStats"/>, <see cref="Flickr.StatsGetPhotosetStats"/> and <see cref="Flickr.StatsGetCollectionStats"/>
    /// </summary>
    public sealed class Stats : IFlickrParsable
    {
        /// <summary>
        /// The number of views the object in question has had (either Photostream, Collection, Photo or Photoset).
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// The number of comments the object in question has had (either Photo or Photoset).
        /// </summary>
        public int Comments { get; set; }

        /// <summary>
        /// The number of favorites the object in question has had (Photo only).
        /// </summary>
        public int Favorites { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "stats")
            {
                throw new System.Xml.XmlException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "Unknown element '{0}' found in Flickr Response.", reader.LocalName));
            }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "views":
                        Views = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "comments":
                        Comments = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "favorites":
                        Favorites = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Skip();
        }
    }
}
