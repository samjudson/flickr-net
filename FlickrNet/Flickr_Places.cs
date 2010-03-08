using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Returns a list of places which contain the query string.
        /// </summary>
        /// <param name="query">The string to search for. Must not be null.</param>
        /// <returns>An array of <see cref="Place"/> instances.</returns>
        public Place[] PlacesFind(string query)
        {
            if (query == null) throw new ArgumentNullException("query");

            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.places.find");
            parameters.Add("query", query);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Places.PlacesCollection;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }

        }

        /// <summary>
        /// Returns a place based on the input latitude and longitude.
        /// </summary>
        /// <param name="latitude">The latitude, between -180 and 180.</param>
        /// <param name="longitude">The longitude, between -90 and 90.</param>
        /// <returns>An instance of the <see cref="Place"/> that matches the locality.</returns>
        public Place PlacesFindByLatLon(decimal latitude, decimal longitude)
        {
            return PlacesFindByLatLon(latitude, longitude, GeoAccuracy.None);
        }

        /// <summary>
        /// Returns a place based on the input latitude and longitude.
        /// </summary>
        /// <param name="latitude">The latitude, between -180 and 180.</param>
        /// <param name="longitude">The longitude, between -90 and 90.</param>
        /// <param name="accuracy">The level the locality will be for.</param>
        /// <returns>An instance of the <see cref="Place"/> that matches the locality.</returns>
        public Place PlacesFindByLatLon(decimal latitude, decimal longitude, GeoAccuracy accuracy)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.places.findByLatLon");
            parameters.Add("lat", latitude.ToString("0.000"));
            parameters.Add("lon", longitude.ToString("0.000"));
            if (accuracy != GeoAccuracy.None) parameters.Add("accuracy", (int)accuracy);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Places.PlacesCollection[0];
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Return a list of locations with public photos that are parented by a Where on Earth (WOE) or Places ID.
        /// </summary>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <returns>Returns an array of <see cref="Place"/> elements.</returns>
        public Place[] PlacesGetChildrenWithPhotosPublic(string placeId, string woeId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.places.getChildrenWithPhotosPublic");

            if ((placeId == null || placeId.Length == 0) && (woeId == null || woeId.Length == 0))
            {
                throw new FlickrException("Both placeId and woeId cannot be null or empty.");
            }

            parameters.Add("place_id", placeId);
            parameters.Add("woe_id", woeId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Places.PlacesCollection;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Get informations about a place.
        /// </summary>
        /// <param name="placeId">A Flickr Places ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <param name="woeId">A Where On Earth (WOE) ID. (While optional, you must pass either a valid Places ID or a WOE ID.)</param>
        /// <returns>The <see cref="Place"/> record for the place/woe ID.</returns>
        public Place PlacesGetInfo(string placeId, string woeId)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.places.getInfo");

            if ((placeId == null || placeId.Length == 0) && (woeId == null || woeId.Length == 0))
            {
                throw new FlickrException("Both placeId and woeId cannot be null or empty.");
            }

            parameters.Add("place_id", placeId);
            parameters.Add("woe_id", woeId);

            FlickrNet.Response response = GetResponseCache(parameters);

            if (response.Status == ResponseStatus.OK)
            {
                return response.Place;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Gets the places of a particular type that the authenticated user has geotagged photos.
        /// </summary>
        /// <returns>The list of places of that type.</returns>
        public Places PlacesPlacesForUser()
        {
            return PlacesPlacesForUser(PlaceType.Continent, null, null, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }

        /// <summary>
        /// Gets the places of a particular type that the authenticated user has geotagged photos.
        /// </summary>
        /// <param name="placeType">The type of places to return.</param>
        /// <param name="woeId">A Where on Earth identifier to use to filter photo clusters.</param>
        /// <param name="placeId">A Flickr Places identifier to use to filter photo clusters. </param>
        /// <returns>The list of places of that type.</returns>
        public Places PlacesPlacesForUser(PlaceType placeType, string woeId, string placeId)
        {
            return PlacesPlacesForUser(placeType, woeId, placeId, 0, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
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
        /// <returns>The list of places of that type.</returns>
        public Places PlacesPlacesForUser(PlaceType placeType, string woeId, string placeId, int threshold, DateTime minUploadDate, DateTime maxUploadDate, DateTime minTakenDate, DateTime maxTakenDate)
        {
            CheckRequiresAuthentication();

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("method", "flickr.places.placesForUser");

            parameters.Add("place_type_id", (int)placeType);
            if (!String.IsNullOrEmpty(woeId)) parameters.Add("woe_id", woeId);
            if (!String.IsNullOrEmpty(placeId)) parameters.Add("place_id", placeId);
            if (threshold > 0) parameters.Add("threshold", threshold);
            if (minTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", Utils.DateToUnixTimestamp(minTakenDate));
            if (maxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", Utils.DateToUnixTimestamp(maxTakenDate));
            if (minUploadDate != DateTime.MinValue) parameters.Add("min_upload_date ", Utils.DateToUnixTimestamp(minUploadDate));
            if (maxUploadDate != DateTime.MinValue) parameters.Add("max_upload_date ", Utils.DateToUnixTimestamp(maxUploadDate));

            return GetResponseCache<Places>(parameters);
        }

    }
}
