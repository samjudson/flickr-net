using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Approve a location suggestion for a photo.
        /// </summary>
        /// <param name="suggestionId">The suggestion to approve.</param>
        /// <param name="callback"></param>
        public void PhotosSuggestionsApproveSuggestionAsync(string suggestionId, Action<FlickrResult<NoResponse>> callback)
        {
            CheckRequiresAuthentication();

            if (String.IsNullOrEmpty(suggestionId)) throw new ArgumentNullException("suggestionId");

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.suggestions.approveSuggestion");
            parameters.Add("suggestion_id", suggestionId);

            GetResponseAsync<NoResponse>(parameters, callback);

        }

        /// <summary>
        /// Get a list of suggestions for a photo.
        /// </summary>
        /// <param name="photoId">The photo id of the photo to list the suggestions for.</param>
        /// <param name="status">The type of status to filter by.</param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public void PhotosSuggestionsGetListAsync(string photoId, SuggestionStatus status, Action<FlickrResult<SuggestionCollection>> callback)
        {
            CheckRequiresAuthentication();

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.suggestions.getList");
            parameters.Add("photo_id", photoId);
            parameters.Add("status_id", status.ToString("d"));

            GetResponseAsync<SuggestionCollection>(parameters, callback);
        }

        /// <summary>
        /// Rejects a suggestion made for a location on a photo. Currently doesn't appear to actually work. Just use <see cref="Flickr.PhotosSuggestionsRemoveSuggestion"/> instead.
        /// </summary>
        /// <param name="suggestionId">The ID of the suggestion to remove.</param>
        /// <param name="callback"></param>
        public void PhotosSuggestionsRejectSuggestionAsync(string suggestionId, Action<FlickrResult<NoResponse>> callback)
        {
            CheckRequiresAuthentication();

            if (String.IsNullOrEmpty(suggestionId)) throw new ArgumentNullException("suggestionId");

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.suggestions.rejectSuggestion");
            parameters.Add("suggestion_id", suggestionId);

            GetResponseAsync<NoResponse>(parameters, callback);

        }

        /// <summary>
        /// Remove a location suggestion from a photo.
        /// </summary>
        /// <param name="suggestionId">The suggestion to remove.</param>
        /// <param name="callback"></param>
        public void PhotosSuggestionsRemoveSuggestionAsync(string suggestionId, Action<FlickrResult<NoResponse>> callback)
        {
            CheckRequiresAuthentication();

            if (String.IsNullOrEmpty(suggestionId)) throw new ArgumentNullException("suggestionId");

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.suggestions.removeSuggestion");
            parameters.Add("suggestion_id", suggestionId);

            GetResponseAsync<NoResponse>(parameters, callback);

        }

        /// <summary>
        /// Suggest a particular location for a photo.
        /// </summary>
        /// <param name="photoId">The id of the photo.</param>
        /// <param name="latitude">The latitude of the location to suggest.</param>
        /// <param name="longitude">The longitude of the location to suggest.</param>
        /// <param name="accuracy">The accuracy of the location to suggest.</param>
        /// <param name="woeId">The WOE ID of the location to suggest.</param>
        /// <param name="placeId">The Flickr place id of the location to suggest.</param>
        /// <param name="note">A note to add to the suggestion.</param>
        /// <param name="callback"></param>
        public void PhotosSuggestionsSuggestLocationAsync(string photoId, double latitude, double longitude, GeoAccuracy accuracy, string woeId, string placeId, string note, Action<FlickrResult<NoResponse>> callback)
        {
            CheckRequiresAuthentication();

            var parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.suggestions.suggestLocation");
            parameters.Add("photo_id", photoId);
            parameters.Add("lat", latitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("lon", longitude.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            parameters.Add("accuracy", accuracy.ToString("D"));
            parameters.Add("place_id", placeId);
            parameters.Add("woe_id", woeId);
            parameters.Add("note", note);

            GetResponseAsync<NoResponse>(parameters, callback);
        }
    }
}
