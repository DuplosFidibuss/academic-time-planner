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
            Budgets = new LinkedList<Budget>();
            PlanProjects = new LinkedList<PlanProject>();
            TogglProjects = new LinkedList<TogglProject>();
        }

        public LinkedList<Budget> Budgets { get; }
        public LinkedList<PlanProject> PlanProjects { get; }
        public LinkedList<TogglProject> TogglProjects { get; }
    }
}
