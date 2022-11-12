using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Tests
{
    [TestClass]
    public class DataManagerTest
    {
        private DataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = new DataManager();
        }

        [TestMethod]
        public void DataCollectionsAreInitializedCorrectlyOnCreation()
        {
            Assert.IsTrue(_dataManager.Budgets.Count == 0);
            Assert.IsTrue(_dataManager.PlanProjects.Count == 0);
            Assert.IsTrue(_dataManager.TogglProjects.Count == 0);
        }

        [TestMethod]
        public void EmptyTogglProjectsIsUpdatedCorrectly()
        {
            var testTogglProjects = TestTogglProject.GetTestTogglProject();

            _dataManager.UpdateTogglData(testTogglProjects);

            foreach (var project in testTogglProjects)
            {
                Assert.IsTrue(_dataManager.TogglProjects[project]);
            }
        }

        [TestMethod]
        public void NonEmptyTogglProjectsIsUpdatedCorrectly()
        {
            var testTogglProjects = TestTogglProject.GetTestTogglProject();
            _dataManager.TogglProjects[testTogglProjects[0]] = true;

            _dataManager.UpdateTogglData(testTogglProjects);

            foreach (var project in testTogglProjects)
            {
                Assert.IsTrue(_dataManager.TogglProjects[project]);
            }
        }

        [TestMethod]
        public void TogglProjectsIsUpdatedCorrectlyWithDeletedProject()
        {
            var testTogglProjects = TestTogglProject.GetTestTogglProject();
            _dataManager.UpdateTogglData(testTogglProjects);

            _dataManager.UpdateTogglData(new List<TogglProject> { testTogglProjects[1] });

            Assert.IsFalse(_dataManager.TogglProjects[testTogglProjects[0]]);
            Assert.IsTrue(_dataManager.TogglProjects[testTogglProjects[1]]);
        }
    }
}
