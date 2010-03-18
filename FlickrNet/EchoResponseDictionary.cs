using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The response returned by the <see cref="Flickr.TestEcho"/> method.
    /// </summary>
    [Serializable]
    public sealed class EchoResponseDictionary : Dictionary<string, string>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.NodeType != System.Xml.XmlNodeType.None && reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                Add(reader.Name, reader.ReadElementContentAsString());
            }
        }
    }
}
