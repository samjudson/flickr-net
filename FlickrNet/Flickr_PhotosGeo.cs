using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Correct the places hierarchy for all the photos for a user at a given latitude, longitude and accuracy.
        /// </summary>
        /// <remarks>
        /// Batch corrections are processed in a delayed queue so it may take a few minutes before the changes are reflected in a user's photos.
        /// </remarks>
        /// <param name="latitude">The latitude of the photos to be update whose valid range is -90 to 90. Anything more than 6 decimal places will be truncated.</param>
        /// <param name="longitude">The longitude of the photos to be updated whose valid range is -180 to 180. Anything more than 6 decimal places will be truncated.</param>
        /// <param name="accuracy">Recorded accuracy level of the photos to be updated. World level is 1, Country is ~3, Region ~6, City ~11, Street ~16. Current range is 1-16. Defaults to 16 if not specified.</param>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        public void PhotosGeoBatchCorrectLocation(double latitude, double longitude, GeoAccuracy accuracy, string placeId, string woeId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.geo.batchCorrectLocation");
            parameters.Add("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("accuracy", (int)accuracy);
            parameters.Add("place_id", placeId);
            parameters.Add("woe_id", woeId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Correct the places hierarchy for a given photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo whose WOE location is being corrected.</param>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        public void PhotosGeoCorrectLocation(string photoId, string placeId, string woeId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.geo.correctLocation");
            parameters.Add("photo_id", photoId);
            parameters.Add("place_id", placeId);
            parameters.Add("woe_id", woeId);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Returns the location data for a give photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo to return the location information for.</param>
        /// <returns>Returns null if the photo has no location information, otherwise returns the location information.</returns>
        public PhotoLocation PhotosGeoGetLocation(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.geo.getLocation");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);
            if (response.Status == ResponseStatus.OK)
            {
                return response.PhotoInfo.Location;
            }
            else
            {
                if (response.Error.Code == 2)
                    return null;
                else
                    throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Indicate the state of a photo's geotagginess beyond latitude and longitude.
        /// </summary>
        /// <remarks>
        /// Note : photos passed to this method must already be geotagged (using the flickr.photos.geo.setLocation method).
        /// </remarks>
        /// <param name="photoId">The id of the photo to set context data for.</param>
        /// <param name="context">ontext is a numeric value representing the photo's geotagginess beyond latitude and longitude. For example, you may wish to indicate that a photo was taken "indoors" or "outdoors". </param>
        public void PhotosGeoSetContext(string photoId, GeoContext context)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("method", "flickr.photos.geo.setContext");
            parameters.Add("photo_id", photoId);
            parameters.Add("context", (int)context);

            GetResponseNoCache<NoResponse>(parameters);
        }

        /// <summary>
        /// Sets the geo location for a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo to set to location for.</param>
        /// <param name="latitude">The latitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
        /// <param name="longitude">The longitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
        public void PhotosGeoSetLocation(string photoId, double latitude, double longitude)
        {
            PhotosGeoSetLocation(photoId, latitude, longitude, GeoAccuracy.None);
        }

        /// <summary>
        /// Sets the geo location for a photo.
        /// </summary>
        /// <param name="photoId">The ID of the photo to set to location for.</param>
        /// <param name="latitude">The latitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
        /// <param name="longitude">The longitude of the geo location. A double number ranging from -180.00 to 180.00. Digits beyond 6 decimal places will be truncated.</param>
        /// <param name="accuracy">The accuracy of the photos geo location.</param>
        public void PhotosGeoSetLocation(string photoId, double latitude, double longitude, GeoAccuracy accuracy)
        {
            System.Globalization.NumberFormatInfo nfi = System.Globalization.NumberFormatInfo.InvariantInfo;
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.geo.setLocation");
            parameters.Add("photo_id", photoId);
            parameters.Add("lat", latitude.ToString(nfi));
            parameters.Add("lon", longitude.ToString(nfi));
            if (accuracy != GeoAccuracy.None)
                parameters.Add("accuracy", ((int)accuracy).ToString());

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
        /// Return a list of photos for a user at a specific latitude, longitude and accuracy.
        /// </summary>
        /// <param name="latitude">The latitude whose valid range is -90 to 90. Anything more than 6 decimal places will be truncated.</param>
        /// <param name="longitude">The longitude whose valid range is -180 to 180. Anything more than 6 decimal places will be truncated.</param>
        /// <param name="accuracy">Recorded accuracy level of the location information. World level is 1, Country is ~3, Region ~6, City ~11, Street ~16. Current range is 1-16. Defaults to 16 if not specified.</param>
        /// <param name="extras"></param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <returns></returns>
        public Photos PhotosGeoPhotosForLocation(double latitude, double longitude, GeoAccuracy accuracy, PhotoSearchExtras extras, int perPage, int page)
        {
            CheckRequiresAuthentication();

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            parameters.Add("method", "flickr.photos.geo.photosForLocation");
            parameters.Add("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("accuracy", (int)accuracy);
            if (extras != PhotoSearchExtras.None) parameters.Add("extras", Utils.ExtrasToString(extras));
            if( perPage > 0 ) parameters.Add("per_page", perPage);
            if (page > 0) parameters.Add("page", page);

            return GetResponseNoCache<Photos>(parameters);

        }

        /// <summary>
        /// Removes Location information.
        /// </summary>
        /// <param name="photoId">The photo ID of the photo to remove information from.</param>
        /// <returns>Returns true if the location information as found and removed. Returns false if no photo information was found.</returns>
        public bool PhotosGeoRemoveLocation(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.geo.removeLocation");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseNoCache(parameters);
            if (response.Status == ResponseStatus.OK)
            {
                return true;
            }
            else
            {
                if (response.Error.Code == 2)
                    return false;
                else
                    throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets a list of photos that do not contain geo location information.
        /// </summary>
        /// <returns>A list of photos that do not contain location information.</returns>
        public Photos PhotosGetWithoutGeoData()
        {
            PartialSearchOptions options = new PartialSearchOptions();
            return PhotosGetWithoutGeoData(options);
        }

        /// <summary>
        /// Gets a list of photos that do not contain geo location information.
        /// </summary>
        /// <param name="options">A limited set of options are supported.</param>
        /// <returns>A list of photos that do not contain location information.</returns>
        public Photos PhotosGetWithoutGeoData(PartialSearchOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.getWithoutGeoData");
            Utils.PartialOptionsIntoArray(options, parameters);

            return GetResponseCache<Photos>(parameters);
        }

        /// <summary>
        /// Gets a list of photos that do not contain geo location information.
        /// </summary>
        /// <param name="options">A limited set of options are supported. 
        /// Unsupported arguments are ignored. 
        /// See http://www.flickr.com/services/api/flickr.photos.getWithGeoData.html for supported properties.</param>
        /// <returns>A list of photos that do not contain location information.</returns>
        [Obsolete("Use the PartialSearchOptions instead")]
        public Photos PhotosGetWithoutGeoData(PhotoSearchOptions options)
        {
            PartialSearchOptions newOptions = new PartialSearchOptions(options);
            return PhotosGetWithoutGeoData(newOptions);
        }

        /// <summary>
        /// Gets a list of photos that contain geo location information.
        /// </summary>
        /// <remarks>
        /// Note, this method doesn't actually return the location information with the photos, 
        /// unless you specify the <see cref="PhotoSearchExtras.Geo"/> option in the <c>extras</c> parameter.
        /// </remarks>
        /// <returns>A list of photos that contain Location information.</returns>
        public Photos PhotosGetWithGeoData()
        {
            PartialSearchOptions options = new PartialSearchOptions();
            return PhotosGetWithGeoData(options);
        }

        /// <summary>
        /// Gets a list of photos that contain geo location information.
        /// </summary>
        /// <remarks>
        /// Note, this method doesn't actually return the location information with the photos, 
        /// unless you specify the <see cref="PhotoSearchExtras.Geo"/> option in the <c>extras</c> parameter.
        /// </remarks>
        /// <param name="options">A limited set of options are supported. 
        /// Unsupported arguments are ignored. 
        /// See http://www.flickr.com/services/api/flickr.photos.getWithGeoData.html for supported properties.</param>
        /// <returns>A list of photos that contain Location information.</returns>
        [Obsolete("Use the new PartialSearchOptions instead")]
        public Photos PhotosGetWithGeoData(PhotoSearchOptions options)
        {
            PartialSearchOptions newOptions = new PartialSearchOptions(options);
            return PhotosGetWithGeoData(newOptions);
        }

        /// <summary>
        /// Gets a list of photos that contain geo location information.
        /// </summary>
        /// <remarks>
        /// Note, this method doesn't actually return the location information with the photos, 
        /// unless you specify the <see cref="PhotoSearchExtras.Geo"/> option in the <c>extras</c> parameter.
        /// </remarks>
        /// <param name="options">The options to filter/sort the results by.</param>
        /// <returns>A list of photos that contain Location information.</returns>
        public Photos PhotosGetWithGeoData(PartialSearchOptions options)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.photos.getWithGeoData");
            Utils.PartialOptionsIntoArray(options, parameters);

            return GetResponseCache<Photos>(parameters);
        }

        /// <summary>
        /// Get permissions for a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo to get permissions for.</param>
        /// <returns>An instance of the <see cref="PhotoPermissions"/> class containing the permissions of the specified photo.</returns>
        public GeoPermissions PhotosGeoGetPerms(string photoId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.geo.getPerms");
            parameters.Add("photo_id", photoId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return new GeoPermissions(response.AllElements[0]);
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Set the permission for who can see geotagged photos on Flickr.
        /// </summary>
        /// <param name="photoId">The ID of the photo permissions to update.</param>
        /// <param name="IsPublic"></param>
        /// <param name="IsContact"></param>
        /// <param name="IsFamily"></param>
        /// <param name="IsFriend"></param>
        public void PhotosGeoSetPerms(string photoId, bool IsPublic, bool IsContact, bool IsFamily, bool IsFriend)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.photos.geo.setPerms");
            parameters.Add("photo_id", photoId);
            parameters.Add("is_public", IsPublic ? "1" : "0");
            parameters.Add("is_contact", IsContact ? "1" : "0");
            parameters.Add("is_friend", IsFriend ? "1" : "0");
            parameters.Add("is_family", IsFamily ? "1" : "0");

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

    }
}
