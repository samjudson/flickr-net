using System;
using System.Configuration;
using System.Xml;

namespace FlickrNet
{
	/// <summary>
	/// Summary description for FlickrConfigurationManager.
	/// </summary>
	internal class FlickrConfigurationManager : IConfigurationSectionHandler
	{
		private const string ConfigSection = "flickrNet";
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
					try
					{
						settings = (FlickrConfigurationSettings)ConfigurationSettings.GetConfig( ConfigSection );
					}
					catch
					{
					}
				}
				
				return settings;
			}
		}

		public object Create(object parent, object configContext, XmlNode section) 
		{	
			return new FlickrConfigurationSettings( section );
		}
	}
}
