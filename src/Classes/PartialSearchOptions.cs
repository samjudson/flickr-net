using System;

namespace FlickrNet
{
    /// <summary>
    /// Summary description for PartialSearchOptions.
    /// </summary>
    public class PartialSearchOptions
    {
        private PhotoSearchExtras extras = PhotoSearchExtras.None;
        private DateTime maxTakenDate = DateTime.MinValue;
        private DateTime maxUploadDate = DateTime.MinValue;
        private DateTime minTakenDate = DateTime.MinValue;
        private DateTime minUploadDate = DateTime.MinValue;
        private int page;
        private PrivacyFilter privacyFilter = PrivacyFilter.None;
        private PhotoSearchSortOrder sort = PhotoSearchSortOrder.None;

        public static PartialSearchOptions Empty
        {
            get { return new PartialSearchOptions(); }
        }

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

        internal PartialSearchOptions(PhotoSearchOptions options)
        {
            Extras = options.Extras;
            MaxTakenDate = options.MaxTakenDate;
            MinTakenDate = options.MinTakenDate;
            MaxUploadDate = options.MaxUploadDate;
            MinUploadDate = options.MinUploadDate;
            Page = options.Page;
            PerPage = options.PerPage;
            PrivacyFilter = options.PrivacyFilter;
        }

        /// <summary>
        /// Minimum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MinUploadDate
        {
            get { return minUploadDate; }
            set { minUploadDate = value; }
        }

        /// <summary>
        /// Maximum date uploaded. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MaxUploadDate
        {
            get { return maxUploadDate; }
            set { maxUploadDate = value; }
        }

        /// <summary>
        /// Minimum date taken. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MinTakenDate
        {
            get { return minTakenDate; }
            set { minTakenDate = value; }
        }

        /// <summary>
        /// Maximum date taken. Defaults to <see cref="DateTime.MinValue"/> which
        /// signifies that the value is not to be used.
        /// </summary>
        public DateTime MaxTakenDate
        {
            get { return maxTakenDate; }
            set { maxTakenDate = value; }
        }

        /// <summary>
        /// Optional extras to return, defaults to all. See <see cref="PhotoSearchExtras"/> for more details.
        /// </summary>
        public PhotoSearchExtras Extras
        {
            get { return extras; }
            set { extras = value; }
        }

        /// <summary>
        /// Number of photos to return per page. Defaults to 100.
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// The page to return. Defaults to page 1.
        /// </summary>
        public int Page
        {
            get { return page; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value", "Must be greater than 0");
                page = value;
            }
        }

        /// <summary>
        /// The sort order of the returned list. Default is <see cref="PhotoSearchSortOrder.None"/>.
        /// </summary>
        public PhotoSearchSortOrder SortOrder
        {
            get { return sort; }
            set { sort = value; }
        }

        /// <summary>
        /// The privacy fitler to filter the search on.
        /// </summary>
        public PrivacyFilter PrivacyFilter
        {
            get { return privacyFilter; }
            set { privacyFilter = value; }
        }

        internal string ExtrasString
        {
            get { return UtilityMethods.ExtrasToString(Extras); }
        }

        internal string SortOrderString
        {
            get { return UtilityMethods.SortOrderToString(SortOrder); }
        }
    }
}