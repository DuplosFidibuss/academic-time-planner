using AcademicTimePlanner.DataMapping.Budget;
using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class DataManager
    {
        private DataManager()
        {
            Budgets = new LinkedList<Budget>();
            PlanProjects = new LinkedList<PlanProject>();
            TogglProjects = new LinkedList<TogglProject>();
        }

        /// <summary>
        /// This class holds all data used by the application.
        /// </summary>
        public static readonly DataManager Instance = new();

        public LinkedList<Budget> Budgets { get; }
        public LinkedList<PlanProject> PlanProjects { get; }
        public LinkedList<TogglProject> TogglProjects { get; }
    }
}
