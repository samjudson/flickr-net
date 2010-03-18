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
        public PhotoCollection InterestingnessGetList(PhotoSearchExtras extras, int page, int perPage)
        {
            return InterestingnessGetList(DateTime.MinValue, extras, page, perPage);
        }

        /// <summary>
        /// Gets a list of photos from the interstingness list for the specified date.
        /// </summary>
        /// <param name="date">The date to return the interestingness list for.</param>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        public PhotoCollection InterestingnessGetList(DateTime date)
        {
            return InterestingnessGetList(date, PhotoSearchExtras.All, 0, 0);
        }

        /// <summary>
        /// Gets a list of photos from the most recent interstingness list.
        /// </summary>
        /// <returns><see cref="PhotoCollection"/> instance containing list of photos.</returns>
        public PhotoCollection InterestingnessGetList()
        {
            return InterestingnessGetList(DateTime.MinValue, PhotoSearchExtras.All, 0, 0);
        }

        /// <summary>
        /// Gets a list of photos from the most recent interstingness list.
        /// </summary>
        /// <param name="date">The date to return the interestingness photos for.</param>
        /// <param name="extras">The extra parameters to return along with the search results.
        /// See <see cref="PhotoSearchOptions"/> for more details.</param>
        /// <param name="perPage">The number of results to return per page.</param>
        /// <param name="page">The page of the results to return.</param>
        /// <returns></returns>
        public PhotoCollection InterestingnessGetList(DateTime date, PhotoSearchExtras extras, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.interestingness.getList");

            if (date > DateTime.MinValue) parameters.Add("date", date.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (extras != PhotoSearchExtras.None)
                parameters.Add("extras", UtilityMethods.ExtrasToString(extras));

            return GetResponseCache<PhotoCollection>(parameters);
        }
    }
}
