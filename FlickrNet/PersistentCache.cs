using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;

namespace FlickrNet
{
	/// <summary>
	/// A threadsafe cache that is backed by disk storage.
	/// 
	/// All public methods that read or write state must be 
	/// protected by the lockFile.  Private methods should
	/// not acquire the lockFile as it is not reentrant.
	/// </summary>
	public sealed class PersistentCache : IDisposable
	{
		// The in-memory representation of the cache.
		// Use SortedList instead of Hashtable only to maintain backward 
		// compatibility with previous serialization scheme.  If we
		// abandon backward compatibility, we should switch to Hashtable.
		private Hashtable dataTable = new Hashtable();

		private readonly CacheItemPersister persister;

		// true if dataTable contains changes vs. on-disk representation
		private bool dirty;

		// The persistent file representation of the cache.
		private readonly FileInfo dataFile;
		private DateTime timestamp;  // last-modified time of dataFile when cache data was last known to be in sync
		private long length;         // length of dataFile when cache data was last known to be in sync
		
		// The file-based mutex.  Named (dataFile.FullName + ".lock")
		private readonly LockFile lockFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistentCache"/> for the given filename and cache type.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="persister"></param>
		public PersistentCache(string filename, CacheItemPersister persister)
		{
			this.persister = persister;
			this.dataFile = new FileInfo(filename);
			this.lockFile = new LockFile(filename + ".lock");
		}

		/// <summary>
		/// Return all items in the cache.  Works similarly to
		/// ArrayList.ToArray(Type).
		/// </summary>
		public ICacheItem[] ToArray(Type valueType)
		{
			using (lockFile.Acquire())
			{
				Refresh();
				string[] keys;
				Array values;
				InternalGetAll(valueType, out keys, out values);
				return (ICacheItem[]) values;
			}
		}

		/// <summary>
		/// Gets or sets cache values.
		/// </summary>
		public ICacheItem this[string key]
		{
			set
			{
				if (key == null)
					throw new ArgumentNullException("key");

				ICacheItem oldItem;
				
				using (lockFile.Acquire())
				{
					Refresh();
					oldItem = InternalSet(key, value);
					Persist();
				}
				if (oldItem != null)
					oldItem.OnItemFlushed();
			}
		}

        /// <summary>
        /// Gets the specified key from the cache unless it has expired.
        /// </summary>
        /// <param name="key">The key to look up in the cache.</param>
        /// <param name="maxAge">The maximum age the item can be before it is no longer returned.</param>
        /// <param name="removeIfExpired">Whether to delete the item if it has expired or not.</param>
        /// <returns></returns>
		public ICacheItem Get(string key, TimeSpan maxAge, bool removeIfExpired)
		{
			Debug.Assert(maxAge > TimeSpan.Zero || maxAge == TimeSpan.MinValue, "maxAge should be positive, not negative");

			ICacheItem item;
			bool expired;
			using (lockFile.Acquire())
			{
				Refresh();

				item = InternalGet(key);
				expired = item != null && Expired(item.CreationTime, maxAge);
				if (expired)
				{
					if (removeIfExpired)
					{
						item = RemoveKey(key);
						Persist();
					}
					else
						item = null;
				}
			}

			if (expired && removeIfExpired)
				item.OnItemFlushed();

			return expired ? null : item;
		}

        /// <summary>
        /// Clears the current cache of items.
        /// </summary>
		public void Flush()
		{
			Shrink(0);
		}

        /// <summary>
        /// Shrinks the current cache to a specific size, removing older items first.
        /// </summary>
        /// <param name="size"></param>
		public void Shrink(long size)
		{
			if (size < 0)
				throw new ArgumentException("Cannot shrink to a negative size", "size");

			ArrayList flushed = new ArrayList();

			using (lockFile.Acquire())
			{
				Refresh();

				string[] keys;
				Array values;
				InternalGetAll(typeof(ICacheItem), out keys, out values);
				long totalSize = 0;
				foreach (ICacheItem cacheItem in values)
					totalSize += cacheItem.FileSize;
				for (int i = 0; i < keys.Length; i++)
				{
					if (totalSize <= size)
						break;
					ICacheItem cacheItem = (ICacheItem) values.GetValue(i);
					totalSize -= cacheItem.FileSize;
					flushed.Add(RemoveKey(keys[i]));
				}

				Persist();
			}

			foreach (ICacheItem flushedItem in flushed)
			{
				Debug.Assert(flushedItem != null, "Flushed item was null--programmer error");
				if (flushedItem != null)
					flushedItem.OnItemFlushed();
			}
		}

