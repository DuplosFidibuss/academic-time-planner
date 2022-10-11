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
        private DateOnly startDate = new DateOnly(2022, 1, 1);
        private DateOnly endDate = new DateOnly(2022, 1, 2);
        private PlanEntry testPlanEntry;
        private PlanTask testPlanTask;
        private PlanProject testPlanProject;
        private importExportJSON ieJson = new importExportJSON();

        [TestInitialize]
        public void initialize() 
        {
            testPlanEntry = new PlanEntry("testPlanEntry_1", startDate, endDate, 60000);

            testPlanTask = new PlanTask(1,"testTask_1");
            testPlanTask.AddPlanEntry(testPlanEntry);

            testPlanProject = new PlanProject(1, "testProject_1");
            testPlanProject.AddPlanTask(testPlanTask);
        }

        [TestMethod]
        public void testSafeJson()
        {
            ieJson.safeJson(testPlanProject);
        }
    }
}
