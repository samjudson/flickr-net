using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Details of an individual EXIF tag.
    /// </summary>
    public sealed class ExifTag : IFlickrParsable
    {
        /// <summary>
        /// The type of EXIF data, e.g. EXIF, TIFF, GPS etc.
        /// </summary>
        public string TagSpace { get; set; }

        /// <summary>
        /// An id number for the type of tag space.
        /// </summary>
        public int TagSpaceId { get; set; }

        /// <summary>
        /// The tag number.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The label, or description for the tag, such as Aperture
        /// or Manufacturer
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The raw EXIF data.
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// An optional clean version of the <see cref="Raw"/> property.
        /// May be null if the <c>Raw</c> property is in a suitable format already.
        /// </summary>
        public string Clean { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "exif")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "tagspace":
                        TagSpace = reader.Value;
                        break;
                    case "tagspaceid":
                        TagSpaceId = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "tag":
                        Tag = reader.Value;
                        break;
                    case "label":
                        Label = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName != "exif")
            {
                switch (reader.LocalName)
                {
                    case "raw":
                        Raw = reader.ReadElementContentAsString();
                        break;
                    case "clean":
                        Clean = reader.ReadElementContentAsString();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;
                }
            }

            reader.Read();
        }
    }
}
