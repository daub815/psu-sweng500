namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using log4net;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Workspace for editing media view model
    /// </summary>
    public class EditMediaWorkspaceViewModel : EditWorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EditMediaWorkspaceViewModel class
        /// </summary>
        /// <param name="mediaType">The type of media type to edit</param>
        public EditMediaWorkspaceViewModel(MediaTypes mediaType)
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
        /// Initializes a new instance of the EditMediaWorkspaceViewModel class
        /// </summary>
        /// <param name="itemToEdit">The element to be edited</param>
        public EditMediaWorkspaceViewModel(Media itemToEdit)
            : this("Edit " + itemToEdit.GetType().Name + " " + itemToEdit.MediaId)
        {
            this.ItemToEdit = itemToEdit;
        }

        /// <summary>
        /// Initializes a new instance of the EditMediaWorkspaceViewModel class
        /// </summary>
        /// <param name="name">The name of the workspace</param>
        protected EditMediaWorkspaceViewModel(string name)
            : base(name)
        {
            this.SaveCommand = new RelayCommand(
                () =>
                    {
                        var crudService = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

                        try
                        {
                            if (null == ((Media)this.ItemToEdit).EntityKey ||
                                ((Media)this.ItemToEdit).EntityState == System.Data.EntityState.Added)
                            {
                                this.ItemToEdit = crudService.Add((Media)this.ItemToEdit);
                                DataStore.Instance.MediaCollection.Add((Media)this.ItemToEdit);
                            }
                            else
                            {
                                this.ItemToEdit = crudService.Update((Media)this.ItemToEdit);
                            }

                            this.CloseWorkspaceCommand.Execute(null);
                        }
                        catch (Exception e)
                        {
                            log.Error("Unable to save the media.", e);
                            this.ErrorMessage = "Unable to save the media.";
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

        #endregion Constructors

        #region Commands

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
