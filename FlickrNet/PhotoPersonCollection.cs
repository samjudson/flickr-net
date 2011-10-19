using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A list of <see cref="PhotoPerson"/> instances.
    /// </summary>
    public sealed class PhotoPersonCollection : System.Collections.ObjectModel.Collection<PhotoPerson>, IFlickrParsable
    {
        /// <summary>
        /// The total number of <see cref="PhotoPerson"/> instances returned.
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// The width of the photo used for positioning the people.
        /// </summary>
        /// <remarks>
        /// This will usually be the medium or the medium-640 image.
        /// </remarks>
        public int PhotoWidth { get; set; }

        /// <summary>
        /// The height of the photo used for positioning the people.
        /// </summary>
        /// <remarks>
        /// This will usually be the medium or the medium-640 image.
        /// </remarks>
        public int PhotoHeight { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "people")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        Total = reader.ReadContentAsInt();
                        break;
                    case "photo_width":
                        PhotoWidth = reader.ReadContentAsInt();
                        break;
                    case "photo_height":
                        PhotoHeight = reader.ReadContentAsInt();
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "person")
            {
                PhotoPerson item = new PhotoPerson();
                ((IFlickrParsable)item).Load(reader);
                Add(item);
            }

            reader.Skip();
        }
    }
}
