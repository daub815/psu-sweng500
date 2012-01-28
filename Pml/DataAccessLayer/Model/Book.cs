namespace Sweng500.Pml.DataAccessLayer
{
    using System;

    /// <summary>
    /// Represents a book
    /// </summary>
    public partial class Book
    {
        /// <summary>
        /// Initializes a new instance of the Book class
        /// </summary>
        public Book()
        {
            // We can't grab the value set by a trigger with SQL CE
            this.DateAdded = DateTime.UtcNow;
        }
    }
}
