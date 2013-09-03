using System.Xml;

namespace FlickrNet
{
    /// <summary>
    /// The details of a tag of a photo.
    /// </summary>
    public sealed class PhotoInfoTag : IFlickrParsable
    {
        /// <summary>
        /// The id of the tag.
        /// </summary>
        public string TagId { get; set; }

        /// <summary>
        /// The author id of the tag.
        /// </summary>
        public string AuthorId { get; set; }

        /// <summary>
        /// The real name of the author of the tag.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Raw copy of the tag, as the user entered it.
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// Is the tag a machine tag.
        /// </summary>
        public bool IsMachineTag { get; set; }

        /// <summary>
        /// The actually tag.
        /// </summary>
        public string TagText { get; set; }

        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "tag")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        TagId = reader.Value;
                        break;
                    case "author":
                        AuthorId = reader.Value;
                        break;
                    case "authorname":
                        AuthorName = reader.Value;
                        break;
                    case "raw":
                        Raw = reader.Value;
                        break;
                    case "machine_tag":
                        IsMachineTag = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            TagText = reader.ReadContentAsString();

            reader.Skip();
        }

        #endregion
    }
}

