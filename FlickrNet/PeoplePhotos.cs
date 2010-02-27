using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A collection of photos returned by the <see cref="Flickr.PandaGetPhotos"/> methods.
    /// </summary>
    public class PeoplePhotos : List<Photo>, IFlickrParsable
    {
        /// <summary>
        /// The number of seconds the application developer should wait before calling this panda again.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// When the list of photos from this panda was last updated.
        /// </summary>
        public bool HasNextPage { get; set; }

        /// <summary>
        /// The total number of photos returned.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PeoplePhotos()
        {
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photos")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "perpage":
                        PerPage = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "has_next_page":
                        HasNextPage = (reader.Value == "1");
                        break;
                    default:
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo p = new Photo(reader);
                if (!String.IsNullOrEmpty(p.PhotoId)) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }
}
