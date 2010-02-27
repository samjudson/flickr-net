using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for CollectionInfo.
	/// </summary>
	public class CollectionInfo : IFlickrParsable
	{
		private string _title;
		private string _description;
		private string _collectionId;
		private int _childCount;
		private DateTime _dateCreated;
		private string _iconLarge;
		private string _iconSmall;
		private string _server;
		private string _secret;

        private List<Photo> _iconPhotos = new List<Photo>();

		/// <summary>
		/// The ID for the collection.
		/// </summary>
		public string CollectionId { get { return _collectionId; } }

		/// <summary>
		/// The number of child collections this collection contains. Call <see cref="Flickr.CollectionsGetTree()"/> for children.
		/// </summary>
		public int ChildCount { get { return _childCount; } }

		/// <summary>
		/// The date the collection was created.
		/// </summary>
		public DateTime DateCreated { get { return _dateCreated; } }

		/// <summary>
		/// The large mosaic icon for the collection.
		/// </summary>
		public string IconLarge { get { return _iconLarge; } }

		/// <summary>
		/// The small mosaix icon for the collection.
		/// </summary>
		public string IconSmall { get { return _iconSmall; } }

		/// <summary>
		/// The server for the icons.
		/// </summary>
		public string Server { get { return _server; } }

		/// <summary>
		/// The secret for the icons.
		/// </summary>
		public string Secret { get { return _secret; } }

		/// <summary>
		/// The description for the collection.
		/// </summary>
		public string Description { get { return _description; } }

		/// <summary>
		/// The title of the description.
		/// </summary>
		public string Title { get { return _title; } }

        /// <summary>
        /// An array of the 12 photos used to create a collection's mosaic.
        /// </summary>
        public Photo[] IconPhotos { get { return _iconPhotos.ToArray(); } }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "collection")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.Name)
                {
                    case "id":
                        _collectionId = reader.Value;
                        break;
                    case "child_count":
                        _childCount = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "datecreate":
                        _dateCreated = Utils.UnixTimestampToDate(reader.Value);
                        break;
                    case "iconlarge":
                        _iconLarge = reader.Value;
                        break;
                    case "iconsmall":
                        _iconSmall = reader.Value;
                        break;
                    case "server":
                        _server = reader.Value;
                        break;
                    case "secret":
                        _secret = reader.Value;
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unknown attribute: " + reader.Name + ", " + reader.Value);
                        break;
                }
            }

            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "title":
                        _title = reader.ReadString();
                        break;
                    case "description":
                        _description = reader.ReadString();
                        break;
                    case "iconphotos":
                        reader.Read();

                        while (reader.LocalName == "photo")
                        {
                            Photo p = new Photo();
                            p.Load(reader);

                            _iconPhotos.Add(p);
                        }
                        reader.Read();
                        return;
                    default:
                        throw new ApplicationException("Unknown element found in response stream: " + reader.Name);
                }
            }

            reader.Skip();

        }
	}
}
