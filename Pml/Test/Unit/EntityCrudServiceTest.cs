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
        /// Cleans the database after each test
        /// </summary>
        [TestCleanup]
        public void CleanDatabase()
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
            EntityCrudService target = new EntityCrudService(); // TODO: Initialize to an appropriate value
            Book book = null; // TODO: Initialize to an appropriate value
            target.Delete(book);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        /// A test for GetBooks
        /// </summary>
        [TestMethod]
        public void GetBooksTest()
        {
            EntityCrudService target = new EntityCrudService(); // TODO: Initialize to an appropriate value
            IEnumerable<Book> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Book> actual;
            actual = target.GetBooks();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        /// A test for Update
        /// </summary>
        [TestMethod]
        public void UpdateTest()
        {
            EntityCrudService target = new EntityCrudService(); // TODO: Initialize to an appropriate value
            Book book = null; // TODO: Initialize to an appropriate value
            target.Update(book);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
