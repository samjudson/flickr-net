using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

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
        public int Page { get; private set; }

		/// <summary>
		/// The total number of pages this search would return.
		/// </summary>
        public int Pages { get; private set; }

		/// <summary>
		/// The number of groups returned per photo.
		/// </summary>
        public int PerPage { get; private set; }

		/// <summary>
		/// The total number of groups that where returned for the search.
		/// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// The owner of these galleries if called from <see cref="Flickr.GalleriesGetList(string, int, int)"/>.
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// The ID photo that these galleries contain if called from <see cref="Flickr.GalleriesGetListForPhoto(string, int, int)"/>.
        /// </summary>
        public string PhotoId { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "galleries")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "user_id":
                        UserId = reader.Value;
                        break;
                    case "photo_id":
                        PhotoId = reader.Value;
                        break;
                    default:
                        throw new ParsingException("Unknown attribute: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "gallery")
            {
                Gallery r = new Gallery();
                ((IFlickrParsable)r).Load(reader);
                Add(r);
            }

            // Skip to next element (if any)
            reader.Skip();
        }
    }
}
