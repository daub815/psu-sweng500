namespace Sweng500.Pml.ViewModel
{
    using GalaSoft.MvvmLight.Command;
    using Sweng500.Pml.DataAccessLayer;

    /// <summary>
    /// Contains the global commands for the application
    /// </summary>
    public class GlobalCommands
    {
        #region Statics

        /// <summary>
        /// The singleton instance
        /// </summary>
        private static readonly GlobalCommands StaticInstance = new GlobalCommands();

        #endregion Statics

        #region Fields

        /// <summary>
        /// Static instance of the edit media command
        /// </summary>
        private RelayCommand<Media> mEditMediaItemCommand;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the GlobalCommands class
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Explicit static constructor to tell C# compiler not to mark type as beforefieldinit")]
        static GlobalCommands()
        {
        }

        /// <summary>
        /// Prevents a default instance of the GlobalCommands class from being created
        /// </summary>
        private GlobalCommands()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the singleton instance of the GlobalCommands
        /// </summary>
        public static GlobalCommands Instance
        {
            get
            {
                return StaticInstance;
            }
        }

        /// <summary>
        /// Gets or sets the command to edit a media item
        /// </summary>
        public RelayCommand<Media> EditMediaItemCommand
        {
            get
            {
                return this.mEditMediaItemCommand;
            }

            set
            {
                // Only allow the assignment once
                if (null == this.mEditMediaItemCommand &&
                    null != value)
                {
                    this.mEditMediaItemCommand = value;
                }
            }
        }

        #endregion Properties

        /// <summary>
        /// Gets the singleton instance of the GlobalCommands
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static GlobalCommands GetInstance()
        {
            return Instance;
        }
    }
}
