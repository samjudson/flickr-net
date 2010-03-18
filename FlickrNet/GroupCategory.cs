using System;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Contains details of a category, including groups belonging to the category and sub categories.
	/// </summary>
	public class GroupCategory : IFlickrParsable
	{
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GroupCategory()
        {
            Subcategories = new List<Subcategory>();
            Groups = new List<Group>();
        }

		/// <summary>
		/// The name for the category.
		/// </summary>
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
		public string PathIds;

		/// <summary>
		/// An array of <see cref="Subcategory"/> items.
		/// </summary>
        public List<Subcategory> Subcategories { get; private set; }

		/// <summary>
		/// An array of <see cref="Group"/> items, listing the groups within this category.
		/// </summary>
        public List<Group> Groups { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "category")
                throw new Exception("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "name":
                        CategoryName = reader.Value;
                        break;
                    case "path":
                        Path = reader.Value;
                        break;
                    case "pathids":
                        PathIds = reader.Value;
                        break;
                    default:
                        throw new Exception("Unknown attribute: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "subcat" || reader.LocalName == "group")
            {
                if (reader.LocalName == "subcat")
                {
                    Subcategory c = new Subcategory();
                    ((IFlickrParsable)c).Load(reader);
                    Subcategories.Add(c);

                }
                else
                {
                    Group s = new Group();
                    ((IFlickrParsable)s).Load(reader);
                    Groups.Add(s);
                }
            }

            // Skip to next element (if any)
            reader.Skip();
        }
    }

	/// <summary>
	/// Holds details of a sub category, including its id, name and the number of groups in it.
	/// </summary>
	public class Subcategory: IFlickrParsable
	{
		/// <summary>
		/// The id of the category.
		/// </summary>
        public string SubcategoryId { get; private set; }
    
		/// <summary>
		/// The name of the category.
		/// </summary>
		public string SubcategoryName;
    
		/// <summary>
		/// The number of groups found within the category.
		/// </summary>
		public int GroupCount;

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            if (reader.LocalName != "category")
                throw new Exception("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        SubcategoryId = reader.Value;
                        break;
                    case "name":
                        SubcategoryName = reader.Value;
                        break;
                    case "count":
                        GroupCount = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new Exception("Unknown attribute: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();
        }
    }

}