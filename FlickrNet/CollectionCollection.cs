using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;

namespace FlickrNet
{
    /// <remarks/>
    public sealed class CollectionCollection : System.Collections.ObjectModel.Collection<Collection>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "collections")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "collection")
            {
                Collection c = new Collection();
                ((IFlickrParsable)c).Load(reader);
                Add(c);
            }

            // Skip to next element (if any)
            reader.Skip();
        }
    }


}
