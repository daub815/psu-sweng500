namespace Sweng500.Pml.ViewModel.Workspaces
{
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Provides a workspace to add/edit a media element
    /// </summary>
    public class EditWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Property name for the MediaToEdit property
        /// </summary>
        public const string MediaToEditPropertyName = "MediaToEdit";

        #endregion Statics

        #region Fields

        /// <summary>
        /// Backing field for the MediaToEdit property
        /// </summary>
        private Media mMediaToEdit = null;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        public EditWorkspaceViewModel()
            : this(new Book())
        {
        }

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="mediaToEdit">The element to be edited</param>
        public EditWorkspaceViewModel(Media mediaToEdit)
            : base("Edit " + mediaToEdit.MediaID)
        {
            this.MediaToEdit = mediaToEdit;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the media to edit
        /// </summary>
        public Media MediaToEdit
        {
            get
            {
                return this.mMediaToEdit;
            }

            protected set
            {
                if (this.MediaToEdit != value)
                {
                    this.mMediaToEdit = value;
                    this.RaisePropertyChanged(MediaToEditPropertyName);
                }
            }
        }

        #endregion Properties
    }
}
