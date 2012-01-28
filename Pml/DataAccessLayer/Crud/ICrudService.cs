namespace Sweng500.Pml.DataAccessLayer
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides the interface for the CRUD functionality
    /// </summary>
    public interface ICrudService
    {
        /// <summary>
        /// Gets the list of books in the inventory
        /// </summary>
        /// <returns>The list of books</returns>
        IEnumerable<Book> GetBooks();

        /// <summary>
        /// Updates the provided book
        /// </summary>
        /// <param name="book">The updated book</param>
        void Update(Book book);

        /// <summary>
        /// Adds the provided book
        /// </summary>
        /// <param name="book">The book to add</param>
        void Add(Book book);

        /// <summary>
        /// Deletes the provided book
        /// </summary>
        /// <param name="book">The book to delete</param>
        void Delete(Book book);
    }
}
