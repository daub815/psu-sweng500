namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Windows.Data;
    using log4net;
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

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Fields

        /// <summary>
        /// The backing collection of workspaces
        /// </summary>
        private ObservableCollection<WorkspaceViewModel> mWorkspaces = new ObservableCollection<WorkspaceViewModel>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainWorkspaceViewModel class
        /// </summary>
        public MainWorkspaceViewModel()
            : base("Personal Media Library")
        {
            this.mWorkspaces.CollectionChanged += (obj, args) =>
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

            this.Workspaces = new ListCollectionView(this.mWorkspaces);
            this.Workspaces.CurrentChanged += (obj, args) =>
                {
                    foreach (var workspace in this.mWorkspaces)
                    {
                        workspace.IsSelected = false;
                    }

                    if (null != this.SelectedWorkspace)
                    {
                        this.SelectedWorkspace.IsSelected = true;
                    }
                    else if (this.Workspaces.MoveCurrentToLast())
                    {
                        this.SelectedWorkspace.IsSelected = true;
                    }
                };

            GlobalCommands.Instance.AddMediaItemCommand = new GalaSoft.MvvmLight.Command.RelayCommand<MediaTypes>(
                mediaType =>
                    {
                        // Create and add the new workspace
                        var workspace = new EditMediaWorkspaceViewModel(mediaType);
                        workspace.IsOpen = true;
                        this.mWorkspaces.Add(workspace);

                        // Make the workspace selected
                        GlobalCommands.Instance.SelectWorkspaceCommand.Execute(workspace);
                    });

            GlobalCommands.Instance.AddPersonCommand = new GalaSoft.MvvmLight.Command.RelayCommand<PersonTypes>(
                personType =>
                    {
                        // Create and add the new workspace
                        var workspace = new EditPersonWorkspaceViewModel(personType);
                        workspace.IsOpen = true;
                        this.mWorkspaces.Add(workspace);

                        // Make the workspace selected
                        GlobalCommands.Instance.SelectWorkspaceCommand.Execute(workspace);
                    });

            GlobalCommands.Instance.EditItemCommand = new GalaSoft.MvvmLight.Command.RelayCommand<object>(
                obj => 
                    {
                        WorkspaceViewModel workspace = null;

                        if (obj is Media)
                        {
                            workspace = new EditMediaWorkspaceViewModel((Media)obj);
                        }
                        else if (obj is Person)
                        {
                            workspace = new EditPersonWorkspaceViewModel((Person)obj);
                        }
                        else
                        {
                            throw new ArgumentException("Invalid item to edit");
                        }

                        // Add the workspace if it's not already in the list
                        if (false == this.mWorkspaces.Any(w => w.Name == workspace.Name))
                        {
                            workspace.IsOpen = true;
                            this.mWorkspaces.Add(workspace);
                        }

                        // Make the workspace selected
                        GlobalCommands.Instance.SelectWorkspaceCommand.Execute(workspace); 
                    },
                media =>
                    {
                        return
                            null != media &&
                            false == (this.SelectedWorkspace is EditWorkspaceViewModel);
                    });

            GlobalCommands.Instance.DeleteItemCommand = new GalaSoft.MvvmLight.Command.RelayCommand<object>(
                obj =>
                    {
                        var crudService = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

                        try
                        {
                            if (obj is Media)
                            {
                                crudService.Delete((Media)obj);
                                DataStore.Instance.MediaCollection.Remove((Media)obj);
                            }
                            else if (obj is Person)
                            {
                                //// TODO: Add delete of a person
                                //// crudService.Delete((Person)obj);
                                DataStore.Instance.PersonCollection.Remove((Person)obj);
                            }
                            else
                            {
                                throw new ArgumentException("Invalid item to edit");
                            }
                        }
                        catch (Exception e)
                        {
                            log.Error("Unable to delete the media", e);
                        }
                    },
                obj =>
                    {
                        return
                            null != obj &&
                            obj is EntityObject &&
                            null != ((EntityObject)obj).EntityKey;
                    });

            GlobalCommands.Instance.SelectWorkspaceCommand = new GalaSoft.MvvmLight.Command.RelayCommand<WorkspaceViewModel>(
                workspace =>
                    {
                        if (this.SelectedWorkspace != workspace)
                        {
                            this.Workspaces.MoveCurrentToPosition(this.mWorkspaces.IndexOf(workspace));
                        }
                    });

            // Add the inventory workspace
            var inventoryWorkspace = new InventoryWorkspaceViewModel
            {
                IsOpen = true
            };

            this.mWorkspaces.Add(inventoryWorkspace);
            GlobalCommands.Instance.SelectWorkspaceCommand.Execute(inventoryWorkspace);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets collection view of workspaces
        /// </summary>
        public ICollectionView Workspaces
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
                return (WorkspaceViewModel)this.Workspaces.CurrentItem;
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
                        if (this.mWorkspaces.Count > 1)
                        {
                            this.mWorkspaces.Remove(workspace);
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
