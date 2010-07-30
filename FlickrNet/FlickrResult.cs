using System;
using System.Net;

namespace FlickrNet
{
    internal class FlickrResultArgs<T> : EventArgs where T : IFlickrParsable
    {
        internal FlickrResultArgs(FlickrResult<T> result)
        {
            Error = result.Error;
            HasError = result.HasError;
            Result = result.Result;
        }

        private Exception _error;
        public bool HasError { get; set; }
        public T Result { get; set; }
        public Exception Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                if (value is FlickrApiException)
                {
                    FlickrApiException ex = value as FlickrApiException;
                    ErrorCode = ex.Code;
                    ErrorMessage = ex.OriginalMessage;
                    HasError = true;
                }
            }
        }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Contains details of the result from Flickr, or the error if an error occurred.
    /// </summary>
    /// <typeparam name="T">The type of the result returned from Flickr.</typeparam>
    public class FlickrResult<T>
    {
        private Exception _error;
        /// <summary>
        /// True if the result returned an error.
        /// </summary>
        public bool HasError { get; set; }

        /// <summary>
        /// If the call was successful then this contains the result.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// If the call was unsuccessful then this contains the exception.
        /// </summary>
        public Exception Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                if (value == null) { HasError = false; return; }
                HasError = true;
                if (value is FlickrApiException)
                {
                    FlickrApiException ex = value as FlickrApiException;
                    ErrorCode = ex.Code;
                    ErrorMessage = ex.OriginalMessage;
                }
            }
        }

        /// <summary>
        /// If an error was returned by the Flickr API then this will contain the error code.
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// If an error was returned by the Flickr API then this will contain the error message.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
