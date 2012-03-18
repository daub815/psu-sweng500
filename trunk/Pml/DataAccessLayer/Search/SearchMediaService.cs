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
            IList<Media> mediaitems = new List<Media>();
            MasterEntities context = null;

            try
            {
                context = new MasterEntities();
                var mediaQuery =
                    from media in context.Media
                    where media.Title.Contains(partialTitle)
                    orderby media.Title
                    select media;
                return mediaQuery.ToList<Media>();
            }
            catch (Exception e)
            {
                log.Error("unable to GetMediaItemsContaining.  received exception: ", e);
                throw;
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
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
            if (MediaTypes.Book == mediatype)
            {
                searchindex = "Books";
                booksearch = true;
            }
            else
            {
                searchindex = "DVD";
            }

            AWSECommerceService ecs = new AWSECommerceService();
            IList<ItemResponse> searchresponse = ecs.ItemSearch(searchindex, title, keywords);
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
        /// Disposes of the service
        /// </summary>
        public void Dispose()
        {
        }

        #endregion ISearchMedia

        /// <summary>
        /// create a book from the search response
        /// </summary>
        /// <param name="itemresponse"> an item returned from the search</param>
        /// <returns> a new book with fields populated</returns>
        private Book CreateBookFromSearch(ItemResponse itemresponse)
        {
            var crud = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

            Book searchbook = new Book();
            searchbook.Title = itemresponse.Title;
            searchbook.Comment = string.Empty;
            searchbook.Description = string.Empty;
            searchbook.ImageUrl = itemresponse.Imageurl;
            searchbook.IsBorrowable = false;

            if (itemresponse.Isbn != null)
            {
                searchbook.ISBN = itemresponse.Isbn;
            }
            else
            {
                string[] eisbn = itemresponse.Eisbn;
                if (eisbn != null && eisbn.Length > 0)
                {
                    searchbook.ISBN = eisbn[0];
                }
            }

            searchbook.LibraryLocation = string.Empty;
            searchbook.Published = itemresponse.Publicationdate;
            searchbook.Publisher = itemresponse.Publisher;

            if (itemresponse.Authorsname != null)
            {
                foreach (ItemName name in itemresponse.Authorsname)
                {
                    List<Person> matched = (from person in crud.GetPeople()
                                  where person.FirstName == name.First &&
                                        person.LastName == name.Last
                                  select person as Person).ToList();

                    if (0 == matched.Count)
                    {
                        Person author = new Author();
                        author.FirstName = name.First;
                        author.LastName = name.Last;
                        author = crud.Add(author);
                        matched = (from person in crud.GetPeople()
                                                where person.FirstName == name.First &&
                                                      person.LastName == name.Last
                                                select person as Person).ToList();

                    }

                    matched.ForEach(a => searchbook.AddPerson(a));
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
            var crud = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

            Video searchvideo = new Video();
            searchvideo.Title = itemresponse.Title;
            searchvideo.Comment = string.Empty;
            searchvideo.Description = string.Empty;
            searchvideo.IsBorrowable = false;
            searchvideo.ImageUrl = itemresponse.Imageurl;
            searchvideo.Publisher = itemresponse.Publisher;
            searchvideo.Released = itemresponse.Releasedate;
            if (itemresponse.Upc != null) 
            {
                searchvideo.UPC = itemresponse.Upc;
            }
           
            return searchvideo;
        }
    }
}
