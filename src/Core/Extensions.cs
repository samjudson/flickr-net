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
        public static string ToUnixTimestamp(this DateTime date)
        {
            return date.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToString(this PhotoSearchExtras extras)
        {
            return null;
        }
    }
}
