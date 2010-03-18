using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Permissions for the selected photo.
	/// </summary>
	public sealed class GeoPermissions : IFlickrParsable
	{
		/// <summary>
		/// The ID for the photo whose permissions these are.
		/// </summary>
		public string PhotoId { get; private set; }

		/// <summary>
		/// Are the general unwashed (public) allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsPublic { get; private set; }
    
		/// <summary>
		/// Are contacts allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsContact { get; private set; }
    
		/// <summary>
		/// Are friends allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsFriend { get; private set; }
    
		/// <summary>
		/// Are family allowed to see the Geo Location information for this photo.
		/// </summary>
		public bool IsFamily { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "id":
                        PhotoId = reader.Value;
                        break;
                    case "ispublic":
                        IsPublic = reader.Value == "1";
                        break;
                    case "iscontact":
                        IsContact = reader.Value == "1";
                        break;
                    case "isfamily":
                        IsFamily = reader.Value == "1";
                        break;
                    case "isfriend":
                        IsFriend = reader.Value == "1";
                        break;
                }
            }
            reader.Read();
        }
    }
}
