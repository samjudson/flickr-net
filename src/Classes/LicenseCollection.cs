using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A class which encapsulates a single property, an array of
    /// <see cref="License"/> objects in its <see cref="LicenseCollection"/> property.
    /// </summary>
    public sealed class LicenseCollection : Collection<License>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "license")
            {
                var license = new License();
                ((IFlickrParsable) license).Load(reader);
                Add(license);
            }

            reader.Skip();
        }

        #endregion
    }
}