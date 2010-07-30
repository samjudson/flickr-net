using System;
using System.Configuration;
using System.Xml;

#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
namespace FlickrNet
{
	/// <summary>
	/// Summary description for FlickrConfigurationManager.
	/// </summary>
	internal class FlickrConfigurationManager : IConfigurationSectionHandler
	{
		private static string ConfigSection = "flickrNet";
		private static FlickrConfigurationSettings settings;

		public FlickrConfigurationManager()
		{
		}

		public static FlickrConfigurationSettings Settings
		{
			get	
			{
				if( settings == null )
				{
					settings = (FlickrConfigurationSettings)ConfigurationManager.GetSection( ConfigSection );
				}
				
				return settings;
			}
		}

		public object Create(object parent, object configContext, XmlNode section) 
		{	
			ConfigSection = section.Name;
			return new FlickrConfigurationSettings( section );
		}
	}
}
#endif
