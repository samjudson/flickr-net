using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="Place"/> items.
    /// </summary>
    public sealed class PlaceCollection : Collection<Place>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "place")
            {
                var place = new Place();
                ((IFlickrParsable) place).Load(reader);
                Add(place);
            }

            reader.Skip();
        }

        #endregion
    }
}