using AcademicTimePlanner.DataMapping.Plan;
using AcademicTimePlanner.DataMapping.Toggl;

namespace AcademicTimePlanner.Data
{
    public class ChartData
    {
        /// <summary>
        /// This class holds all data needed for the graphical overview of planning and tracked time data.
        /// </summary>
        /// <param name="togglProjects"></param>
        /// <param name="planProjects"></param>
        public ChartData(List<TogglProject> togglProjects, List<PlanProject> planProjects)
        {
            TogglProjects = togglProjects;
            PlanProjects = planProjects;
            TotalTrackedTime = (from togglProject in togglProjects select togglProject.GetTotalTime()).Sum();
            TotalPlannedTime = (from planProject in planProjects select planProject.GetTotalTime()).Sum();
            RemainingTime = TotalPlannedTime - TotalTrackedTime;
        }

        public List<TogglProject> TogglProjects { get; }

        public List<PlanProject> PlanProjects { get; }

        public int TotalTrackedTime { get; }

        public int TotalPlannedTime { get; }

        public int RemainingTime { get; }
    }
}
