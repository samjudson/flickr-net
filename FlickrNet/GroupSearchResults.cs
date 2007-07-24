using System;
using System.Xml;
using System.Xml.XPath;

namespace FlickrNet
{
	/// <summary>
	/// Returned by <see cref="Flickr.GroupsSearch(string)"/> methods.
	/// </summary>
	public class GroupSearchResults
	{
		private int page;

		/// <summary>
		/// The current page that the group search results represents.
		/// </summary>
		public int Page { get { return page; } }

		private int pages;

		/// <summary>
		/// The total number of pages this search would return.
		/// </summary>
		public int Pages { get { return pages; } }

		private int perPage;

		/// <summary>
		/// The number of groups returned per photo.
		/// </summary>
		public int PerPage { get { return perPage; } }

		private int total;
		/// <summary>
		/// The total number of groups that where returned for the search.
		/// </summary>
		public int Total { get { return total; } }

		private GroupSearchResultCollection groups = new GroupSearchResultCollection();

		/// <summary>
		/// The collection of groups returned for this search.
		/// </summary>
		/// <example>
		/// The following code iterates through the list of groups returned:
		/// <code>
		/// GroupSearchResults results = flickr.GroupsSearch("test");
		/// foreach(GroupSearchResult result in results.Groups)
		/// {
		///		Console.WriteLine(result.GroupName);
		/// }
		/// </code>
		/// </example>
		public GroupSearchResultCollection Groups { get { return groups; } }

		internal GroupSearchResults(XmlElement element)
		{
			page = Convert.ToInt32(element.GetAttribute("page"));
			pages = Convert.ToInt32(element.GetAttribute("pages"));
			perPage = Convert.ToInt32(element.GetAttribute("perpage"));
			total = Convert.ToInt32(element.GetAttribute("total"));

			XmlNodeList gs = element.SelectNodes("group");
			groups.Clear();
			for(int i = 0; i < gs.Count; i++)
			{
				groups.Add(new GroupSearchResult(gs[i]));
			}
		}
	}

	/// <summary>
	/// Collection containing list of GroupSearchResult instances
	/// </summary>
	public class GroupSearchResultCollection : System.Collections.CollectionBase
	{
		/// <summary>
		/// Method for adding a new <see cref="GroupSearchResult"/> to the collection.
		/// </summary>
		/// <param name="result"></param>
		public void Add(GroupSearchResult result)
		{
			List.Add(result);
		}

		/// <summary>
		/// Method for adding a collection of <see cref="GroupSearchResult"/> objects (contained within a
		/// <see cref="GroupSearchResults"/> collection) to this collection.
		/// </summary>
		/// <param name="results"></param>
		public void AddRange(GroupSearchResultCollection results)
		{
			foreach(GroupSearchResult result in results)
				List.Add(result);
		}

		/// <summary>
		/// Return a particular <see cref="GroupSearchResult"/> based on the index.
		/// </summary>
		public GroupSearchResult this[int index]
		{
			get { return (GroupSearchResult)List[index]; }
			set { List[index] = value; }
		}

		/// <summary>
		/// Removes the selected result from the collection.
		/// </summary>
		/// <param name="result">The result to remove.</param>
		public void Remove(GroupSearchResult result)
		{
			List.Remove(result);
		}

		/// <summary>
		/// Checks if the collection contains the result.
		/// </summary>
		/// <param name="result">The result to see if the collection contains.</param>
		/// <returns>Returns true if the collecton contains the result, otherwise false.</returns>
		public bool Contains(GroupSearchResult result)
		{
			return List.Contains(result);
		}

		/// <summary>
		/// Copies the current collection to an array of <see cref="GroupSearchResult"/> objects.
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(GroupSearchResult[] array, int index)
		{
			List.CopyTo(array, index);
		}
	}

	/// <summary>
	/// A class which encapsulates a single group search result.
	/// </summary>
	public class GroupSearchResult
	{
		private string _groupId;
		private string _groupName;
		private bool _eighteen;

		/// <summary>
		/// The group id for the result.
		/// </summary>
		public string GroupId { get { return _groupId; } }
		/// <summary>
		/// The group name for the result.
		/// </summary>
		public string GroupName { get { return _groupName; } }
		/// <summary>
		/// True if the group is an over eighteen (adult) group only.
		/// </summary>
		public bool EighteenPlus { get { return _eighteen; } }

		internal GroupSearchResult(XmlNode node)
		{
			_groupId = node.Attributes["nsid"].Value;
			_groupName = node.Attributes["name"].Value;
			_eighteen = Convert.ToInt32(node.Attributes["eighteenplus"].Value)==1;
		}
	}
}
