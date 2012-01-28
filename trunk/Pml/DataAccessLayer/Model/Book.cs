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

        /// <summary>
        /// Gets a value indicating whether the provided value equals this value
        /// </summary>
        /// <param name="obj">The object to compare this with</param>
        /// <returns>A value indicating equality</returns>
        public override bool Equals(object obj)
        {
            bool rtn = false;

            var book = obj as Book;
            if (null != book)
            {
                rtn = this.Id == book.Id;
            }

            return rtn;
        }

        /// <summary>
        /// Gets the hashcode for this object
        /// </summary>
        /// <returns>The hashcode for this object</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return this.Id.GetHashCode() * 397;
            }
        }
    }
}
