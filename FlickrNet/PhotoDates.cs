using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// The date information for a photo.
	/// </summary>
	[System.Serializable]
	public class PhotoDates
	{
		/// <summary>
		/// The date the photo was posted (or uploaded).
		/// </summary>
		[XmlIgnore]
		public DateTime PostedDate
		{
			get { return Utils.UnixTimestampToDate(raw_posted); }
		}

		/// <summary>
		/// The raw timestamp for the date the photo was posted.
		/// </summary>
		/// <remarks>Use <see cref="PhotoDates.PostedDate"/> instead.</remarks>
		[XmlAttribute("posted", Form=XmlSchemaForm.Unqualified)]
		public long raw_posted;

		/// <summary>
		/// The date the photo was taken.
		/// </summary>
		[XmlIgnore]
		public DateTime TakenDate
		{
			get { return DateTime.Parse(raw_taken); }
		}

		/// <summary>
		/// The raw timestamp for the date the photo was taken.
		/// </summary>
		/// <remarks>Use <see cref="PhotoDates.TakenDate"/> instead.</remarks>
		[XmlAttribute("taken", Form=XmlSchemaForm.Unqualified)]
		public string raw_taken;

		/// <summary>
		/// The granularity of the taken date.
		/// </summary>
		[XmlAttribute("takengranularity", Form=XmlSchemaForm.Unqualified)]
		public int TakenGranularity;

		/// <summary>
		/// The raw timestamp for the date the photo was last updated.
		/// </summary>
		[XmlAttribute("lastupdate", Form=XmlSchemaForm.Unqualified)]
		public long raw_lastupdate;

		/// <summary>
		/// The date the photo was last updated (includes comments, tags, title, description etc).
		/// </summary>
		[XmlIgnore()]
		public DateTime LastUpdated
		{
			get{ return Utils.UnixTimestampToDate(raw_lastupdate); }
		}

	}

}
