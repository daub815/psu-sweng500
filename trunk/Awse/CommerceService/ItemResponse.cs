namespace Sweng500.Awse.CommerceService
{
    using System;

    /// <summary>
    /// class to hold a subset of ItemAttributes for one item returned from item search
    /// </summary>
    public class ItemResponse
    {
        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the publisher
        /// </summary>
        public string Publisher
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ISBN
        /// </summary>
        public string Isbn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Electronic ISBN
        /// </summary>
        public string[] Eisbn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the publication date
        /// </summary>
        public DateTime Publicationdate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the UPC
        /// </summary>
        public string Upc
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the genre
        /// </summary>
        public string Genre
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the product group returned by the search
        /// </summary>
        public string Productgroup
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the release date returned by the search
        /// </summary>
        public DateTime Releasedate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the the author names returned by the search
        /// </summary>
        public ItemName[] Authorsname
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the director names returned by the search
        /// </summary>
        public ItemName[] Directorsname
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the producer names returned by the search
        /// </summary>
        public ItemName[] Producersname
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the format names returned by the search
        /// </summary>
        public string[] Formats
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image url returned by the search
        /// </summary>
        public string Imageurl
        {
            get;
            set;
        }
    }
}
