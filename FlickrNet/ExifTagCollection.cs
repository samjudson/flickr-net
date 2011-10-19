using System;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <summary>
    /// EXIF data for the selected photo.
    /// </summary>
    public sealed class ExifTagCollection : System.Collections.ObjectModel.Collection<ExifTag>, IFlickrParsable
    {
        /// <summary>
        /// The Photo ID for the photo whose EXIF data this is.
        /// </summary>
        public string PhotoId { get; set; }

        /// <summary>
        /// The Secret of the photo.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// The server number for the photo.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The server farm for this photo.
        /// </summary>
        public string Farm { get; set; }

        /// <summary>
        /// The make and model of the camera that took the photo, if available.
        /// </summary>
        public string Camera { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photo")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotoId = reader.Value;
                        break;
                    case "secret":
                        Secret = reader.Value;
                        break;
                    case "server":
                        Server = reader.Value;
                        break;
                    case "farm":
                        Farm = reader.Value;
                        break;
                    case "camera":
                        Camera = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;

                }
            }

            reader.Read();

            while (reader.LocalName == "exif")
            {
                ExifTag tag = new ExifTag();
                ((IFlickrParsable)tag).Load(reader);
                Add(tag);
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }
}
