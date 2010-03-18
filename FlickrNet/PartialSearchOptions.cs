using System;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for PartialSearchOptions.
	/// </summary>
	public class PartialSearchOptions
	{
		#region Private Variables 
		private DateTime _minUploadDate = DateTime.MinValue;
		private DateTime _maxUploadDate = DateTime.MinValue;
		private DateTime _minTakenDate = DateTime.MinValue;
		private DateTime _maxTakenDate = DateTime.MinValue;
		private PhotoSearchExtras _extras = PhotoSearchExtras.None;
		private PhotoSearchSortOrder _sort = PhotoSearchSortOrder.None;
		private int _perPage = 0;
		private int _page = 0;
		private PrivacyFilter _privacyFilter = PrivacyFilter.None;
		#endregion

		#region Public Properties
		/// <summary>
		/// Minimum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MinUploadDate
		{
			get { return _minUploadDate; }
			set { _minUploadDate = value; }
		}
	
		/// <summary>
		/// Maximum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MaxUploadDate
		{
			get { return _maxUploadDate; }
			set { _maxUploadDate = value; }
		}
	
		/// <summary>
		/// Minimum date taken. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MinTakenDate
		{
			get { return _minTakenDate; }
			set { _minTakenDate = value; }
		}
	
		/// <summary>
		/// Maximum date taken. Defaults to <see cref="DateTime.MinValue"/> which
		/// signifies that the value is not to be used.
		/// </summary>
		public DateTime MaxTakenDate
		{
			get { return _maxTakenDate; }
			set { _maxTakenDate = value; }
		}

		/// <summary>
		/// Optional extras to return, defaults to all. See <see cref="PhotoSearchExtras"/> for more details.
		/// </summary>
		public PhotoSearchExtras Extras
		{
			get { return _extras; }
			set { _extras = value; }
		}

		/// <summary>
		/// Number of photos to return per page. Defaults to 100.
		/// </summary>
		public int PerPage
		{
			get { return _perPage; }
			set { _perPage = value; }
		}

		/// <summary>
		/// The page to return. Defaults to page 1.
		/// </summary>
		public int Page
		{
			get { return _page; }
			set 
			{
				if( value < 0 ) throw new ArgumentOutOfRangeException("Page", "Must be greater than 0");
				_page = value; 
			}
		}

		/// <summary>
		/// The sort order of the returned list. Default is <see cref="PhotoSearchSortOrder.None"/>.
		/// </summary>
		public PhotoSearchSortOrder SortOrder
		{
			get { return _sort; }
			set { _sort = value; }
		}

		/// <summary>
		/// The privacy fitler to filter the search on.
		/// </summary>
		public PrivacyFilter PrivacyFilter
		{
			get { return _privacyFilter; }
			set { _privacyFilter = value; }
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Default constructor.
		/// </summary>
		public PartialSearchOptions()
		{
		}

		/// <summary>
		/// Constructor taking a default <see cref="PhotoSearchExtras"/> parameter.
		/// </summary>
		/// <param name="extras">See <see cref="PhotoSearchExtras"/> for more details.</param>
		public PartialSearchOptions(PhotoSearchExtras extras)
		{
			Extras = extras;
		}

		/// <summary>
		/// Constructor taking a perPage and page parameter.
		/// </summary>
		/// <param name="perPage">The number of photos to return per page (maximum).</param>
		/// <param name="page">The page number to return.</param>
		public PartialSearchOptions(int perPage, int page)
		{
			PerPage = perPage;
			Page = page;
		}

		/// <summary>
		/// Constructor taking a perPage and page parameter and a default <see cref="PhotoSearchExtras"/> parameter.
		/// </summary>
		/// <param name="perPage">The number of photos to return per page (maximum).</param>
		/// <param name="page">The page number to return.</param>
		/// <param name="extras">See <see cref="PhotoSearchExtras"/> for more details.</param>
		public PartialSearchOptions(int perPage, int page, PhotoSearchExtras extras)
		{
			PerPage = perPage;
			Page = page;
			Extras = extras;
		}
		#endregion

		internal PartialSearchOptions(PhotoSearchOptions options)
		{
			this.Extras = options.Extras;
			this.MaxTakenDate = options.MaxTakenDate;
			this.MinTakenDate = options.MinTakenDate;
			this.MaxUploadDate = options.MaxUploadDate;
			this.MinUploadDate = options.MinUploadDate;
			this.Page = options.Page;
			this.PerPage = options.PerPage;
			this.PrivacyFilter = options.PrivacyFilter;
		}

		internal string ExtrasString
		{
			get { return UtilityMethods.ExtrasToString(Extras); }
		}

		internal string SortOrderString
		{
			get	{ return UtilityMethods.SortOrderToString(SortOrder); }
		}

	}
}
