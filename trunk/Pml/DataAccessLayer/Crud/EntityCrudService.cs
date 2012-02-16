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
        /// Gets the list of media items in the inventory
        /// </summary>
        /// <returns>The list of books</returns>
        public IEnumerable<Media> GetMediaItems()
        {
            IList<Media> mediaitems = new List<Media>();
            MasterEntities context = null; 

            try
            {
                context = new MasterEntities();
                foreach (var item in context.Media)
                {
                    mediaitems.Add(item);
                }
            }
            catch (Exception e)
            {
                log.Error("unable to getMediaItems.  received exception: ", e);
                throw;
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }
            return mediaitems;

        }

        /// <summary>
        /// Updates the provided media
        /// </summary>
        /// <param name="media">The updated media</param>
        /// <returns>The updated media with an of the generated items from the service</returns>
        public Media Update(Media media)
        {
            if (null == media)
            {
                log.Warn("null argument sent to Update");
                throw new ArgumentNullException("null argument sent to Update");
            }
            MasterEntities context = null;

            try
            {
                context = new MasterEntities();
                context.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("unable to update a media item.  received exception: ",e);
                throw;
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }

            return media;
        }

        /// <summary>
        /// Adds the provided media
        /// </summary>
        /// <param name="media">The media to add</param>
        /// <returns>The added media with any of the generated items from the service</returns>
        /// <exception cref="System.ArgumentNullException">THe provided media was null</exception>
        public Media Add(Media media)
        {
            if (null == media)
            {
                log.Warn("null argument sent to Add");
                throw new ArgumentNullException("null argument sent to Add");
            }

            if (null != media.EntityKey)
            {
                log.Warn("media item sent to add already exists");
                throw new ArgumentException("media item sent to add already exists");
            }

            MasterEntities context = null;

            try
            {
                context = new MasterEntities();
                context.Media.AddObject(media);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("unable to add a media item.  received exception: ", e);
                throw;
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }

            return media;
        }

        /// <summary>
        /// Deletes the provided media
        /// </summary>
        /// <param name="media">The media to delete</param>
        public void Delete(Media media)
        {
            if (null == media)
            {
                log.Warn("null argument sent to Delete");
                throw new ArgumentNullException("null argument sent to Delete");
            }

            if (null == media.EntityKey)
            {
                log.Warn("media item sent to add that does not have entity key");
                throw new ArgumentException("media item sent to add that does not have entity key");
            }

            MasterEntities context = null;

            try
            {
                context = new MasterEntities();
                context.Media.DeleteObject(media);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("unable to delete the media item.  received exception: ", e);
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

        #endregion ICrudService
    }
}
