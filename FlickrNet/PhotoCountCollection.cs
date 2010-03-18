using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// The information about the number of photos a user has.
	/// </summary>
	public class PhotoCountCollection : List<PhotoCount>, IFlickrParsable
	{
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photocounts")
                throw new Exception("Unknown element found: " + reader.LocalName);

            reader.Read();

            while (reader.LocalName == "photocount")
            {
                PhotoCount c = new PhotoCount();
                ((IFlickrParsable)c).Load(reader);
                Add(c);
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }

	/// <summary>
	/// The specifics of a particular count.
	/// </summary>
	public class PhotoCount : IFlickrParsable
	{
		/// <summary>Total number of photos between the FromDate and the ToDate.</summary>
		/// <remarks/>
        public int Count { get; private set; }
    
		/// <summary>The From date as a <see cref="DateTime"/> object.</summary>
		public DateTime FromDate { get; private set; }

		/// <summary>The To date as a <see cref="DateTime"/> object.</summary>
		public DateTime ToDate { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photocount")
                throw new Exception("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "count":
                        Count = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "fromdate":
                        FromDate = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    case "todate":
                        ToDate = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    default:
                        throw new Exception("Unknown attribute: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();
        }
    }
}