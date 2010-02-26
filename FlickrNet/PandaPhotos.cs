using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public class PandaPhotos: List<Photo>, IFlickrParsable
    {
        public int Interval { get; set; }
        public DateTime LastUpdated { get; set; }
        public int Total { get; set; }
        public string PandaName { get; set; }

        public PandaPhotos()
        {
        }

        public void Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photos")
                throw new FlickrException("Unknown element found: " + reader.LocalName);


            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "interval":
                        Interval = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "panda":
                        PandaName = reader.Value;
                        break;
                    case "lastupdate":
                        LastUpdated = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo p = new Photo(reader);
                if (!String.IsNullOrEmpty(p.PhotoId)) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();
            
        }
    }
}
