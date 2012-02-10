namespace Sweng500.Pml.Client
{
    using System.Windows;
    using Sweng500.Pml.ViewModel.Workspaces;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Occurs on start up of the application and creates the main window
        /// </summary>
        /// <param name="e">The arguments on startup</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create the main window and set the datacontext
            this.MainWindow = new MainWindow
            {
                DataContext = new MainWorkspaceViewModel()
            };

            this.MainWindow.Show();
        }
    }
}
