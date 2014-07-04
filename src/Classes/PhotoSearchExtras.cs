using System;
using System.ComponentModel;

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
    public enum PhotoSearchExtras : long
    {
        /// <summary>
        /// No extras selected.
        /// </summary>
        [Description("")]
        None = 0,

        /// <summary>
        /// Returns a license.
        /// </summary>
        [Description("license")]
        License = 1,

        /// <summary>
        /// Returned the date the photos was uploaded.
        /// </summary>
        [Description("date_upload")]
        DateUploaded = 2,

        /// <summary>
        /// Returned the date the photo was taken.
        /// </summary>
        [Description("date_taken")]
        DateTaken = 4,

        /// <summary>
        /// Returns the name of the owner of the photo.
        /// </summary>
        [Description("owner_name")]
        OwnerName = 8,

        /// <summary>
        /// Returns the server for the buddy icon for this user.
        /// </summary>
        [Description("icon_server")]
        IconServer = 16,

        /// <summary>
        /// Returns the extension for the original format of this photo.
        /// </summary>
        [Description("original_format")]
        OriginalFormat = 32,

        /// <summary>
        /// Returns the date the photo was last updated.
        /// </summary>
        [Description("last_update")]
        LastUpdated = 64,

        /// <summary>
        /// Returns Tags attribute
        /// </summary>
        [Description("tags")]
        Tags = 128,

        /// <summary>
        /// Geo-location information
        /// </summary>
        [Description("geo")]
        Geo = 256,

        /// <summary>
        /// Machine encoded tags
        /// </summary>
        [Description("machine_tags")]
        MachineTags = 512,

        /// <summary>
        /// Return the Dimensions of the Original Image.
        /// </summary>
        [Description("o_dims")]
        OriginalDimensions = 1024,

        /// <summary>
        /// Returns the number of views for a photo.
        /// </summary>
        [Description("views")]
        Views = 2048,

        /// <summary>
        /// Returns the media type of the photo, currently either 'photo' or 'video'.
        /// </summary>
        [Description("media")]
        Media = 4096,

        /// <summary>
        /// The path alias, if defined by the user (replaces the users NSID in the flickr URL for their photostream).
        /// </summary>
        [Description("path_alias")]
        PathAlias = 8192,

        /// <summary>
        /// Returns the URL for the square image, as well as the image size.
        /// </summary>
        [Description("url_sq")]
        SquareUrl = 16384,

        /// <summary>
        /// Returns the URL for the thumbnail image, as well as the image size.
        /// </summary>
        [Description("url_t")]
        ThumbnailUrl = 32768,

        /// <summary>
        /// Returns the URL for the small image, as well as the image size.
        /// </summary>
        [Description("url_s")]
        SmallUrl = 65536,

        /// <summary>
        /// Returns the URL for the medium image, as well as the image size.
        /// </summary>
        [Description("url_m")]
        MediumUrl = 131072,

        /// <summary>
        /// Returns the URL for the large image, as well as the image size.
        /// </summary>
        [Description("url_l")]
        LargeUrl = 262144,

        /// <summary>
        /// Returns the URL for the original image, as well as the image size.
        /// </summary>
        [Description("url_o")]
        OriginalUrl = 524288,

        /// <summary>
        /// Returns the description for the image.
        /// </summary>
        [Description("description")]
        Description = 1048576,

        /// <summary>
        /// Returns the details of CanBlog, CanDownload etc.
        /// </summary>
        [Description("usage")]
        Usage = 2097152,

        /// <summary>
        /// Returns the details for IsPublic, IsFamily and IsFriend.
        /// </summary>
        [Description("visibility")]
        Visibility = 4194304,

        /// <summary>
        /// Large (150x150) square image.
        /// </summary>
        [Description("url_q")]
        LargeSquareUrl = 8388608,

        /// <summary>
        /// Small (320 on longest side) image.
        /// </summary>
        [Description("url_n")]
        Small320Url = 16777216,

        /// <summary>
        /// Returns information on rotation of images compared to original
        /// </summary>
        [Description("rotation")]
        Rotation = 33554432,

        /// <summary>
        /// Large (1600 on largest size) image url.
        /// </summary>
        [Description("url_h")]
        Large1600Url = 33554432 * 2,

        /// <summary>
        /// Large (2048 on largest size) image url.
        /// </summary>
        [Description("url_k")]
        Large2048Url = 33554432 * 4,

        /// <summary>
        /// Medium (800 on largest size) image url.
        /// </summary>
        [Description("url_c")]
        Medium800Url = 33554432 * 8,

        /// <summary>
        /// Returns the URL for the medium 640 image, as well as the image size.
        /// </summary>
        [Description("url_z")]
        Medium640Url = 536870912,

        /// <summary>
        /// Returns the URL for the medium 640 image, as well as the image size.
        /// </summary>
        [Description("count_faves")]
        CountFaves = 1073741824,

        /// Returns the URL for the medium 640 image, as well as the image size.
        /// </summary>
        [Description("count_comments")]
        CountComments = 2147483648L,

        /// <summary>
        /// Returns the URL for all the images, as well as the image sizes.
        /// </summary>
        AllUrls =
            SquareUrl | ThumbnailUrl | SmallUrl | MediumUrl | Medium640Url | Medium800Url | LargeUrl | OriginalUrl |
            LargeSquareUrl | Small320Url | Large1600Url | Large2048Url,

        /// <summary>
        /// Returns all the above information.
        /// </summary>
        All =
            License | DateUploaded | DateTaken | OwnerName | IconServer | OriginalFormat | LastUpdated | Tags | Geo |
            MachineTags | OriginalDimensions | Views | Media | PathAlias | AllUrls | Description | Usage | Visibility |
            Rotation | CountFaves | CountComments,
    }
}
