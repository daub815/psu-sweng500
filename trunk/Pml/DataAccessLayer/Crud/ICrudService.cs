namespace Sweng500.Pml.DataAccessLayer
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides the interface for the CRUD functionality
    /// </summary>
    public interface ICrudService
    {
        /// <summary>
        /// Gets the list of media items in the inventory
        /// </summary>
        /// <returns>The list of media items</returns>
        IEnumerable<Media> GetMediaItems();

        /// <summary>
        /// Updates the provided book
        /// </summary>
        /// <param name="media">The updated media</param>
        /// <returns>The updated media with an of the generated items from the service</returns>
        Media Update(Media media);

        /// <summary>
        /// Adds the provided media
        /// </summary>
        /// <param name="media">The book to add</param>
        /// <returns>The added media with any of the generated items from the service</returns>
        Media Add(Media media);

        /// <summary>
        /// Deletes the provided media
        /// </summary>
        /// <param name="media">The media to delete</param>
        void Delete(Media media);

        /// <summary>
        /// gets a dictionary of the genres defined
        /// </summary>
        /// <returns> a dictionary of available genre</returns>
        IDictionary<int, string> GetGenres();

        /// <summary>
        /// gets a dictionary of the available formats
        /// </summary>
        /// <returns>a dictionary of available formats</returns>
        IDictionary<int, string> GetFormats();

        /// <summary>
        /// gets the list of People currently defined
        /// </summary>
        /// <returns>a list of Person</returns>
        IEnumerable<Person> GetPeople();

        /// <summary>
        /// gets the list of Authors currently defined
        /// </summary>
        /// <returns>a list of known authors</returns>
        IEnumerable<Person> GetAuthors();
    }
}
