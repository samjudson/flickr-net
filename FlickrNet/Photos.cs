using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <remarks/>
	[Serializable]
	public class Photos : List<Photo>, IXmlSerializable
	{
        //private Photo[] _photos = new Photo[0];

		/// <remarks/>
        [Obsolete("Photos class now inherits from List<Photo>. Iterate Photos class directly")]
        public Photo[] PhotoCollection
        {
            get { return this.ToArray(); }
        }

        private int _totalPhotos;
        private int _pageNumber;
        private int _totalPages;
        private int _photosPerPage;

		/// <remarks/>
        [Obsolete("User new Page property")]
        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value; }
        }

        /// <remarks/>
        public int Page { get { return _pageNumber; } set { _pageNumber = value; } }
    
		/// <remarks/>
        [Obsolete("User new Pages property")]
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        /// <remarks/>
        public int Pages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        /// <remarks/>
        public int PhotosPerPage
        {
            get { return _photosPerPage; }
            set { _photosPerPage = value; }
        }


		/// <remarks/>
        public int TotalPhotos
        {
            get { return _totalPhotos; }
            set { _totalPhotos = value; }
        }

        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "photos")
                throw new FlickrException("Unknown element found: " + reader.LocalName);


            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        TotalPhotos = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "perpage":
                        PhotosPerPage = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    default:
                        throw new Exception("Unknown element: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo p = new Photo(reader);
                if( !String.IsNullOrEmpty(p.PhotoId) ) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();

        }

        void IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
        {
            
        }

        #endregion
    }

	/// <summary>
	/// A collection of <see cref="Photo"/> instances.
	/// </summary>
	[System.Serializable]
    [Obsolete("User List<Photo> now")]
	public class PhotoCollection : CollectionBase
	{
	
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PhotoCollection()
		{
		}

		/// <summary>
		/// Creates an instance of the <see cref="PhotoCollection"/> from an array of <see cref="Photo"/>
		/// instances.
		/// </summary>
		/// <param name="photos">An array of <see cref="Photo"/> instances.</param>
		public PhotoCollection(Photo[] photos)
		{
			if (photos == null) return;

			for (int i=0; i<photos.Length; i++)
			{
				List.Add(photos[i]);
			}
		}

		/// <summary>
		/// Gets number of photos in the current collection.
		/// </summary>
		public int Length
		{
			get { return List.Count; }
		}

		/// <summary>
		/// Gets the index of a photo in this collection.
		/// </summary>
		/// <param name="photo">The photo to find.</param>
		/// <returns>The index of the photo, -1 if it is not in the collection.</returns>
		public int IndexOf(Photo photo)
		{
			return List.IndexOf(photo);
		}

		#region ICollection Members

		/// <summary>
		/// Gets a value indicating if the collection is synchronized (thread-safe).
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				return List.IsSynchronized;
			}
		}

		/// <summary>
		/// Copies the elements of the collection to an array of <see cref="Photo"/>, starting at a 
		/// particular index.
		/// </summary>
		/// <param name="array">The array to copy to.</param>
		/// <param name="index">The index in the collection to start copying from.</param>
		public void CopyTo(Photo[] array, int index)
		{
			List.CopyTo(array, index);
		}

		/// <summary>
		/// Gets an object that can be used to synchronize the collection.
		/// </summary>
		public object SyncRoot
		{
			get
			{
				return List.SyncRoot;
			}
		}

		#endregion
	
		#region IList Members

		/// <summary>
		/// Gets a value indicating whether the collection is read-only.
		/// </summary>
		public bool IsReadOnly
		{
			get
			{
				return List.IsReadOnly;
			}
		}

		/// <summary>
		/// Gets or sets a photo based on the index in the collection.
		/// </summary>
		public Photo this[int index]
		{
			get
			{
				return (Photo)List[index];
			}
			set
			{
				List[index] = value;
			}
		}

		/// <summary>
		/// Inserts a <see cref="Photo"/> into the collection at the given index.
		/// </summary>
		/// <param name="index">The index to insert the <see cref="Photo"/> into.
		/// Subsequent photos will be moved up.</param>
		/// <param name="photo">The <see cref="Photo"/> to insert.</param>
		public void Insert(int index, Photo photo)
		{
			List.Insert(index, photo);
		}

		/// <summary>
		/// Removes a photo from the collection.
		/// </summary>
		/// <param name="photo">The <see cref="Photo"/> instance to remove from the collection.</param>
		public void Remove(Photo photo)
		{
			List.Remove(photo);
		}

		/// <summary>
		/// Returns true if the collection contains the photo.
		/// </summary>
		/// <param name="photo">The <see cref="Photo"/> instance to try to find.</param>
		/// <returns>True of False, depending on if the <see cref="Photo"/> is found in the collection.</returns>
		public bool Contains(Photo photo)
		{
			return List.Contains(photo);
		}

		/// <summary>
		/// Adds a <see cref="Photo"/> to the collection.
		/// </summary>
		/// <param name="photo">The <see cref="Photo"/> instance to add to the collection.</param>
		/// <returns>The index that the photo was added at.</returns>
		public int Add(Photo photo)
		{
			return List.Add(photo);
		}

		/// <summary>
		/// Adds an array of <see cref="Photo"/> instances to this collection.
		/// </summary>
		/// <param name="photos">An array of <see cref="Photo"/> instances.</param>
		public void AddRange(Photo[] photos)
		{
			foreach(Photo photo in photos)
				List.Add(photo);
		}

		/// <summary>
		/// Adds all of the photos in another <see cref="PhotoCollection"/> to this collection.
		/// </summary>
		/// <param name="collection">The <see cref="PhotoCollection"/> containing the photos to add
		/// to this collection.</param>
		public void AddRange(PhotoCollection collection)
		{
			foreach(Photo photo in collection)
				List.Add(photo);
		}

		/// <summary>
		/// Gets an instance specifying whether the collection is a fixed size.
		/// </summary>
		public bool IsFixedSize
		{
			get
			{
				return List.IsFixedSize;
			}
		}

		#endregion

		/// <summary>
		/// Converts a PhotoCollection instance to an array of Photo objects.
		/// </summary>
		/// <param name="collection">The collection to convert.</param>
		/// <returns>An array of <see cref="Photo"/> objects.</returns>
		public static implicit operator Photo[](PhotoCollection collection)
		{
			Photo[] photos = new Photo[collection.Count];
			collection.CopyTo(photos, 0);
			return photos;
		}

		/// <summary>
		/// Converts the collection to an array of <see cref="Photo"/> objects.
		/// </summary>
		/// <returns>An array of <see cref="Photo"/> objects.</returns>
		public Photo[] ToPhotoArray()
		{
			return (Photo[])this;
		}

		/// <summary>
		/// Implicitly converts an array of <see cref="Photo"/> objects to a <see cref="PhotoCollection"/>.
		/// </summary>
		/// <param name="photos">The array of <see cref="Photo"/> objects to convert.</param>
		/// <returns></returns>
		public static implicit operator PhotoCollection(Photo[] photos)
		{
			return new PhotoCollection(photos);
		}

		/// <summary>
		/// Creates a <see cref="PhotoCollection"/> from an array of <see cref="Photo"/> objects.
		/// </summary>
		/// <param name="photos">An array of <see cref="Photo"/> objects.</param>
		/// <returns>A new <see cref="PhotoCollection"/> containing all the objects from the array.</returns>
		public static PhotoCollection FromPhotoArray(Photo[] photos)
		{
			return (PhotoCollection)photos;
		}
	}

}