namespace FlickrNet.Exceptions
{
    /// <summary>
    /// A user was included in a description or comment which Flickr rejected.
    /// </summary>
    public sealed class BadUrlFoundException : FlickrApiException
    {
        internal BadUrlFoundException(string message)
            : base(111, message)
        {
        }
    }
}