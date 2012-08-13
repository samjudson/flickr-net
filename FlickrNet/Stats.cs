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
                UtilityMethods.CheckParsingException(reader);
            }

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "views":
                        if (String.IsNullOrEmpty(reader.Value))
                            Views = 0;
                        else
                            Views = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "comments":
                        if (String.IsNullOrEmpty(reader.Value))
                            Comments = 0;
                        else
                            Comments = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "favorites":
                        if (String.IsNullOrEmpty(reader.Value))
                            Favorites = 0;
                        else
                            Favorites = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Skip();
        }
    }
}
