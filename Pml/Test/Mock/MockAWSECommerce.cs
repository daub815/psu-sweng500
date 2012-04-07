namespace Sweng500.Pml.Test.Mock
{
    using System.Collections.Generic;
    using Sweng500.Awse.CommerceService;

    /// <summary>
    /// a mock implementation of IAWSECommerceService
    /// </summary>
    public class MockAWSECommerce : IAWSECommerceService
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
        /// A method to do an item search using the Amazon web service
        /// </summary>
        /// <param name="searchIndex"> the group to do search such as "DVD","Books"</param>
        /// <param name="title">words in the title to search on</param>
        /// <param name="keywords">keywords to search on</param>
        /// <returns>a list of responses that are defined parts of the search response</returns>
        public IList<ItemResponse> ItemSearch(string searchIndex, string title, string keywords)
        {
            IList<ItemResponse> searchResponse = new List<ItemResponse>();
            if ("Books".Equals(searchIndex) && this.AllowBooks)
            {
                for (int i = 0; i < this.NumberOfBooks; i++)
                {
                    ItemResponse bookitem = this.CreateBook(i);
                    searchResponse.Add(bookitem);
                }
            }

            if ("DVD".Equals(searchIndex) && this.AllowVideos)
            {
                for (int i = 0; i < this.NumberOfVideos; i++)
                {
                    ItemResponse videoitem = this.CreateVideo(i);
                    searchResponse.Add(videoitem);
                }
            }

            return searchResponse;
        }

        /// <summary>
        /// A method to do an item search using the Amazon web service for authors within Books
        /// </summary>
        /// <param name="author">name of the author to search on</param>
        /// <param name="keywords">keywords to search on</param>
        /// <returns>a list of responses that are defined parts of the search response</returns>
        public IList<ItemResponse> AuthorSearch(string author, string keywords)
        {
            IList<ItemResponse> searchResponse = new List<ItemResponse>();
            if (this.AllowBooks)
            {
                for (int i = 0; i < this.NumberOfBooks; i++)
                {
                    ItemResponse bookitem = this.CreateBook(i);
                    searchResponse.Add(bookitem);
                }
            }

            return searchResponse;
        }

        /// <summary>
        /// create an item reponse that has fields for a book
        /// </summary>
        /// <param name="num"> the item number</param>
        /// <returns>an ItemResponse</returns>
        private ItemResponse CreateBook(int num)
        {
            ItemResponse ir = new ItemResponse();
            ir.Description = string.Format("Description_{0}", num);
            ir.Isbn = string.Format("ISBN_{0}", num);
            ir.Title = string.Format("BookTitle_{0}", num);
            return ir;
        }

        /// <summary>
        /// create an item reponse that has fields for a video
        /// </summary>
        /// <param name="num"> the item number</param>
        /// <returns>an ItemResponse</returns>
        private ItemResponse CreateVideo(int num)
        {
            ItemResponse ir = new ItemResponse();
            ir.Description = string.Format("Description_{0}", num);
            ir.Upc = string.Format("UPC_{0}", num);
            ir.Title = string.Format("VideoTitle_{0}", num);
            return ir;
        }
    }
}
