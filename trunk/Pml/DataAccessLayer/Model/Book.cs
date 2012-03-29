namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This is the overridden Book class
    /// </summary>
    public partial class Book : Media
    {
        #region Statics

        /// <summary>
        /// The property name for the Authors property
        /// </summary>
        public const string AuthorsPropertyName = "Authors";

        #endregion Statics

        /// <summary>
        /// Initializes a new instance of the Book class.
        /// </summary>
        public Book()
        {
            this.MediaId = Media.GetNewMediaId();
            this.Acquired = DateTime.Now;
            this.Published = DateTime.Now;

            // Raise the associated Authors property when the associations change
            this.AuthorBookAssociations.AssociationChanged += (obj, args) =>
                {
                    this.OnPropertyChanged(AuthorsPropertyName);
                };
        }

        /// <summary>
        /// Gets a property to hold Authors associated with a book
        /// </summary>
        public IList<Author> Authors
        {
            get
            {
                return this.AuthorBookAssociations.Select(a => a.Author).ToList();
            }
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
                assoc.Author = (Author)person;
                assoc.AuthorPersonId = person.PersonId;
                assoc.BookMediaId = this.MediaId;
                this.AuthorBookAssociations.Add(assoc);
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
            AuthorBookAssociation assocremoval = null;

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
                }

                if (true == rtn)
                {
                    this.AuthorBookAssociations.Remove(assocremoval);
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
