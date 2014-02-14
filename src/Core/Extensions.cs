using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrNet
{
    internal static class Extensions
    {
        private static readonly DateTime UnixStartDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string ToUnixTimestamp(this DateTime date)
        {
            TimeSpan ts = date - UnixStartDate;
            return ts.TotalSeconds.ToString("0", NumberFormatInfo.InvariantInfo);
        }

        public static string ToMySql(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToString(this PhotoSearchExtras extras)
        {
            return null;
        }
    }
}
