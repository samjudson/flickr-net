using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;
using System;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Collection containing a users photosets.
	/// </summary>
	public class PhotosetCollection : List<Photoset>, IFlickrParsable
	{
		/// <summary>
		/// Can the user create more photosets.
		/// </summary>
		/// <remarks>
		/// 1 meants yes, 0 means no.
		/// </remarks>
		public bool CanCreate { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photosets")
                throw new ParsingException("Unknown element name '" + reader.LocalName + "' found in Flickr response");

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "cancreate":
                        CanCreate = reader.Value == "1";
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }
            reader.Read();

            while (reader.LocalName == "photoset")
            {
                Photoset photoset = new Photoset();
                ((IFlickrParsable)photoset).Load(reader);
                Add(photoset);
            }

            reader.Skip();
        }
    }

}