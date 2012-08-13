using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Get a list of referring domains for a collection.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetCollectionDomains(DateTime date)
        {
            return StatsGetCollectionDomains(date, null, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for a collection.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="collectionId">The id of the collection to get stats for. If not provided, stats for all collections will be returned.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetCollectionDomains(DateTime date, string collectionId)
        {
            return StatsGetCollectionDomains(date, collectionId, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for a collection.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetCollectionDomains(DateTime date, int page, int perPage)
        {
            return StatsGetCollectionDomains(date, null, page, perPage);
        }

        /// <summary>
        /// Get a list of referring domains for a collection.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="collectionId">The id of the collection to get stats for. If not provided, stats for all collections will be returned.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetCollectionDomains(DateTime date, string collectionId, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getCollectionDomains");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            if (!String.IsNullOrEmpty(collectionId)) parameters.Add("collection_id", UtilityMethods.CleanCollectionId(collectionId));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatDomainCollection>(parameters);
        }

        /// <summary>
        /// Gets the collection of CSV files of archived stats from Flickr.
        /// </summary>
        /// <remarks>
        /// Archived files only available till the 1st June 2010.
        /// </remarks>
        /// <returns></returns>
        public CsvFileCollection StatsGetCsvFiles()
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getCSVFiles");

            return GetResponseCache<CsvFileCollection>(parameters);
        }

        /// <summary>
        /// Get a list of referring domains for all photos.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotoDomains(DateTime date)
        {
            return StatsGetPhotoDomains(date, null, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for a photo.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="photoId">The id of the photo to get stats for. If not provided, stats for all photos will be returned.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotoDomains(DateTime date, string photoId)
        {
            return StatsGetPhotoDomains(date, photoId, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for all photos.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotoDomains(DateTime date, int page, int perPage)
        {
            return StatsGetPhotoDomains(date, null, page, perPage);
        }

        /// <summary>
        /// Get a list of referring domains for a photo.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="photoId">The id of the photo to get stats for. If not provided, stats for all photos will be returned.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotoDomains(DateTime date, string photoId, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotoDomains");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            if (!String.IsNullOrEmpty(photoId)) parameters.Add("photo_id", photoId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatDomainCollection>(parameters);
        }

        /// <summary>
        /// Get a list of referring domains for a photostream.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotostreamDomains(DateTime date)
        {
            return StatsGetPhotostreamDomains(date, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for a photostream.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotostreamDomains(DateTime date, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotostreamDomains");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatDomainCollection>(parameters);
        }

        /// <summary>
        /// Get a list of referring domains for a photoset.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotosetDomains(DateTime date)
        {
            return StatsGetPhotosetDomains(date, null, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for a photoset.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="photosetId">The id of the photoset to get stats for. If not provided, stats for all sets will be returned.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotosetDomains(DateTime date, string photosetId)
        {
            return StatsGetPhotosetDomains(date, photosetId, 0, 0);
        }

        /// <summary>
        /// Get a list of referring domains for a photoset.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotosetDomains(DateTime date, int page, int perPage)
        {
            return StatsGetPhotosetDomains(date, null, page, perPage);
        }

        /// <summary>
        /// Get a list of referring domains for a photoset.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day.</param>
        /// <param name="photosetId">The id of the photoset to get stats for. If not provided, stats for all sets will be returned.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of domains to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns></returns>
        public StatDomainCollection StatsGetPhotosetDomains(DateTime date, string photosetId, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotosetDomains");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            if (!String.IsNullOrEmpty(photosetId)) parameters.Add("photoset_id", photosetId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatDomainCollection>(parameters);
        }

        /// <summary>
        /// Returns the number of views on the given date for the given collection. Only <see cref="Stats.Views"/> will be populated.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="collectionId">The collection to return stats for.</param>
        /// <returns>The stats. Only <see cref="Stats.Views"/> will be populated.</returns>
        public Stats StatsGetCollectionStats(DateTime date, string collectionId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getCollectionStats");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("collection_id", UtilityMethods.CleanCollectionId(collectionId));

            return GetResponseCache<Stats>(parameters);
        }

        /// <summary>
        /// Returns the number of views, comments and favorites on the given date for the given photo.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="photoId">The photo to return stats for.</param>
        /// <returns>The stats.</returns>
        public Stats StatsGetPhotoStats(DateTime date, string photoId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotoStats");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("photo_id", photoId);

            return GetResponseCache<Stats>(parameters);
        }

        /// <summary>
        /// Returns the number of views on the given date for the users photostream. Only <see cref="Stats.Views"/> will be populated.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <returns>The stats. Only <see cref="Stats.Views"/> will be populated.</returns>
        public Stats StatsGetPhotostreamStats(DateTime date)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotostreamStats");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));

            return GetResponseCache<Stats>(parameters);
        }

        /// <summary>
        /// Returns the number of views and comments on the given date for the given photoset. Only <see cref="Stats.Views"/> and <see cref="Stats.Comments"/> will be populated.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="photosetId">The photoset to return stats for.</param>
        /// <returns>The stat. Only <see cref="Stats.Views"/> and <see cref="Stats.Comments"/> will be populated.</returns>
        public Stats StatsGetPhotosetStats(DateTime date, string photosetId)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotosetStats");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("photoset_id", photosetId);

            return GetResponseCache<Stats>(parameters);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photo.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain)
        {
            return StatsGetPhotoReferrers(date, domain, null, 0, 0);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photo.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="photoId">The photo to return referrers for. If missing then referrers for all photos will be returned.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain, string photoId)
        {
            return StatsGetPhotoReferrers(date, domain, photoId, 0, 0);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photo.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain, int page, int perPage)
        {
            return StatsGetPhotoReferrers(date, domain, null, page, perPage);
        }


        /// <summary>
        /// Get a list of referrers from a given domain to a photo.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="photoId">The photo to return referrers for. If missing then referrers for all photos will be returned.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotoReferrers(DateTime date, string domain, string photoId, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotoReferrers");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("domain", domain);
            if (!String.IsNullOrEmpty(photoId)) parameters.Add("photo_id", photoId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatReferrerCollection>(parameters);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photoset.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain)
        {
            return StatsGetPhotosetReferrers(date, domain, null, 0, 0);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photoset.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="photosetId">The photoset to return referrers for. If missing then referrers for all photosets will be returned.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain, string photosetId)
        {
            return StatsGetPhotosetReferrers(date, domain, photosetId, 0, 0);
        }


        /// <summary>
        /// Get a list of referrers from a given domain to a photoset.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain, int page, int perPage)
        {
            return StatsGetPhotosetReferrers(date, domain, null, page, perPage);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photoset.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="photosetId">The photoset to return referrers for. If missing then referrers for all photosets will be returned.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotosetReferrers(DateTime date, string domain, string photosetId, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotosetReferrers");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("domain", domain);
            if (!String.IsNullOrEmpty(photosetId)) parameters.Add("photoset_id", photosetId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatReferrerCollection>(parameters);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a collection.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain)
        {
            return StatsGetCollectionReferrers(date, domain, null, 0, 0);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a collection.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="collectionId">The collection to return referrers for. If missing then referrers for all photosets will be returned.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain, string collectionId)
        {
            return StatsGetCollectionReferrers(date, domain, collectionId, 0, 0);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a collection.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain, int page, int perPage)
        {
            return StatsGetCollectionReferrers(date, domain, null, page, perPage);
        }


        /// <summary>
        /// Get a list of referrers from a given domain to a collection.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="collectionId">The collection to return referrers for. If missing then referrers for all photosets will be returned.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetCollectionReferrers(DateTime date, string domain, string collectionId, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getCollectionReferrers");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("domain", domain);
            if (!String.IsNullOrEmpty(collectionId)) parameters.Add("collection_id", UtilityMethods.CleanCollectionId(collectionId));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatReferrerCollection>(parameters);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photostream.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotostreamReferrers(DateTime date, string domain)
        {
            return StatsGetPhotostreamReferrers(date, domain, 0, 0);
        }

        /// <summary>
        /// Get a list of referrers from a given domain to a photostream.
        /// </summary>
        /// <param name="date">The date to return stats for.</param>
        /// <param name="domain">The domain to return referrers for. This should be a hostname (eg: "flickr.com") with no protocol or pathname.</param>
        /// <param name="page">The page of the results to return. Default is 1.</param>
        /// <param name="perPage">The number of referrers to return per page. The default is 25 and the maximum is 100.</param>
        /// <returns>The referrers.</returns>
        public StatReferrerCollection StatsGetPhotostreamReferrers(DateTime date, string domain, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.stats.getPhotostreamReferrers");
            parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            parameters.Add("domain", domain);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<StatReferrerCollection>(parameters);
        }

        /// <summary>
        /// Get the overall view counts for an account.
        /// </summary>
        /// <returns>The overall number of views.</returns>
        public StatViews StatsGetTotalViews()
        {
            return StatsGetTotalViews(DateTime.MinValue);
        }

        /// <summary>
        /// Get the overall view counts for an account on a given date.
        /// </summary>
        /// <param name="date">The date to return the overall view count for.</param>
        /// <returns>The overall number of views.</returns>
        public StatViews StatsGetTotalViews(DateTime date)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.stats.getTotalViews");
            if (date != DateTime.MinValue) parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));

            return GetResponseCache<StatViews>(parameters);
        }

        /// <summary>
        /// List the photos with the most views, comments or favorites.
        /// </summary>
        /// <returns>A list of <see cref="PopularPhoto"/> instances.</returns>
        public PopularPhotoCollection StatsGetPopularPhotos()
        {
            return StatsGetPopularPhotos(DateTime.MinValue, PopularitySort.None, 0, 0);
        }

        /// <summary>
        /// List the photos with the most views, comments or favorites.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day. If no date is provided, all time view counts will be returned.</param>
        /// <returns>A list of <see cref="PopularPhoto"/> instances.</returns>
        public PopularPhotoCollection StatsGetPopularPhotos(DateTime date)
        {
            return StatsGetPopularPhotos(date, PopularitySort.None, 0, 0);
        }

        /// <summary>
        /// List the photos with the most views, comments or favorites.
        /// </summary>
        /// <param name="sort">The order in which to sort returned photos. Defaults to views. The possible values are views, comments and favorites. </param>
        /// <returns>A list of <see cref="PopularPhoto"/> instances.</returns>
        public PopularPhotoCollection StatsGetPopularPhotos(PopularitySort sort)
        {
            return StatsGetPopularPhotos(DateTime.MinValue, sort, 0, 0);
        }

        /// <summary>
        /// List the photos with the most views, comments or favorites.
        /// </summary>
        /// <param name="sort">The order in which to sort returned photos. Defaults to views. The possible values are views, comments and favorites. </param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns>A list of <see cref="PopularPhoto"/> instances.</returns>
        public PopularPhotoCollection StatsGetPopularPhotos(PopularitySort sort, int page, int perPage)
        {
            return StatsGetPopularPhotos(DateTime.MinValue, sort, page, perPage);
        }

        /// <summary>
        /// List the photos with the most views, comments or favorites.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day. If no date is provided, all time view counts will be returned.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns>A list of <see cref="PopularPhoto"/> instances.</returns>
        public PopularPhotoCollection StatsGetPopularPhotos(DateTime date, int page, int perPage)
        {
            return StatsGetPopularPhotos(date, PopularitySort.None, page, perPage);
        }

        /// <summary>
        /// List the photos with the most views, comments or favorites.
        /// </summary>
        /// <param name="date">Stats will be returned for this date. A day according to Flickr Stats starts at midnight GMT for all users, and timestamps will automatically be rounded down to the start of the day. If no date is provided, all time view counts will be returned.</param>
        /// <param name="sort">The order in which to sort returned photos. Defaults to views. The possible values are views, comments and favorites. </param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 25. The maximum allowed value is 100.</param>
        /// <returns>A list of <see cref="PopularPhoto"/> instances.</returns>
        public PopularPhotoCollection StatsGetPopularPhotos(DateTime date, PopularitySort sort, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.stats.getPopularPhotos");
            if (date != DateTime.MinValue) parameters.Add("date", UtilityMethods.DateToUnixTimestamp(date));
            if (sort != PopularitySort.None) parameters.Add("sort", UtilityMethods.SortOrderToString(sort));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PopularPhotoCollection>(parameters);
        }

    }
}
