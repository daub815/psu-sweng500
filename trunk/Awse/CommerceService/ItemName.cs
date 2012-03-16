namespace Sweng500.Awse.CommerceService
{
    /// <summary>
    /// simple name class with first and last name
    /// </summary>
    public class ItemName
    {
        /// <summary>
        /// Initializes a new instance of the ItemName class
        /// </summary>
        /// <param name="namestring"> the name returned from the search</param>
        public ItemName(string namestring)
        {
            this.First = string.Empty;
            this.Last = string.Empty;

            if (!string.IsNullOrEmpty(namestring))
            {
                string[] words = namestring.Split(' ');
                if (words.Length == 1)
                {
                    this.First = string.Empty;
                    this.Last = words[0];
                }
            
                if (words.Length >= 2)
                {
                    this.First = words[0];
                    this.Last = words[words.Length - 1];
                }
            }
        }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public string First
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public string Last
        {
            get;
            set;
        }
    }
}
