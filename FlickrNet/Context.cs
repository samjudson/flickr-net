using System;
using System.Collections;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// The context of the current photo, as returned by
	/// <see cref="Flickr.PhotosGetContext"/>,
	/// <see cref="Flickr.PhotosetsGetContext"/>
	///  and <see cref="Flickr.GroupsPoolsGetContext"/> methods.
	/// </summary>
	public class Context
	{
		/// <summary>
		/// The number of photos in the current context, e.g. Group, Set or photostream.
		/// </summary>
		public int Count;
		/// <summary>
		/// The next photo in the context.
		/// </summary>
		public ContextPhoto NextPhoto;
		/// <summary>
		/// The previous photo in the context.
		/// </summary>
		public ContextPhoto PreviousPhoto;
	}

	/// <summary>
	/// Temporary class used to excapsulate the context count property.
	/// </summary>
	[System.Serializable]
	public class ContextCount
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ContextCount()
		{
		}

		/// <summary>
		/// The number of photos in the context.
		/// </summary>
		[XmlText()]
		public int Count;
	}

	/// <summary>
	/// The next (or previous) photo in the current context.
	/// </summary>
	[System.Serializable]
	public class ContextPhoto
	{
		/// <summary>
		/// The id of the next photo. Will be "0" if this photo is the last.
		/// </summary>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public string PhotoId;

		/// <summary>
		/// The secret for the photo.
		/// </summary>
		[XmlAttribute("secret", Form=XmlSchemaForm.Unqualified)]
		public string Secret;

		/// <summary>
		/// The title of the next photo in context.
		/// </summary>
		[XmlAttribute("title", Form=XmlSchemaForm.Unqualified)]
		public string Title;

		/// <summary>
		/// The URL, in the given context, for the next or previous photo.
		/// </summary>
		[XmlAttribute("url", Form=XmlSchemaForm.Unqualified)]
		public string Url;

		/// <summary>
		/// The URL for the thumbnail of the photo.
		/// </summary>
		[XmlAttribute("thumb", Form=XmlSchemaForm.Unqualified)]
		public string Thumbnail;
	}

	/// <summary>
	/// All contexts that a photo is in.
	/// </summary>
	public class AllContexts
	{
		private ContextSet[] _sets;
		private ContextGroup[] _groups;

		/// <summary>
		/// An array of <see cref="ContextSet"/> objects for the current photo.
		/// </summary>
		public ContextSet[] Sets
		{
			get { return _sets; }
		}

		/// <summary>
		/// An array of <see cref="ContextGroup"/> objects for the current photo.
		/// </summary>
		public ContextGroup[] Groups
		{
			get { return _groups; }
		}

		internal AllContexts(XmlElement[] elements)
		{
			ArrayList sets = new ArrayList();
			ArrayList groups = new ArrayList();

			if( elements == null ) { _sets = new ContextSet[0]; _groups = new ContextGroup[0]; return; }

			foreach(XmlElement element in elements)
			{
				if( element.Name == "set" )
				{
					ContextSet aset = new ContextSet();
					aset.PhotosetId = element.Attributes["id"].Value;
					aset.Title = element.Attributes["title"].Value;
					sets.Add(aset);
				}

				if( element.Name == "pool" )
				{
					ContextGroup agroup = new ContextGroup();
					agroup.GroupId = element.Attributes["id"].Value;
					agroup.Title = element.Attributes["title"].Value;
					groups.Add(agroup);
				}
			}

			_sets = new ContextSet[sets.Count];
			sets.CopyTo(_sets);

			_groups = new ContextGroup[groups.Count];
			groups.CopyTo(_groups);
		}
	}

	/// <summary>
	/// A set context for a photo.
	/// </summary>
	public class ContextSet
	{
		/// <summary>
		/// The Photoset ID of the set the selected photo is in.
		/// </summary>
		public string PhotosetId;
		/// <summary>
		/// The title of the set the selected photo is in.
		/// </summary>
		public string Title;
	}

	/// <summary>
	/// A group context got a photo.
	/// </summary>
	public class ContextGroup
	{
		/// <summary>
		/// The Group ID for the group that the selected photo is in.
		/// </summary>
		public string GroupId;
		/// <summary>
		/// The title of the group that then selected photo is in.
		/// </summary>
		public string Title;
	}
}
