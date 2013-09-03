using System;
using System.Collections.Generic;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// The response returned by the <see cref="Flickr.TestEcho"/> method.
    /// </summary>
    [Serializable]
    public sealed class EchoResponseDictionary : Dictionary<string, string>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.NodeType != XmlNodeType.None && reader.NodeType != XmlNodeType.EndElement)
            {
                Add(reader.Name, reader.ReadElementContentAsString());
            }
        }

        #endregion
    }
}