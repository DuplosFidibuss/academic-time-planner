using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            Budgets = new List<Budget>();
            PlanProjects = new List<PlanProject>();
            TogglProjects = new Dictionary<TogglProject, bool>();
        }

        public List<Budget> Budgets { get; set; }
        public List<PlanProject> PlanProjects { get; set; }
        public Dictionary<TogglProject, bool> TogglProjects { get; set; }

        public ChartData GetChartData()
        {
            TogglProjects.Clear();
            TestTogglProject.GetTestTogglProject().ForEach(project => TogglProjects.Add(project, true));
            return new ChartData(TogglProjects.Keys.ToList(), PlanProjects);
        }
    }
}
