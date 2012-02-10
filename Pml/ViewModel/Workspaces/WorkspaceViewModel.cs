namespace Sweng500.Pml.ViewModel.Workspaces
{
    using GalaSoft.MvvmLight;

    /// <summary>
    /// Base class for a workspace that provides basic functionality such as visibility
    /// </summary>
    public class WorkspaceViewModel : ViewModelBase
    {
        /// <summary>
        /// The property name of the Name property
        /// </summary>
        public const string NamePropertyName = "Name";

        /// <summary>
        /// The backing field of the Name property
        /// </summary>
        private string mName = string.Empty;

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
                    // Store the old value
                    var oldValue = this.mName;

                    // Set the new value
                    this.mName = value;

                    // Raise the event and let others know of the new and old values
                    this.RaisePropertyChanged(NamePropertyName);
                }
            }
        }
    }
}
