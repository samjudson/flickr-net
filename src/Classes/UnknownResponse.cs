using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace FlickrNet
{
    internal sealed class UnknownResponse : IFlickrParsable
    {
        /// <summary>
        /// The response from Flickr.
        /// </summary>
        public string ResponseXml { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            ResponseXml = reader.ReadOuterXml();
        }

        #endregion

        /// <summary>
        /// Gets a <see cref="XmlDocument"/> containing the response XML.
        /// </summary>
        /// <returns></returns>
        private XDocument GetXmlDocument()
        {
            var document = XDocument.Parse(ResponseXml);

            return document;
        }

        /// <summary>
        /// Gets an attribute value from the given response.
        /// </summary>
        /// <param name="element">The element name to find.</param>
        /// <param name="attribute">The attribute of the element to return.</param>
        /// <returns>The string value of the given attribute, if found.</returns>
        public string GetAttributeValue(string element, string attribute)
        {
            var doc = GetXmlDocument();
            var node = doc.Descendants(element).Attributes(attribute).First();
            return node != null ? node.Value : null;
        }

        public T GetAttributeValue<T>(string element, string attribute)
        {
            var doc = GetXmlDocument();
            var node = doc.Descendants(element).Attributes(attribute).FirstOrDefault();
            if (node != null)
            {
                return (T) Convert.ChangeType(node.Value, typeof (T), null);
            }

            return default(T);
        }

        /// <summary>
        /// Gets an text value of an element from the given response.
        /// </summary>
        /// <param name="element">The element name to find.</param>
        /// <returns>The string value of the given element, if found.</returns>
        public string GetElementValue(string element)
        {
            var doc = GetXmlDocument();
            var node = doc.Descendants(element).FirstOrDefault();
            return node != null ? node.Value : null;
        }


        /// <summary>
        /// Gets an array of text values of an element from the given response.
        /// </summary>
        /// <param name="elementName">The element name to find.</param>
        /// <returns>An array of string values.</returns>
        public string[] GetElementArray(string elementName)
        {
            return GetXmlDocument().Descendants(elementName).Select(n => n.Value).ToArray();
        }

        public string[] GetElementArray(string elementName, string attributeName)
        {
            return GetXmlDocument().Descendants(elementName).Attributes(attributeName).Select(n => n.Value).ToArray();
        }

        public T[] GetElementArray<T>(string elementName, string attributeName)
        {
            return GetXmlDocument().Descendants(elementName).Attributes(attributeName).Select(n => (T) Convert.ChangeType(n.Value, typeof (T), null)).ToArray();
        }
    }
}