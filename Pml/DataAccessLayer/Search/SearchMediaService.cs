namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using log4net;

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
        /// Disposes of the service
        /// </summary>
        public void Dispose()
        {
        }

        #endregion ISearchMedia
    }
}
