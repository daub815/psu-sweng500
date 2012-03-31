namespace Sweng500.Pml.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// This is a test class for SearchMediaServiceTest and is intended
    /// to contain all SearchMediaServiceTest Unit Tests
    /// </summary>
    [TestClass]
    public class SearchRemoteTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get;
           set;
        }

        #region Additional test attributes
        //// 
        ////You can use the following additional attributes as you write your tests:
        ////
        ////Use ClassInitialize to run code before running the first test in the class
        ////[ClassInitialize()]
        ////public static void MyClassInitialize(TestContext testContext)
        ////{
        ////}
        ////
        ////Use ClassCleanup to run code after all tests in a class have run
        ////[ClassCleanup()]
        ////public static void MyClassCleanup()
        ////{
        ////}
        ////
        ////Use TestInitialize to run code before running each test
        ////[TestInitialize()]
        ////public void MyTestInitialize()
        ////{
        ////}
        ////
        ////Use TestCleanup to run code after each test has run
        ////[TestCleanup()]
        ////public void MyTestCleanup()
        ////{
        ////}
        ////
        #endregion

        /// <summary>
        /// A test for SearchRemote
        /// </summary>
        [TestMethod]
        public void SearchRemoteBooksTest()
        {
            SearchMediaService target = new SearchMediaService(); 
            MediaTypes mediatype = MediaTypes.Book; 
            string title = "Star Wars"; 
            string keywords = string.Empty; 
            IEnumerable<Media> actual;
            actual = target.SearchRemote(mediatype, title, keywords);
            foreach (Media media in actual)
            {
                Console.WriteLine("the media returned is: {0}", media.ToString());
            }
        }

        /// <summary>
        /// A test for SearchRemote
        /// </summary>
        [TestMethod]
        public void SearchRemoteVideoTest()
        {
            SearchMediaService target = new SearchMediaService(); // TODO: Initialize to an appropriate value
            MediaTypes mediatype = MediaTypes.Video; // TODO: Initialize to an appropriate value
            string title = "Star Wars"; // TODO: Initialize to an appropriate value
            string keywords = string.Empty; // TODO: Initialize to an appropriate value
            IEnumerable<Media> actual;
            actual = target.SearchRemote(mediatype, title, keywords);
            foreach (Media media in actual)
            {
                Console.WriteLine("the media returned is: {0}", media.ToString());
            }
        }

        /// <summary>
        /// A test for SearchRemote that uses both book and video
        /// </summary>
        [TestMethod]
        public void SearchRemoteBothTest()
        {
            SearchMediaService target = new SearchMediaService(); // TODO: Initialize to an appropriate value
            string title = "Star Wars"; // TODO: Initialize to an appropriate value
            string keywords = string.Empty; // TODO: Initialize to an appropriate value
            bool bookfound = false;
            bool videofound = false;

            IEnumerable<Media> actual;
            actual = target.SearchRemote(title, keywords);
            foreach (Media media in actual)
            {
                if (media is Book)
                {
                    bookfound = true;
                }

                if (media is Video)
                {
                    videofound = true;
                }

                Console.WriteLine("the media returned is: {0}", media.ToString());
            }

            Assert.IsTrue(bookfound, "Did not find a book");
            Assert.IsTrue(videofound, "Did not find a video");
        }

        /// <summary>
        /// A test for SearchRemote that uses both book and video
        /// </summary>
        [TestMethod]
        public void AuthorSearchRemoteTest()
        {
            SearchMediaService target = new SearchMediaService(); // TODO: Initialize to an appropriate value
            string author = "Jim Butcher";
            string keywords = string.Empty; 
            bool bookfound = false;

            IEnumerable<Media> actual;
            actual = target.AuthorSearchRemote(author, keywords);
            foreach (Media media in actual)
            {
                if (media is Book)
                {
                    bookfound = true;
                }

                Console.WriteLine("the media returned is: {0}", media.ToString());
            }

            Assert.IsTrue(bookfound, "Did not find a book");
        }
    }
}
