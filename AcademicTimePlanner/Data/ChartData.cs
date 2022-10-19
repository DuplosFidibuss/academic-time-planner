using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class ChartData
    {
        public ChartData(List<TogglProject> togglProjects, List<PlanProject> planProjects)
        {
            TogglProjects = togglProjects;
            PlanProjects = planProjects;
        }

        public List<TogglProject> TogglProjects { get; set; }

        public List<PlanProject> PlanProjects { get; set; }

        public int TotalTrackedTime { get; set; }

        public int TotalPlannedTime { get; set; }

        public int RemainingTime { get; set; }
    }
}
