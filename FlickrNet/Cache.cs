using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FlickrNet
{
    /// <summary>
    /// Internal Cache class
    /// </summary>
    public static class Cache
    {
        private static PersistentCache responses;

        /// <summary>
        /// A static object containing the list of cached responses from Flickr.
        /// </summary>
        public static PersistentCache Responses
        {
            get
            {
                lock (lockObject)
                {
                    if (responses == null)
                        responses = new PersistentCache(Path.Combine(CacheLocation, "responseCache.dat"), new ResponseCacheItemPersister());
                    return responses;
                }
            }
        }


        private static object lockObject = new object();

        private enum Tristate
        {
            Null, True, False
        }
        private static Tristate cacheDisabled;

        /// <summary>
        /// Returns weither of not the cache is currently disabled.
        /// </summary>
        public static bool CacheDisabled
        {
            get
            {
#if !(WindowsCE || MONOTOUCH || SILVERLIGHT)
                if (cacheDisabled == Tristate.Null && FlickrConfigurationManager.Settings != null)
                    cacheDisabled = FlickrConfigurationManager.Settings.CacheDisabled ? Tristate.True : Tristate.False;
#endif
                if (cacheDisabled == Tristate.Null) cacheDisabled = Tristate.False;

                return cacheDisabled == Tristate.True;
            }
            set
            {
                cacheDisabled = value ? Tristate.True : Tristate.False;
            }
        }

        private static string cacheLocation;

        /// <summary>
        /// Returns the currently set location for the cache.
        /// </summary>
        public static string CacheLocation
        {
            get
            {
#if !(WindowsCE || MONOTOUCH || SILVERLIGHT)
                if (cacheLocation == null && FlickrConfigurationManager.Settings != null)
                    cacheLocation = FlickrConfigurationManager.Settings.CacheLocation;
#endif
                if (cacheLocation == null)
                {
                    try
                    {
#if !(WindowsCE || MONOTOUCH || SILVERLIGHT)
                        cacheLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FlickrNet");
#endif
#if MONOTOUCH
                        cacheLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "../Library/Caches");
#endif
#if WindowsCE
                        cacheLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FlickrNetCache");
#endif
#if SILVERLIGHT
                        cacheLocation = String.Empty;
#endif

                    }
                    catch (System.Security.SecurityException)
                    {
                        // Permission to read application directory not provided.
                        throw new CacheException("Unable to read default cache location. Please cacheLocation in configuration file or set manually in code");
                    }
                }

                if (cacheLocation == null)
                    throw new CacheException("Unable to determine cache location. Please set cacheLocation in configuration file or set manually in code");

                return cacheLocation;
            }
            set
            {
                cacheLocation = value;
            }
        }

        // Default cache size is set to 50MB
        private static long cacheSizeLimit = 52428800;

        internal static long CacheSizeLimit
        {
            get
            {
                return cacheSizeLimit;
            }
            set
            {
                cacheSizeLimit = value;
            }
        }

        // Default cache timeout is 1 hour
        private static TimeSpan cachetimeout = new TimeSpan(0, 1, 0, 0, 0);

        /// <summary>
        /// The default timeout for cachable objects within the cache.
        /// </summary>
        public static TimeSpan CacheTimeout
        {
            get { return cachetimeout; }
            set { cachetimeout = value; }
        }

        /// <summary>
        /// Remove a specific URL from the cache.
        /// </summary>
        /// <param name="url"></param>
        public static void FlushCache(Uri url)
        {
            Responses[url.AbsoluteUri] = null;
        }

        /// <summary>
        /// Clears all responses from the cache.
        /// </summary>
        public static void FlushCache()
        {
            Responses.Flush();
        }

    }

    /// <summary>
    /// A cache item containing details of a REST response from Flickr.
    /// </summary>
    public sealed class ResponseCacheItem : ICacheItem
    {
        /// <summary>
        /// Gets or sets the original URL of the request.
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// Gets or sets the XML response.
        /// </summary>
        public string Response { get; private set; }

        /// <summary>
        /// Gets or sets the time the cache item was created.
        /// </summary>
        public DateTime CreationTime { get; private set; }

        /// <summary>
        /// Gets the filesize of the request.
        /// </summary>
        public long FileSize
        {
            get { return Response == null ? 0 : Response.Length; }
        }

        /// <summary>
        /// Creates an instance of the <see cref="ResponseCacheItem"/> class.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="response"></param>
        /// <param name="creationTime"></param>
        public ResponseCacheItem(Uri url, string response, DateTime creationTime)
        {
            Url = url;
            Response = response;
            CreationTime = creationTime;
        }

        void ICacheItem.OnItemFlushed()
        {
        }

    }

    internal class ResponseCacheItemPersister : CacheItemPersister
    {
        public override ICacheItem Read(Stream inputStream)
        {
            string s = UtilityMethods.ReadString(inputStream);
            string response = UtilityMethods.ReadString(inputStream);

            string[] chunks = s.Split('\n');

            // Corrupted cache record, so throw IOException which is then handled and returns partial cache.
            if (chunks.Length != 2)
                throw new IOException("Unexpected number of chunks found");

            string url = chunks[0];
            DateTime creationTime = new DateTime(long.Parse(chunks[1], System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo));
            ResponseCacheItem item = new ResponseCacheItem(new Uri(url), response, creationTime);
            return item;
        }

        public override void Write(Stream outputStream, ICacheItem cacheItem)
        {
            ResponseCacheItem item = (ResponseCacheItem)cacheItem;
            StringBuilder result = new StringBuilder();
            result.Append(item.Url.AbsoluteUri + "\n");
            result.Append(item.CreationTime.Ticks.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            UtilityMethods.WriteString(outputStream, result.ToString());
            UtilityMethods.WriteString(outputStream, item.Response);
        }
    }

    /// <summary>
    /// An item that can be stored in a cache.
    /// </summary>
    public interface ICacheItem
    {
        /// <summary>
        /// The time this cache item was created.
        /// </summary>
        DateTime CreationTime { get; }

        /// <summary>
        /// Gets called back when the item gets flushed
        /// from the cache.
        /// </summary>
        void OnItemFlushed();

        /// <summary>
        /// The size of this item, in bytes. Return 0
        /// if size management is not important.
        /// </summary>
        long FileSize { get; }
    }

    /// <summary>
    /// An interface that knows how to read/write subclasses
    /// of ICacheItem.  Obviously there will be a tight
    /// coupling between concrete implementations of ICacheItem
    /// and concrete implementations of ICacheItemPersister.
    /// </summary>
    public abstract class CacheItemPersister
    {
        /// <summary>
        /// Read a single cache item from the input stream.
        /// </summary>
        public abstract ICacheItem Read(Stream inputStream);

        /// <summary>
        /// Write a single cache item to the output stream.
        /// </summary>
        public abstract void Write(Stream outputStream, ICacheItem cacheItem);
    }


    /// <summary>
    /// An internal class used for catching caching exceptions.
    /// </summary>
    [Serializable]
    public class CacheException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CacheException"/> class.
        /// </summary>
        public CacheException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheException"/> class with a specified error message.
        /// </summary>
        /// <param name="message"></param>
        public CacheException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CacheException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
