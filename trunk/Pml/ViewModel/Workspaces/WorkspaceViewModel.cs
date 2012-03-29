namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Base class for a workspace that provides basic functionality such as visibility
    /// </summary>
    public class WorkspaceViewModel : ViewModelBase
    {
        #region Statics

        /// <summary>
        /// The property name of the Name property
        /// </summary>
        public const string NamePropertyName = "Name";

        /// <summary>
        /// The property name of the IsSelected property
        /// </summary>
        public const string IsSelectedPropertyName = "IsSelected";

        /// <summary>
        /// The property name of the IsOpen property
        /// </summary>
        public const string IsOpenPropertyName = "IsOpen";

        /// <summary>
        /// The property name of the SelectedItem property
        /// </summary>
        public const string SelectedItemPropertyName = "SelectedItem";

        /// <summary>
        /// The property name of the ErrorMessage property
        /// </summary>
        public const string ErrorMessagePropertyName = "ErrorMessage";

        /// <summary>
        /// The property name of the HasError property
        /// </summary>
        public const string HasErrorPropertyName = "Error";

        #endregion Statics

        #region Fields

        /// <summary>
        /// The backing field of the Name property
        /// </summary>
        private string mName = string.Empty;

        /// <summary>
        /// The backing field of the IsSelected property
        /// </summary>
        private bool mIsSelected = false;

        /// <summary>
        /// The backing field of the IsOpen property
        /// </summary>
        private bool mIsOpen = false;

        /// <summary>
        /// The backing field of the ErrorMessage property
        /// </summary>
        private string mErrorMessage = string.Empty;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the WorkspaceViewModel class
        /// </summary>
        public WorkspaceViewModel()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WorkspaceViewModel class
        /// </summary>
        /// <param name="workspaceName">The workspace name</param>
        public WorkspaceViewModel(string workspaceName)
        {
            this.Name = workspaceName;
            this.CloseWorkspaceCommand = new RelayCommand(
                () =>
                    {
                        this.IsOpen = false;
                    });
        }

        #endregion Constructors

        #region Commands

        /// <summary>
        /// Gets or sets a command to close the workspace
        /// </summary>
        public ICommand CloseWorkspaceCommand
        {
            get;
            protected set;
        }

        #endregion Commands

        #region Properties

        /// <summary>
        /// Gets or sets the name of the workspace
        /// </summary>
        public string Name
        {
            get
            {
                return this.mName;
            }

            set
            {
                if (false == string.Equals(value, this.mName, System.StringComparison.OrdinalIgnoreCase))
                {
                    this.mName = value;
                    this.RaisePropertyChanged(NamePropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this workspace is selected
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.mIsSelected;
            }

            set
            {
                if (value != this.IsSelected)
                {
                    this.mIsSelected = value;
                    this.RaisePropertyChanged(IsSelectedPropertyName);
                    this.RaisePropertyChanged(SelectedItemPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this workspace is open
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.mIsOpen;
            }

            set
            {
                if (value != this.IsOpen)
                {
                    this.mIsOpen = value;
                    this.RaisePropertyChanged(IsOpenPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets or sets the error message
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.mErrorMessage;
            }

            set
            {
                if (false == string.Equals(value, this.ErrorMessage))
                {
                    this.mErrorMessage = value;
                    this.RaisePropertyChanged(ErrorMessagePropertyName);
                    this.RaisePropertyChanged(HasErrorPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether there is an error
        /// </summary>
        public bool HasError
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ErrorMessage);
            }
        }

        /// <summary>
        /// Gets the selected media for this workspace
        /// </summary>
        public virtual object SelectedItem
        {
            get
            {
                return null;
            }
        }

        #endregion Properties
    }
}
