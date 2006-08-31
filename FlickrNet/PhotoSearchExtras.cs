using System;

namespace FlickrNet
{
	/// <summary>
	/// Which photo search extras to be included. Can be combined to include more than one
	/// value.
	/// </summary>
	/// <example>
	/// The following code sets options to return both the license and owner name along with
	/// the other search results.
	/// <code>
	/// PhotoSearchOptions options = new PhotoSearchOptions();
	/// options.Extras = PhotoSearchExtras.License &amp; PhotoSearchExtras.OwnerName
	/// </code>
	/// </example>
	[Flags]
	public enum PhotoSearchExtras
	{
		/// <summary>
		/// No extras selected.
		/// </summary>
		None = 0,
		/// <summary>
		/// Returns a license.
		/// </summary>
		License = 1,
		/// <summary>
		/// Returned the date the photos was uploaded.
		/// </summary>
		DateUploaded = 2,
		/// <summary>
		/// Returned the date the photo was taken.
		/// </summary>
		DateTaken = 4,
		/// <summary>
		/// Returns the name of the owner of the photo.
		/// </summary>
		OwnerName = 8,
		/// <summary>
		/// Returns the server for the buddy icon for this user.
		/// </summary>
		IconServer = 16,
		/// <summary>
		/// Returns the extension for the original format of this photo.
		/// </summary>
		OriginalFormat = 32,
		/// <summary>
		/// Returns the date the photo was last updated.
		/// </summary>
		LastUpdated = 64,
		/// <summary>
		/// Undocumented Tags extra
		/// </summary>
		Tags = 128,
		/// <summary>
		/// Geo-location information
		/// </summary>
		Geo = 256,
		/// <summary>
		/// Returns all the above information.
		/// </summary>
		All = License | DateUploaded | DateTaken | OwnerName | IconServer | OriginalFormat | LastUpdated | Tags | Geo
	}
}
