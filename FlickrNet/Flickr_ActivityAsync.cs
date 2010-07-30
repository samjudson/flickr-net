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
        /// Returns a list of recent activity on photos belonging to the calling user.
        /// </summary>
        /// <remarks>
        /// <b>Do not poll this method more than once an hour.</b>
        /// </remarks>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void ActivityUserPhotosAsync(Action<FlickrResult<ActivityItemCollection>> callback)
        {
            ActivityUserPhotosAsync(null, callback);
        }

        /// <summary>
        /// Returns a list of recent activity on photos belonging to the calling user.
        /// </summary>
        /// <remarks>
        /// <b>Do not poll this method more than once an hour.</b>
        /// </remarks>
        /// <param name="timePeriod">The number of days or hours you want to get activity for.</param>
        /// <param name="timeType">'d' for days, 'h' for hours.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void ActivityUserPhotosAsync(int timePeriod, string timeType, Action<FlickrResult<ActivityItemCollection>> callback)
        {
            if (timePeriod == 0)
                throw new ArgumentOutOfRangeException("timePeriod", "Time Period should be greater than 0");

            if (timeType == null)
                throw new ArgumentNullException("timeType");

            if (timeType != "d" && timeType != "h")
                throw new ArgumentOutOfRangeException("timeType", "Time type must be 'd' or 'h'");

            ActivityUserPhotosAsync(timePeriod + timeType, callback);
        }

        private void ActivityUserPhotosAsync(string timeframe, Action<FlickrResult<ActivityItemCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.activity.userPhotos");
            if (timeframe != null && timeframe.Length > 0) parameters.Add("timeframe", timeframe);

            GetResponseAsync<ActivityItemCollection>(parameters, callback);
        }

        /// <summary>
        /// Returns a list of recent activity on photos commented on by the calling user.
        /// </summary>
        /// <remarks>
        /// <b>Do not poll this method more than once an hour.</b>
        /// </remarks>
        /// <param name="page">The page of the activity to return.</param>
        /// <param name="perPage">The number of activities to return per page.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void ActivityUserCommentsAsync(int page, int perPage, Action<FlickrResult<ActivityItemCollection>> callback)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.activity.userComments");
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<ActivityItemCollection>(parameters, callback);
        }


    }
}
