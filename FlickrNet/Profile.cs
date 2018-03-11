using System;
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
        /// <summary>
        /// Date the user joined Flickr.
        /// </summary>
        public DateTime? JoinDate { get; private set; }
        /// <summary>
        /// Occupation of user, if set.
        /// </summary>
        public string Occupation { get; private set; }
        /// <summary>
        /// Hometown of user, if set and visible.
        /// </summary>
        public string HomeTown { get; private set; }
        /// <summary>
        /// The photoset id of the showcase set for this user.
        /// </summary>
        public string ShowcaseSet { get; private set; }
        /// <summary>
        /// The title of the showcase set for this user.
        /// </summary>
        public string ShowcaseSetTitle { get; private set; }
        /// <summary>
        /// The first name of this user.
        /// </summary>
        public string FirstName { get; private set; }
        /// <summary>
        /// The last name of this user.
        /// </summary>
        public string LastName { get; private set; }
        /// <summary>
        /// The verbose text of this users profile description.
        /// </summary>
        public string ProfileDescription { get; private set; }
        /// <summary>
        /// The web site for this user, if set.
        /// </summary>
        public string WebSite { get; private set; }
        /// <summary>
        /// The city where the user lives, if set.
        /// </summary>
        public string City { get; private set; }
        /// <summary>
        /// The country where the user lives, if set.
        /// </summary>
        public string Country { get; private set; }
        /// <summary>
        /// Facebook username/url.
        /// </summary>
        public string Facebook { get; private set; }
        /// <summary>
        /// Tumblr url.
        /// </summary>
        public string Tumblr { get; private set; }
        /// <summary>
        /// Twitter url.
        /// </summary>
        public string Twitter { get; private set; }
        /// <summary>
        /// Instagram url.
        /// </summary>
        public string Instagram { get; private set; }
        /// <summary>
        /// PInterest url.
        /// </summary>
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
