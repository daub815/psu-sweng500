namespace Sweng500.Pml.ViewModel.Workspaces
{
    using GalaSoft.MvvmLight.Command;
    using log4net;

    /// <summary>
    /// Provides a workspace to add/edit an element
    /// </summary>
    public class EditWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Property name for the ItemToEdit property
        /// </summary>
        public const string ItemToEditPropertyName = "ItemToEdit";

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Fields

        /// <summary>
        /// Backing field for the ItemToEdit property
        /// </summary>
        private object mItemToEdit = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="workspaceName">The name of the workspace</param>
        protected EditWorkspaceViewModel(string workspaceName)
            : base(workspaceName)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the media to edit
        /// </summary>
        public object ItemToEdit
        {
            get
            {
                return this.mItemToEdit;
            }

            protected set
            {
                if (this.ItemToEdit != value)
                {
                    this.mItemToEdit = value;
                    this.RaisePropertyChanged(ItemToEditPropertyName);
                    this.RaisePropertyChanged(SelectedItemPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets the selected item, which is really just the item to edit
        /// </summary>
        public override object SelectedItem
        {
            get
            {
                return this.ItemToEdit; 
            }
        }

        #endregion Properties

        #region Commands

        /// <summary>
        /// Gets or sets a command to save the media item
        /// </summary>
        public RelayCommand SaveCommand
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a command to reset the media item
        /// </summary>
        public RelayCommand ResetCommand
        {
            get;
            protected set;
        }

        #endregion Commands
    }
}
