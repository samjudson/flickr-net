using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// The status of a location suggestion.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public enum SuggestionStatus
    {
        /// <summary>
        /// The suggestion is in a pending state.
        /// </summary>
        Pending = 0,
        /// <summary>
        /// The suggestion has been approved.
        /// </summary>
        Approved = 1,
        /// <summary>
        /// The suggestion has been rejected.
        /// </summary>
        Rejected = 2
    }
}
