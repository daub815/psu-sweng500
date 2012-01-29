﻿namespace Sweng500.Pml.Test.Unit
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

                foreach (var book in context.Books)
                {
                    context.Books.DeleteObject(book);
                }

                context.SaveChanges();
            }
            catch (Exception)
            {
                Assert.Fail("Unable to clear the database after test.");
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
            var originalBooks = Mock.BookObjectMother.CreateNewBooks();
            var booksToAdd = new List<Book>(originalBooks);

            // Create a set of books (original and one to add), but both will be the same
            var testBooks = originalBooks.Zip(booksToAdd, (a, b) => new { Original = a, ToAdd = b });            

            EntityCrudService service = new EntityCrudService();
            foreach (var testBook in testBooks)
            {
                // Match the datetimes since we created a new instance
                testBook.ToAdd.DateAdded = testBook.Original.DateAdded;

                // Add the book
                var addedBook = service.Add(testBook.ToAdd);
                
                // Verify the book content
                Assert.IsNotNull(addedBook);
                Assert.IsNotNull(testBook.Original);
                Assert.IsTrue(testBook.Original.Id != addedBook.Id);
                Assert.IsTrue(testBook.Original.Title == addedBook.Title);
                Assert.IsTrue(testBook.Original.Author == addedBook.Author);
                Assert.IsTrue(testBook.Original.DateAdded == addedBook.DateAdded);
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
            foreach (var book in Mock.BookObjectMother.CreateNewBooks())
            {
                var bookAdded = service.Add(book);
                Assert.IsNotNull(bookAdded);

                expected.Add(bookAdded);
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
                var booksAfterDelete = service.GetBooks();
                Assert.IsTrue(booksAfterDelete.Count() == expected.Count);
                Assert.IsFalse(booksAfterDelete.Contains(bookToDelete));
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

                expected.Add(bookAdded);
            }

            // They should match
            var actual = service.GetBooks();
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

                expectedList.Add(bookAdded);
            }

            // Update each of the books
            var actualList = new List<Book>();
            foreach (var book in expectedList)
            {
                // Update the author
                book.Author = Guid.NewGuid().ToString();
                var updatedAuthor = service.Update(book);
                Assert.IsNotNull(updatedAuthor);
                Assert.IsTrue(book.Equals(updatedAuthor));

                // Update Title
                updatedAuthor.Title = Guid.NewGuid().ToString();
                var updatedTitle = service.Update(updatedAuthor);
                Assert.IsNotNull(updatedAuthor);
                Assert.IsTrue(book.Equals(updatedAuthor));

                actualList.Add(updatedAuthor);
            }

            // Verify the changes are stored
            var updatedList = service.GetBooks();
            var zippedList = updatedList.Zip(actualList, (u, a) => new { FromService = u, TestUpdate = a });
            foreach (var entry in zippedList)
            {
                Assert.IsTrue(entry.FromService.Equals(entry.TestUpdate));
                Assert.IsTrue(entry.FromService.Author == entry.TestUpdate.Author);
                Assert.IsTrue(entry.FromService.Title == entry.TestUpdate.Title);
            }
        }
    }
}
