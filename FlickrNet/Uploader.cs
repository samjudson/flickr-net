using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FlickrNet
{
	/// <summary>
	/// Information returned by the UploadPicture url.
	/// </summary>
	[XmlRoot("rsp")]
	public class Uploader
	{
		private ResponseStatus _status;
		private string _photoId;
		private int _code;
		private string _message;

		/// <summary>
		/// The status of the upload, either "ok" or "fail".
		/// </summary>
		[XmlElement("status", Form=XmlSchemaForm.Unqualified)]
		public ResponseStatus Status
		{
			get { return _status; }
			set { _status = value; }
		}

		/// <summary>
		/// If the upload succeeded then this contains the id of the photo. Otherwise it will be zero.
		/// </summary>
		[XmlElement("photoid", Form=XmlSchemaForm.Unqualified)]
		public string PhotoId
		{
			get { return _photoId; }
			set { _photoId = value; }
		}

		/// <summary>
		/// If the upload failed then this contains the error code.
		/// </summary>
		[XmlElement("error", Form=XmlSchemaForm.Unqualified)]
		public int Code
		{
			get { return _code; }
			set { _code = value; }
		}

		/// <summary>
		/// If the upload failed then this contains the error description.
		/// </summary>
		[XmlElement("verbose", Form=XmlSchemaForm.Unqualified)]
		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}
	}
}
