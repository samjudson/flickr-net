using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A photo in the context of a favorite. Returned as part of a call to <see cref="Flickr.FavoritesGetContext(string, string)"/>.
    /// </summary>
    public sealed class FavoriteContextPhoto : Photo, IFlickrParsable
    {
        /// <summary>
        /// The thumbnail url for the image. Will be the same as <see cref="Photo.ThumbnailUrl"/> if that is also set.
        /// </summary>
        public string FavoriteThumbnailUrl { get; set; }

        /// <summary>
        /// The URL for the favorite, with the context of the user as well.
        /// </summary>
        public string FavoriteUrl { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            FavoriteThumbnailUrl = reader.GetAttribute("thumb");
            FavoriteUrl = reader.GetAttribute("url");

            Load(reader, true);

            reader.Skip();
        }

    }
}
