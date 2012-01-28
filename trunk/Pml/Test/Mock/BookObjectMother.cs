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
                Title = "A Book Title",
                Author = "Kevin Daub"
            };

            yield return new Book()
            {
                Title = "A Book About Nothing",
                Author = "John Smith"
            };
        }

        /// <summary>
        /// Gets a list of books that have an id starting at zero until the number of books minus one
        /// </summary>
        /// <returns>The list of created books with identifiers</returns>
        public static IEnumerable<Book> GetCreatedBooks()
        {
            var books = CreateNewBooks();

            int startingNum = 0;
            foreach (var book in books)
            {
                book.Id = startingNum++;
            }

            return books;
        }
    }
}
