using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of the blog services that Flickr aupports. 
    /// </summary>
    public sealed class BlogServiceCollection : Collection<BlogService>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "services")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "service")
            {
                var service = new BlogService();
                ((IFlickrParsable) service).Load(reader);
                Add(service);
            }

            reader.Skip();
        }

        #endregion
    }
}