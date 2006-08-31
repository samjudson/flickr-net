using System;
using System.Xml.Serialization;

namespace FlickrNet
{
	/// <summary>
	/// Geo-taggin accuracy. Used in <see cref="PhotoSearchOptions.Accuracy"/> and <see cref="BoundaryBox.Accuracy"/>.
	/// </summary>
	/// <remarks>
	/// Level descriptions are only approximate.
	/// </remarks>
	public enum GeoAccuracy
	{
		/// <summary>
		/// No accuracy level specified.
		/// </summary>
		[XmlEnum("0")]
		None = 0,
		/// <summary>
		/// World level, level 1.
		/// </summary>
		[XmlEnum("1")]
		World = 1,
		/// <summary>
		/// Level 2
		/// </summary>
		[XmlEnum("2")]
		Level2 = 2,
		/// <summary>
		/// Level 3 - approximately Country level.
		/// </summary>
		[XmlEnum("3")]
		Country = 3,
		/// <summary>
		/// Level 4
		/// </summary>
		[XmlEnum("4")]
		Level4 = 4,
		/// <summary>
		/// Level 5
		/// </summary>
		[XmlEnum("5")]
		Level5 = 5,
		/// <summary>
		/// Level 6 - approximately Region level
		/// </summary>
		[XmlEnum("6")]
		Region = 6,
		/// <summary>
		/// Level 7
		/// </summary>
		[XmlEnum("7")]
		Level7 = 7,
		/// <summary>
		/// Level 8
		/// </summary>
		[XmlEnum("8")]
		Level8 = 8,
		/// <summary>
		/// Level 9
		/// </summary>
		[XmlEnum("9")]
		Level9 = 9,
		/// <summary>
		/// Level 10
		/// </summary>
		[XmlEnum("10")]
		Level10 = 10,
		/// <summary>
		/// Level 11 - approximately City level
		/// </summary>
		[XmlEnum("11")]
		City = 11,
		/// <summary>
		/// Level 12
		/// </summary>
		[XmlEnum("12")]
		Level12 = 12,
		/// <summary>
		/// Level 13
		/// </summary>
		[XmlEnum("13")]
		Level13 = 13,
		/// <summary>
		/// Level 14
		/// </summary>
		[XmlEnum("14")]
		Level14 = 14,
		/// <summary>
		/// Level 15
		/// </summary>
		[XmlEnum("15")]
		Level15 = 15,
		/// <summary>
		/// Street level (16) - the most accurate level and the default.
		/// </summary>
		[XmlEnum("16")]
		Street = 16
	}
}
