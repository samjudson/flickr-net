using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets a list of photos from the most recent interstingness list.
        /// </summary>
        /// <param name="perPage">Number of photos per page.</param>
        /// <param name="page">The page number to return.</param>
        /// <param name="extras"><see cref="PhotoSearchExtras"/> enumeration.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void InterestingnessGetListAsync(PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            InterestingnessGetListAsync(DateTime.MinValue, extras, page, perPage, callback);
        }

        /// <summary>
        /// Gets a list of photos from the interstingness list for the specified date.
        /// </summary>
        /// <param name="date">The date to return the interestingness list for.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void InterestingnessGetListAsync(DateTime date, Action<FlickrResult<PhotoCollection>> callback)
        {
            InterestingnessGetListAsync(date, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets a list of photos from the most recent interstingness list.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void InterestingnessGetListAsync(Action<FlickrResult<PhotoCollection>> callback)
        {
            InterestingnessGetListAsync(DateTime.MinValue, PhotoSearchExtras.None, 0, 0, callback);
        }

        /// <summary>
        /// Gets a list of photos from the most recent interstingness list.
        /// </summary>
        /// <param name="date">The date to return the interestingness photos for.</param>
        /// <param name="extras">The extra parameters to return along with the search results.
        /// See <see cref="PhotoSearchOptions"/> for more details.</param>
        /// <param name="perPage">The number of results to return per page.</param>
        /// <param name="page">The page of the results to return.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void InterestingnessGetListAsync(DateTime date, PhotoSearchExtras extras, int page, int perPage, Action<FlickrResult<PhotoCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.interestingness.getList");

            if (date > DateTime.MinValue) parameters.Add("date", date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (extras != PhotoSearchExtras.None)
                parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            GetResponseAsync<PhotoCollection>(parameters, callback);
        }
    }
}
