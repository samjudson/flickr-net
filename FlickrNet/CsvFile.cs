using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The details of a CSV files containing archived stats available for download from Flickr.
    /// </summary>
    /// <remarks>
    /// Only available till the 1st June 2010.
    /// </remarks>
    public sealed class CsvFile : IFlickrParsable
    {
        /// <summary>
        /// The web reference for the file.
        /// </summary>
        public string Href { get; private set; }

        /// <summary>
        /// The date the file was created for.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// The type of file archive (either "m" for Month or "d" for day).
        /// </summary>
        public string Type { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "href":
                        Href = reader.Value;
                        break;
                    case "type":
                        Type = reader.Value;
                        break;
                    case "date":
                        Date = DateTime.Parse(reader.Value, System.Globalization.DateTimeFormatInfo.CurrentInfo, System.Globalization.DateTimeStyles.None);
                        break;
                }
            }

            reader.Read();
        }
    }
}
