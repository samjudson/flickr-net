using System;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections.Generic;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for Methods.
	/// </summary>
    public sealed class MethodCollection : System.Collections.ObjectModel.Collection<string>, IFlickrParsable
	{
        void IFlickrParsable.Load(XmlReader reader)
        {
            reader.Read();

            while (reader.LocalName == "method")
            {
                Add(reader.ReadElementContentAsString());
            }

            reader.Skip();
        }
    }

	/// <summary>
	/// A method supported by the Flickr API.
	/// </summary>
	/// <remarks>
	/// See <a href="http://www.flickr.com/services/api">Flickr API Documentation</a> for a complete list
	/// of methods.
	/// </remarks>
    public sealed class Method : IFlickrParsable
	{
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Method()
        {
            Arguments = new System.Collections.ObjectModel.Collection<MethodArgument>();
            Errors = new System.Collections.ObjectModel.Collection<MethodError>();
        }
		/// <summary>
		/// The name of the method.
		/// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Does the method require the call to be authenticated.
        /// </summary>
        public bool NeedsLogin { get; private set; }

        /// <summary>
        /// Does the method request the call to be signed.
        /// </summary>
        public bool NeedsSigning { get; private set; }

        /// <summary>
        /// The minimum level of permissions required for this method call.
        /// </summary>
        public MethodPermission RequiredPermissions { get; private set; }

		/// <summary>
		/// The description of the method.
		/// </summary>
        public string Description { get; private set; }

		/// <summary>
		/// An example response for the method.
		/// </summary>
        public string Response { get; private set; }

		/// <summary>
		/// An explanation of the example response for the method.
		/// </summary>
       public string Explanation { get; private set; }

		/// <summary>
		/// The arguments of the method.
		/// </summary>
        public System.Collections.ObjectModel.Collection<MethodArgument> Arguments { get; private set; }

		/// <summary>
		/// The possible errors that could be returned by the method.
		/// </summary>
        public System.Collections.ObjectModel.Collection<MethodError> Errors { get; private set; }


        #region IFlickrParsable Members

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "method")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "name":
                        Name = reader.Value;
                        break;
                    case "needslogin":
                        NeedsLogin = reader.Value == "1";
                        break;
                    case "needssigning":
                        NeedsSigning = reader.Value == "1";
                        break;
                    case "requiredperms":
                        RequiredPermissions = (MethodPermission)int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            while (reader.LocalName != "method")
            {
                switch (reader.LocalName)
                {
                    case "description":
                        Description = reader.ReadElementContentAsString();
                        break;
                    case "response":
                        Response = reader.ReadElementContentAsString();
                        break;
                    case "explanation":
                        Explanation = reader.ReadElementContentAsString();
                        break;
                }
            }

            reader.ReadToFollowing("argument");

            while (reader.LocalName == "argument")
            {
                MethodArgument a = new MethodArgument();
                ((IFlickrParsable)a).Load(reader);
                Arguments.Add(a);
            }
            reader.ReadToFollowing("error");

            while (reader.LocalName == "error")
            {
                MethodError e = new MethodError();
                ((IFlickrParsable)e).Load(reader);
                Errors.Add(e);
            }
            reader.Read();

            reader.Skip();
        }

        #endregion
    }

    /// <summary>
    /// An enumeration listing the permission levels required for calling the Flickr API methods.
    /// </summary>
    public enum MethodPermission
    {
        /// <summary>
        /// No permissions required.
        /// </summary>
        None = 0,
        /// <summary>
        /// A minimum of 'read' permissions required.
        /// </summary>
        Read = 1,
        /// <summary>
        /// A minimum of 'write' permissions required.
        /// </summary>
        Write = 2
    }

	/// <summary>
	/// An argument for a method.
	/// </summary>
    public sealed class MethodArgument : IFlickrParsable
	{
		/// <summary>
		/// The name of the argument.
		/// </summary>
        public string Name { get; private set; }

		/// <summary>
		/// Is the argument optional or not.
		/// </summary>
        public bool IsOptional { get; private set; }

		/// <summary>
		/// The description of the argument.
		/// </summary>
        public string Description { get; private set; }

        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "argument")
                return;

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "name":
                        Name = reader.Value;
                        break;
                    case "optional":
                        IsOptional = reader.Value == "1";
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            Description = reader.ReadContentAsString();

            reader.Read();
        }
    }

	/// <summary>
	/// A possible error that a method can return.
	/// </summary>
    public sealed class MethodError : IFlickrParsable
	{
		/// <summary>
		/// The code for the error.
		/// </summary>
        public int Code { get; set; }

        /// <summary>
        /// The message for a method error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The description of a method error.
        /// </summary>
        public string Description { get; set; }


        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "error")
                return;

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "code":
                        Code = int.Parse(reader.Value, System.Globalization.NumberFormatInfo.InvariantInfo);
                        break;
                    case "message":
                        Message = reader.Value;
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;
                }
            }

            reader.Read();

            if (reader.NodeType == XmlNodeType.Text)
            {
                Description = reader.ReadContentAsString();
                reader.Read();
            }

        }
    }
}
