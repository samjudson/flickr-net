using System;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Returned by <see cref="Flickr.GroupsSearch(string)"/> methods.
	/// </summary>
    public sealed class GroupSearchResultCollection : System.Collections.ObjectModel.Collection<GroupSearchResult>, IFlickrParsable
	{
		/// <summary>
		/// The current page that the group search results represents.
		/// </summary>
        public int Page { get; private set; }

		/// <summary>
		/// The total number of pages this search would return.
		/// </summary>
        public int Pages { get; private set; }

		/// <summary>
		/// The number of groups returned per photo.
		/// </summary>
        public int PerPage { get; private set; }

		/// <summary>
		/// The total number of groups that where returned for the search.
		/// </summary>
        public int Total { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "groups")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "page":
                        Page = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "total":
                        Total = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "pages":
                        Pages = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        throw new Exception("Unknown attribute: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Read();

            while (reader.LocalName == "group")
            {
                GroupSearchResult r = new GroupSearchResult();
                ((IFlickrParsable)r).Load(reader);
                Add(r);
            }

            // Skip to next element (if any)
            reader.Skip();

        }
    }

	/// <summary>
	/// A class which encapsulates a single group search result.
	/// </summary>
	public sealed class GroupSearchResult : IFlickrParsable
	{
		/// <summary>
		/// The group id for the result.
		/// </summary>
        public string GroupId { get; private set; }
		/// <summary>
		/// The group name for the result.
		/// </summary>
        public string GroupName { get; private set; }
		/// <summary>
		/// True if the group is an over eighteen (adult) group only.
		/// </summary>
        public bool EighteenPlus { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "group")
                throw new FlickrException("Unknown element found: " + reader.LocalName);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "nsid":
                        GroupId = reader.Value;
                        break;
                    case "name":
                        GroupName = reader.Value;
                        break;
                    case "eighteenplus":
                        EighteenPlus = reader.Value == "1";
                        break;
                    default:
                        throw new Exception("Unknown attribute: " + reader.Name + "=" + reader.Value);

                }
            }

            reader.Skip();
        }
    }
}
