using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of the blog services that Flickr aupports. 
    /// </summary>
    public sealed class BlogServiceCollection : System.Collections.ObjectModel.Collection<BlogService>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "services")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "service")
            {
                BlogService service = new BlogService();
                ((IFlickrParsable)service).Load(reader);
                Add(service);
            }

            reader.Skip();

        }
    }
}
