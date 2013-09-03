using System;
using System.Globalization;
using System.Xml;

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
        public string Href { get; set; }

        /// <summary>
        /// The date the file was created for.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The type of file archive (either "m" for Month or "d" for day).
        /// </summary>
        public string Type { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
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
                        Date = DateTime.Parse(reader.Value, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
                        break;
                }
            }

            reader.Read();
        }

        #endregion
    }
}