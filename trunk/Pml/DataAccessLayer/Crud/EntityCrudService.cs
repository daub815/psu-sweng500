namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of the ICrudService using the Entity Framework
    /// </summary>
    public class EntityCrudService : ICrudService
    {
        #region ICrudService

        /// <summary>
        /// Gets the list of books in the inventory
        /// </summary>
        /// <returns>The list of books</returns>
        public IEnumerable<Book> GetBooks()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the provided book
        /// </summary>
        /// <param name="book">The updated book</param>
        public void Update(Book book)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the provided book
        /// </summary>
        /// <param name="book">The book to add</param>
        public void Add(Book book)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the provided book
        /// </summary>
        /// <param name="book">The book to delete</param>
        public void Delete(Book book)
        {
            throw new NotImplementedException();
        }

        #endregion ICrudService
    }
}
