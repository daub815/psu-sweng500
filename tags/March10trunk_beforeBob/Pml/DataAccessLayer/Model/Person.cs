namespace Sweng500.Pml.DataAccessLayer
{
    /// <summary>
    /// Contains the non-auto generated code for the Person object
    /// </summary>
    public partial class Person
    {
        /// <summary>
        /// Determines whether the person objects are the same
        /// </summary>
        /// <param name="obj">The other object</param>
        /// <returns>A value indicating whether the two objects are equal</returns>
        public override bool Equals(object obj)
        {
            bool rtn = false;

            if (obj is Person)
            {
                var otherPerson = (Person)obj;
                rtn = this.PersonId == otherPerson.PersonId;
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
                return this.PersonId * 401;
            }
        }
    }
}
