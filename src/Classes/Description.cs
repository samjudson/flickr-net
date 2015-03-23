using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlickrNet
{
    internal class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }

        public DescriptionAttribute()
        {

        }

        public DescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
