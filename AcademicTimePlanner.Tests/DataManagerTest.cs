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
                Assert.IsTrue(_dataManager.TogglProjects.Contains(project));
                Assert.IsFalse(_dataManager.DeletedTogglProjectIds.Contains(project.TogglId));
            }
        }

        [TestMethod]
        public void NonEmptyTogglProjectsIsUpdatedCorrectly()
        {
            var testTogglProjects = TestTogglProject.GetTestTogglProject();
            _dataManager.TogglProjects.Add(testTogglProjects[0]);

            _dataManager.UpdateTogglData(testTogglProjects);

            foreach (var project in testTogglProjects)
            {
                Assert.IsTrue(_dataManager.TogglProjects.Contains(project));
                Assert.IsFalse(_dataManager.DeletedTogglProjectIds.Contains(project.TogglId));
            }
        }

        [TestMethod]
        public void TogglProjectsIsUpdatedCorrectlyWithDeletedProject()
        {
            var testTogglProjects = TestTogglProject.GetTestTogglProject();
            _dataManager.UpdateTogglData(testTogglProjects);

            _dataManager.UpdateTogglData(new List<TogglProject> { testTogglProjects[1] });

            foreach (var project in testTogglProjects)
            {
                Assert.IsTrue(_dataManager.TogglProjects.Contains(project));
            }
            Assert.IsFalse(_dataManager.DeletedTogglProjectIds.Contains(testTogglProjects[1].TogglId));
            Assert.IsTrue(_dataManager.DeletedTogglProjectIds.Contains(testTogglProjects[0].TogglId));
        }

        [TestMethod]
        public void UpdateTogglProjectsDoesNotDuplicateShiftedEntries()
        {
            var shiftedTestEntry = new TogglEntrySum(DateTime.Today, 3, 1, -1);
            var remainingTestEntry = new TogglEntrySum(DateTime.Today.AddDays(-3), 2, 2, -1);

            var testTogglProject1 = new TogglProject(3, "Test project 1");
            var testTogglProject2 = new TogglProject(4, "Test project 2");

            testTogglProject1.TogglEntrySums.Add(shiftedTestEntry);
            testTogglProject1.TogglEntrySums.Add(remainingTestEntry);

            _dataManager.UpdateTogglData(new List<TogglProject> { testTogglProject1, testTogglProject2 });

            Assert.AreEqual(2, _dataManager.TogglProjects.Count);
            Assert.AreEqual(2, _dataManager.TogglProjects.Find(togglProject => togglProject.TogglId == testTogglProject1.TogglId)!.TogglEntrySums.Count);
            Assert.AreEqual(0, _dataManager.TogglProjects.Find(togglProject => togglProject.TogglId == testTogglProject2.TogglId)!.TogglEntrySums.Count);

            var updatedTestTogglProject1 = new TogglProject(3, "Test project 1");
            var updatedTestTogglProject2 = new TogglProject(4, "Test project 2");

            updatedTestTogglProject1.TogglEntrySums.Add(remainingTestEntry);
            updatedTestTogglProject2.TogglEntrySums.Add(shiftedTestEntry);

            _dataManager.UpdateTogglData(new List<TogglProject> { updatedTestTogglProject1, updatedTestTogglProject2 });

            Assert.AreEqual(2, _dataManager.TogglProjects.Count);
            Assert.AreEqual(1, _dataManager.TogglProjects.Find(togglProject => togglProject.TogglId == testTogglProject1.TogglId)!.TogglEntrySums.Count);
            Assert.AreEqual(1, _dataManager.TogglProjects.Find(togglProject => togglProject.TogglId == testTogglProject2.TogglId)!.TogglEntrySums.Count);
        }

        [TestMethod]
        public void GetProjectsDataReturnsCorrectChartDataWithEmptyDataCollections()
        {
            var chartData = _dataManager.GetProjectsData();

            Assert.IsNotNull(chartData);
            Assert.AreEqual(0, chartData.PlanProjects.Count);
            Assert.AreEqual(0, chartData.LinkedTogglProjects.Count);
        }

        [TestMethod]
        public void GetProjectsDataReturnsCorrectChartDataWithNonEmptyDataCollections()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            var testPlanProject = new PlanProject(new Dictionary<long, double> { { testTogglProject.TogglId, 1.0 } }, "Test");
            _dataManager.PlanProjects.Add(testPlanProject);
            _dataManager.TogglProjects.Add(testTogglProject);

            var chartData = _dataManager.GetProjectsData();

            Assert.IsNotNull(chartData);
            Assert.IsTrue(chartData.PlanProjects.Contains(testPlanProject));
            Assert.IsTrue(chartData.LinkedTogglProjects.Contains(testTogglProject));
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
            var testPlanProject = new PlanProject(new Dictionary<long, double> { { testTogglProject.TogglId, 1.0 } }, "Test");
            _dataManager.PlanProjects.Add(testPlanProject);
            _dataManager.TogglProjects.Add(testTogglProject);

            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(1, loadOverview.Count);
            Assert.AreEqual(testTogglProject.Name, loadOverview[0].TogglProjectName);
            Assert.IsFalse(loadOverview[0].IsDeleted);
            Assert.AreEqual(testPlanProject.Name, loadOverview[0].PlanProjectNames);
        }

        [TestMethod]
        public void GetTogglLoadOverviewReturnsCorrectOverviewWithoutAssociatedPlanProject()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            _dataManager.TogglProjects.Add(testTogglProject);

            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(1, loadOverview.Count);
            Assert.AreEqual(testTogglProject.Name, loadOverview[0].TogglProjectName);
            Assert.IsFalse(loadOverview[0].IsDeleted);
            Assert.AreEqual(DataManager.NoAssociatedPlanProjectName, loadOverview[0].PlanProjectNames);
        }

        [TestMethod]
        public void GetTogglLoadOverviewReturnsCorrectOverviewWithDeletedTogglProject()
        {
            var testTogglProject = TestTogglProject.GetTestTogglProject()[0];
            var testPlanProject = new PlanProject(new Dictionary<long, double> { { testTogglProject.TogglId, 1.0 } }, "Test");
            _dataManager.PlanProjects.Add(testPlanProject);
            _dataManager.TogglProjects.Add(testTogglProject);
            _dataManager.DeletedTogglProjectIds.Add(testTogglProject.TogglId);

            var loadOverview = _dataManager.GetTogglLoadOverview();

            Assert.AreEqual(1, loadOverview.Count);
            Assert.AreEqual(testTogglProject.Name, loadOverview[0].TogglProjectName);
            Assert.IsTrue(loadOverview[0].IsDeleted);
            Assert.AreEqual(testPlanProject.Name, loadOverview[0].PlanProjectNames);
        }

        [TestMethod]
        public void UpdateTogglDictionaryInPlanProjectsUpdatesThePercentagesInPlanProjectsCorrect()
        {
            var testTogglProject_1 = TestTogglProject.GetTestTogglProject()[0];
            var testTogglProject_2 = TestTogglProject.GetTestTogglProject()[1];

            var testPlanProject_1 = new PlanProject(new Dictionary<long, double> { { testTogglProject_1.TogglId, 1.0 }, { testTogglProject_2.TogglId, 1.0 } }, "Test_1");
            var expectedPlanProject_1 = new PlanProject(new Dictionary<long, double> { { testTogglProject_1.TogglId, 1.0 }, { testTogglProject_2.TogglId, 0.5 } }, "Test_1");
            var testPlanProject_2 = new PlanProject(new Dictionary<long, double> { { testTogglProject_2.TogglId, 1.0 } }, "Test_2");
            var expectedPlanProject_2 = new PlanProject(new Dictionary<long, double> { { testTogglProject_2.TogglId, 0.5 } }, "Test_2");

            PlanEntry planEntry = new PlanEntry("test", DateTime.Today, DateTime.Today.AddDays(1), 1);

            testPlanProject_1.AddPlanEntry(planEntry);
            testPlanProject_2.AddPlanEntry(planEntry);

            _dataManager.PlanProjects.Add(testPlanProject_1);
            _dataManager.PlanProjects.Add(testPlanProject_2);

            _dataManager.TogglProjects.Add(testTogglProject_1);
            _dataManager.TogglProjects.Add(testTogglProject_2);

            _dataManager.UpdateTogglDictionaryInPlanProjects();

            foreach (var i in expectedPlanProject_1.TogglProjectIds.Keys)
            {
                Assert.AreEqual(expectedPlanProject_1.TogglProjectIds[i], testPlanProject_1.TogglProjectIds[i]);
            }
            foreach (var i in expectedPlanProject_2.TogglProjectIds.Keys)
            {
                Assert.AreEqual(expectedPlanProject_2.TogglProjectIds[i], testPlanProject_2.TogglProjectIds[i]);
            }
        }
    }
}
