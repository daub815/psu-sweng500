namespace Sweng500.Pml.DataAccessLayer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
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
        
        /// <summary>
        /// the limit for most text fields
        /// </summary>
        private const int TEXTLIMIT = 4000;

        /// <summary>
        /// the limit for ISBN
        /// </summary>
        private const int ISBNLIMIT = 13;

        /// <summary>
        /// the limit for UPC
        /// </summary>
        private const int UPCLIMIT = 15;

        /// <summary>
        /// context used when interfacing with MasterEntities
        /// The service will use a single context throughout its lifetime and dispose if the context when the service is disposed of.
        /// </summary>
        private MasterEntities context = null;

        #region ICrudService

        /// <summary>
        /// Gets the list of media items in the inventory
        /// </summary>
        /// <returns>The list of books</returns>
        public IEnumerable<Media> GetMediaItems()
        {
            IList<Media> mediaitems = new List<Media>();
            MasterEntities context = this.GetContext(); 

            try
            {
                foreach (var item in context.Media)
                {
                    if (item.IsBorrowable && !item.BorrowedMedias.IsLoaded)
                    {
                        item.BorrowedMedias.Load();
                    }

                    if (item is Book)
                    {
                        // Load the associations and the Authors for the book explicitly including 
                        Book book = (Book)item;
                        if (!book.AuthorBookAssociations.IsLoaded)
                        {
                            book.AuthorBookAssociations.Load();
                        }

                        var assoociations = book.AuthorBookAssociations;
                        foreach (AuthorBookAssociation assoc in assoociations)
                        {
                            if (!assoc.AuthorReference.IsLoaded)
                            {
                                assoc.AuthorReference.Load();
                            }
                        }

                        mediaitems.Add(book);
                    }

                    if (item is Video)
                    {
                        // Load the associations and the Directors and Producers for the video explicitly.
                        Video video = (Video)item;
                        if (!video.DirectorAssociations.IsLoaded)
                        {
                            video.DirectorAssociations.Load();
                        }

                        var associations = video.DirectorAssociations;
                        foreach (DirectorAssociation assoc in associations)
                        {
                            if (!assoc.DirectorReference.IsLoaded)
                            {
                                assoc.DirectorReference.Load();
                            }
                        }

                        if (!video.ProducerAssociations.IsLoaded)
                        {
                            video.ProducerAssociations.Load();
                        }

                        var producerassociations = video.ProducerAssociations;
                        foreach (ProducerAssociation assoc in producerassociations)
                        {
                            if (!assoc.ProducerReference.IsLoaded)
                            {
                                assoc.ProducerReference.Load();
                            }
                        }

                        mediaitems.Add(video);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("unable to getMediaItems.  received exception: ", e);
                throw;
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

            MasterEntities context = this.GetContext();
            this.CheckTextFieldLimits(media);
            try
            {
                if (System.Data.EntityState.Detached == media.EntityState
                    || System.Data.EntityState.Unchanged == media.EntityState)
                {
                    context.Attach(media);
                }

                // Update the state of the object to modified
                context.ObjectStateManager.ChangeObjectState(media, System.Data.EntityState.Modified);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string message = string.Format("unable to update a media item.  received exception: {0} ", e);
                log.Error(message);
                throw;
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

            MasterEntities context = this.GetContext();
            this.CheckTextFieldLimits(media);
            if (media is Video)
            {
                Video videoItem = (Video)media;
                context.Media.AddObject(videoItem);
            }

            if (media is Book)
            {
                Book bookitem = (Book)media;
                context.Media.AddObject(bookitem);
            }
 
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string message = string.Format("unable to add a media item.  received exception: {0}", e);
                Console.WriteLine(message);
                log.Error(message);
                throw;
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
                if (log.IsWarnEnabled)
                {
                    log.Warn("media item sent to add that does not have entity key");
                    throw new ArgumentException("media item sent to add that does not have entity key");
                }
            }

            MasterEntities context = this.GetContext();

            try
            {
                if (System.Data.EntityState.Detached == media.EntityState
                    || System.Data.EntityState.Unchanged == media.EntityState)
                {
                    context.Attach(media);
                } 
                
                if (media is Book)
                {
                    Book book = (Book)media;
                    var associations = context.AuthorBookAssociations;
                    foreach (AuthorBookAssociation assoc in associations)
                    {
                        if (assoc.BookMediaId == media.MediaId)
                        {
                            context.AuthorBookAssociations.DeleteObject(assoc);
                        }
                    }  
                }

                if (media is Video)
                {
                    Video video = (Video)media;
                    var directorassociations = context.DirectorAssociations;
                    foreach (DirectorAssociation assoc in directorassociations)
                    {
                        if (assoc.VideoMediaId == media.MediaId)
                        {
                            context.DirectorAssociations.DeleteObject(assoc);
                        }
                    }

                    var producerassociations = context.ProducerAssociations;
                    foreach (ProducerAssociation assoc in producerassociations)
                    {
                        if (assoc.VideoMediaId == media.MediaId)
                        {
                            context.ProducerAssociations.DeleteObject(assoc);
                        }
                    }
                }
                context.Media.DeleteObject(media);
                context.SaveChanges(); 
                if (log.IsInfoEnabled) 
                {
                    log.InfoFormat("Deleting media item: {0}", media);
                }
            }
            catch (Exception e)
            {
                log.Error("unable to delete the media item.  received exception: ", e);
                throw;
            }
        }

        /// <summary>
        /// gets a dictionary of the genres defined
        /// </summary>
        /// <returns>The genres as a dictionary</returns>
        public IDictionary<int, string> GetGenres()
        {
            IDictionary<int, string> genreDictionary = new Dictionary<int, string>();
            MasterEntities context = this.GetContext();
            var query = from c in context.Codes where c.CodeTypeId == (int)DropDownTypes.Genre select c;
            var genres = query.ToList();
            foreach (var item in genres)
            {
                genreDictionary.Add(item.CodeId, item.CodeDescription);
            }

            return genreDictionary;
        }

        /// <summary>
        /// gets a dictionary of the available formats
        /// </summary>
        /// <returns>The formats as a dictionary</returns>
        public IDictionary<int, string> GetFormats()
        {
            IDictionary<int, string> formatDictionary = new Dictionary<int, string>();
            MasterEntities context = this.GetContext();
            var query = from c in context.Codes where c.CodeTypeId == (int)DropDownTypes.Format select c;
            var formats = query.ToList();
            foreach (var formatitem in formats)
            {
                formatDictionary.Add(formatitem.CodeId, formatitem.CodeDescription);
            }

            return formatDictionary;
        }

        /// <summary>
        /// gets a dictionary of the available board ratings
        /// </summary>
        /// <returns>The formats as a dictionary</returns>
        public IDictionary<int, string> GetBoardRatings()
        {
            IDictionary<int, string> ratingDictionary = new Dictionary<int, string>();
            MasterEntities context = this.GetContext();
            var query = from c in context.Codes where c.CodeTypeId == (int)DropDownTypes.BoardRating select c;
            var ratings = query.ToList();
            foreach (var item in ratings)
            {
                ratingDictionary.Add(item.CodeId, item.CodeDescription);
            }

            return ratingDictionary;
        }

        /// <summary>
        /// gets the list of People currently defined
        /// </summary>
        /// <returns>Gets a list of people defined in any way</returns>
        public IEnumerable<Person> GetPeople()
        {
            MasterEntities context = this.GetContext();
            var query = from c in context.People 
                        orderby c.LastName 
                        select c;
            return query.ToList<Person>();
        }

        /// <summary>
        /// gets the list of Authors currently defined
        /// </summary>
        /// <returns>Gets a list of people defined as authors</returns>
        public IEnumerable<Person> GetAuthors()
        {
            MasterEntities context = this.GetContext();
            var query = from p in context.People.OfType<Author>() 
                        orderby p.LastName
                        select p;
            return query.ToList<Person>();
        }

        /// <summary>
        /// Updates the provided person in the inventory
        /// </summary>
        /// <param name="person">The person to be updated</param>
        /// <returns>The updated person</returns>
        public Person Update(Person person)
        {
            if (null == person)
            {
                log.Warn("null argument sent to Update");
                throw new ArgumentNullException("null argument sent to Update");
            }

            MasterEntities context = this.GetContext();

            try
            {
                if (System.Data.EntityState.Detached == person.EntityState
                    || System.Data.EntityState.Unchanged == person.EntityState)
                {
                    context.Attach(person);
                }

                // Update the state of the object to modified
                context.ObjectStateManager.ChangeObjectState(person, System.Data.EntityState.Modified);

                context.SaveChanges();
            }
            catch (Exception e)
            {
                log.Error("unable to update a person, received exception: ", e);
                throw;
            }

            return person;
        }

        /// <summary>
        /// Adds the provided person
        /// </summary>
        /// <param name="person">The person to add</param>
        /// <returns>The added person with the key generated from the service</returns>
        public Person Add(Person person)
        {
            if (null == person)
            {
                log.Warn("null argument sent to Add");
                throw new ArgumentNullException("null argument sent to Add");
            }

            MasterEntities context = this.GetContext();

            try
            {
                context.People.AddObject(person);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to add a person, received exception: " + e.ToString());
                log.Error("unable to add a person, received exception: ", e);
                throw;
            }

            return person;
        }

        /// <summary>
        /// Disposes of the context
        /// </summary>
        public void Dispose()
        {
            if (null != this.context)
            {
                this.context.Dispose();
            }
        }

        #endregion ICrudService

        /// <summary>
        /// get a new context only when it has not been previously valued
        /// otherwise returns the existing context.
        /// </summary>
        /// <returns>the cuurent context</returns>
        internal MasterEntities GetContext()
        {
            if (null == this.context)
            {
                this.context = new MasterEntities();
                this.context.ContextOptions.LazyLoadingEnabled = false;
            }

            return this.context;
        }

        /// <summary>
        /// check the values whuch are text fields, against the data base limits/
        /// If a value exceeds the limit, the value is truncated to the limit size.
        /// </summary>
        /// <param name="media">media item</param>
        private void CheckTextFieldLimits(Media media)
        {
            media.Comment = this.TruncateToLimit(media.Comment, TEXTLIMIT);
            media.Description = this.TruncateToLimit(media.Description, TEXTLIMIT);
            media.ImageUrl = this.TruncateToLimit(media.ImageUrl, TEXTLIMIT);
            media.Publisher = this.TruncateToLimit(media.Publisher, TEXTLIMIT);
            media.Title = this.TruncateToLimit(media.Title, TEXTLIMIT);

            if (media is Book)
            {
                Book book = (Book)media;
                book.ISBN = this.TruncateToLimit(book.ISBN, ISBNLIMIT);
                book.LibraryLocation = this.TruncateToLimit(book.LibraryLocation, TEXTLIMIT);
            }

            if (media is Video)
            {
                Video video = (Video)media;
                video.UPC = this.TruncateToLimit(video.UPC, UPCLIMIT);
            }
        }

        /// <summary>
        /// limit string to defined number of characters
        /// </summary>
        /// <param name="value"> the input value</param>
        /// <param name="limit"> tbhe max number of characters</param>
        /// <returns>a string which map have been truncated</returns>
        private string TruncateToLimit(string value, int limit)
        {
            string returnVal = null;
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