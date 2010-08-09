using System;
using System.Collections.Generic;
using System.Text;

namespace FlickrNet
{
    /// <summary>
    /// A enumeration containing the list of current license types.
    /// </summary>
    public enum LicenseType
    {
        /// <summary>
        /// All Rights Reserved.
        /// </summary>
        AllRightsReserved = 0,
        /// <summary>
        /// Creative Commons: Attribution Non-Commercial, Share-alike License.
        /// </summary>
        AttributionNoncommercialShareAlikeCC = 1,
        /// <summary>
        /// Creative Commons: Attribution Non-Commercial License.
        /// </summary>
        AttributionNoncommercialCC = 2,
        /// <summary>
        /// Creative Commons: Attribution Non-Commercial, No Derivatives License.
        /// </summary>
        AttributionNoncommercialNoDerivativesCC = 3,
        /// <summary>
        /// Creative Commons: Attribution License.
        /// </summary>
        AttributionCC = 4,
        /// <summary>
        /// Creative Commons: Attribution Share-alike License.
        /// </summary>
        AttributionShareAlikeCC = 5,
        /// <summary>
        /// Creative Commons: Attribution No Derivatives License.
        /// </summary>
        AttributionNoDerivativesCC = 6,
        /// <summary>
        /// No Known Copyright Resitrctions (Flickr Commons).
        /// </summary>
        NoKnownCopyrightRestrictions = 7,
        /// <summary>
        /// United States Government Work
        /// </summary>
        UnitedStatesGovernmentWork = 8
    }
}
