using System;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// A collection of members returned by the <see cref="Flickr.GroupsMembersGetList(string)"/> method.
	/// </summary>
    public class MemberCollection : List<Member>, IFlickrParsable
    {
		/// <summary>
		/// The page of the results returned.
		/// </summary>
        public int Page { get; private set; }

		/// <summary>
		/// The total number of pages that could have been returned.
		/// </summary>
        public int Pages { get; private set; }

		/// <summary>
		/// The total number of members in the group.
		/// </summary>
        public int Total { get; private set; }

		/// <summary>
		/// The number of members returned per page.
		/// </summary>
        public int PerPage { get; private set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            //<members page="1" pages="1" perpage="100" total="33">

            if (reader.GetAttribute("page") != null) Page = int.Parse(reader.GetAttribute("page"), System.Globalization.CultureInfo.InvariantCulture);
            if (reader.GetAttribute("pages") != null) Pages = int.Parse(reader.GetAttribute("pages"), System.Globalization.CultureInfo.InvariantCulture);
            if (reader.GetAttribute("perpage") != null) PerPage = int.Parse(reader.GetAttribute("perpage"), System.Globalization.CultureInfo.InvariantCulture);
            if (reader.GetAttribute("total") != null) Total = int.Parse(reader.GetAttribute("total"), System.Globalization.CultureInfo.InvariantCulture);

            ArrayList list = new ArrayList();

            while (reader.Read())
            {
                if (reader.Name == "member")
                {
                    Member m = new Member();
                    ((IFlickrParsable)m).Load(reader);
                    Add(m);
                }
            }
        }
    }
}
