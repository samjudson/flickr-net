using System;
using System.Configuration;
using System.Xml;

// TODO FIX THIS
//#if !(MONOTOUCH || WindowsCE || SILVERLIGHT)
//namespace FlickrNet
//{
//    /// <summary>
//    /// Summary description for FlickrConfigurationManager.
//    /// </summary>
//    internal class FlickrConfigurationManager : IConfigurationSectionHandler
//    {
//        private static string configSection = "flickrNet";
//        private static FlickrConfigurationSettings settings;

//        public static FlickrConfigurationSettings Settings
//        {
//            get
//            {
//                if (settings == null)
//                {
//                    settings = (FlickrConfigurationSettings)ConfigurationManager.GetSection(configSection);
//                }

//                return settings;
//            }
//        }

//        public object Create(object parent, object configContext, XmlNode section)
//        {
//            configSection = section.Name;
//            return new FlickrConfigurationSettings(section);
//        }
//    }
//}
//#endif
