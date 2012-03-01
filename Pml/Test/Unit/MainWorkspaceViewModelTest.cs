namespace Sweng500.Pml.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.ViewModel;
    using Sweng500.Pml.ViewModel.Workspaces;

    /// <summary>
    /// This is a test class for MainWorkspaceViewModelTest and is intended
    /// to contain all MainWorkspaceViewModelTest Unit Tests
    /// </summary>
    [TestClass]
    public class MainWorkspaceViewModelTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get;
            set;
        }

        /// <summary>
        /// A test for MainWorkspaceViewModel Constructor
        /// </summary>
        [TestMethod]
        public void MainWorkspaceViewModelConstructorTest()
        {
            MainWorkspaceViewModel target = new MainWorkspaceViewModel();
            Assert.IsNotNull(target.CloseWorkspaceCommand);
            Assert.IsNotNull(target.SelectedWorkspace);
            
            // Verify workspaces
            Assert.IsNotNull(target.Workspaces);

            // Verify commands are set
            Assert.IsNotNull(GlobalCommands.Instance.AddMediaItemCommand);
            Assert.IsNotNull(GlobalCommands.Instance.EditMediaItemCommand);
        }

        /// <summary>
        /// A test for SelectedWorkspace
        /// </summary>
        [TestMethod]
        public void SelectedWorkspaceTest()
        {
            MainWorkspaceViewModel target = new MainWorkspaceViewModel(); // TODO: Initialize to an appropriate value
            
            // Verify auto select works
            WorkspaceViewModel actual;
            actual = target.SelectedWorkspace;
            Assert.IsNotNull(actual);

            // Verify selected workspace with an added workspace
            InventoryWorkspaceViewModel workspace = new InventoryWorkspaceViewModel();
            workspace.Name = "Second Inventory";
            workspace.IsSelected = true;
            actual = target.SelectedWorkspace;
            Assert.IsNotNull(actual);
            Assert.ReferenceEquals(actual, workspace);
        }
    }
}
