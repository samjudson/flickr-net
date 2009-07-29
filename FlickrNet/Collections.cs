using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <remarks/>
	[System.Serializable]
	public class Collections
	{
		private Collection[] _collections = new Collection[0];

		/// <summary>
		/// An array of <see cref="Collection"/> objects.
		/// </summary>
		[XmlElement("collection", Form = XmlSchemaForm.Unqualified)]
		public Collection[] CollectionList
		{
			get { return _collections; }
			set
			{
				if (value == null)
					_collections = new Collection[0];
				else
					_collections = value;
			}
		}
	}


}
