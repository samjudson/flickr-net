using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    public class PhotoInfoTagCollection: Collection<PhotoInfoTag>, IFlickrParsable
    {
        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.ReadToDescendant("tag");

            while (reader.LocalName == "tag")
            {
                var item = new PhotoInfoTag();
                ((IFlickrParsable) item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }
    }
}