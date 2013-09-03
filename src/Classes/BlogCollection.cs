using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Contains a list of <see cref="Blog"/> items for the user.
    /// </summary>
    public sealed class BlogCollection : Collection<Blog>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "blogs")
                UtilityMethods.CheckParsingException(reader);

            reader.Read();

            while (reader.LocalName == "blog")
            {
                var b = new Blog();
                ((IFlickrParsable) b).Load(reader);
                Add(b);
            }

            // Skip to next element (if any)
            reader.Skip();
        }

        #endregion
    }
}