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
	[Serializable]
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
		/// Returns Tags attribute
		/// </summary>
		Tags = 128,
		/// <summary>
		/// Geo-location information
		/// </summary>
		Geo = 256,
		/// <summary>
		/// Machine encoded tags
		/// </summary>
		MachineTags = 512,
		/// <summary>
		/// Return the Dimensions of the Original Image.
		/// </summary>
		OriginalDimensions = 1024,
		/// <summary>
		/// Returns the number of views for a photo.
		/// </summary>
		Views = 2048,
		/// <summary>
		/// Returns the media type of the photo, currently either 'photo' or 'video'.
		/// </summary>
		Media = 4096,
        /// <summary>
        /// The path alias, if defined by the user (replaces the users NSID in the flickr URL for their photostream).
        /// </summary>
        PathAlias = 8192,
        /// <summary>
        /// Returns the URL for the square image, as well as the image size.
        /// </summary>
        SquareUrl = 16384,
        /// <summary>
        /// Returns the URL for the thumbnail image, as well as the image size.
        /// </summary>
        ThumbnailUrl = 32768,
        /// <summary>
        /// Returns the URL for the small image, as well as the image size.
        /// </summary>
        SmallUrl = 65536,
        /// <summary>
        /// Returns the URL for the medium image, as well as the image size.
        /// </summary>
        MediumUrl = 131072,
        /// <summary>
        /// Returns the URL for the large image, as well as the image size.
        /// </summary>
        LargeUrl = 262144,
        /// <summary>
        /// Returns the URL for the original image, as well as the image size.
        /// </summary>
        OriginalUrl = 524288,
        /// <summary>
        /// Returns the URL for all the images, as well as the image sizes.
        /// </summary>
        AllUrls = SquareUrl | ThumbnailUrl | SmallUrl | MediumUrl | LargeUrl | OriginalUrl,
        /// <summary>
        /// Returns the description for the image.
        /// </summary>
        Description = 524288 * 2,
		/// <summary>
		/// Returns all the above information.
		/// </summary>
		All = License | DateUploaded | DateTaken | OwnerName | IconServer | OriginalFormat | LastUpdated | Tags | Geo | MachineTags | OriginalDimensions | Views | Media | PathAlias | AllUrls | Description
	}
}
