namespace Sweng500.Pml.DataAccessLayer
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides the interface for the CRUD functionality
    /// </summary>
    public interface ICrudService
    {
        IEnumerable<Book> GetBooks();
    }
}
