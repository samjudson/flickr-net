using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// An instance of a photo returned by <see cref="Flickr.GalleriesGetPhotos(string, PhotoSearchExtras)"/>.
    /// </summary>
    public class GalleryPhoto : Photo, IFlickrParsable
    {
        /// <summary>
        /// The comment added to this photo in the gallery, if any.
        /// </summary>
        public string Comment { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            Load(reader);

            if (reader.LocalName == "comment")
                Comment = reader.ReadElementContentAsString();

            if (reader.LocalName == "description")
                Description = reader.ReadElementContentAsString();

            if( reader.NodeType == System.Xml.XmlNodeType.EndElement && reader.LocalName == "photo")
                reader.Skip();
        }
    }
}
