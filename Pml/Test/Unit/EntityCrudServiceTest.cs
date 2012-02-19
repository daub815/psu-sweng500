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
        /// A test for Add
        /// </summary>
        [TestMethod]
        public void AddTest()
        {
            var originalBooks = Mock.MediaObjectMother.CreateNewBooks();
            var booksToAdd = new List<Book>(originalBooks);

            var originalVideos = Mock.MediaObjectMother.CreateNewVideos();

            // Create a set of books (original and one to add), but both will be the same
            var testBooks = originalBooks.Zip(booksToAdd, (a, b) => new { Original = a, ToAdd = b });            

            EntityCrudService service = new EntityCrudService();
            foreach (var testBook in testBooks)
            {
                // Match the datetimes since we created a new instance
                testBook.ToAdd.Acquired = testBook.Original.Acquired;

                // Add the book
                var addedBook = service.Add(testBook.ToAdd);
                
                // Verify the book content
                Assert.IsNotNull(addedBook);
                Assert.IsNotNull(testBook.Original);
                Assert.IsTrue(testBook.Original.MediaID != addedBook.MediaID);
                Assert.IsTrue(testBook.Original.Title == addedBook.Title);

                Assert.IsTrue(testBook.Original.Acquired == addedBook.Acquired);
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

            // Delete the books
            for (int i = expected.Count - 1; i >= 0; --i)
            {
                // Get the book
                var bookToDelete = expected[i];

                // Remove the book
                expected.RemoveAt(i);
                service.Delete(bookToDelete);

                // Check if the book is there
                var mediaAfterDelete = service.GetMediaItems();
                Assert.IsTrue(mediaAfterDelete.Count() == expected.Count);
                Assert.IsFalse(mediaAfterDelete.Contains(bookToDelete));
            }
        }

        /// <summary>
        /// A test for GetBooks
        /// </summary>
        [TestMethod]
        public void GetBooksTest()
        {
            var service = new EntityCrudService();

            // Add the list of books to get
            var expected = new List<Book>();
            foreach (var book in Mock.BookObjectMother.CreateNewBooks())
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
            Assert.IsTrue(actual.Intersect(expected).Count() == expected.Count);
            
            // They should not match
            expected.Add(new Book());
            Assert.IsFalse(actual.Intersect(expected).Count() == expected.Count);
        }

        /// <summary>
        /// A test for Update
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            var service = new EntityCrudService();

            // Add the list of books to get
            var expectedList = new List<Book>();
            foreach (var book in Mock.BookObjectMother.CreateNewBooks())
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
                book.Price = new decimal(4.95);
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
    }
}
