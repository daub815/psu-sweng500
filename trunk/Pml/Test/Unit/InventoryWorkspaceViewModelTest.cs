namespace Sweng500.Pml.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.ViewModel;
    using Sweng500.Pml.ViewModel.Workspaces;    
    
    /// <summary>
    /// This is a test class for InventoryWorkspaceViewModelTest and is intended
    /// to contain all InventoryWorkspaceViewModelTest Unit Tests
    /// </summary>
    [TestClass]
    public class InventoryWorkspaceViewModelTest
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
        /// A test for InventoryWorkspaceViewModel Constructor
        /// </summary>
        [TestMethod]
        public void InventoryWorkspaceViewModelConstructorTest()
        {
            InventoryWorkspaceViewModel target = new InventoryWorkspaceViewModel();
            Assert.IsNotNull(target);
            Assert.IsNotNull(target.Media);
            Assert.ReferenceEquals(target.Media.SourceCollection, DataStore.Instance.MediaCollection);
        }
    }
}
