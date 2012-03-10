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
                var directorassoc = this.DirectorAssociations;
                foreach (DirectorAssociation assoc in directorassoc)
                {
                    if (assoc.DirectorPersonId == person.PersonId)
                    {
                        rtn = true;
                    }
                }
            }
            else if (person is Producer)
            {
                var producerassoc = this.ProducerAssociations;
                foreach (ProducerAssociation assoc in producerassoc)
                {
                    if (assoc.ProducerPersonId == person.PersonId)
                    {
                        rtn = true;
                    }
                }
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

            if (null != person &&
                false == this.ContainsPerson(person))
            {
                if (person is Producer)
                {
                    ProducerAssociation assoc = new ProducerAssociation();
                    assoc.ProducerPersonId = person.PersonId;
                    assoc.VideoMediaId = this.MediaId;

                    this.ProducerAssociations.Add(assoc);
                    rtn = true;
                }
                else if (person is Director)
                {
                    DirectorAssociation assoc = new DirectorAssociation();
                    assoc.DirectorPersonId = person.PersonId;
                    assoc.VideoMediaId = this.MediaId;

                    this.DirectorAssociations.Add(assoc);
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
                    ProducerAssociation assocremoval = null;
                    var producerassoc = this.ProducerAssociations;
                    foreach (ProducerAssociation assoc in producerassoc)
                    {
                        if (assoc.ProducerPersonId == person.PersonId)
                        {
                            assocremoval = assoc;
                            rtn = true;
                        }
                    }

                    if (true == rtn)
                    {
                        this.ProducerAssociations.Remove(assocremoval);
                    }
                }
                else if (person is Director)
                {
                    DirectorAssociation assocremoval = null;
                    var directorassociations = this.DirectorAssociations;
                    foreach (DirectorAssociation assoc in directorassociations)
                    {
                        if (assoc.DirectorPersonId == person.PersonId)
                        {
                            assocremoval = assoc;
                            rtn = true;
                        }
                    }

                    if (true == rtn)
                    {
                        this.DirectorAssociations.Remove(assocremoval);
                    }
                }
            }

            return rtn;
        }

        /// <summary>
        /// Override the string representation
        /// </summary>
        /// <returns>A value representing the book state</returns>
        public override string ToString()
        {
            return string.Format("Title: {0}  Description: {1} Comment: {2}  Id: {3}", this.Title, this.Description, this.Comment, this.MediaId);
        }
    }
}
