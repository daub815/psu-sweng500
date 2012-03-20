namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using log4net;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// The workspace used for editing a person
    /// </summary>
    public class EditPersonWorkspaceViewModel : EditWorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EditPersonWorkspaceViewModel class
        /// </summary>
        /// <param name="personType">The type of person to edit</param>
        public EditPersonWorkspaceViewModel(PersonTypes personType)
            : this("Add " + personType.ToString())
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
        /// Initializes a new instance of the EditPersonWorkspaceViewModel class
        /// </summary>
        /// <param name="itemToEdit">The element to be edited</param>
        public EditPersonWorkspaceViewModel(Person itemToEdit)
            : this("Edit " + itemToEdit.GetType().Name + " " + itemToEdit.PersonId)
        {
            this.ItemToEdit = itemToEdit;
        }

        /// <summary>
        /// Initializes a new instance of the EditPersonWorkspaceViewModel class
        /// </summary>
        /// <param name="name">The name of the workspace</param>
        protected EditPersonWorkspaceViewModel(string name)
            : base(name)
        {
            this.SaveCommand = new RelayCommand(
                () =>
                {
                    var crudService = Repository.Instance.ServiceLocator.GetInstance<ICrudService>();

                    try
                    {
                        if (null == ((Person)this.ItemToEdit).EntityKey)
                        {
                            this.ItemToEdit = crudService.Add((Person)this.ItemToEdit);
                            DataStore.Instance.PersonCollection.Add((Person)this.ItemToEdit);
                        }
                        else
                        {
                            this.ItemToEdit = crudService.Update((Person)this.ItemToEdit);
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
                    if (null != ((Person)this.ItemToEdit).EntityKey)
                    {
                        int index = DataStore.Instance.PersonCollection.IndexOf((Person)this.ItemToEdit);
                        if (-1 != index)
                        {
                            this.ItemToEdit = DataStore.Instance.PersonCollection[index];

                            //// TODO: Raise string.Empty for all properties of ItemToEdit
                        }
                    }
                },
                () =>
                    {
                        return
                            null != ((Person)this.ItemToEdit).EntityKey;
                    });
        }

        #endregion Constructors
    }
}
