namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using log4net;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Provides a workspace to add/edit an element
    /// </summary>
    public class EditWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Property name for the ItemToEdit property
        /// </summary>
        public const string ItemToEditPropertyName = "ItemToEdit";

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Fields

        /// <summary>
        /// Backing field for the ItemToEdit property
        /// </summary>
        private object mItemToEdit = null;

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
                            if (null == ((Media)this.ItemToEdit).EntityKey)
                            {
                                this.ItemToEdit = crudService.Add((Media)this.ItemToEdit);
                                DataStore.Instance.MediaCollection.Add((Media)this.ItemToEdit);
                            }
                            else
                            {
                                this.ItemToEdit = crudService.Update((Media)this.ItemToEdit);
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
                        if (null != ((Media)this.ItemToEdit).EntityKey)
                        {
                            int index = DataStore.Instance.MediaCollection.IndexOf((Media)this.ItemToEdit);
                            if (-1 != index)
                            {
                                this.ItemToEdit = DataStore.Instance.MediaCollection[index];

                                //// TODO: Raise string.Empty for all properties of ItemToEdit
                            }
                        }
                    },
                () =>
                    {
                        return
                            null != ((Media)this.ItemToEdit).EntityKey;
                    });

            this.AddPersonCommand = new RelayCommand<Person>(
                (person) =>
                    {
                        if (false == ((Media)this.ItemToEdit).AddPerson(person))
                        {
                            throw new ArgumentException("Unable to remove the person");
                        }
                    },
                (person) =>
                    {
                        return 
                            null != person &&
                            !((Media)this.ItemToEdit).ContainsPerson(person);
                    });

            this.RemovePersonCommand = new RelayCommand<Person>(
                (person) =>
                    {
                        if (false == ((Media)this.ItemToEdit).RemovePerson(person))
                        {
                            throw new ArgumentException("Unable to remove the person");
                        }
                    },
                (person) =>
                    {
                        return 
                            null != person &&
                            ((Media)this.ItemToEdit).ContainsPerson(person);
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
                    this.ItemToEdit = new Book();
                    break;
                case MediaTypes.Video:
                    this.ItemToEdit = new Video();
                    break;
                default:
                    throw new ArgumentException("Invalid MediaType");
            }
        }

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="personType">The type of person to edit</param>
        public EditWorkspaceViewModel(PersonTypes personType)
        {
            switch (personType)
            {
                case PersonTypes.Author:
                    this.ItemToEdit = new Author();
                    break;
                case PersonTypes.Director:
                    this.ItemToEdit = new Director();
                    break;
                case PersonTypes.Producer:
                    this.ItemToEdit = new Producer();
                    break;
                default:
                    throw new ArgumentException("Invalid PersonType");
            }
        }

        /// <summary>
        /// Initializes a new instance of the EditWorkspaceViewModel class
        /// </summary>
        /// <param name="ItemToEdit">The element to be edited</param>
        public EditWorkspaceViewModel(Media itemToEdit)
            : this("Edit " + itemToEdit.GetType().Name + " " + itemToEdit.MediaId)
        {
            this.ItemToEdit = itemToEdit;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the media to edit
        /// </summary>
        public object ItemToEdit
        {
            get
            {
                return this.mItemToEdit;
            }

            protected set
            {
                if (this.ItemToEdit != value)
                {
                    this.mItemToEdit = value;
                    this.RaisePropertyChanged(ItemToEditPropertyName);
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
                return (Media)this.ItemToEdit;
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

        /// <summary>
        /// Gets or sets a command to add a person to the media item
        /// </summary>
        public RelayCommand<Person> AddPersonCommand
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a command to remove a person to the media item
        /// </summary>
        public RelayCommand<Person> RemovePersonCommand
        {
            get;
            protected set;
        }

        #endregion Commands
    }
}
