using System;

namespace FlickrNet
{
    /// <summary>
    /// DateGranularity, used for setting taken date in <see cref="Flickr.PhotosSetDates(string, DateTime, DateGranularity)"/> 
    /// or <see cref="Flickr.PhotosSetDates(string, DateTime, DateTime, DateGranularity)"/>.
    /// </summary>
    public enum DateGranularity
    {
        /// <summary>
        /// The date specified is the exact date the photograph was taken.
        /// </summary>
        FullDate = 0,
        /// <summary>
        /// The date specified is the year and month the photograph was taken.
        /// </summary>
        YearMonthOnly = 4,
        /// <summary>
        /// The date specified is the year the photograph was taken.
        /// </summary>
        YearOnly = 6,
        /// <summary>
        /// The date is an approximation only and only the year is likely to be supplied.
        /// </summary>
        Circa = 8

    }
}
