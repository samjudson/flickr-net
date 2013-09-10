using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrNetTest
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthTokenRequiredAttribute : Attribute
    {
    }
}
