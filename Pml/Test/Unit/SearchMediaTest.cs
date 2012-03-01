namespace Sweng500.Pml.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.DataAccessLayer;
    
    /// <summary>
    /// This is a test class for SearchMediaTest and is intended
    /// to contain all SearchMediaTest Unit Tests
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
            Utilities.CleanDatabase();
        }

        /// <summary>
        /// Cleans the database after each test
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            Utilities.CleanDatabase();
        }

        /// <summary>
        /// TestInitialize to run code before running each test
        /// </summary>
        [TestInitialize]
        public void MyTestInitialize()
        {
            Utilities.CleanDatabase();
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
            SearchMediaService target = new SearchMediaService(); // TODO: Initialize to an appropriate value
            string partialTitle = string.Empty; // TODO: Initialize to an appropriate value
//            IEnumerable<Media> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Media> actual;
            actual = target.GetMediaItemsContaining(partialTitle);
            int count = 0;
            foreach (Media item in actual)
            {
                count++;
            }

            Assert.IsTrue(0 == count);
        }

        /// <summary>
        /// A test for GetMediaItemsContaining
        /// </summary>
        [TestMethod]
        public void GetMediaItemsContainingTest()
        {
            var service = new EntityCrudService();

            SearchMediaService searchmedia = new SearchMediaService();
            string partialTitle = "tHe"; 
            IList<Media> originalMedia = new List<Media>();
            foreach (var media in Mock.MediaObjectMother.CreateNewBooks())
            {
                var bookAdded = service.Add(media);
                originalMedia.Add(bookAdded);
            }

            foreach (var media in Mock.MediaObjectMother.CreateNewVideos())
            {
                var videoAdded = service.Add(media);
                originalMedia.Add(videoAdded);
            }

            IEnumerable<Media> actual;
            actual = searchmedia.GetMediaItemsContaining(partialTitle);
            int count = 0;
            foreach (Media item in actual)
            {
                Assert.IsTrue(item.Title.ToLowerInvariant().Contains(partialTitle.ToLowerInvariant()));
                count++;
            }

            Assert.IsTrue(1 == count);
        }
    }
}
