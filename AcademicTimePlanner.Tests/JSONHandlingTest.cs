using AcademicTimePlanner.Data.ApplicationData.Plan;
using AcademicTimePlanner.JSONHandling;

namespace AcademicTimePlanner.Tests
{
    // This class only exists to test whether the JSON conversion works as intended. The application itself never accesses local files directly.
    [TestClass]
    public class JSONHandlingTest
    {
        private DateTime _startDate = new DateTime(2022, 10, 16);
        private DateTime _endDate = new DateTime(2022, 10, 22);
        private DateTime _endDate2 = new DateTime(2022, 11, 15);

        private PlanEntry _testPlanEntry_1;
        private PlanEntry _testPlanEntry_2;
        private PlanEntryRepetition _testPlanEntryRepetition_1;
        private PlanEntryRepetition _testPlanEntryRepetition_2;
        private PlanProject _testPlanProject_1;
        private PlanProject _testPlanProject_2;
        private JsonFileHandler _fileHandler = new JsonFileHandler();

        [TestInitialize]
        public void initialize()
        {
            _testPlanEntry_1 = new PlanEntry("testPlanEntry_1", _startDate, _endDate, 2);
            _testPlanEntry_2 = new PlanEntry("testPlanEntry_2", _startDate, _endDate, 2);

            _testPlanEntryRepetition_1 = new PlanEntryRepetition("testPlanEntryRepetition_1", _startDate, _endDate2, 7, 2);
            _testPlanEntryRepetition_2 = new PlanEntryRepetition("testPlanEntryRepetition_2", _startDate, _endDate2, 14, 4);

            _testPlanProject_1 = new PlanProject(new Dictionary<long, double> { { (long) 1, 1.0 }, { (long) 2, 0.5 } }, "testProject_1");
            _testPlanProject_1.AddPlanEntry(_testPlanEntry_1);
            _testPlanProject_1.AddPlanEntryRepetition(_testPlanEntryRepetition_1);

            _testPlanProject_2 = new PlanProject(new Dictionary<long, double> { { (long) 2, 0.5 } }, "testProject_2");
            _testPlanProject_2.AddPlanEntry(_testPlanEntry_2);
            _testPlanProject_2.AddPlanEntryRepetition(_testPlanEntryRepetition_2);
        }

        [TestMethod]
        public void testSafeJson()
        {
            _fileHandler.saveJson(_testPlanProject_1);
            _fileHandler.saveJson(_testPlanProject_2);
        }

        [TestMethod]
        [Ignore]
        public void testLoadJson()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string dataPath = directory + @"\AcademicTimePlanner.Tests\JSON_Files\PlanProject.json";

            var loadedPlanProject = _fileHandler.loadJson(dataPath);

            var oldId = _testPlanProject_1.Id;
            _testPlanProject_1.Id = loadedPlanProject.Id;
            Assert.AreNotEqual(oldId, _testPlanProject_1.Id);
            Assert.AreEqual(_testPlanProject_1.Name, loadedPlanProject.Name);
        }

        [TestMethod]
        [Ignore]
        public void TestLoadTogglJson()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string dataPath = directory + @"\AcademicTimePlanner.Tests\JSON_Files\TogglData.json";

            var togglDetailResponse = _fileHandler.LoadTogglJson(dataPath);

            Assert.IsNotNull(togglDetailResponse);
            Assert.IsNotNull(togglDetailResponse.Data);
            Assert.AreEqual(3, togglDetailResponse.Data.Count);
        }
    }
}
