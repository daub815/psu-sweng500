namespace Sweng500.Awse.CommerceService
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that defines an ItemSearch for the Amazon ProfuctAdvertising API
    /// </summary>
    public interface IAWSECommerceService
    {
        /// <summary>
        /// A method to do an item search using the Amazon web service
        /// </summary>
        /// <param name="searchIndex"> the group to do search such as "DVD","Books"</param>
        /// <param name="title">words in the title to search on</param>
        /// <param name="keywords">keywords to search on</param>
        /// <returns>a list of responses that are defined parts of the search response</returns>
        IList<ItemResponse> ItemSearch(string searchIndex, string title, string keywords);

        /// <summary>
        /// A method to do an item search using the Amazon web service for authors within Books
        /// </summary>
        /// <param name="author">name of the author to search on</param>
        /// <param name="keywords">keywords to search on</param>
        /// <returns>a list of responses that are defined parts of the search response</returns>
        IList<ItemResponse> AuthorSearch(string author, string keywords);       
    }
}
