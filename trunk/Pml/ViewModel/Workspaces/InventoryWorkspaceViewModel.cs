namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System.Collections.ObjectModel;
    using Sweng500.Pml.DataAccessLayer;
using System.ComponentModel;
    using System.Windows.Data;

    /// <summary>
    /// Provides the inventory functionality
    /// </summary>
    public class InventoryWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        #endregion Statics

        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InventoryWorkspaceViewModel class
        /// </summary>
        public InventoryWorkspaceViewModel()
            : base("Inventory")
        {
            this.Media = new ListCollectionView(DataStore.Instance.MediaCollection);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the view of the media Collection
        /// </summary>
        public ICollectionView Media
        {
            get;
            protected set;
        }

        #endregion Properties
    }
}
