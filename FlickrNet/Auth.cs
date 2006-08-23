using System;

namespace FlickrNet
{
	/// <summary>
	/// Used to specify the authentication levels needed for the Auth methods.
	/// </summary>
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
		/// Deleting does not mean deleting photos, just meta data such as tags.
		/// </summary>
		Delete
	}

	/// <summary>
	/// Successful authentication returns a <see cref="Auth"/> object.
	/// </summary>
	public class Auth
	{
		private string _token;
		private AuthLevel _permissions;
		private FoundUser _user;

		/// <summary>
		/// The authentication token returned by the <see cref="Flickr.AuthGetToken"/> or <see cref="Flickr.AuthCheckToken"/> methods.
		/// </summary>
		public string Token
		{
			get { return _token; }
			set { _token = value; }
		}

		/// <summary>
		/// The permissions the current token allows the application to perform.
		/// </summary>
		public AuthLevel Permissions
		{
			get { return _permissions; }
			set { _permissions = value; }
		}

		/// <summary>
		/// The <see cref="User"/> object associated with the token. Readonly.
		/// </summary>
		public FoundUser User
		{
			get { return _user; }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Auth"/> class.
		/// </summary>
		public Auth()
		{
		}

		internal Auth(System.Xml.XmlElement element)
		{
			Token = element.SelectSingleNode("token").InnerText;
			Permissions = (AuthLevel)Enum.Parse(typeof(AuthLevel), element.SelectSingleNode("perms").InnerText, true);
			System.Xml.XmlNode node = element.SelectSingleNode("user");
			_user = new FoundUser(node);
		}
	}
}
