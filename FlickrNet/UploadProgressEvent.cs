using System;

namespace FlickrNet
{
    /// <summary>
    /// Event arguments for a <see cref="Flickr.OnUploadProgress"/> event.
    /// </summary>
    public class UploadProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Number of bytes transfered so far.
        /// </summary>
        public long BytesSent { get; internal set; }

        /// <summary>
        /// Total bytes to be sent
        /// </summary>
        public long TotalBytesToSend { get; internal set; }

        /// <summary>
        /// True if all bytes have been uploaded.
        /// </summary>
        public bool UploadComplete { get { return ProcessPercentage == 100; } }

        /// <summary>
        /// The percentage of the upload that has been completed.
        /// </summary>
        public int ProcessPercentage
        {
            get { return Convert.ToInt32(BytesSent * 100 / TotalBytesToSend); }
        }

        internal UploadProgressEventArgs()
        {
        }

        internal UploadProgressEventArgs(long bytes, long totalBytes)
        {
            this.BytesSent = bytes;
            this.TotalBytesToSend = totalBytes;
        }
    }
}
