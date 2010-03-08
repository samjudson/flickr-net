using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A collection of photos returned by the <see cref="Flickr.PandaGetPhotos(string)"/> methods.
    /// </summary>
    public class PandaPhotos: List<Photo>, IFlickrParsable
    {
        /// <summary>
        /// The number of seconds the application developer should wait before calling this panda again.
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// When the list of photos from this panda was last updated.
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// The total number of photos returned.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The pands that returned this set of photos.
        /// </summary>
        public string PandaName { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PandaPhotos()
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
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "interval":
                        Interval = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "panda":
                        PandaName = reader.Value;
                        break;
                    case "lastupdate":
                        LastUpdated = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo p = new Photo();
                ((IFlickrParsable)p).Load(reader);
                if (!String.IsNullOrEmpty(p.PhotoId)) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();
            
        }
    }
}
