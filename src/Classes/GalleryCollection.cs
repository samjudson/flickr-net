using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Gallery"/> instances.
    /// </summary>
    public sealed class GalleryCollection : Collection<Gallery>, IFlickrParsable
    {
        /// <summary>
        /// The current page that the group search results represents.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// The total number of pages this search would return.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// The number of groups returned per photo.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The total number of groups that where returned for the search.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The owner of these galleries if called from <see cref="Flickr.GalleriesGetList(string, int, int)"/>.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The ID photo that these galleries contain if called from <see cref="Flickr.GalleriesGetListForPhoto(string, int, int)"/>.
        /// </summary>
        public string PhotoId { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "galleries")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    case "user_id":
                        UserId = reader.Value;
                        break;
                    case "photo_id":
                        PhotoId = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "gallery")
            {
                var r = new Gallery();
                ((IFlickrParsable) r).Load(reader);
                Add(r);
            }

            // Skip to next element (if any)
            reader.Skip();
        }

        #endregion
    }
}