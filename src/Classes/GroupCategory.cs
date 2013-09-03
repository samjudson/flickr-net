using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// Contains details of a category, including groups belonging to the category and sub categories.
    /// </summary>
    public sealed class GroupCategory : IFlickrParsable
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GroupCategory()
        {
            Subcategories = new Collection<Subcategory>();
            Groups = new Collection<Group>();
        }

        /// <summary>
        /// The name for the category.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// A forward slash delimited list of the parents of the current group.
        /// </summary>
        /// <remarks>
        /// Can be matched against the list of PathIds to jump directly to a parent group.
        /// </remarks>
        /// <example>
        /// Group Id 91, Romance will return "/Life/Romance" as the Path and "/90/91" as its PathIds
        /// </example>
        public string Path { get; set; }

        /// <summary>
        /// A forward slash delimited list of the ids of the parents of the current group.
        /// </summary>
        /// <remarks>
        /// Can be matched against the Path to jump directly to a parent group.
        /// </remarks>
        /// <example>
        /// Group Id 91, Romance will return "/Life/Romance" as the Path and "/90/91" as its PathIds
        /// </example>
        public string PathIds { get; set; }

        /// <summary>
        /// An array of <see cref="Subcategory"/> items.
        /// </summary>
        public Collection<Subcategory> Subcategories { get; set; }

        /// <summary>
        /// An array of <see cref="Group"/> items, listing the groups within this category.
        /// </summary>
        public Collection<Group> Groups { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "category")
                UtilityMethods.CheckParsingException(reader);

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
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName == "subcat" || reader.LocalName == "group")
            {
                if (reader.LocalName == "subcat")
                {
                    var c = new Subcategory();
                    ((IFlickrParsable) c).Load(reader);
                    Subcategories.Add(c);
                }
                else
                {
                    var s = new Group();
                    ((IFlickrParsable) s).Load(reader);
                    Groups.Add(s);
                }
            }

            // Skip to next element (if any)
            reader.Skip();
        }

        #endregion
    }

    /// <summary>
    /// Holds details of a sub category, including its id, name and the number of groups in it.
    /// </summary>
    public sealed class Subcategory : IFlickrParsable
    {
        /// <summary>
        /// The id of the category.
        /// </summary>
        public string SubcategoryId { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string SubcategoryName { get; set; }

        /// <summary>
        /// The number of groups found within the category.
        /// </summary>
        public int GroupCount { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "category")
                UtilityMethods.CheckParsingException(reader);

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
                        GroupCount = int.Parse(reader.Value, NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }

        #endregion
    }
}