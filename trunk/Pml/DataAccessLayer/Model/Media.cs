namespace Sweng500.Pml.DataAccessLayer
{
    /// <summary>
    /// Contains the non-auto generated code for the Media object
    /// </summary>
    public partial class Media
    {
        /// <summary>
        /// Static lock for the static id
        /// </summary>
        private static readonly object StaticIdLock = new object();

        /// <summary>
        /// Static id used to provide a unique id for new items
        /// </summary>
        private static volatile int StaticId = 0;

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
            unchecked
            {
                return this.MediaId * 397;
            }
        }

        /// <summary>
        /// Provides a method to determine if this person is associated with this Media object
        /// </summary>
        /// <param name="person">The person to check</param>
        /// <returns>A value indicating if this person is associated with this Media object</returns>
        public abstract bool ContainsPerson(Person person);

        /// <summary>
        /// Provides a way to add a person depending on the type of person to the correct collection
        /// </summary>
        /// <param name="person">The Person to add</param>
        /// <returns>A value indicating if it was successfully added to a collection</returns>
        public abstract bool AddPerson(Person person);

        /// <summary>
        /// Provides a way to remove a person depending on the type of person from the correct collection
        /// </summary>
        /// <param name="person">The person to remove</param>
        /// <returns>A value indicating whether it was successfully found and removed from the collection</returns>
        public abstract bool RemovePerson(Person person);

        /// <summary>
        /// Gets a new media id
        /// </summary>
        /// <returns>A new media id</returns>
        protected static int GetNewMediaId()
        {
            lock (StaticIdLock)
            {
                --StaticId;
            }

            return StaticId;
        }
    }
}
