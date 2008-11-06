using System;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for PhotoInfoUsage.
	/// </summary>
	[System.Serializable]
	public class PhotoInfoUsage
	{
		private int _canBlog;
		private int _canDownload;
		private int _canPrint;

		/// <summary>
		/// Specifies if the user allows blogging of this photos. 1 = Yes, 0 = No.
		/// </summary>
		[XmlAttribute("canblog", Form=XmlSchemaForm.Unqualified)]
		public int CanBlog { get { return _canBlog; } set { _canBlog = value; } }

		/// <summary>
		/// Specifies if the user allows downloading of this photos. 1 = Yes, 0 = No.
		/// </summary>
		[XmlAttribute("candownload", Form=XmlSchemaForm.Unqualified)]
		public int CanDownload { get { return _canDownload; } set { _canDownload = value; } }

		/// <summary>
		/// Specifies if the user allows printing of this photos. 1 = Yes, 0 = No.
		/// </summary>
		[XmlAttribute("canprint", Form=XmlSchemaForm.Unqualified)]
		public int CanPrint { get { return _canPrint; } set { _canPrint = value; } }

		/// <summary>
		/// Usage allowances of a photo, based on the users permissions.
		/// </summary>
		public PhotoInfoUsage()
		{
		}
	}
}
