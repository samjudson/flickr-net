using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.Collections.Generic;
#if SILVERLIGHT
using System.Linq;
#endif

namespace FlickrNet
{
    /// <summary>
    /// Internal class providing certain utility functions to other classes.
    /// </summary>
    internal sealed class UtilityMethods
    {
        private static readonly DateTime unixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
                    return String.Empty;

            }
        }

        /// <summary>
        /// Convert a <see cref="TagMode"/> to a string used when passing to Flickr.
        /// </summary>
        /// <param name="tagMode">The tag mode to convert.</param>
        /// <returns>The string to pass to Flickr.</returns>
        public static string TagModeToString(TagMode tagMode)
        {
            switch (tagMode)
            {
                case TagMode.None:
                    return String.Empty;
                case TagMode.AllTags:
                    return "all";
                case TagMode.AnyTag:
                    return "any";
                case TagMode.Boolean:
                    return "bool";
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// Convert a <see cref="MachineTagMode"/> to a string used when passing to Flickr.
        /// </summary>
        /// <param name="machineTagMode">The machine tag mode to convert.</param>
        /// <returns>The string to pass to Flickr.</returns>
        public static string MachineTagModeToString(MachineTagMode machineTagMode)
        {
            switch (machineTagMode)
            {
                case MachineTagMode.None:
                    return String.Empty;
                case MachineTagMode.AllTags:
                    return "all";
                case MachineTagMode.AnyTag:
                    return "any";
                default:
                    return String.Empty;
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
            if (String.IsNullOrEmpty(timestamp)) return DateTime.MinValue;
            try
            {
                return UnixTimestampToDate(Int64.Parse(timestamp, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo));
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
            List<string> extraList = new List<string>();

            if ((extras & PhotoSearchExtras.DateTaken) == PhotoSearchExtras.DateTaken) extraList.Add("date_taken");
            if ((extras & PhotoSearchExtras.DateUploaded) == PhotoSearchExtras.DateUploaded) extraList.Add("date_upload");
            if ((extras & PhotoSearchExtras.IconServer) == PhotoSearchExtras.IconServer) extraList.Add("icon_server");
            if ((extras & PhotoSearchExtras.License) == PhotoSearchExtras.License) extraList.Add("license");
            if ((extras & PhotoSearchExtras.OwnerName) == PhotoSearchExtras.OwnerName) extraList.Add("owner_name");
            if ((extras & PhotoSearchExtras.OriginalFormat) == PhotoSearchExtras.OriginalFormat) extraList.Add("original_format");
            if ((extras & PhotoSearchExtras.LastUpdated) == PhotoSearchExtras.LastUpdated) extraList.Add("last_update");
            if ((extras & PhotoSearchExtras.Tags) == PhotoSearchExtras.Tags) extraList.Add("tags");
            if ((extras & PhotoSearchExtras.Geo) == PhotoSearchExtras.Geo) extraList.Add("geo");
            if ((extras & PhotoSearchExtras.MachineTags) == PhotoSearchExtras.MachineTags) extraList.Add("machine_tags");
            if ((extras & PhotoSearchExtras.OriginalDimensions) == PhotoSearchExtras.OriginalDimensions) extraList.Add("o_dims");
            if ((extras & PhotoSearchExtras.Views) == PhotoSearchExtras.Views) extraList.Add("views");
            if ((extras & PhotoSearchExtras.Media) == PhotoSearchExtras.Media) extraList.Add("media");
            if ((extras & PhotoSearchExtras.PathAlias) == PhotoSearchExtras.PathAlias) extraList.Add("path_alias");
            if ((extras & PhotoSearchExtras.SquareUrl) == PhotoSearchExtras.SquareUrl) extraList.Add("url_sq");
            if ((extras & PhotoSearchExtras.ThumbnailUrl) == PhotoSearchExtras.ThumbnailUrl) extraList.Add("url_t");
            if ((extras & PhotoSearchExtras.SmallUrl) == PhotoSearchExtras.SmallUrl) extraList.Add("url_s");
            if ((extras & PhotoSearchExtras.MediumUrl) == PhotoSearchExtras.MediumUrl) extraList.Add("url_m");
            if ((extras & PhotoSearchExtras.Medium640Url) == PhotoSearchExtras.Medium640Url) extraList.Add("url_z");
            if ((extras & PhotoSearchExtras.LargeSquareUrl) == PhotoSearchExtras.LargeSquareUrl) extraList.Add("url_q");
            if ((extras & PhotoSearchExtras.Small320Url) == PhotoSearchExtras.Small320Url) extraList.Add("url_n");
            if ((extras & PhotoSearchExtras.LargeUrl) == PhotoSearchExtras.LargeUrl) extraList.Add("url_l");
            if ((extras & PhotoSearchExtras.OriginalUrl) == PhotoSearchExtras.OriginalUrl) extraList.Add("url_o");
            if ((extras & PhotoSearchExtras.Description) == PhotoSearchExtras.Description) extraList.Add("description");
            if ((extras & PhotoSearchExtras.Usage) == PhotoSearchExtras.Usage) extraList.Add("usage");
            if ((extras & PhotoSearchExtras.Visibility) == PhotoSearchExtras.Visibility) extraList.Add("visibility");
            if ((extras & PhotoSearchExtras.Rotation) == PhotoSearchExtras.Rotation) extraList.Add("rotation");

            return String.Join(",", extraList.ToArray());
        }

        /// <summary>
        /// Converts a <see cref="PhotoSearchSortOrder"/> into a string for use by the Flickr API.
        /// </summary>
        /// <param name="order">The sort order to convert.</param>
        /// <returns>The string representative for the sort order.</returns>
        public static string SortOrderToString(PhotoSearchSortOrder order)
        {
            switch (order)
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
                    return String.Empty;
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
                    return String.Empty;
            }
        }

        /// <summary>
        /// Adds the partial options to the passed in <see cref="Hashtable"/>.
        /// </summary>
        /// <param name="options">The options to convert to an array.</param>
        /// <param name="parameters">The <see cref="Hashtable"/> to add the option key value pairs to.</param>
        public static void PartialOptionsIntoArray(PartialSearchOptions options, Dictionary<string, string> parameters)
        {
            if (options == null) throw new ArgumentNullException("options");
            if (parameters == null) throw new ArgumentNullException("parameters");

            if (options.MinUploadDate != DateTime.MinValue) parameters.Add("min_uploaded_date", UtilityMethods.DateToUnixTimestamp(options.MinUploadDate).ToString());
            if (options.MaxUploadDate != DateTime.MinValue) parameters.Add("max_uploaded_date", UtilityMethods.DateToUnixTimestamp(options.MaxUploadDate).ToString());
            if (options.MinTakenDate != DateTime.MinValue) parameters.Add("min_taken_date", DateToMySql(options.MinTakenDate));
            if (options.MaxTakenDate != DateTime.MinValue) parameters.Add("max_taken_date", DateToMySql(options.MaxTakenDate));
            if (options.Extras != PhotoSearchExtras.None) parameters.Add("extras", options.ExtrasString);
            if (options.SortOrder != PhotoSearchSortOrder.None) parameters.Add("sort", options.SortOrderString);
            if (options.PerPage > 0) parameters.Add("per_page", options.PerPage.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (options.Page > 0) parameters.Add("page", options.Page.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));
            if (options.PrivacyFilter != PrivacyFilter.None) parameters.Add("privacy_filter", options.PrivacyFilter.ToString("d"));
        }

        internal static void WriteInt32(Stream s, int i)
        {
            s.WriteByte((byte)(i & 0xFF));
            s.WriteByte((byte)((i >> 8) & 0xFF));
            s.WriteByte((byte)((i >> 16) & 0xFF));
            s.WriteByte((byte)((i >> 24) & 0xFF));
        }

        internal static void WriteString(Stream s, string str)
        {
            WriteInt32(s, str.Length);
            foreach (char c in str)
            {
                s.WriteByte((byte)(c & 0xFF));
                s.WriteByte((byte)((c >> 8) & 0xFF));
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
                i |= b << (j * 8);
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
                chars[i] = (char)(lo | (hi << 8));
            }
            return new string(chars);
        }

        private const string PhotoUrlFormat = "http://farm{0}.staticflickr.com/{1}/{2}_{3}{4}.{5}";

        internal static string UrlFormat(Photo p, string size, string extension)
        {
            if (size == "_o" || size == "original")
                return UrlFormat(p.Farm, p.Server, p.PhotoId, p.OriginalSecret, size, extension);
            else
                return UrlFormat(p.Farm, p.Server, p.PhotoId, p.Secret, size, extension);
        }

        internal static string UrlFormat(PhotoInfo p, string size, string extension)
        {
            if (size == "_o" || size == "original")
                return UrlFormat(p.Farm, p.Server, p.PhotoId, p.OriginalSecret, size, extension);
            else
                return UrlFormat(p.Farm, p.Server, p.PhotoId, p.Secret, size, extension);
        }

        internal static string UrlFormat(Photoset p, string size, string extension)
        {
            return UrlFormat(p.Farm, p.Server, p.PrimaryPhotoId, p.Secret, size, extension);
        }

        internal static string UrlFormat(string farm, string server, string photoid, string secret, string size, string extension)
        {
            switch (size)
            {
                case "square":
                    size = "_s";
                    break;
                case "thumbnail":
                    size = "_t";
                    break;
                case "small":
                    size = "_m";
                    break;
                case "medium":
                    size = String.Empty;
                    break;
                case "large":
                    size = "_b";
                    break;
                case "original":
                    size = "_o";
                    break;
            }

            return UrlFormat(PhotoUrlFormat, farm, server, photoid, secret, size, extension);
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

        internal static MemberTypes ParseRoleToMemberType(string memberRole)
        {
            switch (memberRole)
            {
                case "admin":
                    return MemberTypes.Admin;
                case "moderator":
                    return MemberTypes.Moderator;
                case "member":
                    return MemberTypes.Member;
                default:
                    return MemberTypes.None;
            }
        }

        internal static string MemberTypeToString(MemberTypes memberTypes)
        {
            List<string> types = new List<string>();

            if ((memberTypes & MemberTypes.Narwhal) == MemberTypes.Narwhal) types.Add("1");
            if ((memberTypes & MemberTypes.Member) == MemberTypes.Member) types.Add("2");
            if ((memberTypes & MemberTypes.Moderator) == MemberTypes.Moderator) types.Add("3");
            if ((memberTypes & MemberTypes.Admin) == MemberTypes.Admin) types.Add("4");

            return String.Join(",", types.ToArray());
        }

        /// <summary>
        /// Generates an MD5 Hash of the passed in string.
        /// </summary>
        /// <param name="data">The unhashed string.</param>
        /// <returns>The MD5 hash string.</returns>
        public static string MD5Hash(string data)
        {
            byte[] hashedBytes;

#if SILVERLIGHT
            hashedBytes = MD5Core.GetHash(data, Encoding.UTF8);
#else
            using (System.Security.Cryptography.MD5CryptoServiceProvider csp = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                hashedBytes = csp.ComputeHash(bytes, 0, bytes.Length);
            }
#endif
            return BitConverter.ToString(hashedBytes).Replace("-", String.Empty).ToLower(System.Globalization.CultureInfo.InvariantCulture);
        }

        internal static DateTime MySqlToDate(string p)
        {
            string format1 = "yyyy-MM-dd";
            string format2 = "yyyy-MM-dd hh:mm:ss";
            var iformat = System.Globalization.DateTimeFormatInfo.InvariantInfo;

            try
            {
                return DateTime.ParseExact(p, format1, iformat, System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
            }

            try
            {
                return DateTime.ParseExact(p, format2, iformat, System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
            }

            return DateTime.MinValue;

        }

        /// <summary>
        /// Parses a date which may contain only a vald year component.
        /// </summary>
        /// <param name="date">The date, as a string, to be parsed.</param>
        /// <returns>The parsed <see cref="DateTime"/>.</returns>
        public static DateTime ParseDateWithGranularity(string date)
        {
            DateTime output = DateTime.MinValue;

            if (String.IsNullOrEmpty(date)) return output;
            if (date == "0000-00-00 00:00:00") return output;
            if (date.EndsWith("-00-01 00:00:00"))
            {
                output = new DateTime(int.Parse(date.Substring(0, 4), System.Globalization.NumberFormatInfo.InvariantInfo), 1, 1);
                return output;
            }

            string format = "yyyy-MM-dd HH:mm:ss";
            try
            {
                output = DateTime.ParseExact(date, format, System.Globalization.DateTimeFormatInfo.InvariantInfo, System.Globalization.DateTimeStyles.None);
            }
            catch (FormatException)
            {
#if DEBUG
                throw;
#endif
            }
            return output;
        }

        internal static string DateToMySql(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Converts a <see cref="MediaType"/> enumeration into a string used by Flickr.
        /// </summary>
        /// <param name="mediaType">The <see cref="MediaType"/> value to convert.</param>
        /// <returns></returns>
        public static string MediaTypeToString(MediaType mediaType)
        {
            switch (mediaType)
            {
                case MediaType.All:
                    return "all";
                case MediaType.Photos:
                    return "photos";
                case MediaType.Videos:
                    return "videos";
                default:
                    return String.Empty;
            }
        }

        /// <summary>
        /// If an unknown element is found and the DLL is a debug DLL then a <see cref="ParsingException"/> is thrown.
        /// </summary>
        /// <param name="reader">The <see cref="XmlReader"/> containing the unknown xml node.</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void CheckParsingException(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.Attribute)
            {
                throw new ParsingException("Unknown attribute: " + reader.Name + "=" + reader.Value);
            }
            if (!String.IsNullOrEmpty(reader.Value))
                throw new ParsingException("Unknown " + reader.NodeType.ToString() + ": " + reader.Name + "=" + reader.Value);
            else
                throw new ParsingException("Unknown element: " + reader.Name);
                
        }

        /// <summary>
        /// Returns the buddy icon for a given set of server, farm and userid. If no server is present then returns the standard buddy icon.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="farm"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string BuddyIcon(string server, string farm, string userId)
        {
            if (String.IsNullOrEmpty(server) || server == "0")
                return "http://www.flickr.com/images/buddyicon.jpg";
            else
                return String.Format(System.Globalization.CultureInfo.InvariantCulture, "http://farm{0}.staticflickr.com/{1}/buddyicons/{2}.jpg", farm, server, userId);
        }

        /// <summary>
        /// Converts a URL parameter encoded string into a dictionary.
        /// </summary>
        /// <remarks>
        /// e.g. ab=cd&amp;ef=gh will return a dictionary of { "ab" => "cd", "ef" => "gh" }.</remarks>
        /// <param name="response"></param>
        /// <returns></returns>
        public static Dictionary<string, string> StringToDictionary(string response)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] parts = response.Split('&');

            foreach (string part in parts)
            {
#if WindowsCE || SILVERLIGHT
                string[] bits = part.Split('=');
#else
                string[] bits = part.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);
#endif
                if (bits.Length == 1)
                    dic.Add(bits[0], "");
                else
                    dic.Add(bits[0], Uri.UnescapeDataString(bits[1]));
            }

            return dic;
        }

        /// <summary>
        /// Escapes a string for use with OAuth.
        /// </summary>
        /// <remarks>The only valid characters are Alphanumerics and "-", "_", "." and "~". Everything else is hex encoded.</remarks>
        /// <param name="text">The text to escape.</param>
        /// <returns>The escaped string.</returns>
        public static string EscapeOAuthString(string text)
        {
            string value = text;

            value = Uri.EscapeDataString(value).Replace("+", "%20");

            // UrlEncode escapes with lowercase characters (e.g. %2f) but oAuth needs %2F
            value = Regex.Replace(value, "(%[0-9a-f][0-9a-f])", c => c.Value.ToUpper());

            // these characters are not escaped by UrlEncode() but needed to be escaped
            value = value.Replace("(", "%28").Replace(")", "%29").Replace("$", "%24").Replace("!", "%21").Replace(
                "*", "%2A").Replace("'", "%27");

            // these characters are escaped by UrlEncode() but will fail if unescaped!
            value = value.Replace("%7E", "~");

            return value;

            //string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

            //StringBuilder result = new StringBuilder(text.Length * 2);

            //foreach (char symbol in text)
            //{
            //    if (unreservedChars.IndexOf(symbol) != -1)
            //    {
            //        result.Append(symbol);
            //    }
            //    else
            //    {
            //        result.Append('%' + String.Format("{0:X2}", (int)symbol));
            //    }
            //}

            //return result.ToString();
        }


        internal static string CleanCollectionId(string collectionId)
        {
            if (collectionId.IndexOf("-") < 0)
                return collectionId;
            else
                return collectionId.Substring(collectionId.IndexOf("-")+1);
        }

    }

}
