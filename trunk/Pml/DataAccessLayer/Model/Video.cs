namespace Sweng500.Pml.DataAccessLayer
{
    using System;

   /// <summary>
    /// This is the overridden Video class
    /// </summary>
    public partial class Video : Media
    {
        /// <summary>
        /// defines a dafault DateTime
        /// </summary>
        public static readonly DateTime DEFAULTDATETIME = new DateTime(2011, 12, 31, 23, 59, 59);
        
        /// <summary>
        /// Initializes a new instance of the Video class.
        /// Defines default DateTime values
        /// </summary>
        public Video()
            {
                this.Acquired = DEFAULTDATETIME;
                this.Released = DEFAULTDATETIME;
            }
    }
}
