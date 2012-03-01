namespace Sweng500.Pml.DataAccessLayer
{
    using System;

    /// <summary>
    /// This is the overridden Video class
    /// </summary>
    public partial class Video : Media
    {        
        /// <summary>
        /// Initializes a new instance of the Video class.
        /// Defines default DateTime values
        /// </summary>
        public Video()
        {
            this.Acquired = DateTime.Now;
            this.Released = DateTime.Now;
        }
    }
}
