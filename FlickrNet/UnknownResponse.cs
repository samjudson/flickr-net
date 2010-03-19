using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Contains the raw response from Flickr when an unknown method has been called. Used by <see cref="Flickr.TestGeneric"/>.
    /// </summary>
    public sealed class UnknownResponse : IFlickrParsable
    {
        /// <summary>
        /// The response from Flickr.
        /// </summary>
        public string ResponseXml { get; private set; }

        /// <summary>
        /// Gets a <see cref="XmlDocument"/> containing the response XML.
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetXmlDocument()
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(ResponseXml);

            return document;
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            ResponseXml = reader.ReadOuterXml();
        }
    }
}
