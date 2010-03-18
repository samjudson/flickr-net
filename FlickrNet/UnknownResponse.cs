using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.IO;

namespace FlickrNet
{
    /// <summary>
    /// Contains the raw response from Flickr when an unknown method has been called. Used by <see cref="Flickr.TestGeneric"/>.
    /// </summary>
    public class UnknownResponse : IFlickrParsable
    {
        /// <summary>
        /// The response from Flickr.
        /// </summary>
        public string ResponseXml { get; private set; }

        /// <summary>
        /// Gets a read only <see cref="XPathNavigator"/> containing the response XML.
        /// </summary>
        /// <returns></returns>
        public XPathNavigator GetXPathNavigator()
        {
            StringReader reader = new StringReader(ResponseXml);

            return new XPathDocument(reader).CreateNavigator();
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            ResponseXml = reader.ReadOuterXml();
        }
    }
}
