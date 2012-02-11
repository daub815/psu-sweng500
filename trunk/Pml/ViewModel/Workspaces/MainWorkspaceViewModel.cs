namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System;
    using System.Collections.ObjectModel;

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
            TMedia media1 = new TMedia("bob to nowhere", 3663, "This is a book about a trip to no-where");
            media1.Publisher = "nice publisher";
            TMedia media2 = new TMedia("another", 736, "just another book");

            TVideo video1 = new TVideo();
            video1.Title = "Amazing Trip";
            video1.Publisher = "videoPub";
            video1.BoardRating = "PG-13";
            video1.DirectorName = "Frank Capra";
            video1.MediaDescription = "thrilling exploit in 3D";

            // Create the collection
            this.TheCollection = new ObservableCollection<TMedia>();
            this.TheCollection.Add(media1);
            this.TheCollection.Add(media2);
            this.TheCollection.Add(video1);

            // Create the collection
//            this.TheCollection = new ObservableCollection<string>();

//            this.TheCollection.Add(Guid.NewGuid().ToString());
//            this.TheCollection.Add("here is my test item");
//            this.TheCollection.Add(Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Gets or sets the collection
        /// </summary>
        /// <remarks>No need to raise this property changed event because we don't do a swap of values.  We modify this one.</remarks>
        public ObservableCollection<TMedia> TheCollection
        {
            get;
            set;
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
                this.RaisePropertyChanged(TempEditorPropertyName);
            }
        }
    }
}
