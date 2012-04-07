namespace Sweng500.Pml.Test.Mock
{
    using System.Collections.Generic;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// mock implementation of ICrudService
    /// </summary>
    public class MockCrudService : ICrudService
    {
        /// <summary>
        /// Gets or sets a value indicating whether books are returned
        /// </summary>
        public bool AllowBooks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value that controls the number of books returned
        /// </summary>
        public int NumberOfBooks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether videos are returned
        /// </summary>
        public bool AllowVideos
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property that controls the number of videos returned
        /// </summary>
        public int NumberOfVideos
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value that keeps the title of the media item
        /// </summary>
        public string MatchingTitle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether authors are matched
        /// </summary>
        public bool AllowAuthorMatch
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property that controls the number of authors returned
        /// </summary>
        public int NumberOfAuthors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether directors are matched
        /// </summary>
        public bool AllowDirectorMatch
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property that controls the number of directors returned
        /// </summary>
        public int NumberOfDirectors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether producers are matched
        /// </summary>
        public bool AllowProducerMatch
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the property that controls the number of producers returned
        /// </summary>
        public int NumberOfProducers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the list of media items in the inventory
        /// </summary>
        /// <returns>The list of media items</returns>
        public IEnumerable<Media> GetMediaItems()
        {
            IList<Media> items = new List<Media>();
            string title = this.MatchingTitle;
            if (title == null)
            {
                title = string.Empty;
            }
            if (this.AllowBooks)
            {
                for (int i = 0; i < this.NumberOfBooks; i++)
                {
                    Book bookItem = new Book();
                    bookItem.Title = string.Format("Book Title {0} {1}", title, i);
                    bookItem.ISBN = string.Format("ISBN_{0}", i);
                    items.Add(bookItem);
                }
            }


            if (this.AllowVideos)
            {
                for (int i = 0; i < this.NumberOfVideos; i++)
                {
                    Video videoItem = new Video();
                    videoItem.Title = string.Format("Video Title{0}_{1}", title, i);
                    videoItem.UPC = string.Format("UPC_{0}", i);
                    items.Add(videoItem);
                }
            }
            return items;
        }

        /// <summary>
        /// Updates the provided media
        /// </summary>
        /// <param name="media">The media to be updated</param>
        /// <returns>The updated media</returns>
        public Media Update(Media media)
        {
            return media;
        }

        /// <summary>
        /// Adds the provided media
        /// </summary>
        /// <param name="media">The book to add</param>
        /// <returns>The added media with the key generated from the service</returns>
        public Media Add(Media media)
        {
            return media;
        }

        /// <summary>
        /// Deletes the provided media
        /// </summary>
        /// <param name="media">The media to delete</param>
        public void Delete(Media media)
        {
        }

        /// <summary>
        /// gets a dictionary of the genres defined
        /// </summary>
        /// <returns> a dictionary of available genre</returns>
        public IDictionary<int, string> GetGenres()
        {
            return new Dictionary<int, string>();
        }

        /// <summary>
        /// gets a dictionary of the available formats
        /// </summary>
        /// <returns>a dictionary of available formats</returns>
        public IDictionary<int, string> GetFormats()
        {
            return new Dictionary<int, string>();
        }

        /// <summary>
        /// gets the list of People currently defined
        /// </summary>
        /// <returns>a list of Person</returns>
        public IEnumerable<Person> GetPeople()
        {
            IList<Person> people = new List<Person>();
            if (this.AllowAuthorMatch)
            {
                for (int i = 0; i < this.NumberOfAuthors; i++)
                {
                    Author author = new Author();
                    author.FirstName = "Test";
                    author.LastName = string.Format("Author_{0}", i);
                    author.PersonId = 1000 + i;
                    people.Add(author);
                }
            }

            if (this.AllowDirectorMatch)
            {
                for (int i = 0; i < this.NumberOfDirectors; i++)
                {
                    Director director = new Director();
                    director.FirstName = "Test";
                    director.LastName = string.Format("Director", i);
                    director.PersonId = 2000 + i;
                    people.Add(director);
                }
            }

            if (this.AllowProducerMatch)
            {
                for (int i = 0; i < this.NumberOfProducers; i++)
                {
                    Producer producer = new Producer();
                    producer.FirstName = "Test";
                    producer.LastName = string.Format("Producer", i);
                    producer.PersonId = 3000 + i;
                    people.Add(producer);
                }
            }

            return people;
        }

        /// <summary>
        /// mock method for getting the list of Authors currently defined
        /// </summary>
        /// <returns>a list of known authors</returns>
        public IEnumerable<Person> GetAuthors()
        {
            IList<Person> people = new List<Person>();
            if (this.AllowAuthorMatch)
            {
                for (int i = 0; i < this.NumberOfAuthors; i++)
                {
                    Author author = new Author();
                    author.FirstName = "Test";
                    author.LastName = string.Format("Author_{0}", i);
                    author.PersonId = 1000 + i;
                    people.Add(author);
                }
            }

            return people;
        }

        /// <summary>
        /// mock method for Updating the provided person
        /// </summary>
        /// <param name="person">The person to be updated</param>
        /// <returns>The same person as input</returns>
        public Person Update(Person person)
        {
            return person;
        }

        /// <summary>
        /// mock method for Adding the provided person
        /// </summary>
        /// <param name="person">The person to add</param>
        /// <returns>The same person as input</returns>
        public Person Add(Person person)
        {
            return person;
        }

        /// <summary>
        /// mock method that does not Dispose the context
        /// </summary>
        public void Dispose()
        {
        }
    }
}
