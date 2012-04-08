namespace Sweng500.Pml.Client.Views
{
    using System.Windows.Controls;
    using Sweng500.Pml.DataAccessLayer;
    using Sweng500.Pml.ViewModel.Workspaces;

    /// <summary>
    /// Interaction logic for EditWorkspaceView.xaml
    /// </summary>
    public partial class EditWorkspaceView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the EditWorkspaceView class
        /// </summary>
        /// <remarks>Never make this protected or remove it</remarks>
        public EditWorkspaceView()
        {
            // Never call anything before this or remove it
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the value to the media object
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The arguments of the event</param>
        private void Rating_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double?> e)
        {
            // This normally can be done via binding, but the toolkit seems to not understand how to set the value
            var workspace = this.DataContext as EditMediaWorkspaceViewModel;
            if (null != workspace)
            {
                ((Media)workspace.ItemToEdit).NumberOfStars = e.NewValue.GetValueOrDefault();
            }
        }
    }
}
