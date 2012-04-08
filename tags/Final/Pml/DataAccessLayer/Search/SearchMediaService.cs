namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;
    using Sweng500.Awse.CommerceService;

    /// <summary>
    /// Implementation of the ISearchMedia interface 
    /// </summary>
    public class SearchMediaService : ISearchMediaService
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// the limit for most text fields
        /// </summary>
        private const int TEXTLIMIT = 4000;

        /// <summary>
        /// the limit for ISBN
        /// </summary>
        private const int ISBNLIMIT = 13;

        /// <summary>
        /// constant for searching books
        /// </summary>
        private const string BOOKSEARCHINDEX = "Books";

        /// <summary>
        /// constant for searching videos
        /// </summary>
        private const string DVDSEARCHINDEX = "DVD";

        /// <summary>
        /// Gets or sets a mock ICrudService for testing only
        /// </summary>
        public ICrudService MockCrudService
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a mock IAWSECommerceService for testing only
        /// </summary>
        public IAWSECommerceService MockIAWSECommerceService
        {
            get;
            set;
        }

        #region ISearchMedia
        /// <summary>
        /// Gets the list of media items in the inventory that contain
        /// the specified string in the title.
        /// The return is orded by the full media title
        /// </summary>
        /// <param name="partialTitle"> is a user specified search key</param>
        /// <returns>The list of media items orded by the full media title</returns>
        public IEnumerable<Media> GetMediaItemsContaining(string partialTitle)
        {
            ICrudService crudservice = this.GetCrudService();
            string lowerCasePartial = partialTitle.ToLowerInvariant();

            try
            {
                return this.GetMatchingTitles(crudservice, lowerCasePartial);
            }
            catch (Exception e)
            {
                log.ErrorFormat("unable to GetMediaItemsContaining {0}:  received exception: {1}", partialTitle, e);
                throw;
            }
        }

        /// <summary>
        /// Gets the list of media items in the inventory that have a borrower.
        /// The return is orded by the borrower last name
        /// </summary>
        /// <returns>The list of media items that have been borrowed</returns>
        public IEnumerable<Media> GetBorrowedMediaItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// does a search using an amazon web service
        /// </summary>
        /// <param name="mediatype"> the type of media to search for</param>
        /// <param name="title"> words in the title to search for</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of media items build from the search responses</returns>
        public IEnumerable<Media> SearchRemote(MediaTypes mediatype, string title, string keywords)
        {
            IList<Media> values = new List<Media>();
            bool booksearch = false;
            string searchindex = string.Empty;
            string titlearg = string.Empty;
            string keywordarg = string.Empty;
            if (MediaTypes.Book == mediatype)
            {
                searchindex = BOOKSEARCHINDEX;
                booksearch = true;
            }
            else if (MediaTypes.Video == mediatype)
            {
                searchindex = DVDSEARCHINDEX;
            }

            if (null != title)
            {
                titlearg = title;
            }

            if (null != keywords)
            {
                keywordarg = keywords;
            }

            IList<ItemResponse> searchresponse = this.GetSearchResponses(searchindex, titlearg, keywordarg);

            foreach (ItemResponse itemresponse in searchresponse)
            {
                if (booksearch)
                {
                    Book abookresponse = this.CreateBookFromSearch(itemresponse);
                    values.Add(abookresponse);
                }
                else
                {
                    Video avideoresponse = this.CreateVideoFromSearch(itemresponse);
                    values.Add(avideoresponse);
                }
            }

            return values;
        }

        /// <summary>
        /// does a search using an amazon web service.  Automatically search by both Book and Video
        /// </summary>
        /// <param name="title"> words in the title to search for</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of media items build from the search responses</returns>
        public IEnumerable<Media> SearchRemote(string title, string keywords)
        {
            IList<Media> values = new List<Media>();
            IEnumerable<Media> bookresponses = this.SearchRemote(MediaTypes.Book, title, keywords);
            IEnumerable<Media> videoresponses = this.SearchRemote(MediaTypes.Video, title, keywords);
            
            foreach (Media bookitem in bookresponses)
            {
                values.Add(bookitem);
            }

            foreach (Media videoitem in videoresponses)
            {
                values.Add(videoitem);
            }

            return values;
        }

        /// <summary>
        /// does a search using an amazon web service. Search is done within Books
        /// </summary>
        /// <param name="authorname"> author name to search for</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of media items build from the search responses</returns>
        public IEnumerable<Media> AuthorSearchRemote(string authorname, string keywords)
        {
            IList<Media> values = new List<Media>();
            string authorarg = string.Empty;
            string keywordarg = string.Empty;

            if (null != authorname)
            {
                authorarg = authorname;
            }

            if (null != keywords)
            {
                keywordarg = keywords;
            }

            IList<ItemResponse> searchresponse = this.GetAuthorSearchResponses(authorarg, keywordarg);

            foreach (ItemResponse itemresponse in searchresponse)
            {
                Book abookresponse = this.CreateBookFromSearch(itemresponse);
                values.Add(abookresponse);
            }

            return values;
        }

        /// <summary>
        /// Disposes of the service
        /// </summary>
        public void Dispose()
        {
        }

        #endregion ISearchMedia

        /// <summary>
        /// define a method to return the crud service
        /// Return the mock crude service if it is not null else
        /// use Repository to get an instance
        /// </summary>
        /// <returns> an instance of the crudService</returns>
        protected ICrudService GetCrudService()
        {
            if (null != this.MockCrudService)
            {
                return this.MockCrudService;
            }

            return Repository.Instance.ServiceLocator.GetInstance<ICrudService>();
        }

        /// <summary>
        /// define a method to return the ECommerceService
        /// Return the mock ecommerce service if it is not null else
        /// return a new instance
        /// </summary>
        /// <returns> an instance of the crudService</returns>
        protected IAWSECommerceService GetAWSECommerceService()
        {
            if (null != this.MockIAWSECommerceService)
            {
                return this.MockIAWSECommerceService;
            }

            return new AWSECommerceService();
        }

        /// <summary>
        /// method extracted to make testing easier
        /// </summary>
        /// <param name="authorname">author name to search </param>
        /// <param name="keywords"> keywords to search</param>
        /// <returns> a list of item responses</returns>
        protected IList<ItemResponse> GetAuthorSearchResponses(string authorname, string keywords)
        {
            IAWSECommerceService ecs = this.GetAWSECommerceService();
            IList<ItemResponse> searchresponse = ecs.AuthorSearch(authorname, keywords);
            return searchresponse;
        }

        /// <summary>
        /// method extracted to make testing easier
        /// </summary>
        /// <param name="searchindex"> search index</param>
        /// <param name="title"> portion of title to search </param>
        /// <param name="keywords"> keywords to search</param>
        /// <returns> a list of item responses</returns>
        protected IList<ItemResponse> GetSearchResponses(string searchindex, string title, string keywords)
        {
            IAWSECommerceService ecs = this.GetAWSECommerceService();
            IList<ItemResponse> searchresponse = ecs.ItemSearch(searchindex, title, keywords);
            return searchresponse;
        }

        /// <summary>
        /// method to get an author that matches the first name and last name
        /// </summary>
        /// <param name="crudservice"> the service used to retrieve the people</param>
        /// <param name="firstname"> first name to match</param>
        /// <param name="lastname">last name to match</param>
        /// <returns>an author whose first name and last name match the input values or null</returns>
        protected Author GetMatchedAuthor(ICrudService crudservice, string firstname, string lastname)
        {
            Author author = null;
            List<Person> matched = this.GetMatchedPeople(crudservice, firstname, lastname);
            foreach (Person person in matched)
            {
                if (person is Author)
                {
                    author = (Author)person;
                }
            }

            return author;
        }

        /// <summary>
        /// method to get a Director who matches the first name and last name
        /// </summary>
        /// <param name="crudservice"> the service used to retrieve the people</param>
        /// <param name="firstname"> first name to match</param>
        /// <param name="lastname">last name to match</param>
        /// <returns>a Director whose first name and last name match the input values or null</returns>
        protected Director GetMatchedDirector(ICrudService crudservice, string firstname, string lastname)
        {
            Director director = null;
            List<Person> matched = this.GetMatchedPeople(crudservice, firstname, lastname);
            foreach (Person person in matched)
            {
                if (person is Director)
                {
                    director = (Director)person;
                }
            }

            return director;
        }

        /// <summary>
        /// method to get a Producer who matches the first name and last name
        /// </summary>
        /// <param name="crudservice"> the service used to retrieve the people</param>
        /// <param name="firstname"> first name to match</param>
        /// <param name="lastname">last name to match</param>
        /// <returns>a Producer whose first name and last name match the input values or null</returns>
        protected Producer GetMatchedProducer(ICrudService crudservice, string firstname, string lastname)
        {
            Producer producer = null;
            List<Person> matched = this.GetMatchedPeople(crudservice, firstname, lastname);
            foreach (Person person in matched)
            {
                if (person is Producer)
                {
                    producer = (Producer)person;
                }
            }

            return producer;
        }

        /// <summary>
        /// gets a list of media whose title includes the supplied string.  The search is
        /// case insensitive so the items are made lower case for comparision since by default
        /// LINQ is case sensitive.
        /// </summary>
        /// <param name="crudservice"> an instance of the ICrudService interface</param>
        /// <param name="lowerCasePartial"> the title supplied by the user moved to lower case</param>
        /// <returns>a List of Media whose title contains the partialtile in a case insensitive compare</returns>
        protected List<Media> GetMatchingTitles(ICrudService crudservice, string lowerCasePartial)
        {
            List<Media> items = (from media in crudservice.GetMediaItems()
                                 where media.Title.ToLowerInvariant().Contains(lowerCasePartial)
                                 orderby media.Title
                                 select media as Media).ToList();
            return items;
        }

        /// <summary>
        /// common routine to get a list of people that match the name
        /// </summary>
        /// <param name="crudservice"> an instance of the ICrudService interface</param>
        /// <param name="firstname"> first name to match</param>
        /// <param name="lastname">last name to match</param>
        /// <returns>a List of people that match the name</returns>
        private List<Person> GetMatchedPeople(ICrudService crudservice, string firstname, string lastname)
        {
            List<Person> matched = (from person in crudservice.GetPeople()
                                    where person.FirstName == firstname &&
                                        person.LastName == lastname
                                    select person as Person).ToList();
            return matched;
        }

        /// <summary>
        /// create a book from the search response
        /// </summary>
        /// <param name="itemresponse"> an item returned from the search</param>
        /// <returns> a new book with fields populated</returns>
        private Book CreateBookFromSearch(ItemResponse itemresponse)
        {
            var crud = this.GetCrudService();
            Book searchbook = new Book();
            searchbook.Title = this.TruncateToLimit(itemresponse.Title, TEXTLIMIT);
            searchbook.Comment = string.Empty;
            searchbook.Description = this.TruncateToLimit(itemresponse.Description, TEXTLIMIT);
            searchbook.ImageUrl = this.TruncateToLimit(itemresponse.Imageurl, TEXTLIMIT);
            searchbook.IsBorrowable = false;

            if (itemresponse.Isbn != null)
            {
                searchbook.ISBN = this.TruncateToLimit(itemresponse.Isbn, ISBNLIMIT);
            }
            else
            {
                string[] eisbn = itemresponse.Eisbn;
                if (eisbn != null && eisbn.Length > 0)
                {
                    searchbook.ISBN = this.TruncateToLimit(eisbn[0], ISBNLIMIT);
                }
            }

            searchbook.LibraryLocation = string.Empty;
            if (null != itemresponse.Publicationdate
                && itemresponse.Publicationdate > DateTime.MinValue)
            {
                searchbook.Published = itemresponse.Publicationdate;
            }

            searchbook.Publisher = this.TruncateToLimit(itemresponse.Publisher, TEXTLIMIT);

            if (itemresponse.Authorsname != null)
            {
                foreach (ItemName name in itemresponse.Authorsname)
                {
                    Author matched = this.GetMatchedAuthor(crud, name.First, name.Last);

                    if (null == matched)
                    {
                        Author author = new Author();
                        author.FirstName = name.First;
                        author.LastName = name.Last;
                        searchbook.AddPerson(author);
                        ////author = this.AddPerson(crud, author);
                        ////matched = this.GetMatchedAuthor(crud, name.First, name.Last);
                    }

                    if (null != matched)
                    {
                        searchbook.AddPerson(matched);
                    }
                }
            }

            return searchbook;
        }

        /// <summary>
        /// create a video from the search response
        /// </summary>
        /// <param name="itemresponse"> an item returned from the search</param>
        /// <returns> a new video with fields populated</returns>
        private Video CreateVideoFromSearch(ItemResponse itemresponse)
        {
            var crud = this.GetCrudService();

            Video searchvideo = new Video();
            searchvideo.Title = this.TruncateToLimit(itemresponse.Title, TEXTLIMIT);
            searchvideo.Comment = string.Empty;
            searchvideo.Description = this.TruncateToLimit(itemresponse.Description, TEXTLIMIT);
            searchvideo.IsBorrowable = false;
            searchvideo.ImageUrl = this.TruncateToLimit(itemresponse.Imageurl, TEXTLIMIT);
            searchvideo.Publisher = this.TruncateToLimit(itemresponse.Publisher, TEXTLIMIT);
            if (null != itemresponse.Releasedate
                && itemresponse.Releasedate > DateTime.MinValue)
            {
                searchvideo.Released = itemresponse.Releasedate;
            }

            if (itemresponse.Upc != null) 
            {
                searchvideo.UPC = itemresponse.Upc;
            }

            if (itemresponse.Directorsname != null)
            {
                foreach (ItemName name in itemresponse.Directorsname)
                {
                    Director matched = this.GetMatchedDirector(crud, name.First, name.Last);

                    if (null == matched)
                    {
                        Director director = new Director();
                        director.FirstName = name.First;
                        director.LastName = name.Last;
                        searchvideo.AddPerson(director);
                        ////director = this.AddPerson(crud, director);
                        ////matched = this.GetMatchedDirector(crud, name.First, name.Last);
                    }

                    if (null != matched)
                    {
                        searchvideo.AddPerson(matched);
                    }
                }
            }

            if (itemresponse.Producersname != null)
            {
                foreach (ItemName name in itemresponse.Producersname)
                {
                    Producer matched = this.GetMatchedProducer(crud, name.First, name.Last);

                    if (null == matched)
                    {
                        Producer producer = new Producer();
                        producer.FirstName = name.First;
                        producer.LastName = name.Last;
                        searchvideo.AddPerson(producer);
                        ////producer = this.AddPerson(crud, producer);
                        ////matched = this.GetMatchedProducer(crud, name.First, name.Last);
                    }

                    if (null != matched)
                    {
                        searchvideo.AddPerson(matched);
                    }
                }
            }

            return searchvideo;
        }

        /// <summary>
        /// limit string to defined number of characters
        /// </summary>
        /// <param name="value"> the input value</param>
        /// <param name="limit"> tbhe max number of characters</param>
        /// <returns>a string which map have been truncated</returns>
        private string TruncateToLimit(string value, int limit)
        {
            string returnVal = string.Empty;
            if (null != value)
            {
                returnVal = value.Trim();
                if (limit < returnVal.Length)
                {
                    returnVal = returnVal.Substring(0, limit);
                }
            }

            return returnVal;
        }
    }
}
