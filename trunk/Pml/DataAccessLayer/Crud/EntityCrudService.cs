namespace Sweng500.Pml.DataAccessLayer
{
    using System;
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
                log.Error("unable to update a media item.  received exception: ", e);
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

            if (media is Video)
            {
                Video videoItem = (Video)media;
                context.Media.AddObject(videoItem);
            }

            if (media is Book)
            {
                Book bookitem = (Book)media;
                foreach (Person aperson in bookitem.PeopleToAdd)
                {
                    if (aperson.LastName.Length > 0
                        && aperson.FirstName.Length > 0)
                    {
                        context.People.AddObject(aperson);
                        context.SaveChanges();
                        AuthorBookAssociation association = new AuthorBookAssociation();
                        association.AuthorPersonId = aperson.PersonId;
                        association.BookMediaId = media.MediaId;
                        bookitem.AuthorBookAssociations.Add(association);
                    }
                }

                context.Media.AddObject(bookitem);
            }
 
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("unable to add a media item.  received exception: " + e.ToString());
                log.Error("unable to add a media item.  received exception: ", e);
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
                log.Warn("media item sent to add that does not have entity key");
                throw new ArgumentException("media item sent to add that does not have entity key");
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
                    var associations = context.AuthorBookAssociations;
                    Book book = (Book)media;
                }

                context.Media.DeleteObject(media);
                context.SaveChanges();
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
            var query = from c in context.People select c;
            return query.ToList<Person>();
        }

        /// <summary>
        /// gets the list of Authors currently defined
        /// </summary>
        /// <returns>Gets a list of people defined as authors</returns>
        public IEnumerable<Person> GetAuthors()
        {
            MasterEntities context = this.GetContext();
            var query = from p in context.People.OfType<Author>() select p;
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
                context.Attach(person);

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

        #endregion ICrudService
    }
}
