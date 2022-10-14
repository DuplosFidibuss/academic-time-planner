using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.JSONHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicTimePlanner.Tests
{
    [TestClass]
    public class JSONHandlingTest
    {
        private DateTime startDate = new DateTime(2022, 1, 1);
        private DateTime endDate = new DateTime(2022, 1, 2);
        private DateTime endDate2 = new DateTime(2022, 1, 31);

        private PlanEntry testPlanEntry;
        private PlanEntryRepetition testPlanEntryRepetition;
        private PlanTask testPlanTask;
        private PlanProject testPlanProject;
        private JsonFileHandler ieJson = new JsonFileHandler();

        [TestInitialize]
        public void initialize() 
        {
            testPlanEntry = new PlanEntry("testPlanEntry_1", startDate, endDate, 2);

            testPlanEntryRepetition = new PlanEntryRepetition("testPlanEntryRepetition_1", startDate, endDate2, 7, 2);

            testPlanTask = new PlanTask(1,"testTask_1");
            testPlanTask.AddPlanEntry(testPlanEntry);
            testPlanTask.AddRepetitionEntry(testPlanEntryRepetition);

            testPlanProject = new PlanProject(1, "testProject_1");
            testPlanProject.AddPlanTask(testPlanTask);
        }

        [TestMethod]
        public void testSafeJson()
        {
            ieJson.saveJson(testPlanProject);
        }
        
        [TestMethod]
        public void testLoadJson()
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName; ;
            string dataPath = directory + @"\AcademicTimePlanner.Tests\JSON_Files\PlanProject.json";
            PlanProject loadedPlanProject = ieJson.loadJson(dataPath);
            Assert.AreEqual(testPlanProject.Name, loadedPlanProject.Name);
        }
        
    }
}
