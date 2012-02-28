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

                foreach (var media in context.Media)
                {
                    context.Media.DeleteObject(media);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                string message = "Unable to clear the database after test.";
                message = message + "Received Exception: " + e.Message;

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
