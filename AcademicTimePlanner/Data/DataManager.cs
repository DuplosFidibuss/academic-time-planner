using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        public List<Budget> Budgets { get; set; }
        public List<PlanProject> PlanProjects { get; set; }

        private Dictionary<TogglProject, bool> _togglProjects;

        /// <summary>
        /// This class holds all data used and created by the application.
        /// </summary>
        public DataManager()
        {
            Budgets = new List<Budget>();
            PlanProjects = new List<PlanProject>();
            _togglProjects = new Dictionary<TogglProject, bool>();
        }

        public void UpdateTogglData(List<TogglProject> togglProjects)
        {
            var currentTogglProjects = _togglProjects.Keys.ToList();
            _togglProjects.Clear();
            togglProjects.ForEach(togglProject => _togglProjects[togglProject] = true);

            foreach (var currentProject in currentTogglProjects)
            {
                if (!togglProjects.Any(project => project.TogglId == currentProject.TogglId))
                    _togglProjects[currentProject] = false;
            }
        }

        public ChartData GetChartData()
        {
            _togglProjects.Clear();
            TestTogglProject.GetTestTogglProject().ForEach(project => _togglProjects.Add(project, true));
            return new ChartData(_togglProjects.Keys.ToList(), PlanProjects);
        }
    }
}
