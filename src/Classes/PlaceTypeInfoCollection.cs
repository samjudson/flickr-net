using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Collection containing information about the types of 'places' available from the Flickr API.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="PlaceInfo"/> enumeration were possible.
    /// </remarks>
    public sealed class PlaceTypeInfoCollection : Collection<PlaceTypeInfo>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "place_types")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "place_type")
            {
                var item = new PlaceTypeInfo();
                ((IFlickrParsable) item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }

        #endregion
    }
}