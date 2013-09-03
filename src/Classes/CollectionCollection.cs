using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <remarks/>
    public sealed class CollectionCollection : Collection<Collection>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "collections")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "collection")
            {
                var c = new Collection();
                ((IFlickrParsable) c).Load(reader);
                Add(c);
            }

            // Skip to next element (if any)
            reader.Skip();
        }

        #endregion
    }
}