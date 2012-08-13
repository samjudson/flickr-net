
namespace FlickrNet
{
    using System;

    /// <summary>
    /// Used to specify the authentication levels needed for the Auth methods.
    /// </summary>
    [Serializable]
    public enum AuthLevel
    {
        /// <summary>
        /// No access required - do not use this value!
        /// </summary>
        None, 
        /// <summary>
        /// Read only access is required by your application.
        /// </summary>
        Read, 
        /// <summary>
        /// Read and write access is required by your application.
        /// </summary>
        Write, 
        /// <summary>
        /// Read, write and delete access is required by your application.
        /// Deleting does not mean just the ability to delete photos, but also other meta data such as tags.
        /// </summary>
        Delete
    }

    /// <summary>
    /// Successful authentication returns a <see cref="Auth"/> object.
    /// </summary>
    [Serializable]
    public sealed class Auth : IFlickrParsable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Auth"/> class.
        /// </summary>
        public Auth()
        {
        }

        /// <summary>
        /// The authentication token returned by the <see cref="Flickr.AuthGetToken"/> or <see cref="Flickr.AuthCheckToken(string)"/> methods.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The permissions the current token allows the application to perform.
        /// </summary>
        public AuthLevel Permissions { get; set; }

        /// <summary>
        /// The <see cref="User"/> object associated with the token. Readonly.
        /// </summary>
        public FoundUser User { get; set; }

        void IFlickrParsable.Load(System.Xml.XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName != "auth" && reader.LocalName != "oauth")
            {
                switch (reader.LocalName)
                {
                    case "token":
                        Token = reader.ReadElementContentAsString();
                        break;
                    case "perms":
                        Permissions = (AuthLevel)Enum.Parse(typeof(AuthLevel), reader.ReadElementContentAsString(), true);
                        break;
                    case "user":
                        User = new FoundUser();
                        ((IFlickrParsable)User).Load(reader);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        reader.Skip();
                        break;
                }
            }
        }
    }
}
