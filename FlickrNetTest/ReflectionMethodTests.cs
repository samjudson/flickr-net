using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickrNet;
using System.Reflection;

namespace FlickrNetTest
{
    /// <summary>
    /// Summary description for ReflectionMethodTests
    /// </summary>
    [TestClass]
    public class ReflectionMethodTests
    {
        public ReflectionMethodTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ReflectionMethodsBasic()
        {
            Flickr f = TestData.GetInstance();

            MethodCollection methodNames = f.ReflectionGetMethods();

            Assert.IsNotNull(methodNames, "Should not be null");
            Assert.AreNotEqual(0, methodNames.Count, "Should return some method names.");
            Assert.IsNotNull(methodNames[0], "First item should not be null");

        }

        [TestMethod]
        public void ReflectionMethodsCheckWeSupport()
        {
            Flickr f = TestData.GetInstance();

            MethodCollection methodNames = f.ReflectionGetMethods();

            Assert.IsNotNull(methodNames, "Should not be null");
            Assert.AreNotEqual(0, methodNames.Count, "Should return some method names.");
            Assert.IsNotNull(methodNames[0], "First item should not be null");

            Type type = typeof(Flickr);
            MethodInfo[] methods = type.GetMethods();

            int failCount = 0;

            foreach (string methodName in methodNames)
            {
                bool found = false;
                string trueName = methodName.Replace("flickr.", "").Replace(".", "").ToLower();
                foreach (MethodInfo info in methods)
                {
                    if (trueName == info.Name.ToLower())
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    failCount++;
                    Console.WriteLine("Method '" + methodName + "' not found in FlickrNet.Flickr.");
                }
            }

            Assert.AreEqual(0, failCount, "FailCount should be zero. Currently " + failCount + " unsupported methods found.");
        }

        [TestMethod]
        public void ReflectionMethodsCheckWeSupportAsync()
        {
            Flickr f = TestData.GetInstance();

            MethodCollection methodNames = f.ReflectionGetMethods();

            Assert.IsNotNull(methodNames, "Should not be null");
            Assert.AreNotEqual(0, methodNames.Count, "Should return some method names.");
            Assert.IsNotNull(methodNames[0], "First item should not be null");

            Type type = typeof(Flickr);
            MethodInfo[] methods = type.GetMethods();

            int failCount = 0;

            foreach (string methodName in methodNames)
            {
                bool found = false;
                string trueName = methodName.Replace("flickr.", "").Replace(".", "").ToLower() + "async";
                foreach (MethodInfo info in methods)
                {
                    if (trueName == info.Name.ToLower())
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    failCount++;
                    Console.WriteLine("Async Method '" + methodName + "' not found in FlickrNet.Flickr.");
                }
            }

            Assert.AreEqual(0, failCount, "FailCount should be zero. Currently " + failCount + " unsupported methods found.");
        }

        [TestMethod]
        public void ReflectionGetMethodInfoSearchArgCheck()
        {
            PropertyInfo[] properties = typeof(PhotoSearchOptions).GetProperties();

            Method flickrMethod = TestData.GetInstance().ReflectionGetMethodInfo("flickr.photos.search");

            // These arguments are covered, but are named slightly differently from Flickr.
            Dictionary<string, string> exceptions = new Dictionary<string, string>();
            exceptions.Add("license", "licenses"); // Licenses
            exceptions.Add("sort", "sortorder"); // SortOrder
            exceptions.Add("bbox", "boundarybox"); // BoundaryBox
            exceptions.Add("lat", "latitude"); // Latitude
            exceptions.Add("lon", "longitude"); // Longitude
            exceptions.Add("media", "mediatype"); // MediaType

            int numMissing = 0;

            foreach (MethodArgument argument in flickrMethod.Arguments)
            {
                if (argument.Name == "api_key") continue;

                bool found = false;

                string arg = argument.Name.Replace("_", "").ToLower();

                if (exceptions.ContainsKey(arg)) arg = exceptions[arg];

                foreach (PropertyInfo info in properties)
                {
                    string propName = info.Name.ToLower();
                    if (arg == propName)
                    {
                        found = true;
                        break;
                    }
                }


                if (!found)
                {
                    numMissing++;
                    Console.WriteLine("Argument " + argument.Name + " not found.");
                }
            }

            Assert.AreEqual(0, numMissing, "Number of missing arguments should be zero.");
        }

