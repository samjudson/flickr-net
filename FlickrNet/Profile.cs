using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// A user's profile
    /// </summary>
    public class Profile : IFlickrParsable
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public string UserId { get; private set; }
        public DateTime? JoinDate { get; private set; }
        public string Occupation { get; private set; }
        public string HomeTown { get; private set; }
        public string ShowcaseSet { get; private set; }
        public string ShowcaseSetTitle { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileDescription { get; private set; }
        public string WebSite { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string Facebook { get; private set; }
        public string Tumblr { get; private set; }
        public string Twitter { get; private set; }
        public string Instagram { get; private set; }
        public string Pinterest { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                    case "nsid":
                        UserId = reader.Value;
                        break;
                    case "join_date":
                        JoinDate = UtilityMethods.UnixTimestampToDate(reader.Value);
                        break;
                    case "occupation":
                        Occupation = reader.Value;
                        break;
                    case "hometown":
                        HomeTown = reader.Value;
                        break;
                    case "showcase_set":
                        ShowcaseSet = reader.Value;
                        break;
                    case "showcase_set_title":
                        ShowcaseSetTitle = reader.Value;
                        break;
                    case "first_name":
                        FirstName = reader.Value;
                        break;
                    case "last_name":
                        LastName = reader.Value;
                        break;
                    case "profile_description":
                        ProfileDescription = reader.Value;
                        break;
                    case "website":
                        WebSite = reader.Value;
                        break;
                    case "city":
                        City = reader.Value;
                        break;
                    case "country":
                        Country = reader.Value;
                        break;
                    case "facebook":
                        Facebook = reader.Value;
                        break;
                    case "twitter":
                        Twitter = reader.Value;
                        break;
                    case "tumblr":
                        Tumblr = reader.Value;
                        break;
                    case "instagram":
                        Instagram = reader.Value;
                        break;
                    case "pinterest":
                        Pinterest = reader.Value;
                        break;

                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }
        }
    }
}
