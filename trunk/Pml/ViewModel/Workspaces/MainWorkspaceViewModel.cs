namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Contains the main workspace that controls all the children workspaces
    /// </summary>
    public class MainWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Property name for the SelectedWorkspace property
        /// </summary>
        public const string SelectedWorkspacePropertyName = "SelectedWorkspace";

        #endregion Statics

        #region Fields

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWorkspaceViewModel class
        /// </summary>
        public MainWorkspaceViewModel()
            : base("Personal Media Library")
        {
            this.Workspaces = new ObservableCollection<WorkspaceViewModel>
            {
                new InventoryWorkspaceViewModel()
            };

            GlobalCommands.Instance.EditMediaItemCommand = new GalaSoft.MvvmLight.Command.RelayCommand<DataAccessLayer.Media>(
                (media) =>
                {
                    this.Workspaces.Add(new EditWorkspaceViewModel(media));
                });
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a collection of workspaces
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get;
            protected set;
        }

        #endregion Properties
    }
}
