using System;

namespace FlickrNet
{
	/// <summary>
	/// The sort order for the <see cref="Flickr.PhotosSearch(PhotoSearchOptions)"/>,
	/// <see cref="Flickr.PhotosGetWithGeoData()"/>, <see cref="Flickr.PhotosGetWithoutGeoData()"/> methods.
	/// </summary>
	[Serializable]
	public enum PhotoSearchSortOrder
	{
		/// <summary>
		/// No sort order.
		/// </summary>
		None,
		/// <summary>
		/// Sort by date uploaded (posted).
		/// </summary>
		DatePostedAscending,
		/// <summary>
		/// Sort by date uploaded (posted) in descending order.
		/// </summary>
		DatePostedDescending,
		/// <summary>
		/// Sort by date taken.
		/// </summary>
		DateTakenAscending,
		/// <summary>
		/// Sort by date taken in descending order.
		/// </summary>
		DateTakenDescending,
		/// <summary>
		/// Sort by interestingness.
		/// </summary>
		InterestingnessAscending,
		/// <summary>
		/// Sort by interestingness in descending order.
		/// </summary>
		InterestingnessDescending,
		/// <summary>
		/// Sort by relevance
		/// </summary>
		Relevance
	}

}
