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
        /// A test for EntityCrudService Constructor
        /// </summary>
        [TestMethod]
        public void EntityCrudServiceConstructorTest()
        {
            // Nothing really to do right now
        }

        /// <summary>
        /// A test for Add of a video and book and then delete of the video and book
        /// Uses GetMediaItems and looks for expected value
        /// </summary>
        [TestMethod]
        public void AddUpdateDeleteMediaTest()
        {
            DateTime testdt = DateTime.Now;
            int month = testdt.Month;
            int dayOfYear = testdt.DayOfYear;
            int hour = testdt.Hour;
            int minute = testdt.Minute;
            int second = testdt.Second;
            int ms = testdt.Millisecond;
            string bookTitle = string.Format("book test {0} {1} {2} {3} {4}", dayOfYear, hour, minute, second, ms);
            string videoTitle = string.Format("video test {0} {1} {2} {3} {4}", dayOfYear, hour, minute, second, ms);
            string bookComment = "Lets update the comment for the book";
            string videoComment = "Lets update the comment for the video";

            int bookMediaId = 0;
            int videoMediaId = 0;
            Book bookToAdd = Mock.MediaObjectMother.CreateBook(bookTitle);
            Video videoToAdd = Mock.MediaObjectMother.CreateVideo(videoTitle);
            EntityCrudService service = new EntityCrudService();
            //// Add the book
            Book addedBook = (Book)service.Add(bookToAdd);
            Assert.IsTrue(addedBook.MediaId != 0);
            bookMediaId = addedBook.MediaId;

            //// Add the video
            Video addedVideo = (Video)service.Add(videoToAdd);
            Assert.IsTrue(addedVideo.MediaId != 0);
            videoMediaId = addedVideo.MediaId;

            var mediaitems = service.GetMediaItems();
            bool bookFound = false;
            bool videoFound = false;
            foreach (Media item in mediaitems)
            {
                if (item is Book)
                {
                    if (item.MediaId == bookMediaId
                        && item.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        bookFound = true;
                    }
                }

                if (item is Video)
                {
                    if (item.MediaId == videoMediaId
                        && item.Title.Equals(videoTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        videoFound = true;
                    }
                }
            }

            Assert.IsTrue(bookFound);
            Assert.IsTrue(videoFound);
            //// update
            addedBook.Comment = bookComment;
            service.Update(addedBook);

            addedVideo.Comment = videoComment;
            service.Update(addedVideo);

            //// verify the comment is updated
            mediaitems = service.GetMediaItems();
            bookFound = false;
            videoFound = false;
            foreach (Media item in mediaitems)
            {
                if (item is Book)
                {
                    if (item.MediaId == bookMediaId
                        && item.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase)
                        && item.Comment.Equals(bookComment))
                    {
                        bookFound = true;
                    }
                }

                if (item is Video)
                {
                    if (item.MediaId == videoMediaId
                        && item.Title.Equals(videoTitle, StringComparison.OrdinalIgnoreCase)
                        && item.Comment.Equals(videoComment))
                    {
                        videoFound = true;
                    }
                }
            }

            //// delete
            service.Delete(addedBook);
            service.Delete(addedVideo);

            //// verify the items are not found
            mediaitems = service.GetMediaItems();
            bookFound = false;
            videoFound = false;
            foreach (Media item in mediaitems)
            {
                if (item is Book)
                {
                    if (item.MediaId == bookMediaId
                        && item.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        bookFound = true;
                    }
                }

                if (item is Video)
                {
                    if (item.MediaId == videoMediaId
                        && item.Title.Equals(videoTitle, StringComparison.OrdinalIgnoreCase))
                    {
                        videoFound = true;
                    }
                }
            }

                Assert.IsTrue(false == bookFound);
                Assert.IsTrue(false == videoFound);
        }

        /// <summary>
        /// A test for Add of a person and then update of the person
        /// Uses GetPeople and GetAuthors to look for expected value
        /// </summary>
        [TestMethod]
        public void AddUpdatePersonTest()
        {
            DateTime testdt = DateTime.Now;
            int month = testdt.Month;
            int dayOfYear = testdt.DayOfYear;
            int hour = testdt.Hour;
            int minute = testdt.Minute;
            int second = testdt.Second;
            int ms = testdt.Millisecond;
            string lastname = string.Format("person test {0} {1} {2} {3} {4}", dayOfYear, hour, minute, second, ms);
            int authorId = 0;
            string authorPostalCode = "19355";
            string directorPostalCode = "18969";
            int directorId = 0;
            Author authorTest = new Author();
            authorTest.FirstName = "author";
            authorTest.LastName = lastname;

            Director directorTest = new Director();
            directorTest.FirstName = "director";
            directorTest.LastName = lastname;

            EntityCrudService service = new EntityCrudService();
            //// Add the author
            Author addedAuthor = (Author)service.Add(authorTest);
            Assert.IsTrue(addedAuthor.PersonId != 0);
            authorId = addedAuthor.PersonId;

            //// Add the Director
            Director addedDirector = (Director)service.Add(directorTest);
            Assert.IsTrue(addedDirector.PersonId != 0);
            directorId = addedDirector.PersonId;

            var people = service.GetPeople();
            bool authorFound = false;
            bool directorFound = false;
            foreach (Person item in people)
            {
                if (item is Author)
                {
                    if (item.PersonId == authorId
                        && item.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase))
                    {
                        authorFound = true;
                    }
                }

                if (item is Director)
                {
                    if (item.PersonId == directorId
                        && item.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase))
                    {
                        directorFound = true;
                    }
                }
            }

            var authors = service.GetAuthors();

            authorFound = false;
            foreach (Author item in authors)
            {
                   if (item.PersonId == authorId
                        && item.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase))
                    {
                        authorFound = true;
                    }
            }

            Assert.IsTrue(authorFound);

            addedAuthor.PostalCode = authorPostalCode;
            addedDirector.PostalCode = directorPostalCode;
            addedAuthor = (Author)service.Update(addedAuthor);
            addedDirector = (Director)service.Update(addedDirector);

            authorFound = false;
            directorFound = false;
            foreach (Person item in people)
            {
                if (item is Author)
                {
                    if (item.PersonId == authorId
                        && item.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase))
                    {
                        if (item.PostalCode == authorPostalCode)
                        {
                            authorFound = true;
                        }
                    }
                }
                
                if (item is Director)
                {
                    if (item.PersonId == directorId
                        && item.LastName.Equals(lastname, StringComparison.OrdinalIgnoreCase))
                    {
                        if (item.PostalCode == directorPostalCode)
                        {
                            directorFound = true;
                        }
                    }
                }
            }

            Assert.IsTrue(authorFound);
            Assert.IsTrue(directorFound);
        }

        /// <summary>
        /// A test that an exception is thrown if null is passed
        /// </summary>
        [TestMethod]
        public void PersonExceptionTest()
        {
            bool continueOn = false;
            Person nobody = null;
            EntityCrudService service = new EntityCrudService();
            try 
            {
                service.Add(nobody);
                Assert.IsTrue(false, "should have received exception");
            }
            catch (ArgumentNullException)
            {
                continueOn = true;
            }

            if (continueOn)
            {
                try
                {
                    service.Update(nobody);
                }
                catch (ArgumentNullException)
                {
                    return;
                }
            }

            Assert.IsTrue(false, "should have received exception");
        }

        /// <summary>
        /// A test that an exception is thrown if null is passed
        /// </summary>
        [TestMethod]
        public void MediaExceptionTest()
        {
            bool doupdate = false;
            bool dodelete = false;
            Media nomedia = null;
            EntityCrudService service = new EntityCrudService();
            try
            {
                service.Add(nomedia);
                Assert.IsTrue(false, "should have received exception");
            }
            catch (ArgumentNullException)
            {
                doupdate = true;
            }

            if (doupdate)
            {
                try
                {
                    service.Update(nomedia);
                }
                catch (ArgumentNullException)
                {
                    dodelete = true;
                }
            }

            if (dodelete)
            {
                try
                {
                    service.Delete(nomedia);
                }
                catch (ArgumentNullException)
                {
                    return;
                }
            }

            Assert.IsTrue(false, "should have received exception");
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
            Assert.IsTrue(descriptions.Contains("test genre 1"));
            Assert.IsTrue(descriptions.Contains("test genre 2"));

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

            context.SaveChanges();

            Code genre1 = new Code();
            genre1.CodeTypeId = 1;
            genre1.CodeDescription = "test genre 1";

            context.Codes.AddObject(genre1);

            Code genre2 = new Code();
            genre2.CodeTypeId = 1;
            genre2.CodeDescription = "test genre 2";
            context.Codes.AddObject(genre2);

            Code format1 = new Code();
            format1.CodeTypeId = 2;
            format1.CodeDescription = "test format 1";
            context.Codes.AddObject(format1);

            Code format2 = new Code();
            format2.CodeTypeId = 2;
            format2.CodeDescription = "test format 2";
            context.Codes.AddObject(format2);

            Code format3 = new Code();
            format3.CodeTypeId = 2;
            format3.CodeDescription = "test format 3";
            context.Codes.AddObject(format3);

            Code boardRatingG = new Code();
            boardRatingG.CodeTypeId = 3;
            boardRatingG.CodeDescription = "G- General Audiences";
            context.Codes.AddObject(boardRatingG);

            Code boardRatingPG = new Code();
            boardRatingPG.CodeTypeId = 3;
            boardRatingPG.CodeDescription = "PG- Parental Guidance Suggested";
            context.Codes.AddObject(boardRatingPG);

            Code boardRatingPG13 = new Code();
            boardRatingPG13.CodeTypeId = 3;
            boardRatingPG13.CodeDescription = "PG-13- Parents Strongly Cautioned";
            context.Codes.AddObject(boardRatingPG13);

            Code boardRatingR = new Code();
            boardRatingR.CodeTypeId = 3;
            boardRatingR.CodeDescription = "R- Restricted";
            context.Codes.AddObject(boardRatingR);

            context.SaveChanges();
        }
    }
}
