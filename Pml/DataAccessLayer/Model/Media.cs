namespace Sweng500.Pml.DataAccessLayer
{
    /// <summary>
    /// Contains the non-auto generated code for the Media object
    /// </summary>
    public partial class Media
    {
        /// <summary>
        /// Determines whether the media objects are the same
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>A value indicating whether the two objects are equal</returns>
        public override bool Equals(object obj)
        {
            bool rtn = false;

            if (obj is Media)
            {
                var otherMedia = (Media)obj;
                rtn = this.MediaId == otherMedia.MediaId;
            }

            return rtn;
        }

        /// <summary>
        /// Gets the hashcode for this object
        /// </summary>
        /// <returns>The hashcode for this object</returns>
        public override int GetHashCode()
        {
            return this.MediaId * 397;
        }
    }
}
