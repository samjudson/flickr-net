using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Add a selection of tags to a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo.</param>
        /// <param name="tags">An array of strings containing the tags.</param>
        /// <returns>True if the tags are added successfully.</returns>
        public void PhotosAddTags(string photoId, string[] tags)
        {
            string s = string.Join(",", tags);
            PhotosAddTags(photoId, s);
        }

        /// <summary>
        /// Add a selection of tags to a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo.</param>
        /// <param name="tags">An string of comma delimited tags.</param>
        /// <returns>True if the tags are added successfully.</returns>
        public void PhotosAddTags(string photoId, string tags)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.addTags");
            parameters.Add("photo_id", photoId);
            parameters.Add("tags", tags);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Delete a photo from Flickr.
        /// </summary>
        /// <remarks>
        /// Requires Delete permissions. Also note, photos cannot be recovered once deleted.</remarks>
        /// <param name="photoId">The ID of the photo to delete.</param>
        public void PhotosDelete(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.delete");
            parameters.Add("photo_id", photoId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Get all the contexts (group, set and photostream 'next' and 'previous'
        /// pictures) for a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo to get the contexts for.</param>
        /// <returns>An instance of the <see cref="AllContexts"/> class.</returns>
        public AllContexts PhotosGetAllContexts(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getAllContexts");
            parameters.Add("photo_id", photoId);

            return GetResponseCache<AllContexts>(parameters);
        }

        /// <summary>
        /// Gets the most recent 10 photos from your contacts.
        /// </summary>
        /// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
        public PhotoCollection PhotosGetContactsPhotos()
        {
            return PhotosGetContactsPhotos(0, false, false, false, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets the most recent photos from your contacts.
        /// </summary>
        /// <remarks>Returns the most recent photos from all your contact, excluding yourself.</remarks>
        /// <param name="count">The number of photos to return, from between 10 and 50.</param>
        /// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws a <see cref="ArgumentOutOfRangeException"/> exception if the cound
        /// is not between 10 and 50, or 0.</exception>
        public PhotoCollection PhotosGetContactsPhotos(int count)
        {
            return PhotosGetContactsPhotos(count, false, false, false, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets your contacts most recent photos.
        /// </summary>
        /// <param name="count">The number of photos to return, from between 10 and 50.</param>
        /// <param name="justFriends">If true only returns photos from contacts marked as
        /// 'friends'.</param>
        /// <param name="singlePhoto">If true only returns a single photo for each of your contacts.
        /// Ignores the count if this is true.</param>
        /// <param name="includeSelf">If true includes yourself in the group of people to 
        /// return photos for.</param>
        /// <param name="extras">Optional extras that can be returned by this call.</param>
        /// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws a <see cref="ArgumentOutOfRangeException"/> exception if the cound
        /// is not between 10 and 50, or 0.</exception>
        public PhotoCollection PhotosGetContactsPhotos(int count, bool justFriends, bool singlePhoto, bool includeSelf, PhotoSearchExtras extras)
        {
            CheckRequiresAuthentication();

            if (count != 0 && (count < 10 || count > 50) && !singlePhoto)
            {
                throw new ArgumentOutOfRangeException("count", String.Format(System.Globalization.CultureInfo.InvariantCulture, "Count must be between 10 and 50. ({0})", count));
            }
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getContactsPhotos");
            if (count > 0 && !singlePhoto) parameters.Add("count", count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (justFriends) parameters.Add("just_friends", "1");
            if (singlePhoto) parameters.Add("single_photo", "1");
            if (includeSelf) parameters.Add("include_self", "1");
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <returns>A <see cref="PhotoCollection"/> object containing details of the photos returned.</returns>
        public PhotoCollection PhotosGetContactsPublicPhotos(string userId)
        {
            return PhotosGetContactsPublicPhotos(userId, 0, false, false, false, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="extras">A list of extra details to return for each photo.</param>
        /// <returns>A <see cref="PhotoCollection"/> object containing details of the photos returned.</returns>
        public PhotoCollection PhotosGetContactsPublicPhotos(string userId, PhotoSearchExtras extras)
        {
            return PhotosGetContactsPublicPhotos(userId, 0, false, false, false, extras);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
        /// <returns>A <see cref="PhotoCollection"/> object containing details of the photos returned.</returns>
        public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int count)
        {
            return PhotosGetContactsPublicPhotos(userId, count, false, false, false, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
        /// <param name="extras">A list of extra details to return for each photo.</param>
        /// <returns>A <see cref="PhotoCollection"/> object containing details of the photos returned.</returns>
        public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int count, PhotoSearchExtras extras)
        {
            return PhotosGetContactsPublicPhotos(userId, count, false, false, false, extras);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
        /// <param name="justFriends">True to just return photos from friends and family (excluding regular contacts).</param>
        /// <param name="singlePhoto">True to return just a single photo for each contact.</param>
        /// <param name="includeSelf">True to include photos from the user ID specified as well.</param>
        /// <returns></returns>
        public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int count, bool justFriends, bool singlePhoto, bool includeSelf)
        {
            return PhotosGetContactsPublicPhotos(userId, count, justFriends, singlePhoto, includeSelf, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
        /// <param name="justFriends">True to just return photos from friends and family (excluding regular contacts).</param>
        /// <param name="singlePhoto">True to return just a single photo for each contact.</param>
        /// <param name="includeSelf">True to include photos from the user ID specified as well.</param>
        /// <param name="extras">A list of extra details to return for each photo.</param>
        /// <returns></returns>
        public PhotoCollection PhotosGetContactsPublicPhotos(string userId, int count, bool justFriends, bool singlePhoto, bool includeSelf, PhotoSearchExtras extras)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getContactsPublicPhotos");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);
            if (count > 0) parameters.Add("count", count.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (justFriends) parameters.Add("just_friends", "1");
            if (singlePhoto) parameters.Add("single_photo", "1");
            if (includeSelf) parameters.Add("include_self", "1");
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Gets the context of the photo in the users photostream.
        /// </summary>
        /// <param name="photoId">The ID of the photo to return the context for.</param>
        /// <returns></returns>
        public Context PhotosGetContext(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getContext");
            parameters.Add("photo_id", photoId);

            return GetResponseCache<Context>(parameters);
        }

        /// <summary>
        /// Returns count of photos between each pair of dates in the list.
        /// </summary>
        /// <remarks>If you pass in DateA, DateB and DateC it returns
        /// a list of the number of photos between DateA and DateB,
        /// followed by the number between DateB and DateC. 
        /// More parameters means more sets.</remarks>
        /// <param name="dates">Array of <see cref="DateTime"/> objects.</param>
        /// <returns><see cref="PhotoCountCollection"/> class instance.</returns>
        public PhotoCountCollection PhotosGetCounts(DateTime[] dates)
        {
            return PhotosGetCounts(dates, false);
        }

        /// <summary>
        /// Returns count of photos between each pair of dates in the list.
        /// </summary>
        /// <remarks>If you pass in DateA, DateB and DateC it returns
        /// a list of the number of photos between DateA and DateB,
        /// followed by the number between DateB and DateC. 
        /// More parameters means more sets.</remarks>
        /// <param name="dates">Array of <see cref="DateTime"/> objects.</param>
        /// <param name="taken">Boolean parameter to specify if the dates are the taken date, or uploaded date.</param>
        /// <returns><see cref="PhotoCountCollection"/> class instance.</returns>
        public PhotoCountCollection PhotosGetCounts(DateTime[] dates, bool taken)
        {
            if (taken)
                return PhotosGetCounts(null, dates);
            else
                return PhotosGetCounts(dates, null);
        }

        /// <summary>
        /// Returns count of photos between each pair of dates in the list.
        /// </summary>
        /// <remarks>If you pass in DateA, DateB and DateC it returns
        /// a list of the number of photos between DateA and DateB,
        /// followed by the number between DateB and DateC. 
        /// More parameters means more sets.</remarks>
        /// <param name="dates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
        /// <param name="takenDates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
        /// <returns><see cref="PhotoCountCollection"/> class instance.</returns>
        public PhotoCountCollection PhotosGetCounts(DateTime[] dates, DateTime[] takenDates)
        {
            string dateString = null;
            string takenDateString = null;

            if (dates != null && dates.Length > 0)
            {
                Array.Sort<DateTime>(dates);
                dateString = String.Join(",", new List<DateTime>(dates).ConvertAll<string>(new Converter<DateTime, string>(delegate(DateTime d) { return UtilityMethods.DateToUnixTimestamp(d).ToString(); })).ToArray());
            }

            if (takenDates != null && takenDates.Length > 0)
            {
                Array.Sort<DateTime>(takenDates);
                takenDateString = String.Join(",", new List<DateTime>(takenDates).ConvertAll<string>(new Converter<DateTime, string>(delegate(DateTime d) { return UtilityMethods.DateToUnixTimestamp(d).ToString(); })).ToArray());
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getCounts");
            if (dateString != null && dateString.Length > 0) parameters.Add("dates", dateString);
            if (takenDateString != null && takenDateString.Length > 0) parameters.Add("taken_dates", takenDateString);

            return GetResponseCache<PhotoCountCollection>(parameters);
        }

        /// <summary>
        /// Gets the EXIF data for a given Photo ID.
        /// </summary>
        /// <param name="photoId">The Photo ID of the photo to return the EXIF data for.</param>
        /// <returns>An instance of the <see cref="ExifTagCollection"/> class containing the EXIF data.</returns>
        public ExifTagCollection PhotosGetExif(string photoId)
        {
            return PhotosGetExif(photoId, null);
        }

        /// <summary>
        /// Gets the EXIF data for a given Photo ID.
        /// </summary>
        /// <param name="photoId">The Photo ID of the photo to return the EXIF data for.</param>
        /// <param name="secret">The secret of the photo. If the secret is specified then
        /// authentication checks are bypassed.</param>
        /// <returns>An instance of the <see cref="ExifTagCollection"/> class containing the EXIF data.</returns>
        public ExifTagCollection PhotosGetExif(string photoId, string secret)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getExif");
            parameters.Add("photo_id", photoId);
            if (secret != null) parameters.Add("secret", secret);

            return GetResponseCache<ExifTagCollection>(parameters);
        }

        /// <summary>
        /// Get information about a photo. The calling user must have permission to view the photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to fetch information for.</param>
        /// <returns>A <see cref="PhotoInfo"/> class detailing the properties of the photo.</returns>
        public PhotoInfo PhotosGetInfo(string photoId)
        {
            return PhotosGetInfo(photoId, null);
        }

        /// <summary>
        /// Get information about a photo. The calling user must have permission to view the photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to fetch information for.</param>
        /// <param name="secret">The secret for the photo. If the correct secret is passed then permissions checking is skipped. This enables the 'sharing' of individual photos by passing around the id and secret.</param>
        /// <returns>A <see cref="PhotoInfo"/> class detailing the properties of the photo.</returns>
        public PhotoInfo PhotosGetInfo(string photoId, string secret)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getInfo");
            parameters.Add("photo_id", photoId);
            if (secret != null) parameters.Add("secret", secret);

            return GetResponseCache<PhotoInfo>(parameters);
        }

        /// <summary>
        /// Get permissions for a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to get permissions for.</param>
        /// <returns>An instance of the <see cref="PhotoPermissions"/> class containing the permissions of the specified photo.</returns>
        public PhotoPermissions PhotosGetPerms(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getPerms");
            parameters.Add("photo_id", photoId);

            return GetResponseCache<PhotoPermissions>(parameters);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetRecent()
        {
            return PhotosGetRecent(0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetRecent(PhotoSearchExtras extras)
        {
            return PhotosGetRecent(0, 0, extras);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetRecent(int page, int perPage)
        {
            return PhotosGetRecent(page, perPage, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetRecent(int page, int perPage, PhotoSearchExtras extras)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getRecent");
            parameters.Add("api_key", _apiKey);
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Returns the available sizes for a photo. The calling user must have permission to view the photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to fetch size information for.</param>
        /// <returns>A list of <see cref="Size"/> objects.</returns>
        public SizeCollection PhotosGetSizes(string photoId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getSizes");
            parameters.Add("photo_id", photoId);

            return GetResponseCache<SizeCollection>(parameters);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetUntagged()
        {
            return PhotosGetUntagged(0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetUntagged(PhotoSearchExtras extras)
        {
            return PhotosGetUntagged(0, 0, extras);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetUntagged(int page, int perPage)
        {
            return PhotosGetUntagged(page, perPage, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="PhotoCollection"/> class containing the list of photos.</returns>
        public PhotoCollection PhotosGetUntagged(int page, int perPage, PhotoSearchExtras extras)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getUntagged");
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Gets a list of photos not in sets. Defaults to include all extra fields.
        /// </summary>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        public PhotoCollection PhotosGetNotInSet()
        {
            return PhotosGetNotInSet(new PartialSearchOptions());
        }

        /// <summary>
        /// Gets a specific page of the list of photos which are not in sets.
        /// Defaults to include all extra fields.
        /// </summary>
        /// <param name="page">The page number to return.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        public PhotoCollection PhotosGetNotInSet(int page)
        {
            return PhotosGetNotInSet(page, 0, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets a specific page of the list of photos which are not in sets.
        /// Defaults to include all extra fields.
        /// </summary>
        /// <param name="perPage">Number of photos per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        public PhotoCollection PhotosGetNotInSet(int page, int perPage)
        {
            return PhotosGetNotInSet(page, perPage, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets a list of a users photos which are not in a set.
        /// </summary>
        /// <param name="perPage">Number of photos per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras"><see cref="PhotoSearchExtras"/> enumeration.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        public PhotoCollection PhotosGetNotInSet(int page, int perPage, PhotoSearchExtras extras)
        {
            PartialSearchOptions options = new PartialSearchOptions();
            options.PerPage = perPage;
            options.Page = page;
            options.Extras = extras;

            return PhotosGetNotInSet(options);
        }

        /// <summary>
        /// Gets a list of the authenticated users photos which are not in a set.
        /// </summary>
        /// <param name="options">A selection of options to filter/sort by.</param>
        /// <returns>A collection of photos in the <see cref="PhotoCollection"/> class.</returns>
        public PhotoCollection PhotosGetNotInSet(PartialSearchOptions options)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getNotInSet");
            UtilityMethods.PartialOptionsIntoArray(options, parameters);

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Gets a list of all current licenses.
        /// </summary>
        /// <returns><see cref="LicenseCollection"/> instance.</returns>
        public LicenseCollection PhotosLicensesGetInfo()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.licenses.getInfo");
            parameters.Add("api_key", _apiKey);

            return GetResponseCache<LicenseCollection>(parameters);
        }

        /// <summary>
        /// Remove an existing tag.
        /// </summary>
        /// <param name="tagId">The id of the tag, as returned by <see cref="Flickr.PhotosGetInfo(string)"/> or similar method.</param>
        /// <returns>True if the tag was removed.</returns>
        public void PhotosRemoveTag(string tagId)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.removeTag");
            parameters.Add("tag_id", tagId);

            GetResponseCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Return a list of your photos that have been recently created or which have been recently modified.
        /// Recently modified may mean that the photo's metadata (title, description, tags)
        /// may have been changed or a comment has been added (or just modified somehow :-)
        /// </summary>
        /// <param name="minDate">The date from which modifications should be compared.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <returns>Returns a <see cref="PhotoCollection"/> instance containing the list of photos.</returns>
        public PhotoCollection PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras)
        {
            return PhotosRecentlyUpdated(minDate, extras, 0, 0);
        }

        /// <summary>
        /// Return a list of your photos that have been recently created or which have been recently modified.
        /// Recently modified may mean that the photo's metadata (title, description, tags)
        /// may have been changed or a comment has been added (or just modified somehow :-)
        /// </summary>
        /// <param name="minDate">The date from which modifications should be compared.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <returns>Returns a <see cref="PhotoCollection"/> instance containing the list of photos.</returns>
        public PhotoCollection PhotosRecentlyUpdated(DateTime minDate, int page, int perPage)
        {
            return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, page, perPage);
        }

        /// <summary>
        /// Return a list of your photos that have been recently created or which have been recently modified.
        /// Recently modified may mean that the photo's metadata (title, description, tags)
        /// may have been changed or a comment has been added (or just modified somehow :-)
        /// </summary>
        /// <param name="minDate">The date from which modifications should be compared.</param>
        /// <returns>Returns a <see cref="PhotoCollection"/> instance containing the list of photos.</returns>
        public PhotoCollection PhotosRecentlyUpdated(DateTime minDate)
        {
            return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, 0, 0);
        }

        /// <summary>
        /// Return a list of your photos that have been recently created or which have been recently modified.
        /// Recently modified may mean that the photo's metadata (title, description, tags)
        /// may have been changed or a comment has been added (or just modified somehow :-)
        /// </summary>
        /// <param name="minDate">The date from which modifications should be compared.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <returns>Returns a <see cref="PhotoCollection"/> instance containing the list of photos.</returns>
        public PhotoCollection PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.recentlyUpdated");
            parameters.Add("min_date", UtilityMethods.DateToUnixTimestamp(minDate));
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", UtilityMethods.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Search for a set of photos, based on the value of the <see cref="PhotoSearchOptions"/> parameters.
        /// </summary>
        /// <param name="options">The parameters to search for.</param>
        /// <returns>A collection of photos contained within a <see cref="PhotoCollection"/> object.</returns>
        public PhotoCollection PhotosSearch(PhotoSearchOptions options)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.search");
            if (options.UserId != null && options.UserId.Length > 0) parameters.Add("user_id", options.UserId);
            if (options.GroupId != null && options.GroupId.Length > 0) parameters.Add("group_id", options.GroupId);
            if (options.Text != null && options.Text.Length > 0) parameters.Add("text", options.Text);
            if (options.Tags != null && options.Tags.Length > 0) parameters.Add("tags", options.Tags);
            if (options.TagMode != TagMode.None) parameters.Add("tag_mode", options.TagModeString);
            if (options.MachineTags != null && options.MachineTags.Length > 0) parameters.Add("machine_tags", options.MachineTags);
            if (options.MachineTagMode != MachineTagMode.None) parameters.Add("machine_tag_mode", options.MachineTagModeString);
            if (options.MinUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(options.MinUploadDate).ToString());
            if (options.MaxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(options.MaxUploadDate).ToString());
            if (options.MinTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", options.MinTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (options.MaxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", options.MaxTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (options.Licenses.Count != 0)
            {
                List<string> licenseArray = new List<string>();
                foreach (LicenseType license in options.Licenses)
                {
                    licenseArray.Add(license.ToString("d"));
                }
                parameters.Add("license", String.Join(",", licenseArray.ToArray()));
            }
            if (options.PerPage != 0) parameters.Add("per_page", options.PerPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (options.Page != 0) parameters.Add("page", options.Page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (options.Extras != PhotoSearchExtras.None) parameters.Add("extras", options.ExtrasString);
            if (options.SortOrder != PhotoSearchSortOrder.None) parameters.Add("sort", options.SortOrderString);
            if (options.PrivacyFilter != PrivacyFilter.None) parameters.Add("privacy_filter", options.PrivacyFilter.ToString("d"));
            if (options.BoundaryBox.IsSet) parameters.Add("bbox", options.BoundaryBox.ToString());
            if (options.Accuracy != GeoAccuracy.None) parameters.Add("accuracy", options.Accuracy.ToString("d"));
            if (options.SafeSearch != SafetyLevel.None) parameters.Add("safe_search", options.SafeSearch.ToString("d"));
            if (options.ContentType != ContentTypeSearch.None) parameters.Add("content_type", options.ContentType.ToString("d"));
            if (options.HasGeo) parameters.Add("has_geo", "1");
            if (!float.IsNaN(options.Latitude)) parameters.Add("lat", options.Latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (!float.IsNaN(options.Longitude)) parameters.Add("lon", options.Longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (!float.IsNaN(options.Radius)) parameters.Add("radius", options.Radius.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (options.RadiusUnits != RadiusUnit.None) parameters.Add("radius_units", (options.RadiusUnits == RadiusUnit.Miles ? "mi" : "km"));
            if (options.Contacts != ContactSearch.None) parameters.Add("contacts", (options.Contacts == ContactSearch.AllContacts ? "all" : "ff"));
            if (options.WoeId != null) parameters.Add("woe_id", options.WoeId);
            if (options.PlaceId != null) parameters.Add("place_id", options.PlaceId);
            if (options.IsCommons) parameters.Add("is_commons", "1");

            return GetResponseCache<PhotoCollection>(parameters);
        }

        /// <summary>
        /// Set the date taken for a photo.
        /// </summary>
        /// <remarks>
        /// All dates are assumed to be GMT. It is the developers responsibility to change dates to the local users 
        /// timezone.
        /// </remarks>
        /// <param name="photoId">The id of the photo to set the date taken for.</param>
        /// <param name="dateTaken">The date taken.</param>
        /// <param name="granularity">The granularity of the date taken.</param>
        /// <returns>True if the date was updated successfully.</returns>
        public void PhotosSetDates(string photoId, DateTime dateTaken, DateGranularity granularity)
        {
            PhotosSetDates(photoId, DateTime.MinValue, dateTaken, granularity);
        }

        /// <summary>
        /// Set the date the photo was posted (uploaded). This will affect the order in which photos
        /// are seen in your photostream.
        /// </summary>
        /// <remarks>
        /// All dates are assumed to be GMT. It is the developers responsibility to change dates to the local users 
        /// timezone.
        /// </remarks>
        /// <param name="photoId">The id of the photo to set the date posted.</param>
        /// <param name="datePosted">The new date to set the date posted too.</param>
        /// <returns>True if the date was updated successfully.</returns>
        public void PhotosSetDates(string photoId, DateTime datePosted)
        {
            PhotosSetDates(photoId, datePosted, DateTime.MinValue, DateGranularity.FullDate);
        }

        /// <summary>
        /// Set the date the photo was posted (uploaded) and the date the photo was taken.
        /// Changing the date posted will affect the order in which photos are seen in your photostream.
        /// </summary>
        /// <remarks>
        /// All dates are assumed to be GMT. It is the developers responsibility to change dates to the local users 
        /// timezone.
        /// </remarks>
        /// <param name="photoId">The id of the photo to set the dates.</param>
        /// <param name="datePosted">The new date to set the date posted too.</param>
        /// <param name="dateTaken">The new date to set the date taken too.</param>
        /// <param name="granularity">The granularity of the date taken.</param>
        /// <returns>True if the dates where updated successfully.</returns>
        public void PhotosSetDates(string photoId, DateTime datePosted, DateTime dateTaken, DateGranularity granularity)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.setDates");
            parameters.Add("photo_id", photoId);
            if (datePosted != DateTime.MinValue) parameters.Add("date_posted", UtilityMethods.DateToUnixTimestamp(datePosted).ToString());
            if (dateTaken != DateTime.MinValue)
            {
                parameters.Add("date_taken", dateTaken.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                parameters.Add("date_taken_granularity", granularity.ToString("d"));
            }

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Sets the title and description of the photograph.
        /// </summary>
        /// <param name="photoId">The numerical photoId of the photograph.</param>
        /// <param name="title">The new title of the photograph.</param>
        /// <param name="description">The new description of the photograph.</param>
        /// <returns>True when the operation is successful.</returns>
        /// <exception cref="FlickrApiException">Thrown when the photo id cannot be found.</exception>
        public void PhotosSetMeta(string photoId, string title, string description)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.setMeta");
            parameters.Add("photo_id", photoId);
            parameters.Add("title", title);
            parameters.Add("description", description);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Set the permissions on a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to update.</param>
        /// <param name="isPublic">1 if the photo is public, 0 if it is not.</param>
        /// <param name="isFriend">1 if the photo is viewable by friends, 0 if it is not.</param>
        /// <param name="isFamily">1 if the photo is viewable by family, 0 if it is not.</param>
        /// <param name="permComment">Who can add comments. See <see cref="PermissionComment"/> for more details.</param>
        /// <param name="permAddMeta">Who can add metadata (notes and tags). See <see cref="PermissionAddMeta"/> for more details.</param>
        public void PhotosSetPerms(string photoId, int isPublic, int isFriend, int isFamily, PermissionComment permComment, PermissionAddMeta permAddMeta)
        {
            PhotosSetPerms(photoId, (isPublic == 1), (isFriend == 1), (isFamily == 1), permComment, permAddMeta);
        }

        /// <summary>
        /// Set the permissions on a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to update.</param>
        /// <param name="isPublic">True if the photo is public, False if it is not.</param>
        /// <param name="isFriend">True if the photo is viewable by friends, False if it is not.</param>
        /// <param name="isFamily">True if the photo is viewable by family, False if it is not.</param>
        /// <param name="permComment">Who can add comments. See <see cref="PermissionComment"/> for more details.</param>
        /// <param name="permAddMeta">Who can add metadata (notes and tags). See <see cref="PermissionAddMeta"/> for more details.</param>
        public void PhotosSetPerms(string photoId, bool isPublic, bool isFriend, bool isFamily, PermissionComment permComment, PermissionAddMeta permAddMeta)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.setPerms");
            parameters.Add("photo_id", photoId);
            parameters.Add("is_public", (isPublic ? "1" : "0"));
            parameters.Add("is_friend", (isFriend ? "1" : "0"));
            parameters.Add("is_family", (isFamily ? "1" : "0"));
            parameters.Add("perm_comment", permComment.ToString("d"));
            parameters.Add("perm_addmeta", permAddMeta.ToString("d"));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Set the tags for a photo.
        /// </summary>
        /// <remarks>
        /// This will remove all old tags and add these new ones specified. See <see cref="PhotosAddTags(string, string)"/>
        /// to just add new tags without deleting old ones.
        /// </remarks>
        /// <param name="photoId">The id of the photo to update.</param>
        /// <param name="tags">An array of tags.</param>
        public void PhotosSetTags(string photoId, string[] tags)
        {
            string s = string.Join(",", tags);
            PhotosSetTags(photoId, s);
        }

        /// <summary>
        /// Set the tags for a photo.
        /// </summary>
        /// <remarks>
        /// This will remove all old tags and add these new ones specified. See <see cref="PhotosAddTags(string, string)"/>
        /// to just add new tags without deleting old ones.
        /// </remarks>
        /// <param name="photoId">The id of the photo to update.</param>
        /// <param name="tags">An comma-seperated list of tags.</param>
        public void PhotosSetTags(string photoId, string tags)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.setTags");
            parameters.Add("photo_id", photoId);
            parameters.Add("tags", tags);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Sets the content type for a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photos to set.</param>
        /// <param name="contentType">The new content type.</param>
        public void PhotosSetContentType(string photoId, ContentType contentType)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.setContentType");
            parameters.Add("photo_id", photoId);
            parameters.Add("content_type", contentType.ToString("D"));

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Set the safety level for a photo, but only set the hidden aspect.
        /// </summary>
        /// <param name="photoId">The ID of the photo to set the hidden property for.</param>
        /// <param name="hidden">The new value of the hidden value.</param>
        public void PhotosSetSafetyLevel(string photoId, HiddenFromSearch hidden)
        {
            PhotosSetSafetyLevel(photoId, SafetyLevel.None, hidden);
        }

        /// <summary>
        /// Set the safety level for a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo to set the safety level property for.</param>
        /// <param name="safetyLevel">The new value of the safety level value.</param>
        public void PhotosSetSafetyLevel(string photoId, SafetyLevel safetyLevel)
        {
            PhotosSetSafetyLevel(photoId, safetyLevel, HiddenFromSearch.None);
        }

        /// <summary>
        /// Sets the safety level and hidden property of a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photos to set.</param>
        /// <param name="safetyLevel">The new content type.</param>
        /// <param name="hidden">The new hidden value.</param>
        public void PhotosSetSafetyLevel(string photoId, SafetyLevel safetyLevel, HiddenFromSearch hidden)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.setSafetyLevel");
            parameters.Add("photo_id", photoId);
            if (safetyLevel != SafetyLevel.None) parameters.Add("safety_level", safetyLevel.ToString("D"));
            switch (hidden)
            {
                case HiddenFromSearch.Visible:
                    parameters.Add("hidden", "0");
                    break;
                case HiddenFromSearch.Hidden:
                    parameters.Add("hidden", "1");
                    break;
            }

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Gets the first page of favourites for the given photo id.
        /// </summary>
        /// <param name="photoId">The ID of the photo to return the favourites for.</param>
        /// <returns>An array of favourites for photos.</returns>
        public PhotoFavoriteCollection PhotosGetFavorites(string photoId)
        {
            return PhotosGetFavorites(photoId, 0, 0);
        }

        /// <summary>
        /// Get the list of favourites for a photo.
        /// </summary>
        /// <param name="photoId">The photo ID of the photo.</param>
        /// <param name="perPage">How many favourites to return per page. Default is 10.</param>
        /// <param name="page">The page to return. Default is 1.</param>
        /// <returns>An array of favourites for photos.</returns>
        public PhotoFavoriteCollection PhotosGetFavorites(string photoId, int perPage, int page)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.getFavorites");
            parameters.Add("photo_id", photoId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PhotoFavoriteCollection>(parameters);

        }

    }
}
