namespace Sweng500.Pml.Test.IntTest
{
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class IntTest1
    {
        private TestContext testContextInstance;

        /// <summary>
        /// a blank constructor
        /// </summary>
        public IntTest1()
        {
        }

        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }
            set
            {
               this.testContextInstance = value;
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
        public void TestMethod1()
        {
            var crud = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

            Author book1author = null;
            var authors = crud.GetAuthors();
            foreach (Author anauthor in authors)
            {
                book1author = anauthor;
            }


            Person authortest = new Author();
            authortest.FirstName = "test";
            authortest.LastName = "March11A_lastname";
            crud.Add(authortest);


           
            Book book1 = new Book();
            book1.Title = "March 11A First Book";
            book1.ISBN = "2";
            book1.AddPerson(book1author);
            crud.Add(book1);

            Book book2 = new Book();
            book2.Title = "March 10A second Book";
            book2.ISBN = "99903002";
            crud.Add(book2);

            var mediaitems = crud.GetMediaItems();
           authors = crud.GetAuthors();
            var persons = crud.GetPeople();

            foreach (Person person in persons)
            {
                string first = person.FirstName;
                string last = person.LastName;
                int id = person.PersonId;
                Console.WriteLine(person.ToString());
            }

            foreach (Author author in authors)
            {
                string firsta = author.FirstName;
                string lasta = author.LastName;
                int ida = author.PersonId;
            }

            foreach (Media item in mediaitems)
            {
                string title = item.Title;
                if (item is Book)
                {
                    Book book = (Book)item;
                    var bookauthors = book.Authors;
                    foreach (Author anauthor in bookauthors)
                    {
                        string name = anauthor.FirstName + " " + anauthor.LastName;
                        System.Console.WriteLine("The author name is {0}", name);
                    }
                    
                    var associations = book.AuthorBookAssociations;
                    foreach (AuthorBookAssociation association in associations)
                    {
                        int bookid = association.BookMediaId;
                        int personid = association.AuthorPersonId;
                        Author author = association.Author;

                        string last = association.Author.LastName;
                   }
                    Console.WriteLine("book title isbn {0} {1} ", title, book.ISBN);
                }
            }

            string desc = "update My description March10A";
            book2.Description = "update My description March10A";
            Media updatedItem = crud.Update(book2);

            var updatedItems2 = crud.GetMediaItems();
            int count = 0;
            foreach (Media item in updatedItems2)
            {
                if (book2.Equals(item))
                {
                    Assert.IsTrue(item.Description.Equals(desc));
                }
                count++;
            }

            crud.Delete(book2);
            int count2 = 0;
            var updateditem3 = crud.GetMediaItems();
            foreach (Media item in updateditem3)
            {
                count2++;
            }

            Assert.IsTrue(count - count2 == 1);
            crud.Delete(book1);

            var updatedItems = crud.GetMediaItems();
            foreach (Media uitems in updatedItems)
            {
                string myString = uitems.ToString();
            }
        }
    }
}
