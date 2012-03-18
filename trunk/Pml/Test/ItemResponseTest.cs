namespace Sweng500.Pml.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Awse.CommerceService;

    /// <summary>
    /// This is a test class for ItemResponseTest and is intended
    /// to contain all ItemResponseTest Unit Tests
    /// </summary>
    [TestClass]
    public class ItemResponseTest
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
        /// A test for ItemResponse Constructor
        /// </summary>
        [TestMethod]
        public void ItemResponseConstructorTest()
        {
            ItemResponse target = new ItemResponse();
            Assert.IsNotNull(target);
        }

        /// <summary>
        /// A test for Authorsname
        /// </summary>
        [TestMethod]
        public void AuthorsnameTest()
        {
            ItemResponse target = new ItemResponse(); 
            ItemName name1 = new ItemName("Bob Neudecker");
            ItemName name2 = new ItemName("Kevin Daub");
            ItemName name3 = new ItemName("Albert Cavaliere");

            ItemName[] expected = new ItemName[] { name1, name2, name3 };
            ItemName[] actual;
            target.Authorsname = expected;
            actual = target.Authorsname;
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Directorsname
        /// </summary>
        [TestMethod]
        public void DirectorsnameTest()
        {
            ItemResponse target = new ItemResponse();
            ItemName name1 = new ItemName("Bob Neudecker");
            ItemName name2 = new ItemName("Kevin Daub");
            ItemName name3 = new ItemName("Albert Cavaliere");

            ItemName[] expected = new ItemName[] { name2, name3, name1 };
            ItemName[] actual;
            target.Directorsname = expected;
            actual = target.Directorsname;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Eisbn
        /// </summary>
        [TestMethod]
        public void EisbnTest()
        {
            ItemResponse target = new ItemResponse();
            string[] expected = { "1234567890123", "8872737" }; 
            string[] actual;
            target.Eisbn = expected;
            actual = target.Eisbn;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Genre
        /// </summary>
        [TestMethod]
        public void GenreTest()
        {
            ItemResponse target = new ItemResponse(); 
            string expected = "Horror"; 
            string actual;
            target.Genre = expected;
            actual = target.Genre;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Formats
        /// </summary>
        [TestMethod]
        public void FormatsTest()
        {
            ItemResponse target = new ItemResponse(); 
            string[] expected = { "NCTS", "BlueRay", "F1" }; 
            string[] actual;
            target.Formats = expected;
            actual = target.Formats;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Imageurl
        /// </summary>
        [TestMethod]
        public void ImageurlTest()
        {
            ItemResponse target = new ItemResponse();
            string expected = "http://fakeimage"; 
            string actual;
            target.Imageurl = expected;
            actual = target.Imageurl;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Isbn
        /// </summary>
        [TestMethod]
        public void IsbnTest()
        {
            ItemResponse target = new ItemResponse(); 
            string expected = "9393848474723";
            string actual;
            target.Isbn = expected;
            actual = target.Isbn;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Producersname
        /// </summary>
        [TestMethod]
        public void ProducersnameTest()
        {
            ItemResponse target = new ItemResponse();
            ItemName name1 = new ItemName("Bob Neudecker");
            ItemName name2 = new ItemName("Kevin Daub");
            ItemName name3 = new ItemName("Albert Cavaliere");

            ItemName[] expected = new ItemName[] { name3, name2, name1 };
            ItemName[] actual;
            target.Producersname = expected;
            actual = target.Producersname;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Productgroup
        /// </summary>
        [TestMethod]
        public void ProductgroupTest()
        {
            ItemResponse target = new ItemResponse(); 
            string expected = "Video"; 
            string actual;
            target.Productgroup = expected;
            actual = target.Productgroup;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Publicationdate
        /// </summary>
        [TestMethod]
        public void PublicationdateTest()
        {
            ItemResponse target = new ItemResponse(); 
            DateTime expected = new DateTime(2013, 3, 17); 
            DateTime actual;
            target.Publicationdate = expected;
            actual = target.Publicationdate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Publisher
        /// </summary>
        [TestMethod]
        public void PublisherTest()
        {
            ItemResponse target = new ItemResponse();
            string expected = "Random House Publishing";
            string actual;
            target.Publisher = expected;
            actual = target.Publisher;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Releasedate
        /// </summary>
        [TestMethod]
        public void ReleasedateTest()
        {
            ItemResponse target = new ItemResponse(); 
            DateTime expected = new DateTime(2000, 12, 31);
            DateTime actual;
            target.Releasedate = expected;
            actual = target.Releasedate;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Title
        /// </summary>
        [TestMethod]
        public void TitleTest()
        {
            ItemResponse target = new ItemResponse(); 
            string expected = "To Kill A Mockingbird"; 
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Upc
        /// </summary>
        [TestMethod]
        public void UpcTest()
        {
            ItemResponse target = new ItemResponse(); 
            string expected = "8826707019";
            string actual;
            target.Upc = expected;
            actual = target.Upc;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Description
        /// </summary>
        [TestMethod]
        public void DescriptionTest()
        {
            ItemResponse target = new ItemResponse();
            string expected = "<DIV><DIV>Now for the first time ever, J.K. Rowling’s seven bestselling Harry Potter books are available in a stunning paperback boxed set! The Harry Potter series has been hailed as “one for the ages” by Stephen King and “a spellbinding saga’ by USA Today. And most recently, <i>The New York Times</i> called <i>Harry Potter and the Deathly Hallows</i> the “fastest selling book in history.” This is the ultimate Harry Potter collection for Harry Potter fans of all ages!</DIV></div>";
            string actual;
            target.Description = expected;
            actual = target.Description;
            Assert.AreEqual(expected, actual);
        }
    }
}
