namespace Sweng500.Pml.ViewModel.Workspaces
{
    using GalaSoft.MvvmLight;

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

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the WorkspaceViewModel class
        /// </summary>
        public WorkspaceViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the WorkspaceViewModel class
        /// </summary>
        /// <param name="workspaceName">The workspace name</param>
        public WorkspaceViewModel(string workspaceName)
        {
            this.Name = workspaceName;
        }

        #endregion Constructors

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
                }
            }
        }

        #endregion Properties
    }
}
