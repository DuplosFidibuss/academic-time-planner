using AcademicTimePlanner.Data;
using AcademicTimePlanner.DataMapping.Plan;
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
            Assert.AreEqual(0, _dataManager.Budgets.Count);
            Assert.AreEqual(0, _dataManager.PlanProjects.Count);
            Assert.AreEqual(0, _dataManager.TogglProjects.Count);
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

        [TestMethod]
        public void GetChartDataReturnsCorrectChartDataWithEmptyDataCollections()
        {
            var chartData = _dataManager.GetChartData();

            Assert.IsNotNull(chartData);
            Assert.AreEqual(0, chartData.PlanProjects.Count);
            Assert.AreEqual(0, chartData.TogglProjects.Count);
        }

        [TestMethod]
        public void GetChartDataReturnsCorrectChartDataWithNonEmptyDataCollections()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            var testPlanProject = new PlanProject(testTogglProject.TogglId, "Test");
            _dataManager.PlanProjects.Add(testPlanProject);
            _dataManager.TogglProjects.Add(testTogglProject, true);

            var chartData = _dataManager.GetChartData();

            Assert.IsNotNull(chartData);
            Assert.IsTrue(chartData.PlanProjects.Contains(testPlanProject));
            Assert.IsTrue(chartData.TogglProjects.Contains(testTogglProject));
        }

        [TestMethod]
        public void GetTogglLoadOverviewReturnsEmptyListWithEmptyTogglProjects()
        {
            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(0, loadOverview.Count);
        }

        [TestMethod]
        public void GetTogglLoadOverviewReturnsCorrectOverviewWithAssociatedPlanProject()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            var testPlanProject = new PlanProject(testTogglProject.TogglId, "Test");
            _dataManager.PlanProjects.Add(testPlanProject);
            _dataManager.TogglProjects.Add(testTogglProject, true);

            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(1, loadOverview.Count);
            Assert.AreEqual(testTogglProject.Name, loadOverview[0].TogglProjectName);
            Assert.IsFalse(loadOverview[0].IsDeleted);
            Assert.AreEqual(testPlanProject.Name, loadOverview[0].PlanProjectName);
        }

        [TestMethod]
        public void GetTogglLoadOverviewReturnsCorrectOverviewWithoutAssociatedPlanProject()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            _dataManager.TogglProjects.Add(testTogglProject, true);

            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(1, loadOverview.Count);
            Assert.AreEqual(testTogglProject.Name, loadOverview[0].TogglProjectName);
            Assert.IsFalse(loadOverview[0].IsDeleted);
            Assert.AreEqual(DataManager.NoAssociatedPlanProjectName, loadOverview[0].PlanProjectName);
        }

        [TestMethod]
        public void GetTogglLoadOverviewReturnsCorrectOverviewWithDeletedTogglProject()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            var testPlanProject = new PlanProject(testTogglProject.TogglId, "Test");
            _dataManager.PlanProjects.Add(testPlanProject);
            _dataManager.TogglProjects.Add(testTogglProject, false);

            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(1, loadOverview.Count);
            Assert.AreEqual(testTogglProject.Name, loadOverview[0].TogglProjectName);
            Assert.IsTrue(loadOverview[0].IsDeleted);
            Assert.AreEqual(testPlanProject.Name, loadOverview[0].PlanProjectName);
        }
    }
}
