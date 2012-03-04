namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides the interface for the CRUD functionality
    /// </summary>
    public interface ICrudService : IDisposable
    {
        /// <summary>
        /// Gets the list of media items in the inventory
        /// </summary>
        /// <returns>The list of media items</returns>
        IEnumerable<Media> GetMediaItems();

        /// <summary>
        /// Updates the provided media
        /// </summary>
        /// <param name="media">The media to be updated</param>
        /// <returns>The updated media</returns>
        Media Update(Media media);

        /// <summary>
        /// Adds the provided media
        /// </summary>
        /// <param name="media">The book to add</param>
        /// <returns>The added media with the key generated from the service</returns>
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

        /// <summary>
        /// Updates the provided person
        /// </summary>
        /// <param name="person">The person to be updated</param>
        /// <returns>The updated person</returns>
        Person Update(Person person);

        /// <summary>
        /// Adds the provided person
        /// </summary>
        /// <param name="person">The person to add</param>
        /// <returns>The added person with the key generated from the service</returns>
        Person Add(Person person);

        /// <summary>
        /// Dispose of this service
        /// </summary>
        void Dispose();
    }
}