		private static bool Expired(DateTime test, TimeSpan age)
		{
			if (age == TimeSpan.MinValue)
				return true;
			else if (age == TimeSpan.MaxValue)
				return false;
			else
				return test < DateTime.UtcNow - age;
		}

		
		private void InternalGetAll(Type valueType, out string[] keys, out Array values)
		{
			if (!typeof(ICacheItem).IsAssignableFrom(valueType))
				throw new ArgumentException("Type " + valueType.FullName + " does not implement ICacheItem", "valueType");

			keys = (string[]) new ArrayList(dataTable.Keys).ToArray(typeof(string));
			values = Array.CreateInstance(valueType, keys.Length);
			for (int i = 0; i < keys.Length; i++)
				values.SetValue(dataTable[keys[i]], i);

			Array.Sort(values, keys, new CreationTimeComparer());
		}

		private ICacheItem InternalGet(string key)
		{
			if (key == null)
				throw new ArgumentNullException("key");
	
			return (ICacheItem) dataTable[key];
		}

		/// <returns>The old value associated with <c>key</c>, if any.</returns>
		private ICacheItem InternalSet(string key, ICacheItem value)
		{
			if (key == null)
				throw new ArgumentNullException("key");
	
			ICacheItem flushedItem;

			flushedItem = RemoveKey(key);
			if (value != null)  // don't ever let nulls get in
				dataTable[key] = value;

			dirty = dirty || !object.ReferenceEquals(flushedItem, value);

			return flushedItem;
		}

		private ICacheItem RemoveKey(string key)
		{
			ICacheItem cacheItem = (ICacheItem) dataTable[key];
			if (cacheItem != null)
			{
				dataTable.Remove(key);
				dirty = true;
			}
			return cacheItem;
		}

		private void Refresh()
		{
			Debug.Assert(!dirty, "Refreshing even though cache is dirty");

			DateTime newTimestamp = DateTime.MinValue;
			long newLength = -1;
			if (dataFile.Exists)
			{
				dataFile.Refresh();
				newTimestamp = dataFile.LastWriteTime;
				newLength = dataFile.Length;
			}

			if (timestamp != newTimestamp || length != newLength)
			{
				// file changed
				if (!dataFile.Exists)
					dataTable.Clear();
				else
				{
					Debug.WriteLine("Loading cache from disk");
					using (FileStream inStream = dataFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						dataTable = Load(inStream);
					}
				}
			}
		
			timestamp = newTimestamp;
			length = newLength;
			dirty = false;
		}

		private void Persist()
		{
			if (!dirty)
				return;

			Debug.WriteLine("Saving cache to disk");
			using (FileStream outStream = dataFile.Open(FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
			{
				Store(outStream, dataTable);
			}

			dataFile.Refresh();
			timestamp = dataFile.LastWriteTime;
			length = dataFile.Length;

			dirty = false;
		}

		private Hashtable Load(Stream s)
		{
			Hashtable table = new Hashtable();
			int itemCount = UtilityMethods.ReadInt32(s);
			for (int i = 0; i < itemCount; i++)
			{
				try
				{
					string key = UtilityMethods.ReadString(s);
					ICacheItem val = persister.Read(s);
					if( val == null ) // corrupt cache file 
						return table;

					table[key] = val;
				}
				catch(IOException)
				{
					return table;
				}
			}
			return table;
		}

		private void Store(Stream s, Hashtable table)
		{
			UtilityMethods.WriteInt32(s, table.Count);
			foreach (DictionaryEntry entry in table)
			{
				UtilityMethods.WriteString(s, (string) entry.Key);
				persister.Write(s, (ICacheItem) entry.Value);
			}
		}

		private class CreationTimeComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				return ((ICacheItem)x).CreationTime.CompareTo(((ICacheItem)y).CreationTime);
			}
		}

        void IDisposable.Dispose()
        {
            if (lockFile != null) lockFile.Dispose();
        }
    }
}
