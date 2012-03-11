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
        /// a list of people to add who do not have an id defined in the database
        /// </summary>
        private IList<Person> peopletoadd = new List<Person>();

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

            if (rtn == false 
                && person.PersonId == 0)
            {
                foreach (Person aperson in this.peopletoadd)
                {
                    if (person.LastName.Equals(aperson.LastName) 
                        && person.FirstName.Equals(aperson.FirstName))
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
            
            if (false == this.ContainsPerson(person))
            {
                if (person.PersonId == 0)
                {
                    this.peopletoadd.Add(person);
                    rtn = true;
                }
                else
                {
                    AuthorBookAssociation assoc = new AuthorBookAssociation();
                    assoc.AuthorPersonId = person.PersonId;
                    assoc.BookMediaId = this.MediaId;
                    this.AuthorBookAssociations.Add(assoc);
                    rtn = true;
                }
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

            if (null != person &&
                true == this.ContainsPerson(person))
            {
                if (0 == person.PersonId)
                {
                        foreach (Person aperson in this.peopletoadd)
                        {
                            if (person.LastName.Equals(aperson.LastName)
                                && person.FirstName.Equals(aperson.FirstName))
                            {
                                aperson.LastName = string.Empty;
                                aperson.FirstName = string.Empty;
                                rtn = true;
                            }
                        }
                }
                else
                {
                    var authorassoc = this.AuthorBookAssociations;
                    foreach (AuthorBookAssociation assoc in authorassoc)
                    {
                        if (assoc.AuthorPersonId == person.PersonId)
                        {
                            assocremoval = assoc;
                            rtn = true;
                        }
                    }

                    if (true == rtn)
                    {
                        this.AuthorBookAssociations.Remove(assocremoval);
                    }
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

        /// <summary>
        /// Gets or sets a property to hold Authors associated with a book
        /// </summary>
        public IList<Author> Authors
        {
            get { return this.authors; }
            set { this.authors = value; }
        }

        /// <summary>
        /// Gets or Sets property to add peiople.  The add is not done until the book is saved
        /// </summary>
        internal IList<Person> PeopleToAdd
        {
            get { return this.peopletoadd; }
            set { this.peopletoadd = value; }
        }

    }
}
