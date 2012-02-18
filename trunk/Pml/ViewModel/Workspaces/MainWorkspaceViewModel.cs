namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using Sweng500.Pml.DataAccessLayer;

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

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWorkspaceViewModel class
        /// </summary>
        public MainWorkspaceViewModel()
            : base("Personal Media Library")
        {
            this.Workspaces = new ObservableCollection<WorkspaceViewModel>();
            this.Workspaces.CollectionChanged += (obj, args) =>
                {
                    switch (args.Action)
                    {
                        case NotifyCollectionChangedAction.Add:
                            this.OnWorkspacesAdded(args.NewItems.Cast<WorkspaceViewModel>());
                            break;
                        case NotifyCollectionChangedAction.Remove:
                            this.OnWorkspacesRemoved(args.OldItems.Cast<WorkspaceViewModel>());
                            break;
                        case NotifyCollectionChangedAction.Replace:
                            this.OnWorkspacesAdded(args.NewItems.Cast<WorkspaceViewModel>());
                            this.OnWorkspacesRemoved(args.OldItems.Cast<WorkspaceViewModel>());
                            break;
                        default:
                            // There are other actions, but we don't need to worry about them
                            break;
                    };
                };

            GlobalCommands.Instance.AddMediaItemCommand = new GalaSoft.MvvmLight.Command.RelayCommand<MediaTypes>(
                mediaType =>
                    {
                        // Create and add the new workspace
                        var workspace = new EditWorkspaceViewModel(mediaType);
                        workspace.IsOpen = true;
                        this.Workspaces.Add(workspace);

                        // Make the workspace selected
                        GlobalCommands.Instance.SelectWorkspaceCommand.Execute(workspace);
                    });

            GlobalCommands.Instance.EditMediaItemCommand = new GalaSoft.MvvmLight.Command.RelayCommand<Media>(
                media => 
                    {
                        var workspace = new EditWorkspaceViewModel(media);

                        // Add the workspace if it's not already in the list
                        if (false == this.Workspaces.Any(w => w.Name == workspace.Name))
                        {
                            workspace.IsOpen = true;
                            this.Workspaces.Add(workspace);
                        }

                        // Make the workspace selected
                        GlobalCommands.Instance.SelectWorkspaceCommand.Execute(workspace); 
                    });

            GlobalCommands.Instance.SelectWorkspaceCommand = new GalaSoft.MvvmLight.Command.RelayCommand<WorkspaceViewModel>(
                workspace =>
                    {
                        // No need to swap the values
                        if (this.SelectedWorkspace != workspace)
                        {
                            // Swap the values
                            this.SelectedWorkspace.IsSelected = false;
                            workspace.IsSelected = true;
                        }
                    });

            // Add the inventory workspace
            var inventoryWorkspace = new InventoryWorkspaceViewModel
            {
                IsOpen = true,
                IsSelected = true
            };

            this.Workspaces.Add(inventoryWorkspace);
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

        /// <summary>
        /// Gets the selected workspace
        /// </summary>
        public WorkspaceViewModel SelectedWorkspace
        {
            get
            {
                var workspace = this.Workspaces.FirstOrDefault((w) => true == w.IsSelected);

                // Gets the first workspace if there wasn't a selected one and make that selected
                if (null == workspace)
                {
                    workspace = this.Workspaces.LastOrDefault();
                    if (null != workspace)
                    {
                        workspace.IsSelected = true;
                    }
                }

                return workspace;
            }
        }

        #endregion Properties

        /// <summary>
        /// When workspaces are added, we will listen to their properties change
        /// </summary>
        /// <param name="workspaces">The added workspaces</param>
        protected void OnWorkspacesAdded(IEnumerable<WorkspaceViewModel> workspaces)
        {
            foreach (var workspace in workspaces)
            {
                workspace.PropertyChanged += this.OnWorkspacePropertyChanged;
            }
        }

        /// <summary>
        /// When workspaces are removed we will remove our listener
        /// </summary>
        /// <param name="workspaces">The removed workspaces</param>
        protected void OnWorkspacesRemoved(IEnumerable<WorkspaceViewModel> workspaces)
        {
            foreach (var workspace in workspaces)
            {
                workspace.PropertyChanged -= this.OnWorkspacePropertyChanged;
            }
        }

        /// <summary>
        /// Update the main workspace when a workspace changes
        /// </summary>
        /// <param name="source">The source workspace</param>
        /// <param name="args">The property changes</param>
        protected void OnWorkspacePropertyChanged(object source, PropertyChangedEventArgs args)
        {
            if (source is WorkspaceViewModel)
            {
                var workspace = (WorkspaceViewModel)source;

                // Perform an action depending on which property changed
                if (args.PropertyName == WorkspaceViewModel.IsOpenPropertyName)
                {
                    if (false == workspace.IsOpen)
                    {
                        // Only workspaces to be removed if it's not the last one
                        if (this.Workspaces.Count > 1)
                        {
                            this.Workspaces.Remove(workspace);
                        }
                    }
                }
                else if (args.PropertyName == WorkspaceViewModel.IsSelectedPropertyName)
                {
                    if (true == workspace.IsSelected)
                    {
                        // Alert listeners that we have a new selected workspace
                        this.RaisePropertyChanged(SelectedWorkspacePropertyName);
                    }
                }
            }
        }
    }
}
