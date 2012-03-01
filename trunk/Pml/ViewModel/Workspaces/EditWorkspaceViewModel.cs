namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using log4net;
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

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
        /// <param name="workspaceName">The name of the workspace</param>
        public EditWorkspaceViewModel(string workspaceName)
            : base(workspaceName)
        {
            this.SaveCommand = new RelayCommand(
                () =>
                    {
                        var crudService = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

                        try
                        {
                            if (null == this.MediaToEdit.EntityKey)
                            {
                                this.MediaToEdit = crudService.Add(this.MediaToEdit);
                                DataStore.Instance.MediaCollection.Add(this.MediaToEdit);
                            }
                            else
                            {
                                this.MediaToEdit = crudService.Update(this.MediaToEdit);
                            }
                        }
                        catch (Exception e)
                        {
                            log.Error("Unable to save the media.", e);
                        }
                    });

            this.ResetCommand = new RelayCommand(
                () =>
                    {
                    });
        }

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="mediaType">The type of media type to edit</param>
        public EditWorkspaceViewModel(MediaTypes mediaType)
            : this("Add " + mediaType.ToString())
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
            : this("Edit " + mediaToEdit.GetType().Name + " " + mediaToEdit.MediaId)
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
                    this.RaisePropertyChanged(SelectedMediaPropertyName);
                }
            }
        }

        /// <summary>
        /// Gets the selected media, which is really just the media to edit
        /// </summary>
        public override Media SelectedMedia
        {
            get
            {
                return this.MediaToEdit;
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
