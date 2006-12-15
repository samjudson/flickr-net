using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace FlickrNet
{
	/// <summary>
	/// A non-reentrant mutex that is implemented using
	/// a lock file, and thus works across processes,
	/// sessions, and machines (as long as the underlying
	/// FS provides robust r/w locking).
	/// 
	/// To use:
	/// 
	/// FileLock fLock = new FileLock(@"c:\foo\my.lock");
	/// 
	/// using (fLock.Acquire())
	/// {
	///		// protected operations
	/// }
	/// </summary>
	internal class LockFile
	{
		private readonly string filepath;
		private readonly DisposeHelper disposeHelper;
		private Stream stream;

		public LockFile(string filepath)
		{
			this.filepath = filepath;
			this.disposeHelper = new DisposeHelper(this);
		}

		public IDisposable Acquire()
		{
			string dir = Path.GetDirectoryName(filepath);

			lock (this)
			{
#if !WindowsCE
				while (stream != null)
					Monitor.Wait(this);
#endif

				while (true)
				{
					if (!Directory.Exists(dir))
						Directory.CreateDirectory(dir);
					try
					{
						Debug.Assert(stream == null, "Stream was not null--programmer error");
						stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None, 8, false);
						return disposeHelper;
					}
					catch (IOException)
					{
						Thread.Sleep(50);
						continue;
//						int errorCode = Marshal.GetHRForException(ioe) & 0xFFFF;
//						switch (errorCode)
//						{
//							case 32:
//							case 33:
//							case 32 | 0x1620:
//							case 33 | 0x1620:
//								Thread.Sleep(50);
//								continue;
//							default:
//								throw;
//						}
					}
				}
			}
		}

		internal void Release()
		{
			lock (this)
			{
#if !WindowsCE
				// Doesn't hurt to pulse. Note that waiting threads will not actually
				// continue to execute until this critical section is exited.
				Monitor.PulseAll(this);
#endif

				if (stream == null)
					throw new InvalidOperationException("Tried to dispose a FileLock that was not owned");
				try
				{
					stream.Close();
					try
					{
						File.Delete(filepath);
					} catch(IOException) { /* could fail if already acquired elsewhere */ }
				}
				finally
				{
					stream = null;
				}
			}
		}

		private class DisposeHelper : IDisposable
		{
			private readonly LockFile lockFile;

			public DisposeHelper(LockFile lockFile)
			{
				this.lockFile = lockFile;
			}

			public void Dispose()
			{
				lockFile.Release();
			}
		}

		
	}
}
