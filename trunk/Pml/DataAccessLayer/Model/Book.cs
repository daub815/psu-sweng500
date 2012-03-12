namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This is the overridden Book class
    /// </summary>
    public partial class Book : Media
    {
        /// <summary>
        /// a list of authors associated with a book so external does not have to naviagte AuthorBookAssociations
        /// </summary>
        private IList<Author> authors = new List<Author>();

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
        /// Gets or sets a property to hold Authors associated with a book
        /// </summary>
        public IList<Author> Authors
        {
            get { return this.authors; }
            set { this.authors = value; }
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
                var authorassoc = this.AuthorBookAssociations;
                foreach (AuthorBookAssociation assoc in authorassoc) 
                {
                    if (assoc.AuthorPersonId == person.PersonId)
                    {
                        rtn = true;
                    }
                }
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
            
            if (person is Author 
                && false == this.ContainsPerson(person))
            {
                AuthorBookAssociation assoc = new AuthorBookAssociation();
                assoc.AuthorPersonId = person.PersonId;
                assoc.BookMediaId = this.MediaId;
                this.AuthorBookAssociations.Add(assoc);
                rtn = true;
                this.authors.Add((Author)person);
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
            AuthorBookAssociation assocremoval = null;
            IList<Author> remaining = new List<Author>();

            if (null != person &&
                true == this.ContainsPerson(person))
            {
                var authorassoc = this.AuthorBookAssociations;
                foreach (AuthorBookAssociation assoc in authorassoc)
                {
                    if (assoc.AuthorPersonId == person.PersonId)
                    {
                        assocremoval = assoc;
                        rtn = true;
                    } 
                    else 
                    {
                        remaining.Add(assoc.Author);
                    }
                }

                if (true == rtn)
                {
                    this.AuthorBookAssociations.Remove(assocremoval);
                    this.authors = remaining;
                }
            }

            return rtn;
        }

        /// <summary>
        /// Override the string representation
        /// </summary>
        /// <returns>A value representing the book state</returns>
        public override string ToString()
        {
            return string.Format("Title: {0}  Description: {1} Comment: {2} Id: {3} ISBN: {4} ", this.Title, this.Description, this.Comment, this.MediaId, this.ISBN);
        }
    }
}
