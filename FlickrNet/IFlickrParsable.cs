using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// An interface that the classes that are returned by the Flickr API use to enable them to self-deserialize.
    /// </summary>
    /// <remarks>
    /// This enables more than one class to handle the same XML element names when returned by different methods.
    /// </remarks>
    public interface IFlickrParsable
    {
        /// <summary>
        /// Allows each class that implements this interface to be loaded via an <see cref="XmlReader"/>.
        /// </summary>
        /// <param name="reader"></param>
        void Load(XmlReader reader);
    }
}
