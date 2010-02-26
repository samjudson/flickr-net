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
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.addTags");
            parameters.Add("photo_id", photoId);
            parameters.Add("tags", tags);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Delete a photo from Flickr.
        /// </summary>
        /// <remarks>
        /// Requires Delete permissions. Also note, photos cannot be recovered once deleted.</remarks>
        /// <param name="photoId">The ID of the photo to delete.</param>
        public void PhotosDelete(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.delete");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Get all the contexts (group, set and photostream 'next' and 'previous'
        /// pictures) for a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo to get the contexts for.</param>
        /// <returns>An instance of the <see cref="AllContexts"/> class.</returns>
        public AllContexts PhotosGetAllContexts(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getAllContexts");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                AllContexts contexts = new AllContexts(response.AllElements);
                return contexts;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }

        }

        /// <summary>
        /// Gets the most recent 10 photos from your contacts.
        /// </summary>
        /// <returns>An instance of the <see cref="Photo"/> class containing the photos.</returns>
        public Photos PhotosGetContactsPhotos()
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
        public Photos PhotosGetContactsPhotos(int count)
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
        public Photos PhotosGetContactsPhotos(int count, bool justFriends, bool singlePhoto, bool includeSelf, PhotoSearchExtras extras)
        {
            CheckRequiresAuthentication();

            if (count != 0 && (count < 10 || count > 50) && !singlePhoto)
            {
                throw new ArgumentOutOfRangeException("count", String.Format("Count must be between 10 and 50. ({0})", count));
            }
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getContactsPhotos");
            if (count > 0 && !singlePhoto) parameters.Add("count", count.ToString());
            if (justFriends) parameters.Add("just_friends", "1");
            if (singlePhoto) parameters.Add("single_photo", "1");
            if (includeSelf) parameters.Add("include_self", "1");
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
        public Photos PhotosGetContactsPublicPhotos(string userId)
        {
            return PhotosGetContactsPublicPhotos(userId, 0, false, false, false, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="extras">A list of extra details to return for each photo.</param>
        /// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
        public Photos PhotosGetContactsPublicPhotos(string userId, PhotoSearchExtras extras)
        {
            return PhotosGetContactsPublicPhotos(userId, 0, false, false, false, extras);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
        /// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
        public Photos PhotosGetContactsPublicPhotos(string userId, int count)
        {
            return PhotosGetContactsPublicPhotos(userId, count, false, false, false, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Gets the public photos for given users ID's contacts.
        /// </summary>
        /// <param name="userId">The user ID whose contacts you wish to get photos for.</param>
        /// <param name="count">The number of photos to return. Defaults to 10, maximum is 50.</param>
        /// <param name="extras">A list of extra details to return for each photo.</param>
        /// <returns>A <see cref="Photos"/> object containing details of the photos returned.</returns>
        public Photos PhotosGetContactsPublicPhotos(string userId, int count, PhotoSearchExtras extras)
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
        public Photos PhotosGetContactsPublicPhotos(string userId, int count, bool justFriends, bool singlePhoto, bool includeSelf)
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
        public Photos PhotosGetContactsPublicPhotos(string userId, int count, bool justFriends, bool singlePhoto, bool includeSelf, PhotoSearchExtras extras)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getContactsPublicPhotos");
            parameters.Add("api_key", _apiKey);
            parameters.Add("user_id", userId);
            if (count > 0) parameters.Add("count", count.ToString());
            if (justFriends) parameters.Add("just_friends", "1");
            if (singlePhoto) parameters.Add("single_photo", "1");
            if (includeSelf) parameters.Add("include_self", "1");
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets the context of the photo in the users photostream.
        /// </summary>
        /// <param name="photoId">The ID of the photo to return the context for.</param>
        /// <returns></returns>
        public Context PhotosGetContext(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getContext");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                Context c = new Context();
                c.Count = response.ContextCount.Count;
                c.NextPhoto = response.ContextNextPhoto;
                c.PreviousPhoto = response.ContextPrevPhoto;

                return c;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns count of photos between each pair of dates in the list.
        /// </summary>
        /// <remarks>If you pass in DateA, DateB and DateC it returns
        /// a list of the number of photos between DateA and DateB,
        /// followed by the number between DateB and DateC. 
        /// More parameters means more sets.</remarks>
        /// <param name="dates">Array of <see cref="DateTime"/> objects.</param>
        /// <returns><see cref="PhotoCounts"/> class instance.</returns>
        public PhotoCounts PhotosGetCounts(DateTime[] dates)
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
        /// <returns><see cref="PhotoCounts"/> class instance.</returns>
        public PhotoCounts PhotosGetCounts(DateTime[] dates, bool taken)
        {
            StringBuilder s = new StringBuilder(dates.Length * 20);
            foreach (DateTime d in dates)
            {
                s.Append(Utils.DateToUnixTimestamp(d));
                s.Append(",");
            }
            if (s.Length > 0) s.Remove(s.Length - 2, 1);

            if (taken)
                return PhotosGetCounts(null, s.ToString());
            else
                return PhotosGetCounts(s.ToString(), null);
        }
        /// <summary>
        /// Returns count of photos between each pair of dates in the list.
        /// </summary>
        /// <remarks>If you pass in DateA, DateB and DateC it returns
        /// a list of the number of photos between DateA and DateB,
        /// followed by the number between DateB and DateC. 
        /// More parameters means more sets.</remarks>
        /// <param name="dates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
        /// <returns><see cref="PhotoCounts"/> class instance.</returns>
        public PhotoCounts PhotosGetCounts(string dates)
        {
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
        /// <param name="taken_dates">Comma-delimited list of dates in unix timestamp format. Optional.</param>
        /// <returns><see cref="PhotoCounts"/> class instance.</returns>
        public PhotoCounts PhotosGetCounts(string dates, string taken_dates)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getContactsPhotos");
            if (dates != null && dates.Length > 0) parameters.Add("dates", dates);
            if (taken_dates != null && taken_dates.Length > 0) parameters.Add("taken_dates", taken_dates);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.PhotoCounts;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets the EXIF data for a given Photo ID.
        /// </summary>
        /// <param name="photoId">The Photo ID of the photo to return the EXIF data for.</param>
        /// <returns>An instance of the <see cref="ExifPhoto"/> class containing the EXIF data.</returns>
        public ExifPhoto PhotosGetExif(string photoId)
        {
            return PhotosGetExif(photoId, null);
        }

        /// <summary>
        /// Gets the EXIF data for a given Photo ID.
        /// </summary>
        /// <param name="photoId">The Photo ID of the photo to return the EXIF data for.</param>
        /// <param name="secret">The secret of the photo. If the secret is specified then
        /// authentication checks are bypassed.</param>
        /// <returns>An instance of the <see cref="ExifPhoto"/> class containing the EXIF data.</returns>
        public ExifPhoto PhotosGetExif(string photoId, string secret)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getExif");
            parameters.Add("photo_id", photoId);
            if (secret != null) parameters.Add("secret", secret);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                ExifPhoto e = new ExifPhoto(response.PhotoInfo.PhotoId,
                    response.PhotoInfo.Secret,
                    response.PhotoInfo.Server,
                    response.PhotoInfo.ExifTagCollection);

                return e;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
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
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getInfo");
            parameters.Add("photo_id", photoId);
            if (secret != null) parameters.Add("secret", secret);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.PhotoInfo;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Get permissions for a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to get permissions for.</param>
        /// <returns>An instance of the <see cref="PhotoPermissions"/> class containing the permissions of the specified photo.</returns>
        public PhotoPermissions PhotosGetPerms(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getPerms");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return new PhotoPermissions(response.AllElements[0]);
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetRecent()
        {
            return PhotosGetRecent(0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetRecent(PhotoSearchExtras extras)
        {
            return PhotosGetRecent(0, 0, extras);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetRecent(int perPage, int page)
        {
            return PhotosGetRecent(perPage, page, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of the latest public photos uploaded to flickr.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetRecent(int perPage, int page, PhotoSearchExtras extras)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getRecent");
            parameters.Add("api_key", _apiKey);
            if (perPage > 0) parameters.Add("per_page", perPage.ToString());
            if (page > 0) parameters.Add("page", page.ToString());
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns the available sizes for a photo. The calling user must have permission to view the photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to fetch size information for.</param>
        /// <returns>A <see cref="Sizes"/> class whose property <see cref="Sizes.SizeCollection"/> is an array of <see cref="Size"/> objects.</returns>
        public Sizes PhotosGetSizes(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getSizes");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Sizes;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetUntagged()
        {
            return PhotosGetUntagged(0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetUntagged(PhotoSearchExtras extras)
        {
            return PhotosGetUntagged(0, 0, extras);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetUntagged(int perPage, int page)
        {
            return PhotosGetUntagged(perPage, page, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Returns a list of your photos with no tags.
        /// </summary>
        /// <param name="extras">A comma-delimited list of extra information to fetch for each returned record.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns>A <see cref="Photos"/> class containing the list of photos.</returns>
        public Photos PhotosGetUntagged(int perPage, int page, PhotoSearchExtras extras)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getUntagged");
            if (perPage > 0) parameters.Add("per_page", perPage.ToString());
            if (page > 0) parameters.Add("page", page.ToString());
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets a list of photos not in sets. Defaults to include all extra fields.
        /// </summary>
        /// <returns><see cref="Photos"/> instance containing list of photos.</returns>
        public Photos PhotosGetNotInSet()
        {
            return PhotosGetNotInSet(new PartialSearchOptions());
        }

        /// <summary>
        /// Gets a specific page of the list of photos which are not in sets.
        /// Defaults to include all extra fields.
        /// </summary>
        /// <param name="page">The page number to return.</param>
        /// <returns><see cref="Photos"/> instance containing list of photos.</returns>
        public Photos PhotosGetNotInSet(int page)
        {
            return PhotosGetNotInSet(0, page, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets a specific page of the list of photos which are not in sets.
        /// Defaults to include all extra fields.
        /// </summary>
        /// <param name="perPage">Number of photos per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns><see cref="Photos"/> instance containing list of photos.</returns>
        public Photos PhotosGetNotInSet(int perPage, int page)
        {
            return PhotosGetNotInSet(perPage, page, PhotoSearchExtras.None);
        }

        /// <summary>
        /// Gets a list of a users photos which are not in a set.
        /// </summary>
        /// <param name="perPage">Number of photos per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras"><see cref="PhotoSearchExtras"/> enumeration.</param>
        /// <returns><see cref="Photos"/> instance containing list of photos.</returns>
        public Photos PhotosGetNotInSet(int perPage, int page, PhotoSearchExtras extras)
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
        /// <returns>A collection of photos in the <see cref="Photos"/> class.</returns>
        public Photos PhotosGetNotInSet(PartialSearchOptions options)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.getNotInSet");
            Utils.PartialOptionsIntoArray(options, parameters);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets a list of all current licenses.
        /// </summary>
        /// <returns><see cref="Licenses"/> instance.</returns>
        public Licenses PhotosLicensesGetInfo()
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.licenses.getInfo");
            parameters.Add("api_key", _apiKey);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Licenses;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Remove an existing tag.
        /// </summary>
        /// <param name="tagId">The id of the tag, as returned by <see cref="Flickr.PhotosGetInfo(string)"/> or similar method.</param>
        /// <returns>True if the tag was removed.</returns>
        public bool PhotosRemoveTag(string tagId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.removeTag");
            parameters.Add("tag_id", tagId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return true;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Return a list of your photos that have been recently created or which have been recently modified.
        /// Recently modified may mean that the photo's metadata (title, description, tags)
        /// may have been changed or a comment has been added (or just modified somehow :-)
        /// </summary>
        /// <param name="minDate">The date from which modifications should be compared.</param>
        /// <param name="extras">A list of extra information to fetch for each returned record.</param>
        /// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
        public Photos PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras)
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
        /// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
        public Photos PhotosRecentlyUpdated(DateTime minDate, int perPage, int page)
        {
            return PhotosRecentlyUpdated(minDate, PhotoSearchExtras.None, perPage, page);
        }

        /// <summary>
        /// Return a list of your photos that have been recently created or which have been recently modified.
        /// Recently modified may mean that the photo's metadata (title, description, tags)
        /// may have been changed or a comment has been added (or just modified somehow :-)
        /// </summary>
        /// <param name="minDate">The date from which modifications should be compared.</param>
        /// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
        public Photos PhotosRecentlyUpdated(DateTime minDate)
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
        /// <returns>Returns a <see cref="Photos"/> instance containing the list of photos.</returns>
        public Photos PhotosRecentlyUpdated(DateTime minDate, PhotoSearchExtras extras, int perPage, int page)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.recentlyUpdated");
            parameters.Add("min_date", Utils.DateToUnixTimestamp(minDate).ToString());
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString());
            if (page > 0) parameters.Add("page", page.ToString());

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }
        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="userId">The user whose photos you wish to search for.</param>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string userId, string text)
        {
            return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="userId">The user whose photos you wish to search for.</param>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string userId, string text, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="userId">The user whose photos you wish to search for.</param>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string userId, string text, LicenseType license)
        {
            return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="userId">The user whose photos you wish to search for.</param>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string userId, string text, LicenseType license, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string text, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string text)
        {
            return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string text, LicenseType license)
        {
            return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos containing text, rather than tags.
        /// </summary>
        /// <param name="text">The text you want to search for in titles and descriptions.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearchText(string text, LicenseType license, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, "", 0, text, DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos containing an array of tags.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos containing an array of tags.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags)
        {
            return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos containing an array of tags.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, LicenseType license, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos containing an array of tags.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, LicenseType license)
        {
            return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, TagMode tagMode, string text, int perPage, int page)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, TagMode tagMode, string text)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string[] tags, TagMode tagMode, string text, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, LicenseType license)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, LicenseType license, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, int perPage, int page)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="minUploadDate">The minimum upload date.</param>
        /// <param name="maxUploadDate">The maxmimum upload date.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, DateTime minUploadDate, DateTime maxUploadDate, LicenseType license, int perPage, int page, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, String.Join(",", tags), tagMode, text, minUploadDate, maxUploadDate, license, perPage, page, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">An array of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="minUploadDate">The minimum upload date.</param>
        /// <param name="maxUploadDate">The maxmimum upload date.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string[] tags, TagMode tagMode, string text, DateTime minUploadDate, DateTime maxUploadDate, LicenseType license, int perPage, int page)
        {
            return PhotosSearch(userId, String.Join(",", tags), tagMode, text, minUploadDate, maxUploadDate, license, perPage, page, PhotoSearchExtras.All);
        }

        // PhotoSearch - tags versions

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string tags, LicenseType license, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string tags, LicenseType license)
        {
            return PhotosSearch(null, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string tags, TagMode tagMode, string text, int perPage, int page)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string tags, TagMode tagMode, string text)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string tags, TagMode tagMode, string text, PhotoSearchExtras extras)
        {
            return PhotosSearch(null, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, LicenseType license)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="license">The license type to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, LicenseType license, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, 0, "", DateTime.MinValue, DateTime.MinValue, license, 0, 0, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, int perPage, int page)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, int perPage, int page, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, perPage, page, extras);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, PhotoSearchExtras.All);
        }

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, PhotoSearchExtras extras)
        {
            return PhotosSearch(userId, tags, tagMode, text, DateTime.MinValue, DateTime.MinValue, 0, 0, 0, extras);
        }

        // Actual PhotoSearch function

        /// <summary>
        /// Search for photos.
        /// </summary>
        /// <param name="userId">The ID of the user to search the photos of.</param>
        /// <param name="tags">A comma seperated list of tags to search for.</param>
        /// <param name="tagMode">Match all tags, or any tag.</param>
        /// <param name="text">Text to search for in photo title or description.</param>
        /// <param name="perPage">Number of photos to return per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras">Optional extras to return.</param>
        /// <param name="minUploadDate">The minimum upload date.</param>
        /// <param name="maxUploadDate">The maxmimum upload date.</param>
        /// <param name="license">The license type to return.</param>
        /// <returns>A <see cref="Photos"/> instance.</returns>
        public Photos PhotosSearch(string userId, string tags, TagMode tagMode, string text, DateTime minUploadDate, DateTime maxUploadDate, LicenseType license, int perPage, int page, PhotoSearchExtras extras)
        {
            PhotoSearchOptions options = new PhotoSearchOptions();
            options.UserId = userId;
            options.Tags = tags;
            options.TagMode = tagMode;
            options.Text = text;
            options.MinUploadDate = minUploadDate;
            options.MaxUploadDate = maxUploadDate;
            if (license > 0) options.Licenses.Add(license);
            options.PerPage = perPage;
            options.Page = page;
            options.Extras = extras;

            return PhotosSearch(options);
        }

        /// <summary>
        /// Search for a set of photos, based on the value of the <see cref="PhotoSearchOptions"/> parameters.
        /// </summary>
        /// <param name="options">The parameters to search for.</param>
        /// <returns>A collection of photos contained within a <see cref="Photos"/> object.</returns>
        public Photos PhotosSearch(PhotoSearchOptions options)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.search");
            if (options.UserId != null && options.UserId.Length > 0) parameters.Add("user_id", options.UserId);
            if (options.GroupId != null && options.GroupId.Length > 0) parameters.Add("group_id", options.GroupId);
            if (options.Text != null && options.Text.Length > 0) parameters.Add("text", options.Text);
            if (options.Tags != null && options.Tags.Length > 0) parameters.Add("tags", options.Tags);
            if (options.TagMode != TagMode.None) parameters.Add("tag_mode", options.TagModeString);
            if (options.MachineTags != null && options.MachineTags.Length > 0) parameters.Add("machine_tags", options.MachineTags);
            if (options.MachineTagMode != MachineTagMode.None) parameters.Add("machine_tag_mode", options.MachineTagModeString);
            if (options.MinUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", Utils.DateToUnixTimestamp(options.MinUploadDate).ToString());
            if (options.MaxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", Utils.DateToUnixTimestamp(options.MaxUploadDate).ToString());
            if (options.MinTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", options.MinTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (options.MaxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", options.MaxTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (options.Licenses.Count != 0)
            {
                string[] licenseArray = options.Licenses.ConvertAll<string>(delegate(LicenseType license) { return ((int)license).ToString(); }).ToArray();
                parameters.Add("license", String.Join(",", licenseArray));
            }
            if (options.PerPage != 0) parameters.Add("per_page", options.PerPage.ToString());
            if (options.Page != 0) parameters.Add("page", options.Page.ToString());
            if (options.Extras != PhotoSearchExtras.None) parameters.Add("extras", options.ExtrasString);
            if (options.SortOrder != PhotoSearchSortOrder.None) parameters.Add("sort", options.SortOrderString);
            if (options.PrivacyFilter != PrivacyFilter.None) parameters.Add("privacy_filter", options.PrivacyFilter.ToString("d"));
            if (options.BoundaryBox.IsSet) parameters.Add("bbox", options.BoundaryBox.ToString());
            if (options.Accuracy != GeoAccuracy.None) parameters.Add("accuracy", options.Accuracy.ToString("d"));
            if (options.SafeSearch != SafetyLevel.None) parameters.Add("safe_search", options.SafeSearch.ToString("d"));
            if (options.ContentType != ContentTypeSearch.None) parameters.Add("content_type", options.ContentType.ToString("d"));
            if (options.HasGeo) parameters.Add("has_geo", "1");
            if (!float.IsNaN(options.Latitude)) parameters.Add("lat", options.Latitude.ToString("0.0000"));
            if (!float.IsNaN(options.Longitude)) parameters.Add("lon", options.Longitude.ToString("0.0000"));
            if (!float.IsNaN(options.Radius)) parameters.Add("radius", options.Radius.ToString("0.00"));
            if (options.RadiusUnits != RadiusUnits.None) parameters.Add("radius_units", (options.RadiusUnits == RadiusUnits.Miles ? "mi" : "km"));
            if (options.Contacts != ContactSearch.None) parameters.Add("contacts", (options.Contacts == ContactSearch.AllContacts ? "all" : "ff"));
            if (options.WoeId != null) parameters.Add("woe_id", options.WoeId);
            if (options.PlaceId != null) parameters.Add("place_id", options.PlaceId);
            if (options.IsCommons) parameters.Add("is_commons", "1");

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Photos;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
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
        public bool PhotosSetDates(string photoId, DateTime dateTaken, DateGranularity granularity)
        {
            return PhotosSetDates(photoId, DateTime.MinValue, dateTaken, granularity);
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
        public bool PhotosSetDates(string photoId, DateTime datePosted)
        {
            return PhotosSetDates(photoId, datePosted, DateTime.MinValue, DateGranularity.FullDate);
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
        public bool PhotosSetDates(string photoId, DateTime datePosted, DateTime dateTaken, DateGranularity granularity)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.setDates");
            parameters.Add("photo_id", photoId);
            if (datePosted != DateTime.MinValue) parameters.Add("date_posted", Utils.DateToUnixTimestamp(datePosted).ToString());
            if (dateTaken != DateTime.MinValue)
            {
                parameters.Add("date_taken", dateTaken.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
                parameters.Add("date_taken_granularity", granularity.ToString("d"));
            }

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return true;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }

        }

        /// <summary>
        /// Sets the title and description of the photograph.
        /// </summary>
        /// <param name="photoId">The numerical photoId of the photograph.</param>
        /// <param name="title">The new title of the photograph.</param>
        /// <param name="description">The new description of the photograph.</param>
        /// <returns>True when the operation is successful.</returns>
        /// <exception cref="FlickrApiException">Thrown when the photo id cannot be found.</exception>
        public bool PhotosSetMeta(string photoId, string title, string description)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.setMeta");
            parameters.Add("photo_id", photoId);
            parameters.Add("title", title);
            parameters.Add("description", description);

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return true;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }

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
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.setPerms");
            parameters.Add("photo_id", photoId);
            parameters.Add("is_public", (isPublic ? "1" : "0"));
            parameters.Add("is_friend", (isFriend ? "1" : "0"));
            parameters.Add("is_family", (isFamily ? "1" : "0"));
            parameters.Add("perm_comment", permComment.ToString("d"));
            parameters.Add("perm_addmeta", permAddMeta.ToString("d"));

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }

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
        /// <returns>True if the photo was updated successfully.</returns>
        public bool PhotosSetTags(string photoId, string[] tags)
        {
            string s = string.Join(",", tags);
            return PhotosSetTags(photoId, s);
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
        /// <returns>True if the photo was updated successfully.</returns>
        public bool PhotosSetTags(string photoId, string tags)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.setTags");
            parameters.Add("photo_id", photoId);
            parameters.Add("tags", tags);

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return true;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }

        }

        /// <summary>
        /// Sets the content type for a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photos to set.</param>
        /// <param name="contentType">The new content type.</param>
        public void PhotosSetContentType(string photoId, ContentType contentType)
        {
            CheckRequiresAuthentication();

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.setContentType");
            parameters.Add("photo_id", photoId);
            parameters.Add("content_type", (int)contentType);

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
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

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.setSafetyLevel");
            parameters.Add("photo_id", photoId);
            if (safetyLevel != SafetyLevel.None) parameters.Add("safety_level", (int)safetyLevel);
            switch (hidden)
            {
                case HiddenFromSearch.Visible:
                    parameters.Add("hidden", 0);
                    break;
                case HiddenFromSearch.Hidden:
                    parameters.Add("hidden", 1);
                    break;
            }

            FlickrNet.Response response = GetResponseNoCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets the first page of favourites for the given photo id.
        /// </summary>
        /// <param name="photoId">The ID of the photo to return the favourites for.</param>
        /// <returns>An array of favourites for photos.</returns>
        public PhotoFavourites PhotosGetFavorites(string photoId)
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
        public PhotoFavourites PhotosGetFavorites(string photoId, int perPage, int page)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.getFavorites");
            parameters.Add("photo_id", photoId);
            if (page > 0) parameters.Add("page", page);
            if (perPage > 0) parameters.Add("per_page", perPage);

            return GetResponseCache<PhotoFavourites>(parameters);

        }

    }
}
