namespace Sweng500.Pml.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.DataAccessLayer;
    
    /// <summary>
    /// This is a test class for SearchMediaTest and is intended
    /// to contain all SearchMediaTest Unit Tests that do not depend on web services
    /// </summary>
    [TestClass]
    public class SearchMediaServiceTest
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
        /// <summary>
        /// Cleans the database
        /// </summary>
        /// <param name="testContext">Unused by this method</param>
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext = null)
        {
        }

        /// <summary>
        /// Cleans the database after each test
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
        }

        /// <summary>
        /// TestInitialize to run code before running each test
        /// </summary>
        [TestInitialize]
        public void MyTestInitialize()
        {
        }
        
       #endregion

        /// <summary>
        /// A test for SearchMediaService Constructor
        /// </summary>
        [TestMethod]
        public void SearchMediaServiceConstructorTest()
        {
            // Nothing really to do right now
        }

        /// <summary>
        /// A test for GetBorrowedMediaItems
        /// </summary>
        [TestMethod]
        public void GetBorrowedMediaItemsTest()
        {
            SearchMediaService target = new SearchMediaService();
           try
            {
                target.GetBorrowedMediaItems();
            }
            catch (NotImplementedException)
            {
                return;
            }

            Assert.Fail("should have received exception");
        }

        /// <summary>
        /// A test for GetMediaItemsContaining
        /// </summary>
        [TestMethod]
        public void GetMediaItemsContaining_EmptyTest()
        {
            SearchMediaService searchmedia = new SearchMediaService(); // TODO: Initialize to an appropriate value
            string partialTitle = string.Empty;
            Mock.MockCrudService mockCrud = new Mock.MockCrudService();
            mockCrud.AllowVideos = false;
            mockCrud.NumberOfVideos = 0;
            mockCrud.AllowBooks = false;
            mockCrud.NumberOfBooks = 0;
            mockCrud.MatchingTitle = partialTitle;
            searchmedia.MockCrudService = mockCrud;
            IEnumerable<Media> actual = searchmedia.GetMediaItemsContaining(partialTitle);
            int bookcount = 0;
            int videocount = 0;
            foreach (Media item in actual)
            {
                if (item is Book)
                {
                    bookcount++;
                }

                if (item is Video)
                {
                    videocount++;
                }
            }

            Assert.IsTrue(bookcount == mockCrud.NumberOfBooks);
            Assert.IsTrue(videocount == mockCrud.NumberOfVideos);
        }

        /// <summary>
        /// A test for GetMediaItemsContaining
        /// </summary>
        [TestMethod]
        public void GetMediaItemsContainingTest()
        {
            SearchMediaService searchmedia = new SearchMediaService();
            string partialTitle = "tHe"; 

            IEnumerable<Media> actual;

            Mock.MockCrudService mockCrud = new Mock.MockCrudService();
            mockCrud.AllowVideos = true;
            mockCrud.NumberOfVideos = 1;
            mockCrud.AllowBooks = true;
            mockCrud.NumberOfBooks = 2;
            mockCrud.MatchingTitle = partialTitle;
            searchmedia.MockCrudService = mockCrud;
            actual = searchmedia.GetMediaItemsContaining(partialTitle);
            int bookcount = 0;
            int videocount = 0;
            foreach (Media item in actual)
            {
                if (item is Book)
                {
                    bookcount++;
                }

                if (item is Video)
                {
                    videocount++;
                }
            }

            Assert.IsTrue(bookcount == mockCrud.NumberOfBooks);
            Assert.IsTrue(videocount == mockCrud.NumberOfVideos);
        }
    }
}
