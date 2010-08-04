using System;

namespace FlickrNet
{
    /// <summary>
    /// Geo-taggin accuracy. Used in <see cref="PhotoSearchOptions.Accuracy"/> and <see cref="BoundaryBox.Accuracy"/>.
    /// </summary>
    /// <remarks>
    /// Level descriptions are only approximate.
    /// </remarks>
    [Serializable]
    public enum GeoAccuracy
    {
        /// <summary>
        /// No accuracy level specified.
        /// </summary>
        None = 0,
        /// <summary>
        /// World level, level 1.
        /// </summary>
        World = 1,
        /// <summary>
        /// Level 2
        /// </summary>
        Level2 = 2,
        /// <summary>
        /// Level 3 - approximately Country level.
        /// </summary>
        Country = 3,
        /// <summary>
        /// Level 4
        /// </summary>
        Level4 = 4,
        /// <summary>
        /// Level 5
        /// </summary>
        Level5 = 5,
        /// <summary>
        /// Level 6 - approximately Region level
        /// </summary>
        Region = 6,
        /// <summary>
        /// Level 7
        /// </summary>
        Level7 = 7,
        /// <summary>
        /// Level 8
        /// </summary>
        Level8 = 8,
        /// <summary>
        /// Level 9
        /// </summary>
        Level9 = 9,
        /// <summary>
        /// Level 10
        /// </summary>
        Level10 = 10,
        /// <summary>
        /// Level 11 - approximately City level
        /// </summary>
        City = 11,
        /// <summary>
        /// Level 12
        /// </summary>
        Level12 = 12,
        /// <summary>
        /// Level 13
        /// </summary>
        Level13 = 13,
        /// <summary>
        /// Level 14
        /// </summary>
        Level14 = 14,
        /// <summary>
        /// Level 15
        /// </summary>
        Level15 = 15,
        /// <summary>
        /// Street level (16) - the most accurate level and the default.
        /// </summary>
        Street = 16
    }
}
