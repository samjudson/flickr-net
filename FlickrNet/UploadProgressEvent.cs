using System;

namespace FlickrNet
{
    /// <summary>
    /// Event arguments for a <see cref="Flickr.OnUploadProgress"/> event.
    /// </summary>
    public class UploadProgressEventArgs : EventArgs
    {
        private bool uploadComplete;

        private long bytes;

        /// <summary>
        /// Number of bytes transfered so far.
        /// </summary>
        public long Bytes
        {
            get { return bytes; }
        }

        /// <summary>
        /// True if all bytes have been uploaded.
        /// </summary>
        public bool UploadComplete
        {
            get { return uploadComplete; }
        }

        internal UploadProgressEventArgs(long bytes, bool complete)
        {
            this.bytes = bytes;
            this.uploadComplete = complete;
        }
    }
}
