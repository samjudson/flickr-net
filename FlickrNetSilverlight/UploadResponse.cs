using System;

namespace FlickrNet
{
	/// <summary>
	/// Information returned by the UploadPicture url.
	/// </summary>
	public sealed class UploadResponse : IFlickrParsable
	{
		/// <summary>
		/// If the upload succeeded then this contains the id of the photo. Otherwise it will be zero.
		/// </summary>
		public string PhotoId { get; private set; }

		/// <summary>
		/// The ticket id, if using Asynchronous uploading.
		/// </summary>
		public string TicketId { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            throw new NotImplementedException();
        }
    }

}
