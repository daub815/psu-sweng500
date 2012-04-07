namespace Sweng500.Pml.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// This is a test class for SearchMediaServiceTest and is intended
    /// to test web service calls
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

            Mock.MockAWSECommerce mockAWSE = new Mock.MockAWSECommerce();
            mockAWSE.AllowVideos = false;
            mockAWSE.NumberOfVideos = 0;
            mockAWSE.AllowBooks = true;
            mockAWSE.NumberOfBooks = 2;
            target.MockIAWSECommerceService = mockAWSE;
            int bookcount = 0;
            int videocount = 0;
            IEnumerable<Media> actual;
            actual = target.SearchRemote(mediatype, title, keywords);
            foreach (Media media in actual)
            {
                if (media is Book)
                {
                    bookcount++;
                }

                if (media is Video)
                {
                    videocount++;
                }
            }

            Assert.IsTrue(bookcount == mockAWSE.NumberOfBooks, "Did not find correct books");
            Assert.IsTrue(videocount == mockAWSE.NumberOfVideos, "Did not find correct videos");
        }

        /// <summary>
        /// A test for SearchRemote
        /// </summary>
        [TestMethod]
        public void SearchRemoteVideoTest()
        {
            SearchMediaService target = new SearchMediaService(); 
            MediaTypes mediatype = MediaTypes.Video;
            string title = "Star Wars"; 
            string keywords = string.Empty; 

            Mock.MockAWSECommerce mockAWSE = new Mock.MockAWSECommerce();
            mockAWSE.AllowVideos = true;
            mockAWSE.NumberOfVideos = 3;
            mockAWSE.AllowBooks = false;
            mockAWSE.NumberOfBooks = 0;
            target.MockIAWSECommerceService = mockAWSE;
            int bookcount = 0;
            int videocount = 0;
            IEnumerable<Media> actual;
            actual = target.SearchRemote(mediatype, title, keywords);
            foreach (Media media in actual)
            {
                if (media is Book)
                {
                    bookcount++;
                }

                if (media is Video)
                {
                    videocount++;
                }
            }

            Assert.IsTrue(bookcount == mockAWSE.NumberOfBooks, "Did not find correct books");
            Assert.IsTrue(videocount == mockAWSE.NumberOfVideos, "Did not find correct videos");
        }

        /// <summary>
        /// A test for SearchRemote that uses both book and video
        /// </summary>
        [TestMethod]
        public void SearchRemoteBothTest()
        {
            SearchMediaService target = new SearchMediaService(); 
            string title = "Star Wars"; 
            string keywords = string.Empty; 

            Mock.MockAWSECommerce mockAWSE = new Mock.MockAWSECommerce();
            mockAWSE.AllowVideos = true;
            mockAWSE.NumberOfVideos = 1;
            mockAWSE.AllowBooks = true;
            mockAWSE.NumberOfBooks = 2;
            target.MockIAWSECommerceService = mockAWSE;
            int bookcount = 0;
            int videocount = 0;

            IEnumerable<Media> actual;
            actual = target.SearchRemote(title, keywords);
            foreach (Media media in actual)
            {
                if (media is Book)
                {
                    bookcount++;
                }

                if (media is Video)
                {
                    videocount++;
                }
            }

            Assert.IsTrue(bookcount == mockAWSE.NumberOfBooks, "Did not find correct books");
            Assert.IsTrue(videocount == mockAWSE.NumberOfVideos, "Did not find correct videos");
        }

        /// <summary>
        /// A test for SearchRemote that uses book
        /// </summary>
        [TestMethod]
        public void AuthorSearchRemoteTest()
        {
            SearchMediaService target = new SearchMediaService();
            Mock.MockAWSECommerce mockAWSE = new Mock.MockAWSECommerce();
            mockAWSE.AllowVideos = false;
            mockAWSE.AllowBooks = true;
            mockAWSE.NumberOfBooks = 2;
            target.MockIAWSECommerceService = mockAWSE;
            string author = "Jim Butcher";
            string keywords = string.Empty; 

            IEnumerable<Media> actual;
            actual = target.AuthorSearchRemote(author, keywords);
            int count = 0;
            foreach (Media media in actual)
            {
                count++;
            }

            Assert.IsTrue(count == mockAWSE.NumberOfBooks);
        }
    }
}
