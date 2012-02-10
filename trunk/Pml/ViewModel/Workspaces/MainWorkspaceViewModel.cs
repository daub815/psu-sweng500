namespace Sweng500.Pml.ViewModel.Workspaces
{
    /// <summary>
    /// Contains the main workspace that controls all the children workspaces
    /// </summary>
    public class MainWorkspaceViewModel : WorkspaceViewModel
    {
        /// <summary>
        /// The property name for the TempEditor property
        /// </summary>
        public const string TempEditorPropertyName = "TempEditor";

        /// <summary>
        /// The backing field for the TempEditor property
        /// </summary>
        private EditWorkspaceViewModel mTempEditor = new EditWorkspaceViewModel();

        /// <summary>
        /// Initializes a new instance of the MainWorkspaceViewModel class
        /// </summary>
        public MainWorkspaceViewModel()
        {
            this.Name = "Personal Media Library";
        }

        /// <summary>
        /// Gets or sets the temporary editor
        /// </summary>
        public EditWorkspaceViewModel TempEditor
        {
            get
            {
                return this.mTempEditor;
            }

            set
            {
                var oldValue = this.mTempEditor;
                this.mTempEditor = value;
                this.RaisePropertyChanged(TempEditorPropertyName, oldValue, value, true);
            }
        }
    }
}
