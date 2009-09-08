using System;
using System.Text;

namespace FlickrNet
{
	/// <summary>
	/// The type of a member. Passed as a parameter to <see cref="Flickr.GroupsMemberGetList(string)"/> and returned for each <see cref="Member"/> as well.
	/// </summary>
    [Flags]
    public enum MemberType
    {
		/// <summary>
		/// No member type has been specified (all should be returned).
		/// </summary>
        NotSpecified,
		/// <summary>
		/// A basic member.
		/// </summary>
        Member,
		/// <summary>
		/// A group moderator.
		/// </summary>
        Moderator,
		/// <summary>
		/// A group adminstrator.
		/// </summary>
        Admin,
		/// <summary>
		/// Some strange kind of super-admin. Unsupported by the API.
		/// </summary>
        Narwhal
    }
}
