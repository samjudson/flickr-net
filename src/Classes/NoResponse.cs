using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Used by the FlickrNet library when Flickr does not return anything in the body of a response, e.g. for update methods.
    /// </summary>
    public sealed class NoResponse : IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
        }

        #endregion
    }
}