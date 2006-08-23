using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// The information about the number of photos a user has.
	/// </summary>
	[System.Serializable]
	public class PhotoCounts
	{
		/// <summary>
		/// An array of <see cref="PhotoCountInfo"/> instances. Null if no counts returned.
		/// </summary>
		[XmlElement("photocount", Form=XmlSchemaForm.Unqualified)]
		public PhotoCountInfo[] PhotoCountInfoCollection = new PhotoCountInfo[0];
	}

	/// <summary>
	/// The specifics of a particular count.
	/// </summary>
	[System.Serializable]
	public class PhotoCountInfo
	{
		/// <summary>Total number of photos between the FromDate and the ToDate.</summary>
		/// <remarks/>
		[XmlAttribute("count", Form=XmlSchemaForm.Unqualified)]
		public int PhotoCount;
    
		/// <summary>The From date as a <see cref="DateTime"/> object.</summary>
		[XmlIgnore()]
		public DateTime FromDate
		{
			get 
			{
				return Utils.UnixTimestampToDate(fromdate_raw);
			}
		}

		/// <summary>The To date as a <see cref="DateTime"/> object.</summary>
		[XmlIgnore()]
		public DateTime ToDate
		{
			get 
			{
				return Utils.UnixTimestampToDate(todate_raw);
			}
		}

		/// <summary>The original from date in unix timestamp format.</summary>
		/// <remarks/>
		[XmlAttribute("fromdate", Form=XmlSchemaForm.Unqualified)]
		public string fromdate_raw;
    
		/// <summary>The original to date in unix timestamp format.</summary>
		/// <remarks/>
		[XmlAttribute("todate", Form=XmlSchemaForm.Unqualified)]
		public string todate_raw;

	}
}