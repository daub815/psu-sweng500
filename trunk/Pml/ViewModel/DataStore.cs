namespace Sweng500.Pml.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using GalaSoft.MvvmLight;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Centralized repository for all view model data
    /// </summary>
    public class DataStore : ViewModelBase
    {
        #region Statics

        /// <summary>
        /// The singleton instance
        /// </summary>
        private static readonly DataStore StaticInstance = new DataStore();

        #endregion Statics

        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the DataStore class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Explicit static constructor to tell C# compiler not to mark type as beforefieldinit")]
        static DataStore()
        {
        }

        /// <summary>
        /// Prevents a default instance of the DataStore class from being created
        /// </summary>
        private DataStore()
        {
            this.MediaCollection = new ObservableCollection<Media>();

            this.MediaCollection.Add(
                new Book
                {
                    Acquired = DateTime.Now,
                    IsBorrowable = true,
                    IsBorrowed = true,
                    Title = "This is a title"
                });

            this.MediaCollection.Add(
                new Video
                {
                    Acquired = DateTime.Now,
                    IsBorrowable = true,
                    IsBorrowed = true,
                    Title = "This is a title"
                });
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the DataStore
        /// </summary>
        public static DataStore Instance
        {
            get
            {
                return StaticInstance;
            }
        }

        /// <summary>
        /// Gets or sets the collection of media elements
        /// </summary>
        public ObservableCollection<Media> MediaCollection
        {
            get;
            protected set;
        }

        #endregion Properties

        /// <summary>
        /// Gets the singleton instance of the DataStore
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static DataStore GetInstance()
        {
            return Instance;
        }
    }
}
