using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="Place"/> items.
    /// </summary>
    public class PlaceCollection : List<Place>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "place")
            {
                Place place = new Place();
                ((IFlickrParsable)place).Load(reader);
                Add(place);
            }

            reader.Skip();
        }
    }
}
