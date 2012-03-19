namespace Sweng500.Pml.ViewModel.Workspaces
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Data;
    using log4net;
    using Sweng500.Pml.DataAccessLayer;
using System;

    /// <summary>
    /// The workspace used to perform the search and display the results
    /// </summary>
    public class SearchWorkspaceViewModel : WorkspaceViewModel
    {
        #region Statics

        /// <summary>
        /// Property name for the AwaitingResponse property
        /// </summary>
        public const string AwaitingResponsePropertyName = "AwaitingResponse";

        /// <summary>
        /// Class logger
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion Statics

        #region Fields

        /// <summary>
        /// The collection of search results
        /// </summary>
        private ObservableCollection<Media> mSearchResults = new ObservableCollection<Media>();

        /// <summary>
        /// A value indicating whether a response has been received
        /// </summary>
        private bool mAwaitingResponse = true;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SearchWorkspaceViewModel class
        /// </summary>
        /// <param name="title">The title to search for</param>
        public SearchWorkspaceViewModel(string title)
            : base("Search by title of " + title)
        {
            this.Results = new ListCollectionView(this.mSearchResults);
        }

        /// <summary>
        /// Initializes a new instance of the SearchWorkspaceViewModel class
        /// </summary>
        /// <param name="keywords">The list of keywords to search for</param>
        public SearchWorkspaceViewModel(IList<string> keywords)
            : base("Search by keywords of ")
        {
            keywords.ToList().ForEach(k => this.Name += " " + k);
            this.Name = this.Name.Trim();

            this.Results = new ListCollectionView(this.mSearchResults);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the search results
        /// </summary>
        public ICollectionView Results
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a response was received
        /// </summary>
        public bool AwaitingResponse
        {
            get
            {
                return this.mAwaitingResponse;
            }

            protected set
            {
                if (value != this.AwaitingResponse)
                {
                    this.mAwaitingResponse = value;
                    this.RaisePropertyChanged(AwaitingResponsePropertyName);
                }
            }
        }

        #endregion Properties
    }
}
