namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System.ComponentModel;
    using System.Windows.Data;
    using log4net;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Provides the inventory functionality
    /// </summary>
    public class InventoryWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InventoryWorkspaceViewModel class
        /// </summary>
        public InventoryWorkspaceViewModel()
            : base("Inventory")
        {
            this.Media = new ListCollectionView(DataStore.Instance.MediaCollection);
            this.Media.CurrentChanged += (obj, args) => this.RaisePropertyChanged(SelectedMediaPropertyName);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the view of the media Collection
        /// </summary>
        public ICollectionView Media
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the selected media, which is just the currently selected on in the collectionview
        /// </summary>
        public override Media SelectedMedia
        {
            get
            {
                return (Media)this.Media.CurrentItem;
            }
        }

        #endregion Properties
    }
}
