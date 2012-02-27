namespace Sweng500.Pml.Test.Unit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// This is a test class for EntityCrudServiceTest and is intended
    /// to contain all EntityCrudServiceTest Unit Tests
    /// </summary>
    [TestClass]
    public class EntityCrudServiceTest
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

        /// <summary>
        /// Cleans the database
        /// </summary>
        /// <param name="testContext">Unused by this method</param>
        [ClassInitialize]
        public static void CleanDatabase(TestContext testContext = null)
        {
            MasterEntities context = null;

            try
            {
                context = new MasterEntities();

                foreach (var media in context.Media)
                {
                    context.Media.DeleteObject(media);
                    context.SaveChanges();
                }  
            }
            catch (Exception e)
            {
                string message = "Unable to clear the database after test.";
                message = message + "Received Exception: " + e.Message;

                Assert.Fail(message);
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }
        }

        /// <summary>
        /// Cleans the database after each test
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            CleanDatabase();
        }

        /// <summary>
        /// A test for EntityCrudService Constructor
        /// </summary>
        [TestMethod]
        public void EntityCrudServiceConstructorTest()
        {
            // Nothing really to do right now
        }

        /// <summary>
        /// A test for Add of a book
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            var originalBooks = Mock.MediaObjectMother.CreateNewBooks();
            var booksToAdd = Mock.MediaObjectMother.CreateNewBooks();

            // Create a set of books (original and one to add), but both will be the same
            var testBooks = originalBooks.Zip(booksToAdd, (a, b) => new { Original = a, ToAdd = b });            

            EntityCrudService service = new EntityCrudService();
            foreach (var testBook in testBooks)
            {
                // Match the datetimes since we created a new instance
                testBook.ToAdd.Acquired = testBook.Original.Acquired;

                // Add the book
                Book addedBook = (Book)service.Add(testBook.ToAdd);
                
                // Verify the book content
                Assert.IsNotNull(addedBook);
                Assert.IsNotNull(testBook.Original);
////                Assert.IsTrue(testBook.Original.BookID == 0);
////                Assert.IsTrue(testBook.Original.BookID != addedBook.BookID);
                Assert.IsTrue(testBook.Original.MediaId == 0);
                Assert.IsTrue(testBook.Original.MediaId != addedBook.MediaId);
                Assert.IsTrue(testBook.Original.Title == addedBook.Title);

                Assert.IsTrue(testBook.Original.Acquired == addedBook.Acquired);
            }
        }

        /// <summary>
        /// A test for Add of a video
        /// </summary>
        [TestMethod]
        public void AddVideoTest()
        {
            var originalVideos = Mock.MediaObjectMother.CreateNewVideos();
            var videosToAdd = Mock.MediaObjectMother.CreateNewVideos();

            // Create a set of videos (original and one to add), but both will be the same
            var testVideos = originalVideos.Zip(videosToAdd, (a, b) => new { Original = a, ToAdd = b });

            EntityCrudService service = new EntityCrudService();
            foreach (var testMedia in testVideos)
            {
                //// Add the video
                Video addedMedia = (Video)service.Add(testMedia.ToAdd);

                //// Verify the book content
                Assert.IsNotNull(addedMedia);
                Assert.IsNotNull(testMedia.Original);
////                Assert.IsTrue(testMedia.Original.VideoID == 0);
////                Assert.IsTrue(testMedia.Original.VideoID != addedMedia.VideoID);
                Assert.IsTrue(testMedia.Original.MediaId == 0);
                Assert.IsTrue(testMedia.Original.MediaId != addedMedia.MediaId);
                Assert.IsTrue(testMedia.Original.Title == addedMedia.Title);

                Assert.IsTrue(testMedia.Original.Acquired == addedMedia.Acquired);
            }
        }

        /// <summary>
        /// A test for Delete
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            var service = new EntityCrudService();

            // Add the list of books to get
            var expected = new List<Book>();
            foreach (var book in Mock.MediaObjectMother.CreateNewBooks())
            {
                var bookAdded = service.Add(book);
                Assert.IsNotNull(bookAdded);

                expected.Add((Book)bookAdded);
            }

            var mediaItems = service.GetMediaItems();
            int mediaCount = 0;
            foreach (var item in mediaItems)
            {
                mediaCount++;
            }

            Assert.IsTrue(mediaCount == expected.Count);

            // Delete the books
            foreach (Book item in expected)
            {
                service.Delete(item);
            }

            IEnumerable<Media> items = service.GetMediaItems();
            Assert.IsTrue(items.Count() == 0);
        }

        /// <summary>
        /// A test for GetMediaItems that is specific to books
        /// </summary>
        [TestMethod]
        public void GetBooksTest()
        {
            int bookCount = 0;
            int otherCount = 0;
            var service = new EntityCrudService();

            // Add the list of books to get
            var expected = new List<Book>();
            foreach (var book in Mock.MediaObjectMother.CreateNewBooks())
            {
                var bookAdded = service.Add(book);
                Assert.IsNotNull(bookAdded);
                if (bookAdded is Book)
                {
                    expected.Add((Book)bookAdded);
                }
            }

            // They should match
            var actual = service.GetMediaItems();
            foreach (var item in actual)
            {
                if (item is Book)
                {
                    bookCount++;
                }
                else
                {
                    otherCount++;
                }
            }

            Assert.IsTrue(bookCount == expected.Count);
        }

        /// <summary>
        /// A test for GetMediaItems specific to testing videos
        /// </summary>
        [TestMethod]
        public void GetVideosTest()
        {
            var service = new EntityCrudService();

            // Add the list of books to get
            var expected = new List<Video>();
            int videoCount = 0;
            int otherCount = 0;

            foreach (var item in Mock.MediaObjectMother.CreateNewVideos())
            {
                var itemAdded = service.Add(item);
                Assert.IsNotNull(itemAdded);
                if (itemAdded is Video)
                {
                    expected.Add((Video)itemAdded);
                }
            }

            // They should match
            var actual = service.GetMediaItems();
            foreach (var item in actual)
            {
                if (item is Video)
                {
                    videoCount++;
                }
                else
                {
                    otherCount++;
                }
            } 
         
            Assert.IsTrue(videoCount == expected.Count);
        }

        /// <summary>
        /// A test for Update
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            var service = new EntityCrudService();
            int formatId = 0;
            IDictionary<int, string> formats = service.GetGenres();
            ICollection<int> formatKeys = formats.Keys;
            if (formatKeys != null)
            {
                formatId = formatKeys.First();
            }

            // Add the list of books to get
            var expectedList = new List<Book>();
            foreach (var book in Mock.MediaObjectMother.CreateNewBooks())
            {
                var bookAdded = service.Add(book);
                Assert.IsNotNull(bookAdded);

                expectedList.Add((Book)bookAdded);
            }

            // Update each of the books
            var actualList = new List<Book>();
            foreach (var book in expectedList)
            {
                // Update the price
                book.FormatId = formatId;
                var updatedItem = service.Update(book);
                Assert.IsNotNull(updatedItem);
                Assert.IsTrue(book.Equals(updatedItem));

                // Update Title
                updatedItem.Title = "An updated title";
                var updatedTitle = service.Update(updatedItem);
                Assert.IsNotNull(updatedItem);
                Assert.IsTrue(book.Equals(updatedItem));

                actualList.Add((Book)updatedItem);
            }

            // Verify the changes are stored
            var updatedList = service.GetMediaItems();
            var zippedList = updatedList.Zip(actualList, (u, a) => new { FromService = u, TestUpdate = a });
            foreach (var entry in zippedList)
            {
                Assert.IsTrue(entry.FromService.Equals(entry.TestUpdate));
                Assert.IsTrue(entry.FromService.Title == entry.TestUpdate.Title);
            }
        }

        /// <summary>
        /// A test for GetGenre
        /// </summary>
        [TestMethod]
        public void GetCodes()
        {
            this.PopulateCodes();

            var service = new EntityCrudService();
            IDictionary<int, string> genres = service.GetGenres();
            Assert.IsTrue(genres.Count == 2);
            ICollection<string> descriptions = genres.Values;
            Assert.IsTrue(descriptions.Contains("test 1"));
            Assert.IsTrue(descriptions.Contains("test 2"));

            IDictionary<int, string> formats = service.GetFormats();
            Assert.IsTrue(formats.Count == 3);
            ICollection<string> formatDescriptions = formats.Values;
            Assert.IsTrue(formatDescriptions.Contains("test format 1"));
            Assert.IsTrue(formatDescriptions.Contains("test format 2"));

            IDictionary<int, string> ratings = service.GetBoardRatings();
            Assert.IsTrue(ratings.Count == 4);
            ICollection<string> ratingDescriptions = ratings.Values;
            Assert.IsTrue(ratingDescriptions.Contains("R- Restricted"));
        }

        /// <summary>
        /// populates codeTypes and codes in database by clearing them and adding
        /// </summary>
        public void PopulateCodes()
        {
            var context = new MasterEntities();

            foreach (var code in context.Codes)
            {
                context.Codes.DeleteObject(code);
            }

            foreach (var code in context.CodeTypes)
            {
                context.CodeTypes.DeleteObject(code);
            }

            context.SaveChanges();

            CodeType genreType = new CodeType();
            genreType.Type = "Genres";
            context.CodeTypes.AddObject(genreType);

            CodeType formatType = new CodeType();
            formatType.Type = "Formats";
            context.CodeTypes.AddObject(formatType);

            CodeType ratingType = new CodeType();
            ratingType.Type = "MPAA Board Ratings";
            context.CodeTypes.AddObject(ratingType);
            context.SaveChanges();

                Code genre1 = new Code();
                genre1.CodeTypeId = genreType.CodeTypeId;
                genre1.CodeDescription = "test 1";

                context.Codes.AddObject(genre1);

                Code genre2 = new Code();
                genre2.CodeTypeId = genreType.CodeTypeId;
                genre2.CodeDescription = "test 2";
                context.Codes.AddObject(genre2);

                Code format1 = new Code();
                format1.CodeTypeId = formatType.CodeTypeId;
                format1.CodeDescription = "test format 1";
                context.Codes.AddObject(format1);

                Code format2 = new Code();
                format2.CodeTypeId = formatType.CodeTypeId;
                format2.CodeDescription = "test format 2";
                context.Codes.AddObject(format2);

                Code format3 = new Code();
                format3.CodeTypeId = formatType.CodeTypeId;
                format3.CodeDescription = "test format 3";
                context.Codes.AddObject(format3);

                Code boardRatingG = new Code();
                boardRatingG.CodeTypeId = ratingType.CodeTypeId;
                boardRatingG.CodeDescription = "G- General Audiences";
                context.Codes.AddObject(boardRatingG);

                Code boardRatingPG = new Code();
                boardRatingPG.CodeTypeId = ratingType.CodeTypeId;
                boardRatingPG.CodeDescription = "PG- Parental Guidance Suggested";
                context.Codes.AddObject(boardRatingPG);

                Code boardRatingPG13 = new Code();
                boardRatingPG13.CodeTypeId = ratingType.CodeTypeId;
                boardRatingPG13.CodeDescription = "PG-13- Parents Strongly Cautioned";
                context.Codes.AddObject(boardRatingPG13);

                Code boardRatingR = new Code();
                boardRatingR.CodeTypeId = ratingType.CodeTypeId;
                boardRatingR.CodeDescription = "R- Restricted";
                context.Codes.AddObject(boardRatingR);

                context.SaveChanges();
        }
    }
}
