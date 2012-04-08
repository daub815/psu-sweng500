namespace Sweng500.Pml.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;
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
            var crud = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

            var items = crud.GetMediaItems();
            this.MediaCollection = new ObservableCollection<Media>(items);

            var people = crud.GetPeople();
            this.PersonCollection = new ObservableCollection<Person>(people);
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

        /// <summary>
        /// Gets or sets the collection of people
        /// </summary>
        public ObservableCollection<Person> PersonCollection
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the collection of authors
        /// </summary>
        public ObservableCollection<Author> AuthorCollection
        {
            get
            {
                return new ObservableCollection<Author>(this.PersonCollection.OfType<Author>().Cast<Author>());
            }
        }

        /// <summary>
        /// Gets the collection of directors
        /// </summary>
        public ObservableCollection<Director> DirectorCollection
        {
            get
            {
                return new ObservableCollection<Director>(this.PersonCollection.OfType<Director>().Cast<Director>());
            }
        }

        /// <summary>
        /// Gets the collection of producers
        /// </summary>
        public ObservableCollection<Producer> ProducerCollection
        {
            get
            {
                return new ObservableCollection<Producer>(this.PersonCollection.OfType<Producer>().Cast<Producer>());
            }
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
