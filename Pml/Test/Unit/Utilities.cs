namespace Sweng500.Pml.Test.Unit
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Utility class for test methods
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Clear all media objects out of the database
        /// </summary>
        public static void CleanDatabase()
        {
            MasterEntities context = null;

            try
            {
                context = new MasterEntities();

                foreach (var person in context.People)
                {
                    context.People.DeleteObject(person);
                }
               
                foreach (var media in context.Media)
                {
                    if (media is Book)
                    {
                        // clear the Authors for the book explicitly.
                        Book book = (Book)media;
                        book.AuthorBookAssociations.Clear();
                    }

                    if (media is Video)
                    {
                        // clear the directors and producers
                        Video video = (Video)media;
                        video.DirectorAssociations.Clear();
                        video.ProducerAssociations.Clear();
                    }

                    context.Media.DeleteObject(media);
                }
 
                context.SaveChanges();
            }
            catch (Exception e)
            {
                string message = "Unable to clear the database after test.";
                message = message + " Received Exception: " + e.Message;

                Assert.Fail(message);
            }
            finally
            {
                if (null != context)
                {
                    context.Dispose();
                }
            }
        }
    }
}
