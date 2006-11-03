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
	internal sealed class Cache
	{
		private class CacheException : Exception
		{
			public CacheException(string message) : base(message)
			{}

		}

		private static PersistentCache _downloads;


		/// <summary>
		/// A static object containing the list of cached downloaded files.
		/// </summary>
		public static PersistentCache Downloads
		{
			get 
			{
				lock(lockObject)
				{
					if( _downloads == null )
						_downloads = new PersistentCache(Path.Combine(CacheLocation, "downloadCache.dat"), new PictureCacheItemPersister(), CacheSizeLimit);
					return _downloads;
				}
			}
		}

		private static PersistentCache _responses;

		/// <summary>
		/// A static object containing the list of cached responses from Flickr.
		/// </summary>
		public static PersistentCache Responses
		{
			get 
			{
				lock(lockObject)
				{
					if( _responses == null )
						_responses = new PersistentCache(Path.Combine(CacheLocation, "responseCache.dat"), new ResponseCacheItemPersister(), CacheSizeLimit);
					return _responses;
				}
			}
		}


		private Cache()
		{
		}

		private static object lockObject = new object();

		private enum Tristate
		{
			Null, True, False
		}
		private static Tristate _cacheDisabled;

		internal static bool CacheDisabled
		{
			get
			{
				if( _cacheDisabled == Tristate.Null && FlickrConfigurationManager.Settings != null )
					_cacheDisabled = (FlickrConfigurationManager.Settings.CacheDisabled?Tristate.True:Tristate.False);
				
				if( _cacheDisabled == Tristate.Null ) _cacheDisabled = Tristate.False;

				return (_cacheDisabled==Tristate.True);
			}
			set
			{
				_cacheDisabled = value?Tristate.True:Tristate.False;
			}
		}

		private static string _cacheLocation;

		internal static string CacheLocation
		{
			get 
			{ 
				if( _cacheLocation == null && FlickrConfigurationManager.Settings != null )
					_cacheLocation = FlickrConfigurationManager.Settings.CacheLocation;
				if( _cacheLocation == null )
				{
					try
					{
						_cacheLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FlickrNet");
					}
					catch(System.Security.SecurityException)
					{
						// Permission to read application directory not provided.
						throw new CacheException("Unable to read default cache location. Please cacheLocation in configuration file or set manually in code");
					}
				}

				if( _cacheLocation == null )
					throw new CacheException("Unable to determine cache location. Please set cacheLocation in configuration file or set manually in code");

				return _cacheLocation;
			}
			set
			{
				_cacheLocation = value;
			}
		}

		internal static long CacheSizeLimit
		{
			get 
			{
				if( CacheSettings.ContainsKey("SizeLimit") )
					return (long)CacheSettings["SizeLimit"];
				else
					return 50 * 1024 * 1024;
			}
			set 
			{ 
				if( CacheSettings.ContainsKey("SizeLimit") )
					CacheSettings["SizeLimit"] = value;
				else
					CacheSettings.Add("SizeLimit", value);
			}
		}

		internal static long CacheSize
		{
			get 
			{
				if( CacheSettings.ContainsKey("CurrentSize") )
					return (long)CacheSettings["CurrentSize"];
				else
					return 0;
			}
			set 
			{ 
				if( CacheSettings.ContainsKey("CurrentSize") )
					CacheSettings["CurrentSize"] = value;
				else
					CacheSettings.Add("CurrentSize", value);
			}
		}


		// Default cache timeout is 1 hour
		private static TimeSpan _cachetimeout = new TimeSpan(0, 1, 0, 0, 0);

		/// <summary>
		/// The default timeout for cachable objects within the cache.
		/// </summary>
		public static TimeSpan CacheTimeout
		{
			get { return _cachetimeout; }
			set { _cachetimeout = value; }
		}
		
		private static Hashtable _cacheSettings;

		private static Hashtable CacheSettings
		{
			get
			{
				lock(lockObject)
				{
					if( _cacheSettings == null )
						LoadSettings();
					return _cacheSettings;
				}
			}
		}

		private static void SaveSettings()
		{
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			System.IO.Stream stream;

			lock(CacheSettings.SyncRoot)
			{
				if( CacheLocation == null ) return;

				stream = new FileStream(Path.Combine(CacheLocation, "cacheSettings.bin"), FileMode.OpenOrCreate, FileAccess.Write);
				try
				{
					formatter.Serialize(stream, CacheSettings);
				}
				finally
				{
					stream.Close();
				}
			}
		}

		private static void LoadSettings()
		{
			if( !Directory.Exists(CacheLocation) ) Directory.CreateDirectory(CacheLocation);

			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			System.IO.Stream stream = null;

			if( CacheLocation != null ) 
			{
				stream = new FileStream(Path.Combine(CacheLocation, "cacheSettings.bin"), FileMode.OpenOrCreate);
			}

			if( stream == null )
			{
				_cacheSettings = new Hashtable();
				return;
			}
			try
			{
				_cacheSettings = (Hashtable)formatter.Deserialize(stream);
			}
			catch(InvalidCastException)
			{
				_cacheSettings = new Hashtable();
			}
			catch(System.Runtime.Serialization.SerializationException)
			{
				_cacheSettings = new Hashtable();
			}
			finally
			{
				stream.Close();
			}
		}

		internal static void FlushCache(string url)
		{
			Responses[url] = null;
			Downloads[url] = null;
		}

		internal static void FlushCache()
		{
			Responses.Flush();
			Downloads.Flush();
		}

	}

	/// <summary>
	/// A cache item containing details of a REST response from Flickr.
	/// </summary>
	[Serializable]
	public class ResponseCacheItem : ICacheItem
	{
		private string url;
		private string response;
		private DateTime creationTime;

		/// <summary>
		/// Gets or sets the original URL of the request.
		/// </summary>
		public string Url { get { return url; } set { url = value; } }

		/// <summary>
		/// Gets or sets the XML response.
		/// </summary>
		public string Response { get { return response; } set { response = value; } }

		/// <summary>
		/// Gets or sets the time the cache item was created.
		/// </summary>
		public DateTime CreationTime { get { return creationTime; } set { creationTime = value; } }

		/// <summary>
		/// Gets the filesize of the request.
		/// </summary>
		public long FileSize { get { return (response==null?0:response.Length); } }

		void ICacheItem.OnItemFlushed()
		{
		}

	}

	internal class ResponseCacheItemPersister : CacheItemPersister
	{
		public override ICacheItem Read(Stream inputStream)
		{
			string s = Utils.ReadString(inputStream);
			string response = Utils.ReadString(inputStream);

			string[] chunks = s.Split('\n');
			string url = chunks[0];
			DateTime creationTime = new DateTime(long.Parse(chunks[1]));
			ResponseCacheItem item = new ResponseCacheItem();
			item.Url = url;
			item.CreationTime = creationTime;
			item.Response = response;
			return item;
		}

		public override void Write(Stream outputStream, ICacheItem cacheItem)
		{
			ResponseCacheItem item = (ResponseCacheItem) cacheItem;
			StringBuilder result = new StringBuilder();
			result.Append(item.Url + "\n");
			result.Append(item.CreationTime.Ticks + "\n");
			Utils.WriteString(outputStream, result.ToString());
			Utils.WriteString(outputStream, item.Response);
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
	/// Contains details of image held with the Flickr.Net cache.
	/// </summary>
	[Serializable]
	public class PictureCacheItem : ICacheItem
	{
		#region [ Internal Variables ]
		internal string url;
		internal DateTime creationTime;
		internal string filename;
		internal long fileSize;
		#endregion

		#region [ Public Properties ]
		/// <summary>
		/// The URL of the original image on Flickr.
		/// </summary>
		public string Url { get { return url; } }
		/// <summary>
		/// The <see cref="DateTime"/> that the cache item was created.
		/// </summary>
		public DateTime CreationTime { get { return creationTime; } }

		/// <summary>
		/// The filesize in bytes of the image.
		/// </summary>
		public long FileSize { get { return fileSize; } }
		/// <summary>
		/// The Flickr photo id of the image.
		/// </summary>
		public string PhotoId
		{
			get 
			{
				if( url == null ) 
					return null;
				else
				{
					int begin = url.LastIndexOf("/");
					int end = url.IndexOf("_");

					return url.Substring(begin + 1, (end - begin) - 1);
				}
			}
		}
		#endregion

		#region [ Public Methods ]

		void ICacheItem.OnItemFlushed()
		{
			File.Delete(filename);
		}

		#endregion
	}

	/// <summary>
	/// Persists PictureCacheItem objects.
	/// </summary>
	internal class PictureCacheItemPersister : CacheItemPersister
	{
		public override ICacheItem Read(Stream inputStream)
		{
			string s = Utils.ReadString(inputStream);

			string[] chunks = s.Split('\n');
			string url = chunks[0];
			DateTime creationTime = new DateTime(long.Parse(chunks[1]));
			string filename = chunks[2];
			long fileSize = long.Parse(chunks[3]);

			PictureCacheItem pci = new PictureCacheItem();
			pci.url = url;
			pci.creationTime = creationTime;
			pci.filename = filename;
			pci.fileSize = fileSize;
			return pci;
		}

		public override void Write(Stream outputStream, ICacheItem cacheItem)
		{
			PictureCacheItem pci = (PictureCacheItem) cacheItem;
			StringBuilder output = new StringBuilder();

			output.Append(pci.url + "\n");
			output.Append(pci.creationTime.Ticks + "\n");
			output.Append(pci.filename + "\n");
			output.Append(pci.fileSize + "\n");

			Utils.WriteString(outputStream, output.ToString());
		}
	}
}
