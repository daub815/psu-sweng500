namespace Sweng500.Pml.DataAccessLayer
{
    using System;

    /// <summary>
    /// This is the overridden Book class
    /// </summary>
    public partial class Book : Media
    {
        /// <summary>
        /// Initializes a new instance of the Book class.
        /// Defines default DateTime values
        /// </summary>
        public Book()
        {
            this.Acquired = DateTime.Now;
            this.Published = DateTime.Now;
        }
    }
}
