namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using log4net;

    /// <summary>
    /// An implementation of the ICrudService using the Entity Framework
    /// </summary>
    public class EntityCrudService : ICrudService
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ICrudService

        /// <summary>
        /// Gets the list of books in the inventory
        /// </summary>
        /// <returns>The list of books</returns>
        public IEnumerable<Media> GetMediaItems()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the provided media
        /// </summary>
        /// <param name="media">The updated media</param>
        /// <returns>The updated media with an of the generated items from the service</returns>
        public Media Update(Media media)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the provided media
        /// </summary>
        /// <param name="media">The media to add</param>
        /// <returns>The added media with any of the generated items from the service</returns>
        /// <exception cref="System.ArgumentNullException">THe provided media was null</exception>
        public Media Add(Media media)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the provided media
        /// </summary>
        /// <param name="media">The media to delete</param>
        public void Delete(Media media)
        {
            throw new NotImplementedException();
        }

        #endregion ICrudService
    }
}
