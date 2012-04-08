namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface used to search for media.  The search may be done in the pml database
    /// or remotely.
    /// </summary>
    public interface ISearchMediaService : IDisposable
    {
        /// <summary>
        /// Gets the list of media items in the inventory that contain
        /// the specified string in the title.
        /// The return is orded by the full media title
        /// </summary>
        /// <param name="partialTitle"> is a user specified search key</param>
        /// <returns>The list of media items orded by the full media title</returns>
        IEnumerable<Media> GetMediaItemsContaining(string partialTitle);

        /// <summary>
        /// Gets the list of media items in the inventory that have a borrower.
        /// The return is orded by the borrower last name
        /// </summary>
        /// <returns>The list of media items that have been borrowed</returns>
        IEnumerable<Media> GetBorrowedMediaItems();

        /// <summary>
        /// does a search using an amazon web service
        /// </summary>
        /// <param name="mediatype"> the type of media to search for</param>
        /// <param name="title"> words in the title to search for</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of media items build from the search responses</returns>
        IEnumerable<Media> SearchRemote(MediaTypes mediatype, string title, string keywords);

        /// <summary>
        /// does a search using an amazon web service. Automatically search fro both MediaTypes
        /// </summary>
        /// <param name="title"> words in the title to search for</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of media items build from the search responses</returns>
        IEnumerable<Media> SearchRemote(string title, string keywords);

        /// <summary>
        /// does a search using an amazon web service. Search is done within Books
        /// </summary>
        /// <param name="authorname"> author name to search for</param>
        /// <param name="keywords"> keywords to search for</param>
        /// <returns> a list of media items build from the search responses</returns>
        IEnumerable<Media> AuthorSearchRemote(string authorname, string keywords);
    }
}
