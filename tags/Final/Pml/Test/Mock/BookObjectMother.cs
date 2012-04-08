namespace Sweng500.Pml.Test.Mock
{
    using System.Collections.Generic;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// The mother of all book objects for the unit tests
    /// </summary>
    public static class BookObjectMother
    {
        /// <summary>
        /// Returns a list of created books without an id
        /// </summary>
        /// <returns>A list of new books</returns>
        public static IEnumerable<Book> CreateNewBooks()
        {
            yield return new Book()
            {
            };

            yield return new Book()
            {
            };
        }
    }
}
