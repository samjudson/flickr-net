using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public sealed class CsvFileCollection : System.Collections.ObjectModel.Collection<CsvFile>, IFlickrParsable
    {
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.ReadToDescendant("csv");

            while (reader.LocalName == "csv" && reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                CsvFile file = new CsvFile();
                ((IFlickrParsable)file).Load(reader);
                Add(file);
            }
        }
    }
}
