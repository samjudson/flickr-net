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
        /// <returns>An array of <see cref="ActivityItem"/> instances.</returns>
        public ActivityItem[] ActivityUserPhotos()
        {
            return ActivityUserPhotos(null);
        }

        /// <summary>
        /// Returns a list of recent activity on photos belonging to the calling user.
        /// </summary>
        /// <remarks>
        /// <b>Do not poll this method more than once an hour.</b>
        /// </remarks>
        /// <param name="timePeriod">The number of days or hours you want to get activity for.</param>
        /// <param name="timeType">'d' for days, 'h' for hours.</param>
        /// <returns>An array of <see cref="ActivityItem"/> instances.</returns>
        public ActivityItem[] ActivityUserPhotos(int timePeriod, string timeType)
        {
            if (timePeriod == 0)
                throw new ArgumentOutOfRangeException("timePeriod", "Time Period should be greater than 0");

            if (timeType == null)
                throw new ArgumentNullException("timeType");

            if (timeType != "d" && timeType != "h")
                throw new ArgumentOutOfRangeException("timeType", "Time type must be 'd' or 'h'");

            return ActivityUserPhotos(timePeriod + timeType);
        }

        private ActivityItem[] ActivityUserPhotos(string timeframe)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.activity.userPhotos");
            if (timeframe != null && timeframe.Length > 0) parameters.Add("timeframe", timeframe);

            FlickrNet.Response response = GetResponseCache(parameters);
            if (response.Status == ResponseStatus.OK)
            {
                XmlNodeList list = response.AllElements[0].SelectNodes("item");
                ActivityItem[] items = new ActivityItem[list.Count];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = new ActivityItem(list[i]);
                }
                return items;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }

        /// <summary>
        /// Returns a list of recent activity on photos commented on by the calling user.
        /// </summary>
        /// <remarks>
        /// <b>Do not poll this method more than once an hour.</b>
        /// </remarks>
        /// <returns></returns>
        public ActivityItem[] ActivityUserComments(int page, int perPage)
        {
            Hashtable parameters = new Hashtable();
            parameters.Add("method", "flickr.activity.userComments");
            if (page > 0) parameters.Add("page", page);
            if (perPage > 0) parameters.Add("per_page", perPage);

            FlickrNet.Response response = GetResponseCache(parameters);
            if (response.Status == ResponseStatus.OK)
            {
                XmlNodeList list = response.AllElements[0].SelectNodes("item");
                ActivityItem[] items = new ActivityItem[list.Count];
                for (int i = 0; i < items.Length; i++)
                {
                    items[i] = new ActivityItem(list[i]);
                }
                return items;
            }
            else
            {
                throw new FlickrApiException(response.Error);
            }
        }


    }
}
