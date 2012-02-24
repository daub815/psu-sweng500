namespace Sweng500.Pml.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Sweng500.Pml.DataAccessLayer;
    using Sweng500.Pml.ViewModel.Workspaces;

    /// <summary>
    /// Contains the global commands for the application
    /// </summary>
    public class GlobalCommands : ViewModelBase
    {
        #region Statics

        /// <summary>
        /// Property name for the AddMediaItemCommand property
        /// </summary>
        public const string AddMediaItemCommandPropertyName = "AddMediaItemCommand";

        /// <summary>
        /// Property name for the EditMediaItemCommand property
        /// </summary>
        public const string EditMediaItemCommandPropertyName = "EditMediaItemCommand";

        /// <summary>
        /// Property name for the SelectWorkspaceCommand property
        /// </summary>
        public const string SelectWorkspaceCommandPropertyName = "SelectWorkspaceCommand";

        /// <summary>
        /// The singleton instance
        /// </summary>
        private static readonly GlobalCommands StaticInstance = new GlobalCommands();

        #endregion Statics

        #region Fields

        /// <summary>
        /// Backing field for the AddMediaItemCommand property
        /// </summary>
        private RelayCommand<MediaTypes> mAddMediaItemCommand;

        /// <summary>
        /// Backing field for the EditMediaItemCommand property
        /// </summary>
        private RelayCommand<Media> mEditMediaItemCommand;

        /// <summary>
        /// Backing field for the SelectWorkspaceCommand property
        /// </summary>
        private RelayCommand<WorkspaceViewModel> mSelectWorkspaceCommand;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the GlobalCommands class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Explicit static constructor to tell C# compiler not to mark type as beforefieldinit")]
        static GlobalCommands()
        {
        }

        /// <summary>
        /// Prevents a default instance of the GlobalCommands class from being created
        /// </summary>
        private GlobalCommands()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the GlobalCommands
        /// </summary>
        public static GlobalCommands Instance
        {
            get
            {
                return StaticInstance;
            }
        }

        /// <summary>
        /// Gets or sets the command to add a media item
        /// </summary>
        public RelayCommand<MediaTypes> AddMediaItemCommand
        {
            get
            {
                return this.mAddMediaItemCommand;
            }

            set
            {
                if (null == this.AddMediaItemCommand &&
                    null != value)
                {
                    this.mAddMediaItemCommand = value;
                    this.RaisePropertyChanged(AddMediaItemCommandPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to edit a media item
        /// </summary>
        public RelayCommand<Media> EditMediaItemCommand
        {
            get
            {
                return this.mEditMediaItemCommand;
            }

            set
            {
                if (null == this.EditMediaItemCommand &&
                    null != value)
                {
                    this.mEditMediaItemCommand = value;
                    this.RaisePropertyChanged(EditMediaItemCommandPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to select the workspace
        /// </summary>
        public RelayCommand<WorkspaceViewModel> SelectWorkspaceCommand
        {
            get
            {
                return this.mSelectWorkspaceCommand;
            }

            set
            {
                if (null == this.SelectWorkspaceCommand &&
                    null != value)
                {
                    this.mSelectWorkspaceCommand = value;
                    this.RaisePropertyChanged(SelectWorkspaceCommandPropertyName);
                }
            }
        }

        #endregion Properties

        /// <summary>
        /// Gets the singleton instance of the GlobalCommands
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static GlobalCommands GetInstance()
        {
            return Instance;
        }
    }
}
