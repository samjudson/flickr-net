using System;
using System.Collections.Generic;
using System.Xml;

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
        public XmlDocument GetXmlDocument()
        {
            var document = new XmlDocument();
            document.LoadXml(ResponseXml);

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
            XmlDocument doc = GetXmlDocument();
            XmlNode node = doc.SelectSingleNode("//" + element + "/@" + attribute);
            return node != null ? node.Value : null;
        }

        public T GetAttributeValue<T>(string element, string attribute)
        {
            var doc = GetXmlDocument();
            var node = doc.SelectSingleNode("//" + element + "/@" + attribute);
            if (node != null)
            {
                return (T) Convert.ChangeType(node.Value, typeof (T));
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
            var node = doc.SelectSingleNode("//" + element + "[1]");
            return node != null ? node.InnerText : null;
        }


        /// <summary>
        /// Gets an array of text values of an element from the given response.
        /// </summary>
        /// <param name="elementName">The element name to find.</param>
        /// <returns>An array of string values.</returns>
        public string[] GetElementArray(string elementName)
        {
            var array = new List<string>();
            foreach (XmlNode n in GetXmlDocument().SelectNodes("//" + elementName))
            {
                array.Add(n.InnerText);
            }
            return array.ToArray();
        }

        public string[] GetElementArray(string elementName, string attributeName)
        {
            var array = new List<string>();
            foreach (XmlNode n in GetXmlDocument().SelectNodes("//" + elementName + "/@" + attributeName))
            {
                array.Add(n.InnerText);
            }
            return array.ToArray();
        }

        public T[] GetElementArray<T>(string elementName, string attributeName)
        {
            var array = new List<T>();
            foreach (XmlNode n in GetXmlDocument().SelectNodes("//" + elementName + "/@" + attributeName))
            {
                array.Add((T)Convert.ChangeType(n.InnerText, typeof(T)));
            }
            return array.ToArray();
        }
    }
}