namespace Sweng500.Pml.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Awse.CommerceService;

    /// <summary>
    /// This is a test class for ItemNameTest and is intended
    /// to contain all ItemNameTest Unit Tests
    /// </summary>
    [TestClass]
    public class ItemNameTest
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
 
        #endregion

        /// <summary>
        /// A test for ItemName Constructor
        /// </summary>
        [TestMethod]
        public void ItemNameConstructorTestEmpty()
        {
            string namestring = string.Empty; 
            ItemName target = new ItemName(namestring);
            Assert.AreEqual(string.Empty, target.First);
            Assert.AreEqual(string.Empty, target.Last);
        }

        /// <summary>
        /// A test for ItemName Constructor
        /// </summary>
        [TestMethod]
        public void ItemNameConstructorTestSingleName()
        {
            string namestring = "Neudecker";
            ItemName target = new ItemName(namestring);
            Assert.AreEqual(string.Empty, target.First);
            Assert.AreEqual(namestring, target.Last);
        }

        /// <summary>
        /// A test for ItemName Constructor
        /// </summary>
        [TestMethod]
        public void ItemNameConstructorTestTwoName()
        {
            string namestring = "Robert Neudecker";
            ItemName target = new ItemName(namestring);
            Assert.AreEqual("Robert", target.First);
            Assert.AreEqual("Neudecker", target.Last);
        }

        /// <summary>
        /// A test for ItemName Constructor
        /// </summary>
        [TestMethod]
        public void ItemNameConstructorTesThreeName()
        {
            string namestring = "Robert E Neudecker";
            ItemName target = new ItemName(namestring);
            Assert.AreEqual("Robert", target.First);
            Assert.AreEqual("Neudecker", target.Last);
        }

        /// <summary>
        /// A test for First
        /// </summary>
        [TestMethod]
        public void FirstTest()
        {
            ItemName target = new ItemName("J Jakes"); 
            string expected = "John"; 
            string actual;
            target.First = "John";
            actual = target.First;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Last
        /// </summary>
        [TestMethod]
        public void LastTest()
        {
            string namestring = "Janet Evonovich";
            ItemName target = new ItemName(namestring); 
            string expected = "Charles"; 
            string actual;
            target.Last = expected;
            actual = target.Last;
            Assert.AreEqual(expected, actual);
        }
    }
}
