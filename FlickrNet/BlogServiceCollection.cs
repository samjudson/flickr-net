using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of the blog services that Flickr aupports. 
    /// </summary>
    public class BlogServiceCollection : List<BlogService>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "services")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

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
