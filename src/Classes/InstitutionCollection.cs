using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A collection of <see cref="Institution"/> instances.
    /// </summary>
    public sealed class InstitutionCollection : Collection<Institution>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "institution")
            {
                var service = new Institution();
                ((IFlickrParsable) service).Load(reader);
                Add(service);
            }
        }

        #endregion
    }
}