namespace Sweng500.Pml.ViewModel
{
using System;

    
    /// <summary>
    /// Temporary class
    /// </summary>
    public class TMedia
    {     
        /// <summary>
        /// Initializes a new instance of the TempMedia class.
        /// </summary>
        public TMedia()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TempMedia class.
        /// </summary>
        public TMedia(string title, int mediaId, string description)
        {
            this.Title = title;
            this.MediaId = mediaId;
            this.Publisher = "unknown";
            this.PubDate = DateTime.UtcNow;
            this.Borrowable = false;
            this.MediaDescription = description;
        }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MediaId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PubDate { get; set; }

        /// <summary>
        /// Gets or sets the value which includes a general description
        /// </summary>
        public string MediaDescription { get; set; }


        /// <summary>
        /// Gets or sets the value which indicates if the media can be borrowed
        /// </summary>
        public bool Borrowable { get; set; }

    }
}
