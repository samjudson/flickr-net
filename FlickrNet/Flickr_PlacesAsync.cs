using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Returns a list of places which contain the query string.
        /// </summary>
        /// <param name="query">The string to search for. Must not be null.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesFindAsync(string query, Action<FlickrResult<PlaceCollection>> callback)
        {
            if (query == null) throw new ArgumentNullException("query");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.find");
            parameters.Add("query", query);

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Returns a place based on the input latitude and longitude.
        /// </summary>
        /// <param name="latitude">The latitude, between -180 and 180.</param>
        /// <param name="longitude">The longitude, between -90 and 90.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesFindByLatLonAsync(double latitude, double longitude, Action<FlickrResult<Place>> callback)
        {
            PlacesFindByLatLonAsync(latitude, longitude, GeoAccuracy.None, callback);
        }

        /// <summary>
        /// Returns a place based on the input latitude and longitude.
        /// </summary>
        /// <param name="latitude">The latitude, between -180 and 180.</param>
        /// <param name="longitude">The longitude, between -90 and 90.</param>
        /// <param name="accuracy">The level the locality will be for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesFindByLatLonAsync(double latitude, double longitude, GeoAccuracy accuracy, Action<FlickrResult<Place>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.findByLatLon");
            parameters.Add("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (accuracy != GeoAccuracy.None) parameters.Add("accuracy", ((int)accuracy).ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PlaceCollection>(parameters, (r) =>
            {
                FlickrResult<Place> result = new FlickrResult<Place>();
                result.Error = r.Error;
                if (!r.HasError)
                {
                    result.Result = r.Result[0];
                }
                callback(result);
            });
        }

        /// <summary>
        /// Return a list of locations with public photos that are parented by a Where on Earth (WOE) or Places ID.
        /// </summary>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetChildrenWithPhotosPublicAsync(string placeId, string woeId, Action<FlickrResult<PlaceCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.getChildrenWithPhotosPublic");

            if ((placeId == null || placeId.Length == 0) && (woeId == null || woeId.Length == 0))
            {
                throw new FlickrException("Both placeId and woeId cannot be null or empty.");
            }

            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Get informations about a place.
        /// </summary>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetInfoAsync(string placeId, string woeId, Action<FlickrResult<PlaceInfo>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.getInfo");

            if (String.IsNullOrEmpty(placeId) &&  String.IsNullOrEmpty(woeId))
            {
                throw new FlickrException("Both placeId and woeId cannot be null or empty.");
            }

            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);

            GetResponseAsync<PlaceInfo>(parameters, callback);
        }

        /// <summary>
        /// Lookup information about a place, by its flickr.com/places URL.
        /// </summary>
        /// <param name="url">A flickr.com/places URL in the form of /country/region/city. For example: /Canada/Quebec/Montreal</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetInfoByUrlAsync(string url, Action<FlickrResult<PlaceInfo>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.getInfoByUrl");
            parameters.Add("url", url);

            GetResponseAsync<PlaceInfo>(parameters, callback);
        }

        /// <summary>
        /// Gets a list of valid Place Type key/value pairs.
        /// </summary>
        /// <remarks>
        /// All Flickr.Net methods use the <see cref="PlaceType"/> enumeration so this method doesn't serve much purpose.
        /// </remarks>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetPlaceTypesAsync(Action<FlickrResult<PlaceTypeInfoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.getPlaceTypes");

            GetResponseAsync<PlaceTypeInfoCollection>(parameters, callback);
        }

        /// <summary>
        /// Return an historical list of all the shape data generated for a Places or Where on Earth (WOE) ID.
        /// </summary>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetShapeHistoryAsync(string placeId, string woeId, Action<FlickrResult<ShapeDataCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.getShapeHistory");

            if (String.IsNullOrEmpty(placeId) && String.IsNullOrEmpty(woeId))
            {
                throw new FlickrException("Both placeId and woeId cannot be null or empty.");
            }

            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);

            GetResponseAsync<ShapeDataCollection>(parameters, callback);

        }
        
        /// <summary>
        /// Return the top 100 most geotagged places for a day.
        /// </summary>
        /// <param name="placeType">The type for a specific place type to cluster photos by. </param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetTopPlacesListAsync(PlaceType placeType, Action<FlickrResult<PlaceCollection>> callback)
        {
            PlacesGetTopPlacesListAsync(placeType, DateTime.MinValue, null, null, callback);
        }

        
        /// <summary>
        /// Return the top 100 most geotagged places for a day.
        /// </summary>
        /// <param name="placeType">The type for a specific place type to cluster photos by. </param>
        /// <param name="placeId">Limit your query to only those top places belonging to a specific Flickr Places identifier.</param>
        /// <param name="woeId">Limit your query to only those top places belonging to a specific Where on Earth (WOE) identifier.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetTopPlacesListAsync(PlaceType placeType, string placeId, string woeId, Action<FlickrResult<PlaceCollection>> callback)
        {
            PlacesGetTopPlacesListAsync(placeType, DateTime.MinValue, placeId, woeId, callback);
        }

        
        /// <summary>
        /// Return the top 100 most geotagged places for a day.
        /// </summary>
        /// <param name="placeType">The type for a specific place type to cluster photos by. </param>
        /// <param name="date">A valid date. The default is yesterday.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetTopPlacesListAsync(PlaceType placeType, DateTime date, Action<FlickrResult<PlaceCollection>> callback)
        {
            PlacesGetTopPlacesListAsync(placeType, date, null, null, callback);
        }
        /// <summary>
        /// Return the top 100 most geotagged places for a day.
        /// </summary>
        /// <param name="placeType">The type for a specific place type to cluster photos by. </param>
        /// <param name="date">A valid date. The default is yesterday.</param>
        /// <param name="placeId">Limit your query to only those top places belonging to a specific Flickr Places identifier.</param>
        /// <param name="woeId">Limit your query to only those top places belonging to a specific Where on Earth (WOE) identifier.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesGetTopPlacesListAsync(PlaceType placeType, DateTime date, string placeId, string woeId, Action<FlickrResult<PlaceCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.getTopPlacesList");

            parameters.Add("place_type_id", placeType.ToString("D"));
            if (date != DateTime.MinValue) parameters.Add("date", date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Gets the places of a particular type that the authenticated user has geotagged photos.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesPlacesForUserAsync(Action<FlickrResult<PlaceCollection>> callback)
        {
            PlacesPlacesForUserAsync(PlaceType.Continent, null, null, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, callback);
        }

        /// <summary>
        /// Gets the places of a particular type that the authenticated user has geotagged photos.
        /// </summary>
        /// <param name="placeType">The type of places to return.</param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters.</param>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. </param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesPlacesForUserAsync(PlaceType placeType, string woeId, string placeId, Action<FlickrResult<PlaceCollection>> callback)
        {
            PlacesPlacesForUserAsync(placeType, woeId, placeId, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, callback);
        }

        /// <summary>
        /// Gets the places of a particular type that the authenticated user has geotagged photos.
        /// </summary>
        /// <param name="placeType">The type of places to return.</param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters.</param>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. </param>
        /// <param name="threshold">The minimum number of photos that a place type must have to be included. If the number of photos is lowered then the parent place type for that place will be used.
        /// For example if you only have 3 photos taken in the locality of Montreal (WOE ID 3534) but your threshold is set to 5 then those photos will be "rolled up" and included instead with a place record for the region of Quebec (WOE ID 2344924).</param>
        /// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned.</param>
        /// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned. </param>
        /// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned. </param>
        /// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned. </param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesPlacesForUserAsync(PlaceType placeType, string woeId, string placeId, int threshold, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate, Action<FlickrResult<PlaceCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.placesForUser");

            parameters.Add("place_type_id", placeType.ToString("D"));
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (threshold > 0) parameters.Add("threshold", threshold.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", UtilityMethods.DateToMySql(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", UtilityMethods.DateToMySql(maxTakenDate));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(maxUploadDate));

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Return a list of the top 100 unique places clustered by a given placetype for set of tags or machine tags.
        /// </summary>
        /// <param name="placeType">The ID for a specific place type to cluster photos by. </param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters. </param>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. </param>
        /// <param name="threshold">The minimum number of photos that a place type must have to be included. If the number of photos is lowered then the parent place type for that place will be used.</param>
        /// <param name="tags">A list of tags. Photos with one or more of the tags listed will be returned.</param>
        /// <param name="tagMode">Either 'any' for an OR combination of tags, or 'all' for an AND combination. Defaults to 'any' if not specified.</param>
        /// <param name="machineTags"></param>
        /// <param name="machineTagMode"></param>
        /// <param name="minUploadDate">Minimum upload date.</param>
        /// <param name="maxUploadDate">Maximum upload date.</param>
        /// <param name="minTakenDate">Minimum taken date.</param>
        /// <param name="maxTakenDate">Maximum taken date.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesPlacesForTagsAsync(PlaceType placeType, string woeId, string placeId, int threshold, string[] tags, TagMode tagMode, string[] machineTags, MachineTagMode machineTagMode, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate, Action<FlickrResult<PlaceCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.placesForTags");

            parameters.Add("place_type_id", placeType.ToString("D"));
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (threshold > 0) parameters.Add("threshold", threshold.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (tags != null && tags.Length > 0) parameters.Add("tags", String.Join(",", tags));
            if (tagMode != TagMode.None) parameters.Add("tag_mode", UtilityMethods.TagModeToString(tagMode));
            if (machineTags != null && machineTags.Length > 0) parameters.Add("machine_tags", String.Join(",", machineTags));
            if (machineTagMode != MachineTagMode.None) parameters.Add("machine_tag_mode", UtilityMethods.MachineTagModeToString(machineTagMode));
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", UtilityMethods.DateToMySql(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", UtilityMethods.DateToMySql(maxTakenDate));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(maxUploadDate));

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Return a list of the top 100 unique places clustered by a given placetype for set of tags or machine tags.
        /// </summary>
        /// <param name="placeType">The ID for a specific place type to cluster photos by. </param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters. </param>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. </param>
        /// <param name="threshold">The minimum number of photos that a place type must have to be included. If the number of photos is lowered then the parent place type for that place will be used.</param>
        /// <param name="contactType">The type of contacts to return places for. Either all, or friends and family only.</param>
        /// <param name="minUploadDate">Minimum upload date.</param>
        /// <param name="maxUploadDate">Maximum upload date.</param>
        /// <param name="minTakenDate">Minimum taken date.</param>
        /// <param name="maxTakenDate">Maximum taken date.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesPlacesForContactsAsync(PlaceType placeType, string woeId, string placeId, int threshold, ContactSearch contactType, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate, Action<FlickrResult<PlaceCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.placesForContacts");

            parameters.Add("place_type_id", placeType.ToString("D"));
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (threshold > 0) parameters.Add("threshold", threshold.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (contactType != ContactSearch.None) parameters.Add("contacts", (contactType == ContactSearch.AllContacts ? "all" : "ff"));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(maxUploadDate));
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", UtilityMethods.DateToMySql(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", UtilityMethods.DateToMySql(maxTakenDate));

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Return a list of the top 100 unique places clustered by a given placetype for set of tags or machine tags.
        /// </summary>
        /// <param name="placeType">The ID for a specific place type to cluster photos by. </param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters. </param>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. </param>
        /// <param name="boundaryBox">The boundary box to search for places in.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesPlacesForBoundingBoxAsync(PlaceType placeType, string woeId, string placeId, BoundaryBox boundaryBox, Action<FlickrResult<PlaceCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.placesForBoundingBox");

            parameters.Add("place_type_id", placeType.ToString("D"));
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            parameters.Add("bbox", boundaryBox.ToString());

            GetResponseAsync<PlaceCollection>(parameters, callback);
        }

        /// <summary>
        /// Find Flickr Places information by Place ID.
        ///
        /// This method has been deprecated. It won't be removed but you should use <see cref="Flickr.PlacesGetInfo(string, string)"/> instead.
        /// </summary>
        /// <param name="placeId">A Flickr Places ID.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        [Obsolete("This method is deprecated. Please use Flickr.PlacesGetInfoAsync instead.")]
        public void PlacesResolvePlaceIdAsync(string placeId, Action<FlickrResult<PlaceInfo>> callback)
        {
            PlacesGetInfoAsync(placeId, null, callback);
        }

        /// <summary>
        /// Find Flickr Places information by Place URL.
        ///
        /// This method has been deprecated. It won't be removed but you should use <see cref="Flickr.PlacesGetInfoByUrl(string)"/> instead.
        /// </summary>
        /// <param name="url">A Flickr Places URL.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        [Obsolete("This method is deprecated. Please use Flickr.PlacesGetInfoByUrlAsync instead.")]
        public void PlacesResolvePlaceUrlAsync(string url, Action<FlickrResult<PlaceInfo>> callback)
        {
            PlacesGetInfoByUrlAsync(url, callback);
        }


        /// <summary>
        /// Return a list of the top 100 unique tags for a Flickr Places or Where on Earth (WOE) ID.
        /// </summary>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesTagsForPlaceAsync(string placeId, string woeId, Action<FlickrResult<TagCollection>> callback)
        {
            PlacesTagsForPlaceAsync(placeId, woeId, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, callback);
        }
        
        /// <summary>
        /// Return a list of the top 100 unique tags for a Flickr Places or Where on Earth (WOE) ID.
        /// </summary>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="minUploadDate">Minimum upload date. Photos with an upload date greater than or equal to this value will be returned.</param>
        /// <param name="maxUploadDate">Maximum upload date. Photos with an upload date less than or equal to this value will be returned.</param>
        /// <param name="minTakenDate">Minimum taken date. Photos with an taken date greater than or equal to this value will be returned.</param>
        /// <param name="maxTakenDate">Maximum taken date. Photos with an taken date less than or equal to this value will be returned.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PlacesTagsForPlaceAsync(string placeId, string woeId, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate, Action<FlickrResult<TagCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.places.tagsForPlace");

            if (String.IsNullOrEmpty(placeId) && String.IsNullOrEmpty(woeId))
            {
                throw new FlickrException("Both placeId and woeId cannot be null or empty.");
            }

            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", UtilityMethods.DateToMySql(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", UtilityMethods.DateToMySql(maxTakenDate));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date", UtilityMethods.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date", UtilityMethods.DateToUnixTimestamp(maxUploadDate));

            GetResponseAsync<TagCollection>(parameters, callback);
        }
    }
}
