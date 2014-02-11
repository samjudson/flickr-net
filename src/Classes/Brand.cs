using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A particular camera brand.
    /// </summary>
    public class Brand : IFlickrParsable
    {
        /// <summary>
        /// The name of the camera brand. e.g. "Canon".
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// The ID of the camera brand. e.g. "canon".
        /// </summary>
        public string BrandId { get; set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "brand")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        BrandId = reader.Value;
                        break;
                    case "name":
                        BrandName = reader.Value;
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