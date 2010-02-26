using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    public interface IFlickrParsable
    {
        void Load(XmlReader reader);
    }
}
