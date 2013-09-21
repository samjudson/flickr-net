using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNetTest
{
    [TestFixture]
    public class BaseTest
    {
        private Flickr _instance;
        private Flickr _authInstance;
        private Dictionary<string, string> _errorLog;

        protected Flickr Instance
        {
            get { return _instance ?? (_instance = TestData.GetInstance()); }
        }

        protected Flickr AuthInstance
        {
            get { return _authInstance ?? (_authInstance = TestData.GetAuthInstance()); }
        }

        protected bool InstanceUsed
        {
            get { return _instance != null; }
        }

        protected bool AuthInstanceUsed
        {
            get { return _authInstance != null; }
        }

        [SetUp]
        public void InitialiseLoggingAndFlickr()
        {
            _instance = null;
            _authInstance = null;
            _errorLog = new Dictionary<string, string>();
        }

        protected void LogOnError(string key, string information)
        {
            _errorLog.Add(key, information);
        }

        [TearDown]
        public void ErrorLogging()
        {
            if (TestContext.CurrentContext.Result.Status != TestStatus.Failed) return;

            if (InstanceUsed)
            {
                Console.WriteLine(_instance.LastRequest);
                Console.WriteLine(_instance.LastResponse);
            }
            else
            {
                Console.WriteLine(_authInstance.LastRequest);
                Console.WriteLine(_authInstance.LastResponse);
            }

            foreach (var line in _errorLog)
            {
                Console.WriteLine(line.Key + ": " + line.Value);
            }
        }

    }
}
