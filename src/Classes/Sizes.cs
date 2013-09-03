using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Collection of <see cref="Size"/> items for a given photograph.
    /// </summary>
    public sealed class SizeCollection : Collection<Size>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "size")
            {
                var size = new Size();
                ((IFlickrParsable) size).Load(reader);
                Add(size);
            }

            reader.Skip();
        }

        #endregion
    }

    /// <summary>
    /// Contains details about all the sizes available for a given photograph.
    /// </summary>
    public sealed class Size : IFlickrParsable
    {
        /// <summary>
        /// The label for the size, such as "Thumbnail", "Small", "Medium", "Large" and "Original".
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The width of the resulting image, in pixels
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of the resulting image, in pixels
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The source url of the image.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The url to the photographs web page for this particular size.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The media type of this size.
        /// </summary>
        public MediaType MediaType { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "label":
                        Label = reader.Value;
                        break;
                    case "width":
                        Width = reader.ReadContentAsInt();
                        break;
                    case "height":
                        Height = reader.ReadContentAsInt();
                        break;
                    case "source":
                        Source = reader.Value;
                        break;
                    case "url":
                        Url = reader.Value;
                        break;
                    case "media":
                        MediaType = reader.Value == "photo" ? MediaType.Photos : MediaType.Videos;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        #endregion
    }

}
