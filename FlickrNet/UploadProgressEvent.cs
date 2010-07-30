using System;

namespace FlickrNet
{
	/// <summary>
	/// Event arguments for a <see cref="Flickr.OnUploadProgress"/> event.
	/// </summary>
	public class UploadProgressEventArgs : EventArgs
	{
		private bool _uploadComplete;

		private long _bytes;

		/// <summary>
		/// Number of bytes transfered so far.
		/// </summary>
		public long Bytes
		{
			get { return _bytes; }
		}

		/// <summary>
		/// True if all bytes have been uploaded.
		/// </summary>
		public bool UploadComplete
		{
			get { return _uploadComplete; }
		}

		internal UploadProgressEventArgs(long bytes, bool complete)
		{
			_bytes = bytes;
			_uploadComplete = complete;
		}
	}
}
