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
        /// Property name for the AddPersonCommand property
        /// </summary>
        public const string AddPersonCommandPropertyName = "AdddPersonCommand";

        /// <summary>
        /// Property name for the EditItemCommand property
        /// </summary>
        public const string EditItemCommandPropertyName = "EditItemCommand";

        /// <summary>
        /// Property name for the DeleteItemCommand property
        /// </summary>
        public const string DeleteItemCommandPropertyName = "DeleteItemCommand";

        /// <summary>
        /// Property name for the SelectWorkspaceCommand property
        /// </summary>
        public const string SelectWorkspaceCommandPropertyName = "SelectWorkspaceCommand";

        /// <summary>
        /// Property name for the SearchInventoryCommand property
        /// </summary>
        public const string SearchInventoryCommandPropertyName = "SearchInventoryCommand";

        /// <summary>
        /// Property name for the SearchRemoteUsingTitleCommand property
        /// </summary>
        public const string SearchRemoteUsingTitleCommandPropertyName = "SearchRemoteUsingTitleCommand";

        /// <summary>
        /// Property name for the SearchRemoteUsingKeywordsCommand property
        /// </summary>
        public const string SearchRemoteUsingKeywordsCommandPropertyName = "SearchRemoteUsingKeywordsCommand";

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
        /// Backing field for the AddPersonCommand property
        /// </summary>
        private RelayCommand<PersonTypes> mAddPersonCommand;

        /// <summary>
        /// Backing field for the EditItemCommand property
        /// </summary>
        private RelayCommand<object> mEditItemCommand;

        /// <summary>
        /// Backing field for the DeleteItemCommand property
        /// </summary>
        private RelayCommand<object> mDeleteItemCommand;

        /// <summary>
        /// Backing field for the SelectWorkspaceCommand property
        /// </summary>
        private RelayCommand<WorkspaceViewModel> mSelectWorkspaceCommand;

        /// <summary>
        /// Backing field for the SearchInventoryCommand property
        /// </summary>
        private RelayCommand<string> mSearchInventoryCommand;

        /// <summary>
        /// Backing field for the SearchRemoteUsingTitleCommand property
        /// </summary>
        private RelayCommand<string> mSearchRemoteUsingTitleCommand;

        /// <summary>
        /// Backing field for the SearchRemoteUsingKeywordsCommand property
        /// </summary>
        private RelayCommand<string> mSearchRemoteUsingKeywordsCommand;

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
        /// Gets or sets the command to add a person
        /// </summary>
        public RelayCommand<PersonTypes> AddPersonCommand
        {
            get
            {
                return this.mAddPersonCommand;
            }

            set
            {
                if (null == this.AddPersonCommand &&
                    null != value)
                {
                    this.mAddPersonCommand = value;
                    this.RaisePropertyChanged(AddPersonCommandPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to edit a media item
        /// </summary>
        public RelayCommand<object> EditItemCommand
        {
            get
            {
                return this.mEditItemCommand;
            }

            set
            {
                if (null == this.EditItemCommand &&
                    null != value)
                {
                    this.mEditItemCommand = value;
                    this.RaisePropertyChanged(EditItemCommandPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to delete a media item
        /// </summary>
        public RelayCommand<object> DeleteItemCommand
        {
            get
            {
                return this.mDeleteItemCommand;
            }

            set
            {
                if (null == this.DeleteItemCommand &&
                    null != value)
                {
                    this.mDeleteItemCommand = value;
                    this.RaisePropertyChanged(DeleteItemCommandPropertyName);
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

        /// <summary>
        /// Gets or sets the command to search the inventory
        /// </summary>
        public RelayCommand<string> SearchInventoryCommand
        {
            get
            {
                return this.mSearchInventoryCommand;
            }

            set
            {
                if (null == this.SearchInventoryCommand &&
                    null != value)
                {
                    this.mSearchInventoryCommand = value;
                    this.RaisePropertyChanged(SearchInventoryCommandPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to search remotely by title
        /// </summary>
        public RelayCommand<string> SearchRemoteUsingTitleCommand
        {
            get
            {
                return this.mSearchRemoteUsingTitleCommand;
            }

            set
            {
                if (null == this.SearchRemoteUsingTitleCommand &&
                    null != value)
                {
                    this.mSearchRemoteUsingTitleCommand = value;
                    this.RaisePropertyChanged(SearchRemoteUsingTitleCommandPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to search remotely by keywords
        /// </summary>
        public RelayCommand<string> SearchRemoteUsingKeywordsCommand
        {
            get
            {
                return this.mSearchRemoteUsingKeywordsCommand;
            }

            set
            {
                if (null == this.SearchRemoteUsingKeywordsCommand &&
                    null != value)
                {
                    this.mSearchRemoteUsingKeywordsCommand = value;
                    this.RaisePropertyChanged(SearchRemoteUsingKeywordsCommandPropertyName);
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
