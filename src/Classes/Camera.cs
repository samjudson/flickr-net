using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// The details of a particular camera model.
    /// </summary>
    public class Camera : IFlickrParsable
    {
        /// <summary>
        /// The large image for this camera model.
        /// </summary>
        public string LargeImage { get; set; }

        /// <summary>
        /// The small image for this camera model.
        /// </summary>
        public string SmallImage { get; set; }

        /// <summary>
        /// The type of memory used in this camera.
        /// </summary>
        public string MemoryType { get; set; }

        /// <summary>
        /// The size of the LCD screen (if any) used in this camera.
        /// </summary>
        public string LcdScreenSize { get; set; }

        /// <summary>
        /// The number of megapixels for this camera.
        /// </summary>
        public string MegaPixels { get; set; }

        /// <summary>
        /// The unique ID of this camera on Flickr.
        /// </summary>
        public string CameraId { get; set; }

        /// <summary>
        /// The name of this camera.
        /// </summary>
        public string CameraName { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "camera")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        CameraId = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName != "camera")
            {
                switch (reader.LocalName)
                {
                    case "details":
                    case "images":
                        reader.Read();
                        break;
                    case "name":
                        CameraName = reader.ReadElementContentAsString();
                        break;
                    case "megapixels":
                        MegaPixels = reader.ReadElementContentAsString();
                        break;
                    case "lcd_screen_size":
                        LcdScreenSize = reader.ReadElementContentAsString();
                        break;
                    case "memory_type":
                        MemoryType = reader.ReadElementContentAsString();
                        break;
                    case "small":
                        SmallImage = reader.ReadElementContentAsString();
                        break;
                    case "large":
                        LargeImage = reader.ReadElementContentAsString();
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