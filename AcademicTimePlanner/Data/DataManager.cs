using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        public const string NoAssociatedPlanProjectName = "No plan project associated";

        public List<PlanProject> PlanProjects { get; set; }

        public List<TogglProject> TogglProjects { get; set; }

        public List<long> DeletedTogglProjectIds { get; set; }

        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            PlanProjects = new List<PlanProject>();
            TogglProjects = new List<TogglProject>();
            DeletedTogglProjectIds = new List<long>();
        }

        public void UpdateTogglData(List<TogglProject> togglProjects)
        {
            var currentTogglProjects = new List<TogglProject>(TogglProjects);
            TogglProjects.Clear();
            DeletedTogglProjectIds.Clear();
            foreach (var project in togglProjects)
            {
                TogglProjects.Add(project);
            }

            foreach (var currentProject in currentTogglProjects)
            {
                if (!togglProjects.Any(project => project.TogglId == currentProject.TogglId))
                {
                    TogglProjects.Add(currentProject);
                    DeletedTogglProjectIds.Add(currentProject.TogglId);
                }
            }
            Console.WriteLine(DeletedTogglProjectIds.Count);
        }

        public ChartData GetChartData()
        {
            // This is for chart display test purposes.
            //TogglProjects.Clear();
            //TestTogglProject.GetTestTogglProject().ForEach(project => TogglProjects.Add(project));
            return new ChartData(TogglProjects, PlanProjects);
        }

        public List<TogglLoadOverviewData> GetTogglLoadOverview()
        {
            var loadOverview = new List<TogglLoadOverviewData>();
            foreach (var togglProject in TogglProjects)
            {
                var planProject = PlanProjects.Find(project => project.TogglProjectId == togglProject.TogglId);
                var planProjectName = planProject != null ? planProject.Name : NoAssociatedPlanProjectName;
                var projectOverviewData = new TogglLoadOverviewData(togglProject.Name, DeletedTogglProjectIds.Contains(togglProject.TogglId), planProjectName);
                loadOverview.Add(projectOverviewData);
            }
            return loadOverview;
        }

        public void UpdatePlanningData(List<PlanProject> planProjects)
        {
            PlanProjects.Clear();
            PlanProjects.AddRange(planProjects);
        }
    }
}
