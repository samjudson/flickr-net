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
        public NamespaceCollection MachineTagsGetNamespaces()
        {
            return MachineTagsGetNamespaces(null, 0, 0);
        }

        /// <summary>
        /// Return a list of unique namespaces, in alphabetical order.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public NamespaceCollection MachineTagsGetNamespaces(int page, int perPage)
        {
            return MachineTagsGetNamespaces(null, page, perPage);
        }

        /// <summary>
        /// Return a list of unique namespaces, optionally limited by a given predicate, in alphabetical order.
        /// </summary>
        /// <param name="predicate">Limit the list of namespaces returned to those that have the following predicate.</param>
        /// <returns></returns>
        public NamespaceCollection MachineTagsGetNamespaces(string predicate)
        {
            return MachineTagsGetNamespaces(predicate, 0, 0);
        }

        /// <summary>
        /// Return a list of unique namespaces, optionally limited by a given predicate, in alphabetical order.
        /// </summary>
        /// <param name="predicate">Limit the list of namespaces returned to those that have the following predicate.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of photos to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public NamespaceCollection MachineTagsGetNamespaces(string predicate, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getNamespaces");
            if (!String.IsNullOrEmpty(predicate)) parameters.Add("predicate", predicate);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<NamespaceCollection>(parameters);

        }

        /// <summary>
        /// Return a list of unique predicates, in alphabetical order.
        /// </summary>
        /// <returns></returns>
        public PredicateCollection MachineTagsGetPredicates()
        {
            return MachineTagsGetPredicates(null, 0, 0);
        }

        /// <summary>
        /// Return a list of unique predicates, in alphabetical order.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of namespaces to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PredicateCollection MachineTagsGetPredicates(int page, int perPage)
        {
            return MachineTagsGetPredicates(null, page, perPage);
        }

        /// <summary>
        /// Return a list of unique predicates, optionally limited by a given namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of predicates returned to those that have the following namespace.</param>
        /// <returns></returns>
        public PredicateCollection MachineTagsGetPredicates(string namespaceName)
        {
            return MachineTagsGetPredicates(namespaceName, 0, 0);
        }

        /// <summary>
        /// Return a list of unique predicates, optionally limited by a given namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of predicates returned to those that have the following namespace.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of namespaces to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PredicateCollection MachineTagsGetPredicates(string namespaceName, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getPredicates");
            if (!String.IsNullOrEmpty(namespaceName)) parameters.Add("namespace", namespaceName);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PredicateCollection>(parameters);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, in alphabetical order.
        /// </summary>
        /// <returns></returns>
        public PairCollection MachineTagsGetPairs()
        {
            return MachineTagsGetPairs(null, null, 0, 0);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, in alphabetical order.
        /// </summary>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of pairs to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PairCollection MachineTagsGetPairs(int page, int perPage)
        {
            return MachineTagsGetPairs(null, null, page, perPage);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, optionally limited by predicate or namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of pairs returned to those that have the following namespace.</param>
        /// <param name="predicate">Limit the list of pairs returned to those that have the following predicate.</param>
        /// <returns></returns>
        public PairCollection MachineTagsGetPairs(string namespaceName, string predicate)
        {
            return MachineTagsGetPairs(namespaceName, predicate, 0, 0);
        }

        /// <summary>
        /// Return a list of unique namespace and predicate pairs, optionally limited by predicate or namespace, in alphabetical order.
        /// </summary>
        /// <param name="namespaceName">Limit the list of pairs returned to those that have the following namespace.</param>
        /// <param name="predicate">Limit the list of pairs returned to those that have the following predicate.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of pairs to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public PairCollection MachineTagsGetPairs(string namespaceName, string predicate, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getPairs");
            if (!String.IsNullOrEmpty(namespaceName)) parameters.Add("namespace", namespaceName);
            if (!String.IsNullOrEmpty(predicate)) parameters.Add("predicate", predicate);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<PairCollection>(parameters);
        }

        /// <summary>
        /// Return a list of unique values for a namespace and predicate.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetValues(string namespaceName, string predicate)
        {
            return MachineTagsGetValues(namespaceName, predicate, 0, 0);
        }

        /// <summary>
        /// Return a list of unique values for a namespace and predicate.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetValues(string namespaceName, string predicate, int page, int perPage)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            parameters.Add("method", "flickr.machinetags.getValues");
            parameters.Add("namespace", namespaceName);
            parameters.Add("predicate", predicate);
            if (page > 0) parameters.Add("page", page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (perPage > 0) parameters.Add("per_page", perPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            return GetResponseCache<ValueCollection>(parameters);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="addedSince">Only return machine tags values that have been added since this timestamp.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetRecentValues(DateTime addedSince)
        {
            return MachineTagsGetRecentValues(null, null, addedSince, 0, 0);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="addedSince">Only return machine tags values that have been added since this timestamp.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetRecentValues(DateTime addedSince, int page, int perPage)
        {
            return MachineTagsGetRecentValues(null, null, addedSince, page, perPage);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetRecentValues(string namespaceName, string predicate)
        {
            return MachineTagsGetRecentValues(namespaceName, predicate, DateTime.MinValue, 0, 0);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetRecentValues(string namespaceName, string predicate, int page, int perPage)
        {
            return MachineTagsGetRecentValues(namespaceName, predicate, DateTime.MinValue, page, perPage);
        }

        /// <summary>
        /// Fetch recently used (or created) machine tags values.
        /// </summary>
        /// <param name="namespaceName">The namespace that all values should be restricted to.</param>
        /// <param name="predicate">The predicate that all values should be restricted to.</param>
        /// <param name="addedSince">Only return machine tags values that have been added since this timestamp.</param>
        /// <param name="page">The page of results to return. If this argument is omitted, it defaults to 1.</param>
        /// <param name="perPage">Number of values to return per page. If this argument is omitted, it defaults to 100. The maximum allowed value is 500.</param>
        /// <returns></returns>
        public ValueCollection MachineTagsGetRecentValues(string namespaceName, string predicate, DateTime addedSince, int page, int perPage)
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

            return GetResponseCache<ValueCollection>(parameters);
        }

    }
}
