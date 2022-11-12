using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        public List<Budget> Budgets { get; set; }
        public List<PlanProject> PlanProjects { get; set; }

        public Dictionary<TogglProject, bool> TogglProjects { get; set; }

        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            Budgets = new List<Budget>();
            PlanProjects = new List<PlanProject>();
            TogglProjects = new Dictionary<TogglProject, bool>();
        }

        public void UpdateTogglData(List<TogglProject> togglProjects)
        {
            var currentTogglProjects = TogglProjects.Keys.ToList();
            TogglProjects.Clear();
            togglProjects.ForEach(togglProject => TogglProjects[togglProject] = true);

            foreach (var currentProject in currentTogglProjects)
            {
                if (!togglProjects.Any(project => project.TogglId == currentProject.TogglId))
                    TogglProjects[currentProject] = false;
            }
        }

        public ChartData GetChartData()
        {
            TogglProjects.Clear();
            TestTogglProject.GetTestTogglProject().ForEach(project => TogglProjects.Add(project, true));
            return new ChartData(TogglProjects.Keys.ToList(), PlanProjects);
        }

        public List<TogglLoadOverviewData> GetTogglLoadOverview()
        {
            var loadOverview = new List<TogglLoadOverviewData>();
            foreach (var togglProject in TogglProjects.Keys)
            {
                var planProject = PlanProjects.Find(project => project.TogglProjectId == togglProject.TogglId);
                var planProjectName = planProject != null ? planProject.Name : "No plan project associated";
                var projectOverviewData = new TogglLoadOverviewData(togglProject.Name, TogglProjects[togglProject], planProjectName);
                loadOverview.Add(projectOverviewData);
            }
            return loadOverview;
        }
    }
}
