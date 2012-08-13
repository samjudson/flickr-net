using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

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

        public XDocument GetXDocument()
        {
            return XDocument.Parse(ResponseXml);
        }

        /// <summary>
        /// Gets an attribute value from the given response.
        /// </summary>
        /// <param name="response">The response from Flickr, containing the XML returned.</param>
        /// <param name="element">The element name to find.</param>
        /// <param name="attribute">The attribute of the element to return.</param>
        /// <returns>The string value of the given attribute, if found.</returns>
        public string GetAttributeValue(string element, string attribute)
        {
            System.Xml.Linq.XDocument doc = GetXDocument();
            if (String.IsNullOrEmpty(element) || element == "*")
                return doc.Descendants().Attributes(attribute).First().Value;
            else
                return doc.Descendants(element).Attributes(attribute).First().Value;
        }

        /// <summary>
        /// Gets a text value of an element from the given response.
        /// </summary>
        /// <param name="response">The response from Flickr, containing the XML returned.</param>
        /// <param name="element">The element name to find.</param>
        /// <returns>The string value of the given element, if found.</returns>
        public string GetElementValue(string element)
        {
            System.Xml.Linq.XDocument doc = GetXDocument();
            return doc.Descendants(element).First().Value;
        }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            ResponseXml = reader.ReadOuterXml();
        }

        /// <summary>
        /// Gets an array of text values of an element from the given response.
        /// </summary>
        /// </summary>
        /// <param name="elementName">The element name to find.</param>
        /// <returns>An array of string values.</returns>
        public string[] GetElementArray(string elementName)
        {
            var l = from e in GetXDocument().Descendants(elementName) select e.Value;

            return l.ToArray<string>();
        }

        /// <summary>
        /// Gets an array of text values of an element from the given response.
        /// </summary>
        /// </summary>
        /// <param name="elementName">The element name to find.</param>
        /// <returns>An array of string values.</returns>
        public string[] GetElementArray(string elementName, string attributeName)
        {
            System.Xml.Linq.XDocument doc = GetXDocument();
            if (String.IsNullOrEmpty(elementName) || elementName == "*")
                return doc.Descendants().Attributes(attributeName).Select( a => a.Value).ToArray();
            else
                return doc.Descendants(elementName).Attributes(attributeName).Select(a => a.Value).ToArray();
        }

    }
}
