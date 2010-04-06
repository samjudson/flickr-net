using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
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
        private Dictionary<string, ICacheItem> dataTable = new Dictionary<string, ICacheItem>();

		private readonly CacheItemPersister persister;

		// true if dataTable contains changes vs. on-disk representation
		private bool dirty;

		// The persistent file representation of the cache.
		private readonly FileInfo dataFile;
		private DateTime timestamp;  // last-modified time of dataFile when cache data was last known to be in sync
		private long length;         // length of dataFile when cache data was last known to be in sync
		
		// The file-based mutex.  Named (dataFile.FullName + ".lock")
		private readonly LockFile lockFile;

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

		public void Flush()
		{
			Shrink(0);
		}

		public void Shrink(long size)
		{
			if (size < 0)
				throw new ArgumentException("Cannot shrink to a negative size", "size");

            List<ICacheItem> flushed = new List<ICacheItem>();

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

			keys = new List<string>(dataTable.Keys).ToArray();
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

        private Dictionary<string, ICacheItem> Load(Stream s)
		{
            Dictionary<string, ICacheItem> table = new Dictionary<string, ICacheItem>();
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

        private void Store(Stream s, Dictionary<string, ICacheItem> table)
		{
			UtilityMethods.WriteInt32(s, table.Count);
			foreach (KeyValuePair<string, ICacheItem> entry in table)
			{
				UtilityMethods.WriteString(s, entry.Key);
				persister.Write(s, entry.Value);
			}
		}

		private class CreationTimeComparer : IComparer
		{
			public int Compare(object x, object y)
			{
				return ((ICacheItem)x).CreationTime.CompareTo(((ICacheItem)y).CreationTime);
			}
		}

        #region IDisposable Members

        public void Dispose()
        {
            if (lockFile != null) lockFile.Dispose();
        }

        #endregion
    }
}
