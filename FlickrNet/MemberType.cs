using System;
using System.Text;

namespace FlickrNet
{
    [Flags]
    public enum MemberType
    {
        NotSpecified,
        Member,
        Moderator,
        Admin,
        Narwhal
    }
}
