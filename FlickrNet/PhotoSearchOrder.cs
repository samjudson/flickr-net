using System;

namespace FlickrNet
{
	/// <summary>
	/// The sort order for the <see cref="Flickr.PhotosSearch"/>,
	/// <see cref="Flickr.PhotosGetWithGeoData"/>, <see cref="Flickr.PhotosGetWithoutGeoData"/> methods.
	/// </summary>
	public enum PhotoSearchSortOrder
	{
		/// <summary>
		/// No sort order.
		/// </summary>
		None,
		/// <summary>
		/// Sort by date uploaded (posted).
		/// </summary>
		DatePostedAsc,
		/// <summary>
		/// Sort by date uploaded (posted) in descending order.
		/// </summary>
		DatePostedDesc,
		/// <summary>
		/// Sort by date taken.
		/// </summary>
		DateTakenAsc,
		/// <summary>
		/// Sort by date taken in descending order.
		/// </summary>
		DateTakenDesc,
		/// <summary>
		/// Sort by interestingness.
		/// </summary>
		InterestingnessAsc,
		/// <summary>
		/// Sort by interestingness in descending order.
		/// </summary>
		InterestingnessDesc,
		/// <summary>
		/// Sort by relevance
		/// </summary>
		Relevance
	}

}
