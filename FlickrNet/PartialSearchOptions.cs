using System;

namespace FlickrNet
{
    /// <summary>
    /// Summary description for PartialSearchOptions.
    /// </summary>
    public class PartialSearchOptions
    {
        private DateTime minUploadDate = DateTime.MinValue;
        private DateTime maxUploadDate = DateTime.MinValue;
        private DateTime minTakenDate = DateTime.MinValue;
        private DateTime maxTakenDate = DateTime.MinValue;
        private PhotoSearchExtras extras = PhotoSearchExtras.None;
        private PhotoSearchSortOrder sort = PhotoSearchSortOrder.None;
        private int perPage;
        private int page;
        private PrivacyFilter privacyFilter = PrivacyFilter.None;

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
        public int PerPage
        {
            get { return perPage; }
            set { perPage = value; }
        }

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
            get { return UtilityMethods.SortOrderToString(SortOrder); }
        }

    }
}
