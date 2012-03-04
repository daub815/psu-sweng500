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

        /// <summary>
        /// Provides a method to determine if this person is associated with the Directors or Producers collection of this book
        /// </summary>
        /// <param name="person">The person to check</param>
        /// <returns>A value indicating if this person is associated with the Directors or Producers collection of this book</returns>
        public override bool ContainsPerson(Person person)
        {
            bool rtn = false;

            if (person is Director)
            {
                rtn = this.Directors.Contains((Director)person);
            }
            else if (person is Producer)
            {
                rtn = this.Producers.Contains((Producer)person);
            }

            return rtn;
        }

        /// <summary>
        /// Provides a way to add a person depending on the type of person to the correct collection
        /// </summary>
        /// <param name="person">The Person to add</param>
        /// <returns>A value indicating if it was successfully added to a collection</returns>
        public override bool AddPerson(Person person)
        {
            bool rtn = false;

            if (false == this.ContainsPerson(person))
            {
                if (person is Producer)
                {
                    this.Producers.Add((Producer)person);
                    rtn = true;
                }
                else if (person is Director)
                {
                    this.Directors.Add((Director)person);
                    rtn = true;
                }
            }

            return rtn;
        }

        /// <summary>
        /// Provides a way to remove a person depending on the type of person from the correct collection
        /// </summary>
        /// <param name="person">The person to remove</param>
        /// <returns>A value indicating whether it was successfully found and removed from the collection</returns>
        public override bool RemovePerson(Person person)
        {
            bool rtn = false;

            if (true == this.ContainsPerson(person))
            {
                if (person is Producer)
                {
                    rtn = this.Producers.Remove((Producer)person);
                }
                else if (person is Director)
                {
                    rtn = this.Directors.Remove((Director)person);
                }
            }

            return rtn;
        }
    }
}
