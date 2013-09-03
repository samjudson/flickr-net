using System.Collections.ObjectModel;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// The collection of CSV files containing archived stats available for download from Flickr.
    /// </summary>
    /// <remarks>
    /// Only supported until the 1st June 2010.
    /// </remarks>
    public sealed class CsvFileCollection : Collection<CsvFile>, IFlickrParsable
    {
        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.ReadToDescendant("csv");

            while (reader.LocalName == "csv" && reader.NodeType != XmlNodeType.EndElement)
            {
                var file = new CsvFile();
                ((IFlickrParsable) file).Load(reader);
                Add(file);
            }
        }

        #endregion
    }
}