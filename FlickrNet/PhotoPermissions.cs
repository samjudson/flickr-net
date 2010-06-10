using System;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Permissions for the selected photo.
	/// </summary>
    public sealed class PhotoPermissions : IFlickrParsable
	{
		/// <remarks/>
		public string PhotoId { get; private set; }

		/// <remarks/>
		public bool IsPublic { get; private set; }
    
		/// <remarks/>
		public bool IsFriend { get; private set; }
    
		/// <remarks/>
		public bool IsFamily { get; private set; }

		/// <remarks/>
		public PermissionComment PermissionComment { get; private set; }

		/// <remarks/>
		public PermissionAddMeta PermissionAddMeta { get; private set; }

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
                    case "isfamily":
                        IsFamily = reader.Value == "1";
                        break;
                    case "isfriend":
                        IsFriend = reader.Value == "1";
                        break;
                    case "permcomment":
                        PermissionComment = (PermissionComment)Enum.Parse(typeof(PermissionComment), reader.Value, true);
                        break;
                    case "permaddmeta":
                        PermissionAddMeta = (PermissionAddMeta)Enum.Parse(typeof(PermissionAddMeta), reader.Value, true);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();
        }
    }

}
