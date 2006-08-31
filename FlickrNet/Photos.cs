using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections;

namespace FlickrNet
{
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