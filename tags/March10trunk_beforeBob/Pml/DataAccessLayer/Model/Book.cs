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

        /// <summary>
        /// Provides a method to determine if this person is associated with the Authors collection of this book
        /// </summary>
        /// <param name="person">The person to check</param>
        /// <returns>A value indicating if this person is associated with the Authors collection of this book</returns>
        public override bool ContainsPerson(Person person)
        {
            bool rtn = false;

            if (person is Author)
            {
                rtn = this.Authors.Contains((Author)person);
            }

            return rtn;
        }

        /// <summary>
        /// Provides a way to add a person depending on the type of person to the correct collection
        /// </summary>
        /// <param name="person">The Person to add</param>
        /// <returns>A value indicating if it was successfully added to a collection</returns>
        public override bool AddPerson(Person person)
        {
            bool rtn = false;

            if (false == this.ContainsPerson(person))
            {
                this.Authors.Add((Author)person);
                rtn = true;
            }

            return rtn;
        }

        /// <summary>
        /// Provides a way to remove a person depending on the type of person from the correct collection
        /// </summary>
        /// <param name="person">The person to remove</param>
        /// <returns>A value indicating whether it was successfully found and removed from the collection</returns>
        public override bool RemovePerson(Person person)
        {
            bool rtn = false;

            if (null != person &&
                true == this.ContainsPerson(person))
            {
                rtn = this.Authors.Remove((Author)person);
            }

            return rtn;
        }
    }
}
