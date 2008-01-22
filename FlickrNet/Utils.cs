using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace FlickrNet
{
	/// <summary>
	/// Internal class providing certain utility functions to other classes.
	/// </summary>
	internal sealed class Utils
	{
		private static readonly DateTime unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0);

		private Utils()
		{
		}

#if !WindowsCE
		internal static string UrlEncode(string oldString)
		{
			if( oldString == null ) return null;

			string a = System.Web.HttpUtility.UrlEncode(oldString);
			a = a.Replace("&", "%26");
			a = a.Replace("=", "%3D");
			a = a.Replace(" ", "%20");
			return a;
		}
#else
        internal static string UrlEncode(string oldString)
        {
            if (oldString == null) return String.Empty;
            StringBuilder sb = new StringBuilder(oldString.Length * 2);
            Regex reg = new Regex("[a-zA-Z0-9$-_.+!*'(),]");

            foreach (char c in oldString)
            {
                if (reg.IsMatch(c.ToString()))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(ToHex(c));
                }
            }
            return sb.ToString();
        }

        private static string ToHex(char c)
        {
            return ((int)c).ToString("X");
        }
#endif

		/// <summary>
		/// Converts a <see cref="DateTime"/> object into a unix timestamp number.
		/// </summary>
		/// <param name="date">The date to convert.</param>
		/// <returns>A long for the number of seconds since 1st January 1970, as per unix specification.</returns>
		internal static long DateToUnixTimestamp(DateTime date)
		{
			TimeSpan ts = date - unixStartDate;
			return (long)ts.TotalSeconds;
		}

		/// <summary>
		/// Converts a string, representing a unix timestamp number into a <see cref="DateTime"/> object.
		/// </summary>
		/// <param name="timestamp">The timestamp, as a string.</param>
		/// <returns>The <see cref="DateTime"/> object the time represents.</returns>
		internal static DateTime UnixTimestampToDate(string timestamp)
		{
			if( timestamp == null || timestamp.Length == 0 ) return DateTime.MinValue;

			return UnixTimestampToDate(long.Parse(timestamp));
		}

		/// <summary>
		/// Converts a <see cref="long"/>, representing a unix timestamp number into a <see cref="DateTime"/> object.
		/// </summary>
		/// <param name="timestamp">The unix timestamp.</param>
		/// <returns>The <see cref="DateTime"/> object the time represents.</returns>
		internal static DateTime UnixTimestampToDate(long timestamp)
		{
			return unixStartDate.AddSeconds(timestamp);
		}

		/// <summary>
		/// Utility method to convert the <see cref="PhotoSearchExtras"/> enum to a string.
		/// </summary>
		/// <example>
		/// <code>
		///     PhotoSearchExtras extras = PhotoSearchExtras.DateTaken &amp; PhotoSearchExtras.IconServer;
		///     string val = Utils.ExtrasToString(extras);
		///     Console.WriteLine(val);
		/// </code>
		/// outputs: "date_taken,icon_server";
		/// </example>
		/// <param name="extras"></param>
		/// <returns></returns>
		internal static string ExtrasToString(PhotoSearchExtras extras)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			if( (extras & PhotoSearchExtras.DateTaken) == PhotoSearchExtras.DateTaken )
				sb.Append("date_taken");
			if( (extras & PhotoSearchExtras.DateUploaded) == PhotoSearchExtras.DateUploaded )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("date_upload");
			}
			if( (extras & PhotoSearchExtras.IconServer) == PhotoSearchExtras.IconServer )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("icon_server");
			}
			if( (extras & PhotoSearchExtras.License) == PhotoSearchExtras.License )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("license");
			}
			if( (extras & PhotoSearchExtras.OwnerName) == PhotoSearchExtras.OwnerName )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("owner_name");
			}
			if( (extras & PhotoSearchExtras.OriginalFormat) == PhotoSearchExtras.OriginalFormat )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("original_format");
			}

			if( (extras & PhotoSearchExtras.LastUpdated) == PhotoSearchExtras.LastUpdated )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("last_update");
			}

			if( (extras & PhotoSearchExtras.Tags) == PhotoSearchExtras.Tags )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("tags");
			}

			if( (extras & PhotoSearchExtras.Geo) == PhotoSearchExtras.Geo )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("geo");
			}

			if( (extras & PhotoSearchExtras.MachineTags) == PhotoSearchExtras.MachineTags )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("machine_tags");
			}

			if( (extras & PhotoSearchExtras.OriginalDimensions) == PhotoSearchExtras.OriginalDimensions )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("o_dims");
			}

			return sb.ToString();
		}

		internal static string SortOrderToString(PhotoSearchSortOrder order)
		{
			switch(order)
			{
				case PhotoSearchSortOrder.DatePostedAsc:
					return "date-posted-asc";
				case PhotoSearchSortOrder.DatePostedDesc:
					return "date-posted-desc";
				case PhotoSearchSortOrder.DateTakenAsc:
					return "date-taken-asc";
				case PhotoSearchSortOrder.DateTakenDesc:
					return "date-taken-desc";
				case PhotoSearchSortOrder.InterestingnessAsc:
					return "interestingness-asc";
				case PhotoSearchSortOrder.InterestingnessDesc:
					return "interestingness-desc";
				case PhotoSearchSortOrder.Relevance:
					return "relevance";
				default:
					return null;
			}
		}

		internal static void PartialOptionsIntoArray(PartialSearchOptions options, Hashtable parameters)
		{
			if( options.MinUploadDate != DateTime.MinValue ) parameters.Add("min_uploaded_date", Utils.DateToUnixTimestamp(options.MinUploadDate).ToString());
			if( options.MaxUploadDate != DateTime.MinValue ) parameters.Add("max_uploaded_date", Utils.DateToUnixTimestamp(options.MaxUploadDate).ToString());
			if( options.MinTakenDate != DateTime.MinValue ) parameters.Add("min_taken_date", options.MinTakenDate.ToString("yyyy-MM-dd HH:mm:ss"));
			if( options.MaxTakenDate != DateTime.MinValue ) parameters.Add("max_taken_date", options.MaxTakenDate.ToString("yyyy-MM-dd HH:mm:ss"));
			if( options.Extras != PhotoSearchExtras.None ) parameters.Add("extras", options.ExtrasString);
			if( options.SortOrder != PhotoSearchSortOrder.None ) parameters.Add("sort", options.SortOrderString);
			if( options.PerPage > 0 ) parameters.Add("per_page", options.PerPage.ToString());
			if( options.Page > 0 ) parameters.Add("page", options.Page.ToString());
			if( options.PrivacyFilter != PrivacyFilter.None ) parameters.Add("privacy_filter", options.PrivacyFilter.ToString("d"));
		}

		internal static void WriteInt32(Stream s, int i)
		{
			s.WriteByte((byte) (i & 0xFF));
			s.WriteByte((byte) ((i >> 8) & 0xFF));
			s.WriteByte((byte) ((i >> 16) & 0xFF));
			s.WriteByte((byte) ((i >> 24) & 0xFF));
		}

		internal static void WriteString(Stream s, string str)
		{
			WriteInt32(s, str.Length);
			foreach (char c in str)
			{
				s.WriteByte((byte) (c & 0xFF));
				s.WriteByte((byte) ((c >> 8) & 0xFF));
			}
		}

		internal static void WriteAsciiString(Stream s, string str)
		{
			WriteInt32(s, str.Length);
			foreach (char c in str)
			{
				s.WriteByte((byte) (c & 0x7F));
			}
		}

		internal static int ReadInt32(Stream s)
		{
			int i = 0, b;
			for (int j = 0; j < 4; j++)
			{
				b = s.ReadByte();
				if (b == -1)
					throw new IOException("Unexpected EOF encountered");
				i |= (b << (j * 8));
			}
			return i;
		}

		internal static string ReadString(Stream s)
		{
			int len = ReadInt32(s);
			char[] chars = new char[len];
			for (int i = 0; i < len; i++)
			{
				int hi, lo;
				lo = s.ReadByte();
				hi = s.ReadByte();
				if (lo == -1 || hi == -1)
					throw new IOException("Unexpected EOF encountered");
				chars[i] = (char) (lo | (hi << 8));
			}
			return new string(chars);
		}

		internal static string ReadAsciiString(Stream s)
		{
			int len = ReadInt32(s);
			char[] chars = new char[len];
			for (int i = 0; i < len; i++)
			{
				int c = s.ReadByte();
				if (c == -1)
					throw new IOException("Unexpected EOF encountered");
				chars[i] = (char) (c & 0x7F);
			}
			return new string(chars);
		}
	
		private const string photoUrl = "http://farm{0}.static.flickr.com/{1}/{2}_{3}{4}.{5}";

		internal static string UrlFormat(Photo p, string size, string format)
		{
			if( size == "_o" )
				return UrlFormat(photoUrl, p.Farm, p.Server, p.PhotoId, p.OriginalSecret, size, format);
			else
				return UrlFormat(photoUrl, p.Farm, p.Server, p.PhotoId, p.Secret, size, format);
		}

		internal static string UrlFormat(PhotoInfo p, string size, string format)
		{
			if( size == "_o" )
				return UrlFormat(photoUrl, p.Farm, p.Server, p.PhotoId, p.OriginalSecret, size, format);
			else
				return UrlFormat(photoUrl, p.Farm, p.Server, p.PhotoId, p.Secret, size, format);
		}

		internal static string UrlFormat(Photoset p, string size, string format)
		{
			return UrlFormat(photoUrl, p.Farm, p.Server, p.PrimaryPhotoId, p.Secret, size, format);
		}

		private static string UrlFormat(string format, params object[] parameters)
		{
			return String.Format(format, parameters);
		}

		private static readonly Hashtable _serializers = new Hashtable();

		private static XmlSerializer GetSerializer(Type type)
		{
			if( _serializers.ContainsKey(type.Name) )
				return (XmlSerializer)_serializers[type.Name];
			else
			{
				XmlSerializer s = new XmlSerializer(type);
				_serializers.Add(type.Name, s);
				return s;
			}
		}
		/// <summary>
		/// Converts the response string (in XML) into the <see cref="Response"/> object.
		/// </summary>
		/// <param name="responseString">The response from Flickr.</param>
		/// <returns>A <see cref="Response"/> object containing the details of the </returns>
		internal static Response Deserialize(string responseString)
		{
			XmlSerializer serializer = GetSerializer(typeof(FlickrNet.Response));
			try
			{
				// Deserialise the web response into the Flickr response object
				StringReader responseReader = new StringReader(responseString);
				FlickrNet.Response response = (FlickrNet.Response)serializer.Deserialize(responseReader);
				responseReader.Close();

				return response;
			}
			catch(InvalidOperationException ex)
			{
				// Serialization error occurred!
				throw new ResponseXmlException("Invalid response received from Flickr.", ex);
			}
		}

		internal static object Deserialize(System.Xml.XmlNode node, Type type)
		{
			XmlSerializer serializer = GetSerializer(type);
			try
			{
				// Deserialise the web response into the Flickr response object
				System.Xml.XmlNodeReader reader = new System.Xml.XmlNodeReader(node);
				object o = serializer.Deserialize(reader);
				reader.Close();

				return o;
			}
			catch(InvalidOperationException ex)
			{
				// Serialization error occurred!
				throw new ResponseXmlException("Invalid response received from Flickr.", ex);
			}
		}



	}

}
