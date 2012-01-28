namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using log4net;

    /// <summary>
    /// An implementation of the ICrudService using the Entity Framework
    /// </summary>
    public class EntityCrudService : ICrudService
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ICrudService

        /// <summary>
        /// Gets the list of books in the inventory
        /// </summary>
        /// <returns>The list of books</returns>
        public IEnumerable<Book> GetBooks()
        {
            IList<Book> rtn = new List<Book>();
            MasterEntities context = null;

            try
            {
                // The connection string is expected to be in the App.config
                context = new MasterEntities();

                // Detach all the books, so we can return them
                foreach (var book in context.Books)
                {
                    context.Detach(book);
                    rtn.Add(book);
                }
            }
            catch (Exception e)
            {
                log.Error("Unable to get the books", e);
                throw;
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }

            return rtn;
        }

        /// <summary>
        /// Updates the provided book
        /// </summary>
        /// <param name="book">The updated book</param>
        /// <returns>The updated book with an of the generated items from the service</returns>
        public Book Update(Book book)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the provided book
        /// </summary>
        /// <param name="book">The book to add</param>
        /// <returns>The added book with any of the generated items from the service</returns>
        /// <exception cref="System.ArgumentNullException">THe provided book was null</exception>
        public Book Add(Book book)
        {
            if (null == book)
            {
                log.Warn("The provided book was null.");
                throw new ArgumentNullException("Book cannot be null");
            }
            else if (null != book.EntityKey)
            {
                log.Warn("The provided book was already added. Use UpdateBook instead.");
            }

            MasterEntities context = null;

            try
            {
                // The connection string is expected to be in the App.config
                context = new MasterEntities();

                // Add the book
                context.Books.AddObject(book);

                // Save the changes
                context.SaveChanges();

                // Detach the book, so we can send it back
                context.Books.Detach(book);
            }
            catch (Exception e)
            {
                log.Error("Unable to add the provided book", e);
                throw;
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }

            return book;
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
