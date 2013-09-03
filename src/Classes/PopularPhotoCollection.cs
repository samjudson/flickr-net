using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of popular photos as returned by <see cref="Flickr.StatsGetPopularPhotos(DateTime, PopularitySort, int, int)"/>
    /// </summary>
    public sealed class PopularPhotoCollection : Collection<PopularPhoto>, IFlickrParsable
    {
        /// <summary>
        /// The page of the results returned. Will be 1 even if no results are returned.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The number of pages of photos returned. Will likely be 1 if no photos are returned.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The number of photos returned per page.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The total number of photos that match the query. Call the method again to retrieve each page of results if Total > PerPage.
        /// </summary>
        public int Total { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "photos")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "perpage":
                        PerPage = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                var photo = new PopularPhoto();
                ((IFlickrParsable) photo).Load(reader);
                Add(photo);
            }

            reader.Skip();
        }

        #endregion
    }
}