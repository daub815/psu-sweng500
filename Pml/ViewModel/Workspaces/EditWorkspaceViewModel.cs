namespace Sweng500.Pml.ViewModel.Workspaces
{
    using Sweng500.Pml.DataAccessLayer;
    /// <summary>
    /// Provides a workspace to add/edit a media element
    /// </summary>
    public class EditWorkspaceViewModel : WorkspaceViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        public EditWorkspaceViewModel()
            : this(new Media())
        {
        }

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="mediaToEdit"></param>
        public EditWorkspaceViewModel(Media mediaToEdit)
            : base("Edit " + mediaToEdit.MediaID)
        {
        }

        #endregion Constructors
    }
}
