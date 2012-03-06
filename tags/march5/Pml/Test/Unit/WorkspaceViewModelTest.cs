namespace Sweng500.Pml.Test.Unit
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Sweng500.Pml.ViewModel.Workspaces;

    /// <summary>
    /// This is a test class for WorkspaceViewModelTest and is intended
    /// to contain all WorkspaceViewModelTest Unit Tests
    /// </summary>
    [TestClass]
    public class WorkspaceViewModelTest
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
        /// A test for WorkspaceViewModel Constructor
        /// </summary>
        [TestMethod]
        public void WorkspaceViewModelConstructorTest()
        {
            string workspaceName = "My Workspace";
            WorkspaceViewModel target = new WorkspaceViewModel(workspaceName);
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Name, workspaceName);
            Assert.IsNotNull(target.CloseWorkspaceCommand);
            Assert.IsFalse(target.IsOpen);
            Assert.IsFalse(target.IsSelected);
        }

        /// <summary>
        /// A test for WorkspaceViewModel Constructor
        /// </summary>
        [TestMethod]
        public void WorkspaceViewModelConstructorTest1()
        {
            WorkspaceViewModel target = new WorkspaceViewModel();
            Assert.IsNotNull(target);
            Assert.AreEqual(target.Name, null);
            Assert.IsNotNull(target.CloseWorkspaceCommand);
            Assert.IsFalse(target.IsOpen);
            Assert.IsFalse(target.IsSelected);
        }

        /// <summary>
        /// A test for CloseWorkspaceCommand
        /// </summary>
        [TestMethod]
        [DeploymentItem("Sweng500.Pml.ViewModel.dll")]
        public void CloseWorkspaceCommandTest()
        {
            WorkspaceViewModel target = new WorkspaceViewModel();
            Assert.IsNotNull(target);
            Assert.IsNotNull(target.CloseWorkspaceCommand);

            // Verify can execute always returns true
            Assert.IsTrue(target.CloseWorkspaceCommand.CanExecute(null));
            Assert.IsTrue(target.CloseWorkspaceCommand.CanExecute(10));

            // Verify the execute works
            target.IsOpen = true;
            target.CloseWorkspaceCommand.Execute(null);
            Assert.IsFalse(target.IsOpen);

            // Verify it stays closed
            target.CloseWorkspaceCommand.Execute(null);
            Assert.IsFalse(target.IsOpen);
        }

        /// <summary>
        /// A test for IsOpen
        /// </summary>
        [TestMethod]
        public void IsOpenTest()
        {
            WorkspaceViewModel target = new WorkspaceViewModel();
            bool expected = false; 
            bool actual;
            target.IsOpen = expected;
            actual = target.IsOpen;
            Assert.AreEqual(expected, actual);

            expected = true;
            target.IsOpen = expected;
            actual = target.IsOpen;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for IsSelected
        /// </summary>
        [TestMethod]
        public void IsSelectedTest()
        {
            WorkspaceViewModel target = new WorkspaceViewModel();
            bool expected = false;
            bool actual;
            target.IsOpen = expected;
            actual = target.IsSelected;
            Assert.AreEqual(expected, actual);

            expected = true;
            target.IsSelected = expected;
            actual = target.IsSelected;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for Name
        /// </summary>
        [TestMethod]
        public void NameTest()
        {
            WorkspaceViewModel target = new WorkspaceViewModel();
            string expected = "My Workspace";
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);

            expected = null;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);

            expected = string.Empty;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
        }
    }
}