        [TestMethod]
        [Ignore]
        public void ReflectionMethodsCheckWeSupportAndParametersMatch()
        {
            List<string> exceptions = new List<string>();
            exceptions.Add("flickr.photos.getWithGeoData");
            exceptions.Add("flickr.photos.getWithouGeoData");
            exceptions.Add("flickr.photos.search");
            exceptions.Add("flickr.photos.getNotInSet");
            exceptions.Add("flickr.photos.getUntagged");

            Flickr f = TestData.GetInstance();

            MethodCollection methodNames = f.ReflectionGetMethods();

            Assert.IsNotNull(methodNames, "Should not be null");
            Assert.AreNotEqual(0, methodNames.Count, "Should return some method names.");
            Assert.IsNotNull(methodNames[0], "First item should not be null");

            Type type = typeof(Flickr);
            MethodInfo[] methods = type.GetMethods();

            int failCount = 0;

            foreach (string methodName in methodNames)
            {
                bool found = false;
                bool foundTrue = false;
                string trueName = methodName.Replace("flickr.", "").Replace(".", "").ToLower();
                foreach (MethodInfo info in methods)
                {
                    if (trueName == info.Name.ToLower())
                    {
                        found = true;
                        break;
                    }
                }
                // Check the number of arguments to see if we have a matching method.
                if (found && !exceptions.Contains(methodName))
                {
                    Method method = f.ReflectionGetMethodInfo(methodName);
                    foreach (MethodInfo info in methods)
                    {
                        if (method.Arguments.Count - 1 == info.GetParameters().Length)
                        {
                            foundTrue = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    failCount++;
                    Console.WriteLine("Method '" + methodName + "' not found in FlickrNet.Flickr.");
                }
                if (found && !foundTrue)
                {
                    Console.WriteLine("Method '" + methodName + "' found but no matching method with all arguments.");
                }
            }

            Assert.AreEqual(0, failCount, "FailCount should be zero. Currently " + failCount + " unsupported methods found.");
        }


        [TestMethod]
        public void ReflectionGetMethodInfoTest()
        {
            Flickr f = TestData.GetInstance();
            Method method = f.ReflectionGetMethodInfo("flickr.reflection.getMethodInfo");

            Assert.IsNotNull(method, "Method should not be null");
            Assert.AreEqual("flickr.reflection.getMethodInfo", method.Name, "Method name not set correctly");

            Assert.AreEqual(MethodPermission.None, method.RequiredPermissions);

            Assert.AreEqual(2, method.Arguments.Count, "There should be two arguments");
            Assert.AreEqual("api_key", method.Arguments[0].Name, "First argument should be api_key.");
            Assert.IsFalse(method.Arguments[0].IsOptional, "First argument should not be optional.");

            Assert.AreEqual(8, method.Errors.Count, "There should be 8 errors.");
            Assert.AreEqual(1, method.Errors[0].Code, "First error should have code of 1");
            Assert.AreEqual("Method not found", method.Errors[0].Message, "First error should have code of 1");
            Assert.AreEqual("The requested method was not found.", method.Errors[0].Description, "First error should have code of 1");
        }

        [TestMethod]
        public void ReflectionGetMethodInfoFavContextArguments()
        {
            var methodName = "flickr.favorites.getContext";
            var method = TestData.GetInstance().ReflectionGetMethodInfo(methodName);

            Assert.AreEqual(6, method.Arguments.Count);
            Assert.AreEqual("The id of the photo to fetch the context for.", method.Arguments[1].Description);
            Assert.IsNull(method.Arguments[4].Description);
        }

        private void GetExceptionList()
        {
            Dictionary<int, List<string>> errors = new Dictionary<int, List<string>>();
            Flickr.CacheDisabled = true;

            Flickr f = TestData.GetInstance();
            var list = f.ReflectionGetMethods();
            foreach (var methodName in list)
            {
                Console.WriteLine("Method = " + methodName);
                var method = f.ReflectionGetMethodInfo(methodName);

                foreach (var exception in method.Errors)
                {
                    if (!errors.ContainsKey(exception.Code))
                    {
                        errors[exception.Code] = new List<string>();
                    }

                    var l = errors[exception.Code];
                    if (!l.Contains(exception.Message))
                    {
                        l.Add(exception.Message);
                    }
                }
            }

            foreach (var pair in errors)
            {
                Console.WriteLine("Code,Message");
                foreach (string l in pair.Value)
                {
                    Console.WriteLine(pair.Key + ",\"" + l + "\"");
                }
                Console.WriteLine();
            }
        }
    }
}
