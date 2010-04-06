using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Gets a list of contacts for the logged in user.
        /// Requires authentication.
        /// </summary>
        /// <returns>An instance of the <see cref="ContactCollection"/> class containing the list of contacts.</returns>
        public ContactCollection ContactsGetList()
        {
            return ContactsGetList(null, 0, 0);
        }

        /// <summary>
        /// Gets a list of contacts for the logged in user.
        /// Requires authentication.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of contacts to return per page. If this argument is omitted, it defaults to 1000. The maximum allowed value is 1000.</param>
        /// <returns>An instance of the <see cref="ContactCollection"/> class containing the list of contacts.</returns>
        public ContactCollection ContactsGetList(int page, int perPage)
        {
            return ContactsGetList(null, page, perPage);
        }

        /// <summary>
        /// Gets a list of contacts for the logged in user.
        /// Requires authentication.
        /// </summary>
        /// <param name="filter">An optional filter of the results. The following values are valid: "friends", "family", "both", "neither".</param>
        /// <returns>An instance of the <see cref="ContactCollection"/> class containing the list of contacts.</returns>
        public ContactCollection ContactsGetList(string filter)
        {
            return ContactsGetList(filter, 0, 0);
        }

        /// <summary>
        /// Gets a list of contacts for the logged in user.
        /// Requires authentication.
        /// </summary>
        /// <param name="filter">An optional filter of the results. The following values are valid: "friends", "family", "both", "neither".</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of contacts to return per page. If this argument is omitted, it defaults to 1000. The maximum allowed value is 1000.</param>
        /// <returns>An instance of the <see cref="ContactCollection"/> class containing the list of contacts.</returns>
        public ContactCollection ContactsGetList(string filter, int page, int perPage)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.contacts.getList");
            if (!String.IsNullOrEmpty(filter)) parameters.Add("filter", filter);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<ContactCollection>(parameters);
        }

        /// <summary>
        /// Return a list of contacts for a user who have recently uploaded photos along with the total count of photos uploaded.
        /// </summary>
        /// <remarks>
        /// This method is still considered experimental. We don't plan for it to change or to go away but so long as this notice is present you should write your code accordingly.
        /// </remarks>
        /// <returns>List of contacts for the authenticated user.</returns>
        public ContactCollection ContactsGetListRecentlyUploaded()
        {
            return ContactsGetListRecentlyUploaded(DateTime.MinValue, null);
        }

        /// <summary>
        /// Return a list of contacts for a user who have recently uploaded photos along with the total count of photos uploaded.
        /// </summary>
        /// <remarks>
        /// This method is still considered experimental. We don't plan for it to change or to go away but so long as this notice is present you should write your code accordingly.
        /// </remarks>
        /// <param name="filter">Limit the result set to all contacts or only those who are friends or family. Valid options are:
        /// "ff" friends and family, and "all" all your contacts.
        /// Default value is "all".</param>
        /// <returns>List of contacts for the authenticated user.</returns>
        public ContactCollection ContactsGetListRecentlyUploaded(string filter)
        {
            return ContactsGetListRecentlyUploaded(DateTime.MinValue, filter);
        }

        /// <summary>
        /// Return a list of contacts for a user who have recently uploaded photos along with the total count of photos uploaded.
        /// </summary>
        /// <remarks>
        /// This method is still considered experimental. We don't plan for it to change or to go away but so long as this notice is present you should write your code accordingly.
        /// </remarks>
        /// <param name="dateLastUpdated">Limits the resultset to contacts that have uploaded photos since this date. The default offset is (1) hour and the maximum (24) hours.</param>
        /// <returns>List of contacts for the authenticated user.</returns>
        public ContactCollection ContactsGetListRecentlyUploaded(DateTime dateLastUpdated)
        {
            return ContactsGetListRecentlyUploaded(dateLastUpdated, null);
        }

        /// <summary>
        /// Return a list of contacts for a user who have recently uploaded photos along with the total count of photos uploaded.
        /// </summary>
        /// <remarks>
        /// This method is still considered experimental. We don't plan for it to change or to go away but so long as this notice is present you should write your code accordingly.
        /// </remarks>
        /// <param name="dateLastUpdated">Limits the resultset to contacts that have uploaded photos since this date. The default offset is (1) hour and the maximum (24) hours.</param>
        /// <param name="filter">Limit the result set to all contacts or only those who are friends or family. Valid options are:
        /// "ff" friends and family, and "all" all your contacts.
        /// Default value is "all".</param>
        /// <returns>List of contacts for the authenticated user.</returns>
        public ContactCollection ContactsGetListRecentlyUploaded(DateTime dateLastUpdated, string filter)
        {
            CheckRequiresAuthentication();

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.contacts.getListRecentlyUploaded");
            if( dateLastUpdated != DateTime.MinValue) parameters.Add("date_last_updated", UtilityMethods.DateToUnixTimestamp(dateLastUpdated));
            if( !String.IsNullOrEmpty(filter) ) parameters.Add("filter", filter);

            return GetResponseNoCache<ContactCollection>(parameters);

        }

        /// <summary>
        /// Gets a list of the given users contact, or those that are publically avaiable.
        /// </summary>
        /// <param name="userId">The Id of the user who's contacts you want to return.</param>
        /// <returns>An instance of the <see cref="ContactCollection"/> class containing the list of contacts.</returns>
        public ContactCollection ContactsGetPublicList(string userId)
        {
            return ContactsGetPublicList(userId, 0, 0);
        }

        /// <summary>
        /// Gets a list of the given users contact, or those that are publically avaiable.
        /// </summary>
        /// <param name="userId">The Id of the user who's contacts you want to return.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of contacts to return per page. If this argument is omitted, it defaults to 1000. The maximum allowed value is 1000.</param>
        /// <returns>An instance of the <see cref="ContactCollection"/> class containing the list of contacts.</returns>
        public ContactCollection ContactsGetPublicList(string userId, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.contacts.getPublicList");
            parameters.Add("user_id", userId);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            return GetResponseCache<ContactCollection>(parameters);
        }

    }
}
