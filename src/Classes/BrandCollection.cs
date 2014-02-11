using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of camera brands
    /// </summary>
    public class BrandCollection : Collection<Brand>, IFlickrParsable
    {
        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "brands")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "brand")
            {
                var b = new Brand();
                ((IFlickrParsable)b).Load(reader);
                Add(b);
            }

            // Skip to next element (if any)
            reader.Skip();
        }
    }
}