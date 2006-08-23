using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections;

namespace FlickrNet
{
	/// <summary>
	/// Which photo search extras to be included. Can be combined to include more than one
	/// value.
	/// </summary>
	/// <example>
	/// The following code sets options to return both the license and owner name along with
	/// the other search results.
	/// <code>
	/// PhotoSearchOptions options = new PhotoSearchOptions();
	/// options.Extras = PhotoSearchExtras.License &amp; PhotoSearchExtras.OwnerName
	/// </code>
	/// </example>
	[Flags]
	public enum PhotoSearchExtras
	{
		/// <summary>
		/// No extras selected.
		/// </summary>
		None = 0,
		/// <summary>
		/// Returns a license.
		/// </summary>
		License = 1,
		/// <summary>
		/// Returned the date the photos was uploaded.
		/// </summary>
		DateUploaded = 2,
		/// <summary>
		/// Returned the date the photo was taken.
		/// </summary>
		DateTaken = 4,
		/// <summary>
		/// Returns the name of the owner of the photo.
		/// </summary>
		OwnerName = 8,
		/// <summary>
		/// Returns the server for the buddy icon for this user.
		/// </summary>
		IconServer = 16,
		/// <summary>
		/// Returns the extension for the original format of this photo.
		/// </summary>
		OriginalFormat = 32,
		/// <summary>
		/// Returns the date the photo was last updated.
		/// </summary>
		LastUpdated = 64,
		/// <summary>
		/// Undocumented Tags extra
		/// </summary>
		Tags = 132,
		/// <summary>
		/// Returns all the above information.
		/// </summary>
		All = License | DateUploaded | DateTaken | OwnerName | IconServer | OriginalFormat | LastUpdated | Tags
	}

	/// <remarks/>
	[Serializable]
	public class Photos 
	{
    
		/// <remarks/>
		[XmlElement("photo", Form=XmlSchemaForm.Unqualified)]
		public PhotoCollection PhotoCollection = new PhotoCollection();
    
		/// <remarks/>
		[XmlAttribute("page", Form=XmlSchemaForm.Unqualified)]
		public long PageNumber;
    
		/// <remarks/>
		[XmlAttribute("pages", Form=XmlSchemaForm.Unqualified)]
		public long TotalPages;
    
		/// <remarks/>
		[XmlAttribute("perpage", Form=XmlSchemaForm.Unqualified)]
		public long PhotosPerPage;
    
		/// <remarks/>
		[XmlAttribute("total", Form=XmlSchemaForm.Unqualified)]
		public long TotalPhotos;
	}

	/// <remarks/>
	[System.Serializable]
	public class Photo 
	{
    
		/// <remarks/>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string PhotoId;
    
		/// <remarks/>
		[XmlAttribute("owner", Form=XmlSchemaForm.Unqualified)]
		public string UserId;
    
		/// <remarks/>
		[XmlAttribute("secret", Form=XmlSchemaForm.Unqualified)]
		public string Secret;
    
		/// <remarks/>
		[XmlAttribute("server", Form=XmlSchemaForm.Unqualified)]
		public string Server;
    
		/// <remarks/>
		[XmlAttribute("title", Form=XmlSchemaForm.Unqualified)]
		public string Title;
    
		/// <remarks/>
		[XmlAttribute("ispublic", Form=XmlSchemaForm.Unqualified)]
		public int IsPublic;
    
		/// <remarks/>
		[XmlAttribute("isfriend", Form=XmlSchemaForm.Unqualified)]
		public int IsFriend;
    
		/// <remarks/>
		[XmlAttribute("isfamily", Form=XmlSchemaForm.Unqualified)]
		public int IsFamily;

		/// <remarks/>
		[XmlAttribute("isprimary", Form=XmlSchemaForm.Unqualified)]
		public int IsPrimary;

		/// <remarks/>
		[XmlAttribute("license", Form=XmlSchemaForm.Unqualified)]
		public string License;

		/// <remarks/>
		[XmlAttribute("dateupload", Form=XmlSchemaForm.Unqualified)]
		public string dateupload_raw;

		/// <summary>
		/// Converts the raw dateupload field to a <see cref="DateTime"/>.
		/// </summary>
		[XmlIgnore]
		public DateTime DateUploaded
		{
			get { return Utils.UnixTimestampToDate(dateupload_raw); }
		}

