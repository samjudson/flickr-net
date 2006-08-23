using System;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for Methods.
	/// </summary>
	public class Methods
	{
		private Methods()
		{
		}

		internal static string[] GetMethods(XmlElement element)
		{
			XmlNodeList nodes = element.SelectNodes("method");
			string[] _methods = new string[nodes.Count];
			for(int i = 0; i < nodes.Count; i++)
			{
				_methods[i] = nodes[i].Value;
			}
			return _methods;
		}
	}

	/// <summary>
	/// A method supported by the Flickr API.
	/// </summary>
	/// <remarks>
	/// See <a href="http://www.flickr.com/services/api">Flickr API Documentation</a> for a complete list
	/// of methods.
	/// </remarks>
	[Serializable]
	public class Method
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Method()
		{
		}

		/// <summary>
		/// The name of the method.
		/// </summary>
		[XmlAttribute("name", Form=XmlSchemaForm.Unqualified)]
		public string Name;

		/// <summary>
		/// The description of the method.
		/// </summary>
		[XmlElement("description", Form=XmlSchemaForm.Unqualified)]
		public string Description;

		/// <summary>
		/// An example response for the method.
		/// </summary>
		[XmlElement("response", Form=XmlSchemaForm.Unqualified)]
		public string Response;

		/// <summary>
		/// An explanation of the example response for the method.
		/// </summary>
		[XmlElement("explanation", Form=XmlSchemaForm.Unqualified)]
		public string Explanation;

		/// <summary>
		/// The arguments of the method.
		/// </summary>
		[XmlElement("arguments", Form=XmlSchemaForm.Unqualified)]
		public Arguments Arguments;

		/// <summary>
		/// The possible errors that could be returned by the method.
		/// </summary>
		[XmlArray()]
		[XmlArrayItem("error", typeof(MethodError), Form=XmlSchemaForm.Unqualified)]
		public MethodError[] Errors;

	}

	/// <summary>
	/// An instance containing a collection of <see cref="Argument"/> instances.
	/// </summary>
	[Serializable]
	public class Arguments
	{
		/// <summary>
		/// A collection of <see cref="Argument"/> instances.
		/// </summary>
		[XmlElement("argument", Form=XmlSchemaForm.Unqualified)]
		public Argument[] ArgumentCollection;
	}

	/// <summary>
	/// An argument for a method.
	/// </summary>
	[Serializable]
	public class Argument
	{
		/// <summary>
		/// The name of the argument.
		/// </summary>
		[XmlElement("name")]
		public string ArgumentName;

		/// <summary>
		/// Is the argument optional or not.
		/// </summary>
		[XmlElement("optional")]
		public int Optional;

		/// <summary>
		/// The description of the argument.
		/// </summary>
		[XmlText()]
		public string ArgumentDescription;
	}

	/// <summary>
	/// A possible error that a method can return.
	/// </summary>
	[Serializable]
	public class MethodError
	{
		/// <summary>
		/// The code for the error.
		/// </summary>
		[XmlElement("code")]
		public int Code;

	}
}
