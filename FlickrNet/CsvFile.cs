using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public sealed class CsvFile : IFlickrParsable
    {
        public string Href { get; private set; }

        public DateTime Date { get; private set; }

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
