using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// Used by the FlickrNet library when Flickr does not return anything in the body of a response, e.g. for update methods.
    /// </summary>
    public class NoResponse : IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
        }
    }
}
