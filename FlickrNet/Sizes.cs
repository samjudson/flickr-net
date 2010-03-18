using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;
using System;

namespace FlickrNet
{
	/// <summary>
	/// Collection of <see cref="Size"/> items for a given photograph.
	/// </summary>
    public sealed class SizeCollection : System.Collections.ObjectModel.Collection<Size>, IFlickrParsable
	{
        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "size")
            {
                Size size = new Size();
                ((IFlickrParsable)size).Load(reader);
                Add(size);
            }

            reader.Skip();
        }
    }

	/// <summary>
	/// Contains details about all the sizes available for a given photograph.
	/// </summary>
    public sealed class Size : IFlickrParsable
	{
		/// <summary>
		/// The label for the size, such as "Thumbnail", "Small", "Medium", "Large" and "Original".
		/// </summary>
		public string Label { get; private set; }
    
        /// <summary>
        /// The width of the resulting image, in pixels
        /// </summary>
		public int Width { get; private set; }
    
		/// <summary>
		/// The height of the resulting image, in pixels
		/// </summary>
		public int Height { get; private set; }
    
		/// <summary>
		/// The source url of the image.
		/// </summary>
		public Uri Source { get; private set; }
    
		/// <summary>
		/// The url to the photographs web page for this particular size.
		/// </summary>
		public Uri Url { get; private set; }

        /// <summary>
        /// The media type of this size.
        /// </summary>
        public MediaType MediaType { get; set; }


        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "label":
                        Label = reader.Value;
                        break;
                    case "width":
                        Width = reader.ReadContentAsInt() ;
                        break;
                    case "height":
                        Height = reader.ReadContentAsInt();
                        break;
                    case "source":
                        Source = new Uri(reader.Value);
                        break;
                    case "url":
                        Url = new Uri(reader.Value);
                        break;
                    case "media":
                        MediaType = reader.Value == "photo" ? MediaType.Photos : MediaType.Videos;
                        break;
                    default:
                        throw new ParsingException("Unknown attribute value: " + reader.LocalName + "=" + reader.Value);
                }
            }

            reader.Read();
        }

    }
}