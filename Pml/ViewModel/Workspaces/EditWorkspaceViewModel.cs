namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
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
        /// <param name="mediaType">The type of media type to edit</param>
        public EditWorkspaceViewModel(MediaTypes mediaType)
            : base("Add " + mediaType.ToString())
        {
            switch (mediaType)
            {
                case MediaTypes.Book:
                    this.MediaToEdit = new Book();
                    break;
                case MediaTypes.Video:
                    this.MediaToEdit = new Video();
                    break;
                default:
                    throw new ArgumentException("Invalid MediaType");
            }
        }

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="mediaToEdit">The element to be edited</param>
        public EditWorkspaceViewModel(Media mediaToEdit)
            : base("Edit " + mediaToEdit.GetType().Name + " " + mediaToEdit.MediaID)
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
