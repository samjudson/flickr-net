using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Internal class providing certain utility functions to other classes.
	/// </summary>
	public sealed class UtilityMethods
	{
		private static readonly DateTime unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0);

		private UtilityMethods()
		{
		}

        /// <summary>
        /// Converts <see cref="AuthLevel"/> to a string.
        /// </summary>
        /// <param name="level">The level to convert.</param>
        /// <returns></returns>
        public static string AuthLevelToString(AuthLevel level)
        {
            switch (level)
            {
                case AuthLevel.Delete:
                    return "delete";
                case AuthLevel.Read:
                    return "read";
                case AuthLevel.Write:
                    return "write";
                case AuthLevel.None:
                    return "none";
                default:
                    return "";

            }
        }

        /// <summary>
        /// Encodes a URL quesrystring data component.
        /// </summary>
        /// <param name="data">The data to encode.</param>
        /// <returns>The URL encoded string.</returns>
        public static string UrlEncode(string data)
        {
            return Uri.EscapeDataString(data);
        }

		/// <summary>
		/// Converts a <see cref="DateTime"/> object into a unix timestamp number.
		/// </summary>
		/// <param name="date">The date to convert.</param>
		/// <returns>A long for the number of seconds since 1st January 1970, as per unix specification.</returns>
		public static string DateToUnixTimestamp(DateTime date)
		{
			TimeSpan ts = date - unixStartDate;
			return ts.TotalSeconds.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
		}

		/// <summary>
		/// Converts a string, representing a unix timestamp number into a <see cref="DateTime"/> object.
		/// </summary>
		/// <param name="timestamp">The timestamp, as a string.</param>
		/// <returns>The <see cref="DateTime"/> object the time represents.</returns>
		public static DateTime UnixTimestampToDate(string timestamp)
		{
			if( String.IsNullOrEmpty(timestamp) ) return DateTime.MinValue;
            try
            {
                return UnixTimestampToDate(Int64.Parse(timestamp, System.Globalization.NumberFormatInfo.InvariantInfo));
            }
            catch (FormatException)
            {
                return DateTime.MinValue;
            }
		}

		/// <summary>
		/// Converts a <see cref="long"/>, representing a unix timestamp number into a <see cref="DateTime"/> object.
		/// </summary>
		/// <param name="timestamp">The unix timestamp.</param>
		/// <returns>The <see cref="DateTime"/> object the time represents.</returns>
		public static DateTime UnixTimestampToDate(long timestamp)
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
		public static string ExtrasToString(PhotoSearchExtras extras)
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

			if( (extras & PhotoSearchExtras.Views) == PhotoSearchExtras.Views )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("views");
			}

			if( (extras & PhotoSearchExtras.Media) == PhotoSearchExtras.Media )
			{
				if( sb.Length>0 ) sb.Append(",");
				sb.Append("media");
			}

			return sb.ToString();
		}

        /// <summary>
        /// Converts a <see cref="PhotoSearchSortOrder"/> into a string for use by the Flickr API.
        /// </summary>
        /// <param name="order">The sort order to convert.</param>
        /// <returns>The string representative for the sort order.</returns>
		public static string SortOrderToString(PhotoSearchSortOrder order)
		{
			switch(order)
			{
				case PhotoSearchSortOrder.DatePostedAscending:
					return "date-posted-asc";
				case PhotoSearchSortOrder.DatePostedDescending:
					return "date-posted-desc";
				case PhotoSearchSortOrder.DateTakenAscending:
					return "date-taken-asc";
				case PhotoSearchSortOrder.DateTakenDescending:
					return "date-taken-desc";
				case PhotoSearchSortOrder.InterestingnessAscending:
					return "interestingness-asc";
				case PhotoSearchSortOrder.InterestingnessDescending:
					return "interestingness-desc";
				case PhotoSearchSortOrder.Relevance:
					return "relevance";
				default:
					return null;
			}
		}

        /// <summary>
        /// Converts a <see cref="PopularitySort"/> enum to a string.
        /// </summary>
        /// <param name="sortOrder">The value to convert.</param>
        /// <returns></returns>
        public static string SortOrderToString(PopularitySort sortOrder)
        {
            switch (sortOrder)
            {
                case PopularitySort.Comments:
                    return "comments";
                case PopularitySort.Favorites:
                    return "favorites";
                case PopularitySort.Views:
                    return "views";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Adds the partial options to the passed in <see cref="Hashtable"/>.
        /// </summary>
        /// <param name="options">The options to convert to an array.</param>
        /// <param name="parameters">The <see cref="Hashtable"/> to add the option key value pairs to.</param>
		public static void PartialOptionsIntoArray(PartialSearchOptions options, Dictionary<string, string> parameters)
		{
			if( options.MinUploadDate != DateTime.MinValue ) parameters.Add("min_uploaded_date", UtilityMethods.DateToUnixTimestamp(options.MinUploadDate).ToString());
			if( options.MaxUploadDate != DateTime.MinValue ) parameters.Add("max_uploaded_date", UtilityMethods.DateToUnixTimestamp(options.MaxUploadDate).ToString());
			if( options.MinTakenDate != DateTime.MinValue ) parameters.Add("min_taken_date", options.MinTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            if (options.MaxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", options.MaxTakenDate.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo));
			if( options.Extras != PhotoSearchExtras.None ) parameters.Add("extras", options.ExtrasString);
			if( options.SortOrder != PhotoSearchSortOrder.None ) parameters.Add("sort", options.SortOrderString);
			if( options.PerPage > 0 ) parameters.Add("per_page", options.PerPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
			if( options.Page > 0 ) parameters.Add("page", options.Page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
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
			return String.Format(System.Globalization.CultureInfo.InvariantCulture, format, parameters);
		}

        internal static MemberTypes ParseIdToMemberType(string memberTypeId)
        {
            switch (memberTypeId)
            {
                case "1":
                    return MemberTypes.Narwhal;
                case "2":
                    return MemberTypes.Member;
                case "3":
                    return MemberTypes.Moderator;
                case "4":
                    return MemberTypes.Admin;
                default:
                    return MemberTypes.None;
            }
        }

        internal static string MemberTypeToString(MemberTypes memberTypes)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if ((memberTypes & MemberTypes.Member) == MemberTypes.Member)
                sb.Append("2");
            if ((memberTypes & MemberTypes.Moderator) == MemberTypes.Moderator)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append("3");
            }
            if ((memberTypes & MemberTypes.Admin) == MemberTypes.Admin)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append("4");
            }
            if ((memberTypes & MemberTypes.Narwhal) == MemberTypes.Narwhal)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append("1");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates an MD5 Hash of the passed in string.
        /// </summary>
        /// <param name="data">The unhashed string.</param>
        /// <returns>The MD5 hash string.</returns>
        public static string MD5Hash(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider csp = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            byte[] hashedBytes = csp.ComputeHash(bytes, 0, bytes.Length);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }

        /// <summary>
        /// Parses a date which may contain only a vald year component.
        /// </summary>
        /// <param name="date">The date, as a string, to be parsed.</param>
        /// <returns>The parsed <see cref="DateTime"/>.</returns>
        public  static DateTime ParseDateWithGranularity(string date)
        {
            DateTime output;

            string format = "yyyy-MM-dd HH:mm:ss";
            bool ableToParse = DateTime.TryParseExact(date, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None, out output);
            if (!ableToParse)
            {
                if (Regex.IsMatch(date, @"^\d{4}-00-01 00:00:00$"))
                {
                    output = new DateTime(int.Parse(date.Substring(0, 4), System.Globalization.NumberFormatInfo.InvariantInfo), 1, 1);
                }
            }

            return output;
        }

    }

}
