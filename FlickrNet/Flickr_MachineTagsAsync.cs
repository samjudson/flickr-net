using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Return a list of unique namespaces, in alphabetical order.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetNamespacesAsync(Action<FlickrResult<NamespaceCollection>> callback)
        {
            MachineTagsGetNamespacesAsync(null, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique namespaces, in alphabetical order.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetNamespacesAsync(int page, int perPage, Action<FlickrResult<NamespaceCollection>> callback)
        {
            MachineTagsGetNamespacesAsync(null, page, perPage, callback);
        }

        /// <summary>
        /// Return a list of unique namespaces, optionally limited by a given predicate, in alphabetical order.
        /// </summary>
        /// <param name="predicate">Limit the list of namespaces returned to those that have the following predicate.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetNamespacesAsync(string predicate, Action<FlickrResult<NamespaceCollection>> callback)
        {
            MachineTagsGetNamespacesAsync(predicate, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique namespaces, optionally limited by a given predicate, in alphabetical order.
        /// </summary>
        /// <param name="predicate">Limit the list of namespaces returned to those that have the following predicate.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetNamespacesAsync(string predicate, int page, int perPage, Action<FlickrResult<NamespaceCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getNamespaces");
            if (!String.IsNullOrEmpty(predicate)) parameters.Add("predicate", predicate);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<NamespaceCollection>(parameters, callback);

        }

        /// <summary>
        /// Return a list of unique predicates, in alphabetical order.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPredicatesAsync(Action<FlickrResult<PredicateCollection>> callback)
        {
            MachineTagsGetPredicatesAsync(null, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique predicates, in alphabetical order.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of namespaces to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPredicatesAsync(int page, int perPage, Action<FlickrResult<PredicateCollection>> callback)
        {
            MachineTagsGetPredicatesAsync(null, page, perPage, callback);
        }

        /// <summary>
        /// Return a list of unique predicates, optionally limited by a given namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of predicates returned to those that have the following namespace.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPredicatesAsync(string namespaceName, Action<FlickrResult<PredicateCollection>> callback)
        {
            MachineTagsGetPredicatesAsync(namespaceName, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique predicates, optionally limited by a given namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of predicates returned to those that have the following namespace.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of namespaces to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPredicatesAsync(string namespaceName, int page, int perPage, Action<FlickrResult<PredicateCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getPredicates");
            if (!String.IsNullOrEmpty(namespaceName)) parameters.Add("namespace", namespaceName);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PredicateCollection>(parameters, callback);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, in alphabetical order.
        /// </summary>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPairsAsync(Action<FlickrResult<PairCollection>> callback)
        {
            MachineTagsGetPairsAsync(null, null, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, in alphabetical order.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of pairs to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPairsAsync(int page, int perPage, Action<FlickrResult<PairCollection>> callback)
        {
            MachineTagsGetPairsAsync(null, null, page, perPage, callback);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, optionally limited by predicate or namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of pairs returned to those that have the following namespace.</param>
        /// <param name="predicate">Limit the list of pairs returned to those that have the following predicate.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPairsAsync(string namespaceName, string predicate, Action<FlickrResult<PairCollection>> callback)
        {
            MachineTagsGetPairsAsync(namespaceName, predicate, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, optionally limited by predicate or namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of pairs returned to those that have the following namespace.</param>
        /// <param name="predicate">Limit the list of pairs returned to those that have the following predicate.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of pairs to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetPairsAsync(string namespaceName, string predicate, int page, int perPage, Action<FlickrResult<PairCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getPairs");
            if (!String.IsNullOrEmpty(namespaceName)) parameters.Add("namespace", namespaceName);
            if (!String.IsNullOrEmpty(predicate)) parameters.Add("predicate", predicate);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<PairCollection>(parameters, callback);
        }

        /// <summary>
        /// Return a list of unique values for a namespace and predicate.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetValuesAsync(string namespaceName, string predicate, Action<FlickrResult<ValueCollection>> callback)
        {
            MachineTagsGetValuesAsync(namespaceName, predicate, 0, 0, callback);
        }

        /// <summary>
        /// Return a list of unique values for a namespace and predicate.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetValuesAsync(string namespaceName, string predicate, int page, int perPage, Action<FlickrResult<ValueCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getValues");
            parameters.Add("namespace", namespaceName);
            parameters.Add("predicate", predicate);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<ValueCollection>(parameters, callback);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="addedSince">Only return machine tags values that have been added since this timestamp.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetRecentValuesAsync(DateTime addedSince, Action<FlickrResult<ValueCollection>> callback)
        {
            MachineTagsGetRecentValuesAsync(null, null, addedSince, 0, 0, callback);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="addedSince">Only return machine tags values that have been added since this timestamp.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetRecentValuesAsync(DateTime addedSince, int page, int perPage, Action<FlickrResult<ValueCollection>> callback)
        {
            MachineTagsGetRecentValuesAsync(null, null, addedSince, page, perPage, callback);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetRecentValuesAsync(string namespaceName, string predicate, Action<FlickrResult<ValueCollection>> callback)
        {
            MachineTagsGetRecentValuesAsync(namespaceName, predicate, DateTime.MinValue, 0, 0, callback);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetRecentValuesAsync(string namespaceName, string predicate, int page, int perPage, Action<FlickrResult<ValueCollection>> callback)
        {
            MachineTagsGetRecentValuesAsync(namespaceName, predicate, DateTime.MinValue, page, perPage, callback);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="addedSince">Only return machine tags values that have been added since this timestamp.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void MachineTagsGetRecentValuesAsync(string namespaceName, string predicate, DateTime addedSince, int page, int perPage, Action<FlickrResult<ValueCollection>> callback)
        {
            if (String.IsNullOrEmpty(namespaceName) && String.IsNullOrEmpty(predicate) && addedSince == DateTime.MinValue)
            {
                throw new ArgumentException("Must supply one of namespaceName, predicate or addedSince");
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getRecentValues");
            if (!String.IsNullOrEmpty(namespaceName)) parameters.Add("namespace", namespaceName);
            if (!String.IsNullOrEmpty(predicate)) parameters.Add("predicate", predicate);
            if (addedSince != DateTime.MinValue) parameters.Add("added_since", UtilityMethods.DateToUnixTimestamp(addedSince));
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<ValueCollection>(parameters, callback);
        }

    }
}
