using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.JSONHandling;

namespace AcademicTimePlanner.Tests
{
    [TestClass]
    public class JSONHandlingTest
    {
        private DateTime startDate = new DateTime(2022, 10, 16);
        private DateTime endDate = new DateTime(2022, 10, 22);
        private DateTime endDate2 = new DateTime(2022, 11, 15);

        private PlanEntry testPlanEntry_1;
        private PlanEntry testPlanEntry_2;
        private PlanEntryRepetition testPlanEntryRepetition_1;
        private PlanEntryRepetition testPlanEntryRepetition_2;
        private PlanProject testPlanProject_1;
        private PlanProject testPlanProject_2;
        private JsonFileHandler ieJson = new JsonFileHandler();

        [TestInitialize]
        public void initialize()
        {
            testPlanEntry_1 = new PlanEntry("testPlanEntry_1", startDate, endDate, 2);
            testPlanEntry_2 = new PlanEntry("testPlanEntry_2", startDate, endDate, 2);

            testPlanEntryRepetition_1 = new PlanEntryRepetition("testPlanEntryRepetition_1", startDate, endDate2, 7, 2);
            testPlanEntryRepetition_2 = new PlanEntryRepetition("testPlanEntryRepetition_2", startDate, endDate2, 14, 4);

            testPlanProject_1 = new PlanProject(new Dictionary<long, double> { { (long)1, 1.0 }, { (long)2, 0.5 } }, "testProject_1");
            testPlanProject_1.AddPlanEntry(testPlanEntry_1);
            testPlanProject_1.AddRepetitionEntry(testPlanEntryRepetition_1);

            testPlanProject_2 = new PlanProject(new Dictionary<long, double> { { (long)2, 0.5 } }, "testProject_2");
            testPlanProject_2.AddPlanEntry(testPlanEntry_2);
            testPlanProject_2.AddRepetitionEntry(testPlanEntryRepetition_2);
        }

        [TestMethod]
        public void testSafeJson()
        {
            ieJson.saveJson(testPlanProject_1);
            ieJson.saveJson(testPlanProject_2);
        }

        [TestMethod]
        [Ignore]
        public void testLoadJson()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string dataPath = directory + @"\AcademicTimePlanner.Tests\JSON_Files\PlanProject.json";
            PlanProject loadedPlanProject = ieJson.loadJson(dataPath);
            var oldId = testPlanProject_1.Id;
            testPlanProject_1.Id = loadedPlanProject.Id;
            Assert.AreNotEqual(oldId, testPlanProject_1.Id);
            Assert.AreEqual(testPlanProject_1.Name, loadedPlanProject.Name);
        }

    }
}