		/// <summary>
		/// Converts the raw lastupdate field to a <see cref="DateTime"/>.
		/// </summary>
		[XmlIgnore]
		public DateTime LastUpdated
		{
			get { return Utils.UnixTimestampToDate(lastupdate_raw); }
		}

		/// <remarks/>
		[XmlAttribute("lastupdate", Form=XmlSchemaForm.Unqualified)]
		public string lastupdate_raw;

		/// <remarks/>
		[XmlAttribute("datetaken", Form=XmlSchemaForm.Unqualified)]
		public string datetaken_raw;

		/// <summary>
		/// Converts the raw datetaken field to a <see cref="DateTime"/>.
		/// </summary>
		[XmlIgnore]
		public DateTime DateTaken
		{
			get { return System.DateTime.Parse(datetaken_raw); }
		}

		/// <remarks/>
		[XmlAttribute("ownername", Form=XmlSchemaForm.Unqualified)]
		public string OwnerName;

		/// <remarks/>
		[XmlAttribute("iconserver", Form=XmlSchemaForm.Unqualified)]
		public string IconServer;

		/// <summary>
		/// Optional extra field containing the original format (jpg, png etc) of the 
		/// photo.
		/// </summary>
		[XmlAttribute("originalformat", Form=XmlSchemaForm.Unqualified)]
		public string OriginalFormat;

		/// <summary>
		/// Undocumented tags attribute
		/// </summary>
		[XmlAttribute("tags", Form=XmlSchemaForm.Unqualified)]
		public string RawTags;

		private const string photoUrl = "http://static.flickr.com/{0}/{1}_{2}{3}.{4}";

		/// <summary>
		/// The url to the web page for this photo. Uses the users userId, not their web alias, but
		/// will still work.
		/// </summary>
		[XmlIgnore()]
		public string WebUrl
		{
			get { return string.Format("http://www.flickr.com/photos/{0}/{1}/", UserId, PhotoId); }
		}

		/// <summary>
		/// The URL for the square thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string SquareThumbnailUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_s", "jpg"); }
		}

		/// <summary>
		/// The URL for the thumbnail of a photo.
		/// </summary>
		[XmlIgnore()]
		public string ThumbnailUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_t", "jpg"); }
		}

		/// <summary>
		/// The URL for the small copy of a photo.
		/// </summary>
		[XmlIgnore()]
		public string SmallUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_m", "jpg"); }
		}

		/// <summary>
		/// The URL for the medium copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that extremely small images will not have a medium copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		[XmlIgnore()]
		public string MediumUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "", "jpg"); }
		}

		/// <summary>
		/// The URL for the large copy of a photo.
		/// </summary>
		/// <remarks>There is a chance that small images will not have a large copy.
		/// Use <see cref="Flickr.PhotosGetSizes"/> to get the available URLs for a photo.</remarks>
		[XmlIgnore()]
		public string LargeUrl
		{
			get { return string.Format(photoUrl, Server, PhotoId, Secret, "_b", "jpg"); }
		}

		/// <summary>
		/// If <see cref="OriginalFormat"/> was returned then this will contain the url of the original file.
		/// </summary>
		[XmlIgnore()]
		public string OriginalUrl
		{
			get 
			{ 
				if( OriginalFormat == null || OriginalFormat.Length == 0 )
					throw new InvalidOperationException("No original format information available.");

				return string.Format(photoUrl, Server, PhotoId, Secret, "_o", OriginalFormat);
			}
		}
	}

	/// <summary>
	/// Permissions for the selected photo.
	/// </summary>
	[System.Serializable]
	public class PhotoPermissions
	{
		/// <remarks/>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string PhotoId;

		/// <remarks/>
		[XmlAttribute("ispublic", Form=XmlSchemaForm.Unqualified)]
		public int IsPublic;
    
		/// <remarks/>
		[XmlAttribute("isfriend", Form=XmlSchemaForm.Unqualified)]
		public int IsFriend;
    
		/// <remarks/>
		[XmlAttribute("isfamily", Form=XmlSchemaForm.Unqualified)]
		public int IsFamily;

		/// <remarks/>
		[XmlAttribute("permcomment", Form=XmlSchemaForm.Unqualified)]
		public PermissionComment PermissionComment;

		/// <remarks/>
		[XmlAttribute("permaddmeta", Form=XmlSchemaForm.Unqualified)]
		public PermissionAddMeta PermissionAddMeta;
	}

	/// <summary>
	/// A collection of <see cref="Photo"/> instances.
	/// </summary>
	[System.Serializable]
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