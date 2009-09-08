using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Contains details of a category, including groups belonging to the category and sub categories.
	/// </summary>
	[System.Serializable]
	public class Category
	{
		/// <summary>
		/// The name for the category.
		/// </summary>
		[XmlAttribute("name", Form=XmlSchemaForm.Unqualified)]
		public string CategoryName;
    
		/// <summary>
		/// A forward slash delimited list of the parents of the current group.
		/// </summary>
		/// <remarks>
		/// Can be matched against the list of PathIds to jump directly to a parent group.
		/// </remarks>
		/// <example>
		/// Group Id 91, Romance will return "/Life/Romance" as the Path and "/90/91" as its PathIds
		/// </example>
		[XmlAttribute("path", Form=XmlSchemaForm.Unqualified)]
		public string Path;
    
		/// <summary>
		/// A forward slash delimited list of the ids of the parents of the current group.
		/// </summary>
		/// <remarks>
		/// Can be matched against the Path to jump directly to a parent group.
		/// </remarks>
		/// <example>
		/// Group Id 91, Romance will return "/Life/Romance" as the Path and "/90/91" as its PathIds
		/// </example>
		[XmlAttribute("pathids", Form=XmlSchemaForm.Unqualified)]
		public string PathIds;

		/// <summary>
		/// An array of <see cref="SubCategory"/> items.
		/// </summary>
		[XmlElement("subcat", Form=XmlSchemaForm.Unqualified)]
		public SubCategory[] SubCategories;

		/// <summary>
		/// An array of <see cref="Group"/> items, listing the groups within this category.
		/// </summary>
		[XmlElement("group", Form=XmlSchemaForm.Unqualified)]
		public Group[] Groups;
	}

	/// <summary>
	/// Holds details of a sub category, including its id, name and the number of groups in it.
	/// </summary>
	[System.Serializable]
	public class SubCategory
	{
		/// <summary>
		/// The id of the category.
		/// </summary>
		[XmlAttribute("id", Form=XmlSchemaForm.Unqualified)]
		public long SubCategoryId;
    
		/// <summary>
		/// The name of the category.
		/// </summary>
		[XmlAttribute("name", Form=XmlSchemaForm.Unqualified)]
		public string SubCategoryName;
    
		/// <summary>
		/// The number of groups found within the category.
		/// </summary>
		[XmlAttribute("count", Form=XmlSchemaForm.Unqualified)]
		public int GroupCount;
	}

}