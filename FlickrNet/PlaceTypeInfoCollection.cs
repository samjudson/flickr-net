using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Collection containing information about the types of 'places' available from the Flickr API.
    /// </summary>
    /// <remarks>
    /// Use the <see cref="PlaceInfo"/> enumeration were possible.
    /// </remarks>
    public sealed class PlaceTypeInfoCollection : System.Collections.ObjectModel.Collection<PlaceTypeInfo>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "place_types")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "place_type")
            {
                PlaceTypeInfo item = new PlaceTypeInfo();
                ((IFlickrParsable)item).Load(reader);
                Add(item);
            }

            reader.Skip();

        }
    }
}
